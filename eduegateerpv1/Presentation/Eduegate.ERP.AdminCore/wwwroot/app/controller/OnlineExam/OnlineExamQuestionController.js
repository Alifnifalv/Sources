app.controller("OnlineExamQuestionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.AnswerTypeChanges = function (model) {

        if (model.AnswerTypes) {
            if (model.AnswerTypes == model.TextAnswerTypeID) {
                model.QuestionOptionMaps = [];
            }
            else if (model.AnswerTypes == model.MultipleChoiceTypeID) {
                model.QuestionOptionMaps.forEach(map => {
                    if (map.IsCorrectAnswer) {
                        map.IsCorrectAnswer = false;
                    }
                });
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
            var url = utility.myHost + "Schools/School/GetSubjectbyQuestionGroup?questionGroupID=" + groupID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result.data.length > 0) {
                        model.SubjectName = result.data[0].Value;
                        model.SubjectID = result.data[0].Key;
                    }
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

    $scope.PassageQuestionChanges = function (crudmodel) {

        var model = crudmodel;

        var groupID = parseInt(model.PassageQuestion);

        if (groupID) {
            showOverlay();
            var url = utility.myHost + "Schools/School/GetPassageQuestionDetails?passageQuestionID=" + groupID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result.data) {
                        model.PassageQuestionName = result.data;
                    }
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    };

}]);