app.directive('prettyp', function () {
    return function (scope, element, attrs) {
        $(element).prettyPhoto({ theme: 'light_square', deeplinking: false, social_tools: false });
    }
});