app.controller("OnlineExamQuestionMapController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.OnlineExamChanges = function (crudmodel) {
        showOverlay();
        var model = crudmodel;
        var url = "OnlineExams/OnlineExam/GetOnlineExamDetailsByExamID?onlineExamID=" + model.OnlineExam.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.OnlineExamID = result.data.OnlineExamIID;
                model.ExamName = result.data.Name;
                model.ExamDescription = result.data.Description;
                hideOverlay();
                $scope.ListOfQuestions = $scope.LookUps.Questions;
            }, function () {
                hideOverlay();
            });
    };

    //function to fill grid selected question details
    $scope.QuestionSelects = function ($event, $element, gridModel) {
        //showOverlay();
        var model = gridModel;
        var onlineExamID = model.OnlineExam?.Key;

        if (onlineExamID == null || onlineExamID == 0) {
            $().showMessage($scope, $timeout, true, "Please Select Online Exam !");
            model.Questions = null;
            hideOverlay();
            return false;
        }

        if ($scope.ListOfQuestions == null || $scope.ListOfQuestions == undefined) {
            var qtns = $scope.CRUDModel.ViewModel.Questions;
        }
        else {
            var qtns = $scope.ListOfQuestions;
        }
        if (qtns != undefined) {

            if (gridModel.OnlineExamQuestionMapDetail != undefined) {
                gridModel.OnlineExamQuestionMapDetail.forEach(y => {
                    if (!gridModel.Questions.find(z => z.QuestionID == y.QuestionID)) {
                        gridModel.OnlineExamQuestionMapDetail = gridModel.OnlineExamQuestionMapDetail.filter(function (item) {
                            return item["QuestionID"] != y.QuestionID
                        })
                    }
                });
            }

            gridModel.Questions.forEach(y => {
                if (!gridModel.OnlineExamQuestionMapDetail.find(z => z.QuestionID == y.Key)) {

                    var currentQuestionID = y.Key;

                    var url = "Schools/School/GetQuestionDetailsByQuestionID?questionID=" + currentQuestionID;
                    $http({ method: 'Get', url: url })
                        .then(function (result) {

                            gridModel.OnlineExamQuestionMapDetail.push({
                                QuestionID: result.data.QuestionID,
                                QuestionOptionCount: result.data.QuestionOptionCount,
                                AnswerType: result.data.AnswerType,
                                Question: result.data.Question,
                                Points: result.data.Points,
                            });
                            hideOverlay();
                        }, function () {
                            hideOverlay();
                        });
                }

            });
        }
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);