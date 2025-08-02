
/* required directive for ui select */
app.directive('uiSelectRequired', function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$validators.uiSelectRequired = function (modelValue, viewValue) {
                return (modelValue && modelValue.length > 0 ? true : false);
            };
        }
    };
});