app.controller("AIUploadFileController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    console.log("AIUploadFileController");

    $scope.IsExtractionLoading = false;
    $scope.LessonPlanChapterDTO = {};
    $scope.LessonPlanChapterDTO.SubjectUnits = [];
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


    //In-Progress
    $scope.toEntity = [];


    $scope.Init = function (window, model, screen) {
        windowContainer = '#' + window;
        $scope.Screen = screen;

        $scope.InitializeDropZonePlugin();
        $http({
            method: 'Get',
            url: "Mutual/GetDynamicLookUpData?lookType=ActiveAcademicYear&defaultBlank=false",
        }).then(function (result) {
            $scope.AcademicYears = result.data;
        });

        $http({
            method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=Classes&defaultBlank=false'
        }).then(function (result) {
            $scope.Classes = result.data;
        });


        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Section&defaultBlank=false",
        }).then(function (result) {
            $scope.Section = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Subject&defaultBlank=false",
        }).then(function (result) {
            $scope.Subjects = result.data;
        });


   
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
            $("#excelUploadDropzone").dropzone({
                previewsContainer: "#previews",
                addRemoveLinks: true,
                uploadMultiple: true,
                init: function () {
                    var dropzoneInstance = this;

                    dropzoneInstance.on("sending", function () {
                        $(".box__uploading").show();
                        $(".box__success, .box__error").hide();
                    });

                    dropzoneInstance.on("success", function (file, response) {
                        $scope.$apply(function () {
                            $scope.uploadedExcelDocuments = [];
                            $.each(response.FileInfo, function (index, item) {
                                item.FilePath = item.FilePath + "?" + Math.random();
                                $scope.uploadedExcelDocuments.push(item);
                            });

                            console.log("Excel uploaded successfully:", response);

                            // Hide uploading, show success
                            $(".box__uploading").hide();
                            $(".box__success").show();

                            // Call ShowExcelAsSpreadsheet() after upload completion
                            $scope.ShowExcelAsSpreadsheet();

                            // Get ContentFileIID from response and pass it to uploadFile
                            if (file && response.FileInfo[0].ContentFileIID) {
                                $scope.uploadFile(file, response.FileInfo[0].ContentFileIID);
                            }
                        });
                    });

                    dropzoneInstance.on("error", function (file, errorMessage) {
                        $(".box__uploading").hide();
                        $(".box__error").show().find("span").text(errorMessage);
                    });

                    dropzoneInstance.on("complete", function () {
                        setTimeout(function () {
                            $(".box__uploading, .box__success, .box__error").fadeOut();
                        }, 3000); // Hide messages after 3 seconds
                    });
                }
            });
        });
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
                   

                    $('.dropzone')[0].dropzone.files.forEach(function (file) {
                        file.previewElement.remove()
                    })

                    $('.dropzone').removeClass('dz-started')
         
                }
                else {
                    $().showGlobalMessage($root, $timeout, true, result.Message);
       
                }
            },
            error: function (error) {
                $().showGlobalMessage($root, $timeout, true, "Something went wrong. Please try later!");
          
            }
        })
    }


    $scope.ExtractedData = []; // Initialize with your data or leave it empty

    $scope.uploadFile = function (file, contentFileIID) {
        if (!file) {
            alert("No file selected.");
            return;
        }

        var formData = new FormData();
        formData.append("file", file); // Match backend parameter name
        formData.append("ContentFileIID", contentFileIID); 

        $scope.IsExtractionLoading = true;
        var url = "Schools/AILessonPlanner/ExtractPdfOrExcelData"; // Calls your backend

        $http.post(url, formData, {
            headers: { "Content-Type": undefined }, // Let the browser set content type
            transformRequest: angular.identity, // Prevent Angular from modifying FormData
        })
            .then(function (response) {
                alert("Upload successful: " + response.data.message);
                let extractedData = JSON.parse(response.data.data);

                // Assign the classes to $scope.classes
                $scope.ExtractedData = extractedData.Classes;

                console.log($scope.ExtractedData);
                $scope.IsExtractionLoading = false;

            })
            .catch(function (error) {
                alert("Error uploading file: " + (error.data ? error.data.message : "Unknown error"));
            });
    };

    $scope.subject = $scope.subject || { chapters: [] };

    $scope.getTotalUnits = function () {
        let totalUnits = 0;
        angular.forEach($scope.subject.chapters, function (chapter) {
            totalUnits += chapter.concepts.length;
        });
        return totalUnits;
    };

    $scope.getTotalLessonPlans = function () {
        let totalLessonPlans = 0;
        angular.forEach($scope.subject.chapters, function (chapter) {
            angular.forEach(chapter.concepts, function (concept) {
                totalLessonPlans += (concept.learning_objectives ? concept.learning_objectives.length : 0);
            });
        });
        return totalLessonPlans;
    };

    $scope.hardcodedExcelBase64 = $scope.uploadedExcelDocuments.ContentData

    $scope.ShowExcelAsSpreadsheet = function () {
        if (!$scope.uploadedExcelDocuments || !$scope.uploadedExcelDocuments.length) {
            console.warn("No file uploaded");
            return;
        }

        var file = $scope.uploadedExcelDocuments[0]; // Get the first uploaded file
        var fileExtension = file.ContentFileName.split('.').pop().toLowerCase();

        document.getElementById("fileName").innerText = file.ContentFileName;

        if (fileExtension === "xlsx" || fileExtension === "xls") {
            // Excel file handling
            var binaryString = atob(file.ContentData);
            var bytes = new Uint8Array(binaryString.length);
            for (var i = 0; i < binaryString.length; i++) {
                bytes[i] = binaryString.charCodeAt(i);
            }

            var workbook = XLSX.read(bytes.buffer, { type: "array", cellDates: true });
            var sheetName = workbook.SheetNames[0]; // Get first sheet
            var sheet = workbook.Sheets[sheetName];

            var sheetData = XLSX.utils.sheet_to_json(sheet, { header: 1, defval: "" });
            var headers = sheetData[0] || [];
            var data = sheetData.length > 1 ? sheetData.slice(1) : [];

            // Remove rows where all columns are empty
            data = data.filter(row => row.some(cell => cell !== ""));

            var container = document.getElementById("fileContainer");
            container.innerHTML = ""; // Clear previous content

            var hot = new Handsontable(container, {
                data: data,
                rowHeaders: true,
                colHeaders: headers,
                filters: true,
                dropdownMenu: true,
                stretchH: "all",
                licenseKey: "non-commercial-and-evaluation",
            });
        }
        else if (fileExtension === "pdf") {
            try {
                console.log("Decoding PDF data...");

                // Decode Base64 string into Uint8Array
                var binaryString = atob(file.ContentData);
                var bytes = new Uint8Array(binaryString.length);
                for (var i = 0; i < binaryString.length; i++) {
                    bytes[i] = binaryString.charCodeAt(i);
                }

                console.log("PDF data size:", bytes.length, "bytes");

                if (bytes.length === 0) {
                    throw new Error("Decoded PDF file is empty.");
                }

                // Clear container
                var container = document.getElementById("fileContainer");
                container.innerHTML = ""; // Clear previous content

                // Load PDF
                var loadingTask = pdfjsLib.getDocument({ data: bytes.buffer });
                loadingTask.promise.then(function (pdf) {
                    console.log("Total Pages:", pdf.numPages);

                    for (let pageNumber = 1; pageNumber <= pdf.numPages; pageNumber++) {
                        pdf.getPage(pageNumber).then(function (page) {
                            var viewport = page.getViewport({ scale: 1.5 });


                            var desiredWidth = 1000; // Set desired width
                            var scale = desiredWidth / viewport.width; // Calculate scale to maintain aspect ratio
                            viewport = page.getViewport({ scale: scale });
                            // Create canvas for each page
                            var canvas = document.createElement("canvas");
                            canvas.classList.add('card', 'rounded-3' , 'mb-2');
                            canvas.id = "pdfCanvas" + pageNumber;
                            container.appendChild(canvas);

                            canvas.height = viewport.height;
                            canvas.width = viewport.width;

                            var ctx = canvas.getContext("2d");
                            var renderContext = {
                                canvasContext: ctx,
                                viewport: viewport
                            };

                            page.render(renderContext);
                        }).catch(function (error) {
                            console.error("Error rendering page:", error);
                        });
                    }
                }).catch(function (error) {
                    console.error("Error loading PDF:", error);
                });
            } catch (error) {
                console.error("PDF processing error:", error);
            }
        } else {
            console.warn("Unsupported file format");
        }
    };

    $scope.saveLessonPlans = function () {

        if (!$scope.LessonPlanChapterDTO.AcademicYear) {
            $().showGlobalMessage($root, $timeout, true, "Please select AcademicYear!");
            return false;
        }
        if (!$scope.LessonPlanChapterDTO.Class) {
            $().showGlobalMessage($root, $timeout, true, "Please select Class!");
            return false;
        }
        if (!$scope.LessonPlanChapterDTO.Section) {
            $().showGlobalMessage($root, $timeout, true, "Please select Section!");
            return false;
        }
        if (!$scope.LessonPlanChapterDTO.Subject) {
            $().showGlobalMessage($root, $timeout, true, "Please select Subject!");
            return false;
        }

        let payload = [];

        $scope.ExtractedData.forEach(classItem => {
            classItem.Subjects.forEach(subject => {
                subject.Chapters.forEach(chapter => {

                    let chapterToSave = {
                        AcademicYearID: $scope.LessonPlanChapterDTO.AcademicYear.Key,
                        ClassID: $scope.LessonPlanChapterDTO.Class.Key,
                        SectionID: $scope.LessonPlanChapterDTO.Section.Key,
                        SubjectID: $scope.LessonPlanChapterDTO.Subject.Key,

                        ChapterTitle: chapter.ChapterName,
                        SubjectUnits: chapter.Lessons 
                    };
                    payload.push(chapterToSave);
                });
            });
        });

        console.log("Data being sent to server:", payload);

        $.ajax({
            url: 'Schools/AILessonPlanner/SaveChapterEntries',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(payload),
            success: function (result) {
                if (!result.IsError) {
                    console.log("Lesson plans saved successfully", result);
                    $().showGlobalMessage($root, $timeout, false, "Lesson plans saved successfully!");
                } else {
                    console.error("Error saving lesson plan:", result.Response);
                    $().showGlobalMessage($root, $timeout, true, "Error saving: " + result.Response);
                }
            },
            error: function (xhr, status, error) {
                console.error("AJAX Error saving lesson plan:", xhr.responseText);
                $().showGlobalMessage($root, $timeout, true, "An unexpected error occurred.");
            }
        });
    };


    $scope.ShowUploadedExcelFile = function () {
        if (!$scope.uploadedExcelDocuments || $scope.uploadedExcelDocuments.length === 0) {
            console.warn("No uploaded files to preview.");
            return;
        }

        // Assuming we want to preview the first uploaded file
        var uploadedFile = $scope.uploadedExcelDocuments[0];

        if (!uploadedFile.ContentData) {
            console.error("ContentData is missing for the uploaded file.");
            return;
        }

        try {
            // Decode Base64 string to binary
            var binaryString = atob(uploadedFile.ContentData);
            var bytes = new Uint8Array(binaryString.length);
            for (var i = 0; i < binaryString.length; i++) {
                bytes[i] = binaryString.charCodeAt(i);
            }

            // Read as Excel Workbook
            var workbook = XLSX.read(bytes, { type: 'array' });

            // Extract first sheet
            var firstSheetName = workbook.SheetNames[0];
            var sheetData = XLSX.utils.sheet_to_json(workbook.Sheets[firstSheetName], { header: 1 });

            console.log("Extracted Excel Data:", sheetData);

            // Update scope for table rendering
            $scope.$apply(function () {
                $scope.excelData = sheetData;
            });
        } catch (error) {
            console.error("Error processing Excel file:", error);
        }
    };

        $scope.SaveUploadedExcelFile = function ($event) {

  

        if (!$scope.uploadedExcelDocuments || $scope.uploadedExcelDocuments.length == 0) {
            $().showGlobalMessage($root, $timeout, true, "Please upload Excel Document!");
            return false;
        }

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
    }



  



   
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