// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#table-data').DataTable({
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true
    });
    $('#table-data-modal').DataTable({
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true
    });
    $('#table-datasaledetail-modal').DataTable({
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true
    });
});


//Disable field
var applied = localStorage.getItem("applied") == "true";
if (applied) {
    $("#needenable").removeClass("disablefield");
    $("#needenableleft").addClass("disablefield");
    $("#btnEdit").removeClass("d-none");
    $("#btnView").removeClass("d-none");
    $("#btnCancelAll").removeClass("d-none");
} else {
    $("#needenable").hasClass("disablefield");
    $("#needenableleft").removeClass("disablefield");
    $("#btnEdit").hasClass("d-none");
    $("#btnView").hasClass("d-none");
    $("#btnCancelAll").hasClass("d-none");
}

$('#ActiveAdd').click(function () {
    $("#needenable").removeClass("disablefield");
    $("#needenableleft").addClass("disablefield");
    $("#btnAdd").removeClass("d-none");
    $("#btnCancelAll").removeClass("d-none");
    /*localStorage.setItem("applied", applied);*/
});

$('.btnenable').click(function () {
    if (!applied) {
        $("#needenable").removeClass("disablefield");
        $("#needenableleft").addClass("disablefield");
        $("#btnEdit").removeClass("d-none");
        $("#btnView").removeClass("d-none");
        $("#btnCancelAll").removeClass("d-none");
        applied = true;
    } else {
        $("#needenable").hasClass("disablefield");
        $("#needenableleft").removeClass("disablefield");
        $("#btnEdit").hasClass("d-none");
        $("#btnView").hasClass("d-none");
        $("#btnCancelAll").hasClass("d-none");
        applied = false;
    }
    localStorage.setItem("applied", applied);
});

$('.btncancel').click(function () {
    localStorage.clear();
});

//Binding Selectlist PromotionType to Textbox PromotionType
function ddlPromoTypeList() {
    var d = document.getElementById("ddlPromoType");
    var displaytext = d.options[d.selectedIndex].text;
    document.getElementById("txtPromoType").value = displaytext;
}

$("#generate").click(function () {
    $("#generatefield").prop("disabled", false);
    $("#importfield").prop("disabled", true);
});
$("#import").click(function () {
    $("#generatefield").prop("disabled", true);
    $("#importfield").prop("disabled", false);
});


//$('#Menubar a').on('click', function () {
//    var $this = $(this),
//        $bc = $('<div class="item"></div>');

//    $this.parents('li').each(function (n, li) {
//        var $a = $(li).children('a').clone();
//        $bc.prepend(' / ', $a);
//    });
//    $('.breadcrumb').html($bc.prepend('<a asp-area="" asp-controller="Home" asp-action="Index">Home</a>'));
//    return false;
//}) 
