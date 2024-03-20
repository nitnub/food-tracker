//namespace FoodTrackerWeb.wwwroot.js
//{
//    public class reaction
//    {
//    }
//}
//$(document).ready(function () {

//    $('label').(console.log("fff"));
//})
var foodId;
function addReaaction(foodId, typeId, severityId, active) {
    console.log(`Adding: ${foodId} =>  Type: ${typeId} => ${severityId}`);
}

function removeReaaction(foodId, typeId, severityId, active) {
    console.log(`Removing: ${foodId} => Type: ${typeId} => ${severityId}`);
}

function viewReactions(id) {
    foodId = id;
    console.log(`Open reactions for Food ID: ${foodId}`)
}

$('.food-chip').on('mouseup', function (e) {
    console.log(e);
})

$('.form-check').on('mouseup', function (e) {
    var radio = $(this).find('input[type=radio]');
    const [typeId, severityId] = radio[0].value.split('-');
    if (radio.is(':checked')) {
        removeReaaction(foodId, typeId, severityId);
        //console.log(radio[0])
        //console.log(radio[0].value)
        radio.prop('checked', false);

    } else {
        addReaaction(foodId, typeId, severityId);
        radio.prop('checked', true);
    }

});

// Disable default radio behavior
$('.form-check').on('click', function (e) {
    e.preventDefault();
});