app.controller("OrderController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Order Tracking View Loaded");

    //Initializing the product price
    $scope.Init = function (model) {
        $scope.TransactionStatusList = {};
        $scope.GetTransactionStatuses();
        $scope.submitted = false;
        $scope.OrderTrack = model;
        $scope.currentStatus = $scope.OrderTrack.StatusID;
        //console.log($scope.OrderTrack);
        
    },

    $scope.Save = function () {
        $scope.submitted = true;
        $scope.IsOrderStatus = true;
        $scope.Isdate = true;
        var formValid = $scope.trackingForm.$valid;

        if ($scope.currentStatus == $scope.OrderTrack.StatusID && parseInt($scope.OrderTrack.StatusID) > 0)
        {
            $scope.IsOrderStatus = false;
            return false;
        }
        
        if ($("#TransactionDate").val().trim() == '') {
            $scope.Isdate = false;
            return false;
        }


        if (formValid) {
        
            var vm = {
                OrderID: $scope.OrderTrack.OrderID,
                StatusID: parseInt($scope.OrderTrack.StatusID),
                Description: $scope.OrderTrack.Description,
                StatusDate: $("#TransactionDate").val(),
            };

            $http({ method: 'POST', url: 'Order/Save', data: vm })
        .then(function (result) {
            $().showMessage($scope, $timeout, false, 'Sucessfully saved.');
            $scope.List();
        });

        
        }
    },

    $scope.GetTransactionStatuses = function () {
        $http({ method: 'GET', url: 'Mutual/GetLookUpData?lookType=TransactionStatus' })
        .then(function (result) {
            console.log("Trasaction list loaded successfully.");
            $scope.TransactionStatusList = result;
            //selectedStatus = result.filter(function (item) {
            //    return (item.Key == $scope.OrderTrack.StatusID);
            //});
            
            //$scope.OrderTrack.StatusValue = selectedStatus[0].Value;
        });
    }

    $scope.List = function () {
        $("#LayoutContentSection").html(' <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

        $http({ method: 'Get', url: "Order/List" })
        .then(function (result) {
            $("#LayoutContentSection").html($compile(result)($scope));
        });
    }


    
    //$scope.SelectedValue = function () {
       

    //   console.log($filter('filter')($scope.TransactionStatusList, { Key: $scope.OrderTrack.StatusID })[0].Value);

    //    selectedStatus = $scope.TransactionStatusList.filter(function (item) {
    //        return (item.Key == $scope.OrderTrack.StatusID);
    //    });
    //    return selectedStatus[0].Value;
    //}

   

}]);

