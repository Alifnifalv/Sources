app.controller("FeeDueController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    //console.log("FeeDueController Loaded");
   angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.FeeCycleMaster = [];

    $scope.GetStudentsByDropDowns = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;

        var url = "Schools/School/GetStudentsByParameters?academicYearID=" + model.Academic.Key + "&classID=" + model.StudentClass.Key + "&sectionID=" + model.Section.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.LookUps.SchoolWiseStudents = result.data;
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
                $scope.LookUps.FeePeriod = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
        $scope.loadFeeCycleMaster(model);
        $scope.GetStudentsByDropDowns($event, $element, model);
    };

    $scope.ChangeFeeMaster = function ($event, $element, model) {
        showOverlay();
        var findData = null;
        model.Amount = null;

        $scope.LookUps.FeePeriod = [];

        model.FeeMaster.forEach(function (feeMasterID) {
            var url = "Schools/School/GetFeePeriod?academicYearID=" + model.Academic.Key + "&studentID=" + 0 + "&feeMasterID=" + feeMasterID.Key;
            $http.get(url)
                .then(function (result) {
                    $scope.LookUps.FeePeriod = result.data;
                    $scope.HideFields(model);
                    hideOverlay();
                })
                .catch(function (error) {
                    hideOverlay();
                });
        });

        hideOverlay();

    };

    $scope.HideFields = function (model) {
        if ($scope.LookUps.FeePeriod.length > 0) {
            model.IsEnableAmount = false;
        }
        else {
            model.IsEnableAmount = true;
        }
    }

    //To save fee dues
    $scope.SaveStudentFeeDue = function () {

        var feeDueDate = $scope.CRUDModel.ViewModel.FeeDueGenerationDateString;
        var invoiceDate = $scope.CRUDModel.ViewModel.InvoiceDateString;

        const today = new Date();
        today.setHours(0, 0, 0, 0); // Set the time component to midnight

        const feeDueDateParts = feeDueDate.split('/');
        const feeDueDateObj = new Date(feeDueDateParts[2], feeDueDateParts[1] - 1, feeDueDateParts[0]);
        feeDueDateObj.setHours(0, 0, 0, 0); // Set the time component to midnight

        const invoiceDateParts = invoiceDate.split('/');
        const invoiceDateObj = new Date(invoiceDateParts[2], invoiceDateParts[1] - 1, invoiceDateParts[0]);
        invoiceDateObj.setHours(0, 0, 0, 0); // Set the time component to midnight

        if (feeDueDateObj < today || invoiceDateObj < today) {
            $().showMessage($scope, $timeout, true, "Invoice/fee due generation is permissible for today or future dates only!!");
            return false;
        }

        if (feeDueDate == null) {
            $().showMessage($scope, $timeout, true, "Please select Fee Due Date!");
            return false;
        }

        if (invoiceDate == null) {
            $().showMessage($scope, $timeout, true, "Please select Invoice Date!");
            return false;
        }
         else {
            showOverlay();

            $.ajax({

                url: utility.myHost + "Schools/School/SaveStudentFeeDue",
                type: "POST",
                data: {
                    "StudentFeeDueIID": $scope.CRUDModel.ViewModel.StudentFeeDueIID,
                    "FeeDueGenerationDateString": $scope.CRUDModel.ViewModel.FeeDueGenerationDateString,
                    "InvoiceDateString": $scope.CRUDModel.ViewModel.InvoiceDateString,
                    "StudentClass": $scope.CRUDModel.ViewModel.StudentClass,
                    "Section": $scope.CRUDModel.ViewModel.Section,
                    "Student": $scope.CRUDModel.ViewModel.Student,
                    "FeePeriod": $scope.CRUDModel.ViewModel.FeePeriod,
                    "Academic": $scope.CRUDModel.ViewModel.Academic,
                    "FeeMaster": $scope.CRUDModel.ViewModel.FeeMaster,
                    "Amount": $scope.CRUDModel.ViewModel.Amount,
                    "Remarks": $scope.CRUDModel.ViewModel.Remarks,
                },
                success: function (result) {

                    if (!result.IsFailed && result != null) {
                        $().showMessage($scope, $timeout, false, result.Message);
                    }
                    else {

                        $().showMessage($scope, $timeout, true, result.Message);
                       
                    }
                },
                complete: function (result) {

                    //$().showMessage($scope, $timeout, result.IsFailed, result.Message);
                    hideOverlay();
                }
            });
        }
    }
    $scope.FeeMasterChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        var url = "Frameworks/CRUD/Get?screen=FeeMaster&ID=" + model.FeeMaster.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.FeeDueMonthly = [];
                model.IsFeePeriodDisabled = result.data.FeeCycle != 3;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FeePeriodChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        model.FeeDueMonthly = [];
        var url = "Schools/School/GetSplitUpPeriod?periodID=" + model.FeePeriod.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.FeeDueMonthly = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

   
    $scope.SplitAmount = function ($event, $element, gridModel) {
        showOverlay();
        var sum = 0;
        for (var i = 0, l = gridModel.FeeDueMonthly.length; i < l; i++) {
            if (i === (gridModel.FeeDueMonthly.length - 1)) {
                gridModel.FeeDueMonthly[i].Amount = gridModel.Amount - sum;
                break;
            }
            else {

                gridModel.FeeDueMonthly[i].Amount = Math.round(gridModel.Amount / gridModel.FeeDueMonthly.length);
                sum = sum + gridModel.FeeDueMonthly[i].Amount;
            }
        }


        hideOverlay();
    };


    $scope.FillFeeDues = function (row) {
        var windowName = 'FeeDueEdit';
        var viewName = 'Fee Due';

        if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + row.StudentFeeDueIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
            });
    };
    $scope.FillFeeCollection = function (StudentIID, InvoiceNo) {
        var windowName = 'FeeCollection';
        var viewName = 'Fee Collection';

        if ($scope.ShowWindow("Edit" + windowName,  viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + StudentIID + "&parameters="+"InvoiceNo=" + InvoiceNo;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName,  viewName, "Edit" + windowName);
            });
    };


    $scope.loadFeeCycleMaster = function(model) {
        var url = 'Mutual/GetDynamicLookUpData?lookType=FeeCycleMaster&defaultBlank=false';
        $http({ method: 'GET', url: url })
            .then(function (response) {
                $scope.FeeCycleMaster = response.data;
                hideOverlay();
            })
            .catch(function (error) {
                console.error('Error loading FeeCycleMaster:', error);
                hideOverlay(); // You may want to handle this differently based on the error
            });
    }

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);