app.directive('filetype', function () {
    return {
        restrict: 'A',
        scope: {
            ngModel: '=',
            ngChange: '&',
            type: '@'
        },
        link: function (scope, element, attrs) {
            if (scope.type.toLowerCase() != 'file') {
                return;
            }
            element.bind('change', function () {
                let files = element[0].files;
                scope.ngModel = files;
                scope.$apply();
                scope.ngChange();
            });
        }
    };
});