app.controller("MeetingRequestController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("MeetingRequestController Loaded");

    $scope.SignUpDetails = [];
    $scope.Students = [];

    $scope.IsShowSlotDetails = false;

    $scope.Employees = [];

    $scope.IsError = false;
    $scope.ErrorMessage = "";
    var dateFormat = null;

    function showOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#FeePaymentOverlay").fadeIn();
            });
        });
    }

    function hideOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#FeePaymentOverlay").fadeOut();
            });
        });
    }

    $scope.Init = function (model) {

        $scope.MeetingRequest = model;

        $scope.ShowPreLoader = true;

        $scope.GetParentStudentsDetails();

        $scope.FillLookupsAndSettings();
    };

    $scope.FillLookupsAndSettings = function () {

        $http({
            method: 'Get', url: utility.myHost + "Setting/GetSettingValueByKey?settingKey=" + "DateFormat",
        }).then(function (result) {
            dateFormat = result.data;
        });

        $scope.FacultyTypes = [
            {
                "Key": 1,
                "Value": "Principal / Head Mistress"
            },
            {
                "Key": 2,
                "Value": "Vice-principal"
            },
            {
                "Key": 3,
                "Value": "Class teacher"
            },
            {
                "Key": 4,
                "Value": "Class coordinator"
            },
            {
                "Key": 5,
                "Value": "Associate teacher"
            },
            {
                "Key": 6,
                "Value": "Other teacher"
            }
        ];

    };

    $scope.GetParentStudentsDetails = function () {
        $.ajax({
            url: utility.myHost + "Home/GetParentStudents",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.StudentDetails = result.Response;

                            if ($scope.StudentDetails.length == 1) {

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

                    });
                }

                $scope.ShowPreLoader = false;
            }
        });
    };

    $scope.OnFacultyTypeChanges = function () {

        $scope.IsError = false;
        $scope.ErrorMessage = "";

        var facultyTypeID = $scope.SelectedFacultyType != null ? $scope.SelectedFacultyType.Key : null;
        var studentID = $scope.MeetingRequest.Student != null ? $scope.MeetingRequest.Student.Key : null;

        if (!facultyTypeID) {
            return true;
        }

        if (!studentID) {
            $scope.IsError = true;
            $scope.ErrorMessage = "Select a student!";
            return true;
        }

        $scope.Employees = [];

        $timeout(function () {
            $scope.$apply(function () {
                $scope.IsShowSlotDetails = false;
            });
        });

        if (facultyTypeID == 1) {
            $scope.FillPrincipalOrMistress();
        }
        else if (facultyTypeID == 2) {
            $scope.FillVicePrincipal();
        }
        else if (facultyTypeID == 3) {
            $scope.FillHeadTeacher();
        }
        else if (facultyTypeID == 4) {
            $scope.FillClassCoordinator();
        }
        else if (facultyTypeID == 5) {
            $scope.FillAssociateTeacher();
        }
        else if (facultyTypeID == 6) {
            $scope.FillOtherTeachers();
        }

    };

    $scope.FillPrincipalOrMistress = function () {

        var url = utility.myHost + "Signup/GetSchoolPrincipalOrHeadMistress?schoolID=" + $scope.MeetingRequest.SchoolID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (!result.data.IsError) {

                    var empKeyValue = result.data.Response;
                    $scope.MeetingRequest.Faculty = empKeyValue;

                    $scope.Employees.push(empKeyValue);

                    $scope.OnEmployeeChanges();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillVicePrincipal = function () {
        var url = utility.myHost + "Signup/GetSchoolWisePrincipal?schoolID=" + $scope.MeetingRequest.SchoolID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (!result.data.IsError) {

                    var empKeyValue = result.data.Response;
                    $scope.MeetingRequest.Faculty = empKeyValue;

                    $scope.Employees.push(empKeyValue);

                    $scope.OnEmployeeChanges();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillHeadTeacher = function () {
        var classID = $scope.MeetingRequest.ClassID;
        var sectionID = $scope.MeetingRequest.SectionID;
        var academicYearID = $scope.MeetingRequest.AcademicYearID;

        var url = utility.myHost + "Signup/GetClassHeadTeacher?classID=" + classID + "&sectionID=" + sectionID + "&academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (!result.data.IsError) {

                    var empKeyValue = result.data.Response;
                    $scope.MeetingRequest.Faculty = empKeyValue;

                    $scope.Employees.push(empKeyValue);

                    $scope.OnEmployeeChanges();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillClassCoordinator = function () {
        var classID = $scope.MeetingRequest.ClassID;
        var sectionID = $scope.MeetingRequest.SectionID;
        var academicYearID = $scope.MeetingRequest.AcademicYearID;

        var url = utility.myHost + "Signup/GetClassCoordinator?classID=" + classID + "&sectionID=" + sectionID + "&academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (!result.data.IsError) {

                    var empKeyValue = result.data.Response;
                    $scope.MeetingRequest.Faculty = empKeyValue;

                    $scope.Employees.push(empKeyValue);

                    $scope.OnEmployeeChanges();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillAssociateTeacher = function () {
        var classID = $scope.MeetingRequest.ClassID;
        var sectionID = $scope.MeetingRequest.SectionID;
        var academicYearID = $scope.MeetingRequest.AcademicYearID;

        var url = utility.myHost + "Signup/GetAssociateTeachers?classID=" + classID + "&sectionID=" + sectionID + "&academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (!result.data.IsError) {

                    $scope.Employees = result.data.Response;

                    if ($scope.Employees.length == 1) {
                        $scope.MeetingRequest.Faculty = $scope.Employees[0];

                        $scope.OnEmployeeChanges();
                    }
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillOtherTeachers = function () {
        var classID = $scope.MeetingRequest.ClassID;
        var sectionID = $scope.MeetingRequest.SectionID;
        var academicYearID = $scope.MeetingRequest.AcademicYearID;

        var url = utility.myHost + "Signup/GetOtherTeachersByClass?classID=" + classID + "&sectionID=" + sectionID + "&academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (!result.data.IsError) {

                    $scope.Employees = result.data.Response;

                    if ($scope.Employees.length == 1) {
                        $scope.MeetingRequest.Faculty = $scope.Employees[0];

                        $scope.OnEmployeeChanges();
                    }
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.OnEmployeeChanges = function () {

        $scope.IsShowSlotDetails = true;

        $scope.MeetingSlots = [];

        $scope.FillTimeSlotsForEmployee();

        $scope.IsError = false;
        $scope.ErrorMessage = "";
    };

    $scope.FillTimeSlotsForEmployee = function () {

        var employeeID = $scope.MeetingRequest.Faculty.Key;
        var requestedSlotDateString = $scope.MeetingRequest.RequestedDateString;

        if (!employeeID) {
            return false;
        }
        else if (!requestedSlotDateString) {
            return false;
        }
        else {

            $.ajax({
                url: utility.myHost + "SignUp/GetMeetingRequestSlotsByEmployeeID?employeeID=" + employeeID + "&reqSlotDateString=" + requestedSlotDateString,
                type: "GET",
                success: function (result) {
                    if (!result.IsError && result.Response != null) {
                        $timeout(function () {
                            $scope.$apply(function () {

                                var signUps = result.Response;

                                signUps.forEach(sign => {
                                    sign.SignupSlotMaps.forEach(map => {
                                        map.SignupSlotMapTimes.forEach(time => {
                                            $scope.MeetingSlots.push(time);
                                        })
                                        
                                    })
                                })
                            });

                        });
                    }

                    $scope.ShowPreLoader = false;
                }
            });
        }
    };

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

    $scope.OnRequestDateChanges = function () {

        $scope.IsError = false;
        $scope.ErrorMessage = "";

        $scope.IsShowSlotDetails = true;

        $scope.MeetingSlots = [];

        var currentDate = new Date();

        if ($scope.MeetingRequest.RequestedDateString <= currentDate) {
            $scope.IsError = true;
            $scope.ErrorMessage = "Select a date that is greater than or equal to the current date.";

            $scope.MeetingRequest.RequestedDateString = null;
            return true;
        }

        $scope.MeetingRequest.RequestedDateString = moment($scope.MeetingRequest.RequestedDateString).format(dateFormat.toUpperCase());

        $scope.FillTimeSlotsForEmployee();
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

    $scope.SlotSelectionChange = function (slot) {

        $scope.MeetingRequest.RequestedSignupSlotMapID = slot.SignupSlotMapIID;

        $timeout(function () {
            $scope.$apply(function () {

                var radioButton = document.getElementById("slot_" + slot.SignupSlotMapIID);
                if (radioButton) {
                    radioButton.checked = true;
                }
            });
        });

        $scope.IsError = false;
        $scope.ErrorMessage = "";
    };

    $scope.ConfirmAndRequestSlot = function () {

        var meetingRequest = $scope.MeetingRequest;

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
            $.ajax({
                url: utility.myHost + "SignUp/SubmitMeetingRequest",
                type: "POST",
                data: meetingRequest,
                success: function (result) {
                    if (result.IsError) {
                        $().showGlobalMessage($root, $timeout, true, result.Response);
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, false, "Slot request successful!");

                        window.location.href = utility.myHost + "SignUp/MeetingRequestList";
                    }
                },
                error: function (result) {
                    $scope.IsError = false;
                    $scope.ErrorMessage = result;
                }
            });
        }
    };

}]);