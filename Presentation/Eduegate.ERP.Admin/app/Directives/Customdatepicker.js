app.directive('customdatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModel) {

            var dateFormat = _dateFormat.toUpperCase();

            $(elem).datepicker({
                changeMonth: true,
                changeYear: true,
                format: dateFormat,
            });

            $(elem).on("dp.change", function (e) {
                ngModel.$setViewValue($(e.target).data().date);
            });
        },
    };
});