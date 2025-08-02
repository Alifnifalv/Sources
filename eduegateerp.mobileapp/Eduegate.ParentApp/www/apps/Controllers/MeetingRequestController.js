app.controller("MeetingRequestController", [
    '$scope', '$http', '$location', '$rootScope', '$timeout', '$state', 'GetContext', 'rootUrl',
    function ($scope, $http, $location, $rootScope, $timeout, $state, GetContext, rootUrl) {

        var context = GetContext.Context();
        var dataService = rootUrl.SchoolServiceUrl;
        var appDataUrl = rootUrl.RootUrl;
        $scope.ContentService = rootUrl.ContentServiceUrl;


        $rootScope.ShowLoader = true;
        $scope.SignUpDetails = [];
        $scope.Students = [];
        $scope.Employees = [];
        $scope.IsShowSlotDetails = false;
        $scope.IsError = false;
        $scope.ErrorMessage = "";
        var dateFormat = null;

        $scope.init = function (model) {
            $scope.MeetingRequest = model || {};
            $scope.GetParentStudentsDetails();
            $scope.FillLookupsAndSettings();

        };

        // Fill lookup values like Faculty Types
        $scope.FillLookupsAndSettings = function () {
            $http({
                method: 'GET',
                url: appDataUrl + '/GetSettingValueByKey?settingKey=' + 'DateFormat',
                headers: {
                    "Accept": "application/json;charset=UTF-8",
                    "Content-type": "application/json; charset=utf-8",
                    "CallContext": JSON.stringify(context)
                }
            }).then(function (result) {
                dateFormat = result.data;
            });

            $scope.FacultyTypes = [
                { "Key": 1, "Value": "Principal / Head Mistress" },
                { "Key": 2, "Value": "Vice-principal" },
                { "Key": 3, "Value": "Head teacher" },
                { "Key": 4, "Value": "Class coordinator" },
                { "Key": 5, "Value": "Associate teacher" },
                { "Key": 6, "Value": "Other teacher" }
            ];
        };

        // Fetch Parent's students details
        $scope.GetParentStudentsDetails = function () {
            $http({
                method: 'GET',
                url: `${dataService}/GetParentStudents`,
                headers: {
                    "Accept": "application/json;charset=UTF-8",
                    "Content-type": "application/json; charset=utf-8",
                    "CallContext": JSON.stringify(context)
                }
            }).then(function (result) {
                $scope.StudentDetails = result.data;
                $rootScope.ShowLoader = false;

                if ($scope.StudentDetails.length === 1) {

                    $scope.MeetingRequest.Student = {
                        "Key": $scope.StudentDetails[0].StudentIID,
                        "Value": $scope.StudentDetails[0].StudentFullName
                    };

                    $scope.MeetingRequest.SchoolID = $scope.StudentDetails[0].SchoolID;
                    $scope.MeetingRequest.AcademicYearID = $scope.StudentDetails[0].AcademicYearID;
                    $scope.MeetingRequest.ClassID = $scope.StudentDetails[0].ClassID;
                    $scope.MeetingRequest.SectionID = $scope.StudentDetails[0].SectionID;

                    $scope.EnableStudentButton($scope.MeetingRequest.Student);
                }
            });
        };

        // Change FacultyType and load appropriate data
        $scope.OnFacultyTypeChanges = function () {
            $scope.Employees = [];
            $scope.IsShowSlotDetails = false;
            var facultyTypeID = $scope.SelectedFacultyType.Key;
            switch (facultyTypeID) {
                case 1: $scope.FillPrincipalOrMistress(); break;
                case 2: $scope.FillVicePrincipal(); break;
                case 3: $scope.FillHeadTeacher(); break;
                case 4: $scope.FillClassCoordinator(); break;
                case 5: $scope.FillAssociateTeacher(); break;
                case 6: $scope.FillOtherTeachers(); break;
            }
        };

        // Call respective API to fill the employee data
        $scope.FillPrincipalOrMistress = function () {
            $scope.FillEmployeeDetails("GetSchoolPrincipalOrHeadMistress");
        };

        $scope.FillVicePrincipal = function () {
            $scope.FillEmployeeDetails("GetSchoolWisePrincipal");
        };

        $scope.FillHeadTeacher = function () {
            $scope.FillEmployeeDetails("GetClassHeadTeacher", true);
        };

        $scope.FillClassCoordinator = function () {
            $scope.FillEmployeeDetails("GetClassCoordinator", true);
        };

        $scope.FillAssociateTeacher = function () {
            $scope.FillAssociateTeacher("GetAssociateTeachers", true);
        };

        $scope.FillOtherTeachers = function () {
            $scope.FillOtherTeachers("GetOtherTeachersByClass", true);
        };

        // Function to fill employee details based on class (if required)
        $scope.FillEmployeeDetails = function (endpoint, useClassParams) {
            var params = `?schoolID=${$scope.MeetingRequest.SchoolID}`;
            if (useClassParams) {
                params += `&classID=${$scope.MeetingRequest.ClassID}&sectionID=${$scope.MeetingRequest.SectionID}&academicYearID=${$scope.MeetingRequest.AcademicYearID}`;
            }

            $http({
                method: 'GET',
                url: `${dataService}/${endpoint}${params}`,
                headers: {
                    "Accept": "application/json;charset=UTF-8",
                    "Content-type": "application/json; charset=utf-8",
                    "CallContext": JSON.stringify(context)
                }
            }).then(function (result) {
                if (!result.data.IsError) {
                    var empKeyValue = result.data;
                    $scope.MeetingRequest.Faculty = empKeyValue;
                    $scope.Employees.push(empKeyValue);
                    $scope.OnEmployeeChanges();
                }
                $rootScope.ShowLoader = false;
            });
        };

        // Change employee and load available time slots
        $scope.OnEmployeeChanges = function () {
            $scope.IsShowSlotDetails = true;
            $scope.MeetingSlots = [];
            $scope.FillTimeSlotsForEmployee();
        };


        // Fetch available time slots for selected employee
        $scope.FillTimeSlotsForEmployee = function () {
            var employeeID = $scope.MeetingRequest.Faculty.Key;
            var requestedSlotDateString = $scope.MeetingRequest.RequestedDateString;

            if (!employeeID || !requestedSlotDateString) return;

            $http({
                method: 'GET',
                url: `${dataService}/GetMeetingRequestSlotsByEmployeeID?employeeID=${employeeID}&reqSlotDateString=${requestedSlotDateString}`,
                headers: {
                    "Accept": "application/json;charset=UTF-8",
                    "Content-type": "application/json; charset=utf-8",
                    "CallContext": JSON.stringify(context)
                }
            }).then(function (result) {
                $scope.MeetingSlots = [];
                var signUps = result.data;

                signUps.forEach(sign => {
                    sign.SignupSlotMaps.forEach(map => {
                        map.SignupSlotMapTimes.forEach(time => {
                            $scope.MeetingSlots.push(time);
                        });
                    });
                });

                $rootScope.ShowLoader = false;
            }).catch(function () {
                $scope.IsError = true;
                $scope.ErrorMessage = "Failed to load time slots. Please try again.";
            });
        };
        // Fetch associate teachers
        $scope.FillAssociateTeacher = function () {
            var classID = $scope.MeetingRequest.ClassID;
            var sectionID = $scope.MeetingRequest.SectionID;
            var academicYearID = $scope.MeetingRequest.AcademicYearID;

            if (!classID || !sectionID || !academicYearID) {
                return;
            }

            $rootScope.ShowLoader = true;

            $http({
                method: 'GET',
                url: `${dataService}/GetAssociateTeachers`,
                headers: {
                    "Accept": "application/json;charset=UTF-8",
                    "Content-type": "application/json; charset=utf-8"
                },
                params: {
                    classID: classID,
                    sectionID: sectionID,
                    academicYearID: academicYearID
                }
            }).then(function (result) {
                if (!result.data.IsError) {
                    $scope.Employees = result.data;
                }
                $rootScope.ShowLoader = false;
            });
        };

        // Fetch other teachers
        $scope.FillOtherTeachers = function () {
            var classID = $scope.MeetingRequest.ClassID;
            var sectionID = $scope.MeetingRequest.SectionID;
            var academicYearID = $scope.MeetingRequest.AcademicYearID;

            if (!classID || !sectionID || !academicYearID) {
                return;
            }

            $rootScope.ShowLoader = true;

            $http({
                method: 'GET',
                url: `${dataService}/GetOtherTeachersByClass`,
                headers: {
                    "Accept": "application/json;charset=UTF-8",
                    "Content-type": "application/json; charset=utf-8"
                },
                params: {
                    classID: classID,
                    sectionID: sectionID,
                    academicYearID: academicYearID
                }
            }).then(function (result) {
                if (!result.data.IsError) {
                    $scope.Employees = result.data;

                }
                $rootScope.ShowLoader = false;
            });
        };



        // Handle student selection change
        $scope.StudentSelectionChange = function (student) {

            $scope.MeetingRequest.Student = {
                "Key": student.StudentIID,
                "Value": student.StudentFullName
            };

            $scope.MeetingRequest.SchoolID = student.SchoolID;
            $scope.MeetingRequest.AcademicYearID = student.AcademicYearID;
            $scope.MeetingRequest.ClassID = student.ClassID;
            $scope.MeetingRequest.SectionID = student.SectionID;

            $scope.EnableStudentButton($scope.MeetingRequest.Student);

            $scope.OnFacultyTypeChanges();

            $scope.IsError = false;
            $scope.ErrorMessage = "";
        };
        $scope.EnableStudentButton = function (student) {
            $timeout(function () {
                $scope.$apply(function () {

                    var radioButton = document.getElementById("student_" + student.Key);
                    if (radioButton) {
                        radioButton.checked = true;
                    }
                });
            });
        };

        $scope.OnRequestDateChanges = function () {
            // Parse the selected date and today's date for comparison with specified format
            var requestedDate = moment($scope.MeetingRequest.RequestedDateString, 'DD/MM/YYYY', true);
            var today = moment().startOf('day'); // Set to midnight for accurate comparison

            // Check if the requested date is today or in the past
            if (!requestedDate.isValid() || !requestedDate.isAfter(today)) {
                $scope.IsError = true;
                $scope.ErrorMessage = "Error: The requested date cannot be today or in the past.";
                $scope.MeetingRequest.RequestedDateString = null; // Clear the date input if invalid
                return;
            }

            // Clear error if the date is valid
            $scope.IsError = false;
            $scope.ErrorMessage = "";

            // Proceed with the slot details if the date is valid
            $scope.IsShowSlotDetails = true;
            $scope.MeetingSlots = [];
            var requestedSlotDateString = requestedDate.format('DD/MM/YYYY');
            $scope.FillTimeSlotsForEmployee();
        };


        // Handle slot selection
        $scope.SlotSelectionChange = function (slot) {
            $scope.MeetingRequest.RequestedSignupSlotMapID = slot.SignupSlotMapIID;
            $scope.IsError = false;
            $scope.ErrorMessage = "";
        };

        // Confirm and submit meeting request
        $scope.ConfirmAndRequestSlot = function () {
            var meetingRequest = angular.copy($scope.MeetingRequest);

            if (!meetingRequest.Student.Key) {
                $scope.IsError = true;
                $scope.ErrorMessage = "Submission by the student is required!";
                return true;
            }
            else if (!meetingRequest.Faculty.Key) {
                $scope.IsError = true;
                $scope.ErrorMessage = "Faculty is required to submission!";
                return true;
            }
            else if (!meetingRequest.RequestedDateString || meetingRequest.RequestedDateString == "?") {
                $scope.IsError = true;
                $scope.ErrorMessage = "Date is required for submission!";
                return true;
            }
            else {

                var submitButton = document.getElementById("MeetingRequestSubmissionButton");
                submitButton.innerHTML = "Submitting...";
                submitButton.disabled = true;

              meetingRequest.RequestedDateString = moment(meetingRequest.RequestedDateString, "DD/MM/YYYY").format(dateFormat.toUpperCase());

            $http({
                method: 'POST',
                url: `${dataService}/SubmitMeetingRequest`,
                data: meetingRequest,
                headers: {
                    "Accept": "application/json;charset=UTF-8",
                    "Content-type": "application/json; charset=utf-8",
                    "CallContext": JSON.stringify(context)
                }
            }).then(function (result) {
                if (result.data.operationResult == 1) {
                    $rootScope.ShowToastMessage(result.data.Message, 'success');
                     submitButton.innerHTML = "Submit";
                    submitButton.disabled = false;
                    $state.go("meetingrequestlist");
                } else {
                    $rootScope.ShowToastMessage(result.data.Message);
                }
            }).catch(function (result) {
                $scope.IsError = true;
                $scope.ErrorMessage = result.data.Message;
            });
        };
    }
    }
]);
