app.controller("OnlineExamQuestionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.AnswerTypeChanges = function (model) {

        if (model.AnswerTypes) {
            if (model.AnswerTypes == model.TextAnswerTypeID) {
                model.QuestionOptionMaps = [];
            }
        }
    };

    $scope.QuestionGroupChanges = function (crudmodel) {

        var model = crudmodel;

        model.SubjectName = null;
        model.SubjectID = null;

        var groupID = model.QuestionGroup?.Key;

        if (groupID) {
            showOverlay();
            var url = "Schools/School/GetSubjectbyQuestionGroup?questionGroupID=" + groupID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    model.SubjectName = result.data[0].Value;
                    model.SubjectID = result.data[0].Key;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    };

    $scope.AnswerSelectionClick = function (crudModel, gridModel) {

        if (gridModel.IsCorrectAnswer) {
            if (crudModel.AnswerTypes) {
                if (crudModel.AnswerTypes == crudModel.MultipleChoiceTypeID) {
                    if (crudModel.QuestionOptionMaps.length > 1) {
                        crudModel.QuestionOptionMaps.forEach(map => {
                            if (map.OptionText != gridModel.OptionText) {
                                map.IsCorrectAnswer = false;
                            }
                        })
                    }
                }
            }
        }
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);