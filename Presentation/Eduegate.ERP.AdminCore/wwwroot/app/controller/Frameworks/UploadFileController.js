app.controller("UploadFileController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    console.log("UploadFileController");
    $scope.uploadedPdfDocuments = [];
    $scope.uploadedExcelDocuments = [];
    $scope.BankReconciliationUnMatchedWithLedgerEntries = [];
    $scope.BankReconciliationUnMatchedWithBankEntries = [];
    $scope.BankReconciliationManualEntry = [];
    $scope.BankReconciliationManualEntryGrid = [];
    $scope.BankReconciliationBankTransGrid = [];
    $scope.DataFeedModel = {};
    $scope.DataFeedModel.BankReconciliationIID = 0;
    $scope.LedgerClosingBalInput = 0.0000;
    $scope.BankClosingBalInput = 0.0000;
    $scope.Search = [];
    $scope.Search.BankAccount = {};
    $scope.BankReconciliation = {};
    $scope.HeadIID = 0;
    $scope.BankReconciliation.BankReconciliationHeadIID = 0;
    $scope.BankReconciliationHeadIID = 0;
    $scope.GroupingTotalBank = 0;
    $scope.GroupingTotalLedger = 0;
    $scope.isUploadDialogVisible = false;
    $scope.UploadOpeningFileMessage = "";
    $scope.previousDateString = "";
    $scope.OpeningBankReconciliationIID = 0;
    $scope.BankReconciliationMatchingBankEntries = [];
    $scope.BankReconciliationStatusID = 1;
    $scope.Init = function (window, model, screen) {
        windowContainer = '#' + window;
        $scope.Screen = screen;

        $scope.InitializeDropZonePlugin();

        if (screen == "BankReconcilation") {
            $scope.Model = model;

            $http({
                method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=BankAccounts&defaultBlank=false'
            }).then(function (result) {
                $scope.BankAccounts = result.data;
            });

            if ($scope.Model != null && $scope.Model != undefined && $scope.Model.BankReconciliationHeadIID != null && $scope.Model.BankReconciliationHeadIID != 0) {
                $scope.FillEditData($scope.Model);
            }
        }
    }
    $scope.FillEditData = function (model) {
        $scope.DataFeedModel.BankReconciliationIID = model.BankReconciliationHeadIID;
        $scope.HeadIID = model.BankReconciliationHeadIID;
        $scope.BankReconciliation.BankReconciliationHeadIID = model.BankReconciliationHeadIID;
        $scope.Search.BankAccount.Key = model.BankAccountID;
        $scope.Search.BankAccount.Value = model.BankName;
        $scope.FromDateString = model.FromDateString;
        $scope.ToDateString = model.ToDateString;
        $scope.BankStatementID = model.BankStatementID;
        $scope.BankName = model.BankName;
        $scope.BankAccountID = model.BankAccountID;
        $scope.FromDate = model.FromDate;
        $scope.ToDate = model.ToDate;
        // $scope.ContentFileData = model.ContentFileData;
        $scope.BankReconciliationStatusID = model.BankReconciliationStatusID;
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
        $scope.BankReconciliationMatchingLedgerEntries = model.BankReconciliationMatchingLedgerEntries;
        $scope.BankReconciliationMatchingBankEntries = model.BankReconciliationMatchingBankTransEntries;

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

    $scope.DownloadBankTransCSV = function () {

        if ($scope.BankReconciliationBankTrans == null || $scope.BankReconciliationBankTrans.length < 0) {
            $().showGlobalMessage($root, $timeout, true, "Upload and save Bank Statement!");
            return false;
        }
        const selectedDate = $scope.FromDateString || 'UnknownDate';
        const fileName = `BankReconciliationBankTrans_${selectedDate}.csv`;
        const csvData = convertToCSV($scope.BankReconciliationBankTrans);
        const blob = new Blob([csvData], { type: 'text/csv;charset=utf-8;' });
        const link = document.createElement('a');
        const url = URL.createObjectURL(blob);
        link.setAttribute('href', url);
        link.setAttribute('download', fileName);
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    };

    function convertToCSV(data) {

        const headers = [
            'PostDate',
            'PartyName',
            'Reference',
            'ChequeNo',
            'ChequeDate',
            'Description',
            'Debit',
            'Credit',
            'Balance'
        ];

        const rows = data.map(item => [
            item.PostDate || '',
            item.PartyName || '',
            item.Reference || '',
            item.ChequeNo || '',
            item.ChequeDate || '',
            item.Narration || '',
            item.BankDebitAmount || '',
            item.BankCreditAmount || '',
            item.Balance || ''

        ].map(value => `"${value}"`));

        const csvContent = [headers.join(','), ...rows.map(row => row.join(','))].join('\n');
        return csvContent;
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
                        $scope.uploadedPdfDocuments = [];
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
                        $scope.uploadedExcelDocuments = [];
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
        $scope.uploadedPdfDocuments = [];
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
        $scope.uploadedExcelDocuments = [];
        xhr.open('POST', url, true)
        xhr.onreadystatechange = function (url) {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var result = JSON.parse(xhr.response)
                if (result.Success == true && result.FileInfo.length > 0) {
                    $.each(result.FileInfo, function (index, item) {
                        item.ReferenceID = item.ContentFileIID;
                        $scope.uploadedExcelDocuments.push(item)
                    });
                }
            }
        }
        xhr.send(fd);
    }
    $scope.ViewReport = function (id) {

        if (id == undefined || id == null || id == 0) {
            $().showGlobalMessage($root, $timeout, true, "please save uploaded file first");
            return false;
        }

        var reportName = "BankReconciliationReport";
        var reportHeader = "Bank Reconciliation Report";

        if ($scope.ShowWindow(reportName, reportHeader, reportName))
            return;

        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        var reportingService = $root.ReportingService;

        if (reportingService == "ssrs") {

            //var parameter = "BudgetID=" + row.Budget.Key + '&AsOnDate=' + Row.DateToday)"
            var parameter = "BankReconciliationHeadIID=" + id;

            var reportUrl = "";

            $.ajax({
                url: utility.myHost + "Reports/Report/GetReportUrlandParameters?reportName=" + reportName,
                type: 'GET',
                success: function (result) {
                    if (result.Response) {
                        reportUrl = result.Response + "&" + parameter;
                        var loadContent = "<center><div class='bounce-wrapper' id='Load'><div class='three-bounce-inner'><div class='bounce-item bi-item1'></div><div class='bounce-item bi-item2'></div><div class='bounce-item bi-item3'></div></div></div></center></div>";
                        $('#' + windowName).html(loadContent + '<iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
                        var $iFrame = $('iframe[reportname=' + reportName + ']');
                        $iFrame.ready(function () {
                            setTimeout(function () {
                                $("#Load", $('#' + windowName)).hide();
                            }, 1000);
                        });
                    }
                }
            });
            //SSRS report viewer end
        }
        else if (reportingService == 'bold') {

            //Bold reports viewer start
            var url = utility.myHost + 'ReportViewer/Index?reportName=' + reportName;
            $http({ method: 'Get', url })
                .then((result) => {
                    $('#' + reportName, '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                    $scope.ShowWindow(reportName, reportHeader, reportName);
                });
            //Bold reports viewer end
        }
        else {
            //New Report viewer start
            var parameterObject = {
                "BankReconciliationHeadIID": id
            };

            // Convert to JSON format
            let parameterString = JSON.stringify(parameterObject);

            var reportUrl = utility.myHost + 'Reports/ReportView/ViewReports?reportName=' + reportName + "&parameter=" + parameterString;
            $.ajax({
                url: reportUrl,
                type: 'GET',
                success: function (result) {
                    $('#' + windowName, '#LayoutContentSection').replaceWith($compile(result)($scope))
                }
            });
            //New Report viewer end
        }
    };

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
    $scope.GroupingTotalCreditBank = 0;
    $scope.GroupingTotalDebitBank = 0;
    $scope.GroupingTotalCreditLedger = 0;
    $scope.GroupingTotalDebitLedger = 0;
    $scope.UpdateSelectedIndex = function (index, isSelected, type) {
        var ltotal = 0;
        if (type == 'B') {
            $scope.GroupingTotalBank = 0;
            $scope.GroupingTotalCreditBank = 0;
            $scope.GroupingTotalDebitBank = 0;

            angular.forEach($scope.BankReconciliationMatchingBankEntries, function (entry) {
                if (entry.IsMatching == true) {
                    $scope.GroupingTotalBank = $scope.GroupingTotalBank + (entry.BankDebitAmount - entry.BankCreditAmount);
                    $scope.GroupingTotalCreditBank = $scope.GroupingTotalCreditBank + entry.BankCreditAmount;
                    $scope.GroupingTotalDebitBank = $scope.GroupingTotalDebitBank + entry.BankDebitAmount;
                }
            });
            $scope.GroupingTotalBank = parseFloat($scope.GroupingTotalBank.toFixed(2));
            $scope.GroupingTotalCreditBank = parseFloat($scope.GroupingTotalCreditBank.toFixed(2));
            $scope.GroupingTotalDebitBank = parseFloat($scope.GroupingTotalDebitBank.toFixed(2));
        }
        else {
            $scope.GroupingTotalLedger = 0;
            $scope.GroupingTotalCreditLedger = 0;
            $scope.GroupingTotalDebitLedger = 0;
            angular.forEach($scope.BankReconciliationMatchingLedgerEntries, function (entry) {
                if (entry.IsMatching == true) {
                    $scope.GroupingTotalLedger = $scope.GroupingTotalLedger + (entry.LedgerDebitAmount - entry.LedgerCreditAmount);
                    $scope.GroupingTotalCreditLedger = $scope.GroupingTotalCreditLedger + entry.LedgerCreditAmount;
                    $scope.GroupingTotalDebitLedger = $scope.GroupingTotalDebitLedger + entry.LedgerDebitAmount;
                }
            });
            $scope.GroupingTotalLedger = parseFloat($scope.GroupingTotalLedger.toFixed(2));
            $scope.GroupingTotalCreditLedger = parseFloat($scope.GroupingTotalCreditLedger.toFixed(2));
            $scope.GroupingTotalDebitLedger = parseFloat($scope.GroupingTotalDebitLedger.toFixed(2));
        }

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
                    $scope.selectedBankEntryIndex = null;
                }
            }
            else {
                var idx = $scope.selectedLedgerIndices.indexOf(index);
                if (idx > -1) {
                    $scope.selectedLedgerIndices.splice(idx, 1);
                    $scope.selectedLedgerIndex = null;
                }
            }
        }

    };
    $scope.getMatchedEntriesCount = function () {
        return $scope.BankReconciliationMatchingBankEntries.filter(entry => entry.IsMoved === true).length;
    };
    $scope.getUnMatchedEntriesCount = function () {
        return $scope.BankReconciliationMatchingBankEntries.filter(entry => entry.IsMoved !== true).length;
    };
    $scope.getRemainingMatchedEntriesCount = function () {
        return $scope.BankReconciliationMatchingBankEntries.filter(entry => entry.IsMoved !== true).length;
    };
    $scope.MovetoMatching = function () {
        var selectedBankEntries = [];
        var selectedLedgerEntries = [];
        var MatchedEntries = [];
        ledgerSelctedCount = $scope.BankReconciliationMatchingLedgerEntries.filter(entry => entry.IsMatching === true).length;
        bankSelctedCount = $scope.BankReconciliationMatchingBankEntries.filter(entry => entry.IsMatching === true).length;

        if ($scope.GroupingTotalBank != 0 && $scope.GroupingTotalLedger != 0 && $scope.GroupingTotalBank == -1 * $scope.GroupingTotalLedger) {

        angular.forEach($scope.BankReconciliationMatchingLedgerEntries, function (trans) {
            if (trans.IsMatching) {
                selectedLedgerEntries.push(trans);
            }
        });

        angular.forEach($scope.BankReconciliationMatchingBankEntries, function (trans) {
            if (trans.IsMatching) {
                selectedBankEntries.push(trans);
            }
        });
        if (ledgerSelctedCount > 1 || bankSelctedCount > 1) {
            var referenceGroupNo = new Date().getTime().toString();
            var referenceGroup = "RefGroup_"; 
            var referenceName = referenceGroup + referenceGroupNo;
            
            var combinedEntry = {
                AccountID: null,
                ReferenceGroupNo: referenceGroupNo,
                Particulars: referenceName ,
                BankDescription: referenceName,
                PostDate: "-",
                TransDate: "-",
                ChequeNo: "-",
                ChequeDate: "-",
                PartName: "-",
                Reference: "-",
                LedgerDebitAmount: $scope.GroupingTotalDebitLedger || 0,
                LedgerCreditAmount: $scope.GroupingTotalCreditLedger || 0,
                BankDebitAmount: $scope.GroupingTotalDebitBank || 0,
                BankCreditAmount: $scope.GroupingTotalCreditBanK || 0,
                BankStatementEntryID: null,
                MatchedBankEntries: [],
                MatchedLedgerEntries: []
            };
            if (selectedBankEntries && selectedBankEntries.length > 0) {
                combinedEntry.MatchedBankEntries = selectedBankEntries.map(matched => ({

                    BankDescription: matched.Narration,
                    ChequeNo: matched.ChequeNo ?? '-',
                    ChequeDate: matched.ChequeDate ?? '-',
                    PostDate: matched.PostDate,
                    PartyName: matched.PartyName ?? '-',
                    Reference: matched.Reference ?? '-',
                    BankCreditAmount: (typeof matched.BankCreditAmount === 'undefined') ? 0 : matched.BankCreditAmount,
                    BankDebitAmount: (typeof matched.BankDebitAmount === 'undefined') ? 0 : matched.BankDebitAmount,
                    BankStatementEntryID: (typeof matched.BankStatementEntryID === 'undefined') ? 0 : matched.BankStatementEntryID,
                    ReferenceGroupNo: referenceGroupNo,
                    ReferenceGroupName: referenceName
                }));
            }

            // Add matched ledger entries 
            if (selectedLedgerEntries && selectedLedgerEntries.length > 0) {
                combinedEntry.MatchedLedgerEntries = selectedLedgerEntries.map(matched => ({
                    AccountID: (typeof matched.AccountID === 'undefined') ? 0 : matched.AccountID,
                    Particulars: matched.Narration,
                    ChequeNo: matched.ChequeNo ?? '-',
                    LedgerCreditAmount: (typeof matched.LedgerCreditAmount === 'undefined') ? 0 : matched.LedgerCreditAmount,
                    LedgerDebitAmount: (typeof matched.LedgerDebitAmount === 'undefined') ? 0 : matched.LedgerDebitAmount,
                    TransDate: matched.PostDate,
                    ChequeDate: matched.ChequeDate ?? '-',
                    TranHeadID: matched.TranHeadID,
                    TranTailID: matched.TranTailID,
                    PartyName: matched.PartyName ?? '-',
                    Reference: matched.Reference ?? '-',
                    ReferenceGroupNo: referenceGroupNo,
                    ReferenceGroupName: referenceName

                }));
            }


            //console.log('combinedEntry:', combinedEntry);
            //console.log('combinedEntry.MatchedBankEntries:', combinedEntry.MatchedBankEntries);
            //console.log('combinedEntry.MatchedLedgerEntries :', combinedEntry.MatchedLedgerEntries);
            $scope.BankReconciliationMatchedEntries.push(combinedEntry);
            $scope.MatchedBankEntries = combinedEntry.MatchedBankEntries;
            $scope.MatchedLedgerEntries = combinedEntry.MatchedLedgerEntries;
            $scope.SaveBankReconciliationMatchedEntry(combinedEntry);
            MatchedEntries.push(combinedEntry);
        }
        else {
            if (selectedBankEntries.length != 0 && selectedLedgerEntries.length != 0) {
                selectedLedgerEntries.forEach(function (ledgerEntry, index) {
                    let bankEntry = selectedBankEntries[index];
                    var combinedEntry = {
                        AccountID: ledgerEntry.AccountID,
                        Particulars: ledgerEntry.Narration,
                        BankDescription: bankEntry.Narration,
                        PostDate: bankEntry.PostDate,
                        TransDate: ledgerEntry.PostDate,
                        ChequeNo: ledgerEntry.ChequeNo,
                        ChequeDate: ledgerEntry.ChequeDate,
                        PartName: ledgerEntry.PartName,
                        Reference: ledgerEntry.Reference,
                        LedgerDebitAmount: ledgerEntry.LedgerDebitAmount || 0,
                        LedgerCreditAmount: ledgerEntry.LedgerCreditAmount || 0,
                        BankDebitAmount: bankEntry.BankDebitAmount || 0,
                        BankCreditAmount: bankEntry.BankCreditAmount || 0,
                        BankStatementEntryID: bankEntry.BankStatementEntryID || 0,
                        MatchedBankEntries: [],
                        MatchedLedgerEntries: []
                    };
                    if (selectedBankEntries && selectedBankEntries.length > 0) {
                        combinedEntry.MatchedBankEntries = selectedBankEntries.map(matched => ({
                            BankDescription: matched.Narration,
                            ChequeNo: matched.ChequeNo,
                            ChequeDate: matched.ChequeDate,
                            PostDate: matched.PostDate,
                            PartyName: matched.PartyName,
                            Reference: matched.Reference,
                            BankCreditAmount: matched.BankCreditAmount || 0,
                            BankDebitAmount: matched.BankDebitAmount || 0,
                            BankStatementEntryID: matched.BankStatementEntryID,
                            ReferenceGroupNo: null,
                            ReferenceGroupName: null
                        }));
                    }

                    // Add matched ledger entries 
                    if (selectedLedgerEntries && selectedLedgerEntries.length > 0) {
                        combinedEntry.MatchedLedgerEntries = selectedLedgerEntries.map(matched => ({
                            AccountID: matched.AccountID,
                            Particulars: matched.Narration,
                            ChequeNo: matched.ChequeNo,
                            LedgerCreditAmount: matched.LedgerCreditAmount,
                            LedgerDebitAmount: matched.LedgerDebitAmount,
                            TransDate: matched.PostDate,
                            ChequeDate: matched.ChequeDate,
                            TranHeadID: matched.TranHeadID,
                            TranTailID: matched.TranTailID,
                            PartyName: matched.PartyName,
                            Reference: matched.Reference,
                            ReferenceGroupNo: null,
                            ReferenceGroupName: null

                        }));
                    }
                    $scope.BankReconciliationMatchedEntries.push(combinedEntry);
                    MatchedEntries.push(combinedEntry);
                });
                $scope.SaveBankReconciliationMatchedEntry(MatchedEntries);
            }
        }
        angular.forEach($scope.BankReconciliationMatchingLedgerEntries, function (entry) {
            if (entry.IsMatching == true) {
                entry.ReferenceGroupNo = referenceGroupNo;
                entry.IsMatching = false;
                entry.IsMoved = true;
            }
        });

        angular.forEach($scope.BankReconciliationMatchingBankEntries, function (entry) {
            if (entry.IsMatching == true) {
                entry.ReferenceGroupNo = referenceGroupNo;
                entry.IsMatching = false;
                entry.IsMoved = true;
            }
        });

        angular.forEach(selectedLedgerEntries, function (ledgerEntry) {
            let indexToRemove = -1;

            angular.forEach($scope.BankReconciliationUnMatchedWithBankEntries, function (matchedEntry, index) {
                //if (matchedEntry.Particulars === ledgerEntry.Narration && matchedEntry.TransDate === ledgerEntry.PostDate && ledgerEntry.LedgerDebitAmount == matchedEntry.LedgerDebitAmount && ledgerEntry.LedgerCreditAmount == matchedEntry.LedgerCreditAmount) {
                //matchedEntry.AccountID == ledgerEntry.AccountID &&
                if (matchedEntry.Particulars == ledgerEntry.Narration && ledgerEntry.LedgerDebitAmount == matchedEntry.LedgerDebitAmount && ledgerEntry.LedgerCreditAmount == matchedEntry.LedgerCreditAmount) {

                    indexToRemove = index;
                    return;
                }
            });

            if (indexToRemove !== -1) {
                $scope.BankReconciliationUnMatchedWithBankEntries.splice(indexToRemove, 1);
                // console.log('after removal:', $scope.BankReconciliationUnMatchedWithBankEntries);
            }
        });


        angular.forEach(selectedBankEntries, function (bankEntry) {
            let indexToRemove = -1;
            //matchedEntry.BankStatementEntryID == bankEntry.BankStatementEntryID && 
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
        }
        else {
            alert("Please select entries from both tables with equal amount");
            return false;
        }

        $scope.GroupingTotalBank = 0;
        $scope.GroupingTotalLedger = 0;

    };

    $scope.RemoveReconciliationEntry = function (ledgerEntry, bankEntry) {


        let indexToRemove = -1;
        angular.forEach($scope.BankReconciliationUnMatchedWithBankEntries, function (matchedEntry, index) {

            if (matchedEntry.Particulars == ledgerEntry.Narration && ledgerEntry.LedgerDebitAmount == matchedEntry.LedgerDebitAmount && ledgerEntry.LedgerCreditAmount == matchedEntry.LedgerCreditAmount) {

                indexToRemove = index;
                return;
            }
        });

        if (indexToRemove !== -1) {
            $scope.BankReconciliationUnMatchedWithBankEntries.splice(indexToRemove, 1);
        }

        let indexToRemove1 = -1;
        angular.forEach($scope.BankReconciliationUnMatchedWithLedgerEntries, function (matchedEntry, index) {
            if (matchedEntry.Particulars == bankEntry.Narration && bankEntry.BankCreditAmount == matchedEntry.BankCreditAmount && bankEntry.BankDebitAmount == matchedEntry.BankDebitAmount) {

                indexToRemove1 = index;
                return;
            }
        });

        if (indexToRemove !== -1) {
            $scope.BankReconciliationUnMatchedWithLedgerEntries.splice(indexToRemove, 1);
        }
    }
    $scope.AddReconciliationEntry = function (ledgerEntry, bankEntry) {
        if (ledgerEntry) {


            let EntryLedger = {
                Particulars: ledgerEntry.Narration,
                BankDescription: "-",
                PostDate: "-",
                TransDate: ledgerEntry.PostDate,
                ChequeNo: ledgerEntry.ChequeNo,
                ChequeDate: ledgerEntry.ChequeDate,
                PartName: ledgerEntry.PartName,
                Reference: ledgerEntry.Reference,
                LedgerDebitAmount: ledgerEntry.LedgerDebitAmount,
                LedgerCreditAmount: ledgerEntry.LedgerCreditAmount,
                BankDebitAmount: 0,
                BankCreditAmount: 0,
                //TranHeadID = ledgerEntry.TranHeadID,
                //TranTailID = ledgerEntry.TranTailID, 
            };
            $scope.BankReconciliationUnMatchedWithBankEntries.push(EntryLedger);
            ledgerEntry.IsMoved = false;
            ledgerEntry.IsMatching = false;


        }
        if (bankEntry) {

            let EntryBank = {
                Particulars: bankEntry.Narration,
                PostDate: bankEntry.PostDate,
                TransDate: "-",
                ChequeNo: bankEntry.ChequeNo,
                ChequeDate: bankEntry.ChequeDate,
                PartName: bankEntry.PartName,
                Reference: bankEntry.Reference,
                LedgerDebitAmount: 0,
                LedgerCreditAmount: 0,
                BankDebitAmount: bankEntry.BankDebitAmount,
                BankCreditAmount: bankEntry.BankCreditAmount,
                BankStatementEntryID: bankEntry.BankStatementEntryID
            };
            $scope.BankReconciliationUnMatchedWithLedgerEntries.push(EntryBank);
            bankEntry.IsMoved = false;
            bankEntry.IsMatching = false;
        }


    }
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

            if (ledgerSelected.length > 0) {
                ledgerSelected.forEach(function (ledgerEntry, index) {

                    let EntryLedger = {
                        Particulars: ledgerEntry.Narration,
                        BankDescription: "-",
                        PostDate: "-",
                        TransDate: ledgerEntry.PostDate,
                        ChequeNo: ledgerEntry.ChequeNo,
                        ChequeDate: ledgerEntry.ChequeDate,
                        PartName: ledgerEntry.PartName,
                        Reference: ledgerEntry.Reference,
                        LedgerDebitAmount: ledgerEntry.LedgerDebitAmount,
                        LedgerCreditAmount: ledgerEntry.LedgerCreditAmount,
                        BankDebitAmount: 0,
                        BankCreditAmount: 0,
                        //TranHeadID = ledgerEntry.TranHeadID,
                        //TranTailID = ledgerEntry.TranTailID, 
                    };
                    $scope.BankReconciliationUnMatchedWithBankEntries.push(EntryLedger);

                });
                ledgerSelected.forEach(function (entry) {
                    entry.IsMoved = false;
                    entry.IsMatching = false;
                    //entry.IsUndoMatching = false;
                });
            }
            if (bankSelected.length > 0) {
                bankSelected.forEach(function (bankEntry, index) {

                    let EntryBank = {
                        Particulars: bankEntry.Narration,
                        PostDate: bankEntry.PostDate,
                        TransDate: "-",
                        ChequeNo: bankEntry.ChequeNo,
                        ChequeDate: bankEntry.ChequeDate,
                        PartName: bankEntry.PartName,
                        Reference: bankEntry.Reference,
                        LedgerDebitAmount: 0,
                        LedgerCreditAmount: 0,
                        BankDebitAmount: bankEntry.BankDebitAmount,
                        BankCreditAmount: bankEntry.BankCreditAmount,
                        BankStatementEntryID: bankEntry.BankStatementEntryID
                    };
                    $scope.BankReconciliationUnMatchedWithLedgerEntries.push(EntryBank);
                });
            }

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
            if (matchedEntry.MatchedLedgerEntries != undefined && matchedEntry.MatchedLedgerEntries.length > 0) {
                angular.forEach(matchedEntry.MatchedLedgerEntries, function (lEntry) {


                    if (ledgerEntry.Narration === lEntry.Particulars) {
                        ledgerEntry.IsMoved = false;
                        ledgerEntry.IsMatching = false;
                        let EntryLedger = {
                            Particulars: ledgerEntry.Narration,
                            BankDescription: "-",
                            PostDate: "-",
                            TransDate: ledgerEntry.PostDate,
                            ChequeNo: ledgerEntry.ChequeNo,
                            ChequeDate: ledgerEntry.ChequeDate,
                            PartName: ledgerEntry.PartName,
                            Reference: ledgerEntry.Reference,
                            LedgerDebitAmount: ledgerEntry.LedgerDebitAmount,
                            LedgerCreditAmount: ledgerEntry.LedgerCreditAmount,
                            BankDebitAmount: 0,
                            BankCreditAmount: 0,
                            ReconciliationDebitAmount: ledgerEntry.LedgerCreditAmount,
                            ReconciliationCreditAmount: ledgerEntry.LedgerDebitAmount,
                            AccountID: ledgerEntry.AccountID,
                            BankStatementEntryID: null
                        };
                        $scope.BankReconciliationUnMatchedWithBankEntries.push(EntryLedger);
                    }

                });
                return true;
            }
            else {
                if (ledgerEntry.Narration === matchedEntry.Particulars) {
                    ledgerEntry.IsMoved = false;
                    ledgerEntry.IsMatching = false;

                    let EntryLedger = {
                        Particulars: ledgerEntry.Narration,
                        BankDescription: "-",
                        PostDate: "-",
                        TransDate: ledgerEntry.PostDate,
                        ChequeNo: ledgerEntry.ChequeNo,
                        ChequeDate: ledgerEntry.ChequeDate,
                        PartName: ledgerEntry.PartName,
                        Reference: ledgerEntry.Reference,
                        LedgerDebitAmount: ledgerEntry.LedgerDebitAmount,
                        LedgerCreditAmount: ledgerEntry.LedgerCreditAmount,
                        BankDebitAmount: 0,
                        BankCreditAmount: 0,
                        ReconciliationDebitAmount: ledgerEntry.LedgerCreditAmount,
                        ReconciliationCreditAmount: ledgerEntry.LedgerDebitAmount,
                        AccountID: ledgerEntry.AccountID,
                        BankStatementEntryID: null
                    };
                    $scope.BankReconciliationUnMatchedWithBankEntries.push(EntryLedger);
                }
            }
        });

        angular.forEach($scope.BankReconciliationMatchingBankEntries, function (bankEntry) {

            if (matchedEntry.MatchedBankEntries != undefined && matchedEntry.MatchedBankEntries.length > 0) {
                angular.forEach(matchedEntry.MatchedBankEntries, function (bEntry) {
                    if (bankEntry.Narration === bEntry.BankDescription && bankEntry.PostDate === bEntry.PostDate) {
                        bankEntry.IsMoved = false;
                        bankEntry.IsMatching = false;

                        let EntryBank = {
                            Particulars: bankEntry.Narration,
                            PostDate: bankEntry.PostDate,
                            TransDate: "-",
                            ChequeNo: bankEntry.ChequeNo,
                            ChequeDate: bankEntry.ChequeDate,
                            PartName: bankEntry.PartName,
                            Reference: bankEntry.Reference,
                            LedgerDebitAmount: 0,
                            LedgerCreditAmount: 0,
                            BankDebitAmount: bankEntry.BankDebitAmount,
                            BankCreditAmount: bankEntry.BankCreditAmount,
                            ReconciliationDebitAmount: bankEntry.BankCreditAmount,
                            ReconciliationCreditAmount: bankEntry.BankDebitAmount,
                            BankStatementEntryID: bankEntry.BankStatementEntryID
                        };
                        $scope.BankReconciliationUnMatchedWithLedgerEntries.push(EntryBank);

                    }
                });
                return false;
            }
            else {
                if (bankEntry.Narration === matchedEntry.BankDescription && bankEntry.PostDate === matchedEntry.PostDate) {
                    bankEntry.IsMoved = false;
                    bankEntry.IsMatching = false;

                    let EntryBank = {
                        Particulars: bankEntry.Narration,
                        PostDate: bankEntry.PostDate,
                        TransDate: "-",
                        ChequeNo: bankEntry.ChequeNo,
                        ChequeDate: bankEntry.ChequeDate,
                        PartName: bankEntry.PartName,
                        Reference: bankEntry.Reference,
                        LedgerDebitAmount: 0,
                        LedgerCreditAmount: 0,
                        BankDebitAmount: bankEntry.BankDebitAmount,
                        BankCreditAmount: bankEntry.BankCreditAmount,
                        ReconciliationDebitAmount: bankEntry.BankCreditAmount,
                        ReconciliationCreditAmount: bankEntry.BankDebitAmount,
                        BankStatementEntryID: bankEntry.BankStatementEntryID
                    };
                    $scope.BankReconciliationUnMatchedWithLedgerEntries.push(EntryBank);

                }
            }
        });
    };
    $scope.InsertRow = function (index, bankStatementID) {
        var model = $scope.BankReconciliationBankTrans;
        var defaultRow = {
            PostDate: null,
            BankCreditAmount: 0,
            BankDebitAmount: 0,
            Balance: 0,
            ChequeNo: "-",
            PartyName: "-",
            Reference: "-",
            ChequeDate: "-",
            Narration: "-",
            BankStatementID: $scope.BankStatementID,
            BankStatementEntryID: null,
            SlNO: 0

        };

        var row = ($scope.BankReconciliationBankTransGrid && $scope.BankReconciliationBankTransGrid[0])
            ? angular.copy($scope.BankReconciliationBankTransGrid[0])
            : angular.copy(defaultRow);

        model.splice(index + 1, 0, row);

        model.forEach((item, idx) => {
            item.SlNO = idx + 1;
        });
    };

    $scope.validateAmount = function (amount) {
       let val = amount;

        if (!val || isNaN(val)) {
            amount = 0;
        } else {
           
            let parsed = parseFloat(val);
            amount = parsed.toFixed(2);
        }

        return amount;
    };

    $scope.RemoveRow = function (index) {

        if ($scope.BankReconciliationBankTrans.length == 1) {
            if (index == 1) {
                $scope.LoadEntryGrid();
            }
        }
        else {

            var model = $scope.BankReconciliationBankTrans;
            var row = $scope.BankReconciliationBankTransGrid[0];

            model.splice(index, 1);

            if (index === 0) {
                $scope.InsertRow(index, row, model);
            }
        }
    };

    $scope.LoadEntryGrid = function () {

        $scope.BankReconciliationManualEntry = [];

        $scope.BankReconciliationBankTransGrid = [];

        $scope.BankReconciliationManualEntry.push(JSON.parse(JSON.stringify($scope.BankReconciliationOldManualEntry)));

        $scope.BankReconciliationBankTransGrid = JSON.parse(JSON.stringify($scope.BankReconciliationManualEntry));
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
    $scope.SaveBankStatementEntry = function (index) {

        let bankStatementEntry = $scope.BankReconciliationBankTrans[index];
        if (!bankStatementEntry.Narration) {
            $().showGlobalMessage($root, $timeout, true, "Narration cannot be left empty!");
            return false;
        }
        if (!bankStatementEntry.PostDate) {
            $().showGlobalMessage($root, $timeout, true, "Please select a Post Date!");
            return false;
        }
        if (!(bankStatementEntry.BankDebitAmount != undefined && bankStatementEntry.BankDebitAmount !== 0
            || bankStatementEntry.BankCreditAmount != undefined && bankStatementEntry.BankCreditAmount !== 0)) {
            $().showGlobalMessage($root, $timeout, true, "Please enter Debit or Credit!");
            return false;
        }

        showSpinner();
        var data = { data: bankStatementEntry }
        $.ajax({
            url: 'Documents/DocManagement/SaveBankStatementEntry',
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (result) {
                $().showGlobalMessage($root, $timeout, false, 'saved successfully!');
                if (result.ID != null && result.ID != 0) {
                    $scope.BankReconciliationBankTrans[index].BankStatementEntryID = result.ID;
                    $scope.AutoMatchingBankEntry($scope.BankReconciliationBankTrans[index]);
                }
            }

        })
        hideSpinner();
    }
    $scope.AutoMatchingBankEntry = function (bankEntry) {
        var MatchedEntries = [];
        let matchedLedger = $scope.BankReconciliationMatchingLedgerEntries.find(ledger =>
            bankEntry.BankCreditAmount == ledger.LedgerDebitAmount &&
            bankEntry.BankDebitAmount == ledger.LedgerCreditAmount &&
            (
                (ledger.PartyName && bankEntry.Narration.includes(ledger.PartyName)) ||
                (ledger.Reference && bankEntry.Narration.includes(ledger.Reference)) ||
                (ledger.ChequeNo && bankEntry.Narration.includes(ledger.ChequeNo)) ||
                (ledger.Narration && bankEntry.Narration.includes(ledger.Narration)) ||
                (ledger.Narration && ledger.Narration.includes(bankEntry.Narration))
            )
        );

        if (matchedLedger) {
            if (matchedLedger && bankEntry) {

                let combinedEntry = {
                    AccountID: matchedLedger.AccountID,
                    Particulars: matchedLedger.Narration,
                    BankDescription: bankEntry.Narration,
                    PostDate: bankEntry.PostDate,
                    TransDate: matchedLedger.PostDate,
                    ChequeNo: matchedLedger.ChequeNo,
                    ChequeDate: matchedLedger.ChequeDate,
                    PartName: matchedLedger.PartName,
                    Reference: matchedLedger.Reference,
                    LedgerDebitAmount: matchedLedger.LedgerDebitAmount,
                    LedgerCreditAmount: matchedLedger.LedgerCreditAmount,
                    BankDebitAmount: bankEntry.BankDebitAmount,
                    BankCreditAmount: bankEntry.BankCreditAmount,
                    BankStatementEntryID: bankEntry.BankStatementEntryID,
                    MatchedBankEntries: [],
                    MatchedLedgerEntries: []
                };
                if (bankEntry) {
                    combinedEntry.MatchedBankEntries = {
                        PostDate: bankEntry.PostDate,
                        BankDescription: bankEntry.Narration,
                        PartyName: bankEntry.PartyName,
                        Reference: bankEntry.Reference,
                        ChequeDate: bankEntry.ChequeDate,
                        ChequeNo: bankEntry.ChequeNo,
                        AccountID: bankEntry.AccountID,
                        BankDebitAmount: bankEntry.BankDebitAmount,
                        BankCreditAmount: bankEntry.BankCreditAmount,
                        BankStatementEntryID: bankEntry.BankStatementEntryID
                    };
                }

                // Add matched ledger entries 
                if (matchedLedger) {
                    combinedEntry.MatchedLedgerEntries = {
                        TransDate: matchedLedger.PostDate,
                        Particulars: matchedLedger.Narration,
                        PartyName: matchedLedger.PartyName,
                        Reference: matchedLedger.Reference,
                        ChequeDate: matchedLedger.ChequeDate,
                        ChequeNo: matchedLedger.ChequeNo,
                        LedgerDebitAmount: matchedLedger.LedgerDebitAmount,
                        LedgerCreditAmount: matchedLedger.LedgerCreditAmount,
                        AccountID: matchedLedger.AccountID,
                        BankStatementEntryID: matchedLedger.BankStatementEntryID,
                        TranHeadID: matchedLedger.TranHeadID,
                        TranTailID: matchedLedger.TranTailID
                    };
                }

                $scope.BankReconciliationMatchedEntries.push(combinedEntry);
                MatchedEntries.push(combinedEntry);

                let bEntry = {
                    AccountID: 0,
                    Narration: bankEntry.Narration,
                    IsMatching: false,
                    IsMoved: true,
                    SlNO: bankEntry.SlNO,
                    PostDate: bankEntry.PostDate,
                    ChequeNo: bankEntry.ChequeNo,
                    ChequeDate: bankEntry.ChequeDate,
                    PartName: bankEntry.PartName,
                    Reference: bankEntry.Reference,
                    BankDebitAmount: bankEntry.BankDebitAmount,
                    BankCreditAmount: bankEntry.BankCreditAmount,
                    BankStatementEntryID: bankEntry.BankStatementEntryID,
                    BankStatementID: bankEntry.BankStatementID,
                    IsSelected: bankEntry.IsSelected,
                };

                $scope.BankReconciliationMatchingBankEntries.push(bEntry);
            }
            matchedLedger.IsMatching = false;
            matchedLedger.IsMoved = true;
            $scope.RemoveReconciliationEntry(matchedLedger, bankEntry);
            $scope.SaveBankReconciliationMatchedEntry(MatchedEntries);

        } else {

            let bEntry = {
                AccountID: 0,
                Narration: bankEntry.Narration,
                IsMatching: false,
                IsMoved: false,
                SlNO: bankEntry.SlNO,
                PostDate: bankEntry.PostDate,
                ChequeNo: bankEntry.ChequeNo,
                ChequeDate: bankEntry.ChequeDate,
                PartName: bankEntry.PartName,
                Reference: bankEntry.Reference,
                BankDebitAmount: bankEntry.BankDebitAmount,
                BankCreditAmount: bankEntry.BankCreditAmount,
                BankStatementEntryID: bankEntry.BankStatementEntryID,
                BankStatementID: bankEntry.BankStatementID,
                IsSelected: bankEntry.IsSelected,
            };

            $scope.BankReconciliationMatchingBankEntries.push(bEntry);
            $scope.AddReconciliationEntry(null, bEntry);

        }
        $scope.BankReconciliationMatchingBankEntries.sort((a, b) => {
            return moment(a.PostDate, "DD/MM/YYYY") - moment(b.PostDate, "DD/MM/YYYY");
        });


    }
    $scope.SaveUploadedPdfFiles = function ($event) {
        if (!$scope.Search.BankAccount.Key) {
            $().showGlobalMessage($root, $timeout, true, "Please select a Bank!");
            return false;
        }
        if (!$scope.FromDateString) {
            $().showGlobalMessage($root, $timeout, true, "Please select a From Date!");
            return false;
        }
        if (!$scope.ToDateString) {
            $().showGlobalMessage($root, $timeout, true, "Please select a To Date!");
            return false;
        }
        if (!$scope.uploadedPdfDocuments || $scope.uploadedPdfDocuments.length == 0) {
            $().showGlobalMessage($root, $timeout, true, "Please upload PDF Document!");
            return false;
        }
        showSpinner();
        var data = { files: $scope.uploadedPdfDocuments, BankAccountID: $scope.Search.BankAccount.Key, FromDate: $scope.FromDateString, ToDate: $scope.ToDateString }
        $.ajax({
            url: 'Documents/DocManagement/SaveUploadedPdfFiles',
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (result) {
                if (result.Success) {
                    $().showGlobalMessage($root, $timeout, false, 'File uploaded successfully!');
                    $scope.uploadedPdfDocuments = []
                    $scope.$apply(function () {
                        // $scope.BankTransactions = result.Data.BankReconciliationUnMatchedWithLedgerEntries;
                        $scope.BankReconciliation.BankReconciliationHeadIID = result.Data.BankReconciliationHeadIID;
                        $scope.HeadIID = result.Data.BankReconciliationHeadIID;
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
                        $scope.BankReconciliationMatchingLedgerEntries = result.Data.BankReconciliationMatchingLedgerEntries;
                        $scope.BankReconciliationMatchingBankEntries = result.Data.BankReconciliationMatchingBankTransEntries;
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

                    $('.dropzone')[0].dropzone.files.forEach(function (file) {
                        file.previewElement.remove()
                    })

                    $('.dropzone').removeClass('dz-started')
                    hideSpinner();
                }
                else {
                    $().showGlobalMessage($root, $timeout, true, result.Message);
                    hideSpinner()
                }
            },
            error: function (error) {
                $().showGlobalMessage($root, $timeout, true, "Something went wrong. Please try later!");
                hideSpinner()
            }
        })
    }

    $scope.customSearchBankFilter = function (BankReconciliationMatchingBankEntry) {
        if (!$scope.searchBankQuery) {
            return true; // If no search query, show all results
        }
        var query = $scope.searchBankQuery.toLowerCase();

        return (BankReconciliationMatchingBankEntry.Narration.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingBankEntry.ChequeNo.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingBankEntry.PartyName.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingBankEntry.Reference.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingBankEntry.BankDebitAmount.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingBankEntry.BankCreditAmount.Value.toLowerCase().indexOf(query) !== -1);
    };

    $scope.customSearchLedgerFilter = function (BankReconciliationMatchingLedgerEntry) {
        if (!$scope.searchQuery || !$scope.searchBankQuery) {
            return true;
        }
        var query = $scope.searchLedgerQuery.toLowerCase();
        return (BankReconciliationMatchingLedgerEntry.Narration.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingLedgerEntry.ChequeNo.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingLedgerEntry.PartyName.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingLedgerEntry.Reference.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingLedgerEntry.LedgerDebitAmount.Value.toLowerCase().indexOf(query) !== -1 ||
            BankReconciliationMatchingLedgerEntry.LedgerCreditAmount.Value.toLowerCase().indexOf(query) !== -1);
    };

    $scope.SaveUploadedExcelFile = function ($event) {

        if ($scope.Screen == "BankReconcilation") {
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
            if (!$scope.previousDateString) {
                $().showGlobalMessage($root, $timeout, true, "Please check as on date. As on date can not be empty!");
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
        }

        if (!$scope.uploadedExcelDocuments || $scope.uploadedExcelDocuments.length == 0) {
            $().showGlobalMessage($root, $timeout, true, "Please upload Excel Document!");
            return false;
        }

        showSpinner();
        var data = { files: $scope.uploadedExcelDocuments, screen: $scope.Screen }
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

    $scope.SaveBankReconciliationMatchedEntry = function (BankReconciliationMatchedEntries) {

        showSpinner();

        $scope.BankReconciliation = {};

        if ($scope.HeadIID != undefined && $scope.HeadIID != 0) {
            $scope.BankReconciliation.BankReconciliationHeadIID = $scope.HeadIID;
        }
        else
            $scope.BankReconciliation.BankReconciliationHeadIID = 0;

        if ($scope.DataFeedModel.BankReconciliationIID != undefined && $scope.DataFeedModel.BankReconciliationIID != 0 && !$scope.BankReconciliation.BankReconciliationHeadIID) {
            $scope.BankReconciliation.BankReconciliationHeadIID = $scope.DataFeedModel.BankReconciliationIID;
        }

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
        $scope.BankReconciliation.BankReconciliationMatchedEntries = $scope.BankReconciliationMatchedEntries;

        showSpinner();
        var url = utility.myHost + "Documents/DocManagement/SaveBankReconciliationMatchedEntry";
        $http({
            method: 'POST',
            url: url,
            data: JSON.stringify($scope.BankReconciliation),  // Directly passing the object
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            hideSpinner();
            console.log("Success", result);
        }, function (error) {
            hideSpinner();
            console.log("Error", error);
        });
        //var url = utility.myHost + "Documents/DocManagement/SaveBankReconciliationMatchedEntry";
        //$http({
        //    method: 'POST',
        //    url: url,
        //    data: {
        //        "BankReconciliationHeadIID": $scope.BankReconciliation.BankReconciliationHeadIID,
        //        "BankOpeningBalance": $scope.BankReconciliation.BankOpeningBalance,
        //        "BankClosingBalance": $scope.BankReconciliation.BankClosingBalance,
        //        "BankAccountID": $scope.BankReconciliation.BankAccountID,
        //        "BankName": $scope.BankReconciliation.BankName,
        //        "FromDate": $scope.BankReconciliation.FromDate,
        //        "ToDate": $scope.BankReconciliation.ToDate,
        //        "BankStatementID": $scope.BankReconciliation.BankStatementID,
        //        "ContentFileData": $scope.BankReconciliation.ContentFileData,
        //        "ContentFileID": $scope.BankReconciliation.ContentFileID,
        //        "ContentFileName": $scope.BankReconciliation.ContentFileName,
        //        "LedgerOpeningBalance": $scope.BankReconciliation.LedgerOpeningBalance,
        //        "LedgerClosingBalance": $scope.BankReconciliation.LedgerClosingBalance,
        //        "BankReconciliationManualEntry": $scope.BankReconciliation.BankReconciliationManualEntry,
        //        "BankReconciliationUnMatchedWithLedgerEntries": $scope.BankReconciliation.BankReconciliationUnMatchedWithLedgerEntries,
        //        "BankReconciliationUnMatchedWithBankEntries": $scope.BankReconciliation.BankReconciliationUnMatchedWithBankEntries,
        //        "BankReconciliationMatchedEntries": $scope.BankReconciliation.BankReconciliationMatchedEntries
        //    },
        //    headers: { 'Content-Type': 'application/json' }
        //}).then(function (result) {
        //    hideSpinner();
        //    console.log("Success", result);
        //}, function (error) {
        //    hideSpinner();
        //    console.log("Error", error);
        //});      
    };
    $scope.SaveBankReconciliationEntry = function (type) {
        if (!$scope.Search.BankAccount.Key) {
            $().showGlobalMessage($root, $timeout, true, "Please select a Bank!");
            return false;
        }

        if (type == "post") {
            if ($scope.BankReconciliationMatchedEntries.length > 0 && !($scope.BankReconciliationUnMatchedWithLedgerEntries.length == 0)) {
                $().showGlobalMessage($root, $timeout, true, "Posting is only allowed after all the entries have been matched!");
                return false;
            }
           

        }
        else {
            if (!($scope.BankReconciliationMatchedEntries.length > 0 || $scope.BankReconciliationUnMatchedWithLedgerEntries > 0 || $scope.BankReconciliationUnMatchedWithBankEntries > 0)) {
                $().showGlobalMessage($root, $timeout, true, " There are no records found for bank reconciliation!");
                return false;
            }
        }

        $scope.BankReconciliation = {};

        if ($scope.HeadIID != undefined && $scope.HeadIID != 0) {
            $scope.BankReconciliation.BankReconciliationHeadIID = $scope.HeadIID;
        }
        else
            $scope.BankReconciliation.BankReconciliationHeadIID = 0;

        if ($scope.DataFeedModel.BankReconciliationIID != undefined && $scope.DataFeedModel.BankReconciliationIID != 0 && !$scope.BankReconciliation.BankReconciliationHeadIID) {
            $scope.BankReconciliation.BankReconciliationHeadIID = $scope.DataFeedModel.BankReconciliationIID;
        }
        $scope.BankReconciliation.BankReconciliationStatusID = (type === "post") ? 2 : 1;
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
        $scope.BankReconciliation.BankReconciliationMatchedEntries = $scope.BankReconciliationMatchedEntries;
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

                $scope.BankReconciliation.BankReconciliationHeadIID = result.data.ID;
                $scope.HeadIID = result.data.ID;
                $scope.BankReconciliationHeadIID = result.data.ID;
                $scope.BankReconciliationStatusID = $scope.BankReconciliation.BankReconciliationStatusID;
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
                    if ($scope.Screen == "BankReconcilation") {
                        $scope.SaveOpeningBankStatement(result.data.ID);
                    }
                    else if ($scope.Screen == "AssetManualEntry") {
                        $scope.SaveAssetOpeningEntries(result.data.ID);
                    }
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
            "&BankClosingBalInput=" + $scope.BankClosingBalInput + "&BankAccountID=" + $scope.Search.BankAccount?.Key + "&FromDate=" + $scope.previousDateString + "&ToDate=" + $scope.previousDateString + "";
        var fd = new FormData();
        fd.append('file', file);

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
            .then(function (result) {
                // $scope.DataFeedModel.BankReconciliationID = 0;
                $scope.OpeningBankReconciliationIID = result.data.ID;
                $().showGlobalMessage($root, $timeout, false, "Uploaded successfully!");
            })
            .catch(function (error) {
                // Handle the error here
                console.error('Error saving bank opening details:', error);
                $().showGlobalMessage($root, $timeout, true, "Error in saving bank opening details!");
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
            if (entry.LedgerDebitAmount != undefined && entry.LedgerDebitAmount != null)
                totalLedgerDebit += (parseFloat(entry.LedgerDebitAmount ?? 0) || 0);
            if (entry.LedgerCreditAmount != undefined && entry.LedgerCreditAmount != null)
                totalLedgerCredit += (parseFloat(entry.LedgerCreditAmount ?? 0) || 0);
            if (entry.BankDebitAmount != undefined && entry.BankDebitAmount != null)
                totalBankDebit += (parseFloat(entry.BankDebitAmount ?? 0) || 0);
            if (entry.BankCreditAmount != undefined && entry.BankCreditAmount != null)
                totalBankCredit += (parseFloat(entry.BankCreditAmount ?? 0) || 0);
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

    //$scope.UploadOpening = function () {        
    //    if ($scope.FromDateString && $scope.ToDateString) {
    //        $scope.isUploadDialogVisible = true;
    //    } else {
    //        alert('Please select both From Date and To Date.');
    //    }
    //};
    $scope.UploadOpening = function () {
        if (!$scope.FromDateString) {
            $().showGlobalMessage($root, $timeout, true, "Please select a From Date!");
            return false;
        }

        if ($scope.FromDateString) {
            var dateParts = $scope.FromDateString.split('/');
            var day = parseInt(dateParts[0], 10);
            var month = parseInt(dateParts[1], 10) - 1;
            var year = parseInt(dateParts[2], 10);

            var fromDate = new Date(year, month, day);

            var previousDate = new Date(fromDate);
            previousDate.setDate(fromDate.getDate() - 1);

            $scope.previousDateString = ("0" + previousDate.getDate()).slice(-2) + '/' +
                ("0" + (previousDate.getMonth() + 1)).slice(-2) + '/' +
                previousDate.getFullYear();

            $scope.UploadOpeningFileMessage = "Please upload Opening Bank File as on " + $scope.previousDateString;

            $('#uploadModal').modal('show');

        } else {
            alert('Please select From Date');
        }
    };


    $scope.confirmUpload = function () {

        $scope.downloadFunction();

        $('#uploadModal').modal('hide');
    };

    $scope.downloadFunction = function () {

        console.log("Download initiated with dates:", $scope.FromDateString, $scope.ToDateString);
    };

    $scope.SaveAssetOpeningEntries = function (id) {
        $scope.DataFeedModel.FeedFileName = $('.filename').val();
        var file = $scope.feedFile;

        var uploadUrl = "Documents/DocManagement/SaveAssetOpeningEntryDetails?feedLogID=" + id;

        var fd = new FormData();
        fd.append('file', file);

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (result) {

            $scope.AssetOpeningID = result.data.ID;

            $().showGlobalMessage($root, $timeout, false, "Uploaded successfully!");

            $scope.$parent.RowModel.ReLoad();

        }).catch(function (error) {
            // Handle the error here
            console.error('Error asset opening details:', error);
            $().showGlobalMessage($root, $timeout, true, "Error in asset opening details!");
        });
    };

    $scope.DownloadExcelTemplate = function () {
        var url = "DataFeed/DataFeed/DownloadExcelTemplate?screen=" + $scope.Screen;
        var link = document.createElement("a");
        link.href = utility.myHost + url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };

}]);