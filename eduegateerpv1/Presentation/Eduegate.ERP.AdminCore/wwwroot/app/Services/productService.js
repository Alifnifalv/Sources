/*
Services registered on the module with module.service are classes. 
Use module.service instead of module.provider or module.factory unless you need to do initialization beyond just creating a new instance of the class.
*/

// service
angular
    .module('Eduegate.ERP.Admin')
    .service('productService', product);

product.$inject = ['$http'];

function product($http) {

    this.getProductAndSKUByID = function (productSKUMapID) {
        var url = 'Product/GetProductAndSKUByID?productSKUMapID=' + productSKUMapID;
        return $http.get(url)
        .then(function (response) { return response.data });
    };

}