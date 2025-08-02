app.controller("DriverScheduleLogController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.init = function () {

        $.ajax({
            type: 'GET',
            url: utility.myHost + "Mutual/GetDynamicLookUpData?lookType=Route&defaultBlank=false;",
            success: function (result) {
                $scope.Routes = result;
            }
        });

        $scope.ButtonText = "Mark as";
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    //Function for driver schedule log edit
    //$scope.EditDriverScheduleLog = function (driverScheduleLogIID) {
    //    var windowName = 'DriverSchedule';
    //    var viewName = 'Edit Driver Schedule';

    //    if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
    //        return;

    //    $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
    //    editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + driverScheduleLogIID;

    //    $http({ method: 'Get', url: editUrl })
    //        .then(function (result) {
    //            $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
    //            $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
    //        });
    //};

    $scope.StopEntryStatusChanges = function (viewModel) {

        if (!viewModel.StopEntryStatus) {
            return false;
        };

        if ($scope.LookUps.StopEntryStatus.length > 0) {
            viewModel.ScheduleLogTypes = [];
            var logStatus = $scope.LookUps.StopEntryStatus.find(s => s.Key == viewModel.StopEntryStatus);

            if (logStatus != null) {
                if (logStatus.Value.toLowerCase().includes("pick")) {
                    viewModel.ScheduleLogInType = "PICK-IN";
                    viewModel.ScheduleLogOutType = "PICK-OUT";
                }
                else if (logStatus.Value.toLowerCase().includes("drop")) {
                    viewModel.ScheduleLogInType = "DROP-IN";
                    viewModel.ScheduleLogOutType = "DROP-OUT";
                }
            }
        }

    };

    $scope.StudentChanges = function (viewModel) {

        if (!viewModel.Student.Key) {
            return false;
        };

        showOverlay();
        model = viewModel;

        var url = "Schools/School/GetStudentDetailFromStudentID?StudentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.ClassName = result.data.ClassName;
                model.SectionName = result.data.SectionName;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

    };

    $scope.RouteGroupChanges = function (viewModel) {

        if (!viewModel.RouteGroup) {
            return false;
        }

        showOverlay();
        var model = viewModel;
        model.Route = [];
        model.RouteStop = [];
        model.Vehicle = [];

        var url = "Schools/School/GetRoutesByRouteGroupID?routeGroupID=" + model.RouteGroup;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Route = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.RouteChanges = function (viewModel) {

        if (!viewModel.Route.Key) {
            return false;
        }

        showOverlay();
        var model = viewModel;
        model.RouteStop = [];

        var stopurl = "Schools/School/GetRouteStopsByRoute?routeID=" + model.Route.Key;
        $http({ method: 'Get', url: stopurl })
            .then(function (result) {
                $scope.LookUps.RouteStopMap = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

        var vehurl = "Schools/School/GetVehiclesByRoute?routeID=" + model.Route.Key;
        $http({ method: 'Get', url: vehurl })
            .then(function (result) {
                $scope.LookUps.VehicleDetails = result.data;

                if ($scope.LookUps.VehicleDetails.length == 1) {
                    model.Vehicle = {
                        Key: $scope.LookUps.VehicleDetails[0].Key,
                        Value: $scope.LookUps.VehicleDetails[0].Value
                    }
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.SheduleLogInStatusChanges = function (viewModel) {

        if (!viewModel.SheduleLogInStatus) {
            return false;
        }

        if ($scope.LookUps.SheduleLogStatus.length > 0) {

            var logStatus = $scope.LookUps.SheduleLogStatus.find(s => s.Key == viewModel.SheduleLogInStatus);

            if (logStatus != null) {
                if (logStatus.Value.toLowerCase().includes("in")) {
                    viewModel.InStatus = "I";
                }
                else if (logStatus.Value.toLowerCase().includes("out")) {
                    viewModel.InStatus = "O";
                }
                else {
                    viewModel.InStatus = "A";
                }
            }
        }

    };

    $scope.SheduleLogOutStatusChanges = function (viewModel) {

        if (!viewModel.SheduleLogOutStatus) {
            return false;
        }

        if ($scope.LookUps.SheduleLogStatus.length > 0) {

            var logStatus = $scope.LookUps.SheduleLogStatus.find(s => s.Key == viewModel.SheduleLogOutStatus);

            if (logStatus != null) {
                if (logStatus.Value.toLowerCase().includes("in")) {
                    viewModel.OutStatus = "I";
                }
                else if (logStatus.Value.toLowerCase().includes("out")) {
                    viewModel.OutStatus = "O";
                }
                else {
                    viewModel.OutStatus = "A";
                }
            }
        }

    };

}]);