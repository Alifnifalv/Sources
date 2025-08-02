app.controller("UploadFileController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    console.log("UploadFileController");
    $scope.uploadedPdfDocuments = [];
    $scope.uploadedExcelDocuments = [];
    $scope.BankReconciliationUnMatchedWithLedgerEntries = [];
    $scope.BankReconciliationUnMatchedWithBankEntries = [];
    $scope.BankReconciliationManualEntry = [];
    $scope.BankReconciliationManualEntryGrid = [];
    $scope.DataFeedModel = {};
    $scope.DataFeedModel.BankReconciliationIID = 0;
    $scope.LedgerClosingBalInput = 0.0000;
    $scope.BankClosingBalInput = 0.0000;
    $scope.Search = [];
    $scope.Search.BankAccount = {};
    $scope.BankReconciliation = {};
    $scope.BankReconciliation.BankReconciliationHeadIID = 0;
    $scope.Init = function (window, model) {
        windowContainer = '#' + window;
        $scope.Model = model;
        $scope.InitializeDropZonePlugin();
        $http({
            method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=BankAccounts&defaultBlank=false'
        }).then(function (result) {
            $scope.BankAccounts = result.data;
        });
        if ($scope.Model != null && $scope.Model != undefined && $scope.Model.BankReconciliationHeadIID != null && $scope.Model.BankReconciliationHeadIID != 0 )
        {
            $scope.FillEditData($scope.Model);
        }
    }
    $scope.FillEditData = function (model)
    {
        $scope.DataFeedModel.BankReconciliationIID = model.BankReconciliationHeadIID;
        $scope.BankReconciliation.BankReconciliationHeadIID = model.BankReconciliationHeadIID;
      
        $scope.FromDateString = model.FromDateString;
        $scope.ToDateString = model.ToDateString;
        $scope.BankStatementID = model.BankStatementID;
        $scope.BankName = model.BankName;
        $scope.BankAccountID = model.BankAccountID;
        $scope.FromDate = model.FromDate;
        $scope.ToDate = model.ToDate;
       // $scope.ContentFileData = model.ContentFileData;
        $scope.ContentFileID = model.ContentFileID;
        $scope.ContentFileName = model.ContentFileName;
        $scope.LedgerOpeningBalance = model.LedgerOpeningBalance;
        $scope.LedgerClosingBalance = model.LedgerClosingBalance;
        $scope.BankOpeningBalance = model.BankOpeningBalance;
        $scope.BankClosingBalance = model.BankClosingBalance;
        $scope.BankReconciliationBankTrans = model.BankReconciliationBankTrans;
        $scope.BankReconciliationLedgerTrans = model.BankReconciliationLedgerTrans;
        $scope.BankReconciliationEntries = model.BankReconciliationEntries;
        $scope.BankReconciliationMatchedEntries = model.BankReconciliationMatchedEntries;
        $scope.BankReconciliationUnMatchedWithLedgerEntries = model.BankReconciliationUnMatchedWithLedgerEntries;
        $scope.BankReconciliationUnMatchedWithBankEntries = model.BankReconciliationUnMatchedWithBankEntries;
        $scope.BankReconciliationMatchingLedgerEntries = model.BankReconciliationLedgerTrans;
        $scope.BankReconciliationMatchingBankEntries = model.BankReconciliationBankTrans;
        $scope.BankReconciliationManualEntry = model.BankReconciliationManualEntry;
        $scope.BankReconciliationOldManualEntry = model.BankReconciliationManualEntry;
    }
    $scope.DownloadTemplate = function () {

        $.ajax({
            url: "Documents/DocManagement/DownloadTemplate?templateTypeID=1",
            type: 'GET',
            success: function (result) {
                if (result.IsSuccess == true)
                    window.location = result.DownloadPath;
                else {

                }
            },
            error: function () {

            }
        })
        //window.location = $('#DataFeedType option:selected').attr('attribute_url');

    }
    $scope.InitializeDropZonePlugin = function () {
        Dropzone.autoDiscover = false;
        $timeout(function () {
            $("#pdfUploadDropzone").dropzone({
                previewsContainer: "#previews",
                addRemoveLinks: true,
                uploadMultiple: true,
                success: function (response, file) {
                    $.each(file.FileInfo, function (index, item) {
                        item.FilePath = item.FilePath + "?" + Math.random();
                        //+ item.FileName.split('.')[0]
                        $scope.uploadedPdfDocuments.push(item);
                    });
                    console.log("uploaded successfully" + response);
                },
                multiplesuccess: function (file, response) {
                    console.log("uploaded successfully" + response);
                }
            });
            $("#excelUploadDropzone").dropzone({
                previewsContainer: "#previews",
                addRemoveLinks: true,
                uploadMultiple: true,
                success: function (response, file) {
                    $.each(file.FileInfo, function (index, item) {
                        item.FilePath = item.FilePath + "?" + Math.random();
                        //+ item.FileName.split('.')[0]
                        $scope.uploadedExcelDocuments.push(item);
                    });
                    console.log("uploaded successfully" + response);
                },
                multiplesuccess: function (file, response) {
                    console.log("uploaded successfully" + response);
                }
            });
        });
    }


    $scope.searchNodes = function (query) {

        $scope.cy.nodes().style({
            'background-color': 'SteelBlue',
            'color': '#444444',           // Reset label color to black
            'font-size': '14px',       // Reset font size
            'text-shadow': 'none',     // Reset text shadow
            'border-width': 0,
            'border-color': '#000',
            'text-background-color': '#D9D9D9',
            'text-border-color': '#D9D9D9',

        });

        if (query) {
            const matchedNodes = $scope.cy.nodes().filter(function (ele) {
                return ele.data('label').toLowerCase().includes(query.toLowerCase());
            });

            // Highlight matched nodes by changing label color, border color, font size, and adding a glow effect
            matchedNodes.style({
                'background-color': 'SteelBlue',
                'color': 'red',          // Change label color to red
                'font-size': '14px',     // Increase font size for better visibility
                'text-shadow': '0 0 10px rgba(255, 0, 0, 0.75)', // Apply glow effect to text

            });
        }
    };
    $scope.TriggerUploadPdfFile = function () {
        angular.element('#UploadFile').trigger('click');
    }
    $scope.TriggerUploadExcelFile = function () {
        angular.element('#UploadFile').trigger('click');
    }
    $scope.UploadPdfFiles = function (uploadfiles) {
        var url = 'Documents/DocManagement/UploadPdfDocument'
        var xhr = new XMLHttpRequest();
        var fd = new FormData();
        for (i = 0; i < uploadfiles.files.length; i++) {
            fd.append(uploadfiles.files[i].name, uploadfiles.files[i])
        }

        xhr.open('POST', url, true)
        xhr.onreadystatechange = function (url) {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var result = JSON.parse(xhr.response)
                if (result.Success == true && result.FileInfo.length > 0) {
                    $.each(result.FileInfo, function (index, item) {
                        item.FilePath = item.FilePath + '?' + item.ContentFileName.split('.')[0] + Math.random()
                        item.ReferenceID = item.ContentFileIID;
                        $scope.uploadedPdfDocuments.push(item)
                    });
                }
            }
        }
        xhr.send(fd);
    }
    $scope.UploadExcelFiles = function (uploadfiles) {
        var url = 'Documents/DocManagement/UploadExcelDocument'
        var xhr = new XMLHttpRequest();
        var fd = new FormData();
        for (i = 0; i < uploadfiles.files.length; i++) {
            fd.append(uploadfiles.files[i].name, uploadfiles.files[i])
        }

        xhr.open('POST', url, true)
        xhr.onreadystatechange = function (url) {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var result = JSON.parse(xhr.response)
                if (result.Success == true && result.FileInfo.length > 0) {
                    $.each(result.FileInfo, function (index, item) {
                        item.FilePath = item.FilePath + '?' + item.ContentFileName.split('.')[0] + Math.random()
                        item.ReferenceID = item.ContentFileIID;
                        $scope.uploadedExcelDocuments.push(item)
                    });
                }
            }
        }
        xhr.send(fd);
    }
    $scope.DeleteUploadedDcuments = function (DocInfo) {
        var fileName = DocInfo != null && DocInfo != undefined ? DocInfo.FileName : ""
        $.ajax({
            url: "Documents/DocManagement/DeleteUploadedDocument?fileName=" + fileName,
            type: "POST",
            success: function (result) {
                if (result.Success) {
                    if (fileName != null) {
                        $scope.uploadedPdfDocuments = $.grep($scope.uploadedPdfDocuments, function (e) {
                            return e.FileName != DocInfo.FileName;
                        });
                    }
                }
            }
        })
    }

    $scope.selectedBankEntryIndices = [];
    $scope.selectedLedgerIndices = [];
    $scope.selectedBankEntryIndex = null;
    $scope.selectedLedgerIndex = [];
    $scope.UpdateSelectedIndex = function (index, isSelected, type) {
        if (isSelected) {
            if (type == 'B') {
                $scope.selectedBankEntryIndices.push(index);
                $scope.selectedBankEntryIndex = index;
            }
            else {
                $scope.selectedLedgerIndices.push(index);
                $scope.selectedLedgerIndex = index;
            }
        } else {
            if (type == 'B') {
                var idx = $scope.selectedBankEntryIndices.indexOf(index);
                if (idx > -1) {
                    $scope.selectedBankEntryIndices.splice(idx, 1);
                }
            }
            else {
                var idx = $scope.selectedLedgerIndices.indexOf(index);
                if (idx > -1) {
                    $scope.selectedLedgerIndices.splice(idx, 1);
                }
            }
        }
    };
    $scope.MovetoMatching = function () {
        let selectedBankEntries = [];
        let selectedLedgerEntries = [];

        if ($scope.BankReconciliationMatchingLedgerEntries[$scope.selectedLedgerIndex].IsMatching) {
            $scope.BankReconciliationMatchingLedgerEntries[$scope.selectedLedgerIndex].IsMoved = true;
            selectedLedgerEntries.push($scope.BankReconciliationMatchingLedgerEntries[$scope.selectedLedgerIndex]);
        }
        else
            $scope.BankReconciliationMatchingLedgerEntries[$scope.selectedLedgerIndex].IsMoved = false;

        if ($scope.BankReconciliationMatchingBankEntries[$scope.selectedBankEntryIndex].IsMatching) {
            $scope.BankReconciliationMatchingBankEntries[$scope.selectedBankEntryIndex].IsMoved = true;
            selectedBankEntries.push($scope.BankReconciliationMatchingBankEntries[$scope.selectedBankEntryIndex]);
        }
        else
            $scope.selectedBankEntries[$scope.selectedBankEntryIndex].IsMoved = false;


        if (selectedBankEntries.length > 0 && selectedLedgerEntries.length > 0) {

            selectedLedgerEntries.forEach(function (ledgerEntry, index) {
                let bankEntry = selectedBankEntries[index];
                let combinedEntry = {
                    Particulars: ledgerEntry.Narration,
                    BankDescription: bankEntry.Narration,
                    PostDate: bankEntry.PostDate,
                    TransDate: ledgerEntry.PostDate,
                    ChequeNo: ledgerEntry.ChequeNo,
                    LedgerDebitAmount: ledgerEntry.LedgerDebitAmount,
                    LedgerCreditAmount: ledgerEntry.LedgerCreditAmount,
                    BankDebitAmount: bankEntry.BankDebitAmount,
                    BankCreditAmount: bankEntry.BankCreditAmount
                };

                $scope.BankReconciliationMatchedEntries.push(combinedEntry);
            });

            $scope.BankReconciliationMatchingLedgerEntries[$scope.selectedLedgerIndex].IsMatching = false;
            $scope.BankReconciliationMatchingBankEntries[$scope.selectedBankEntryIndex].IsMatching = false;

            angular.forEach(selectedLedgerEntries, function (ledgerEntry) {
                let indexToRemove = -1;
                console.log('Before removal:', $scope.BankReconciliationUnMatchedWithBankEntries);
                angular.forEach($scope.BankReconciliationUnMatchedWithBankEntries, function (matchedEntry, index) {
                    //if (matchedEntry.Particulars === ledgerEntry.Narration && matchedEntry.TransDate === ledgerEntry.PostDate && ledgerEntry.LedgerDebitAmount == matchedEntry.LedgerDebitAmount && ledgerEntry.LedgerCreditAmount == matchedEntry.LedgerCreditAmount) {

                    if (matchedEntry.Particulars == ledgerEntry.Narration && ledgerEntry.LedgerDebitAmount == matchedEntry.LedgerDebitAmount && ledgerEntry.LedgerCreditAmount == matchedEntry.LedgerCreditAmount) {

                        indexToRemove = index;
                        return;
                    }
                });

                if (indexToRemove !== -1) {
                    $scope.BankReconciliationUnMatchedWithBankEntries.splice(indexToRemove, 1);
                    console.log('after removal:', $scope.BankReconciliationUnMatchedWithBankEntries);
                }
            });

            angular.forEach(selectedBankEntries, function (bankEntry) {
                let indexToRemove = -1;

                angular.forEach($scope.BankReconciliationUnMatchedWithLedgerEntries, function (matchedEntry, index) {
                    if (matchedEntry.Particulars == bankEntry.Narration && bankEntry.BankCreditAmount == matchedEntry.BankCreditAmount && bankEntry.BankDebitAmount == matchedEntry.BankDebitAmount) {

                        indexToRemove = index;
                        return;
                    }
                });

                if (indexToRemove !== -1) {
                    $scope.BankReconciliationUnMatchedWithLedgerEntries.splice(indexToRemove, 1);

                }
            });

            //$scope.clearSelections();
        } else {
            alert('Please select entries from both tables.');
        }
    };
    $scope.UndoMatching = function () {


        let ledgerSelected = [];
        let bankSelected = [];

        angular.forEach($scope.BankReconciliationMatchingLedgerEntries, function (entry) {
            if (entry.IsMatching == true && entry.IsMoved == true) {
                ledgerSelected.push(entry);
            }
        });

        angular.forEach($scope.BankReconciliationMatchingBankEntries, function (entry) {
            if (entry.IsMatching == true && entry.IsMoved == true) {
                bankSelected.push(entry);
            }
        });

        if (ledgerSelected.length > 0 && bankSelected.length > 0) {

            angular.forEach(ledgerSelected, function (ledgerEntry) {
                let indexToRemove = -1;

                angular.forEach($scope.BankReconciliationMatchedEntries, function (matchedEntry, index) {
                    //if (matchedEntry.Particulars === ledgerEntry.Narration && matchedEntry.TransDate === ledgerEntry.PostDate && ledgerEntry.LedgerDebitAmount == matchedEntry.LedgerDebitAmount && ledgerEntry.LedgerCreditAmount == matchedEntry.LedgerCreditAmount) {

                    if (ledgerEntry.LedgerDebitAmount == matchedEntry.LedgerDebitAmount && ledgerEntry.LedgerCreditAmount == matchedEntry.LedgerCreditAmount) {

                        indexToRemove = index;
                    }
                });

                if (indexToRemove !== -1) {
                    $scope.BankReconciliationMatchedEntries.splice(indexToRemove, 1);


                }
            });

            angular.forEach(bankSelected, function (bankEntry) {
                let indexToRemove = -1;

                angular.forEach($scope.BankReconciliationMatchedEntries, function (matchedEntry, index) {
                    if (bankEntry.BankCreditAmount == matchedEntry.BankCreditAmount && bankEntry.BankDebitAmount == matchedEntry.BankDebitAmount) {

                        indexToRemove = index;
                    }
                });

                if (indexToRemove !== -1) {
                    $scope.BankReconciliationMatchedEntries.splice(indexToRemove, 1);

                }
            });

            if (bankSelected.length > 0 && ledgerSelected.length > 0) {
                ledgerSelected.forEach(function (ledgerEntry, index) {

                    let EntryLedger = {
                        Particulars: ledgerEntry.Narration,
                        BankDescription: "-",
                        PostDate: "-",
                        TransDate: ledgerEntry.PostDate,
                        ChequeNo: ledgerEntry.ChequeNo,
                        CheqDate: ledgerEntry.CheqDate,
                        VoucherRef: ledgerEntry.VoucherRef,
                        LedgerDebitAmount: ledgerEntry.LedgerDebitAmount,
                        LedgerCreditAmount: ledgerEntry.LedgerCreditAmount,
                        BankDebitAmount: 0,
                        BankCreditAmount: 0,
                        //TranHeadID = ledgerEntry.TranHeadID,
                        //TranTailID = ledgerEntry.TranTailID, 
                    };
                    $scope.BankReconciliationUnMatchedWithBankEntries.push(EntryLedger);

                });

                bankSelected.forEach(function (bankEntry, index) {

                    let EntryBank = {
                        Particulars: bankEntry.Narration,
                        PostDate: bankEntry.PostDate,
                        TransDate: "-",
                        ChequeNo: "-",
                        CheqDate: "-",
                        VoucherRef: "-",
                        LedgerDebitAmount: 0,
                        LedgerCreditAmount: 0,
                        BankDebitAmount: bankEntry.BankDebitAmount,
                        BankCreditAmount: bankEntry.BankCreditAmount
                    };
                    $scope.BankReconciliationUnMatchedWithLedgerEntries.push(EntryBank);
                });

            }
            ledgerSelected.forEach(function (entry) {
                entry.IsMoved = false;
                entry.IsMatching = false;
                //entry.IsUndoMatching = false;
            });
            bankSelected.forEach(function (entry) {
                entry.IsMoved = false;
                entry.IsMatching = false;
                //entry.IsUndoMatching = false;
            });

        }
        else {
            alert('not found rows for undo matching.');
        }
    };

    $scope.UndoMatchingRow = function (matchedEntry) {

        let indexToRemove = $scope.BankReconciliationMatchedEntries.indexOf(matchedEntry);
        if (indexToRemove !== -1) {
            $scope.BankReconciliationMatchedEntries.splice(indexToRemove, 1);
        }
        angular.forEach($scope.BankReconciliationMatchingLedgerEntries, function (ledgerEntry) {
            if (ledgerEntry.Narration === matchedEntry.Particulars && ledgerEntry.PostDate === matchedEntry.TransDate) {
                ledgerEntry.IsMoved = false;
                ledgerEntry.IsMatching = false;

                let EntryLedger = {
                    Particulars: ledgerEntry.Narration,
                    BankDescription: "-",
                    PostDate: "-",
                    TransDate: ledgerEntry.PostDate,
                    ChequeNo: ledgerEntry.ChequeNo,
                    CheqDate: ledgerEntry.CheqDate,
                    VoucherRef: ledgerEntry.VoucherRef,
                    LedgerDebitAmount: ledgerEntry.LedgerDebitAmount,
                    LedgerCreditAmount: ledgerEntry.LedgerCreditAmount,
                    BankDebitAmount: 0,
                    BankCreditAmount: 0,
                    ReconciliationDebitAmount: ledgerEntry.LedgerCreditAmount,
                    ReconciliationCreditAmount: ledgerEntry.LedgerDebitAmount,
                };
                $scope.BankReconciliationUnMatchedWithBankEntries.push(EntryLedger);
            }
        });

        angular.forEach($scope.BankReconciliationMatchingBankEntries, function (bankEntry) {
            if (bankEntry.Narration === matchedEntry.BankDescription && bankEntry.PostDate === matchedEntry.PostDate) {
                bankEntry.IsMoved = false;
                bankEntry.IsMatching = false;

                let EntryBank = {
                    Particulars: bankEntry.Narration,
                    PostDate: bankEntry.PostDate,
                    TransDate: "-",
                    ChequeNo: "-",
                    CheqDate: "-",
                    VoucherRef: "-",
                    LedgerDebitAmount: 0,
                    LedgerCreditAmount: 0,
                    BankDebitAmount: bankEntry.BankDebitAmount,
                    BankCreditAmount: bankEntry.BankCreditAmount,
                    ReconciliationDebitAmount: bankEntry.BankCreditAmount,
                    ReconciliationCreditAmount: bankEntry.BankDebitAmount,
                };
                $scope.BankReconciliationUnMatchedWithLedgerEntries.push(EntryBank);

            }
        });
    };
    $scope.InsertRow = function (index) {

        var model = $scope.BankReconciliationManualEntry;
        var row = $scope.BankReconciliationManualEntryGrid[0];

        model.splice(index + 1, 0, angular.copy(row));
    };

    $scope.RemoveRow = function (index) {

        if ($scope.BankReconciliationManualEntry.length == 1) {
            if (index == 1) {
                $scope.LoadEntryGrid();
            }
        }
        else {

            var model = $scope.BankReconciliationManualEntry;
            var row = $scope.BankReconciliationManualEntryGrid[0];

            model.splice(index, 1);

            if (index === 0) {
                $scope.InsertRow(index, row, model);
            }
        }
    };

    $scope.LoadEntryGrid = function () {

        $scope.BankReconciliationManualEntry = [];

        $scope.BankReconciliationManualEntryGrid = [];

        $scope.BankReconciliationManualEntry.push(JSON.parse(JSON.stringify($scope.BankReconciliationOldManualEntry)));

        $scope.BankReconciliationManualEntryGrid = JSON.parse(JSON.stringify($scope.BankReconciliationManualEntry));
    };

    $scope.InitiateFeed = function () {
        if ($('#DataFeedType option:selected').index() > 0) {

            $scope.DataFeedModel.FeedFileName = $('.filename').val();
            var model = angular.copy($scope.DataFeedModel);


            $.ajax({
                url: 'DataFeed/DataFeed/InitiateFeed',
                type: 'POST',
                data: model,
                success: function (result) {
                    if ($("#divUploadProgress" + result.ID).length == 0) {
                        $("#divUploadedFiles").append(result.RawHTML);
                    }
                    else {
                        $("#divUploadProgress" + result.ID).replaceWith(result.RawHTML);
                    }
                    $scope.DataFeedModel.DataFeedID = result.ID;
                    $scope.ProcessFeed();
                }
            })
        }
    }

    $scope.SaveUploadedPdfFiles = function ($event) {
        if (!$scope.Search.BankAccount.Key) {
            $().showGlobalMessage($root, $timeout, true, "Please select a Bank!");
            return false;
        }
        if (!$scope.FromDateString) {
            $().showGlobalMessage($root, $timeout, true, "Please From Date!");
            return false;
        }
        if (!$scope.ToDateString) {
            $().showGlobalMessage($root, $timeout, true, "Please To Date!");
            return false;
        }
        showSpinner();        
        var data = { files: $scope.uploadedPdfDocuments, BankAccountID: $scope.Search.BankAccount.Key, FromDate:$scope.FromDateString, ToDate:$scope.ToDateString  }
        $.ajax({
            url: 'Documents/DocManagement/SaveUploadedPdfFiles',
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (result) {
                if (result.Success) {
                    $scope.uploadedPdfDocuments = []
                    $scope.$apply(function () {
                        // $scope.BankTransactions = result.Data.BankReconciliationUnMatchedWithLedgerEntries;
                        $scope.SelfCheckingString = result.Data.SelfCheckingString;
                        $scope.FromDateString = result.Data.FromDateString;
                        $scope.ToDateString = result.Data.ToDateString;
                        $scope.BankStatementID = result.Data.BankStatementID;
                        $scope.BankName = result.Data.BankName;
                        /*$scope.BankAccountID = result.Data.BankAccountID;*/
                        $scope.FromDate = result.Data.FromDate;
                        $scope.ToDate = result.Data.ToDate;
                        $scope.ContentFileData = result.Data.ContentFileData;
                        $scope.ContentFileID = result.Data.ContentFileID;
                        $scope.ContentFileName = result.Data.ContentFileName;
                        $scope.LedgerOpeningBalance = result.Data.LedgerOpeningBalance;
                        $scope.LedgerClosingBalance = result.Data.LedgerClosingBalance;
                        $scope.BankOpeningBalance = result.Data.BankOpeningBalance;
                        $scope.BankClosingBalance = result.Data.BankClosingBalance;
                        $scope.BankReconciliationBankTrans = result.Data.BankReconciliationBankTrans;
                        $scope.BankReconciliationLedgerTrans = result.Data.BankReconciliationLedgerTrans;
                        $scope.BankReconciliationEntries = result.Data.BankReconciliationEntries;
                        $scope.BankReconciliationMatchedEntries = result.Data.BankReconciliationMatchedEntries;
                        $scope.BankReconciliationUnMatchedWithLedgerEntries = result.Data.BankReconciliationUnMatchedWithLedgerEntries;
                        $scope.BankReconciliationUnMatchedWithBankEntries = result.Data.BankReconciliationUnMatchedWithBankEntries;
                        $scope.BankReconciliationMatchingLedgerEntries = result.Data.BankReconciliationLedgerTrans;
                        $scope.BankReconciliationMatchingBankEntries = result.Data.BankReconciliationBankTrans;
                        $scope.BankReconciliationManualEntry = result.Data.BankReconciliationManualEntry;
                        $scope.BankReconciliationOldManualEntry = result.Data.BankReconciliationManualEntry;
                        //console.log($scope.BankReconciliationUnMatchedWithBankEntries);
                        //console.log($scope.BankReconciliationUnMatchedWithLedgerEntries);
                        //if ($scope.BankReconciliationBankTrans.length > 0) {

                        //} else {

                        //    $().showGlobalMessage($root, $timeout, true, "No record(s) found!");
                        //}
                        hideSpinner();
                    });

                    $().showMessage($scope, $timeout, true, 'Files uploaded successfully.')
                    $('.dropzone')[0].dropzone.files.forEach(function (file) {
                        file.previewElement.remove()
                    })

                    $('.dropzone').removeClass('dz-started')
                    hideSpinner();
                }
            },
            error: function (error) {
                $().showMessage($scope, $timeout, true, 'Upload failed')
                hideSpinner()
            }
        })
    }
    $scope.SaveUploadedExcelFile = function ($event) {

        if (!$scope.Search.BankAccount.Key) {
            $().showGlobalMessage($root, $timeout, true, "Please select a Bank!");
            return false;
        }
        if (!$scope.FromDateString) {
            $().showGlobalMessage($root, $timeout, true, "Please select From Date!");
            return false;
        }
        if (!$scope.ToDateString) {
            $().showGlobalMessage($root, $timeout, true, "Please select To Date!");
            return false;
        }
        if (!$scope.LedgerClosingBalInput) {
            $().showGlobalMessage($root, $timeout, true, "Please enter Ledger Closing Balance!");
            return false;
        }
        if (!$scope.BankClosingBalInput) {
            $().showGlobalMessage($root, $timeout, true, "Please enter Bank Closing Balance!");
            return false;
        }

        showSpinner();
        var data = { files: $scope.uploadedExcelDocuments }
        $.ajax({
            url: 'Documents/DocManagement/SaveUploadedExcelFiles',
            type: 'POST',
            dataType: 'json',
            data: data,
            //success: function (result) {
            //    if (result.Success) {
            //        $scope.SaveOpeningFiles = [];
            success: function (result) {

                $scope.DataFeedModel.DataFeedID = result.ID;
                if (result.ID != null && result.ID != 0) {
                    $scope.ProcessFeed();
                }
            }

        })
        hideSpinner();
    }
    $scope.BankAccountChanges = function (selected) {
        //$scope.Search.BankAccount = selected;
    };
    $scope.EditBankReconciliation = function (bankReconciliationHeadIID) {
  
        if (bankReconciliationHeadIID) {
            //showSpinner();
            var url = utility.myHost + "Accounts/BankReconcilation/BankReconcilation?bankReconciliationHeadIID=" + bankReconciliationHeadIID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    /*hideSpinner()*/
                    $scope.AddWindow('BankReconcilation', 'BankReconcilation', 'BankReconcilation');

                    $('#' + 'BankReconcilation', '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                    $scope.ShowWindow('BankReconcilation', 'BankReconcilation', 'BankReconcilation');

                }, function () {
                    /*hideSpinner()*/
                });
        }
        else {
            /*hideSpinner()*/
        }
    };
    $scope.SaveBankReconciliationEntry = function () {
        if (!$scope.Search.BankAccount.Key) {
            $().showGlobalMessage($root, $timeout, true, "Please select a Bank!");
            return false;
        }
        if (!($scope.BankReconciliationManualEntry.length > 0 || $scope.BankReconciliationUnMatchedWithLedgerEntries > 0 || $scope.BankReconciliationUnMatchedWithBankEntries > 0)) {
            $().showGlobalMessage($root, $timeout, true, "Please select a Bank Reconciliation!");
            return false;
        }
        $scope.BankReconciliation = {};
        if ($scope.DataFeedModel.BankReconciliationIID != undefined && $scope.DataFeedModel.BankReconciliationIID != 0) {
            $scope.BankReconciliation.BankReconciliationHeadIID == $scope.DataFeedModel.BankReconciliationIID;
        }
        if ($scope.BankReconciliation.BankReconciliationHeadIID != undefined && $scope.BankReconciliation.BankReconciliationHeadIID != 0)
        {
            $scope.BankReconciliation.BankReconciliationHeadIID == $scope.BankReconciliation.BankReconciliationHeadIID;
        }
        else
            $scope.BankReconciliation.BankReconciliationHeadIID = 0;

        $scope.BankReconciliation.BankOpeningBalance = $scope.BankOpeningBalance;
        $scope.BankReconciliation.BankClosingBalance = $scope.BankClosingBalance;
        $scope.BankReconciliation.BankAccountID = $scope.Search.BankAccount.Key;
        $scope.BankReconciliation.BankName = $scope.BankName;
        $scope.BankReconciliation.FromDate = $scope.FromDate;
        $scope.BankReconciliation.ToDate = $scope.ToDate;
        $scope.BankReconciliation.BankStatementID = $scope.BankStatementID;
        $scope.BankReconciliation.ContentFileData = $scope.ContentFileData;
        $scope.BankReconciliation.ContentFileID = $scope.ContentFileID;
        $scope.BankReconciliation.ContentFileName = $scope.ContentFileName;
        $scope.BankReconciliation.LedgerOpeningBalance = $scope.LedgerOpeningBalance;
        $scope.BankReconciliation.LedgerClosingBalance = $scope.LedgerClosingBalance;
        $scope.BankReconciliation.BankReconciliationManualEntry = $scope.BankReconciliationManualEntry;
        $scope.BankReconciliation.BankReconciliationUnMatchedWithLedgerEntries = $scope.BankReconciliationUnMatchedWithLedgerEntries;
        $scope.BankReconciliation.BankReconciliationUnMatchedWithBankEntries = $scope.BankReconciliationUnMatchedWithBankEntries;

        showSpinner();

        var url = utility.myHost + "Documents/DocManagement/SaveBankReconciliationEntry";
        $http({
            method: 'Post',
            url: url,
            data: $scope.BankReconciliation
        }).then(function (result) {
            if (result.data.IsError) {
                $().showGlobalMessage($root, $timeout, true, result.data.Response);
            }
            else {
                $().showGlobalMessage($root, $timeout, false, result.data.Response);

                $timeout(function () {
                    $scope.$apply(function () {

                        //$scope.LoadEntryGrid();
                    });
                }, 1000);
            }
            hideSpinner()
            return false;
        }, function () {
            hideSpinner()
        });
    };

    //$scope.ProcessFeed = function () {

    //    $scope.DataFeedModel.FeedFileName = $('.filename').val();

    //    var file = $scope.feedFile;
    //    var url = utility.myHost + "Documents/DocManagement/ProcessFeed";
    //    var uploadUrl = "Documents/DocManagement/ProcessFeed?ID=" + $scope.DataFeedModel.DataFeedID;
    //    var fd = new FormData();
    //    fd.append('file', file);

    //    $http({
    //        method: 'Post',
    //        url: uploadUrl,
    //        data: $scope.BankReconciliation
    //    }).then(function (result) {

    //        hideSpinner()
    //        return false;
    //    }, function () {
    //        hideSpinner()
    //    });

    //    $http.post(uploadUrl, fd, {
    //        transformRequest: angular.identity,
    //        headers: { 'Content-Type': undefined }
    //    })

    //        .then(function (result) {

    //            $scope.DataFeedModel.DataFeedID = result.data.ID;
    //            if (result.data.ID != null && result.data.ID != 0) {
    //                $scope.SaveOpeningBankStatement(result.data.ID);
    //            }
    //        })
    //        .error(function () {
    //        });
    //}
    $scope.ProcessFeed = function () {
        $scope.DataFeedModel.FeedFileName = $('.filename').val();
        var file = $scope.feedFile;
        var uploadUrl = "Documents/DocManagement/ProcessFeed?ID=" + $scope.DataFeedModel.DataFeedID;
        var fd = new FormData();
        fd.append('file', file);

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
            .then(function (result) {
                $scope.DataFeedModel.DataFeedID = result.data.ID;
                if (result.data.ID != null && result.data.ID != 0) {
                    $scope.SaveOpeningBankStatement(result.data.ID);
                }
            })
            .catch(function (error) {
                // Handle the error here
                console.error('Error uploading the file:', error);
            });
    };
    $scope.SaveOpeningBankStatement = function (id) {
        $scope.DataFeedModel.FeedFileName = $('.filename').val();
        var file = $scope.feedFile;
        var uploadUrl = "Documents/DocManagement/SaveBankOpeningDetails?ID=" + id +
            "&LedgerClosingBalInput=" + $scope.LedgerClosingBalInput +
            "&BankClosingBalInput=" + $scope.BankClosingBalInput + "&BankAccountID=" + $scope.Search.BankAccount?.Key + "&FromDate=" + $scope.FromDateString + "&ToDate=" + $scope.ToDateString + "";
        var fd = new FormData();
        fd.append('file', file);

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
            .then(function (result) {
                $scope.DataFeedModel.BankReconciliationID = 0;
                $scope.DataFeedModel.BankReconciliationIID = result.data.ID;

            })
            .catch(function (error) {
                // Handle the error here
                console.error('Error saving bank opening details:', error);
            });
    };
    $scope.calculateTotals = function () {
        let totalLedgerDebit = 0;
        let totalLedgerCredit = 0;
        let totalBankDebit = 0;
        let totalBankCredit = 0;
        let totalReconciliationDebit = 0;
        let totalReconciliationCredit = 0;

        // Calculate total for manual entries
        angular.forEach($scope.BankReconciliationManualEntry, function (entry) {
            totalLedgerDebit += (parseFloat(entry.LedgerDebitAmount) || 0);
            totalLedgerCredit += (parseFloat(entry.LedgerCreditAmount) || 0);
            totalBankDebit += (parseFloat(entry.BankDebitAmount) || 0);
            totalBankCredit += (parseFloat(entry.BankCreditAmount) || 0);
            entry.ReconciliationDebitAmount = 0;
            entry.ReconciliationCreditAmount = 0;
            if (parseFloat(entry.LedgerCreditAmount) > 0)

                entry.ReconciliationDebitAmount = parseFloat(entry.LedgerCreditAmount);
            if (parseFloat(entry.LedgerDebitAmount) > 0)
                entry.ReconciliationCreditAmount = (parseFloat(entry.LedgerDebitAmount) || 0);
            if (parseFloat(entry.BankCreditAmount) > 0)
                entry.ReconciliationDebitAmount = (parseFloat(entry.BankCreditAmount) || 0);
            if (parseFloat(entry.BankDebitAmount) > 0)
                entry.ReconciliationCreditAmount = (parseFloat(entry.BankDebitAmount) || 0);

            totalReconciliationDebit += (parseFloat(entry.ReconciliationDebitAmount) || 0);
            totalReconciliationCredit += (parseFloat(entry.ReconciliationCreditAmount) || 0);
        });

        // Add LedgerDebitAmount from UnMatchedWithLedgerEntries
        angular.forEach($scope.BankReconciliationUnMatchedWithLedgerEntries, function (entry) {
            totalLedgerDebit += (parseFloat(entry.LedgerDebitAmount) || 0);
            totalLedgerCredit += (parseFloat(entry.LedgerCreditAmount) || 0);
            totalBankDebit += (parseFloat(entry.BankDebitAmount) || 0);
            totalBankCredit += (parseFloat(entry.BankCreditAmount) || 0);
            totalReconciliationDebit += (parseFloat(entry.ReconciliationDebitAmount) || 0);
            totalReconciliationCredit += (parseFloat(entry.ReconciliationCreditAmount) || 0);
        });

        // Add LedgerDebitAmount from UnMatchedWithBankEntries
        angular.forEach($scope.BankReconciliationUnMatchedWithBankEntries, function (entry) {
            totalLedgerDebit += (parseFloat(entry.LedgerDebitAmount) || 0);
            totalLedgerCredit += (parseFloat(entry.LedgerCreditAmount) || 0);
            totalBankDebit += (parseFloat(entry.BankDebitAmount) || 0);
            totalBankCredit += (parseFloat(entry.BankCreditAmount) || 0);
            totalReconciliationDebit += (parseFloat(entry.ReconciliationDebitAmount) || 0);
            totalReconciliationCredit += (parseFloat(entry.ReconciliationCreditAmount) || 0);
        });


        $scope.TotalLedgerDebitAmount = totalLedgerDebit.toFixed(2);
        $scope.TotalLedgerCreditAmount = totalLedgerCredit.toFixed(2);
        $scope.TotalBankDebitAmount = totalBankDebit.toFixed(2);
        $scope.TotalBankCreditAmount = totalBankCredit.toFixed(2);
        $scope.TotalReconciliationDebitAmount = totalReconciliationDebit.toFixed(2);
        $scope.TotalReconciliationCreditAmount = totalReconciliationCredit.toFixed(2);
    };
    $scope.$watch('BankReconciliationManualEntry', function (newVal, oldVal) {
        $scope.calculateTotals();
    }, true);

    $scope.$watch('BankReconciliationUnMatchedWithLedgerEntries', function (newVal, oldVal) {
        $scope.calculateTotals();
    }, true);

    $scope.$watch('BankReconciliationUnMatchedWithBankEntries', function (newVal, oldVal) {
        $scope.calculateTotals();
    }, true);
    // Function to show the spinner
    function showSpinner() {
        document.getElementById('spinner').classList.remove('d-none');
        document.getElementById('spinner').classList.add('d-block');  // or d-flex depending on your layout
    }

    // Function to hide the spinner
    function hideSpinner() {
        document.getElementById('spinner').classList.add('d-none');
        document.getElementById('spinner').classList.remove('d-block');  // or d-flex
    }

}]);