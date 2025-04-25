$(document).ready(function () {
    var table = $('#datatableList').DataTable({
        "processing": true,
        "serverSide": true,
        "dom": '<"top">t<"bottom"p>i<"clear">',
        "ajax": {
            "url": "/Products/List",
            "type": "POST",
            "datatype": "json"
            //"success": function (data) {
            //    console.log(data)
            //}
        },
        "columns": [
            { "data": "sku" },
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return `<div style="display: grid; grid-template-columns: auto minmax(0, 1fr); align-items: center; column-gap: 10px;">
                              <img 
                                src="${row.imageURL}" 
                                alt="Image" 
                                style="width: 50px; height: 50px; object-fit: cover; border: 1px solid #ddd; border-radius: 4px; flex-shrink: 0;">
                              <span style="text-align: beginning; overflow-wrap: break-word; white-space: normal;">${row.name}</span>
                            </div>`;
                },
                "autoWidth": true
            },
            { "data": "category" },
            { "data": "brand" },
            {
                "data": "srp",
                "render": function (data)
                {
                    return '<span>'+data+'</span>';
                }
            },
            { "data": "productType" },
            { "data": "unit" },
            {
                "data": "id",
                "render": function (data, type, row, meta)
                {
                    return `<button type="button" class="btn btn-outline-primary btn-view" data-id="${data}"><i class="fas fa-eye"></i></button> |
                            <button type="button" class="btn btn-outline-primary btn-edit" data-id="${data}"><i class="fas fa-pencil-alt"></i></button> |
                            <button type="button" class="btn btn-outline-primary btn-delete" data-id="${data}"><i class="fas fa-trash-alt"></i></button>`;
                }
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "responsive": true
    });

});

$(document).on("click", ".btn-search", function () {
    alert("this button is clicked");
});