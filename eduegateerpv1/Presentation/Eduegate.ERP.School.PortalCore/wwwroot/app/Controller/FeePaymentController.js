//var app = angular.module('rzSliderDemo', ['rzSlider', 'ui.bootstrap'])
app.controller("FeePaymentController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {

    //$controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("FeePaymentController Loaded");
    $scope.Students = [];
    $scope.SelectedStudent = [];
    $scope.FeeMonthly = [];
    $scope.FeeMonthlyHis = [];
    $scope.FeeTypes = [];
    $scope.StudentDetails = null;
    $scope.FineDues = [];

    $scope.StudentName = {};
    $scope.Class = {};
    $scope.Section = {};
    $scope.ClassId = {};
    $scope.activeStudentID = 0;

    $scope.CurrentDate = new Date();

    $scope.TotalAmountToBePaid = 0;

    $scope.ShowPreLoader = true;

    $scope.init = function () {
        showOverlay();

        $scope.GetTotalFeePayAmount = function (data) {

            if (typeof (data) === 'undefined') {
                return 0;
            }
            var sum = 0;


            $.each(data, function (index, objModel) {
                $.each(objModel.FeeTypes, function (index, objModelinner) {

                    if (objModelinner.IsPayingNow) {
                        sum = sum + objModelinner.NowPaying;
                    }

                });
            });
            $scope.TotalAmountToBePaid = sum;

            return sum;
        }

        $timeout(function () {
            showOverlay();
            $scope.getFeePaymentData();
        });

        //To convert value to integer.
        $scope.parseInt = function (val) {
            return parseInt(val);
        }

        $http({
            method: 'Get', url: utility.myHost + "Setting/GetSettingValueByKey?settingKey=" + "QPAY_PAYMENT_MODE_ID",
        }).then(function (result) {
            $scope.QPAYPaymentMode = result.data;

            $scope.PaymentModeID = $scope.QPAYPaymentMode;

        });

        $http({
            method: 'Get', url: utility.myHost + "Setting/GetSettingValueByKey?settingKey=" + "FEECOLLECTIONPAYMENTMODE_ONLINE",
        }).then(function (result) {
            $scope.OnlinePaymentMode = result.data;

        });
    };

    //Update student wise fee
    $scope.UpdateStudentFee = function (studentRow) {

        var feeSum = 0;

        if (studentRow.IsSelected) {
            studentRow.FeeTypes.forEach(r => {
                if (r.IsExternal) {
                    //r.disable = true;
                    r.IsPayingNow = false;
                }
                else {
                    //r.disable = false;
                    r.IsPayingNow = true;
                }
            });
        }
        else {
            studentRow.FeeTypes.forEach(r => {
                r.disable = false;
                r.IsPayingNow = false;
            });
        }

        studentRow.FeeTypes.forEach(typeRow => {

            $scope.UpdateTypeFee(typeRow, studentRow);

            feeSum += Math.round(typeRow.NowPaying, 2);
        });

        studentRow.NowPaying = feeSum;
    };

    //Update fee type related fees
    $scope.UpdateTypeFee = function (typeRow, studentRow) {

        var feeSum = 0;

        if (typeRow.IsPayingNow == false) {
            angular.forEach(studentRow.FeeTypes, function (item) {
                if (typeRow.FeePeriodID != null && item.FeePeriodID != null && (typeRow.FeeMasterID == item.FeeMasterID && typeRow.FeePeriodID < item.FeePeriodID && item.IsPayingNow == true)) {
                    typeRow.IsPayingNow = true;
                    callToasterPlugin('error', "Please select the term in an order!");
                    return false;
                }
            });
            angular.forEach(studentRow.FeeTypes, function (item) {
                if ((typeRow.FeePeriodID != null && typeRow.IsPayingNow == false) && (item.FeePeriodID == null && item.IsPayingNow == true)) {
                    typeRow.IsPayingNow = true;
                    callToasterPlugin('error', "Kindly make sure to pay the regular fees first!");
                    return false;
                }
            });
        }

        if (typeRow.IsExternal) {
            if (studentRow.IsSelected && typeRow.IsPayingNow) {
                var checkDat = studentRow.FeeTypes.filter(x => x.IsPayingNow == true && x.IsExternal != false).length;
                if (checkDat > 0) {
                    callToasterPlugin('error', "This is external fee, can't club with other fees. please select it seperatly!");
                    typeRow.IsPayingNow = false;
                    return false;
                }
            }
        }
        else {
            var checkDat = studentRow.FeeTypes.filter(x => x.IsPayingNow == true && x.IsExternal == true).length;
            if (checkDat > 0) {
                callToasterPlugin('error', "Already selected an external fee. can't club with this fee!");
                typeRow.IsPayingNow = false;
                return false;
            }
        }

        if (typeRow.IsPayingNow == true) {

            var notpayingfees = studentRow.FeeTypes.filter(item => item.IsPayingNow == false);
            var payingfeeperiodID = typeRow.FeePeriodID;

            angular.forEach(notpayingfees, function (item) {
                if (payingfeeperiodID != null && item.FeePeriodID != null && (typeRow.FeeMasterID == item.FeeMasterID && payingfeeperiodID > item.FeePeriodID && item.IsPayingNow == false)) {
                    typeRow.IsPayingNow = false;
                    $rootScope.ShowToastMessage("Please select the term in an order!", 'error');
                    return false;
                }
            });
            angular.forEach(studentRow.FeeTypes, function (item) {
                if ((item.FeePeriodID != null && item.IsPayingNow == false) && (typeRow.FeePeriodID == null && typeRow.IsPayingNow == true)) {
                    typeRow.IsPayingNow = false;
                    callToasterPlugin('error', "Kindly make sure to pay the regular fees first!");
                    return false;
                }
            });
        }

        if (typeRow.IsPayingNow == true) {

            feeSum += Math.round(typeRow.Amount, 2);

            if (feeSum == 0) {
                if (typeRow.Amount != feeSum) {
                    feeSum = typeRow.Amount;
                }
            }
        }

        typeRow.NowPaying = feeSum;

        if (studentRow.FeeTypes.some(x => x.IsPayingNow == true)) {
            studentRow.IsSelected = true;
        }
        else {
            studentRow.IsSelected = false;
        }

        $scope.UpdateStudentRowFee(studentRow);

        $scope.UpdateMothWiseCheckBox(typeRow);
    };

    //Update student now paying fee
    $scope.UpdateStudentRowFee = function (studentFeeData) {

        var feeSum = 0;

        studentFeeData.FeeTypes.forEach(typeRow => {
            feeSum += Math.round(typeRow.NowPaying, 2);
        });

        studentFeeData.NowPaying = feeSum;
    }

    //Monthly check box updation based on fee type checking
    $scope.UpdateMothWiseCheckBox = function (feeTypeData) {

        feeTypeData.FeeMonthly.forEach(monthRow => {
            if (feeTypeData.IsPayingNow == true) {
                monthRow.IsRowSelected = true;

                monthRow.NowPaying = monthRow.OldNowPaying;
            }
            else {
                monthRow.IsRowSelected = false;

                monthRow.NowPaying = 0;
            }
        });
    };

    //Type and student wise check box updation based on month wise selection
    $scope.UpdateTypeAndStudentCheckBox = function (feeTypeData, studentRowData) {

        if (feeTypeData.NowPaying == 0) {
            feeTypeData.IsPayingNow = false;
        }
        else {
            feeTypeData.IsPayingNow = true;
        }

        if (studentRowData.FeeTypes.some(x => x.IsPayingNow == true)) {
            studentRowData.IsSelected = true;
        }
        else {
            studentRowData.IsSelected = false;
        }
    };

    //Update fee type monthly wise fees
    $scope.UpdateTypeFeeMonthWise = function (typeRow, studentRow, index) {

        var feeSum = 0;

        //Month order checking
        for (var i = 0, l = typeRow.FeeMonthly.length; i < l; i++) {
            if (i > index && (typeRow.FeeMonthly[index].IsRowSelected == false) && typeRow.FeeMonthly[i].IsRowSelected == true) {
                callToasterPlugin('error', "Please select the month in an order!");
                typeRow.FeeMonthly[index].IsRowSelected = true;
                return false;
            }

            if (i < index && (typeRow.FeeMonthly[index].IsRowSelected == true) && typeRow.FeeMonthly[i].IsRowSelected == false) {
                callToasterPlugin('error', "Please select the month in an order!");
                typeRow.FeeMonthly[index].IsRowSelected = false;
                return false;
            }
        }
       

        typeRow.FeeMonthly.forEach(monthRow => {
            if (monthRow.IsRowSelected == true) {
                feeSum += Math.round(monthRow.Amount, 2);

                monthRow.NowPaying = monthRow.OldNowPaying;
            }
            else {
                monthRow.NowPaying = 0;
            }
        });

        typeRow.NowPaying = feeSum;

        $scope.UpdateTypeAndStudentCheckBox(typeRow, studentRow);

        $scope.UpdateStudentRowFee(studentRow);
    };

    $scope.GetTotalFeeAmount = function (data) {
        if (typeof (data) === 'undefined') {
            return 0;
        }

        var sum = 0;
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].Amount)
                sum += parseFloat(data[i].Amount);

        }

        return sum;
    };

    function showOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#FeePaymentOverlay").fadeIn();
            });
        });
    }

    function hideOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#FeePaymentOverlay").fadeOut();
            });
        });
    }


    //$scope.studentProfile = function (studentId) {

    //     //showOverlay();
    //     $scope.StudentDetails = [];
    //     $.ajax({
    //         type: "GET",
    //         data: { studentId: studentId },
    //         url: utility.myHost + "Home/GetStudentDetails",
    //         contentType: "application/json;charset=utf-8",
    //         success: function (result) {
    //             if (!result.IsError && result !== null) {
    //                 $scope.$apply(function () {
    //                     $scope.StudentDetails = result.Response[0];
    //                 });
    //             }
    //             $("#getStudentAttendancebtn").click();
    //         },
    //         error: function () {

    //         },
    //         complete: function (result) {
    //             //hideOverlay();
    //         }
    //     });
    // }



    //$scope.getFeeMonthly = function (studentId) {

    //    $scope.FeeMonthly = [];
    //    $.ajax({
    //        type: "GET",
    //        data: { studentId: studentId },
    //        url: utility.myHost + "Home/FillFeeDue",
    //        contentType: "application/json;charset=utf-8",
    //        success: function (result) {
    //            if (!result.IsError && result !== null) {
    //                $scope.FeeTypes = result.Response;
    //            }
    //            $scope.FeeMonthlyHis = [];
    //            $.ajax({
    //                type: "GET",
    //                data: { studentId: studentId },
    //                url: utility.myHost + "Home/GetFeeCollected",
    //                contentType: "application/json;charset=utf-8",
    //                success: function (result) {
    //                    if (!result.IsError && result !== null) {
    //                        $scope.FeeTypeHis = result.Response;
    //                    }

    //                },
    //                error: function () {

    //                },
    //                complete: function (result) {
    //                    //hideOverlay();
    //                }
    //            });
    //        },
    //        error: function () {

    //        },
    //        complete: function (result) {

    //        }
    //    });
    //}

    $scope.getFeePaymentData = function () {
        showOverlay();
        $scope.feePays = [];
        $.ajax({
            type: "GET",
            url: utility.myHost + "Fee/FillFeePaymentDetails",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.feePays = result.Response;

                        //$scope.feePays.forEach(x => {
                        //    x.FeeTypes.forEach(y => {
                        //        if (y.IsExternal) {
                        //            y.IsPayingNow = false;
                        //            y.disable = true;
                        //            x.NowPaying = x.NowPaying - y.NowPaying;
                        //        }
                        //        else {
                        //            y.IsPayingNow = true;
                        //            y.disable = false;
                        //        }
                        //    });
                        //});

                        $scope.ShowPreLoader = false;
                    });
                }
                hideOverlay();
            },
            error: function () {
                hideOverlay();
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }

    //$scope.getFines = function (studentId) {

    //    $scope.FineDues = [];
    //    $.ajax({
    //        type: "GET",
    //        data: { studentId: studentId },
    //        url: utility.myHost + "Home/FillFineDue",
    //        contentType: "application/json;charset=utf-8",
    //        success: function (result) {
    //            console.log(result);
    //            if (!result.IsError && result !== null) {
    //                $scope.FineDues = result.Response;
    //            }
    //            $scope.FineHis = [];
    //            $.ajax({
    //                type: "GET",
    //                data: { studentId: studentId },
    //                url: utility.myHost + "Home/GetFineCollected",
    //                contentType: "application/json;charset=utf-8",
    //                success: function (result) {
    //                    if (!result.IsError && result !== null) {
    //                        $scope.FineHis = result.Response;
    //                    }

    //                },
    //                error: function () {

    //                },
    //                complete: function (result) {
    //                    //hideOverlay();
    //                }
    //            });
    //        },
    //        error: function () {

    //        },
    //        complete: function (result) {

    //        }
    //    });
    //}

    $scope.ExpandCollapase = function (event, model, field) {
        model[field] = !model[field];

    };

    $scope.FeePaymentClick = function () {
        showOverlay();
        $scope.IsClickToPay = true;
        var totalAmountToBePaid = $scope.TotalAmountToBePaid;
        if (totalAmountToBePaid > 0) {

            $scope.IsClickToPay = true;

            $scope.SubmitAmountLog(totalAmountToBePaid);

        }
        else {
            hideOverlay();
            callToasterPlugin('error', "Payment requires an amount greater than zero!");
            $scope.IsClickToPay = false;
        }

    };

    $scope.SubmitAmountLog = function (totalAmountToBePaid) {

        var url = utility.myHost + "PaymentGateway/SubmitAmountAsLog?totalAmount=" + totalAmountToBePaid;

        $http({
            url: url,
            method: "POST",
        }).then(function (result) {

            if (!result.data.IsError && result.data.Response !== null) {
                $scope.InitiateSession();
            }
            else {
                hideOverlay();
                callToasterPlugin('error', "Something went wrong, try again later!");
                $scope.IsClickToPay = false;
            }

        }, function () {
            hideOverlay();
        });
    }

    $scope.InitiateSession = function () {

        var url = utility.myHost + "PaymentGateway/InitiatePayment?PaymentModeID=" + $scope.PaymentModeID;

        $http({
            url: url,
            method: "POST",
        }).then(function (result) {

            if (!result.data.IsError && result.data.Response !== null) {
                $scope.InitiateFeeCollections();
            }
            else {
                hideOverlay();
                callToasterPlugin('error', "Something went wrong, try again later!");
                $scope.IsClickToPay = false;
            }

        }, function () {
            hideOverlay();
        });

    }

    $scope.InitiateFeeCollections = function () {
        var feeList = [];

        var totalAmount = 0;

        $scope.feePays.forEach(fp => {

            var feeTypeList = [];

            if (fp.FeeTypes.length > 0) {

                var selectedFeeTypes = fp.FeeTypes.filter(ft => ft.IsPayingNow == true);

                if (selectedFeeTypes.length > 0) {

                    selectedFeeTypes.forEach(sft => {

                        var monthlyFeeList = [];

                        if (sft.FeeMonthly.length > 0) {

                            var selectedMonthlyFees = sft.FeeMonthly.filter(fmly => fmly.IsRowSelected == true);

                            if (selectedMonthlyFees.length > 0) {

                                selectedMonthlyFees.forEach(sfmly => {

                                    monthlyFeeList.push({
                                        "FeeCollectionMonthlySplitIID": 0,
                                        "MonthID": sfmly.MonthID,
                                        "Amount": sfmly.NowPaying,
                                        "NowPaying": sfmly.NowPaying,
                                        "FeeDueMonthlySplitID": sfmly.FeeDueMonthlySplitID,
                                        "Year": sfmly.Year,
                                        "CreditNote": sfmly.CreditNote,
                                        "Balance": sfmly.Balance
                                    });

                                });
                            }
                        }

                        feeTypeList.push({
                            "FeeCollectionFeeTypeMapsIID": 0,
                            "FeeMasterID": sft.FeeMasterID,
                            "FeePeriodID": sft.FeePeriodID,
                            "Amount": sft.Amount,
                            "FeeDueFeeTypeMapsID": sft.FeeDueFeeTypeMapsID,
                            "NowPaying": sft.NowPaying,
                            "MontlySplitMaps": monthlyFeeList
                        });

                        totalAmount += sft.NowPaying;

                    });
                }
            }

            feeList.push({
                "FeeCollectionIID": 0,
                "StudentID": fp.StudentID,
                "ClassID": fp.ClassID,
                "SectionID": fp.SectionID,
                "SchoolID": fp.SchoolID,
                "AcadamicYearID": fp.AcademicYearID,
                "FeePaymentModeID": $scope.PaymentModeID,
                "FeeTypes": feeTypeList
            });

        });

        if ($scope.TotalAmountToBePaid != totalAmount) {
            callToasterPlugin('error', "The total amount does not match the sum of the selected types!");
        }

        if (feeList.length > 0) {

            $.ajax({
                type: "POST",
                data: JSON.stringify(feeList),
                url: utility.myHost + "Fee/InitiateFeeCollections",
                contentType: "application/json;charset=utf-8",
                success: function (result) {

                    if (!result.IsError && result !== null) {
                        window.location.replace(utility.myHost + "PaymentGateway/Initiate?paymentModeID=" + $scope.PaymentModeID);
                    }
                    else {
                        callToasterPlugin('error', "Something went wrong, try again later!");

                        $scope.IsClickToPay = false;
                    }
                },
                error: function () {
                    hideOverlay();
                },
                complete: function (result) {
                    hideOverlay();
                }
            });

        }
        else {
            hideOverlay();
        }
    }

    $scope.SaveFeepaymentGateway = function () {
        $.ajax({
            type: "POST",
            //data: { studentId: $scope.Students[0].StudentIID },
            url: utility.myHost + "Home/SaveFeepaymentGateway",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                //if (!result.IsError && result !== null) {
                //    $scope.AssignmentList = result.Response;
                //}
            },
            //error: function () {

            //},
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.ClosePopup = function () {
        $("#ItemPopup").fadeOut("fast");
        $(".gridItemOverlay").hide();
    }
    $scope.OpenPopup = function () {
        $("#ItemPopup").fadeIn("fast");
        $(".gridItemOverlay").show();
    }
    $scope.SubmitPopup = function () {
        $("#ItemPopup").fadeOut("fast");
        $(".gridItemOverlay").hide();
    }

    $scope.ChangePaymentMethod = function (type) {

        if (type == "DebitCard") {
            $scope.PaymentModeID = $scope.QPAYPaymentMode;
        }
        else {
            $scope.PaymentModeID = $scope.OnlinePaymentMode;
        }
    };

    $scope.FeePaymentTabClick = function () {
        window.location.replace(utility.myHost + "Fee/Index");
    };

}]);