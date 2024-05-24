(() => {
    // The width and height of the captured photo. We will set the
    // width to the value defined here, but the height will be
    // calculated based on the aspect ratio of the input stream.

    //const width = 320; // We will scale the photo width to this
    const width = 640; // We will scale the photo width to this
    let height = 0; // This will be computed based on the input stream

    // |streaming| indicates whether or not we're currently streaming
    // video from the camera. Obviously, we start at false.

    let streaming = false;

    // The various HTML elements we need to configure or control. These
    // will be set by the startup() function.

    let video = null;
    let canvas = null;
    let photo = null;
    let captureUPCButton = null;

    function showViewLiveResultButton() {
        if (window.self !== window.top) {
            // Ensure that if our document is in a frame, we get the user
            // to first open it in its own tab or window. Otherwise, it
            // won't be able to request permission for camera access.
            document.querySelector(".contentarea").remove();
            const button = document.createElement("button");
            button.textContent = "View live result of the example code above";
            document.body.append(button);
            button.addEventListener("click", () => window.open(location.href));
            return true;
        }
        return false;
    }

    function startup() {
        if (showViewLiveResultButton()) {
            return;
        }
        video = document.getElementById("video");
        canvas = document.getElementById("canvas");
        photo = document.getElementById("photo");
        captureUPCButton = document.getElementById("captureUPCButton");

        navigator.mediaDevices
            .getUserMedia({ video: true, audio: false })
            .then((stream) => {
                video.srcObject = stream;
                video.play();
            })
            .catch((err) => {
                console.error(`An error occurred: ${err}`);
            });

        video.addEventListener(
            "canplay",
            (ev) => {
                if (!streaming) {
                    height = video.videoHeight / (video.videoWidth / width);

                    // Firefox currently has a bug where the height can't be read from
                    // the video, so we will make assumptions if this happens.

                    if (isNaN(height)) {
                        height = width / (4 / 3);
                    }

                    video.setAttribute("width", width);
                    video.setAttribute("height", height);
                    canvas.setAttribute("width", width);
                    canvas.setAttribute("height", height);
                    streaming = true;
                }
            },
            false,
        );

        captureUPCButton.addEventListener(
            "click",
            (ev) => {
                takepicture();
                ev.preventDefault();
                updateUPC();
            },
            false,
        );

        clearphoto();
    }

    // Fill the photo with an indication that none has been
    // captured.

    function clearphoto() {
        const context = canvas.getContext("2d");
        context.fillStyle = "#AAA";
        context.fillRect(0, 0, canvas.width, canvas.height);

        const data = canvas.toDataURL("image/png");
        photo.setAttribute("src", data);
    }

    // Capture a photo by fetching the current contents of the video
    // and drawing it into a canvas, then converting that to a PNG
    // format data URL. By drawing it on an offscreen canvas and then
    // drawing that to the screen, we can change its size and/or apply
    // other changes before drawing it.

    function takepicture() {
        const context = canvas.getContext("2d");
        if (width && height) {
            canvas.width = width;
            canvas.height = height;
            context.drawImage(video, 0, 0, width, height);

            const data = canvas.toDataURL("image/png");
            photo.setAttribute("src", data);
        } else {
            clearphoto();
        }
    }

    // Set up our event listener to run the startup process
    // once loading is complete.
    window.addEventListener("load", startup, false);
})();


//document.getElementById("imageFile").addEventListener("change", function () {
//    let file = this.files[0];
//    let image = document.getElementById('image');
//    let reader = new FileReader();

//    reader.addEventListener("load", function () {
//        image.src = reader.result;
//    }, false);

//    if (file) {
//        reader.readAsDataURL(file);
//    }
//});

