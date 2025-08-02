app.controller("MeetingFollowupController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    var countdownInterval;

    $scope.init = function (model, windowname) {

        $scope.ParentRemarks = null;
        $scope.TeacherRemarks = null;

        $scope.GetActiveSignups();
        hideOverlay();
    }

    function showOverlay() {
        $('.preload-overlay', $('#MeetingFollowups')).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $('#MeetingFollowups')).hide();
    }

    $scope.MeetingSlotSelection = function (event, timeMap, slotMap, signup) {

        $scope.SelectedSignupDetail = signup;
        $scope.SelectedSlotMapDetail = slotMap;

        $scope.SelectedSlotTimeDetail = timeMap;
        $scope.SelectedSlotAllocationDetail = timeMap.SignupSlotAllocationMaps.length > 0 ? timeMap.SignupSlotAllocationMaps[0] : null;

        $scope.SelectedSlotRemarkMapDetail = $scope.SelectedSlotAllocationDetail != null ? $scope.SelectedSlotAllocationDetail.SignupSlotRemarkMap : timeMap.SignupSlotAllocationRemarkMap;

        var url = "Signups/Signup/FollowupDetails";
        var name = "Follow-up Details";

        $scope.LoadScreen(event, url, name);
    }

    $scope.LoadScreen = function (event, url, name) {
        var loadingTemplate = '<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:black;"></span></center>';

        $(".smartMenus li a").removeClass("active");
        $(event.currentTarget).addClass("active");
        if (event != undefined) {
            event.preventDefault()
            if (event.stopPropagation) event.stopPropagation()
        }

        if ($(event.currentTarget).hasClass('brcolor') == true) { return }
        $('.smartmenu_Content_Inner').html(loadingTemplate);
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $('.smartmenu_Content_Inner').html($compile(result.data)($scope));
            }, function (error) {
                $().showMessage($scope, $timeout, true, error);
                $('.preload-overlay', $(windowContainer)).fadeOut(500)
            });

    }

    $scope.GetActiveSignups = function (viewModel) {
        showOverlay();
        var url = "Signups/Signup/GetEmployeesActiveSignups";
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (!result.data.IsError) {

                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.ActiveSignups = result.data.Response;
                        });
                    });
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.RemarkEntryChanges = function (entry, type) {
        showOverlay();
        var url = "Signups/Signup/GetEmployeesActiveSignups";
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (!result.data.IsError) {

                    $scope.ActiveSignups = result.data.Response;
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.SaveMeetingSlotRemarks = function () {

        if (!$scope.SelectedSlotRemarkMapDetail.SignupSlotAllocationMapID) {
            $().showGlobalMessage($root, $timeout, true, "Remark entry is only allowed when someone has been allocated.!", 3000);
            return false;
        }

        var signupSlotRemarkMap = $scope.SelectedSlotRemarkMapDetail;

        showOverlay();

        $.ajax({
            url: utility.myHost + "Signups/Signup/SaveSignupSlotRemarks",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(signupSlotRemarkMap),
            success: function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, "Saving failed!");
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, "Saved successfully!");

                    //$timeout(function () {
                    //    $scope.$apply(function () {
                    //        $scope.GetActiveSignups();
                    //    });
                    //});
                }
                hideOverlay();
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    // To get meeting end time (countdown time) start
    function startTimer() {
        var endTime = dateTimeStringToTimestamp($scope.SelectedSlotTimeDetail.SlotDate, $scope.SelectedSlotTimeDetail.EndTime);
        $scope.IsTimeExpiring = false;

        countdownInterval = setInterval(function () {
            try {
                var currentDate = new Date();
                var now = currentDate.getTime();

                // Find the distance between now and the end date
                var distance = endTime - now;

                // Time calculations for days, hours, minutes, and seconds
                var days = Math.floor(distance / (1000 * 60 * 60 * 24));
                var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                var seconds = Math.floor((distance % (1000 * 60)) / 1000);

                // Display the remaining time with days included
                $scope.$apply(function () {
                    if (days > 0) {
                        $scope.ExpiryTime = days + "d " + hours + "h " + minutes + "m " + seconds + "s";
                    }
                    else if (days == 0 && hours > 0) {
                        $scope.ExpiryTime = hours + "h " + minutes + "m " + seconds + "s";
                    }
                    else {
                        $scope.ExpiryTime = minutes + "m " + seconds + "s";
                    }
                });

                if ((hours == 0 && minutes == 10 && seconds == 0) || (hours == 0 && minutes == 5 && seconds == 0)) {
                    $scope.$apply(function () {
                        $scope.MeetingEndBalanceTime = $scope.ExpiryTime;
                    });
                    $scope.LoadTimeAlert();
                    $scope.IsTimeExpiring = true;
                }

                $scope.$apply(function () {
                    $scope.MeetingEndBalanceTime = $scope.ExpiryTime;
                });

                if (distance < 0) {
                    clearInterval(countdownInterval);

                    $scope.$apply(function () {
                        $scope.ExpiryTime = "EXPIRED";
                        $scope.IsTimeExpiring = true;
                    });

                    return true;
                } else if (hours == 0 && minutes <= 10 && seconds == 0) {
                    $scope.IsTimeExpiring = true;
                }
            } catch (error) {
                clearInterval(countdownInterval);
            }

        }, 1000);
    }

    // Modified function to handle date and time together
    function dateTimeStringToTimestamp(dateString, timeString) {
        // Create a Date object from the date string
        let date = new Date(dateString);

        // Split the time string into hours, minutes, and seconds
        let [hours, minutes, seconds] = timeString.split(':').map(Number);

        // Set the time on the date object
        date.setHours(hours, minutes, seconds, 0); // set milliseconds to 0

        // Convert the Date object to a timestamp in milliseconds
        return date.getTime();
    }

    $scope.LoadTimeAlert = function () {
        var toastElList = [].slice.call(document.querySelectorAll('.toast'));
        var toastList = toastElList.map(function (toastEl) {
            return new bootstrap.Toast(toastEl);
        });
        toastList.forEach(toast => toast.show());
    };

    $scope.LoadMeetingEndCountdown = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.IsTimeExpiring = false;
                $scope.ExpiryTime = null;
                clearInterval(countdownInterval);
            });

            startTimer();
        }, 0);
    };

    // To get meeting end time (countdown time) end

}]);