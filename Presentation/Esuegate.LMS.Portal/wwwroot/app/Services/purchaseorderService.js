/*use a factory instead for consistency.*/
/* Purchase Order Service */
(function () {
    'use strict';

    angular
        .module('Eduegate.Signup.Portal')
        .factory('purchaseorderService', purchaseorderService);

    purchaseorderService.$inject = ['$http'];

    function purchaseorderService($http) {

        /* declare variable to use inside the each method */
        var url;

        /* methods */
        var service = {
            getNextTransactionNo: getNextTransactionNo
        };
        return service;

        /* 
        @description
            get next TransactionNo from DocumentTypes based on documentTypeID
        @param
            {number} documentTypeID
        @return
            {string} TransactionNo
        */
        function getNextTransactionNo(documentTypeID) {
            url = 'Mutual/GetNextTransactionNumber?documentTypeID=' + documentTypeID;
            return $http.get(url)
                .then(getNextTransactionNoComplete)
                .catch(getNextTransactionNoFailed);

            function getNextTransactionNoComplete(response) {
                return response.data;
            }

            function getNextTransactionNoFailed(error) {
                console.log('XHR failed for purchaseorderService.getNextTransactionNo.' + error.data);
            }
        }

    }

})();