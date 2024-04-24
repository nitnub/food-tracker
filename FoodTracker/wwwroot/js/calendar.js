



function updateYear(year) {
    updateCalendar(year, new Date().getMonth() + 1);
}
function updateMonth(month) {
    updateCalendar(new Date().getFullYear(), month);
}

function updateCalendar(year, month, day = 1) {
    console.log("Update Calendar:", `/Guest/Calendar/UpdateView?year=${year}&month=${month}&day=${day}`)
    $.ajax({
        url: `/Guest/Calendar/UpdateView?year=${year}&month=${month}&day=${day}`,
        type: 'GET',
        //contentType: 'application/json',
        success: function (data) {
            console.log('Success!');
            //console.log(data);
            //if (data.success) {
            //    $(`#foodChip-${id}`).remove();
            //    $('.delete-food-modal').modal('hide');
            //    resetAllFields();
            //}
        }
    })
}