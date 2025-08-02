app.controller("AssetController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$rootScope", "$controller",
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $rootScope, $controller) {


        console.log("Asset Controller");

        $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout });



        $scope.AssetCodeChange = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {
            if (currentRow.AssetCode == null || currentRow.AssetCode.Key==null)
            {
                if (currentRow.hasOwnProperty("Description") == true)
                    currentRow.Description = null;
                if (currentRow.hasOwnProperty("CategoryName") == true)
                    currentRow.CategoryName = null;
                if (currentRow.hasOwnProperty("AssetStartDate") == true)
                    currentRow.AssetStartDate = null;
                if (currentRow.hasOwnProperty("AssetValue") == true)
                    currentRow.AssetValue = null;
                if (currentRow.hasOwnProperty("AssetGlAccount") == true)
                    currentRow.AssetGlAccount = null;
                if (currentRow.hasOwnProperty("AssetGlAccID") == true)
                    currentRow.AssetGlAccID = null;

                if (currentRow.hasOwnProperty("Quantity") == true)
                    currentRow.Quantity = null;
                if (currentRow.hasOwnProperty("Debit") == true)
                    currentRow.Debit = null;
                if (currentRow.hasOwnProperty("Credit") == true)
                    currentRow.Credit = null;
                if (currentRow.hasOwnProperty("StartDate") == true)
                    currentRow.StartDate = null;
                return false;
            }
            //
            if (currentRow.hasOwnProperty("Credit") == true) // This case is when Row for GL Account changed to Asset Row
                currentRow.Credit = null;

            $.ajax({
                url: "AssetEntry/GetAssetByID?ID=" + currentRow.AssetCode.Key,
                type: 'GET',
                success: function (resultItem) {
                    if (resultItem != null) {
                        if (currentRow.hasOwnProperty("Description") == true)
                            currentRow.Description = resultItem.Description;
                        if (currentRow.hasOwnProperty("CategoryName") == true)
                            currentRow.CategoryName = resultItem.AssetCategory.Value;
                        if (currentRow.hasOwnProperty("AssetStartDate") == true)
                            currentRow.AssetStartDate = resultItem.StartDate;
                        if (currentRow.hasOwnProperty("AssetValue") == true)
                            currentRow.AssetValue = resultItem.AssetValue;
                        if (currentRow.hasOwnProperty("AssetGlAccount") == true)
                            currentRow.AssetGlAccount = resultItem.AssetGlAccount;
                        if (currentRow.hasOwnProperty("AssetGlAccID") == true)
                            currentRow.AssetGlAccID = resultItem.AssetGlAccount.Key;
                    }    
                }
            })

        }

        $scope.AccountCodeChange = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {

            if (currentRow.AssetGlAccount == null || currentRow.AssetGlAccount.Key == null)
            {
                if (currentRow.hasOwnProperty("Quantity") == true)
                    currentRow.Quantity = null;
                if (currentRow.hasOwnProperty("Debit") == true)
                    currentRow.Debit = null;
                if (currentRow.hasOwnProperty("Credit") == true)
                    currentRow.Credit = null;
                if (currentRow.hasOwnProperty("StartDate") == true)
                    currentRow.StartDate = null;
                e.preventDefault();
                return false;
            }

            $.ajax({
                url: "AssetEntry/GetAccountByID?ID=" + currentRow.AssetGlAccount.Key,
                type: 'GET',
                success: function (resultItem) {
                    if (resultItem != null) {
                        if (currentRow.hasOwnProperty("Description") == true)
                            currentRow.Description = resultItem.AccountName;

                        if (currentRow.hasOwnProperty("AccumulatedDepreciation") == true)
                            currentRow.AccumulatedDepreciation = resultItem.AccumulatedDepreciation;
                    }
                }
            })
        }

        $scope.OnCreditChange = function (currentRow,model, dataType, control, multiplesingle,  rowIndex) {
            if (currentRow.hasOwnProperty("CreditTotal") == true)
                currentRow.CreditTotal = (currentRow.Credit != null ? currentRow.Credit : 0) * (currentRow.Quantity != null && currentRow.Quantity != 0? currentRow.Quantity : 1);

            //if (currentRow.hasOwnProperty("Debit") == true && currentRow.Debit > 0)
            //{
            //    if (currentRow.hasOwnProperty("Credit") == true)
            //        currentRow.Credit = 0;
            //}
        }

       // $scope.OnDebitChange = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {
       $scope.OnDebitChange = function (currentRow, model, dataType, control, multiplesingle, rowIndex) {
            if (currentRow.hasOwnProperty("DebitTotal") == true)
                currentRow.DebitTotal = (currentRow.Debit != null ? currentRow.Debit : 0) * (currentRow.Quantity != null && currentRow.Quantity != 0 ? currentRow.Quantity : 1);

            //if (currentRow.hasOwnProperty("Credit") == true && currentRow.Debit > 0) {
            //    if (currentRow.hasOwnProperty("Debit") == true)
            //        currentRow.Debit = 0;
            //}
        }

       $scope.OnDepreciationAssetCodeChange = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {
           if (currentRow.AssetCode == null || currentRow.AssetCode.Key == null) {
               if (currentRow.hasOwnProperty("Description") == true)
                   currentRow.Description = null;
              
               if (currentRow.hasOwnProperty("AssetStartDate") == true)
                   currentRow.AssetStartDate = null;
               if (currentRow.hasOwnProperty("AssetValue") == true)
                   currentRow.AssetValue = null;
              
               if (currentRow.hasOwnProperty("Quantity") == true)
                   currentRow.Quantity = null;
               currentRow.Amount = 0;
               currentRow.NetDeprecition = 0;

               if (currentRow.hasOwnProperty("AccumulatedDepreciation") == true)
                   currentRow.AccumulatedDepreciation = null;
               return false;
           }


            $.ajax({
                url: "AssetEntry/GetAssetByID?ID=" + currentRow.AssetCode.Key,
                type: 'GET',
                success: function (resultItem) {
                    if (resultItem != null) {
                        if (resultItem.AssetValue == null || resultItem.AssetValue == 0)
                        {
                            alert(" This asset "+currentRow.AssetCode.Value+"is not set with asset Value. Please enter asset value in Asset Entry Screen")
                            currentRow.AssetCode = null;
                            currentRow.Description = null;
                            currentRow.AssetQuanity = null;
                            currentRow.AssetValue = null;
                            currentRow.AccumulatedDepreciation = null;
                            return false;
                        }

                        if (currentRow.hasOwnProperty("Description") == true)
                            currentRow.Description = resultItem.Description;
                        if (currentRow.hasOwnProperty("CategoryName") == true)
                            currentRow.CategoryName = resultItem.AssetCategory.Value;
                        if (currentRow.hasOwnProperty("AssetStartDate") == true)
                            currentRow.AssetStartDate = FormatedAssetStartDate(resultItem.StartDate);
                        if (currentRow.hasOwnProperty("AssetValue") == true)
                            currentRow.AssetValue = resultItem.AssetValue < 0 ? (resultItem.AssetValue * -1) : resultItem.AssetValue;
                        if (currentRow.hasOwnProperty("AssetGlAccount") == true)
                            currentRow.AssetGlAccount = resultItem.AssetGlAccount;
                        if (currentRow.hasOwnProperty("AssetGlAccID") == true)
                            currentRow.AssetGlAccID = resultItem.AssetGlAccount.Key;
                        if (currentRow.hasOwnProperty("DepreciationYears") == true)
                            currentRow.DepreciationYears = resultItem.DepreciationYears;
                        if (currentRow.hasOwnProperty("AssetQuantity") == true) {
                            currentRow.AssetQuantity = resultItem.Quantity;                            
                        }
                        if (currentRow.hasOwnProperty("Quantity") == true) {
                            currentRow.Quantity = resultItem.Quantity;
                        }
                        
                        if (currentRow.hasOwnProperty("AccumulatedDepreciation") == true)
                            currentRow.AccumulatedDepreciation = resultItem.AccumulatedDepreciation;
                        
                        
                    }
                    if (currentRow.hasOwnProperty("Quantity") == true && currentRow.Quantity != null
                                && currentRow.hasOwnProperty("StartDate") == true && currentRow.StartDate != null) {
                        CalculateDeprecation(currentRow);
                    }

                }
            })

        }

        $scope.OnAssetQuatityChange = function (currentRow, model, dataType, control, multiplesingle, rowIndex) {
            
            if (currentRow.hasOwnProperty("DebitTotal") == true)
                currentRow.DebitTotal = (currentRow.Debit != null ? currentRow.Debit : 0) * (currentRow.Quantity != null && currentRow.Quantity != 0 ? currentRow.Quantity : 1);
            if (currentRow.hasOwnProperty("CreditTotal") == true)
                currentRow.CreditTotal = (currentRow.Credit != null ? currentRow.Credit : 0) * (currentRow.Quantity != null && currentRow.Quantity != 0 ? currentRow.Quantity : 1);

        }

        $scope.OnDepreciationQuatityChange = function (currentRow, model, dataType, control, multiplesingle, rowIndex) {
            if(currentRow.StartDate ==null)
            {
                alert("Please select Date before adding Quantity");
                return false;
            }
            if (currentRow.hasOwnProperty("DebitTotal") == true)
                currentRow.DebitTotal = (currentRow.Debit != null ? currentRow.Debit : 0) * (currentRow.Quantity != null && currentRow.Quantity != 0 ? currentRow.Quantity : 1);
            if (currentRow.hasOwnProperty("CreditTotal") == true)
                currentRow.CreditTotal = (currentRow.Credit != null ? currentRow.Credit : 0) * (currentRow.Quantity != null && currentRow.Quantity != 0 ? currentRow.Quantity : 1);

            if (currentRow.hasOwnProperty("AssetCode") == true && currentRow.AssetCode != null
                    && currentRow.hasOwnProperty("StartDate") == true && currentRow.StartDate !=null) {  
                CalculateDeprecation(currentRow);
            }
        }
        $scope.OnDepreciationDateChange = function (currentRow, model, dataType, control, multiplesingle, rowIndex) {

            if (currentRow.hasOwnProperty("AssetCode") == true && currentRow.AssetCode != null
                    && currentRow.hasOwnProperty("Quantity") == true && currentRow.Quantity != null) {
                CalculateDeprecation(currentRow);
            }
        }
        CalculateDeprecation = function (currentRow)
        {
            var AccumulatedDepreciation = currentRow.AccumulatedDepreciation;//For one Unit
            if (AccumulatedDepreciation == null || AccumulatedDepreciation == 0) AccumulatedDepreciation = 1;//Check

            var Quatity = currentRow.Quantity;
            var AssetValue = currentRow.AssetValue;
            var DepreciationYears = currentRow.DepreciationYears
            //(Asset Value * difference of start date and calculated date of depreciation) / (Depreciation yrs*365)

           
            var AssetStartDate = new Date(currentRow.AssetStartDate);
            var DepreciationCalcDate = new Date(currentRow.StartDate);

            var timeDiff = Math.abs(AssetStartDate.getTime() - DepreciationCalcDate.getTime());
            var NumberOfDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

            var CalculatedDepreciationForOneUnit = (AssetValue * NumberOfDays) / (DepreciationYears * 365);
            CalculatedDepreciationForOneUnit = Math.round(CalculatedDepreciationForOneUnit * 1000) / 1000

            var TotalDepreciationAmount = CalculatedDepreciationForOneUnit * Quatity; //Display
            TotalDepreciationAmount = Math.round(TotalDepreciationAmount * 1000) / 1000

            //var CalculatedDepreciation = 200;

            if (currentRow.hasOwnProperty("Amount") == true) {
                currentRow.Amount = CalculatedDepreciationForOneUnit; //For one Unit- Store in DB
                currentRow.TotalDepreciationAmount = TotalDepreciationAmount; //Depre Multiplied by Qty for Display
            }

            if (currentRow.hasOwnProperty("NetDeprecition") == true) {
                currentRow.NetDeprecition = currentRow.AccumulatedDepreciation + TotalDepreciationAmount;
                currentRow.NetDeprecition = Math.round(currentRow.NetDeprecition * 1000) / 1000
            }
            
        }

        FormatedAssetStartDate = function (date) {
            if (date == null) {
                return null;
            }
            else {
                var d = new Date(date),
                    month = '' + (d.getMonth() + 1),
                    day = '' + d.getDate(),
                    year = d.getFullYear();

                if (month.length < 2) month = '0' + month;
                if (day.length < 2) day = '0' + day;

                return [year, month, day].join('-');
            }
        }


        $scope.AssetCodesSearch = function (searchText) {

            $.ajax({
                url: "AssetMaster/AssetCodesSearch?searchText=" + searchText,
                type: 'GET',
                success: function (assetsList) {

                    $scope.AssetCode = [];

                    if (assetsList != undefined && assetsList != null) {
                        $.each(assetsList, function (index, item) {
                            $scope.AssetCode.push(item);
                        });

                        $scope.$apply();
                    }
                }
            })
        }

            //    angular.module('caco.feed.filter', [])
            //.filter('AssetSumByKey', function () {
            //    return function (data, key) {
            //        if (typeof (data) === 'undefined' || typeof (key) === 'undefined') {
            //            return 0;
            //        }

            //        var sum = 0;
            //        for (var i = data.length - 1; i >= 0; i--) {
            //            sum += parseInt(data[i][key]);
            //        }

            //        return sum;
            //    };
            //});
    }]);