
$("#searchButton").on('click', function () {
    const val = $("#searchQuery").val().replace(" ", "%20");
    $.ajax({
        url: `/Guest/Product/GetUSDAProducts?userQuery=${val}`,
        success: function (data) {
            $("#productView").html(data);

            $(".food-chip").on('click', function (e) {
                const foodName = $(this).find('.food-chip-name')[0].innerText
                activateModal(foodName)
            });
        }
    })
});

function activateModal(productName) {
    $('.food-details-modal').modal('show');
    $('.modal-title').html(productName);
    selectFoodByName(productName);
}

function selectFoodByName(foodName) {
    $.ajax({
        url: `/Guest/Food/GetFoodDetailsByName?foodName=${foodName}`,
        type: 'GET',
        success: function (data) {
            if (data) {
                $(`#foodCard`).html(data);
                $.event.trigger('loadAddFood', [{ location: 'product' }]);
            }
        }
    })
}
