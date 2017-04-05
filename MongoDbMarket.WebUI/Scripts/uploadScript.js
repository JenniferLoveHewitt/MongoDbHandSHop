$("input[type=file]").on('change', function () {

    if (typeof (FileReader) != "undefined") {

        var image_holder = $("#image-holder");
        var reader = new FileReader();

        reader.onload = function (e) {
            $("<img />", {
                "src": e.target.result,
                "class": "thumb-image",
                "width": "100px",
                "height" : "120px"
            }).css("padding-right", "30px")
                .appendTo(image_holder);
        }

        $(".imageInfo p").remove();

        image_holder.show();
        reader.readAsDataURL($(this)[0].files[0]);
    }
    else {
        alert("Ошибка во время загрузки");
    }
});
