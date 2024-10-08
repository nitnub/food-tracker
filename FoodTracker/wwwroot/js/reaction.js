﻿
$('.food-chip').on('click', function () {

    const foodId = this.attributes.value.value;
    let foodColor = "";

    if (this.classList.contains("yellow")) {
        foodColor = "yellow";
    }
    if (this.classList.contains("red")) {
        foodColor = "red";
    }

    $.ajax({
        url: `/Guest/Reaction/GetReactions?activeFoodId=${foodId}`,
        success: function (data) {
            $('#reactionView').html(data);

            document.dispatchEvent(new CustomEvent("load-reaction-details"));

            if (reactionHeaderIsSafe()) {
                return;
            }

            updateReactionFocusHeaderColor(foodColor);
        }
    })
});

function toggleReaaction(foodId, typeId, severityId, active) {
    $.ajax({
        url: '/Guest/Reaction/ToggleReaction',
        type: 'POST',
        data: JSON.stringify({
            foodId, typeId, severityId,
            AppUserId: '0',
            Active: true,
        }),
        contentType: 'application/json',
        dataType: 'json',
        success: function (r) {
            updateDayReactionColor(foodId, r.updatedColor);
        }
    })
}

function updateReaction(s) {
    const [foodId, typeId, severityId] = s.split('-');
    const theDiv = $(`#radio${s}`);

    $(theDiv).on('click', (e) => e.preventDefault());

    const checked = theDiv.is(':checked');

    toggleReaaction(foodId, typeId, severityId);
    theDiv.prop('checked', !checked);
}

function updateUserSafeFood(id) {
    $.ajax({
        url: `/Guest/Reaction/UpdateUserSafeFood?id=${id}`,
        type: 'POST',
        success: function (r) {
            if (r.active) {
                updateDayReactionColor(id, 'green');

                $('#userSafeFoodInput').prop('checked', true);
                $('#reactionPicker').hide();
            } else {
                updateDayReactionColor(id, r.updatedColor);

                $('#userSafeFoodInput').prop('checked', false);
                $('#reactionPicker').show()
            }
        }
    })
}

function updateDayReactionColor(foodId, color) {
    updateFoodChipColor(foodId, color)
    updateReactionFocusHeaderColor(color);
}

function updateReactionFocusHeaderColor(color) {
    const header = $('#reactionFocusHeader')[0];
    header.classList.remove('red', 'green', 'yellow');

    if (color != null && color.length != 0) {
        header.classList.add(color);
    }
}

function updateFoodChipColor(foodId, color) {
    const chip = $(`.food-chip[value='${foodId}']`)[0];
    chip.classList.remove('red', 'green', 'yellow');

    if (color != null && color.length != 0) {
        chip.classList.add(color);
    }
}

function reactionHeaderIsSafe() {
    return $('#reactionFocusHeader')[0].classList.contains('green');
}
