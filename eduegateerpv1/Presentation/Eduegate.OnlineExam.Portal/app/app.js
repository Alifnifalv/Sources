var app = angular.module('Eduegate.OnlineExam.Portal',
    ['ngRoute', 'ngSanitize']);


(function () {
    angular
        .module('Eduegate.OnlineExam.Portal')
        .value('$subscription', $.connection);   
})();

app.filter('makePositive', function () {
    return function (num) { return Math.abs(num); };
});

app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            out = items;
        }

        return out;
    };
});


app.directive('angulardatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            $(element).datepicker({
                onSelect: function (date) {
                    ngModel.$setViewValue(date);
                }
            });

            ngModel.$formatters.push(function (value) {
                return new Date(value).toLocaleDateString();
            });
        }
    };
});

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

app.config(['$httpProvider', function ($httpProvider) {
    // Line break to avoid horizontal scroll. Put it all on one
    //  line in your real code.
    $httpProvider.defaults.headers
                 .common['X-Requested-With'] = 'XMLHttpRequest';
}]);

app.directive('prettyp', function () {
    return function (scope, element, attrs) {
        $(element).prettyPhoto({ theme: 'light_square', deeplinking: false, social_tools: false });
    }
});

app.directive('angularckeditor', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, elm, attr, ngModel) {
            var ck = CKEDITOR.replace(elm[0]);

            ck.on('pasteState', function () {
                $scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            });

            ngModel.$render = function (value) {
                ck.setData(ngModel.$modelValue);
            };
        }
    };
});

app.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                // this next if is necessary for when using ng-required on your input. 
                // In such cases, when a letter is typed first, this parser will be called
                // again, and the 2nd time, the value will be undefined

                if (inputValue == undefined)
                    return ''

                var transformedInput = inputValue.replace(/[^0-9]/g, '');

                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
});

app.directive('ngBlur', function () {
    return function (scope, elem, attrs) {
        elem.bind('blur', function () {
            scope.$apply(attrs.ngBlur);
        });
    };
});

app.directive('wbDatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModel) {
            //HH:mm ->  24 hours format, hh:mm A -> 12 hrs
            var splittedDateTime = _dateTimeFormat.split(" ")
            var dateTimeFormat = splittedDateTime[0].toUpperCase();

            $(elem).datetimepicker({
                format: dateTimeFormat,
            });

            $(elem).on("dp.change", function (e) {
                ngModel.$setViewValue($(e.target).data().date);
            });
        },
    };
});

app.directive('datetimepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModel) {
            //HH:mm ->  24 hours format, hh:mm A -> 12 hrs
            var splittedDateTime = _dateTimeFormat.split(" ")
            var dateTimeFormat = splittedDateTime[0].toUpperCase() + " " + splittedDateTime[1] + " " + splittedDateTime[2].replace("tt", "A");

            $(elem).datetimepicker({
                format: dateTimeFormat,
            });

            $(elem).on("dp.change", function (e) {
                ngModel.$setViewValue($(e.target).data().date);
            });
        },
    };
});

app.directive('timepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModel) {
            //HH:mm ->  24 hours format, hh:mm A -> 12 hrs
            var splittedDateTime = _dateTimeFormat.split(" ")
            var dateTimeFormat = splittedDateTime[0].toUpperCase() + " " + splittedDateTime[1] + " " + splittedDateTime[2].replace("tt", "A");

            $(elem).datetimepicker({
                format: 'LT',
            });

            $(elem).on("dp.change", function (e) {
                ngModel.$setViewValue($(e.target).data().date);
            });
        },
    };
});

app.directive('customdatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModel) {

            var dateFormat = _dateFormat.toUpperCase();

            $(elem).datetimepicker({
                format: dateFormat,
            });

            $(elem).on("dp.change", function (e) {
                ngModel.$setViewValue($(e.target).data().date);
            });
        },
    };
});


app.directive('angulardatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            $(element).datepicker({
                onSelect: function (date) {
                    ngModel.$setViewValue(date);
                }
            });

            ngModel.$formatters.push(function (value) {
                return new Date(value).toLocaleDateString();
            });
        }
    };
});


/* this will convert raw html to safe html */
app.filter("safehtml", ['$sce', function ($sce) {
    return function (htmlCode) {
        return $sce.trustAsHtml(htmlCode);
    }
}]);


app.filter('sumByKey', function () {
    return function (data, key, charge) {
        if (typeof (data) === 'undefined' || typeof (key) === 'undefined') {
            return 0;
        }

        var sum = 0;
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i][key] != null)
                sum += parseFloat(data[i][key]);
        }

        if (charge == undefined)
            return sum;
        else
            return sum + parseFloat(charge);
    };
});

app.directive('initialvalue', function ($parse) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, $element, $attrs, ngModel) {
            var initialValue = $attrs.initialvalue;
            $parse($attrs.ngModel).assign($scope, initialValue);
        }
    }
});

app.filter('range', function () {
    return function (n) {
          var res = [];
          for (var i = 0; i < n; i++) {
              res.push(i);
          }
          return res;
      };
});

app.filter("dateFilter", function () {
    return function (item) {
        if (item != null) {
            return new Date(item);
        }
        return "";
    }});

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

app.factory('alert', function($uibModal) {

    function show(action, event) {
        return $uibModal.open({
            templateUrl: 'modalContent.html',
            controller: function() {
                var vm = this;
                vm.action = action;
                vm.event = event;
            },
            controllerAs: 'vm'
        });
    }

    return {
        show: show
    };
});

app.directive('selectonclick', ['$window', function ($window) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.on('click', function () {
                if (!$window.getSelection().toString()) {
                    // Required for mobile Safari
                    this.setSelectionRange(0, this.value.length)
                }
            });
        }
    };
}]);

app.directive('onerrorsrc', function () {
    return {
        link: function (scope, element, attrs) {
            if (!element.src) {
                element[0].src = attrs.onerrorsrc;
            }

            element.bind('error', function () {
                if (attrs.src != attrs.onerrorsrc) {
                    attrs.$set('src', attrs.onerrorsrc);
                }
            });
        }
    }
});
