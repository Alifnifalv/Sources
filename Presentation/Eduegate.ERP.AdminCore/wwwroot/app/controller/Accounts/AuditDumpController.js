app.controller("AuditDumpController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope","$q", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root, $q) {
    console.log("AuditDumpController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.CrudWindowContainer = null;

    $scope.company = null;
    $scope.fiscalyears = null;

    $scope.MainData = {
        "Companies": [],
        "FiscalYear": { "Key": null, "Value": null },
        "FromDateString": null,
        "ToDateString": null,
        "IsConsolidated": false,
        "IsCompSelected": true,
    };
    function showOverlay() {
        $('.preload-overlay', $('#AuditDump')).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $('#AuditDump')).hide();
    }

    $scope.init = function (windowname) {

        $scope.CrudWindowContainer = windowname;
        $scope.IsAllSelected = false;

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Company&defaultBlank=false",
        }).then(function (result) {
            $scope.Company = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=FiscalYear&defaultBlank=false",
        }).then(function (result) {
            $scope.FiscalYears = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=AuditData&defaultBlank=false",
        }).then(function (result) {
            $scope.AuditData = result.data;
        });

    }

    $scope.DownloadDatas = function (auditDataID) {
        var datas = $scope.MainData;

        if (datas) {
            var data = {
                AuditDataID: auditDataID,
                Companies: datas.Companies.map(c => c.Key).join(','), // Comma-separated companies
                FiscalYear_ID: datas.FiscalYear.Key,
                StartDate: datas.StartDate,
                EndDate: datas.EndDate,
                IsConsolidated: datas.IsConsolidated,
            };

            showOverlay();

            $http({
                method: 'POST',
                url: "Accounts/AuditDump/DownloadDatas",
                data: JSON.stringify(data),
                responseType: 'blob',
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(function (response) {
                if (response.status === 200) {

                    var disposition = response.headers('Content-Disposition');
                    var fileName = "AuditData.xlsx";

                    if (disposition) {
                        var match = disposition.match(/filename="?([^"]+)"?/);
                        var fileName = match[1].split(';')[0];
                    }

                    var blob = new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

                    var link = document.createElement('a');
                    var url = window.URL.createObjectURL(blob);
                    link.href = url;
                    link.download = fileName;
                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);

                    $().showGlobalMessage($root, $timeout, false, "File downloaded successfully!");

                    hideOverlay();
                } else {
                    $().showGlobalMessage($root, $timeout, true, "Failed to download file.");
                    hideOverlay();
                }
            }).catch(function (error) {
                $().showGlobalMessage($root, $timeout, true, "Error occurred while downloading.");
                hideOverlay();
            });
        } else {
            $().showGlobalMessage($root, $timeout, true, "No data found to save/update result!");
        }
    };


    $scope.FieldChanges = function () {
        var datas = $scope.MainData;

        if (datas.Companies != null && datas.Companies.length > 0 && datas.FiscalYear != null && datas.StartDate != null && datas.EndDate != null) {
            $scope.IsAllSelected = true;
        }
        else {
            $scope.IsAllSelected = false;
        }

        if (datas.FiscalYear != null) {
            getFinancialYear(datas.FiscalYear.Key)
        }
    }

    function getFinancialYear(fiscalYearID) {
        showOverlay();
        $.ajax({
            type: "GET",
            url: utility.myHost + "Accounts/AuditDump/GetFiscalYearByFiscalYear?fiscalYearID=" + fiscalYearID,
            success: function (result) {
                if (result) {

                    $scope.MainData.StartDate = result.StartDateString;
                    $scope.MainData.EndDate = result.EndDateString;

                    hideOverlay();
                } else {

                    hideOverlay();

                }
            },
            error: function () {

            },
            complete: function () {
                hideOverlay();
            }
        });
    }

}]);

