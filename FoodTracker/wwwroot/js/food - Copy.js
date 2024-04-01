var foodId;
var foodName;


// Declare tags and input elements for food aliases
//let tags;
//let input;

// Listen for new alias entries on load

let tags = document.getElementById('tags');
let input = document.getElementById('input-tag');

listenForTabs()

function selectFood(id) {
    //var food = foodList.find(f => f.id === id);

    //foodId = id;
    //foodName = food.name;

    //updateFoodCard(food);
    console.log('calling...')


        $.ajax({
            url: `/Guest/Food/GetFoodDetailsById/${id}`,
            type: 'GET',
            //contentType: 'application/json',
            success: function (data) {
                if (data) {
                    //console.log(data)
                    console.log('DATA PULLED FROM DB!');
                    $(`#foodCard`).html(data);

                    // On page reload, listen for new alias entries
                    tags = document.getElementById('tags');
                    input = document.getElementById('input-tag');
                    console.log(input);
                    listenForTabs();
                    //$('.delete-food-modal').modal('hide');
                    //resetAllFields();
                }
                    //console.log(data)
            }
        })

    //$(function () {
    //    $('form').submit(function () {
    //        $(this).append($(tags));
    //        return true;
    //    });
    //});


    //$(function () {
    //    $('form').submit(function () {
    //        $(this).append($("#ValueOutsideForm"));
    //        return true;
    //    });
    //});
}

function selectFoodByName(foodName) {
    //var food = foodList.find(f => f.id === id);

    //foodId = id;
    //foodName = food.name;

    //updateFoodCard(food);
    console.log('calling...')


    //$.ajax({
    //    url: `/Guest/Food/GetFoodDetailsById/${id}`,
    //    type: 'GET',
    //    //contentType: 'application/json',
    //    success: function (data) {
    //        if (data) {
    //            //console.log(data)
    //            console.log('DATA PULLED FROM DB!');
    //            $(`#foodCard`).html(data);

    //            tags = document.getElementById('tags');
    //            input = document.getElementById('input-tag');
    //            console.log(input);
    //            listenForTabs();
    //            //$('.delete-food-modal').modal('hide');
    //            //resetAllFields();
    //        }
    //        //console.log(data)
    //    }
    //})
    $.ajax({
        //url: `/guest/food/getfooddetails?foodName=${productName}`,
        //url: `/Guest/Food/GetFoodDetailsByName?foodName=${JSON.stringify(productName)}`,
        url: `/Guest/Food/GetFoodDetailsByName?foodName=${foodName}`,
        type: 'GET',
        success: function (data) {
            if (data) {
                //console.log(data)
                console.log('DATA PULLED FROM DB!');
                $(`#foodCard`).html(data);

                tags = document.getElementById('tags');
                input = document.getElementById('input-tag');
                console.log(input);
                listenForTabs();
                //$('.delete-food-modal').modal('hide');
                //resetAllFields();
            }
            //console.log(data)
        }
    })

    //$(function () {
    //    $('form').submit(function () {
    //        $(this).append($(tags));
    //        return true;
    //    });
    //});


    //$(function () {
    //    $('form').submit(function () {
    //        $(this).append($("#ValueOutsideForm"));
    //        return true;
    //    });
    //});
}

//function updateFoodCard(food) {


//    // Update Food details UI 
//    $('#foodHeader').html(`<div>${food.name} </div>`)

//    $('#foodId').val(food.id);
//    $('#nameInput').val(food.name);
//    $('#vegetarianInput').prop('checked', food.vegetarian);
//    $('#veganInput').prop('checked', food.vegan);
//    $('#glutenInput').prop('checked', food.glutenFree);
//    $('#fodmapInput').val(food.fodmapId);
//    $('#submitButton').html('Update');
//    //$('.food-input').prop('disabled', false);
//    //$('.food-input-button').prop('visible', true);


//    $('.hiddenButton').show();
//    console.log('global check')
//    if (food.global) {
//        console.log("is global");
//        $('.food-input').prop('disabled', true);
//        $('.food-input-button').prop('hidden', true);
//    } else {
//        console.log("NOT global");
//        $('.food-input').prop('disabled', false);
//        $('.food-input-button').prop('hidden', false);
//        //$('.food-input').prop('disabled', false);
//        //$('.food-input-button').prop('visible', true);
//    }

//    // Update FODMAP details card
//    fodChange(food.fodmapId ? food.fodmapId : '');
//}

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




//function activateModal(productName) {

//    $('.food-details-modal').modal('show');
//    $('.modal-title').html(productName);
//    console.log("Clicked");
//    console.log(productName);
//    //console.log(foodList);

//    $.ajax({
//        url: `/guest/food/getfooddetails?foodName=${productName}`,
//        type: 'POST',
//        success: function (data) {
//            console.log(data)
//            $("#modal-body").html(data);
//        }
//    })
//}



