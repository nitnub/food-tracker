
function openDayActivityUpdate(dateTime) {
    dayId = dateTime;
    console.log('DayID:', dayId);
    const dayConverted = Date.parse(dayId);

    const dc2 = Date.toString(dayConverted)
    console.log(dayConverted);
    console.log(dc2);
    $.ajax({
        url: `/Guest/Activity/GetDayActivities?dateTime=${dateTime}`,
        success: function (data) {
            $('#updateModalBody').html(data);

            updateSelectedTab('Activities'); 
  
            $('#updateModal').modal('show');
        }
    })
}





function dayActivityUpsert(dateTime) {

    $.ajax({
        url: `/Guest/Activity/UpsertDayActivities?dateTime=${dateTime}`,
        success: function (data) {
            $('#updateModalBody').html(data);
            $('#updateModal').modal('show');
        }
    })
}

function getActivityTypes(activitiesList) {
    const nameComparator = (a, b) => a.name.localeCompare(b.name);
    return activitiesList
                .sort(nameComparator)
                .map(a => `<option value="${a.id}">${a.name}</option>`);
}
function getIntensities(intensitiesList) {
    return intensitiesList
                .map(a => `<option value="${a.id}">${a.name}</option>`);
}

function getOptions(optionsObjectList) {
    const nameComparator = (a, b) => a.name.localeCompare(b.name);

    return optionsObjectList
                .sort(nameComparator)
                .map(a => `<option value="${a.id}">${a.name}</option>`);
}

function getHoursOptions() {
    let output = '<option value="0" selected="">0</option>';

    for (let i = 1; i <= 23; i++) {
        output += `<option value="${i}">${i}</option>`
    }
    return output;          
}
function getMinutesOptions() {
    let output = '<option value="0" selected="">0</option>';

    for (let i = 5; i <= 55; i+=5) {
        output += `<option value="${i}">${i}</option>`
    }
    return output;
}

function addActivityItem() {
    //const container = $('#activityGroup');

    //foodOptions = createFoodOptions(foodJson);
    const activityGroup = document.getElementById('activityGroup')
    const div = document.createElement("div");
    const actId = new Date().getTime().toString();


    console.log(activitiesList);
    console.log(intensitiesList);


    //div.prop('id', 'my-id'+actId);
    div.setAttribute("class", "border form-group rounded-2 my-2 shadow p-2 activity-" + actId);

    
/*    <div id="activity-item-${actId}" class="border form-group rounded-2 my-2 shadow bg-white p-2">*/
 
    div.innerHTML = `
        <input value="0" hidden="" type="number" data-val="true" data-val-required="The Id field is required." id="Activities_${actId}__Activity_Id" name="Activities[${actId}].Activity.Id"><input name="__Invariant" type="hidden" value="Activities[${actId}].Activity.Id">
        <div class="d-flex col-6">

            <div class="form-floating py-2 mx-1 col-6">
                <select class="form-select" data-val="true" data-val-required="The TypeId field is required." id="Activities_${actId}__Activity_TypeId" name="Activities[${actId}].Activity.TypeId">
                     ${getActivityTypes(activitiesList) }
                </select>
                <label class="ms-2">Activity Type</label>
                <span class="text-danger field-validation-valid" data-valmsg-for="Activities[${actId}].Activity.TypeId" data-valmsg-replace="true"></span>
            </div>

            <div class="form-floating py-2 mx-1 col-6">
                <select class="form-select " data-val="true" data-val-required="The IntensityId field is required." id="Activities_${actId}__Activity_IntensityId" name="Activities[${actId}].Activity.IntensityId">
                            ${getIntensities(intensitiesList) }
                </select>
                <label class="ms-2">Intensity</label>
                <span class="text-danger field-validation-valid" data-valmsg-for="Activities[${actId}].Activity.Duration.Hours" data-valmsg-replace="true"></span>
            </div>

            <div class="form-floating py-2 mx-1 col-5">
                <input type="time" value="12:00" class="form-control" data-val="true" data-val-required="The Start Time field is required." id="Activities_${actId}__Activity_DateTime" name="Activities[${actId}].Activity.DateTime"><input name="__Invariant" type="hidden" value="Activities[${actId}].Activity.DateTime">
                <label class="ms-2">Start Time</label>
                <span class="text-danger field-validation-valid" data-valmsg-for="Activities[${actId}].Activity.DateTime" data-valmsg-replace="true"></span>
            </div>

            <div class="form-floating py-2 mx-1 col-3">
                <select class="form-select " data-val="true" data-val-required="The Hours field is required." id="Activities_${actId}__Hours" name="Activities[${actId}].Hours">
                 ${getHoursOptions()}
                </select>
                <label class="ms-2">Hours</label>
                <span class="text-danger field-validation-valid" data-valmsg-for="Activities[${actId}].Hours" data-valmsg-replace="true"></span>
            </div>

            <div class="form-floating py-2 mx-1 col-3">
                <select class="form-select" data-val="true" data-val-required="The Minutes field is required." id="Activities_${actId}__Minutes" name="Activities[${actId}].Minutes">
                   ${getMinutesOptions()}
                </select>
                <label class="ms-2">Minutes</label>
                <span class="text-danger field-validation-valid" data-valmsg-for="Activities[${actId}].Minutes" data-valmsg-replace="true"></span>
            </div>

        </div>
        <div style="display: flex; justify-content:end">
            <a class="link-dark" type="button" onclick="removeActivity('${actId}')">Remove</a>
        </div>`
    activityGroup.appendChild(div)

    //$(`#nmiUnits_${newId}`).on('change', function () {
    //    $(`#nmiUnits_${newId}`).valid();
    //});

    //addDynamicFoodSelect(newId);

    $('#activityUpdateContainer').show();
}

