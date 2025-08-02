app.controller("DocumentTypeController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$controller",
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller) {
        console.log("DocumentTypeController");

        angular.extend(this, $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, productService: productService, purchaseorderService: purchaseorderService, accountService: accountService }));

        $scope.LoadDocumentLookup = function (lookupName, system) {

            if ($scope.LookUps[lookupName] != undefined && $scope.LookUps[lookupName].length > 0)
                return;

            $scope.LookUps[lookupName] = [{ 'Key': '', Value: 'Loading..' }];

            $.ajax({
                type: "GET",
                url: 'Mutual/GetDocumentTypesBySystem?system=' + system,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result.Data == undefined) {
                            $scope.LookUps[lookupName] = result;
                        }
                        else {
                            $scope.LookUps[lookupName] = result.Data;
                        }
                    });
                }
            });
        }
    }]);