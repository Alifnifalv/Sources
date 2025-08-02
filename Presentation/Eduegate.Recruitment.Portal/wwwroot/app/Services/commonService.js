(function () {
    'use strict';

    angular
        .module('Eduegate.Recruitment.Portal')
        .factory('commonService', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            get: get,
            post: post
        };
        
        function serviceCall(method, url, paramObject, successCallback) {
            $http({ method: method, url: url, data: paramObject }).
            then(function (response) {
                if (successCallback != undefined) {
                    successCallback(response);
                }
            }, function (response) {
                console.log('some error occured in service call.' + response);
            });
        };
         
        function get(url, data, successCallback) {
            serviceCall('GET', url, '', successCallback);
        }

        function post(url, data, successCallback) {
            serviceCall('POST', url, data, successCallback);
        }

        return service;
    }
})();