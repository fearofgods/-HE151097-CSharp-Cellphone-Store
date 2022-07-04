
$(document).ready(function () {
    //Load category
    $.ajax({
        url: "/Header/LoadCategory",
        success: function (receive) {
            let categories = JSON.parse(receive);
            for (let i in categories) {
                $('#catemenu').append(`<a class="dropdown-item" role="presentation" href="#">${categories[i].Cname}</a>`);
            }
        }
    });
})