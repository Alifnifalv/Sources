app.controller("TicketEntitlementEntryController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.init = function (model, windowname) {

        $scope.FillSettingDetails(model);
    }
    $scope.EmployeeChanges = function ($event, $element, viewvModel) {

        var model = viewvModel;

        var url = "Payroll/TicketEntitlementEntry/GetEmployeeAirfareDetails?employeeID=" + model.Employee.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data != null) {
                    model.TicketEligibleFromDateString = result.data.TicketEligibleFromDateString;
                    model.ISTicketEligible = result.data.ISTicketEligible;
                    model.EmployeeNearestAirportID = result.data.EmployeeNearestAirportID;
                    model.TicketEntitilementID = result.data.TicketEntitilementID;
                    model.GenerateTravelSector = result.data.GenerateTravelSector;
                    model.LastTicketGivenDate = result.data.LastTicketGivenDate;
                    model.LastTicketgivenString = result.data.LastTicketgivenString;
                    model.IsTwoWay = result.data.IsTwoWay;
                    model.FlightClassID = result.data.FlightClassID;
                    model.EmployeeNearestAirport = result.data.EmployeeNearestAirport;
                    model.DateOfJoiningString = result.data.DateOfJoiningString;
                    model.DateOfJoining = result.data.DateOfJoining;
                    model.FlightClass = result.data.FlightClass;
                    model.TicketEntitilement = result.data.TicketEntitilement;
                    model.TicketEntitilementDays = result.data.TicketEntitilementDays;
                    model.DepartmentID = result.data.DepartmentID;
                    model.BalanceBroughtForward = result.data.BalanceBroughtForward;
                    $scope.FillSettingDetails(model);

                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.GenerateAirfare = function (viewModel) {

        if (viewModel == null || viewModel.Employee == null || viewModel.Employee.Key == null) {
            $().showGlobalMessage($root, $timeout, true, "No employee has been selected!");
            return false;
        }
        if (viewModel.ISTicketEligible != true) {
            $().showGlobalMessage($root, $timeout, true, "The Ticket is not eligible; therefore, airfare cannot be calculated!");
            return false;
        }
        if (viewModel.TicketIssueDateString == undefined || viewModel.TicketIssueDateString == null) {
            $().showGlobalMessage($root, $timeout, true, "The Airfare Ticket Entry date is null or invalid; therefore, airfare cannot be calculated!");
            return false;
        }
        if (viewModel.TicketEligibleFromDateString == undefined || viewModel.TicketEligibleFromDateString == null) {
            $().showGlobalMessage($root, $timeout, true, "The Ticket Eligible From Date is null or invalid; therefore, airfare cannot be calculated!");
            return false;
        }
        if (viewModel.IsTicketFareIssued && viewModel.IsTicketFareReimbursed) {
            $().showGlobalMessage($root, $timeout, true, "Both 'Ticket Fare Issued' and 'Ticket Fare Reimbursed' cannot be selected at the same time!");
            return false;
        }
        if (viewModel.IsTicketFareIssued !== true && viewModel.IsTicketFareReimbursed !== true) {
            $().showGlobalMessage($root, $timeout, true, "Both 'Ticket Fare Issued' and 'Ticket Fare Reimbursed' cannot be unchecked at the same time!");
            return false;
        }
        if (viewModel.VacationStartingDateString == undefined || viewModel.VacationStartingDateString == null) {
            $().showGlobalMessage($root, $timeout, true, "The Vacation Starting Date is null or invalid; therefore, airfare cannot be calculated!");
            return false;
        }
        if (viewModel.VacationDaysEveryYear == undefined || viewModel.VacationDaysEveryYear == null || viewModel.VacationDaysEveryYear == 0) {
            $().showGlobalMessage($root, $timeout, true, "The Vacation Days for EveryYear is null or invalid; therefore, airfare cannot be calculated!");
            return false;
        }
        if (viewModel.DateOfJoiningString == undefined || viewModel.DateOfJoiningString == null) {
            $().showGlobalMessage($root, $timeout, true, "The Date Of Joining is null or invalid; therefore, airfare cannot be calculated!");
            return false;
        }
        if (viewModel.TicketEntitilementDays == undefined || viewModel.TicketEntitilementDays == null || viewModel.TicketEntitilementDays == 0) {
            $().showGlobalMessage($root, $timeout, true, "The Ticket Entitlement Days is null or invalid; therefore, airfare cannot be calculated!");
            return false;
        }
        if (viewModel.TravelReturnAirfare == undefined || viewModel.TravelReturnAirfare == null || viewModel.TravelReturnAirfare == 0) {
            $().showGlobalMessage($root, $timeout, true, "The Travel Return Airfare is null or invalid; therefore, airfare cannot be calculated!");
            return false;
        }

        var model = viewModel;
        $scope.SalaryComponents = [];
        const lastVacationDate = moment(model.VacationStartingDateString, "DD/MM/YYYY")
            .add(model.VacationDaysEveryYear, 'days');
        // Find the maximum date
        let maxDate;
        if (model.LastTicketGivenDate) {
            maxDate = moment.max(
                moment(model.DateOfJoiningString, "DD/MM/YYYY"),
                moment(model.TicketEligibleFromDateString, "DD/MM/YYYY"),
                moment(model.LastTicketgivenString, "DD/MM/YYYY")
            );
        } else {
            maxDate = moment.max(
                moment(model.DateOfJoiningString, "DD/MM/YYYY"),
                moment(model.TicketEligibleFromDateString, "DD/MM/YYYY")
            );
        }

        if (model.IsConsidereLOP) {

            const daysToBeConsideredForLOP = model.DaysToBeConsideredForLOP || 0;
            if ((model.LOPToBeConsideredCalculation || 0) > 0)
            {
                if (daysToBeConsideredForLOP < (model.LOPToBeConsideredCalculation || 0))
                model.LOPforTicketEntitilement = model.LOPToBeConsideredCalculation
            }
            else
            {
                
                const TicketIssueDate = moment(model.TicketIssueDateString, "DD/MM/YYYY");

                const lastDateOfPreviousMonth = TicketIssueDate.clone().subtract(1, 'month').endOf('month');

                const formattedPreviousMonthDate = lastDateOfPreviousMonth.format("DD/MM/YYYY");
                const previousMonthDateMoment = moment(formattedPreviousMonthDate, "DD/MM/YYYY");
                const maxDateMoment = moment(maxDate);

                const lopDays = previousMonthDateMoment.diff(maxDateMoment, 'days');

                model.LOPforTicketEntitilement = 0;
                if (daysToBeConsideredForLOP < lopDays)
                    model.LOPforTicketEntitilement = lopDays < 0 ? 0 : lopDays;//LOP
            }
        }
        var diffLOPfromlastVacationDate = lastVacationDate.subtract(model.LOPforTicketEntitilement || 0, 'days');

        // Calculate days diffLOPfromlastVacationDate and ticket entitlement
        const daysDifference = diffLOPfromlastVacationDate.diff(maxDate, 'days');

        //TicketEntitilementPer
        model.TicketEntitilementPer = (daysDifference /
            (model.TicketEntitilementDays + parseFloat(model.BalanceBroughtForward || 0))).toFixed(2);

        // Calculate TicketfarePayable
        model.TicketfarePayable =
            (model.TravelReturnAirfare * model.TicketEntitilementPer).toFixed(0);

        var faretobeConsidered = 0;
        if (model.IsTicketFareIssued)
            faretobeConsidered = model.TicketFareIssuedPercentage;
        else
            faretobeConsidered = model.TicketFareReimbursementPercentage;
        model.TicketIssuedOrFareReimbursed = (model.TravelReturnAirfare * (faretobeConsidered / 100)).toFixed(0);
        model.BalanceCarriedForwardPer = (model.TicketEntitilementPer - faretobeConsidered).toFixed(2);
        model.BalanceTicketAmountPayable = (model.TravelReturnAirfare - model.TicketfarePayable).toFixed(0);
        return model.TicketfarePayable;

    };

    $scope.FillSettingDetails = function (model) {

        var url = "Payroll/TicketEntitlementEntry/FillSettingDetails";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.TicketFareIssuedPercentage = result.data.TicketFareIssuedPercentage;
                model.TicketFareReimbursementPercentage = result.data.TicketFareReimbursementPercentage;
                model.VacationDaysEveryYear = result.data.VacationDaysEveryYear;
                model.VacationStartingDate = result.data.VacationStartingDate;
                model.TicketIssueDate = result.data.TicketIssueDate;
                model.VacationStartingDateString = result.data.VacationStartingDateString;
                //model.TicketIssueDateString = result.data.TicketIssueDateString;
                model.TravelReturnAirfare = result.data.TravelReturnAirfare;
                model.IsTicketFareIssued = result.data.IsTicketFareIssued;
                model.IsTicketFareReimbursed = result.data.IsTicketFareReimbursed;
                model.DaysToBeConsideredForLOP = result.data.DaysToBeConsideredForLOP;
                $scope.GetTravelReturnAirfare(model);
            }, function () {

            });
    };


    $scope.GenerateTravelSector = function ($event, $element, employeeModel) {
        if (employeeModel.EmployeeNearestAirport == null || employeeModel.EmployeeNearestAirport.Key == null || employeeModel.TicketEntitilement == null || employeeModel.TicketEntitilement.Key == null) return false;
        showOverlay();
        employeeModel.TravelReturnAirfare = 0;
        var url = "Payroll/Employee/GetTicketEntitilement?ticketEntitilementID=" + employeeModel.TicketEntitilement.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result.data != null) {
                    const parsedData = JSON.parse(result.data);
                    $scope.EmployerCountryAirportID = parsedData.CountryAirportID;
                    $scope.EmployerCountryAirport = parsedData.CountryAirportShortName;
                    employeeModel.TicketEntitilementDays = parsedData.NoOfDays
                    const match = employeeModel.EmployeeNearestAirport.Value.match(/\(([^)]+)\)/);

                    if (match) {
                        const shortName = match[1];
                        if (employeeModel.IsTwoWay)
                            employeeModel.GenerateTravelSector = $scope.EmployerCountryAirport + "_" + shortName + "_" + $scope.EmployerCountryAirport;
                        else
                            employeeModel.GenerateTravelSector = $scope.EmployerCountryAirport + "_" + shortName;

                        $scope.GetTravelReturnAirfare(employeeModel);
                    }
                }
                hideOverlay();

            }, function () {
                hideOverlay();
            });
    };
    $scope.GetTravelReturnAirfare = function (vModel) {

        var ticketEntitilementEntryViewModel = {
            TicketIssueDateString: vModel.TicketIssueDateString,
            DepartmentID: vModel.DepartmentID,
            GenerateTravelSector: vModel.GenerateTravelSector,
            FlightClassID: parseInt(vModel.FlightClass.Key)
        };
        var loadUrl = "/Payroll/TicketEntitlementEntry/GetSectorTicketAirfare";

        $.ajax({
            type: "POST",
            url: loadUrl,
            data: JSON.stringify(ticketEntitilementEntryViewModel),
            contentType: "application/json",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        vModel.TravelReturnAirfare = 0;
                        hideOverlay();
                        return false;
                    } else {

                        vModel.TravelReturnAirfare = result.Rate || 0;
                        hideOverlay();
                    }
                });
                hideOverlay();
            }
        });
    }
    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);