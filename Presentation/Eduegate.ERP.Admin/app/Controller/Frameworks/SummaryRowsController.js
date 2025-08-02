(function () {
    'use strict';

    angular
        .module('Eduegate.ERP.Admin')
        .controller('SummaryRowsController', controller);

    controller.$inject = ['commonService', "$interval", '$rootScope'];

    function controller(commonService, $interval, $rootScope) {
        /* jshint validthis:true */
        var vm = this;
        vm.commonService = commonService;
        vm.SummaryRows = {};
        vm.param = {}
        vm.ShowTime = null;

        $interval(function () {
            vm.ShowTime = Date.now();
        }, 1000);


        vm.activate = function (controllerName, viewName) {
            vm.commonService.get(controllerName + '/SearchSummaryData?view=' + viewName, vm.param, vm.loadSuccess)
        }

        vm.loadSuccess = function (response) {
            var datas = JSON.parse(response.data);
            vm.SummaryRows = datas.Datas;
        }

        vm.FilterAction = function (filterValue) {
            $rootScope.$broadcast('Call_ApplyFilterFromSummary', filterValue);
        }
    }
})();