(function () {
    /*compare two input fields*/
    var compareTo = function () {
        return {
            require: "ngModel",
            scope: {
                otherModelValue: "=compareTo"
            },
            link: function (scope, element, attributes, ngModel) {
                ngModel.$validators.compareTo = function (modelValue) {
                    return modelValue == scope.otherModelValue;
                };
                scope.$watch("otherModelValue", function () {
                    ngModel.$validate();
                });
            }
        };
    };
    app.directive("compareTo", compareTo);

    app.directive('validateEmail', function () {
        //email regular expression.
        var emailRegul = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,5}$/;

        return {
            require: 'ngModel',
            link: function (scope, element, attributes, ctrl) {
                element.on('change', function doValidate() {

                    if (element.val().length > 0) {
                        if (emailRegul.test(element.val())) {
                            ctrl.$setValidity('EmailId', true);
                        } else {
                            ctrl.$setValidity('EmailId', false);
                        }
                    } else {
                        ctrl.$setValidity('EmailId', true);
                    }
                });
                //scope.$watch(doValidate);
            }
        };
    });

    app.directive('passwordCharactersValidator', function () {
        var PASSWORD_FORMATS = [
          /[^\w\s]+/, //special characters
          /[A-Z]+/, //uppercase letters
          /\w+/, //other letters
          /\d+/ //numbers
        ];

        return {
            require: 'ngModel',
            link: function (scope, element, attributes, ngModel) {
                ngModel.$parsers.push(function (value) {
                    var status = true;
                    angular.forEach(PASSWORD_FORMATS, function (regex) {
                        status = status && regex.test(value);
                    });
                    ngModel.$setValidity('passwordCharacters', status);
                    return value;
                });

                scope.$watch(function () {
                    ngModel.$validate();
                });
            }
        }
    });

    app.directive('ngUnique', ['$http', '$compile', function (async, $compile) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {
                elem.on('change', function (evt) {
                    scope.$apply(function () {
                        console.log(attrs);
                        /* get the input value */
                        var val = elem.val();

                        /* call the Account MVC controller becuase if we call using Angular service first directives run then service so which is not usable here. */
                        if (attrs.hasOwnProperty("controllercall") == true)
                            var ajaxConfiguration = { method: 'GET', url: attrs.controllercall + '?' + attrs.param1 + '=' + val };
                        async(ajaxConfiguration)
                            .then(function (data, status, headers, config) {
                                /* if user exist in the DB then return true but here directive behave opposite, false mean show message on UI*/
                                if (data.data === "False") {
                                    //ctrl.$setValidity('unique', true);
                                    elem.parent().children('span.dir-already').remove();
                                }
                                else {
                                    //ctrl.$setValidity('unique', false);
                                    elem.parent().children('span.dir-already').remove();
                                    var newDirective = angular.element('<span class="dir-already" style="color:red;">' + val + ' already exist.</span>');
                                    elem.parent().append(newDirective);
                                    elem.val("");
                                    $compile(newDirective)(scope);
                                }
                            });
                    });
                });
            }
        }
    }]);


    /* this will valid decimal value at 3 decimal point */
    app.directive('validDecimal', function () {
        return {
            require: '?ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                if (!ngModelCtrl) {
                    return;
                }

                ngModelCtrl.$parsers.push(function (val) {
                    if (angular.isUndefined(val)) {
                        var val = '';
                    }

                    var clean = val.replace(/[^-0-9\.]/g, '');
                    var negativeCheck = clean.split('-');
                    var decimalCheck = clean.split('.');
                    if (!angular.isUndefined(negativeCheck[1])) {
                        negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                        clean = negativeCheck[0] + '-' + negativeCheck[1];
                        if (negativeCheck[0].length > 0) {
                            clean = negativeCheck[0];
                        }
                    }

                    if (!angular.isUndefined(decimalCheck[1])) {
                        decimalCheck[1] = decimalCheck[1].slice(0, 3);
                        clean = decimalCheck[0] + '.' + decimalCheck[1];
                    }

                    if (val !== clean) {
                        ngModelCtrl.$setViewValue(clean);
                        ngModelCtrl.$render();
                    }
                    return clean;
                });

                element.bind('keypress', function (event) {
                    if (event.keyCode === 32) {
                        event.preventDefault();
                    }
                });
            }
        };
    });

    /* this will validate percent should be less than 100 */
    app.directive('percent', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attributes, ctrl) {

                element.bind('change', function doValidate() {
                    scope.$apply(function () {
                        if (element.val() <= 100) {
                            ctrl.$setValidity('IsPercent', true);
                        } else {
                            ctrl.$setValidity('IsPercent', false);
                        }
                    });

                });
            }
        };
    });

    app.directive('ngCrudUnique', ['$http', '$compile', function (async, $compile) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {
                elem.on('change', function (evt) {

                    scope.$watch(attrs.ngModel, function (value) {
                        console.log("watch attrs.ngModel " + attrs.ngModel);
                        console.log("watch value " + value);
                    });

                    scope.$apply(function () {
                        console.log(attrs);
                        /* get the input value */
                        var val = elem.val();

                        /* call the Account MVC controller becuase if we call using Angular service first directives run then service so which is not usable here. */
                        if (attrs.hasOwnProperty("controllercall") == true)
                            var ajaxConfiguration = { method: 'GET', url: attrs.controllercall };
                        async(ajaxConfiguration)
                            .then(function (data, status, headers, config) {

                                if (data.data === "True") {
                                    //ctrl.$setValidity('unique' + scope.$index, false);
                                    elem.parent().children('span.dir-already').remove();
                                    var newDirective = angular.element('<span class="dir-already cufon" style="color:red;">' + val + ' '+ attrs.message + '</span>');
                                    elem.parent().append(newDirective);
                                    elem.val("");
                                    //$compile(newDirective)(scope);
                                }
                                else {
                                    //ctrl.$setValidity('unique' + scope.$index, true);
                                    elem.parent().children('span.dir-already').remove();
                                }
                            });
                    });
                });
            }
        }
    }]);

}());