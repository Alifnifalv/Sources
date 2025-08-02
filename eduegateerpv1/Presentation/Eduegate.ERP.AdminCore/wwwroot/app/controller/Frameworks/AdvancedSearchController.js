app.controller("AdvancedSearchController", ['commonService', "$scope", "$compile", "$timeout", function (commonService, $scope, $compile, $timeout) {
    console.log("AdvancedSearchController");
    var vm = this;
    vm.commonService = commonService;

    vm.Model = {};
    var view = null;
    var viewFullPath = null;
    $scope.RowModel = {};
    $scope.RowModel.rows = [];
    vm.AdvanceSearchRunTimeFilter = null
    vm.TotalRecords = 0;
    vm.TotalPages = 0;
    vm.CurrentPage = 0;
    vm.PageSize = 0;
    vm.SortBy = null;
    vm.IsWayPointAttached = false;
    vm.IsWayPointInProcess = false;
    var windowContainer = null;
    var xhrFilterRequest = null;
    var windowres, modelres, vres, viewfullpathres = null;
    vm.Waypoint = null;

    $scope.init = function (window, model, v, viewfullpath) {
        windowres = window; modelres = model; vres = v; viewfullpathres = viewfullpath;
        windowContainer = '#' + window;
        $scope.Model = JSON.parse(model);
        view = v;
        viewFullPath = viewfullpath;
        if (v == "SalesOrderAdvanceSearch" || "CustomerContacts") {
            $scope.SearchClick();
        }
        $('.popup-wrap .load', $(windowContainer)).hide();
    }

    $scope.ResetSearchClick = function () {
        console.log($scope.Model);
        $scope.Model.forEach(function (index) {
            index.FilterValue = null;
            index.FilterValue2 = null;
            index.FilterValue3 = null;
        });
        $scope.SearchClick();
    }

    function ResetFilter(filter) {
        $('.preload-overlay', $(windowContainer)).show();
        $('.preloader', $(windowContainer)).show();
        if (xhrFilterRequest != null) {
            xhrFilterRequest.abort();
        }
        xhrFilterRequest =
            $.ajax({
                type: "POST",
                url: 'Mutual/ResetFilter',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify($scope.Model),
                success: FitlerSuccess,
            });
    }

    $scope.SearchClick = function () {
        $('.load', $(windowContainer)).show();
        $scope.RowModel.rows = [];
        SaveFilter();
    }

    function SaveFilter() {
        $('.preload-overlay', $(windowContainer)).show();
        $('.preloader', $(windowContainer)).show();
        if (xhrFilterRequest != null) {
            xhrFilterRequest.abort();
        }
        xhrFilterRequest =
            $.ajax({
                type: "POST",
                url: 'Mutual/SaveFilter',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify($scope.Model),
                success: FitlerSuccess,
            });
    }

    function FitlerSuccess() {
        vm.TotalRecords = 0;
        vm.TotalPages = 0;
        vm.CurrentPage = 0;
        vm.PageSize = 0;
        vm.IsWayPointInProcess = false;
        vm.SortBy = null;
        $scope.RowModel.rows = [];
        $('.load', $(windowContainer)).show();
        if ($scope.$parent.AdvanceSearchRunTimeFilter != null) {
            vm.AdvanceSearchRunTimeFilter = $scope.$parent.AdvanceSearchRunTimeFilter
        }
        
        $('#AttachWayPointsDiv', $(windowContainer)).show();
        $('.popup-wrap .load', $(windowContainer)).show();
        vm.commonService.get('Frameworks/Search/SearchData?view=' + view + '&currentPage=' + (vm.CurrentPage + 1) + "&orderby=" + vm.SortBy + "&runtimeFilter=" + vm.AdvanceSearchRunTimeFilter + "&startDate=" + GetDBSynchTime(), vm.param, LoadSuccess);
    }

    function GetDBSynchTime() {
        var newDateTime = new Date();
        return newDateTime.getDate() + "/" + newDateTime.getMonthName() + "/" + newDateTime.getFullYear() + " " + newDateTime.toLocaleTimeString();
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

        vm.TotalRecords = datas.TotalRecords;
        vm.CurrentPage = datas.CurrentPage;
        vm.TotalPages = datas.TotalPages;
        vm.PageSize = datas.PageSize;

        if (vm.TotalRecords > 0) {
            vm.IsWayPointInProcess = false;
            $("table.listing", $(windowContainer)).next("p").remove();
        }
        else {
            /* when table has no data then we have to show the message */
            $("table.listing", $(windowContainer)).next("p").remove();
            $("table.listing", $(windowContainer)).after("<p><br>Result not found.</p>");
        }

        $timeout(function () {
            if (!vm.IsWayPointAttached) {
                //attachWayPoint();
            }
            else {
                $.waypoints('refresh');
            }
        }, 100);

        $('.popup-wrap .load', $(windowContainer)).hide();
        $('.load', $(windowContainer)).hide();
        $('#AttachWayPointsDiv', $(windowContainer)).hide();
    }

    function attachWayPoint() {
        vm.Waypoint = $('#AttachWayPointsDiv', $(windowContainer)).waypoint(function (direction) {
            if (direction == "down" && vm.CurrentPage != vm.TotalPages && vm.IsWayPointInProcess == false) {
                $('.popup-wrap .load').show();
                vm.IsWayPointInProcess = true;
                vm.commonService.get(view + '/SearchData?currentPage=' + (vm.CurrentPage + 1) + "&orderby=" + vm.SortBy + "&runtimeFilter=" + vm.AdvanceSearchRunTimeFilter, vm.param, LoadSuccess);
            }
        }, {
            offset: '100%',
            triggerOnce: false
        });
    }

    $scope.CloseSearch = function () {
        var parentContainerID = $(windowContainer).attr('parentcontainerID');
        if (parentContainerID != undefined && parentContainerID != '') {
            $("#advanceSearchForCRUD", $("#" + parentContainerID)).html('');
        }
        else {
            $("#advanceSearchForCRUD").html('');
        }
        $('.popup').removeClass('show').hide();
        $('.preload-overlay', $(windowContainer)).hide();
        $('.preloader', $(windowContainer)).hide();
    }

    $scope.HideFilterPanel = function (event) {
        $(event.currentTarget).toggleClass('slide');
        if ($(event.currentTarget).hasClass('slide')) {
            $('.fl-advsearch-section', $(windowContainer)).slideUp(200);
        }
        else {
            $('.fl-advsearch-section', $(windowContainer)).slideDown(200);
        }
    }

    $scope.RowModel.RowClick = function (viewName, index, event, data) {
        $scope.$parent.AdvanceSearchCallBack(view, data, viewFullPath);
        $scope.CloseSearch();
    }
}]);