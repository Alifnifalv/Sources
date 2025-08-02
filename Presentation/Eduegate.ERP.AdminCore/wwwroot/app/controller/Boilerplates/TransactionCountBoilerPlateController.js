app.controller("TransactionCountBoilerPlateController", ["$scope", "$timeout", "$window", "$http", "$compile", "$sce", "$q",
    function ($scope, $timeout, $window, $http, $compile, $sce, $q) {
        console.log('TransactionCountBoilerPlateController controller loaded.');
        $scope.runTimeParameter = null;
        $scope.Model = null;
        $scope.window = "";
        var DocumentTypeIDs = null;
        $scope.TransactionDetails = 0;
        var DateFrom;
        var DateTo;
        var intervalID;

        //current date
        $scope.CurrentDateStart = new Date();
        $scope.CurrentDateStart.setHours(0, 0, 0, 0);

        $scope.CurrentDateEnd = new Date();
        $scope.CurrentDateEnd.setHours(23, 59, 59, 999);
             

        //yesterday
        $scope.YesterdayDateStart = new Date();
        $scope.YesterdayDateStart.setDate($scope.YesterdayDateStart.getDate() - 1);
        $scope.YesterdayDateStart.setHours(0, 0, 0, 0);

        $scope.YesterdayDateEnd = new Date();
        $scope.YesterdayDateEnd.setDate($scope.YesterdayDateEnd.getDate() - 1);
        $scope.YesterdayDateEnd.setHours(23, 59, 59, 999);

        $scope.YesterdayToCurrentDateTime = new Date();
        $scope.YesterdayToCurrentDateTime.setDate($scope.YesterdayToCurrentDateTime.getDate() - 1);
        var currentDate = new Date();
        $scope.YesterdayToCurrentDateTime.setHours(currentDate.getHours(), currentDate.getMinutes(), currentDate.getSeconds(), currentDate.getMilliseconds());

        //current month
        $scope.CurrentMonthDateStart = new Date();
        $scope.CurrentMonthDateStart.setDate(1);
        $scope.CurrentMonthDateStart.setHours(0, 0, 0, 0);

        //last month
        $scope.LastMonthDateStart = new Date();
        $scope.LastMonthDateStart.setMonth($scope.LastMonthDateStart.getMonth() - 1);
        $scope.LastMonthDateStart.setDate(1);
        $scope.LastMonthDateStart.setHours(0, 0, 0, 0);

        $scope.LastMonthDateEnd = new Date();
        $scope.LastMonthDateEnd.setDate(0);

        $scope.init = function (model, window, documentTypeID, dateFrom, dateTo, isLive) {
            $scope.runTimeParameter = model.parameter;
            $scope.Model = model;
            $scope.window = window;
            DocumentTypeIDs = documentTypeID;
            DateFrom = dateFrom;
            DateTo = dateTo;
            GetTrnasactionCount();
            
            if (isLive != undefined && isLive) {
                intervalID = setInterval(GetTrnasactionCount, 60 * 1000);
            }
        };

        function GetTrnasactionCount() {
            //$http({
            //    method: 'POST',
            //    url: "Inventories/InventoryDetails/GetTransactionDetails",
            //    data: { "DocumentTypeID": DocumentTypeIDs, "DateFrom": DateFrom, "DateTo": DateTo },
            //})
            //.then(function (result) {
            //    $scope.TransactionDetails = result.data; 
            //})
            //.finally(function () {
            //});
        }
    }]);

