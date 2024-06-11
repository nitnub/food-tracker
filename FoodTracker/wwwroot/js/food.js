
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
            $.event.trigger('loadAddFood', [{ location: 'food' }]);
        }
    })
}
