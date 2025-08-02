app.controller("FinancialYearClosingController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope","$q", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root, $q) {
    console.log("FinancialYearClosingController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.Company = [];
    $scope.ViewType = null;
    $scope.Nodes = [];
    $scope.IsCompSelected = true;
    $scope.CrudWindowContainer = null;

    $scope.MainData = {
        "Company": { "Key": null, "Value": null },
        "PrvFY": { "Key": null, "Value": null },
        "CrtFY": { "Key": null, "Value": null },
        "BudgetSuggestion": { "Key": null, "Value": null },
        "AccountGroup": { "Key": null, "Value": null },
        "CostCenter": { "Key": null, "Value": null },
        "PrvFromDateString": null,
        "PrvToDateString": null,
        "CrtFromDateString": null,
        "CrtToDateString": null,
        "IsCompSelected": true,
    };
    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.init = function (model, windowname) {

        $scope.CrudWindowContainer = windowname;
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
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=BudgetSuggestions&defaultBlank=false",
        }).then(function (result) {
            $scope.BudgetSuggestions = result.data;
        });

        $scope.HideLoad();
    }

    //$scope.CompanyDetails = function () {
    //    var model = $scope.CRUDModel.ViewModel;
    //    if (model.Company) {
    //        showOverlay();
    //        var url = "Accounts/FinancialYearClosing/GetFiscalYearDetails";
    //        $http({ method: 'Get', url: url })
    //            .then(function (result) {

    //                //model.Details.PreviousFinancialYearID = result.data[1].FiscalYear_ID;
    //                model.Details.PreviousFinancialYearString = result.data[1].FiscalYear_ID.toString();
    //                model.Details.PrStartDateString = FormattedDate(result.data[1].StartDate);
    //                model.Details.PrEndDateString = FormattedDate(result.data[1].EndDate);

    //                //model.Details.CurrentFinancialYearID = result.data[0].FiscalYear_ID;
    //                model.Details.CurrentFinancialYearString = result.data[0].FiscalYear_ID.toString();
    //                model.Details.CrStartDateString = FormattedDate(result.data[0].StartDate);
    //                model.Details.CrEndDateString = FormattedDate(result.data[0].EndDate);

    //                hideOverlay();
    //            },
    //                function () {
    //                    hideOverlay();
    //                });
    //    }
    //};

    $scope.CompanyDetails = function (company) {
        if (company) {
            showOverlay();
            var url = "Accounts/FinancialYearClosing/GetFiscalYearDetails";

            $http({
                method: 'GET',
                url: url
            }).then(function (response) {
                var result = response.data;

                $scope.MainData.Company.Key = company.Key;
                $scope.MainData.Company.Value = company.Value;

                $scope.MainData.PrvFY.Key = result[1].FiscalYear_ID;
                $scope.MainData.PrvFY.Value = result[1].FiscalYear_Name;
                $scope.MainData.PrvFromDateString = FormattedDate(result[1].StartDate);
                $scope.MainData.PrvToDateString = FormattedDate(result[1].EndDate);
                $scope.MainData.PrvStatusName = result[1].StatusName;
                $scope.MainData.PrvAuditTypeName = result[1].AuditTypeName;

                $scope.MainData.CrtFY.Key = result[0].FiscalYear_ID;
                $scope.MainData.CrtFY.Value = result[0].FiscalYear_Name;
                $scope.MainData.CrStartDateString = FormattedDate(result[0].StartDate);
                $scope.MainData.CrEndDateString = FormattedDate(result[0].EndDate);
                $scope.MainData.CrntStatusName = result[0].StatusName;
                $scope.MainData.CrntAuditTypeName = result[0].AuditTypeName;

                $scope.ViewType = 'PrvFinancialAudit';
                $scope.ViewType1 = 'CrntFinancialAudit';
                $scope.IsCompSelected = false;
                //$('#shoLoad').show();
                $scope.LoadTreeView();

                hideOverlay();
            }, function () {
                hideOverlay();
            });
        }
    };

    function FormattedDate(dateString) {

        const date = new Date(dateString);

        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();

        const formattedDate = `${day}/${month}/${year}`;

        return formattedDate;
    }

    $scope.LoadTreeView = function (parentNodeId) {
        var promise = loadTreeNodes();
        promise.then(function (result) {
            $scope.Nodes = result.Nodes;
            $('#shoLoad').hide();
            $('#shoLoad1').hide();
        });

        var promise1 = loadTreeNodes1();
        promise1.then(function (result) {
            $scope.Nodes1 = result.Nodes;
            $('#shoLoad').hide();
            $('#shoLoad1').hide();
        });
    }

    function loadTreeNodes(parentNodeId) {
        return $q(function (resolve, reject) {
            var uri = 'Accounts/FinancialYearClosing/GetSmartTreeView?type='
                + $scope.ViewType + "&searchText=" + $scope.MainData.Company.Key;

            if (parentNodeId) {
                uri = uri + '&parentID=' + parentNodeId;
            }

            $.ajax({
                type: 'GET',
                url: uri,
                success: function (result) {
                    resolve(result);
                }
            });
        });
    }

    function loadTreeNodes1(parentNodeId) {
        return $q(function (resolve, reject) {
            var uri = 'Accounts/FinancialYearClosing/GetSmartTreeView?type='
                + $scope.ViewType1 + "&searchText=" + $scope.MainData.Company.Key;

            if (parentNodeId) {
                uri = uri + '&parentID=' + parentNodeId;
            }

            $.ajax({
                type: 'GET',
                url: uri,
                success: function (result) {
                    resolve(result);
                }
            });
        });
    }

    $scope.FireEvent = function ($event, childnode) {
        $($event.target).closest("span").toggleClass("expand");

        if (childnode.Nodes.length > 0) {
            if ($($event.target).closest("span").hasClass('expand')) {
                $($event.target).closest('li').find('ul:first').slideDown('fast');
            }
            else {
                $($event.target).closest('li').find('ul:first').slideUp('fast');
            }
            return;
        }

        var promise = loadTreeNodes(childnode.NodeID);
        promise.then(function (result) {
            $timeout(function () {
                $scope.$apply(function () {
                    childnode.Nodes = result.Nodes;

                    if (childnode.Nodes.length === 0) {
                        $($event.target).closest('li').addClass('leaf');
                    }
                    else {
                        $($event.target).closest('li').removeClass('leaf');
                    }

                    $timeout(function () {
                        if ($($event.target).closest("span").hasClass('expand')) {
                            $($event.target).closest('li').find('ul:first').slideDown('fast');
                        }
                        else {
                            $($event.target).closest('li').find('ul:first').slideUp('fast');
                        }
                    });
                });
            });
        });
    };

    $scope.HideLoad = function () {
        $('#shoLoad').hide();
        $('#shoLoad1').hide();
    }

    $scope.SaveEntries = function () {

        var entries = $scope.MainData;
        if (entries) {
            var data = {
                Company_ID: parseInt(entries.Company.Key),
                Prv_FiscalYear_ID: entries.PrvFY.Key,
                FiscalYear_ID: entries.CrtFY.Key
            };

            showOverlay();

            $http({
                method: 'POST',
                url: "Accounts/FinancialYearClosing/SaveFYCEntries",
                data: JSON.stringify(data),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(function (result) {
                if (result.data.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.data.Response);
                } else {
                    $().showGlobalMessage($root, $timeout, false, result.data.Response);

                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.IsCompSelected = false;
                        });
                    }, 1000);
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
        } else {
            $().showGlobalMessage($root, $timeout, true, "No data found to save/update result!");
        }
    };


}]);

