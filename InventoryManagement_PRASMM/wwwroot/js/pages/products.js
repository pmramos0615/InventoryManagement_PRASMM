﻿$(document).ready(function () {
    $('#datatableList').DataTable({
        "ajax": {
            "url": "/Products/List",
            "type": "Get",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "autoWidth": true }, //index 0
            { "data": "sku", "autoWidth": true }, //index 0
            { "data": "category", "autoWidth": true }, //index 0
            { "data": "brand", "autoWidth": true }, //index 0
            { "data": "srp", "autoWidth": true }, //index 0
            { "data": "unit", "autoWidth": true }, //index 0
            { "data": "qty", "autoWidth": true }, //index 0
            { "data": "createdBy", "autoWidth": true }, //index 0
            {
                "data": "id", "width": "100px", "render": function (data) {
                    return '<a class="me-3 btnEdit" href="/Products/Details/' + data + '"><img src="img/icons/edit.svg" alt="img"></a>';
                }
            }
        ],
        
        "language": {
            "emptyTable": "No data found, Please click on <b>Add New</b> button"
        }
    });
    oTable = $('#datatableList').DataTable();


    $("#btnDelete").click(function () {
        var id = $("#ID").val();
        Swal.fire({
            title: "Are you sure?",
            text: "You want to delete this product record!",
            type: "warning",
            showCancelButton: !0,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!",
            confirmButtonClass: "btn btn-primary",
            cancelButtonClass: "btn btn-danger ml-1",
            buttonsStyling: !1,
        }).then(function (t) {
            Delete(id);
            t.value &&
            Swal.fire({
                type: "success",
                title: "Deleted!",
                text: "Your product record has been deleted.",
                confirmButtonClass: "btn btn-success",
            });
        });
    });

    function Delete(id) {
        $.ajax({
            type: "GET",
            url: "/Products/Delete/" + id,
            success: function (data) {
                if (data.status) {
                    var url = $("#RedirectTo").val();
                    location.href = url;
                }
            }
        });
    }
});