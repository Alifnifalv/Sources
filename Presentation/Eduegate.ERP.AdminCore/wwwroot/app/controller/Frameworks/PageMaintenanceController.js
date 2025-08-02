app.controller("PageMaintenanceController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$controller",
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller) {
        console.log("Page Maintenance Controller");

        $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, productService: productService, purchaseorderService: purchaseorderService, accountService: accountService });

        $scope.FilterGridData = function (event, column) {
            console.log("filter");

            $("#Overlay").fadeIn(100);
            var filterValue = $scope.CRUDModel.ViewModel.ReferenceID;

            if (filterValue != undefined && filterValue != '') {
                filterValue = 'referenceid=' + filterValue;
            }
            else
                filterValue = '';

            var dataurl = 'Page/GetBolierPlateMaps?pageID=' + this.CRUDModel.IID + '&parameter=' + filterValue;

            $http({ method: 'Get', url: dataurl })
              .then(function (result) {
                  $scope.CRUDModel.ViewModel.BoilerPlates = result.BoilerPlates;
                  $scope.CRUDModel.ViewModel.ReferenceID = result.ReferenceID;

                  if (result.ReferenceID != null && result.ReferenceID != '') {
                      $scope.CRUDModel.ViewModel.HasReferenceID = true;
                  }
                  else {
                      $scope.CRUDModel.ViewModel.HasReferenceID = false;
                  }

                  $(event.currentTarget).parent('span').css("display", "none");
                  $("#Overlay").hide();
              });
        }

        $scope.LoadBoilerPlateGridData = function (result) {
            if (result.hasOwnProperty("BoilerPlates") == true)
                $scope.GridList = result.BoilerPlates;
        }

        $scope.OnChangeBoilerPlate = function (control, rowIndex) {
            //var abc = currentRow;
            var selectedBoilerPlate = $(control)[0].selected.Key;

            $.ajax({
                url: "CMS/Page/GetBoilerPlateParameters?boilerPlateID=" + selectedBoilerPlate,
                type: 'GET',
                success: function (result) {
                    var abc = result;
                    $scope.$apply(function () {
                        var referenceID = $scope.CRUDModel.ViewModel.BoilerPlates[rowIndex].ReferenceID;
                        $scope.CRUDModel.ViewModel.BoilerPlates[rowIndex] = result.BoilerPlateGridViewModel;
                        $scope.CRUDModel.ViewModel.BoilerPlates[rowIndex].ReferenceID = referenceID;
                        //$scope.CRUDModel.ViewModel.BoilerPlates[rowIndex].BoilerPlateID = result.BoilerPlateGridViewModel.BoilerPlateID;
                    });
                    $(control)[0].selected.Key = selectedBoilerPlate;
                }
            })


            // adding a new row in the grid.
            if (rowIndex == $scope.CRUDModel.ViewModel.BoilerPlates.length - 1) {
                var vm = angular.copy($scope.CRUDModel.ViewModel.BoilerPlates);
                $scope.CRUDModel.ViewModel.BoilerPlates.push(vm);
            }
        }
    }]);