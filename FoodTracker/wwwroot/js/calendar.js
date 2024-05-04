
// const url = url: `/Guest/Activity/GetDayActivities?dateTime=${dateTime}`
function openCalendarModal(url, sectionName) {
    console.log("1", dateTime);
    $.ajax({
        url,
        success: function (data) {
            console.log("Activity Success");
            $('#updateModalBody').html(data);

            updateSelectedTab(sectionName);

            $('#activityUpdateContainer').hide();
            $('#updateModal').modal('show');
        }
    })
    console.log("2");
}


function updateSelectedTab(sectionName) {
    $('#updateTabList').children().each(function (p) {
        const button = $(this).find('button')[0];

        if (button.innerText == sectionName) {
            $(button).addClass('active');
            $(button).prop('aria-selected', true);
        } else {
            $(button).removeClass('active');
            $(button).prop('aria-selected', false);
        }
    });
}




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