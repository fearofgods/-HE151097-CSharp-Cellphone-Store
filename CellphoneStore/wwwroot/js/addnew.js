$(document).ready(function () {
    let storageCount = $('.storage-items') == undefined ? 0 : $('.storage-items').length;

    function closeModal() {
        setTimeout(function () {
            $('#checkModal').modal('hide');
            location.reload();
        }, 3000)
    }

    function showAlert(content) {
        $('#myAlert').css('display', 'block');
        $('#myAlert').addClass('.alert-warning');
        $('#myAlert').html(content);
        setTimeout(function () {
            $('#myAlert').css('display', 'none');
        }, 3000)
    }

    $('#color-add').click(function () {
        let colorCount = $('.color-items') == undefined ? 0 : $('.color-items').length;

        if (colorCount < 10) {
            $('#color-content').append(`<div class="color-items" id="color-${colorCount}">
                            <input class="color-input color-new" type="text" placeholder="Thêm màu"/>
                            <input class="delete-color" type="button" value="Xóa"/>
                        </div>`);
        } else {
            showAlert('Đã đạt giới hạn của lựa chọn màu sắc!');
        }
    });

    $('#storage-add').click(function () {
        let storageCount = $('.storage-items') == undefined ? 0 : $('.storage-items').length;

        if (storageCount < 10) {
            $('#storage-content').append(`<div class="storage-items" id="storage-${storageCount}">
                            <input class="storage-input storage-new" type="text" placeholder="Thêm bộ nhớ"/>
                            <input class="delete-storage" type="button" value="Xóa"/>
                        </div>`);
        } else {
            showAlert('Đã đạt giới hạn của lựa chọn bộ nhớ!');
        }
    });

    $('#description').summernote({
        placeholder: 'Mô tả sản phẩm',
        tabsize: 2,
        height: 1000,
        width: 955
    });

    $('#save').click(function () {
        let color = $('.color-new');
        let storage = $('.storage-new');

        let colorData = Array.prototype.slice.call(color).map(function (e) {
            return e.value;
        });

        let storageData = Array.prototype.slice.call(storage).map(function (e) {
            return e.value;
        });
        //Thông tin cơ bản
        let cid = $('select[name=cid]').val().trim();
        let pid = $('input[name=pid]').val().trim();
        let name = $('input[name=pname]').val().trim();
        let image = $('input[name=image]').val().trim();
        let price = $('input[name=price]').val().trim();
        let amount = $('input[name=amount]').val().trim();
        let description = $('#description').summernote('code');
        //Thông tin cấu hình
        let screen = $('input[name=screen]').val().trim();
        let os = $('input[name=os]').val().trim();
        let rearcam = $('input[name=rearcam]').val().trim();
        let frontcam = $('input[name=frontcam]').val().trim();
        let soc = $('input[name=soc]').val().trim();
        let ram = $('input[name=ram]').val().trim();
        let sim = $('input[name=sim]').val().trim();
        let battery = $('input[name=battery]').val().trim();


        $('#checkModal').modal('show');
        $('#checkModalLabel').html(`Thêm mới!`);
        $('#bodyContent').html(`Bạn có muốn muốn tạo?`);
        $('#confirmAction').click(function () {
            $.ajax({
                type: "post",
                url: "/Admin/AddNewProduct",
                data: {
                    Pid: pid,
                    Cid: cid,
                    Name: name,
                    Image: image,
                    Price: price,
                    Description: description,
                    Amount: amount,
                    Screen: screen,
                    Os: os,
                    Rearcam: rearcam,
                    Frontcam: frontcam,
                    Soc: soc,
                    Ram: ram,
                    Sim: sim,
                    Battery: battery,
                    colors: JSON.stringify(colorData),
                    storages: JSON.stringify(storageData)
                },
                success: function (response) {
                    $('#checkModal').modal('hide');
                    if (response.status === "Success") {
                        showAlert(response.content);
                        parent.remove();
                    } else {
                        showAlert(response.content);
                    };
                }
            });
        });


    });

    $('#save-changes').click(function () {

        //Thông tin cơ bản
        let cid = $('select[name=cid]').val().trim();
        let pid = $('input[name=pid]').val().trim();
        let name = $('input[name=pname]').val().trim();
        let image = $('input[name=image]').val().trim();
        let price = $('input[name=price]').val().trim();
        let amount = $('input[name=amount]').val().trim();
        let description = $('#description').summernote('code');
        //Thông tin cấu hình
        let screen = $('input[name=screen]').val().trim();
        let os = $('input[name=os]').val().trim();
        let rearcam = $('input[name=rearcam]').val().trim();
        let frontcam = $('input[name=frontcam]').val().trim();
        let soc = $('input[name=soc]').val().trim();
        let ram = $('input[name=ram]').val().trim();
        let sim = $('input[name=sim]').val().trim();
        let battery = $('input[name=battery]').val().trim();

        //Color
        let color = $('.color-old');
        let colorId = $('.color-id');

        let colorData = Array.prototype.slice.call(color).map(function (e) {
            return e.value;
        });

        let colorIds = Array.prototype.slice.call(colorId).map(function (e) {
            return e.value;
        });
        //Color Array
        let colorsArray = [];
        for (let i = 0; i < colorData.length; i++) {
            colorsArray.push({ Pid: pid, Id: colorIds[i], Color: colorData[i] });
        }

        //Storage
        let storage = $('.storage-old');
        let storageId = $('.storage-id');
        //Convert class to array
        let storageData = Array.prototype.slice.call(storage).map(function (e) {
            return e.value;
        });

        let storageIds = Array.prototype.slice.call(storageId).map(function (e) {
            return e.value;
        });
        //Storage array
        let storageArray = [];
        for (let i = 0; i < storageData.length; i++) {
            storageArray.push({ Pid: pid, Id: storageIds[i], Storage: storageData[i] });
        }

        //New Color, new storage
        let colorNew = $('.color-new');
        let storageNew = $('.storage-new');

        let newColors = Array.prototype.slice.call(colorNew).map(function (e) {
            return e.value;
        });

        let newStorages = Array.prototype.slice.call(storageNew).map(function (e) {
            return e.value;
        });

        $('#checkModal').modal('show');
        $('#checkModalLabel').html(`Cập nhật!`);
        $('#bodyContent').html(`Bạn có chắc chắn lưu các thay đổi?`);
        $('#confirmAction').click(function () {
            $.ajax({
                type: "post",
                url: "/Admin/UpdateProduct",
                data: {
                    Pid: pid,
                    Cid: cid,
                    Name: name,
                    Image: image,
                    Price: price,
                    Description: description,
                    Amount: amount,
                    Screen: screen,
                    Os: os,
                    Rearcam: rearcam,
                    Frontcam: frontcam,
                    Soc: soc,
                    Ram: ram,
                    Sim: sim,
                    Battery: battery,
                    oldColors: JSON.stringify(colorsArray),
                    oldStorages: JSON.stringify(storageArray),
                    newColors: JSON.stringify(newColors),
                    newStorages: JSON.stringify(newStorages)
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

    $("body").on("click", ".delete-color", function () {
        let parent = $(this).parent();
        if (!parent.hasClass('color-current')) {
            //console.log(parent);
            parent.remove();
        } else {
            let storageid = $(this).closest('.color-current').find('.color-id').val().trim();
            let storagename = $(this).closest('.color-current').find('.color-input').val().trim();

            $('#checkModal').modal('show');
            $('#checkModalLabel').html(`Xóa!`);
            $('#bodyContent').html(`Bạn có muốn xóa ${storagename}?`);
            $('#confirmAction').click(function () {
                $.ajax({
                    type: "post",
                    url: "/Admin/RemoveColor",
                    data: {
                        id: storageid
                    },
                    success: function (response) {
                        $('#checkModal').modal('hide');
                        if (response.status === "Success") {
                            showAlert(response.content);
                            parent.remove();
                        } else {
                            showAlert(response.content);
                        };
                    }
                });
            });
        };
    });

    $("body").on("click", ".delete-storage", function () {
        let parent = $(this).parent();
        if (!parent.hasClass('storage-current')) {
            //console.log(parent);
            parent.remove();
        } else {

            let storageid = $(this).closest('.storage-current').find('.storage-id').val().trim();
            let storagename = $(this).closest('.storage-current').find('.storage-input').val().trim();

            $('#checkModal').modal('show');
            $('#checkModalLabel').html(`Xóa!`);
            $('#bodyContent').html(`Bạn có muốn xóa ${storagename}?`);
            $('#confirmAction').click(function () {
                $.ajax({
                    type: "post",
                    url: "/Admin/RemoveStorage",
                    data: {
                        id: storageid
                    },
                    success: function (response) {
                        $('#checkModal').modal('hide');
                        if (response.status === "Success") {
                            showAlert(response.content);
                            parent.remove();
                        } else {
                            showAlert(response.content);
                        };
                    }
                });
            });
        };
    });
})

//const btn = document.querySelector('.delete-color');
//btn.onclick = function () {
//    console.log('abc');
//}
