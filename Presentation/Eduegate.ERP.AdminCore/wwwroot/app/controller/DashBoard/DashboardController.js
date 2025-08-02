app.controller('DashboardController', ['$scope', '$http', "$compile", "$window", '$location', "$timeout", "$rootScope",
    function ($scope, $http, $compile, $window, $location, $timeout, $root) {
        console.log('DashboardController controller loaded.');
        $scope.runTimeParameter = null;
        $scope.Model = null;
        $scope.window = "";
        $scope.TransactionCount = 0;
        $scope.gridPageNo = 5;
        $scope.CardDatas = [];
        $scope.StudentTransportDetails = {};
        $scope.isStudentTransport = false;
        $scope.StudentID = 0;
        $scope.activeStudentID = 0;
        $scope.HoliDayData = [];
        $scope.AttendenceData = [];
        $scope.AttendanceFullData = [];
        $scope.StudentBehaviorRemarksList = null;
        $scope.PresentStatusCodeData = [];
        $scope.FeeMonthly = [];
        $scope.FeeMonthlyHis = [];
        $scope.FeeTypes = [];
        $scope.FeeTypeHis = [];
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

        $scope.init = function (model, window, viewName) {
            $scope.runTimeParameter = model.RuntimeParameters;
            $scope.Model = model;
            $scope.window = window;
            $scope.StudentID = getParameterValue('REFERENCEID', 'number');
            $scope.activeStudentID = $scope.StudentID;
            $scope.Years = [];
            for (let i = $scope.CurrentDateYear - 5; i <= $scope.CurrentDateYear; i++) {
                $scope.Years.push(i.toString());
            }
            if (window != null && window == "window_UserProfile") {
                LoadUserProfileDetails();
            }
            else if (window != null && window == "window_TeacherTimeTable") {
                var today = new Date();
                var weekDayID = today.getDay() + 1;

                LoadDashBoardTeacherWeeklyTimeTable(weekDayID);
            }
            else if (window != null && window == "window_DataList" || window != null && window == "window_GridList") {
                var pagNo = $scope.gridPageNo;
                LoadGridDatas(pagNo, viewName);
            }
            else if (window != null && window == "window_DirectorsDashBoard") {
                LoadDataForDirectorsDashBoard();
            }
            else if (window != null && window == "window_Notifications") {
                LoadDashBoardNotifications();
            }
            else if (window != null && window == "window_MenuCards") {
                var chartID = model.RuntimeParameters.filter(x => x.Key == "ChartID")[0].Value;
                DashBoardMenuCardCounts(chartID);
            }
            else if (window != null && window == "window_StudentPortfolio") {
                $scope.LoadStudentProfile($scope.StudentID);
            }
            else if (window != null && window == "window_StudentTransportSummary") {
                $scope.LoadStudentTransportDetails();
            }
            else if (window != null && window == "window_StudentAttendance") {
                $scope.MonthDate();
                $scope.GetWeekStartDay();
                $scope.LoadData();
            }
            else if (window != null && window == "window_StudentFeeSummary") {  
               // $scope.onTabChange("Tab_01");
                $scope.FillInvoice($scope.StudentID);
            }
            else if (window != null && window == "_SK_FeeHistory") {

                $scope.GetFeeCollectionHistory($scope.StudentID);
               // $scope.onTabChange("Tab_01");
            }
            else if (window != null && window == "window_StudentBehaviorRemarks") {
                $scope.FillStudentBehaviorRemarks($scope.StudentID);
            }
        };

        $scope.onTabChange = function (tabId) {
            if (tabId == "Tab_01") {
                $scope.TabName = "Tab_01";
                $("#Tab_01_nav").show();
                $("#FeeSummary").show();                
                $("#Tab_02_nav").hide();
                $("#FeeHistory").hide();
                $("#Tab_01_nav").toggleClass('active');
            }
            else if (tabId == "Tab_02") {
                $scope.TabName = "Tab_02";
                $("#Tab_02_nav").show();
                $("#FeeHistory").show();
                $("#Tab_01_nav").hide();
                $("#FeeSummary").hide(); 
                $("#Tab_02_nav").toggleClass('active');
            }
        };

        function LoadDataForDirectorsDashBoard() {
            var url = "Schools/School/LoadDataForDirectorsDashBoard";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result == null || result.data.length == 0) {
                        return false;
                    }
                    const teacherType = ["Full Time", "Part Time"];
                    $scope.TeacherTypes = teacherType;
                    $scope.TitleName = "Teacher Details";
                    $scope.ResultData = result.data;
                }, function () {
                });
        }

        function DashBoardMenuCardCounts(chartID) {
            var url = "Schools/School/GetCountsForDashBoardMenuCards?chartID=" + chartID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result == null || result.data.length == 0) {
                        return false;
                    }

                    const dataList = JSON.parse(result.data.ColumnDatas);

                    dataList.forEach((x, index) => {
                        $scope.CardDatas.push({
                            Header: result.data.ColumnHeaders[index],
                            Value: x
                        });
                    });
                });
        }

        function LoadUserProfileDetails() {
            var url = "Schools/School/UserProfileForDashBoard";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result == null || result.data == null) {
                        return false;
                    }

                    var today = new Date();
                    var dd = String(today.getDate()).padStart(2, '0');
                    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                    var yyyy = today.getFullYear();

                    today = dd + '-' + mm + '-' + yyyy;

                    $scope.CurrentDate = today;
                    $scope.UserProfile = result.data;
                });
        }

        function LoadDashBoardNotifications() {
            var url = "Schools/School/GetNotificationsForDashBoard";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result == null || result.data.length == 0) {
                        return false;
                    }

                    $scope.Notifications = result.data;
                });
        }

        function LoadDashBoardTeacherWeeklyTimeTable(weekDayID) {

            $scope.dayNames = [];

            $scope.dayNames = [
                {
                    "name": "MON",
                    "value": "2",
                    "class": ""
                }, {
                    "name": "TUE",
                    "value": "3",
                    "class": ""
                }, {
                    "name": "WED",
                    "value": "4",
                    "class": ""
                }, {
                    "name": "THU",
                    "value": "5",
                    "class": ""
                }, {
                    "name": "FRI",
                    "value": "6",
                    "class": ""
                }, {
                    "name": "SAT",
                    "value": "7",
                    "class": ""
                }, {
                    "name": "SUN",
                    "value": "1",
                    "class": ""
                }
            ];

            updateClass = $scope.dayNames.findIndex((obj => obj.value == weekDayID));
            $scope.dayNames[updateClass].class = "active";

            var url = "Schools/School/GetWeeklyTimeTableForDashBoard?weekDayID=" + weekDayID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result == null || result.data.TimeTableAllocations.length == 0) {
                        $scope.TimeTable = null;
                        $scope.returnLabel = "No datas found...";
                        return false;
                    }
                    $scope.TimeTable = result.data;
                    $scope.returnLabel = null;
                });
        }


        function LoadGridDatas(toPage, viewName) {

            if (viewName == null || viewName == "" || viewName == undefined) {
                return null;
            }
            var runtimeFilter = "";

            if ($scope.StudentID != 0)
                runtimeFilter = "" + "StudentID=" + $scope.StudentID + "";


            var url = 'Frameworks/Search' + '/SearchData?view=' + viewName + '&currentPage=1&runtimeFilter=' + runtimeFilter + ' &pageSize=' + toPage;
            contentType: "application/json;charset=utf-8",
                $http({ method: 'Get', url: url })
                    .then(function (result) {
                        if (result == null || result.data.length == 0) {
                            return false;
                        }

                        $scope.DataResults = JSON.parse(result.data).Datas;
                        $scope.LoadingLabel = '';
                    });
        }

        $scope.viewMoreDatas = function (gridPageNo) {

            var page = gridPageNo;
            toPage = page + 5;

            LoadGridDatas(toPage);

            $scope.gridPageNo = toPage;

            $scope.LoadingLabel = 'loading...';
        }

        $scope.refreshGrid = function (pageNo) {

            LoadGridDatas(pageNo);

            $scope.gridPageNo = pageNo;

            $scope.LoadingLabel = 'loading...';
        }

        $scope.DayChanges = function (weekDayID) {
            LoadDashBoardTeacherWeeklyTimeTable(weekDayID);
        }

        $scope.ClickFireEvent = function ($event, viewFullPath, title) {

            var passParam = "List," + viewFullPath + ",$event," + title;
            var menuParameters = null;
            var menuEvent = $event;
            if (viewFullPath == null || viewFullPath == undefined || viewFullPath == '') {
                return false;
            }

            $root.FireEvent(menuEvent, passParam, menuParameters);
        }
        //----------------Student Cockpit---------------------

        $scope.LoadStudentProfile = function (studentId) {

            $.ajax({
                type: "GET",
                data: { studentId: studentId },
                url: "Schools/School/GetStudentDetailsWithProfile",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.StudentDetails = result.Response[0];
                            var today = new Date();
                            var dd = String(today.getDate()).padStart(2, '0');
                            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                            var yyyy = today.getFullYear();

                            today = dd + '-' + mm + '-' + yyyy;

                            $scope.CurrentDate = today;
                        });
                    }

                },
                error: function () {

                },
                complete: function (result) {

                }
            });
        }
        $scope.FillStudentInfo = function (studentId) {
            var windowName = 'Student';
            var viewName = 'Student';
            $scope.ReDirectScreen(studentId, windowName, viewName);
        }
        $scope.FillStudentJourney = function (studentID, studentProfile) {

            var name = 'TwinView';
            var title = 'Student Journey';

            var url = utility.myHost + 'Home/TwinView?referenceID=' + studentID + '&imageContentID=' + encodeURIComponent(studentProfile);

            $scope.AddWindow(name, title, name);

            $http({ method: 'Get', url })
                .then((result) => {
                    $('#' + name, '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                    $scope.ShowWindow(name, title, name);
                });

        }

        $scope.FillTransportInfo = function (TransportId) {
            var windowName = 'StudentRouteStopMap';
            var viewName = 'StudentRouteStopMap';
            $scope.ReDirectScreen(TransportId, windowName, viewName);
        }
        $scope.ReDirectScreen = function (ID, windowName, viewName) {
            var windowName = windowName;
            var viewName = viewName;

            if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
                return;

            $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
            editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + ID;

            $http({ method: 'Get', url: editUrl })
                .then(function (result) {
                    $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                    $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
                });
        }
        $scope.LoadScreen = function ($event, url, name, title) {


            $scope.AddWindow(name, title, name);

            $http({ method: 'Get', url })
                .then((result) => {
                    $('#' + name, '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                    $scope.ShowWindow(name, title, name);
                });
        };

        //To convert value to integer.
        $scope.parseInt = function (val) {
            return parseInt(val);
        }
        $scope.LoadStudentTransportDetails = function () {
            $scope.StudentTransportDetails = null;

            $.ajax({
                type: "GET",
                data: { StudentID: $scope.StudentID },
                url: "Schools/School/GetStudentTransportDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result.length != 0) {
                            $scope.isStudentTransport = true;
                            $scope.StudentTransportDetails = result[0];

                        }
                        else {
                            $scope.isStudentTransport = false;
                        }
                    });
                },
                error: function () {

                },
                complete: function (result) {

                }
            });
        }
        $scope.FillInvoice = function (studentId) {

            var studentId = $scope.StudentID;
            $.ajax({
                type: "GET",
                data: { studentId: studentId },
                url: "Schools/School/GetStudentFeeDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.FeeDueDetails = result.Response;
                            $scope.FeeTypes = $scope.FeeDueDetails.StudentFeeDueTypes;
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
        $scope.FillFeeCollection = function (studentID) {

            var studentIID = $scope.StudentID;
            var windowName = 'FeeCollection';
            var viewName = 'Fee Collection';
            var InvoiceNo = "";
            var isAutoFill = true;
            if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
                return;

            $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
            editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + studentIID + "&parameters=" + "IsAutoFill=" + isAutoFill;

            $http({ method: 'Get', url: editUrl })
                .then(function (result) {
                    $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                    $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
                });
        };

        $scope.GetFeeCollectionHistory = function (StudentId) {

            $scope.FeeCollectionHistories = [];
            var studentId = $scope.StudentID;
            $.ajax({
                type: "GET",
                data: { studentId: studentId },
                url: "Schools/School/GetFeeCollectionHistory", 
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.FeeCollectionHistories = result;
                        });
                    }
                },
                complete: function (result) {

                },
                error: function () {

                }
            });
        };
        $scope.ExpandCollapase = function (event, model, field) {
            model[field] = !model[field];
            var $groupRow = $(event.currentTarget).closest('tr').next();

            if (model[field]) {
                $groupRow.show();
            } else {
                $groupRow.hide();
            }
        };
        $scope.FillStudentBehaviorRemarks = function (studentId) {
            $.ajax({
                type: "GET",
                data: { StudentID: studentId },
                url: "Schools/School/GetStudentBehavioralRemarks",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.StudentBehaviorRemarksList = result;
                        });
                    }

                },
                error: function () {

                },
                complete: function (result) {

                }
            });
        }
        //Get Attendence 
        $scope.getStudentAttendance = function () {

            if (isNaN(parseInt($("#attendanceYear").val())) || parseInt($("#attendanceYear").val()) == null) {
                return null;
            }
            $scope.SelectedMonth = parseInt($("#attendanceMonth").val());
            $scope.SelectedYear = parseInt($("#attendanceYear").val());

            $scope.GetWeekStartDay();
            $scope.LoadData();
            $scope.GetCalendarDatas();

        }
        $scope.MonthDate = function () {
            var date = new Date($scope.SelectedYear, $scope.SelectedMonth + 1, 0).getDate();
            $scope.SelectedMonthDate = date;
        };

        $scope.GetWeekStartDay = function () {
            var filterMonth = $scope.ShortMonthList.find(x => x.num == $scope.SelectedMonth + 1);
            $scope.WeekStartDay = (new Date("01/" + filterMonth.name + "" + $scope.SelectedYear)).getDay();
            return $scope.WeekStartDay;
        };

        $scope.LoadData = function () {
            //  $rootScope.ShowLoader = true;
            $.ajax({
                type: 'GET',
                url: "Schools/School/GetAcademicCalenderByMonthYear?month=" + ($scope.SelectedMonth + 1) + '&year=' + $scope.SelectedYear,
                success: function (result2) {
                    $scope.HoliDayData = result2;

                },
                complete: function (result) {
                    $scope.LoadAttendenceData();
                },
                error: function () {
                    // $rootScope.ShowLoader = false;
                },
            });

        };
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
                    'className': "d-flex rounded-2 flex-center  w-auto fs-3 fw-bold"
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
                        textColor = "#0ead1f";
                    }
                    else if (slot.statusTitle == "A") {
                        textColor = "#f37d1a";
                    }
                    else if (slot.statusTitle == "AE") {
                        textColor = "#f54213";
                    }
                    else if (slot.statusTitle == "H") {
                        textColor = "#ea2b15";
                    }
                    else if (slot.statusTitle == "L") {
                        textColor = "#fa06e0";
                    }
                    else if (slot.statusTitle == "T") {
                        textColor = "#855649";
                    }
                    else if (slot.statusTitle == "TE") {
                        textColor = "#b38f86";
                    }
                    else if (slot.statusTitle == "W") {
                        textColor = "#95b3d7";
                    }
                    else if (slot.statusTitle == "NA") {
                        textColor = "#8e0d0d";
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
                        color = "#fff";
                    }
                    else if (slot.statusTitle == "A") {
                        color = "#fff";
                    }
                    else if (slot.statusTitle == "AE") {
                        color = "#fff";
                    }
                    else if (slot.statusTitle == "H") {
                        color = "#fff";
                    }
                    else if (slot.statusTitle == "L") {
                        color = "#fff";
                    }
                    else if (slot.statusTitle == "T") {
                        color = "#fff";
                    }
                    else if (slot.statusTitle == "TE") {
                        color = "#fff";
                    }
                    else if (slot.statusTitle == "W") {
                        color = "#fff";
                    }
                    else if (slot.statusTitle == "NA") {
                        color = "#fff";
                    }
                    else {
                        color = "#fff";
                    }
                }
                else {
                    color = "#fff";
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
                eventBackgroundColor: '#ffffff00',
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

            $timeout(function () {

                calendar.render();

            }, 1000);
        }
        $scope.LoadAttendenceData = function () {
            //$rootScope.ShowLoader = true;
            //$(".PageLoaderBG_v02").show();

            $scope.AttendanceFullData = [];
            $.ajax({
                type: 'GET',
                url: "Schools/School/GetStudentAttendenceByYearMonthStudentId?month=" +
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
        function getParameterValue(parameterName, type) {
            if (!$scope.runTimeParameter) {
                return getDefault(type);
            }

            let keyValueViewModel = $scope.runTimeParameter.find(a => a.Key === parameterName);
            if (!keyValueViewModel || !keyValueViewModel.Value) {
                return getDefault(type);
            }

            return convertType(keyValueViewModel.Value, type);
        }

        function getDefault(type) {
            switch (type) {
                case 'string':
                    return '';
                case 'number':
                    return 0;
                case 'boolean':
                    return false;
                case 'object':
                    return null;
                default:
                    return null;
            }
        }

        function convertType(value, type) {
            switch (type) {
                case 'string':
                    return String(value);
                case 'number':
                    return Number(value);
                case 'boolean':
                    return value.toLowerCase() === 'true';
                case 'object':
                    try {
                        return JSON.parse(value);
                    } catch (e) {
                        return null;
                    }
                default:
                    return value;
            }
        }
        //-------------------------------------

    }]);

