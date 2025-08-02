app.controller('HomeController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', 'loggedIn', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce, loggedIn) {
    console.log('Home controller loaded.');
    var dataService = rootUrl.RootUrl;
    var context = GetContext.Context();
    $scope.DashbaordType = 1;
    $scope.UserName = context.EmailID;

    $scope.IsLoggedIn = false;

    $scope.to_trusted = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    $scope.Init = function () {
        $rootScope.ShowLoader = false;
        $rootScope.ShowPreLoader = false;

        if (context.LoginID)
        {
            var loggedInPromise = loggedIn.CheckLogin(context, dataService);
            loggedInPromise.then(function (model) {
    
                if (model.data != null && model.data != undefined) {
                    if (!model.data.LoginID) {
                        $state.go("login", null, { reload: true });
                    }
                    else{
                        $scope.IsLoggedIn = true;
                    }
                }
                else{
                    $state.go("login", null, { reload: true });
                }
            });
        }
        else{
            $state.go("login", null, { reload: true });  
        }
        
    };
}]);