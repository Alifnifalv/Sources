app.controller("OnlineExamController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.QnGroupChanges = function (gridModel) {

        var model = gridModel;
        var qnGroupID = model.QuestionGroup?.Key;

        if (!qnGroupID) {
            return false;
        }

        showOverlay();
        var url = "OnlineExams/OnlineExam/GetQnGroupDetailsByID?qnGroupID=" + qnGroupID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data.IsError) {
                    $().showMessage($scope, $timeout, true, "Oops! Something went wrong. Please select a different group.");

                    model.QuestionGroup = {
                        "Key": null,
                        "Value": null
                    };
                }
                else {
                    if (result.data.Response.TotalQuestions == 0) {
                        $().showMessage($scope, $timeout, true, "Sorry, no questions are available for the selected group. Please choose another group or add questions to the selected group.");
                        model.QuestionGroup = {
                            "Key": null,
                            "Value": null
                        };
                    }
                    else {
                        model.GroupTotalQnCount = result.data.Response.TotalQuestions;
                    }
                }
                hideOverlay();

            }, function () {
                hideOverlay();
            });
    };

    $scope.UpdateDetailGridValues = function (field, currentRow, rowIndex) {

        if (field == "NumberOfQuestions") {
            if (!currentRow.NumberOfQuestions) {
                return false;
            }

            var qnGroupID = currentRow.QuestionGroup?.Key;

            if (!qnGroupID) {
                $().showMessage($scope, $timeout, true, "Please select a group first.");
                currentRow.NumberOfQuestions = null;
                return false;
            }

            if (currentRow.NumberOfQuestions > currentRow.GroupTotalQnCount) {
                currentRow.NumberOfQuestions = null;
                $().showMessage($scope, $timeout, true, "Error: Enter a value equal to or less than " + currentRow.GroupTotalQnCount + " (the total number of questions in this group).");
            }
        }
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    };

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    };

}]);