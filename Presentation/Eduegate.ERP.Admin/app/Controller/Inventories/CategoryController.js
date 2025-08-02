app.controller("CategoryController", ["$scope", "$compile", "$http", "$timeout", "$controller",
    function ($scope, $compile, $http, $timeout, $controller) {
        console.log("RepairOrderController");

        $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout});
        $scope.LoadCategoryLookup = function (lookupName, model) {
            $scope.model = model;
            if ($scope.model.LoadCategoryLookUp == false)
                return;
            
            $scope.LookUps[lookupName] = [{ 'Key': '', Value: 'Loading..' }];
            var url = "Category/GetCategories";
            if (model.IsReporting == true) {
                url = "Category/GetReportingCategories";
            }
            $.ajax({
                type: "GET",
                url: url,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result.Data == undefined) {
                            $scope.LookUps[lookupName] = result;
                        }
                        else {
                            $scope.LookUps[lookupName] = result.Data;
                        }
                        $scope.model.LoadCategoryLookUp = false;
                    });
                }
            });
        }

        $scope.IsLoadCategoryLookUp = function () {
            $scope.model.LoadCategoryLookUp = true;
        }
       
    }]);