function removeActivity(id) {
    $(`.activity-${id}`).remove();
}

//function addDynamicFoodSelect(newId) {
//    let previous_value;
//    let current_value;

//    $(`#nmiFood_${newId}`).on('click', function (e) {
//        previous_value = $(this)[0].value;
//    }).on('change', function () {
//        current_value = $(this).val()
//        makeMealItemFoodVisible(previous_value);

//        // hide new selection
//        const tar = $('.mi-food').find($(`option[value='${current_value}']`));
//        tar.each(function () {
//            this.hidden = true;
//        })
//    });

//}


//function updateReaction(s) {
//    const [reactantId, typeId, severityId] = s.split('_');
//    const theDiv = $(`#radio${s}`);

//    $(theDiv).on('click', (e) => e.preventDefault());
//    const checked = theDiv.is(':checked');

//    toggleReaaction(reactantId, typeId, severityId);
//    theDiv.prop('checked', !checked);
//}

//function toggleReaaction(reactantId, typeId, severityId, active) {
//    let updatedReactions = "";
//    let icon;
//    let color;

//    $.ajax({
//        url: '/Guest/Calendar/ToggleDayReaction',
//        type: 'POST',
//        data: JSON.stringify({
//            IdentifiedOn: reactantId,
//            reactantId, typeId, severityId,
//            AppUserId: '0',
//            Active: true,
//        }),
//        contentType: 'application/json',
//        dataType: 'json',
//        success: function (r) {
//            if (!r.success || r.isUserSafeDay) return;

//            updateDayReactionBar(reactantId, r.activeIcons, r.dayColor.toLowerCase());
//            updateDayReactionColor(reactantId, r.dayColor.toLowerCase());
//        }
//    })
//}

//function updateUserSafeDay(date) {
//    let updatedReactions = "";

//    $.ajax({
//        url: `/Guest/Calendar/UpdateUserSafeDay?date=${date}`,
//        type: 'POST',
//        success: function (r) {
//            if (!r.success)
//                return;

//            if (r.active) {
//                updateDayReactionColor(date, 'green');

//                $('#userSafeDayInput').prop('checked', true);
//                $('#dayReactionPicker').children().hide();
//            } else {
//                updateDayReactionColor(date, r.updatedColor);

//                $('#userSafeDayInput').prop('checked', false);
//                $('#dayReactionPicker').children().removeClass("d-none").show();
//            }
//           updateDayReactionBar(date, r.reactionIcons, r.updatedColor);
//        }
//    })
//}

//function updateDayReactionColor(date, color) {
//    updateDateBubbleColor(date, color)
//    //updateDayReactionBar(color);
//}

//function updateReactionFocusHeaderColor(color) {
//    const header = $('#reactionFocusHeader')[0];
//    header.classList.remove('red', 'green', 'yellow', 'gray');

//    if (color != null && color.length != 0) {
//        header.classList.add(color);
//    }
//}

//function updateDateBubbleColor(date, color) {
//    const dateBubble = $(`#day_${date}`)[0]

//    dateBubble.classList.remove('bg-red', 'bg-green', 'bg-yellow', 'bg-gray');

//    if (color != null && color.length != 0) {
//        dateBubble.classList.add(`bg-${color}`);
//    }
//}

//function updateDayReactionBar(date, activeIcons, dayColor) {

//    let updatedReactions = "";
//    activeIcons.forEach(icon => {
//        updatedReactions += `
//            <div
//                class="calendar-badge ${icon.color}"
//                data-toggle="tooltip"
//                data-placement="top"
//                title="${icon.name}"
//                >
//                ${icon.html}
//            </div>`
//    })

//    $(`#reactionContainer${date}`)[0].innerHTML = updatedReactions;
//    updateDayReactionColor(date, dayColor.toLowerCase());
//}

// Activate modal tab functionality
$('#activities-tab').on('click', function () {
    openDayActivityUpdate(dayId);
})

