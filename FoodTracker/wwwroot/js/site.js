// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/* global bootstrap: false */
/* Food chips' FODMAP badge tooltips*/
(() => {
    'use strict'
    const tooltipTriggerList = Array.from(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    tooltipTriggerList.forEach(tooltipTriggerEl => {
        new bootstrap.Tooltip(tooltipTriggerEl)
    })
})()



//$(window).on("resize", function() {

//    const windowWidth = $(window).width(); // visible
//    const windowHeight = $(window).height(); // visible
//    const docWidth = $(document).width(); // full document
//    const docHeight = $(document).height(); // full document



//    if (windowWidth < 576) {
//        $(".label-month").each(function (p) {
//            $(this)[0].innerText = $(this)[0].innerText.substring(0, 3);
//        });

//        $(".label-day").each(function (p) {

//            $(this)[0].innerText = $(this)[0].innerText.substring(0, 1);
//        });
//    } else {
//        $(".label-month").each(function (p) {
//            $(this)[0].innerText = $(this)[0].innerText.substring(0, 3);
//        });

//        $(".label-day").each(function (p) {

//            $(this)[0].innerText = $(this)[0].innerText.substring(0, 1);
//        });
//    }
//});

updateUI();

$(window).on("resize", updateUI);
document.addEventListener("load-reaction-details", updateUI);

function updateUI() {
    const outerWindowWidth = $(window).outerWidth(); // get outer width to accommodate for the scrollbar!

    if (outerWindowWidth < 576) {

        // Calendar View
        $(".month-label-full").hide();

        $(".calendar-month-option").each(function (p) {
            $(this)[0].innerText = $(this).attr("short");
        });

        // calendar day labels
        $(".day-label-short").hide();
        $(".day-label-full").hide();
     
        // reaction picker view
        $(".reaction-severity-name").hide();
        $(".reaction-severity-id").show();

    }

    //else if (windowWidth < 992) {
    //}

    //else if (outerWindowWidth < 992) {

    //    $(".day-label-short").show();
    //    $(".day-label-full").hide();


    //    $(".calendar-month-option").each(function (p) {
    //        $(this)[0].innerText = $(this).attr("long");
    //    });


    //    // reaction picker view
    //    $(".reaction-severity-name").show();
    //    $(".reaction-severity-id").hide();
    //}

    else {
        $(".day-label-short").show();
        $(".day-label-full").show();

        $(".calendar-month-option").each(function (p) {
            $(this)[0].innerText = $(this).attr("long");
        });
        
        // reaction picker view
        $(".reaction-severity-name").show();
        $(".reaction-severity-id").hide();
    }
}
