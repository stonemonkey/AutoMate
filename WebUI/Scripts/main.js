var map = null;

function initMap() {
    var defer = $.Deferred();
    $.ajax({
        url: 'home/GetBingMapsAuthKey',
        success: function (data) {
            map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
                credentials: data,
                enableClickableLogo: false,
                showDashboard: false,
                animations: false
            });
            Microsoft.Maps.loadModule('Microsoft.Maps.Search', {});

            map.setView({ zoom: 6, center: new Microsoft.Maps.Location(45.867063, 24.916992) });

            getCounties().then(function () {
                defer.resolve();
            });
        }
    });

    return defer.promise();
}

function getCounties() {
    var defer = $.Deferred();
    $.getJSON('home/GetBoundaries', function (data) {
        $(data).each(function () {
            var countyName = this.Name;
            var arrayCollection = [];

            $(this.Locations).each(function () {

                    var location = new Microsoft.Maps.Location(this.Latitude, this.Longitude);
                    arrayCollection.push(location);
                });
            var color = new Microsoft.Maps.Color(0, 255, 0, 0);

            var line = new Microsoft.Maps.Polygon(arrayCollection, {
                fillColor: color,
                strokeColor: new Microsoft.Maps.Color(200, 0, 100, 100),
                strokeThickness: 1
            });
            line.oldColor = color;
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
                    fillColor: line.oldColor,
                    strokeColor: new Microsoft.Maps.Color(200, 0, 100, 100),
                    strokeThickness: 1
                });
            });

            Microsoft.Maps.Events.addHandler(line, 'click', function () {
                $('#county').html(line.countyName);
            });

            map.entities.push(line);
            
        });
        defer.resolve();
    });

    return defer.promise();
}

function getCounty(countyName) {
    var length = map.entities.getLength();
    for (var i = 0; i < length; i++) {
        var county = map.entities.get(i);
        if (county.countyName == countyName) {
            return county;
         }
    }
}
function clearColors() {
    var length = map.entities.getLength();
    for (var i = 0; i < length; i++) {
        var county = map.entities.get(i);
        county.setOptions({
            fillColor: new Microsoft.Maps.Color(0, 255, 0, 0)
        });
    }
}

function getMap() {
    
    $.ajax({
        type: "GET",
        datatype: 'json',
        url: "Home/GetStatistics",
        data: {
            startDate: $('#slider').dateRangeSlider("values").min.toJSON(),
            endDate: $('#slider').dateRangeSlider("values").max.toJSON()
        },
        success: function (data) {

            clearColors();

            $.each(data.ClientStatisticses, function (index, item) {
                var color = new Microsoft.Maps.Color(item.AgresivityRate, 255, 0, 0);
                var x = getCounty(item.Location);
                if (x) {
                    x.oldColor = color;
                    x.setOptions({
                        fillColor: color
                    });
                } else {
                    console.log("NOF " + item.Location);
                }
            });
        },
        complete: function () {
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
    initMap().then(function() {
        getStartEndDate().then(getMap);
    });
    
    var appBar, metroJs;
    appBar = $(".appbar").applicationBar({
        preloadAltBaseTheme: true,
        bindKeyboard: true,
        metroLightUrl: '~/content/images/metroIcons_light.jpg',
        metroDarkUrl: '~/content/images/metroIcons.jpg'
    });
    // append the theme options 
    metroJs = jQuery.fn.metrojs;
    metroJs.theme.appendAccentColors({
        accentListContainer: ".theme-options"
    });
    metroJs.theme.appendBaseThemes({
        baseThemeListContainer: ".base-theme-options"
    });
    
});


