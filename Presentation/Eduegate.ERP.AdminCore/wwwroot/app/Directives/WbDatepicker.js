app.directive('wbDatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModel) {
            //HH:mm ->  24 hours format, hh:mm A -> 12 hrs
            var splittedDateTime = _dateTimeFormat.split(" ");
            var dateTimeFormat = splittedDateTime[0];

            $(elem).datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: dateTimeFormat,
                timepicker: false
            });
        },
    };
});