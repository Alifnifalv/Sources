app.controller("WorkflowLogController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Workflow log Loaded");
    $scope.WorkflowLogs = [];
    $scope.CurrentStatus = {};
    $scope.IsApprovedUsers = false;
    $scope.IsRestartedUsers = false;
    $scope.HideButtons = false;
    //Initializing the pos view model
    $scope.Init = function (headID, workflowID) {
        $timeout(function () {
            if (workflowID!=null) {
                $scope.GetWorkflowLogByWorflowID(headID, workflowID);
            }
            else {
                $scope.GetWorkflowLogs(headID);
            }
        });
    };

    $scope.GetWorkflowLogByWorflowID = function (headID, workflowID) {
        $('#WorkflowLoad').show();
        $.ajax({
            type: "GET",
            url: "Workflows/Workflow/GetWorkflowLogByWorflowID?headID=" + headID + '&workflowID=' + workflowID,
            success: function (result) {
                $scope.$apply(function () {
                    var currentStatus = null;
                    $.each(result, function (index, item) {                        
                        if ((item.StatusID === 1 || item.StatusID === 0 ) && currentStatus === null) {
                            currentStatus = item;
                        }
                        $scope.WorkflowLogs.push(item);
                    });

                    var readyForNextIndex = true;

                    $scope.WorkflowLogs.forEach((x, index) => {
                        if (x.IsFlowCompleted === false && readyForNextIndex === true) {
                            $scope.WorkflowLogs[index].IsBtnHide = false;
                            readyForNextIndex = false;
                        }
                        else {
                            $scope.WorkflowLogs[index].IsBtnHide = true;
                        }
                    });

                    $scope.HideButtons = $scope.WorkflowLogs.some(obj => obj.HideButtons === true);

                    $scope.CurrentStatus = currentStatus;

                    if ($scope.CurrentStatus) {
                        if ($scope.CurrentStatus.Approvers) {
                            $.each($scope.CurrentStatus.Approvers, function (index, item) {
                                if (currentStatus!=4 && item.Key == $scope.CurrentStatus.LoggedinEmployeeID) {
                                    $scope.IsApprovedUsers = true;
                                }
                                else if (currentStatus == 4 && item.Key == $scope.CurrentStatus.LoggedinEmployeeID)
                                {
                                    $scope.IsRestartedUsers = true;
                                }
                            });
                        }
                    }
                    $('#WorkflowLoad').hide();
                    $timeout(function () {
                        $('#WorkflowLoad').closest(".popover").popover('update');
                    });
                });
            }
        });
    };

    $scope.GetWorkflowLogs = function (headID) {
        $('#WorkflowLoad').show();
        $("#IsRejectedUsersbtn").hide();
        $.ajax({
            type: "GET",
            url: "Workflows/Workflow/GetWorkflowLog?headID=" + headID,
            success: function (result) {
                $scope.$apply(function () {
                    var currentStatus = null;
                    $.each(result, function (index, item) {
                        if ((item.StatusID == 1 || item.StatusID == 0) && currentStatus == null) {
                            currentStatus = item;
                            $("#IsRejectedUsersbtn").show();
                        }

                        $scope.WorkflowLogs.push(item);
                    });

                    $scope.CurrentStatus = currentStatus;

                    if ($scope.CurrentStatus) {
                        if ($scope.CurrentStatus.Approvers) {
                            $.each($scope.CurrentStatus.Approvers, function (index, item) {
                                if (item.Key == $scope.CurrentStatus.LoggedinEmployeeID) {
                                    $scope.IsApprovedUsers = true;
                                }
                            });
                        }
                    }
                    $('#WorkflowLoad').hide();
                });
            }
        });
    };

    $scope.Approve = function (event) {
        $('#WorkflowLoad').show();
        $.ajax({
            type: "GET",
            url: "Workflows/Workflow/ApproveWorkflowStatus?workflowTransactionHeadRuleMapID="
                + $scope.CurrentStatus.WorkflowTransactionHeadRuleMapID +
                "&employeeID=" + $scope.CurrentStatus.LoggedinEmployeeID +
                "&statusID=4"+
                "&applicationID=" + $scope.CurrentStatus.HeadID,
            success: function (result) {
                if (!result) {
                    $().showMessage($scope, $timeout, true, "Error occured!!");
                }
                else {
                    $("#IsApprovedUsersbtn").hide();
                    $scope.IsApprovedUsers = false;
                }
                $scope.WorkflowLogs = [];
                $scope.Init($scope.CurrentStatus.HeadID, $scope.CurrentStatus.WorkflowID);
                //$("#ReLoadBtn").trigger("click");
            },
            error: function () {
                $().showMessage($scope, $timeout, true, "Error occured!!");
            },
            complete: function () {
                $scope.IsApprovedUsers = true;
                $('#WorkflowLoad').hide();
            }
        });
    }

    $scope.Reject = function (event) {
        $('#WorkflowLoad').show();
        $.ajax({
            type: "GET",
            url: "Workflows/Workflow/ApproveWorkflowStatus?workflowTransactionHeadRuleMapID="
                + $scope.CurrentStatus.WorkflowTransactionHeadRuleMapID +
                "&employeeID=" + $scope.CurrentStatus.LoggedinEmployeeID +
                "&statusID=5" +
                "&applicationID=" + $scope.CurrentStatus.HeadID,
            success: function (result) {
                if (!result) {
                    $().showMessage($scope, $timeout, true, "Error occured!!");
                }
                else {
                    $("#IsRejectedUsersbtn").hide();
                    $("#IsApprovedUsersbtn").hide();
                    $('#WorkflowLoad').hide();
                    $scope.IsApprovedUsers = false;
                }
                $scope.WorkflowLogs = [];
                $scope.Init($scope.CurrentStatus.HeadID, $scope.CurrentStatus.WorkflowID);
                //$("#ReLoadBtn").trigger("click");
            },
            error: function () {
                $().showMessage($scope, $timeout, true, "Error occured!!");
            },
            complete: function () {
                $scope.IsApprovedUsers = true;
                $('#WorkflowLoad').hide();
            }
        });
    }

    $scope.WorkFlowButton = function (status,data) {

        var statusID = null;

        if (status == 'Reject') {
            statusID = 0;
        }
        else {
            statusID = status;
        }

        $('#WorkflowLoad').show();
        $.ajax({
            type: "GET",
            url: "Workflows/Workflow/ApproveWorkflowStatus?workflowTransactionHeadRuleMapID="
                + data.WorkflowTransactionHeadRuleMapID +
                "&employeeID=" + $scope.CurrentStatus.LoggedinEmployeeID +
                "&statusID=" + statusID +
                "&applicationID=" + $scope.CurrentStatus.HeadID,
            success: function (result) {
                if (!result) {
                    $().showMessage($scope, $timeout, true, "Error occured!!");
                }
                else {
                    $("#IsRejectedUsersbtn").hide();
                    $("#IsApprovedUsersbtn").hide();
                    $('#WorkflowLoad').hide();
                    $scope.IsApprovedUsers = false;
                }
                $scope.WorkflowLogs = [];
                $scope.Init($scope.CurrentStatus.HeadID, $scope.CurrentStatus.WorkflowID);
                //$("#ReLoadBtn").trigger("click");
            },
            error: function () {
                $().showMessage($scope, $timeout, true, "Error occured!!");
                $scope.$parent.RowModel.ReLoad();
            },
            complete: function () {
                $scope.IsApprovedUsers = true;
                $('#WorkflowLoad').hide();
                $scope.$parent.RowModel.ReLoad();
            }
        });
    }

}]);