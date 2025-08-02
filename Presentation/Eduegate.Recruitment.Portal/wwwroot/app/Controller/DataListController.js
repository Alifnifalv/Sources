app.controller("DataListController", ["commonService", "$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function (commonService, $scope, $http, $compile, $window, $timeout, $location, $route, $root) {
        console.log("DataListController Loaded");

        var vm = this;
        vm.windowContainer = null;
        vm.HasFilters = false;
        $scope.ScreenName = null;
        $scope.RowModel = {};
        $scope.RowModel.rows = [];
        $scope.TotalRecords = 0;
        $scope.TotalPages = 0;
        $scope.CurrentPage = 0;
        $scope.PageSize = 0;
        $scope.IsWayPointInProcess = false;
        $scope.SortBy = null;
        $scope.AdvanceSearchRunTimeFilter = null;
        var xhrFilterRequest = null;


        $scope.Init = function (window, model,screen) {
            vm = this;
            vm.commonService = commonService;
            vm.windowContainer = '#' + window;
            $scope.Model = JSON.parse(model);
            $scope.ScreenName = screen;

            LoadPageData($scope.ScreenName);
        };

        $scope.QtyChanges = function (index, item) {
            var test = item;
        }

        $scope.ResetSearchClick = function () {
            $scope.RowModel.rows = [];
            $scope.RowModel.CurrentPage = "1";
            console.log($scope.Model);
            $scope.Model.forEach(function (index) {
                index.FilterValue = null;
                index.FilterValue2 = null;
                index.FilterValue3 = null;
            });
            $scope.QuickFilter = {};
            $scope.SearchClick();
        }

        $scope.SearchClick = function () {
            $scope.RowModel.rows = [];
            $('.load', $(vm.windowContainer)).show();
            SaveFilter();
        }

        function SaveFilter() {
            $scope.RowModel.rows = [];
            $('.preload-overlay', $(vm.windowContainer)).show();
            $('.preloader', $(vm.windowContainer)).show();
            if (xhrFilterRequest != null) {
                xhrFilterRequest.abort();
            }
            xhrFilterRequest =
                $.ajax({
                    type: "POST",
                    url: utility.myHost + 'Mutual/SaveFilter',
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify($scope.Model),
                    success: LoadPageData($scope.ScreenName),
                });
        }

        function LoadPageData(screen) {
            $scope.TotalRecords = 0;
            $scope.TotalPages = 0;
            $scope.CurrentPage = 0;
            $scope.PageSize = 0;
            $scope.IsWayPointInProcess = false;
            $scope.SortBy = null;
            $scope.RowModel.rows = [];
            $('.load', $(vm.windowContainer)).show();
            if ($scope.$parent.AdvanceSearchRunTimeFilter != null) {
                $scope.AdvanceSearchRunTimeFilter = $scope.$parent.AdvanceSearchRunTimeFilter
            }

            $('.popup-wrap .load', $(vm.windowContainer)).show();            
            vm.commonService.get(utility.myHost + 'Search/SearchData?view=' + screen + '&currentPage=' + ($scope.CurrentPage + 1) + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
        }

        function GetDBSynchTime() {
            var newDateTime = new Date();
            return newDateTime.getDate() + "/" + newDateTime.getMonth() + "/" + newDateTime.getFullYear() + " " + newDateTime.toLocaleTimeString();
        }

        function LoadSuccess(response) {
            var datas;
            if (response.data.NewData == undefined)
                datas = JSON.parse(response.data);
            else
                datas = JSON.parse(response.data.NewData);
            $.each(datas.Datas, function (index, row) {
                $scope.RowModel.rows.push(row);
            });

            $scope.TotalRecords = datas.TotalRecords;
            $scope.CurrentPage = datas.CurrentPage;
            $scope.TotalPages = datas.TotalPages;
            $scope.PageSize = datas.PageSize;

            if ($scope.TotalRecords > 0) {
                $scope.IsWayPointInProcess = false;
                $("table.listing", $(vm.windowContainer)).next("p").remove();
            }
            else {
                /* when table has no data then we have to show the message */
                $("table.listing", $(vm.windowContainer)).next("p").remove();
                $("table.listing", $(vm.windowContainer)).after("<p><br>Result not found.</p>");
            }

            $timeout(function () {
                if (!$scope.IsWayPointAttached) {
                }
                else {
                    $.waypoints('refresh');
                }
            }, 100);

            $('.popup-wrap .load', $(vm.windowContainer)).hide();
            $('.load', $(vm.windowContainer)).hide();
        }


        $scope.EditList = function (viewName, index, event, parameter, viewTitle) {
            $scope.EditView(viewName, index, event, true, parameter, viewTitle);
        };

        $scope.EditView = function (viewName, index, event, isCRUD, parameter, viewTitle) {
            event.stopPropagation();
            var windowName = viewName.substring(viewName.indexOf('/') + 1);
            var editUrl;

            if (!viewTitle) {
                viewTitle = viewName;
            }

            switch (windowName) {
                case "VendorRFQList":
                case "VendorPurchaseOrder":
                case "VendorPurchaseReturn":
                    window.location = '/Home/RFQItemList?iid=' + GetIDColumnValue(vm.RowModel.rows[index]) + '&screen=' + viewTitle + '&window=' + windowName;
                    break;
                default:
                    break;

            }
        }

        function GetIDColumnValue(object) {
            var identifier = null;
            identifier = GetColumn(object, 'IID')

            if (identifier == undefined) {
                identifier = GetColumn(object, 'ID');
            }

            if (identifier == undefined) {
                identifier = GetFirstColumn(object);
            }

            return identifier;
        }

        function GetColumn(object, column) {
            for (var key in object) {
                if (key.indexOf(column) != -1) {
                    return object[key];
                }
            }
        }

        function GetFirstColumn(object) {
            for (var key in object) {

                if (key != 'IsRowSelected' && key != '' && key != 'RowCategory')
                    return object[key];
            }
        }

        $scope.FirstPage = function (event) {
            if ($scope.CurrentPage != 1) {
                $scope.RowModel.CurrentPage = "1";
                $scope.RowModel.rows = [];
                $('.tablewrapper .content-load', vm.WindowContainer).show();

                vm.commonService.get(utility.myHost + 'Search/SearchData?view=' + $scope.ScreenName + '&currentPage=' + "1" + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
            }
        };

        $scope.PreviousPage = function (event) {
            if ($scope.CurrentPage != 1) {
                $scope.RowModel.CurrentPage = $scope.CurrentPage - 1;
                $scope.RowModel.rows = [];
                $('.tablewrapper .content-load', vm.windowContainer).show();
                vm.commonService.get(utility.myHost + 'Search/SearchData?view=' + $scope.ScreenName + '&currentPage=' + ($scope.CurrentPage - 1) + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
            }
        };

        $scope.NextPage = function (event) {
            if ($scope.CurrentPage < $scope.TotalPages) {
                $scope.RowModel.CurrentPage = $scope.CurrentPage + 1;
                $scope.RowModel.rows = [];
                $('.tablewrapper .content-load', vm.windowContainer).show();
                vm.commonService.get(utility.myHost + 'Search/SearchData?view=' + $scope.ScreenName + '&currentPage=' + ($scope.CurrentPage + 1) + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
            }
        };

        $scope.LastPage = function (event) {
            if ($scope.CurrentPage != $scope.TotalPages) {
                $scope.RowModel.CurrentPage = $scope.TotalPages;
                $scope.RowModel.rows = [];
                $('.tablewrapper .content-load', vm.windowContainer).show();
                vm.commonService.get(utility.myHost + 'Search/SearchData?view=' + $scope.ScreenName + '&currentPage=' + $scope.TotalPages + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
            }
        }

        $scope.GoToPage = function (event) {

            if ($scope.GoToPageNo >= 1 && $scope.GoToPageNo <= $scope.TotalPages) {
                $scope.RowModel.CurrentPage = $scope.GoToPageNo;
                $scope.RowModel.rows = [];
                $('.tablewrapper .content-load', vm.windowContainer).show();
                vm.commonService.get(utility.myHost + 'Search/SearchData?view=' + $scope.ScreenName + '&currentPage=' + $scope.GoToPageNo + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
            }
        }

        $scope.ShowWindow = function (reportName, reportHeader) {
            // Implementation  
            console.log('ShowWindow called with:', reportName, reportHeader);
            // Return a boolean value based on your logic  
            return false;
        };

        $scope.AddWindow = function (reportName, reportHeader) {
            // Implementation  
            console.log('AddWindow called with:', reportName, reportHeader);
            var windowName = 'someWindowName'; // example logic  
            return windowName;
        };

        $scope.Print = function (event, reportName, reportHeader, listScreen, row, parameter = "") {
            if (!row || !row.ReportName) {
                reportName = reportName;
            } else {
                return false;
            }

            if ($scope.ShowWindow(reportName, reportHeader, reportName) || !row)
                return;

            var windowName = $scope.AddWindow(reportName, reportHeader, reportName);
            var iidColumn = GetColumn(row, 'IID');

            var iidColumn = GetColumnName(row, 'IID');
            //var parameter = "";
            if (parameter == "") {
                if (iidColumn) {
                    parameter = iidColumn + "=" + row[iidColumn];
                }
            }

            var reportingService = $root.ReportingService;

            if (reportingService == "ssrs") {
                //SSRS report viewer start
                $.ajax({
                    url: utility.myHost + "Report/GetReportUrlandParameters?reportName=" + reportName,
                    type: 'GET',
                    success: function (result) {
                        if (result.Response) {
                            var reportUrl = result.Response + "&" + parameter;
                            // Redirect to the Reportview action with the URL as a query parameter  
                            window.location.href = utility.myHost + "Report/Reportview?reportUrl=" + encodeURIComponent(reportUrl) + "&listScreen=" + listScreen;
                        } else {
                            console.error("Failed to get a valid response. No 'Response' found.");
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error("AJAX request failed:", status, error);
                    }
                });
                //SSRS report viewer end
            }
            else if (reportingService == 'bold') {

                //Bold reports viewer start
                var url = utility.myHost + 'ReportViewer/Index?reportName=' + reportName;
                $http({ method: 'Get', url })
                    .then((result) => {
                        $('#' + reportName, '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                        $scope.ShowWindow(reportName, reportHeader, reportName);
                    });
                //Bold reports viewer end
            }
            else {
                //New Report viewer start

                // Split by '&' if there are multiple parameters, otherwise process single parameter
                let commaSeparated = parameter.includes('&') ? parameter.replace(/&/g, ',') : parameter;

                // Convert to JSON-like object
                let parameterObject = {};
                commaSeparated.split(',').forEach(function (param) {
                    let [key, value] = param.split('=');

                    if (key.toLowerCase().includes("date")) {
                        var dateFormat = $root.DateFormat != null ? $root.DateFormat.toUpperCase() : "DD/MM/YYYY";
                        // Using moment to format the date
                        value = moment(value).format(dateFormat);
                    }

                    parameterObject[key] = value || "";  // Assign empty string if no value exists
                });

                // Convert to JSON format
                let parameterString = JSON.stringify(parameterObject);

                var reportUrl = utility.myHost + 'Reports/ReportView/ViewReports?reportName=' + reportName + "&parameter=" + parameterString + "&listScreen=" + listScreen;

                window.location.href = reportUrl;
            }
        };
            
        function GetColumnName(object, column) {
            for (var key in object) {
                if (key.indexOf(column) != -1) {
                    return key;
                }
            }
        }

    }
]);
