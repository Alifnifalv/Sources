app.controller('ClassStudentController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce) {
    console.log('Class Student Controller loaded.');

    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();

    $scope.ParentUrlService = rootUrl.ParentUrl;

    $scope.StudentMoreDetails = false;
    $scope.StudentFullDetails = false;

    $rootScope.ShowLoader = true;
    $rootScope.ShowPreLoader = true;

    $scope.ClassID = $stateParams.classID;
    $scope.SectionID = $stateParams.sectionID;

    $scope.StudentDetails = [];

    $scope.Init = function () {
        $scope.LoadStudentInfo($scope.ClassID, $scope.SectionID)
    }

    $scope.LoadStudentInfo = function (classID, sectionID) {

        $http({
            method: 'GET',
            url: dataService + '/GetStudentsByTeacherClassAndSection?classID=' + classID + '&sectionID=' + sectionID,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.StudentDetails = result;

            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;
        }).error(function (err) {
            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;
        });

    }

    $scope.ViewStudentLessDetails = function () {
        $scope.StudentMoreDetails = false;
        $scope.StudentFullDetails = false;
    }

    $scope.ViewStudentFullDetails = function () {
        $scope.StudentMoreDetails = false;
        $scope.StudentFullDetails = true;
    }

}]);