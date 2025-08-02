(function () {
    'use strict';

    angular
        .module('Eduegate.ERP.Admin')
        .controller('UserViewFilterController', userViewFilter);

    userViewFilter.$inject = ['commonService'];

    function userViewFilter(commonService) {
        /* jshint validthis:true */
        var vm = this;
        vm.commonService = commonService;
        vm.selection = [];

        

        vm.activate = function () {
            //things to don on controller init
        }

        vm.createView = function() {
            //create view
            // on success show alert: view created
            vm.reset();
            //populate this view under dropdown box and on grid
        }

        vm.cancel = function () {
            vm.reset();
        }

        vm.reset = function reset() {
            //uncheck fields
            //hide view
        }

        //add columns to an array
        vm.toggleSelection = function (column) {
            var idx = vm.selection.indexOf(column);

            // is already selected, remove it
            if (idx > -1) {
                vm.selection.splice(idx, 1);
            }

            // is newly selected, push it
            else {
                vm.selection.push(column);
            }
            console.log(vm.selection);
        };

        //vm.activate();
    }
})();
