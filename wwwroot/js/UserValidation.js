$(document).ready(function () {
    $('#btn').click(function (event) {
        event.preventDefault(); 
        if (validateForm()) {
            AddUserData();
        }
    });

   function validateForm() {
        var isValid = true;
        
        $('input,textarea').css('border-color', '');
        $("#msgspan, #emailmsg, #nummsg, #addmsg, #passmsg, #msgcpass").text("");
        $('input,textarea').each(function () {
            if ($(this).val().trim() === '') {
                $(this).css('border-color', 'red');
                //$("#msgspan, #emailmsg, #nummsg, #addmsg, #passmsg, #msgcpass").text("please fill this field").css("color","red")
                isValid = false;
            }
        });

        // Validate name format
        var name = $('#name').val();
        var nameRegex = /^[A-Za-z\s]+$/;
        if (!nameRegex.test(name)) {
            $('#name').css('border-color', 'red');
            isValid = false;
        }

        // Validate email format
        var email = $('#emailid').val();
        var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(email)) {
            $('#emailid').css('border-color', 'red');
            isValid = false;
        }

        // Validate phone number length
        var phone = $('#phone').val();
        if (phone.length < 10) {
            $('#phone').css('border-color', 'red');
            isValid = false;
        }

        // Generate and set random password if empty
        var password = $('#password').val();
        if (password.trim() === '') {
            password = generate
            
            RandomPassword(12);
            $('#password').val(password);
            $('#cpasword').val(password);
        }

        // Check if passwords match
        var cpassword = $('#cpasword').val();
        if (password !== cpassword) {
            $('#cpasword').css('border-color', 'red');
            isValid = false;
        }

        return isValid;
    }

    function AddUserData() {
        var formData = new FormData();
        formData.append('Name', $('#name').val());
        formData.append('Email', $('#emailid').val());
        formData.append('Phone', $('#phone').val());
        formData.append('Address', $('#address').val());
        formData.append('Password', $('#password').val());
        formData.append('Cpassword', $('#cpasword').val());
        formData.append('IsActive', $('#isactive').is(':checked'));

        $.ajax({
            url: '/user/addUser',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                try {
                    var statusCode = res.status;
                    var message = res.message;
                    if (statusCode == 1) {
                        Swal.fire({
                            icon: "success",
                            title: message,
                            text: 'Thank you',
                            timer: 5000,
                            showConfirmButton: true,
                            showCloseButton: true,
                            showCancelButton: true,
                            timerProgressBar: true
                        });

                        // Reset form fields and border colors
                        $('input, textarea').val('');
                        $('input, textarea').css('border-color', '');
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: message,
                            text: 'Sorry',
                            timer: 5000,
                            showConfirmButton: true,
                            showCloseButton: true,
                            showCancelButton: true,
                            timerProgressBar: true
                        });
                    }
                } catch (error) {
                    console.log(error);
                }
            },
            error: function (error) {
                alert('An error occurred. Please try again.');
                console.log("Error: ", error);
            }
        });
    }
});
function alphabetOnly() {
    try {
        var digitRegex = /[^a-zA-Z\s]+/g;
        var x1 = document.getElementById("name");
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
function email() {
    try {
        var digitRegex =/[^a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2}$/g

;
        var x1 = document.getElementById("emailid");
        var x = x1.value;

        x1.value = x.replace(digitRegex, '');

        if (x.trim() === "") {
            return false;
        } else {
            var a = /[^a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2}$/g
;
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
document.getElementById("emailid").addEventListener("input", email);
