//var app = angular.module('rzSliderDemo', ['rzSlider', 'ui.bootstrap'])
app.controller("FeePaymentController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope, $uibModal) {

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

        $scope.changeurl = function () {
            var currentLocation = location.protocol + '//' + location.host + location.pathname;
            //alert(currentLocation);
            const nextURL = 'https://my-website.com/page_b';
            const nextTitle = 'My new page title';
            const nextState = { additionalInformation: 'Updated the URL with JS' };

            // This will create a new entry in the browser's history, without reloading
            window.history.pushState("", "", event.target.currentLocation);
            event.preventDefault();
        }

        //end change url
        $timeout(function () {
            showOverlay();
            $scope.getFeePaymentData();
        });

        //To convert value to integer.
        $scope.parseInt = function (val) {
            return parseInt(val);
        }
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

    //function leapYear(year) {
    //    return ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0);
    //}

    //var totalMonthsShown = 12;

    //var today = new Date();
    //var todayMonthIndex = today.getMonth();
    //var todayYear = today.getFullYear();

    //var monthSlider = document.getElementById('month-slider');

    //var months = [
    //    'Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'
    //];

    //var daysInMonths = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    //var monthLabels = [];
    //var monthYearLabels = [];
    //var monthData = [];
    //var lastDaysData = [];

    //// Start iterator variables with today's year and month.
    //var thisYear = todayYear;
    //var thisMonth = todayMonthIndex;

    //// Iterate backwards through all the months to display setting
    //// the values of items on the scale
    //for (i = (totalMonthsShown - 1); i >= 0; i--) {

    //    monthYearLabels[i] = months[thisMonth] + ' ' + thisYear;
    //    monthLabels[i] = months[thisMonth];
    //    monthData[i] = thisYear + '-' + (thisMonth + 1);

    //    // February then ensure leap days are considered.
    //    if (thisMonth == 1 && leapYear(thisYear)) {
    //        lastDaysData[i] = 29;
    //    }
    //    else {
    //        lastDaysData[i] = daysInMonths[thisMonth];
    //    }

    //    // When month reaches January then decrement the year
    //    // and set the month to December for the next iteration.
    //    // For all other months then the only the month is decremented.
    //    if (thisMonth == 0) {
    //        thisMonth = 11;
    //        thisYear--;
    //    }
    //    else {
    //        thisMonth--;
    //    }

    //}

    //var range = {
    //    'min': 0,
    //    'max': totalMonthsShown - 1
    //}

    //noUiSlider.create(monthSlider, {
    //    start: [totalMonthsShown - 7, totalMonthsShown - 1],

    //    step: 1,
    //    range: range,
    //    tooltips: true,
    //    connect: true,
    //    animate: true,
    //    animationDuration: 600,

    //    pips: {
    //        mode: 'steps',
    //        density: totalMonthsShown - 1,
    //        // Force major pips for value.
    //        filter: function () {
    //            return 1;
    //        }
    //    }
    //});

    //// Remove the shortcut active class when manually setting a range.
    //monthSlider.noUiSlider.on('start', function () {
    //    $('.shortcuts li').removeClass('active');
    //});

    //monthSlider.noUiSlider.on('update', function (values, handle) {

    //    var monthIndex = parseInt(values[handle]);

    //    var prefixes = ['From', 'To'];

    //    if (handle == 0) {
    //        var day = 1;
    //    }
    //    else if (handle == 1) {
    //        var day = lastDaysData[monthIndex];
    //    }

    //    // Set the tooltip values.
    //    $('.noUi-handle[data-handle="' + handle + '"]').find('.noUi-tooltip').html(prefixes[handle] + '<br /><strong>' + day + ' ' + monthYearLabels[monthIndex] + '</strong>');

    //    // Update the pips values.

    //    $('.noUi-pips .noUi-value').each(function () {
    //        var index = $(this).html();
    //        $(this).html(monthLabels[index]);
    //    });

    //    // Update the input elements.
    //    var minValueIndex = parseInt(values[0]);
    //    var maxValueIndex = parseInt(values[1]);
    //    $('input[name="month-range-min"]').val(monthData[minValueIndex]);
    //    $('input[name="month-range-max"]').val(monthData[maxValueIndex]);

    //});

    //$('.month-slider-wrapper .shortcuts li').mousedown(function () {
    //    var monthPeriod = $(this).attr('data-min-range');

    //    var newValues = [
    //        (totalMonthsShown - monthPeriod),
    //        (totalMonthsShown - 1)
    //    ];

    //    monthSlider.noUiSlider.set(newValues);

    //    $('.shortcuts li').removeClass('active');
    //    $(this).addClass('active');

    //});

    //$scope.mywardsTabSelection = function(id) {

    //    if (id == 'StudentProfile') {
    //        $("#aboutme").hide();
    //        $("#TransportRoute").show();
    //    }
    //    else if (id == 'Attendance') {
    //        $("#getStudentAttendancebtn").click();
    //    }
    //    else {
    //        $("#aboutme").show();
    //        $("#TransportRoute").hide();
    //    }
       
    //}
    //$scope.mywardsTabSelection('FeePayment');
    //end tab selection 

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
   //         url: utility.myHost + "/Home/GetStudentDetails",
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
    //        url: utility.myHost + "/Home/FillFeeDue",
    //        contentType: "application/json;charset=utf-8",
    //        success: function (result) {
    //            if (!result.IsError && result !== null) {
    //                $scope.FeeTypes = result.Response;
    //            }
    //            $scope.FeeMonthlyHis = [];
    //            $.ajax({
    //                type: "GET",
    //                data: { studentId: studentId },
    //                url: utility.myHost + "/Home/GetFeeCollected",
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
        $scope.feePays = [] ;
        $.ajax({
            type: "GET",
            url: utility.myHost + "/Fee/FillFeePaymentDetails",
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
    //        url: utility.myHost + "/Home/FillFineDue",
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
    //                url: utility.myHost + "/Home/GetFineCollected",
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
        var totalAmountToBePaid = $scope.TotalAmountToBePaid;
        if (totalAmountToBePaid > 0) {

            $scope.SubmitAmountLog(totalAmountToBePaid);

        }
        else {
            hideOverlay();
            callToasterPlugin('error', "Payment requires an amount greater than zero!");
        }

    };

    $scope.SubmitAmountLog = function (totalAmountToBePaid) {

        var url = utility.myHost + "/PaymentGateway/SubmitAmountAsLog?totalAmount=" + totalAmountToBePaid;

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
            }

        }, function () {
            hideOverlay();
        });
    }

    $scope.InitiateSession = function () {

        var url = utility.myHost + "/PaymentGateway/InitiatePayment"

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
            }

        }, function () {
            hideOverlay();
        });

    }

    $scope.InitiateFeeCollections = function () {
        var feeList = [];

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
                "FeeTypes": feeTypeList
            });

        });

        if (feeList.length > 0) {

            $.ajax({
                type: "POST",
                data: JSON.stringify(feeList),
                url: utility.myHost + "/Fee/InitiateFeeCollections",
                contentType: "application/json;charset=utf-8",
                success: function (result) {

                    if (!result.IsError && result !== null) {
                        window.location.replace(utility.myHost + "/PaymentGateway/Initiate");
                    }
                    else {
                        callToasterPlugin('error', "Something went wrong, try again later!");
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
            url: utility.myHost + "/Home/SaveFeepaymentGateway",
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

}]);