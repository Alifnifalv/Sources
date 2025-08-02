app.controller("FilterController", ["$scope", "$compile", "$timeout", function ($scope, $compile,$timeout) {
    console.log("FilterController");
    $scope.Model = {};
    var view = null;
    $scope.WindowContainer = null;
    $scope.LookUps = [];

    $scope.init = function (windowContainer, model, v) {
        $scope.Model = JSON.parse(model.JsonModel);
        view = v;
        $scope.WindowContainer = windowContainer;
    };

    $scope.ResetSaveFilter = function () {
        $('.preload-overlay').show();
        $('.preloader').show();

        $.ajax({
            type: "POST",
            url: 'Mutual/ResetFilter',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify($scope.Model),
            success: success,
        });
    };

    $scope.SaveFilter = function () {
        $('.preload-overlay').show();
        $('.preloader').show();

        $.ajax({
            type: "POST",
            url: 'Mutual/SaveFilter',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify($scope.Model),
            success: success,
        });
    };

    function success() {
        $('.preload-overlay').hide();
        $('.preloader').hide();
        //$scope.$parent.RowModel.activate(view, $scope.$parent.RowModel.ControlerName)
        $scope.$parent.RowModel.ReLoad();
    }

    $scope.dateiconclick = function () {
        $("#datepicker-icon-controller").focus();
    }

    $scope.LoadLookups = function (lookupName) {

        if ($scope.LookUps[lookupName] != null)
            return;

        $scope.LookUps[lookupName] = { Key: "", Value: "" };

        $.ajax({
            type: "GET",
            url: "Mutual/GetLookUpData?lookType=" + lookupName,
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
    };

    $scope.ClearField = function (index) {
        $scope.Model[index].FilterValue = null;
        $scope.Model[index].FilterValue2 = null;
        $scope.Model[index].FilterValue3 = null;
    };
}]);