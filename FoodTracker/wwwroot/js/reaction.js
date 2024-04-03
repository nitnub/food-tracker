
$(".food-chip").on('click', function () {
    //console.log(this.attributes.value.value);
    //var foodId = e.target.attributes.value.value;
    const foodId = this.attributes.value.value;
    $.ajax({
        url: `/Guest/Reaction/GetReactions?activeFoodId=${foodId}`,
        success: function (data) {
            $("#reactionView").html(data);
        }
    })
});


function toggleReaaction(foodId, typeId, severityId, active) {

    $.ajax({
        url: "/Guest/Reaction/ToggleReaction",
        type: "POST",
        data: JSON.stringify({
            foodId, typeId, severityId,
            AppUserId: "0",
            Active: true,

        }),
        contentType: "application/json",
        dataType: "json",
        success: function (r) {
            if (!r.success) {
                console.log("Unable to update reaction");
                return false;
            }
            console.log(r);

            const tst = $(`.food-chip[value="${foodId}"]`)[0];
            tst.classList.remove("red", "green", "yellow");
            if (r.updatedColor.length > 0) {
                tst.classList.add(r.updatedColor);

            }
            console.log(tst);
            console.log("Add was a success...");
            return true;
        }
    })

}



//function addReaaction(foodId, typeId, severityId, active) {

//    $.ajax({
//        url: "/guest/reaction/addreaction",
//        type: "POST",
//        data: JSON.stringify({
//            foodId, typeId, severityId,
//            AppUserId: "0",
//            Active: true,

//        }),
//        contentType: "application/json",
//        dataType: "json",
//        success: function (r) {
//            if (!r.success) {
//                console.log("Unable to update reaction");
//                return;
//            }
//            console.log(r);

//            const tst = $(`.food-chip[value="${foodId}"]`)[0];
//            tst.classList.remove("red", "green", "yellow");
//            if (r.updatedColor.length > 0) {
//                tst.classList.add(r.updatedColor);

//            }
//            console.log(tst);
//            console.log("Add was a success...");
//        }
//    })

//}

//function removeReaaction(foodId, typeId, severityId, active) {
//    $.ajax({
//        url: "/guest/reaction/removereaction",
//        type: "POST",
//        data: JSON.stringify({
//            Id: 0,
//            foodId, typeId, severityId,
//            AppUserId: "0",
//            Active: false,

//        }),
//        contentType: "application/json",
//        dataType: "json",
//        success: function () {
//            console.log("Remove was a success...");
//        }
//    });
//}

function viewReactions(id) {
    foodId = id;
    focusFood = id;
}

function testSubmit(s) {
    const [foodId, typeId, severityId] = s.split('-');
    const theDiv = $(`#radio${s}`);

    $(theDiv).on('click', (e) => e.preventDefault())
    if (theDiv.is(':checked')) {
        console.log("here2");
        toggleReaaction(foodId, typeId, severityId);
        theDiv.prop('checked', false);
        //theDiv.prop('checked', false);
        //removeReaaction(foodId, typeId, severityId);
    }
    else {
        toggleReaaction(foodId, typeId, severityId);
        theDiv.prop('checked', true);
        //addReaaction(foodId, typeId, severityId);
    }
}

function updateUserSafeFood(id) {
    $.ajax({
        url: `/Guest/Reaction/UpdateUserSafeFood?id=${id}`,
        type: 'POST',
        success: function (r) {
            if (r.success) {

                if (r.active) {
                    $('#userSafeFoodInput').prop('checked', true);
                    $('#reactionPicker').hide();
                } else {
                    $('#userSafeFoodInput').prop('checked', false);
                    $('#reactionPicker').show()
                }
            }
        }
    })
}