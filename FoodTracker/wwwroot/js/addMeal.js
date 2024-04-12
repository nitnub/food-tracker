
let unitOptions;
let foodOptions

function createUnitOptions(unitJson) {
    let output = "";
    unitJson.forEach(u => output += `<option value="${u.id}">${u.namePlural}</option>`);
    return output;
}

function createFoodOptions(foodJson) {
    let output = "";
    foodJson.forEach(f => output += `<option value="${f.id}">${f.name}</option>`);
    return output;
}

function addMeal(mealId) {
   
    const newId = new Date().getTime().toString();
    var mealGroup = document.getElementById('mealGroup')
    var div = document.createElement("div");

    div.setAttribute("class", "border form-group rounded-2 my-2 shadow bg-white p-2 remove-meal-item-new" + newId);

    div.innerHTML = `
        <div class="d-flex">
            <div class="form-floating py-2 col-6">
                <select class="form-select food-input meal-item-food-input" data-val="true" data-val-required="The FoodId field is required." id="MealItems_${newId}__FoodId" name="MealItems[${newId}].FoodId">
                    <option value="" selected="">--Select Food--</option>
                        ${foodOptions}
                </select>
            </div>

            <div class="d-flex">

                <div class="form-floating py-2 col-6">
                    <input class="form-control food-input " type="text" data-val="true" data-val-number="The field Volume must be a number." data-val-required="The Volume field is required." id="MealItems_${newId}__Volume" name="MealItems[${newId}].Volume" value="">
                    <label class="ms-2" for="MealItems_${newId}__Volume">Volume</label>
                    <span class="text-danger field-validation-valid" data-valmsg-for="MealItems[${newId}].Volume" data-valmsg-replace="true"></span>
                </div>


                <div class="form-floating py-2 col-6">
                    <select class="form-select food-input meal-item-units" data-val="true" data-val-required="The VolumeUnitsId field is required." id="MealItems_${newId}__VolumeUnitsId" name="MealItems[${newId}].VolumeUnitsId">
                        <option value="" selected="">--Units--</option>
                            ${unitOptions}
                    </select>
                </div>
            </div>

        </div> 
        <div style="display: flex; justify-content:end">
            <a class="link-dark" type="button" onclick="removeNewMealItem(${newId})">Remove</a>
        </div>`

    mealGroup.appendChild(div)
}

function activateModal(dayObj, activeMealId = 0) {
    $('.meal-details-modal').modal('show');
    $('.modal-title').html(dayObj);

    dayObj.activeMealId = activeMealId;
    getMeal(dayObj);
}

function getMeal(dayObj) {

    //const clickedDate = dayObj.DateTime.substring(0, 10)
    //if (clickedDate === getTodaysDateFormatted()) {
    //    dayObj.DateTime = new Date();
    //};
    //console.log(activeMeal)
    //console.log("activeMealId");
    //console.log(activeMealId);
    //dayObj.activeMealId = activeMealId;
    $.ajax({
        url: `/Guest/Calendar/UpsertMeal`,
        type: 'POST',
        data: JSON.stringify(dayObj),
        contentType: 'application/json',
        success: function (data) {
            if (data) {
                // Populate meal card
                $(`#mealCard`).html(data);

                // Populate new meal item dropdowns
                unitOptions = createUnitOptions(unitJson);
                foodOptions = createFoodOptions(foodJson);
            }
        }
    })
}

function removeMeal(id) {
    $.ajax({
        url: `/Guest/Calendar/DeleteMeal/${id}`,
        type: 'DELETE',
        contentType: 'application/json',
        success: function (data) {
            if (data.success) {
                $('.remove-meal-' + id).remove();
                $('.delete-meal-modal').modal('hide');
            }
        }
    })
}

function removeNewMealItem(id) {
    console.log("Remove:", id)
    $('.remove-meal-item-new' + id).remove();
}

function removeMealItemConfirmation(id) {
    console.log("id");
    console.log(id);
    const mealItem = mealItems.find(m => m.id === id);
    $('.delete-meal-item-modal').modal('show');
    $('.modal-body').html(`Permanently delete meal item "<b>${mealItem.food.name}</b>"?`);
    $('.modal-footer').html(`
        <a onClick=cancelRemoveMealItem() class="btn btn-secondary mx-2">Cancel</a>
        <a onClick=removeMealItem(${id}) class="btn btn-danger mx-2">Delete</a>`);
}

function cancelRemoveMealItem() {
    $('.delete-meal-item-modal').modal('hide');
}

function getProducts(query, page = 1) {
    query = query.replace(' ', '%20');

    $.ajax({
        url: `/Guest/Product/GetUSDAProducts?userQuery=${query}&pageNumber=${page}`,
        success: function (data) {
            $("#productView").html(data);
            $(".food-chip").on('click', function (e) {
                const foodName = $(this).find('.food-chip-name')[0].innerText
                activateModal(foodName)
            });

            const query = $('#searchQuery').val();
            const page = $('#productResultsHeader').attr('page');
            const pageCount = $('#productResultsHeader').attr('pageCount');

            const paginationDiv = getPaginationDiv(query, page, pageCount);
            $('.paginationDiv').html(paginationDiv)
        }
    })
}

//function getTodaysDateFormatted() {
//    const today = new Date()
//    const year = today.getFullYear();
//    const month = (today.getMonth() + 1).toLocaleString('en-US', { minimumIntegerDigits: 2 }); // month is 0-indexed, date is not
//    const day = today.getDate().toLocaleString('en-US', { minimumIntegerDigits: 2 });

//    return `${year}-${month}-${day}`;
//}