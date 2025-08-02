app.controller("ExamsListController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {
        console.log("ExamsListController Loaded");

        $scope.CandidateOnlineExams = [];
        $scope.ExamCompleteStatusID = null;
        $scope.ExamStartedStatusID = null;

        $scope.Init = function (examLists) {
            $scope.GetLookUpDatas();
            $scope.CandidateOnlineExams = examLists;
        };

        $scope.GetLookUpDatas = function () {
            $http({
                method: 'GET',
                url: utility.myHost + "ExamSetting/GetSettingValueByKey?settingKey=ONLINE_EXAM_STATUSID_COMPLETE"
            }).then(function (result) {
                $scope.ExamCompleteStatusID = result.data;
            });

            $http({
                method: 'GET',
                url: utility.myHost + "ExamSetting/GetSettingValueByKey?settingKey=ONLINE_EXAM_STATUSID_START"
            }).then(function (result) {
                $scope.ExamStartedStatusID = result.data;
            });
        };

        $scope.StartExam = function (examData) {
            $http({
                method: 'GET',
                url: utility.myHost + "Home/CheckExamQuestionAvailability?examID=" + examData.OnlineExamID + "&candidateOnlinExamMapID" + examData.CandidateOnlinExamMapID,
                params: {
                    examID: examData.OnlineExamID,
                    candidateOnlinExamMapID: examData.CandidateOnlinExamMapID
                }
            }).then(function (response) {
                if (response.data.IsError) {
                    callToasterPlugin('error', response.data.Response, 10000);
                } else {
                    $scope.SaveExamTimes(examData.OnlineExamID, examData.CandidateOnlinExamMapID, examData.TotalExamDuration);
                }
            }).catch(function (error) {
                callToasterPlugin('error', "Something went wrong, try again later!");
            });
        };

        $scope.SaveExamTimes = function (onlineExamID, candidateOnlinExamMapID, totalDuration) {
            $http({
                method: 'GET',
                url: utility.myHost + "Home/InsertExamMapStartEndTime",
                params: {
                    candidateOnlinExamMapID: candidateOnlinExamMapID,
                    durationInMinutes: totalDuration
                }
            }).then(function (response) {
                if (response.data.IsError) {
                    callToasterPlugin('error', response.data.Response, 10000);
                } else {
                    $window.location.replace(utility.myHost + "Home/Questions?examID=" + onlineExamID + "&candidateOnlinExamMapID=" + candidateOnlinExamMapID);
                }
            }).catch(function (error) {
                callToasterPlugin('error', "Something went wrong, try again later!");
            });
        };

    }
]);
