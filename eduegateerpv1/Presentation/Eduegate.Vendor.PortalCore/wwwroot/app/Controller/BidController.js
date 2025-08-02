app.controller("BidController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {
        console.log("BidController Loaded");

        $scope.isSlideVisible = false;
        $scope.collapseItemList = false;
        $scope.collapseComparedList = false;
        $scope.itemListAll = true;
        $scope.CompareList = null;
        $scope.isModalVisible = false;
        $scope.isModalOpenBtnVisible = true;

        $scope.Init = function (window, referenceID) {

            if (window == "BidUsersList") {
                $scope.LoadTenderList();
            }
            else if (window == "RFQComparisonScreen") {
                $scope.LoadRFQData(referenceID);
            }
        };


        // Function to toggle slide visibility  
        $scope.toggleSlide = function (tender) {
            $scope.isSlideVisible = !$scope.isSlideVisible;
            if ($scope.isSlideVisible) {
                $scope.ListBidUsers(tender.TenderIID);
            }
        };

        // Function to close the slide  
        $scope.closeSlide = function () {
            $scope.isSlideVisible = false;
        };


        $scope.LoadTenderList = function () {
            $.ajax({
                url: utility.myHost + "Bid/GetTenderList",
                type: "GET",
                success: function (result) {
                    if (!result.IsError && result.Response != null) {
                        $scope.$apply(function () {
                            $scope.TenderList = result.Response;
                        });
                    }
                }
            });
        };


        $scope.ListBidUsers = function (tenderID) {
            return new Promise(function (resolve, reject) {
                $.ajax({
                    url: utility.myHost + "Bid/GetBidUserListByTenderID?tenderID=" + tenderID,
                    type: "GET",
                    success: function (result) {
                        if (!result.IsError && result.Response != null) {
                            $scope.$apply(function () {
                                $scope.BidUsersList = result.Response;
                            });
                            resolve(result.Response); // Resolve the promise  
                        } else {
                            reject(new Error("Error fetching bid users.")); // Reject if there's an error  
                        }
                    },
                    error: function (err) {
                        reject(err); // Reject the promise on AJAX error  
                    }
                });
            });
        };


        $scope.OpenRFQCompareScreen = function (tender) {
            window.location = '/Bid/RFQComparison?tenderIID=' + tender.TenderIID;
        }

        $scope.LoadRFQData = function (referenceID) {

            var tenderID = referenceID;

            $.ajax({
                url: utility.myHost + "Bid/GetQuotationListByTenderID?tenderIID=" + tenderID,
                type: "GET",
                success: function (result) {
                    if (!result.IsError && result.Response != null) {
                        $scope.$apply(function () {
                            $scope.QuotationList = result.Response;
                            let groupedQuotations = $scope.QuotationList.TransactionDetails.reduce(function (acc, transaction) {
                                // Check if the QuotationNo entry already exists in the accumulator  
                                if (!acc[transaction.QuotationNo]) {
                                    acc[transaction.QuotationNo] = {
                                        QuotationNo: transaction.QuotationNo,
                                        SupplierCode: transaction.SupplierCode,
                                        Supplier: transaction.Supplier,
                                        NetAmount: transaction.Amount, // Initialize with the current transaction amount  
                                        GrossAmount: transaction.GrossAmount, // Initialize with the current transaction amount  
                                        SubmittedDateString: transaction.SubmittedDate, // Initialize with the current transaction SubmittedDate  
                                        Count: 1, // Start count at 1 since this is the first transaction  
                                        Details: [transaction], // Initialize details with the current transaction  
                                        isCollapsed: true,
                                        selected: true
                                    };
                                } else {
                                    // If the QuotationNo exists, accumulate the amount and adjust other fields  
                                    acc[transaction.QuotationNo].NetAmount += transaction.Amount; // Add to the total amount  
                                    acc[transaction.QuotationNo].GrossAmount += transaction.GrossAmount; // Add to the Gross amount  
                                    acc[transaction.QuotationNo].Count++; // Increment the count  

                                    acc[transaction.QuotationNo].Details.push(transaction); // Add to the details 

                                    // Keep SubmittedDateString as the first submitted date if it hasn't been set yet  
                                    if (!acc[transaction.QuotationNo].SubmittedDateString) {
                                        acc[transaction.QuotationNo].SubmittedDateString = transaction.SubmittedDateString;
                                    }
                                    if (!acc[transaction.QuotationNo].QTDiscount) {
                                        acc[transaction.QuotationNo].QTDiscount = transaction.QTDiscount;
                                    }
                                }
                                return acc;
                            }, {});

                            // Convert the accumulator object back to an array  
                            $scope.GroupedQuotationList = Object.values(groupedQuotations);

                            // Sort the array by NetAmount in ascending order  
                            $scope.GroupedQuotationList.sort(function (a, b) {
                                return a.NetAmount - b.NetAmount; // Ascending order  
                            });

                            //Set all checkbox value set as true
                            $scope.GroupedQuotationList = $scope.GroupedQuotationList.map(group => {
                                return {
                                    ...group,
                                    Details: group.Details.map(detail => ({
                                        ...detail,
                                        selected: true
                                    }))
                                };
                            });

                        });
                    }
                }
            });
        }

        $scope.toggleDetails = function (quotation) {
            quotation.isCollapsed = !quotation.isCollapsed;
        };

        // Function to toggle the collapsible section
        $scope.toggleCollapse = function (collapse) {
            if (collapse == "collapseItemList") {
                $scope.collapseItemList = !$scope.collapseItemList;
            }
            else if (collapse == "collapseComparedList") {
                $scope.collapseComparedList = !$scope.collapseComparedList;
            }
        };


        //CheckBox functionalities -- Start
        $scope.itemListCheckBoxClick = function (quotation) {
            quotation.selected = !quotation.selected;

            quotation.Details.forEach(detail => {
                detail.selected = quotation.selected;
            });
        };

        $scope.detailCheckBoxClick = function (item) {
            item.selected = !item.selected;
        };
        //CheckBox functionalities -- End


        //Compare list
        $scope.compareList = function () {

            const selectedItems = $scope.GroupedQuotationList.flatMap(x =>
                x.Details.filter(item => item.selected && item.Quantity > 0)
            );

            const groupedItems = {};
            selectedItems.forEach(item => {
                if (!groupedItems[item.SKUID.Key]) {
                    groupedItems[item.SKUID.Key] = [item];
                } else {
                    groupedItems[item.SKUID.Key].push(item);
                }
            });

            // Find the item with the least price in each group
            const finalPriceList = Object.values(groupedItems).map(group => {
                return group.reduce((acc, cur) => acc.UnitPrice < cur.UnitPrice ? acc : cur);
            });

            let groupedQuotations = finalPriceList.reduce(function (acc, compareList) {
                if (!acc[compareList.QuotationNo]) {
                    acc[compareList.QuotationNo] = {
                        QuotationNo: compareList.QuotationNo,
                        SupplierCode: compareList.SupplierCode,
                        Supplier: compareList.Supplier,
                        Count: 1,
                        Details: [compareList],
                        isCollapsed: false,
                    };
                } else {
                    acc[compareList.QuotationNo].Count++; // Increment the count  

                    acc[compareList.QuotationNo].Details.push(compareList); // Add to the details 
                }
                return acc;
            }, {});

            $scope.CompareList = Object.values(groupedQuotations);

        };


        $scope.ValidateAndOpenTender = function (tender) {
            $scope.ListBidUsers(tender.TenderIID)
                .then(function () {
                    if ($scope.BidUsersList) {
                        let isOpenedUsers = $scope.BidUsersList.filter(x => x.IsTenderOpened);
                        let currentUserLog = $scope.BidUsersList.find(x => x.LoginID == tender.LoginID);
                        let NumberOfAuth = $scope.BidUsersList[0].NumOfAuthorities;

                        if (NumberOfAuth > 1 && (isOpenedUsers.length < NumberOfAuth)) {
                            $scope.$apply(function () {
                                if (currentUserLog) {
                                    if (currentUserLog.IsTenderOpened) {
                                        $scope.isModalOpenBtnVisible = false;
                                    } else {
                                        $scope.isModalOpenBtnVisible = true;
                                        $scope.SelectedTender = tender;
                                        $scope.TenderAuthMapIID = currentUserLog.TenderAuthMapIID;
                                    }
                                }

                                $('#exampleModal').modal('show');
                            });
                        }
                        else {
                            $scope.OpenRFQCompareScreen(tender);
                        }
                    }
                    else {
                        console.log("No bid users found !");
                        return null;
                    }
                })
                .catch(function (error) {
                    console.error("Failed to fetch bid users:", error);
                });
        };

        $scope.closeModal = function () {
            $('#exampleModal').modal('hide');
        };

        //Update log when bid user is opened
        $scope.OpenAndUpdateLog = function (tender, iid) {
            $.ajax({
                url: utility.myHost + "Bid/OpenAndUpdateTenderLog",
                type: "POST",
                data: { iid: iid },
                success: function (result) {
                    $scope.$apply(function () {
                        if (result) {
                            $('#exampleModal').modal('hide');
                            $scope.OpenRFQCompareScreen(tender);
                        } else {
                            $scope.ValidateAndOpenTender(tender);
                        }
                    });
                }
            });
        };


        $scope.ApproveList = function (tenderID, userDetails) {

            const finalItemList = $scope.CompareList.map(item => item.Details).flat();

            GetSettingValue('BID_APPROVAL_DOC_TYP_ID').then(function (docTypeID) {
                var toEntity = {
                    TransactionHead: {
                        TenderID: tenderID,
                        DocumentTypeID: docTypeID,
                        TransactionDetails: finalItemList
                    }
                };

                $.ajax({
                    url: utility.myHost + "Bid/SaveBidApprovalItemList",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(toEntity),
                    success: function (result) {
                        $scope.$apply(function () {
                            if (result.IsError === false) {
                                toastr.success(result.ReturnMessage);
                            } else {
                                toastr.error(result.ReturnMessage);
                            }
                        });
                    }
                });
            }).catch(function (error) {
                toastr.error('Error fetching document type ID');
            });
        };


        function GetSettingValue(settingCode) {
            return $http({
                method: 'GET',
                url: utility.myHost + "Mutual/GetSettingValueByKey?settingKey=" + settingCode,
            }).then(function (result) {
                if (result) {
                    return result.data;
                } else {
                    return null;
                }
            });
        }

    }
]);
