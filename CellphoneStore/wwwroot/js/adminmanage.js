$(document).ready(function () {

});

function closeModal() {
    setTimeout(function () {
        $('#checkModal').modal('hide');
        location.reload();
    }, 3000)
}
function openModal(pid) {
    $('#checkModal').modal('show');
    $('#checkModalLabel').html(`Xóa!`);
    $('#bodyContent').html(`Bạn có muốn xóa ${pid}?`);
    $('#confirmAction').click(function () {
        $.ajax({
            type: "post",
            url: "/Admin/RemoveProduct",
            data: {
                id: pid
            },
            success: function (response) {
                if (response.status === 'Success') {
                    $('.modal-footer').css("display", "none");
                    $('#checkModal').modal('show');
                    $('#checkModalLabel').html(`Thành công`);
                    $('#bodyContent').html(`Bạn đã xóa thành công ${pid}`);
                    closeModal();
                } else {
                    $('.modal-footer').css("display", "none");
                    $('#checkModal').modal('show');
                    $('#checkModalLabel').html(`Thất bại`);
                    $('#bodyContent').html(`Bạn đã xóa thất bại ${pid}`);
                    closeModal();
                }
            }
        });
    })
}