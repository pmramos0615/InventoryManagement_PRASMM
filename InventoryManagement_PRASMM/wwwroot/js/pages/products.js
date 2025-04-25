
//$("#btnDelete").click(function () {
//    var id = $("#ID").val();
//    Swal.fire({
//        title: "Are you sure?",
//        text: "You want to delete this product record!",
//        type: "warning",
//        showCancelButton: !0,
//        confirmButtonColor: "#3085d6",
//        cancelButtonColor: "#d33",
//        confirmButtonText: "Yes, delete it!",
//        confirmButtonClass: "btn btn-primary",
//        cancelButtonClass: "btn btn-danger ml-1",
//        buttonsStyling: !1,
//    }).then(function (t) {
//        (function (t) {
//            if (t.value) {
//                Delete(id);
//                Swal.fire({
//                    type: "success",
//                    title: "Deleted!",
//                    text: "Your product record has been deleted.",
//                    confirmButtonClass: "btn btn-success",
//                });
//            }
//        })
//    });
//});
$("#btnDelete").on("click", function () {
    var id = $("#ID").val();
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",  // Use 'icon' instead of 'type'
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!",
        customClass: {
            confirmButton: "btn btn-primary",
            cancelButton: "btn btn-danger ml-1"
        }
    }).then((result) => {
        if (result.isConfirmed) {  
            Swal.fire({
                icon: "success",
                title: "Deleted!",
                text: "Your file has been deleted.",
                confirmButtonColor: "#3085d6",
                customClass: {
                    confirmButton: "btn btn-success"
                }
            }).then(() => {
                if (result.isConfirmed) { 
                    Delete(id);
                    window.location.href = "/Products";
                }
                
            });
        }
    });
});

