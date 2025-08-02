app.directive('colorpicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, $element, $attrs, model) {
            $($element).colorpicker({
                showOn: "button",
            });

            $scope.$watch($attrs['ngModel'], function (v) {
                $($element).colorpicker("val", $($element).val());
            });
            
            $($element).on("change.color", function (event, color) {
                $(e.target).val(color);
                model.$setViewValue(color);
            });
        }
    }
});

