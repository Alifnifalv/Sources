app.controller("ReportViewController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $rootScope) {

    console.log('ReportViewController Loaded');

    $scope.ReportName = null;
    $scope.Parameters = null;
    $scope.ParameterModel = {};

    $scope.WindowContainer = null;
    $scope.LookUps = [];
    var lookLoadCount = 0;

    $scope.IsLoading = false;

    $scope.IsOnInit = false;

    function showOverlay() {
        $('#reportprocessingloaderModal_' + $scope.WindowContainer).modal('show');
    }

    function hideOverlay() {
        // Optionally hide it after some processing time
        setTimeout(function () {
            $('#reportprocessingloaderModal_' + $scope.WindowContainer).modal('hide');
        }, 1000); // Change the timeout as necessary
    }

    $scope.init = function (reportName, windowContainer, model, dateFormat, reportLookups, reportParameters) {

        clientDateFormat = dateFormat;
        $scope.ReportName = reportName;
        $scope.Parameters = model.ReportParameter;
        $scope.WindowContainer = windowContainer;
        $scope.ReportLookups = reportLookups;
        $scope.ReportParameters = reportParameters;

        $scope.IsOnInit = true;

        $scope.LoadReport();

        $scope.LoadLookups();
    };

    $scope.LoadLookups = function () {
        $.each($scope.ReportLookups, function (index, lookup) {
            if (lookup.IsOnInit === false) {
                $scope.LookUps[lookup.LookUpName] = [{ Key: '', Value: '' }]
                lookLoadCount++
            }
            else {
                if (lookup.IsLazyLoad) {
                    fillLazyLookup(lookup);
                }
                else {
                    $scope.LookUps[lookup.LookUpName] = lookup.Lookups

                    lookLoadCount++
                }
            }
        });
    };

    function fillLazyLookup(lookup) {
        $.ajax({
            type: 'GET',
            url: lookup.Url,
            contentType: 'application/json;charset=utf-8',
            success: function (result) {
                $scope.$apply(function () {
                    if (result.Data == undefined) {
                        $scope.LookUps[lookup.LookUpName] = result
                    } else {
                        $scope.LookUps[lookup.LookUpName] = result.Data
                    }

                    if (lookup.CallBack != undefined && lookup.CallBack != '') {
                        $scope.$eval(lookup.CallBack)
                    }

                    lookLoadCount++
                })
            },
            error: function () {
                lookLoadCount++
            }
        });
    };

    $scope.CloseOffCanvas = function () {
        var myOffcanvas = document.getElementById('offcanvasRight_' + $scope.WindowContainer);

        if (myOffcanvas) {
            if (myOffcanvas.classList.contains('show')) {
                myOffcanvas.classList.remove('show');
            }
        }
    }

    $scope.LoadReportButtonClick = function (format) {

        $scope.IsOnInit = false;
        $scope.LoadReport(format);
    }

    $scope.LoadReport = function (format) {
        showOverlay();
        if (!format) {
            format = 'html5';
        }

        var parameter = {};
        $('#' + $scope.WindowContainer).find('form').serializeArray().forEach(function (formData) {
            var multipleParameters = $scope.ParameterModel[formData.name];
            if (multipleParameters && multipleParameters.length > 0) {
                var parameterString = "";
                multipleParameters.forEach(param => {
                    parameterString += !parameterString ? param.Key : "," + param.Key;
                });

                parameter[formData.name] = parameterString;
            }
            else {
                parameter[formData.name] = formData.value;
            }
        });

        //if ($scope.Parameters) {
        //    var param = $scope.Parameters;
        //    $rootScope.GetKeys(param).forEach(function (key) {
        //        parameter[key] = param[key];
        //    });
        //}

        var nonHiddenParameters = [];
        // Loop through each entry in $scope.ReportParameters
        $scope.ReportParameters.forEach(function (reportParam) {
            var key = reportParam.Name;

            var param = $scope.Parameters.length > 0 ? $scope.Parameters.find(p => p.Key == key) : null;

            // Check if the key from $scope.ReportParameters is missing in the parameter object
            if (!(key in parameter)) {
                if (param) {
                    parameter[key] = param.Value;
                }
                else {
                    // If the key is missing, add it to the parameter object with an empty value
                    parameter[key] = "";  // You can set a default value if needed
                }
            }

            if ((parameter[key].trim() === "" || parameter[key].trim() === "0") && param && param.Value) {
                parameter[key] = param.Value;
            }

            if (!reportParam.Hidden) {
                nonHiddenParameters.push(reportParam);
            }
        });

        if (nonHiddenParameters.length > 0) {
            for (var i = 0; i < nonHiddenParameters.length; i++) {
                var reportParam = nonHiddenParameters[i];

                // Check if the parameter name exists and has a non-empty value
                if (parameter.hasOwnProperty(reportParam.Name) && parameter[reportParam.Name].trim() === "") {
                    //if (!reportParam.Name.toLowerCase().contains("search") && !reportParam.Name.toLowerCase().contains("date")) {
                    if (!reportParam.Name.toLowerCase().contains("search") && !reportParam.AllowBlank) {
                        hideOverlay();
                        if (!$scope.IsOnInit) {
                            $().showGlobalMessage($rootScope, $timeout, true, "Select all parameters!", 2000);
                        }
                        return false;  // Return false if a matching name has an empty value
                    }
                }
            }
        }

        $timeout(function () {
            if (nonHiddenParameters.length == 0) {
                $scope.CloseOffCanvas();
            }

            showOverlay();
            $scope.IsLoading = true;

            var url = utility.myHost + "Reports/ReportView/ReportViewPOST";

            var requestModel = {
                "ReportName": $scope.ReportName,
                "Parameters": JSON.stringify(parameter),
                "Format": format
            };

            // Fetch report HTML via POST request
            $http({
                method: 'POST',
                url: url,
                data: requestModel
            }).then(function (response) {
                // Get full HTML content of the report
                var fullReportHtml = response.data;
                $scope.FullHtmlReport = response.data;

                // Extract the styles (style tags or link tags)
                var parser = new DOMParser();
                var htmlDoc = parser.parseFromString(fullReportHtml, 'text/html');
                var styles = htmlDoc.head.innerHTML; // Extract all style tags and links from the head

                // Split content by page breaks
                var pages = fullReportHtml.split('<div style="page-break-after:always">');

                // Store pages in scope for pagination
                $scope.ReportPages = pages;
                $scope.CurrentPageIndex = 1;

                // Store styles to apply to each page
                $scope.reportStyles = styles;

                hideOverlay();

                // Show the first page
                $scope.ShowPage($scope.CurrentPageIndex);

                $scope.IsLoading = false;

                $scope.CloseOffCanvas();
            }).catch(function (error) {
                hideOverlay();
                $scope.IsLoading = false;

                $().showGlobalMessage($rootScope, $timeout, true, error.data, 3500);
            }).then(function () {
                hideOverlay();
            });
        });

    };

    // Function to show a specific page
    $scope.ShowPage = function (pageIndex) {
        pageIndex = pageIndex - 1;
        if ($scope.ReportPages && $scope.ReportPages.length > 0 && pageIndex >= 0 && pageIndex < $scope.ReportPages.length) {
            // Inject the styles and HTML of the current page into a container
            document.querySelector('#htmlView' + $scope.WindowContainer).innerHTML = $scope.reportStyles + $scope.ReportPages[pageIndex];

            // After inserting the content, find the div with id ending in '_gr' and remove the height style
            let targetDiv = document.querySelector('#htmlView' + $scope.WindowContainer + ' div[dir="LTR"] div[id$="_gr"]');

            // Check if the target div exists and remove the height style attribute
            if (targetDiv && targetDiv.style.height) {
                targetDiv.style.removeProperty('height');
            }

            $scope.CurrentPageIndex = pageIndex + 1;
            $scope.OldPageIndex = $scope.CurrentPageIndex;
        }
    };

    // Pagination controls
    $scope.NextPage = function () {
        if ($scope.CurrentPageIndex < $scope.ReportPages.length) {
            $scope.ShowPage($scope.CurrentPageIndex + 1);
        }
    };

    $scope.PreviousPage = function () {
        if ($scope.CurrentPageIndex > 0) {
            $scope.ShowPage($scope.CurrentPageIndex - 1);
        }
    };

    $scope.FirstPage = function () {
        if ($scope.CurrentPageIndex > 0) {
            $scope.ShowPage(1);
        }
    };

    $scope.LastPage = function () {
        if ($scope.CurrentPageIndex > 0) {
            $scope.ShowPage($scope.ReportPages.length);
        }
    };

    // Jump to a specific page
    $scope.GoToPage = function (pageNumber) {
        // Convert to zero-based index
        var pageIndex = pageNumber - 1;

        if ($scope.ReportPages) {
            // Check if the page number is valid
            if (pageIndex >= 0 && pageIndex < $scope.ReportPages.length) {
                $scope.ShowPage(pageIndex);
            }
            else {
                $().showGlobalMessage($rootScope, $timeout, true, "Invalid page number.", 1500);
            }
        }
        else {
            $scope.CurrentPageIndex = null;
            $().showGlobalMessage($rootScope, $timeout, true, "No pages are available for search.", 3500);
        }
    };

    // Add the function that triggers GoToPage after the model is updated.
    $scope.TriggerGoToPage = function () {
        // Using $timeout to ensure ng-model has been updated before calling GoToPage.
        $timeout(function () {
            var pageIndex = parseInt($scope.CurrentPageIndex);

            if (pageIndex.toString() == "NaN") {

                $scope.CurrentPageIndex = $scope.OldPageIndex;

                $().showGlobalMessage($rootScope, $timeout, true, "Only allow numbers.", 1500);
            }
            else {
                // Pass the updated value (CurrentPageIndex + 1) to the GoToPage function.
                $scope.GoToPage(pageIndex + 1);
            }
        });
    };

    $scope.DownloadReport = function (format, type) {

        var isPrint = false;
        if (format == "pdf") {
            if (type == "print") {
                isPrint = true;
            }
        }

        var parameter = {};
        $('#' + $scope.WindowContainer).find('form').serializeArray().forEach(function (formData) {
            var multipleParameters = $scope.ParameterModel[formData.name];
            if (multipleParameters && multipleParameters.length > 0) {
                var parameterString = "";
                multipleParameters.forEach(param => {
                    parameterString += !parameterString ? param.Key : "," + param.Key;
                });

                parameter[formData.name] = parameterString;
            }
            else {
                parameter[formData.name] = formData.value;
            }
        });

        //if ($scope.Parameters) {
        //    var param = $scope.Parameters;
        //    $rootScope.GetKeys(param).forEach(function (key) {
        //        parameter[key] = param[key];
        //    });
        //}

        var nonHiddenParameters = [];
        // Loop through each entry in $scope.ReportParameters
        $scope.ReportParameters.forEach(function (reportParam) {
            var key = reportParam.Name;

            var param = $scope.Parameters.length > 0 ? $scope.Parameters.find(p => p.Key == key) : null;

            // Check if the key from $scope.ReportParameters is missing in the parameter object
            if (!(key in parameter)) {
                if (param) {
                    parameter[key] = param.Value;
                }
                else {
                    // If the key is missing, add it to the parameter object with an empty value
                    parameter[key] = "";  // You can set a default value if needed
                }
            }

            if ((parameter[key].trim() === "" || parameter[key].trim() === "0" || parameter[key].trim() <= "0") && param && param.Value) {
                parameter[key] = param.Value;
            }

            if (!reportParam.Hidden) {
                nonHiddenParameters.push(reportParam);
            }
        });

        if (nonHiddenParameters.length > 0) {
            for (var i = 0; i < nonHiddenParameters.length; i++) {
                var reportParam = nonHiddenParameters[i];

                // Check if the parameter name exists and has a non-empty value
                if (parameter.hasOwnProperty(reportParam.Name) && parameter[reportParam.Name].trim() === "") {
                    //if (!reportParam.Name.toLowerCase().contains("search") && !reportParam.Name.toLowerCase().contains("date")) {
                    if (!reportParam.Name.toLowerCase().contains("search") && !reportParam.AllowBlank) {
                        hideOverlay();
                        $().showGlobalMessage($rootScope, $timeout, true, "Select all parameters!", 2000);

                        return false;  // Return false if a matching name has an empty value
                    }
                }
            }
        }

        var reportUrl = utility.myHost + "Reports/ReportView/Show?reportName=" + $scope.ReportName + "&format=" + format + "&parameters=" + JSON.stringify(parameter) + "&isPrint=" + isPrint;
        const pdfWindow = window.open(reportUrl);
        pdfWindow.print();
    };

    $scope.RefreshSelect2 = function (dataUrl, select, dataSource, lookUpName, isLoadOnce) {

        var data = $scope.ReportLookups.find(rl => rl.LookUpName == lookUpName);

        if (data != null && !data.IsLazyLoad) {
            $timeout(function () {
                $scope.$apply(function () {

                    $scope.LookUps[lookUpName] = data.Lookups;
                });
            });
            return false;
        }

        if (data != null && data.IsReLoadedByParameters) {
            $timeout(function () {
                $scope.$apply(function () {

                    $scope.LookUps[lookUpName] = data.Lookups;
                });
            });

            return false;
        }

        if (isLoadOnce != undefined && isLoadOnce) {
            var lookUp = $scope.LookUps[dataSource.split('.')[1]];

            if (lookUp != null && lookUp.length > 1)
                return;
        }

        if (dataUrl.indexOf('?') > 0) {
            var url = dataUrl + "&lookupName=" + dataSource;
        } else {
            var url = dataUrl + "?lookupName=" + dataSource;
        }

        if (select.search == undefined) {
            url = url + "&searchText=";
        }
        else {
            url = url + "&searchText=" + select.search;
        }

        searchHandler = $.ajax({
            url: url,
            type: 'GET',
            success: function (result) {

                $scope.LookUps[lookUpName] = [];

                $.each(result.Data, function (index, item) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.LookUps[lookUpName].push({ Key: item.Key, Value: item.Value });
                        });
                    });

                });
            },
            complete: function (result) {
            }
        });
    };

    $scope.OnSelectedSelect2 = function (selected, parameterName) {
        $scope.ParameterModel[parameterName] = selected.selected;

        $scope.UpdateLookupsByParameters(selected.selected, parameterName);
    };

    $scope.UpdateLookupsByParameters = function (selectedValue, parameterName) {

        var parameter = {};
        $('#' + $scope.WindowContainer).find('form').serializeArray().forEach(function (formData) {
            if (formData.name == parameterName) {
                var multipleParameters = $scope.ParameterModel[formData.name];
                if (multipleParameters && multipleParameters.length > 0) {
                    var parameterString = "";
                    multipleParameters.forEach(param => {
                        parameterString += !parameterString ? param.Key : "," + param.Key;
                    });

                    formData.value = parameterString;
                }
                else {
                    formData.value = selectedValue.Key;
                }
            }
            else {
                var multipleParameters = $scope.ParameterModel[formData.name];
                if (multipleParameters && multipleParameters.length > 0) {
                    var parameterString = "";
                    multipleParameters.forEach(param => {
                        parameterString += !parameterString ? param.Key : "," + param.Key;
                    });

                    formData.value = parameterString;
                }
            }
            parameter[formData.name] = formData.value;
        });

        // Loop through each entry in $scope.ReportParameters
        $scope.ReportParameters.forEach(function (reportParam) {
            var key = reportParam.Name;

            // Check if the key from $scope.ReportParameters is missing in the parameter object
            if (!(key in parameter)) {
                // If the key is missing, add it to the parameter object with an empty value
                parameter[key] = "";  // You can set a default value if needed
            }
        });

        var parameterDetail = $scope.ReportParameters.find(p => p.Name == parameterName)

        if (parameterDetail != null) {

            if (selectedValue.Value || selectedValue.length > 0) {

                var fullParameterName = '@' + parameterName; // Add '@' to the parameter name

                // Loop through each item in $scope.ReportLookups
                $scope.ReportLookups.forEach(function (lookup) {
                    if (lookup.LookUpQueryParameters.length > 0) {
                        // Check if the LookUpQueryParameters array contains the fullParameterName
                        if (lookup.LookUpQueryParameters.includes(fullParameterName)) {

                            if ((selectedValue && selectedValue.Key == "0" && !selectedValue.Key) || selectedValue && selectedValue.length == 0) {

                                if (!lookup.IsLazyLoad) {
                                    lookup.Lookups = lookup.InitLookups;
                                }
                                lookup.IsReLoadedByParameters = false;
                            }
                            else {

                                var url = utility.myHost + "Reports/ReportView/GetUpdatedLookupsByParameters";

                                $.ajax({
                                    method: 'GET',
                                    url: url,
                                    data: {
                                        query: lookup.LookUpQuery,
                                        parameters: JSON.stringify(parameter),
                                        parameterKey: lookup.ParameterKey,
                                        parameterValue: lookup.ParameterValue
                                    },
                                    success: function (result) {

                                        if (!result.IsError) {
                                            lookup.Lookups = result.Response;
                                            lookup.IsReLoadedByParameters = true;
                                        }
                                    },
                                    complete: function (result) {
                                    }
                                });
                            }
                        }
                    }
                    else {
                        if (lookup.LookUpQuery.toLowerCase().contains("sps_")) {
                            var url = utility.myHost + "Reports/ReportView/GetUpdatedLookupsByParameters";

                            $.ajax({
                                method: 'GET',
                                url: url,
                                data: {
                                    query: lookup.LookUpQuery,
                                    parameters: JSON.stringify(parameter),
                                    parameterKey: lookup.ParameterKey,
                                    parameterValue: lookup.ParameterValue,
                                    isProcedure: true,
                                },
                                success: function (result) {

                                    if (!result.IsError) {
                                        lookup.Lookups = result.Response;
                                        lookup.IsReLoadedByParameters = true;
                                    }
                                },
                                complete: function (result) {
                                }
                            });
                        }
                    }
                });
            }
        }
    }

}]);