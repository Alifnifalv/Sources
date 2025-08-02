app.controller("ExamsListController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("ExamsListController Loaded");

    $scope.CandidateOnlineExams = [];

    $scope.Init = function (examLists) {
        $scope.GetLookUpDatas();

        $scope.CandidateOnlineExams = examLists;
    };

    $scope.GetLookUpDatas = function () {

        $http({
            method: 'Get', url: utility.myHost + "/ExamSetting/GetSettingValueByKey?settingKey=" + "ONLINE_EXAM_STATUSID_COMPLETE",
        }).then(function (result) {
            $scope.ExamCompleteStatusID = result.data;
        });

        $http({
            method: 'Get', url: utility.myHost + "/ExamSetting/GetSettingValueByKey?settingKey=" + "ONLINE_EXAM_STATUSID_START",
        }).then(function (result) {
            $scope.ExamStartedStatusID = result.data;
        });
    };

    $scope.StartExam = function (examData) {
        $.ajax({
            type: "GET",
            url: utility.myHost + "/Home/CheckExamQuestionAvailability?examID=" + examData.OnlineExamID + "&candidateOnlinExamMapID" + examData.CandidateOnlinExamMapID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.IsError) {
                    callToasterPlugin('error', result.Response, 10000);
                }
                else {
                    $scope.SaveExamTimes(examData.OnlineExamID, examData.CandidateOnlinExamMapID, examData.TotalExamDuration)
                    //window.location.replace(utility.myHost + "/Home/Questions?examID=" + examID + "&candidateOnlinExamMapID=" + candidateOnlinExamMapID);
                }
            },
            error: function () {
                callToasterPlugin('error', "Something went wrong, try again later!");
            },
            complete: function (result) {

            }
        });
    };

    $scope.SaveExamTimes = function (onlineExamID, candidateOnlinExamMapID, totalDuration) {

        $.ajax({
            type: "GET",
            url: utility.myHost + "/Home/InsertExamMapStartEndTime?candidateOnlinExamMapID=" + candidateOnlinExamMapID + "&durationInMinutes=" + totalDuration,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.IsError) {
                    callToasterPlugin('error', result.Response, 10000);
                }
                else {

                    window.location.replace(utility.myHost + "/Home/Questions?examID=" + onlineExamID + "&candidateOnlinExamMapID=" + candidateOnlinExamMapID);
                }
            },
            error: function () {
                callToasterPlugin('error', "Something went wrong, try again later!");
            },
            complete: function (result) {

            }
        });
    }

}]);