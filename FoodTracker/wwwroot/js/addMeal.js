function addMeal(mealId) {
    const newId = 0;
    console.log(mealId);
    /*return*/;
    var mealGroup = document.getElementById('mealGroup')
    var div = document.createElement("div");

    div.setAttribute("class", "border form-group rounded-2 my-2 shadow bg-white p-2 remove-meal-new" + newId);

    div.innerHTML = `
        <input hidden="" type="number" data-val="true" data-val-required="The Id field is required." id="MealItemss_${newId}__Id" name="MealItems[${newId}].Id" value="0"><input name="__Invariant" type="hidden" value="MealItems[${newId}].Id">
        <input hidden="" type="number" data-val="true" data-val-required="The ProjectId field is required." id="MealItems_${newId}__MealId" name="MealItems[${newId}].MealId" value="${mealId}"><input name="__Invariant" type="hidden" value="MealItems[${newId}].MealId">
        <div class="form-floating py-2 form col-12">
            <input class="form-control border-1" type="text" data-val="true" data-val-required="The Title field is required." id="Videos_${newId}__Title" name="Videos[${newId}].Title" >
            <label class="ms-2 text-dark" for="Videos_${newId}__Title">Title</label>
            <span class="text-danger field-validation-valid" data-valmsg-for="Videos[${newId}].Title" data-valmsg-replace="true"></span>
        </div>

        <div class="form-floating py-2 col-12">
            <input class="form-control border-1" type="text" data-val="true" data-val-required="The Description field is required." id="Videos_${newId}__Description" name="Videos[${newId}].Description" >
            <label class="ms-2 text-dark" for="Videos_${newId}__Description">Description</label>
            <span class="text-danger field-validation-valid" data-valmsg-for="Videos[${newId}].Description" data-valmsg-replace="true"></span>
        </div>

        <div class="form-floating py-2 col-12">
            <input class="form-control border-1" type="text" data-val="true" data-val-required="The Tool Tip field is required." id="Videos_${newId}__ToolTip" name="Videos[${newId}].ToolTip" >
            <label class="ms-2 text-dark" for="Videos_${newId}__ToolTip">Tool Tip</label>
            <span class="text-danger field-validation-valid" data-valmsg-for="Videos[${newId}].ToolTip" data-valmsg-replace="true"></span>
        </div>

        <div class="form-floating py-2 col-12">
            <input class="form-control border-1" type="text" data-val="true" data-val-required="The URL field is required." id="Videos_${newId}__URL" name="Videos[${newId}].URL" >
            <label class="ms-2 text-dark" for="Videos_${newId}__URL">URL</label>
            <span class="text-danger field-validation-valid" data-valmsg-for="Videos[${newId}].URL" data-valmsg-replace="true"></span>
        </div>

        <div class="form-floating py-2 col-12">
            <input class="form-control border-1" type="number" data-val="true" data-val-required="The Order field is required." id="Videos_${newId}__Order" name="Videos[${newId}].Order" value="${newId + 1}"><input name="__Invariant" type="hidden" value="${newId + 1}">
            <label class="ms-2 text-dark" for="Videos_${newId}__Order">Order</label>
            <span class="text-danger text! field-validation-valid" data-valmsg-for="Videos[${newId}].Order" data-valmsg-replace="true"></span>
        </div>

        <div class="form-check py-2 col-12">
            <input class="form-check-input border-1" type="checkbox" checked="checked" data-val="true" data-val-required="The Active field is required." id="Videos_${newId}__Active" name="Videos[${newId}].Active" value="true">
            <label class="form-check-label ms-2 text-dark" for="Videos_${newId}__Active">Active</label>
            <span class="text-danger field-validation-valid" data-valmsg-for="Videos[${newId}].Active" data-valmsg-replace="true"></span>
        </div>
        <div style="display: flex; justify-content:end">
            <a class="link-dark" type="button" onclick="removeNewMeal(${newId})">Remove</a>
        </div>`

    mealGroup.appendChild(div)
    newId++;
}


function activateModal(mealId) {
    $('.meal-details-modal').modal('show');
    $('.modal-title').html(mealId);

    //return
    console.log(`Activating meal for ${mealId}`)
    getMeal(mealId);
}

function getMeal(mealId) {

    console.log(`getMeal for ${mealId}`)
    $.ajax({
        url: `/Guest/Calendar/UpsertMeal?id=${mealId}`,
        type: 'GET',
        success: function (data) {
            if (data) {

                $(`#mealCard`).html(data);
                console.log("Added data...");
                //$.event.trigger('loadAddFood', [{ location: 'product' }]);
            }
        }
    })
}

//function getMeal(foodName) {
//    $.ajax({
//        url: `/Guest/Food/GetFoodDetailsByName?foodName=${foodName}`,
//        type: 'GET',
//        success: function (data) {
//            if (data) {
//                $(`#foodCard`).html(data);
//                $.event.trigger('loadAddFood', [{ location: 'product' }]);
//            }
//        }
//    })
//}

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