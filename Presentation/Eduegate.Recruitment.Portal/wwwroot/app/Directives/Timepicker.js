app.directive('timepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModel) {
            //HH:mm ->  24 hours format, hh:mm A -> 12 hrs
            //var splittedDateTime = _dateTimeFormat.split(" ")
            //var dateTimeFormat = splittedDateTime[0].toUpperCase() + " " + splittedDateTime[1] + " " + splittedDateTime[2].replace("tt", "A");
            jQuery(elem).datetimepicker({
                datepicker: false, 
                formatTime: 'g:i A',
                format: 'g:i A',
                step: 5,
                validateOnBlur: false

            });

            $(elem).on("dp.change", function (e) {
                ngModel.$setViewValue($(e.target).data().date);
            });
        },
    };
});