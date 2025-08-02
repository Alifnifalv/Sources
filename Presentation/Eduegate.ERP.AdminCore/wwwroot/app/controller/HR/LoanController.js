app.controller("LoanController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    console.log("LoanController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.EmployeeID = 0;
    $scope.TotalSalary = 0;
    $scope.ActiveStatus = null;

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.init = function (model, windowname) {

        // $scope.LoanModel = model;
        //$scope.IsApproveLoan = model.IsApproveLoan;
        //$scope.IsLoanEntry = model.IsLoanEntry;
        $scope.FillLoanData(model, model.LoanRequestID, model.LoanHeadID);

    }

    $scope.ChangeInstallmentDateFormat = function (model, settingsID) {

        if (model.InstallmentDateString != undefined && model.InstallmentDateString != "" && model.InstallmentDateString != null && settingsID == 1) {
            model.InstallmentDateString = FormatDate(model.InstallmentDateString);
            settingsID == 0;
        }
        else {
            model.InstallmentDateString = null;
        }

    }
    function FormatDate(inputDate) {
        return new Date(inputDate.split('/').reverse().join('/')).toLocaleDateString('en-US', { month: 'short', year: 'numeric' });
    }
    $scope.LoanTypeChanges = function (event, model) {
        if (model.LoanType == 2) {

            model.NoofInstallments = 1;
            model.IsDisable = true;

        }
        else {
            model.NoofInstallments = model.NoOfInstallments;
            model.IsDisable = false;
        }

        if (model.LoanType == null || model.LoanType == undefined || model.LoanType == "") {
            model.IsAllDisable = true;
        }
        else
        {
            model.IsAllDisable = false;
        }
        $timeout(function () {

        });
        $scope.CheckLoanFeasibility(model);
        // $scope.GetInstallAmount(model);
    }

    $scope.LoanEntryStatusChanges = function (model) {
        if (model.LoanEntryStatus == 4 || model.LoanEntryStatus == 5) //paid or schedueld sttaus
        {
            $().showMessage($scope, $timeout, true, 'Paid Status can not be selected');
            model.LoanEntryStatus = model.LoanEntryStatusID.toString();
        }
        if (model.LoanEntryStatus == 4 || model.LoanEntryStatus == 5) {

            $().showMessage($scope, $timeout, true, 'Scheduled Status can not be selected');
            model.LoanEntryStatus = model.LoanEntryStatusID.toString();
        }
    }
    $scope.SplitInstallAmount = function (model) {
       showOverlay();
        let sum = 0;
        let balanceAmt = 0;
        let installAmount = 0;
        model.LoanDetailDTOs = [];
        model.LoanApprovalInstallments = [];
        var activeInstallmentStatus = model.ActiveStatus;
        var installmentDate = null;
        for (let i = 0, l = model.NoofInstallments; i < l; i++) {
            installmentDate = new Date(model.Paymentstartdate);
            installmentDate.setMonth(installmentDate.getMonth() + i);
            if (i === (model.NoofInstallments - 1)) {
                balanceAmt = Math.round(model.LoanAmount - sum);
                model.LoanApprovalInstallments.push({
                    LoanDetailID: 0,
                    LoanHeadID: null,
                    InstallmentDate: installmentDate,
                    InstallmentReceivedDate: null,
                    InstallmentAmount: balanceAmt,
                    Remarks: null,
                    IsPaid: null,
                    LoanEntryStatus: activeInstallmentStatus,
                    InstallmentDateString: installmentDate.toLocaleDateString('en-GB'),
                }); 
                break;
            } else {
                installAmount = Math.round((model.LoanAmount || 0) / model.NoofInstallments);
                model.LoanApprovalInstallments.push({
                    LoanDetailID: 0,
                    LoanHeadID: null,
                    InstallmentDate: installmentDate,
                    InstallmentDateString: installmentDate.toLocaleDateString('en-GB'),
                    InstallmentReceivedDate: null,
                    InstallmentAmount: installAmount,
                    Remarks: null,
                    IsPaid: null,
                    LoanEntryStatus: activeInstallmentStatus
                });
                sum += (model.LoanApprovalInstallments[i].InstallmentAmount || 0);
            }
        }
        if (model.NoofInstallments != '') {
            model.PaymentEndDate = installmentDate;
            model.PaymentEndDateString = installmentDate.toLocaleDateString('en-GB');
        }
        //model.LoanApprovalInstallments = model.LoanDetailDTOs;
        hideOverlay();
    };

    FillLoanData = function (gridModel, loanRequestID, loanHeadID) {
        var model = gridModel;
        $scope.LoanInstallments = [];
        showOverlay();
        var url = "Payroll/Loan/FillLoanData?LoanRequestID=" + loanRequestID + "&loanHeadID=" + loanHeadID + "";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result != null && result.data != null && result.data.IsError != true) {
                    model.LoanHeadIID = result.data.Response.LoanHeadIID;
                    model.EmployeeID = result.data.Response.EmployeeID;
                    model.Employee = result.data.Response.Employee;
                    model.LoanRequestID = result.data.Response.LoanRequestID;
                    model.LoanTypeID = result.data.Response.LoanTypeID;
                    model.LoanType = result.data.Response.LoanTypeID.toString();
                    model.LoanRequestNo = result.data.Response.LoanRequestNo;
                    model.LoanStatus = result.data.Response.LoanStatusID.toString();
                    model.NoofInstallments = result.data.Response.NoOfInstallments;
                    model.LoanDate = result.data.Response.LoanDate;
                    model.PaymentstartdateString = result.data.Response.PaymentstartdateString;
                    model.LoanAmount = result.data.Response.LoanAmount;
                    model.InstallmentAmount = result.data.Response.InstallmentAmount;
                    model.Remarks = result.data.Response.Remarks;
                    model.LoanDateString = result.data.Response.LoanDateString;
                   

                    result.data.Response.LoanApprovalInstallments.forEach(details => {
                        $scope.LoanInstallments.push(
                            {
                                LoanDetailID: details.LoanDetailID,
                                LoanHeadID: details.LoanHeadID,
                                InstallmentDate: details.InstallmentDate,
                                InstallmentDateString: details.InstallmentDateString,
                                InstallmentReceivedDateString: details.InstallmentReceivedDateString,
                                InstallmentReceivedDate: details.InstallmentReceivedDate,
                                InstallmentAmount: details.InstallmentAmount,
                                Remarks: details.Remarks,
                                LoanEntryStatusID: details.LoanEntryStatusID,
                                LoanEntryStatus: details.LoanEntryStatusID.toString(),
                                IsDisableStatus: details.IsDisableStatus
                            }
                        );
                    });

                    model.LoanApprovalInstallments = $scope.LoanInstallments;
                }
                else {
                    $().showMessage($scope, $timeout, true, result.data.Response);
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.CheckLoanFeasibility = function (model) {
        if (model.Employee != null && model.Employee.Key != null &&
            model.LoanAmount != undefined && model.LoanAmount != null && model.LoanAmount != 0 &&
            model.NoofInstallments != undefined && model.NoofInstallments != null && model.NoofInstallments != 0) {

            var sum = 0, totalSalary = 0;
            sum = parseFloat(model.LoanAmount) / parseInt(model.NoofInstallments);
            sum = roundAwayFromZero(sum || 0);

            if ($scope.TotalSalary == 0 || $scope.EmployeeID != model.Employee.Key) {
                $scope.GetTotalSalaryAmount(model);
            }

            if ($scope.TotalSalary != undefined && $scope.TotalSalary != 0 && sum > 0) {
                if (sum < $scope.TotalSalary) {
                    model.InstallmentAmount = sum.toFixed(0);
                    //model.LoanApprovalInstallments.forEach(details => {
                    //    details.InstallmentAmount = model.InstallmentAmount;
                    //});

                    $scope.SplitInstallAmount(model);
                    return true;
                } else {
                    model.InstallmentAmount = sum.toFixed(0);
                    $().showMessage($scope, $timeout, true, 'Requested amount has not been approved due to salary considerations!');
                    return false;
                }
            }
        }

        return false; // In case any of the initial conditions fail
    };

    // Ensure this function is defined
    function roundAwayFromZero(num) {
        return (num > 0) ? Math.ceil(num) : Math.floor(num);
    }


    $scope.GetTotalSalaryAmount = function (model) {
        showOverlay();
        if (model.Employee != null && model.Employee.Key != null) {
            var url = "Payroll/Loan/GetTotalSalaryAmount?employeeID=" + model.Employee.Key + "&loanDate=null";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result != null && result.data != null && result.data.IsError != true) {
                        if (result.data.Response == 0) {
                            $().showMessage($scope, $timeout, true, 'Please verify the salary structure. It appears that the salary amount is currently zero!');
                            hideOverlay();
                            $scope.TotalSalary = 0;
                            return 0;
                        }
                        $scope.EmployeeID = model.Employee.Key;
                        $scope.TotalSalary = result.data.Response;
                        hideOverlay();
                        return result.data.Response;
                    }
                    else {
                        $().showMessage($scope, $timeout, true, 'Something went wrong.Please try again later!');
                        hideOverlay();
                        result.data.Response = 0;
                        return 0;
                    }
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        else {
            hideOverlay();
        }
    };

    $scope.CheckLoanFeasibilityAndSplitInstallAmount = function (viewModel) {

        $scope.CheckLoanFeasibility(viewModel);
        $scope.SplitInstallAmount(viewModel);
    };


    $scope.StartDateChange = function (viewModel) {
        showOverlay();

        var model = viewModel;

        var installmentDate = null;
        let sum = 0;
        let balanceAmt = 0;
        let installAmount = 0;
        model.LoanDetailDTOs = [];
        model.LoanApprovalInstallments = [];
        var activeInstallmentStatus = model.ActiveStatus;

        for (let i = 0, l = model.NoofInstallments; i < l; i++) {
            installmentDate = moment(model.PaymentstartdateString, 'DD/MM/YYYY').toDate();
            installmentDate.setMonth(installmentDate.getMonth() + i);
            if (i === (model.NoofInstallments - 1)) {
                balanceAmt = Math.round(installAmount - sum);
                model.LoanApprovalInstallments.push({
                    LoanDetailID: 0,
                    LoanHeadID: null,
                    InstallmentDate: installmentDate,
                    InstallmentReceivedDate: null,
                    InstallmentAmount: balanceAmt,
                    Remarks: null,
                    IsPaid: null,
                    LoanEntryStatus: activeInstallmentStatus,
                    InstallmentDateString: installmentDate.toLocaleDateString('en-GB'),
                });
                break;
            } else {
                installAmount = Math.round((model.LoanAmount || 0) / model.NoofInstallments);
                model.LoanApprovalInstallments.push({
                    LoanDetailID: 0,
                    LoanHeadID: null,
                    InstallmentDate: installmentDate,
                    InstallmentDateString: installmentDate.toLocaleDateString('en-GB'),
                    InstallmentReceivedDate: null,
                    InstallmentAmount: installAmount,
                    Remarks: null,
                    IsPaid: null,
                    LoanEntryStatus: activeInstallmentStatus
                });
                //sum += (model.LoanAmount || 0);
            }

        }

        if (model.NoofInstallments != '') {
            model.PaymentEndDate = installmentDate;
            model.PaymentEndDateString = installmentDate.toLocaleDateString('en-GB');
        }
        hideOverlay();
    };

    $scope.GetInstallAmount = function (data) {
        if (typeof (data) === 'undefined' || typeof (data.LoanAmount) === 'undefined' || typeof (data.NoofInstallments) === 'undefined' || data.LoanAmount == null || data.NoofInstallments == null || parseFloat(data.LoanAmount) < parseInt(data.NoofInstallments)) {
            return 0;
        }
        var sum = 0, totalSalary = 0;

        sum = parseFloat(data.LoanAmount) / parseInt(data.NoofInstallments);
        sum = roundAwayFromZero(sum || 0);

        data.InstallmentAmount = sum.toFixed(3);
        return sum;


    };
    //function roundAwayFromZero(value) {
    //    return Math.sign(value) * Math.round(Math.abs(value));
    //}

}]);
