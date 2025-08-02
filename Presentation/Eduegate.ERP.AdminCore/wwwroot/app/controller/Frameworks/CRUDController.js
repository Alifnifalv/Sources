app.controller("CRUDController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$rootScope", function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $rootScope) {
    console.log("CRUDController");
    $scope.CRUDModel = null;
    $scope.ModelStructure = null;
    $scope.LookUps = [];
    $scope.SearchList = [];
    $scope.Message = null;
    $scope.SupplierAccountID = null;
    $scope.SupplierAccountName = null;
    $scope.UnitList = [];
    $scope.IsError = false;
    $scope.Documents = [];
    $scope.CreditnoteNumberDet = [];
    $scope.CurrencyDet = [];
    $scope.ProvisionalAccountList = [];
    $scope.IsLoading = true;
    $scope.PartialCalculation = false;
    $scope.AdvanceSearchInvoker = null;
    $scope.AdvanceSearchRunTimeFilter = null;
    $scope.formName = '';
    var IsExitAfterSave = false;
    var IsNextAfterSave = false;

    var ajaxHandler = null;
    var Entity = null;
    var IID = null;
    var lookLoadCount = 0;
    $scope.cropper = null;
    $scope.GridArrayDefault = null;
    $scope.CrudWindowContainer = null;
    $scope.QuickSmartView = [];
    $scope.GridList = [];
    var DefaultData;
    $scope.CurrencyExchangeRate = 1.000000;
    $scope.decimalPlaces = 3;
    $scope.GridPagination = {}; // Change this to navigate to different pages
    $scope.ItemsPerPage = 5; // Adjust the number of items per page as needed
    $scope.SearchQuery = ''; // Search query for filtering items

    $scope.NavigateToPage = function (gridName, $index) {
        $scope.GridPagination[gridName] = $index;
    }

    $scope.GetTotalPages = function (totalLength) {
        return Math.ceil(totalLength / $scope.ItemsPerPage);
    }

    $scope.UpdatePageSize = function (pageSize) {

        if (pageSize) {
            $scope.ItemsPerPage = pageSize;
        } else {
            $scope.ItemsPerPage = 5;
        }
    }

    $scope.Init = function (window, model, entity, id) {
        $scope.CrudWindowContainer = '#' + window;
        Entity = entity;
        IID = id;
        $scope.CRUDModel = model;

        if (model.ViewModel) {
            $scope.CRUDModel.ViewModel = angular.copy(model.ViewModel);
        }
        else if (model.Model) {
            $scope.CRUDModel.Model = angular.copy(model.Model);
        } else {
            $scope.CRUDModel.Model = angular.copy(model.MasterViewModel);
        }


        DefaultData = model;
        $scope.formName = 'crud' + model.Name + '_form';
        LoadLookups(model.Urls);
        $scope.Load(lookupsCompletedCallback);

        if (!IID || IID === 0) {
            $timeout(function () {
                $('.controls input', $($scope.CrudWindowContainer)).filter(":visible:first").focus();
                $scope.SetFieldSettings();
            });
        }

        if ($scope.CRUDModel.Screen == 116) {
            if (model.ClientParameters.length > 0) {
                model.ClientParameters.forEach(function (param) {
                    if ($scope.CRUDModel.ViewModel) {
                        if ($scope.CRUDModel.ViewModel.hasOwnProperty(param.ParameterName)) {
                            $scope.CRUDModel.ViewModel[param.ParameterName] = param.Value;
                        }
                    }
                });
            }
            if ($scope.CRUDModel.ViewModel.IsAutoCreation) {

                $scope.FillTicketDetails($scope.CRUDModel.ViewModel);
            }
        }
    };

    $scope.SetLeadClassAcademicYear = function (viewModel) {
        if (viewModel.School == null || viewModel.School == "") return false;
        showOverlay();
        var model = viewModel;
        var academicYear = model.ContactDetails.AcademicYearCode;
        var className = model.ContactDetails.Class?.Value;
        $scope.$apply(function () {
            var url = "Schools/School/GetAcademicYearBySchool?schoolID=" + model.School;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.LookUps.AcademicYear = result.data;
                    model.ContactDetails.AcademicYear = $scope.LookUps.AcademicYear.find(x => x.Value == academicYear).Key;

                }, function () {
                    hideOverlay();
                });

            var url = "Schools/School/GetClassesBySchool?schoolID=" + model.School;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.LookUps.Classes = result.data;
                    model.ContactDetails.class = $scope.LookUps.Classes.find(x => x.Value == className);


                }, function () {
                    hideOverlay();
                });
        });

    };


    $scope.SetApplicnClassAcademicYear = function (viewModel) {
        if (viewModel.School == null || viewModel.School == "") return false;
        showOverlay();
        var model = viewModel;
        var academicYear = model.Academicyear;
        var className = model.Class?.Value;
        $scope.$apply(function () {
            var url = "Schools/School/GetAcademicYearBySchool?schoolID=" + model.School;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.LookUps.AcademicYear = result.data;
                    model.SchoolAcademicyear = $scope.LookUps.AcademicYear.find(x => x.Value == academicYear).Key;

                }, function () {
                    hideOverlay();
                });

            var url = "Schools/School/GetClassesBySchool?schoolID=" + model.School;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.LookUps.Classes = result.data;
                    model.Class = $scope.LookUps.Classes.find(x => x.Value == className);


                }, function () {
                    hideOverlay();
                });
        });

    };
    $scope.SetStudentClassAcademicYear = function (viewModel) {
        if (viewModel.School == null || viewModel.School == "") return false;
        showOverlay();
        var model = viewModel;
        var academicYear = model.Academicyear;
        var className = model.Class?.Value;
        var sectionName = model.Section?.Value;
        $scope.$apply(function () {
            var url = "Schools/School/GetAcademicYearBySchool?schoolID=" + model.School;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.LookUps.AcademicYear = result.data;
                    //model.Academicyear = $scope.LookUps.AcademicYear.find(x => x.Value.replace(/\s/g, "") == academicYear.replace(/\s/g, "")).Key;

                }, function () {
                    hideOverlay();
                });

            var url = "Schools/School/GetClassesBySchool?schoolID=" + model.School;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.LookUps.Classes = result.data;
                    model.Class = $scope.LookUps.Classes.find(x => x.Value == className);


                }, function () {
                    hideOverlay();
                });

            var url = "Schools/School/GetSectionsBySchool?schoolID=" + model.School;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.LookUps.Section = result.data;
                    model.Section = $scope.LookUps.Section.find(x => x.Value == sectionName);


                }, function () {
                    hideOverlay();
                });
        });

    };


    $scope.AddNumber = function (number1, number2) {
        if (number1 && number2) {
            return parseFloat(number1) + parseFloat(number2);
        } else if (number1) {
            return parseFloat(number1);
        } else if (number2) {
            return parseFloat(number2);
        }
    };

    $scope.GetSum = function (data, key) {
        if (typeof (data) === 'undefined' || typeof (key) === 'undefined') {
            return 0;
        }

        var sum = 0;
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i][key] !== null)
                sum += parseFloat(data[i][key]);
        }

        return sum;
    };

    $scope.GetAmountByPercentage = function (amount, percentage) {
        if (amount && percentage) {
            return parseFloat(amount) * parseFloat(percentage) / 100;
        }
    };

    function lookupsCompletedCallback() {
        var model;

        if ($scope.CRUDModel.ViewModel) {
            model = angular.copy($scope.CRUDModel.ViewModel);
        }
        else {
            model = angular.copy($scope.CRUDModel.Model);
        }

        $scope.ModelStructure = angular.copy(model);
    }

    $scope.TemplateItemChange = function (event, element, detail) {
        if (detail.TaxTemplate === "" || detail.TaxTemplate === null) {
            detail.TaxPercentage = 0;
            detail.HasTaxInclusive = false;
            return;
        }

        showOverlay();
        var url = "Mutual/GetTaxTemplateItem?taxTemplateID=" + detail.TaxTemplate;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                detail.TaxPercentage = result.data.Percentage;
                detail.HasTaxInclusive = result.data.HasTaxInclusive;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.SetFieldSettings = function () {
        var notSetFieldSettings = new JSLINQ($scope.CRUDModel.FieldSettings)
            .Where(function (item) {
                return item.IsSet === false;
            }).items;

        $.each(notSetFieldSettings, function (index, data) {
            if ($scope.CRUDModel.Model && $scope.CRUDModel.Model.MasterViewModel) {
                $scope.FieldSettings(data.ModelName, $scope.CRUDModel.Model.MasterViewModel, data);

                if (data.ModelName.contains('DocumentType') || data.FieldName.contains('DocumentType')) {
                    $scope.DocumentTypeChange(null, null);
                }
            }
        });
    };

    $scope.FieldSettings = function (modelName, modelPrefix, element) {
        var fieldSetting = new JSLINQ($scope.CRUDModel.FieldSettings)
            .Where(function (item) {
                return item.ModelName === modelName;
            }).items[0];

        if (fieldSetting && fieldSetting.DefaultValue) {
            switch (fieldSetting.DefaultValue) {
                case "CurrentDate":
                    var date = new Date();
                    var formattedDate = moment(date).format(_dateFormat.toUpperCase());
                    modelPrefix[modelName] = formattedDate;
                    fieldSetting.IsSet = true;
                    break;
                default:
                    switch (fieldSetting.DateType) {
                        case "Select2":
                            var values = fieldSetting.DefaultValue.split(',');
                            $timeout(function () {
                                $scope.$apply(function () {
                                    if (element && element.ngModel) {
                                        element.ngModel.$setViewValue({ Key: values[0], Value: values[1] });
                                    }

                                    modelPrefix[modelName] = { Key: values[0], Value: values[1] };
                                });
                            });
                            fieldSetting.IsSet = true;
                            break;
                        default:
                            if (modelName.contains('.')) {
                                var splitted = modelName.split('.');

                                if (!modelPrefix[splitted[0]]) {
                                    modelPrefix[splitted[0]] = {};
                                }

                                if (!modelPrefix[splitted[0]][splitted[1]]) {
                                    modelPrefix[splitted[0]][splitted[1]] = {};
                                }

                                modelPrefix[splitted[0]][splitted[1]] = fieldSetting.DefaultValue;
                            }
                            else {
                                modelPrefix[modelName] = fieldSetting.DefaultValue;
                            }

                            fieldSetting.IsSet = true;
                            break;
                    }
                    break;
            }
        }
    };

    $scope.GetSubLookUpData = function (lookupName, subLookupName) {
        return $scope.LookUps[lookupName + "_" + subLookupName];
    };

    $scope.MultilanguagePopup = function (event, action) {
        event.preventDefault();
        var currentwindow = $(event.currentTarget).closest('.windowcontainer').attr('windowindex');
        $(".windowcontainer[windowindex=" + currentwindow + "] .languageoption").hide();
        var popup = $(event.target.parentNode).find('.languageoption');

        if (action === 'show') {
            $(popup).fadeIn(300, function () {
                $(popup).find('input,textarea').filter(':visible:first').focus();
            });
        };
    };

    $scope.ValidateField = function (event, element, fldName, viewName) {
        var validateUrl = viewName + "/ValidateField";
        if ($('#' + fldName).valid()) {
            showOverlay();

            var model;

            if ($scope.CRUDModel.ViewModel) {
                model = angular.copy($scope.CRUDModel.ViewModel);
            }
            else {
                model = angular.copy($scope.CRUDModel.Model);
            }

            $.ajax({
                type: "POST",
                url: validateUrl,
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ model: JSON.stringify({ data: model, fieldName: fldName, Screen: $scope.CRUDModel.Screen }) }),
                success: function (result) {
                    hideOverlay();

                    if (result.IsError)
                        $().showMessage($scope, $timeout, true, result.UserMessage);
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
    };

    $scope.TotalDiscountByPercentage = function (amountField) {
        var sum = 0;
        if (!isNaN($scope.CRUDModel.Model.MasterViewModel.DiscountPercentage)) {
            for (var i = $scope.CRUDModel.Model.DetailViewModel.length - 1; i >= 0; i--) {
                if (amountField) {
                    sum += parseFloat($scope.CRUDModel.Model.DetailViewModel[i][amountField]);
                }
                else {
                    sum += parseFloat($scope.CRUDModel.Model.DetailViewModel[i].Amount ?? 0);
                }
            }

            if (!$scope.CRUDModel.Model.MasterViewModel.DeliveryCharge) {
                $scope.CRUDModel.Model.MasterViewModel.DeliveryCharge = 0;
            }

            sum = sum - parseFloat($scope.CRUDModel.Model.MasterViewModel.DeliveryCharge);

            $scope.CRUDModel.Model.MasterViewModel.Discount = (sum * ($scope.CRUDModel.Model.MasterViewModel.DiscountPercentage / 100)).toFixed(3);
            $scope.SetLandingCostLastCostPrice(null);
            $scope.ChangeEntitlementAmount();
        }
    };

    $scope.TotalDiscountByAmount = function (amountField) {
        var sum = 0;
        if (!isNaN($scope.CRUDModel.Model.MasterViewModel.Discount)) {
            for (var i = $scope.CRUDModel.Model.DetailViewModel.length - 1; i >= 0; i--) {

                if (amountField) {
                    sum += parseFloat($scope.CRUDModel.Model.DetailViewModel[i][amountField]);
                }
                else {
                    sum += parseFloat($scope.CRUDModel.Model.DetailViewModel[i].Amount);
                }
            }

            if (!$scope.CRUDModel.Model.MasterViewModel.DeliveryCharge) {
                $scope.CRUDModel.Model.MasterViewModel.DeliveryCharge = 0;
            }

            sum = sum - parseFloat($scope.CRUDModel.Model.MasterViewModel.DeliveryCharge);

            $scope.CRUDModel.Model.MasterViewModel.DiscountPercentage = Math.round((parseFloat($scope.CRUDModel.Model.MasterViewModel.Discount) / sum) * 100 * 100) / 100;



            $scope.SetLandingCostLastCostPrice(null);
            $scope.ChangeEntitlementAmount();
        }

    };

    $scope.ChangeEntitlementAmount = function () {
        if (isNaN($scope.CRUDModel.Model.MasterViewModel.Discount)) {
            $scope.CRUDModel.Model.MasterViewModel.Discount = 0.000;
        }
        if ($scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps != null) {
            var totalPrice = $scope.CRUDModel.Model.DetailViewModel.reduce(function (sum, current) {
                return (Number(sum ?? 0) + Number(current.Amount ?? 0));
            }, 0) - ($scope.CRUDModel.Model.MasterViewModel.Discount ?? 0);

            $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps[0].Amount = Number(totalPrice ?? 0).toFixed(3);
        }
    };

    $scope.InsertGridRow = function (index, row, model) {
        model.splice(index + 1, 0, angular.copy(row));
    };

    $scope.RemoveGridRow = function (index, row, model) {
        model.splice(index, 1);

        if (index === 0) {
            $scope.InsertGridRow(index, row, model);
        }
        if ($scope.CRUDModel.Model != null && $scope.CRUDModel.Model.MasterViewModel != null)
            $scope.ChangeEntitlementAmount();
    };

    $scope.ShowQuickSmartView = function (viewName, model, title) {
        var tabIndex = 0;
        var isTabFound = false;

        if (!model) {
            return;
        }

        if (!title) {
            title = viewName;
        }

        $.each($scope.QuickSmartView, function (key, value) {
            tabIndex++;

            if (value.ViewName === viewName) {
                isTabFound = true;
                return;
            }
        });

        var detailUrl = viewName + "/DetailedView?IID=" + model;

        $http({ method: 'Get', url: detailUrl })
            .then(function (result) {

                var htmlString = '';

                if (isTabFound) {
                    $('[tabindex=' + (tabIndex).toString() + ']', $($scope.CrudWindowContainer)).remove();
                    htmlString = '<div class="quickviewpanel slide" tabindex=' + (tabIndex) + '> <div class="q-view-inner">' + result.data + '</div> </div>';
                }
                else {
                    $scope.QuickSmartView.push({ Title: title, ViewName: viewName });
                    htmlString = '<div class="quickviewpanel slide" tabindex=' + $scope.QuickSmartView.length + '> <div class="q-view-inner">' + result.data + '</div> </div>';
                }

                $("#editrightpanel", $($scope.CrudWindowContainer)).append($compile(htmlString)($scope));

                $('[tabindex=' + ($scope.QuickSmartView.length).toString() + '] .remove', $($scope.CrudWindowContainer)).click(function () {
                    $scope.$apply(function () {
                        $scope.CloseQuickSmartView(parseInt($(this).closest('.quickviewpanel').attr('tabindex')) - 1);
                    });
                });
            });
    };

    $scope.OpenQuickSmartView = function (tabIndex) {
        $('.quickviewpanel', $($scope.CrudWindowContainer)).removeClass('slide');
        $('[tabindex=' + (tabIndex + 1).toString() + ']', $($scope.CrudWindowContainer)).addClass('slide');
    };

    $scope.CloseQuickSmartView = function (tabIndex) {
        $scope.QuickSmartView.splice(tabIndex, 1);
        $('[tabindex=' + (tabIndex + 1).toString() + ']', $($scope.CrudWindowContainer)).remove();
    };

    $scope.TabClick = function (event, containerName) {
        $("li.active", $($scope.CrudWindowContainer)).removeClass("active");
        $(event.currentTarget.parentElement).addClass('active');


        $('.tab-pane', $($scope.CrudWindowContainer)).css("display", "none");
        $('#' + containerName, $($scope.CrudWindowContainer)).show();
    };

    $scope.AddNewItem = function (modelName) {
        var newItem = angular.copy($scope.ModelStructure);
        modelName.push(newItem);
    };

    $scope.DeleteItem = function (modelName, index) {
        if (confirm("Are you sure, wants to delete?")) {
            modelName.pop(modelName[index]);
        }
    };


    $scope.RemoveContent = function (url, prefixAsString, sourceModelAsString, index) {
        if (confirm("Are you sure, wants to delete?")) {
            var contentUrl = utility.myHost + url;
            var index = index
            $http({ method: 'Get', url: contentUrl }).then(function () {
                var model = GetVariableFromString($scope, prefixAsString);
                //model[index][sourceModelAsString] = null;

                if (index == 'undefined' || index == null) {
                    model[sourceModelAsString] = null;
                }
                else {
                    model[index][sourceModelAsString] = null;
                }

                //model[index] = null;
                //model.splice(index, 1)

            });
        }
    };

    $scope.SelectTabItem = function (modelName, tabName, index) {
        if (modelName === undefined)
            modelName = [];

        if (modelName[tabName] === undefined)
            modelName[tabName] = index;

        return modelName[tabName] === index;
    };

    $scope.ClickTabItem = function (modelName, tabName, index, dataUrl) {
        modelName[tabName] = index;

        if (dataUrl !== "" && dataUrl !== undefined) {
            //var doc = $scope.CRUDModel.ViewModel.Document

            if ($scope.CRUDModel.ViewModel.Document.IsLoaded !== undefined && $scope.CRUDModel.ViewModel.Document.IsLoaded)
                return;

            $scope.CRUDModel.ViewModel.Document.IsLoaded = true;
            dataUrl = dataUrl + '?' + $scope.CRUDModel.ViewModel.Document.ReferenceParameterName + '=' + IID + '&' + $scope.CRUDModel.ViewModel.Document.EntityParameterName + '=' + $scope.CRUDModel.Name;
            $http({ method: 'GET', url: dataUrl })
                .then(function (result) {
                    //$scope.GridArrayDefault =  angular.copy($scope.CRUDModel.ViewModel.Document.Documents[0]);
                    $scope.CRUDModel.ViewModel.Document.Documents = result.data;
                    if (result.data.length > 0 && result.data[0].DocumentFileIID > 0) {
                        $scope.InsertOnTop($scope.CRUDModel.ViewModel.Document, 0);
                    }
                });
        }
    };

    $scope.InsertOnTop = function (array, index) {
        if (index === 0)
            array.Documents.splice(0, 0, angular.copy({}));
        //else 
        //  array.Documents.splice(index, 1);
    };

    function LoadLookups(urls) {
        $.each(urls, function (index, url) {
            if (url.IsOnInit === false) {
                $scope.LookUps[url.LookUpName] = [{ 'Key': '', Value: '' }];
                lookLoadCount++;
            }
            else {
                $scope.LookUps[url.LookUpName] = [{ 'Key': '', Value: '' }];

                $.ajax({
                    type: "GET",
                    url: url.Url,
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        $scope.$apply(function () {
                            if (result.Data === undefined) {
                                $scope.LookUps[url.LookUpName] = result;
                            }
                            else {
                                $scope.LookUps[url.LookUpName] = result.Data;
                            }

                            if (url.CallBack !== undefined && url.CallBack != '') {
                                $scope.$eval(url.CallBack);
                            }

                            lookLoadCount++;
                        });
                    },
                    error: function () {
                        lookLoadCount++;
                    }
                });
            }
        });
    }

    $scope.SetResultValue = function (sourceValue, destValue) {
        var variable = GetVariableFromString($scope, sourceValue);
        $scope.CRUDModel.ViewModel[destValue] = variable;
    };

    $scope.Load = function (lookupsCompletedCallback) {
        if ((IID === null || IID === 0) && !$scope.CRUDModel.ReferenceIIDs) {
            IID = 0;
            var lookUpLoads = setInterval(function () {
                if (lookLoadCount >= $scope.CRUDModel.Urls.length) {
                    lookupsCompletedCallback();
                    hideOverlay();

                    if ($scope.CRUDModel.Screen === 0) {
                        clearInterval(lookUpLoads);

                        $scope.$apply(function () {
                            $scope.CRUDModel = angular.copy(DefaultData);
                            SetDefaultValues();
                        });
                    }
                    else {
                        clearInterval(lookUpLoads);
                        SetDefaultValues();
                    }
                }
            }, 100);

            return;
        }

        //make sure all the lookups are completed and send the actual load
        var dataLoads = setInterval(function () {
            if (lookLoadCount >= $scope.CRUDModel.Urls.length) {
                lookupsCompletedCallback();
                var getUrl = null;

                //if ($scope.CRUDModel.Screen == 2377) {
                //    $scope.GetStudentTransportDatas($scope.CRUDModel.ViewModel, $scope.CRUDModel.IID);
                //}

                if ($scope.CRUDModel.IsGenericCRUDSave === true) {
                    getUrl = 'Frameworks/CRUD/Get?screen=' + $scope.CRUDModel.Screen + '&ID=' + $scope.CRUDModel.IID;
                }
                else {
                    if ($scope.CRUDModel.ReferenceIIDs)
                        getUrl = $scope.CRUDModel.ViewFullPath + '/Get/' + $scope.CRUDModel.ReferenceIIDs;
                    else
                        getUrl = $scope.CRUDModel.ViewFullPath + '/Get/' + IID.toString();
                }

                $.ajax({
                    type: "GET",
                    url: getUrl,
                    contentType: "json",
                    success: LoadCallBack,
                    complete: function (result) {
                        if ($scope.CRUDModel.IID != 0 && $scope.CRUDModel.ViewModel != null && $scope.CRUDModel.ViewModel.School != undefined) {

                            if ($scope.CRUDModel.Screen == 2319) {
                                $scope.SetLeadClassAcademicYear($scope.CRUDModel.ViewModel);
                            }
                            else if ($scope.CRUDModel.Screen == 2123) {
                                $scope.SetApplicnClassAcademicYear($scope.CRUDModel.ViewModel);
                            }
                            else if ($scope.CRUDModel.Screen == 2011) {
                                $scope.SetStudentClassAcademicYear($scope.CRUDModel.ViewModel);
                            }
                        }
                        hideOverlay();
                    }
                });

                clearInterval(dataLoads);
            }

        }, 100);
    };

    function SetDefaultValues() {
        var model = null;

        if ($scope.CRUDModel.ViewModel !== undefined)
            model = $scope.CRUDModel.ViewModel;
        else
            model = $scope.CRUDModel.Model;

        $.each($scope.CRUDModel.ClientParameters, function (index, val) {
            SetParameterValue(model, val);
            SetParameterValue(model.MasterViewModel, val);

            $.each(model.DetailViewModel, function (ind, detail) {
                SetParameterValue(detail, val);
            });
        });
    }

    function SetParameterValue(model, val) {

        for (variable in model) {
            if (variable === val.ParameterName) {
                if (val.ParameterBindingName.indexOf('.') > 0) {
                    var keys = val.ParameterBindingName.split('.');

                    if (keys.length === 2) {
                        model[keys[0]] = {};
                        model[keys[0]][keys[1]] = val.Value;
                    }
                }
                else {
                    model[val.ParameterBindingName] = val.Value;
                }
            }
        }
    }

    function LoadCallBack(result) {
        if (result.IsError !== undefined) {

            if (result.IsError) {
                $().showMessage($scope, $timeout, true, result.UserMessage);
                hideOverlay();
                return;
            }
        }

        $scope.$apply(function () {
            if ($scope.CRUDModel.ViewModel !== undefined)
                $scope.CRUDModel.ViewModel = result;
            else
                $scope.CRUDModel.Model = result;
        });

        hideOverlay();
    }

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.Save = function (event, isExit, saveUrl, isString, successCallBack, isNext) {
        if (!saveUrl) {
            saveUrl = $scope.CRUDModel.ViewFullPath + '/Save';
        }

        if (!successCallBack) {
            successCallBack = SaveCallBack;
        }

        IsExitAfterSave = isExit ? true : false;
        IsNextAfterSave = isNext ? true : false;

        var form = $(event.currentTarget).closest("form");
        if (!form.validateForm()) {
            $().showMessage($scope, $timeout, true, 'Please fill required fields');
            return false;
        }

        showOverlay();

        if (ajaxHandler !== null) {
            ajaxHandler.abort();
        }

        if (isString) {
            ajaxHandler = $.ajax({
                type: "POST",
                url: saveUrl,
                dataType: "json",
                data: { model: JSON.stringify({ screen: Entity, data: $scope.CRUDModel.ViewModel }) },
                success: successCallBack,
                error: function (error) {
                    $().showMessage($scope, $timeout, true, error.responseText);
                },
                complete: function () {
                    hideOverlay();
                    ajaxHandler = null;
                }
            });
        } else {
            ajaxHandler = $.ajax({
                type: "POST",
                url: saveUrl,
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify($scope.CRUDModel.ViewModel),
                success: SaveCallBack,
                error: function (error) {
                    $().showMessage($scope, $timeout, true, error.responseText);
                },
                complete: function () {
                    hideOverlay();
                    ajaxHandler = null;
                }
            });
        }
    };

    $scope.SaveMaster = function (event, isExit, isNext) {
        $scope.Save(event, isExit, 'Frameworks/CRUD/SaveMaster', true, SaveMasterCallBack, isNext);
    };

    $scope.SaveMasterDetail = function (event, isExit, isNext) {
        $scope.SaveTransaction(event, isExit, 'Frameworks/CRUD/SaveMasterDetail', true, SaveMasterDetailCallBack, isNext);
    };

    function SaveMasterCallBack(result) {
        if (result.IsError) {
            $().showMessage($scope, $timeout, true, result.UserMessage);
            return;
        } else {
            $scope.$apply(function () {
                if ($scope.CRUDModel.ViewModel != undefined) {
                    try {
                        $scope.CRUDModel.ViewModel = JSON.parse(result);
                    } catch (e) {
                        $scope.CRUDModel.ViewModel = result;
                    }

                    if ($scope.CRUDModel.ViewModel.FeeCollectionIID != undefined && $scope.CRUDModel.ViewModel.FeeCollectionIID > 0) {
                        $scope.CRUDModel.Name == 'FeeCollection';
                        var reportName = 'FeeReceipt';
                        var reportHeader = 'Fee Collection Print Preview';
                        var reportFullName = '%2fEduegate.Inventory.Reports%2fFeeReceipt';
                        $scope.MailReport(event, reportName, reportHeader, reportFullName);
                    }
                }
                else {
                    try {
                        $scope.CRUDModel.Model = JSON.parse(result);
                    } catch (e) {
                        $scope.CRUDModel.Model = result;
                    }
                }
            });
            $().showMessage($scope, $timeout, false, "Sucessfully saved.");
        }

        if (result.isAutoClose != undefined && result.isAutoClose == true) {
            var index = $("ul.bodyrightmain-tab li.active").index()
            if (index > -1) {
                $scope.$parent.CloseWindowTab(index);
            }
            $().showGlobalMessage($rootScope, $timeout, result.IsError, result.UserMessage);
        }

        if (IsExitAfterSave) $scope.CloseWindow();
        if (IsNextAfterSave) $scope.NextNewEntry();

        IsExitAfterSave = false;
        IsNextAfterSave = false;
    }

    function SaveMasterDetailCallBack(result) {
        if (result.IsError) {
            $().showMessage($scope, $timeout, true, result.UserMessage);
            return;
        } else {
            $scope.$apply(function () {
                if ($scope.CRUDModel.ViewModel != undefined)
                    $scope.CRUDModel.ViewModel = JSON.parse(result);
                else
                    $scope.CRUDModel.Model = JSON.parse(result);
            });

            $().showMessage($scope, $timeout, false, "Sucessfully saved.");
        }

        if (result.isAutoClose !== undefined && result.isAutoClose === true) {
            var index = $("ul.bodyrightmain-tab li.active").index();
            if (index > -1) {
                $scope.$parent.CloseWindowTab(index);
            }
            $().showGlobalMessage($rootScope, $timeout, result.IsError, result.UserMessage);
        }

        if (IsExitAfterSave) $scope.CloseWindow();
        if (IsNextAfterSave) $scope.NextNewEntry();

        IsExitAfterSave = false;
        IsNextAfterSave = false;
    }

    function SaveCallBack(result) {
        if (result.IsError) {
            $().showMessage($scope, $timeout, true, result.UserMessage);
            return;
        } else {
            $scope.$apply(function () {
                if ($scope.CRUDModel.ViewModel != undefined)
                    $scope.CRUDModel.ViewModel = result;
                else
                    $scope.CRUDModel.Model = result;
            });

            $().showMessage($scope, $timeout, false, "Sucessfully saved.");


        }

        if (result.isAutoClose !== undefined && result.isAutoClose === true) {
            var index = $("ul.bodyrightmain-tab li.active").index();
            if (index > -1) {
                $scope.$parent.CloseWindowTab(index);
            }
            $().showGlobalMessage($rootScope, $timeout, result.IsError, result.UserMessage);
        }

        if (IsExitAfterSave) $scope.CloseWindow();
        if (IsNextAfterSave) $scope.NextNewEntry();

        IsExitAfterSave = false;
        IsNextAfterSave = false;
    }

    $scope.List = function () {
        if ($scope.ShowWindow(Entity + "Lists", Entity + " Lists", Entity + "Lists"))
            return;

        $("#Overlay").fadeIn(100);

        var listUrl = Entity + "/List";
        $http({ method: 'Get', url: listUrl })
            .then(function (result) {
                $("#LayoutContentSection").append($compile(result.data)($scope));
                $scope.AddWindow(Entity + "Lists", Entity + " Lists", Entity + "Lists");
                $("#Overlay").fadeOut(100);
            });
    };

    $scope.TriggerUploadFile = function (event, parentEvent) {
        if (event === undefined) {
            $timeout(function () {
                $("#UploadFile").trigger('click');
            });
        }
        else {
            if (event.target.type === "file") return;
            event.preventDefault();
            event.stopPropagation();

            $timeout(function () {
                $($('input', event.currentTarget)[0]).trigger('click');
            });
        }
    };

    $scope.UploadImageFiles = function (uploadfiles, url, imageType, prefixAsString, sourceModelAsString, dataRow, index, element) {
        var xhr = new XMLHttpRequest();
        var fd = new FormData();
        fd.append("ImageType", imageType);

        for (i = 0; i < uploadfiles.files.length; i++) {
            fd.append(uploadfiles.files[i].name, uploadfiles.files[i]);
        }

        xhr.open("POST", url, true);
        xhr.onreadystatechange = function (url) {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var result = JSON.parse(xhr.response);
                if (result.Success === true && result.FileInfo.length > 0) {
                    $scope.$apply(function () {
                        var model = GetVariableFromString($scope, prefixAsString);
                        if (dataRow !== undefined) {
                            if (element !== undefined) {
                                model[index][sourceModelAsString] = result.FileInfo[0].ContentFileIID;
                            }
                            else {
                                if (result.FileInfo[0].ContentFileIID) {
                                    model[index][sourceModelAsString] = result.FileInfo[0].ContentFileIID;
                                }

                                //model[index][sourceModelAsString] = result.FileInfo[0].ContentFileName || result.FileInfo[0].FileName;
                            }

                            if (result.FileInfo[0].ContentFileName || result.FileInfo[0].FileName) {
                                model[index]["ContentFileName"] = result.FileInfo[0].ContentFileName || result.FileInfo[0].FileName;
                            }
                            if (result.FileInfo[0].ContentFileIID) {
                                model[index]["ContentFileIID"] = result.FileInfo[0].ContentFileIID;
                            }
                            //model[sourceModelAsString] = result.FileInfo[0].FilePath;
                            //$('#imageViewer').attr('src', result.FileInfo[0].FilePath);
                        }

                        else {

                            if (result.FileInfo[0].ContentFileIID) {
                                if (model[sourceModelAsString] !== undefined) {
                                    {
                                        model[sourceModelAsString] = result.FileInfo[0].ContentFileIID.toString();
                                    }
                                }
                                else {
                                    if (model.ProfileUrl !== undefined) {
                                        model.ProfileUrl = result.FileInfo[0].ContentFileIID.toString();
                                    }
                                }

                            }

                        }
                    });
                }
                else {
                    $().showMessage($scope, $timeout, true, result.Message);
                }
            }
        };

        xhr.send(fd);
    };

    $scope.DownloadURL = function (url) {
        var link = document.createElement("a");
        link.href = utility.myHost + url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };

    $scope.ViewContentFile = function (contentID) {
        $('#globalRightDrawer').modal('show');
        $('#globalRightDrawer').find('.modal-body').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');
        $('#globalRightDrawer').find('.modal-body').html('<iframe id="myFrame" src="Content/ReadContentsByIDWithoutAttachment?contentID=' + contentID + '" style="height: 638px; width: 100%;" frameborder="0"></iframe>');

        hideOverlay();
    }

    $scope.DeleteContentsByID = function (contentID, prefixAsString, sourceModelAsString) {

        $http({ method: 'GET', url: "Content/DeleteContentsByID?contentID=" + contentID })
            .then(function () {
                $scope.$apply(function () {
                    var model = GetVariableFromString($scope, prefixAsString);
                    if (dataRow !== undefined) {
                        if (element !== undefined) {
                            model[index][sourceModelAsString] = null;
                        }
                        else {
                            if (result.FileInfo[0].ContentFileIID) {
                                model[index][sourceModelAsString] = null;
                            }
                        }
                    }
                });
            });
    };

    function GetVariableFromString(root, variableString) {
        var object = root;
        $.each(variableString.split("."), function (index, value) {
            object = object[value];
        });

        return object;
    }

    var callBackData = null;

    $scope.AdvanceSearch = function (view, partialCalculation, data, invoker, runtimeFilter, event) {
        $('#Overlay').show();
        var listUrl = "Mutual/AdvancedFilter?view=" + view;
        callBackData = data;
        $scope.PartialCalculation = partialCalculation;
        $scope.AdvanceSearchInvoker = invoker;
        $scope.AdvanceSearchRunTimeFilter = runtimeFilter;
        $http({ method: 'Get', url: listUrl })
            .then(function (result) {
                $("#advanceSearchForCRUD", $($scope.CrudWindowContainer)).html($compile(result.data)($scope));
                var popdetect = $('#advanceSearchForCRUD', $($scope.CrudWindowContainer)).attr('data-popup-type');
                var popdetect = $('#advanceSearchForCRUD', $($scope.CrudWindowContainer)).attr('data-popup-type');
                $('#advanceSearchForCRUD', $($scope.CrudWindowContainer)).children().attr('parentcontainerID', $($scope.CrudWindowContainer).attr('id'));// assign parentContainer for the advance search popup
                var parentcontainer = $('.pagecontent', $($scope.CrudWindowContainer)).height();
                var ypos = $(".popup[data-popup-type='" + popdetect + "']", $($scope.CrudWindowContainer)).outerHeight() / 2;
                var topvalue = ypos + parentcontainer - 160;
                $(".popup[data-popup-type='" + popdetect + "']", $($scope.CrudWindowContainer)).css({ 'margin-top': '-' + topvalue + 'px' }).fadeIn(500).addClass('show');
                var listheight = $('.search-content', $($scope.CrudWindowContainer)).height();

                if (listheight > 261) {
                    $('.searchlist.tableheader', $($scope.CrudWindowContainer)).addClass('fixedposition');
                }

                $('#Overlay').hide();
            });
    };

    $scope.AdvanceSearchCallBack = function (view, data, viewFullPath) {
        if ($scope.PartialCalculation === 'True')
            $scope.PartialCalculation = true;
        else
            $scope.PartialCalculation = false;

        if (viewFullPath.endsWith('/')) {
            viewFullPath = viewFullPath.slice(0, -1);
        }

        switch (view) {
            case 'Employee':
                $http({ method: 'Get', url: `Payroll/Employee/Get?ID=${data.EmployeeIID}` })
                    .then((result) => {
                        $scope.CRUDModel.ViewModel.Employee = result.data;
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;
            case 'Customer':
                if ($scope.CRUDModel.ViewModel != undefined || $scope.CRUDModel.ViewModel != null) {
                    $('#Overlay').show();
                    $http({
                        method: 'Get', url: 'Customer/GetCustomerSummaryDetails/' + data.CustomerIID
                    })
                        .then(function (result) {
                            $scope.CRUDModel.ViewModel.Customer = result.data;
                            $('#Overlay').hide();
                        }, function () {
                            $('#Overlay').hide();
                        });
                    break;
                }
                else if ($scope.CRUDModel.MasterViewModel != undefined || $scope.CRUDModel.MasterViewModel != null) {
                    $('#Overlay').show();
                    $http({
                        method: 'Get', url: 'Customer/GetCustomerSummaryDetails/' + data.CustomerIID
                    })
                        .then(function (result) {
                            $scope.CRUDModel.Model.MasterViewModel.Customer.Key = result.data.CustomerIID;
                            var midName = null;
                            if (result.data.MiddleName == null || result.data.MiddleName == undefined || result.data.MiddleName == "" || result.data.MiddleName == "null") {
                                midName = " ";
                            }
                            else {
                                midName = result.data.MiddleName;
                            }
                            $scope.CRUDModel.Model.MasterViewModel.Customer.Value = result.data.FirstName + " " + midName + " " + result.data.LastName;
                            $('#Overlay').hide();
                        }, function () {
                            $('#Overlay').hide();
                        });
                }
                break;
            case 'Supplier':
                $http({ method: 'Get', url: 'Supplier/GetSupplierDetails/' + data.SupplierIID })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel.Supplier = result.data;
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;
            case 'Price':
                callBackData.PriceListName = data.PriceDescription;
                callBackData.ProductPriceListIID = data.ProductPriceListIID;
                break;
            case 'PurchaseOrder':
                $('#Overlay').show();
                $.ajax({
                    type: "GET",
                    url: view + '/Get?ID=' + data.HeadIID + '&partialCalculation=' + $scope.PartialCalculation,
                    contentType: "json",
                    success: function (result) {
                        if (result.UserMessage != undefined) {
                            $().showMessage($scope, $timeout, true, result.UserMessage);
                        }
                        else {
                            // result.MasterViewModel.TransactionHeadIID = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
                            result.MasterViewModel.DocumentType = $scope.CRUDModel.Model.MasterViewModel.DocumentType;
                            result.MasterViewModel.ReferenceTransactionHeaderID = result.MasterViewModel.TransactionHeadIID;
                            result.MasterViewModel.ReferenceTransactionNo = result.MasterViewModel.TransactionNo;
                            result.MasterViewModel.TransactionNo = $scope.CRUDModel.Model.MasterViewModel.TransactionNo;
                            result.MasterViewModel.TransactionDate = $scope.CRUDModel.Model.MasterViewModel.TransactionDate;

                            $.each(result.DetailViewModel, function (index, item) {
                                item.TransactionDetailID = "0";
                                item.TransactionHead = "0";
                            });
                            $scope.$apply(function () {
                                $scope.CRUDModel.Model = result;
                                $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionHeaderID = data.HeadIID;
                                $scope.CRUDModel.Model.MasterViewModel.TransactionStatus = null;
                            });
                        }

                        $scope.BranchChange(null, null);
                        $('#Overlay').hide();
                    },
                    complete: function (result) {
                        $('#Overlay').hide();
                    }
                });
                break;
            case 'PurchaseInvoice':
            case 'PurchaseReturnRequest':
            case 'BranchTransferRequest':
            case 'GoodsReceivedNote':
            case 'SalesQuotation':
            case 'BundleWrap':
                $('#Overlay').show();
                $.ajax({
                    type: "GET",
                    url: view + '/Get/' + data.HeadIID,
                    contentType: "json",
                    success: function (result) {
                        $('#Overlay').hide();

                        try {
                            result.MasterViewModel.TransactionHeadIID = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
                        }
                        catch (ex) { result.MasterViewModel.TransactionHeadIID = "0"; }
                        result.MasterViewModel.DocumentType = "0";
                        result.MasterViewModel.ReferenceTransactionNo = result.MasterViewModel.TransactionNo;
                        result.MasterViewModel.TransactionDate = $scope.CRUDModel.Model.MasterViewModel.TransactionDate;

                        $.each(result.DetailViewModel, function (index, item) {
                            item.TransactionDetailID = "0";
                            item.TransactionHead = "0";
                        });
                        $scope.$apply(function () {
                            result.MasterViewModel.TransactionNo = $scope.CRUDModel.Model.MasterViewModel.TransactionNo;
                            $scope.CRUDModel.Model = result;
                            $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionHeaderID = data.HeadIID;
                            $scope.CRUDModel.Model.MasterViewModel.TransactionStatus = null;
                        });

                        $scope.BranchChange(null, null);
                    },
                    complete: function (result) {
                        $('#Overlay').hide();
                    }
                });
                break;
            case 'PurchaseOrderAdvanceSearch':
            case 'PurchaseInvoiceAdvanceSearch':
            case 'PurchaseReturnRequestAdvanceSearch':
            case 'SalesReturnRequestAdvanceSearch':
            case 'SalesInvoiceAdvanceSearch':
            case 'BundleWrapAdvanceSearch':
            case 'SalesQuotationAdvanceSearch':
            case 'GRNAdvanceSearch':
            case 'GRNAdvanceSearchView':
            case 'ServiceEntryAdvanceSearch':
            case 'SalesOrderAdvanceSearch':
            case 'CanteenSalesOrderAdvanceSearch':
                $('#Overlay').show();
                $.ajax({
                    type: "GET",
                    url: viewFullPath + '/Get/' + data.HeadIID,
                    contentType: "json",
                    success: function (result) {
                        $('#Overlay').hide();

                        try {
                            result.MasterViewModel.TransactionHeadIID = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
                        }
                        catch (ex) { result.MasterViewModel.TransactionHeadIID = 0; }
                        result.MasterViewModel.DocumentType = $scope.CRUDModel.Model.MasterViewModel.DocumentType;
                        result.MasterViewModel.TransactionNo = $scope.CRUDModel.Model.MasterViewModel.TransactionNo;
                        result.MasterViewModel.ReferenceTransactionNo = data.TransactionNo;
                        //result.MasterViewModel.TransactionDate = moment($scope.CRUDModel.Model.MasterViewModel.TransactionDate).format(_dateFormat.toUpperCase());

                        $.each(result.DetailViewModel, function (index, item) {
                            item.TransactionDetailID = "0";
                            item.TransactionHead = "0";
                        });
                        $scope.$apply(function () {
                            var additionalExpenseList = $scope.CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps;
                            var transEntitlementMap = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps;
                            $scope.CRUDModel.Model = result;
                            //$scope.CRUDModel.Model.MasterViewModel.Validity = moment(result.MasterViewModel.Validity).format(_dateFormat.toUpperCase());
                            $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionHeaderID = data.HeadIID;
                            $scope.CRUDModel.Model.MasterViewModel.DocumentStatus = null;
                            $scope.CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps = additionalExpenseList;
                            $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps = transEntitlementMap;
                            $scope.ChangeEntitlementAmount();

                            $scope.CRUDModel.Model.MasterViewModel.PaidAmount = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps[0].Amount;
                        });

                        if ($scope.CRUDModel?.Name == "FOCSales") {
                            // Loop through the array and update the amount value to 0 for each item
                            $scope.CRUDModel.Model.DetailViewModel.forEach(function (item) {
                                item.Amount = 0;
                            });
                            $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps.forEach(function (item) {
                                item.Amount = 0;
                            });
                        }

                        $scope.BranchChange(null, null);

                        if ($scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != 0 || $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != undefined || $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != null) {
                            $scope.CRUDModel.Model.DetailViewModel.IsDisableSelect2 = true;
                        }
                    }
                });
                break;


            //case 'SalesOrderAdvanceSearch':
            //    view = 'SalesOrderAdvanceSearch';
            //    pickSalesSearch(view, data, $scope.AdvanceSearchInvoker);
            //    break;
            case 'SalesOrder':
                pickSalesSearch(view, data, $scope.AdvanceSearchInvoker);
                break;
            case 'SalesInvoice':
                pickSalesSearch(view, data);
                break;
            case 'ReplacementAdvanced':
                view = 'ReplacementAdvanced';
                pickSalesSearch(view, data, $scope.AdvanceSearchInvoker);
                break;
            case 'SalesReturnRequest':
                pickSalesSearch(view, data, $scope.AdvanceSearchInvoker);
                break;

            case 'CustomerContacts':
                pickContactsSearch(view, data, $scope.AdvanceSearchInvoker);
                break;

            case 'Contacts':
                $('#Overlay').show();
                $.ajax({
                    type: "GET",
                    url: view + '/Get/' + data.ContactIID,
                    contentType: "json",
                    success: function (result) {
                        $('#Overlay').hide();

                        $scope.$apply(function () {

                            if (result != null && result != undefined) {
                                if (result.CustomerIID > 0) {

                                    if ($scope.LookUps.length > 0) {
                                        if ($scope.LookUps.Customer != null && $scope.LookUps.Customer != undefined) {
                                            /* get the customer and contact detail*/
                                            var custID = result.CustomerIID;
                                            var custName = result.FirstName + " " + result.MiddleName + result.LastName;
                                            var cust = { Key: custID, Value: custName };
                                            // prevent dublicate data
                                            var match = $.grep($scope.LookUps.Customer.Data, function (e) { return e.Key == custID });
                                            // push selected customer in customr lookup 
                                            if (match.length == 0)
                                                $scope.LookUps.Customer.Data.push(cust);

                                            // assign selected customer to master view model
                                            $scope.CRUDModel.Model.MasterViewModel.Customer.Key = custID;
                                            $scope.CRUDModel.Model.MasterViewModel.Customer.Value = custName;
                                        }
                                    }

                                    var activeInnerTab = $($scope.CrudWindowContainer).find(".menuinnertab li.active").attr("data-target");

                                    if (activeInnerTab == "DeliveryDetails") {

                                        // assign selected contact detail into DeliveryDetails
                                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.ContactID = result.Contacts[0].ContactID;
                                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.ContactPerson = result.Contacts[0].FirstName;
                                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.DeliveryAddress = result.Contacts[0].AddressName;
                                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.MobileNo = result.Contacts[0].MobileNo1;
                                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.LandLineNo = result.Contacts[0].PhoneNo1;
                                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.AreaID = result.Contacts[0].AreaID;
                                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.SpecialInstructions = result.Contacts[0].SpecialInstructions;
                                    }
                                    if (activeInnerTab == "BillingDetails") {
                                        // assign selected contact detail into Billing details
                                        $scope.CRUDModel.Model.MasterViewModel.BillingDetails.ContactID = result.Contacts[0].ContactID;
                                        $scope.CRUDModel.Model.MasterViewModel.BillingDetails.ContactPerson = result.Contacts[0].FirstName;
                                        $scope.CRUDModel.Model.MasterViewModel.BillingDetails.BillingAddress = result.Contacts[0].AddressName;
                                        $scope.CRUDModel.Model.MasterViewModel.BillingDetails.MobileNo = result.Contacts[0].MobileNo1;
                                        $scope.CRUDModel.Model.MasterViewModel.BillingDetails.LandLineNo = result.Contacts[0].PhoneNo1;
                                        $scope.CRUDModel.Model.MasterViewModel.BillingDetails.AreaID = result.Contacts[0].AreaID;
                                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.SpecialInstructions = result.Contacts[0].SpecialInstructions;
                                    }
                                }
                            }
                        });
                    },
                    complete: function (result) {
                        $('#Overlay').hide();
                    }
                });
                break;
            case 'Transaction':
                $('#Overlay').show();
                $.ajax({
                    type: "GET",
                    url: view + '/Get/' + data.HeadIID,
                    contentType: "json",
                    success: function (result) {
                        $('#Overlay').hide();

                        $scope.$apply(function () {
                            $scope.CRUDModel.Model.MasterViewModel.DocumentType = "0";
                            $scope.CRUDModel.Model.MasterViewModel.TransactionNo = "";
                            $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo = data.HeadIID;
                            $scope.CRUDModel.Model.MasterViewModel.ReferenceTransaction = data.TransactionNo;

                            if (result.TransactionDetails.length > 0) {

                                if ($scope.CRUDModel.Model.DetailViewModel.SKUID == null) {
                                    var exists = new JSLINQ($scope.CRUDModel.Model.DetailViewModel)
                                        .Where(function (item) {
                                            return item.SKUID == null || item.SKUID.Key == null;
                                        });

                                    $scope.CRUDModel.Model.DetailViewModel.splice(exists);
                                }

                                $.each(result.TransactionDetails, function (index, item) {

                                    $scope.CRUDModel.Model.DetailViewModel.push({
                                        JobEntryDetailIID: 0, JobEntryHeadID: 0, SKUID: item.SKUID, Quantity: item.Quantity, BarCode: item.BarCode,
                                        Description: item.ProductSKU, IsRowSelected: false, CreatedBy: null, CreatedDate: null, UpdatedBy: null, UpdatedDate: null, TimeStamps: null
                                    });
                                });
                            }
                        });
                    },
                    complete: function (result) {
                        $('#Overlay').hide();
                    }
                });
                break;
            
            case 'ReadyForShipping':
                $('#Overlay').show();
                $.ajax({
                    type: "GET",
                    url: view + '/Get/' + data.JobEntryHeadIID,
                    contentType: "json",
                    success: function (result) {
                        $('#Overlay').hide();

                        $scope.$apply(function () {
                            $scope.CRUDModel.Model.DetailViewModel.push({
                                Description: result.DetailViewModel[0].Description, JobEntryHeadID: $scope.CRUDModel.Model.MasterViewModel.JobEntryHeadIID,
                                JobID: result.DetailViewModel[0].JobID, JobNumber: result.DetailViewModel[0].JobNumber,
                                InvoiceNo: result.DetailViewModel[0].InvoiceNo, IsRowSelected: false,
                                CityName: result.DetailViewModel[0].CityName, ShoppingCartID: result.DetailViewModel[0].ShoppingCartID
                            });
                        });
                    },
                    complete: function (result) {
                        $('#Overlay').hide();
                    }
                });
                break;
            case 'Login':
                $('#Overlay').show();
                $http({ method: 'Get', url: 'Securities/Login/Get/' + data.LoginIID })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel.Login.LoginIID = result.data.LoginID;
                        $scope.CRUDModel.ViewModel.Login.LoginUserID = result.data.LoginUserID;
                        $scope.CRUDModel.ViewModel.Login.LoginEmailID = result.data.LoginEmailID;
                        $scope.CRUDModel.ViewModel.Login.Status = result.data.UserStatus;
                        $scope.CRUDModel.ViewModel.Login.TimeStamps = result.data.TimeStamps;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;
            case 'StudentLogin':
                $('#Overlay').show();
                $http({ method: 'Get', url: 'Securities/Login/Get/' + data.LoginIID })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel.StudentLogin.LoginIID = result.data.LoginID;
                        $scope.CRUDModel.ViewModel.StudentLogin.LoginUserID = result.data.LoginUserID;
                        $scope.CRUDModel.ViewModel.StudentLogin.LoginEmailID = result.data.LoginEmailID;
                        $scope.CRUDModel.ViewModel.StudentLogin.Status = result.data.UserStatus;
                        $scope.CRUDModel.ViewModel.StudentLogin.TimeStamps = result.data.TimeStamps;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;
            case 'ParentLogin':
                $('#Overlay').show();
                $http({ method: 'Get', url: 'Securities/Login/Get/' + data.LoginIID })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel.ParentLogin.LoginIID = result.data.LoginID;
                        $scope.CRUDModel.ViewModel.ParentLogin.LoginUserID = result.data.LoginUserID;
                        $scope.CRUDModel.ViewModel.ParentLogin.LoginEmailID = result.data.LoginEmailID;
                        $scope.CRUDModel.ViewModel.ParentLogin.Status = result.data.UserStatus;
                        $scope.CRUDModel.ViewModel.ParentLogin.TimeStamps = result.data.TimeStamps;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;
            case 'StudentApplicationAdvancedSearch':
                $('#Overlay').show();
                $http({
                    method: 'Get', url: 'Schools/School/GetStudentFromApplicationID?applicationID='
                        + data.ApplicationIID
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel = result.data;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;
            case 'CandidateStudentApplicationAdvancedSearch':
                $('#Overlay').show();
                $http({
                    method: 'Get', url: 'Schools/School/GetStudentApplicationIDforCandidate?applicationID='
                        + data.ApplicationIID
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel = result.data;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;
            case 'LeadAdvancedSearchView':
                $('#Overlay').show();
                $http({
                    method: 'Get', url: 'Schools/School/GetLeadDataFromLeadID?leadID='
                        + data.LeadIID
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel = result.data;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;

            case 'ParentAdvancedSearch':
                $('#Overlay').show();
                $http({
                    method: 'Get', url: 'Schools/School/GetParentDetailFromParentID?parentID='
                        + data.ParentIID
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel.Guardians = result.data;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;

            case 'StudenAdvancedSearch':
                if ($scope.CRUDModel.ViewModel != undefined || $scope.CRUDModel.ViewModel != null) {
                    $('#Overlay').show();
                    $http({
                        method: 'Get', url: 'Schools/School/GetStudentDetailFromStudentID?StudentID='
                            + data.StudentIID
                    })
                        .then(function (result) {
                            $scope.CRUDModel.ViewModel.Student.Key = result.data.StudentIID;
                            $scope.CRUDModel.ViewModel.Student.Value = result.data.StudentFullName;
                            $scope.CRUDModel.ViewModel.AdmissionNumber = result.data.AdmissionNumber;
                            $scope.CRUDModel.ViewModel.ClassID = result.data.ClassID;
                            $scope.CRUDModel.ViewModel.SectionID = result.data.SectionID;
                            $scope.CRUDModel.ViewModel.Section = result.data.SectionName;
                            $scope.CRUDModel.ViewModel.Class = result.data.ClassName;
                            $scope.CRUDModel.ViewModel.Academic.Key = result.data.AcademicYearID;
                            $scope.CRUDModel.ViewModel.Academic.Value = result.data.AcademicYear;
                            $scope.CRUDModel.ViewModel.SchoolID = result.data.SchoolID;

                            $('#Overlay').hide();
                        }, function () {
                            $('#Overlay').hide();
                        });

                    $http({ method: 'Get', url: "Schools/School/GetSiblingDueDetailsFromStudentID?StudentID=" + data.StudentIID })
                        .then(function (result) {
                            $scope.CRUDModel.ViewModel.SiblingFeeInfo = result.data.Response;

                            hideOverlay();
                        }, function () {
                            hideOverlay();
                        });

                    $http({ method: 'Get', url: "Schools/School/FillPendingFees?classId=0&studentId=" + data.StudentIID })
                        .then(function (result) {
                            if (result == null || result.data.length == 0) {
                                $().showMessage($scope, $timeout, true, "There is no pending fee details available");
                                $scope.CRUDModel.ViewModel.FeeTypes = null;
                                $scope.CRUDModel.ViewModel.FeeInvoice = null;
                                hideOverlay();
                                return false;
                            }
                            //model.FeeTypes = result.data;
                            $scope.CRUDModel.ViewModel.FeeInvoice = result.data;

                            hideOverlay();
                        }, function () {
                            hideOverlay();
                        });
                }

                else if ($scope.CRUDModel.MasterViewModel != undefined || $scope.CRUDModel.MasterViewModel != null) {
                    $('#Overlay').show();
                    $http({
                        method: 'Get', url: 'Schools/School/GetStudentDetailFromStudentID?StudentID='
                            + data.StudentIID
                    })
                        .then(function (result) {
                            $scope.CRUDModel.Model.MasterViewModel.Student.Key = result.data.StudentIID;
                            $scope.CRUDModel.Model.MasterViewModel.StudentID = result.data.StudentIID;
                            $scope.CRUDModel.Model.MasterViewModel.Student.Value = result.data.StudentFullName;
                            $scope.CRUDModel.Model.MasterViewModel.ClassSectionDescription = result.data.ClassName + " " + result.data.SectionName;
                            $scope.CRUDModel.Model.MasterViewModel.EmailID = result.data.EmailID;
                            $scope.CRUDModel.Model.MasterViewModel.SchoolID = result.data.SchoolID;
                            $('#Overlay').hide();
                        }, function () {
                            $('#Overlay').hide();
                        });
                }

                break;
            case 'EmployeeAdvancedSearch':
                if ($scope.CRUDModel.ViewModel != undefined || $scope.CRUDModel.ViewModel != null) {
                    $('#Overlay').show();
                    $http({
                        method: 'Get', url: 'Schools/School/GetEmployeeFromEmployeeID?EmployeeID='
                            + data.EmployeeIID
                    })
                        .then(function (result) {
                            $scope.CRUDModel.ViewModel.Employee.Key = result.data.EmployeeIID;
                            $scope.CRUDModel.ViewModel.Employee.Value = result.data.EmployeeName;
                            $('#Overlay').hide();
                        }, function () {
                            $('#Overlay').hide();
                        });
                    break;
                }
                else if ($scope.CRUDModel.MasterViewModel != undefined || $scope.CRUDModel.MasterViewModel != null) {
                    $('#Overlay').show();
                    $http({
                        method: 'Get', url: 'Schools/School/GetEmployeeFromEmployeeID?EmployeeID='
                            + data.EmployeeIID
                    })
                        .then(function (result) {
                            $scope.CRUDModel.Model.MasterViewModel.Employee.Key = result.data.EmployeeIID;
                            $scope.CRUDModel.Model.MasterViewModel.Employee.Value = result.data.EmployeeName;
                            $('#Overlay').hide();
                        }, function () {
                            $('#Overlay').hide();
                        });
                }

            case 'LibraryStudentsAdvanceSearchView':
                $('#Overlay').show();
                $http({
                    method: 'Get', url: 'Schools/School/GetLibraryStudentFromStudentID?studentID='
                        + data.StudentID
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel.Student.Key = result.data.StudentID;
                        $scope.CRUDModel.ViewModel.Student.Value = result.data.StudentName;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;

            case 'LibraryStaffssAdvanceSearch':
                $('#Overlay').show();
                $http({
                    method: 'Get', url: 'Schools/School/GetLibraryStaffFromEmployeeID?employeeID='
                        + data.EmployeeID
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel.Employee.Key = result.data.EmployeeID;
                        $scope.CRUDModel.ViewModel.Employee.Value = result.data.StaffName;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;

            case 'ProductAdvancedSearchView':
                $scope.CRUDModel.Model.DetailViewModel[0].SKUID.Key = data.ProductSKUMapIID;
                $scope.CRUDModel.Model.DetailViewModel[0].SKUID.Value = data.SKUName;
                $scope.ModelStructure.DetailViewModel[0].SKUID.Key = data.ProductSKUMapIID;
                $scope.ModelStructure.DetailViewModel[0].SKUID.Value = data.SKUName;
                GetProductDetailsAndInventory($scope.ModelStructure.DetailViewModel[0], data.ProductSKUMapIID, $scope.ModelStructure.MasterViewModel.DocumentReferenceTypeID, $scope.CRUDModel.Model.MasterViewModel.Branch.Key);
                //GetProductDetailsAndInventory(currentRow, currentRow.SKUID.Key, $scope.ModelStructure.MasterViewModel.DocumentReferenceTypeID, branchID);
                break;
            case 'PhysicalStockEntryPick':
                $('#Overlay').show();
                var url = "Inventories/StockVerification/FillPhysicalStockDataINStockUpdate?ID=" + data.HeadIID;
                $http({
                    method: 'Get', url: url
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel = result.data;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;
            case 'CareerListing':
                $('#Overlay').show();

                var IsInterviewManagement = $scope.CRUDModel.ViewModel.IsInterviewManagement == true ? true : false;
                if (IsInterviewManagement) {
                    var url = "HR/JobOpening/GetShortListedApplicants?ID=" + data.JobIID;
                }
                else {
                    var url = "HR/JobOpening/GetApplicantsForShortList?ID=" + data.JobIID;
                }

                $http({
                    method: 'Get', url: url
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel = result.data;
                        if (result.data.ShortList.length <= 0)
                        {
                            $().showMessage($scope, $timeout, true, 'No records found');
                        }
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;
            case 'JDAdvanceSearch':
                $('#Overlay').show();
                var url = "HR/JobOpening/GetCandidateFromInterviewMap?ID=" + data.MapID;

                $http({
                    method: 'Get', url: url
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel = result.data;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;

            case 'ShortListAdvanceSearch':
                $('#Overlay').show();
                var url = "HR/JobOpening/GetApplicantsForShortList?ID=" + data.JobIID + "&isShortListed=" + true;
                $http({
                    method: 'Get', url: url
                })
                    .then(function (result) {
                        $scope.CRUDModel.ViewModel = result.data;
                        $('#Overlay').hide();
                    }, function () {
                        $('#Overlay').hide();
                    });
                break;

            case 'AssetTransferRequest':
            case 'AssetTransfer':
                $('#Overlay').show();
                $.ajax({
                    type: "GET",
                    url: viewFullPath + '/Get/' + data.HeadIID,
                    contentType: "json",
                    success: function (result) {
                        $('#Overlay').hide();

                        try {
                            result.MasterViewModel.TransactionHeadIID = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
                        }
                        catch (ex) {
                            result.MasterViewModel.TransactionHeadIID = 0;
                        }
                        result.MasterViewModel.DocumentType = $scope.CRUDModel.Model.MasterViewModel.DocumentType;
                        result.MasterViewModel.TransactionNo = $scope.CRUDModel.Model.MasterViewModel.TransactionNo;
                        result.MasterViewModel.ReferenceTransactionNo = data.TransactionNo;
                        $.each(result.DetailViewModel, function (index, item) {
                            item.TransactionDetailID = "0";
                            item.TransactionHead = "0";
                            $.each(item.TransactionSerialMaps, function (index, map) {
                                map.AssetTransactionSerialMapIID = "0";
                                map.TransactionDetailID = "0";
                            });
                        });
                        $scope.$apply(function () {
                            $scope.CRUDModel.Model = result;
                            $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionHeaderID = data.HeadIID;
                        });

                        //$scope.BranchChange(null, null);

                        if ($scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != 0 || $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != undefined || $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != null) {
                            $scope.CRUDModel.Model.DetailViewModel.IsDisableSelect2 = true;
                        }
                    }
                });
                break;
            case 'AccountTransactionSearch':
                $('#Overlay').show();
               
                $.ajax({
                    type: "GET",
                    url: "Accounts/ExpenditureAllocation/GetAccountTransactionsDetails?headID=" + data.HeadIID,
                    contentType: "json",
                    success: function (result) {
                        $('#Overlay').hide();

                        try {
                            $scope.$apply(function () {
                                $scope.CRUDModel.ViewModel.ExpenditureAllocTransactions = result;
                            });
                        }
                        catch (ex) {
                           
                        }
                        
                    }
                });
                break;
            case 'AssetPurchaseOrderAdvanceSearch':
                $('#Overlay').show();
                $.ajax({
                    type: "GET",
                    url: viewFullPath + '/Get/' + data.HeadIID,
                    contentType: "json",
                    success: function (result) {
                        $('#Overlay').hide();

                        try {
                            result.MasterViewModel.TransactionHeadIID = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
                        }
                        catch (ex) { result.MasterViewModel.TransactionHeadIID = 0; }
                        result.MasterViewModel.DocumentType = $scope.CRUDModel.Model.MasterViewModel.DocumentType;
                        result.MasterViewModel.TransactionNo = $scope.CRUDModel.Model.MasterViewModel.TransactionNo;
                        result.MasterViewModel.ReferenceTransactionNo = data.TransactionNo;
                        //result.MasterViewModel.TransactionDate = moment($scope.CRUDModel.Model.MasterViewModel.TransactionDate).format(_dateFormat.toUpperCase());

                        $.each(result.DetailViewModel, function (index, item) {
                            item.TransactionDetailID = "0";
                            item.TransactionHead = "0";
                        });
                        $scope.$apply(function () {
                            var additionalExpenseList = $scope.CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps;
                            var transEntitlementMap = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps;
                            $scope.CRUDModel.Model = result;
                            //$scope.CRUDModel.Model.MasterViewModel.Validity = moment(result.MasterViewModel.Validity).format(_dateFormat.toUpperCase());
                            $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionHeaderID = data.HeadIID;
                            $scope.CRUDModel.Model.MasterViewModel.DocumentStatus = null;
                            $scope.CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps = additionalExpenseList;
                            $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps = transEntitlementMap;
                            $scope.ChangeEntitlementAmount();

                        });

                        if ($scope.CRUDModel?.Name == "FOCSales") {
                            // Loop through the array and update the amount value to 0 for each item
                            $scope.CRUDModel.Model.DetailViewModel.forEach(function (item) {
                                item.Amount = 0;
                            });
                            $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps.forEach(function (item) {
                                item.Amount = 0;
                            });
                        }

                        $scope.BranchChange(null, null);

                        if ($scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != 0 || $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != undefined || $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != null) {
                            $scope.CRUDModel.Model.DetailViewModel.IsDisableSelect2 = true;
                        }
                    }
                });
                break;
        }
    };

    $scope.ChangeInventoryStudent = function ($event, $element, model) {
        if (model.Student.Key == null || model.Student.Key == "") return false;
        showOverlay();
        model.ClassSectionDescription = null;
        var url = "Schools/School/GetStudentDetailFromStudentID?StudentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.Model.MasterViewModel.ClassSectionDescription = result.data.ClassName + " " + result.data.SectionName;
                $scope.CRUDModel.Model.MasterViewModel.EmailID = result.data.EmailID;
                $scope.CRUDModel.Model.MasterViewModel.SchoolID = result.data.SchoolID;
                $scope.CRUDModel.Model.MasterViewModel.AdmissionNo = result.data.AdmissionNumber;
                $scope.CRUDModel.Model.MasterViewModel.StudentHouse = result.data.StudentHouse;

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.RichEditorClick = function (event, viewModel, fieldName, directModel) {
        event.preventDefault();
        var model;

        if (directModel) {
            model = { HtmlText: directModel.CultureValue, CallBack: "ApplyRichEditor" }
        }
        else {
            $scope.texteditor = $(event.currentTarget.closest('.controls')).find('textarea')[0];
            model = { HtmlText: $scope.texteditor.value, CallBack: "ApplyRichEditor" };
        }

        $http({ method: 'POST', url: 'Mutual/ShowCKEditor', contentType: "application/json;charset=utf-8", data: model })
            .then(function (content) {
                $('.overlaydiv').show();
                $('#dailogue').html($compile(content.data)($scope));
                var xpos = $("#dailogue").outerWidth() / 2;
                var ypos = $("#dailogue").outerHeight() / 2;
                $("#dailogue").css({ 'margin-top': '-' + ypos + 'px', 'margin-left': '-' + xpos + 'px' });
                $("#dailogue.popup").addClass('show');
                $("#dailogue").fadeIn();
                $("#dailogue").find('#apply').click(function () {
                    var htmlValue = $("#dailogue").find('#richtext').val();
                    if (directModel) {
                        directModel.CultureValue = htmlValue;
                    }
                    else {
                        GetFieldInstanceFromString(viewModel, fieldName, htmlValue);
                    }

                    $("#dailogue .cancel").click();
                });
            });
    };

    function GetFieldInstanceFromString(fieldField, field, value) {
        var obj = $scope;
        var colName = null;
        $.each(fieldField.split("."), function (index, val) {
            colName = val;

            if (obj[val] != null && val != field) {
                obj = obj[val];
            }

        });

        obj[colName] = value;
        return obj[colName];
    }

    $scope.CloseCKEditor = function () {
        $("#dailogue").fadeOut();
        $('#dailogue').html('');
        $('.overlaydiv').hide();
        $("#dailogue.popup").removeClass('show');
    };

    $scope.OnSchedulerTypeChangeSelect2 = function (select, index, model) {
        $('#Overlay').show();
        $.ajax({
            url: "Scheduler/GetEntityValues?type=" + select.selected.Key,
            type: 'GET',
            success: function (result) {
                $scope.$apply(function () {
                    $scope.LookUps["EntityType"] = result;
                });
            },
            complete: function (result) {
                $('#Overlay').hide();
            }
        });
    }

    $scope.OnGLAccountChangeSelect2 = function (select, index, model) {
        model.IsGLAccount = false;

        if (select.selected.Key == '' || select.selected.Key == null) return;

        $('#Overlay').show();

        $.ajax({
            url: "Accounts/AccountEntry/Get?ID=" + select.selected.Key,
            type: 'GET',
            success: function (result) {
                $scope.$apply(function () {
                    model.Name = result.AccountName;
                    model.IsGLAccount = true;
                    model.AccountID = result.AccountID;
                    model.AccountCode = reuslt.Alias;
                });
            },
            complete: function (result) {
                $('#Overlay').hide();
            }
        });
    };

    $scope.OnEntitlementChange = function (gridModel) {
        var model = gridModel;

    }
    // ------------------------ Purchase Additional Expense ------------------------
    $scope.RemoveAddExpenseGridRow = function (index, row, model) {
        model.splice(index, 1);

        if (index === 0) {
            $scope.InsertGridRow(index, row, model);
        }
        $scope.SetLandingCostLastCostPrice(null);
    };

    $scope.OnCurrencyChange = function (gridModel) {
        var model = gridModel;
        if ($scope.CurrencyDet == undefined || $scope.CurrencyDet.length == 0) {
            var url = "Schools/School/GetCurrencyDetails";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.CurrencyDet = result.data;

                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        if ($scope.CurrencyDet.length != 0) {
            var currencyData = $scope.CurrencyDet.find(x => x.CurrencyID == gridModel.Currency?.Key);
            if (currencyData != null) {
                $scope.decimalPlaces = currencyData.DecimalPrecisions || 0;
                gridModel.ExchangeRate = currencyData.ExchangeRate.toFixed(6);
            }

        }

    }
    $scope.OnPurchaseCurrencyChange = function (element) {

        if ((isNaN($scope.CRUDModel.Model.MasterViewModel.ForeignInvoiceAmount)
            || $scope.CRUDModel.Model.MasterViewModel.ForeignInvoiceAmount == null
            || $scope.CRUDModel.Model.MasterViewModel.ForeignInvoiceAmount == "")
            && (isNaN($scope.CRUDModel.Model.MasterViewModel.LocalInvoiceAmount)
                || $scope.CRUDModel.Model.MasterViewModel.LocalInvoiceAmount == null
                || $scope.CRUDModel.Model.MasterViewModel.LocalInvoiceAmount == "")) {
            if ($scope.CurrencyDet == undefined || $scope.CurrencyDet.length == 0) {
                var url = "Schools/School/GetCurrencyDetails";
                $http({ method: 'Get', url: url })
                    .then(function (result) {
                        $scope.CurrencyDet = result.data;
                        // $scope.LookUps.PurchaseCurrency = $scope.CurrencyDet;

                    }, function () {
                        var currencyData = $scope.CurrencyDet.find(x => x.CurrencyID == $scope.CRUDModel.Model.MasterViewModel.Currency?.Key);
                        if (currencyData != null) {
                            $scope.decimalPlaces = currencyData.DecimalPrecisions || 0;
                            $scope.CRUDModel.Model.MasterViewModel.ExchangeRate = Number(currencyData.ExchangeRate ?? 0).toFixed(6);
                        }

                    });
            }
            else {
                var currencyData = $scope.CurrencyDet.find(x => x.CurrencyID == $scope.CRUDModel.Model.MasterViewModel.Currency?.Key);
                if (currencyData != null) {
                    $scope.CRUDModel.Model.MasterViewModel.ExchangeRate = Number(currencyData.ExchangeRate ?? 0).toFixed(6);
                    $scope.CurrencyExchangeRate = $scope.CRUDModel.Model.MasterViewModel.ExchangeRate;
                    $scope.decimalPlaces = currencyData.DecimalPrecisions || 0;
                }
            }
            if (isNaN($scope.CRUDModel.Model.MasterViewModel.ExchangeRate)) {
                $scope.GetAllPurchaseLocalAmount(null);
            }
        }
    }
    $scope.GetPurchaseExchangeRate = function (element) {

        if ($scope.CurrencyDet == undefined || $scope.CurrencyDet.length == 0) {
            var url = "Schools/School/GetCurrencyDetails";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.CurrencyDet = result.data;
                    hideOverlay();
                }, function () {
                    var currencyData = $scope.CurrencyDet.find(x => x.CurrencyID == $scope.CRUDModel.Model.MasterViewModel.Currency?.Key);
                    if (currencyData != null) {
                        $scope.decimalPlaces = currencyData.DecimalPrecisions || 0;
                        return Number(currencyData.ExchangeRate || 0).toFixed(6);
                    }

                });
        }

        var currencyData = $scope.CurrencyDet.find(x => x.CurrencyID == $scope.CRUDModel.Model.MasterViewModel.Currency?.Key);
        if (currencyData != null) {
            $scope.decimalPlaces = currencyData.DecimalPrecisions || 0;
            return Number(currencyData.ExchangeRate ?? 0).toFixed(6);
        }
        return 1;
    };
    $scope.OnAdditionalExpenseChange = function (gridModel) {
        var model = gridModel;
        if (model.IsAffectSupplier != true && gridModel.AdditionalExpense.Key != undefined) {
            $scope.getProvisionalAccountByAdditionalExpense(gridModel.AdditionalExpense.Key, gridModel);

        }
    }
    $scope.GetLocalAmount = function (gridModel) {
        var model = gridModel;
        gridModel.LocalAmount = Number((gridModel.ExchangeRate ?? 0) * (gridModel.ForeignAmount ?? 0)).toFixed(3);
        $scope.SetLandingCostLastCostPrice(null);
    }
    $scope.GetExchangeRate = function (element) {

        if (!isNaN($scope.CRUDModel.Model.MasterViewModel.ForeignInvoiceAmount) && $scope.CRUDModel.Model.MasterViewModel.ForeignInvoiceAmount != null && $scope.CRUDModel.Model.MasterViewModel.ForeignInvoiceAmount != "" && !isNaN($scope.CRUDModel.Model.MasterViewModel.LocalInvoiceAmount) && $scope.CRUDModel.Model.MasterViewModel.LocalInvoiceAmount != null && $scope.CRUDModel.Model.MasterViewModel.LocalInvoiceAmount != "") {
            $scope.CRUDModel.Model.MasterViewModel.ExchangeRate = (($scope.CRUDModel.Model.MasterViewModel.LocalInvoiceAmount ?? 0) / ($scope.CRUDModel.Model.MasterViewModel.ForeignInvoiceAmount ?? 0)).toFixed(6);

        }
        else {
            $scope.CRUDModel.Model.MasterViewModel.ExchangeRate = $scope.CurrencyExchangeRate;
        }
        $scope.GetAllPurchaseLocalAmount(null);
    }

    $scope.GetAllPurchaseLocalAmount = function (element) {

        if (!isNaN($scope.CRUDModel.Model.MasterViewModel.ExchangeRate) && $scope.CRUDModel.Model.DetailViewModel.length > 0) {
            $scope.CRUDModel.Model.DetailViewModel.forEach(x => {
                if (parseFloat(x.ForeignRate ?? 0) == 0 && parseFloat(x.UnitPrice ?? 0)) {
                    x.ForeignRate = (Number(x.UnitPrice ?? 0) / Number($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0)).toFixed(3);
                }
                if (!isNaN(x.ForeignRate)) {
                    x.UnitPrice = (Number($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0) * Number(x.ForeignRate ?? 0)).toFixed(3);
                    x.ExchangeRate = Number($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0).toFixed(6)
                    x.ForeignAmount = (Number(x.ForeignRate ?? 0) * Number(x.Quantity ?? 0)).toFixed(3);
                    x.Amount = (Number(x.UnitPrice ?? 0) * Number(x.Quantity ?? 0)).toFixed(3);
                }
                else {
                    x.UnitPrice = 0;
                    x.Amount = 0;
                    x.ForeignAmount = 0;
                }

            });
            $scope.SetLandingCostLastCostPrice(null);
            $scope.ChangeEntitlementAmount();
        }

    }
    $scope.GetPurchaseLocalAmount = function (gridModel) {
        var model = gridModel;
        if (!isNaN(model.ForeignRate)) {
            model.ForeignAmount = (Number(model.ForeignRate ?? 0) * Number(model.Quantity ?? 0)).toFixed(3);
            model.UnitPrice = (Number($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0) * Number(model.ForeignRate ?? 0)).toFixed(3);
            model.ExchangeRate = Number($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0).toFixed(6);
            model.Amount = (Number(model.UnitPrice ?? 0) * Number(model.Quantity ?? 0)).toFixed(3);
        }
        else if (model.IsScreen == "PurchaseRequest") {
            model.Amount = (Number(model.ExpectedUnitPrice ?? 0) * Number(model.Quantity ?? 0)).toFixed(3);
        }
        else {
            model.UnitPrice = 0;
            model.Amount = 0;
            model.ForeignAmount = 0;
            model.ExchangeRate = Number($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0).toFixed(6);
        }

        $scope.SetLandingCostLastCostPrice(null);
        $scope.ChangeEntitlementAmount();
    }
    $scope.SetProvisionalAccount = function (gridModel) {

        if (gridModel.IsAffectSupplier == true) {
            var supplierAccount = [];
            if ($scope.SupplierAccountID == null) {
                var supplierID = $scope.CRUDModel.Model.MasterViewModel.Supplier.Key;
                setSupplierProvisionalAccount(gridModel, supplierID);
            }
            else {
                supplierAccount.push({ Key: $scope.SupplierAccountID, Value: $scope.SupplierAccountName });
                $scope.LookUps.ProvisionalAccount = supplierAccount;
                gridModel.ProvisionalAccount = $scope.LookUps.ProvisionalAccount.find(x => x.Key == $scope.SupplierAccountID);
            }
        }
        else {
            $scope.getProvisionalAccountByAdditionalExpense(gridModel.AdditionalExpense.Key, gridModel);
        }
        if (gridModel.ExchangeRate == null || gridModel.ExchangeRate == '')
            gridModel.ExchangeRate = Number(1).toFixed(6);
    }
    setSupplierProvisionalAccount = function (gridModel, supplierId) {

        var uri = "Mutual/GetAccountBySupplierID?supplierID=" + supplierId;
        $.ajax({
            url: uri,
            type: 'GET',
            success: function (result) {
                $scope.$apply(function () {
                    $scope.SupplierAccountID = result.AccountID;
                    $scope.SupplierAccountName = result.AccountName;
                    // $scope.LookUps[lookUpName].push({ Key: item.Key, Value: item.Value });
                });
            },
            complete: function (result) {
                var supplierAccount = [];
                supplierAccount.push({ Key: $scope.SupplierAccountID, Value: $scope.SupplierAccountName });
                $scope.LookUps.ProvisionalAccount = supplierAccount;
                gridModel.ProvisionalAccount = $scope.LookUps.ProvisionalAccount.find(x => x.Key == $scope.SupplierAccountID);
            }
        });
    }
    $scope.getProvisionalAccountByAdditionalExpense = function (additionalExpenseID, gridModel) {
        $scope.LookUps["ProvisionalAccount"] = [];
        $scope.ProvisionalAccountList = [];
        if (gridModel.ExchangeRate == null || gridModel.ExchangeRate == '')
            gridModel.ExchangeRate = Number(1).toFixed(6);
        var uri = "Mutual/GetProvisionalAccountByAdditionalExpense?additionalExpenseID=" + additionalExpenseID;
        $.ajax({
            url: uri,
            type: 'GET',
            success: function (result) {
                $scope.$apply(function () {
                    $scope.ProvisionalAccountList = result;
                    $.each(result, function (index, item) {
                        $scope.LookUps["ProvisionalAccount"].push({ Key: item.ProvisionalAccountID.toString(), Value: item.AccountName });
                    });

                    if ($scope.ProvisionalAccountList.length != 0) {
                        var defaultVal = $scope.ProvisionalAccountList.find(x => x.IsDefault == true);
                        if (defaultVal != null) {
                            gridModel.ProvisionalAccount = $scope.LookUps.ProvisionalAccount.find(x => x.Key == defaultVal.ProvisionalAccountID, x.Value == defaultVal.AccountName);
                        }
                    }
                });
            }
        });
    }

    $scope.getAccountBySupplierId = function (supplierId) {
        var uri = "Mutual/GetAccountBySupplierID?supplierID=" + supplierId;
        $.ajax({
            url: uri,
            type: 'GET',
            success: function (result) {
                $scope.$apply(function () {
                    $scope.SupplierAccountID = result.AccountID;
                    $scope.SupplierAccountName = result.AccountName;
                    // $scope.LookUps[lookUpName].push({ Key: item.Key, Value: item.Value });
                });
            }
        });
    }

    $scope.SetLandingCostLastCostPrice = function (gridModel) {
        if ($scope.CRUDModel.Model.MasterViewModel.hasOwnProperty("AdditionalExpTransMaps") == true) {
            var totalAdditionalExpense = Object.keys($scope.CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps).reduce(function (sum, key) {
                return sum + parseFloat(($scope.CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps[key].LocalAmount ?? 0));
            }, 0);
            var subtotal = GetSubTotal(gridModel);
            //------ Local Discount

            if (!isNaN($scope.CRUDModel.Model.MasterViewModel.DiscountPercentage) &&
                $scope.CRUDModel.Model.MasterViewModel.DiscountPercentage != undefined
                && $scope.CRUDModel.Model.MasterViewModel.DiscountPercentage != ''
            ) {
                $scope.CRUDModel.Model.MasterViewModel.Discount = (Number(subtotal ?? 0) * (Number($scope.CRUDModel.Model.MasterViewModel.DiscountPercentage ?? 0) / 100)).toFixed(3);
            }
            else
                $scope.CRUDModel.Model.MasterViewModel.Discount = 0.000;

            var totalInvoiceDiscount = ($scope.CRUDModel.Model.MasterViewModel.Discount ?? 0);
            if ($scope.CRUDModel.Model.DetailViewModel.length > 0) {
                $scope.CRUDModel.Model.DetailViewModel.forEach(x => {
                    if ((x.Amount ?? 0) != 0 && (subtotal ?? 0) != 0) {
                        var lineDiscount = 0;
                        if (!isNaN(totalInvoiceDiscount) && totalInvoiceDiscount != 0) {
                            lineDiscount = Number(((totalInvoiceDiscount ?? 0) / (Number(subtotal ?? 0))) * (Number(x.Amount ?? 0))).toFixed(3);
                        }
                        else
                            lineDiscount = 0.000;

                        if (!isNaN(totalAdditionalExpense) && totalAdditionalExpense != 0) {
                            x.LandingCost = Number(((totalAdditionalExpense ?? 0) / (Number(subtotal ?? 0))) * (Number(x.Amount ?? 0))).toFixed(3);
                        }
                        else
                            x.LandingCost = 0.000;

                        x.LastCostPrice = Number(((Number(x.Amount ?? 0)) + ((Number(x.LandingCost ?? 0)) - lineDiscount)) / (Number(x.Quantity ?? 0))).toFixed(3);
                    }
                    else {
                        x.LandingCost = 0.000;
                        x.LastCostPrice = 0.000;
                    }

                });
            }
        }
    }

    GetSubTotal = function (gridModel) {
        var subTotal = $scope.CRUDModel.Model.DetailViewModel.reduce(function (sum, current) {
            return Number(sum ?? 0) + Number(current.Amount ?? 0);
        }, 0); //- ($scope.CRUDModel.Model.MasterViewModel.Discount ?? 0);

        return Number(subTotal ?? 0).toFixed(3);
    }
    $scope.LoadUnit = function (rowIndex) {

        if ($scope.CRUDModel.Model.DetailViewModel[rowIndex].UnitDTO != undefined && $scope.CRUDModel.Model.DetailViewModel[rowIndex].UnitDTO.length > 0) {
            $scope.LookUps['Unit'] = [];
            var fitlers = new JSLINQ($scope.CRUDModel.Model.DetailViewModel[rowIndex].UnitDTO).Where(function (unit) {
                $scope.LookUps['Unit'].push(unit);
            });
        }
        else {
            if ($scope.CRUDModel.Model.DetailViewModel[rowIndex].UnitGroupID != null)
                $scope.GetUnitDetailsByUnitGroup($scope.CRUDModel.Model.DetailViewModel[rowIndex], $scope.CRUDModel.Model.DetailViewModel[rowIndex].UnitGroupID, null);
        }

    }

    $scope.OnUnitChange = function (gridModel) {
        var model = gridModel;

        (gridModel.Unit.Key != undefined)
        {
            if (gridModel.UnitList != null && gridModel.UnitList.length > 0) {
                gridModel.UnitID = gridModel.Unit.Key;
                unitDat = gridModel.UnitList.find(x => x.UnitID == gridModel.Unit.Key);
                if (unitDat != undefined) {

                    gridModel.Fraction = unitDat.Fraction;
                }
                else {
                    gridModel.Fraction = 0;
                }
            }
            else {
                if (gridModel.UnitGroupID != null && gridModel.Unit.Key != null)
                    $scope.GetUnitDataByUnitGroup(gridModel, gridModel.UnitGroupID, gridModel.Unit.Key);
            }
        }
    }

    // ------------------------END Purchase Additional Expense Other New changes------------------------

    $scope.OnCreditNoteChange = function (gridModel) {
        var model = gridModel;
        gridModel.ReferenceNo = gridModel.CreditNoteNumber?.Value;
        if ($scope.CreditnoteNumberDet != undefined) {
            var crnNoteData = $scope.CreditnoteNumberDet.find(x => x.SchoolCreditNoteIID == gridModel.CreditNoteNumber?.Key);
            if (crnNoteData != null) {
                gridModel.Amount = crnNoteData.Amount;
            }
        }
    }
    $scope.OnChangeSelect2 = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {

        if (multiplesingle == "multiple") {

            if (dataType == "Numeric") {
                if (control.selected[0].Key != undefined && control.selected[0].Key != null && control.selected[0].Key != '') {
                    $scope.CRUDModel[model] = Number(control.selected[0].Key);
                }                
            }
            else {
                $scope.CRUDModel[model] = control.selected[0].Key;
            }
        }
        else {
            if (dataType == "Numeric") {

                if (control.selected) {
                    $scope.CRUDModel[model] = Number(control.selected.Key);
                }

                /* Add code to bind table based on product selection */
                bindCustomerExternalSetting(model);
                /* add code to bind delivery detail in sales order customer selection */
                bindDeliveryDetail(model);

                // get City By CountryId
                if (model.toLowerCase() === "country") {
                    var countryId = Number(control.selected.Key);
                    if (countryId != undefined && countryId != null) {
                        $scope.getCityByCountryId(countryId);
                    }
                    $scope.CRUDModel.ViewModel.City = null;
                }

                // get Area By CityId
                if (model.toLowerCase() === "city") {
                    var cityId = Number(control.selected.Key);
                    if (cityId != undefined && cityId != null) {
                        $scope.getAreaByCityId(cityId);
                    }
                }

                // get Supplier AccountID
                if (model.toLowerCase() === "supplier") {
                    var supplierId = Number(control.selected.Key);
                    if (supplierId != undefined && supplierId != null) {
                        $scope.getAccountBySupplierId(supplierId);
                    }
                }

            }
            else {
                $scope.CRUDModel[model] = control.selected.Key;
            }
        }

    }

    var searchHandler = null;

    $scope.GetClaims = function (claimType) {
        return $scope.LookUps['Claims_' + claimType];
    }

    $scope.GetSettingValue = function (type, prefix) {
        return $scope.LookUps[prefix + '_' + type];
    }

    // Below functions are related to the grid in the Purchase order screen.
    $scope.RefreshSelect2 = function (dataUrl, select, dataSource, currentRow, isLoadOnce) {

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


        if (dataSource == "LookUps.SerialList") {
            url = url + "&productSKUMapID=" + currentRow.ProductSKUMapID;
        }

        if (select.search == undefined) {
            url = url + "&searchText=";
        }
        else {
            url = url + "&searchText=" + select.search;
        }
        //$('#Overlay').show();

        searchHandler = $.ajax({
            url: url,
            type: 'GET',
            success: function (result) {
                var lookUpName = result.LookUpName.split('.')[1];
                $scope.LookUps[lookUpName] = [];

                $.each(result.Data, function (index, item) {
                    if (item.ProductSKUMapIID == undefined) {
                        $scope.LookUps[lookUpName].push({ Key: item.Key, Value: item.Value });
                    } else {
                        $scope.LookUps[lookUpName].push({ Key: item.ProductSKUMapIID, Value: item.ProductSKU });
                    }
                });
            },
            complete: function (result) {
                // $('#Overlay').hide();
            }
        });
    };

    $scope.onSelected = function (item) {
        setTimeout(function () {
            var nextInput = item.searchInput.closest('td').nextAll().children('input:first');
            nextInput.focus();
            nextInput.select();
        });
    };

    function GetProductDetailsAndInventory(currentRow, skuID, documentReferenceTypeID, branchID) {
        //$('#Overlay').show();

        $.ajax({
            url: "Inventories/InventoryDetails/GetProductSKUInventoryDetail?skuIID=" + skuID + "&documentReferenceTypeID=" +
                documentReferenceTypeID + "&branchID=" + branchID,
            type: 'GET',
            success: function (productItem) {
                if (productItem != null) {

                    if (currentRow.hasOwnProperty("BranchID") == true)
                        currentRow.BranchID = productItem.BranchID;
                    if (currentRow.hasOwnProperty("ProductCode") == true)
                        currentRow.ProductCode = productItem.ProductCode;
                    if (currentRow.hasOwnProperty("BarCode") == true)
                        currentRow.BarCode = productItem.BarCode;
                    if (currentRow.hasOwnProperty("PartNo") == true)
                        currentRow.PartNo = productItem.PartNo;
                    if (currentRow.hasOwnProperty("Quantity") == true) {
                        currentRow.AvailableQuantity = (productItem.Quantity ?? 0);
                        currentRow.Quantity = 1;
                    }
                    if (currentRow.hasOwnProperty("ProductImage") == true)
                        currentRow.ProductImage = productItem.ImageFile;
                    if (currentRow.hasOwnProperty("UnitPrice") == true)
                        currentRow.UnitPrice = productItem.UnitPrice ?? 0;
                    if (currentRow.hasOwnProperty("CostPrice") == true)
                        currentRow.CostPrice = productItem.CostPrice ?? 0;
                    if (currentRow.hasOwnProperty("Amount") == true)
                        currentRow.Amount = currentRow.IsScreen == "FOCSales" ? 0 : (productItem.UnitPrice ?? 0) * (currentRow.Quantity ?? 0);
                    if (currentRow.hasOwnProperty("WarrantyDate") == true)
                        currentRow.WarrantyDate = productItem.WarrantyDate;
                    // Add or Modify the serial Number
                    if (currentRow.hasOwnProperty("IsSerialNumber") == true) {
                        currentRow.IsSerialNumber = productItem.IsSerialNumber;
                        currentRow.SKUDetails = productItem.IsSerialNumber == true ? [] : null;
                    }
                    if (currentRow.hasOwnProperty("IsSerialNumberOnPurchase") == true) {
                        currentRow.IsSerialNumberOnPurchase = productItem.IsSerialNumberOnPurchase;
                        currentRow.SKUDetails = productItem.IsSerialNumberOnPurchase == true ? [] : null;
                    }
                    if (currentRow.hasOwnProperty("IsSerailNumberAutoGenerated") == true) {
                        currentRow.IsSerailNumberAutoGenerated = productItem.IsSerailNumberAutoGenerated;
                        currentRow.SKUDetails = productItem.IsSerailNumberAutoGenerated == true ? [] : null;
                    }
                    if (currentRow.hasOwnProperty("ProductLength") == true) {
                        currentRow.ProductLength = productItem.ProductLength;
                        currentRow.SKUDetails = productItem.ProductLength == true ? [] : null;
                    }
                    if (currentRow.hasOwnProperty("DeliveryCharge") == true) {
                        currentRow.DeliveryCharge = productItem.DeliveryCharge;
                        currentRow.SKUDetails = productItem.DeliveryCharge == true ? [] : null;
                    }
                    if (currentRow.hasOwnProperty("Weight") == true) {
                        currentRow.Weight = productItem.Weight;
                        currentRow.SKUDetails = productItem.Weight == true ? [] : null;
                    }
                    if (currentRow.hasOwnProperty("LocationName") == true) {
                        currentRow.LocationBarcode = productItem.LocationBarcode;
                        currentRow.LocationName = productItem.LocationName;
                        currentRow.LocationID = productItem.LocationID;
                    }

                    if (currentRow.hasOwnProperty("IsSerialNumber") == true) {
                        currentRow.IsSerialNumber = productItem.IsSerialNumber;
                        currentRow.SKUDetails = productItem.IsSerialNumber == true ? [] : null;
                    }

                    if (currentRow.IsScreen == "PurchaseInvoice" || currentRow.IsScreen == "PurchaseOrder" || currentRow.IsScreen == "GRN" || currentRow.IsScreen == "PurchaseReturn" || currentRow.IsScreen == "ServiceEntry" || currentRow.IsScreen == "BranchTransfer" || currentRow.IsScreen == "AssetPurchaseOrder") {
                        currentRow.UnitPrice = 0;
                        currentRow.UnitGroupID = productItem.PurchaseUnitGroupID;
                        if (productItem.PurchaseUnitGroupID != null) {
                            $scope.GetUnitDataByUnitGroup(currentRow, productItem.PurchaseUnitGroupID, productItem.PurchaseUnitID);
                        }
                        else {
                            $scope.LookUps.Unit = null;
                            currentRow.Unit.Key = null;
                            currentRow.Unit.Value = null;
                            currentRow.Fraction = 0;
                            $().showMessage($scope, $timeout, true, 'Please set Purchase Unit Group and Purchase Unit for the product');
                            return;
                        }
                        if (currentRow.hasOwnProperty("ForeignRate")) {
                            if (parseFloat(currentRow.ForeignRate ?? 0) == 0 && parseFloat(currentRow.UnitPrice ?? 0)) {
                                currentRow.ForeignRate = Number(currentRow.UnitPrice ?? 0) / Number($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0).toFixed(3);
                            }
                            currentRow.ExchangeRate = parseFloat($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0).toFixed(6);

                            $scope.GetPurchaseLocalAmount(currentRow);
                        }
                        else {
                            $scope.ChangeEntitlementAmount();
                        }


                        if (currentRow.Amount != 0)
                            $scope.SetLandingCostLastCostPrice(currentRow);
                    }
                    else if (currentRow.IsScreen == "PurchaseRequest") {
                        currentRow.UnitGroupID = productItem.PurchaseUnitGroupID;
                        if (productItem.PurchaseUnitGroupID != null) {
                            $scope.GetUnitDataByUnitGroup(currentRow, productItem.PurchaseUnitGroupID, productItem.PurchaseUnitID);
                        }
                    }



                    if (currentRow.IsScreen == "SalesOrder" || currentRow.IsScreen == "SalesInvoice" || currentRow.IsScreen == "SalesReturn" || currentRow.IsScreen == "FOCSales" || currentRow.IsScreen == "SalesInvoiceLite") {

                        currentRow.UnitGroupID = productItem.SellingUnitGroupID;
                        if (productItem.SellingUnitGroupID != null) {
                            $scope.GetUnitDataByUnitGroup(currentRow, productItem.SellingUnitGroupID, productItem.SellingUnitID);
                        }
                        else {
                            $scope.LookUps.Unit = null;
                            currentRow.Unit.Key = null;
                            currentRow.Unit.Value = null;
                            currentRow.Fraction = 0;
                            $().showMessage($scope, $timeout, true, 'Please set selling Unit Group and selling Unit for the product');
                            return;
                        }
                        currentRow.ExchangeRate = parseFloat($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0).toFixed(6);
                        //if (currentRow.hasOwnProperty("ForeignRate")) {
                        //    if (parseFloat(currentRow.ForeignRate ?? 0) == 0 && parseFloat(currentRow.UnitPrice ?? 0)) {
                        //        currentRow.ForeignRate = Number(currentRow.UnitPrice ?? 0) / Number($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0)
                        //    }
                        //    currentRow.ExchangeRate = parseFloat($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0);

                        //    $scope.GetPurchaseLocalAmount(currentRow);
                        //}
                        //else {
                        //    $scope.ChangeEntitlementAmount();
                        //}

                        $scope.ChangeEntitlementAmount();
                    }

                    if (currentRow.hasOwnProperty("TaxTemplate") == true && productItem.TaxTemplateID != undefined && productItem.TaxTemplateID != null) {
                        currentRow.TaxTemplate = productItem.TaxTemplateID.toString();
                    }

                    if (currentRow.hasOwnProperty("TaxPercentage") == true) {
                        currentRow.TaxPercentage = productItem.TaxPercentage;
                    }
                }
            },
            complete: function (result) {
                $('#Overlay').hide();
            }
        });
    }

    $scope.GetUnitDetailsByUnitGroup = function (currentRow, PurchaseUnitGroupID, PurchaseUnitID) {
        $.ajax({
            type: "GET",
            url: 'Inventories/InventoryDetails/GetUnitDataByUnitGroup?groupID=' + PurchaseUnitGroupID,
            success: function (result) {
                $scope.$apply(function () {

                    currentRow.UnitList = result;
                    var unitKeyData = []
                    currentRow.UnitList.forEach(x => {
                        unitKeyData.push({ Key: x.UnitID, Value: x.UnitCode });

                    });
                    $scope.LookUps.Unit = unitKeyData;
                    currentRow.UnitDTO = unitKeyData;
                });
            },
            complete: function (result) {
            }
        });
    }
    $scope.GetUnitDataByUnitGroup = function (currentRow, PurchaseUnitGroupID, PurchaseUnitID) {
        $scope.UnitList = [];
        $scope.LookUps.Unit = [];
        currentRow.Unit.Key = null;
        currentRow.Unit.Value = null;
        currentRow.Fraction = 0;
        currentRow.UnitID = null;
        $.ajax({
            type: "GET",
            url: 'Inventories/InventoryDetails/GetUnitDataByUnitGroup?groupID=' + PurchaseUnitGroupID,
            success: function (result) {
                $scope.$apply(function () {

                    currentRow.UnitList = result;
                    var unitKeyData = []
                    currentRow.UnitList.forEach(x => {
                        unitKeyData.push({ Key: x.UnitID, Value: x.UnitCode });

                    });
                    $scope.LookUps.Unit = unitKeyData;
                    currentRow.UnitDTO = unitKeyData;
                });
            },
            complete: function (result) {
                $scope.$apply(function () {
                    if ($scope.LookUps.Unit.length > 0 && PurchaseUnitID != null) {
                        var unitDat = currentRow.UnitList.find(x => x.UnitID == PurchaseUnitID);
                        if (unitDat != undefined) {
                            currentRow.Unit.Key = unitDat.UnitID;
                            currentRow.Unit.Value = unitDat.UnitCode;
                            currentRow.Fraction = unitDat.Fraction ?? 0;
                            currentRow.UnitID = unitDat.UnitID;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        return false;
                    }
                });

            }

        });
    }
    $scope.OnChangeProductSelect2 = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {

        if (!currentRow.SKUID) return;

        currentRow.Description = currentRow.SKUID.Value;

        var branchID = null;

        if ($scope.CRUDModel.Model.MasterViewModel.Branch == undefined) {
            branchID = 0;
        } else
            if ($scope.CRUDModel.Model.MasterViewModel.Branch.Key == undefined) {
                branchID = $scope.CRUDModel.Model.MasterViewModel.Branch;
            }
            else {
                branchID = $scope.CRUDModel.Model.MasterViewModel.Branch.Key;
            }


        GetProductDetailsAndInventory(currentRow, currentRow.SKUID.Key, $scope.ModelStructure.MasterViewModel.DocumentReferenceTypeID, branchID);
        // adding a new row in the grid.
        if (rowIndex == $scope.CRUDModel.Model.DetailViewModel.length) {
            var vm = angular.copy($scope.CRUDModel.DetailViewModel);
            $scope.CRUDModel.Model.DetailViewModel.push(vm);
        }

        if ($scope.CRUDModel.Model.MasterViewModel.Allocations != null) {
            $scope.UpdateAllocations(currentRow, rowIndex);
        }

    }

    $scope.OnDefaultEmptyChange = function () {
    }

    $scope.UpdateDetailGridValues = function (field, currentRow, rowIndex) {

        if (currentRow == null || currentRow == undefined)
            return;

        switch (field) {
            case "Credit":
            case "Debit":

                if (currentRow.Credit == null) {
                    currentRow.Credit = 0;
                }

                if (currentRow.Debit == null) {
                    currentRow.Debit = 0;
                }

                currentRow.Amount = (-1 * parseFloat(currentRow.Credit)) + parseFloat(currentRow.Debit);

                break;
            case "UnitPrice":
            case "Quantity":
            case "SellingPrice":
            case "CostPrice":
                if (!isNaN(parseFloat(currentRow.UnitPrice)) && !isNaN(parseInt(currentRow.Quantity)) || !isNaN(parseFloat(currentRow.ItemCostPrice)) && !isNaN(parseInt(currentRow.ItemSellingPrice))) {
                    if ($scope.CRUDModel?.Name == "FOCSales") {
                        currentRow.Amount = 0;
                    }
                    else {
                        currentRow.Amount = parseInt(currentRow.Quantity) * parseFloat(currentRow.UnitPrice);
                    }

                    if (currentRow.IsScreen == "PurchaseRequest") {
                        currentRow.Amount = parseInt(currentRow.Quantity) * parseFloat(currentRow.ExpectedUnitPrice);
                    }

                    if (!isNaN(parseFloat(currentRow.ItemCostPrice)) && !isNaN(parseInt(currentRow.ItemSellingPrice))) {
                        currentRow.CostPrice = parseInt(currentRow.Quantity) * parseFloat(currentRow.ItemCostPrice);
                        currentRow.SellingPrice = parseInt(currentRow.Quantity) * parseFloat(currentRow.ItemSellingPrice);
                    }
                    if ($scope.CRUDModel?.Name == "SalesReturn" && (parseInt(currentRow.Quantity) > parseInt(currentRow.ActualQuantity))) {
                        //currentRow.Amount = 0;
                        currentRow.Quantity = currentRow.ActualQuantity;
                        currentRow.Amount = parseInt(currentRow.ActualQuantity) * parseFloat(currentRow.UnitPrice);
                        $().showMessage($scope, $timeout, true, 'Quantity cannot exceed more than the invoiced quantity.');
                    }

                    if (currentRow.hasOwnProperty("ForeignRate")) {
                        if (parseFloat(currentRow.ForeignRate ?? 0) == 0 && parseFloat(currentRow.UnitPrice ?? 0) != 0 && ($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0) != 0) {
                            currentRow.ForeignRate = parseFloat(currentRow.UnitPrice) / parseFloat($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0).toFixed(3);
                        }
                        currentRow.ExchangeRate = parseFloat($scope.CRUDModel.Model.MasterViewModel.ExchangeRate ?? 0).toFixed(6);
                        $scope.GetPurchaseLocalAmount(currentRow);
                    }
                    //else {
                    //    $scope.ChangeEntitlementAmount();
                    //}

                    $scope.ChangeEntitlementAmount();

                    if (currentRow.Weight != null & ((parseInt(currentRow.Quantity) * parseInt(currentRow.Weight)) <= 5000)) {
                        var branchID = null;

                        if ($scope.CRUDModel.Model.MasterViewModel.Branch.Key == undefined) {
                            branchID = $scope.CRUDModel.Model.MasterViewModel.Branch;
                        }
                        else {
                            branchID = $scope.CRUDModel.Model.MasterViewModel.Branch.Key;
                        }

                        $('#Overlay').show();

                        $.ajax({
                            url: "Inventory/GetProductSKUInventoryDetail?skuIID=" + currentRow.SKUID.Key + "&documentReferenceTypeID=" + $scope.ModelStructure.MasterViewModel.DocumentReferenceTypeID + "&branchID=" + branchID,
                            type: 'GET',
                            success: function (productItem) {
                                if (currentRow.hasOwnProperty("DeliveryCharge") == true) {
                                    currentRow.DeliveryCharge = productItem.DeliveryCharge;
                                    currentRow.SKUDetails = productItem.DeliveryCharge == true ? [] : null;
                                }
                            },
                            complete: function (result) {
                                $('#Overlay').hide();
                            }
                        });


                        //if (currentRow.hasOwnProperty("LandingCost")) {
                        //    $scope.SetLandingCostLastCostPrice(currentRow);
                        //}

                    }
                    else if (currentRow.Weight != null) {
                        currentRow.DeliveryCharge = Math.ceil(currentRow.DeliveryCharge + ((parseInt(currentRow.Quantity) * parseInt(currentRow.Weight)) - 5000) / 1000);
                    }
                }
                $scope.UpdateAllocations(currentRow, rowIndex);
                break;
            case "CGSTAmount":
                var totalAmount = Object.keys($scope.CRUDModel.Model.DetailViewModel).reduce(function (sum, key) {
                    return sum + parseFloat($scope.CRUDModel.Model.DetailViewModel[key].CGSTAmount);
                }, 0);

                $.each($scope.CRUDModel.Model.MasterViewModel.TaxDetails.Taxes, function (index, value) {
                    if (value.TaxName == 'CGST') {
                        value.TaxAmount = totalAmount;
                    }
                });

                break;
            case "SGSTAmount":
                var totalAmount = Object.keys($scope.CRUDModel.Model.DetailViewModel).reduce(function (sum, key) {
                    return sum + parseFloat($scope.CRUDModel.Model.DetailViewModel[key].SGSTAmount);
                }, 0);

                $.each($scope.CRUDModel.Model.MasterViewModel.TaxDetails.Taxes, function (index, value) {
                    if (value.TaxName == 'SGST') {
                        value.TaxAmount = totalAmount;
                    }
                });

                break;

            case "ExpectedUnitPrice":
                if (currentRow.IsScreen == "PurchaseRequest") {
                    currentRow.Amount = parseInt(currentRow.Quantity) * parseFloat(currentRow.ExpectedUnitPrice);
                }
                break;

            default:
                break;
        }
    };

    $scope.UpdateAllocations = function (currentRow, rowIndex) {

        if ($scope.CRUDModel.Model.MasterViewModel.Allocations == undefined)
            return;

        var allocationRowCount = $scope.CRUDModel.Model.MasterViewModel.Allocations.Allocations.length;

        if (allocationRowCount < rowIndex) {
            var vmAllocation = angular.copy($scope.CRUDModel.MasterViewModel.Allocations.Allocations[0]);
            vmAllocation.ProductID = currentRow.SKUID.Key;
            vmAllocation.ProductName = currentRow.SKUID.Value;
            vmAllocation.Quantity = currentRow.Quantity;
            vmAllocation.AllocatedQuantity = [];
            vmAllocation.BranchIDs = [];
            vmAllocation.BranchName = [];
            $.each($scope.LookUps.Branch, function (index, item) {
                vmAllocation.AllocatedQuantity.push(0);
                vmAllocation.BranchIDs.push(item.Key);
                vmAllocation.BranchName.push(item.Value);
            });
            $scope.CRUDModel.Model.MasterViewModel.Allocations.Allocations.push(vmAllocation);
        }
        else {
            var arrayIndex = rowIndex - 1;
            $scope.CRUDModel.Model.MasterViewModel.Allocations.Allocations[arrayIndex].ProductID = currentRow.SKUID.Key;
            $scope.CRUDModel.Model.MasterViewModel.Allocations.Allocations[arrayIndex].ProductName = currentRow.SKUID.Value;
            $scope.CRUDModel.Model.MasterViewModel.Allocations.Allocations[arrayIndex].Quantity = currentRow.Quantity;
        }
    }

    $scope.SaveTransaction = function (event, isExit, saveUrl, isString, successCallBack, isNext) {
        IsExitAfterSave = isExit ? true : false;
        IsNextAfterSave = isNext ? true : false;

        var form = $(event.currentTarget).closest("form");

        if (!form.validateForm()) {
            $().showMessage($scope, $timeout, true, 'Please fill required fields');
            return false;
        }

        showOverlay();

        if (!saveUrl) {
            saveUrl = $scope.CRUDModel.ViewFullPath + '/Save';
        }

        if (!successCallBack) {
            successCallBack = SaveCallBack;
        }

        if (ajaxHandler != null) {
            ajaxHandler.abort();
        }

        if (isString) {
            ajaxHandler = $.ajax({
                type: "POST",
                url: saveUrl,
                dataType: "json",
                data: { model: JSON.stringify({ screen: Entity, data: $scope.CRUDModel.Model }) },
                success: successCallBack,
                error: function (error) {
                    $().showMessage($scope, $timeout, true, error.responseText);
                },
                complete: function () {
                    hideOverlay();
                    ajaxHandler = null;
                }
            });
        } else {
            ajaxHandler = $.ajax({
                type: "POST",
                url: saveUrl,
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify($scope.CRUDModel.Model),
                success: successCallBack,
                error: function (error) {
                    $().showMessage($scope, $timeout, true, error.responseText);
                },
                complete: function () {
                    hideOverlay();
                }
            });
        }
    }

    $scope.SaveMasterDetailV2 = function (event, isExit, saveUrl, isString, successCallBack, isNext) {
        $scope.SaveTransaction(event, isExit, saveUrl, false, successCallBack, isNext);
    }

    /* this will add item based on ProductSKUMapID in ExternalProductSettings view model */
    function bindCustomerExternalSetting(model) {
        if (($scope.CRUDModel.Name == "Customer" || $scope.CRUDModel.Name == "Supplier") && model == "ProductSKUMapID") {
            if ($scope.CRUDModel.ViewModel.ExternalSettings) {

                var isExist = $.grep($scope.CRUDModel.ViewModel.ExternalSettings.ExternalProductSettings, function (result) { return result.ProductSKUMapID == $scope.CRUDModel[model]; });
                if (isExist.length > 0) {
                    $().showMessage($scope, $timeout, true, 'Selected product exists already');
                    return;
                }
                // Remove First blank row
                if ($scope.CRUDModel.ViewModel.ExternalSettings.ExternalProductSettings[0].ProductSKUMapID == 0) {
                    $scope.CRUDModel.ViewModel.ExternalSettings.ExternalProductSettings.pop();
                }
                // Call Service
                productService.getProductAndSKUByID($scope.CRUDModel[model])
                    .then(function (data) {
                        $scope.CRUDModel.ViewModel.ExternalSettings.ExternalProductSettings.push(data);
                    });
            }
        }
    }
    $scope.AccountGroupChanges = function ($event, $element, groupModel) {
        $('#Overlay').show();
        var model = groupModel;
        model.Account = null;
        var url = "Schools/School/GetAccountByGroupID?groupID=" + model.AccountGroup.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Account = result.data;
                $scope.CostCenter = null;
                $('#Overlay').hide();
            }, function () {
                $('#Overlay').hide();
            });
    };
    $scope.getStep = function () {
        if (!$scope.decimalPlaces) return "0.01";  // Default step if not set
        return (1 / Math.pow(10, $scope.decimalPlaces)).toFixed($scope.decimalPlaces);
    };
    $scope.CustomerAccountChanges = function (event, element) {
        if (!$scope.CRUDModel.Model.MasterViewModel.Account) {
            return;
        }
        var url = "Schools/School/GetAccountGroupByAccountID?accountID=" + $scope.CRUDModel.Model.MasterViewModel.Account.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                let aEntry = {
                    IsRowSelected: false,
                    SelectAll: false,
                    AccountTransactionDetailIID: 0,
                    AccountTransactionHeadID: 0,
                    ReceivableID: 0,
                    SerialNo: 1,
                    AccountGroup: {
                        "Key": result.data[0].Key,
                        "Value": result.data[0].Value
                    },
                    AccountGroupID: parseInt(result.data[0].Key),
                    Account: {
                        "Key": $scope.CRUDModel.Model.MasterViewModel.Account.Key,
                        "Value": $scope.CRUDModel.Model.MasterViewModel.Account.Value
                    },
                    AccountID: parseInt($scope.CRUDModel.Model.MasterViewModel.Account.Key),
                    Debit: 0,
                    Credit: 0,
                    DebitTotal: 0,
                    CreditTotal: 0,
                    CostCenter:
                    {
                        "Key": null,
                        "Value": null
                    },
                    CostCenterID: null,
                    Budget: {
                        "Key": null,
                        "Value": null
                    },
                    BudgetID: null,
                    AccountSubLedgers: {
                        "Key": null,
                        "Value": null
                    },
                    SubLedgerID: null,
                    InvoiceNumber: null,
                    ReferenceNumber: null,
                    InvoiceAmount: null,
                    ReturnAmount: null,

                    PaidAmount: null,
                    UnpaidAmount: null,
                    Amount: null,
                    CurrencyName: null,
                    ExchangeRate: null,
                    Remarks: null
                };

                if (!$scope.CRUDModel.DetailViewModel[0]) {
                    $scope.CRUDModel.Model.DetailViewModel[0] = [];
                }

                $scope.CRUDModel.Model.DetailViewModel[0] = aEntry;

                $('#Overlay').hide();
            }, function () {
                $('#Overlay').hide();
            });
    }
    $scope.CustomerAccountChangesInCreditNote = function (event, element) {
        if (!$scope.CRUDModel.Model.MasterViewModel.Account) {
            return;
        }
        $scope.GetCustomerPendingInvoices(event, element);
        var url = "Schools/School/GetAccountGroupByAccountID?accountID=" + $scope.CRUDModel.Model.MasterViewModel.Account.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                let aEntry = {
                    IsRowSelected: false,
                    SelectAll: false,
                    AccountTransactionDetailIID: 0,
                    AccountTransactionHeadID: 0,
                    ReceivableID: 0,
                    SerialNo: 1,
                    AccountGroup: {
                        "Key": result.data[0].Key,
                        "Value": result.data[0].Value
                    },
                    AccountGroupID: parseInt(result.data[0].Key),
                    Account: {
                        "Key": $scope.CRUDModel.Model.MasterViewModel.Account.Key,
                        "Value": $scope.CRUDModel.Model.MasterViewModel.Account.Value
                    },
                    AccountID: parseInt($scope.CRUDModel.Model.MasterViewModel.Account.Key),
                    Debit: 0,
                    Credit: 0,
                    DebitTotal: 0,
                    CreditTotal: 0,
                    CostCenter:
                    {
                        "Key": null,
                        "Value": null
                    },
                    CostCenterID: null,
                    Budget: {
                        "Key": null,
                        "Value": null
                    },
                    BudgetID: null,
                    AccountSubLedgers: {
                        "Key": null,
                        "Value": null
                    },
                    SubLedgerID: null,
                    InvoiceNumber: null,
                    ReferenceNumber: null,
                    InvoiceAmount: null,
                    ReturnAmount: null,

                    PaidAmount: null,
                    UnpaidAmount: null,
                    Amount: null,
                    CurrencyName: null,
                    ExchangeRate: null,
                    Remarks: null
                };

                if (!$scope.CRUDModel.DetailViewModel[0]) {
                    $scope.CRUDModel.Model.DetailViewModel[0] = [];
                }

                $scope.CRUDModel.Model.DetailViewModel[0] = aEntry;


                $('#Overlay').hide();
            }, function () {
                $('#Overlay').hide();
            });
    }
    $scope.DocumentTypeChange = function (event, element) {

        if (!$scope.CRUDModel.Model.MasterViewModel.DocumentType) {
            return;
        }

        //get next transaction number
        var documentTypeID = null;
        if ($scope.CRUDModel.Model == undefined) {
            documentTypeID = $scope.CRUDModel.ViewModel.DocumentType;
        } else {
            if ($scope.CRUDModel.Model.MasterViewModel.DocumentType.Key) {
                if ($scope.CRUDModel.Model.MasterViewModel.DocumentType.Key === undefined)
                    documentTypeID = $scope.CRUDModel.Model.MasterViewModel.DocumentType;
                else
                    documentTypeID = $scope.CRUDModel.Model.MasterViewModel.DocumentType.Key;
            }
        }

        purchaseorderService.getNextTransactionNo(documentTypeID)
            .then(function (data) {
                if ($scope.CRUDModel.Model === undefined) {
                    $scope.CRUDModel.ViewModel.TicketNo = data;
                }
                else {
                    $scope.CRUDModel.Model.MasterViewModel.TransactionNo = data;
                }
            });

        if ($scope.CRUDModel.Model.MasterViewModel.TaxDetails) {
            // get the tax details
            $('#Overlay').show();
            $.ajax({
                type: "GET",
                url: 'Inventories/InventoryDetails/GetTaxDetails?documentID=' + documentTypeID,
                success: function (result) {
                    $scope.$apply(function () {
                        $scope.CRUDModel.Model.MasterViewModel.TaxDetails.Taxes = result.Taxes;
                    });
                },
                complete: function (result) {
                    $('#Overlay').hide();
                }
            });
        }
    };

    $scope.updateFirstRowDebit = function (groupModel) {
        if ($scope.CRUDModel.Model.DetailViewModel.length > 1) {
            let sum = $scope.CRUDModel.Model.DetailViewModel.slice(1) // Exclude the first row
                .reduce((total, row) => total + ((row.Debit || 0) - (row.Credit || 0)), 0);

            $scope.CRUDModel.Model.DetailViewModel[0].Debit = Math.abs(sum);
            $scope.CRUDModel.Model.DetailViewModel[0].Credit = 0;
            $scope.CRUDModel.Model.DetailViewModel[0].Amount = (-1 * parseFloat($scope.CRUDModel.Model.DetailViewModel[0].Credit)) + parseFloat($scope.CRUDModel.Model.DetailViewModel[0].Debit);
        }
    };

    $scope.updateFirstRowCredit = function (groupModel) {
        if ($scope.CRUDModel.Model.DetailViewModel.length > 1) {
            let sum = $scope.CRUDModel.Model.DetailViewModel.slice(1) // Exclude the first row
                .reduce((total, row) => total + ((row.Debit || 0) - (row.Credit || 0)), 0);

            if ((sum ?? 0) > ($scope.CRUDModel.Model.MasterViewModel.PendingAmount ?? 0)) {
                $().showMessage($scope, $timeout, true, 'The Total Amount is greater than Pending Amount !');
                groupModel.Credit = 0;
                groupModel.Debit = 0;
                groupModel.Amount = 0;
                return false;
            }
            $scope.CRUDModel.Model.DetailViewModel[0].Credit = Math.abs(sum);
            $scope.CRUDModel.Model.DetailViewModel[0].Debit = 0;
            $scope.CRUDModel.Model.DetailViewModel[0].Amount = (-1 * parseFloat($scope.CRUDModel.Model.DetailViewModel[0].Credit)) + parseFloat($scope.CRUDModel.Model.DetailViewModel[0].Debit);
        }
    };

    $scope.TaxTemplateChange = function (event, row) {
        $('#Overlay').show();

        $.ajax({
            type: "GET",
            url: 'Mutual/GetTaxTemplateItem?taxTemplateID=' + row.TaxTemplate,
            success: function (result) {
                $scope.$apply(function () {
                    row.TaxPercentage = result.Percentage;
                    row.TaxTemplateID = result.TaxTemplateID;
                });
            },
            complete: function (result) {
                $('#Overlay').hide();
            }
        });
    };

    $scope.GetCustomerEntitlements = function (customerID) {
        $('#Overlay').show();
        $.ajax({
            type: "GET",
            url: 'Mutual/GetCustomerEntitlements?customerID=' + customerID,
            success: function (result) {
                $scope.$apply(function () {
                    $scope.LookUps['EntitlementMaster'] = result;
                });
            },
            complete: function (result) {
                $('#Overlay').hide();
            }
        });
    };

    $scope.CustomerChange = function (event, element) {
        var customerID = null;
        if ($scope.CRUDModel.Model == undefined) {
            customerID = $scope.CRUDModel.ViewModel.Customer;
        }
        else {
            if ($scope.CRUDModel.Model.MasterViewModel.Customer.Key == undefined) {
                customerID = $scope.CRUDModel.Model.MasterViewModel.Customer;
            }
            else
                customerID = $scope.CRUDModel.Model.MasterViewModel.Customer.Key;
        }
        if (customerID > 0) {
            $scope.GetCustomerEntitlements(customerID);
            accountService.getContactByCustomerId(customerID)
                .then(function (data) {
                    console.log(data);
                    $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails = data.ShippingContact;
                });
        }
    };

    $scope.OnGLAccountsCodeChange = function ($select, index, detail) {
        var model = detail;

        detail.Description = $select.selected.Value;

        showOverlay();
        var model = detail;

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

    $scope.onAccountSelected = function ($select) {
        $($select.$element).parent('td').next('td').next('input').focus();
    };

    $scope.BranchChange = function (event, element) {
        var branchID = null;
        if ($scope.CRUDModel.Model == undefined) {
            if ($scope.CRUDModel.ViewModel.Branch.Key == undefined)
                branchID = $scope.CRUDModel.ViewModel.Branch;
            else
                branchID = $scope.CRUDModel.ViewModel.Branch.Key;
        } else {

            if ($scope.CRUDModel.Model.MasterViewModel.Branch.Key == undefined)
                branchID = $scope.CRUDModel.Model.MasterViewModel.Branch;
            else {
                branchID = $scope.CRUDModel.Model.MasterViewModel.Branch.Key;
                ////reset docuemtn type
                //$scope.CRUDModel.Model.MasterViewModel.DocumentType = null;
            }
        }

        //$scope.LookUps['DocumentType'] = [{ 'Key': '', Value: '' }];
        //$.ajax({
        //    type: "GET",
        //    url: GetUrl('DocumentType') + '&branchID=' + branchID,
        //    contentType: "application/json;charset=utf-8",
        //    success: function (result) {
        //        $scope.$apply(function () {
        //            $scope.LookUps['DocumentType'] = result;
        //            var data = result.find(x => x.Key!=null);
        //            if (data != null) {
        //                $scope.CRUDModel.Model.MasterViewModel.DocumentType.Key = data.Key;
        //                $scope.CRUDModel.Model.MasterViewModel.DocumentType.Value = data.Value;
        //            }
        //        });
        //    }
        //});
    }

    function GetUrl(lookUpName) {
        var url = '';

        $.each($scope.CRUDModel.Urls, function (index, data) {
            if (data.LookUpName == lookUpName) {
                url = data.Url;
            }
        });

        return url;
    }

    $scope.BringDynamicPopup = function (detail) {
        detail.IsShowDynamicPopup = true;
    }

    $scope.DynamicPoupClose = function (detail) {
        detail.IsShowDynamicPopup = false;
    }

    $scope.BringPopup = function (detail) {

        if (detail.Quantity == 0 || (detail.hasOwnProperty("IsSerailNumberAutoGenerated") == true && detail.IsSerailNumberAutoGenerated == true)) {
            return;
        }

        if (Entity == "SalesInvoice" && detail.hasOwnProperty("IsSerialNumber") == true) {
            if (detail.IsSerialNumber != true) {
                return;
            }
        }
        else if (Entity == "PurchaseInvoice" && detail.hasOwnProperty("IsSerialNumberOnPurchase") == true) {
            if (detail.IsSerialNumberOnPurchase != true) {
                return;
            }
        }
        //else if (Entity == "AssetEntry" && detail.hasOwnProperty("IsSerialNumberOnAssetEntry") == true) {
        //    if (detail.IsSerialNumberOnAssetEntry != true) {
        //        return;
        //    }
        //}
        else if (Entity.toLowerCase().contains("asset")) {
            if (detail.IsSerialNumberOnAssetEntry != true) {
                return;
            }
        }
        else {
            return;
        }

        detail.IsQuantityPopup = true;

        if (detail.SKUDetails == null) {
            detail.SKUDetails = [];
        }

        if (detail.SKUDetails.length < detail.Quantity) {
            for (i = detail.SKUDetails.length; i < detail.Quantity; i++) {
                var skuDetail = angular.copy($scope.CRUDModel.DetailViewModel.SKUDetails)[0];
                skuDetail.ProductSKUMapID = detail.SKUID.Key;
                detail.SKUDetails.push(skuDetail);
            }
        }
        else if (detail.SKUDetails.length > detail.Quantity) {
            for (i = detail.SKUDetails.length; i > detail.Quantity; i--) {
                detail.SKUDetails.pop();
            }
        }

        // check is that property inside the object
        if (typeof detail.IsSerialNumber != "undefined") {
            if (detail.IsSerialNumber) {
                // get the serial number
                for (var i = 0; i < detail.SKUDetails.length; i++) {
                    if (detail.SKUDetails[i].ProductSKUMapID == detail.SKUID.Key) {
                        if ($scope.LookUps['SerialList'].length > 0) return;
                        $scope.RefreshSelect2("Inventory/GetProductInventorySerialMaps", { select: undefined }, "LookUps.SerialList", detail.SKUDetails[i]);
                    }
                }
            }
        }
    }

    $scope.SerialKeyDuplicationCheck = function (event) {
        if (event.target.value != '') {
            showOverlay();
            $.ajax({
                type: "POST",
                url: Entity + '/SerialKeyDuplicationCheck',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify($scope.CRUDModel.Model),
                success: function (respone) {
                    if (respone.IsError) {
                        $().showMessage($scope, $timeout, true, respone.ErrorMessage);
                    }
                },
                complete: function () {
                    hideOverlay();

                }
            });
        }

    }

    $scope.PoupClose = function (detail) {
        detail.IsQuantityPopup = false;
    }

    $scope.FireEvent = function ($event, parameters, menuParameters) {
        $rootScope.FireEvent($event, parameters, menuParameters)
    }

    $scope.SelectAllHeader = function (event, rows, selectAll) {
        $.each(rows, function (index, data) {
            data.IsRowSelected = selectAll;
        });
    }

    /* this method is used when we select customer to create Sales Order */
    function bindDeliveryDetail(model) {
        if (($scope.CRUDModel.Name === "SalesOrder" || $scope.CRUDModel.Name === "SalesInvoice" || $scope.CRUDModel.Name === "SalesReturnRequest"
            || $scope.CRUDModel.Name === "SalesReturn") && model === "Customer") {
            // Call service, addressType=2 for shipping
            accountService.getBillingShippingContact($scope.CRUDModel[model], 2)
                .then(function (data) {
                    // assign here
                    $scope.CRUDModel.Model.MasterViewModel.BillingDetails = data.Item1;
                    $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails = data.Item2;

                    //$scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.AreaID = data.AreaID;
                    //$scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.DeliveryAddress = data.DeliveryAddress;
                    //$scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.ContactPerson = data.ContactPerson;
                    //$scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.MobileNo = data.MobileNo;
                    //$scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.LandLineNo = data.LandLineNo;
                    //$scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.SpecialInstructions = data.SpecialInstructions;

                });
        } else if ($scope.CRUDModel.Name === "ShoppingCart") {
            accountService.getContactByCustomerId($scope.CRUDModel[model])
                .then(function (data) {
                    console.log(data);
                    $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails = data.ShippingContact;
                });
        }
    }

    function pickContactsSearch(view, data, invoker) {

        $('#Overlay').show();
        $.ajax({
            type: "GET",
            url: view + '/GetContactDetail/' + data.ContactIID,
            contentType: "json",
            success: function (result) {
                $('#Overlay').hide();
                if (invoker == 'ShoppingCartController') {
                    $scope.$apply(function () {
                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails = result;
                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.Area = result.Area.Value;
                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.City = result.City.Value;
                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.Country = result.Country.Value;
                        //set invoker to null again
                        $scope.AdvanceSearchInvoker = null;
                        $scope.SaveOrderAddress(0, result.ContactID, '', result.CustomerID)
                    });
                }
                else
                    $scope.$apply(function () {
                        $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails = result;
                    })

            }
        });
    }

    function pickSalesSearch(view, data, invoker) {

        $('#Overlay').show();

        var controllerUrl = view + '/Get/' + data.HeadIID;

        $.ajax({
            type: "GET",
            url: controllerUrl,
            contentType: "json",
            success: function (result) {
                $('#Overlay').hide();

                if (invoker == 'TicketController') {
                    $scope.$apply(function () {
                        $scope.CRUDModel.ViewModel.TicketProductSKUs = result.DetailViewModel;
                        $scope.CRUDModel.ViewModel.HeadID = result.MasterViewModel.TransactionHeadIID;


                        //set invoker to null again
                        $scope.AdvanceSearchInvoker = null;
                    });
                }
                else if (invoker == 'ReplacementController') {
                    $scope.$apply(function () {
                        console.log(result.MasterViewModel);
                        //$scope.CRUDModel.ViewModel.DetailViewModel = result.DetailViewModel;
                        $scope.CRUDModel.Model.MasterViewModel = result.MasterViewModel;
                        $scope.CRUDModel.Model.DetailViewModel = result.DetailViewModel;
                        $scope.CRUDModel.Model.MasterViewModel.DocumentType = "0";
                        //set invoker to null again
                        $scope.AdvanceSearchInvoker = null;
                    });
                }
                else {
                    result.MasterViewModel.TransactionHeadIID = 0;
                    result.MasterViewModel.DocumentType = "0";
                    result.MasterViewModel.ReferenceTransactionNo = result.MasterViewModel.TransactionNo;
                    result.MasterViewModel.TransactionNo = "";
                    result.MasterViewModel.DeliveryDetails.OrderContactMapID = 0;
                    result.MasterViewModel.TransactionDate = $scope.CRUDModel.Model.MasterViewModel.TransactionDate;

                    $.each(result.DetailViewModel, function (index, item) {
                        item.TransactionDetailID = 0;
                        item.TransactionHead = "0";
                    });
                    $scope.$apply(function () {
                        $scope.CRUDModel.Model = result;
                        $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionHeaderID = data.HeadIID;
                        $scope.CRUDModel.Model.MasterViewModel.TransactionStatus = null;
                    });
                }

                $scope.BranchChange(null, null);
            }
        });
    }

    $scope.ProductExists = function (event) {
        event.stopPropagation();
        event.preventDefault();
        if (event.keyCode == 13) {
            $.ajax({
                url: "Catalogs/ProductSKU/ProductSKUSearchUsingBarcode?searchText=" + $scope.CRUDModel.Model.MasterViewModel.SearchText,
                type: 'GET',
                success: function (productsList) {
                    var productExists;
                    productsList = productsList.Data;

                    if (productsList.length > 0) {
                        $.each(productsList, function (index, item) {
                            productExists = $.grep($scope.CRUDModel.Model.DetailViewModel, function (result) { if (result.SKUID != null) { return eval(result.SKUID.Key) == item.ProductSKUMapIID; } })[0];

                            if (productExists != null && productExists != undefined) {
                                $("#" + $scope.CRUDModel.Model.DetailViewModel.indexOf(productExists) + "_Quantity").focus().select();
                            }
                            else {
                                $scope.$apply(function () {
                                    var newItem = angular.copy($scope.ModelStructure.DetailViewModel[0]);
                                    newItem.SKUID = { Key: item.ProductSKUMapIID, Value: item.ProductSKU };
                                    newItem.Description = item.ProductSKU;
                                    newItem.PartNo = item.PartNo;
                                    newItem.Barcode = item.BarCode;
                                    newItem.TaxTemplate = item.TaxTemplateID.toString();
                                    newItem.TaxTemplateID = item.TaxTemplateID;
                                    newItem.TaxPercentage = item.TaxPercentage;
                                    newItem.UnitPrice = item.UnitPrice;
                                    newItem.Quantity = 1;
                                    newItem.ProductCode = null;
                                    newItem.Fraction = 0;
                                    newItem.Unit = { Key: null, Value: null };
                                    newItem.UnitGroupID = 0;
                                    newItem.UnitList = [];
                                    newItem.UnitID = null;
                                    newItem.UnitDTO = null;
                                    //newItem.IsScreen = newItem[0].IsScreen;

                                    $scope.CRUDModel.Model.DetailViewModel.splice(0, 0, newItem);
                                    //$().showMessage($scope, $timeout, true, "Product does not exist in grid");
                                    $timeout(function () {
                                        var branchID;

                                        if ($scope.CRUDModel.Model.MasterViewModel.Branch == undefined) {
                                            branchID = 0;
                                        } else
                                            if ($scope.CRUDModel.Model.MasterViewModel.Branch.Key == undefined) {
                                                branchID = $scope.CRUDModel.Model.MasterViewModel.Branch;
                                            }
                                            else {
                                                branchID = $scope.CRUDModel.Model.MasterViewModel.Branch.Key;
                                            }

                                        GetProductDetailsAndInventory(newItem, newItem.SKUID.Key, $scope.ModelStructure.MasterViewModel.DocumentReferenceTypeID, branchID);
                                        $("#" + $scope.CRUDModel.Model.DetailViewModel.indexOf(newItem) + "_Quantity").focus().select();

                                        if (newItem.Quantity == 1)
                                            $scope.UpdateDetailGridValues("Quantity", newItem, $scope.CRUDModel.Model.DetailViewModel.indexOf(newItem));
                                    });
                                });
                            }
                        });
                    }
                    else {
                        $().showMessage($scope, $timeout, true, "Product does not exist in grid");
                    }

                    $scope.CRUDModel.Model.MasterViewModel.SearchText = null;
                }
            })
        }
    }

    $scope.Comments = function (event) {
        $("#commentPanel").show();
        $('.popup.comments', $($scope.CrudWindowContainer)).slideDown("fast");
        $('.transparent-overlay', $($scope.CrudWindowContainer)).show();

        $.ajax({
            url: "Mutual/Comment?type=" + $scope.CRUDModel.EntityType + "&referenceID=" + IID,
            type: 'GET',
            success: function (content) {
                $('#commentPanel', $($scope.CrudWindowContainer)).html($compile(content)($scope));
            }
        });
    }

    $scope.Attachments = function (event) {
        $('.popup.attachments', $($scope.CrudWindowContainer)).slideDown("fast");
        $('.transparent-overlay', $($scope.CrudWindowContainer)).show();

        $.ajax({
            url: "Mutual/Attachment?type=" + $scope.CRUDModel.EntityType + "&referenceID=" + IID,
            type: 'GET',
            success: function (content) {
                $('#attachmentPanel', $($scope.CrudWindowContainer)).html($compile(content)($scope));
            }
        });
    }

    $scope.CloseAttachmentOverlay = function (event) {
        $(event.currentTarget).hide();
        $('.popup.attachments', $($scope.CrudWindowContainer)).slideUp("fast");
    }

    $scope.CloseCommentOverlay = function (event) {
        $(event.currentTarget).hide();
        $('.popup.comments', $($scope.CrudWindowContainer)).slideUp("fast");
    }

    $scope.DeleteGridItems = function (event, details) {

        console.log($(event.currentTarget));

        itemsToDelete = $.grep(details, function (result) { return result.IsRowSelected == true });

        this.CRUDModel.Model.DetailViewModel = $.grep(this.CRUDModel.Model.DetailViewModel, function (result) { return result.IsRowSelected == false });

        if ($scope.CRUDModel.Model.MasterViewModel.Allocations != null && $scope.CRUDModel.Model.MasterViewModel.Allocations.Allocations.length > 0 && itemsToDelete.length > 0) {
            $scope.DeleteAllocation(itemsToDelete);
        }
    }

    $scope.DeleteAllocation = function (itemsToDelete) {

        $.each(itemsToDelete, function (index, item) {
            if (item.SKUID != null) {
                $scope.CRUDModel.Model.MasterViewModel.Allocations.Allocations = $.grep($scope.CRUDModel.Model.MasterViewModel.Allocations.Allocations, function (result) { return result.ProductID != item.SKUID.Key });
            }
        })
    }

    $scope.OnChangeTicketSKUSelect2 = function (model, dataType, control, multiplesingle, currentRow, rowIndex) {

        // adding a new row in the grid.
        if (rowIndex == $scope.CRUDModel.ViewModel.TicketProductSKUs.length) {
            var vm = angular.copy($scope.CRUDModel.DetailViewModel);
            $scope.CRUDModel.ViewModel.TicketProductSKUs.push(vm);
        }
    }

    $scope.OnChangeSerialNo = function () {
        // we have fetch the data based on the skumap id for this seriallist control
        $scope.LookUps["LookUps.SerialList"] = [];
    }

    $scope.PrintTransaction = function (event, reportName, reportHeader, reportFullName) {
        var headIID = null;
        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        if ($scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID != undefined) {
            headIID = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
        }
        if ($scope.CRUDModel.Model.MasterViewModel.AccountTransactionHeadIID != undefined) {
            headIID = $scope.CRUDModel.Model.MasterViewModel.AccountTransactionHeadIID;
        }
        if ($scope.CRUDModel.Name == 'Mission') {
            headIID = $scope.CRUDModel.Model.MasterViewModel.JobEntryHeadIID;
        }
        if ($scope.CRUDModel.Name == 'ReadyForShipping') {
            headIID = $scope.CRUDModel.Model.MasterViewModel.JobEntryHeadIID;
            if (headIID == 0) {
                $().showMessage($scope, $timeout, true, 'Assign these jobs to mission');
                return;
            }
        }
        if (headIID == null) {
            return false;
        }

        var reportingService = $rootScope.ReportingService;

        if (reportingService == "ssrs") {

            //SSRS report viewer start
            $http({
                method: 'GET',
                url: utility.myHost + "Reports/Report/GetReportUrlandParameters?reportName=" + reportName
            }).then(function (filename) {
                if (filename.data.Response) {
                    reportUrl = filename.data.Response + "&HeadID=" + headIID;
                    var loadContent = "<center><div class='bounce-wrapper' id='Load'><div class='three-bounce-inner'><div class='bounce-item bi-item1'></div><div class='bounce-item bi-item2'></div><div class='bounce-item bi-item3'></div></div></div></center></div>";
                    $('#' + windowName).html(loadContent + '<iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
                    var $iFrame = $('iframe[reportname=' + reportName + ']');
                    $iFrame.ready(function () {
                        setTimeout(function () {
                            $("#Load", $('#' + windowName)).hide();
                        }, 1000);
                    });
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
                "HeadID": headIID
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

    //Below method is called when the cancel button is clicked.
    $scope.ReLoadPage = function () {
        $("#append-panel-body").empty();
        if (IID <= 0) {
            if ($scope.CRUDModel.ViewModel) {
                $scope.CRUDModel.ViewModel = angular.copy($scope.ModelStructure);
            }
            else {
                $scope.CRUDModel.Model = angular.copy($scope.ModelStructure);
            }
            return;
        }

        var getUrl;

        if ($scope.CRUDModel.IsGenericCRUDSave == true) {
            getUrl = 'Frameworks/CRUD/Get?screen=' + $scope.CRUDModel.Screen + '&ID=' + $scope.CRUDModel.IID;
        }
        else
            if ($scope.CRUDModel.ReferenceIIDs != null)
                getUrl = $scope.CRUDModel.ViewFullPath + '/Get/' + $scope.CRUDModel.ReferenceIIDs;
            else
                getUrl = $scope.CRUDModel.ViewFullPath + '/Get/' + IID.toString();

        showOverlay();
        $.ajax({
            url: getUrl,
            type: 'GET',
            success: LoadCallBack
        });
    };

    $scope.NextNewEntry = function () {
        $scope.$apply(function () {
            if ($scope.CRUDModel.ViewModel) {
                $scope.CRUDModel.ViewModel = angular.copy($scope.ModelStructure);
            }
            else {
                $scope.CRUDModel.Model = angular.copy($scope.ModelStructure);
            }
        });

        $('.controls input', $($scope.CrudWindowContainer)).filter(":visible:first").focus();
    };

    $scope.CloseWindow = function () {
        if ($($scope.CrudWindowContainer).closest('.windowcontainer').attr('windowindex')) {
            $scope.CloseWindowTab($($scope.CrudWindowContainer).closest('.windowcontainer').attr('windowindex'));
        }
        else {
            $($scope.CrudWindowContainer).parents('.popup').find('.close').trigger('click');
        }
    };

    $scope.ShowGridFilter = function (event) {
        console.log(event);

        if ($(event.currentTarget).next().css("display") == "none") {
            $(event.currentTarget).next().css("display", "block");
        }
        else {
            $(event.currentTarget).next().css("display", "none");
        }
    }

    $scope.ShowNumberOfDaysTextBox = function (deliveryTypeID, numberOfdaysEntityType) {

        if (deliveryTypeID <= 0)
            return;

        if (deliveryTypeID == numberOfdaysEntityType) // 'n' number of days
            $(".NumberOfDays").css("display", "block");
        else
            $(".NumberOfDays").css("display", "none");
    }

    $scope.UniqueTag = function (selectedItem) {

        if (selectedItem.Key != undefined) { //Existing Tag

            if ($scope.CRUDModel.ViewModel.Tags) {
                var existModel = $.grep($scope.CRUDModel.ViewModel.Tags, function (item) { return item.Value.toLowerCase().trim() == selectedItem.Value.toLowerCase().trim() });

                if (existModel.length > 1) {
                    $().showMessage($scope, $timeout, true, 'Tag Name already exists...');
                    $scope.CRUDModel.ViewModel.Tags.pop(selectedItem);
                }
            }

            return;
        }
        else { // New Tag
            var existsDB = $.grep($scope.LookUps.BrandTags, function (item) { return item.Value.toLowerCase().trim() == selectedItem.Value.toLowerCase().trim() });

            if (existsDB.length > 0) {
                $().showMessage($scope, $timeout, true, 'Tag Name already exists...');
                $scope.CRUDModel.ViewModel.Tags.pop(selectedItem);
                return;
            }
        }
    }

    $scope.tagTransform = function (newTag) {
        return { Key: newTag.toUpperCase(), Value: newTag.toUpperCase() };
    };

    $scope.tagTransformExact = function (newTag) {
        return { Key: newTag, Value: newTag };
    };

    $scope.GetAreaByCountryID = function () {
        console.log("CountryID: " + $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.CountryID);
        var uri = "SalesOrder/GetAreaByCountryID?countryID=" + $scope.CRUDModel.Model.MasterViewModel.DeliveryDetails.CountryID;
        $.ajax({
            url: uri,
            type: 'GET',
            success: function (result) {
                // Bind area list
                $scope.$apply(function () {
                    $scope.LookUps['Areas'] = result.AreaList;
                });
            }
        });

    }

    $scope.getCityByCountryId = function (countryId) {
        $.ajax({
            url: 'Mutual/GetCityByCountryID?countryID=' + countryId,
            type: 'GET',
            success: function (result) {
                $scope.LookUps['Cities'] = result;
            }
        });
    }

    $scope.getAreaByCityId = function (cityId) {
        var uri = "Mutual/GetAreaByCityID?cityID=" + cityId;
        $.ajax({
            url: uri,
            type: 'GET',
            success: function (result) {
                $scope.$apply(function () {
                    $scope.LookUps['Areas'] = result;
                });
            }
        });
    }


    $scope.onCountryChangeSelect2 = function (select2, model) {
        // reset the selected value
        model.City = null;
        if (select2.selected.Key) {
            $scope.getCityByCountryId(select2.selected.Key);
        }

    }

    $scope.onCityChangeSelect2 = function (select2, model) {
        // reset the selected value
        model.Area = null;
        if (select2.selected.Key) {
            $scope.getAreaByCityId(select2.selected.Key);
        }
    }

    $scope.onCityClickSelect2 = function (select2, model) {
        if (model.Country.Key) {
            $scope.getCityByCountryId(model.Country.Key)
        }
    }

    $scope.onAreaClickSelect2 = function (select2, model) {
        if (model.City.Key) {
            $scope.getAreaByCityId(model.City.Key)
        }
    }

    $scope.ClearCache = function (event, type) {
        $.ajax({
            url: 'Mutual/ClearCache?countryID=' + countryId,
            type: 'GET',
            success: function (result) {
                $scope.LookUps['Cities'] = result;
            }
        });
    }

    $scope.GetMainDesignation = function (selected, rowIndex, currentRow) {
        showOverlay();
        if ($scope.CRUDModel.Model.MasterViewModel.isNewRequest == null) {
            $scope.CRUDModel.Model.MasterViewModel.EMP_NO = 0;
            $scope.CRUDModel.Model.MasterViewModel.EMP_REQ_NO = 0;
        }
        $scope.LookUps['Designation'] = [];
        $scope.LookUps['MainDesignation'] = [];
        currentRow.MainDesignation = null;
        currentRow.Designation = null;

        $.ajax({
            url: 'Mutual/GetMainDesignation?gdesig_code=' + selected.selected.Key,
            type: 'GET',
            success: function (result) {
                $scope.LookUps['MainDesignation'] = result;
                hideOverlay();
            }
        });
    }

    $scope.GetHRDesignation = function (selected, rowIndex, currentRow) {
        $scope.LookUps['Designation'] = [];
        if ($scope.CRUDModel.Model.MasterViewModel.isNewRequest == null) {
            $scope.CRUDModel.Model.MasterViewModel.EMP_NO = 0;
            $scope.CRUDModel.Model.MasterViewModel.EMP_REQ_NO = 0;
        }
        currentRow.Designation = null;

        showOverlay();
        $.ajax({
            url: 'Mutual/GetHRDesignation?mdesig_code=' + selected.selected.Key,
            type: 'GET',
            success: function (result) {
                $scope.LookUps['Designation'] = result;
                hideOverlay();
            }
        });
    }

    $scope.GetAllowancebyPayComp = function (selected, rowIndex, currentRow) {
        showOverlay();
        $scope.LookUps['Allowance'] = [];

        $.ajax({
            url: 'Mutual/GetAllowancebyPayComp?paycomp=' + selected.selected.Key,
            type: 'GET',
            success: function (result) {
                $scope.LookUps['Allowance'] = result;
                hideOverlay();
            }
        });
    }

    $scope.GetLocationByDept = function (selected, rowIndex, currentRow) {
        showOverlay();
        currentRow.Location = null;

        $scope.LookUps['Location'] = [];
        $.ajax({
            url: 'Mutual/GetLocationByDept?deptCode=' + selected.selected.Key,
            type: 'GET',
            success: function (result) {
                $scope.LookUps['Location'] = result;
                hideOverlay();
            }
        });
    }

    $scope.AllowanceChange = function (rowIndex) {
        // adding a new row in the grid.
        if (rowIndex == $scope.CRUDModel.Model.MasterViewModel.Allowance.Allowances.length) {
            $scope.CRUDModel.Model.MasterViewModel.Allowance.Allowances.push(angular.copy($scope.ModelStructure.MasterViewModel.Allowance.Allowances[0]));
        }
    }

    $scope.LoadAllowences = function (rowIndex) {
        $scope.LookUps['AllowanceFiltered'] = [];
        var fitlers = new JSLINQ($scope.LookUps['Allowance']).Where(function (allowence) {
            var exists = new JSLINQ($scope.CRUDModel.Model.MasterViewModel.Allowance.Allowances)
                .Where(function (item) {
                    return item.Allowance != null && item.Allowance.Key == allowence.Key;
                });
            if (exists.items.length == 0) {
                $scope.LookUps['AllowanceFiltered'].push(allowence);
            }
        });
    }

    $scope.GetNextEmpNo = function (selected, rowIndex, currentRow) {
        if ($scope.CRUDModel.Model.MasterViewModel.isNewRequest == null) {
            showOverlay();
            $.ajax({
                url: 'Mutual/GetNextEmpNo?desigCode=' + selected.selected.Key + "&docType=ERN",
                type: 'GET',
                success: function (result) {
                    $timeout(function () {
                        $scope.CRUDModel.Model.MasterViewModel.EMP_REQ_NO = result;
                    });

                    //$scope.LookUps['Allowance'] = result;
                    hideOverlay();
                }
            });

            $.ajax({
                url: 'Mutual/GetNextEmpNo?desigCode=' + selected.selected.Key + "&docType=ENO",
                type: 'GET',
                success: function (result) {
                    $timeout(function () {
                        $scope.CRUDModel.Model.MasterViewModel.EMP_NO = result;
                    });
                    //$scope.LookUps['Allowance'] = result;
                    hideOverlay();
                }
            });

        }
    }

    $scope.GetEmploymentProcessStatus = function () {
        if ($scope.CRUDModel.Model.MasterViewModel.isNewRequest == null) {
            $.ajax({
                url: 'EmploymentRequest/GetEmploymentRequestStatus?empReqNo=0',
                type: 'GET',
                success: function (result) {
                    $timeout(function () {
                        $scope.CRUDModel.Model.MasterViewModel.EMP_NO = result;
                    });
                    $scope.LookUps['EmploymentProcessStatus'] = result;
                }
            });
        }
        else {
            $.ajax({
                url: 'EmploymentRequest/GetEmploymentRequestStatus?empReqNo=' + $scope.CRUDModel.Model.MasterViewModel.EMP_REQ_NO,
                type: 'GET',
                success: function (result) {
                    $timeout(function () {
                        $scope.CRUDModel.Model.MasterViewModel.EMP_NO = result;
                    });
                    $scope.LookUps['EmploymentProcessStatus'] = result;
                }
            });
        }
    }

    $scope.GetDocType = function (selected, rowIndex, currentRow) {
        showOverlay();

        $scope.LookUps['FinalDocType'] = [];
        $.ajax({
            url: 'Mutual/GetDocType?RecruitTypeID=' + selected.selected.Key,
            type: 'GET',
            success: function (result) {
                $scope.LookUps['FinalDocType'] = result;
                hideOverlay();
            }
        });
        $scope.LookUps['Agent'] = [];
        if (selected.selected.Key != "2") {
            $scope.GetAgent(selected, rowIndex, currentRow);
        }
    }

    $scope.LoadDocuments = function (rowIndex) {
        $scope.LookUps['DocType'] = [];
        var fitlers = new JSLINQ($scope.LookUps['FinalDocType']).Where(function (document) {
            var exists = new JSLINQ($scope.CRUDModel.Model.MasterViewModel.Document.Documents)
                .Where(function (item) {
                    return item.UploadDocumentType != null && item.UploadDocumentType.Key == document.Key;
                });
            if (exists.items.length == 0) {
                $scope.LookUps['DocType'].push(document);
            }
        });
    }

    $scope.LoadEntitlement = function (rowIndex) {
        $scope.LookUps['Entitlement'] = [];
        var fitlers = new JSLINQ($scope.LookUps['EntitlementMaster']).Where(function (entitlement) {
            var exists = new JSLINQ($scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps)
                .Where(function (item) {
                    if (item == undefined || item.Entitlement == null) return false;
                    return item.Entitlement.Key == entitlement.Key;
                });
            if (exists.items.length == 0) {
                $scope.LookUps['Entitlement'].push(entitlement);
            }
        });
    }

    $scope.DocumentChange = function (rowIndex) {
        if (rowIndex == $scope.CRUDModel.Model.MasterViewModel.Document.Documents.length) {
            $scope.CRUDModel.Model.MasterViewModel.Document.Documents.push(angular.copy($scope.ModelStructure.MasterViewModel.Document.Documents[0]));
        }
    }

    $scope.GetAgent = function (selected, rowIndex, currentRow) {
        showOverlay();

        $scope.LookUps['Agent'] = [];
        $.ajax({
            url: 'Mutual/GetAgent',
            type: 'GET',
            success: function (result) {
                $scope.LookUps['Agent'] = result;
                hideOverlay();
            }
        });
    }

    $scope.SetProposedIncreasePercent = function (event, element, rowIndex) {
        if ($scope.CRUDModel.Model.MasterViewModel.BasicSalary != undefined && $scope.CRUDModel.Model.MasterViewModel.BasicSalary != null && $scope.CRUDModel.Model.MasterViewModel.BasicSalary != 0) {
            $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncrease = (parseFloat($scope.CRUDModel.Model.MasterViewModel.BasicSalary) + parseFloat(($scope.CRUDModel.Model.MasterViewModel.BasicSalary * $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreasePercentage / 100)));

            //if ($scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncrease != "" && $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncrease != null && $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncrease != undefined) {
            //    $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncrease = $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncrease.toFixed(3);
            //}
            $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreaseAmount = (parseFloat($scope.CRUDModel.Model.MasterViewModel.BasicSalary) * parseFloat($scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreasePercentage / 100)).toFixed(3);
        }
    }

    $scope.SetProposedIncreaseAmount = function (event, element, rowIndex) {
        if ($scope.CRUDModel.Model.MasterViewModel.BasicSalary != undefined && $scope.CRUDModel.Model.MasterViewModel.BasicSalary != null && $scope.CRUDModel.Model.MasterViewModel.BasicSalary != 0) {
            $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreasePercentage = (parseFloat($scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreaseAmount) * 100) / parseFloat($scope.CRUDModel.Model.MasterViewModel.BasicSalary);
            //if ($scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreasePercentage.isNaN()) {
            //    $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreasePercentage = "";
            //}
            //else {
            //    $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreasePercentage = $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreasePercentage.toFixed(2);
            //}
            $scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncrease = (parseFloat($scope.CRUDModel.Model.MasterViewModel.BasicSalary) + parseFloat($scope.CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases[rowIndex].ProposedIncreaseAmount)).toFixed(3);


        }
    }

    $scope.Edit = function (view, ID) {
        if ($scope.ShowWindow("Edit" + view, "Edit " + view, "Edit" + view))
            return;

        $("#Overlay").fadeIn(100);

        var editUrl = view + "/Edit/" + ID.toString();
        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $("#LayoutContentSection").append($compile(result.data)($scope));
                $scope.AddWindow("Edit" + view, "Edit " + view, "Edit" + view);
                $("#Overlay").fadeOut(100);
            });
    }

    $scope.CreateVoucher = function () {
        $http({ method: 'GET', url: 'Voucher/CreateVoucher ' })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.VoucherNo = result.data;
            })
    }

    $scope.TicketActionChange = function (event, element, selecteditem) {
        if (!(selecteditem == "" || selecteditem == null || selecteditem == undefined)) {
            $(".ActionTabClass").show();
            $(".ActionTabClass").find("div.innertab").hide();
            $(".ActionTabClass").find("div.innertab." + selecteditem).show();
            $(".ActionTabClass").find("ul li").removeClass("active");
            $(".ActionTabClass").find("ul li[data-target='" + selecteditem + "']").addClass("active");
        }
        else {
            $(".ActionTabClass").hide();
        }

    }

    $scope.CustomerFirstNameChange = function (FirstName, Contacts) {
        if (Contacts != null && Contacts != undefined && Contacts != '' && Contacts.length > 0) {
            for (var i = 0; i <= Contacts.length - 1; i++) {
                if (Contacts[i].FirstName == null || Contacts[i].FirstName == undefined || Contacts[i].FirstName == '') {
                    Contacts[i].FirstName = FirstName;
                }
            }
        }

    }

    $scope.CustomerLastNameChange = function (LastName, Contacts) {
        if (Contacts != null && Contacts != undefined && Contacts != '' && Contacts.length > 0) {
            for (var i = 0; i <= Contacts.length - 1; i++) {
                if (Contacts[i].LastName == null || Contacts[i].LastName == undefined || Contacts[i].LastName == '') {
                    Contacts[i].LastName = LastName;
                }
            }
        }

    }

    $scope.SupplierChange = function (event, element) {
        var supplierID = null;
        supplierID = $scope.CRUDModel.Model.MasterViewModel.Supplier.Key;

        if (supplierID > 0) {
            GetSupplierDelivery(supplierID);
        }
    }

    function GetSupplierDelivery(supplierID) {
        $.ajax({
            type: "GET",
            url: 'Supplier/GetSupplierDeliveryMethod?supplierID=' + supplierID,
            success: function (result) {
                $scope.$apply(function () {
                    $scope.CRUDModel.Model.MasterViewModel.ReceivingMethod = result;
                });
            }
        });
    };

    $scope.SupplierChangeOnReturn = function (event, element) {
        var supplierID = null;
        supplierID = $scope.CRUDModel.Model.MasterViewModel.Supplier.Key;

        if (supplierID > 0) {
            GetSupplierReturn(supplierID);
        }
    }

    function GetSupplierReturn(supplierID) {
        $.ajax({
            type: "GET",
            url: 'Supplier/GetSupplierReturnMethod?supplierID=' + supplierID,
            success: function (result) {
                $scope.$apply(function () {
                    $scope.CRUDModel.Model.MasterViewModel.ReturnMethod = result;
                });
            }
        });
    };

    $scope.LoadUniqueSettings = function (lookupName, model, rowIndex) {
        $scope.LookUps[lookupName + "Master"] = [];

        if ($scope.LookUps[lookupName]) {
            var fitlers = new JSLINQ($scope.LookUps[lookupName]).Where(function (setting) {
                var exists = new JSLINQ(model)
                    .Where(function (item) {
                        if (item == undefined || item.SettingCode == null) return false;
                        return item.SettingCode.Key == setting.Key;
                    });
                if (exists.items.length == 0) {
                    $scope.LookUps[lookupName + "Master"].push(setting);
                }
            });
        }
    };

    $scope.OnSelect2ChangeOverrides = function (currentRow, control) {

    };

    $scope.SearchStudent = function ($event, $element) {
        var url;
        var classID = 0;
        var sectionID = 0;
        if ($scope.CRUDModel.Model == undefined)
            return false;
        if ($scope.CRUDModel.Model.ClassID == undefined || $scope.CRUDModel.Model.ClassID == null) {
            if ($scope.CRUDModel.Model.StudentClass.Key != undefined && $scope.CRUDModel.Model.StudentClass.Key != null) {
                classID = $scope.CRUDModel.Model.StudentClass.Key;
            }
            else
                classID = 0;
        }
        else {
            classID = $scope.CRUDModel.Model.ClassID;
        }
        if ($scope.CRUDModel.Model.SectionID == undefined || $scope.CRUDModel.Model.SectionID == null) {
            if ($scope.CRUDModel.Model.Section.Key != undefined && $scope.CRUDModel.Model.Section.Key != null) {
                sectionID = $scope.CRUDModel.Model.Section.Key;
            }
            else
                sectionID = 0;
        }
        else {
            sectionID = $scope.CRUDModel.Model.SectionID;
        }

        if (sectionID != 0 && classID != 0) {
            url = 'Schools/School/SearchStudent?classID=' + classID + "&sectionID=" + sectionID;
        }
        else if (classID != 0) {
            url = 'Schools/School/SearchStudent?classID=' + classID;
        } else if (sectionID != 0) {
            url = 'Schools/School/SearchStudent?sectionID=' + sectionID;
        }

        $.ajax({
            type: "GET",
            url: url,
            success: function (result) {
                $scope.$apply(function () {
                    $scope.CRUDModel.Model.LooksUps['Students'] = result;
                });
            }
        });
    };

    $scope.GetNextSequence = function (type, model, field) {
        $.ajax({
            type: "GET",
            url: 'Mutual/GetNextSequence?sequenceType=' + type,
            success: function (result) {
                $scope.$apply(function () {
                    model[field] = result;
                });
            }
        });
    };

    $scope.MailTransactionsReport = function (event, reportName, reportHeader, reportFullName) {

        var headIID = null;
        var emailID = null;
        //var reportTitle = null;
        var feeCollectionData = {};

        if ($scope.CRUDModel.Name == 'FeeCollection') {
            headIID = $scope.CRUDModel.ViewModel.FeeCollectionIID;
            emailID = $scope.CRUDModel.ViewModel.EmailID;
            //reportTitle = $scope.CRUDModel.ViewModel.AdmissionNumber + '_' + $scope.CRUDModel.ViewModel.FeeReceiptNo;

            if (emailID == null || emailID == '-') {
                $().showMessage($scope, $timeout, true, "Please update with valid Email ID");
                return false;
            } else {

                feeCollectionData = {
                    "FeeCollectionIID": headIID,
                    "AdmissionNo": $scope.CRUDModel.ViewModel.AdmissionNumber,
                    "FeeReceiptNo": $scope.CRUDModel.ViewModel.FeeReceiptNo,
                    "EmailID": emailID,
                    "SchoolID": $scope.CRUDModel.ViewModel.SchoolID,
                }

                $http({
                    method: 'POST',
                    url: "Home/GenerateAndEmailFeeReceipt",
                    data: feeCollectionData
                }).then(function (result) {
                    if (!result.data.IsError) {
                        $().showMessage($rootScope, $timeout, false, result.data.Response);
                    }
                    else {
                        $().showMessage($rootScope, $timeout, true, "Mail Sending Failed");
                    }
                });
            }
        }
    }

    $scope.MailReport = function (event, reportName, reportHeader, reportFullName) {

        var headIID = null;
        var emailID = null;
        //var reportTitle = null;
        var feeCollectionData = {};

        if ($scope.CRUDModel.Name == 'FeeCollection') {
            headIID = $scope.CRUDModel.ViewModel.FeeCollectionIID;
            emailID = $scope.CRUDModel.ViewModel.EmailID;
            //reportTitle = $scope.CRUDModel.ViewModel.AdmissionNumber + '_' + $scope.CRUDModel.ViewModel.FeeReceiptNo;

            if (emailID == null || emailID == '-') {
                $().showMessage($scope, $timeout, true, "Please update with valid Email ID");
                return false;
            } else {

                feeCollectionData = {
                    "FeeCollectionIID": headIID,
                    "AdmissionNo": $scope.CRUDModel.ViewModel.AdmissionNumber,
                    "FeeReceiptNo": $scope.CRUDModel.ViewModel.FeeReceiptNo,
                    "EmailID": emailID,
                    "SchoolID": $scope.CRUDModel.ViewModel.SchoolID,
                }

                $http({
                    method: 'POST',
                    url: "Home/GenerateAndEmailFeeReceipt",
                    data: feeCollectionData
                }).then(function (result) {
                    if (!result.data.IsError) {
                        $().showMessage($scope, $timeout, false, result.data.Response);
                    }
                    else {
                        $().showMessage($scope, $timeout, true, "Mail Sending Failed");
                    }
                });
            }
        }
    }

    $scope.PrintMasterReport = function (event, reportName, reportHeader, reportFullName) {

        var headIID = null;

        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        if ($scope.CRUDModel.IID != 0 && $scope.CRUDModel.Name != 'CollectFeeAccountPosting') {
            headIID = $scope.CRUDModel.IID;
        }

        if (headIID == null && $scope.CRUDModel.Name != 'CollectFeeAccountPosting') {

            if ($scope.CRUDModel.Name == 'FeeCollection') {
                headIID = $scope.CRUDModel.ViewModel.FeeCollectionIID;
            }

            if ($scope.CRUDModel.Name == 'StudentApplication') {
                headIID = $scope.CRUDModel.ViewModel.ApplicationIID;
            }

            if ($scope.CRUDModel.Name == 'StudentTransferRequest') {
                headIID = $scope.CRUDModel.ViewModel.StudentTransferRequestIID;
            }

        }

        var formattedSDate = null;
        var formattedEDate = null;
        var cashierID = null;
        var parameterObject = {};

        if ($scope.CRUDModel.Name == 'CollectFeeAccountPosting') {
            cashierID = $scope.CRUDModel.ViewModel.CashierEmployee != null ? $scope.CRUDModel.ViewModel.CashierEmployee.Key : null;
            var fromDateSting = $scope.CRUDModel.ViewModel.CollectionDateFromString;
            var toDateString = $scope.CRUDModel.ViewModel.CollectionDateToString;

            if (fromDateSting == null || toDateString == null || !cashierID) {
                $().showMessage($scope, $timeout, true, "Please fill out required fields!");
                return false;
            } else {
                var sDate = new Date(moment(fromDateSting, 'DD/MM/YYYY'));
                //format that date into a different format
                formattedSDate = moment(sDate).format("MM/DD/YYYY");

                var eDate = new Date(moment(toDateString, 'DD/MM/YYYY'));
                //format that date into a different format
                formattedEDate = moment(eDate).format("MM/DD/YYYY");

                parameterObject = {
                    "SDATE": formattedSDate,
                    "EDATE": formattedEDate,
                    "CASHIER_ID": cashierID,
                };
            }
        }
        else {
            if ($scope.CRUDModel.Name == 'FeeCollection') {
                parameterObject = {
                    "FeeCollectionIID": headIID
                };
            }
            else if ($scope.CRUDModel.Name == 'StudentApplication') {
                parameterObject = {
                    "ApplicationIID": headIID
                };
            }
            else if ($scope.CRUDModel.Name == 'StudentTransferRequest') {
                parameterObject = {
                    "StudentTransferRequestIID": headIID
                };
            }
            else {
                parameterObject = {
                    "HeadID": headIID
                };
            }
        }

        var reportingService = $rootScope.ReportingService;

        if (reportingService == "ssrs") {

            //SSRS report viewer start
            if ($scope.CRUDModel.Name == 'CollectFeeAccountPosting') {
                showOverlay();

                $.ajax({
                    url: utility.myHost + "Reports/Report/GetReportUrlandParameters?reportName=" + reportName,
                    type: 'GET',
                    success: function (result) {
                        if (result.Response) {
                            reportUrl = result.Response + "&SDATE=" + formattedSDate + "&EDATE=" + formattedEDate + "&CASHIER_ID=" + cashierID;
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
                hideOverlay();
            }
            else {
                $.ajax({
                    url: utility.myHost + "Reports/Report/GetReportUrlandParameters?reportName=" + reportName,
                    type: 'GET',
                    success: function (result) {
                        if (result.Response) {
                            reportUrl = result.Response + "&HeadID=" + headIID;
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

            }
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


    $scope.ExpandCollapase = function (event, model, field) {
        model[field] = !model[field];
        var $groupRow = $(event.currentTarget).closest('tr').next();

        if (model[field]) {
            $groupRow.slideUp('slow');
        } else {
            $groupRow.slideDown('slow');
        }
    };
    $scope.FeeMasterChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        var url = "Frameworks/CRUD/Get?screen=FeeMaster&ID=" + model.FeeMaster.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.FeeMonthly = [];
                model.IsFeePeriodDisabled = result.data.FeeCycle != 1;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FeePeriodChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        model.FeeMonthly = [];
        var url = "Schools/School/GetSplitUpPeriod?periodID=" + model.FeePeriod.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.FeeMonthly = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
    $scope.SplitAmount = function ($event, $element, gridModel) {
        showOverlay();
        gridModel.FeeMonthly.l.forEach(x => {
            x.Amount = Math.round(gridModel.Amount / gridModel.FeeMonthly.length, 2);
            //x.TaxPercentage = Math.round(gridModel.TaxPercentage / gridModel.FeeMonthly.length, 2);
            //x.TaxAmount = gridModel.TaxAmount / gridModel.FeeMonthly.length;
        });
        hideOverlay();
    };

    $scope.GetTotalMarks = function (data) {
        if (typeof (data) === 'undefined') {
            return 0;
        }

        var sum = 0;

        for (var i = data.length - 1; i >= 0; i--) {
            if ((data[i].MaximumMarks)) {
                sum += parseFloat(data[i].MaximumMarks);
            }
        }
        return sum;
    };

    $scope.LoadCommunication = function (row, event, ID, screenID) {
        $rootScope.LoadCommunication(row, event, ID, screenID, $scope.CrudWindowContainer);
    };


    $scope.GetStudentTransportDatas = function (viewModel, TransportApplctnStudentMapIID) {
        console.log("functionLoaded");
        showOverlay();
        var model = viewModel;
        var IID = TransportApplctnStudentMapIID;
        $scope.$apply(function () {
            var url = "Schools/School/GetStudentTransportApplication?TransportApplctnStudentMapIID=" + IID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    var academic = result.data.AcademicYear;
                    var student = result.data.Student;
                    var appNumber = result.data.ApplicationNumber;
                    var startDate = result.data.DateFrom;
                    var schoolID = result.data.SchoolID;
                    model.TransportApplctnStudentMapID = result.data.TransportApplctnStudentMapID;
                    model.ApplicationNumber = appNumber;
                    model.Student = $scope.LookUps.StudentList.find(x => x.Key == student.Key, x.Value == student.Value);
                    model.DateFromString = startDate;
                    model.SchoolID = schoolID;
                    if (academic != undefined) {
                        model.AcademicYear = $scope.LookUps.AcademicYear.find(x => x.Key == academic.Key, x.Value == academic.Value);
                    }
                    else {
                        $().showMessage($scope, $timeout, true, 'Please select  school properly!');
                        return false;
                    }
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        });
    };

    $scope.UploadCropImageFiles = function (uploadfiles, url, imageType, prefixAsString, sourceModelAsString,
        dataRow, index, element, imageWidth, imageHeight) {
        if (!imageWidth) {
            imageWidth = 1308;
        }

        if (!imageHeight) {
            imageHeight = 859;
        }

        var file = uploadfiles.files[0];
        if (file) {
            if ($rootScope.cropper) {
                $rootScope.cropper.destroy();
            }
            $(".dynamicPopoverOverlay").show();
            $("#dynamicPopover").addClass('cropPopup');
            var parentControl = $(element).closest('.controls');
            $rootScope.ShowPopup($(parentControl).find('.croppingImagePopover'), $scope.CrudWindowContainer);

            var newImage = new Image();

            if (/^image\/\w+/.test(file.type)) {
                uploadedImageType = file.type;
                uploadedImageName = file.name;
                newImage.src = URL.createObjectURL(file);
                $rootScope.cropper = new Cropper(newImage, {
                    dragMode: 'move',
                    aspectRatio: 1 / (imageHeight / imageWidth),
                    movable: false,
                    scalable: false,
                    zoomable: true,
                    restore: false,
                    guides: true,
                    center: false,
                    highlight: false,
                    cropBoxMovable: true,
                    cropBoxResizable: false,
                    toggleDragModeOnDblclick: false,
                    fillColor: '#ffffff'
                });
            }
            $('.dynamicPopoverContainer').html(newImage);
            $('.dynamicPopoverContainer')
                .append("<div class='cropBtn'><button class='button-orange' onclick='angular.element(this).scope().SaveCroppedImage(\""
                    + file.name + "\",\"" + url + "\",\""
                    + prefixAsString + "\"," + (index ? index : -1).toString()
                    + ",\"" + sourceModelAsString + "\",\""
                    + imageType + "\"," + imageWidth + "," + imageHeight
                    + ")'>Save</button></div>");

        }

        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    };

    $rootScope.SaveCroppedImage = function (fileName, url, prefixAsString, index, sourceModelAsString,
        imageType, imageWidth, imageHeight) {
        if ($rootScope.cropper) {
            var xhr = new XMLHttpRequest()
            var fd = new FormData()
            $rootScope.cropper.getCroppedCanvas({
                fillColor: '#ffffff',
                width: imageWidth,
                height: imageHeight
            }).toBlob(function (blob) {
                fd.append('imageType', imageType);
                fd.append('png', blob, fileName);
                xhr.open('POST', url, true)
                xhr.onreadystatechange = function (url) {
                    if (xhr.readyState == 4 && xhr.status == 200) {
                        var result = JSON.parse(xhr.response)
                        if (result.Success == true && result.FileInfo.length > 0) {
                            $scope.$apply(function () {
                                var model = GetVariableFromStrings($scope, prefixAsString);
                                if (index != -1) {
                                    model[index][sourceModelAsString] = result.FileInfo[0].FilePath;
                                }
                                else {
                                    model[sourceModelAsString] = result.FileInfo[0].FilePath;
                                }

                                if (model['New' + sourceModelAsString + 's']) {
                                    model['New' + sourceModelAsString + 's'].push({ 'FilePath': result.FileInfo[0].FilePath });
                                }
                            })
                        }
                    }
                }
                xhr.send(fd)
            });
        }

        $('#dynamicPopover .close').trigger('click');
    }

    $scope.ClosePopup = function (event) {
        $(event.target).closest('.popupwindow').fadeOut().removeClass('show');
        $('.overlaydiv').removeClass('whitebg').fadeOut();
        $('.popupwindow').removeClass('moveleft');
    }

    function GetVariableFromStrings(root, variableString) {
        var object = root

        if (variableString) {
            $.each(variableString.split('.'), function (index, value) {
                object = object[value]
            })
        }

        return object
    }

    $scope.OnKeyPressSearchTextFocus = function () {
        $scope.CRUDModel.Model.MasterViewModel.SearchText = null;
        document.getElementById("SearchText").focus();
    }

    $scope.GetNextTransactionNumberByDocumentType = function (documentTypeID) {

        if (documentTypeID) {
            url = utility.myHost + 'Mutual/GetNextTransactionNumber?documentTypeID=' + documentTypeID;

            $http.get(url).then(function (response) {
                $scope.CRUDModel.ViewModel.TransactionNo = response.data;
                return true;
            });
        }
    }

    $scope.BarcodeScan = function ($event) {
        $event.stopPropagation();
        $event.preventDefault();
        //var data = $scope.CRUDModel.Model.MasterViewModel;
        if ($event.keyCode == 13) {
            $('#Overlay').show();
            $.ajax({
                type: "GET",
                url: "Catalogs/ProductSKU/SOSearch?searchText=" + $scope.CRUDModel.Model.MasterViewModel.SOSearch,
                contentType: "json",
                success: function (result) {
                    if (result) {
                        if (result != 0) {
                            $.ajax({
                                type: "GET",
                                url: 'Inventories/SalesOrder/Get/' + result, // Same URL as above. Is this intentional?
                                contentType: "json",
                                success: function (result1) {
                                    $('#Overlay').hide();
                                    if (result1.MasterViewModel.SITransactionHeadID) {
                                        $().showMessage($scope, $timeout, true, 'A sales invoice has already been processed for this sales order.!');
                                        return false;
                                    }
                                    else {

                                        try {
                                            result1.MasterViewModel.TransactionHeadIID = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
                                        } catch (ex) {
                                            result1.MasterViewModel.TransactionHeadIID = 0;
                                        }
                                        result1.MasterViewModel.DocumentType = $scope.CRUDModel.Model.MasterViewModel.DocumentType;
                                        result1.MasterViewModel.TransactionNo = $scope.CRUDModel.Model.MasterViewModel.TransactionNo;
                                        result1.MasterViewModel.ReferenceTransactionNo = $scope.CRUDModel.Model.MasterViewModel.SOSearch;
                                        //result1.MasterViewModel.TransactionDate = moment($scope.CRUDModel.Model.MasterViewModel.TransactionDate).format(_dateFormat.toUpperCase());

                                        $.each(result1.DetailViewModel, function (index, item) {
                                            item.TransactionDetailID = "0";
                                            item.TransactionHead = "0";
                                        });

                                        $scope.$apply(function () {
                                            var additionalExpenseList = $scope.CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps;
                                            var transEntitlementMap = $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps;
                                            $scope.CRUDModel.Model = result1;
                                            //$scope.CRUDModel.Model.MasterViewModel.Validity = moment(result1.MasterViewModel.Validity).format(_dateFormat.toUpperCase());
                                            $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionHeaderID = result;
                                            $scope.CRUDModel.Model.MasterViewModel.DocumentStatus = null;
                                            $scope.CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps = additionalExpenseList;
                                            $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps = transEntitlementMap;
                                            $scope.ChangeEntitlementAmount();
                                        });

                                        if ($scope.CRUDModel && $scope.CRUDModel.Name == "FOCSales") {
                                            // Loop through the array and update the amount value to 0 for each item
                                            $scope.CRUDModel.Model.DetailViewModel.forEach(function (item) {
                                                item.Amount = 0;
                                            });
                                            $scope.CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps.forEach(function (item) {
                                                item.Amount = 0;
                                            });
                                        }

                                        $scope.BranchChange(null, null);

                                        if ($scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != 0 || $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != undefined || $scope.CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != null) {
                                            $scope.CRUDModel.Model.DetailViewModel.IsDisableSelect2 = true;
                                        }
                                    }
                                }
                            });
                        }
                    }
                    else {
                        $('#Overlay').hide();
                        $().showMessage($scope, $timeout, true, 'No sales orders were found for this number.!');
                        return false;
                    }
                }
            });
        }
    };

    $scope.FillTicketDetails = function (viewModel) {
        var model = viewModel
        dueURL = utility.myHost + 'Supports/Ticket/GetTicketFeeDueDetails?studentID=' + viewModel.StudentID;

        $http.get(dueURL).then(function (result) {

            $timeout(function () {
                $scope.$apply(function () {
                    model.TicketFeeDueMaps = result.data.Response;

                    model.Parent = {
                        "Key": model.ParentID,
                        "Value": model.ParentName,
                    };

                    model.CustomerNotification = model.CustomerNotification == "true" ? true : false;
                    model.IsSendMail = model.IsSendMail == "true" ? true : false;
                    model.IsAutoCreation = model.IsAutoCreation == "true" ? true : false;

                    var sumOfDueAmount = model.TicketFeeDueMaps.reduce((total, current) => total + current.DueAmount, 0);
                    var invoiceNos = model.TicketFeeDueMaps.map(item => item.InvoiceNo).join(', ');

                    model.Description1 = model.Description1 + "</p>  <p>Total Due Amount : " + sumOfDueAmount + " QAR</p>  <p>Invoice numbers : " + invoiceNos /*+ "</p>  <p>InvoiceDate : " + row.InvoiceDate*/ + "</p>  <p>Repayment as soon as possible</p> ";
                });
            }, 1);

        });
    };

    $scope.GetLocationMap = function () {
        return new Promise(async (resolve, reject) => {
            let selectedGeoLocation = {
                Latitude: null,
                Longitude: null
            };

            async function initMap() {
                // Request needed libraries.
                const { Map } = await google.maps.importLibrary("maps");
                const myLatlng = { lat: 25.276379, lng: 51.190652 };
                const map = new google.maps.Map(document.getElementById("mapLatLng"), {
                    zoom: 7,
                    center: myLatlng,
                });

                // Create the initial InfoWindow.
                let infoWindow = new google.maps.InfoWindow({
                    content: "Click the map to get Lat/Lng!",
                    position: myLatlng,
                });

                infoWindow.open(map);

                // Configure the click listener.
                map.addListener("click", (mapsMouseEvent) => {
                    // Close the current InfoWindow.
                    infoWindow.close();

                    // Create a new InfoWindow.
                    infoWindow = new google.maps.InfoWindow({
                        position: mapsMouseEvent.latLng,
                    });

                    selectedGeoLocation.Latitude = mapsMouseEvent.latLng.lat().toFixed(6);
                    selectedGeoLocation.Longitude = mapsMouseEvent.latLng.lng().toFixed(6);

                    infoWindow.setContent(
                        JSON.stringify(mapsMouseEvent.latLng.toJSON(), null, 2)
                    );
                    infoWindow.open(map);

                    // Resolve the promise with the selected location
                    resolve(selectedGeoLocation);
                });
            }

            // Initialize the map and start the process
            initMap();
        });
    };

    $scope.ShowGoogleMap = function (model, latitudeFieldName, longitudeFieldName) {
        $('#globalPopup').modal('show');
        $('#globalPopup').find('.modal-body').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');
        $('#globalPopup').find('.modal-title').html("Map");
        $('#globalPopup').find('.modal-body').html('<div id="mapLatLng" style="height: 638px; width: 100%;"></div>');

        // Call GetLocationMapInNewWindow and use .then() to handle the promise
        $scope.GetLocationMap().then(function (selectedLocation) {
            $scope.$apply(function () {
                model[latitudeFieldName] = selectedLocation.Latitude;
                model[longitudeFieldName] = selectedLocation.Longitude;
            });

            $('#globalPopup').modal('hide');
        });

    };

    $scope.GetEmployeeDetailsByEmployeeID = function (event, element) {
        var requestedID = null;
        requestedID = $scope.CRUDModel.Model.MasterViewModel.Requisitioned.Key;

        $scope.RequisitionedDepartment = null;
        var loadUrl = null;

        loadUrl = "/Inventories/PurchaseRequest/GetEmployeeDetailsByEmployeeID?employeeID=" + requestedID;

        $.ajax({
            type: "GET",
            url: loadUrl,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    }
                    else {
                        $scope.CRUDModel.Model.MasterViewModel.RequisitionedDepartment = result.DepartmentName;
                        $scope.CRUDModel.Model.MasterViewModel.DepartmentID = result.DepartmentID;

                    }
                });
            }
        });
    }

    $scope.BringAssetSerialPopup = function (detail) {

        if (detail.Quantity == 0 || (detail.hasOwnProperty("IsSerailNumberAutoGenerated") == true && detail.IsSerailNumberAutoGenerated == true)) {
            return;
        }

        if (detail.TransactionSerialMaps == null) {
            detail.TransactionSerialMaps = [];
        }

        if (detail.TransactionSerialMaps.length < detail.Quantity) {
            for (i = detail.TransactionSerialMaps.length; i < detail.Quantity; i++) {
                var serialDetail = angular.copy($scope.CRUDModel.DetailViewModel.TransactionSerialMaps)[0];
                serialDetail.AssetID = detail.Assset != null ? detail.Assset.Key : null;
                detail.TransactionSerialMaps.push(serialDetail);
            }
        }
        else if (detail.TransactionSerialMaps.length > detail.Quantity) {
            for (i = detail.TransactionSerialMaps.length; i > detail.Quantity; i--) {
                detail.TransactionSerialMaps.pop();
            }
        }

        $rootScope.AssetDetailViewModel = detail;
    }

    $scope.AssetChanges = function (model) {
        var assetID = model.Asset != null ? model.Asset.Key : null;

        if (assetID) {
            var url = utility.myHost + "Asset/GetAssetDetailsByID?assetID=" + assetID;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    var assetDetails = result.data;
                    model.IsRequiredSerialNumber = assetDetails != null ? assetDetails.IsRequiredSerialNumber : false;

                    hideOverlay();
                }, function () {
                    hideOverlay();
                })
        }
    };


    $scope.LoadCustomerPendingInvoices = function (event, element) {

        $.ajax({
            url: "Accounts/RVInvoice/GetPendingInvoices"
                + "?customerID=" + $scope.CRUDModel.Model.MasterViewModel.DetailAccount.Key,
            type: 'GET',
            success: function (result) {
                if (result != null) {
                    $scope.$apply(function () {
                        $scope.CRUDModel.Model.DetailViewModel = [];
                        $.each(result, function (index, item) {
                            $scope.CRUDModel.Model.DetailViewModel.push({
                                AccountID: item.AccountID,
                                Amount: 0,
                                InvoiceAmount: item.InvoiceAmount,
                                InvoiceNumber: item.InvoiceNumber,
                                PaidAmount: item.PaidAmount,
                                PaymentDueDate: moment(item.TransactionDate).format(_dateFormat.toUpperCase()),
                                ReferenceReceiptID: item.ReceivableIID,
                                ReferencePaymentID: item.PayableIID,
                                ReceivableID: item.ReceivableIID
                                //Remarks: ,
                                //SerNo: item,
                            });
                        });
                    });
                }


            },
            error: function () {
                $().showMessage($scope, $timeout, true, "Error occured!!");

            }
        });
    };

    $scope.BalanceAmountChanges = function (row) {
        if ((row.Amount || 0) <= 0) {
            return false;
        }
        var amountToPay = (row.InvoiceAmount || 0) - (row.PaidAmount || 0);
        if ((row.Amount || 0) > (amountToPay || 0)) {
            $().showMessage($scope, $timeout, true, 'The amount cannot exceed the balance to pay :' + amountToPay);
            row.Amount = null;
        }
    }

    $scope.CheckLastDayOfMonth = function (model, fieldName) {
        if (!model || !model[fieldName]) return; // Ensure model & field exist

        var stringDate = model[fieldName]; // Get the date value

        if (stringDate) {
            var url = utility.myHost + "Home/CheckLastDayOfMonth?stringDate=" + encodeURIComponent(stringDate);

            $http.get(url)
                .then(function (result) {
                    if (result.data.IsError) {
                        $().showMessage($scope, $timeout, true, result.data.Response);

                        // Clear the input field dynamically using model and fieldName
                        model[fieldName] = null;
                    }
                })
                .catch(function (error) {
                    console.error("Error checking last day of month:", error);
                })
                .finally(function () {
                    hideOverlay();
                });
        }
    };

    $scope.GetPendingInvoiceDetails = function (event, element) {

        if (!$scope.CRUDModel.Model.MasterViewModel.InvoiceID && $scope.CRUDModel.Model.MasterViewModel.InvoiceID.Key) {
            return;
        }  
        var combined = $scope.CRUDModel.Model.MasterViewModel.InvoiceID.Key;
        var parts = combined.split('#');
        $scope.CRUDModel.Model.MasterViewModel.ReferenceHeadID = parts[0]; 
        $scope.CRUDModel.Model.MasterViewModel.ReceivableID = parts[1];
        var amountValue = $scope.CRUDModel.Model.MasterViewModel.InvoiceID.Value;

        var match = amountValue.match(/\(([^)]+)\)/);
        var amount = match ? parseFloat(match[1]) : 0;
        $scope.CRUDModel.Model.MasterViewModel.PendingAmount = amount;  
     
    }

    $scope.GetCustomerPendingInvoices = function (event, element) {
        $.ajax({
            url: "Accounts/RegularCredit/GetCustomerPendingInvoices"
                + "?accountID=" + $scope.CRUDModel.Model.MasterViewModel.Account.Key + "&branchID=" + $scope.CRUDModel.Model.MasterViewModel.Branch + "",
            type: 'GET',
            success: function (result) {
                if (result != null && result.length>0) {
                    $scope.$apply(function () {
                        $scope.LookUps.PendingInvoices = result;
                    });
                }
                else {
                    $scope.LookUps.PendingInvoices = null;
                    $scope.CRUDModel.Model.MasterViewModel.InvoiceID.Key = null;
                    $scope.CRUDModel.Model.MasterViewModel.InvoiceID.Value = null;
                }
            },
            error: function () {
                $().showMessage($scope, $timeout, true, "Error occured!!");

            }
        });
    }

}]);