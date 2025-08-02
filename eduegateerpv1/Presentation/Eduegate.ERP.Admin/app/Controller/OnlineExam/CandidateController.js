app.controller("CandidateController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.ClassChanges = function ($event, $element, crudmodel) {
        showOverlay();
        var model = crudmodel;
        var url = "Schools/School/GetClassStudents?classId=" + model.Class.Key + "&sectionId=0";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Students = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.AllStudentsCheckBox = function ($event, $element, applicationModel) {
        var checkBox = applicationModel.IsAllStudents;
        var nwCandidateCheckBox = applicationModel.IsNewCandidate;

        if (nwCandidateCheckBox == true) {
            applicationModel.IsAllStudents = false;
            return false;
        }
        else if (checkBox == true) {
            applicationModel.Student = null;
        }
        else {
            return false;
        }
    };

    $scope.NewCandidateClicks = function ($event, $element, applicationModel) {
        var checkBox = applicationModel.IsNewCandidate;
        if (checkBox == true) {
            applicationModel.Student = null;
            applicationModel.IsAllStudents = false;
            applicationModel.Class = null;
            applicationModel.ExceptStudentList = null;
        }
        else {
            return false;
        }
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    };

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    };

    $scope.OnlineExamChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        var url = "OnlineExams/OnlineExam/GetOnlineExamDetailsByExamID?onlineExamID=" + model.OnlineExam.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result.data.TotalQnGroupsInExam == 0) {

                    $().showMessage($scope, $timeout, true, "Sorry, no question groups or questions are available for the selected exam. Please choose another exam or add groups to the selected exam.");

                    gridModel.OnlineExam = {
                        "Key": null,
                        "Value": null
                    };
                }
                else {
                    gridModel.Duration = result.data.MaximumDuration;
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

}]);