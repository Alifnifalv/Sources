
app.controller("ViewController", ['commonService', "$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function (commonService,$scope, $http, $compile, $window, $timeout, $location, $route, $controller,$root) {
    console.log("List Controller");
    var vm = this;
    vm.commonService = commonService;

    $scope.SelectedIds = [];
    $scope.SelectedRow = null;
    $scope.SelectedRowIndex = null;
    $scope.SelectedIID = null;
    $scope.FilteredData = null;

    $scope.Message = null;
    $scope.IsError = false;
    $scope.WindowContainer = null;

    $scope.ChildInit = function (model, ViewIID, windowID) {
        $scope.WindowContainer = windowID;
    }

    $scope.BringQuickFilter = function () {
        $('.headerFilter', $('#' + $scope.WindowContainer)).slideToggle('fast');
    }

    $scope.ChangeJobProfileStatus = function (event) {
        $http({ method: 'Get', url: 'HR/JobProfile/MoveToArchive?id=' + IID })
            .then(function (result) {
                $().showGlobalMessage($scope, $timeout, false, "Applied job moved to archived.");
            });
    };


    $scope.AllocateInvoices = function (event, allocationType) {
        if ($scope.SelectedIID === null) {
            $().showGlobalMessage($scope, $timeout, true, "Please select a voucher for selection.");
            return;
        }

        var name = allocationType === "Receipt" ? "EditRVInvoiceAllocation" : "EditPVInvoiceAllocation";
        var title = allocationType + " Allocations";
        $scope.AddWindow(name, title, name);
        var url = 'Accounts/' + allocationType + 'Allocation/Allocate?invoiceIDs=' + $scope.SelectedIID;

        $http({ method: 'Get', url: url })
            .then(function (result) {
                $('#' + name, "#LayoutContentSection").replaceWith($compile(result.data)($scope));
                $scope.ShowWindow(name, title, name);
            });       
    };

    $scope.MarkAsIN = function (event, status,viewName) {
        var url = 'Frameworks/Search/SearchData?view=' + viewName + '&currentPage=1';
        vm.commonService.get(url, vm.param, LoadSuccess);

        if ($scope.SelectedIds.length > 0) {

            if ($scope.FilteredData && Array.isArray($scope.FilteredData)) {
                var toUpdateData = $scope.FilteredData.filter(item => {
                    return $scope.SelectedIds.some(id => item.IID === id) &&
                        (item.Attendance !== "A" && item.Attendance !== "CL");
                });

                var scheduleIIDList = toUpdateData.map(item => {
                    return { Key: item.INScheduleIID.toString(), Value: null };
                });

                UpdateStatus(scheduleIIDList, status);
            }
        }
        else {
            $().showGlobalMessage($root, $timeout, true, "Please select and mark I/O");
            return false;
        }
    };


    $scope.MarkAsOUT = function (event, status, viewName) {
        var url = 'Frameworks/Search/SearchData?view=' + viewName + '&currentPage=1';
        vm.commonService.get(url, vm.param, LoadSuccess);

        if ($scope.SelectedIds.length > 0) {

            if ($scope.FilteredData && Array.isArray($scope.FilteredData)) {
                var toUpdateData = $scope.FilteredData.filter(item => {
                    return $scope.SelectedIds.some(id => item.IID === id) &&
                        (item.Attendance !== "A" && item.Attendance !== "CL");
                });

                var scheduleIIDList = toUpdateData.map(item => {
                    return { Key: item.OUTScheduleIID.toString(), Value: null };
                });

                UpdateStatus(scheduleIIDList, status);
            }
        }
        else {
            $().showGlobalMessage($root, $timeout, true, "Please select and mark I/O");
            return false;
        }
    };

    function LoadSuccess(response) {

        if (response.data.IsError) {

            if (response.data.IsError == true) {
                $().showGlobalMessage($root, $timeout, true, response.data.Message);
                return;
            }
        }

        var datas = typeof response.data === 'string' ? JSON.parse(response.data) : response.data;
        $scope.FilteredData = datas.Datas;
    }


    function UpdateStatus(scheduleIIDList, status) {
        $.ajax({
            url: "Schools/Transport/UpdateScheduleLogStatus",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                IIDs: scheduleIIDList,
                Status: status,
                IsRowUpdation: false,
            }),
            success: function (response) {
                if (response) {
                    $().showGlobalMessage($root, $timeout, false, response);
                }
            },
            error: function (xhr, status, error) {
                console.log("Error: " + error);
            }
        });

        $scope.SelectedIds = [];
    }

}]);