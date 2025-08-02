(function () {
    'use strict';

    angular
        .module('Eduegate.ERP.Admin')
        .controller('LiveTableRowsController', controller);

    controller.$inject = ['commonService', '$scope', '$compile', '$http', '$timeout', "$interval"];

    function controller(commonService, $scope, $compile, $http, $timeout, $interval) {
        /* jshint validthis:true */
        var vm = this;
        vm.commonService = commonService;
        vm.rows = {};
        vm.param = {}
        vm.SelectAll = false;
        vm.ControlerName = '';
        vm.SortBy = null;
        vm.DateTimeFormat = utility.getDateFormat(_dateTimeFormat);
        vm.DateFormat = _dateFormat;
        vm.QuickFilter = [];
        vm.WindowContainer = null;
        vm.DefaultFilter = null;
        vm.LastProcessedDateTime = null;
        vm.ActionType = null;
        vm.pageSize = 1000;
        vm.ViewName = '';
        vm.IsLive = false;
        vm.scope = $scope;
        vm.LookUps = [];
        //filter panel
        vm.IsShowFilterView = false;

        $interval(function () {
            $scope.ShowTime = Date.now();
        }, 1000);

        $scope.ShowRemainingHours = function (date) {
            return utility.getRemainingHoursText(date, $scope.ShowTime);
        };

        $scope.$watch(function (scope) { return vm.SortBy },
              function (newValue, oldValue) {
                  if (newValue != oldValue)
                      vm.ReLoad();
              }
             );

        vm.activate = function (window, controllerName, viewName, actionType, quickFilterViewModel, defaultFilter, isLive) {
            if (quickFilterViewModel != undefined)
                vm.QuickFilter = JSON.parse(quickFilterViewModel);

            vm.rows = {};
            vm.ControlerName = controllerName;
            vm.DefaultFilter = defaultFilter;
            vm.ActionType = actionType;
            vm.LastProcessedDateTime = GetDBSynchTime();
            vm.commonService.get(vm.ControlerName + '/SearchData?currentPage=1&type=' + vm.ActionType + "&pageSize=" + vm.pageSize + "&startDate=" + vm.LastProcessedDateTime, vm.param, LoadSuccess);
            vm.WindowContainer = window;
            vm.ViewName = viewName;

            if (isLive != undefined) {
                vm.IsLive = isLive;
            }
        };

        //add for loading lookup drodownlist.
        vm.LoadLookups = function (lookupName) {

            if (vm.LookUps[lookupName] != null)
                return;

            vm.LookUps[lookupName] = [];
            vm.LookUps[lookupName].push({ Key: "", Value: "Loading.." });

            $.ajax({
                type: "GET",
                url: "Mutual/GetLookUpData?lookType=" + lookupName,
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


        ///end
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

        function GetDBSynchTime() {
            var newDateTime = new Date();
            return newDateTime.getDate() + "/" + newDateTime.getMonthName() + "/" + newDateTime.getFullYear() + " " + newDateTime.toLocaleTimeString();
        }

        vm.QuickSearch = function (event) {
            vm.rows = {};
            $('.listtable .load', vm.WindowContainer).show();

            $.ajax({
                type: "POST",
                url: 'Mutual/SaveFilter',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(vm.QuickFilter),
                success: success,
            });
        };

        vm.ResetQuickSearch = function (event) {
            vm.rows = {};
            $('.listtable .load', vm.WindowContainer).show();

            $.ajax({
                type: "POST",
                url: 'Mutual/ResetFilter',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(vm.QuickFilter),
                success: success,
            });
        }

        function success() {
            $('.preload-overlay').hide();
            $('.preloader').hide();
            vm.activate(vm.WindowContainer, vm.ControlerName, vm.ViewName, vm.ActionType, JSON.stringify(vm.QuickFilter));
        }

        function LoadSuccess(response) {
            if (response.data.IsError) {

                if (response.data.IsError == true) {
                    $().showGlobalMessage($root, $timeout, true, response.data.Message);
                    return;
                }
            }

            var datas = JSON.parse(response.data.NewData);
            vm.rows = datas.Datas;
            vm.SelectAll = false;
            resetSelectedIds();
            $('.tablewrapper .content-load').hide();

            $timeout(function () {
                SynchData();
            }, 5000);
        }

        function Reload() {
            vm.LastProcessedDateTime = GetDBSynchTime();
            vm.commonService.get(vm.ControlerName + '/SearchData?currentPage=1&type=' + vm.ActionType + "&pageSize=" + vm.pageSize + "&startDate=" +
                vm.LastProcessedDateTime, vm.param, LoadSuccess)
        }

        function SynchData() {
            if (vm.IsLive) {
                var newDateTime = GetDBSynchTime();
                $.ajax({
                    type: "GET",
                    url: vm.ControlerName + '/SearchData?currentPage=1&type=' + vm.ActionType + "&pageSize=" + vm.pageSize + "&startDate=" +
                    vm.LastProcessedDateTime + "&endDate=" + newDateTime,
                    contentType: "application/json;charset=utf-8",
                    success: LoadSynchSuccess,
                });

                vm.LastProcessedDateTime = newDateTime;
            }
        }

        function LoadSynchSuccess(response) {

            if (response != null && response != undefined) {

                if (response.NewData != null && response.NewData != undefined) {

                    var datas = JSON.parse(response.NewData);

                    if (datas.Datas.length > 0) {
                        datas.Datas.forEach(function (element, index) {
                            vm.rows.unshift(element);
                        });
                    }
                }
                if (response.ChangedData != null && response.ChangedData != undefined) {

                    var removeData = JSON.parse(response.ChangedData);

                    if (removeData.Datas.length > 0) {
                        removeData.Datas.forEach(function (element, index) {
                            vm.rows.splice(element, 1);
                        });
                    }
                }
            }

            $timeout(function () {
                SynchData();
            }, 5000);
        }

        vm.SelectAllHeader = function (event, rows) {
            $scope.SelectedIds.length = 0;
            vm.scope.$parent.SelectedIds.length = 0;
            if (event.target.checked === true) {
                $.each(rows, function (index, data) {
                    data.IsRowSelected = vm.SelectAll;
                    var iid = GetIDColumnValue(data);
                    $scope.SelectedIds.push(iid);
                    vm.scope.$parent.SelectedIds.push(iid);
                });
            } else {
                $scope.SelectedIds.length = 0;
                vm.scope.$parent.SelectedIds.length = 0;
                rows.forEach(row => row.IsRowSelected = false);
            }
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
            if (row.IsRowSelected == true) {
                // push
                $scope.SelectedIds.push(iid);
                vm.scope.$parent.SelectedIds.push(iid);
            }
            else
                if (row.IsRowSelected == false) {
                    //pop
                    var index = $scope.SelectedIds.indexOf(iid);
                    $scope.SelectedIds.splice(index, 1);
                    index = vm.scope.$parent.SelectedIds.indexOf(iid);
                    vm.scope.$parent.SelectedIds.splice(index, 1);
                }

            event.stopPropagation();
        };


        vm.ReLoad = function (event) {
            resetSelectedIds();
            vm.rows = {};
            vm.SelectAll = false;
            $('.listtable .load', vm.WindowContainer).show();
            vm.commonService.get(vm.ControlerName + '/SearchData?currentPage=1&type=' + vm.ActionType + '&orderby=' + vm.SortBy + "&pageSize=" + vm.pageSize + "&startDate=" + vm.LastProcessedDateTime, vm.param, LoadSuccess)
        };

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

        $scope.MultiSelectClick = function (row) {
            row.IsRowSelected = !row.IsRowSelected;

            var iid = GetIDColumnValue(row);
            var index = $scope.SelectedIds.indexOf(iid);

            if (index > -1) {
                $scope.SelectedIds.splice(index, 1);
            }

            $scope.SelectedIds.push(iid);
        };

        vm.RowClick = function (viewName, index, event, row, isDetailed) {
            event.stopPropagation();
            $('.productlisttable table tbody tr', vm.WindowContainer).removeClass('highlightrow');
            $(event.currentTarget).addClass('highlightrow');
            var editUrl = viewName + "/" + vm.ActionType + "?IID=" + GetIDColumnValue(vm.rows[index]);

            if (isDetailed != undefined && isDetailed) {
                $(event.currentTarget).parents('.pagecontent').addClass('summaryview detail-panel minimize-fields summaryviewbigger');
                var subRow = $('.subrow td[colspan]', vm.WindowContainer);

                if (subRow.attr('oldcolspan') == null) {
                    subRow.attr('oldcolspan', $('.subrow td[colspan]', vm.WindowContainer).attr('colspan'));
                    subRow.attr('colspan', 4);
                }
            }
            else {
                $(event.currentTarget).parents('.pagecontent').addClass('summaryview');
            }

            $("#summarypanel", vm.WindowContainer).html(' <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

            $http({ method: 'Get', url: editUrl })
                .then(function (result) {
                    $("#summarypanel", vm.WindowContainer).html($compile(result.data)($scope));
                });
        };

        vm.EditView = function (viewName, index, event) {
            event.stopPropagation();

            var windowName = viewName.substring(viewName.indexOf('/') + 1);
            if ($scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName))
                return;

            $("#Overlay").fadeIn(100);

            var editUrl = viewName + "/Edit/" + GetIDColumnValue(vm.rows[index]);
            $http({ method: 'Get', url: editUrl })
                .then(function (result) {
                    $("#LayoutContentSection").append($compile(result.data)($scope));
                    $scope.AddWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
                    $("#Overlay").fadeOut(100);
                });
        };

        vm.GetChildView = function (viewName, index, event) {

            event.stopPropagation();
            var currentRow = event.currentTarget;

            if ($(currentRow).hasClass('loading'))
                return;

            if ($(currentRow).hasClass('loaded')) {
                toggleGridTree(currentRow);
                return;
            }

            var colspan = $($(currentRow).closest('tr')).find('td').length;

            //set the status loading
            $(currentRow).addClass('loading');
            $(currentRow).closest('tr').after('<tr class="gridloader"><td colspan=' + colspan + '> <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint"></span></center></tr></td>');
            // get children
            var url = viewName + "/ChildList/" + GetIDColumnValue(vm.rows[index]);
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    //create subrow
                    var colspan = $($(currentRow).closest('tr')).find('td').length;
                    $(currentRow).closest('tr').next('tr').remove();
                    result = result.replace('%%COLSPAN%%', colspan);
                    $(currentRow).closest('tr').after($compile(result.data)($scope));
                    // expand
                    toggleGridTree(currentRow);

                    $(currentRow).removeClass('loading');
                    $(currentRow).addClass('loaded');
                });
        };

        function toggleGridTree(currentRow) {
            $(currentRow).toggleClass("fa-plus-square-o");
            $(currentRow).toggleClass("fa-minus-square-o");
            //$(currentRow).closest('tr').next().toggleClass("displaytablerow");
            //$(currentRow).closest('tr').next().toggleClass("mainrowbg");
            $(currentRow).closest('tr').next('tr').slideToggle();
        }

        function GetIDColumnValue(object) {
            var identifier = null;
            identifier = GetColumn(object, 'IID');

            if (identifier == undefined) {
                identifier = GetColumn(object, 'ID');
            }

            return identifier;
        }

        function GetColumn(object, column) {
            for (var key in object) {
                if (key.indexOf(column) != -1) {
                    return object[key];
                }
            }
        }

        vm.ChildrenActivate = function (controllerName, viewModel) {

            if (viewModel != undefined)
                vm.model = JSON.parse(viewModel);

            $('.listtable .load', vm.WindowContainer).show();

            vm.rows = {};
            vm.ControlerName = controllerName;

            vm.commonService.get(vm.ControlerName + '/SearchData?currentPage=1&runtimeFilter=' + vm.model.RuntimeFilter, '', LoadChildrenSuccess)
        };

        function LoadChildrenSuccess(response) {
            var datas = JSON.parse(response.data);
            vm.ActionType = "DetailedView";
            vm.WindowContainer = $scope.$parent.RowModel.WindowContainer
            vm.rows = datas.Datas;
            $('.listtable .load', vm.WindowContainer).hide();
        }

        vm.DeleteChild = function (model, viewName, index, event) {

            event.stopPropagation();

            var parentJobEntryHeadID = GetIDColumnValue(vm.rows[index]);
            var deleteUrl = viewName + "/Delete?ID=" + parentJobEntryHeadID + "&jobEntryHeadID=" + eval(model.split("=")[1]);

            $.ajax({
                type: "GET",
                url: deleteUrl,
                success: function (result) {
                    console.log(result);
                }
            });
        }

        function resetSelectedIds() {
            vm.scope.$parent.SelectedIds = [];
            $scope.SelectedIds = [];
        }

        //added for lookup table effect
        function ApplyFilterFromSummary(filterValue) {

            vm.rows = {};
            $('.listtable .load', vm.WindowContainer).show();
            var url = vm.ControlerName + '/SearchData?currentPage=1&orderby=' + vm.SortBy + '&runtimeFilter2=' + filterValue;
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

        vm.ShowComments = function (type, referenceID, event) {
            event.stopPropagation();

            var yposition = event.pageY - 234;
            $('.popup.gridpopupfields', $(vm.WindowContainer)).slideDown("fast");
            $('.popup.gridpopupfields', $(vm.WindowContainer)).css({ 'top': yposition });
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

        vm.ClosePopup = function (event) {
            $(event.currentTarget).hide();
            $('.popup.gridpopupfields', $(vm.WindowContainer)).fadeOut("fast");
            $('#popupContainer', $(vm.WindowContainer)).html('');
            //$('.statusview').removeClass('active');
        };

        $scope.CloseCommentOverlay = function (event) {
            $(event.currentTarget).hide();
            $('.popup.gridpopupfields', $(vm.WindowContainer)).slideUp("fast");
        };
    }
})();