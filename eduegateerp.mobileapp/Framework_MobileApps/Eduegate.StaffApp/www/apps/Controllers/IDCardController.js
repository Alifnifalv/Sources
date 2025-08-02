app.controller('IDCardController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce) {
    console.log('IDCardController loaded.');
    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();

    $scope.ParentUrlService = rootUrl.ParentUrl;

    $scope.Profile = null;

    $scope.onProfileFilling = true;

    // $rootScope.ShowLoader = true;
    // $rootScope.ShowPreLoader = true;

    $scope.init = function () {

    }

    $scope.LoadProfileInfo = function () {
        
        $http({
            method: 'GET',
            url: dataService + '/GetStaffProfile',
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.Profile = result;

            if ($scope.Profile == null) {
                $scope.onProfileFilling = false;
            }

            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;

        }).error(function (err) {
            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;
        });
        
    }

}]);