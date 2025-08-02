angular.module('Eduegate.Recruitment.Portal').directive('datetimePicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            element.datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,  // Enable month dropdown  
                changeYear: true,   // Enable year dropdown  
                onSelect: function (dateText) {
                    scope.$apply(function () {
                        ngModelCtrl.$setViewValue(dateText);
                    });
                }
            });

            // Update the datepicker when the model changes  
            ngModelCtrl.$render = function () {
                element.datepicker('setDate', ngModelCtrl.$viewValue);
            };

            // Cleanup on destroy  
            element.on('$destroy', function () {
                element.datepicker('destroy');
            });
        }
    };
});