//// Tags input imported from:
//// http://bootstrap-tagsinput.github.io/bootstrap-tagsinput/examples/

//$(document).ready(function () {

//    $('input[name="input"]').tagsinput({
//        trimValue: true,
//        confirmKeys: [13, 44],
//        focusClass: 'my-focus-class'
//    });

//    $('.bootstrap-tagsinput input').on('focus', function () {
//        $(this).closest('.bootstrap-tagsinput').addClass('has-focus');
//    }).on('blur', function () {
//        $(this).closest('.bootstrap-tagsinput').removeClass('has-focus');
//    });

//});


//// Get the tags and input elements from the DOM
//const tags = document.getElementById('tags');
//const input = document.getElementById('input-tag');

// Add an event listener for keydown on the input element
//input.addEventListener('keydown', function (event) {

//    // Check if the key pressed is 'Enter'
//    if (event.key === 'Enter') {

//        console.log(input.value);
//        console.log(input.id);

//        // Prevent the default action of the keypress
//        // event (submitting the form)
//        event.preventDefault();

//        // Get the trimmed value of the input element
//        const tagContent = input.value.trim();

//        // Create a new list item element for the tag
//        const tag = document.createElement('li');

//        // If the trimmed value is not an empty string
//        if (tagContent !== '') {

//            // Set the text content of the tag to
//            // the trimmed value
//            tag.innerText = tagContent;

//            // Add a delete button to the tag
//            //tag.innerHTML += '<button class="alias-delete">X</button>';
//            tag.innerHTML += `<svg class="alias-delete xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-lg" viewBox="0 0 16 16">
//                                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/>
//                              </svg>`;

//            // Append the tag to the tags list
//            tags.appendChild(tag);

//            // Clear the input element's value
//            input.value = '';
//        }
//    }
//});

