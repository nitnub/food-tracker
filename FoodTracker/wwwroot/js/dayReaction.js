
function dayReactionUpdate(dateTime) {
    dayId = dateTime;
    $.ajax({
        url: `/Guest/Calendar/GetDayReactions?dateTime=${dateTime}`,
        success: function (data) {
            $('#updateModalBody').html(data);

            updateSelectedTab('Overall Feeling')
            $('#updateModal').modal('show');
        }
    })
}

function updateReaction(s) {
    const [reactantId, typeId, severityId] = s.split('_');
    const theDiv = $(`#radio${s}`);

    $(theDiv).on('click', (e) => e.preventDefault());

    toggleReaaction(reactantId, typeId, severityId, theDiv);
}

function toggleReaaction(reactantId, typeId, severityId, theDiv) {
    $.ajax({
        url: '/Guest/Calendar/ToggleDayReaction',
        type: 'POST',
        data: JSON.stringify({
            IdentifiedOn: reactantId,
            reactantId, typeId, severityId,
            AppUserId: '0',
            Active: true,
        }),
        contentType: 'application/json',
        dataType: 'json',
        success: function (r) {
            if (!r.success || r.isUserSafeDay) return;

            updateDayReactionBar(reactantId, r.activeIcons, r.dayColor.toLowerCase());
            updateDayReactionColor(reactantId, r.dayColor.toLowerCase());

            const checked = theDiv.is(':checked');
            theDiv.prop('checked', !checked);
        }
    })
}

function updateUserSafeDay(date) {

    $.ajax({
        url: `/Guest/Calendar/UpdateUserSafeDay?date=${date}`,
        type: 'POST',
        success: function (r) {
            if (!r.success)
                return;

            if (r.active) {
                updateDayReactionColor(date, 'green');

                $('#userSafeDayInput').prop('checked', true);
                $('#dayReactionPicker').children().hide();
            } else {
                updateDayReactionColor(date, r.updatedColor);

                $('#userSafeDayInput').prop('checked', false);
                $('#dayReactionPicker').children().removeClass("d-none").show();
            }
            updateDayReactionBar(date, r.reactionIcons, r.updatedColor);
        }
    })
}

function updateDayReactionColor(date, color) {
    updateDateBubbleColor(date, color)
    //updateDayReactionBar(color);
}

function updateReactionFocusHeaderColor(color) {
    const header = $('#reactionFocusHeader')[0];
    header.classList.remove('red', 'green', 'yellow', 'gray');

    if (color != null && color.length != 0) {
        header.classList.add(color);
    }
}

function updateDateBubbleColor(date, color) {
    const dateBubble = $(`#day_${date}`)[0]

    dateBubble.classList.remove('bg-red', 'bg-green', 'bg-yellow', 'bg-gray');

    if (color != null && color.length != 0) {
        dateBubble.classList.add(`bg-${color}`);
    }
}

function updateDayReactionBar(date, activeIcons, dayColor) {

    let updatedReactions = '';
    let additionalCount = '';

    if (activeIcons.length > MAX_DISPLAY_REACTIONS) {
        additionalCount = `
            <div>
                <div>+${activeIcons.length - MAX_DISPLAY_REACTIONS + 1}</div>
            </div>
        `
        activeIcons.length = MAX_DISPLAY_REACTIONS - 1;
    }

    activeIcons.forEach(icon => {
        updatedReactions += `
                <div
                    class="calendar-badge ${icon.color}"
                    data-toggle="tooltip" 
                    data-placement="top" 
                    title="${icon.name}"
                    >
                    ${icon.html}
                </div>`
    })

    updatedReactions += additionalCount;

    $(`#reactionContainer${date}`)[0].innerHTML = updatedReactions;
    updateDayReactionColor(date, dayColor.toLowerCase());
}

// Activate modal tab functionality
$('#overall-tab').on('click', function () {
    dayReactionUpdate(dayId);
});