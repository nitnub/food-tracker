
$("#searchButton").click(function (e) {
    const val = $("#searchQuery").val().replace(" ", "%20");
    $.ajax({
        url: `/guest/product/getusdaproducts?userQuery=${val}`,
        success: function (data) {
            $("#pView").html(data);
        }
    })
});

function activateModal(productName) {

    $('.food-details-modal').modal('show');
    $('.modal-title').html(productName);

    selectFoodByName(productName);
 
}




function selectFoodByName(foodName) {

    console.log('calling from product.js...');



    $.ajax({
        url: `/Guest/Food/GetFoodDetailsByName?foodName=${foodName}`,
        type: 'GET',
        success: function (data) {
            if (data) {

                console.log('DATA PULLED FROM DB!');
                $(`#foodCard`).html(data);

                //listenForTabs();
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
