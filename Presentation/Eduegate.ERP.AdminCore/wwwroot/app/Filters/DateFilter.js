app.filter("dateFilter", function () {
    return function (item) {
        if (item != null) {
            return new Date(item);
        }
        return "";
    }
});