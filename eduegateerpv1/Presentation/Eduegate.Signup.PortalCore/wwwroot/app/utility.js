function utility() { }

/*if you want to find that object contain value or not
retrun : boolean(TRUE or  FALSE)*/
utility.checkObjectContain = function (obj, value) {
    return !!~obj.indexOf(value)
};

/* get the URL's */
utility.host = window.location.host;
utility.hostname = window.location.hostname;
utility.protocol = window.location.protocol;
utility.port = window.location.port;
utility.origin = window.location.origin
utility.href = window.location.href;
utility.myHost = "/";


/* this will return you collection of array */
utility.getArrayByNumber = function (number) {
    return new Array(number);
}

utility.redirect = function (path) {
    window.location = path;
}

utility.getDateFormat = function (serverDateFormat) {
    if (serverDateFormat.indexOf('tt') > 0) {
        serverDateFormat = serverDateFormat.replace('tt', 'a');
    }
    return serverDateFormat;
}


/*set position on top*/
utility.winScroll = function (x, y) {
    window.scrollTo(x, y);
}

/* this will remove particular element from list 
@list: list of object
@item: which item we have to remove from the list
*/
utility.removeItemFromList = function (list, item) {
    for (var i = list.length; i--;) {
        if (list[i] === item) {
            list.splice(i, 1);
        }
    }
}

/* check number is integer or not */
utility.isInt = function (n) {
    return Number(n) === n && n % 1 === 0;
}

utility.formatDate = function (date) {
    var year = date.getFullYear(),
        month = date.getMonth() + 1, // months are zero indexed
        day = date.getDate(),
        hour = date.getHours(),
        minute = date.getMinutes(),
        second = date.getSeconds(),
        hourFormatted = hour % 12 || 12, // hour returned in 24 hour format
        minuteFormatted = minute < 10 ? "0" + minute : minute,
        morning = hour < 12 ? " AM" : " PM";

    return month + "/" + day + "/" + year + " " + hourFormatted + ":" +
        minuteFormatted + morning;
}

function ViewButtonLoaderWithOverlay() {
    $("#Overlay").fadeIn();
    $("#ButtonLoader").fadeIn();
}

function HideButtonLoaderWithOverlay() {
    $("#Overlay").fadeOut();
    $("#ButtonLoader").fadeOut();
}

function viewLoader() {
    $("#LayoutContentSection").html(' <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');
}

function HideLoaderWithErrorMessage() {
    $("#LayoutContentSection").html(' <center><span style="color:Red;">Looks system is broken, please contact system administrator or try after some time.</span></center>');
}

utility.getRemainingHoursText = function (date, today) {
    var deliveryDate = new Date(date);
    var diffMs = (today - deliveryDate); // milliseconds between now & delivery date
    var seconds = Math.floor((diffMs / 1000) % 60);
    var diffMins = Math.floor((diffMs / 1000 / 60) % 60);// minutes
    var diffHrs = Math.floor((diffMs / (1000 * 60 * 60)) % 24);// hours
    //var diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24));// days -- days were 24 so commented this
    var diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24) % 30);// days --how to calculate day on month ? 

    var remainingString = Math.abs(diffDays) + "days " + Math.abs(diffHrs) + ":" + Math.abs(diffMins) + " " + Math.abs(seconds);

    if (diffMs > 0)
        return remainingString + " delayed";
    else
        return remainingString + " remaining";

    return remainingString;
}

utility.GetHoursTakenText = function (startDate, currentDate, isJobPicked) {

    if (isJobPicked == false)
        return "";

    var todayDate = new Date(currentDate); // current date and time
    var assignedDate = new Date(startDate); // job start date

    var diffMs = (todayDate - assignedDate);
    var seconds = Math.floor((diffMs / 1000) % 60);
    var diffMins = Math.floor((diffMs / 1000 / 60) % 60);// minutes
    var diffHrs = Math.floor((diffMs / (1000 * 60 * 60)) % 24);// hours
    //var diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24));// days -- days were 24 so commented this
    var diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 30));// days --how to calculate day on month ? 
    return Math.abs(diffDays) + "days " + Math.abs(diffHrs) + ":" + Math.abs(diffMins) + " " + Math.abs(seconds);
}

//Populates google map on a give div id with given coordinates and zoom level
utility.GetMapByCoordinates = function (latitude, longitude, zoomLevel, elementName) {
    map = null;
    document.getElementById(elementName).innerHTML = "";
    //google.maps.visualRefresh = true;
    var latLong = new google.maps.LatLng(latitude, longitude);

    // These are options that set initial zoom level, where the map is centered globally to start, and the type of map to show
    var mapOptions = {
        zoom: zoomLevel,
        center: latLong,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };

    // This makes the div with id a google map
    map = new google.maps.Map(document.getElementById(elementName), mapOptions);

    var marker = new google.maps.Marker({
        position: latLong
    });

    marker.setMap(map);
    map.setZoom(zoomLevel);
    map.setCenter(marker.getPosition());
}

utility.openInNewTab = function (url) {
    var win = window.open(url, '_blank');
    win.focus();
}

utility.replaceString = function (string, find, replace) {
    if (replace === undefined)
        replace = '';

    return string.replace(new RegExp(find, 'gi'), replace);

}

utility.generateUUID = function () {
    var d = new Date().getTime();
    // if ( window && window.performance && typeof window.performance.now === "function") {
    //     d += performance.now(); //use high-precision timer if available
    // }
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
};

utility.UserCache = {};
