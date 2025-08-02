app.controller("BoilerPlateController", ["$scope", "$compile", "$http", "$timeout", function ($scope, $compile, $http, $timeout) {
    console.log("BoilerPlateController");

    var ViewName = null;
    $scope.DataSource = null;
    $scope.GetBoilerPlatesURL = 'Boilerplate/GetBoilerPlates';
    $scope.selectedItem = null;
    var initializeCallBack;

    $scope.init = function (boilerPlateInfo, windowName, callback) {
        initializeCallBack = callback;
        GetBoilerPlates(boilerPlateInfo, windowName);

    };
    angular.element(document).ready(function () {
        $scope.GetNotification();
    });
    function GetBoilerPlates(boilerPlateInfo, windowName) {
        if ($scope.$parent.runTimeParameter != undefined) {
            for (var i = 0; i <= $scope.$parent.runTimeParameter.length - 1; i++) {
                boilerPlateInfo.RuntimeParameters.push($scope.$parent.runTimeParameter[i]);
            }
        }

        $http({
            method: 'POST',
            url: $scope.GetBoilerPlatesURL,
            data: boilerPlateInfo,
        })
            .then(function (result) {
                if (!result.IsError) {
                    if (result.data != undefined && result.data != null && result.data != '') {
                        $scope.DataSource = JSON.parse(result.data);
                    };
                }

                if (initializeCallBack) {
                    initializeCallBack($scope.DataSource);
                }
            })
            .finally(function () {
            });
    }

    $scope.GetNotification = function () {

        $scope.NotificationList = [];
        $.ajax({
            type: "GET",

            url: utility.myHost + "Home/GetNotificationAlerts",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.NotificationList = result.Response;
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    angular.element(document).ready(function () {
        $(document).on('click', '.msgListItem', function () {
            var currentElement = $(this);
            var LoginID = $(this).attr('id');
        
            window.location = "NewApplicationFromSibling?loginID=" + LoginID;
            
            
        });

    });

}]);