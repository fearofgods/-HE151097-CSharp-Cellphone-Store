$(document).ready(function () {
    function showAlert(content) {
        $('#myAlert').css('display', 'block');
        $('#myAlert').addClass('.alert-warning');
        $('#myAlert').html(content);
        setTimeout(function () {
            $('#myAlert').css('display', 'none');
            location.reload();

        }, 3000)
    }

    $('.update-btn').click(function () {
        let target = $(this).parent().parent();
        let uid = target.find('td input[name=username]').val();
        let role = target.find('td select[name=role]').val();
        $('#checkModal').modal('show');
        $('#checkModalLabel').html(`Cập nhật!`);
        $('#bodyContent').html(`Bạn có muốn xóa cập nhật thay đổi!`);
        $('#confirmAction').click(function () {
            $.ajax({
                type: "post",
                url: "/Admin/UpdateUser",
                data: {
                    Username: uid,
                    Role: role
                },
                success: function (response) {
                    $('#checkModal').modal('hide');
                    if (response.status === "Success") {
                        showAlert(response.content);
                    } else {
                        showAlert(response.content);
                    };
                }
            });
        });
    });
})