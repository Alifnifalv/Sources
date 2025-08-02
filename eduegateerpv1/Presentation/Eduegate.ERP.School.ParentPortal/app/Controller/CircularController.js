app.controller('CircularController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout) {
    console.log('Circular Controller loaded.');

    $scope.CircularList = [];

    $scope.ShowPreLoader = true;

    $scope.init = function () {

        showOverlay();

        $.ajax({
            type: "GET",
            url: utility.myHost + "/Home/GetCircularList",
            success: function (result) {
                $timeout(function () {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.CircularList = result.Response;

                        hideOverlay();

                        $scope.ShowPreLoader = false;
                    }
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

    $scope.DownloadURL = function (url) {
        var link = document.createElement("a");
        link.href = url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };

    $scope.GetExpiryDateDifference = function (circularData) {
        
        var currentDate = new Date();
        var circularExpiryDate = new Date(moment(circularData.ExpiryDate, 'DD/MM/YYYY'));

        const diffTime = Math.abs(circularExpiryDate - currentDate);
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

        return diffDays;
    };

    function showOverlay() {
        $("#CircularListOverlay").fadeIn();
        $("#CircularListOverlayButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#CircularListOverlay").fadeOut();
        $("#CircularListOverlayButtonLoader").fadeOut();
    }

}]);