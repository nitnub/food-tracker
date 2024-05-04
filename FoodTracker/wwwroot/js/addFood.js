
$(window).on('load', function () {
    listenForTabs();
    listenForDelete();
})

$(document).on("loadAddFood", function (event, data) {
    listenForTabs();
    listenForDelete();
    addExistingAliasToForm();
});
function removeFood(id) {
    $.ajax({
        url: `/Guest/Food/Delete/${id}`,
        type: 'DELETE',
        contentType: 'application/json',
        success: function (data) {
            if (data.success) {
                $(`#foodChip-${id}`).remove();
                $('.delete-food-modal').modal('hide');
                resetAllFields();
            }
        }
    })
}

function cancelRemoveFood() {
    $('.delete-food-modal').modal('hide');
}

function removeFoodConfirmation(id, foodName) {
    $('.delete-food-modal').modal('show');
    $('.modal-body').html(`Permanently delete "<b>${foodName}</b>"?`);
    $('.modal-footer').html(`
        <a onClick=cancelRemoveFood() class="btn btn-secondary mx-2">Cancel</a>
        <a onClick=removeFood(${id}) class="btn btn-danger mx-2">Delete</a>`);
}

function resetAllFields() {
    // Show and activate all fields
    $('.food-input').prop('disabled', false);
    $('.food-input-button').prop('visible', true);

    // Clear food details
    $('#foodHeader').html(`<div>Add Food</div>`)
    $('#foodId').val(0);
    $('#nameInput').val('');
    $('#vegetarianInput').prop('checked', false);
    $('#veganInput').prop('checked', false);
    $('#glutenInput').prop('checked', false);
    $('#tags').empty();
    $('#fodmapInput').val('');
    $('#submitButton').html('Add');

    // Clear FODMAP card
    resetFodmapCard();
}

function clickVegan() {
    if ($('#veganInput').prop('checked')) {
        $('#vegetarianInput').prop('checked', true);
    }
}

function clickVegetarian() {
    if (!$('#vegitarianInput').prop('checked')) {
        $('#veganInput').prop('checked', false);
    }
}

function listenForTabs() {
    let input = document.getElementById('input-tag');
    input && input.addEventListener('keydown', function (e) {

        // Check if the key pressed is 'Enter'
        if (e.key === 'Enter') {
            e.preventDefault();

            foodId = e.target.attributes.foodid.value;

            const tag = document.createElement('li');
            tag.id = `alias-${foodId}-new${Date.now()}`;
            tag.className = "alias-item-new";

            const tagContent = input.value.trim();
            if (tagContent !== '') {

                tag.innerText = tagContent;
                tag.innerHTML += `
                    <svg class="alias-delete xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-lg" viewBox="0 0 16 16">
                        <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/>
                    </svg>`;

                tags.appendChild(tag);
                input.value = '';

                $('#foodAliasList').html("");

                addExistingAliasToForm(foodId);

                //$('#tags').children().each(
                //    function (i) {

                //        const div = createAliasDiv(i, foodId, this.innerText);
                //        //const div = document.createElement('div');
                //        //div.innerHTML += `
                //        //    <label hidden for="Food_Aliases_${i}_"></label>
                //        //    <input hidden value="${i}" type="number" data-val="true" data-val-required="The Id field is required." id="Food_Aliases_${i}__Id" name="Food.Aliases[${i}].Id">
                //        //    <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].Id">
                //        //    <input hidden value="${this.innerText}" type="text" data-val="true" data-val-required="The Alias field is required." id="Food_Aliases_${i}__Alias" name="Food.Aliases[${i}].Alias">
                //        //    <input hidden value="${foodId}" type="number" data-val="true" data-val-required="The FoodId field is required." id="Food_Aliases_${i}__FoodId" name="Food.Aliases[${i}].FoodId">
                //        //    <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].FoodId">`

                //        document.getElementById('foodAliasList').appendChild(div);
                //    }
                //)
            }
        }
    });
}



