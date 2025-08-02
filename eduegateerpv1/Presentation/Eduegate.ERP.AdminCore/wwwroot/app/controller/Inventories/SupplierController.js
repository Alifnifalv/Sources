app.controller("SupplierController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Supplier View Loaded");
    $scope.employees = [];

    //Initializing the pos view model
    $scope.Init = function (model) {
        $scope.SupplierModel = model;
        $scope.IsSupplierDetailSection = true;
        $scope.IsContactSection = false;
        $scope.submitted = false;
        $scope.TitleMasters = [];
        $scope.CountryMasters = [];

        $scope.GetTitleMasters();
        $scope.GetCountryMasters();
        $scope.GetEmployeeList();
        window.setTimeout(function () {
            $scope.$apply();
        },2000);
    }

    $scope.GetTitleMasters = function () {

        $.ajax({
            type: "GET",
            url: "Mutual/GetLookUpData?lookType=Title",
            success: function (result) {

                $.each(result, function (index, item) {

                    if (item.Value != "") {
                        var title = { TitleIID: item.Key, TitleName: item.Value };
                        $scope.TitleMasters.push(title);
                    }
                });
            }
        });
    }

    $scope.GetCountryMasters = function () {

        $.ajax({
            type: "GET",
            url: "Supplier/GetCountryMasters",
            success: function (result) {

                if (result.IsError == false)
                {
                    $.each(result.CountryMasters, function (index, item) {
                        $scope.CountryMasters.push(item);
                    });
                }
            }
        });
    }

    $scope.CountrySelected = function (contact, countryID) {

        var selectedValue = $.grep($scope.CountryMasters, function (result) { return result.CountryID == countryID; })[0];

        if (selectedValue != undefined)
        {
            if(selectedValue.TelephoneCode!=null)
                contact.TelephoneCode = selectedValue != undefined ? "+" + selectedValue.TelephoneCode : "";
        }        
    }

    $scope.LoadSupplierInfoSection = function (ctrl, IsSupplierInfo) {

        $scope.IsSupplierDetailSection = IsSupplierInfo;
        $scope.IsContactSection = !IsSupplierInfo;
        $scope.SettingLeftPanelActiveSection(ctrl);
    }

    $scope.LoadContactInfoSection = function (ctrl, IsContactInfo) {

        $scope.IsContactSection = IsContactInfo;
        $scope.IsSupplierDetailSection = !IsContactInfo;
        $scope.SettingLeftPanelActiveSection(ctrl);
    }

    $scope.SettingLeftPanelActiveSection = function (ctrl) {
        $(ctrl).parent().find("li").removeClass("active");
        $(ctrl).addClass("active")
    }

    $scope.LoadSupplierList = function () {
        if ($scope.ShowWindow("SupplierLists", "Supplier Price", "SupplierLists"))
            return;

        $("#Overlay").fadeIn(100);

        $.ajax({
            type: "GET",
            url: "Supplier/List",
            success: function (result) {
                console.log("Supplier view loaded successfully");
                $("#LayoutContentSection").append($compile(result)($scope));
                $scope.AddWindow("SupplierLists", "Supplier Lists", "SupplierLists");
                $("#Overlay").fadeOut(100);
            }
        });
    }

    $scope.SaveSupplier = function () {

        $scope.submitted = true;
        var supplierFormValid = $scope.Supplier.$valid;
        var contactFormValid = $scope.Contact.$valid;

        if (supplierFormValid == true && contactFormValid == true) {

            ViewButtonLoaderWithOverlay();

            $.ajax({
                type: "POST",
                url: "Supplier/SaveSupplier",
                data: $scope.SupplierModel,
                success: function (result) {
                    HideButtonLoaderWithOverlay();

                    if (result.IsError == false) {
                        window.setTimeout(function () {
                            $scope.LoadSupplierList();
                        }, 10);   
                    }
                }
            });
        }
        else {
            if (supplierFormValid == false) {
                $scope.LoadSupplierInfoSection("#liSupplierInfo", true);
                return;
            }
            if (contactFormValid == false) {
                $scope.LoadContactInfoSection("#liContactInfo", true);
                return;
            }
        }
    }

    /* get the employee list to bind select control of PM responsible */
    $scope.GetEmployeeList = function () {

        var url = "Payroll/Employee/GetEmployees";
        $http({ method: 'Get', url: url })
        .then(function (result) {
            $scope.employees = result;
        });
    }

}]);