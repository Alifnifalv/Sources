app.controller("OnlineExamResultController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("OnlineExamResultController Loaded");

    $scope.SelectedCandidate = null;
    $scope.SelectedAcademicYear = null;
    $scope.SelectedExam = null;
    $scope.ExamResult = null;

    function showOverlay() {
        $('.preload-overlay', $('#OnlineExamResult')).attr('style', 'display:block');
    };

    function hideOverlay() {
        $('.preload-overlay', $('#OnlineExamResult')).hide();
    };

    $scope.Init = function (model, windowname, type) {
        $scope.type = type;

        $http({
            method: 'Get',
            url: "Mutual/GetDynamicLookUpData?lookType=ActiveAcademicYear&defaultBlank=false",
        }).then(function (result) {
            $scope.AcademicYears = result.data;
            $scope.LoadCurrentAcademicYear();
        });

        $http({
            method: 'Get',
            url: "Mutual/GetDynamicLookUpData?lookType=OnlineExamResultStatus&defaultBlank=false",
        }).then(function (result) {
            $scope.ResultStatuses = result.data;
        });

        $http({
            method: 'Get',
            url: "Mutual/GetDynamicLookUpData?lookType=Candidate&defaultBlank=false",
        }).then(function (result) {
            $scope.Candidates = result.data;
        });

    };

    $scope.SearchCriteriaChanges = function (selectedData, type) {
        if (type == "candidate") {
            $scope.SelectedCandidate = selectedData;
            $scope.CandidateChanges();
        }
        if (type == "exam") {
            $scope.SelectedExam = selectedData;
        }
    }

    $scope.CandidateChanges = function () {

        var candidateID = $scope.SelectedCandidate?.Key;
        var academicYearID = $scope.SelectedAcademicYear?.Key;

        if (candidateID) {
            showOverlay();

            $http({
                method: 'Get',
                url: "OnlineExams/OnlineExam/GetOnlineExamsByCandidateAndAcademicYear?candidateID=" + candidateID + "&academicYearID=" + academicYearID,
            }).then(function (result) {

                if (result.data.IsError) {
                    hideOverlay();
                    $().showGlobalMessage($root, $timeout, true, "Error while fetch online exams!");
                    return false;
                }
                else {
                    $scope.OnlineExams = result.data.Response;
                    hideOverlay();
                }
            });
        }
        else {
            $().showGlobalMessage($root, $timeout, true, "Candiate is required to fetch online exams!");
            return false;
        }
    }

    $scope.LoadCurrentAcademicYear = function () {

        $http({
            method: 'Get',
            url: "Schools/School/GetCurrentAcademicYear",
        }).then(function (result) {

            var academicYr = $scope.AcademicYears.find(x => x.Key == result.data);
            $scope.SelectedAcademicYear = academicYr;
        });
    }

    $scope.isEmptyObject = function (obj) {
        return angular.equals({}, obj);
    };

    $scope.GetExamResult = function () {

        var candidateID = $scope.SelectedCandidate?.Key;
        var examID = $scope.SelectedExam?.Key;

        if (!candidateID) {
            $().showGlobalMessage($root, $timeout, true, "Candidate is required!");
            return false;
        }
        else if (!examID) {
            $().showGlobalMessage($root, $timeout, true, "Exam is required!");
            return false;
        }
        else {
            showOverlay();

            $http({
                method: 'Get',
                url: "OnlineExams/OnlineExam/GetOnlineExamResults?candidateID=" + candidateID + "&examID=" + examID,
            }).then(function (result) {

                if (result.data.IsError) {
                    hideOverlay();
                    $().showGlobalMessage($root, $timeout, true, "Error while get result!");
                    return false;
                }
                else {
                    $scope.ExamResult = result.data.Response;
                    hideOverlay();
                }
            });
        }
    }

    $scope.GridMarkChanges = function (result, questionGrid) {

        if (questionGrid.Marks > questionGrid.TotalMarksOfQuestion) {
            questionGrid.Marks = questionGrid.OldMarks;
            $().showGlobalMessage($root, $timeout, true, "Please enter a mark that is less than or equal to the total mark!");
            return false;
        }

        var markObtained = 0;

        result.QuestionMapResults.forEach(map => {
            if (map.Marks) {
                markObtained += parseFloat(map.Marks);
            }            
        });

        var obtainedPercentage = parseFloat(markObtained) / parseFloat(result.TotalMarks) * 100;

        result.MarksObtained = markObtained;
        result.ObtainedMarksPercentage = obtainedPercentage != 0 ? obtainedPercentage.toFixed(2) : 0;
    };

    $scope.SaveMarkEntry = function () {

        var examResult = $scope.ExamResult;
        if (examResult) {
            showOverlay();

            var url = "OnlineExams/OnlineExam/UpdateOnlineExamResult";
            $http({
                method: 'Post',
                url: url,
                data: examResult
            }).then(function (result) {
                if (result.data.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.data.Response);
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, result.data.Response);

                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.GetExamResult();
                        });
                    }, 1000);
                }
                hideOverlay();
                return false;
            }, function () {
                hideOverlay();
            });
        }
        else {
            $().showGlobalMessage($root, $timeout, true, "No data found to save/update result!");
        }
    };

    $scope.ExpandCollapase = function (event, model, field) {
        model[field] = !model[field];
    };

}]);