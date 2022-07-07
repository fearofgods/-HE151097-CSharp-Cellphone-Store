$(document).ready(function () {
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
});

