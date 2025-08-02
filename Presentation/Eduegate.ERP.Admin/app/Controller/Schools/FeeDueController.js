app.controller("FeeDueController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    //console.log("FeeDueController Loaded");
   angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    //$scope.ClassChanges = function ($event, $element, dueModel) {
    //    showOverlay();
    //    dueModel.Student = {};
    //    var model = dueModel;
    //    var url = "Schools/School/GetClassStudents?classId=" + model.Class.Key + "&sectionId=0";
    //    $http({ method: 'Get', url: url })
    //        .then(function (result) {
    //            $scope.LookUps.Students = result.data;
    //            hideOverlay();
    //        }, function () {
    //            hideOverlay();
    //        });
    //};

    $scope.ClassSectionChange = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
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


    $scope.AcademicYearChanges = function ($event, $element, model) {
        showOverlay();

        var url = "Schools/School/GetFeePeriod?academicYearID=" + model.Academic.Key+"&studentID="+0;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.FeePeriod = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.ChangeFeeMaster = function ($event, $element, model) {
        showOverlay();
        var FeeMasterDetails = [];
        var findData = null;

        model.Amount = null;

        if (model.FeeMaster.length > 1 || model.FeeMaster.length == 0) {
            model.IsEnableAmount = false;
            hideOverlay();
            return false;
        }

        var url = 'Mutual/GetDynamicLookUpData?lookType=FeeCycleMaster&defaultBlank=false';
        $http({ method: 'Get', url: url })
            .then(function (result) {

                FeeMasterDetails = result.data;

                findData = FeeMasterDetails.find(x => x.Key == model.FeeMaster[0].Key);

                if (findData == undefined || findData == null) {
                    model.IsEnableAmount = false;
                }

                else {
                    model.IsEnableAmount = true;
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    //To save fee dues
    $scope.SaveStudentFeeDue = function () {

    //    if ($scope.CRUDModel.ViewModel.Class == null || $scope.CRUDModel.ViewModel.Class.Key == null)  
    //{
    //    $().showMessage($scope, $timeout, true, "Please select class!");
    //    return false;
    //}
    if ($scope.CRUDModel.ViewModel.FeeDueGenerationDateString == null) {
        $().showMessage($scope, $timeout, true, "Please select Fee Due Date!");
        return false;
    }
    if ($scope.CRUDModel.ViewModel.InvoiceDateString == null) {
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
                    "Class": $scope.CRUDModel.ViewModel.Class,
                    "Section": $scope.CRUDModel.ViewModel.Section,
                    "Student": $scope.CRUDModel.ViewModel.Student,
                    "FeePeriod": $scope.CRUDModel.ViewModel.FeePeriod,
                    "Academic": $scope.CRUDModel.ViewModel.Academic,
                    "FeeMaster": $scope.CRUDModel.ViewModel.FeeMaster,
                    "Amount": $scope.CRUDModel.ViewModel.Amount,
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


}]);