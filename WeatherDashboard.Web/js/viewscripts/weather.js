
$(document).ready(function () {

    var URL = document.baseURI;
    (function ($) {
        var Weather = {
            init: function () {

                this.cache();
                this.bindEvents();
                

                return this;
            },
            cache: function () {

                this.tempGraph = $('#tempGraph');
                this.datepicker1 = $('#datepicker1');
                this.selCity = $('#selCity');
                this.selScale = $('#selScale');
                this.gridTemps = $('#gridTemps');

            },
            bindEvents: function () {
                var self = Weather;

                var dateNow = new Date();

                this.datepicker1.datetimepicker({
                    inline: true,
                    defaultDate:dateNow,
                    maxDate: 'now',
                });

                this.loadCities();
                

                this.selCity.on("change", self.loadDataChart);
                this.selScale.on("change", self.loadDataChart);
                this.datepicker1.on("dp.change", self.loadDataChart);
            },

            loadDataChart: function(){
                var self = Weather;

                $body = $("body");

                var result = $.ajax({
                    url: URL + 'Home/GetWeather',
                    type: 'GET',
                    data: { endDate: self.datepicker1.data('DateTimePicker').date().toISOString(), cityName: self.selCity.val(), scale: self.selScale.val() },
                    contentType: 'application/json',
                    dataType: 'json',
                    async: true,
                    beforeSend: function () {
                        $body.addClass("loading");
                    },
                    success: function (data) {
                        
                        if (data.success) {
                            self.createChart(data.categories, data.temps);

                            var objs = [];

                            $.each(data.datesfull, function (i, item) {
                                objs.push({ date: item, temp: data.temps[i]});
                            });

                            self.loadGrid(objs);

                        }
                        else {
                            alert(data.Msg);
                        }

                        $body.removeClass("loading");
                    },
                    error: function (xhr) {
                        alert(xhr.status + " " + xhr.statusText);
                        $body.removeClass("loading");
                    },
                    done: function (xhr) {
                        
                    }
                });

            },

            createChart: function (cat, temps) {
                var self = Weather;
  
                self.tempGraph.highcharts({
                    chart: {
                        type: 'line'
                    },
                    title: {
                        text: 'History weather in ' + self.selCity.val()
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        categories: cat
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: $( "#selScale option:selected" ).text()
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            pointPadding: 0.2,
                            borderWidth: 0
                        },
                        line: {
                            dataLabels: {
                                enabled: true
                            },
                            enableMouseTracking: false
                        }
                    },
                    series: [{
                        name: self.selCity.val(),
                        data: temps

                    }]
                });
            },

            loadCities: function () {
                var self = Weather;

                var result = $.ajax({
                    url: URL + 'Home/GetCities',
                    type: 'GET',
                    contentType: 'application/json',
                    dataType: 'json',
                    async: true,
                    beforeSend: function () {

                    },
                    success: function (data) {
                        if (data.success) {
                            self.selCity.find('option').remove().end();
                            $.each(data.cities, function (i, item) {
                                var o = new Option(item.name, item.name);
                                $(o).html(item.name);
                                self.selCity.append(o);
                            });

                            self.loadScales();
                        }
                        else {
                            alert(data.Msg);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.status + " " + xhr.statusText);
                    },
                    done: function (xhr) {

                    }
                });

            },

            loadScales: function () {
                var self = Weather;

                var result = $.ajax({
                    url: URL + 'Home/GetScales',
                    type: 'GET',
                    contentType: 'application/json',
                    dataType: 'json',
                    async: true,
                    beforeSend: function () {

                    },
                    success: function (data) {
                        if (data.success) {
                            self.selScale.find('option').remove().end();
                            $.each(data.scales, function (id, text) {
                                var o = new Option(text, id);
                                $(o).html(text);
                                self.selScale.append(o);
                            });

                            self.loadDataChart();
                        }
                        else {
                            alert(data.Msg);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.status + " " + xhr.statusText);
                    },
                    done: function (xhr) {

                    }
                });

            },

            loadGrid: function (data) {
                var self = Weather;

                try {
                    self.gridTemps.dataTable().fnDestroy();
                } catch (e) { }
                //self.BusquedaCfiForm();
                var oTable = self.gridTemps.dataTable({
                    bDestroy: true,
                    aaSorting: [[0, 'desc']],
                    sDom: 'RfClrtip',
                    bJQueryUI: true,
                    sPaginationType: "full_numbers",
                    bInfo: true,
                    aaData: data,
                    renderer: { header: "bootstrap" },
                    deferRender: true,
                    responsive: true,
                    //  aaSorting: [[1, 'desc']],
                    aoColumns: [{
                        "mData": function (data, type, row) {
                            return data.date;
                        }, 

                    }, {
                        "mData": function (data, type, row) {

                            return data.temp;
                        },

                    },
                    ],
                    fnCreatedRow: function (nRow, dato, dato1) {

                    },
                });
            },

        }
        window.Weather = Weather.init();

    })(jQuery);
});
