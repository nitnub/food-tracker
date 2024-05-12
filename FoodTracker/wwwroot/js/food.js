
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

// Filter Food Chips
$('#foodSearchInput').on('keyup', function (e) {
    const phrase = $(this)
                        .val()
                        .toLowerCase()
                        .trim();
    $('#foodChipContainer').children().each(function (i) {
        const div = $(this)[0];
        const label = div.innerText
                        .toLowerCase()
                        .trim();

        if (label.length == 0 || label.includes(phrase)) {
            $(div).show();
        } else {
            $(div).hide();
        }
    })
})


