$(function () {

    $("#ddlAppList").change(function () {
        var selValue = $('#ddlAppList').val();
        $.ajax({
            type: "GET",
            url: '/AppRole/GetAppRoleList',
            data: { selectedAppID: selValue },
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var table = $('#table-data').DataTable();
                table.clear();
                table.destroy();
                $('#table-data').DataTable({
                    data: data,
                    columns: [
                        { data: 'number' },
                        { data: 'name' },
                        { data: 'description' },
                        /*{ data: '' },*/
                        {
                            data: null,
                            //render: function (data, type, row) { return '<div class="crud-button align-center mb-3"><button class="btn btn-success" id="detail-btn" onclick="location.href= \'@Url.Action("Details", "AppRole", new { roleID= 1 })\'">Chi tiết</button><button class="btn btn-danger" id="delete-btn">Xóa</button></div>' }
                            render: function (data, type, row) { return '<div class="crud-button align-center mb-3"><button class="btn btn-success" id="detail-btn" type="button" onclick="location.href= \'/AppRole/Details?roleID=' + row.id + '\'">Chi tiết</button><button class="btn btn-danger" id="delete-btn">Xóa</button></div>' }
                        }
                    ],
                });
            },
            error: function () {
                alert("Error while inserting data");
            }
        });
        return false;
    });



    $("#create-btn").click(function () {
        var selectedAppName = $('#ddlAppList option:selected').text();
        if (selectedAppName == null) {
            selectedAppName = document.getElementById('ddlAppList').value;
        }
        //window.location.href = '/AppRole/Create?selectedAppName=' + selectedAppName;
        $.ajax({
            type: "GET",
            url: '/AppRole/Create',
            data: { selectedAppName: selectedAppName },
            //dataType: "json",
            //contentType: "application/json; charset=utf-8",
            success: function () {
                window.location.href = '/AppRole/Create';
            },
            error: function () {
                alert("Error while inserting data");
            }
        });
    })
});