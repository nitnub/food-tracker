
$("#searchButton").on('click', function (e) {

    const val = $("#searchQuery").val().replace(" ", "%20");
    $.ajax({
        url: `/guest/product/getusdaproducts?userQuery=${val}`,
        success: function (data) {
            $("#productView").html(data);

            $(".food-chip").on('click', function (e) {
                var foodName = e.target.innerText;
                console.log("Access")
                activateModal(foodName)
            });
        }
    })
});

function activateModal(productName) {
    $('.food-details-modal').modal('show');
    $('.modal-title').html(productName);
    console.log("Access1")
    selectFoodByName(productName);
}

function selectFoodByName(foodName) {
    console.log("Access2", foodName);
    $.ajax({
        url: `/Guest/Food/GetFoodDetailsByName?foodName=${foodName}`,
        type: 'GET',
        success: function (data) {
            if (data) {
                $(`#foodCard`).html(data);
                console.log("Sucess...")
            }
        }
    })
}
