// Function to fetch category list for dropdown
function GetCategoryListForDropdown() {
    $.ajax({
        url: '/Admin/CategoryListForDropdown',
        type: 'GET',
        success: function (res) {
            for (var x of res) {
                $('#cat_Id').append(`<option value="${x.categoryId}">${x.categoryName}</option>`);
            }
        },
        error: function (error) {
            console.log('Error fetching category list:', error);
        }
    });
}

// Function to delete a product
function deleteProductById(productId) {
    $.ajax({
        url: '/Admin/DeleteProduct',
        type: 'GET',
        data: { ProductId: productId },
        success: function (res) {
            Swal.fire({
                icon: "success",
                title: 'Data Deleted Successfully',
                text: 'Thank you',
                timer: 5000,
                showConfirmButton: true,
                showCloseButton: true,
                showCancelButton: true,
                timerProgressBar: true
            });
            $('#tbl tbody').empty();
            selectProduct();
        },
        error: function (error) {
            console.log('Error deleting product:', error);
        }
    });
}

// Function to fetch product details by ID
function getListById(productId) {
    $.ajax({
        url: '/Admin/GetProductListById',
        type: 'GET',
        data: { ProductId: productId },
        success: function (response) {
            var x = response[0];
            $("#hdnprodductid").val(x.id);
            $("#cat_Id").val(x.categoryId);
            $("#product_name").val(x.productName);
            $("#product_price").val(x.productPrice);
            CKEDITOR.instances['product_description'].setData(x.productDescription);
            $("#isactive").prop("checked", x.isActive);
            $("#blah").prop("src", "/Images/" + x.productImage).show();
            $("#btn").text("Update Now");
            $("#ModalLabel").text("Edit Product");
        },
        error: function (error) {
            console.log('Error fetching product details:', error);
        }
    });
}

// Function to add or update product
function AddOrUpdateProduct() {
    var activevalue = $("#isactive").is(":checked");
    var formData = new FormData();
    formData.append('Id', $("#hdnprodductid").val());
    formData.append('CategoryId', $("#cat_Id").val());
    formData.append('ProductName', $('#product_name').val().trim());
    formData.append('ProductPrice', $("#product_price").val());
    formData.append('ProductDescription', CKEDITOR.instances['product_description'].getData());
    formData.append('IsActive', activevalue);

    if ($("#product_Image").get(0).files.length > 0) {
        var pic = $("#product_Image").get(0).files[0];
        formData.append('file', pic);
    }

    $.ajax({
        url: '/Admin/AddOrUpdateProduct',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (res) {
            var statusCode = res.status;
            var message = res.message;

            if (statusCode == 1) {
                Swal.fire({
                    icon: "success",
                    title: message,
                    text: 'Thank you',
                    confirmButtonText: 'OK',
                    timer: 5000,
                    showConfirmButton: true,
                    showCloseButton: true,
                    showCancelButton: true,
                    timerProgressBar: true
                });
                $('#Modal').modal('hide');
                selectProduct();
                resetFormFields();
                $("#Modal .close").click();
            } else {
                Swal.fire({
                    icon: "error",
                    title: message,
                    text: 'Thank you',
                    confirmButtonText: 'OK',
                    timer: 5000,
                    showConfirmButton: true,
                    showCloseButton: true,
                    showCancelButton: true,
                    timerProgressBar: true
                });
            }
        },
        error: function (error) {
            console.log('Error adding/updating product:', error);
        }
    });
}

// Function to validate select input
function validateSelectInput() {
    var selectedOption = $("#cat_Id").val();
    return selectedOption !== "";
}

$(document).ready(function () {
    selectProduct();
    GetCategoryListForDropdown();

    $('#btn').click(function () {
        var formIsValid = true;

        // Validation for product name
        var productName = $('#product_name').val().trim();
        if (!productName.match(/^[a-zA-Z\s]*$/) || productName === "") {
            formIsValid = false;
            $('#product_name').css('border', '1px solid red');
        } else {
            $('#product_name').css('border', '');
        }

        // Validation for category selection
        var isSelectValid = validateSelectInput();
        if (!isSelectValid) {
            formIsValid = false;
            $('#cat_Id').css('border', '1px solid red');
        } else {
            $('#cat_Id').css('border', '');
        }

        // Validation for product price
        var productPrice = $('#product_price').val();
        if (!productPrice || isNaN(productPrice)) {
            formIsValid = false;
            $('#product_price').css('border', '1px solid red');
        } else {
            $('#product_price').css('border', '');
        }

        // If form is valid, proceed to add/update product
        if (formIsValid) {
            AddOrUpdateProduct();
        }
    });

    // Modal close event handling
    $('.modal .close, .modal-footer .btn-danger').click(function () {
        $('#Modal').modal('hide').on('hidden.bs.modal', function () {
            resetFormFields();
        });
    });
});

// Function to reset form fields
function resetFormFields() {
    $('#hdnprodductid, #cat_Id, #product_name, #product_price').val('');
    $('#product_name, #product_price').css('border', '');
    CKEDITOR.instances['product_description'].setData('');
    $("#isactive").prop("checked", true);
    $('#product_Image').val('');
    $("#blah").prop("src", "#").hide();
    $('.ModalLabel').text('Add Product');
    $("#btn").text("Submit");
    $('#cat_Id').css('border', '');
}
