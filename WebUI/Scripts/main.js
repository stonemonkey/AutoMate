var map = null;

function initMap(key) {
    if (map) {
        return;
    }
    map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
        credentials: key,
        enableClickableLogo: false,
        showDashboard: false,
        animations: false
    });
    Microsoft.Maps.loadModule('Microsoft.Maps.Search', {});

    map.setView({ zoom: 6, center: new Microsoft.Maps.Location(45.867063, 24.916992) });
}
function getMap() {

    var clientStatisticsItems = [];

    function getAgresivityRateByLocation(locationName) {
        for (var i = 0, len = clientStatisticsItems.length; i < len; i++) {
            if (clientStatisticsItems[i].Location === locationName)
                return clientStatisticsItems[i].AgresivityRate;
        }
        return 0;
    }

    $('.preloader').show();
    
    $.ajax({
        type: "GET",
        datatype: 'json',
        url: "Home/GetStatistics",
        data: {
            startDate: $('#slider').dateRangeSlider("values").min.toJSON(),
            endDate: $('#slider').dateRangeSlider("values").max.toJSON()
        },
        success: function (data) {
           
            initMap(data.BingMapAuthKey);
            map.entities.clear();
            


            $.each(data.ClientStatisticses, function (index, item) {
                clientStatisticsItems.push(item);
            });


            $(data.Counties).each(function () {
                var countyName = this.Name;
                var arrayCollection = [];
                $(this.Locations).each(function () {

                    var location = new Microsoft.Maps.Location(this.Latitude, this.Longitude);
                    arrayCollection.push(location);
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
        },
        complete: function () {
            $('.preloader').hide();
        }
    });
}

function getStartEndDate() {
    var getDate = function (sDate) {
        var yy = sDate.split('-')[0];
        var mm = sDate.split('-')[1];
        var dd = sDate.split('-')[2];
        return new Date(parseInt(yy), parseInt(mm), parseInt(dd));
    };
    var defer = $.Deferred();
    $.getJSON('Home/GetStartEndDate', function (data) {
        $('#endDate').val(data.EndDate);
        $('#startDate').val(data.StartDate);
        $("#slider").dateRangeSlider({
            bounds: {
                min: (getDate(data.StartDate)),
                max: (getDate(data.EndDate))
            },
            defaultValues: {
                min: getDate(data.StartDate),
                max: getDate(data.EndDate)
            }
        }).bind("valuesChanged", getMap);
        defer.resolve();
    });
    return defer.promise();
}

$(function () {
    
    getStartEndDate().then(getMap);

});


