app.controller('TeacherClassController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce) {
    console.log('Teacher Class Controller loaded.');

    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();
    $scope.ParentUrlService = rootUrl.ParentUrl;

    $rootScope.ShowLoader = true;
    $rootScope.ShowPreLoader = true;

    $scope.TeacherClasses = [];

    $scope.Init = function () {
        $scope.LoadTeacherClassInfo();
    }

    $scope.LoadTeacherClassInfo = function () {

        $http({
            method: 'GET',
            url: dataService + '/GetTeacherClass',
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.TeacherClasses = result;

            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;
        }).error(function (err) {
            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;
        });

    }

    $scope.ClassStudentsViewClick = function (detail) {
        $state.go("classstudents", { classID: detail.ClassID, sectionID: detail.SectionID });
    }

}]);