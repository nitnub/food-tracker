$('#searchQuery').on('keydown', function (e) {
    if (e.key.toLowerCase() == 'enter') {
        getProductsFromSearch();
    }
})
$("#searchButton").on('click', function (e) {
    getProductsFromSearch();
});


function getProductsFromSearch() {
    const query = $("#searchQuery").val();
    getProducts(query);
}

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

function getProducts(query, page = 1) {
    query = query.replace(' ', '%20');

    $.ajax({
        url: `/Guest/Product/GetUSDAProducts?userQuery=${query}&pageNumber=${page}`,
        success: function (data) {

            hideCameraControls();
            $("#productView").html(data);

            addFoodChipListener();
            updatePaginationDiv();
        }
    })
}

function hideCameraControls() {
    $('#upcDisplay')[0].innerText = '';
    $('#cameraViewContainer').hide();
}

function addFoodChipListener() {
    $(".food-chip").on('click', function (e) {
        const foodName = $(this).find('.food-chip-name')[0].innerText
        activateModal(foodName)
    });
}

function updatePaginationDiv() {
    const query = $('#searchQuery').val();
    const page = $('#productResultsHeader').attr('page');
    const pageCount = $('#productResultsHeader').attr('pageCount');

    const paginationDiv = getPaginationDiv(query, page, pageCount);
    $('.paginationDiv').html(paginationDiv)
}