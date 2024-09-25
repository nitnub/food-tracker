
function openCalendarModal(url, sectionName) {
    $.ajax({
        url,
        success: function (data) {
            $('#updateModalBody').html(data);

            updateSelectedTab(sectionName);

            $('#activityUpdateContainer').hide();
            $('#updateModal').modal('show');
        }
    })
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

    $.ajax({
        url: `/Guest/Calendar/UpdateView?year=${year}&month=${month}&day=${day}`,
        type: 'GET',
        success: function (data) {
            // TODO: Add toast?
        }
    })
}
