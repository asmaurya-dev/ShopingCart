
//Ajax

function GetCategoryListForDropdown()
{
    $.ajax({
        url: '/Admin/CategoryListForDropdown',
        type: 'GET',
        success: function (res) {
            for (var x of res) {
                $('#cat_Id').append(`<option value="${x.categoryId}"> ${x.categoryName} </option>`)
            }
        }
    })
}

function DeleteProduct(ProductId) {
    try {
        $.ajax({
            url: '/Admin/DeleteProduct',
            type: 'GET',
            data: { ProductId: ProductId },
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

                SelectProduct();
            }
        });
    } catch (error) {
        console.log(error);
    }
}

function GetListById(productId) {
    $.ajax({
        url: '/Admin/GetProductListById',
        type: 'GET',
        data: { ProductId: productId },
        success: function (response) {
            debugger;
            var x = response[0];
            $("#hdnprodductid").val(x.id);
            $("#cat_Id").val(x.categoryId);
            $("#product_name").val(x.productName);
            $("#product_price").val(x.productPrice);
            CKEDITOR.instances['product_description'].setData(x.productDescription);
            $("#isactive").prop("checked", x.isActive);
            $("#blah").prop("src", "/Images/" + x.productImage).show();
           
            $("#btn").text("Update Now");
            $("#ModalLabel").text("Edit Category");


        }
    });
}

function AddOrUpdateProduct() {
    debugger;
    var activevalue = $("#isactive").is(":checked") ? "true" : "false";
    var formData = new FormData();
    formData.append('Id', $("#hdnprodductid").val());
    formData.append('CategoryId', $("#cat_Id").val());
    formData.append('ProductName', $('#product_name').val().trim());
    formData.append('ProductPrice', $("#product_price").val());
    formData.append('productDescription', CKEDITOR.instances['product_description'].getData());
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
            debugger;
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
                $("#Modal").trigger('click');
                $('#Modal').modal('hide');
                SelectProduct()
                resetFormFields()
            }
            if (statusCode == -1) {
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

        }


    });
}
//Validation

$(document).ready(function () {
  
    SelectProduct();
    GetCategoryListForDropdown()

    $('#btn').click(function () {
        debugger;
        try {
            var formIsValid = true;
            
            var categoryName = $('#product_name').val();
            var isProductNameValid = alphabetOnly();
            if (!categoryName || !isProductNameValid) {
                formIsValid = false;
                $('#product_name').css('border', '1px solid red');
            } else {
                $('#product_name').css('border', ''); 
            }
            var isSelectValid = validateSelectInput();
            if (!isSelectValid) {
                formIsValid = false;
                $('#cat_Id').css('border', '1px solid red');
            } else {
                $('#cat_Id').css('border', '');
            }
            var productPrice = $('#product_price').val();
            if (!productPrice) {
                formIsValid = false;
                $('#product_price').css('border', '1px solid red');
            } else {
                $('#product_price').css('border', '');
            }

            if (formIsValid) {
                AddOrUpdateProduct();
                //setTimeout(function () {
                //   $('#hdnprodductid, #cat_Id, #product_name, #product_Image, #product_price').val('');
                //    CKEDITOR.instances['product_description'].getData();
                //    $("#isactive").prop("checked", true);
                //    $('.ModalLabel').text('Add product');
                //    $("#btn").text("Submit");
                //}, 500); 
            }
        } catch (error) {
            console.log(error);
        }
    });
});

$('.modal .close, .modal-footer .btn-danger').click(function () {
    try {
        setTimeout(function () {
            $('#Modal').modal('hide').on('hidden.bs.modal', function ()
            {
                resetFormFields();
                afterModalHidden();
            });
            resetFormFields();
        }, 500); // 500ms delay before executing the changes
    } catch (error) {
        console.log(error);
    }
});
function resetFormFields() {
    $('#hdnprodductid, #cat_Id, #product_name, #product_price').val('');
    $('#hdnprodductid, #cat_Id, #product_name, #product_price').css('border',"");
    CKEDITOR.instances['product_description'].setData('');
    $("#isactive").prop("checked", true);
    $('#product_Image').val('');
    $("#blah").prop("src", "#").hide();
    $('.ModalLabel').text('Add product');
    $("#btn").text("Submit");
    $('#Category').css('border', '');
}


function alphabetOnly() {
    try {
        var digitRegex = /[^a-zA-Z\s]+/g;
        var x1 = document.getElementById("product_name");
        var x = x1.value;

        x1.value = x.replace(digitRegex, '');

        if (x.trim() === "") {
            return false;
        } else {
            var a = /^[A-Za-z\s]+$/;
            if (a.test(x)) {
                return true;
            } else {
                return false;
            }
        }
    } catch (error) {
        console.log(error);
    }
}

function validateSelectInput() {
    var selectedOption = document.getElementById("cat_Id").value;
    if (selectedOption === "") {
       
        
        return false; 
    }
    return true; 
}



