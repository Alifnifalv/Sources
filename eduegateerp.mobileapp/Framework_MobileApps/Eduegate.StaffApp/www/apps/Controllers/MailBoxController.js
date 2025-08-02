app.controller('MailBoxController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce) {
    console.log('MailBoxController loaded.');
    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();
    
    $scope.MailBoxes = [];

    $rootScope.ShowLoader = true;

    $scope.init = function() {
        $http({
            method: 'GET',
            url: dataService + '/GetMyNotification',
            data: $scope.user,
            headers: { 
                "Accept": "application/json;charset=UTF-8", 
                "Content-type": "application/json; charset=utf-8", 
                "CallContext": JSON.stringify(context) 
            }
        }).success(function (result) {
            $scope.MailBoxes = result;

            $rootScope.ShowLoader = false;
        })
        .error(function(){
            $rootScope.ShowLoader = false;
        });
    }

    // $scope.init();
}]);