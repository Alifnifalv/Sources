app.directive(
    'ngWith',
    function () {
        return {
            restrict: 'A',
            transclude: true,
            template: '<div>ng-with: </div>',
            controller: function ($scope, $element, $attrs, $transclude) {
                console.log('controller scope:', $scope);

                console.log($attrs.ngWith);
                $scope.model = $scope.$eval($attrs.ngWith);
                console.log('model: ', $scope.model);

                function registerTranscludeScopeWatcher(scope, property) {
                    scope.$watch(property, function (newValue, oldValue) {
                        console.log('watcher (' + property + ') > ', arguments);
                        $scope.model[property] = newValue;
                    });
                }

                function registerParentScopeWatcher(scope, property) {
                    $scope.$watch($attrs.ngWith + '.' + property, function (newValue, oldValue) {
                        console.log('watcher (' + property + ') < ', arguments);
                        scope[property] = newValue;
                    });
                }

                // modify the transcluded scope then add the cloned node to the template
                $transclude(function (clone, scope) {
                    console.log('$transclude scope:', scope);
                    scope.name = 'AngularJS';
                    //scope.model = model;

                    for (var m in $scope.model) {
                        if ($scope.model.hasOwnProperty(m)) {
                            scope[m] = $scope.model[m];
                            registerTranscludeScopeWatcher(scope, m);
                            registerParentScopeWatcher(scope, m);
                        }
                    }


                    var transcluded = angular.element('<span style="color:red"></span>').append(clone);
                    $element.children().append(transcluded);
                });
            }
        };
    }
);