app.controller("MeetingRequestListController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("MeetingRequestListController Loaded");

    $scope.MeetingRequests = [];

    function showOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#MeetingRequestListOverlay").fadeIn();
            });
        });
    }

    function hideOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#MeetingRequestListOverlay").fadeOut();
            });
        });
    }

    $scope.Init = function () {
        $scope.ShowPreLoader = true;

        $scope.GetUserDetails();
        $scope.FillMeetingRequests();
    };

    $scope.NewRequestButtonClick = function () {
        window.location.href = utility.myHost + "SignUp/MeetingRequest";
    };

    $scope.GetUserDetails = function () {
        $.ajax({
            url: utility.myHost + "Home/GetUserDetails",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $timeout(function () {
                        $scope.$apply(function () {

                            $scope.UserDetails = result.Response;
                            $scope.ParentID = $scope.UserDetails != null && $scope.UserDetails.Parent != null ? $scope.UserDetails.Parent.ParentIID : null;
                        });
                    });
                }
            }
        });
    };

    $scope.FillMeetingRequests = function () {
        showOverlay();

        $.ajax({
            url: utility.myHost + "SignUp/GetMeetingRequestsByParentID?parentID=" + $scope.ParentID,
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.MeetingRequests = result.Response;

                            hideOverlay();
                        });

                    });
                }

                $scope.ShowPreLoader = false;
            }
        });
    };

}]);