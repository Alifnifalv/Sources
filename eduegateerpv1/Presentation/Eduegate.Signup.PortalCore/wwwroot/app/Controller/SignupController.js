app.controller("SignupController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("SignupController Loaded");

    $scope.SignUpDetails = [];
    $scope.FreeSlotCount = 0;
    $scope.Students = [];

    $scope.SelectedStudent = {
        "Key": null,
        "Value": null,
    };

    $scope.IsError = false;
    $scope.ErrorMessage = "";

    $scope.Init = function (screenType, groupID) {

        $scope.ShowPreLoader = true;
        $scope.GroupID = groupID;

        $scope.GetUserDetails();

        if (screenType == "eventDetails") {

            $scope.FillSignUpDetailsByGroupID(groupID);
        }
        //else if (screenType == "event") {

        //    $scope.GetActiveSignUpGroups();
        //}
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

    $scope.FillSignUpDetailsByGroupID = function (groupID) {
        $.ajax({
            url: utility.myHost + "SignUp/FillSignUpDetailsByGroupID?groupID=" + groupID,
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.GroupDetails = result.Response;
                            $scope.SignUpDetails = $scope.GroupDetails.SignUpDTOs;

                            $scope.Students = $scope.GroupDetails.Students;

                            if ($scope.GroupDetails != null) {
                                $scope.FreeSlotCount = 0;
                                $scope.SignUpDetails.forEach(signup => {
                                    $scope.FreeSlotCount += signup.SlotAvailableCount;
                                });
                            }
                        });

                    });
                }

                $scope.ShowPreLoader = false;
            }
        });
    };

    $scope.GetActiveSignUpGroups = function () {
        $.ajax({
            url: utility.myHost + "SignUp/GetActiveSignupGroups",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $scope.$apply(function () {
                        $scope.SignUpGroupDetails = result.Response;
                    });
                }

                $scope.ShowPreLoader = true;
            }
        });
    };

    $scope.SlotSelectionClick = function (timeSlot, slotMap, signUpDetail) {

        $scope.SelectedTimeSlot = timeSlot;
        $scope.SelectedSignupSlot = slotMap;
        $scope.SelectedSignup = signUpDetail;

        if ($scope.Students.length == 1) {
            $scope.SelectedStudent.Key = $scope.Students[0].Key;
            $scope.SelectedStudent.Value = $scope.Students[0].Value;

            var radioButton = document.getElementById("student_" + $scope.SelectedStudent.Key);
            if (radioButton) {
                radioButton.checked = true;
            }
        }
        else {
            $scope.SelectedStudent = {
                "Key": null,
                "Value": null,
            };

            var radioButtons = document.querySelectorAll('[id*="student_"]');
            // Loop through all radio buttons with IDs containing "student_"
            radioButtons.forEach(function (radioButton) {
                radioButton.checked = false; // Uncheck the radio button
            });
        }

        $scope.IsError = false;
        $scope.ErrorMessage = "";

        $('#SlotBookingConfirmationModal').modal('show');
    };

    $scope.StudentSelectionChange = function (student) {
        $scope.SelectedStudent.Key = student.Key;
        $scope.SelectedStudent.Value = student.Value;

        var radioButton = document.getElementById("student_" + student.Key);
        if (radioButton) {
            radioButton.checked = true;
        }

        $scope.IsError = false;
        $scope.ErrorMessage = "";
    };

    $scope.ConfirmAndSaveSlot = function () {

        if (!$scope.SelectedStudent.Key) {
            $scope.IsError = true;
            $scope.ErrorMessage = "Select an student";
            return false;
        }

        var isError = false;
        var errorMessage = "";
        var studentID = parseInt($scope.SelectedStudent.Key);

        $scope.SelectedSignup.SignupSlotMaps.forEach(slot => {
            slot.SignupSlotMapTimes.forEach(time => {
                if (time.ParentID == $scope.ParentID && time.StudentID == studentID) {
                    isError = true;
                    errorMessage = "You have already booked another slot for the same meetup for this student. If you wish to book this slot, please cancel the previously booked one.";
                }
            });
        });

        if (!isError) {
            $scope.SignUpDetails.forEach(signup => {
                signup.SignupSlotMaps.forEach(slot => {
                    slot.SignupSlotMapTimes.forEach(time => {
                        if (time.ParentID == $scope.ParentID && time.StudentID == studentID) {

                            if (time.SlotDateString == $scope.SelectedTimeSlot.SlotDateString) {
                                if (time.StartTimeString == $scope.SelectedTimeSlot.StartTimeString) {
                                    isError = true;
                                    errorMessage = "Unable to book at the same time for multiple meetups within one event for same student.";
                                }
                            }
                        }
                    });
                });
            });
        }

        if (isError) {
            $().showGlobalMessage($root, $timeout, true, errorMessage, 5000);
            $('#SlotBookingConfirmationModal').modal('hide');
        }
        else {

            $('#SlotBookingConfirmationModal').modal('hide');

            var timeSlot = $scope.SelectedTimeSlot;
            timeSlot.StudentID = $scope.SelectedStudent.Key;
            timeSlot.SignupOrganizerEmployeeName = $scope.SelectedSignup.OrganizerEmployeeName;

            $.ajax({
                url: utility.myHost + "SignUp/SaveSelectedSignUpSlot",
                type: "POST",
                data: timeSlot,
                success: function (result) {
                    if (result.IsError) {
                        $().showGlobalMessage($root, $timeout, true, result.Response);
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, false, "Slot booking successful!");
                    }

                    $scope.FillSignUpDetailsByGroupID($scope.GroupID);
                },
                error: function (result) {
                    var error = result;
                }
            });
        }
    };

    $scope.SlotCancelClick = function (timeSlot, slotMap, signUpDetail) {

        $scope.SelectedTimeSlot = timeSlot;
        $scope.SelectedSignupSlot = slotMap;
        $scope.SelectedSignup = signUpDetail;

        $('#SlotCancelConfirmationModal').modal('show');
    };

    $scope.ConfirmAndCancelSlot = function () {

        $('#SlotCancelConfirmationModal').modal('hide');

        var timeSlot = $scope.SelectedTimeSlot;

        $.ajax({
            url: utility.myHost + "SignUp/CancelSelectedSignUpSlot",
            type: "POST",
            data: timeSlot,
            success: function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.Response, 3000);
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, "Slot cancellation successful!");
                }

                $scope.FillSignUpDetailsByGroupID($scope.GroupID);
            },
            error: function (result) {
                var error = result;
            }
        });
    };

    $scope.ExpandCollapase = function (event, model, field) {
        model[field] = !model[field];
    };

    $scope.GroupViewClick = function (groupDetails) {
        window.location.replace(utility.myHost + "SignUp/EventDetails?groupID=" + groupDetails.SignupGroupID);
    }

}]);