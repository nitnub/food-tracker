var foodId;
var foodName;


function selectFood(id) {

        $.ajax({
            url: `/Guest/Food/GetFoodDetailsById/${id}`,
            type: 'GET',
            success: function (data) {
                if (data) {
                    console.log('DATA PULLED FROM DB!');
                    $(`#foodCard`).html(data);
                }
            }
        })
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