function addExistingAliasToForm(foodId = 0) {
    $('#tags').children().each(
        function (i) {

            const div = createAliasDiv(i, foodId, this.innerText);
            //const div = document.createElement('div');
            //div.innerHTML += `
            //    <label hidden for="Food_Aliases_${i}_"></label>
            //    <input hidden value="${i}" type="number" data-val="true" data-val-required="The Id field is required." id="Food_Aliases_${i}__Id" name="Food.Aliases[${i}].Id">
            //    <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].Id">
            //    <input hidden value="${this.innerText}" type="text" data-val="true" data-val-required="The Alias field is required." id="Food_Aliases_${i}__Alias" name="Food.Aliases[${i}].Alias">
            //    <input hidden value="${foodId}" type="number" data-val="true" data-val-required="The FoodId field is required." id="Food_Aliases_${i}__FoodId" name="Food.Aliases[${i}].FoodId">
            //    <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].FoodId">`

            document.getElementById('foodAliasList').appendChild(div);
        }
    )
}

function listenForDelete() {
    let tags = document.getElementById('tags');
    tags && tags.addEventListener('click', function (event) {

        if (event.target.classList.contains('alias-delete')) {
            const divId = event.target.parentNode.id;
            $('#foodAliasList').html("");

            foodId = divId.split("-")[1];
            let i = 0;
            $('#tags').children().each(
                function (e) {
                    // if tag DOES NOT match the clicked item, keep it
                    if (this.id !== divId) {
                        const div = createAliasDiv(i, foodId, this.innerText);
                        //const div = document.createElement('div');
                        //div.innerHTML += `
                        //    <label hidden for="Food_Aliases_${i}_"></label>
                        //    <input hidden value="${i}" type="number" data-val="true" data-val-required="The Id field is required." id="Food_Aliases_${i}__Id" name="Food.Aliases[${i}].Id">
                        //    <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].Id">
                        //    <input hidden value="${this.innerText}" type="text" data-val="true" data-val-required="The Alias field is required." id="Food_Aliases_${i}__Alias" name="Food.Aliases[${i}].Alias">
                        //    <input hidden value="${foodId}" type="number" data-val="true" data-val-required="The FoodId field is required." id="Food_Aliases_${i}__FoodId" name="Food.Aliases[${i}].FoodId">
                        //    <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].FoodId">`

                        document.getElementById('foodAliasList').appendChild(div)
                        i++;
                    }
                })
            event.target.parentNode.remove();
        }
    });
}

function deleteFoodAlias(event) {
    if (event.target.classList.contains('alias-delete')) {
        event.target.parentNode.remove();
    }
}

function submitFoodUpdate() {
    console.log("TEST");
    console.log($("#foodFormTest").serialize());
    $.ajax({
        type: "POST",
        url: '/Guest/Food/AddFood',
        data: $("#foodFormTest").serialize(),
        success: function (data) {
            // set behavior by work area
            switch (window.location.pathname) {
                case '/Guest/Product':
                    $('.food-details-modal').modal('hide');
                    $('#pView').find('*').filter(function () {
                        if ($(this).text() === data.originalName) {
                            $(this).css('background-color', 'red');
                        }
                    });
                    break;
                case '/Guest/Food':
                    location.reload();
                    break;
                default:
                    break;
            }
        },
    });

}

function createAliasDiv(index, foodId, innerText) {

    const div = document.createElement('div');
    div.innerHTML += `
        <label hidden for="Food_Aliases_${index}_"></label>
        <input hidden value="0" type="number" data-val="true" data-val-required="The Id field is required." id="Food_Aliases_${index}__Id" name="Food.Aliases[${index}].Id">
        <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${index}].Id">
        <input hidden value="${innerText}" type="text" data-val="true" data-val-required="The Alias field is required." id="Food_Aliases_${index}__Alias" name="Food.Aliases[${index}].Alias">
        <input hidden value="${foodId}" type="number" data-val="true" data-val-required="The FoodId field is required." id="Food_Aliases_${index}__FoodId" name="Food.Aliases[${index}].FoodId">
        <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${index}].FoodId">`

    return div;
}

