app.controller("SignupController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.ApplyButtonClick = function (viewModel) {
        if (!validateRequiredFields(viewModel)) {
            return false;
        }

        showOverlay();
        $scope.FillGridDetails(viewModel);
    };

    function validateRequiredFields(viewModel) {

        if (!viewModel.FromDateString) {
            showErrorMessage("From date is required to apply!");
            return false;
        } else if (!viewModel.ToDateString) {
            showErrorMessage("To date is required to apply!");
            return false;
        } else if (!viewModel.SignupFilter.StartTimeString) {
            showErrorMessage("Start time is required to apply!");
            return false;
        } else if (!viewModel.SignupFilter.EndTimeString) {
            showErrorMessage("End time is required to apply!");
            return false;
        } else if (!viewModel.SignupFilter.Duration) {
            showErrorMessage("Duration is required to apply!");
            return false;
        } else if (moment(viewModel.FromDateString, 'DD/MM/YYYY').toDate() > moment(viewModel.ToDateString, 'DD/MM/YYYY').toDate()) {
            showErrorMessage("The to date must be greater than or equal to the from date.");
            return false;
        } else if ($scope.TimeConvertTo24HoursString(viewModel.SignupFilter.StartTimeString) > $scope.TimeConvertTo24HoursString(viewModel.SignupFilter.EndTimeString)) {
            showErrorMessage("The end time must be greater than or equal to the start time.");
            return false;
        } else if (viewModel.SignupFilter.SignupSlotType == viewModel.SignupFilter.RecurringSlotTypeID) {
            if (viewModel.SignupFilter.WeekDays.length <= 0) {
                showErrorMessage("At least one week day is required for the recurring type.");
                return false;
            }
        }
        return true;
    }

    function showErrorMessage(message) {
        $().showMessage($scope, $timeout, true, message);
    }

    $scope.ValidateDigitsOnly = function (model) {
        // Regular expression to check if data contains any non-digit characters
        const nonDigitPattern = /\D/;

        // Check if data matches the pattern for non-digit characters
        if (nonDigitPattern.test(model.Duration)) {
            showErrorMessage("Only digits are allowed in the duration field.");
            model.Duration = parseInt(model.Duration);
            return false;
        }
        else {
            return true;
        }
    };

    $scope.FillGridDetails = function (model) {

        model.SignupSlotMaps = [];

        var durationInMinutes = model.SignupFilter.Duration ? parseInt(model.SignupFilter.Duration) : 0;
        var bufferTimeInMinutes = model.SignupFilter.BufferTime ? parseInt(model.SignupFilter.BufferTime) : 0;

        var startTimeString = $scope.TimeConvertTo24HoursString(model.SignupFilter.StartTimeString);
        var endTimeString = $scope.TimeConvertTo24HoursString(model.SignupFilter.EndTimeString);

        const fromDate = moment(model.FromDateString, 'DD/MM/YYYY').toDate();
        const toDate = moment(model.ToDateString, 'DD/MM/YYYY').toDate();
        const startTime = new Date("1970-01-01T" + startTimeString);
        const endTime = new Date("1970-01-01T" + endTimeString);

        var selectedWeekdays = [];
        model.SignupFilter.WeekDays.forEach(day => {
            selectedWeekdays.push(day.Value);
        });

        for (let currentDate = new Date(fromDate); currentDate <= toDate; currentDate.setDate(currentDate.getDate() + 1)) {

            if (selectedWeekdays.length > 0) {
                var dayShortName = currentDate.toLocaleDateString('en-US', { weekday: 'short' });
                if (!selectedWeekdays.includes(dayShortName)) {
                    continue;
                }
            }

            for (let currentTime = new Date(startTime); currentTime < endTime; currentTime.setMinutes(currentTime.getMinutes() + durationInMinutes + bufferTimeInMinutes)) {

                var slotStartTime = new Date(currentDate);
                slotStartTime.setHours(currentTime.getHours());
                slotStartTime.setMinutes(currentTime.getMinutes());

                var slotEndTime = new Date(slotStartTime);
                slotEndTime.setMinutes(slotEndTime.getMinutes() + durationInMinutes);

                // Push Values to Grid Variable
                model.SignupSlotMaps.push({
                    "SignupSlotMapIID": 0,
                    "SignupSlotType": model.SignupFilter.SignupSlotType,
                    "SlotDateString": moment(currentDate).format("DD/MM/YYYY"),
                    "StartTimeString": moment(slotStartTime).format("hh:mm A"),
                    "EndTimeString": moment(slotEndTime).format("hh:mm A"),
                    "Duration": durationInMinutes,
                    "SlotMapStatus": model.SignupFilter.SlotMapStatusID.toString(),
                });
            }
        }
        hideOverlay();
    };

    $scope.TimeConvertTo24HoursString = function (time12h) {
        const [time, modifier] = time12h.split(' ');
        let [hours, minutes] = time.split(':');

        if (modifier.toLowerCase() === 'am') {
            if (hours === '12') {
                hours = '00';
            }
        } else {
            if (hours !== '12') {
                hours = (parseInt(hours, 10) + 12).toString();
            }
        }

        // Add leading zero if hours are single digit
        if (hours.length === 1) {
            hours = '0' + hours;
        }

        return hours + ':' + minutes;
    };

    $scope.DateChanges = function (viewModel) {

        var currentDate = new Date(); // Get the current date and time
        currentDate.setHours(0, 0, 0, 0); // Set time as zero

        if (!viewModel.FromDateString) {
            return false;
        }
        else if (moment(viewModel.FromDateString, 'DD/MM/YYYY').toDate() < moment(currentDate, 'DD/MM/YYYY').toDate()) {
            showErrorMessage("The 'From date' should be greater than or equal to today's date.");

            viewModel.FromDateString = null;
            return false;
        }

        if (!viewModel.ToDateString) {
            return false;
        }
        else if (moment(viewModel.ToDateString, 'DD/MM/YYYY').toDate() < moment(currentDate, 'DD/MM/YYYY').toDate()) {
            showErrorMessage("The 'To date' should be greater than or equal to today's date.");

            viewModel.ToDateString = null;
            return false;
        }

        if (viewModel.SignupGroup) {

            if (moment(viewModel.FromDateString, 'DD/MM/YYYY').toDate() < moment(viewModel.GroupDateFromString, 'DD/MM/YYYY').toDate()) {

                showErrorMessage("The 'From date' must be greater than or equal to the 'Group from date' which is " + viewModel.GroupDateFromString);

                viewModel.FromDateString = null;
                return false;
            }
            else if (moment(viewModel.ToDateString, 'DD/MM/YYYY').toDate() > moment(viewModel.GroupDateToString, 'DD/MM/YYYY').toDate()) {

                showErrorMessage("The 'To date' must be less than or equal to the 'Group to date' which is " + viewModel.GroupDateToString);
                viewModel.ToDateString = null;

                return false;
            }
        }

        if (moment(viewModel.ToDateString, 'DD/MM/YYYY').toDate() < moment(viewModel.FromDateString, 'DD/MM/YYYY').toDate()) {
            showErrorMessage("The 'To date' must be greater than or equal to the 'From date'.");

            viewModel.ToDateString = null;
            return false;
        }

        return true;
    };

    $scope.GroupChanges = function (viewModel) {
        if (!viewModel.SignupGroup) {
            return false;
        }
        else {
            var url = "Signups/Signup/GetSignupGroupDetailsByID?signupGroupID=" + viewModel.SignupGroup;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    if (!result.data.IsError) {

                        var resultResponse = result.data.Response;

                        viewModel.GroupDateFromString = resultResponse.FromDateString;
                        viewModel.GroupDateToString = resultResponse.ToDateString;
                    }

                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    };

    $scope.GridDateChanges = function (masterModel, gridModel) {

        if (!gridModel.SlotDateString) {
            return false;
        }

        if (masterModel.SignupGroup) {

            if (moment(gridModel.SlotDateString, 'DD/MM/YYYY').toDate() < moment(masterModel.GroupDateFromString, 'DD/MM/YYYY').toDate()) {

                showErrorMessage("The 'Slot date' must be greater than or equal to the 'Group from date' which is " + masterModel.GroupDateFromString);

                gridModel.SlotDateString = null;
                return false;
            }
            else if (moment(gridModel.SlotDateString, 'DD/MM/YYYY').toDate() > moment(masterModel.GroupDateToString, 'DD/MM/YYYY').toDate()) {

                showErrorMessage("The 'Slot date' must be less than or equal to the 'Group to date' which is " + masterModel.GroupDateToString);
                gridModel.SlotDateString = null;

                return false;
            }
        }

        return true;
    };

    $scope.GridTimeChanges = function (masterModel, gridModel) {

        if (!gridModel.StartTimeString) {
            return false;
        }
        else if (!gridModel.EndTimeString) {
            return false;
        }
        else if (gridModel.EndTimeString < gridModel.StartTimeString) {
            showErrorMessage("The 'End time' should be greater than 'Start time'.");

            gridModel.EndTimeString = null;
            return false;
        }

        if (masterModel.SignupGroup) {

            if (moment(gridModel.SlotDateString, 'DD/MM/YYYY').toDate() < moment(masterModel.GroupDateFromString, 'DD/MM/YYYY').toDate()) {

                showErrorMessage("The 'Slot date' must be greater than or equal to the 'Group from date' which is " + masterModel.GroupDateFromString);

                gridModel.SlotDateString = null;
                return false;
            }
            else if (moment(gridModel.SlotDateString, 'DD/MM/YYYY').toDate() > moment(masterModel.GroupDateToString, 'DD/MM/YYYY').toDate()) {

                showErrorMessage("The 'Slot date' must be less than or equal to the 'Group to date' which is " + masterModel.GroupDateToString);
                gridModel.SlotDateString = null;

                return false;
            }
        }

        if (gridModel.EndTimeString < gridModel.StartTimeString) {
            showErrorMessage("The 'End time' must be greater than the 'Start time'.");

            gridModel.EndTimeString = null;
            return false;
        }

        gridModel.Duration = getDurationInMinutes(gridModel.StartTimeString, gridModel.EndTimeString);

        return true;
    };

    function getDurationInMinutes(fromTime, toTime) {
        // Parse the input time strings
        const from = $scope.TimeConvertTo24Hours(fromTime);
        const to = $scope.TimeConvertTo24Hours(toTime);

        // Create Date objects for the from and to times
        const fromDate = new Date();
        fromDate.setHours(from.hours, from.minutes, 0, 0);

        const toDate = new Date();
        toDate.setHours(to.hours, to.minutes, 0, 0);

        // Calculate the duration in milliseconds
        const durationMs = toDate - fromDate;

        // Convert the duration from milliseconds to minutes
        const durationMinutes = durationMs / 1000 / 60;

        // If the duration is negative, it means the toTime is on the next day
        if (durationMinutes < 0) {
            return (24 * 60) + durationMinutes; // Add 24 hours worth of minutes
        }

        return durationMinutes;
    };

    $scope.TimeConvertTo24Hours = function (timein12Hr) {
        const [timePart, modifier] = timein12Hr.split(' ');
        let [hours, minutes] = timePart.split(':').map(Number);

        if (modifier.toLowerCase() === 'pm' && hours !== 12) {
            hours += 12;
        } else if (modifier.toLowerCase() === 'am' && hours === 12) {
            hours = 0;
        }

        return { hours, minutes };
    }

    $scope.ClassSectionChanges = function (viewModel) {

        var classID = viewModel.Class != null ? viewModel.Class.Key : null;
        var sectionID = viewModel.Section;

        if (!classID) {
            return false;
        }
        else if (!sectionID) {
            return false;
        }
        else {
            var url = "Schools/School/GetClassStudents?classID=" + classID + "&sectionID=" + sectionID;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.LookUps.Students = result.data;

                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    };

    $scope.SignupTypeChanges = function (viewModel) {
        if (!viewModel.SignupType) {
            return false;
        }
        else {

            viewModel.OrganizerEmployee = {
                "Key": null,
                "Value": null
            };

            if (viewModel.SignupType == viewModel.SignupMeetingRequestTypeID) {
                var url = "Payroll/Employee/GetEmployeeDatasByLogin";
                $http({ method: 'Get', url: url })
                    .then(function (result) {

                        if (!result.data.IsError) {

                            var resultResponse = result.data.Response;

                            if (resultResponse.EmployeeIID > 0) {

                                viewModel.OrganizerEmployee = {
                                    "Key": resultResponse.EmployeeIID.toString(),
                                    "Value": resultResponse.EmployeeCode + " - " + resultResponse.EmployeeName
                                };
                            }
                        }

                        hideOverlay();
                    }, function () {
                        hideOverlay();
                    });
            }
        }
    };

}]);