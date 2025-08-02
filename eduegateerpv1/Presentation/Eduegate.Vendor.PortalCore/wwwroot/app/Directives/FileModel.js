app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attributes) {
            element.bind('change', function () {
                scope.$apply(function () {
                    $parse(attributes.fileModel).assign(scope, element[0].files[0]);
                });
            });
        }
    };
}]);