app.directive('angulardatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            $(element).datepicker({
                changeMonth: true,
                changeYear: true,
                onSelect: function (date) {
                    ngModel.$setViewValue(date);
                }
            });

            ngModel.$formatters.push(function (value) {
                return new Date(value).toLocaleDateString();
            });
        }
    };
});