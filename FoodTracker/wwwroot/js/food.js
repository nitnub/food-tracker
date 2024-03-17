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
    // Update UI on delete
    $('#foodHeader').html(`<div>Add Food</div>`)

    $('#foodId').val(0);
    $('#nameInput').val('');
    $('#vegetarianInput').prop('checked', false);
    $('#veganInput').prop('checked', false);
    $('#glutenInput').prop('checked', false);
    $('#fodmapInput').val('');
    $('#submitButton').html('Add');
    $('.hiddenButton').hide();
}