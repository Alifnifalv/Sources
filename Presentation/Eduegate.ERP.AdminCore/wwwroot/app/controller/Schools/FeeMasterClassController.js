app.controller("FeeMasterClassController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("FeeMasterClassController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.FeePeriods = [];

    $scope.FeeMasterChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        var url = "Frameworks/CRUD/Get?screen=FeeMaster&ID=" + model.FeeMaster.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.FeeMonthly = [];
                model.IsFeePeriodDisabled = result.data.FeeCycle != 1;
                $scope.LookUps.FeePeriod = [];
                if ($scope.FeePeriods.length == 0) {
                    var url = "Schools/School/GetFeePeriod?academicYearID=" + $scope.CRUDModel.ViewModel.Academic.Key + "&studentID=" + 0 + "&feeMasterID=" + 0;
                    $http({ method: 'Get', url: url })
                        .then(function (result) {

                            $scope.LookUps.FeePeriod == result.data;

                        }, function () {

                        });
                }
                else
                    $scope.LookUps.FeePeriod = $scope.FeePeriods;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.FeeMasterChangesFromCredit = function ($event, $element, gridModel) {
        showOverlay();

        $scope.LookUps.InvoiceNo = null;
        var model = gridModel;

        var studentID = $scope.CRUDModel.ViewModel.Student.Key

        if (studentID == null || studentID == undefined || studentID == "") {
            $().showMessage($scope, $timeout, true, "Please select student first ! ");
            model.FeeMaster = [];
            hideOverlay();

            return false;
        }

        var FeeMasterID = model.FeeMaster.Key;

        model.FeePeriod = {};
        model.InvoiceNo = {};
        model.Months = {};
        model.Years = {};
        model.Amount = null;

        var url = "Frameworks/CRUD/Get?screen=FeeMaster&ID=" + FeeMasterID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                model.IsFeePeriodDisabled = result.data.FeeCycle != 1;
                $scope.LookUps.FeePeriod = [];
                if (model.IsFeePeriodDisabled != true) {
                    var url = "Schools/School/GetFeePeriod?academicYearID=0" + "&studentID=" + studentID + "&feeMasterID=" + FeeMasterID;
                    $http({ method: 'Get', url: url })
                        .then(function (result) {
                            $scope.LookUps.FeePeriod = result.data;
                        }, function () {

                        });
                }
                else {
                    $scope.LookUps.FeePeriod = $scope.FeePeriods;
                    $scope.GetInvoiceNo($event, $element, gridModel);
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FeeMasterChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        var url = "Frameworks/CRUD/Get?screen=FeeMaster&ID=" + model.FeeMaster.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.FeeMonthly = [];
                model.IsFeePeriodDisabled = result.data.FeeCycle != 1;
                if ($scope.FeePeriods.length == 0) {
                    var url = "Schools/School/GetFeePeriod?academicYearID=" + $scope.CRUDModel.ViewModel.Academic.Key + "&studentID=" + 0 + "&feeMasterID=" + 0;
                    $http({ method: 'Get', url: url })
                        .then(function (result) {

                            $scope.LookUps.FeePeriod == result.data;

                        }, function () {

                        });
                }
                else {
                    $scope.LookUps.FeePeriod = $scope.FeePeriods;
                   
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.GetInvoiceNo = function ($event, $element, gridModel) {
        //showOverlay();
        var model = gridModel;
        var feeperiod = model.FeePeriod?.Key;
        var url = "Schools/School/GetInvoiceForCreditNote?classId=0&studentId=" + $scope.CRUDModel.ViewModel.Student.Key + "&feeMasterID=" + model.FeeMaster?.Key + "&feePeriodID=" + feeperiod;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.InvoiceNo = result.data;                
            }, function () {
                //hideOverlay();
            });
    };
    $scope.SplitUpPeriodMonthYear = function ($event, $element, gridModel, crudModel) {
        showOverlay();
        var model = gridModel;
        model.MonthSplitList = [];
        var feeperiod = model.FeePeriod?.Key;
        var url = "Schools/School/GetFeeDueMonthlyDetails?studentFeeDueID=" + model.InvoiceNo.Key + "&feeMasterID=" + model.FeeMaster.Key + "&feePeriodID=" + feeperiod;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                gridModel.FeeDueFeeTypeMapsID = result.data.FeeDueFeeTypeMapsID;
                gridModel.FeeDueMonthlySplitID = result.data.FeeDueMonthlySplitID;
                $scope.LookUps.Months = result.data.MonthList;
                $scope.LookUps.Years = result.data.YearList;
                gridModel.MonthSplitList = result.data.MonthSplitList;

                if (result.data.MonthList.length == 0) {
                    gridModel.Amount = result.data.MonthSplitList.find(a => a.FeeDueFeeTypeMapsID == gridModel.FeeDueFeeTypeMapsID)?.Amount;
                    gridModel.CorrectAmount = gridModel.Amount;
                }
                var monthsByInvoices = crudModel.FeeTypes.filter(y =>
                    y.InvoiceNo?.Key == gridModel.InvoiceNo?.Key && y.Months?.Key != null).map(m => m.Months);

               $scope.LookUps.Months = $scope.LookUps.Months?.filter(a => !monthsByInvoices?.some(b => b.Key === a.Key));

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.GetYear = function ($event, $element, gridModel) {        
        if (gridModel.MonthSplitList!=null && gridModel.MonthSplitList.length > 0) {
            var data = gridModel.MonthSplitList.find(x => x.MonthID == gridModel.Months.Key);
            gridModel.Years = $scope.LookUps.Years.find(x => x.Value == data.Year);
            gridModel.FeeDueMonthlySplitID = data.FeeDueMonthlySplitID;
            gridModel.FeeDueFeeTypeMapsID = data.FeeDueFeeTypeMapsID;
            gridModel.Amount = data.Amount;
            
            //Store currentAmount for validate amount is greater or not
            gridModel.CorrectAmount = data.Amount;
        }
        else {

        }
    };

    $scope.UpdateDetailGridValues = function ($event, $element, gridModel) {

        var gridData = $element;
        //decimal Amount is callig commonly -- only this function is working for this textbox so passing and validate by the screenID
        if (gridData.ScreenID == 2297 && gridData.InvoiceNo.Key != null) {
            if (gridData.Amount > gridData.CorrectAmount) {
                $().showMessage($scope, $timeout, true, "The entered amount must not be greater than the invoice amount. ");
                $element.Amount = $element.CorrectAmount;
            }
            return false;
        }
        else {
            return false;
        }
    }

    $scope.ClassChanges = function ($event, $element, dueModel) {
        //showOverlay();
        dueModel.Student = {};
        var model = dueModel;
        SectionId = 0;
        var url = "Schools/School/GetClassStudents?classId=" + model.StudentClass.Key + "&SectionId=" + SectionId;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Student = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.ClassSectionChange = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;
        var classId = $scope.CRUDModel.ViewModel.StudentClass?.Key;
        if (classId == null) {

            hideOverlay();
            return false;
        }
        var sectionId = $scope.CRUDModel.ViewModel.Section?.Key;
        if (sectionId == null) {
            sectionId = 0;
        }
        var url = "Schools/School/GetClassStudents?classID=" + classId + "&sectionID=" + sectionId;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.LookUps.Student = result.data;
                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };

    $scope.SplitAmount = function ($event, $element, gridModel) {
        showOverlay();
        var sum = 0;
        for (var i = 0, l = gridModel.FeeMonthly.length; i < l; i++) {
            if (i === (gridModel.FeeMonthly.length - 1)) {
                gridModel.FeeMonthly[i].Amount = gridModel.Amount - sum;
                break;
            }
            else {

                gridModel.FeeMonthly[i].Amount = Math.round(gridModel.Amount / gridModel.FeeMonthly.length);
                sum = sum + gridModel.FeeMonthly[i].Amount;
            }
        }


        hideOverlay();
    };


    $scope.FineMasterChanges = function ($event, $element, dueModel) {
        showOverlay();
        var model = dueModel;
        var url = "Schools/School/GetFineAmount?fineMasterID=" + model.FineMasterName.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.Amount = result.data.Amount;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.StructureAcademicChanges = function ($event, $element, model) {
        showOverlay();
        var url = "Schools/School/GetFeeStructure?academicYearID=" + model.Academic.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.FeeStructures = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.AcademicYearChanges = function ($event, $element, model) {
        showOverlay();

        var url = "Schools/School/GetFeePeriod?academicYearID=" + model.Academic.Key + "&studentID=" + 0 + "&feeMasterID=" + 0;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.FeePeriods = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.GetGridLookUpsForSchoolCreditNote = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;

        $scope.LookUps.FeePeriod = [];
        $scope.LookUps.InvoiceNo = [];
        $scope.LookUps.FeeMaster = [];

        var url = "Schools/School/GetGridLookUpsForSchoolCreditNote?studentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.FeePeriod = result.data.FeePeriod;
                $scope.LookUps.InvoiceNo = result.data.FeeInvoiceList;
                $scope.LookUps.FeeMaster = result.data.FeeMaster;
                hideOverlay();
            }, function () {

            });
        hideOverlay();
    };



    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }
}]);

