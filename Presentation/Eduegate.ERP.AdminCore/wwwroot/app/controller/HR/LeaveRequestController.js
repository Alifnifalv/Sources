
app.controller("LeaveRequestController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("LeaveRequestController Loaded");
    function showoverlay() {
        $('.preload-overlay', $($scope.crudwindowcontainer)).attr('style', 'display:block');
    }

    function hideoverlay() {
        $('.preload-overlay', $($scope.crudwindowcontainer)).hide();
    }


    $scope.LeaveTypeChanges = function (event, model) {
        if (!model.LeaveType) {
            return false;
        }
        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "VACATION_LEAVE_TYPE",
        }).then(function (result) {
            $scope.leaveTypeID = result.data;
            if ($scope.leaveTypeID && $scope.leaveTypeID == model.LeaveType) {
                $http({
                    method: 'Get', url: "Payroll/Payroll/GetSettingValueByKey?settingKey=" + "VACATION_END_DATE",
                }).then(function (result) {
                    /* model.ToDateString = result.data;*/
                    var formattedSDate = new Date(moment(result.data, 'DD/MM/YYYY'));
                    var systemDate = new Date();
                    if (formattedSDate > systemDate) {
                        model.ToDateString = moment(formattedSDate).format("DD/MM/YYYY");
                        $http({
                            method: 'Get', url: "Payroll/Payroll/GetSettingValueByKey?settingKey=" + "VACATION_START_DATE",
                        }).then(function (result) {
                            // model.FromDateString = result.data;

                            var formattedSDate = new Date(moment(result.data, 'DD/MM/YYYY'));
                            model.FromDateString = moment(formattedSDate).format("DD/MM/YYYY");
                        });
                    }
                });

                //model.IsDisable = true;
            }
            else {
                //model.IsDisable = false;
            }
        });

    }

}]);