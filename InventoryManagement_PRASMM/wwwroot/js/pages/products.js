$(document).ready(function () {
    $('#datatableList').DataTable({
        "ajax": {
            "url": "/Products/List",
            "type": "Get",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "name",
                "autoWidth": true,
                "render": function (data, type, row) {
                    return `<div style="display: flex; align-items: center;">
                    <img src="${row.imageURL}" alt="" style="width: 30px; height: 30px; border-radius: 50%; margin-right: 10px;">
                    <span>${data}</span>
                </div>`;
                }
            },
            { "data": "sku", "autoWidth": true }, 
            { "data": "category", "autoWidth": true }, 
            { "data": "brand", "autoWidth": true }, 
            { "data": "srp", "autoWidth": true },
            { "data": "unit", "autoWidth": true },
            { "data": "qty", "autoWidth": true },
            { "data": "createdBy", "autoWidth": true },
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
});

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

$('#file-choose').click(function () {
    $('#file-input').click();
});

$(document).on("change", "#file-input", function () {
    var file = $('#file-input').val();

    if (file) {
        console.log(file)
        $('#input-text').val(file.split('\\').pop())
    }
    else
    {
        $('#input-text').val('No File Selected')
    }
})
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