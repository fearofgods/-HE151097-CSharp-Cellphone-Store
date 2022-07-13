$(document).ready(function () {
    let colorCount = 0;
    let storageCount = 0;

    function showAlert(content) {
        $('#myAlert').css('display', 'block');
        $('#myAlert').addClass('.alert-warning');
        $('#myAlert').html(content);
        setTimeout(function () {
            $('#myAlert').css('display', 'none');
        }, 3000)
    }

    $('#color-add').click(function () {
        if (colorCount < 10) {
            $('#color-content').append(`<div class="color-items" id="color-${colorCount}">
                            <input class="color-input" type="text" placeholder="Thêm màu"/>
                            <input class="delete-color" type="button" value="Xóa"/>
                        </div>`);
            colorCount += 1;
        } else {
            showAlert('Đã đạt giới hạn của lựa chọn màu sắc!');
        }
    });

    $('#storage-add').click(function () {
        if (storageCount < 10) {
            $('#storage-content').append(`<div class="storage-items" id="storage-${storageCount}">
                            <input class="storage-input" type="text" placeholder="Thêm bộ nhớ"/>
                            <input class="delete-storage" type="button" value="Xóa"/>
                        </div>`);
            storageCount += 1;
        } else {
            showAlert('Đã đạt giới hạn của lựa chọn bộ nhớ!');
        }
    });

    $('#description').summernote({
        placeholder: 'Mô tả sản phẩm',
        tabsize: 2,
        height: 100,
        width: 955
    });

    $('#save').click(function () {
        let color = $('.color-input');
        let storage = $('.storage-input');

        let colorData = Array.prototype.slice.call(color).map(function (e) {
            return e.value;
        });

        let storageData = Array.prototype.slice.call(storage).map(function (e) {
            return e.value;
        });
        //Thông tin cơ bản
        let cid = $('select[name=cid]').val();
        let pid = $('input[name=pid]').val();
        let name = $('input[name=pname]').val();
        let image = $('input[name=image]').val();
        let price = $('input[name=price]').val();
        let amount = $('input[name=amount]').val();
        let description = $('textarea[name=des]').val();
        //Thông tin cấu hình
        let screen = $('input[name=screen]').val();
        let os = $('input[name=os]').val();
        let rearcam = $('input[name=rearcam]').val();
        let frontcam = $('input[name=frontcam]').val();
        let soc = $('input[name=soc]').val();
        let ram = $('input[name=ram]').val();
        let sim = $('input[name=sim]').val();
        let battery = $('input[name=battery]').val();


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
                console.log(response);
            }
        });
    })

    $("body").on("click", ".delete-color", function () {
        $(this).parent().remove();
        colorCount -= 1;
    });

    $("body").on("click", ".delete-storage", function () {
        $(this).parent().remove();
        storageCount -= 1;
    });
})

//const btn = document.querySelector('.delete-color');
//btn.onclick = function () {
//    console.log('abc');
//}
