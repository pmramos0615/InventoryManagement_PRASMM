$(document).ready(function () {
    $('#datatableList').DataTable({
        "processing": true, // for show processing bar
        "serverSide": true, // for process on server side
        "orderMulti": false, // for disable multi column order,
        "dom": 'lrtip',
        "ajax": {
            "url": "/PurchaseOrder/List",
            "type": "Post",
            "datatype": "json"
        },
        "columns": [
            { "data": "supplierName", "autoWidth": true }, 
            { "data": "poNo", "autoWidth": true }, 
            { "data": "poDateStr", "autoWidth": true }, 
            { "data": "orderStatus", "autoWidth": true }, 
            { "data": "grandTotal", "autoWidth": true }, 
            { "data": "paid", "autoWidth": true }, 
            { "data": "due", "autoWidth": true }, 
            { "data": "paymentStatus", "autoWidth": true }, 
            {
                "data": "id", "width": "100px", "render": function (data) {
                    return '<a class="me-3 btnEdit" href="/PurchaseOrder/Details/' + data + '"><img src="img/icons/edit.svg" alt="img"></a>';
                }
            }
        ],
        
        "language": {
            "emptyTable": "No data found, Please click on <b>Add New</b> button"
        }
    });
    oTable = $('#datatableList').DataTable();


    $('#btnSearch').click(function () {
        //Apply search for Employee Name // DataTable column index 0
        oTable.columns(0).search($('#SupplierName').val().trim());
        oTable.columns(1).search($('#PONo').val().trim());
        oTable.columns(2).search($('#DateFrom').val().trim());
        oTable.columns(3).search($('#DateTo').val().trim());
        //hit search on server
        oTable.draw();
    });


    $("#btnDelete").click(function () {
        var id = $("#ID").val();
        Swal.fire({
            title: "Are you sure?",
            text: "You want to delete this purchase order record!",
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
                text: "Your purchase order record has been deleted.",
                confirmButtonClass: "btn btn-success",
            });
        });
    });

    function Delete(id) {
        $.ajax({
            type: "GET",
            url: "/PurchaseOrder/Delete/" + id,
            success: function (data) {
                if (data.status) {
                    var url = $("#RedirectTo").val();
                    location.href = url;
                }
            }
        });
    }

    //THIS IS A TEST OVERRIDE THE CONSOLE.LOG of the  browser
    console.log = function () { };
});