app.controller("AttendenceController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("AttendenceController Loaded");

    $scope.Students = [];
    $scope.Staffs = [];
    $scope.CurrentAttendence = {};
    $scope.Model = {};
    $scope.SelectedMonth = null;
    $scope.SelectedYear = null;
    $scope.CurrentDate = new Date();
    $scope.SelectedDay = new Date().getDate();
    $scope.SelectedYear = new Date().getFullYear();
    $scope.SelectedMonth = new Date().getMonth();
    $scope.AttendenceReasons = [];
    $scope.PresentStatuses = [];
    $scope.AttendanceStatuses = [];
    $scope.AllStudentAttendance = [];
    $scope.Reason = null;
    $scope.type = null;
    $scope.PresentStatus = 6;
    $scope.PStatus = 0;
    $scope.updateStatus = 0;
    $scope.HoliDayData = [];
    $scope.PresentStatuses = [];
    $scope.AttendenceReasonKey = null;
    $scope.SelectedMonthDate = 0;
    $scope.ButtonText = "Mark All";
    $scope.NotificationButton = "Send today's attendance to parents";
    $scope.Months = [

        'January', 'February', 'March', 'April', 'May',

        'June', 'July', 'August', 'September',

        'October', 'November', 'December'

    ];
  

    $scope.Init = function (model, windowname, type) {
        $scope.MonthDate();
        $scope.ResetDay();
        $scope.type = type;



        $.ajax({
            type: 'GET',
            url: utility.myHost + "/Mutual/GetDynamicLookUpData?lookType=AttendenceReason&defaultBlank=false;",
            success: function (result) {
                $scope.AttendenceReasons = result;
            }
        });

        if ($scope.type == "student") {

            var attnStatuses = [];

            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=MainTeacherClasses&defaultBlank=false",
            }).then(function (result) {
                $scope.Class = result.data;
            });

            //Sections
            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=MainTeacherSections&defaultBlank=false",
            }).then(function (result) {
                $scope.Section = result.data;
            });

            $.ajax({
                type: 'GET',
                url: utility.myHost + "/Mutual/GetDynamicLookUpData?lookType=PresentStatus&defaultBlank=false;",
                success: function (result) {
                    $scope.PresentStatusData = result;
                }
            });

            $.ajax({
                type: 'GET',
                url: "Attendence/GetPresentStatuses",
                success: function (result) {
                    $scope.PresentStatuses = result;

                    $scope.PresentStatuses.forEach(x => {
                        if (x.StatusTitle !== "L" && x.StatusTitle !== "H" && x.StatusTitle !== "T" && x.StatusTitle !== "TE" && x.StatusTitle !== "W" && x.StatusTitle !== "LA" && x.StatusTitle !== "UM") attnStatuses.push({
                            'PresentStatusID': x.PresentStatusID,
                            'StatusDescription': x.StatusDescription,
                            'StatusTitle': x.StatusTitle,
                        })
                    }
                    );
                    $scope.AttendanceStatuses = attnStatuses;
                }
            });

        }
        if ($scope.type == "staff") {

            $.ajax({
                type: 'GET',
                url: utility.myHost + "/Mutual/GetDynamicLookUpData?lookType=CalendarMasters&defaultBlank=false;",
                success: function (result) {
                    $scope.CalendarMasterData = result;
                }
            });

            $.ajax({
                type: 'GET',
                url: utility.myHost + "/Mutual/GetDynamicLookUpData?lookType=PresentStaffStatus&defaultBlank=false;",
                success: function (result) {
                    $scope.PresentStatusData = result;
                }
            });

            $.ajax({
                type: 'GET',
                url: "Attendence/GetStaffPresentStatuses",
                success: function (result) {
                    $scope.PresentStatuses = result;
                    //console.log($scope.PresentStatuses);
                }
            });

        }

        $(document).ready(function () {
            if ($scope.type == "student") {
                $scope.ClassSectionChange = function (selected) {

                    if (selected.ngModel.$name == "Class") {
                        $scope.selectedClass = selected.selected;
                    }
                    if (selected.ngModel.$name == "Section") {
                        $scope.selectedSection = selected.selected;
                    }

                    var classId = $scope.selectedClass?.Key;
                    var sectionId = $scope.selectedSection?.Key;

                    if (classId == null) {
                        classId = 0;
                        $().showMessage($scope, $timeout, true, "Please Select an Class");
                        return false;
                    }

                    if (sectionId === null) {
                        sectionId = 0;
                        return false;
                    }
                    if (classId != null && classId != 'undefined' && sectionId != null && sectionId != 'undefined') {
                        $scope.MonthDate();
                        $scope.LoadAttendenceData();
                    }
                };
            }

            if ($scope.type == "staff") {
                $scope.CalendarChange = function (selected) {
                    $scope.selectedCalendar = selected.selected;

                    var calendarID = $scope.selectedCalendar?.Key;

                    if (calendarID == null || calendarID == 'undefined') {
                        calendarID = 0;
                        $().showMessage($scope, $timeout, true, "Please select an calendar");
                        return false;
                    }
                    else {
                        $scope.MonthDate();
                        $scope.LoadAttendenceData();
                    }
                };
            }

            $(".monthPicker").datepicker({
                changeMonth: true,
                changeYear: true,
                onSelect: function (dateText) {
                    var selectedDate = new Date(dateText);
                    $scope.SelectedMonth = selectedDate.getMonth();
                    $scope.SelectedYear = selectedDate.getFullYear();
                    $scope.LoadAttendenceData();
                    //$scope.LoadAttendence();
                    //$scope.SelectedDay = selectedDate.getDate();
                },
                onChangeMonthYear: function (year, month) {
                    $scope.SelectedMonth = parseInt(month) - 1;
                    $scope.SelectedYear = year;
                    $scope.MonthDate();
                    $scope.LoadAttendenceData();

                }
            });
        });
    };


    $scope.LoadData = function () {
        if ($scope.type == "student") {
            if ($scope.selectedClass == undefined || $scope.selectedSection == undefined) {
                $().showGlobalMessage($root, $timeout, true, "Select Class and Section!");
                return false;
            }
            else {
                $scope.LoadAttendenceData();
            }
        }
        else if ($scope.type == "staff") {
            if ($scope.selectedCalendar == undefined) {
                $().showGlobalMessage($root, $timeout, true, "Select calendar!");
                return false;
            }
            else {
                $scope.LoadAttendenceData();
            }
        }
    };

    $scope.MonthDate = function () {
        var date = new Date($scope.SelectedYear, $scope.SelectedMonth + 1, 0).getDate();
        $scope.SelectedMonthDate = date + 1;
    };

    $scope.GetStudentImage = function (studentId, fileName) {     
        return utility.ImageUrl + "StudentProfile/" + studentId + "/Thumbnail/" + fileName;
    }

    $scope.LoadAttendenceData = function () {
        if ($scope.type == "student") {
            if ($scope.selectedClass == undefined || $scope.selectedSection == undefined) {
                $().showGlobalMessage($root, $timeout, true, "Select Class and Section!");
                return false;
            }
            var classId = $scope.selectedClass?.Key;
            var sectionId = $scope.selectedSection?.Key;
        }
        if ($scope.type == "staff") {
            if ($scope.selectedCalendar == undefined) {
                $().showGlobalMessage($root, $timeout, true, "Select calendar!");
                return false;
            }
            var calendarID = $scope.selectedCalendar?.Key;
        }
        showOverlay();
        if ($scope.type === 'student') {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "/Schools/Calender/GetAcademicCalenderByMonthYear?month=" + ($scope.SelectedMonth + 1) + '&year=' + $scope.SelectedYear,
                success: function (result2) {
                    $scope.HoliDayData = result2;

                },
                complete: function (result) {
                    $.ajax({
                        type: 'GET',
                        url: utility.myHost + "/Schools/Attendence/GetStudentAttendenceByYearMonth?month=" + $scope.SelectedMonth + '&year=' + $scope.SelectedYear + "&classId=" + classId + "&sectionId=" + sectionId,
                        success: function (result) {
                            $scope.AttendenceData = result;
                        },
                        complete: function (result) {
                            $scope.LoadAttendence();
                        }

                    });

                }
            });

        }
        hideOverlay();

        if ($scope.type === 'staff') {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "/Schools/Calender/GetCalendarEventsByCalendarID?calendarID=" + calendarID,
                success: function (result2) {
                    $scope.HoliDayData = result2;
                },
                complete: function (result) {
                    $.ajax({
                        type: 'GET',
                        url: utility.myHost + "/Schools/Attendence/GetStaffAttendanceByMonthYear?month=" + $scope.SelectedMonth + '&year=' + $scope.SelectedYear,
                        success: function (result) {
                            $scope.AttendenceData = result;
                        },
                        complete: function (result) {
                            $scope.LoadAttendence();
                        }

                    });

                }
            });
        }
    };

    $scope.LoadAttendence = function () {

        showOverlay();
        $scope.SelectedDay = new Date().getDate();
        var classId = $scope.selectedClass?.Key;
        var sectionId = $scope.selectedSection?.Key;

        if ($scope.type === 'student') {

            $scope.Students = [];
            $.ajax({
                url: "Schools/School/GetClasswiseStudentData?classId=" + classId + "&sectionId=" + sectionId,
                type: "GET",
                success: function (result3) {
                    if (!result3.IsError && result3 != null) {
                        $scope.$apply(function () {
                            //JSON.parse(result).data.forEach(student => {
                            $scope.AllStudentAttendance = result3;
                            angular.forEach(result3, function (student) {
                                var totalDate = $scope.SelectedMonthDate - 1;
                                student.attendances = [];
                                for (var i = 1; i <= totalDate; i++) {

                                    if ($scope.AttendenceData != null) {
                                        var existingData = $scope.AttendenceData.find(a => a.StudentID == student.StudentIID &&
                                            i == new Date(a.AttendenceDate).getDate() &&
                                            $scope.SelectedMonth == new Date(a.AttendenceDate).getMonth() &&
                                            $scope.SelectedYear == new Date(a.AttendenceDate).getFullYear()
                                        );
                                    }
                                    if ($scope.HoliDayData != null) {
                                        var holiDay = $scope.HoliDayData.find(h =>
                                            i == h.Day &&
                                            ($scope.SelectedMonth + 1) == h.Month &&
                                            $scope.SelectedYear == h.Year
                                        );
                                    }
                                    //if (holiDay != null)
                                    //{
                                    //    var event = (PresentStatusID != 3 || holiDay == null) ? '' : holiDay.EventTitle.charAt(0);
                                    //}
                                    //var actualDate = new Date(i.toString() + '/' + $scope.Months[$scope.SelectedMonth] + '/' + $scope.SelectedYear.toString());
                                    //var dateValue = student.AdmissionDate != null ? parseInt(student.AdmissionDate.replace(/\/Date\((\d+)\)\//g, "$1")) : null;
                                    //var admissionDate = new Date(dateValue);

                                    var feestartdateValue = student.FeeStartDate != null ? parseInt(student.FeeStartDate.replace(/\/Date\((\d+)\)\//g, "$1")) : null;

                                    //var feeStartDate = moment(new Date(feestartdateValue)).format(_dateFormat.toUpperCase());
                                    //var actualDateString = moment(new Date(actualDate)).format(_dateFormat.toUpperCase());

                                    var feeStartDateString = moment(new Date(feestartdateValue)).format("MM/DD/YYYY");
                                    var feeStartDate = new Date(feeStartDateString);


                                    var dataStatus = false; var PresentStatusID = 9;//Unmarked
                                    if (existingData == null || existingData == 'undefined') {
                                        if (holiDay != 'undefined' && holiDay != null) {
                                            if (holiDay.AcademicYearCalendarEventType == '1') {
                                                dataStatus = false;
                                                PresentStatusID = 3;//Holiday
                                            }
                                            else if (holiDay.AcademicYearCalendarEventType == '2') {
                                                dataStatus = false;
                                                PresentStatusID = 10;//Weekend
                                            }

                                        }
                                        else {
                                            dataStatus = true;
                                            PresentStatusID = 9;//Unmarked
                                        }
                                    }
                                    else {
                                        PresentStatusID = existingData.PresentStatusID;
                                    }
                                    if (PresentStatusID == null) {
                                        PresentStatusID = 9;//Unmarked
                                    }

                                    //if ((existingData == null || typeof (existingData) == 'undefined') ||

                                    //    (typeof (holiDay) != 'undefined' && holiDay != null))
                                    //{
                                    //    dataStatus = false;
                                    //    PresentStatusID = 3;
                                    //}
                                    //else if (actualDate < admissionDate)
                                    //    dataStatus = false;
                                    //else
                                    //    dataStatus = true;

                                    // var dataStatus = existingData == null ? true : false;

                                    student.attendances.push({
                                        //date: i.toString() + '/' + ($scope.SelectedMonth + 1).toString() + '/' + $scope.SelectedYear.toString(),
                                        month: $scope.SelectedMonth, year: $scope.SelectedYear, day: i,
                                        status: dataStatus,
                                        statusId: PresentStatusID,
                                        statusTitle: $scope.PresentStatuses.find(a => a.PresentStatusID == PresentStatusID).StatusTitle,
                                        reason: (holiDay == null) ? '' : holiDay.EventTitle,
                                        //ColorCode: (holiDay == null) ? '' : holiDay.ColorCode,
                                        actualDate: new Date(i.toString() + '/' + $scope.Months[$scope.SelectedMonth] + '/' + $scope.SelectedYear.toString()),
                                        feeStartDate: feeStartDate,
                                    });
                                    //console.log('PresentStatusID Completed');
                                }
                                $scope.Students.push(student);
                            });

                            console.log($scope.Students);
                        });
                        hideOverlay();
                    }
                    else {
                        hideOverlay();
                        alert("No Student found!");
                    }
                }
            });
        }

        if ($scope.type === 'staff') {
            var calendarID = $scope.selectedCalendar?.Key;
            $scope.Staffs = [];

            $.ajax({
                url: "Schools/School/GetEmployeesByCalendarID?calendarID=" + calendarID,
                type: "GET",
                success: function (result) {
                    if (!result.IsError && result != null) {
                        $scope.StaffData = result;
                        $scope.$apply(function () {
                            $scope.StaffData.forEach(staff => {
                                var totalDate = $scope.SelectedMonthDate - 1;
                                staff.attendances = [];
                                for (var i = 1; i <= totalDate; i++) {

                                    if ($scope.AttendenceData != null) {
                                        var existingData = $scope.AttendenceData.find(a => a.EmployeeID == staff.EmployeeIID &&
                                            i == new Date(a.AttendenceDate).getDate() &&
                                            $scope.SelectedMonth == new Date(a.AttendenceDate).getMonth() &&
                                            $scope.SelectedYear == new Date(a.AttendenceDate).getFullYear()
                                        );
                                    }
                                    if ($scope.HoliDayData != null) {
                                        var holiDay = $scope.HoliDayData.find(h =>
                                            i == h.Day &&
                                            ($scope.SelectedMonth + 1) == h.Month &&
                                            $scope.SelectedYear == h.Year
                                        );
                                    }

                                    var joiningDateValue = staff.DateOfJoining != null ? parseInt(staff.DateOfJoining.replace(/\/Date\((\d+)\)\//g, "$1")) : null;

                                    var joiningDateString = moment(new Date(joiningDateValue)).format("MM/DD/YYYY");
                                    var joiningDate = new Date(joiningDateString);


                                    var dataStatus = false; var PresentStatusID = 7;//Unmarked
                                    if (existingData == null || existingData == 'undefined') {
                                        if (holiDay != 'undefined' && holiDay != null) {
                                            if (holiDay.AcademicYearCalendarEventType == '1') {
                                                dataStatus = false;
                                                PresentStatusID = 5;//Holiday
                                            }
                                            else if (holiDay.AcademicYearCalendarEventType == '2') {
                                                dataStatus = false;
                                                PresentStatusID = 6; //Weekend
                                            }
                                        }
                                        else {
                                            dataStatus = true;
                                            PresentStatusID = 7;//Unmarked
                                        }
                                    }
                                    else {
                                        PresentStatusID = existingData.PresentStatusID;
                                    }
                                    if (PresentStatusID == null) {
                                        PresentStatusID = 7;//Unmarked
                                    }

                                    staff.attendances.push({
                                        month: $scope.SelectedMonth, year: $scope.SelectedYear, day: i,
                                        status: dataStatus,
                                        statusId: PresentStatusID,
                                        statusTitle: $scope.PresentStatuses.find(a => a.PresentStatusID == PresentStatusID).StatusTitle,
                                        reason: (holiDay == null) ? '' : holiDay.EventTitle,
                                        actualDate: new Date(i.toString() + '/' + $scope.Months[$scope.SelectedMonth] + '/' + $scope.SelectedYear.toString()),
                                        joiningDate: joiningDate,
                                    });
                                    //console.log('PresentStatusID Completed');
                                }
                                $scope.Staffs.push(staff);
                            });
                        });
                    }
                    else {
                        hideOverlay();
                        alert("No Staff found!");
                    }
                }
            });
        }
    };

    $scope.SetAttendenceStatus = function (popupcontainer, event, attendence, model) {
        $scope.Model = model;
        $scope.CurrentAttendence = attendence;
        var dataAttr = $(event.currentTarget).attr('data-attr');
        var popupLeftPos = 0;
        var targetLeftPos = $(event.target).offset().left;
        var targetTopPos = $(event.target).offset().top - $(document).scrollTop();
        var pageWidth = $(document).outerWidth();
        var windowHeight = $(window).height();
        var eventWidth = $(event.target).outerWidth();
        var eventHeight = $(event.target).height();
        var popcontainerWidth = $(popupcontainer).outerWidth();
        var popcontainerHeight = $(popupcontainer).outerHeight();
        var popupTopPos = targetTopPos + eventHeight;
        var displayLeftArea = targetLeftPos + popcontainerWidth;
        var visiblePopupArea = popupTopPos + popcontainerHeight;

        $scope.PresentStatus = [];
        $scope.Reason = null;
        $scope.AttendenceReason = [];

        $(popupcontainer).fadeIn("fast");
        if (displayLeftArea > pageWidth) {
            popupLeftPos = targetLeftPos - popcontainerWidth + eventWidth;
            $(popupcontainer).addClass('rightAligned');
        }
        else {
            popupLeftPos = targetLeftPos;
            $(popupcontainer).removeClass('rightAligned');
        }
        if (visiblePopupArea > windowHeight) {
            newTopPos = popupTopPos - popcontainerHeight - eventHeight;
            $(popupcontainer).addClass('setTooltipBottom');
        }
        else {
            newTopPos = popupTopPos;
            $(popupcontainer).removeClass('setTooltipBottom');
        }

        $(popupcontainer).css({ "left": popupLeftPos, "top": newTopPos });
    };

    $scope.MarkAsAbsent = function (popupcontainer) {
        $scope.updateStatus == 0;
        if ($scope.CurrentAttendence) {
            $(popupcontainer).slideUp();

            saveAttendence();

            if ($scope.updateStatus == 1)
                $scope.CurrentAttendence.status = false;
        }


    };

    $scope.MarkAsPresent = function (attendence, model) {
        $scope.Model = model;
        $scope.updateStatus == 1;
        $scope.PStatus = 1;

        if ($scope.CurrentAttendence) {
            $scope.CurrentAttendence = attendence;


            saveAttendence();
            if ($scope.updateStatus == 1) {
                $scope.CurrentAttendence.status = true;

            }

        }

    };

    $scope.changePresentStatus = function (statusId) {
        $scope.PresentStatus = statusId;
        $scope.PStatus = statusId;
    }

    function saveAttendence() {
        showOverlay();
        if ($scope.PresentStatus == null) {
            alert("Please Select Any Status!");
            $scope.updateStatus = 0;
            hideOverlay();
            return false;
        }

        //if (($scope.PStatus == 0 && ($scope.PresentStatus != null && $scope.PresentStatus != null && $scope.PresentStatus != 1)) && ($scope.AttendenceReason == null || $scope.AttendenceReason == null)) {
        //    alert("Please Select Attendence Reason!");
        //    $scope.updateStatus = 0;
        //    return false;
        //}

        //if (($scope.PStatus == 0 && ($scope.PresentStatus != null && $scope.PresentStatus != null && $scope.PresentStatus != 1)) && ($scope.Reason == null)) {
        //    alert("Please Fill Reason Note!");
        //    $scope.updateStatus = 0;
        //    return false;
        //}
        else {
            var attendenceDate = moment($scope.CurrentAttendence.actualDate).format(_dateFormat.toUpperCase());
            $scope.CurrentAttendence.date = attendenceDate;
            if ($scope.PresentStatus.Key == undefined) {
                $scope.PresentStatus = $scope.PresentStatus;
            }
            else {
                $scope.PresentStatus = $scope.PresentStatus.Key;
            }

            if ($scope.AttendenceReason == undefined || $scope.AttendenceReason.Key == null) {
                $scope.AttendenceReasonKey = $scope.AttendenceReasonKey;
            }
            else {
                $scope.AttendenceReasonKey = $scope.AttendenceReason.Key;
            }

            $.ajax({
                url: utility.myHost + "Schools/Attendence/SaveAttendence",
                type: "POST",
                data: {
                    "StaffID": $scope.Model.EmployeeIID,
                    "StudentID": $scope.Model.StudentIID,
                    "AttendenceDateString": $scope.CurrentAttendence.date,
                    "PresentStatusID": $scope.PresentStatus,//$scope.PStatus == 0 ? $scope.PresentStatus : 1,
                    "AttendenceReasonID": $scope.PStatus == 0 ? $scope.AttendenceReasonKey : null,
                    "Reason": $scope.Reason,
                    "ClassID": $scope.selectedClass?.Key,
                    "SectionID": $scope.selectedSection?.Key
                },
                success: function (result) {
                    if (!result.IsError && result != null) {
                        //if ($scope.updateStatus == 1)
                        //    alert(result.Message);
                    }
                    if (result.IsFailed)
                        $scope.updateStatus = 0;
                    else
                        $scope.updateStatus = 1;

                    $scope.$apply(function () {
                        if ($scope.PStatus == 1 || ($scope.PresentStatus == null || $scope.PresentStatus.Key == null || ($scope.PresentStatus.Key != null && $scope.PresentStatus.Key == 1))) {
                            if ($scope.updateStatus == 1) {
                                $scope.CurrentAttendence.status = true;
                                $scope.PStatus = 0;
                            }
                        }
                        else {
                            if ($scope.updateStatus == 1)
                                $scope.CurrentAttendence.status = false;
                        }
                        $scope.LoadAttendenceData();
                    });
                },
            });
            hideOverlay();
        }
    }

    $scope.UpdateAllStudentAttendence = function () {

        showOverlay();
        if ($scope.PresentStatus == null) {
            alert("Please Select Any Status!");
            $scope.updateStatus = 0;
            hideOverlay();
            return false;
        }
        var attendenceDate = moment($scope.CurrentAttendence.actualDate).format(_dateFormat.toUpperCase());
        var classId = $scope.selectedClass?.Key;
        var sectionId = $scope.selectedSection?.Key;
        var PStatus = $scope.PresentStatus;

        $scope.ButtonText = "saving attendance...";

        if ($scope.type === 'student') {
            $.ajax({
                url: utility.myHost + "/Schools/Attendence/UpdateAllStudentAttendence?classId=" + classId + "&sectionId=" + sectionId + "&AttendenceDateString=" + attendenceDate + "&PStatus=" + PStatus,
                type: "POST",
                success: function (result) {
                    if (result.IsFailed)
                        $scope.updateStatus = 0;
                    else
                        $scope.updateStatus = 1;

                    $scope.$apply(function () {
                        $scope.LoadAttendenceData();
                        $scope.ResetDay();
                    });
                }

            });
        }

        hideOverlay();
    }

    $scope.MarkDay = function ($index) {
        var date = new Date($index.toString() + '/' + $scope.Months[$scope.SelectedMonth] + '/' + $scope.SelectedYear.toString())
        $scope.CurrentAttendence.actualDate = date;
        var datestring = moment($scope.CurrentAttendence.actualDate).format(_dateFormat.toUpperCase());

        $scope.ButtonText = "Mark All For ( " + datestring + " )";

    };

    $scope.ResetDay = function () {
        $scope.CurrentAttendence.actualDate = new Date();
        var datestring = moment($scope.CurrentAttendence.actualDate).format(_dateFormat.toUpperCase());
        $scope.ButtonText = "Mark All For ( " + datestring + " )";
        //$(this).css('background', 'white!important');
    };

    $scope.UpdateAllStaffAttendance = function () {

        showOverlay();
        if ($scope.PresentStatus == null) {
            alert("Please Select Any Status!");
            $scope.updateStatus = 0;
            hideOverlay();
            return false;
        }
        var attendenceDate = moment($scope.CurrentAttendence.actualDate).format(_dateFormat.toUpperCase());
        var calendarID = $scope.selectedCalendar?.Key;

        var PStatus = $scope.PresentStatus;

        $scope.ButtonText = "saving attendance...";
        if ($scope.type === 'staff') {
            $.ajax({
                url: utility.myHost + "/Schools/Attendence/UpdateAllStaffAttendance?calendarID=" + calendarID + "&AttendenceDateString=" + attendenceDate + "&PStatus=" + PStatus,
                type: "POST",
                success: function (result) {
                    if (result.IsFailed)
                        $scope.updateStatus = 0;
                    else
                        $scope.updateStatus = 1;
                    hideOverlay();
                    $scope.$apply(function () {
                        $scope.LoadAttendenceData();
                        $scope.ResetDay();
                    });
                },
                error: function () {
                    $scope.ResetDay();
                    hideOverlay();
                },

            });
        }

        hideOverlay();
    }

    $scope.SendAttendancePushNotification = function () {

        var classId = $scope.selectedClass?.Key;
        var sectionId = $scope.selectedSection?.Key;
        showOverlay();

        if (classId == undefined || classId == null || classId == "") {
           //$().showMessage($scope, $timeout, true, "Please select Class!");
            //callToasterPlugin('error', "Please select Class!");
            alert("Please select Class !");
            hideOverlay();
            return false
        }

        if (sectionId == undefined || sectionId == null || sectionId == "") {
            //$().showMessage($scope, $timeout, true, "Please select Section!");
            alert("Please select Section !");
            hideOverlay();
            return false
        }

        $.ajax({
            url: utility.myHost + "Schools/School/SendTodayAttendancePushNotification?classId=" + classId + "&sectionId=" + sectionId,
            type: "POST",
            success: function (result) {
                if (result != null || result != undefined)
                    alert(result);
                hideOverlay();
            },
            error: function () {

            },
        });
        hideOverlay();
    };


    $scope.SendAttendanceNotificationsToParents = function () {

        var classId = $scope.selectedClass?.Key;
        var sectionId = $scope.selectedSection?.Key;
        showOverlay();

        if (classId == undefined || classId == null || classId == "") {
            alert("Please select Class !");
            hideOverlay();
            return false
        }

        if (sectionId == undefined || sectionId == null || sectionId == "") {
            alert("Please select Section !");
            hideOverlay();
            return false
        }
        $scope.NotificationButton = "Sending notification.....";

        $.ajax({
            url: utility.myHost + "Schools/School/SendAttendanceNotificationsToParents?classId=" + classId + "&sectionId=" + sectionId,
            type: "POST",
            success: function (result) {
                if (result != null || result != undefined)
                    alert(result);
                hideOverlay();
                $scope.NotificationButton = "Send today's attendance to parents";
            },
            error: function () {

            },
        });
        hideOverlay();
    };

    function showOverlay() {
        $("#showLoader").fadeIn();
        $("#showLoaderButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#showLoader").fadeOut();
        $("#showLoaderButtonLoader").fadeOut();
    }

}]);

