app.controller('DashboardController', ['$scope', '$http', "$compile", "$window", '$location', "$timeout", "$rootScope",
    function ($scope, $http, $compile, $window, $location, $timeout, $root) {
        console.log('DashboardController controller loaded.');
        $scope.runTimeParameter = null;
        $scope.Model = null;
        $scope.window = "";
        $scope.TransactionCount = 0;
        $scope.gridPageNo = 5;
        $scope.CardDatas = [];

        $scope.init = function (model, window,viewName) {
            $scope.runTimeParameter = model.parameter;
            $scope.Model = model;
            $scope.window = window;

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
                            Value : x
                        });
                    });
                });
        }


        function LoadUserProfileDetails() {
            var url = "Schools/School/UserProfileForDashBoard";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result == null || result.data.length == 0) {
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

            var url = 'Frameworks/Search' + '/SearchData?view=' + viewName + '&currentPage=1&pageSize=' +toPage ;
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

        $scope.DayChanges = function (weekDayID){
            LoadDashBoardTeacherWeeklyTimeTable(weekDayID);
        }

        $scope.ClickFireEvent = function ($event,viewFullPath, title) {

            var passParam = "List," + viewFullPath+",$event," + title;
            var menuParameters = null;
            var menuEvent = $event;
            if (viewFullPath == null || viewFullPath == undefined || viewFullPath == '') {
                return false;
            }

            $root.FireEvent(menuEvent, passParam, menuParameters);
        }

    }]);

