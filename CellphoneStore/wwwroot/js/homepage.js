$(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/Home/LoadTop4Products",
        success: function (response) {
            let object = JSON.parse(response);
            for (let item in object) {
                $('#newlist').append(`<div class="col-lg-3 col-md-4 col-sm-6 product">
                                            <div class="item-wrapper">
                                                <div class="item-img">
                                                    <a href="#"><img src="${object[item].Image}" class="img-responsive img-i" alt="${object[item].Name}"></a>
                                                </div>
                                                <div class="item-price">
                                                    <h3>${object[item].Name}</h3>
                                                    <p>${object[item].Price}&nbsp;₫</p>
                                                </div>
                                            </div>
                                        </div>`);
            }
            //console.log(object)
        }
    });
});
