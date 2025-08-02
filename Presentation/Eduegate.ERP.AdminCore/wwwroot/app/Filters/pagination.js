app.filter('pagination', function () {
    return function (input, start) {
        if (!Array.isArray(input) || input.length === 0) {
            return []; // return an empty array if input is not an array or is empty
        }

        start = +start || 0; // parse to int, default to 0 if NaN
        return input.slice(start);
    };
});