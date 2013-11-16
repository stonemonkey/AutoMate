var map = null;
var clientStatisticsItems = [];

function getAgresivityRateByLocation(locationName) {
    for (var i = 0, len = clientStatisticsItems.length; i < len; i++) {

        if (clientStatisticsItems[i].Location === locationName)
            return clientStatisticsItems[i].AgresivityRate;
    }
    return 0;
}

function getMap() {
    $.ajax({
        type: "GET",
        url: "Home/BingMapsAuthenticationKey",
        success: function (data) {
            map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
                credentials: data   ,
                enableClickableLogo: false,
            });

            Microsoft.Maps.loadModule('Microsoft.Maps.Search', {});

            map.setView({ zoom: 6, center: new Microsoft.Maps.Location(45.867063, 24.916992) });

            $.getJSON('Home/GetLocationAgresivtyRates', function (data) {

                $.each(data, function (index, item) {
                    clientStatisticsItems.push(item);
                });

                $.ajax({
                    url: 'Home/GetRoKmlData',
                    traditional: true,
                    success: function (data) {
                        var xml = $(data);
                        xml.find('coordinates').each(function () {
                            var res = this.textContent.split('\n');
                            var countyName = $(this).parent().parent().parent().parent().parent().find('name').text();
                            var arrayCollection = [];
                            $(res).each(function () {
                                var loc = this.split(',');

                                if (parseFloat(loc[0]) && parseFloat(loc[1])) {
                                    var location = new Microsoft.Maps.Location(parseFloat(loc[1]), parseFloat(loc[0]));
                                    arrayCollection.push(location);
                                }
                            });

                            var color = new Microsoft.Maps.Color(getAgresivityRateByLocation(countyName), 255, 0, 0);

                            var line = new Microsoft.Maps.Polygon(arrayCollection, {
                                fillColor: color,
                                strokeColor: new Microsoft.Maps.Color(200, 0, 100, 100),
                                strokeThickness: 1
                            });
                            line.countyName = countyName;

                            Microsoft.Maps.Events.addHandler(line, 'mouseover', function () {
                                line.setOptions({
                                    fillColor: new Microsoft.Maps.Color(100, 100, 0, 100),
                                    strokeColor: new Microsoft.Maps.Color(200, 0, 100, 100),
                                    strokeThickness: 3
                                });
                            });

                            Microsoft.Maps.Events.addHandler(line, 'mouseout', function () {
                                line.setOptions({
                                    fillColor: color,
                                    strokeColor: new Microsoft.Maps.Color(200, 0, 100, 100),
                                    strokeThickness: 1
                                });
                            });

                            Microsoft.Maps.Events.addHandler(line, 'click', function () {
                                $('#county').html(line.countyName);
                            });

                            map.entities.push(line);
                        });
                    }
                });
            });
        }
    });
}

$(function () {
    getMap();
});


