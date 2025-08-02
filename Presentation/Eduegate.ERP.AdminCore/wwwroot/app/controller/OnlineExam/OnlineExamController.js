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
                var datas = result.data.Response;

                if (result.data.IsError) {
                    $().showMessage($scope, $timeout, true, "Oops! Something went wrong. Please select a different group.");

                    model.QuestionGroup = {
                        "Key": null,
                        "Value": null
                    };
                }
                else {
                    if (datas.TotalQuestions == 0 && datas.TotalPassageQuestions == 0) {
                        $().showMessage($scope, $timeout, true, "Sorry, no questions are available for the selected group. Please choose another group or add questions to the selected group.");
                        model.QuestionGroup = {
                            "Key": null,
                            "Value": null
                        };
                    }
                    else {
                        model.GroupTotalQnCount = result.data.Response.TotalQuestions;
                        model.TotalNoOfQuestions = result.data.Response.TotalQuestions;
                        model.TotalNoOfPassageQuestions = result.data.Response.TotalPassageQuestions;
                        if (result.data.Response.TotalQuestions == 0) {
                            model.NumberOfQuestions = 0
                        }
                        if (result.data.Response.TotalPassageQuestions == 0) {
                            model.NumberOfPassageQuestions = 0
                        }
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

        else if (field == "NumberOfPassageQuestions") {
            if (!currentRow.NumberOfPassageQuestions) {
                return false;
            }

            var qnGroupID = currentRow.QuestionGroup?.Key;

            if (!qnGroupID) {
                $().showMessage($scope, $timeout, true, "Please select a group first.");
                currentRow.NumberOfPassageQuestions = null;
                return false;
            }

            if (currentRow.NumberOfPassageQuestions > currentRow.TotalNoOfPassageQuestions) {
                currentRow.NumberOfPassageQuestions = null;
                $().showMessage($scope, $timeout, true, "Error: Enter a value equal to or less than " + currentRow.GroupTotalQnCount + " (the total number of questions in this group).");
            }
        }

        else if (field == "ObjMarkGroup") {

            var marks = currentRow.ObjMarkGroup;
            var subjectID = $scope.CRUDModel.ViewModel.Subjects?.Key;
            var classID = $scope.CRUDModel.ViewModel.StudentClass?.Key;
            var sameMarksExist = $scope.CRUDModel.ViewModel.ObjectiveQuestionMaps?.filter(a => a.ObjMarkGroup === marks).length;

            if (marks != 0 && marks != null && subjectID != null && subjectID != 'undefined' && classID != null && classID != 'undefined') {

                if (sameMarksExist > 1) {
                    $().showMessage($scope, $timeout, true, "The mark already exists, please try adding it to the existing one!");
                    currentRow.ObjMarkGroup = null;

                    return;
                }

                showOverlay();

                var url = "OnlineExams/OnlineExam/GetQuestionsByMarks?marks=" + marks + '&subjectID=' + subjectID + "&classID=" + classID + "&isPassage=false";
                $http({ method: 'Get', url: url })
                    .then(function (result) {
                        currentRow.TotalNoOfQuestions = result.data;
                        hideOverlay();
                    }, function () {
                        hideOverlay();
                    });
            }
            else {
                $().showMessage($scope, $timeout, true, "Please select class and subject!");
                currentRow.ObjMarkGroup = null;
            }
        }

        else if (field == "SubMarkGroup") {

            var marks = currentRow.SubMarkGroup;
            var subjectID = $scope.CRUDModel.ViewModel.Subjects?.Key;
            var classID = $scope.CRUDModel.ViewModel.StudentClass?.Key;
            var sameMarksExist = $scope.CRUDModel.ViewModel.SubjectiveQuestionMaps?.filter(a => a.SubMarkGroup === marks).length;

            if (marks != 0 && marks != null && subjectID != null && subjectID != 'undefined' && classID != null && classID != 'undefined') {

                if (sameMarksExist > 1) {
                    $().showMessage($scope, $timeout, true, "The mark already exists, please try adding it to the existing one!");
                    currentRow.SubMarkGroup = null;

                    return;
                }

                showOverlay();

                var url = "OnlineExams/OnlineExam/GetQuestionsByMarks?marks=" + marks + '&subjectID=' + subjectID + "&classID=" + classID + "&isPassage=true";
                $http({ method: 'Get', url: url })
                    .then(function (result) {
                        currentRow.TotalNoOfQuestions = result.data;
                        hideOverlay();
                    }, function () {
                        hideOverlay();
                    });
            }
            else {
                $().showMessage($scope, $timeout, true, "Please select class and subject!");
                currentRow.SubMarkGroup = null;
            }
        }

        else if (field == "ObjNoOfQuestions") {

            var marks = currentRow.ObjMarkGroup;
            var questions = currentRow.ObjNoOfQuestions;

            if (marks == 0 || marks == null || marks == 'undefined') {
                $().showMessage($scope, $timeout, true, "Please enter marks!");
                currentRow.ObjMarkGroup = null;
                currentRow.ObjMarkGroup = null;
                currentRow.TotalNoOfQuestions = null;
                currentRow.TotalMarks = null;
            }
            else {
                var totalMarks = marks * questions;
                if (parseInt(currentRow.ObjNoOfQuestions) > parseInt(currentRow.TotalNoOfQuestions))
                {
                    $().showMessage($scope, $timeout, true, "Number of question and marks can't be greater than the Total questions!");
                    currentRow.ObjNoOfQuestions = null;
                    currentRow.TotalMarks = null;
                }
                else {
                    currentRow.TotalMarks = totalMarks;
                }
            }

        }

        else if (field == "SubNoOfQuestions") {

            var marks = currentRow.SubMarkGroup;
            var questions = currentRow.SubNoOfQuestions;

            if (marks == 0 || marks == null || marks == 'undefined') {
                $().showMessage($scope, $timeout, true, "Please enter marks!");
                currentRow.SubMarkGroup = null;
                currentRow.SubNoOfQuestions = null;
                currentRow.TotalNoOfQuestions = null;
                currentRow.TotalMarks = null;
            }
            else {
                var totalMarks = marks * questions;
                if (parseInt(currentRow.SubNoOfQuestions) > parseInt(currentRow.TotalNoOfQuestions))
                {
                    $().showMessage($scope, $timeout, true, "Number of question and marks can't be greater than the Total questions!");
                    currentRow.TotalMarks = null;
                    currentRow.SubNoOfQuestions = null;
                }
                else {
                    currentRow.TotalMarks = totalMarks;
                }
            }
        }
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    };

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    };

    $scope.SubjectChanges = function (model) {

        //var subjects = $scope.CRUDModel.ViewModel.Subjects;
        //var subjectIDs = subjects.map(subject => subject.Key).join(",");

        var subjectID = $scope.CRUDModel.ViewModel.Subjects?.Key;
        var classID = $scope.CRUDModel.ViewModel.StudentClass?.Key;

        showOverlay();
        //var url = "OnlineExams/OnlineExam/GetQuestionGroupByExamIDs?subjectIDs=" + subjectIDs;
        //$http({ method: 'Get', url: url })
        //    .then(function (result) {
        //        $scope.LookUps.QuestionGroups = result.data;
        //        hideOverlay();
        //    }, function () {
        //        hideOverlay();
        //    });

        var url = "OnlineExams/OnlineExam/GetQuestionDetails?subjectID=" + subjectID + "&classID=" + classID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.ObjTotalQuestions = result.data[0];
                $scope.CRUDModel.ViewModel.SubTotalQuestions = result.data[1];
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    //$scope.ChangeMarks = function (model) {

    //    var totalMarks = $scope.CRUDModel.ViewModel.MaximumMarks;
    //    var objMarks = $scope.CRUDModel.ViewModel.ObjectiveQuestionMarks;
    //    var subMarks = $scope.CRUDModel.ViewModel.SubjectiveQuestionMarks;

    //    if (subMarks != 0 || subMarks == null) {
    //        $scope.CRUDModel.ViewModel.SubjectiveQuestionMarks = parseFloat(totalMarks) - parseFloat(objMarks);
    //    }

    //    if (objMarks != 0 || objMarks == null) {
    //        $scope.CRUDModel.ViewModel.ObjectiveQuestionMarks = parseFloat(totalMarks) - parseFloat(subMarks);
    //    }

    //};

    $scope.PercentageCalcuation = function (field) {

        var totalMarks = parseInt($scope.CRUDModel.ViewModel.MaximumMarks ?? 0);
        var percentage = parseInt($scope.CRUDModel.ViewModel.PassPercentage ?? 0);
        var minMarks = parseInt($scope.CRUDModel.ViewModel.MinimumMarks ?? 0);

        if (field == "minMarks") {
            $scope.CRUDModel.ViewModel.PassPercentage = parseInt((100 * minMarks) / totalMarks);
        }

        if (field == "percentage") {
            $scope.CRUDModel.ViewModel.MinimumMarks = parseInt((percentage * totalMarks) / 100);
        }

    };

}]);