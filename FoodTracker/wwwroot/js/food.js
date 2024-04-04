var foodId;
var foodName;

$(".food-chip").on('click', function (e) {
    var foodId = $(this).attr('value');
    selectFood(foodId);
});

function selectFood(id) {

    $.ajax({
        url: `/Guest/Food/GetFoodDetailsById/${id}`,
        type: 'GET',
        success: function (data) {
                $(`#foodMainView`).html(data);
        }
    })
}

function removeFood(id) {

    console.log("Removing:", id)
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

function cancelRemoveFood(id) {
    console.log("Canceling removal of:", id)
    $('.delete-food-modal').modal('hide');
}

function removeFoodConfirmation(id, foodName) {
    console.log(id);
    console.log(foodName);
    $('.delete-food-modal').modal('show');
    $('.modal-body').html(`Permanently delete "<b>${foodName}</b>"?`);
    $('.modal-footer').html(`
        <a onClick=cancelRemoveFood(${id}) class="btn btn-secondary mx-2">Cancel</a>
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
