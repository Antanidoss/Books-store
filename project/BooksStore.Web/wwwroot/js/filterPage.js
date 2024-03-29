﻿$(document).ready(function () {
    $('[class*=categoryFilter-checkbox]').click((e) => {
        if ($(e.target).is(':checked')) {
            $('[name="CategoryIds[' + $(e.target).attr('id') + ']"]').val($(e.target).val());
        }
        else {
            $(`[name*="CategoryIds[${$(e.target).attr('id')}]"]`).val('0');
        }
    });

    $('[name*="CategoryIds"][value!="0"]').each((index, elem) => {
        $('[class*="categoryFilter-checkbox"][value="' + $(elem).val() + '"]').prop('checked', true)
    });
});

var leftPolzunokValue = $(".polzunok-input-5-left").val();
var rightPolzunokValue = $(".polzunok-input-5-right").val() == 0 ? 300 : $(".polzunok-input-5-right").val();

$(".polzunok-5").slider({
    min: 0,
    max: 500,
    values: [leftPolzunokValue, rightPolzunokValue],
    range: true,
    animate: "fast",
    slide: function (event, ui) {
        $(".polzunok-input-5-left").val(ui.values[0]);
        $(".polzunok-input-5-right").val(ui.values[1]);
    }
});

$(".polzunok-input-5-left").val($(".polzunok-5").slider("values", 0));
$(".polzunok-input-5-right").val($(".polzunok-5").slider("values", 1));

$(".polzunok-container-5 input").change(function () {
    var input_left = $(".polzunok-input-5-left").val().replace(/[^0-9]/g, ''),
        opt_left = $(".polzunok-5").slider("option", "min"),
        where_right = $(".polzunok-5").slider("values", 1),
        input_right = $(".polzunok-input-5-right").val().replace(/[^0-9]/g, ''),
        opt_right = $(".polzunok-5").slider("option", "max"),
        where_left = $(".polzunok-5").slider("values", 0);

    if (input_left > where_right) {
        input_left = where_right;
    }
    if (input_left < opt_left) {
        input_left = opt_left;
    }
    if (input_left == "") {
        input_left = 0;
    }
    if (input_right < where_left) {
        input_right = where_left;
    }
    if (input_right > opt_right) {
        input_right = opt_right;
    }
    if (input_right == "") {
        input_right = 0;
    }

    $(".polzunok-input-5-left").val(input_left);
    $(".polzunok-input-5-right").val(input_right);

    if (input_left != where_left) {
        $(".polzunok-5").slider("values", 0, input_left);
    }
    if (input_right != where_right) {
        $(".polzunok-5").slider("values", 1, input_right);
    }
});

