imgInput.onchange = evt => {
    const [file] = imgInput.files
    if (file) {
        imgPreview.src = URL.createObjectURL(file)
    }
}


//$('.datepicker').datepicker({
//    format: 'dd/mm/yyyy',
//    startDate: '-3d'
//});




$(document).ready(function () {
    $('#table-data').DataTable().clear();
    //$('#table-data').DataTable().destroy();
    var table = $('#table-data').DataTable();
    table.clear();
    //table.destroy();
    $('#table-data').DataTable({
        language: {
            lengthMenu: "Display _MENU_ records per page",
            zeroRecords: "Nothing found - sorry",
            info: "Showing page _PAGE_ of _PAGES_",
            infoEmpty: "No records available",
            infoFiltered: "(filtered from _MAX_ total records)",
            search: "",
            searchPlaceholder: "Filter",
        },
    });
});