$(document).on("click", ".btnSwal", function () {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Once the data is saved, modifications may no longer be possible. Please ensure all information is accurate before proceeding.',
        imageUrl: '/img/CustomSwalIcons/questionLogo.png',
        imageAlt: 'Question Logo',
        customClass: {
            image: 'custom-circle-image'
        },
        showCancelButton: true,
        confirmButtonText: 'Proceed',
        cancelButtonText: 'Cancel',
        customClass: {
            confirmButton: 'btn btn-primary',
            cancelButton: 'btn btn-dark'
        }
    }).then((result) => {
        if (result.value) { // "Proceed" clicked

            // Check required fields
            if ($('#Name').val() === '' || $('#SKU').val() === '' || $('#CategoryID').val() === '0' || $('#BrandID').val() === '0' || $('#UnitID').val() === '0' || $('#BarCode').val() === '' || $('#ItemCode').val() === '' || parseFloat($('#AcquiredCost').val()) === 0 || parseFloat($('#MarkupPrice').val()) === 0 || parseFloat($('#SRP').val()) === 0 || parseInt($('#MinQty').val()) === 0 || $('#TaxID').val() === '0' || $('#TaxAmountID').val() === '0' || $('#ProductTypeID').val() === '0') {
                alert("Please fill out all required fields.");

            }
            else {
                if ($('#variantselection').is(':visible') || $('#variantspecific').is(':visible')) {
                    if ($('#ddVariantType').val() === '0' || $('#ddVariantSpecific').val() === '0') {
                        alert("Please fill out all required fields.");
                    }
                    else {
                        // Show loading animation and progress bar
                        Swal.fire({
                            html: `<div id="lottie-animation" style="width: 80%; height: 80%; margin: auto;"></div>
                                    <div id="progress-bar-container" style="width: 100%; height: 10px; background-color: #f3f3f3; border-radius: 5px; margin-top: 20px;">
                                        <div id="progress-bar" style="width: 0%; height: 100%; background-color: #4caf50; border-radius: 5px;"></div>
                                    </div>`,
                            showConfirmButton: false,
                            allowOutsideClick: false
                        });

                        $('#formProducts').submit();
                        //after Submit the ActionResult will return a NoCotentent result then we redirect to the Index page
                        var animation = lottie.loadAnimation({
                            container: $('#lottie-animation')[0],
                            renderer: 'svg',
                            loop: false,
                            autoplay: true,
                            path: '/js/animation/Animation - 1743129363779.json'
                        });

                        $('#progress-bar').animate({ width: '100%' }, 3000);

                        $(animation).on('complete', function () {
                            window.location.href = '/Products/Index';
                            $(window).on('load', function () {
                                $('#global-loader').hide(); // 
                            });
                        });

                    }
                }
                else {
                    Swal.fire({
                        html: `<div id="lottie-animation" style="width: 80%; height: 80%; margin: auto;"></div>
                                    <div id="progress-bar-container" style="width: 100%; height: 10px; background-color: #f3f3f3; border-radius: 5px; margin-top: 20px;">
                                        <div id="progress-bar" style="width: 0%; height: 100%; background-color: #4caf50; border-radius: 5px;"></div>
                                    </div>`,
                        showConfirmButton: false,
                        allowOutsideClick: false
                    });

                    $('#formProducts').submit();
                    //after Submit the ActionResult will return a NoCotentent result then we redirect to the Index page
                    var animation = lottie.loadAnimation({
                        container: $('#lottie-animation')[0],
                        renderer: 'svg',
                        loop: false,
                        autoplay: true,
                        path: '/js/animation/Animation - 1743129363779.json'
                    });

                    $('#progress-bar').animate({ width: '100%' }, 3000);

                    $(animation).on('complete', function () {
                        window.location.href = '/Products/Index';
                        $(window).on('load', function () {
                            $('#global-loader').hide(); // 
                        });
                    });
                }
            }
        }
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

$(document).on("change", "#ProductTypeID", function () {
    if ($('#ProductTypeID').val() == 2) {
        $('#variantselection').show();
        $('#ddVariantType').empty();
        $('#ddVariantType').append('<option value="0">Select</option>');
        $.ajax({
            type: 'GET',
            url: '/Products/GetVarianTypeBySubscriptionID',
            /* data: { id: id },*/
            dataType: 'json',
            success: function (response) {
                for (var x = 0; x < response.data.length; x++) {
                    $('#ddVariantType').append('<option value="' + response.data[x].id + '">' + response.data[x].description + '</option>');
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    else {
        $('#variantselection').hide();
        $('#ddVariantType').empty();
        $('#ddVariantType').append('<option value="0">Select</option>');
        $('#ddVariantType').val("0").change();
    }
});

$(document).on("change", "#ddVariantType", function () {
    $('#VariantTypeID').val($('#ddVariantType').val()).change()
    var variantTypeID = $('#ddVariantType').val()
    $('#ddVariantSpecific').empty();
    $('#ddVariantSpecific').append('<option value="0">Select</option>');
    if ($('#ddVariantType').val() > 0) {
        $('#variantspecific').show()
        $.ajax({
            type: 'GET',
            url: '/Products/GetSpecifiedVariantbyID',
            data: { variantTypeID: variantTypeID },
            dataType: 'json',
            success: function (response) {
                for (var x = 0; x < response.data.length; x++) {
                    $('#ddVariantSpecific').append('<option value="' + response.data[x].id + '">' + response.data[x].description + '</option>');
                }
                console.log(response.data)
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    else
    {
        $('#variantspecific').hide()
    }
});

$(document).on("change", "#ddVariantSpecific", function () {
    $('#VarianSpecifiedID').val($('#ddVariantSpecific').val()).change();
});

$(document).on("change", "#CategoryID", function () {
    var categoryId = $('#CategoryID').val()
    if (categoryId > 0)
    {
        $.ajax({
            type: 'GET',
            url: '/Products/GetProductSubCategoryByCategoryID',
            data: { categoryId: categoryId },
            dataType: 'json',
            success: function (response) {
                $('#ddSubCategory').empty();
                if (response.data.length == 0) {
                    $('#ddSubCategory').append('<option value="0">No Sub-Category Available</option>');
                }
                else {
                    $('#ddSubCategory').append('<option value="0">Select</option>');
                }

                for (var x = 0; x < response.data.length; x++) {
                    $('#ddSubCategory').append('<option value="' + response.data[x].id + '">' + response.data[x].name  + '</option>');
                }
               /* console.log(response.data)*/
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    else
    {
        $('#ddSubCategory').empty();
        $('#ddCategory').append('<option value="0">Select</option>');
        $('#CategoryID').val($('#ddSubCategory').val()).change();
    }
});

$(document).on("change", "#TaxID", function () {
    var taxtypeId = $('#TaxID').val()
    if (taxtypeId > 0) {
        $('#ddTaxAmount').empty();
        $.ajax({
            type: 'GET',
            url: '/Products/GetTaxByTaxTypeID',
            data: { taxtypeId: taxtypeId },
            dataType: 'json',
            success: function (response) {
                if (response.data.length == 0) {
                    $('#ddTaxAmount').append('<option value="0">No Sub-Category Available</option>');
                }
                else {
                    $('#ddTaxAmount').append('<option value="0">Select</option>');
                }

                for (var x = 0; x < response.data.length; x++) {
                    $('#ddTaxAmount').append('<option value="' + response.data[x].id + '">' + response.data[x].amount + '</option>');
                }
                $('#ddTaxAmount').val('0').change();

            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    else {
        $('#ddTaxAmount').empty();
        $('#ddTaxAmount').append('<option value="0">Select</option>');
        $('#TaxAmountID').val($('#ddTaxAmount').val()).change();
    }
});

$(document).on("change", "#ddSubCategory", function () {
    $('#SubCategoryID').val($('#ddSubCategory').val())
});

$(document).on("change", "#ddTaxAmount", function () {
    $('#TaxAmountID').val($('#ddTaxAmount').val())
});

$(document).on("click", ".imagePreview-close", function () {
    $('#imagePreview').attr('src', '#');
    $('#imagePreview').hide();
});

$(document).ready(function () {

    var categoryId = $('#CategoryID').val()
    var variantTypeID = $('#VariantTypeID').val()
    var productTypeId = $('#ProductTypeID').val()
    var taxtypeId = $('#TaxAmountID').val()

    if (taxtypeId != 0) {
        $('#ddTaxAmount').empty();
        $.ajax({
            type: 'GET',
            url: '/Products/GetTaxByTaxTypeID',
            data: { taxtypeId: taxtypeId },
            dataType: 'json',
            success: function (response) {
                if (response.data.length == 0) {
                    $('#ddTaxAmount').append('<option value="0">No Sub-Category Available</option>');
                }
                else {
                    $('#ddTaxAmount').append('<option value="0">Select</option>');
                }

                for (var x = 0; x < response.data.length; x++) {
                    $('#ddTaxAmount').append('<option value="' + response.data[x].id + '">' + response.data[x].amount + '</option>');
                }
                 
                $('#ddTaxAmount').val($('#TaxAmountID').val()).change
            },
            error: function (error) {
                console.log(error);
            }

        });
    }

    if (categoryId != 0)
    {
        $.ajax({
            type: 'GET',
            url: '/Products/GetProductSubCategoryByCategoryID',
            data: { categoryId: categoryId },
            dataType: 'json',
            success: function (response) {
                $('#ddSubCategory').empty();
                if (response.data.length == 0) {
                    $('#ddSubCategory').append('<option value="0">No Sub-Category Available</option>');
                }
                else {
                    $('#ddSubCategory').append('<option value="0">Select</option>');
                }

                for (var x = 0; x < response.data.length; x++) {
                    $('#ddSubCategory').append('<option value="' + response.data[x].id + '">' + response.data[x].name + '</option>');
                }
                /* console.log(response.data)*/

                $('#ddSubCategory').val($('#SubCategoryID').val()).change()
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    if (productTypeId == 2) {
        $('#variantselection').show();
        $.ajax({
            type: 'GET',
            url: '/Products/GetVarianTypeBySubscriptionID',
            /* data: { id: id },*/
            dataType: 'json',
            success: function (response) {
                $('#ddVariantType').empty();
                if (response.data.length == 0) {
                    $('#ddVariantType').append('<option value="0">No Sub-Category Available</option>');
                }
                else {
                    $('#ddVariantType').append('<option value="0">Select</option>');
                }

                for (var x = 0; x < response.data.length; x++) {
                    $('#ddVariantType').append('<option value="' + response.data[x].id + '">' + response.data[x].description + '</option>');
                }
                $('#ddVariantType').val($('#VariantTypeID').val()).change()
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    if (variantTypeID != 0)
    {
        $('#variantspecific').show()
        $.ajax({
            type: 'GET',
            url: '/Products/GetSpecifiedVariantbyID',
            data: { variantTypeID: variantTypeID },
            dataType: 'json',
            success: function (response) {
                $('#ddVariantSpecific').empty();
                if (response.data.length == 0) {
                    $('#ddVariantSpecific').append('<option value="0">No Sub-Category Available</option>');
                }
                else {
                    $('#ddVariantSpecific').append('<option value="0">Select</option>');
                }
                for (var x = 0; x < response.data.length; x++) {
                    $('#ddVariantSpecific').append('<option value="' + response.data[x].id + '">' + response.data[x].description + '</option>');
                }
                $('#ddVariantSpecific').val($('#VarianSpecifiedID').val()).change()
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
});