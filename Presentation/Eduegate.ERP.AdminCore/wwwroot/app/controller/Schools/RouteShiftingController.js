app.controller("RouteShiftingController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("RouteShiftingController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.StudentName = [];

    $scope.GetStudentDetails = function ($event, $element, crudModel) {
        if (crudModel.Routes == null || crudModel.Routes.Key == 0 || crudModel.Routes.Key == "" || crudModel.Routes.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select route!");
            return false;
        }
        
        showOverlay();
        var model = crudModel;
        var url = "Schools/School/GetStudentRouteDetailsList?routeId=" + model.Routes.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.CRUDModel.ViewModel.StudentList = result.data;

                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };
   
    $scope.GetStaffDetails = function ($event, $element, crudModel) {
        if (crudModel.Routes == null || crudModel.Routes.Key == 0 || crudModel.Routes.Key == "" || crudModel.Routes.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select route!");
            return false;
        }

        showOverlay();
        var model = crudModel;
        var url = "Schools/School/GetStaffRouteDetailsList?routeId=" + model.Routes.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.CRUDModel.ViewModel.StaffList = result.data;

                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };

    $scope.AddBelowStud = function (selectedindex) {
        showOverlay();
        var applyFromDate;
        var applyToDate;
        var applyPickupStop;
        var applyDropStop;

        $.each($scope.CRUDModel.ViewModel.StudentList, function (index, objModel) {
            if (selectedindex == index) {
                applyFromDate = objModel.DateFromString;
                applyToDate = objModel.DateToString;
                applyPickupStop = objModel.PickupStopMap;
                applyDropStop = objModel.DropStopMap;
            }
            else if (selectedindex < index) {
                objModel.DateFromString = applyFromDate;
                objModel.DateToString = applyToDate;
                objModel.PickupStopMap = applyPickupStop;
                objModel.DropStopMap = applyDropStop;
            }
            hideOverlay();
        });
    };

    $scope.AddBelowStaff = function (selectedindex) {
        showOverlay();
        var applyFromDate;
        var applyToDate;
        var applyPickupStop;
        var applyDropStop;

        $.each($scope.CRUDModel.ViewModel.StaffList, function (index, objModel) {
            if (selectedindex == index) {
                applyFromDate = objModel.DateFromString;
                applyToDate = objModel.DateToString;
                applyPickupStop = objModel.PickupStopMap;
                applyDropStop = objModel.DropStopMap;
            }
            else if (selectedindex < index) {
                objModel.DateFromString = applyFromDate;
                objModel.DateToString = applyToDate;
                objModel.PickupStopMap = applyPickupStop;
                objModel.DropStopMap = applyDropStop;
            }
            hideOverlay();
        });
    };

    $scope.FillStudentTransportDatas = function ($event, $element, crudModel) {
        var model = $scope.CRUDModel.ViewModel;
        var gridModel = crudModel;

        if (model.Routes == null || model.Routes.Key == 0 || model.Routes.Key == "" || model.Routes.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select route!");
            $scope.CRUDModel.ViewModel.StudentList[0].Student = null;
            return false;
        }

        var stud = gridModel.find(x => x.StudentID == null || x.StudentID == undefined || x.Student.Key != x.StudentID);

        var studentID = stud.Student.Key;

        if (studentID == undefined || studentID == null || studentID == "") {
            $().showMessage($scope, $timeout, true, "Please select Student !");
            hideOverlay();
            return false
        }

        var type = "both";

        showOverlay();
        var url = "Schools/School/GetStudentTransportDetailsByStudentID?studentID=" + studentID + "&IsRouteType=" + type;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                stud.StudentRouteStopMapID = result.data.StudentRouteStopMapID;
                stud.StudentID = result.data.StudentID;
                stud.ClassID = result.data.ClassID;
                stud.SectionID = result.data.SectionID;
                //stud.ClassSection = result.data.ClassSection;
                stud.OldDropStop = result.data.DropStop;
                stud.OldPickUpStop = result.data.PickupStop;
                stud.DateFromString = result.data.DateFromString;
                stud.DateToString = result.data.DateToString;

                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };


    $scope.FillStaffTransportDatas = function ($event, $element, crudModel) {
        var model = $scope.CRUDModel.ViewModel;
        var gridModel = crudModel;

        if (model.Routes == null || model.Routes.Key == 0 || model.Routes.Key == "" || model.Routes.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select route!");
            $scope.CRUDModel.ViewModel.StudentList[0].Student = null;
            return false;
        }

        var staffDat = gridModel.find(x => x.StaffID == null || x.StaffID == undefined || x.Staff.Key != x.StaffID);

        var staffID = staffDat.Staff.Key;

        if (staffID == undefined || staffID == null || staffID == "") {
            $().showMessage($scope, $timeout, true, "Please select Staff !");
            hideOverlay();
            return false
        }

        var type = "both";

        showOverlay();
        var url = "Schools/School/GetStaffTransportDetailsByStaffID?staffID=" + staffID + "&IsRouteType=" + type;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                staffDat.StaffRouteStopMapID = result.data.StaffRouteStopMapID;
                staffDat.StaffID = result.data.StaffID;
                staffDat.OldDropStop = result.data.DropStop;
                staffDat.OldPickUpStop = result.data.PickupStop;
                staffDat.DateFromString = result.data.DateFromString;
                staffDat.DateToString = result.data.DateToString;

                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };

    $scope.RouteGroupChanges = function ($event, $element, viewModel) {

        if (!viewModel.RouteGroupID) {
            return false;
        }

        showOverlay();
        var model = viewModel;

        var url = "Schools/School/GetRoutesByRouteGroupID?routeGroupID=" + model.RouteGroupID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Route = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.ToRouteGroupChanges = function ($event, $element, viewModel) {

        if (!viewModel.ToRouteGroupID) {
            return false;
        }

        showOverlay();
        var model = viewModel;

        $http({
            method: 'Get',
            url: "Schools/School/GetPickupStopMapsByRouteGroupID?routeGroupID=" + model.ToRouteGroupID,
        }).then(function (result) {
            $scope.LookUps.PickupStopMap = result.data;
            hideOverlay();
        }, function () {
            hideOverlay();
        });

        $http({
            method: 'Get',
            url: "Schools/School/GetDropStopMapsByRouteGroupID?routeGroupID=" + model.ToRouteGroupID,
        }).then(function (result) {
            $scope.LookUps.DropStopMap = result.data;
            hideOverlay();
        }, function () {
            hideOverlay();
        });

    };


    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);