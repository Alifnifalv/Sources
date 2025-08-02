app.controller("MeetingRemarksController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("Meeting Remarks controller loaded");

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
        $scope.FillAllotedMeetings();
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

    $scope.FillAllotedMeetings = function () {
        showOverlay();

        $.ajax({
            url: utility.myHost + "SignUp/GetParentAllotedMeetings?parentID=" + $scope.ParentID,
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.AllottedMeetings = result.Response;

                            hideOverlay();
                        });

                    });
                }

                $scope.ShowPreLoader = false;
            }
        });
    };

    $scope.SubmitRemarkEntry = function (slotDet) {

        var remarkEntry = slotDet.SignupSlotRemarkMap;

        if (!remarkEntry.ParentRemarks) {
            $().showGlobalMessage($root, $timeout, true, "Enter a remark before submitting!", 2000);
            return true;
        }        
        else {
            $.ajax({
                url: utility.myHost + "SignUp/SubmitMeetingRemarks",
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(remarkEntry),
                success: function (result) {
                    if (result.IsError) {
                        $().showGlobalMessage($root, $timeout, true, "Saving failed!");
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, false, "Saved successfully!", 2000);

                        $timeout(function () {
                            $scope.$apply(function () {
                                $scope.FillAllotedMeetings();
                            });

                        });
                    }

                    hideOverlay();
                },
                complete: function (result) {
                }
            });
        }
    };

}]);