//function upload() {
//    const host = location.hostname
//    const protocol = location.protocol
//    const uploadPath = '/upload'
//    let xhr = new XMLHttpRequest();
//    let formData = new FormData();
//    let file = document.getElementById('file').files[0];
//    formData.append('barcodeImage', file, file.name);
//    xhr.open('POST', uploadPath, true);
//    xhr.send(formData);
//    xhr.onreadystatechange = function () {
//        if (xhr.readyState == 4 && xhr.status == 200) {
//            document.getElementById("results").innerHTML = "Detection Results: " + xhr.responseText;
//        }
//    }
//}





//$(`#captureUPCButton`).on('click', updateUPC);

//function updateUPC() {

//    console.log("UPDATE UPC FUNC")
//    // get value

//    $.ajax({
//        url: `/Guest/Product/GetUPC/10`,
//        //url: `/Guest/Product/GetUPC/10?year=${year}&month=${month}&day=${day}`,
//        type: 'GET',
//        success: function (data) {
//            // TODO: Add toast?
//            if (data.success) {
//                $('#upcDisplay')[0].innerText = data.code;
//            }
//        }
//    })
//    // set div
//}


//$("#captureUPCButton").on('click', function () {



//    var file = document.getElementById("upcImage").src;
//    var file2 = document.getElementById("upcImage");

//    var image = document.getElementById("canvas").toDataURL("image/png");
//    var image2 = document.getElementById("canvas").toDataURL("image/bmp");
//    console.log('file');
//    console.log(file);
//    console.log(file2);
//    console.log(image);
//    console.log(image2);
//    $.ajax({
//        type: "POST",
//        url: 'Guest/Product/GetUPC',
//        data: { base64image: file },
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        success: function (result) {
//            //$('#myModal3').modal('hide'); //hide the modal
//        },
//        error: function () {
//            alert("Unable to add image");
//        }
//    });  












//    //var form = $("#cameraForm");
//    //var image = document.getElementById("canvas").toDataURL("image/png");
//    //image = image.replace('data:image/png;base64,', '');
//    //$("#imageData").val(image);
//    ////form.submit();

//    //console.log("image");
//    //console.log(image);
//    //const a = $('#canvas').val();
//    //const b = $('#canvas')[0].innerHTML;

//    //console.log(a);
//    //console.log(b);
//    //$.ajax({
//    //    url: `/Guest/Product/GetUPC`,
//    //    //url: `/Guest/Product/GetUPC/10?year=${year}&month=${month}&day=${day}`,
//    //    //type: 'GET',
//    //    type: 'POST',
//    //    data: JSON.stringify(image),
//    //    contentType: 'application/json',

//    //    success: function (data) {
//    //        // TODO: Add toast?
//    //        if (data.success) {
//    //            $('#upcDisplay')[0].innerText = data.code;
//    //        }
//    //    }
//    //})
//});



function updateUPC() {

    //var file = document.getElementById("upcImage").src;
    //var file2 = document.getElementById("upcImage");

    //var image = document.getElementById("canvas").toDataURL("image/png");
    //var image2 = document.getElementById("canvas").toDataURL("image/bmp");


    const base64Canvas = document.getElementById("canvas").toDataURL("image/jpeg").split(';base64,')[1];
    //console.log('file');
    //console.log(file);
    //console.log(file2);
    //console.log(image);
    //console.log(image2);
    $.ajax({
        type: "POST",
        url: 'Product/GetUPC',
        //data: { base64image: file },
        //data: JSON.stringify(image2),
        data: JSON.stringify(base64Canvas),
        //dataType: "json",
        //contentType: "application/json; charset=utf-8",
        contentType: 'application/json',
        success: function (data) {
            if (data.success) {
                $('#upcDisplay')[0].innerText = data.code;

                $('#cameraViewContainer').hide();
                getProducts(data.code);
            } else {
                $('#upcDisplay')[0].innerText = data.code;
                $('#cameraViewContainer').show();
            }
            console.log('data.code');
            console.log(data.code);
        },
        error: function () {
            $('#upcDisplay')[0].innerText = "Error";
        }
    });




}
