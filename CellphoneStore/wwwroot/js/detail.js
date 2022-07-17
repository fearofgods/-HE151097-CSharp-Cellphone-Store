$(document).ready(function () {

    function showAlert(content) {
        $('#message').css('display', 'block');
        $('#message').html(content);
        setTimeout(function () {
            $('#message').css('display', 'none');
        }, 3000)
    }

    $('.color-option').each(function (e) {
        $('input[type=radio]', this).get(0).checked = true;
    });

    $('.storage-option').each(function (e) {
        $('input[type=radio]', this).get(0).checked = true;
    });

    $(".owl-carousel").owlCarousel();

    $(".owl-carousel").owlCarousel({
        autoplay: true,
        autoplayhoverpause: true,
        autoplaytimeout: 100,
        singleItem: true,
        items: 5,
        nav: true,
        loop: true,
        margin: 5,
        responsive: {
            0: {
                items: 1,
                dots: false
            },
            600: {
                items: 1,
                dots: false
            },
            1000: {
                items: 1,
                dots: false
            }
        }
    });

    $(".owl-carousel").each(function (index, el) {
        var containerHeight = $(el).height();
        $(el).find("img").each(function (index, img) {
            var w = $(img).prop('naturalWidth');
            var h = $(img).prop('naturalHeight');
            $(img).css({
                'width': Math.round(containerHeight * w / h) + 'px',
                'height': containerHeight + 'px'
            });
        }),
            $(el).owlCarousel({
                autoWidth: true
            });
    });

    $('#btn-1').click(function () {
        let id = $('#productId').val();

        let color_col = document.getElementsByName('color-choice');
        for (let i = 0; i < color_col.length; i++) {
            if (color_col[i].type === "radio" && color_col[i].checked === true) {
                var color = color_col[i].value;
            }
        }
        let storage_col = document.getElementsByName('storage-choice');
        for (let i = 0; i < storage_col.length; i++) {
            if (storage_col[i].type === "radio" && storage_col[i].checked === true) {
                var storage = storage_col[i].value;
            }
        }

        $.ajax({
            type: "post",
            url: "/Cart/AddToCart",
            data: {
                id: id,
                num: 1,
                cid: color,
                sid: storage
            },
            success: function (response) {
                if (response.status === 'Success') {
                    showAlert('Đã thêm sản phẩm vào giỏ hàng!');
                }
            }
        });
    });
});
