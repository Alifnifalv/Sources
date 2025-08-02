app.controller('SiblingApplicationListController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout) {
    console.log('Sibling Application List Controller loaded.');

    $scope.Applications = [];

    $scope.ShowPreLoader = true;

    $scope.init = function () {

        showOverlay();

        $.ajax({
            type: 'GET',
            url: utility.myHost + "/Home/UserApplications",
            success: function (result) {
                $timeout(function () {
                    $scope.$apply(function () {
                        $scope.Applications = result.Response;

                        //console.log($scope.Applications);

                        hideOverlay();
                        $scope.ShowPreLoader = false;
                    });
                });
            },
            error: function () {
                hideOverlay();
                $scope.ShowPreLoader = false;
            },
            complete: function (result) {
                hideOverlay();
                $scope.ShowPreLoader = false;
            }
        });
    };

    $scope.NewApplicationClick = function () {
        window.location.replace(utility.myHost + "NewApplicationFromSibling?loginID=" + $rootScope.LoginID);
    };

    function showOverlay() {
        $("#ApplicationListOverlay").fadeIn();
        $("#ApplicationListOverlayButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#ApplicationListOverlay").fadeOut();
        $("#ApplicationListOverlayButtonLoader").fadeOut();
    }

}]);