$(document).ready(function () {

    $('.remove-qty').click(function () {
        let parent = $(this).parent().parent();
        let pid = parent.find('input[name=pid]').val();
        let cid = parent.find('input[name=cid]').val();
        let sid = parent.find('input[name=sid]').val();

        let qty = parent.find('input[name=qty]');

        $.ajax({
            type: "post",
            url: "/Cart/QuantityProcess",
            data: {
                id: pid,
                num: -1,
                cid: cid,
                sid: sid
            },
            success: function (response) {
                if (response.status === 'Success') {
                    qty.val(qty.val() - 1);
                    location.reload();
                }
            }
        });
    });

    $('.add-qty').click(function () {
        let parent = $(this).parent().parent();
        let pid = parent.find('input[name=pid]').val();
        let cid = parent.find('input[name=cid]').val();
        let sid = parent.find('input[name=sid]').val();

        let qty = parent.find('input[name=qty]');

        $.ajax({
            type: "post",
            url: "/Cart/QuantityProcess",
            data: {
                id: pid,
                num: 1,
                cid: cid,
                sid: sid
            },
            success: function (response) {
                if (response.status === 'Success') {
                    qty.val(Number.parseInt(qty.val()) + 1);
                    location.reload();
                }
            }
        });

    });


    $('.delete').click(function () {
        let parent = $(this).parent();
        let pid = parent.find('input[name=pid]').val();
        let cid = parent.find('input[name=cid]').val();
        let sid = parent.find('input[name=sid]').val();
        $.ajax({
            type: "post",
            url: "/Cart/QuantityProcess",
            data: {
                id: pid,
                num: 0,
                cid: cid,
                sid: sid
            },
            success: function (response) {
                if (response.status === 'Success') {
                    parent.parent().remove();
                    location.reload();
                }
            }
        });
    });
});
