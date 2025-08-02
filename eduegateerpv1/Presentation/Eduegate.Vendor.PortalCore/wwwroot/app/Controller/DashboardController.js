app.controller("DashboardController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {
        console.log("DashboardController Loaded");

        $scope.SettingValue = null;

        $scope.Init = function (model, window, viewName) {
            $scope.DashBoardMenuCardCounts();
            $scope.GetNotifications();
        };

        $scope.DashBoardMenuCardCounts = function () {  
            GetSettingValue("CHART_META_DATA_ID_FOR_VENDOR_PORTAL").then(function () {
                $scope.CardDatas = []; // Initialize CardDatas as an empty array  

                $.ajax({
                    type: "GET",
                    url: "/Home/GetCountsForDashBoardMenuCards?chartID=" + $scope.SettingValue,
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        $scope.$apply(function () {
                            if (result) {
                                const columnDatas = result.ColumnDatas.map(item => JSON.parse(item));
                                const CardDatas = columnDatas.map(data => ({
                                    Header: data[0],          // Title  
                                    Value: data[1],           // Value  
                                    CardColor: data[2]       // Color  
                                }));
                                $scope.CardDatas = CardDatas;
                            } else {
                                $scope.CardDatas = [];  // Ensure CardDatas is empty if result is null  
                            }
                        });
                    }
                });
            });
        };

        $scope.GetNotifications = function () {

            $.ajax({
                type: "GET",
                url: "/Home/GetNotifications",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result) {
                            $scope.Notifications = result;
                        } else {
                            $scope.Notifications = null;  
                        }
                    });
                }
            });
        }

        function GetSettingValue(settingCode) {
            // Return a promise  
            return $http({
                method: 'GET',
                url: utility.myHost + "Mutual/GetSettingValueByKey?settingKey=" + settingCode,
            }).then(function (result) {
                if (result.data) {
                    $scope.SettingValue = result.data;
                } else {
                    $scope.SettingValue = null;
                }
            });
        }

    }
]);
