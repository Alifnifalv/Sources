app.controller('HomeController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', 'loggedIn', 'offlineSync',
    function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce, loggedIn, $offlineSync) {
        console.log('Home controller loaded.');
        var dataService = rootUrl.RootUrl;
        var context = GetContext.Context();
        $scope.DashbaordType = 1;
        $scope.UserName = context.EmailID;

        $scope.IsLoggedIn = false;

        $scope.to_trusted = function (html_code) {
            return $sce.trustAsHtml(html_code);
        };

        $rootScope.UserClaimSets = context.UserClaimSets;


        
        $scope.Init = function () {
            $rootScope.ShowLoader = true;

            var loggedInPromise = loggedIn.CheckLogin(context, dataService);
            loggedInPromise.then(function (model) {

                if (model.data != null && model.data != undefined) {
                    if (!model.data.LoginID) {
                        $rootScope.ShowLoader = false;
                        $state.go("login", null, { reload: true });
                    }
                    else {
                        $scope.IsLoggedIn = true;
                        $rootScope.ShowLoader = false;
                        if ($rootScope.UserClaimSets.some(code => code.Value === 'Driver')){
                            $scope.DashbaordType = 1;
                
                        }
                        if ($rootScope.UserClaimSets.some(code => code.Value === 'Director')){
                            $scope.DashbaordType = 4;
                        }
                    }
                }
                else {
                    $rootScope.ShowLoader = false;
                    $state.go("login", null, { reload: true });
                }
            });
        };       
    }]);