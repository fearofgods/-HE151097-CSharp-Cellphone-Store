$(document).ready(function () {
    let message = localStorage.getItem('remember');
    if (message == 'true') {
        let username = localStorage.getItem('username');
        let password = localStorage.getItem('password');

        $('input[name=remember]').prop('checked', true);
        $('input[name=login-username]').val(username);
        $('input[name=login-password]').val(password);
    }

    $('input[name=password], input[name=repassword]').on('keyup', function () {
        let password = $('input[name=password]').val();
        let repassword = $('input[name=repassword]').val();

        let strongPassword = new RegExp('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})');
        const check = strongPassword.test(password);

        if (check == true) {
            $('#register-message').html('Mật khẩu hợp lệ').css('color', 'green');
            if (password === repassword) {
                $('#register-message').html('Mật khẩu hợp lệ').css('color', 'green');
            } else {
                $('#register-message').html('Mật khẩu không khớp').css('color', 'red');
            }
        } else {
            $('#register-message').html('Mật khẩu có ít nhất 8 kí tự, có chữ viết hoa thường, chứa kí tự đặc biệt và số!').css('color', 'red');
        }
    });

    $('input[name=login-btn]').click(function () {
        let check = $('input[name=remember]:checked').val();
        console.log(check)
        if (check === 'true') {
            let username = $('input[name=login-username]').val();
            let password = $('input[name=login-password]').val();
            localStorage.setItem('username', username);
            localStorage.setItem('password', password);
            localStorage.setItem('remember', 'true');
        }

        $.ajax({
            type: "post",
            url: "/Authentication/Login",
            data: {
                Username: $('input[name=login-username]').val(),
                Password: $('input[name=login-password]').val()
            },
            success: function (response) {
                //console.log(response.content);
                if (response != null && response != undefined) {
                    //let result = JSON.parse(response);
                    if (response.status === 'Success') {
                        window.location = '/Home/Index';
                    } else if (response.status === 'Fail') {
                        $('#login-message').html(response.content).css('color', 'red');
                    }
                }
            }
        });
    });

});


