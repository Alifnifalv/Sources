app.controller("StudentFeeConcessionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.ParentDetails = null;

    $scope.GetFeeDueDetails = function (model) {

        if (!model.AcademicYear) {
            $().showMessage($scope, $timeout, true, "Please select a academic year");
            return false;
        };
        if (!model.Student.Key) {
            $().showMessage($scope, $timeout, true, "Please select a student");
            return false;
        };

        showOverlay();

        var url = "Schools/School/GetFeeDueForConcession?studentID=" + model.Student.Key + "&academicYearID=" + model.AcademicYear;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (!result.data.IsError) {
                    model.StudentConcessionDetail = result.data;
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
        model.student = {};
        model.Parent = {};
        model.Student = null;
        $scope.LookUps.Students = [];
        var url = "Schools/School/GetStudentDetailsByStaff?staffID=" + model.Staff.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Students = result.data;
                //if (result.data.length == 1) {
                //    model.Student.Key = result.data[0].Key;
                //    model.Student.Value = result.data[0].Value;
                //}
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.ParentChanges = function ($event, $element, viewvModel) {

        var model = viewvModel;
        model.student = {};
        model.Staff = {};
        model.Student = null;
        $scope.LookUps.Students = [];
        var url = "Schools/School/GetStudentDetailsByParent?parentID=" + model.Parent.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Students = result.data;
                //if (result.data.length == 1) {
                //    model.Student.Key = result.data[0].Key;
                //    model.Student.Value = result.data[0].Value;
                //}
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
            model.NetToPay = (model.Amount - (model.ConcessionAmount ?? 0)).toFixed(3);
        }
    };

    $scope.ConcessionAmountChanges = function (gridModel) {
        var sum = 0;
        var model = gridModel;
        if (!isNaN(gridModel.ConcessionAmount)) {
            
            model.ConcessionPercentage = Math.round((parseFloat(gridModel.ConcessionAmount) / model.Amount) * 100 * 100) / 100;
            model.NetToPay = (model.Amount - (model.ConcessionAmount ?? 0)).toFixed(3);
            model.ConcessionAmount = parseFloat(gridModel.ConcessionAmount).toFixed(3);
        }

    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);