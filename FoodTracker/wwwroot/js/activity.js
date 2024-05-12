
function openDayActivityUpdate(dateTime) {
    dayId = dateTime;

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

    for (let i = 5; i <= 55; i += 5) {
        output += `<option value="${i}">${i}</option>`
    }
    return output;
}

function addActivityItem() {

    const activityGroup = document.getElementById('activityGroup')
    const div = document.createElement("div");
    const actId = new Date().getTime().toString();

    div.setAttribute("class", "border form-group rounded-2 my-2 shadow p-2 activity-" + actId);

    div.innerHTML = `
        <input value="0" hidden="" type="number" data-val="true" data-val-required="The Id field is required." id="Activities_${actId}__Activity_Id" name="Activities[${actId}].Activity.Id"><input name="__Invariant" type="hidden" value="Activities[${actId}].Activity.Id">
        <div class="d-flex col-6">

            <div class="form-floating py-2 mx-1 col-6">
                <select class="form-select" data-val="true" data-val-required="The TypeId field is required." id="Activities_${actId}__Activity_TypeId" name="Activities[${actId}].Activity.TypeId">
                     ${getActivityTypes(activitiesList)}
                </select>
                <label class="ms-2">Activity Type</label>
                <span class="text-danger field-validation-valid" data-valmsg-for="Activities[${actId}].Activity.TypeId" data-valmsg-replace="true"></span>
            </div>

            <div class="form-floating py-2 mx-1 col-6">
                <select class="form-select " data-val="true" data-val-required="The IntensityId field is required." id="Activities_${actId}__Activity_IntensityId" name="Activities[${actId}].Activity.IntensityId">
                            ${getIntensities(intensitiesList)}
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

    $('#activityUpdateContainer').show();
}

function removeActivity(id) {
    $(`.activity-${id}`).remove();
}

// Activate modal tab functionality
$('#activities-tab').on('click', function () {
    openDayActivityUpdate(dayId);
})
