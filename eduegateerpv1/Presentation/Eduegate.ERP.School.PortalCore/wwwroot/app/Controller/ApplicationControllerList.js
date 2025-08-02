app.controller('ApplicationControllerList', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout) {
    console.log('ApplicationController controller loaded.');
    $scope.Applications = [];
    $scope.Casts = [];
    $scope.Relegions = [];
    $scope.Genders = [];
    $scope.Classes = [];
    $scope.Categories = [];
    $scope.init = function () {

        $timeout(function () {
            showOverlay();
            $.ajax({
                type: 'GET',
                url: utility.myHost + "Home/UserApplications",
                success: function (result) {
                    $timeout(function () {
                        $scope.Applications = result.Response;
                        console.log($scope.Applications);
                    });

                    hideOverlay();
                }
            });  
        });
    };   

    function showOverlay() {
        $("#ApplicationListOverlay").fadeIn();
        $("#ApplicationListOverlayButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#ApplicationListOverlay").fadeOut();
        $("#ApplicationListOverlayButtonLoader").fadeOut();
    }

}]).filter("filterdate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        var m = x.match(re);
        if (m) return new Date(parseInt(m[1]));
        else return null;
    };
});