app.directive('lowercase', ['$parse',  function ($parse) {
    return {
        require: 'ngModel',
        link: function ($scope, $element, $attrs, ngModel) {
            $($element).on('keyup', function (e) {
                var lowerCase = $(e.target).val().toLowerCase();
                $parse($attrs.ngModel).assign($scope, lowerCase)
                $(e.target).val(lowerCase);
            });
        }
    };
}]);