app.controller("FeePeriodController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("FeePeriodController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.NumberofMonthChanges = function (model) {

        if (model.NumberOfPeriods != "" && model.NumberOfPeriods != null && model.NumberOfPeriods > 0)
        {
            var fromDate = model.PeriodDateString;
            var dateNo = model.NumberOfPeriods;
            var newmonth = parseInt(dateNo) + 0;

            var splitDate = fromDate.split('/');
            var newdate = new Date(splitDate[2], splitDate[1] - 1, splitDate[0])
            newdate.addMonths(newmonth);
            newdate.setDate(1);
            newdate.addDays(-1);
            model.PeriodToDateString = moment(newdate).format(_dateFormat.toUpperCase());

        }

        else
        {
            model.PeriodToDateString = null;
        }

    };

}]);