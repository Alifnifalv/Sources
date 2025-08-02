(function () {
    'use strict';

    angular
        .module('Eduegate.ERP.Admin')
        .controller('TableRowsController', controller);

    controller.$inject = ['commonService', '$scope', '$compile', '$http', '$timeout', '$rootScope'];

    function controller(commonService, $scope, $compile, $http, $timeout, $root) {
        /* jshint validthis:true */
        var vm = this;
        vm.commonService = commonService;
        vm.rows = {};
        vm.param = {}
        vm.SelectAll = false;
        vm.TotalRecords = 0;
        vm.TotalPages = 0;
        vm.CurrentPage = 0;
        vm.PageSize = 0;
        vm.GoToPageNo = 0;
        vm.ControlerName = '';
        vm.SortBy = null;

        vm.DateTimeFormat = 'medium';
        vm.DateFormat = _dateFormat;
        vm.QuickFilter = [];
        vm.WindowContainer = null;
        vm.ViewName = '';
        vm.HasFilters = false;
        vm.LookUps = [];

        //filter panel
        vm.IsShowFilterView = false;
        vm.IsSortableListViewToggle = true;
        vm.RuntimeFilter = null;

        vm.SwitchView = function (event) {
            vm.IsSortableListViewToggle = !vm.IsSortableListViewToggle;
            var gridconatiner = $(event.currentTarget).closest('.windowcontainer');
            $(gridconatiner).html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

            $.ajax({
                type: "GET",
                url: vm.ControlerName + '/List?view=' + vm.ViewName + '&isSortableList=' + vm.IsSortableListViewToggle,
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(vm.QuickFilter),
                success: function (result) {
                    $(gridconatiner).replaceWith($compile(result)($scope));
                    $scope.ShowWindow(vm.ViewName + 'Lists', vm.ViewName + ' List', vm.ViewName)
                },
            });
        }

        $scope.$watch(function (scope) { return vm.SortBy },
            function (newValue, oldValue) {
                if (newValue != oldValue)
                    vm.ReLoad();
            }
        );

        vm.activate = function (window, controllerName, quickFilterViewModel, viewName, hasFilter, appliedSort, runtimeFilter) {
            if (quickFilterViewModel != undefined)
                vm.QuickFilter = JSON.parse(quickFilterViewModel);

            vm.IsSortableListViewToggle = appliedSort;
            vm.HasFilters = hasFilter;
            vm.rows = {};
            vm.ControlerName = controllerName;


            var url = vm.ControlerName + '/SearchData?view=' + viewName + '&currentPage=1';
            vm.RuntimeFilter = runtimeFilter;

            if (vm.RuntimeFilter) {
                url = url + "&runtimeFilter=" + vm.RuntimeFilter + "";
            }

            vm.commonService.get(url, vm.param, LoadSuccess);

            if (window !== undefined) {
                vm.WindowContainer = window;
            }

            vm.ViewName = viewName;
        }

        vm.Comments = function (viewName, index, event) {
            var yposition = event.pageY - 234;
            $('.popup.gridpopupfields', $(vm.WindowContainer)).slideDown("fast");
            $('.popup.gridpopupfields', $(vm.WindowContainer)).css({ 'top': yposition });
            $('.transparent-overlay', $(vm.WindowContainer)).show();
            $.ajax({
                url: "Mutual/Comment?type=Transaction&referenceID=" + GetIDColumnValue(row),
                type: 'GET',
                success: function (content) {
                    $('#commentPanel', $(WindowContainer)).html($compile(content)($scope));
                }
            });
        };

        vm.LoadLookups = function (lookupName, isLazyLoad, $select) {
            if (!isLazyLoad && vm.LookUps[lookupName] != null)
                return;

            vm.LookUps[lookupName] = [];
            vm.LookUps[lookupName].push({ Key: "", Value: "Loading.." });
            var loadUrl;

            if (!isLazyLoad) {
                loadUrl = "Mutual/GetLookUpData?lookType=" + lookupName;
            }
            else {
                loadUrl = "Mutual/GetLazyLookUpData?lookType=" + lookupName + "&lookupName=" + lookupName + "&searchText=" + $select.search;
            }

            $.ajax({
                type: "GET",
                url: loadUrl,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result.Data == undefined) {
                            vm.LookUps[lookupName] = result;
                        }
                        else {
                            vm.LookUps[lookupName] = result.Data;
                        }
                    });
                }
            });
        };

        vm.SortClick = function (event, columnName) {
            var order = 'asc';

            if ($(event.currentTarget).hasClass('asc')) {
                order = 'desc';
            }
            else if ($(event.currentTarget).hasClass('asc')) {
                order = 'asc';
            }

            $(event.currentTarget).closest('thead').find('a').removeClass('asc');
            $(event.currentTarget).closest('thead').find('a').removeClass('desc');
            $(event.currentTarget).addClass(order);

            vm.SortBy = columnName + ' ' + order;
        };

        vm.QuickSearch = function (event) {
            vm.rows = {};
            $('.tablewrapper .content-load', vm.WindowContainer).show();

            $.ajax({
                type: "POST",
                url: 'Mutual/SaveFilter',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(vm.QuickFilter),
                success: success,
            });
        };

        vm.ResetQuickSearch = function () {
            $.each(vm.QuickFilter, function (index, data) {
                data.FilterValue = null;
                data.FilterValue2 = null;
                data.FilterValue3 = null;
                data.IsDirty = false;
            });

            vm.rows = {};
            $('.tablewrapper .content-load', vm.WindowContainer).show();

            $.ajax({
                type: "POST",
                url: 'Mutual/ResetFilter',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(vm.QuickFilter),
                success: success,
            });
        };

        //Export data to Selected file format
        vm.ExportData = function (event) {
            $("#Overlay").fadeIn(100);
            $.ajax({
                type: 'POST',
                url: 'Settings/Export/ExportData?viewName=' + vm.ViewName,
                contentType: 'application/json',
                success: function (result) {
                    if (result.Success == true)
                        window.location = result.FilePath;
                    else
                        alert(result.Message);
                },
                complete: function () {
                    $("#Overlay").fadeOut(100);
                }
            });
        };

        function success() {
            $('.preload-overlay').hide();
            $('.preloader').hide();
            vm.activate(vm.WindowContainer, vm.ControlerName, JSON.stringify(vm.QuickFilter), vm.ViewName, vm.HasFilters, vm.IsSortableListViewToggle, vm.RuntimeFilter);
        }

        function LoadSuccess(response) {

            if (response.data.IsError) {

                if (response.data.IsError == true) {
                    $().showGlobalMessage($root, $timeout, true, response.data.Message);
                    return;
                }
            }

            var datas = JSON.parse(response.data);
            vm.rows = datas.Datas;
            console.log(datas);
            vm.TotalRecords = datas.TotalRecords;
            vm.CurrentPage = datas.CurrentPage;
            vm.TotalPages = datas.TotalPages;
            vm.PageSize = datas.PageSize;
            vm.GoToPageNo = datas.GoToPageNo;
            $('.tablewrapper .content-load').hide();
        }

        vm.SelectAllHeader = function (event, rows) {
            $.each(rows, function (index, data) {
                data.IsRowSelected = vm.SelectAll;
            });
        };

        vm.CustomizeUserView = function (event, viewName) {
            $http({ method: 'Get', url: 'Mutual/CustomizeUserView?view=' + viewName })
                .then(function (result) {
                    $("#LayoutContentSection").append($compile(result.data)($scope));
                    $('.overlaydiv').fadeIn(500);
                });
        };

        vm.RowSelection = function (event, row) {
            vm.SelectAll = false;
            var iid = GetIDColumnValue(row);
            var index = $scope.SelectedIds.indexOf(iid);

            if (index > -1) {
                $scope.SelectedIds.splice(index, 1);
            }

            $scope.SelectedIds.push(iid);
            event.stopPropagation();
        };

        vm.ReLoad = function (event) {
            vm.rows = {};
            $('.tablewrapper .content-load', vm.WindowContainer).show();

            var url = vm.ControlerName + '/SearchData?view=' + vm.ViewName + '&currentPage=1&orderby=' + vm.SortBy;

            if (vm.RuntimeFilter) {
                url = url + "&runtimeFilter=" + vm.RuntimeFilter + "";
            }

            vm.commonService.get(url, vm.param, LoadSuccess);
        };

        vm.FirstPage = function (event) {
            if (vm.CurrentPage != 1) {
                vm.rows = {};
                $('.tablewrapper .content-load', vm.WindowContainer).show();

                var url = vm.ControlerName + '/SearchData?view=' + vm.ViewName + '&currentPage=1&orderby=' + vm.SortBy;

                if (vm.RuntimeFilter) {
                    url = url + "&runtimeFilter=" + vm.RuntimeFilter + "";
                }

                vm.commonService.get(url, vm.param, LoadSuccess)
            }
        };

        vm.PreviousPage = function (event) {
            if (vm.CurrentPage != 1) {
                vm.rows = {};
                $('.tablewrapper .content-load', vm.WindowContainer).show();
                var url = vm.ControlerName + '/SearchData?view=' + vm.ViewName + '&currentPage=' + (vm.CurrentPage - 1) + "&orderby=" + vm.SortBy;

                if (vm.RuntimeFilter) {
                    url = url + "&runtimeFilter=" + vm.RuntimeFilter + "";
                }

                vm.commonService.get(url, vm.param, LoadSuccess)
            }
        };

        vm.NextPage = function (event) {
            if (vm.CurrentPage < vm.TotalPages) {
                vm.rows = {};
                $('.tablewrapper .content-load', vm.WindowContainer).show();

                var url = vm.ControlerName + '/SearchData?view=' + vm.ViewName + '&currentPage=' + (vm.CurrentPage + 1) + "&orderby=" + vm.SortBy;

                if (vm.RuntimeFilter) {
                    url = url + "&runtimeFilter=" + vm.RuntimeFilter + "";
                }

                vm.commonService.get(url, vm.param, LoadSuccess);
            }
        };

        vm.LastPage = function (event) {
            if (vm.CurrentPage != vm.TotalPages) {
                vm.rows = {};
                $('.tablewrapper .content-load', vm.WindowContainer).show();

                var url = vm.ControlerName + '/SearchData?view=' + vm.ViewName + '&currentPage=' + vm.TotalPages + "&orderby=" + vm.SortBy;

                if (vm.RuntimeFilter) {
                    url = url + "&runtimeFilter=" + vm.RuntimeFilter + "";
                }

                vm.commonService.get(url, vm.param, LoadSuccess);
            }
        }

        vm.GoToPage = function (event) {

            if (vm.GoToPageNo >= 1 && vm.GoToPageNo <= vm.TotalPages) {
                vm.rows = {};
                $('.tablewrapper .content-load', vm.WindowContainer).show();

                var url = vm.ControlerName + '/SearchData?view=' + vm.ViewName + '&currentPage=' + vm.GoToPageNo + "&orderby=" + vm.SortBy;

                if (vm.RuntimeFilter) {
                    url = url + "&runtimeFilter=" + vm.RuntimeFilter + "";
                }

                vm.commonService.get(url, vm.param, LoadSuccess);
            }
        }

        //filter panel related code
        vm.LoadFilterView = function () {
            $(".filterdropdown", vm.WindowContainer).attr("style", "");
            if (!vm.IsShowFilterView) {
                if ($('#filterContent', vm.WindowContainer).html() == '') {
                    $("#filterContent", vm.WindowContainer).html(' <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');
                    vm.commonService.get('Mutual/Filter?view=' + vm.ViewName, vm.param, LoadFilterPanel)
                }
            }

            vm.IsShowFilterView = !vm.IsShowFilterView;
        };

        function LoadFilterPanel(result) {
            $("#filterContent", vm.WindowContainer).html($compile(result.data)($scope));
        }

        vm.CloseFilterView = function () {
            vm.IsShowFilterView = false;
        };

        $scope.CloseWindow = function (event) {
            var window = $(event.currentTarget).closest('.windowcontainer');
            $scope.CloseWindowTab(window.attr('windowindex'));
            window.removeClass('active');
        };

        vm.EditViewCRUDFramework = function (viewName, index, event, parameter, viewTitle) {
            vm.EditView(viewName, index, event, true, parameter, viewTitle);
        };

        vm.CRUDDuplicate = function (viewName, index, event, parameter) {
            $root.showConfirmationPopup(
                {
                    name: 'duplicateConfirm',
                    title: 'Confirm copy',
                    message: 'Are you sure you want to create a copy',
                    firstButtonLabel: 'Cancel',
                    secondButtonLabel: 'Copy',
                    confirmEvent: function () {
                        vm.Duplicate(viewName, index, event, parameter, true);
                    }
                }
            );
        }

        vm.Duplicate = function (viewName, index, event, parameter, isCRUD) {
            var duplicateUrl;
            //var IDValue = GetIDColumnValue(vm.rows[index]);
            var IDValue = index;
            vm.rows = {}
            $('.tablewrapper .content-load', vm.WindowContainer).show();

            if (isCRUD) {
                duplicateUrl = 'Frameworks/CRUD/Duplicate?screen=' + viewName + '&ID=' + IDValue;

                if (parameter) {
                    duplicateUrl = duplicateUrl + '&parameters=' + parameter
                }
            } else {
                duplicateUrl = viewName + '/Clone/' + IDValue;
            }

            $http({ method: 'Get', url: duplicateUrl })
                .then(function (result) {
                    $('.tablewrapper .content-load', vm.WindowContainer).hide();
                    vm.ReLoad();

                    if (result.data.IsError == true) {
                        $().showGlobalMessage($root, $timeout, true, result.data.UserMessage)
                        return
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, false, result.data.UserMessage)
                    }

                }, function () {
                    $().showGlobalMessage($root, $timeout, true, "Error occured while creating the copy.")
                    $('.tablewrapper .content-load', vm.WindowContainer).hide();
                    vm.ReLoad();
                });
        }

        vm.EditView = function (viewName, index, event, isCRUD, parameter, viewTitle) {
            event.stopPropagation();
            var windowName = viewName.substring(viewName.indexOf('/') + 1);
            var editUrl;

            if (!viewTitle) {
                viewTitle = viewName;
            }

            switch (windowName) {
                case "SKU":
                case "Product":
                case "SupplierProduct":
                    viewName = "Product";
                    viewTitle = 'Product';
                    windowName = "Product";

                    /*  if ($scope.ShowWindow("Edit" + windowName, "Edit " + viewTitle, "Edit" + windowName))*/
                    if ($scope.ShowWindow("Edit" + windowName, viewTitle, "Edit" + windowName))
                        return;

                    //$scope.AddWindow("Edit" + windowName, "Edit " + viewTitle, "Edit" + windowName);
                    $scope.AddWindow("Edit" + windowName, viewTitle, "Edit" + windowName);
                    var productID = null;

                    if (vm.rows[index].ProductID) {
                        productID = vm.rows[index].ProductID;
                    } else {
                        productID = vm.rows[index].ProductIID;
                    }

                    editUrl = 'Catalogs/Product/Edit?&ID=' + productID;

                    $http({ method: 'Get', url: editUrl })
                        .then(function (result) {
                            $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                            $scope.ShowWindow("Edit" + windowName, viewTitle, "Edit" + windowName);
                        });
                    break;
                case "PointOfSale":
                    if ($scope.ShowWindow("Edit" + windowName, viewTitle, "Edit" + windowName))
                        return;

                    $scope.AddWindow("Edit" + windowName, viewTitle, "Edit" + windowName);
                    editUrl = 'Inventories/PointOfSale/Edit?ID=' + GetIDColumnValue(vm.rows[index]);

                    $http({ method: 'Get', url: editUrl })
                        .then(function (result) {
                            $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                            $scope.ShowWindow("Edit" + windowName, viewTitle, "Edit" + windowName);
                        });
                    break;
                case "Price":
                    if ($scope.ShowWindow("Edit" + windowName, viewTitle, "Edit" + windowName))
                        return;

                    $scope.AddWindow("Edit" + windowName, viewTitle, "Edit" + windowName);
                    editUrl = 'Catalogs/Price/Edit?ID=' + GetIDColumnValue(vm.rows[index]);

                    $http({ method: 'Get', url: editUrl })
                        .then(function (result) {
                            $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                            $scope.ShowWindow("Edit" + windowName, viewTitle, "Edit" + windowName);
                        });
                    break;
                default:
                    if ($scope.ShowWindow("Edit" + windowName, viewTitle, "Edit" + windowName))
                        return;

                    $scope.AddWindow("Edit" + windowName, viewTitle, "Edit" + windowName);

                    if (isCRUD) {
                        editUrl = 'Frameworks/CRUD/Create?screen=' + viewName + "&ID=" + GetIDColumnValue(vm.rows[index]);

                        if (parameter) {
                            editUrl = editUrl + "&parameters=" + parameter;
                        }
                    }
                    else {
                        editUrl = viewName + "/Edit/" + GetIDColumnValue(vm.rows[index]);
                    }

                    $http({ method: 'Get', url: editUrl })
                        .then(function (result) {
                            $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                            $scope.ShowWindow("Edit" + windowName, viewTitle, "Edit" + windowName);
                        });
                    break;
            }
        };

        vm.GetChildView = function (viewName, index, event, childview) {

            event.stopPropagation();
            var currentRow = event.currentTarget;

            if ($(currentRow).hasClass('loading'))
                return;

            if ($(currentRow).hasClass('loaded')) {
                toggleGridTree(currentRow);
                return;
            }

            var extraFilter = "";

            switch (viewName) {
                case 'Marketplace/SupplierProduct':
                case 'Marketplace/SupplierProductPriceSetting':
                    var branchID = GetColumn(vm.rows[index], "BranchID");
                    extraFilter = "?branchID=" + branchID;
                    break;
                default:
                    extraFilter = "";
            }

            var colspan = $($(currentRow).closest('tr')).find('td').length;

            //set the status loading
            $(currentRow).addClass('loading');
            $(currentRow).closest('tr').after('<tr class="gridloader"><td colspan=' + colspan + '> <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint"></span></center></tr></td>');
            // get children
            var url = viewName + "/ChildList?view=" + childview + "&ID=" + GetIDColumnValue(vm.rows[index]) + extraFilter;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    //create subrow
                    var colspan = $($(currentRow).closest('tr')).find('td').length;
                    $(currentRow).closest('tr').next('tr').remove();
                    result = result.data.replace('%%COLSPAN%%', colspan);
                    $(currentRow).closest('tr').after($compile(result)($scope));
                    // expand
                    toggleGridTree(currentRow);

                    $(currentRow).removeClass('loading');
                    $(currentRow).addClass('loaded');
                });
        }

        vm.DeleteParent = function (viewName, index, event) {
            event.stopPropagation();

            $http({ method: 'Get', url: viewName + '/Delete/' + GetIDColumnValue(vm.rows[index]) })
                .then(function (result) {

                });
        }

        vm.DeleteChild = function (viewName, index, event) {
            event.stopPropagation();

            $http({ method: 'Get', url: viewName + '/DeleteChild/' + GetIDColumnValue(vm.rows[index]) })
                .then(function (result) {

                });
        }

        function toggleGridTree(currentRow) {
            $(currentRow).closest('tr').next('tr').slideToggle('slow', function () {
                $(currentRow).toggleClass("fa-plus-square-o");
                $(currentRow).toggleClass("fa-minus-square-o");
            });

        }

        vm.WorkFlowStatusUpdate = function (index, row, event) {
            event.stopPropagation();
            //$(event.currentTarget).addClass('active');
            //var xposition = event.pageX - 480;
            var yposition = event.pageY - 234;
            $('.popup.gridpopupfields', $(vm.WindowContainer)).slideDown("fast");
            $('.popup.gridpopupfields', $(vm.WindowContainer)).css({ 'top': yposition });
            $('.transparent-overlay', $(vm.WindowContainer)).show();
            $('#popupContainer', $(vm.WindowContainer)).html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

            $.ajax({
                url: "HR/EmploymentRequest/WorkFlow?requestID=" + GetIDColumnValue(row),
                type: 'GET',
                success: function (content) {
                    $('#popupContainer', $(vm.WindowContainer)).html($compile(content)($scope));
                    $('#popupContainer', $(vm.WindowContainer)).removeClass('loading');
                    $('#popupContainer', $(vm.WindowContainer)).addClass('loaded');
                }
            });
        }

        vm.ClosePopup = function (event) {
            $(event.currentTarget).hide();
            $('.popup.gridpopupfields', $(vm.WindowContainer)).fadeOut("fast");
            $('#popupContainer', $(vm.WindowContainer)).html('');
            //$('.statusview').removeClass('active');
        }


        //vm.RowClick = function (viewName, index, event, row, isDetailed, isReloadAlways) {
        //    var container = vm.WindowContainer;
        //    $scope.$parent.SelectedRow = vm.rows[index];
        //    $scope.$parent.SelectedRowIndex = index;
        //    $scope.$parent.SelectedIID = GetIDColumnValue(vm.rows[index]);

        //    if (container == null) {
        //        container = event.currentTarget.closest('.windowContainer');
        //    }

        //    $('.tablewrapper table tbody tr', container).removeClass('highlightrow');
        //    $(event.currentTarget).addClass('highlightrow');

        //    if ($(event.currentTarget).parents('.pagecontent').hasClass('summaryview') && $scope.$$childTail.SetDefaultView != undefined && isReloadAlways != true) {
        //        $scope.$$childTail.SetDefaultView($scope.SelectedIID);
        //        return;
        //    }

        //    var editUrl = viewName + "/DetailedView?IID=" + $scope.SelectedIID;

        //    if (isDetailed != undefined && isDetailed) {
        //        $(event.currentTarget).parents('.pagecontent').addClass('summaryview detail-panel minimize-fields');
        //        var subRow = $('.subrow td[colspan]', container);

        //        if (subRow.attr('oldcolspan') == null) {
        //            subRow.attr('oldcolspan', $('.subrow td[colspan]', container).attr('colspan'));
        //            subRow.attr('colspan', 4);
        //        }
        //    }
        //    else {
        //        $(event.currentTarget).parents('.pagecontent').addClass('summaryview');
        //    }

        //    $("#summarypanel", container).html(' <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

        //    $http({ method: 'Get', url: editUrl })
        //        .then(function (result) {
        //            $("#summarypanel", container).html($compile(result.data)($scope));
        //        }, function () {
        //            $("#summarypanel", container).html('');
        //            $(event.currentTarget).parents('.pagecontent').removeClass('summaryview');
        //        });
        //};

        function GetIDColumnValue(object) {
            var identifier = null;
            identifier = GetColumn(object, 'IID')

            if (identifier == undefined) {
                identifier = GetColumn(object, 'ID');
            }

            if (identifier == undefined) {
                identifier = GetFirstColumn(object);
            }

            return identifier;
        }

        function GetFirstColumn(object) {
            for (var key in object) {

                if (key != 'IsRowSelected' && key != '' && key != 'RowCategory')
                    return object[key];
            }
        }

        function GetColumn(object, column) {
            for (var key in object) {
                if (key.indexOf(column) != -1) {
                    return object[key];
                }
            }
        }

        function GetColumnName(object, column) {
            for (var key in object) {
                if (key.indexOf(column) != -1) {
                    return key;
                }
            }
        }

        vm.ChildrenActivate = function (controllerName, viewModel) {

            if (viewModel != undefined)
                vm.model = JSON.parse(viewModel);

            $('.listtable .load', vm.WindowContainer).show();

            vm.rows = {};
            vm.ControlerName = controllerName;
            var url = vm.ControlerName + '/SearchData?currentPage=1&runtimeFilter=' + vm.model.RuntimeFilter;

            if (vm.RuntimeFilter) {
                url = url + "&runtimeFilter=" + vm.RuntimeFilter + "";
            }

            vm.commonService.get(url, '', LoadChildrenSuccess)
        }

        function LoadChildrenSuccess(response) {
            var datas = JSON.parse(response.data);
            vm.rows = datas.Datas;
            $('.listtable .load', vm.WindowContainer).hide();
        }

        function ApplyFilterFromSummary(filterValue) {

            vm.rows = {};
            $('.listtable .load', vm.WindowContainer).show();
            var url = vm.ControlerName + '/SearchData?view=' + vm.ViewName + '&currentPage=1&orderby=' + vm.SortBy + '&runtimeFilter2=' + filterValue;

            if (vm.RuntimeFilter) {
                url = url + "&runtimeFilter=" + vm.RuntimeFilter + "";
            }

            vm.commonService.get(url, vm.param, LoadSuccess);
        }

        $scope.$on('Call_ApplyFilterFromSummary', function (event, data) {
            //access data here 
            console.log("in table rows controller:" + data);
            ApplyFilterFromSummary(data);
        });

        vm.DropDownOnChange = function (index, valuefield) {
            if (valuefield == 1) {
                vm.QuickFilter[index].IsDirty = vm.QuickFilter[index].FilterValue.length != 0 && vm.QuickFilter[index].FilterValue3.Key.length != 0;
            }
            else if (valuefield == 2) {
                vm.QuickFilter[index].IsDirty = (vm.QuickFilter[index].FilterValue3 != null &&
                    vm.QuickFilter[index].FilterValue3.Key != null && vm.QuickFilter[index].FilterValue3.Key.length != 0);
            }
        };

        vm.ShowGoogleMap = function (Row) {
            event.stopPropagation();
            window.open("http://www.google.com/maps/place/" + Row.Latitude + "," + Row.Longitude);
        };

        vm.ShowComments = function (type, referenceID, event) {
            event.stopPropagation();
            var posX = $(event.currentTarget).offset().left,
                posY = $(event.currentTarget).offset().top;
            $('.popup.gridpopupfields', $(vm.WindowContainer)).slideDown("fast");
            $('.popup.gridpopupfields', $(vm.WindowContainer)).css({ 'top': posY - $(vm.WindowContainer).offset().top + 10, 'left': posX });
            $('.transparent-overlay', $(vm.WindowContainer)).show();
            $('#popupContainer', $(vm.WindowContainer)).html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

            $.ajax({
                url: 'Mutual/Comment?type=' + type + '&referenceID=' + referenceID,
                type: 'GET',
                success: function (content) {
                    $('#popupContainer', $(vm.WindowContainer)).html($compile(content)($scope));
                    $('#popupContainer', $(vm.WindowContainer)).removeClass('loading');
                    $('#popupContainer', $(vm.WindowContainer)).addClass('loaded');
                }
            });
        };

        vm.ShowDataHistory = function (title, row, $event) {
            event.preventDefault();
            $("[data-original-title]").popover('dispose');

            $(event.currentTarget).popover({
                container: 'body',
                placement: 'left',
                html: true,
                content: function () {
                    return '';
                }

            });

            $(event.currentTarget).popover('show');
            $scope.HistoryInfo = row;
            var htmlContent = $('#dataHistoryTemplate').html();
            var content = $compile(htmlContent)($scope);
            $('#' + $(event.currentTarget).attr('aria-describedby')).find('.popover-body').html(content);
        }

        vm.HideDataHistory = function () {
            event.preventDefault();
            $("[data-original-title]").popover('dispose');
        }

        vm.ShowDashboard = function (type, referenceID, event, pageID) {
            event.stopPropagation();
            $scope.LoadScreen(event, "Frameworks/Dashboard/EntityDashboard?pageID=" + pageID + '&referenceID=' + referenceID, type + 'Dashboard', type + ' Dashboard');
        };

        vm.LoadWorkflow = function (row, event, ID, workflowID) {
            event.preventDefault();

            $("[data-original-title]").popover('dispose');

            $(".popover", vm.WindowContainer).hide();
            $(event.currentTarget).popover({
                container: 'body',
                placement: 'bottom',
                html: true,
                content: function () { return '<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>' }
            }).on('show.bs.popover', function () {
                //$(".overlaydiv").show();
            }).on('hide.bs.popover', function () {
                $(".overlaydiv").hide();
            });

            $(event.currentTarget).popover('show');

            $.ajax({
                url: 'Workflows/Workflow/GetWorkflowDetails?headID=' + (ID ? ID : row.HeadIID).toString() + '&workflowID=' + workflowID,
                type: 'GET',
                success: function (content) {
                    $('#' + $(event.currentTarget).attr('aria-describedby')).find('.popover-body').html($compile(content)($scope));
                }
            });

        };

        vm.Print = function (event, reportName, reportHeader, reportFullName, row) {


            if (row.ReportName == null || row.ReportName == undefined || row.reportName == "") {
                reportFullName = reportFullName;
            }
            else {
                reportFullName = row.ReportName;
            }

            var headID = null;
            if ($scope.ShowWindow(reportName, reportHeader, reportName) || row == null)
                return;

            var windowName = $scope.AddWindow(reportName, reportHeader, reportName);


            var iidColumn = GetColumnName(row, 'IID');
            var parameter = "";

            if (iidColumn) {
                parameter = iidColumn + "=" + row[iidColumn];
            }

            //var reportUrl = "Home/GeneratePDFReports?reportName=" + reportFullName + "&HeadID=" + $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID + '&returnFileBytes=' + true;
            var reportUrl = "Home/ViewReports?reportName=" + reportFullName + "&" + parameter; // $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
            $('#' + windowName).append('<script>function onLoadComplete() { }</script><center></center><iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
            var $iFrame = $('iframe[reportname=' + reportName + ']');
            $iFrame.on('load', function () {
                $("#Load", $('#' + windowName)).hide();
            });
        };

        vm.PrintTransaction = function (event, reportName, reportHeader, reportFullName, headIID) {
            $http({ method: 'GET', url: "Home/ViewReports?returnFileBytes=true&HeadID=" + headIID + "&reportName=" + reportFullName })
                .then(function (filename) {
                    var w = window.open();
                    w.document.write('<iframe onload="isLoaded()" id="pdf" name="pdf" src="' + filename.data + '"></iframe><script>function isLoaded(){window.frames[\"pdf\"].print();}</script>');
                });

        };

        $scope.CloseCommentOverlay = function (event) {
            $(event.currentTarget).hide();
            $('.popup.gridpopupfields', $(vm.WindowContainer)).slideUp("fast");
        }

        vm.ShowPopup = function (event) {
            var elment = $("body").find(".popup.workFlow");
            elment.addClass('show');
            var popupHeight = $(elment).outerHeight(),
                popupWidth = $(elment).outerWidth(),
                eventTop = $(event.currentTarget).offset().top,
                eventLeft = $(event.currentTarget).offset().left,
                eventWidth = $(event.currentTarget).outerWidth(),
                popupTopPos = eventTop - popupHeight - 10,
                popupLeftPos = eventLeft - popupWidth / 2 + eventWidth / 2;
            $(elment).css({ 'top': popupTopPos, 'left': popupLeftPos })
        }

        vm.LoadCommunication = function (row, event, ID, screenID) {
            $root.LoadCommunication(row, event, ID, screenID, vm.WindowContainer);
        };

        vm.PostApplication = function (row, event, ID, screenID) {
            event.preventDefault();

            $("[data-original-title]").popover('dispose');

            $(".popover", vm.WindowContainer).hide();
            $(event.currentTarget).popover({
                container: 'body',
                placement: 'bottom',
                html: true,
                content: function () { return '<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>' }
            }).on('show.bs.popover', function () {
                $(".overlaydiv").show();
            }).on('hide.bs.popover', function () {
                $(".overlaydiv").hide();
            });

            $(event.currentTarget).popover('show');
            $('#' + $(event.currentTarget).attr('aria-describedby'))
                .find('.popover-body').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

            $.ajax({
                type: "POST",
                url: utility.myHost + "Schools/School/MoveToApplication?leadID=" + (ID ? ID : row.LeadIID).toString() + "&screenID=" + screenID,
                contentType: "application/json;charset=utf-8",
                success: function (content) {
                    $(event.currentTarget).popover('hide');
                    $().showGlobalMessage($root, $timeout, true, content);
                    return;
                }
            });

        };

        vm.ResentCredentials = function (row, event, ID, screenID) {

            event.preventDefault();

            $("[data-original-title]").popover('dispose');

            $(".popover", vm.WindowContainer).hide();
            $(event.currentTarget).popover({
                container: 'body',
                placement: 'bottom',
                html: true,
                content: function () { return '<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>' }
            }).on('show.bs.popover', function () {
                $(".overlaydiv").show();
            }).on('hide.bs.popover', function () {
                $(".overlaydiv").hide();
            });

            $(event.currentTarget).popover('show');
            $('#' + $(event.currentTarget).attr('aria-describedby'))
                .find('.popover-body').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

            $.ajax({
                type: "POST",
                url: utility.myHost + "Schools/School/ResentLoginCredentials?leadID=" + (ID ? ID : row.LeadIID).toString() + "&screenID=" + screenID,
                contentType: "application/json;charset=utf-8",
                success: function (content) {
                    $(event.currentTarget).popover('hide');
                    if (content != "false") {
                        $().showGlobalMessage($root, $timeout, false, content);
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, true, "Can't Send Credentials,LeadCode not found in Student Application!");
                    }
                }

            });

        };

        vm.SendReportMail = function (event, reportName, reportHeader, reportFullName, row) {
            hideMailConfirmBoxOverlay();
            $scope.MailEvent = event;
            if (row.ReportName) {
                $scope.MailReportName = row.ReportName;
            }
            else {
                $scope.MailReportName = reportName;
            }

            $scope.MailReportFullName = reportFullName;
            $scope.MailRowData = row;

            $scope.MailEvent.preventDefault();
            $("[data-original-title]").popover('dispose');

            $($scope.MailEvent.currentTarget).popover({
                container: 'body',
                placement: 'left',
                html: true,
                showCloseButton: true,
                arrowOffsetValue: 10,
                content: function () {
                    return '';
                }
            });

            $($scope.MailEvent.currentTarget).popover('show');
            var htmlContent = $('#MailConfirmationWindow').html();
            var content = $compile(htmlContent)($scope);
            $('#' + $($scope.MailEvent.currentTarget).attr('aria-describedby')).find('.popover-body').html(content);
        };

        $scope.ConfirmMailSend = function () {
            showMailConfirmBoxOverlay();
            var feeCollectionData = {};
            var headIID = null;
            var emailID = null;
            if ($scope.MailReportName == 'FeeReceipt' || $scope.MailReportName == 'FeeReceiptPACE') {
                headIID = $scope.MailRowData.FeeCollectionIID;
                emailID = $scope.MailRowData.EmailID;
            }
            if (!emailID || emailID == '-') {
                $().showGlobalMessage($root, $timeout, true, "Please update with valid Email ID");
                $scope.CloseMailConfirmBox();
                return false;
            } else {

                feeCollectionData = {
                    "FeeCollectionIID": headIID,
                    "StudentID": $scope.MailRowData.StudentID,
                    "AdmissionNo": $scope.MailRowData.AdmissionNumber,
                    "FeeReceiptNo": $scope.MailRowData.feeReceiptNo,
                    "EmailID": emailID,
                    "SchoolID": $scope.MailRowData.SchoolID,
                    "ReportName": $scope.MailReportName,
                }

                $http({
                    method: 'POST',
                    url: "Home/GenerateAndEmailFeeReceipt",
                    data: feeCollectionData
                }).then(function (result) {
                    if (!result.data.IsError) {
                        $().showGlobalMessage($root, $timeout, false, result.data.Response);
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, true, "Mail Sending Failed");
                    }
                    hideMailConfirmBoxOverlay();
                    $scope.CloseMailConfirmBox();
                });
            }
        }

        $scope.CloseMailConfirmBox = function () {
            $scope.MailEvent.preventDefault();
            $("[data-original-title]").popover('dispose');
        }

        function showMailConfirmBoxOverlay() {
            $timeout(function () {
                $scope.$apply(function () {
                    $("#MailConfirmationOverlay").show();
                });
            });
        };

        function hideMailConfirmBoxOverlay() {
            $scope.IsEnableLoader = false;
            $timeout(function () {
                $scope.$apply(function () {
                    $("#MailConfirmationOverlay").hide();
                });
            });
        };

        $('html').on('click', function (e) {
            if (document.getElementsByClassName('popover').length > 0) {
                if (typeof $(e.target).data('original-title') == 'undefined' && !$(e.target).parents().is('.popover.show')) {
                    $("[data-original-title]").popover('dispose');
                    $(".popover").addClass('hide').removeClass('show');
                    $(".popover").removeClass('popover');
                }
            }
        });

        vm.LoadPopover = function (row, event, url, width, title) {
            event.preventDefault();
            event.stopPropagation();

            $("[data-original-title]").popover('dispose');
            $(".popover", vm.WindowContainer).hide();
            var targetElement = $(event.currentTarget);
            var title = (title ? title : 'General');
            targetElement.popover({
                title: title,
                container: 'body',
                placement: 'left',
                html: true,
                content: function () { return '<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>' }
            });

            targetElement.popover('show');

            $.ajax({
                url: url,
                type: 'GET',
                success: function (content) {
                    $('#' + $(event.currentTarget).attr('aria-describedby')).find('.popover-body').html($compile(content)($scope));

                    if (width) {
                        $('#' + $(event.currentTarget).attr('aria-describedby')).css("width", width + "px");;
                        $('#' + $(event.currentTarget).attr('aria-describedby')).css("min-width", width + "px");;
                    }

                    $(event.currentTarget).attr('data-original-title', title);
                    $('#' + $(event.currentTarget).attr('aria-describedby')).find('.popover-header').html(title);
                    window.dispatchEvent(new Event('resize'));
                }
            });
        };

        vm.LoadDialogueWindow = function (row, event, url, width) {
            if (!width) {
                width = 530;
            }

            event.stopPropagation()
            const el = document.querySelector('.pagecontent');
            var yposition = $($(event.target).closest('td')).offset().top - 150;
            var left = $($(event.target).closest('td')).offset().left - width - 60;

            var $popupContainer = $('.popup.gridpopupfields', $(vm.WindowContainer));
            $popupContainer.first().width(width);
            $popupContainer.first().slideDown('fast');
            $popupContainer.first().css({ top: yposition + el.scrollTop, left: left })
            $('.transparent-overlay', $(vm.WindowContainer)).show()
            $('#popupContainer', $(vm.WindowContainer)).html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>')

            $.ajax({
                url: url,
                type: 'GET',
                success: function (content) {
                    $('#popupContainer', $(vm.WindowContainer)).html($compile(content)($scope))
                    $('#popupContainer', $(vm.WindowContainer)).removeClass('loading')
                    $('#popupContainer', $(vm.WindowContainer)).addClass('loaded')
                }
            });
        }

    }
})();