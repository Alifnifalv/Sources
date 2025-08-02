app.controller("RouteController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));
    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.AllPeriods = [];
    $scope.selecetedPrd = [];
    $scope.clickedLat = 0;
    $scope.clickedLng = 0;
    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }


    //$scope.LoadFeePeriods = function (model) {
    //    if ($scope.CRUDModel.ViewModel.IsFilterFeePeriod == 0) {
    //        if (model.AcademicYear != undefined && model.AcademicYear.Key != undefined) {
    //            $scope.GetFeePeriods(model.AcademicYear.Key);
    //            $scope.FillDataBasedOnSelectedAcademicYear(model);
    //        }
    //        else {
    //            $().showMessage($scope, $timeout, true, 'Please select route group');
    //        }
    //    }
    //};

    $scope.AcademicYearChanges = function (model) {
        $scope.CRUDModel.ViewModel.IsFilterFeePeriod = 1;
        $scope.CRUDModel.ViewModel.FeePeriod = [];
        $scope.CRUDModel.ViewModel.RouteMonthlySplit = null;
        $scope.GetFeePeriods(model.AcademicYear.Key);
        $scope.FillDataBasedOnSelectedAcademicYear(model);
    };

    $scope.GetFeePeriods = function (academicYearID) {
        showOverlay();
        var url = "Schools/School/GetTransportFeePeriod?academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.FeePeriod = result.data;
                $scope.CRUDModel.ViewModel.IsFilterFeePeriod = 1;
                //if (result != null & result.data != null) {
                //    $scope.GetFeePeriodMonthlySplit(result.data);
                //}
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.GetFeePeriodMonthlySplit = function (resultAcademic) {
        var feeprdList = resultAcademic.map(function (item) { return item.Key; });
        var url = "Schools/School/GetFeePeriodMonthlySplit";
        $http({ method: 'Post', url: url, data: feeprdList })
            .then(function (resultdata) {
                $scope.AllPeriods = resultdata;
                var prd = $scope.AllPeriods.data;
                if (prd != undefined) {
                    prd.forEach(y => {
                        if (!$scope.CRUDModel.ViewModel.FeePeriod.find(z => z.FeePeriodID == y.FeePeriodID)) {
                            $scope.selecetedPrd = $scope.selecetedPrd.filter(function (item) {
                                return item["FeePeriodID"] != y.FeePeriodID
                            })
                        }
                    });

                    prd.forEach(x => {

                        $scope.CRUDModel.ViewModel.FeePeriod.forEach(y => {
                            if (x.FeePeriodID.toString() == y.Key && !$scope.selecetedPrd.find(z => z.MonthID == x.MonthID && z.YearID == x.YearID)) {
                                $scope.selecetedPrd.push({
                                    MonthID: x.MonthID,
                                    Year: x.Year,
                                    FeePeriodID: x.FeePeriodID,
                                    IsRowSelected: true,
                                    MonthName: x.MonthName,
                                    StudentRouteMonthlySplitIID: 0,
                                    IsCollected: 0,
                                    StudentRouteStopMapID: 0,
                                    Amount: 0

                                });
                            }

                        });

                    });
                }

                $scope.CRUDModel.ViewModel.RouteMonthlySplit = $scope.selecetedPrd;
            }, function () {

            });
    };
    $scope.PeriodChanges = function ($event, $element, gridModel) {
        //showOverlay();
        var model = gridModel;
        var periodList = gridModel.FeePeriod;
        var academicYearId = gridModel.AcademicYear?.Key;
        if (academicYearId == null || academicYearId == 0) {
            $().showMessage($scope, $timeout, true, "Please select route group");
            gridModel.FeePeriod = null;
            $scope.AllPeriods = null;
            hideOverlay();
            return false;
        }

        $scope.GetFeePeriodMonthlySplit(periodList);

        if ($scope.AllPeriods == undefined || $scope.AllPeriods.data.length == 0) {
            $scope.GetFeePeriods(gridModel.AcademicYear.Key);
        }
        else {
            var prd = $scope.AllPeriods.data;
            if (prd != undefined) {

                if ($scope.selecetedPrd != undefined) {
                    $scope.selecetedPrd.forEach(y => {
                        if (!gridModel.FeePeriod.find(z => z.FeePeriodID == y.FeePeriodID)) {
                            $scope.selecetedPrd = $scope.selecetedPrd.filter(function (item) {
                                return item["FeePeriodID"] != y.FeePeriodID
                            })
                        }
                    });
                }
                prd.forEach(x => {

                    gridModel.FeePeriod.forEach(y => {
                        if (x.FeePeriodID.toString() == y.Key && !$scope.selecetedPrd.find(z => z.MonthID == x.MonthID && z.YearID == x.YearID)) {
                            $scope.selecetedPrd.push({
                                MonthID: x.MonthID,
                                Year: x.Year,
                                FeePeriodID: x.FeePeriodID,
                                IsRowSelected: true,
                                MonthName: x.MonthName,
                                StudentRouteMonthlySplitIID: 0,
                                IsCollected: 0,
                                StudentRouteStopMapID: 0,
                                Amount: 0

                            });
                        }

                    });

                });
            }

            gridModel.RouteMonthlySplit = $scope.selecetedPrd;
        }
    }
    $scope.Getdemoval = function () {
        showOverlay();
        var dateFrom = "";

        if ($scope.CRUDModel.ViewModel.DateFromString != null
            && $scope.CRUDModel.ViewModel.DateFromString != "") {
            dateFrom = $scope.CRUDModel.ViewModel.DateFromString;
        }

        var dateTo = "";

        if ($scope.CRUDModel.ViewModel.DateToString != null
            && $scope.CRUDModel.ViewModel.DateToString != "") {
            dateTo = $scope.CRUDModel.ViewModel.DateToString;
        }

        if (dateFrom != "") {
            $.getJSON("school/checkAcademicYearValidation",
                { fDate: dateFrom, toDate: dateTo }, function (result) {
                    if (result.status == false) {
                        alert(result.message);
                        if (result.Filed == "dateFrom") {
                            $scope.CRUDModel.ViewModel.DateToString = null;
                        }
                        if (result.Filed == "dateTo") {
                            $scope.CRUDModel.ViewModel.DateFromString = null;
                        }
                        $("#append-panel-body", $($scope.CrudWindowContainer)).empty();
                        hideOverlay();
                        return false;
                    }
                    else {
                        var PickupID = 0;

                        if ($scope.CRUDModel.ViewModel.PickupStopMap.Key != null) {
                            PickupID = $scope.CRUDModel.ViewModel.PickupStopMap.Key;
                        }

                        var DropID = 0;

                        if ($scope.CRUDModel.ViewModel.DropStopMap.Key != null) {
                            DropID = $scope.CRUDModel.ViewModel.DropStopMap.Key;
                        }

                        var appendValue = "";

                        if (dateTo == "") {
                            dateTo = "31/03/2022";
                        }

                        var url = "Schools/School/GetAcademicYearMonthlyWise?fDate=" + dateFrom + "&&toDate=" + dateTo + "&&PickupID=" + PickupID + "&&DropID=" + DropID;
                        $http({ method: 'Get', url: url })
                            .then(function (result) {
                                var resultCount = result.data.length;
                                $("#append-panel-body", $($scope.CrudWindowContainer)).empty();
                                for (var i = 0; i < resultCount; i++) {
                                    var resultArray = result.data[i].split('~');
                                    appendValue = appendValue +
                                        "<div class='TransportSplitContanier'> <div class='amount'>" +
                                        (resultArray[0] ? resultArray[0] : '').toString() + "</div> <div class='MonthandYear currSign'>" +
                                        (resultArray[1] ? resultArray[1] : '0') + "</div> </div>"
                                }
                                $("#append-panel-body", $($scope.CrudWindowContainer)).append(appendValue);
                                hideOverlay();
                            }, function () {
                                hideOverlay();
                            });
                    }
                })
        }
        return "1";
    }

    $scope.PickupStopChanges = function ($event, $element, dueModel) {
        if (dueModel.PickUpRoute == undefined || dueModel.PickUpRoute == null || dueModel.PickUpRoute == "" || dueModel.PickUpRoute.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select route to school !");

            dueModel.PickUpRoute = "";
            dueModel.PickupStopMap = "";
            return false;
        }
        showOverlay();
        dueModel.PickupSeatAvailability = {};
        var model = dueModel;
        var url = "Schools/School/GetPickUpBusSeatAvailabilty?RouteStopMapId=" + model.PickupStopMap.Key + "&academicYearID=" + model.AcademicYear.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.PickupSeatAvailability.MaximumSeatCapacity = result.data.PickupSeatAvailability.MaximumSeatCapacity;
                $scope.CRUDModel.ViewModel.PickupSeatAvailability.AllowSeatCapacity = result.data.PickupSeatAvailability.AllowSeatCapacity;
                $scope.CRUDModel.ViewModel.PickupSeatAvailability.SeatOccupied = result.data.PickupSeatAvailability.SeatOccupied;
                $scope.CRUDModel.ViewModel.PickupSeatAvailability.SeatAvailability = result.data.PickupSeatAvailability.SeatAvailability;
                $scope.CRUDModel.ViewModel.PickupBusNumber = result.data.PickupBusNumber;
                /*$scope.Getdemoval();*/
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.DropStopChanges = function ($event, $element, dueModel) {
        if (dueModel.DropRoute == undefined || dueModel.DropRoute == null || dueModel.DropRoute == "" || dueModel.DropRoute.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select route to home !");
            dueModel.DropRoute = "";
            dueModel.DropStopMap = "";
            return false;
        }
        showOverlay();
        dueModel.DropSeatAvailability = {};
        var model = dueModel;
        var url = "Schools/School/GetDropBusSeatAvailabilty?RouteStopMapId=" + model.DropStopMap.Key + "&academicYearID=" + model.AcademicYear.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.DropSeatAvailability.MaximumSeatCapacity = result.data.DropSeatAvailability.MaximumSeatCapacity;
                $scope.CRUDModel.ViewModel.DropSeatAvailability.AllowSeatCapacity = result.data.DropSeatAvailability.AllowSeatCapacity;
                $scope.CRUDModel.ViewModel.DropSeatAvailability.SeatOccupied = result.data.DropSeatAvailability.SeatOccupied;
                $scope.CRUDModel.ViewModel.DropSeatAvailability.SeatAvailability = result.data.DropSeatAvailability.SeatAvailability;
                $scope.CRUDModel.ViewModel.DropBusNumber = result.data.DropBusNumber;
                /*$scope.Getdemoval();*/
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.StaffPickupStopChanges = function ($event, $element, dueModel) {
        if (dueModel.AcademicYear == undefined || dueModel.AcademicYear == null || dueModel.AcademicYear == "") {
            $().showMessage($scope, $timeout, true, "Please select Academic Year!");
            dueModel.PickUpRoute = "";
            dueModel.PickupStopMap = "";
            return false;
        }
        showOverlay();
        dueModel.PickupSeatAvailability = {};
        var model = dueModel;
        var url = "Schools/School/GetPickUpBusSeatStaffAvailabilty?RouteStopMapId=" + model.PickupStopMap.Key + "&academicYearID=" + model.AcademicYear.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.PickupSeatAvailability.MaximumSeatCapacity = result.data.PickupSeatAvailability.MaximumSeatCapacity;
                $scope.CRUDModel.ViewModel.PickupSeatAvailability.AllowSeatCapacity = result.data.PickupSeatAvailability.AllowSeatCapacity;
                $scope.CRUDModel.ViewModel.PickupSeatAvailability.SeatOccupied = result.data.PickupSeatAvailability.SeatOccupied;
                $scope.CRUDModel.ViewModel.PickupSeatAvailability.SeatAvailability = result.data.PickupSeatAvailability.SeatAvailability;
                $scope.CRUDModel.ViewModel.PickupBusNumber = result.data.PickupBusNumber;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.StaffDropStopChanges = function ($event, $element, dueModel) {
        if (dueModel.AcademicYear == undefined || dueModel.AcademicYear == null || dueModel.AcademicYear == "") {
            $().showMessage($scope, $timeout, true, "Please select Academic Year!");
            dueModel.DropRoute = "";
            dueModel.DropStopMap = "";
            return false;
        }
        showOverlay();
        dueModel.DropSeatAvailability = {};
        var model = dueModel;
        var url = "Schools/School/GetDropBusSeatStaffAvailabilty?RouteStopMapId=" + model.DropStopMap.Key + "&academicYearID=" + model.AcademicYear.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.CRUDModel.ViewModel.DropSeatAvailability.MaximumSeatCapacity = result.data.DropSeatAvailability.MaximumSeatCapacity;
                $scope.CRUDModel.ViewModel.DropSeatAvailability.AllowSeatCapacity = result.data.DropSeatAvailability.AllowSeatCapacity;
                $scope.CRUDModel.ViewModel.DropSeatAvailability.SeatOccupied = result.data.DropSeatAvailability.SeatOccupied;
                $scope.CRUDModel.ViewModel.DropSeatAvailability.SeatAvailability = result.data.DropSeatAvailability.SeatAvailability;
                $scope.CRUDModel.ViewModel.DropBusNumber = result.data.DropBusNumber;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.AddBelow = function (selectedindex) {
        showOverlay();
        var fareOneWay;

        $.each($scope.CRUDModel.ViewModel.Stop, function (index, objModel) {
            //$scope.$apply(function () {
            if (selectedindex == index) {
                fareOneWay = objModel.RouteFareOneWay;
            }
            else if (selectedindex < index) {
                objModel.RouteFareOneWay = fareOneWay;
            }
            //});
            hideOverlay();
        });
    };


    $scope.SelectLocation = function (index) {
        var model = $scope.CRUDModel.ViewModel.Stop;
        model[index].Latitude = $scope.clickedLat;
        model[index].Longitude = $scope.clickedLng;
    }

    $scope.EnableRouteType = function (model) {
        $scope.CRUDModel.ViewModel.DropStopMap = null;
        $scope.CRUDModel.ViewModel.PickupStopMap = null;
        $scope.CRUDModel.ViewModel.DropRoute = null;
        $scope.CRUDModel.ViewModel.PickUpRoute = null;
        $("#append-panel-body").empty();

        if ($("#IsOneWay").prop("checked") == true) {
            $("#RouteType").prop('disabled', false);
        }
        else {
            $("#RouteType").prop('disabled', true);
            $scope.CRUDModel.ViewModel.RouteType = null;
        }
    }

    $scope.RouteTypeChanges = function ($event, $element, model) {
        if (model.RouteType.Key == "1") {
            $("#PickupStopMap").prop('disabled', false);
            $("#DropStopMap").prop('disabled', true);
            $("#PickUpRoute").prop('disabled', false);
            $("#DropRoute").prop('disabled', true);

            $scope.CRUDModel.ViewModel.DropStopMap = "";
            $scope.CRUDModel.ViewModel.DropRoute = "";
            $scope.CRUDModel.ViewModel.DropSeatAvailability.MaximumSeatCapacity = "";
            $scope.CRUDModel.ViewModel.DropSeatAvailability.AllowSeatCapacity = "";
            $scope.CRUDModel.ViewModel.DropSeatAvailability.SeatOccupied = "";
            $scope.CRUDModel.ViewModel.DropSeatAvailability.SeatAvailability = "";
            $scope.CRUDModel.ViewModel.DropBusNumber = "";
            $scope.CRUDModel.ViewModel.PickupStopMap = "";
            $scope.CRUDModel.ViewModel.PickUpRoute = "";
        }
        else {
            $("#PickupStopMap").prop('disabled', true);
            $("#DropStopMap").prop('disabled', false);
            $("#PickUpRoute").prop('disabled', true);
            $("#DropRoute").prop('disabled', false);

            $scope.CRUDModel.ViewModel.PickupSeatAvailability.MaximumSeatCapacity = "";
            $scope.CRUDModel.ViewModel.PickupSeatAvailability.AllowSeatCapacity = "";
            $scope.CRUDModel.ViewModel.PickupSeatAvailability.SeatOccupied = "";
            $scope.CRUDModel.ViewModel.PickupSeatAvailability.SeatAvailability = "";
            $scope.CRUDModel.ViewModel.PickupBusNumber = "";
            $scope.CRUDModel.ViewModel.PickupStopMap = "";
            $scope.CRUDModel.ViewModel.PickUpRoute = "";

        }

    };

    $scope.ToSchoolRouteChanges = function ($event, $element, applicationModel) {
        showOverlay();
        var model = applicationModel;

        if (model.RouteGroup == undefined || model.RouteGroup == "" || model.RouteGroup == 0) {
            $().showMessage($scope, $timeout, true, "Please select route group !");
            model.PickUpRoute = null;
            hideOverlay();
            return false;
        }

        model.PickupStopMap = null;
        var url = "Schools/School/GetRouteStopsByRoute?routeID=" + model.PickUpRoute.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.PickupStopMap = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.ToHomeRouteChanges = function ($event, $element, applicationModel) {
        showOverlay();
        var model = applicationModel;

        if (model.RouteGroup == undefined || model.RouteGroup == "" || model.RouteGroup == 0) {
            $().showMessage($scope, $timeout, true, "Please select route group !");
            model.DropRoute = null;
            hideOverlay();
            return false;
        }

        model.DropStopMap = null;
        var url = "Schools/School/GetRouteStopsByRoute?routeID=" + model.DropRoute.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
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

    $scope.CreateStudentRoute = function (TransportApplctnStudentMapIID) {
        var windowName = 'TransportApplicationStudentRouteStopMap';
        var viewName = 'Create Student Transport';

        if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + TransportApplctnStudentMapIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
            });
    };

    $scope.FillDataBasedOnSelectedAcademicYear = function (model) {
        var academicYearID = model.AcademicYear?.Key;
        showOverlay();
        var url = "Schools/School/GetAcademicYearDataByAcademicYearID?academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                model.DateToString = result.data.EndDateString;

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    }

    $scope.RouteGroupChanges = function ($event, $element, viewModel) {

        if (!viewModel.RouteGroup) {
            return false;
        }

        showOverlay();
        var model = viewModel;
        model.PickUpRoute = {
            key: null,
            Value: null
        };
        model.DropRoute = {
            key: null,
            Value: null
        };

        var url = "Schools/School/GetRoutesByRouteGroupID?routeGroupID=" + model.RouteGroup;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Route = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

        $scope.GetAcademicYearIDByGroupID(model.RouteGroup);
    };

    $scope.GetAcademicYearIDByGroupID = function (routeGroupID) {
        showOverlay();

        var url = "Schools/School/GetAcademicYearDataByGroupID?routeGroupID=" + routeGroupID;

        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.CRUDModel.ViewModel.AcademicYear.Key = result.data.AcademicYearID;
                $scope.CRUDModel.ViewModel.AcademicYear.Value = result.data.Description + " (" + result.data.AcademicYearCode + ")";

                hideOverlay();
                $scope.AcademicYearChanges($scope.CRUDModel.ViewModel);
            }, function () {
                hideOverlay();
            });
    };

}]);