function listenForTabs() {
    input.addEventListener('keydown', function (event) {
        console.log("button press - alias area")
        // Check if the key pressed is 'Enter'
        if (event.key === 'Enter') {
            event.preventDefault();

            //console.log(input);
            //console.log(input.data);
            //console.log(event.target.attributes.foodid.value);
            //console.log(event.attr('foodid'));
            //console.log(input.prop('foodid'));
            //console.log(input.prop('foodId'));


            // Add to DB. On success, display in UI
            //const newAlias = JSON.stringify( {foodId: '1', alias: 'new food alias'})
            const foodId = event.target.attributes.foodid.value;
            const newAlias = input.value;

            ////event.preventDefault();
            ////return
            //$.ajax({
            //    url: `/Guest/Food/AddFoodAlias/${foodIid}?newAlias=${newAlias}`,
            //    type: 'POST',
            //    //contentType: 'application/json',
            //    success: function (data) {
            //        if (data.success) {
            //            //console.log(data)
            //            console.log('DATA COMMITTED TO DB!');

            //        }
            //        else {
            //            console.log('FAILED TO COMMIT DATA TO DB!');
            //        }
            //    }
            //})





            // Prevent the default action of the keypress
            // event (submitting the form)

            // Get the trimmed value of the input element
            const tagContent = input.value.trim();

            // Create a new list item element for the tag
            const tag = document.createElement('li');

            // add tag id
            tag.id = `alias-${foodId}-new${Date.now()}`;
            // add tag classes
            tag.className = "alias-item-new";

            // If the trimmed value is not an empty string
            if (tagContent !== '') {

                // Set the text content of the tag to
                // the trimmed value
                tag.innerText = tagContent;

                // Add a delete button to the tag
                //tag.innerHTML += '<button class="alias-delete">X</button>';
                tag.innerHTML += `<svg class="alias-delete xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-lg" viewBox="0 0 16 16">
                                    <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/>
                                  </svg>`;

                // Append the tag to the tags list
                tags.appendChild(tag);

                // Clear the input element's value
                input.value = '';


                        console.log($('#foodAliasList'))

                
                //$('#foodAliasList').innerHTML = '';
                
                //$('.alias-items').each(function (i) {
                //    console.log(i);
                //    console.log(this.innerText);
                //});
                const aliasList = document.getElementById('foodAliasList');

                //return;
                console.log(1);
                //aliasList.innerHTML = "";
                /* console.log(aliasList.innerHTML.empt);*/
                $('#foodAliasList').html("");
                console.log(2);
                //aliasList.replaceChildren();
                //return;
                $('#tags').children().each(
                    function (i) {
                        //console.log(i);
                        //console.log(this.innerText);
                        const div = document.createElement('div');

                        //$('#foodAliasList').innerHTML += `
                        div.innerHTML += `
                            <label hidden for="Food_Aliases_${i}_"></label>
                            <input hidden value="${i}" type="number" data-val="true" data-val-required="The Id field is required." id="Food_Aliases_${i}__Id" name="Food.Aliases[${i}].Id">
                            <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].Id">
                            <input hidden value="${this.innerText}" type="text" data-val="true" data-val-required="The Alias field is required." id="Food_Aliases_${i}__Alias" name="Food.Aliases[${i}].Alias">
                            <input hidden value="${foodId}" type="number" data-val="true" data-val-required="The FoodId field is required." id="Food_Aliases_${i}__FoodId" name="Food.Aliases[${i}].FoodId">
                            <input hidden name="__Invariant" type="hidden" value="Food.Aliases[${i}].FoodId">`


                        //div.innerHTML += `
                        //    <label for="Food_Aliases_${i}_"></label>
                        //    <input value="0" type="number" data-val="true" data-val-required="The Id field is required." id="Food_Aliases_0__Id" name="Food.Aliases[0].Id">
                        //    <input name="__Invariant" type="hidden" value="Food.Aliases[0].Id">
                        //    <input value="sfefs" type="text" data-val="true" data-val-required="The Alias field is required." id="Food_Aliases_0__Alias" name="Food.Aliases[0].Alias">
                        //    <input value="1" type="number" data-val="true" data-val-required="The FoodId field is required." id="Food_Aliases_0__FoodId" name="Food.Aliases[0].FoodId">
                        //    <input name="__Invariant" type="hidden" value="Food.Aliases[0].FoodId">`

                        //div.innerHTML += `
                        //    <label asp-for="@Model.Food.Aliases[${i}]"></label> 
                        //    <input asp-for="@Model.Food.Aliases[${i}].Id" />
                        //    <input asp-for="@Model.Food.Aliases[${i}].Alias" value="${this.innerText}" />
                        //    <input asp-for="@Model.Food.Aliases[${i}].FoodId" value="${foodIid}" />`



                        //    < label hidden asp -for= "Food.Aliases[${i}].Alias" ></label >
                        
                        //<input hidden asp-for="Food.Aliases[${i}].Alias" value="${this.innerText}" />
                        //<input hidden asp-for="Food.Aliases[${i}].Id" />
                        //<input hidden asp-for="Food.Aliases[${i}].FoodId" value="${foodIid}" />

                        console.log(div.innerHTML);

                        document.getElementById('foodAliasList').appendChild(div)
                        //$('#foodAliasList').appendChild(div);

                    }

                )
                        console.log($('#foodAliasList'))

                //var formData = new FormData();

                //$("input[name='Food.Alias.AudioSelected']").each(function (i) {
                //    var AudioSelected = $(this).val();
                //    formData.append("Sentences[" + i + "].Audio.AudioSelected", AudioSelected);

                //});
                //$("input[name='Image.ImageSelected']").each(function (i) {
                //    var ImageSelected = $(this).val();
                //    formData.append("Sentences[" + i + "].Image.ImageSelected", ImageSelected);

                //});
                //$("input[name='SentenceText']").each(function (i) {
                //    var SentenceText = $(this).val();
                //    formData.append("Sentences[" + i + "].SentenceText", SentenceText);

                //});




   



            }
        }
    });

    tags.addEventListener('click', function (event) {

        // If the clicked element has the class 'alias-delete'
        if (event.target.classList.contains('alias-delete')) {
            //const shell = event.target.parentNode.parentNode.parentNode;
            const shell = event.target.parentNode;
            //const siblings = shell.children();

            //const clickedId = event.target.parentNode.parentNode.id;
            const clickedId = event.target.parentNode.id;
            //console.log("CLICKED ID:", clickedId);
            //console.log(shell);
            $('#foodAliasList').html("");
            //const divId = event.target.parentNode.parentNode.id;
            const divId = event.target.parentNode.id;
            //console.log('divid=', div)
            const foodId = divId.split("-")[1];
            //console.log('foodId', foodId);
            let i = 0;
            $('#tags').children().each(
                function (inc) {
                // if does not match the clicked item, keep.



                    console.log("ID:", this.id);
                    console.log("ClickedID: ", clickedId);
                    if (this.id !== clickedId) {

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

                //console.log("child id = ", this.id)
                //console.log(this)


                // stopped here. see above step.


                })


            // Remove the parent element (the tag)
            console.log(event.target);
            //event.target.parentNode.parentNode.remove();
            event.target.parentNode.remove();



            //const alias = event.target.parentNode.parentNode.innerText;
            const alias = event.target.parentNode.innerText.trim();
            const id = event.target.parentNode.id;
            const aliasId = id.split('-')[2];

            console.log(tags.children);
            console.log(id);
            console.log('alias id:', aliasId);
            console.log(alias);
            //console.log(aliasId);
            //return
            //$.ajax({
            //    url: `/Guest/Food/RemoveFoodAlias/${aliasId}?alias=${alias}`,
            //    type: 'DELETE',
            //    //contentType: 'application/json',
            //    success: function (data) {
            //        if (data.success) {
            //            //console.log(data)
            //            console.log('DATA REMOVED FROM DB!');
            //            // Remove the parent element (the tag)
            //            event.target.parentNode.parentNode.remove();
            //            console.log(event.target);
            //        }
            //        else {
            //            console.log('FAILED TO REMOVE DATA FROM DB!');
            //        }
            //    }
            //})

        }
    }); 


}



//function enterFoodAlias(event) {connsole.log('aefefAAA') }
//function enterFoodAlias2(event) {
//    // Check if the key pressed is 'Enter'
//        event.preventDefault();

//    console.log('inn enterfoodalias')
//    if (event.key === 'Enter') {

//        console.log(input.value);
//        console.log(input.id);

//        // Prevent the default action of the keypress 
//        // event (submitting the form) 

//        // Get the trimmed value of the input element 
//        const tagContent = input.value.trim();

//        // Create a new list item element for the tag
//        const tag = document.createElement('li');

//        // If the trimmed value is not an empty string 
//        if (tagContent !== '') {

//            // Set the text content of the tag to  
//            // the trimmed value 
//            tag.innerText = tagContent;

//            // Add a delete button to the tag 
//            //tag.innerHTML += '<button class="alias-delete">X</button>'; 
//            tag.innerHTML += `<svg class="alias-delete xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-lg" viewBox="0 0 16 16">
//                                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/>
//                              </svg>`;

//            // Append the tag to the tags list 
//            tags.appendChild(tag);

//            // Clear the input element's value 
//            input.value = '';
//        }
//    }
//}

function deleteFoodAlias(event) {
    // If the clicked element has the class 'alias-delete' 
    if (event.target.classList.contains('alias-delete')) {

        // Remove the parent element (the tag) 
        event.target.parentNode.remove();
        console.log(event.target);
    }
}


// //Add an event listener for click on the tags list
//tags.addEventListener('click', function (event) {

//    // If the clicked element has the class 'alias-delete'
//    if (event.target.classList.contains('alias-delete')) {

//        // Remove the parent element (the tag)
//        event.target.parentNode.remove();
//        console.log(event.target);
//    }
//});







//$("#submit").click(function (e) {
//    e.preventDefault();
//    var formData = new FormData();

//    $("input[name='Food.Alias.AudioSelected']").each(function (i) {
//        var AudioSelected = $(this).val();
//        formData.append("Sentences[" + i + "].Audio.AudioSelected", AudioSelected);

//    });
//    $("input[name='Image.ImageSelected']").each(function (i) {
//        var ImageSelected = $(this).val();
//        formData.append("Sentences[" + i + "].Image.ImageSelected", ImageSelected);

//    });
//    $("input[name='SentenceText']").each(function (i) {
//        var SentenceText = $(this).val();
//        formData.append("Sentences[" + i + "].SentenceText", SentenceText);

//    });
//    console.log("TEST !@#");
//    return;
//    $.ajax({
//        method: 'post',
//        url: "StoryTest/AddSentence",
//        data: formData,
//        processData: false,
//        contentType: false,
//        success: function () {

//        }
//    });

//});

$("#addItem").click(function () {
    $.ajax({
        url: this.href,
        cache: false,
        success: function (html) { $("#editorRows").append(html); }
    });
    return false;
});

$("a.deleteRow").on("click", function () {
    $(this).parents("div.editorRow:first").remove();
    return false;
});


console.log(window.location.pathname);
$(document).on('click', '#submitButtonTest', function () {
    console.log("IN TEST");
    console.log($("#foodFormTest").serialize());
    $.ajax({
        type: "POST",
        url: '/Guest/Food/AddFood',
        data: $("#foodFormTest").serialize(),
        success: function (data) {
            //read data back from server and do what ever you want
            console.log(data)
            console.log("THis is a tsesttaewt ewr ");

            // If in product area
            switch (window.location.pathname) {
                case '/Guest/Product':
                    console.log("Calling from the product area");
                    $('.food-details-modal').modal('hide');
                    console.log(data.originalName);
                    //$(`.food-chip:contains("${data.originalName}")`).css('background-color', 'red');

                    $('#pView').find('*').filter(function () {
                        if ($(this).text() === data.originalName) {
                            console.log("FOUND")
                            $(this).css('background-color', 'red');
                        }
                    });




                    break;
                case '/Guest/Food':
                    location.reload();
                    console.log("Calling from the food area");
                    break;
                default:
                    break;

            }

            // If in food area
            if (false) {
                location.reload();
            }
        },
            /*dataType: dataType*/
        });

});



     