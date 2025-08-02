app.controller('StudentPickupRequestListController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout) {
    console.log('Student pickup request list controller loaded.');

    $scope.StudentsPickupRequests = [];
    $scope.ShowPreLoader = true;

    $scope.init = function () {

        showOverlay();
        $.ajax({
            type: 'GET',
            url: utility.myHost + "Home/GetStudentPickupRequests",
            success: function (result) {
                if (!result.IsError) {
                    $scope.$apply(function () {
                        $scope.StudentsPickupRequests = result.Response;
                    });                    
                }

                hideOverlay();
                $scope.ShowPreLoader = false;
            }
        });
    };

    $scope.NewStudentPickupRequestClick = function () {
        window.location = "StudentPickupRequest";
    };

    $scope.EditButtonClick = function () {

    };

    $scope.CancelButtonClick = function (request) {

        var pickupRequestID = request.StudentPickupRequestIID;

        if (!pickupRequestID) {
            return false;
        }

        showOverlay();

        $.ajax({
            type: "POST",
            url: utility.myHost + "Home/CancelStudentPickupRequestByID?pickupRequestID=" + pickupRequestID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.IsError == true) {
                    callToasterPlugin('error', result.Response);

                    hideOverlay();
                    return false;
                }
                else {
                    callToasterPlugin('success', result.Response);

                    $timeout(function () {
                        $scope.init();
                    }, 1000);
                }
            }
        });
    };

    function showOverlay() {
        $("#StudentPickupRequestListOverlay").fadeIn();
        $("#StudentPickupRequestListOverlayButtonLoader").fadeIn();
    };

    function hideOverlay() {
        $("#StudentPickupRequestListOverlay").fadeOut();
        $("#StudentPickupRequestListOverlayButtonLoader").fadeOut();
    };

}]);