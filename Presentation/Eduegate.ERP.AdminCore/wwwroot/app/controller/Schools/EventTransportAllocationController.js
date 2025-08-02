app.controller("EventTransportAllocationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));
    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.FillPickUpData = function ($event, $element, viewModel) {

        if (viewModel.IsPickUp == true) {
            $scope.CRUDModel.ViewModel.IsDrop = false;
            $scope.CRUDModel.ViewModel.IsRouteType = "Pick"
            $scope.CRUDModel.ViewModel.SelectedDetails = "Allocation for PickUp";
        }
        else {
            $scope.CRUDModel.ViewModel.IsDrop = true;
            $scope.CRUDModel.ViewModel.IsRouteType = "Drop"
            $scope.CRUDModel.ViewModel.SelectedDetails = "Allocation for Drop";
        }
    };

    $scope.FillDropData = function ($event, $element, viewModel) {

        if (viewModel.IsDrop == true) {
            $scope.CRUDModel.ViewModel.IsPickUp = false;
            $scope.CRUDModel.ViewModel.IsRouteType = "Drop"
            $scope.CRUDModel.ViewModel.SelectedDetails = "Allocation for Drop";
        }
        else {
            $scope.CRUDModel.ViewModel.IsPickUp = true;
            $scope.CRUDModel.ViewModel.IsRouteType = "Pick"
            $scope.CRUDModel.ViewModel.SelectedDetails = "Allocation for PickUp";
        }
    };

    $scope.FillStudentTransportDatas = function ($event, $element, crudModel) {
        showOverlay();
        var checkboxPick = $scope.CRUDModel.ViewModel.IsPickUp;
        var checkboxDrop = $scope.CRUDModel.ViewModel.IsDrop;
        var model = crudModel;
        if (checkboxPick == false && checkboxDrop == false || checkboxPick == undefined && checkboxDrop == undefined) {
            $().showMessage($scope, $timeout, true, "Please select pickUp/Drop checkbox !");
            hideOverlay();
            $scope.CRUDModel.ViewModel.StudentList[0].Student = null;
            return false
        }

        var stud = model.find(x => x.StudentID == null || x.StudentID == undefined || x.Student.Key != x.StudentID);

        var studentID = stud.Student.Key;

        if (studentID == undefined || studentID == null || studentID == "") {
            $().showMessage($scope, $timeout, true, "Please select Student !");
            hideOverlay();
            return false
        }

        var type = $scope.CRUDModel.ViewModel.IsRouteType;

        showOverlay();
        var url = "Schools/School/GetStudentTransportDetailsByStudentID?studentID=" + studentID + "&IsRouteType=" + type;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                stud.StudentRouteStopMapID = result.data.StudentRouteStopMapID;
                stud.StudentID = result.data.StudentID;
                stud.ClassSection = result.data.ClassSection;
                stud.DropStop = result.data.DropStop;
                stud.PickupStop = result.data.PickupStop;
                stud.PickUpRoute = result.data.PickUpRoute;
                stud.DropRoute = result.data.DropRoute;

                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };

    $scope.FillStaffTransportDatas = function ($event, $element, crudModel) {
        showOverlay();
        var checkboxPick = $scope.CRUDModel.ViewModel.IsPickUp;
        var checkboxDrop = $scope.CRUDModel.ViewModel.IsDrop;
        var model = crudModel;
        if (checkboxPick == false && checkboxDrop == false || checkboxPick == undefined && checkboxDrop == undefined) {
            $().showMessage($scope, $timeout, true, "Please select pickUp/Drop checkbox !");
            hideOverlay();
            $scope.CRUDModel.ViewModel.StaffList[0].Staff = null;
            return false
        }

        var staffDat = model.find(x => x.StaffID == null || x.StaffID == undefined || x.Staff.Key != x.StaffID);

        var staffID = staffDat.Staff.Key;

        if (staffID == undefined || staffID == null || staffID == "") {
            $().showMessage($scope, $timeout, true, "Please select Staff !");
            hideOverlay();
            return false
        }

        var type = $scope.CRUDModel.ViewModel.IsRouteType;

        showOverlay();
        var url = "Schools/School/GetStaffTransportDetailsByStaffID?staffID=" + staffID + "&IsRouteType=" + type;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                staffDat.StaffRouteStopMapID = result.data.StaffRouteStopMapID;
                staffDat.StaffID = result.data.StaffID;
                staffDat.Designation = result.data.Designation;
                staffDat.DropStop = result.data.DropStop;
                staffDat.PickupStop = result.data.PickupStop;
                staffDat.PickUpRoute = result.data.PickUpRoute;
                staffDat.DropRoute = result.data.DropRoute;

                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };

    $scope.FillData = function ($event, $element, crudModel) {

        showOverlay();

        if (crudModel.EventTransportAllocationIID != 0) {
            hideOverlay();
            return false;
        }

        if (crudModel.ListStaffs == false && crudModel.ListStudents == false) {
            $().showMessage($scope, $timeout, true, "Please select List Students/List Staff 'checkbox' !");
            hideOverlay();
            return false;
        }

        if (crudModel.IsPickUp == false && crudModel.IsDrop == false) {
            $().showMessage($scope, $timeout, true, "Please select pickUp/Drop checkbox !");
            hideOverlay();
            return false;
        }

        if (crudModel.Route.length == 0 && crudModel.Class.length == 0 && crudModel.ListStudents == true) {
            $().showMessage($scope, $timeout, true, "Please select From Route/Class !");
            hideOverlay();
            return false;
        }

        var RouteIDs = crudModel.Route.map(function (item) {
            return { Key: item.Key, Value: item.Value };
        });

        var ClassIDs = crudModel.Class.map(function (item) {
            return { Key: item.Key, Value: item.Value };
        });

        var eventDto = {
            Route: RouteIDs,
            Class: ClassIDs,
            ListStudents: crudModel.ListStudents,
            ListStaffs: crudModel.ListStaffs,
            IsRouteType: crudModel.IsRouteType
        };

        $scope.GetTransportData(eventDto);
    };

    $scope.GetTransportData = function (eventDto) {
        var loadUrl = "/Schools/School/GetStudentandStaffsForEvent";

        $.ajax({
            type: "POST",
            url: loadUrl,
            data: JSON.stringify(eventDto),
            contentType: "application/json",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        hideOverlay();
                        return false;
                    } else {
                        hideOverlay();
                        $scope.CRUDModel.ViewModel.StudentList = result.filter(x => x.StudentID != null);
                        $scope.CRUDModel.ViewModel.StaffList = result.filter(x => x.StaffID != null);

                        if ($scope.CRUDModel.ViewModel.StudentList.length <= 0 && $scope.CRUDModel.ViewModel.StaffList.length <= 0) {
                            $scope.CRUDModel.ViewModel.SelectedDetails = "No datas found for " + $scope.CRUDModel.ViewModel.IsRouteType;
                        }
                        else {
                            $scope.CRUDModel.ViewModel.SelectedDetails = "List of " + $scope.CRUDModel.ViewModel.IsRouteType;
                        }
                    }
                });
                hideOverlay();
            }
        });
    }

}]);