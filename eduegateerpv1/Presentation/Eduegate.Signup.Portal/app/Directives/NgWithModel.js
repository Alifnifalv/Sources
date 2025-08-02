app.directive(
    'ngWithModel',
    function () {
        return {
            restrict: 'A',
            transclude: true,
            template: '<div>ng-with-model: </div>',
            controller: function ($scope, $element, $attrs, $transclude) {
                console.log('controller scope:', $scope);

                console.log($attrs.ngWithModel);
                var model = $scope.$eval($attrs.ngWithModel);
                console.log(model);
                // modify the transcluded scope then add the cloned node to the template
                $transclude(function (clone, scope) {
                    console.log('$transclude scope:', scope);
                    scope.name = 'AngularJS';
                    scope.model = model;
                    var transcluded = angular.element('<span style="color:red"></span>').append(clone);
                    $element.children().append(transcluded);
                });
            }
        };
    }
);