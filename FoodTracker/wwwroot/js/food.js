var foodId;
var foodName;


function selectFood(id) {
    var food = foodList.find(f => f.id === id);

    foodId = id;
    foodName = food.name;
    //$(`#foodChip-${foodId}`).addClass('selectedFoodChip');
    // Update UI on click
    $('#foodHeader').html(`<div>${food.name} </div>`)

    $('#foodId').val(food.id);
    $('#nameInput').val(food.name);
    $('#vegetarianInput').prop('checked', food.vegetarian);
    $('#veganInput').prop('checked', food.vegan);
    $('#glutenInput').prop('checked', food.glutenFree);
    $('#fodmapInput').val(food.fodmapId);
    $('#submitButton').html('Update');
    $('.hiddenButton').show();
}

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

clickVegetarian
function clickVegetarian() {
    if (!$('#vegitarianInput').prop('checked')) {
        $('#veganInput').prop('checked', false);
    }
}











var elementWarn = `
        <svg focusable="false" aria-hidden="true" height="24" width="24">
            <path d="M11 15h2v2h-2zm0-8h2v6h-2zm.99-5C6.47 2 2 6.48 2 12s4.47 10 9.99 10C17.52 22 22 17.52 22 12S17.52 2 11.99 2zM12 20c-4.42 0-8-3.58-8-8s3.58-8 8-8 8 3.58 8 8-3.58 8-8 8z"></path>
        </svg>`

var elementSafe = `
        <svg class="inactiveReactant" focusable="false" aria-hidden="true" height="24" width="24">
            <path d="M9 16.17 4.83 12l-1.42 1.41L9 19 21 7l-1.41-1.41z"></path>
        </svg>`






//function fodChange() {

//    let fodId = document.getElementById('fodmapInput').value;

//    var fm = fodMapList.find(f => f.id == fodId);
//    console.log(fm);

//    if (fm == null) {
//        $('#fodName').html('No FODMAP');

//        resetFodmapCard();
//    }
//    else {

//        $('#fodName').html(fm.name);
//        $('#fodCategory').html(fm.category.name);

//        $("#fodColor").removeClass();
//        $('#fodColor').addClass(`fodFlag fod${fm.color.name}`);

//        $("#fodDot").removeClass();
//        $('#fodDot').addClass(`fodColorCircle fod${fm.color.name}`);


//        var dailyIntakeResult = `
//                <div class="fodSafeCheckContainer">
//                    <svg class=" fodSafeCheck " focusable="false" aria-hidden="true" viewBox="0 0 24 24">
//                        <path d="M9 16.17 4.83 12l-1.42 1.41L9 19 21 7l-1.41-1.41z"></path>
//                    </svg>
//                </div>
//                <div >FODMAP Safe!</div> `

//        if (fm.maxUse < 9999) {
//            dailyIntakeResult = `                   
//                <div id="fodMaxIntakeContainer" class="fodMaxIntakeContainer">
//                    <div class="fodMaxIntake">
//                        ${fm.maxUse}${fm.maxUse === 1 ? fm.maxUse.shortName : fm.maxUseUnits.shortNamePlural}
//                        </div>
//                    <div >Max Daily Intake</div>
//                </div>`


//        }

//        $('#fodMaxIntakeContainer').html(dailyIntakeResult);
        
//        var aliasString = fm.aliases.map(a => a.alias).join(", ");
//        $('#fodAlias').html(aliasString);

//        $('#elementsContainer').html(
//            ['oligos', 'fructose', 'polyols', 'lactose'].map(el =>
//                `<div id="fodOligos" class="${fm[el] ? 'activeReactant' : 'inactiveReactant'}" aria-label="${fm[el] ? 'contains' : 'free of' } ${fm}"> 
//                    <div id="fodOligosLabel">
//                     ${fm[el] ? elementWarn : elementSafe}
//                    </div>
//                    <div>
//                        ${el}
//                    </div>
//                </div>`));
//    }  
//}

//document.getElementById('fodmapInput').addEventListener('change', fodChange);

//// And run the function on page load as well, as until now
//fodChange();




//function resetFodmapCard() {

//    $("#fodColor").removeClass();
//    $('#fodColor').addClass(`fodFlag fodBlue`);

//    $("#fodDot").removeClass();
//    $('#fodDot').addClass(`fodColorCircle fodBlue`);


//    $('#fodCategory').html('FODMAP Category');
//    $('#fodAlias').html('...');

//    $('#elementsContainer').html(
//        ['oligos', 'fructose', 'polyols', 'lactose'].map(el =>
//            `<div class="inactiveReactant"> 
//                    <div>
//                        <svg xmlns="http://www.w3.org/2000/svg" height="24" width="24" fill="currentColor" class="bi bi-question-lg" viewBox="0 0 16 16">
//                            <path fill-rule="evenodd" d="M4.475 5.458c-.284 0-.514-.237-.47-.517C4.28 3.24 5.576 2 7.825 2c2.25 0 3.767 1.36 3.767 3.215 0 1.344-.665 2.288-1.79 2.973-1.1.659-1.414 1.118-1.414 2.01v.03a.5.5 0 0 1-.5.5h-.77a.5.5 0 0 1-.5-.495l-.003-.2c-.043-1.221.477-2.001 1.645-2.712 1.03-.632 1.397-1.135 1.397-2.028 0-.979-.758-1.698-1.926-1.698-1.009 0-1.71.529-1.938 1.402-.066.254-.278.461-.54.461h-.777ZM7.496 14c.622 0 1.095-.474 1.095-1.09 0-.618-.473-1.092-1.095-1.092-.606 0-1.087.474-1.087 1.091S6.89 14 7.496 14"/>
//                        </svg>
//                    </div>
//                    <div >
//                        ${el}
//                    </div>
//                </div>`));


//    $('#fodMaxIntakeContainer').html(`
//                <div class="fodUnknownContainer">
//                <div>
//                    <svg class="fodCheckUnknown" xmlns="http://www.w3.org/2000/svg" height="100" width="100" viewBox="0 0 16 16" >
//                      <path d="M4.475 5.458c-.284 0-.514-.237-.47-.517C4.28 3.24 5.576 2 7.825 2c2.25 0 3.767 1.36 3.767 3.215 0 1.344-.665 2.288-1.79 2.973-1.1.659-1.414 1.118-1.414 2.01v.03a.5.5 0 0 1-.5.5h-.77a.5.5 0 0 1-.5-.495l-.003-.2c-.043-1.221.477-2.001 1.645-2.712 1.03-.632 1.397-1.135 1.397-2.028 0-.979-.758-1.698-1.926-1.698-1.009 0-1.71.529-1.938 1.402-.066.254-.278.461-.54.461h-.777ZM7.496 14c.622 0 1.095-.474 1.095-1.09 0-.618-.473-1.092-1.095-1.092-.606 0-1.087.474-1.087 1.091S6.89 14 7.496 14"/>
//                    </svg>
//                </div>`);
//}
