var foodId;
var foodName;

console.log(`addFood.js for ${window.location.pathname}`);
//$(document).ready(function () {
//});
listenForTabs();
listenForDelete();

function removeFood() {
    $.ajax({
        url: `/Guest/Food/Delete/${foodId}`,
        type: 'DELETE',
        contentType: 'application/json',
        success: function (data) {
            if (data.success) {
                $(`#foodChip-${foodId}`).remove();
                $('.delete-food-modal').modal('hide');
                resetAllFields();
            }
        }
    })
}

function cancelRemoveFood() {
    $('.delete-food-modal').modal('hide');
}

function removeFoodConfirmation() {
    $('.delete-food-modal').modal('show');
    $('.modal-body').html(`Permanently delete "<b>${foodName}</b>"?`);
    $('.modal-footer').html(`
        <a onClick=cancelRemoveFood() class="btn btn-secondary mx-2">Cancel</a>
        <a onClick=removeFood() class="btn btn-danger mx-2">Delete</a>`);
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
    $('#fodmapInput').val('');
    $('#submitButton').html('Add');
    $('.hiddenButton').hide();

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
    input.addEventListener('keydown', function (e) {
        console.log("button press - alias area")
        // Check if the key pressed is 'Enter'
        if (e.key === 'Enter') {
            e.preventDefault();

            foodId = e.target.attributes.foodid.value;

            const tag = document.createElement('li');
            tag.id = `alias-${foodId}-new${Date.now()}`;
            tag.className = "alias-item-new";


            const tagContent = input.value.trim();
            if (tagContent !== '') {

                // Set the text content of the tag to
                // the trimmed value
                tag.innerText = tagContent;

                // Add a delete button to the tag
                tag.innerHTML += `<svg class="alias-delete xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-lg" viewBox="0 0 16 16">
                                    <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/>
                                  </svg>`;

                // Append the tag to the tags list
                tags.appendChild(tag);

                // Clear the input element's value
                input.value = '';

                $('#foodAliasList').html("");
                $('#tags').children().each(
                    function (i) {
                        const div = document.createElement('div');

                        div.innerHTML += `
                            <label hidden for="Food_Aliases_${i}_"></label>
                            <input hidden value="${i}" type="number" data-val="true" data-val-required="The Id field is required." id="Food_Aliases_${i}__Id" name="Food.Aliases[${i}].Id">
                            <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].Id">
                            <input hidden value="${this.innerText}" type="text" data-val="true" data-val-required="The Alias field is required." id="Food_Aliases_${i}__Alias" name="Food.Aliases[${i}].Alias">
                            <input hidden value="${foodId}" type="number" data-val="true" data-val-required="The FoodId field is required." id="Food_Aliases_${i}__FoodId" name="Food.Aliases[${i}].FoodId">
                            <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].FoodId">`

                        document.getElementById('foodAliasList').appendChild(div);
                    }
                )
            }
        }
    });
}

function listenForDelete() {
    let tags = document.getElementById('tags');
    tags.addEventListener('click', function (event) {

        // If the clicked element has the class 'alias-delete'
        if (event.target.classList.contains('alias-delete')) {

            const divId = event.target.parentNode.id;
            $('#foodAliasList').html("");

            foodId = divId.split("-")[1];
            let i = 0;
            $('#tags').children().each(
                function (e) {
                    // if tag DOES NOT match the clicked item, keep it
                    if (this.id !== divId) {

                        const div = document.createElement('div');

                        div.innerHTML += `
                            <label hidden for="Food_Aliases_${i}_"></label>
                            <input hidden value="${i}" type="number" data-val="true" data-val-required="The Id field is required." id="Food_Aliases_${i}__Id" name="Food.Aliases[${i}].Id">
                            <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].Id">
                            <input hidden value="${this.innerText}" type="text" data-val="true" data-val-required="The Alias field is required." id="Food_Aliases_${i}__Alias" name="Food.Aliases[${i}].Alias">
                            <input hidden value="${foodId}" type="number" data-val="true" data-val-required="The FoodId field is required." id="Food_Aliases_${i}__FoodId" name="Food.Aliases[${i}].FoodId">
                            <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].FoodId">`

                        document.getElementById('foodAliasList').appendChild(div)
                        i++;
                    }
                })

            // Remove the parent element (the tag)
            event.target.parentNode.remove();
        }
    });
}

function deleteFoodAlias(event) {
    // If the clicked element has the class 'alias-delete' 
    if (event.target.classList.contains('alias-delete')) {

        // Remove the parent element (the tag) 
        event.target.parentNode.remove();
        console.log(event.target);
    }
}

$(document).on('click', '#submitButtonTest', function () {
    $.ajax({
        type: "POST",
        url: '/Guest/Food/AddFood',
        data: $("#foodFormTest").serialize(),
        success: function (data) {
            // If in product area
            switch (window.location.pathname) {
                case '/Guest/Product':
                    $('.food-details-modal').modal('hide');

                    //$(`.food-chip:contains("${data.originalName}")`).css('background-color', 'red');  // For testing...
                    $('#pView').find('*').filter(function () {
                        if ($(this).text() === data.originalName) {
                            console.log("FOUND")
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
});

