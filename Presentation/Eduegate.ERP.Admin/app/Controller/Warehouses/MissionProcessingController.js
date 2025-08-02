app.controller("MissionProcessingController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$controller",
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller) {
        console.log("Mission Processing Controller");
        var shipmentDetailRow;
        var popupContent;

        $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, productService: productService, purchaseorderService: purchaseorderService, accountService: accountService });

        $scope.OnServiceProviderChange = function (event, model) {

            if (model.ServiceProvider.Key != '1') {
                model.Driver = null;
                model.Vehicle = null
                model.IsServiceProver = true;               
            }
            else {
                model.IsServiceProver = false;               
            }
        }
        
        $scope.ShowShipment = function (model, event) {
            $scope.submitted = false;
            shipmentDetailRow = model;

            popupContent = $(event.currentTarget).closest('td').find('.fieldpopup');
            $(popupContent).html('<div class="main_loader"><span class="fa fa-circle-o-notch fa-pulse"></span></div>');

            $scope.LookUps["Cities"] = [];
            $http({ method: 'Get', url: 'ServiceProviderAPI/GetSMSACities' })
             .then(function (result) {
                 $scope.LookUps["Cities"] = result;
             });

            $http({ method: 'Get', url: 'Distributions/ReadyForShipping/SMSAShipment' })
             .then(function (result) {
                 $(popupContent).html($compile(result)($scope));
                 $http({ method: 'Get', url: 'Distributions/ReadyForShipping/GetShipmentInfo?jobID=' + model.JobID })
                    .then(function (result) {
                        $scope.CRUDModel.Model.Shipment = result;
                    });
             });
        }

        $scope.AddShipment = function (model, detail) {
            $scope.submitted = true;
            if (!$scope.smsaForm.$valid) {
                return;
            }

            $http({ method: 'POST', url: 'ServiceProviderAPI/AddShipment', data: model })
             .then(function (result) {
                 if (result.includes("Fail")) {
                     $().showMessage($scope, $timeout, true, result);
                 }
                 else {
                     shipmentDetailRow.AWBNo = result;
                     $scope.PoupClose(null);
                }
             })
            .error(function (error) {
                $().showMessage($scope, $timeout, true, error);
            });
        }

        $scope.PrintAWBPDF = function (model) {
            model.AWBNo = utility.replaceString(model.AWBNo, '"');
            $http({ method: 'Get', url: 'ServiceProviderAPI/GenerateAWBPDF?referenceID=' + model.AWBNo })
             .then(function (result) {
                 if (result.IsError) {
                     $().showMessage($scope, $timeout, true, result.UserMessage);
                 } else { 
                     utility.openInNewTab(result.data);
                 }
             });
        }

        $scope.ShowShipmentStatus = function (model) {
            model.AWBNo = utility.replaceString(model.AWBNo, '"');
            $http({ method: 'Get', url: 'ServiceProviderAPI/GetTracking?referenceID=' + model.AWBNo })
                 .then(function (result) {
                     if (!result.IsError) {
                         $().showMessage($scope, $timeout, false, result.data);
                     } else {
                         $().showMessage($scope, $timeout, true, result.UserMessage);
                     }
                 });
        }

        $scope.PoupClose = function (model) {
            $(popupContent).html('');
            $(popupContent).closest('.popupwindow').hide();
        }
    }]);