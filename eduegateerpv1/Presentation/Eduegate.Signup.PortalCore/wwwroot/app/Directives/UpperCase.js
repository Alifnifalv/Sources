app.directive('uppercase', function ($parse) {
    return {
        require: 'ngModel',
        link: function ($scope, $element, $attrs, ngModel) {
            $($element).on('keyup', function (e) {
                var upperCase = $(e.target).val().toUpperCase();
                $parse($attrs.ngModel).assign($scope, upperCase )
                $(e.target).val(upperCase);
            });
        }
    };
});