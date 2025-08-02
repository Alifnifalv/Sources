app.controller("FeeDueCancellationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.ParentDetails = null;


    $scope.FillDueCancellationDetails = function (row) {
        var windowName = 'FeeDueCancellation';
        var viewName = 'Cancelled Fee Dues' ;

        if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + row.FeeDueCancellationIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
            });
    };

    $scope.GetFeeDueDetails = function (model) {

        if (!model.Student.Key) {
            return false;
        };

        showOverlay();

        var url = "Schools/School/GetFeeDueForDueCancellation?studentID=" + model.Student.Key + "&academicYearID=" + model.AcademicYear;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (!result.data.IsError) {
                    model.FeeDueDetails = result.data;
                }
                hideOverlay();

            }, function () {
                hideOverlay();
            });

    };

    $scope.StudentChanges = function (model) {

        if (!model.Student.Key) {
            return false;
        };

        showOverlay();

        //var url = "Schools/School/GetGuardianDetails?studentID=" + model.Student.Key;
        //$http({ method: 'Get', url: url })
        //    .then(function (result) {
        //        if (!result.data.IsError) {
        //            $scope.ParentDetails = result.data.Response;
        //        }
        //        hideOverlay();

        //    }, function () {
        //        hideOverlay();
        //    });

    };

    $scope.StaffChanges = function ($event, $element, viewvModel) {

        var model = viewvModel;
        var url = "Schools/School/GetStaffDetailsByStudentID?staffID=" + model.Employee.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Students = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.ParentChanges = function ($event, $element, viewvModel) {

        var model = viewvModel;
        model.student = [];
        $scope.LookUps.Students = [];
        var url = "Schools/School/GetStudentDetailsByParent?parentID=" + model.Parent.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Students = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.GetFeeDueMonthlySplits = function (gridModel) {
        showOverlay();
        var model = gridModel;
        model.MonthSplitList = [];
        var feeperiod = model.FeePeriod?.Key;
        var url = "Schools/School/GetFeeDueMonthlySplits?studentFeeDueID=" + model.InvoiceNo.Key + "&feeMasterID=" + model.FeeMaster.Key + "&feePeriodID=" + feeperiod;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                gridModel.FeeDueFeeTypeMapsID = result.data.FeeDueFeeTypeMapsID;
                gridModel.FeeDueMonthlySplitID = result.data.FeeDueMonthlySplitID;
                //$scope.LookUps.Months = result.data.MonthList;
                //$scope.LookUps.Years = result.data.YearList;
                gridModel.MonthSplitList = result.data.MonthSplitList;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.GetYear = function ($event, $element, gridModel) {
        if (gridModel.MonthSplitList.length > 0) {
            var data = gridModel.MonthSplitList.find(x => x.MonthID == gridModel.Months.Key);
            gridModel.Years = $scope.LookUps.Years.find(x => x.Value == data.Year);
            gridModel.FeeDueMonthlySplitID = data.FeeDueMonthlySplitID;
            gridModel.FeeDueFeeTypeMapsID = data.FeeDueFeeTypeMapsID;
        }       
    };


    $scope.GroupChanges = function ($event, $element, applicationModel) {

        var model = applicationModel;

        if (model.StudentGroup.Key == 2 || model.StudentGroup.Key == 3) {
            var url = "Home/GetStaffDetailsByStudentID?studentID=" + model.Student.Key + "&academicYearID= " + model.AcademicYear.Key;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    model.Employee = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        if (model.StudentGroup.Key == 1) {
            var url = "Home/GetParentDetailsByStudentID?studentID=" + model.Student.Key;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.Parent = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }

    };
    
    $scope.ConcessionPercentageChanges = function (gridModel) {
        var model = gridModel;
        var sum = 0;
        if (!isNaN(model.ConcessionPercentage)) {
           
            model.ConcessionAmount = (model.Amount * (model.ConcessionPercentage / 100)).toFixed(3);
            model.NetToPay = model.Amount - (model.ConcessionAmount ?? 0);
        }
    };

    $scope.ConcessionAmountChanges = function (gridModel) {
        var sum = 0;
        var model = gridModel;
        if (!isNaN(gridModel.ConcessionAmount)) {

            model.ConcessionPercentage = Math.round((parseFloat(model.ConcessionAmount) / model.Amount) * 100 * 100) / 100;
            model.NetToPay = model.Amount - (model.ConcessionAmount ?? 0);
        }

    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);