app.controller("PageController", ["$scope", "$timeout", "$window", "$http", "$compile", "$sce", "$q",
    function ($scope, $timeout, $window, $http, $compile, $sce, $q) {
        console.log('PageController controller loaded.');

        $scope.runTimeParameter = null;
        $scope.LazyLoadBoilerPlateNos = 0;
        $scope.GetBoilerPlatesURL = 'Boilerplate/GetBoilerPlates';
        $scope.Model = null;
        $scope.window = "";
        $scope.windowname = "";

        $scope.init = function (model, window, windowname) {
            $scope.runTimeParameter = model.parameter;
            $scope.Model = model;
            $scope.window = window;
            $scope.windowname = windowname;
            $scope.LoadBoilerPlates();
        };

        $scope.LoadBoilerPlates = function () {
            $timeout(function () {
                $.each($scope.Model.ViewModel.Boilerplates, function (index) {
                    var width = new JSLINQ(this.RuntimeParameters).Where(function (data) { return data.Key === "Width"}).items[0];

                    if (width === null || width === undefined) {
                        width = {};
                        width.Value = "100";
                    }

                    $("#" + $scope.windowname + " .main-page").append($compile("<div id='BoilerPlate" + this.BoilerplateMapIID + "' class='dboard_columns col-span-" + width.Value + "'></div>")($scope));

                    $.ajax({
                        type: 'POST',
                        url: utility.myHost + "/Boilerplate/Template",
                        data: this,
                        success: function (result) {
                            $("#" + $scope.windowname + " .main-page #BoilerPlate" + result.BoilerplateMapIID).append($compile(result.Content)($scope));
                        }
                    });
                });
            });
        }
    }]);

