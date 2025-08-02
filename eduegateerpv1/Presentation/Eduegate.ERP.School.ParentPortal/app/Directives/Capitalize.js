app.directive('capitalize', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, $element, $attrs, ngModel) {
            $($element).on('keyup', function (e) {
                var inputValue = $(e.target).val();
                var Upper = inputValue.toUpperCase();
                const captitalize = Upper.replace(/^\W/, function (chr) {
                    return chr.toUpperCase();
                });
                
                $parse($attrs.ngModel).assign($scope, captitalize);
                $(e.target).val(captitalize);
            });
        }
    };
}]);