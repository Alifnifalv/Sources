app.filter('ext', function () {
    return function (fileName) {
        return fileName ? fileName.substr(fileName.lastIndexOf('.') + 1) : null;
    };
})
