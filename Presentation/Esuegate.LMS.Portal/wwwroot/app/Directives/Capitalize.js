app.directive('capitalize', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, $element, $attrs, ngModel) {
            $($element).on('keyup', function (e) {
                var inputValue = $(e.target).val();
                var lower = inputValue.toLowerCase();
                const captitalize = lower.replace(/^\w/, function (chr) {
                    return chr.toUpperCase();
                });
                
                $parse($attrs.ngModel).assign($scope, captitalize);
                $(e.target).val(captitalize);
            });
        }
    };
}]);