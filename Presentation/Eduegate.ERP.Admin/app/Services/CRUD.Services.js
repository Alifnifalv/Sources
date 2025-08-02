(function () {
    'use strict';

    angular
        .module('Eduegate.ERP.Admin')
        .factory('crudService', crudService);

    crudService.$inject = ['$http'];

    function crudService($http) {

        var service = {
            getDetailedView: getDetailedView,
            getList: getList,
        };

        return service;

        function getDetailedView(url) {
            
        }


        function getList(customerId) {
           
        }
    }

})();