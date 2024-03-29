﻿
$(".food-chip").click(function (e) {
    var foodId = e.target.attributes.value.value;
    $.ajax({
        url: `/guest/reaction/getreactions?activeFoodId=${foodId}`,
        success: function (data) {
            $("#pView").html(data);
        }
    })
});


function addReaaction(foodId, typeId, severityId, active) {
    $.ajax({
        url: "/guest/reaction/addreaction",
        type: "POST",
        data: JSON.stringify({
            foodId, typeId, severityId,
            AppUserId: "0",
            Active: true,

        }),
        contentType: "application/json",
        dataType: "json",
        success: function () {
            console.log("Add was a success...");
        }
    })

}

function removeReaaction(foodId, typeId, severityId, active) {
    $.ajax({
        url: "/guest/reaction/removereaction",
        type: "POST",
        data: JSON.stringify({
            Id: 0,
            foodId, typeId, severityId,
            AppUserId: "0",
            Active: false,

        }),

        contentType: "application/json",
        dataType: "json",
        success: function () {
            console.log("Remove was a success...");
        }
    });
}

function viewReactions(id) {
    foodId = id;
    focusFood = id;
}




function testSubmit(s) {
    const [foodId, typeId, severityId] = s.split('-');

    var theDiv = $(`#radio${s}`);

    $(theDiv).on('click', (e) => e.preventDefault())

    if (theDiv.is(':checked')) {
        theDiv.prop('checked', false);
        removeReaaction(foodId, typeId, severityId);
    }
    else {
        theDiv.prop('checked', true);
        addReaaction(foodId, typeId, severityId);
    }
}
