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
    };
});