app.directive('initialvalue', ['$parse',  function ($parse) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, $element, $attrs, ngModel) {
            var initialValue = $attrs.initialvalue;
            $parse($attrs.ngModel).assign($scope, initialValue);
        }
    }
}]);