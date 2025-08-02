(function () {
    'use strict';

    angular
    .module('Eduegate.ERP.School.Portal')
    .factory('accountService', accountService);

    accountService.$inject = ['$http'];

    function accountService($http) {

        /* declare variable to use inside the each method */
        var url;

        /* methods */
        var service = {
            getBillingShippingContact: getBillingShippingContact,
            getContactByCustomerId: getContactByCustomerId,
        }
        return service;

        /* 
        @description
            get contact list based on customerID and addressType
        @param
            {number} customerID
            {enum object} addressType in form of {All = 0,Billing = 1,Shipping = 2}
        @return
            {object} list of contact
        */
        function getBillingShippingContact(customerID, addressType) {
            url = 'SalesOrder/GetDeliveryAddress?customerID=' + customerID + '&addressType=' + addressType;
            return $http.get(url)
            .then(getBillingShippingContactComplete)
            .catch(getBillingShippingContactFailed);

            function getBillingShippingContactComplete(response) {
                return response.data;
            }

            function getBillingShippingContactFailed(error) {
                console.log('XHR failed for accountService.getBillingShippingContact.' + error.data);
            }
        }


        function getContactByCustomerId(customerId) {
            url = 'Customer/GetContactByCustomerId?customerId=' + customerId;
            return $http.get(url)
                .then(getContactByCustomerIdComplete)
                .catch(getContactByCustomerIdFailed);

            function getContactByCustomerIdComplete(response) {
                return response.data;
            }

            function getContactByCustomerIdFailed(error) {
                console.log('XHR failed for accountService.getBillingShippingContact.' + error.data);
            }
        }
    }

})();