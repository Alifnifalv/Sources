app.controller("AccountVoucherController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$rootScope", "$controller",
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $rootScope, $controller) {


        console.log("RVMissionController Controller");

        $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout });



        $scope.DocumentTypeChange = function () {
            var GetNextTransactionNumberByMonthYearUrl = 'RVMission/GetNextTransactionNumberByMonthYear';
            $.ajax({
                type: "POST",
                url: GetNextTransactionNumberByMonthYearUrl,
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ vm: $scope.CRUDModel.Model}),
                success: function (result) {
                    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();

                    //if (result.IsError)
                    //    $().showMessage($scope, $timeout, true, result.UserMessage);
                    
                    $scope.CRUDModel.Model.MasterViewModel.TransactionNumber = result;
                },
                complete: function (result) {
                    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                }
            });

        }


        $scope.GetNextTransactionNumber = function () {

            var GetNextTransactionNumberByMonthYearUrl = 'JV/GetNextTransactionNumberByMonthYear';
            $.ajax({
                type: "POST",
                url: GetNextTransactionNumberByMonthYearUrl,
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ vm: $scope.CRUDModel.Model }),
                success: function (result) {
                    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();

                    //if (result.IsError)
                    //    $().showMessage($scope, $timeout, true, result.UserMessage);

                    $scope.CRUDModel.Model.MasterViewModel.TransactionNumber = result;
                },
                complete: function (result) {
                    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                }
            });

        }

        $scope.AccountCodeChange = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {

            var index = 0;
            var baseRow = null;
            if ($scope.CRUDModel.Model.DetailViewModel != null) {
                baseRow = $scope.CRUDModel.Model.DetailViewModel[index];
            }
            $scope.CRUDModel.Model.DetailViewModel.splice(1, $scope.CRUDModel.Model.DetailViewModel.length - 1);
            baseRow.InvoiceNumber = null;
            baseRow.InvoiceAmount = null;
            baseRow.PaidAmount = null;
            baseRow.UnPaidAmount = null;
            baseRow.Amount = null;
            baseRow.ReturnAmount = null;
            baseRow.CurrencyName = null;
            baseRow.ExchangeRate = null;
            baseRow.DueDate = null;
            baseRow.ReceivableID = null;
            baseRow.CurrencyID = null;
            baseRow.JobMissionNumber = null;

            $.ajax({
                url: "RVMission/GetReceivablesByAccountId?AccountID=" + currentRow.DetailAccount.Key + "&DocumentType=" + currentRow.DocumentReferenceTypeID,
                type: 'GET',
                success: function (ReceivablesDTOList) {
                    if (ReceivablesDTOList != null) {


                        $.each(ReceivablesDTOList, function (index, resultItem) {
                            var currentRow = null;
                            if ($scope.CRUDModel.Model.DetailViewModel != null) {
                                currentRow = $scope.CRUDModel.Model.DetailViewModel[index];
                            }
                            if (currentRow == null) {
                                $scope.CRUDModel.Model.DetailViewModel.splice(index, 0, angular.copy(baseRow));
                                currentRow = $scope.CRUDModel.Model.DetailViewModel[index];
                            }

                            if (currentRow.hasOwnProperty("InvoiceNumber") == true)
                                currentRow.InvoiceNumber = resultItem.InvoiceNumber;

                            if (currentRow.hasOwnProperty("InvoiceAmount") == true)
                                currentRow.InvoiceAmount = resultItem.Amount;

                            if (currentRow.hasOwnProperty("PaidAmount") == true)
                                currentRow.PaidAmount = resultItem.PaidAmount;

                            if (currentRow.hasOwnProperty("UnPaidAmount") == true)
                                currentRow.UnPaidAmount = resultItem.Amount - resultItem.ReturnAmount - resultItem.PaidAmount;

                            if (currentRow.hasOwnProperty("Amount") == true)
                                currentRow.Amount = 0;

                            if (currentRow.hasOwnProperty("ReturnAmount") == true)
                                currentRow.ReturnAmount = resultItem.ReturnAmount;


                            if (currentRow.hasOwnProperty("CurrencyName") == true)
                                currentRow.CurrencyName = resultItem.CurrencyName;

                            if (currentRow.hasOwnProperty("CurrencyID") == true)
                                currentRow.CurrencyID = resultItem.CurrencyID;

                            if (currentRow.hasOwnProperty("ExchangeRate") == true)
                                currentRow.ExchangeRate = resultItem.ExchangeRate;

                            if (currentRow.hasOwnProperty("DueDate") == true)
                                currentRow.DueDate = resultItem.DueDate;

                            if (currentRow.hasOwnProperty("ReceivableID") == true)
                                currentRow.ReceivableID = resultItem.ReceivableIID;

                            if (currentRow.hasOwnProperty("JobMissionNumber") == true)
                                currentRow.JobMissionNumber = resultItem.JobMissionNumber;


                        })


                    }
                }
            })
        };

        $scope.AllocationAmountChange = function (model) {
            $.each($scope.CRUDModel.Model.DetailViewModel, function (index, currentRow) {
                if (currentRow.hasOwnProperty("Amount") === true)
                    currentRow.Amount = 0;
            });
        };


        $scope.AllocateAmount = function (model) {
            var Amount = model.MasterViewModel.Amount;
            var RemainingAmount = Amount;
            $.each(model.DetailViewModel, function (index, rowItem) {
                if (RemainingAmount > 0) {
                    if (RemainingAmount >= rowItem.UnPaidAmount) {
                        rowItem.Amount = rowItem.UnPaidAmount;
                        RemainingAmount = RemainingAmount - rowItem.UnPaidAmount;
                    }
                    else {
                        rowItem.Amount = RemainingAmount;
                        RemainingAmount = 0;
                    }
                }
            });

        };

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




        $scope.PayableAccountCodeChange = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {

            var index = 0;
            var baseRow = null;
            if ($scope.CRUDModel.Model.DetailViewModel != null) {
                baseRow = $scope.CRUDModel.Model.DetailViewModel[index];
            }
            $scope.CRUDModel.Model.DetailViewModel.splice(1, $scope.CRUDModel.Model.DetailViewModel.length - 1);
            baseRow.InvoiceNumber = null;
            baseRow.InvoiceAmount = null;
            baseRow.PaidAmount = null;
            baseRow.UnPaidAmount = null;
            baseRow.Amount = null;
            baseRow.ReturnAmount = null;
            baseRow.CurrencyName = null;
            baseRow.ExchangeRate = null;
            baseRow.DueDate = null;
            baseRow.PayableID = null;
            baseRow.CurrencyID = null;
            
            $.ajax({
                url: "PVInvoice/GetPayablesByAccountId?AccountID=" + currentRow.DetailAccount.Key + "&DocumentType=" + currentRow.DocumentReferenceTypeID,
                type: 'GET',
                success: function (ReceivablesDTOList) {
                    if (ReceivablesDTOList != null) {


                        $.each(ReceivablesDTOList, function (index, resultItem) {
                            var currentRow = null;
                            if ($scope.CRUDModel.Model.DetailViewModel != null) {
                                currentRow = $scope.CRUDModel.Model.DetailViewModel[index];
                            }
                            if (currentRow == null) {
                                $scope.CRUDModel.Model.DetailViewModel.splice(index, 0, angular.copy(baseRow));
                                currentRow = $scope.CRUDModel.Model.DetailViewModel[index];
                            }

                            if (currentRow.hasOwnProperty("InvoiceNumber") == true)
                                currentRow.InvoiceNumber = resultItem.InvoiceNumber;

                            if (currentRow.hasOwnProperty("InvoiceAmount") == true)
                                currentRow.InvoiceAmount = resultItem.Amount;

                            if (currentRow.hasOwnProperty("PaidAmount") == true)
                                currentRow.PaidAmount = resultItem.PaidAmount;

                            if (currentRow.hasOwnProperty("UnPaidAmount") == true)
                                currentRow.UnPaidAmount = resultItem.Amount - resultItem.ReturnAmount -  resultItem.PaidAmount;

                            if (currentRow.hasOwnProperty("Amount") == true)
                                currentRow.Amount = 0;

                            if (currentRow.hasOwnProperty("ReturnAmount") == true)
                                currentRow.ReturnAmount = resultItem.ReturnAmount;


                            if (currentRow.hasOwnProperty("CurrencyName") == true)
                                currentRow.CurrencyName = resultItem.CurrencyName;

                            if (currentRow.hasOwnProperty("CurrencyID") == true)
                                currentRow.CurrencyID = resultItem.CurrencyID;

                            if (currentRow.hasOwnProperty("ExchangeRate") == true)
                                currentRow.ExchangeRate = resultItem.ExchangeRate;
                            if (currentRow.hasOwnProperty("DueDate") == true)
                                currentRow.DueDate = resultItem.DueDate;

                            if (currentRow.hasOwnProperty("PayableID") == true)
                                currentRow.PayableID = resultItem.PayableIID;



                        })


                    }
                }
            })
        }



        $scope.ProductSKUCodeChange = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {

            var index = 0;
            var baseRow = null;
            if ($scope.CRUDModel.Model.DetailViewModel != null) {
                baseRow = $scope.CRUDModel.Model.DetailViewModel[index];
            }
            $scope.CRUDModel.Model.DetailViewModel.splice(1, $scope.CRUDModel.Model.DetailViewModel.length - 1);
            baseRow.AvailableQuantity = null;
            baseRow.Amount = null;
            baseRow.CurrentAvgCost = null;
            baseRow.NewAvgCost = null;
        

            $.ajax({
                url: "DebitProduct/GetProductSKUMapByID?ProductSKUMapIID=" + currentRow.SKUID.Key,
                type: 'GET',
                success: function (SKUDTOList) {
                    if (SKUDTOList != null) {


                        $.each(SKUDTOList, function (index, resultItem) {
                            var currentRow = null;
                            if ($scope.CRUDModel.Model.DetailViewModel != null) {
                                currentRow = $scope.CRUDModel.Model.DetailViewModel[index];
                            }
                            if (currentRow == null) {
                                $scope.CRUDModel.Model.DetailViewModel.splice(index, 0, angular.copy(baseRow));
                                currentRow = $scope.CRUDModel.Model.DetailViewModel[index];
                            }

                            if (currentRow.hasOwnProperty("AvailableQuantity") == true)
                                currentRow.AvailableQuantity = resultItem.AvailableQuantity;

                            if (currentRow.hasOwnProperty("CurrentAvgCost") == true)
                                currentRow.CurrentAvgCost = resultItem.CurrentAvgCost;

                            if (currentRow.hasOwnProperty("ProductSKUCode") == true)
                                currentRow.ProductSKUCode = resultItem.ProductSKUCode;

                          

                            if (currentRow.hasOwnProperty("Amount") == true)
                                currentRow.Amount = 0;

                        
                        })


                    }
                }
            })
        }


        $scope.AmountChange = function (detail) {
            if (detail.hasOwnProperty("NewAvgCost") == true)
                detail.NewAvgCost = detail.CurrentAvgCost - (detail.Amount / detail.AvailableQuantity);
          
        }

        $scope.AccountGroupChanges = function ($event, $element, groupModel) {
            showOverlay();
            var model = groupModel;
            model.Account = null;
            var url = "Schools/School/GetAccountByGroupID?groupID=" + model.AccountGroup.Key;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.Account = result.data;
                    $scope.CostCenter = null;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        };

        $scope.PaymentModeChange = function ($event, $element, paymentModel) {
            showOverlay();
            var model = paymentModel;
            //model.Account = null;
            var url = "Schools/School/GetAccountByPayementModeID?paymentModeID=" + model.PaymentModes.Key;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.Account = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        };

        $scope.AccountChange = function ($event, $element, gridModel) {
            showOverlay();
            var model = gridModel;
            model.AccountGroup = null;
            var url = "Schools/School/GetAccountGroupByAccountID?accountID=" + model.Account.Key;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.AccountGroup = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        };
        $scope.AccountChangesForCostCenter = function ($event, $element, gridModel) {
            showOverlay();
            var model = gridModel;
           
            $http({
                method: 'Get',
                url: "Schools/School/GetCostCenterByAccount?accountID=" + model.Account.Key,
            })
                .then(function (result) {
                    $scope.LookUps.CostCenter = result.data;
                    if (result.data.length == 1) {
                        model.CostCenter.Key = result.data[0].Key;
                        model.CostCenter.Value = result.data[0].Value;
                    }
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });

            $http({
                method: 'Get',
                url: "Schools/School/GetSubLedgerByAccount?accountID=" + model.Account.Key,
            })
                .then(function (result) {
                    $scope.LookUps.AccountSubLedgers = result.data;
                    if (result.data.length == 1) {
                        model.AccountSubLedgers.Key = result.data[0].Key;
                        model.AccountSubLedgers.Value = result.data[0].Value;
                    }
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        };

        function showOverlay() {
            $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
        }

        function hideOverlay() {
            $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
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