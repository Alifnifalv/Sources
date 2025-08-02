app.filter('sumByKey', function () {
    return function (data, key, charge) {
        if (typeof (data) === 'undefined' || typeof (key) === 'undefined') {
            return 0;
        }

        var sum = 0;
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i][key] != null)
                sum += parseFloat(data[i][key]);
        }

        if (charge == undefined)
            return sum;
        else
            return sum + parseFloat(charge);
    };
});