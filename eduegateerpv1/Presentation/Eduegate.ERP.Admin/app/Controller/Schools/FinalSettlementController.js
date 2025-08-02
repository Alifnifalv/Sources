app.controller("FinalSettlementController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("FinalSettlementController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.TotalFeeAmount = 0;
    $scope.TotalCollectedAmount = 0;
    $scope.FeeData = [];
    $scope.FineData = [];

    $scope.StudentChanges = function (gridModel) {
        showOverlay();
        var model = gridModel;

        var studentId = model.Student?.Key;
        if (studentId == null || studentId == 0) {
            $().showMessage($scope, $timeout, true, "Please Select a Student");
            $scope.FeeTypes = null;

            hideOverlay();
            return false;
        }
        $scope.FeeTypes = null;

        var url = "Schools/School/GetStudentDetailFromStudentID?StudentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result == null || result.data.length == 0) {
                    $().showMessage($scope, $timeout, true, "There is no details available for this student");
                    $scope.FeeTypes = null;
                    hideOverlay();
                    return false;
                }
                $scope.CRUDModel.ViewModel.AdmissionNumber = result.data.AdmissionNumber;
                $scope.CRUDModel.ViewModel.Class = result.data.ClassName;
                $scope.CRUDModel.ViewModel.ClassID = result.data.ClassID;
                $scope.CRUDModel.ViewModel.Section = result.data.SectionName;
                $scope.CRUDModel.ViewModel.SectionID = result.data.SectionID;               
                $scope.CRUDModel.ViewModel.AcademicYearID = result.data.AcademicYearID;
                $scope.CRUDModel.ViewModel.Academic = result.data.AcademicYear;
                $scope.CRUDModel.ViewModel.SchoolID = result.data.SchoolID;
                var url = "Schools/School/FillFeeDueDataForSettlement?studentId=" + model.Student.Key + "&AcademicId=" + $scope.CRUDModel.ViewModel.AcademicYearID;
                $http({ method: 'Get', url: url })
                    .then(function (result) {
                        if (result == null || result.data.length == 0) {
                            $().showMessage($scope, $timeout, true, "There is no fee details available for this student");
                            $scope.FeeTypes = null;
                            hideOverlay();
                            return false;
                        }
                        $scope.CRUDModel.ViewModel.FeeTypes = result.data;

                        hideOverlay();
                    }, function () {
                        hideOverlay();
                    });
            }, function () {
                  
            });

    };

    $scope.LoadPaymentMode = function (rowIndex) {
        $scope.LookUps['PaymentModesExclude'] = [];
        var fitlers = new JSLINQ($scope.LookUps['PaymentModeList']).Where(function (PaymentMode) {
            var exists = new JSLINQ($scope.CRUDModel.ViewModel.FeePaymentMap)
                .Where(function (item) {
                    if (item == undefined || item.PaymentMode == null) return false;
                    return item.PaymentMode.Key == PaymentMode.Key;
                });
            if (exists.items.length == 0) {
                $scope.LookUps['PaymentModesExclude'].push(PaymentMode);
            }
        });
    }

    $scope.PaymodeChanges = function ($event, $index, gridModel) {
        showOverlay();
        var model = gridModel;
        if (gridModel.PaymentMode.Value != 'Cheque') {

            gridModel.IsTDateDisabled = true;
            gridModel.ReferenceNo = "-";
        }
        else {
            gridModel.IsTDateDisabled = false;
            gridModel.ReferenceNo = "";
        }
        gridModel.Amount = $scope.TotalFeeAmount - $scope.TotalCollectedAmount;
        hideOverlay();
    };

    $scope.GetTotalCollectedAmount = function (data) {
        if (typeof (data) === 'undefined') {
            return 0;
        }
        var sum = 0;
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].Amount)
                sum += parseFloat(data[i].Amount);

        }
        $scope.TotalCollectedAmount = sum;
        return sum;
    };

    $scope.GetTotalFeeAmount = function (data, fineData) {
        if (typeof (data) === 'undefined' && typeof (fineData) === 'undefined') {
            return 0;
        }

        var sum = 0;
        if (data != null) {
            for (var i = data.length - 1; i >= 0; i--) {
                if (data[i].Amount && data[i].IsRowSelected == true)
                    sum += parseFloat(data[i].Amount);

            }
        }
        if (fineData != null) {
            for (var i = fineData.length - 1; i >= 0; i--) {
                if (fineData[i].Amount)
                    sum += parseFloat(fineData[i].Amount);
            }
        }
        $scope.TotalFeeAmount = sum;
        if ($scope.CRUDModel.ViewModel.FeePaymentMap != undefined && $scope.CRUDModel.ViewModel.FeePaymentMap.length == 1)
            $scope.CRUDModel.ViewModel.FeePaymentMap[0].Amount = parseFloat($scope.TotalFeeAmount ?? 0);
        return sum;
    };

    $scope.UpdateAmount = function (e) {

        e.gridModel.Amount = e.gridModel.PayableAmount - e.gridModel.Refund;

    };

    function setDefaultasZero(model, type) {
        if (type == 1) {
            model.PayableAmount = 0;
        }
        if (type == 2) {
            model.Refund = 0;
        }
        model.Balance = (model.PayableAmount ?? 0) - (model.Refund ?? 0);
        return false;
    }

    $scope.ClearSelection = function (gridModel) {
        showOverlay();
        var model = gridModel;
        if (model.IsRowSelected == false) {

            for (var i = 0, l = gridModel.FeeDueMonthlyFinal.length; i < l; i++) {
                gridModel.FeeDueMonthlyFinal[i].IsRowSelected = false;
            }

        }
        else {

            for (var i = 0, l = gridModel.FeeDueMonthlyFinal.length; i < l; i++) {
                gridModel.FeeDueMonthlyFinal[i].IsRowSelected = true;
            }
        }
        hideOverlay();

    }

    $scope.SplitFeeAmount = function (gridModel, type) {
        showOverlay();
        gridModel.Amount = gridModel.PayableAmount - gridModel.Refund;

        gridModel.FeeDueMonthlyFinal.map(setDefaultasZero, type);
        if (type == 1) {
            var sumPayableAmount = gridModel.PayableAmount;

            for (var i = 0, l = gridModel.FeeDueMonthlyFinal.length; i < l; i++) {

                var amt = gridModel.FeeDueMonthlyFinal[i].Amount - (gridModel.FeeDueMonthlyFinal[i].CreditNote ?? 0) - (gridModel.FeeDueMonthlyFinal[i].PrvCollect ?? 0);

                gridModel.FeeDueMonthlyFinal[i].PayableAmount = 0;
                if (amt >= sumPayableAmount) {
                    gridModel.FeeDueMonthlyFinal[i].PayableAmount = parseFloat(sumPayableAmount);
                    gridModel.FeeDueMonthlyFinal[i].Balance = (gridModel.FeeDueMonthlyFinal[i].PayableAmount ?? 0) - (gridModel.FeeDueMonthlyFinal[i].Refund ?? 0);
                    gridModel.FeeDueMonthlyFinal[i].IsRowSelected = true;
                    break;
                }
                else {

                    gridModel.FeeDueMonthlyFinal[i].PayableAmount = amt;
                    gridModel.FeeDueMonthlyFinal[i].Balance = (gridModel.FeeDueMonthlyFinal[i].PayableAmount ?? 0) - (gridModel.FeeDueMonthlyFinal[i].Refund ?? 0);
                    gridModel.FeeDueMonthlyFinal[i].IsRowSelected = true;
                    sumPayableAmount = sumPayableAmount - amt;
                }
            }


        }
        if (type == 2) {
            var sumRefundAmount = gridModel.Refund;

            for (var i = 0, l = gridModel.FeeDueMonthlyFinal.length; i < l; i++) {

                var amt = gridModel.FeeDueMonthlyFinal[i].Amount - (gridModel.FeeDueMonthlyFinal[i].CreditNote ?? 0) - (gridModel.FeeDueMonthlyFinal[i].PrvCollect ?? 0) - (gridModel.FeeDueMonthlyFinal[i].PayableAmount ?? 0);

                gridModel.FeeDueMonthlyFinal[i].Refund = 0;
                if (amt >= sumRefundAmount) {
                    gridModel.FeeDueMonthlyFinal[i].Refund = parseFloat(sumRefundAmount);
                    gridModel.FeeDueMonthlyFinal[i].Balance = (gridModel.FeeDueMonthlyFinal[i].PayableAmount ?? 0) - (gridModel.FeeDueMonthlyFinal[i].Refund ?? 0);
                    gridModel.FeeDueMonthlyFinal[i].IsRowSelected = true;

                    break;
                }
                else {

                    gridModel.FeeDueMonthlyFinal[i].Refund = amt;
                    gridModel.FeeDueMonthlyFinal[i].Balance = (gridModel.FeeDueMonthlyFinal[i].PayableAmount ?? 0) - (gridModel.FeeDueMonthlyFinal[i].Refund ?? 0);
                    gridModel.FeeDueMonthlyFinal[i].IsRowSelected = true;
                    sumRefundAmount = sumRefundAmount - amt;
                }
            }

        }




        hideOverlay();
    };


    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }


    $scope.UpdateParentFee = function (parentrow, index, e) {
        var payableAmount = 0;
        var refund = 0;
        var balance = 0;
        e.FeeDueMonthlyFinal.Balance = e.FeeDueMonthlyFinal.PayableAmount - e.FeeDueMonthlyFinal.Refund;
        parentrow.FeeDueMonthlyFinal.forEach(element => {
            if (element.IsRowSelected == true) {
                payableAmount += Math.round(element.PayableAmount);
                refund += Math.round(element.Refund);
                balance += Math.round(element.Balance);
            }
        });
        parentrow.PayableAmount = payableAmount;
        parentrow.Refund = refund;
        parentrow.Amount = balance;
        parentrow.IsRowSelected = true;

    };




}]);