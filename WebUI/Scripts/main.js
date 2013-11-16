
var map = null;
var myToolbox = null;

fromXMLString = function (strXml) {
    if (window.DOMParser) {
        return jQuery(new DOMParser().parseFromString(strXml, "text/xml"));
    } else if (window.ActiveXObject) {
        var doc = new ActiveXObject("Microsoft.XMLDOM");
        doc.async = "false";
        doc.loadXML(strXml);
        return jQuery(doc);
    } else {
        return jQuery(strXml);
    }
};

function getMap() {
    map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
        credentials: 'Aq1q9FCxgHiuqrRe3NywYeSb1BPb-q6eyuyYNHe2rg6-XLds9hzYrAiDKwNBQvZY',
        enableClickableLogo: false,
    });

    map.setView({ zoom: 6, center: new Microsoft.Maps.Location(45.867063, 24.916992) });

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
                var line = new Microsoft.Maps.Polygon(arrayCollection, {
                    fillColor: new Microsoft.Maps.Color(100, 100, 0, 100),
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
                        fillColor: new Microsoft.Maps.Color(100, 100, 0, 100),
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
}

$(function () {
    getMap();
});
