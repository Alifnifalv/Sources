app.controller("DataManagementController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {

    $scope.Tables = [];
    $scope.ServerSize = [];
    $scope.FormattedServerSize = [];
    $scope.chartInstance = null;
    $scope.LastYearDate = null;

    $scope.Init = function (model, windowname, type) {
        //$scope.type = type;
        $scope.CrudWindowContainer = '#' + windowname;

        if (type == 'time') {
            $scope.FillTableDetails();
            $scope.LastYearDate();
            $scope.GetScheduleTypes();
        }
        else if (type == 'Backup')
        {
            $scope.DatabaseDetails();
            $scope.GetBackupTypes();
        }
        else if (type == 'ArchiveSchedule')
        {
            $scope.GetScheduledTables();
        }
        else
        {
            $scope.CheckTempDirectory();
        }
    }

    function showErrorMessage(message) {
        $().showMessage($scope, $timeout, true, message);
    }

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.ModifyTables = function (model) {

        $scope.TableData = model;

        //$scope.modalTitle = "Modify Tables";
        $scope.showLoading = true;
        $scope.showConfirmation = false;
        $scope.showMap = false;

        $('#globalPopup').modal('show');

        // Use $timeout instead of setTimeout
        $timeout(function () {
            $scope.showLoading = false;
            $scope.showConfirmation = true;
        }, 1000); // Simulating a loading time of 1 second
    };

    $scope.ConfirmModify = function () {
        var table = $scope.TableData.TableName;
        var date = $scope.TillDate;
        $http({
            method: 'POST',
            //data: $scope.TableData,
            //data: { tableName: $scope.TableData.TableName, Date: $scope.TillDate.toString() },
            url: "DataManagements/DataManagement/ArchiveTable?tableName=" + table + "&Date=" + date,
        }).then(function (result) {
            // Handle the result of the HTTP request
            console.log('Table modification confirmed.');
            $scope.showConfirmation = false;
            $scope.showMap = true;
        });
    };

    $scope.CancelModify = function () {
        $('#globalPopup').modal('hide');
        console.log('Table modification canceled.');
    };


    $scope.FillTableDetails = function () {
        $http({
            method: 'Get', url: "DataManagements/DataManagement/FillTableDetails",
        }).then(function (result) {
            $scope.Tables = result.data;

        });
    };

    $scope.CheckTempDirectory = function () {
        $http({
            method: 'Get', url: "DataManagements/DataManagement/CheckTempDirectory",
        }).then(function (result) {
            $scope.ServerSize = result.data;

            $scope.FormattedServerSize = {
                TotalSize: FormatSize($scope.ServerSize.TotalSize),
                AvailableSize: FormatSize($scope.ServerSize.AvailableSize),
                TempSize: FormatSize($scope.ServerSize.TempSize)
            };
            //$timeout(function () {
            //    $scope.myPieChart();
            //}, 1000);
            
        });
    };

    function FormatSize(size) {
        if (typeof size === 'string') {
            size = parseFloat(size);
        }

        if (typeof size !== 'number' || isNaN(size)) {
            throw new TypeError("The input size must be a valid number or a numeric string");
        }

        var sizes = ["B", "KB", "MB", "GB", "TB"];
        var formattedSize = size;
        var order = 0;

        while (formattedSize >= 1024 && order < sizes.length - 1) {
            order++;
            formattedSize = formattedSize / 1024;
        }

        return formattedSize.toFixed(2) + " " + sizes[order];
    }

    $scope.DeleteTempFiles = function () {
        //$scope.modalTitle = "Modify Tables";
        $scope.showLoading = true;
        $scope.showConfirmation = false;
        $scope.showMap = false;

        $('#globalPopup').modal('show');

        // Use $timeout instead of setTimeout
        $timeout(function () {
            $scope.showLoading = false;
            $scope.showConfirmation = true;
        }, 1000); // Simulating a loading time of 1 second
    };

    $scope.ConfirmDelete = function () {
        $http({
            method: 'POST',
            url: "DataManagements/DataManagement/DeleteAllFromServer",
        }).then(function (result) {
            // Handle the result of the HTTP request
            $().showGlobalMessage($root, $timeout, false, result.message, 3000);
            $scope.showConfirmation = false;
            $scope.showMap = true;
            $scope.CancelModify();
            $timeout(function () {
                $scope.$apply(function () {
                    $scope.CheckTempDirectory();
                });
            }, 1000);
        });
    };

    $scope.LastYearDate = function () {
        var todayDate = new Date();
        var twoYearsBackDate = new Date();
        twoYearsBackDate.setFullYear(todayDate.getFullYear() - 2);

        $scope.OgTillDate = twoYearsBackDate.toLocaleDateString('en-GB');
        $scope.TillDate = twoYearsBackDate.getDate() + "-" + twoYearsBackDate.getMonth() + "-" + twoYearsBackDate.getFullYear();
    };

    $scope.ScheduleTables = function (model,popupName) {
        var popupName = popupName != null ? '#globalPopup' + popupName : '#globalPopup';
        $scope.TableData = model;

        //$scope.modalTitle = "Modify Tables";
        $scope.showLoading = true;
        $scope.showConfirmation = false;
        $scope.showMap = false;

        $(popupName).modal('show');

        // Use $timeout instead of setTimeout
        $timeout(function () {
            $scope.showLoading = false;
            $scope.showConfirmation = true;
        }, 1000); // Simulating a loading time of 1 second
    };

    $scope.DatabaseDetails = function () {
        $http({
            method: 'Get', url: "DataManagements/DataManagement/DatabaseDetails",
        }).then(function (result) {
            $scope.Databases = result.data;

        });
    }

    $scope.GetScheduleTypes = function () {
        $http({
            method: 'Get', url: "DataManagements/DataManagement/GetScheduleTypes",
        }).then(function (result) {
            $scope.ScheduleTypes = result.data;

        });
    }

    $scope.ScheduleChanges = function (selectedSchedule) {

        var typeID = selectedSchedule.SubscriptionTypeID;

        var todayDate = new Date();
        var filterDate = new Date();
        if (typeID == 1)
        {
            filterDate.setFullYear(todayDate.getFullYear() - 1);
        }
        if (typeID == 2) {
            filterDate.setFullYear(todayDate.getFullYear() - 1);
            filterDate.setMonth(3);
            filterDate.setDate(31);
        }
        if (typeID == 3) {
            filterDate.setMonth(todayDate.getMonth() - 2);
        }

        $scope.TillDate = filterDate.getDate() + "/" + filterDate.getMonth() + "/" + filterDate.getFullYear();
    }

    $scope.DateChanges = function (screen) {
        if (screen == 'Table') {
            $scope.ScheduleDate = document.getElementById("SubsciptionStartDateString").value;
        }
        else if (screen == 'Backup') {
            $scope.ScheduleDate = document.getElementById("BackupDateString").value;
        }
    }


    $scope.ConfirmSchedule = function () {
        var table = $scope.TableData.TableName;
        var date = $scope.OgTillDate;
        var scheduleDate = $scope.ScheduleDate;

        $http({
            method: 'POST',
            url: "DataManagements/DataManagement/ScheduleArchive?tableName=" + table + "&date=" + date + "&scheduleDate=" + scheduleDate,
        }).then(function (result) {
            // Handle the result of the HTTP request
            $().showGlobalMessage($root, $timeout, false, result.data.Message, 3000);
            $scope.showConfirmation = false;
            $scope.showMap = true;
            $scope.CancelSchedule();
            //$timeout(function () {
            //    $scope.$apply(function () {
            //        $scope.CheckTempDirectory();
            //    });
            //}, 1000);
        });
    };

    $scope.CancelSchedule = function () {
        $('#globalPopupSchedule').modal('hide');
        console.log('Table modification canceled.');
    };

    $scope.ShowPopUp = function (model, type) {

        $scope.TableData = model;

        //$scope.modalTitle = "Modify Tables";
        $scope.showLoading = true;
        $scope.showConfirmation = false;
        $scope.showMap = false;

        var popUp = '#globalPopup' + type;

        $(popUp).modal('show');

        // Use $timeout instead of setTimeout
        $timeout(function () {
            $scope.showLoading = false;
            $scope.showConfirmation = true;
        }, 1000); // Simulating a loading time of 1 second
    };

    $scope.CancelPopup = function (type) {
        var popUpWindow = '#globalPopup' + type;

        $(popUpWindow).modal('hide');
        console.log('Table modification canceled.');
    };

    $scope.GetScheduledTables = function () {
        $http({
            method: 'Get', url: "DataManagements/DataManagement/GetScheduledTables",
        }).then(function (result) {
            $scope.ScheduledTables = result.data;

        });
    }

    $scope.ConfirmDeleteSchedules = function () {
        var jobID = document.getElementById("jobID").value;

        $http({
            method: 'POST',
            url: "DataManagements/DataManagement/DeleteSchedule?jobID=" + jobID,
        }).then(function (result) {
            // Handle the result of the HTTP request
            $().showGlobalMessage($root, $timeout, false, result.data.Message, 3000);
            $scope.showConfirmation = false;
            $scope.showMap = true;
            $scope.CancelModify();
            $timeout(function () {
                $scope.$apply(function () {
                    $scope.GetScheduledTables();
                });
            }, 1000);
        });
    };

    $scope.DeleteSchedules = function (model) {
        //var popupName = popupName != null ? '#globalPopup' + popupName : '#globalPopup';
        $scope.TableData = model;

        //$scope.modalTitle = "Modify Tables";
        $scope.showLoading = true;
        $scope.showConfirmation = false;
        $scope.showMap = false;

        $('#globalPopup').modal('show');

        // Use $timeout instead of setTimeout
        $timeout(function () {
            $scope.showLoading = false;
            $scope.showConfirmation = true;
        }, 1000); // Simulating a loading time of 1 second
    };

    $scope.ConfirmDbBackup = function () {
        showOverlay();
        var databaseName = $scope.TableData.DatabaseName;
        var typeID = $scope.BackupType.Key;

        $http({
            method: 'POST',
            url: "DataManagements/DataManagement/BackupDB?databaseName=" + databaseName + "&typeID=" + typeID,
        }).then(function (result) {
            hideOverlay();
            var status = result.data.operationResult == 1 ? false : true;
            $().showGlobalMessage($root, $timeout, status, result.data.Message, 3000);
            $scope.showConfirmation = false;
            $scope.showMap = true;
            $scope.CancelPopup('Backup');
            if (status == false && typeID == 1) {
                window.open(result.data.Message)
                $().showGlobalMessage($root, $timeout, false, "Download complete.");
            }
            else if (status == false && typeID == 0) {
                $().showGlobalMessage($root, $timeout, false, "Backup downloaded in server in the path of " + result.data.Message, 5000);
            }
            else
            {
                $().showGlobalMessage($root, $timeout, true, result.data.Message, 3000);
            }
        });
    };

    $scope.GetBackupTypes = function () {
        $http({
            method: 'Get', url: "DataManagements/DataManagement/GetBackupTypes",
        }).then(function (result) {
            $scope.BackupTypes = result.data;

        });
    }

    $scope.BackupTypeChanges = function (value) {

        $scope.BackupType = value;
    }

    $scope.ConfirmDbScheduleBackup = function () {
        showOverlay();
        var databaseName = $scope.TableData.DatabaseName;
        var typeID = $scope.BackupType.Key;
        var scheduleDate = $scope.ScheduleDate;

        $http({
            method: 'POST',
            url: "DataManagements/DataManagement/ScheduleBackupDB?databaseName=" + databaseName + "&typeID=" + typeID + "&scheduleDate=" + scheduleDate,
        }).then(function (result) {
            hideOverlay();
            var status = result.data.operationResult == 1 ? false : true;
            $().showGlobalMessage($root, $timeout, status, result.data.Message, 3000);
            $scope.showConfirmation = false;
            $scope.showMap = true;
            $scope.CancelPopup('ScheduleBackup');
        });
    };
    
}]);