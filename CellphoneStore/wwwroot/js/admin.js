$(document).ready(function () {
    $('#menu').click(function () {
        let width = document.getElementById('sidenav').clientWidth;
        if (width === 0) {
            $('#sidenav').css("width", "250px");
            $('#main').css("margin-left", "250px");
        } else {
            $('#sidenav').css("width", "0px");
            $('#main').css("margin-left", "0px");
        }
    });

    
})