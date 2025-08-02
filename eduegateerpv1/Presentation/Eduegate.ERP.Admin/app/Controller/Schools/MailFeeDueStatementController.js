app.controller("MailFeeDueStatementController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.ClassSectionChanges = function ($event, $element, model) {
        model.FeeDueDetails = [];
        showOverlay();
        if (!model.AsOnDate) {
            $().showMessage($scope, $timeout, true, "Please select date first!");
            model.Class = null;
            model.Section = null;
            hideOverlay();
            return false;
        };

        if (!model.Class.Key) {
            hideOverlay();
            return false;
        };

        showOverlay();

        var url = "Schools/School/GetFeeDueDatasForReportMail?asOnDateString=" + model.AsOnDate + "&classID=" + model.Class.Key + "&sectionID=" + model.Section?.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (!result.data.IsError) {
                    if (result.data.length == 0) {
                     $().showMessage($scope, $timeout, true, "No fee dues found!");
                     hideOverlay();
                    } else {
                        model.FeeDueDetails = result.data;
                    }
                }
                hideOverlay();

            }, function () {
                hideOverlay();
            });
    };

    $scope.SendReportToALL = function ($event, $element, model) {

        showOverlay();
        if (!model.AsOnDate || !model.Class.Key) {
            hideOverlay();
            return false;
        };

        if (model.FeeDueDetails.length <= 0) {
            $().showMessage($scope, $timeout, true, "There is no students found for send mail !");
            hideOverlay();
            return false;
        }
        else {
            var sendList = model.FeeDueDetails.filter(f => f.IsSelected == true);

            if (sendList.length <= 0) {
                $().showMessage($scope, $timeout, true, "please select any 1 fee due to send !");
                hideOverlay();
                return false;
            }

            sendList.forEach(x => {
                showOverlay();
                $scope.SendReportMailALL(x);
            });
            hideOverlay();
            $().showMessage($scope, $timeout, false, "Mail Send Successfully.");
        }
    }

    $scope.ClearALL = function ($event, $element, model) {

        showOverlay();

        model.Class = null;
        model.Section = null;
        model.Select_Deselect = null;
        model.FeeDueDetails = null;

        hideOverlay();
    }


    $scope.CheckBoxClicks = function ($event, $element, model) {
        showOverlay();
        if (model.Select_Deselect == true) {
            if (model.FeeDueDetails.length > 0) {
                model.FeeDueDetails.forEach(x => {
                    x.IsSelected = true;
                });
                hideOverlay();
            }
            else {
                hideOverlay();
                return false;
            }
        }
        else {
            if (model.FeeDueDetails.length > 0) {
                model.FeeDueDetails.forEach(x => {
                    x.IsSelected = false;
                });
                hideOverlay();
            }
            else {
                hideOverlay();
                return false;
            }
        }
    };


    $scope.SendReportMail = function (gridModel) {

        showOverlay();
        if (gridModel.StudentID) {
            $.ajax({
                url: utility.myHost + "Schools/School/SendFeeDueMailReportToParent",
                type: "POST",
                data: {

                    "StudentID": gridModel.StudentID,
                    "AsOnDate": gridModel.AsOnDate,
                    "ParentEmailID": gridModel.ParentEmailID,
                    "StudentName": gridModel.StudentName,
                    "AdmissionNo": gridModel.AdmissionNo,
                    "ParentLoginID": gridModel.ParentLoginID,
                    "SchoolID": gridModel.SchoolID,
                    "SchoolName": gridModel.SchoolName,
                    "Class": gridModel.Class,
                },
                success: function (result) {
                    if (result.returnMessage) {
                        $().showMessage($scope, $timeout, true, result.returnMessage);
                        hideOverlay();
                    }
                    else {
                        $().showMessage($scope, $timeout, false, "Mail Send Successfully.");
                        hideOverlay();
                    }
                },
                error: function () {
                    hideOverlay();
                },
            });
        }
    }


    $scope.SendReportMailALL = function (gridModel) {
        if (gridModel.StudentID) {
            $.ajax({
                url: utility.myHost + "Schools/School/SendFeeDueMailReportToParent",
                type: "POST",
                data: {

                    "StudentID": gridModel.StudentID,
                    "AsOnDate": gridModel.AsOnDate,
                    "ParentEmailID": gridModel.ParentEmailID,
                    "StudentName": gridModel.StudentName,
                    "AdmissionNo": gridModel.AdmissionNo,
                    "ParentLoginID": gridModel.ParentLoginID,
                    "SchoolID": gridModel.SchoolID,
                    "SchoolName": gridModel.SchoolName,
                    "Class": gridModel.Class,
                },
            });
        }
    }


    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);