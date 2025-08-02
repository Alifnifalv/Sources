//var app = angular.module('rzSliderDemo', ['rzSlider', 'ui.bootstrap'])
app.controller("MyWardController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope, $uibModal) {

    //$controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("MyWardController Loaded");
    $scope.Students = [];
    $scope.StudentsAttendence = [];
    $scope.SelectedStudent = [];
    $scope.FeeMonthly = [];
    $scope.FeeMonthlyHis = [];
    $scope.FeeTypes = [];
    $scope.FeeTypeHis = [];
    $scope.StudentDetails = null;
    $scope.ParentDetails = null;
    $scope.StudentLeaveApplication = null;
    $scope.FineDues = [];
    $scope.FineHis = [];
    $scope.ReasonData = [];
    $scope.CircularList = [];
    $scope.AssignmentList = [];
    $scope.WeekDay = {};
    $scope.ClassTime = {};
    $scope.StudentName = {};
    $scope.Class = {};
    $scope.Section = {};
    $scope.ClassId = {};
    $scope.StudentDetailsFull = null;
    $scope.activeStudentID = 0;
    $scope.activeStudentClassID = 0;
    $scope.activeStudentSectionID = 0;
    $scope.StudentTransportDetails = {};
    $scope.isStudentTransport = false;
    $scope.onOtherReason = false;

    $scope.HoliDayData = [];
    $scope.AttendenceData = [];
    $scope.AttendanceFullData = [];

    $scope.PresentStatusCodeData = [];

    $scope.Years = [];
    $scope.SelectedMonthDate = 0;
    $scope.MonthDays = [];
    $scope.CurrentDate = new Date();
    $scope.CurrentDateMonth = new Date().getMonth();
    $scope.CurrentDateYear = new Date().getFullYear();
    $scope.SelectedDay = new Date().getDate();
    $scope.SelectedYear = new Date().getFullYear();
    $scope.SelectedMonth = new Date().getMonth();
    $scope.SelectedDate = { SelectedYear: new Date().getFullYear().toString(), SelectedMonth: new Date().getMonth().toString() }
    $scope.Months = [

        'January', 'February', 'March', 'April', 'May',

        'June', 'July', 'August', 'September',

        'October', 'November', 'December'

    ];
    $scope.WeakList = [{ daynum: 1, dayName: 'Sunday' }, { daynum: 2, dayName: 'Monday' }, { daynum: 3, dayName: 'Tuesday' }, { daynum: 4, dayName: 'Wednesday' }, { daynum: 5, dayName: 'Thursday' }, { daynum: 6, dayName: 'Friday' }, { daynum: 7, dayName: 'Saturday' }];
    $scope.Days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    $scope.WKDays = [{ num: 1, name: 'Sun' }, { num: 2, name: 'Mon' }, { num: 3, name: 'Tue' }, { num: 4, name: 'Wed' }, { num: 5, name: 'Thu' }, { num: 6, name: 'Fri' }, { num: 7, name: 'Sat' }];
    $scope.ShortMonthList = [{ num: 1, name: 'Jan' }, { num: 2, name: 'Feb' }, { num: 3, name: 'Mar' }, { num: 4, name: 'Apr' }, { num: 5, name: 'May' }, { num: 6, name: 'Jun' }, { num: 7, name: 'Jul' }, { num: 8, name: 'Aug' }, { num: 9, name: 'Sep' }, { num: 10, name: 'Oct' }, { num: 11, name: 'Nov' }, { num: 12, name: 'Dec' }];

    $scope.StudentMarkList = [];
    $scope.ExamsList = [];
    $scope.ProgressReportGraphLabel = [];
    $scope.ProgressReportDataSet = [];
    $scope.isOpenProgressReportGraph = false;
    $scope.ShowPreLoader = false;
    $scope.ProgressReportURL = "";
    $scope.IsAssignmentOpened = false;

    $scope.init = function () {
        $scope.Years = [];
        for (let i = $scope.CurrentDateYear - 5; i <= $scope.CurrentDateYear; i++) {
            $scope.Years.push(i.toString());
        }
        //change url
        $scope.changeurl = function () {
            var currentLocation = location.protocol + '//' + location.host + location.pathname;
            //alert(currentLocation);
            const nextURL = 'https://my-website.com/page_b';
            const nextTitle = 'My new page title';
            const nextState = { additionalInformation: 'Updated the URL with JS' };

            // This will create a new entry in the browser's history, without reloading
            window.history.pushState("", "", event.target.currentLocation);
            event.preventDefault();
        }

        $.ajax({
            type: "GET",
            data: { parentId: 60 },
            url: utility.myHost + "/Home/GetStudentsSiblings",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                var activeStudentID = $("#CandidateIDfromOutSide").val();
                $scope.activeStudentID = activeStudentID;
                var activeStudentClassID = 0;
                var activeStudentSectionID = 0;
                var activeStudentName = "";
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.Students = result.Response;
                        if ($scope.Students !== null && $scope.Students.length > 0) {
                            if (activeStudentID == 0) {
                                activeStudentID = $scope.Students[0].StudentIID;
                                activeStudentClassID = $scope.Students[0].ClassID;
                                activeStudentSectionID = $scope.Students[0].SectionID;
                                activeStudentName = $scope.Students[0].FirstName + ' ' + $scope.Students[0].MiddleName + ' ' + $scope.Students[0].LastName + ""
                                $scope.activeStudentID = $scope.Students[0].StudentIID;
                                $scope.activeStudentClassID = $scope.Students[0].ClassID;
                                $scope.activeStudentSectionID = $scope.Students[0].SectionID;
                            }
                            else {
                                $scope.Students.forEach(function (index) {
                                    if (index.StudentIID == activeStudentID) {
                                        activeStudentClassID = index.ClassID;
                                        activeStudentSectionID = index.SectionID;
                                        activeStudentName = index.FirstName + ' ' + index.MiddleName + ' ' + index.LastName + "";
                                        $scope.activeStudentID = index.StudentIID;
                                        $scope.activeStudentClassID = index.ClassID;
                                        $scope.activeStudentSectionID = index.SectionID;
                                    }
                                });
                            }
                            $("#StudentID").val($scope.activeStudentID);
                            $("#ClassID").val($scope.activeStudentClassID);
                            $scope.SelectedStudent = {
                                "ClassID": "" + $scope.Students[0].ClassID + "", "ClassName": "" + $scope.Students[0].ClassName + "",
                                "SectionID": "" + $scope.Students[0].SectionID + "", "SectionName": "" + $scope.Students[0].SectionName + "",
                                "StudentID": "" + activeStudentID + "", "StudentName": "" + activeStudentName
                            };

                            $("#activedStudentID").val(activeStudentID);
                            $("#activedClassID").val(activeStudentClassID);
                            $("#activedSectionID").val(activeStudentSectionID);

                            $scope.studentProfile(activeStudentID);
                            ////$scope.fillStudentProgressReport(activeStudentID);

                            // $scope.getFeeMonthly(activeStudentID);
                            ////$scope.getFines(activeStudentID);
                            ////-------------------------------------------
                            ////$scope.getMarkList(activeStudentID);
                            ////$scope.getAssignments(activeStudentID);
                            //$scope.fillSkillRegister(activeStudentID, activeStudentClassID);
                            //$scope.studentLeave(activeStudentID);
                            //$scope.leaveApplication(activeStudentID);
                            //$scope.fillWeekDay();
                            ////$scope.fillClassTime();
                            //$scope.timeTable(activeStudentID);
                            ////$scope.examList(activeStudentID);
                            //$scope.studentTransfer(activeStudentID);
                            //$scope.transferApplication(activeStudentID);
                            ////$scope.classTeacherDetail(activeStudentID);
                            ////$scope.feeHistory(activeStudentID);
                            //$scope.getstudentTransportDetails();
                            //$scope.fillReasons();
                            //$scope.getStudentAttendance();
                        }
                    });
                }
                if (result == null) {
                    window.location.href = "Account/LogIn";
                }
                //hideOverlay();
                $("#angularContent").show(); $scope.changeurl();
            },
            error: function () {

            },
            complete: function (result) {
                ///hideOverlay();
            }


        });

        $scope.MonthDate();
        $scope.GetWeekStartDay();

        $scope.LoadData();

        //To convert value to integer.
        $scope.parseInt = function (val) {
            return parseInt(val);
        }
    };


    $scope.UpdateParentFee = function (parentrow) {
        var feeSum = 0;
        parentrow.FeeMonthly.forEach(element => {
            if (element.IsRowSelected == true) {
                feeSum += Math.round(element.Amount, 2);
            }
        });
        parentrow.Amount = feeSum;
    };
    $scope.getFee = function () {
        var curDate = this.gridModel.Slider.value;
        var sum = 0;
        $.each(this.gridModel.Slider.options.stepsArray, function (index, objModel) {
            if (objModel.value == curDate) {
                sum = sum + objModel.legend;
                return false;
            }
            sum = sum + objModel.legend;

        });
        this.gridModel.PayAmount = sum;
        return sum;
    }

    $scope.GetTotalFeePayAmount = function (data) {
        if (typeof (data) === 'undefined') {
            return 0;
        }
        var sum = 0;
        $.each(data, function (index, objModel) {
            $.each(objModel.FeeTypes, function (index, objModelinner) {
                sum = sum + objModelinner.Amount;
            });
        });
        return sum;
    }

    $scope.getstudentTransportDetails = function () {
        $scope.StudentTransportDetails = null;
        //showOverlay();
        $.ajax({
            type: "GET",
            data: { StudentID: $scope.activeStudentID },
            url: utility.myHost + "/Home/GetStudentTransportDetails",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.length != 0) {
                    $scope.StudentTransportDetails = result[0];
                    $scope.isStudentTransport = true;
                }
                else {
                    $scope.isStudentTransport = false;
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    angular.element(document).ready(function () {
        $(document).on('click', '.otherCandidates', function () {
            $scope.$apply(function () {
                var element = angular.element('#Activities');
                element.addClass('Active');
            });
            var currentElement = $(this);
            var studentID = $(this).attr('id');
            $scope.activeStudentID = studentID;

            $("#CandidateIDfromOutSide").val(studentID);
            $scope.onOtherCandidatesclick(studentID);
            //$scope.ProgressReport(studentID);
            $scope.getstudentTransportDetails();
            //$("#StudentProfile").show();
        });

    });

    //on other candidates click function
    $scope.onOtherCandidatesclick = function (studentID) {
        if (studentID !== null && studentID > 0) {
            $scope.activeStudentID = studentID;
            showOverlay();
            $scope.$apply(function () {
                $.each($scope.Students, function (index, objModel) {
                    if (objModel.StudentIID === Number(studentID)) {
                        objModel.IsSelected = true;
                        $scope.SelectedStudent = {
                            "ClassID": "" + objModel.ClassID + "", "ClassName": "" + objModel.ClassName + "",
                            "SectionID": "" + objModel.SectionID + "", "SectionName": "" + objModel.SectionName + "",
                            "StudentID": "" + objModel.StudentIID + "", "StudentName": "" + objModel.FirstName + ' ' + objModel.MiddleName + ' ' + objModel.LastName + ""

                        };
                        $scope.activeStudentClassID = objModel.ClassID;
                        $scope.activeStudentSectionID = objModel.SectionID;


                    } else {
                        objModel.IsSelected = false;
                    }
                });

                $scope.studentProfile(studentID);
                //$("#StudentProfile").show();
                $scope.mywardsTabSelection('ChangeStudent');
                //$scope.fillInvoice(studentID);
                //$scope.feeHistory(studentID);
                //$scope.fillMarkList(studentID);
                //$scope.fillSkillRegister(studentID, 1);//Progress Report
                //$scope.fillAssignments(studentID);
                //$scope.studentLeave(studentID);
                // $scope.fillWeekDay();
                //$scope.fillClassTime();
                //$scope.timeTable(studentID);
                //$scope.examList(studentID);
                //$scope.studentTransfer(studentID);
                //$scope.classTeacherDetail(studentID);
                //$scope.leaveApplication(studentID);
                //$scope.transferApplication(studentID);

                //$scope.getFeeMonthly(studentID);
                //$scope.getFines(studentID);
                //$scope.getMarkList(studentID);
                //$scope.getAssignments(studentID);
                //$scope.feeHistory(studentID);
                //$scope.fillReasons();
            });

            hideOverlay();
        }
    };
    //end on other candidates click function

    //tab selection click
    $scope.mywardsTabSelection = function (id) {
        $("#StudentProfile").hide();
        $timeout(function () {
            if (id == 'StudentProfile') {
                $("#aboutme").hide();
                $("#TransportRoute").show();
                $("#StudentProfile").show();
            }
            else if (id == 'Attendance') {
                $scope.getStudentAttendance();
            }
            else if (id == "Fees") {
                $scope.fillInvoice($scope.activeStudentID);
                //$scope.feeHistory($scope.activeStudentID);
            }
            else if (id == "Fines") {
                $scope.getFines($scope.activeStudentID);
            }
            else if (id == "TimeTable") {
                $scope.fillClassTime();
            }
            else if (id == "Assignments") {
                //$scope.fillAssignments($scope.activeStudentID);
                $scope.getAssignments($scope.activeStudentID);
                $scope.studentsubjectlist($scope.activeStudentID);
            }
            //else if (id == "Attendance") {
            //    $scope.LoadAttendenceData();
            //}
            else if (id == "ExamList") {
                $scope.examList($scope.activeStudentID);
                //$scope.fillMarkList($scope.activeStudentID);
                $scope.getMarkList($scope.activeStudentID);
            }
            else if (id == "TCRequest") {
                $scope.fillReasons();
                $scope.studentTransfer($scope.activeStudentID);
                $scope.transferApplication.StudentID = $scope.activeStudentID;
            }
            else if (id == "ProgressReport") {
                // $scope.fillStudentProgressReport($scope.activeStudentID);
                $scope.getProgressReport($scope.activeStudentID);
            }
            else if (id == "Leave") {
                $scope.studentLeave($scope.activeStudentID);
            }
            else if (id == "Teacher") {
                $scope.classTeacherDetail($scope.activeStudentID);
            }
            else if (id == "Circular") {
                $("#Circular").show();
                $scope.getCircularList();
            }
            else if (id == "Agenda") {
                $("#Agenda").show();
                $scope.getAgendaList($scope.activeStudentID);
            }
            else if (id == "LessonPlan") {
                $("#LessonPlan").show();
                $scope.getLessonPlanList($scope.activeStudentID);
            }
            else if (id == 'ChangeStudent') {
                $('.nav-link').removeClass('active');
                $("#StudentProfile").show();
                $("#StudentMoreDetail").removeClass('active');
                $("#Fees").removeClass('active');
                $("#Assignments").removeClass('active');
                $("#Leave").removeClass('active');
                $("#TeacherDetails").removeClass('active');
                $("#Attendance").removeClass('active');
                $("#TCRequest").removeClass('active');
                $("#Topic").removeClass('active');
                $("#LessonPlanParentPortal").removeClass('active');
                $("#ProgressReport").removeClass('active');
                $("#Assessment").removeClass('active');
            }
            else if (id == "Assessment") {
                $scope.InitiateAssessment();
            }
            else {
                $("#aboutme").show();
                $("#TransportRoute").hide();
            }
        }, 0);

        return true;
    }
    //end tab selection 

    $scope.GetTotalFeeAmount = function (data) {
        if (typeof (data) === 'undefined') {
            return 0;
        }

        var sum = 0;
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].Amount)
                sum += parseFloat(data[i].Amount);

        }

        return sum;
    };

    function showOverlay() {
        $("#MyWardOverlay").fadeIn();
        $("#MyWardOverlayButtonLoader").fadeIn();
    }

    $scope.fillAssignments = function (studentId) {
        showOverlay();
        $scope.AssignmentList = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId, SubjectID: SubjectID },
            url: utility.myHost + "/Home/GetAssignmentStudentwise",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.AssignmentList = result.Response;
                }
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }

    $scope.fillWeekDay = function () {
        //showOverlay();
        $scope.WeekDay = [];
        $.ajax({
            type: "GET",
            url: utility.myHost + "Home/GetDynamicLookUpData?lookType=WeekDay&defaultBlank=false",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.WeekDay = result;
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.fillClassTime = function () {
        //showOverlay();
        $scope.ClassTime = [];
        $.ajax({
            type: "GET",

            url: utility.myHost + "Home/GetDynamicLookUpData?lookType=ClassTime&defaultBlank=false",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.ClassTime = result;
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.fillMarkList = function (studentId) {
        //showOverlay();
        $scope.MarkLists = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/GetMarkListStudentwise",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.MarkLists = result.Response;
                    });
                }

                //hideOverlay();
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    // Progress Report Multiple SP Call
    $scope.fillStudentProgressReport = function (studentId) {
        //showOverlay();
        $scope.SkillRegister = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/GetStudentProgressReport",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result !== null) {
                    var ExamMark = []; var resultData = {};
                    $scope.ExamsList = result.Data.examList;
                    var GraphLabel = []; var GraphDataSet = []; var MaxMark = 0; var FinalMark = 0;
                    GraphLabel = []; $scope.ProgressReportGraphLabel = []; $scope.ProgressReportDataSet = [];
                    var tempProgressReportDataSet = [];
                    result.Data.examList.forEach(function (exam) {

                        GraphLabel.push(exam.ExamName);
                        result.Data.markList.forEach(function (mark) {
                            if (exam.ExamID == mark.ExamID) {
                                ExamMark.push(mark);
                                MaxMark = MaxMark + mark.MaximumMarks;
                                FinalMark = FinalMark + mark.Mark;

                            }
                        });
                        FinalMark = (FinalMark / MaxMark) * 100;
                        GraphDataSet.push(FinalMark);
                        FinalMark = 0; MaxMark = 0;
                        exam.MarkList = ExamMark;



                        ExamMark = []; //GraphLabel = []; GraphDataSet = [];
                    });
                    $scope.ProgressReportDataSet = [
                        {
                            label: GraphLabel,
                            data: GraphDataSet,
                            backgroundColor: "#ED7D31",
                            borderColor: "#ED7D31",
                            fill: false,
                            pointradius: 4,
                            pointhoverradius: 5,
                        },
                    ]
                    $scope.ProgressReportGraphLabel = GraphLabel;
                    $scope.StudentMarkList = result.Data.examList;
                    $scope.studentprogressReportGraph();
                    $scope.isOpenProgressReportGraph = true;
                }

                //hideOverlay();
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    //Progress Report Graph Binding
    $scope.studentprogressReportGraph = function () {

        var config = {
            type: "line",
            data: {
                labels: $scope.ProgressReportGraphLabel,
                datasets: $scope.ProgressReportDataSet,
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    position: "bottom",
                },
                hover: {
                    mode: "index",
                },
                scales: {
                    xAxes: [
                        {
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: "Month",
                            },
                        },
                    ],
                    yAxes: [
                        {
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: "Value",
                            },
                        },
                    ],
                },
                title: {
                    display: false,
                },
            },
        };

        var ctx = document.getElementById("bar-chart-Student-Progress").getContext("2d");
        ctx.height = 100;
        window.myLine = new Chart(ctx, config);
    }
    //End Progress Report Graph Binding

    // Progress Report
    $scope.fillSkillRegister = function (studentId, classId) {
        //showOverlay();
        $scope.SkillRegister = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId, ClassId: classId },
            url: utility.myHost + "/Home/GetStudentSkillRegister",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.Exams = result.Response.Exams;
                        $scope.SkillRegister = result.Response.Subjects;
                    });
                }

                //hideOverlay();
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.fillInvoice = function (studentId) {
        //showOverlay();
        $scope.FeeMonthly = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/FillFeeDue",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {

                    $scope.$apply(function () {
                        $scope.FeeTypes = result.Response;
                    });
                }
                hideOverlay();
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.feeHistory = function (studentId) {

        //showOverlay();
        $scope.FeeMonthlyHis = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/GetStudentFeeCollection",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {

                    $scope.$apply(function () {
                        $scope.FeeTypeHis = result.Response;
                    });
                }
                hideOverlay();
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.studentProfile = function (studentId) {

        //showOverlay();
        $scope.StudentDetails = [];
        $scope.ParentDetails = [];
        $scope.StudentDetailsFull = [];

        if (studentId) {
            $.ajax({
                type: "GET",
                data: { studentId: studentId },
                url: utility.myHost + "/Home/GetStudentDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.StudentDetails = result.Response[0];
                            $scope.getstudentTransportDetails();
                        });
                    }
                    $("#getStudentAttendancebtn").click();
                },
                error: function () {

                },
                complete: function (result) {
                    //hideOverlay();
                }
            });

            $.ajax({
                type: "GET",
                data: { studentId: studentId },
                url: utility.myHost + "/Home/GetGuardianDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.ParentDetails = result.Response;
                        });
                    }
                    $("#getStudentAttendancebtn").click();
                },
                error: function () {

                },
                complete: function (result) {
                    //hideOverlay();
                }
            });
        }

        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/UserApplications",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.StudentDetailsFull = result.Response[0];
                    });
                }
                $("#getStudentAttendancebtn").click();
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.classTeacherDetail = function (studentId) {

        showOverlay();
        $scope.TeacherDetails = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/GetTeacherDetails",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.TeacherDetails = result.Response;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.studentsubjectlist = function (studentId) {

        showOverlay();
        $scope.TeacherDetails = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/Getstudentsubjectlist",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.studentsubjectlist = result.Response;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.timeTable = function (studentId) {

        //showOverlay();
        $scope.ClassTimeTable = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/GetClassTimeTable",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.ClassTimeTable = result.Response;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.examList = function (studentId) {

        //showOverlay();
        $scope.ExamLists = [];
        $scope.ExamSubjects = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/GetExamLists",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.ExamLists = result.Response;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.studentLeave = function (studentId) {
        showOverlay();
        $scope.StudentLeaveApplications = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/GetStudentLeaveApplication",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.StudentLeaveApplications = result.Response;
                    });

                    let date_pickers = document.querySelectorAll('.datepicker');
                    $.each(date_pickers, function () {
                        let options = { format: 'dd/mm/yyyy' }
                        options.setDefaultDate = true,
                            options.defaultDate = new Date();
                        M.Datepicker.init(this, options);
                        this.value = "";
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.leaveApplication = function (studentId) {

        $('#StudentID').val(studentId);
    };

    $scope.submitLeaveApplication = function () {
        var Reason = $("#Reason").val();
        if (Reason != null && Reason != "") {
            showOverlay();
            var leaveApplication = {};
            $('#leaveApplication').serializeArray().map(function (item) {
                if (leaveApplication[item.name]) {
                    if (typeof (leaveApplication[item.name]) === "string") {
                        leaveApplication[item.name] = [config[item.name]];
                    }
                    leaveApplication[item.name].push(item.value);
                }
                else if (item.name == "StudentID") {
                    leaveApplication[item.name] = $scope.activeStudentID;
                }
                else if (item.name == "FromDateString") {
                    leaveApplication[item.name] = moment(FromDateString.value).format('DD/MM/YYYY');
                }
                else if (item.name == "ToDateString") {
                    leaveApplication[item.name] = moment(ToDateString.value).format('DD/MM/YYYY');
                }
                else {
                    leaveApplication[item.name] = item.value;
                }
            });

            $("#SubmitLeaveBtn").html("Submitting...");
            $("#SubmitLeaveBtn").prop("disabled", true);

            $.ajax({
                type: "POST",
                data: JSON.stringify(leaveApplication),
                url: utility.myHost + "/Home/SubmitLeaveApplication",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.leaveApplication = result.Response;
                    }
                    //-------------------------------------------   
                    //$scope.studentLeave(leaveApplication.StudentID);
                    $('#leaveApplication')[0].reset();
                    $("#Leave .featuresTab ul li, #Leave div.featureContent").removeClass("active");
                    $('#Leave .featuresTab ul li[data-attr="LeaveList"], #Leave div.featureContent[data-attr="LeaveList"]').addClass('active');
                    $("#SubmitLeaveBtn").html("Submit");
                    $("#SubmitLeaveBtn").prop("disabled", false);
                    if (result.Response.IsError == false) {
                        callToasterPlugin('success', 'Leave applied successfully');
                    }
                    else {
                        callToasterPlugin('error', result.Response.ErrorMessage);
                    }

                },
                error: function () {

                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
    };

    $scope.submitAssignmentDocument = function () {
        //var Reason = $("#Reason").val();
        showOverlay();
        var leaveApplication = {};
        leaveApplication[ProfileUploadFile] = $("#ProfileUploadFile").val();
        leaveApplication[AttachmentName] = $("#AttachmentName").val();
        leaveApplication[Description] = $("#Description").val();
        leaveApplication[Notes] = $("#Notes").val();
        //$('#leaveApplication').serializeArray().map(function (item) {
        //    if (leaveApplication[item.name]) {
        //        if (typeof (leaveApplication[item.name]) === "string") {
        //            leaveApplication[item.name] = [config[item.name]];
        //        }
        //        leaveApplication[item.name].push(item.value);
        //    }
        //    else if (item.name == "StudentID") {
        //        leaveApplication[item.name] = $scope.activeStudentID;
        //    }
        //    else if (item.name == "FromDateString" || item.name == "ToDateString") {
        //        leaveApplication[item.name] = moment(FromDateString.value).format('DD/MM/YYYY');
        //    }
        //    else {
        //        leaveApplication[item.name] = item.value;
        //    }
        //});

        //$("#SubmiAssignmentDcoumentBtn").html("Submitting...");
        //$("#SubmiAssignmentDcoumentBtn").prop("disabled", true);

        $.ajax({
            type: "POST",
            data: JSON.stringify(leaveApplication),
            url: utility.myHost + "/Content/UploadContents",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    //$scope.leaveApplication = result.Response;
                }
                //-------------------------------------------   
                //$scope.studentLeave(leaveApplication.StudentID);
                //$('#leaveApplication')[0].reset();
                //$("#Leave .featuresTab ul li, #Leave div.featureContent").removeClass("active");
                //$('#Leave .featuresTab ul li[data-attr="LeaveList"], #Leave div.featureContent[data-attr="LeaveList"]').addClass('active');
                //$("#SubmitLeaveBtn").html("Submit");
                //$("#SubmitLeaveBtn").prop("disabled", false);
                //if (result.Response.IsError == false) {
                //    callToasterPlugin('success', 'Leave applied successfully');
                //}
                //else {
                //    callToasterPlugin('error', 'Leave not applied');
                //}

            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.EditLeaveRequestClick = function (requestId) {
        $scope.LeaveApplication = [];
        $("#Leave #Leave_01").removeClass("active");
        $('#Leave #Leave_02').addClass('active tab-pane');
        $.ajax({
            type: "GET",
            url: utility.myHost + "/Home/GetleaveApplication?Id=" + requestId,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.LeaveApplication = result.Response;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.NewLeaveApplicationClick = function () {

        $("#Leave .featuresTab ul li, #Leave div.featureContent").removeClass("active");
        $('#Leave .featuresTab ul li[data-attr="LeaveApplication"], #Leave div.featureContent[data-attr="LeaveApplication"]').addClass('active');
        $('#leaveApplication')[0].reset();
    };


    $scope.studentTransfer = function (studentId) {
        showOverlay();
        $scope.StudentTransferApplications = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/GetStudentTransferApplication",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.StudentTransferApplications = result.Response;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.transferApplication = function (studentId) {
        $('#StudentID').val(studentId);
    };

    $scope.onReasonDataChange = function () {
        var id = $scope.StudentTransferRequestReasons.Key;
        if (id == ("7")) {
            $("#txtReasonOther").val("");
            $scope.onOtherReason = true;
        }
        else {
            $scope.onOtherReason = false;
        }
    };

    $scope.TransportApplication_checkbox1 = function () {
        if (document.getElementById("IsSchoolChange").checked) {
            document.getElementById("IsLeavingCountry").checked = false;
        }
        else {
            document.getElementById("IsLeavingCountry").checked = true;
        }
    }

    $scope.TransportApplication_checkbox2 = function () {
        if (document.getElementById("IsLeavingCountry").checked) {
            document.getElementById("IsSchoolChange").checked = false;
        }
        else {
            document.getElementById("IsSchoolChange").checked = true;
        }
    }

    $scope.fillReasons = function () {
        //showOverlay();
        $scope.ReasonData = [];
        var OtherData = {};
        $.ajax({
            type: "GET",
            url: utility.myHost + "Home/GetDynamicLookUpData?lookType=StudentTransferRequestReasons&defaultBlank=false",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.ReasonData = result;
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.buttonAction = function () {
        var reason = $("#StudentTransferRequestReasons").val();
        if (reason == null || reason == "" || reason == "?") {
            callToasterPlugin('error', "Please Select Reason");
            $("#SubmitTcRequestBtn").html("Submit");
            $("#SubmitTcRequestBtn").prop("disabled", false);
            return false;
        }
    };

    //not using
    $scope.submitTransferApplication = function () {
        var reason = $("#StudentTransferRequestReasons").val();
        if (reason != null && reason != "" && reason != "?") {
            showOverlay();
            var transferApplication = {};
            $('#transferApplication').serializeArray().map(function (item) {
                if (transferApplication[item.name]) {
                    if (typeof (transferApplication[item.name]) === "string") {
                        transferApplication[item.name] = [config[item.name]];
                    }
                    transferApplication[item.name].push(item.value);
                }
                else if (item.name == "StudentID") {
                    transferApplication[item.name] = $scope.activeStudentID;
                }
                else if (item.name == "ExpectingRelivingDateString") {
                    transferApplication[item.name] = moment(ExpectingRelivingDateString.value).format('DD/MM/YYYY');

                }
                else {
                    transferApplication[item.name] = item.value;
                }
            });
            transferApplication.StudentID = $('#StudentID').val();

            $("#SubmitTcRequestBtn").html("Submitting...");
            $("#SubmitTcRequestBtn").prop("disabled", true);
            $.ajax({
                type: "POST",
                data: JSON.stringify(transferApplication),
                url: utility.myHost + "/Home/SubmitTransferApplication",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.transferApplication = result.Response;
                    }
                    //-------------------------------------------   
                    //$scope.studentTransfer(transferApplication.StudentID);
                    $('#transferApplication ')[0].reset();
                    $("#TCRequest .featuresTab ul li, #TCRequest div.featureContent").removeClass("active");
                    $('#TCRequest .featuresTab ul li[data-attr="TCRequestList"], #TCRequest div.featureContent[data-attr="TCRequestList"]').addClass('active');
                    $("#SubmitTcRequestBtn").html("Submit");
                    $("#SubmitTcRequestBtn").prop("disabled", false);
                    if (result.Response.IsError == false) {
                        callToasterPlugin('success', 'Transfer Request applied successfully');
                    }
                    else {
                        callToasterPlugin('error', result.Response.ErrorMessage);
                    }

                },
                error: function () {

                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
    };

    $scope.EditTransferRequestClick = function (requestId) {

        $scope.transferApplication = [];
        $("#TCRequest #TCRequest01").removeClass("active");
        $('#TCRequest #TCRequest02').addClass('active');
        $.ajax({
            type: "GET",
            url: utility.myHost + "/Home/GetTransferApplication?Id=" + requestId,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.transferApplication = result.Response;
                        $('#StudentTransferRequestReasons').val($scope.transferApplication.TransferRequestReasonID);
                        if ($scope.transferApplication.TransferRequestReasonID == ("7")) {
                            $("#txtReasonOther").val("");
                            $scope.onOtherReason = true;
                        }
                        else {
                            $scope.onOtherReason = false;
                        }
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.NewTCRequestApplicationClick = function () {
        $("#TCRequest #TCRequest01").removeClass("active");
        $('#TCRequest #TCRequest02').addClass('active');
        //$('#transferApplication ')[0].reset();
    };

    $scope.getFeeMonthly = function (studentId) {

        $scope.FeeMonthly = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/FillFeeDue",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.FeeTypes = result.Response;
                }
                $scope.FeeMonthlyHis = [];
                $.ajax({
                    type: "GET",
                    data: { studentId: studentId },
                    url: utility.myHost + "/Home/GetFeeCollected",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result !== null) {
                            $scope.FeeTypeHis = result.Response;
                        }

                    },
                    error: function () {

                    },
                    complete: function (result) {
                        //hideOverlay();
                    }
                });
            },
            error: function () {

            },
            complete: function (result) {

            }
        });
    }


    $scope.getFines = function (studentId) {

        $scope.FineDues = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/FillFineDue",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);
                if (!result.IsError && result !== null) {
                    $scope.FineDues = result.Response;
                }
                $scope.FineHis = [];
                $.ajax({
                    type: "GET",
                    data: { studentId: studentId },
                    url: utility.myHost + "/Home/GetFineCollected",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result !== null) {
                            $scope.FineHis = result.Response;
                        }

                    },
                    error: function () {

                    },
                    complete: function (result) {
                        //hideOverlay();
                    }
                });
            },
            error: function () {

            },
            complete: function (result) {

            }
        });
    }

    $scope.getMarkList = function (studentId) {

        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "/Home/GetMarkListStudentwise",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.MarkLists = result.Response;
                }
                //-------------------------------------------              
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }

    $scope.openAssignmentDocument = function (assignment) {
        $("#AssignmentDocumentUploadPanel").show();
        $(".AssignmentListPanel").hide();
        var StudentId = $("#CandidateIDfromOutSide").val();
        var AssignmentIID = assignment.AssignmentIID;
        $("#StudentId").val(StudentId);
        $("#AssignmentIID").val(AssignmentIID);
    }

    $scope.getAssignments = function (studentId, Subject) {

        if ($scope.IsAssignmentOpened) {
            table.destroy();
        }
        $scope.IsAssignmentOpened = true;
        var subjectId = Subject?.Key;

        showOverlay();

        $.ajax({
            type: "GET",
            data: { studentId: studentId, SubjectID: subjectId },
            url: utility.myHost + "/Home/GetAssignmentStudentwise",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.AssignmentList = result.Response;


                    function format(d) {
                        // `d` is the original data object for the row
                        const array1 = d.Attachments;





                        for (let item of array1) {
                            $scope.DownloadURL = function (url) {
                                var link = document.createElement("a");
                                link.href = url;
                                document.body.appendChild(link);
                                link.click();
                                document.body.removeChild(link);
                                delete link;
                            };
                            return (
                                '<dl>' +
                                '<dt>Description</dt>' +
                                '<dd>' +
                                item.Description +
                                '</dd>' +
                                '<dt>Notes</dt>' +
                                '<dd ng-if="item.Notes != null">' +
                                item.Notes +
                                '</dd>' +
                                '<dt>Attachment</dt>' +
                                `<a class="btn btn-icon-white btn-sm btn-primary me-2" href="ReadContentsByID?contentID=${item.ContentFileIID}"><i class="fa fa-2x fa-download fs-3" aria-hidden="true" ng-click="DownloadURL('ReadContentsByID?contentID='${item.ContentFileIID})"></i>download</a>` +
                                '</dl>'

                            );


                        }


                        //return array1;
                    }

                    table = $('#myTable').DataTable({
                        searching: false,
                        lengthMenu: [
                            [5, 10, 25, 50, -1],
                            ['5 rows', '10 rows', '25 rows', '50 rows', 'Show all']
                        ],
                        data: result.Response,
                        "columns": [
                            {
                                className: 'dt-control',
                                orderable: false,
                                data: null,
                                defaultContent: ''
                            },
                            { data: "CreatedDateString" },
                            { data: "Subject.Value" },
                            { data: "Title" },
                            { data: "Description" },
                            { data: "FreezeDateString" },
                            { data: "AssignmentType.Value" }

                        ],
                        order: [[1, 'dsc']]
                    });

                    table.on('click', 'td.dt-control', function (e) {
                        let tr = e.target.closest('tr');
                        let row = table.row(tr);

                        if (row.child.isShown()) {
                            // This row is already open - close it
                            row.child.hide();
                        }
                        else {
                            // Open this row
                            row.child(format(row.data())).show();
                        }
                    });


                }
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }


    $scope.getCircularList = function () {
        showOverlay();
        $.ajax({
            type: "GET",
            data: { parentId: 0 },
            url: utility.myHost + "/Home/GetCircularList",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.CircularList = result.Response;
                    }
                });
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }
    $scope.getAgendaList = function (studentId) {
        showOverlay();
        $.ajax({
            type: "GET",
            data: { studentID: studentId },
            url: utility.myHost + "/Home/GetAgendaList",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.AgendaList = result.Response;
                    }
                });
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }

    $scope.getLessonPlanList = function (studentId) {
        showOverlay();
        $.ajax({
            type: "GET",
            data: { studentID: studentId },
            url: utility.myHost + "/Home/GetLessonPlanList",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.LessonPlanList = result.Response;
                    }
                });
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }

    $scope.ExpandCollapase = function (event, model, field) {
        model[field] = !model[field];
        var $groupRow = $(event.currentTarget).closest('tr').next();

        if (model[field]) {
            $groupRow.show();
        } else {
            $groupRow.hide();
        }
    };

    $scope.NewApplicationClick = function () {
        window.location = "NewApplicationFromSibling?loginID=" + $rootScope.LoginID;
    };
    $scope.CircularClick = function () {
        window.location = "CircularList?loginID=" + $rootScope.LoginID;
    };
    $scope.NewApplicationGuestClick = function () {
        window.location = "NewApplication?loginID=" + $rootScope.LoginID;
    };

    $scope.ApplicationListClick = function () {
        window.location = "ApplicationListFromSibling";
    };

    $scope.CircularListClick = function () {
        window.location = "CircularList";
    };

    //function hideOverlay() {
    //    $("#MyWardOverlay").fadeOut();
    //    $("#MyWardOverlayButtonLoader").fadeOut();
    //}

    $scope.FeePaymentClick = function () {

        window.location.replace(utility.myHost + "/Home/FeepaymentGateway");

    };

    $scope.SaveFeepaymentGateway = function () {
        $.ajax({
            type: "POST",
            //data: { studentId: $scope.Students[0].StudentIID },
            url: utility.myHost + "/Home/SaveFeepaymentGateway",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                //if (!result.IsError && result !== null) {
                //    $scope.AssignmentList = result.Response;
                //}
            },
            //error: function () {

            //},
            complete: function (result) {
                hideOverlay();
            }
        });


    };

    $scope.MonthDate = function () {
        var date = new Date($scope.SelectedYear, $scope.SelectedMonth + 1, 0).getDate();
        $scope.SelectedMonthDate = date;
    };

    $scope.GetWeekStartDay = function () {
        var filterMonth = $scope.ShortMonthList.find(x => x.num == $scope.SelectedMonth + 1);
        $scope.WeekStartDay = (new Date("01/" + filterMonth.name + "/" + $scope.SelectedYear)).getDay();
        return $scope.WeekStartDay;
    };

    //Get Attendence 
    $scope.getStudentAttendance = function () {
        //if (isNaN(parseInt($("#attendanceYear").val())) || parseInt($("#attendanceYear").val()) == null) {
        //    /*return null;*/

        //    $("#attendanceYear").val($scope.CurrentDate.getFullYear());
        //    $("#attendanceMonth").val($scope.CurrentDate.getMonth() + 1);
        //}
        //else {
        //    $scope.SelectedMonth = parseInt($("#attendanceMonth").val() - 1);
        //    $scope.SelectedYear = parseInt($("#attendanceYear").val());
        //}
        //$scope.LoadAttendenceData();
        if (isNaN(parseInt($("#attendanceYear").val())) || parseInt($("#attendanceYear").val()) == null) {
            return null;
        }
        $scope.SelectedMonth = parseInt($("#attendanceMonth").val());
        $scope.SelectedYear = parseInt($("#attendanceYear").val());

        $scope.GetWeekStartDay();
        $scope.LoadData();
        $scope.GetCalendarDatas();

    }


    $scope.LoadData = function () {
        $rootScope.ShowLoader = true;
        $.ajax({
            type: 'GET',
            url: utility.myHost + "/Home/GetAcademicCalenderByMonthYear?month=" + ($scope.SelectedMonth + 1) + '&year=' + $scope.SelectedYear,
            success: function (result2) {
                $scope.HoliDayData = result2;

            },
            complete: function (result) {
                $scope.LoadAttendenceData();
            },
            error: function () {
                $rootScope.ShowLoader = false;
            },
        });

    };

    $scope.LoadAttendenceData = function () {
        //$rootScope.ShowLoader = true;
        //$(".PageLoaderBG_v02").show();

        $scope.AttendanceFullData = [];
        $.ajax({
            type: 'GET',
            url: utility.myHost + "/Home/GetStudentAttendenceByYearMonthStudentId?month=" +
                $scope.SelectedMonth + '&year=' + $scope.SelectedYear + '&studentId=' + $scope.activeStudentID,
            success: function (result) {
                $scope.$apply(function () {
                    $scope.AttendenceData = result;

                    $scope.AttendanceFullData = [];
                    if ($scope.AttendenceData.length > 0) {

                        var totalDate = $scope.SelectedMonthDate;

                        for (var i = 1; i <= totalDate; i++) {
                            var totalDate = $scope.SelectedMonthDate;

                            var attendanceDataNew = $scope.AttendenceData.find(a => i == moment(a.AttendenceDate).format("D") &&
                                ($scope.SelectedMonth + 1) == moment(a.AttendenceDate).format("M") &&
                                $scope.SelectedYear == moment(a.AttendenceDate).format("YYYY")
                            );

                            if (attendanceDataNew != 'undefined' && attendanceDataNew != null) {

                                var dateValue = attendanceDataNew.AdmissionDate != null ? parseInt(attendanceDataNew.AdmissionDate.replace(/\/Date\((\d+)\)\//g, "$1")) : null;
                                var admissionDate = new Date(dateValue);

                                var feeStartDateString = moment(attendanceDataNew.FeeStartDate).format("MM/DD/YYYY");
                                var feeStartDate = new Date(feeStartDateString);

                                if ($scope.HoliDayData != null) {
                                    var holiDay = $scope.HoliDayData.find(h =>
                                        moment(attendanceDataNew.AttendenceDate).format("D") == h.Day &&
                                        ($scope.SelectedMonth + 1) == h.Month &&
                                        $scope.SelectedYear == h.Year
                                    );
                                }

                                var dataStatus = false; var PresentStatusID = 9;
                                var PresentStatusTitle = 'UM';

                                if (holiDay != 'undefined' && holiDay != null) {
                                    if (holiDay.AcademicYearCalendarEventType == '1') {
                                        dataStatus = false;
                                        PresentStatusID = 3;
                                        PresentStatusTitle = 'H';
                                    }
                                    else if (holiDay.AcademicYearCalendarEventType == '2') {
                                        dataStatus = false;
                                        PresentStatusID = 10;
                                        PresentStatusTitle = 'W';
                                    }
                                    else {
                                        dataStatus = true;
                                        PresentStatusID = 9;
                                        PresentStatusTitle = 'UM';
                                    }

                                }
                                else {
                                    dataStatus = true;
                                    PresentStatusID = 9;
                                    PresentStatusTitle = 'UM';
                                }

                                if (PresentStatusTitle == null || PresentStatusTitle == 'undefined') {
                                    dataStatus = true;
                                    PresentStatusID = 9;
                                    PresentStatusTitle = 'UM';
                                }
                                else {
                                    PresentStatusID = attendanceDataNew.PresentStatusID;
                                    PresentStatusTitle = attendanceDataNew.PresentStatusTitle;
                                }
                                if (PresentStatusID == null) {
                                    PresentStatusID = 9;
                                    PresentStatusTitle = 'UM';
                                }

                                $scope.AttendanceFullData.push({
                                    month: $scope.SelectedMonth, year: $scope.SelectedYear, day: moment(attendanceDataNew.AttendenceDate).format("D"),
                                    status: dataStatus,
                                    statusId: PresentStatusID,
                                    statusTitle: PresentStatusTitle,
                                    reason: (typeof (holiDay) == 'undefined' || holiDay == null) ? ' ' : holiDay.EventTitle,
                                    actualDate: new Date(moment(attendanceDataNew.AttendenceDate).format("D") + '/' + $scope.Months[$scope.SelectedMonth] + '/' + $scope.SelectedYear.toString()),
                                    feeStartDate: feeStartDate,
                                });
                            }
                            else {

                                if ($scope.HoliDayData != null) {
                                    var holiDay = $scope.HoliDayData.find(h => i == h.Day &&
                                        ($scope.SelectedMonth + 1) == h.Month &&
                                        $scope.SelectedYear == h.Year
                                    );
                                }

                                if (holiDay != 'undefined' && holiDay != null) {
                                    if (holiDay.AcademicYearCalendarEventType == '1') {
                                        dataStatus = false;
                                        PresentStatusID = 3;
                                        PresentStatusTitle = 'H';
                                    }
                                    else if (holiDay.AcademicYearCalendarEventType == '2') {
                                        dataStatus = false;
                                        PresentStatusID = 10;
                                        PresentStatusTitle = 'W';
                                    }
                                    else {
                                        dataStatus = true;
                                        PresentStatusID = 9;
                                        PresentStatusTitle = 'UM';
                                    }

                                }
                                else {
                                    dataStatus = true;
                                    PresentStatusID = 9;
                                    PresentStatusTitle = 'UM';
                                }

                                $scope.AttendanceFullData.push({
                                    month: $scope.SelectedMonth, year: $scope.SelectedYear, day: i,
                                    status: dataStatus,
                                    statusId: PresentStatusID,
                                    statusTitle: PresentStatusTitle,
                                    reason: (typeof (holiDay) == 'undefined' || holiDay == null) ? ' ' : holiDay.EventTitle,
                                    actualDate: new Date(i.toString() + '/' + $scope.Months[$scope.SelectedMonth] + '/' + $scope.SelectedYear.toString()),
                                    feeStartDate: feeStartDate,
                                });

                            }
                        }
                    }//IF condition end
                    else {

                        var totalDate = $scope.SelectedMonthDate;

                        for (var i = 1; i <= totalDate; i++) {

                            var dataStatus = true; var PresentStatusID = 9;
                            var PresentStatusTitle = 'UM';

                            if ($scope.HoliDayData.length > 0) {
                                var holiDay = $scope.HoliDayData.find(h => i == h.Day &&
                                    ($scope.SelectedMonth + 1) == h.Month &&
                                    $scope.SelectedYear == h.Year
                                );

                                if (holiDay != 'undefined' && holiDay != null) {
                                    if (holiDay.AcademicYearCalendarEventType == '1') {
                                        dataStatus = false;
                                        PresentStatusID = 3;
                                        PresentStatusTitle = 'H';
                                    }
                                    else if (holiDay.AcademicYearCalendarEventType == '2') {
                                        dataStatus = false;
                                        PresentStatusID = 10;
                                        PresentStatusTitle = 'W';
                                    }
                                    else {
                                        dataStatus = true;
                                        PresentStatusID = 9;
                                        PresentStatusTitle = 'UM';
                                    }

                                }
                                else {
                                    dataStatus = true;
                                    PresentStatusID = 9;
                                    PresentStatusTitle = 'UM';
                                }
                            }

                            if (PresentStatusTitle == null || PresentStatusTitle == 'undefined') {
                                dataStatus = true;
                                PresentStatusID = 9;
                                PresentStatusTitle = 'UM';
                            }
                            if (PresentStatusID == null) {
                                PresentStatusID = 9;
                                PresentStatusTitle = 'UM';
                            }

                            $scope.AttendanceFullData.push({
                                month: $scope.SelectedMonth, year: $scope.SelectedYear, day: i,
                                status: dataStatus,
                                statusId: PresentStatusID,
                                statusTitle: PresentStatusTitle,
                                reason: ' ',
                                actualDate: new Date(i + '/' + $scope.Months[$scope.SelectedMonth] + '/' + $scope.SelectedYear.toString()),
                                feeStartDate: '',
                            });
                        }

                    }
                    $scope.GetCalendarDatas();

                    //$scope.GetMonthDays();
                    //$(".PageLoaderBG_v02").hide();
                })
            },
            complete: function (result) {

            }

        });

    };

    $scope.GetMonthDays = function () {
        $scope.MonthDays = [];

        for (var j = 1; j <= $scope.WeekStartDay; j++) {
            $scope.MonthDays.push({ 'Day': null, 'StatusData': 'null' })
        }

        var days = [].constructor($scope.SelectedMonthDate).length;
        for (var i = 1; i <= days; i++) {
            $scope.MonthDays.push({ 'Day': i, 'StatusData': GetAttendanceSlot(i) })
        }
    }

    function GetAttendanceSlot(day) {

        if ($scope.AttendanceFullData.length > 0) {
            var slot = $scope.AttendanceFullData.find(x => x.day == day);
            if (slot != undefined || slot != null) {
                return slot.statusTitle;
            }
            else {
                return "UM";
            }
        }

        return null;
    };

    //End to get Attendence

    $scope.ClosePopup = function () {
        $("#ItemPopup").fadeOut("fast");
        $(".gridItemOverlay").hide();
    }

    $scope.OpenPopup = function () {
        $("#ItemPopup").fadeIn("fast");
        $(".gridItemOverlay").show();
    }

    $scope.SubmitPopup = function () {
        $("#ItemPopup").fadeOut("fast");
        $(".gridItemOverlay").hide();
    }

    $("#attendanceMonth").val(7);
    $("#attendanceYear").val(2021);

    function showOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#FeeListOverlay").fadeIn();
                $("#FeeListOverlayButtonLoader").fadeIn();
                $("#FeeHisListOverlay").fadeIn();
                $("#FeeHisListOverlayButtonLoader").fadeIn();
                $("#AssignmentListOverlay").fadeIn();
                $("#AssignmentListOverlayButtonLoader").fadeIn();
                $("#LeaveListOverlay").fadeIn();
                $("#LeaveListOverlayButtonLoader").fadeIn();
                $("#TeacherListOverlay").fadeIn();
                $("#TeacherListOverlayButtonLoader").fadeIn();
                $("#TCListOverlay").fadeIn();
                $("#TCListOverlayButtonLoader").fadeIn();
                $("#CircularListOverlay").fadeIn();
                $("#CircularListOverlayButtonLoader").fadeIn();
                $("#TopicListOverlay").fadeIn();
                $("#TopicListOverlayButtonLoader").fadeIn();
                $("#LessonPlanListOverlay").fadeIn();
                $("#LessonPlanListOverlayButtonLoader").fadeIn();
                $("#AssessmentOverlay").fadeIn();
                $("#AssessmentOverlayButtonLoader").fadeIn();
                $scope.ShowPreLoader = true;
            });
        });
    }

    function hideOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#FeeListOverlay").fadeOut();
                $("#FeeListOverlayButtonLoader").fadeOut();
                $("#FeeHisListOverlay").fadeOut();
                $("#FeeHisListOverlayButtonLoader").fadeOut();
                $("#AssignmentListOverlay").fadeOut();
                $("#AssignmentListOverlayButtonLoader").fadeOut();
                $("#LeaveListOverlay").fadeOut();
                $("#LeaveListOverlayButtonLoader").fadeOut();
                $("#TeacherListOverlay").fadeOut();
                $("#TeacherListOverlayButtonLoader").fadeOut();
                $("#TCListOverlay").fadeOut();
                $("#TCListOverlayButtonLoader").fadeOut();
                $("#CircularListOverlay").fadeOut();
                $("#CircularListOverlayButtonLoader").fadeOut();
                $("#TopicListOverlay").fadeOut();
                $("#TopicListOverlayButtonLoader").fadeOut();
                $("#LessonPlanListOverlay").fadeOut();
                $("#LessonPlanListOverlayButtonLoader").fadeOut();
                $("#AssessmentOverlay").fadeOut();
                $("#AssessmentOverlayButtonLoader").fadeOut();
                $scope.ShowPreLoader = false;
            });
        });
    }

    $scope.DownloadURL = function (url) {
        var link = document.createElement("a");
        link.href = url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };


    $scope.getProgressReport = function (studentId) {
        showOverlay();
        $.ajax({
            url: utility.myHost + "/Home/GetProgressReport",
            method: 'GET',
            data: { studentId: studentId },
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.ProgressReportURL = utility.myHost + result;
                    });
                }
            },
            error: function () { },
            complete: function (result) {
                hideOverlay();
            }
        });
    }

    function callToasterPlugin(status, title) {
        new Notify({
            status: status,
            title: title,
            effect: 'fade',
            speed: 300,
            customClass: null,
            customIcon: null,
            showIcon: true,
            showCloseButton: true,
            autoclose: true,
            autotimeout: 3000,
            gap: 20,
            distance: 20,
            type: 1,
            position: 'center'
        })
    };

    $scope.myClickFunction = function () {
        if ($scope.routeDatas == true) {
            $scope.routeDatas = false;
        }
        else {
            $scope.routeDatas = true;
        }
    }
    Fancybox.bind('[data-fancybox="gallery"]', {
        Thumbs: false,
        //Toolbar: false,
        //animated: false,
        //dragToClose: false,

        //showClass: false,
        //hideClass: false,

        //closeButton: "top",

        Image: {
            //click: "close",
            wheel: "slide",
            //zoom: false,
            //fit: "cover",
        },

        //Thumbs: {
        //    minScreenHeight: 0,
        //},
    });
    $scope.FillMonthDaysAttendance = function () {

        $scope.MonthDate();

        $scope.MonthDaysAttendance = [];

        var monthDays = [].constructor($scope.SelectedMonthDate).length;

        for (var day = 1; day <= monthDays; day++) {
            var slotData = $scope.GetAttendanceSlotData(day);
            $scope.MonthDaysAttendance.push({
                'title': slotData.Title, 'start': $scope.SelectedYear + "-" + ('0' + ($scope.SelectedMonth + 1)).slice(-2) + "-" + ('0' + day).slice(-2),
                'color': slotData.Color,
                'textColor': slotData.textColor,
                'className': "d-flex rounded-2 flex-center  w-50px"
            });
        }
        return $scope.MonthDaysAttendance;
    }

    $scope.GetAttendanceSlotData = function (day) {
        var dayData = {};
        if ($scope.AttendanceFullData.length > 0) {
            var slot = $scope.AttendanceFullData.find(x => x.day == day);
            if (slot != undefined || slot != null) {

                dayData = {
                    "Title": slot.statusTitle,
                    "Color": $scope.GetAttendanceSlotColor(day),
                    "textColor": $scope.GetAttendanceSlotTextColor(day),
                    "Description": slot.statusDescription
                };
            }
            else {
                dayData = {
                    "Title": "UM",
                    "Color": "#90a4ae",
                    "Description": "Unmarked"
                };
            }
        }

        return dayData;
    };
    $scope.GetAttendanceSlotTextColor = function (day) {
        var textColor = null
        if ($scope.AttendanceFullData.length > 0) {
            var slot = $scope.AttendanceFullData.find(x => x.day == day);
            if (slot != undefined || slot != null) {

                if (slot.statusTitle == "P") {
                    textColor = "#fff";
                }
                else if (slot.statusTitle == "A") {
                    textColor = "#fff";
                }
                else if (slot.statusTitle == "AE") {
                    textColor = "#3F4254";
                }
                else if (slot.statusTitle == "H") {
                    textColor = "#111111";
                }
                else if (slot.statusTitle == "L") {
                    textColor = "#ce93d8";
                }
                else if (slot.statusTitle == "T") {
                    textColor = "#3F4254";
                }
                else if (slot.statusTitle == "TE") {
                    textColor = "#3F4254";
                }
                else if (slot.statusTitle == "W") {
                    textColor = "#3F4254";
                }
                else if (slot.statusTitle == "NA") {
                    textColor = "#3F4254";
                }
                else {
                    textColor = "#3F4254";
                }
            }
            else {
                textColor = "#90a4ae";
            }
        }

        return textColor;
    };

    $scope.GetAttendanceSlotColor = function (day) {
        var color = null;

        if ($scope.AttendanceFullData.length > 0) {
            var slot = $scope.AttendanceFullData.find(x => x.day == day);
            if (slot != undefined || slot != null) {

                if (slot.statusTitle == "P") {
                    color = "#28a745";
                }
                else if (slot.statusTitle == "A") {
                    color = "#dc3545";
                }
                else if (slot.statusTitle == "AE") {
                    color = "#e57373";
                }
                else if (slot.statusTitle == "H") {
                    color = "yellow";
                }
                else if (slot.statusTitle == "L") {
                    color = "#ce93d8";
                }
                else if (slot.statusTitle == "T") {
                    color = "#b2ebf2";
                }
                else if (slot.statusTitle == "TE") {
                    color = "#b2dfdb";
                }
                else if (slot.statusTitle == "W") {
                    color = "#E1E3EA";
                }
                else if (slot.statusTitle == "NA") {
                    color = "#e0e0e0";
                }
                else {
                    color = "#f8f9fa";
                }
            }
            else {
                color = "#90a4ae";
            }
        }

        return color;
    };

    $scope.GetDateByMonth = function () {
        var slot1 = ''
        slot1 = $scope.SelectedYear + "-" + ('0' + ($scope.SelectedMonth + 1)).slice(-2) + "-" + '01';
        return slot1;
    };

    $scope.GetCalendarDatas = function () {
        //const element = document.getElementById("calendar");

        var calendarEl = document.getElementById("calendar");
        var calendar = new FullCalendar.Calendar(calendarEl, {
            headerToolbar: {
                left: "",
                center: "title",
                right: ""
            },


            height: 620,
            contentHeight: 600,
            aspectRatio: 3,

            nowIndicator: true,



            initialView: "dayGridMonth",
            // initialDate:"2014-02-01",
            initialDate: $scope.GetDateByMonth(),

            editable: false,
            dayMaxEvents: true, // allow "more" link when too many events
            navLinks: true,
            events: $scope.FillMonthDaysAttendance(),
            eventContent: function (info) {
                var element = $(info.el);

                if (info.event.extendedProps && info.event.extendedProps.description) {
                    if (element.hasClass("fc-day-grid-event")) {
                        element.data("content", info.event.extendedProps.description);
                        element.data("placement", "top");
                        KTApp.initPopover(element);
                    }
                }
            }
        });

        calendar.render();
    }

    //#region Assessment related
    $scope.InitiateAssessment = function () {

        $scope.AcademicYears = [];
        $scope.TermExams = [];
        $scope.ExamGroups = [];
        $scope.ProgressReports = [];

        $scope.SelectedExamGroup = {};
        $scope.SelectedAcademicYear = {};
        $scope.SelectedTermExam = {};

        $scope.GetExamGroupData();
        $scope.GetAcademicYearsByStudentID($scope.activeStudentID);
    };

    $scope.GetExamGroupData = function () {
        $scope.ExamGroups = [];
        $scope.SelectedExamGroup = {};

        $http({
            method: 'Get',
            url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=ExamGroups&defaultBlank=false",
        }).then(function (result) {
            $scope.ExamGroups = result.data;
        });
    };

    $scope.GetAcademicYearsByStudentID = function (studentID) {
        $scope.AcademicYears = [];
        $scope.ProgressReports = [];

        $scope.SelectedAcademicYear = {};

        $scope.IsShowAssessmentLoader = true;

        var studentDetail = $scope.Students.find(s => s.StudentIID == studentID);
        var schoolID = studentDetail != null || studentDetail != undefined ? studentDetail.SchoolID : null;

        if (!schoolID) {
            return false;
        }
        else {
            showOverlay();
            $.ajax({
                type: "GET",
                data: { schoolID: schoolID },
                url: utility.myHost + "/Home/GetAcademicYearBySchool?schoolID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result) {
                        $scope.AcademicYears = result.Response;
                    }
                },
                complete: function (result) {
                    if ($scope.AcademicYears.length == 1) {
                        $scope.AcademicYears.forEach(x => {
                            if (x.Key != null) {
                                $scope.$apply(function () {
                                    $timeout(function () {
                                        $scope.SelectedAcademicYear.Key = x.Key;
                                        $scope.SelectedAcademicYear.Value = x.Value;
                                    }, 1000);
                                });
                            };
                        });
                    }
                    hideOverlay();
                },
                error: function () {
                    hideOverlay();
                }
            });
        }
    };

    $scope.GetExamsList = function () {
        $scope.TermExams = [];
        $scope.ProgressReports = [];

        $scope.SelectedTermExam = {};

        $scope.IsShowAssessmentLoader = true;

        var studentDetail = $scope.Students.find(s => s.StudentIID == $scope.activeStudentID);

        var classID = studentDetail.ClassID;
        var sectionID = studentDetail.SectionID;
        var examGroupID = $scope.SelectedExamGroup != null || $scope.SelectedExamGroup != undefined ? $scope.SelectedExamGroup.Key : null;
        var academicYearID = $scope.SelectedAcademicYear != null || $scope.SelectedAcademicYear != undefined ? $scope.SelectedAcademicYear.Key : null;

        if (!academicYearID) {
            callToasterPlugin('error', "Please select an academic year to access the exam.");
            return false;
        }
        else if (!examGroupID) {
            callToasterPlugin('error', "Please select a term to access the exam.");
            return false;
        }
        else if (!classID || !sectionID) {
            callToasterPlugin('error', "Unable to retrieve exams. Please try again later.");
            return false;
        }
        else {
            showOverlay();

            $.ajax({
                type: "GET",
                data: { classID: classID, sectionID: sectionID, examGroupID: examGroupID, academicYearID: academicYearID },
                url: utility.myHost + "/Home/GetExamsByClassAndGroup",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result) {
                        $scope.$apply(function () {
                            $timeout(function () {
                                $scope.TermExams = result.Response;
                            }, 1000);
                        });
                    }
                },
                complete: function (result) {
                    if ($scope.TermExams.length == 1) {
                        $scope.TermExams.forEach(x => {
                            if (x.Key != null) {
                                $scope.$apply(function () {
                                    $timeout(function () {
                                        $scope.SelectedExam.Key = x.Key;
                                        $scope.SelectedExam.Value = x.Value;
                                    }, 1000);
                                });
                            };
                        });
                    }
                    hideOverlay();
                },
                error: function () {
                    hideOverlay();
                }
            });
        }
    };

    $scope.GetPublishedProgressReports = function () {
        $scope.ProgressReports = [];

        $scope.IsShowAssessmentLoader = true;

        var studentID = $scope.activeStudentID;
        var examID = $scope.SelectedTermExam != null || $scope.SelectedTermExam != undefined ? $scope.SelectedTermExam.Key : null;

        if (!examID) {
            callToasterPlugin('error', "Please select a exam to access the progress reports.");
            return false;
        }
        else if (!studentID) {
            callToasterPlugin('error', "Unable to get progress reports at this time, please try again later.");
            return false;
        }
        else {
            showOverlay();

            $.ajax({
                type: "GET",
                data: { studentID: studentID, examID: examID },
                url: utility.myHost + "/Home/GetStudentPublishedProgressReports",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError) {
                        $scope.ProgressReports = result.Response;

                        $scope.IsShowAssessmentLoader = false;
                    }
                },
                complete: function (result) {
                    hideOverlay();
                },
                error: function () {
                    hideOverlay();
                }
            });
        }
    };
    //#endregion Assessment related ending

}]);