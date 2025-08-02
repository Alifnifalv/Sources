/* this will convert raw html to safe html */
app.filter("safehtml", ['$sce', function ($sce) {
    return function (htmlCode) {
        return $sce.trustAsHtml(htmlCode);
    }
}]);