app.controller('CoachingFormListController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout) {
    console.log('Coaching form list controller controller loaded.');

    $scope.StudentsCoachingForms = [];
    $scope.ShowPreLoader = true;

    $scope.init = function () {

        showOverlay();
        $.ajax({
            type: 'GET',
            url: utility.myHost + "FormBuilder/GetCoachingFormList",
            success: function (result) {
                if (!result.IsError) {
                    $scope.$apply(function () {
                        $scope.StudentsCoachingForms = result.Response;
                    });                    
                }

                hideOverlay();
                $scope.ShowPreLoader = false;
            }
        });
    };

    $scope.NewCoachingFormButtonClick = function () {
        window.location = "CoachingForm";
    };

    function showOverlay() {
        $("#CoachingFormListOverlay").fadeIn();
        $("#CoachingFormListOverlayButtonLoader").fadeIn();
    };

    function hideOverlay() {
        $("#CoachingFormListOverlay").fadeOut();
        $("#CoachingFormListOverlayButtonLoader").fadeOut();
    };

}]);