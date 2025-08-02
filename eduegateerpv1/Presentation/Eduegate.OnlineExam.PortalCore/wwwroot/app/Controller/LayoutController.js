app.controller("LayoutController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {
        console.log("LayoutController Loaded");

        $scope.CandidateDetails = [];
        $scope.ExamNotifications = [];
        $scope.UpcomingExams = [];

        $scope.Init = function (details) {
            $scope.GetLookUpDatas();
            $scope.CandidateDetails();
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

        $scope.CandidateDetails = function () {
            $http({
                method: 'GET',
                url: utility.myHost + "Home/GetCandidateDetails"
            }).then(function (result) {
                $scope.CandidateDetails = result.data;
            });
        }

    }
]);
