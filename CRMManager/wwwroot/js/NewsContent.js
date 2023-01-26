function SelectUtility() {
    let parentElementVN = document.getElementById("pills-content");
    let contentNewsVN = parentElementVN.querySelector(".k-content").innerHTML;
    let parentElementEN = document.getElementById("pills-content-en");
    let contentNewsEN = parentElementEN.querySelector(".k-content").innerHTML;


    let txtPreviewVN = document.getElementById("preview-newsVN");
    let txtPreviewEN = document.getElementById("preview-newsEN");
    //txtPreviewVN.innerHTML = contentNewsVN;
    //txtPreviewEN.innerHTML = contentNewsEN;
    txtPreviewVN.innerHTML = window.frames[0].document.body.innerHTML;
    txtPreviewEN.innerHTML = window.frames[1].document.body.innerHTML;
}

let validateSizeImages = $(".ValidateSizeImage");

var validExt = ".png, .gif, .jpeg, .jpg";
function fileExtValidate(fdata) {
    var filePath = fdata.value;
    var getFileExt = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
    var pos = validExt.indexOf(getFileExt);
    if (pos < 0) {
        alert("This file is not allowed, please upload image file.");
        return false;
    } else {
        return true;
    }
}

function ValidateSizeImage(input) {
    let elementID = $(input).attr("data-id");
    let imageSize = elementID.split('-')[1];
    let file = input.files[0];
    let fileUp = input;

    if (fileExtValidate(fileUp)) {
        var reader = new FileReader();
        console.log(reader);
        reader.onload = function (e) {
            var image = new Image();
            image.src = reader.result;
            image.onload = function () {
                let width = parseFloat(imageSize.split('x')[0]);
                let height = parseFloat(imageSize.split('x')[1]);
                let from = (width / height) - 0.1;
                let to = (width / height) + 0.1;
                let rate = this.width / this.height;
                if (rate >= from && rate <= to && this.width >= (width - width * 0.1)) {
                    $("#img-" + imageSize).attr('src', e.target.result);
                }
                else {
                    alert('Invalid Image size: ' + imageSize);
                    input.value = "";
                    $("#img-" + imageSize).removeAttr('src');
                }
            }

        }
        if (file) {
            reader.readAsDataURL(file);
        } else {
            preview.src = "";
        }
        //reader.readAsDataURL(input.files[0]);
    }
    else {
        preview.src = "";
        txtUploadFile.value = "";
    }
}

validateSizeImages.each(function () {
    $(this).change(function () {
        ValidateSizeImage(this)
    })
})