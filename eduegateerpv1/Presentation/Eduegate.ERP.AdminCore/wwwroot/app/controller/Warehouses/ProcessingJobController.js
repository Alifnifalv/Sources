
app.controller("ProcessingJobController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$interval", function ($scope, $http, $compile, $window, $timeout, $location, $route, $interval) {
    console.log("Processing Job controller");
    var windowContainer = null;
    var ViewName = null;
    var IID = null;
    $scope.ViewModels = [];
    var DefaultDynamicView = null;
    $scope.ShowTime = null;
    $scope.Model = null;
    $scope.LookUps = [];
    $scope.IsJobCompleted = false;
    $scope.HoursRemaining = "";
    $scope.HoursTaken = "";
    var lookLoadCount = 0;

    $interval(function () {
        $scope.ShowTime = Date.now();
    }, 1000);

    $scope.ShowRemainingHours = function (date, operationStatus) {
        if (eval(operationStatus) != 4) {
            if (date != undefined)
                $scope.HoursRemaining = utility.getRemainingHoursText(date, $scope.ShowTime);
            else
                $scope.HoursRemaining = "";
        }

        return $scope.HoursRemaining;
    }

    $scope.ShowHoursTakenForJob = function (date, operationStatus) {
        if (eval(operationStatus) != 4) {
            if (date != undefined)
                $scope.HoursTaken = utility.GetHoursTakenText(date, Date.now(), $scope.Model.MasterViewModel.IsPicked);
            else
                $scope.HoursTaken = "";
        }

        return $scope.HoursTaken;
    }

    $scope.Init = function (window, viewName, model, iid) {
        IID = iid;
        windowContainer = '#' + window;
        $scope.Model = model;
        ViewName = viewName;
        LoadLookups(model.Urls);
        LoadJobDetails();
        $scope.OperationTypeUrl = model.Type;
    }

    function LoadJobDetails() {

        var lookUpLoads = setInterval(function () {
            if (lookLoadCount >= $scope.Model.Urls.length) {
                $http({
                    method: 'Get', url: 'Warehouses/JobOperation/Get?ID=' + IID.toString()
                })
                 .then(function (result) {
                     $scope.Model = result.data;
                     $(".preload-overlay", $(windowContainer)).css("display", "none");
                 });

                clearInterval(lookUpLoads);
            }

        }, 100);

    }

    $scope.CloseSummaryPanel = function (event) {
        $(event.currentTarget, $(windowContainer)).closest('.pagecontent').removeClass('summaryview detail-panel minimize-fields summaryviewbigger');
        $(".preload-overlay", $(windowContainer)).css("display", "none");
        $(windowContainer).find("#summarypanel").html('');
    };

    function LoadLookups(urls) {
        if (urls == undefined || urls == null) {
            return;
        }

        $.each(urls, function (index, url) {
            $scope.LookUps[url.LookUpName] = [{ 'Key': '', Value: 'Loading..' }];
            $.ajax({
                type: "GET",
                url: url.Url,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        $scope.LookUps[url.LookUpName] = result;
                        lookLoadCount++;
                    });
                }
            });
        });
    }

    $scope.SaveJobOperation = function (isDoneFlag, $event) {

        if (isDoneFlag == true) {

            if ($scope.Model.MasterViewModel.JobOperationStatus.Key == 1) { //Job not started
                $().showMessage($scope, $timeout, true, "Still the job is not started");
                return false;
            }

            // handle for packing
            if ($scope.OperationTypeUrl.toLowerCase().indexOf('packing') >= 0) {
                //if ($("#JobOperation").find("input[type=text][verified=true]").length > 0) { //Verification textbox not valid
                //    // don't need to do anything
                //} else {
                //    $().showMessage($scope, $timeout, true, "Few of the product is not verified!");
                //    return false;
                //}
            }
            else {
                if ($("#JobOperation").find("input[type=text][verified=false]").length > 0) { //Verification textbox not valid
                    $().showMessage($scope, $timeout, true, "Few of the product is not verified!");
                    return false;
                }

                // validate if anyof textbox is empty
                var isValidTextBox = true;
                if ($("#JobOperation").find("input[type=text]").each(function () {
                   if (this.value == "") {
                    isValidTextBox = false;
                    return false;
                }
                }));

                if (isValidTextBox == false) {
                    $().showMessage($scope, $timeout, true, "Few of the product is not verified!");
                    return false;
                }

            }

            // check if it has Invoice or not in case of StockOut
            if ($scope.OperationTypeUrl.toLowerCase().indexOf('stockout') >= 0 && $scope.Model.MasterViewModel.TransactionHeadIID) {
                var valid = false;
                $.ajax({
                    url: "JobOperation/ValidateSalesInvoiceForStockOut?jobEntryHeadId=" + $scope.Model.MasterViewModel.TransactionHeadIID,
                    type: 'GET',
                    success: function (result) {
                        valid = result;
                        if (valid.toLowerCase() == "false") {
                            $().showMessage($scope, $timeout, true, "Please create Sales Invoice for this job.");
                            return false;
                        } else {
                            $scope.SaveJob(isDoneFlag,$event);
                        }
                    }
                });
            }
            else {
                $scope.SaveJob(isDoneFlag,$event);
            }
        } else {
            $scope.SaveJob(isDoneFlag);
        }
    }

    $scope.SaveJob = function (isDoneFlag,$event) {
        $(".preload-overlay", $(windowContainer)).css("display", "block");
        $scope.Model.MasterViewModel.IsDoneFlag = isDoneFlag;
        var data = { MasterModel: $scope.Model.MasterViewModel, DetailModel: $scope.Model.DetailViewModel };

        $.ajax({
            type: "POST",
            url: $scope.OperationTypeUrl,
            data: JSON.stringify(data),
            contentType: "application/json;charset=utf-8",
            success: function (result) {

                console.log(result);
                $(".preload-overlay", $(windowContainer)).css("display", "none");

                if (result.IsError == false) {

                    if (isDoneFlag == true) {

                        $().showMessage($scope, $timeout, false, "Job is completed.");
                        $scope.IsJobCompleted = true;
                        $scope.Model.MasterViewModel.JobOperationStatus.Key = result.OperationModel.MasterViewModel.JobOperationStatus.Key;
                        $scope.Model.MasterViewModel.JobOperationStatus.Value = result.OperationModel.MasterViewModel.JobOperationStatus.Value;
                        $scope.ShowRemainingHours($scope.Model.MasterViewModel.DueDate, $scope.Model.MasterViewModel.JobOperationStatus.Key);
                        $scope.ShowHoursTakenForJob($scope.Model.MasterViewModel.DueDate, $scope.Model.MasterViewModel.JobOperationStatus.Key);
                        $scope.CloseSummaryPanel($event);
                    }
                    else {
                        $scope.Model = result.OperationModel;
                        $scope.IsJobCompleted = false;
                    }

                }
                else {
                    $().showMessage($scope, $timeout, true, result.ErrorMessage);
                }
            }
        });
    }

    $scope.PickJob = function (event, ctrl) { //While picking the job

        $(".preload-overlay", $(windowContainer)).css("display", "block");

        if ($scope.Model.MasterViewModel.IsPicked == true) {

            var inProcess = 2;

            $.ajax({
                type: "POST",
                url: "JobOperation/AssignedJob",
                data: JSON.stringify($scope.Model),
                contentType: "application/json;charset=utf-8",
                success: function (result) {

                    if (result.IsError == false) { //Once assigned job is success setting job operation staus from "Not Started" to "In Process"
                        $scope.Model.MasterViewModel.JobOperationStatus.Key = inProcess; // In Process job operation status
                        $scope.AssignedJobOnSuccess();
                    }
                    if ($scope.Model.MasterViewModel.OperationTypes == 4)
                    {
                        $scope.GenerateInvoice("Inventories/SalesInvoice");
                    }
                },
                error: function (result) {
                    $(".preload-overlay", $(windowContainer)).css("display", "none");
                    $scope.Model.MasterViewModel.IsPicked = false;
                    $().showMessage($scope, $timeout, true, "Picking Job is Failed");
                }
            });
        }
        else {

            var notStarted = 1;

            $.ajax({
                type: "POST",
                url: "JobOperation/AssignedJob",
                data: JSON.stringify($scope.Model),
                contentType: "application/json;charset=utf-8",
                success: function (result) {

                    if (result.IsError == false) { //Once assigned job is success setting job operation staus from "Not Started" to "In Process"
                        $scope.Model.MasterViewModel.JobOperationStatus.Key = notStarted; // In Process job operation status
                        $scope.Model.MasterViewModel.EmployeeID = null;
                        $scope.AssignedJobOnSuccess();
                    }
                },
                error: function (result) {
                    $(".preload-overlay", $(windowContainer)).css("display", "none");
                }
            });
        }
    }

    // Need to move all report function at common place
    $scope.PrintTransaction = function (event, reportName, reportHeader, reportFullName) {
        var headIID = $scope.Model.MasterViewModel.ReferenceTransaction;

        if (reportName == "SalesInvoice") {
            $.ajax({
                url: "Mutual/ExecuteViewGrid?type=TransactionInvoiceDetails&IID1=" + $scope.Model.MasterViewModel.ReferenceTransaction,
                type: 'GET',
                success: function (productItem) {
                    //console.log(productItem)
                    if (productItem != null && productItem != "") {
                        var result = JSON.parse(productItem);
                        if (result != null && result != "" && result[0].InvoiceID != null) {

                            var headIID = $scope.Model.MasterViewModel.ReferenceTransaction;

                            if ($scope.ShowWindow(reportName, reportHeader, reportName) || headIID == null)
                                return;

                            var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

                            //var reportUrl = "Home/GeneratePDFReports?reportName=" + reportFullName + "&HeadID=" + $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID + '&returnFileBytes=' + true;
                            var reportUrl = "Home/ViewReports?reportName=" + reportFullName + "&HeadID=" + result[0].InvoiceID;
                            $('#' + windowName).append('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center><iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px onload="onLoadComplete(this);"></></iframe>');
                            var $iFrame = $('iframe[reportname=' + reportName + ']');
                            $iFrame.load(function () {
                                $("#Load", $('#' + windowName)).hide();
                            });
                        }
                    }

                    else {
                        $().showMessage($scope, $timeout, true, "Create SI First");
                    }
                }
            })
        }
        else {
            if ($scope.ShowWindow(reportName, reportHeader, reportName) || headIID == null)
                return;

            var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

            //var reportUrl = "Home/GeneratePDFReports?reportName=" + reportFullName + "&HeadID=" + $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID + '&returnFileBytes=' + true;
            var reportUrl = "Home/ViewReports?reportName=" + reportFullName + "&HeadID=" + headIID;
            $('#' + windowName).append('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center><iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px onload="onLoadComplete(this);"></></iframe>');
            var $iFrame = $('iframe[reportname=' + reportName + ']');
            $iFrame.load(function () {
                $("#Load", $('#' + windowName)).hide();
            });
        }
    };

    $scope.AssignedJobOnSuccess = function () { //On Success of assigned job with updated job operation status
        $.ajax({
            type: "POST",
            url: "JobOperation/AssignedJob",
            data: JSON.stringify($scope.Model),
            contentType: "application/json;charset=utf-8",
            success: function (result) {

                if (result.IsError == false) { //If service is failed resetting to old value
                    $scope.Model.MasterViewModel.JobOperationStatus.Key = result.JobOperation.MasterViewModel.JobOperationStatus.Key;
                    $scope.Model.MasterViewModel.JobOperationStatus.Value = result.JobOperation.MasterViewModel.JobOperationStatus.Value;
                    $scope.Model.MasterViewModel.EmployeeID = result.JobOperation.MasterViewModel.EmployeeID;
                    $scope.Model.MasterViewModel.EmployeeName = result.JobOperation.MasterViewModel.EmployeeName;
                    $scope.Model.MasterViewModel.TimeStamps = result.JobOperation.MasterViewModel.TimeStamps;
                }
                else {
                    $scope.Model.MasterViewModel.IsPicked = false;
                }

                $(".preload-overlay", $(windowContainer)).css("display", "none");
                $().showMessage($scope, $timeout, false, "Job Assigned to you and started");
            }
        });
    }

    $scope.Comments = function (event) {
        $('.popup.comments', $(windowContainer)).slideDown("fast");
        $('.transparent-overlay', $(windowContainer)).show();

        $.ajax({
            url: "Mutual/Comment?type=Job&referenceID=" + IID.toString(),
            type: 'GET',
            success: function (content) {
                $('#commentPanel', $(windowContainer)).html($compile(content)($scope));
            }
        });
    }

    $scope.CloseCommentOverlay = function (event) {
        $(event.currentTarget).hide();
        $('.popup.comments', $(windowContainer)).slideUp("fast");
    }

    $scope.VerifyLocationBarcode = function (detail) {

        if (detail.LocationBarcode == detail.VerifyLocation)
            detail.IsVerifyLocation = true;
        else
            detail.IsVerifyLocation = false;
    }

    $scope.SaveSKULocation = function (detail) {

        $(".preload-overlay", $(windowContainer)).css("display", "block");

        var url = 'JobOperation/SaveSKULocation?productSkuID=' + detail.ProductSKUID + "&validateLocationID=" + detail.LocationID + "&verifyLocationBarcode=" + detail.VerifyLocation;

        $.ajax({
            type: "GET",
            url: url,
            contentType: "application/json;charset=utf-8",
            success: function (result) {

                if (result.IsError == false) {
                    detail.LocationID = result.Location.LocationIID;
                    detail.LocationBarcode = detail.VerifyLocation = result.Location.BarCode;
                    detail.IsVerifyLocation = true;
                    $(".preload-overlay", $(windowContainer)).css("display", "none");
                    //$scope.SaveJobOperation(false);
                }
                else {
                    detail.IsVerifyLocation = false;
                    $(".preload-overlay", $(windowContainer)).css("display", "none");
                }
            },
            error: function (result) {
                $(".preload-overlay", $(windowContainer)).css("display", "none");
                console.log(result);
            }
        });
    }

    $scope.SaveSKUBarCode = function (detail) {

        if (detail.BarCode === detail.VerifyBarCode) {
            detail.IsVerifyBarCode = true;
            return;
        }

        $(".preload-overlay", $(windowContainer)).css("display", "block");
        var url = 'JobOperation/SaveSKUBarCode?productSkuID=' + detail.ProductSKUID + "&verifyBarCode=" + detail.VerifyBarCode;

        $.ajax({
            type: "GET",
            url: url,
            contentType: "application/json;charset=utf-8",
            success: function (result) {

                if (result.IsError == false) {
                    detail.BarCode = detail.VerifyBarCode = result.Output;
                    detail.IsVerifyBarCode = true;
                    $(".preload-overlay", $(windowContainer)).css("display", "none");
                }
                else {
                    detail.IsVerifyLocation = false;
                    $(".preload-overlay", $(windowContainer)).css("display", "none");
                }
            },
            error: function (result) {
                $(".preload-overlay", $(windowContainer)).css("display", "none");
                console.log(result);
            }
        });
    }

    $scope.VerifyProductBarcode = function (detail) {

        if (detail.BarCode == detail.VerifyBarCode)
            detail.IsVerifyBarCode = true;
        else
            detail.IsVerifyBarCode = false;
    }

    $scope.VerifyProductPartNo = function (detail) {

        if (detail.PartNo == detail.VerifyPartNo)
            detail.IsVerifyPartNo = true;
        else
            detail.IsVerifyPartNo = false;
    }

    $scope.GenerateInvoice = function (view) {

        var windowName = view.substring(view.indexOf('/') + 1);
        if ($scope.ShowWindow('Create' + windowName, 'Create ' + view, 'Create' + windowName))
            return;
        if (view == "Inventories/SalesInvoice")
        {
            if ($scope.Model.MasterViewModel.TransactionHeadIID) {
                $("#Overlay").fadeIn(100);
                var generateInvoiceUrl = view + "/GenerateInvoiceScreenForJob?ID=" + $scope.Model.MasterViewModel.TransactionHeadIID;
                $.ajax({
                    url: generateInvoiceUrl,
                    type: 'GET',
                    success: function (result) {
                        $("#LayoutContentSection").append($compile(result)($scope)).updateValidation();
                        $scope.AddWindow('Create' + windowName, 'Create ' + view, 'Create' + windowName);
                        $("#Overlay").fadeOut(100);
                    }
                });
            }
        }
        else
        {
            if ($scope.Model.MasterViewModel.TransactionHeadIID) {
                $("#Overlay").fadeIn(100);
                var generateInvoiceUrl = view + "/GenerateInvoiceScreenForJob?ID=" + $scope.Model.MasterViewModel.TransactionHeadIID;
                $.ajax({
                    url: generateInvoiceUrl,
                    type: 'GET',
                    success: function (result) {
                        $("#LayoutContentSection").append($compile(result)($scope)).updateValidation();
                        $scope.AddWindow('Create' + windowName, 'Create ' + view, 'Create' + windowName);
                        $("#Overlay").fadeOut(100);
                    }
                });
            }
        }
    }

    $scope.Copy = function (detail, property) {
        if (property.toLowerCase().indexOf('location') >= 0) {
            detail.VerifyLocation = detail.LocationBarcode;
            $scope.VerifyLocationBarcode(detail);
        }

        if (property.toLowerCase().indexOf('barcode') >= 0) {
            detail.VerifyBarCode = detail.BarCode;
            $scope.VerifyProductBarcode(detail);
        }

        if (property.toLowerCase().indexOf('partno') >= 0) {
            detail.VerifyPartNo = detail.PartNo;
            $scope.VerifyProductPartNo(detail);
        }
    }


    $scope.SaveSKUPartNo = function (detail) {

        if (detail.PartNo === detail.VerifyPartNo) {
            detail.IsVerifyPartNo = true;
            return;
        }

        $(".preload-overlay", $(windowContainer)).css("display", "block");
        var url = 'JobOperation/SaveSKUPartNo?productSkuID=' + detail.ProductSKUID + "&verifyPartNo=" + detail.VerifyPartNo;

        $.ajax({
            type: "GET",
            url: url,
            contentType: "application/json;charset=utf-8",
            success: function (result) {

                if (result.IsError == false) {
                    detail.PartNo = detail.VerifyPartNo = result.Output;
                    detail.IsVerifyPartNo = true;
                    $(".preload-overlay", $(windowContainer)).css("display", "none");
                }
                //else {
                //    detail.IsVerifyLocation = false;
                //    $(".preload-overlay", $(windowContainer)).css("display", "none");
                //}
            },
            error: function (result) {
                $(".preload-overlay", $(windowContainer)).css("display", "none");
                console.log(result);
            }
        });
    }

}]);
