app.controller('MailBoxController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', 
function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce) {

    console.log('MailBoxController loaded.');
    $scope.PageName = "Notifications";

    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();

    $scope.MailBoxes = [];
    $scope.page = 1;
    $scope.pageSize = 20;
    $scope.loading = false; // 👈 Prevent duplicate API calls
    $scope.noMoreData = false; // 👈 Optional: Stop calling when all data loaded

    $scope.init = function () {
        if ($scope.loading || $scope.noMoreData) return;

        $scope.loading = true;
        $rootScope.ShowLoader = true;

        $http({
            method: 'GET',
            url: `${dataService}/GetMyNotification?page=${$scope.page}&pageSize=${$scope.pageSize}`,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            if (result && result.length > 0) {
                $scope.MailBoxes = $scope.MailBoxes.concat(result);
                $scope.page++;
                if (result.length < $scope.pageSize) {
                    $scope.noMoreData = true; // 👈 No more data available
                }
            } else {
                $scope.noMoreData = true; // 👈 No data returned
            }
            $scope.loading = false;
            $rootScope.ShowLoader = false;
        }).error(function () {
            $scope.loading = false;
            $rootScope.ShowLoader = false;
        });
    };

    angular.element(document).ready(function () {
        var container = document.getElementById('mailbox-container');

        if (container) {
            container.addEventListener('scroll', function () {
                if (container.scrollTop + container.clientHeight >= container.scrollHeight - 50) {
                    $scope.$applyAsync(function () {
                        $scope.init(); // 👈 Will be blocked if already loading
                    });
                }
            });
        }

        // Initial load
        $scope.init();
    });
}]);
