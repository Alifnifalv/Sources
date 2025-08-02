app.controller("CollectionFeeController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("CollectionFeeController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.TotalFeeAmount = 0;
    $scope.TotalCollectedAmount = 0;
    $scope.FeeData = [];
    $scope.FineData = [];
    $scope.CurrentSchool = 0;
    $scope.CurrentAcademicYear = 0;
    $scope.Cashier = [];
    $scope.CurrentEmployee = 0;

    $scope.Academic = [];


    $.ajax({
        url: "Schools/School/GetCurrentUserData",
        type: "GET",
        success: function (result) {
            if (result != null) {
                $scope.$apply(function () {
                    $scope.HeaderInfoModel = result;
                    $scope.LoginID = $scope.HeaderInfoModel.LoginID;
                    $scope.CurrentSchool = $scope.HeaderInfoModel.SchoolID;
                    $scope.CurrentAcademicYear = $scope.HeaderInfoModel.AcademicYearID
                    $scope.CurrentEmployee = $scope.HeaderInfoModel.EmployeeID
                });
            }
        },
        complete: function (result) {
            $http({
                method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=Cashier&defaultBlank=false'
            }).then(function (result) {
                $scope.Cashier = result.data;
                $scope.CRUDModel.ViewModel.CashierEmployee = $scope.Cashier.find(x => x.Key == $scope.CurrentEmployee);
            });
        }
    });

    $.ajax({
        type: "GET",
        data: { schoolID: 0 },
        url: utility.myHost + "Schools/School/GetAcademicYearBySchool",
        contentType: "application/json;charset=utf-8",
        success: function (result) {

            $scope.$apply(function () {
                $scope.LookUps.AcademicYear = result;
            });

        },
        error: function () {

        },
        complete: function (result) {
            $.ajax({
                type: "GET",

                url: "Schools/School/GetCurrentAcademicYear",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {
                        if ($scope.CRUDModel.ViewModel.Academic != undefined) {
                            $scope.$apply(function () {
                                if ($scope.LookUps.AcademicYear.length > 1) {
                                    var academic = $scope.LookUps.AcademicYear.find(x => x.Key == result);
                                    if (academic != undefined) {
                                        $scope.CRUDModel.ViewModel.Academic.Key = academic.Key;
                                        $scope.CRUDModel.ViewModel.Academic.Value = academic.Value;
                                    }
                                    else {
                                        return false;
                                    }
                                }
                                else {
                                    return false;
                                }
                            });
                        }

                    }
                },
                error: function () {

                },
                complete: function (result) {
                }

            });
        }
    });



    $scope.FeeMasterChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        var url = "Frameworks/CRUD/Get?screen=FeeMaster&ID=" + model.FeeMaster.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                //model.FeeMonthly = [];
                model.IsFeePeriodDisabled = true;
                //model.IsFeePeriodDisabled = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.PaymodeChanges = function ($event, $index, gridModel) {
        showOverlay();
        var model = gridModel;

        if (gridModel.PaymentMode.Value == 'Cheque' || gridModel.PaymentMode.Value == 'Credit Card') {
            gridModel.IsTDateDisabled = false;
            gridModel.ReferenceNo = "";
        }
        else {
            gridModel.IsTDateDisabled = true;
            gridModel.ReferenceNo = "-";
        }

        gridModel.Amount = ($scope.TotalFeeAmount ?? 0) - ($scope.TotalCollectedAmount ?? 0);
        //console.log(gridModel.Amount)
        hideOverlay();
    };

    $scope.ClearSelection = function (gridModel) {
        showOverlay();
        var model = gridModel;
        if (model.IsRowSelected == false) {
            var isClear = true;
            angular.forEach($scope.CRUDModel.ViewModel.FeeInvoice, function (item) {
                if (model.FeePeriodID != null && item.FeePeriodID != null && (model.FeeMasterID == item.FeeMasterID && model.FeePeriodID < item.FeePeriodID && item.IsRowSelected == true)) {
                    model.IsRowSelected = true;
                    isClear = false;
                    $().showMessage($scope, $timeout, true, "Please select the term in an order!");
                    hideOverlay();
                    return false;
                }
            });
        }
        else {
            var isFill = true;
            angular.forEach($scope.CRUDModel.ViewModel.FeeTypes, function (item) {
                if (model.FeePeriodID != null && item.FeePeriodID != null && (model.FeeMasterID == item.FeeMasterID && model.FeePeriodID > item.FeePeriodID && item.IsRowSelected == false)) {
                    model.IsRowSelected = false;
                    $().showMessage($scope, $timeout, true, "Please select the term in an order!");
                    isFill = false;
                    hideOverlay();
                    return false;
                }
            });

        }
        hideOverlay();
    }


    $scope.SelectFees = function (gridModel) {
        console.log(gridModel);
        //$scope.FineData = [];
        showOverlay();
        var model = gridModel;

        if (model.IsExternal) {
            $scope.CRUDModel.ViewModel.FeeInvoice.forEach((item, index) => {
                if (item.IsExternal) {
                    $scope.CRUDModel.ViewModel.FeeInvoice[index].IsRowCheckBoxDisable = false;
                }
                else {
                    $scope.CRUDModel.ViewModel.FeeInvoice[index].IsRowCheckBoxDisable = true;
                }
            });
        }
        else {
            $scope.CRUDModel.ViewModel.FeeInvoice.forEach((item, index) => {
                if (item.IsExternal) {
                    $scope.CRUDModel.ViewModel.FeeInvoice[index].IsRowCheckBoxDisable = true;
                }
                else {
                    $scope.CRUDModel.ViewModel.FeeInvoice[index].IsRowCheckBoxDisable = false;
                }
            });
        }

        if (!model.IsRowSelected) {
            var notSelected = $scope.CRUDModel.ViewModel.FeeInvoice.filter(x => x.IsRowSelected == false).length;
            var fullList = $scope.CRUDModel.ViewModel.FeeInvoice.length;

            if (notSelected == fullList) {
                $scope.CRUDModel.ViewModel.FeeInvoice.forEach((item, index) => {
                    $scope.CRUDModel.ViewModel.FeeInvoice[index].IsRowCheckBoxDisable = false;
                });
            }
        }

        if (model.IsRowSelected == true) {

            var isFill = true;

            angular.forEach($scope.CRUDModel.ViewModel.FeeInvoice, function (item) {
                if (model.FeePeriodID != null && item.FeePeriodID != null && (model.FeeMasterID == item.FeeMasterID && model.FeePeriodID > item.FeePeriodID && item.IsRowSelected == false)) {
                    model.IsRowSelected = false;
                    $().showMessage($scope, $timeout, true, "Please select the term in an order!");
                    isFill = false;
                    hideOverlay();
                    return false;
                }
            });
            angular.forEach($scope.CRUDModel.ViewModel.FeeTypes, function (item) {
                if (model.StudentFeeDueID == item.value) {
                    isFill = false;
                    hideOverlay();
                    return false;
                }
            });


            if (isFill == true) {
                var url = "Schools/School/GetFeesByInvoiceNo?studentFeeDueID=" + model.StudentFeeDueID;

                $http({ method: 'Get', url: url })
                    .then(function (result) {
                        result.data.FeeTypes.forEach(x => {
                            $scope.FeeData.push({
                                InvoiceNo: x.InvoiceNo,
                                Amount: x.Amount,
                                FeePeriodID: x.FeePeriodID,
                                StudentFeeDueID: x.StudentFeeDueID,
                                FeeMasterClassMapID: x.FeeMasterClassMapID,
                                FeeDueFeeTypeMapsID: x.FeeDueFeeTypeMapsID,
                                CreditNoteFeeTypeMapID: x.CreditNoteFeeTypeMapID,
                                FeeMaster: x.FeeMaster,
                                FeePeriod: x.FeePeriod,
                                FeeMasterID: x.FeeMasterID,
                                InvoiceDateString: x.InvoiceDateString,
                                InvoiceDate: x.InvoiceDate,
                                CreditNote: x.CreditNote,
                                PrvCollect: x.PrvCollect,
                                Balance: x.Balance,
                                NowPaying: x.NowPaying,
                                NowPayingOld: x.NowPaying,
                                IsRowSelected: true,
                                FeeMonthly: x.FeeMonthly != null ? x.FeeMonthly.map(y => y) : null,

                            });

                        });


                        result.data.FeeFines.forEach(x => {
                            $scope.FineData.push({
                                InvoiceNo: x.InvoiceNo,
                                Fine: x.Fine,
                                Amount: x.Amount,
                                FineMasterID: x.FineMasterID,
                                FineMasterStudentMapID: x.FineMasterStudentMapID,
                                InvoiceDateString: x.InvoiceDateString,
                                InvoiceDate: x.InvoiceDate,
                                FeeDueFeeTypeMapsID: x.FeeDueFeeTypeMapsID,
                                StudentFeeDueID: x.StudentFeeDueID,
                            });
                        });

                        $scope.CRUDModel.ViewModel.FeeTypes = $scope.FeeData;
                        $scope.CRUDModel.ViewModel.FeeFines = $scope.FineData;
                        hideOverlay();
                    }, function () {
                        hideOverlay();
                    });
            }
        }
        else {
            var isClear = true;
            //angular.forEach($scope.CRUDModel.ViewModel.FeeInvoice, function (item) {
            //    if (model.FeePeriodID != null && item.FeePeriodID != null && (model.FeeMasterID == item.FeeMasterID && model.FeePeriodID < item.FeePeriodID && item.IsRowSelected == true)) {
            //        model.IsRowSelected = true;
            //        isClear = false;
            //        $().showMessage($scope, $timeout, true, "Please select the term in an order!");
            //        hideOverlay();
            //        return false;
            //    }
            //});

            if (isClear == true) {
                $scope.FeeData.filter(x => Number(x.StudentFeeDueID) === model.StudentFeeDueID).forEach(x => $scope.FeeData.splice($scope.FeeData.indexOf(x), 1));
                $scope.CRUDModel.ViewModel.FeeTypes = $scope.FeeData;
                $scope.FineData.filter(x => Number(x.StudentFeeDueID) === model.StudentFeeDueID).forEach(x => $scope.FineData.splice($scope.FineData.indexOf(x), 1));
                $scope.CRUDModel.ViewModel.FeeFines = $scope.FineData;
                hideOverlay();
            }
        }


    };




    $scope.ClassChanges = function ($event, $element, dueModel) {
        showOverlay();
        dueModel.Student = {};
        var model = dueModel;
        var SectionId = model.Section?.Key;
        if (SectionId == null) {
            SectionId = 0;
        }
        var url = "Schools/School/GetClassStudents?classId=" + model.StudentClass.Key + "&SectionId=" + SectionId;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Student = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.StudentChanges = function (gridModel) {
        showOverlay();
        var model = gridModel;

        var studentId = model.Student?.Key;
        if (studentId == null || studentId == 0) {
            $().showMessage($scope, $timeout, true, "Please Select a Student");
            $scope.FeeTypes = null;
            $scope.FeeInvoice = null;
            hideOverlay();
            return false;
        }
        $scope.FeeData.length = 0;
        $scope.FineData.length = 0;
        $scope.CRUDModel.ViewModel.FeeTypes = $scope.FeeData;
        $scope.CRUDModel.ViewModel.FeeFines = $scope.FineData;
        model.FeeInvoice = null;
        //model.FeeMonthly = [];
        var url = "Schools/School/GeStudentFromStudentsID?StudentID=" + studentId;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result == null || result.data.length == 0) {
                    $().showMessage($scope, $timeout, true, "There is no details available for this student");
                    $scope.FeeTypes = null;
                    hideOverlay();
                    return false;
                }
                $scope.CRUDModel.ViewModel.AdmissionNumber = result.data.AdmissionNumber;
                $scope.CRUDModel.ViewModel.Class = result.data.Class;
                $scope.CRUDModel.ViewModel.ClassID = result.data.ClassID;
                $scope.CRUDModel.ViewModel.Section = result.data.Section;
                $scope.CRUDModel.ViewModel.SectionID = result.data.SectionID;
                $scope.CRUDModel.ViewModel.EmailID = result.data.EmailID;
                $scope.CRUDModel.ViewModel.Academic.Key = result.data.Academic.Key;
                $scope.CRUDModel.ViewModel.Academic.Value = result.data.Academic.Value;
                $scope.CRUDModel.ViewModel.SchoolID = result.data.SchoolID;
                $scope.CRUDModel.ViewModel.StudentStatus = result.data.StudentStatus;
                $scope.CRUDModel.ViewModel.StudentStatusID = result.data.StudentStatusID;
                
            }, function () {

            });

        var url = "Schools/School/GetSiblingDueDetailsFromStudentID?StudentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.SiblingFeeInfo = result.data.Response;


            }, function () {

            });

        var url = "Schools/School/FillPendingFees?classId=0&studentId=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result == null || result.data.length == 0) {
                    $().showMessage($scope, $timeout, true, "There is no pending fee details available");
                    $scope.FeeTypes = null;
                    $scope.FeeInvoice = null;
                    hideOverlay();
                    return false;
                }
                //model.FeeTypes = result.data;
                model.FeeInvoice = result.data;

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };



    $scope.GetTotalFeeAmount = function (data, previousFees, fineData) {
        if (typeof (data) === 'undefined' && typeof (previousFees) === 'undefined' && typeof (fineData) === 'undefined') {
            return 0;
        }

        var sum = 0;

        if (previousFees != null) {
            for (var i = previousFees.length - 1; i >= 0; i--) {
                if (previousFees[i].Balance)
                    sum += parseFloat(previousFees[i].Balance);

            }
        }

        if (data != null) {
            for (var i = data.length - 1; i >= 0; i--) {
                if (data[i].NowPaying && data[i].IsRowSelected == true)
                    sum += parseFloat(data[i].NowPaying);

            }
        }
        if (fineData != null) {
            for (var i = fineData.length - 1; i >= 0; i--) {
                if (fineData[i].NowPaying)
                    sum += parseFloat(fineData[i].NowPaying);
            }
        }
        sum = sum.toFixed(2);
        $scope.TotalFeeAmount = sum;
        if ($scope.CRUDModel.ViewModel.FeePaymentMap != undefined && $scope.CRUDModel.ViewModel.FeePaymentMap.length == 1)
            $scope.CRUDModel.ViewModel.FeePaymentMap[0].Amount = parseFloat($scope.TotalFeeAmount ?? 0);
        return sum;
    };
    $scope.GetTotalCollectedAmount = function (data) {
        if (typeof (data) === 'undefined') {
            return 0;
        }

        var sum = 0;
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].Amount)// && data[i].TaxAmount
                sum += parseFloat(data[i].Amount);//+ parseFloat(data[i].TaxAmount);

        }
        sum = sum.toFixed(2);
        $scope.TotalCollectedAmount = sum;


        return sum;
    };

    $scope.UpdateParentFee = function (parentrow, index) {
        var feeSum = 0;
        var balance = 0;
        var creditNote = 0;
        var nowPaying = 0;
        for (var i = 0, l = parentrow.FeeMonthly.length; i < l; i++) {
            if (i > index && (parentrow.FeeMonthly[index].IsRowSelected == false) && parentrow.FeeMonthly[i].IsRowSelected == true) {
                $().showMessage($scope, $timeout, true, "Please select the month in an order!");
                parentrow.FeeMonthly[index].IsRowSelected = true;
                return false;
            }

            if (i < index && (parentrow.FeeMonthly[index].IsRowSelected == true) && parentrow.FeeMonthly[i].IsRowSelected == false) {
                $().showMessage($scope, $timeout, true, "Please select the month in an order!");
                parentrow.FeeMonthly[index].IsRowSelected = false;
                return false;
            }

        }
        if (parentrow.FeeMonthly[index].IsRowSelected == false) {

            parentrow.FeeMonthly[index].NowPayingOld = parentrow.FeeMonthly[index].NowPaying;
            parentrow.FeeMonthly[index].NowPaying = 0.00;
        }
        else {
            parentrow.FeeMonthly[index].NowPaying = parentrow.FeeMonthly[index].NowPayingOld;
        }

        parentrow.FeeMonthly.forEach(element => {
            if (element.IsRowSelected == true) {
                feeSum += Math.round(element.Amount);
                creditNote += Math.round(element.CreditNote);
                nowPaying += Math.round(element.NowPaying);
                balance += Math.round(element.Balance);
            }
        });
        parentrow.Amount = feeSum;
        parentrow.Balance = balance;
        parentrow.CreditNote = creditNote;
        parentrow.NowPaying = nowPaying;
        //if ($scope.CRUDModel.ViewModel.FeePaymentMap != undefined && $scope.CRUDModel.ViewModel.FeePaymentMap.length == 1)
        //    $scope.CRUDModel.ViewModel.FeePaymentMap[0].Amount = parseFloat($scope.TotalFeeAmount ?? 0) ;
    };


    $scope.LoadPaymentMode = function (rowIndex) {
        $scope.LookUps['AccountPaymentMode'] = [];
        var fitlers = new JSLINQ($scope.LookUps['PaymentModeList']).Where(function (PaymentMode) {
            var exists = new JSLINQ($scope.CRUDModel.ViewModel.FeePaymentMap)
                .Where(function (item) {
                    if (item == undefined || item.PaymentMode == null) return false;
                    return item.PaymentMode.Key == PaymentMode.Key;
                });
            if (exists.items.length == 0) {
                $scope.LookUps['AccountPaymentMode'].push(PaymentMode);
            }
        });
    }


    //To fill the collection grid in account posting 
    $scope.FillCollectFee = function () {

        var CashierEmployee = $scope.CRUDModel.ViewModel.CashierEmployee?.Key;
        if ($scope.CRUDModel.ViewModel.CollectionDateFromString == null || $scope.CRUDModel.ViewModel.CollectionDateToString == null || CashierEmployee == null) {
            $().showMessage($scope, $timeout, true, "Please fill out required fields!");
            return false;
        } else {
            showOverlay();
            var url = "Schools/School/GetCollectFeeAccountData?fromDate=" + $scope.CRUDModel.ViewModel.CollectionDateFromString + "&toDate=" + $scope.CRUDModel.ViewModel.CollectionDateToString + "&cashierID=" + CashierEmployee;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result == null || result.data.length == 0) {
                        $().showMessage($scope, $timeout, true, "There is no fee details available");
                        $scope.CRUDModel.ViewModel.DetailData = null;
                        hideOverlay();
                        return false;
                    }
                    $scope.CRUDModel.ViewModel.DetailData = result.data.Response.DetailData;
                    $scope.CRUDModel.ViewModel.PayModeData = result.data.Response.PayModeData;


                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    }

    $scope.AccountPosting = function () {

        var CashierEmployee = $scope.CRUDModel.ViewModel.CashierEmployee?.Key;
        if ($scope.CRUDModel.ViewModel.CollectionDateFromString == null || $scope.CRUDModel.ViewModel.CollectionDateToString == null || CashierEmployee == null) {
            $().showMessage($scope, $timeout, true, "Please fill out required fields!");
            return false;
        } else {
            showOverlay();
            $.ajax({

                url: utility.myHost + "Schools/School/FeeAccountPosting",
                type: "POST",
                data: {

                    "fromDate": $scope.CRUDModel.ViewModel.CollectionDateFromString,
                    "toDate": $scope.CRUDModel.ViewModel.CollectionDateToString,
                    "cashierID": CashierEmployee
                },
                success: function (result) {
                    if (result != null) {

                        $().showMessage($scope, $timeout, result.IsFailed, result.Message);
                        if (!result.IsFailed) {
                            $scope.CRUDModel.ViewModel.DetailData.forEach(x => {
                                x.IsPosted = true;
                            });
                        }
                    }
                },
                complete: function (result) {

                    hideOverlay();
                }
            });
        }
    }

    function setDefaultasZero(model) {
        model.NowPaying = 0;
        model.Balance = (model.Amount - (model.CreditNote ?? 0) - (model.PrvCollect ?? 0));
        return false;
    }

    $scope.SplitFeeAmount = function (gridModel) {
        showOverlay();
        var sum = gridModel.NowPaying;
        if (gridModel.NowPaying > (gridModel.Amount - (gridModel.CreditNote ?? 0) - (gridModel.PrvCollect ?? 0))) {
            $().showMessage($scope, $timeout, true, "Paying amount cannot be greater than due amount");

            gridModel.NowPaying = (gridModel.Amount - (gridModel.CreditNote ?? 0) - (gridModel.PrvCollect ?? 0));
            gridModel.Balance = (gridModel.Amount - gridModel.CreditNote ?? 0) - (gridModel.PrvCollect ?? 0) - (gridModel.NowPaying ?? 0);
            gridModel.FeeMonthly.map(setDefaultasZero);
            hideOverlay();
            return false;
        }
      
        gridModel.Balance = (gridModel.Amount - gridModel.CreditNote ?? 0) - (gridModel.PrvCollect ?? 0) - (gridModel.NowPaying ?? 0);
        gridModel.FeeMonthly.map(setDefaultasZero);
        for (var i = 0, l = gridModel.FeeMonthly.length; i < l; i++) {

            var amt = (gridModel.FeeMonthly[i].Amount - (gridModel.FeeMonthly[i].CreditNote ?? 0) - (gridModel.FeeMonthly[i].PrvCollect ?? 0));
            var nowpay = parseFloat(gridModel.NowPaying);
            gridModel.FeeMonthly[i].NowPaying = 0;
            if (amt >= sum) {
                gridModel.FeeMonthly[i].NowPaying = parseFloat(sum);
                gridModel.FeeMonthly[i].Balance = (gridModel.FeeMonthly[i].Amount - gridModel.FeeMonthly[i].CreditNote ?? 0) - (gridModel.FeeMonthly[i].PrvCollect ?? 0) - gridModel.FeeMonthly[i].NowPaying;
                break;
            }
            else {

                gridModel.FeeMonthly[i].NowPaying = amt;
                gridModel.FeeMonthly[i].Balance = (gridModel.FeeMonthly[i].Amount - gridModel.FeeMonthly[i].CreditNote ?? 0) - (gridModel.FeeMonthly[i].PrvCollect ?? 0) - gridModel.FeeMonthly[i].NowPaying;
                sum = sum - amt;
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
}]);

