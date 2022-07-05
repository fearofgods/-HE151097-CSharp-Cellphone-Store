$(document).ready(function () {
    function convertCurrency(price) {
        newPrice = parseInt(price, 10);
        newPrice = newPrice.toLocaleString('vn-VN');
        newPrice = newPrice.replaceAll(',', '.').concat('đ');
        return newPrice;

    }

    $.ajax({
        type: "post",
        url: "/Home/TopSell",
        success: function (response) {
            let object = JSON.parse(response);

            let content = "";

            for (let item in object) {
                content += `<div class="col-lg-3 col-md-4 col-sm-6">
                                        <div class="item-top">
                                            <div class="item-top-img">
                                                <a href="#"><img src="${object[item].Image}" alt="${object[item].Name}"></a>
                                            </div>
                                            <div class="item-top-title">
                                                <h2>${object[item].Name}</h2>
                                                <p>${convertCurrency(object[item].Price)}</p>
                                            </div>
                                        </div>
                                    </div>`
                
            }

            $('#best').append(`<div class="owl-carousel col-md-12 col-sm-12">
                                    ${content}
                                </div>`)

            $(document).ready(function () {
                $(".owl-carousel").owlCarousel();
            });

            $(".owl-carousel").owlCarousel({
                autoplay: true,
                autoplayhoverpause: true,
                autoplaytimeout: 100,
                nav: true,
                loop: true,
                margin: 0,
                responsive: {
                    0: {
                        items: 1,
                        dots: false
                    },
                    600: {
                        items: 2,
                        dots: false
                    },
                    1000: {
                        items: 4,
                        dots: false
                    }
                }
            });
        }
    });

    $.ajax({
        type: "post",
        url: "/Home/LoadTop4Products",
        success: function (response) {
            let object = JSON.parse(response);
            for (let item in object) {
                $('#newlist').append(
                    `<div class="col-lg-3 col-md-4 col-sm-6 product">
                         <div class="item-wrapper">
                             <div class="item-img">
                                 <a href="#"><img src="${object[item].Image}" class="img-responsive img-i" alt="${object[item].Name}"></a>
                             </div>
                             <div class="item-price">
                                 <h3>${object[item].Name}</h3>
                                 <p>${convertCurrency(object[item].Price)}</p>
                             </div>
                         </div>
                    </div>`
                );
            }
            //console.log(object)
        }
    });

    $('#home-loadmore').click(function () {
        let amount = document.getElementsByClassName("product").length;
        //console.log(amount);
        if (amount < 8) {
            $.ajax({
                type: "post",
                url: "/Home/LoadNextTop4Products",
                data: {
                    id: amount,
                },
                success: function (response) {
                    //console.log(response);
                    let object = JSON.parse(response);
                    for (let item in object) {
                        $('#newlist').append(
                            `<div class="col-lg-3 col-md-4 col-sm-6 product">
                            <div class="item-wrapper">
                                 <div class="item-img">
                                     <a href="#"><img src="${object[item].Image}" class="img-responsive img-i" alt="${object[item].Name}"></a>
                                 </div>
                                 <div class="item-price">
                                     <h3>${object[item].Name}</h3>
                                     <p>${convertCurrency(object[item].Price)}</p>
                                 </div>
                             </div>
                        </div>`
                        );
                    }
                }
            });
        }
    });



});


