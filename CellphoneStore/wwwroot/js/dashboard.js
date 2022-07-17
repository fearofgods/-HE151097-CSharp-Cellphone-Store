
$(document).ready(function () {

    google.charts.load('current', {
        'packages': ['corechart', 'bar']
    }).then(() => {
        getTopSellData(5);

        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            let target = $(e.target);
            if (target.hasClass('topsell')) {
                getTopSellData(5);
            } else if (target.hasClass('highorders')){
                getTopHighestOrder(5);
            }
        });

    });

    function getTopSellData(value) {
        $.ajax({
            type: "post",
            url: "/Admin/GetTopSellProduct",
            dataType: "JSON",
            data: {
                take: value
            },
            success: function (result) {
                //console.log(result);
                drawTopSellChart(result);
            }
        });
    }

    function getTopHighestOrder(value) {
        $.ajax({
            type: "post",
            url: "/Admin/GetTopValueOrders",
            dataType: "JSON",
            data: {
                take: value
            },
            success: function (result) {
                //console.log(result);
                drawTopHighestOrderCharts(result);
            }
        });
    }

    function drawTopSellChart(result) {
        let arr = [[{ label: 'Tên sản phẩm', type: 'string' },
            { label: 'Số lượng', type: 'number' }]];
        for (item in result) {
            arr.push([result[item].name, result[item].count]);
        }
        
        let data = google.visualization.arrayToDataTable(arr);

        var view = new google.visualization.DataView(data);

        view.setColumns([0, 1,
            {
                calc: "stringify",
                sourceColumn: 1,
                type: "string",
                role: "annotation"
            }]);

        let options = {
            title: `Top ${arr.length - 1} sản phẩm bán chạy!`,
            width: 1200,
            height: 600,
            legend: 'none',
            hAxis: { title: 'Số lượng (đơn vị: cái)' }
        }

        let chart = new google.visualization.BarChart(document.getElementById('linechart_topsell'));

        chart.draw(view, options);

    }

    function drawTopHighestOrderCharts(result) {
        let arr = [[{ label: 'Mã đơn hàng', type: 'string' },
        { label: 'Giá trị', type: 'number' }]];
        for (item in result) {
            arr.push([result[item].id, result[item].total/1000000]);
        }
        let data = google.visualization.arrayToDataTable(arr);

        var view = new google.visualization.DataView(data);

        view.setColumns([0, 1,
            {
                calc: "stringify",
                sourceColumn: 1,
                type: "string",
                role: "annotation"
            }]);

        let options = {
            title: `Top ${arr.length - 1} đơn hàng có giá trị cao nhất!`,
            width: 1200,
            height: 600,
            legend: 'none',
            hAxis: { title: 'Đơn vị: Triệu VND' },
            vAxis: { title: 'Mã đơn hàng'}
        }

        let chart = new google.visualization.BarChart(document.getElementById('linechart_highestorder'));

        chart.draw(view, options);
    }
})