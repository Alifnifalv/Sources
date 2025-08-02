app.controller("CKEditorController", ["$scope", "$compile", "$http", "$timeout", function ($scope, $compile, $http, $timeout) {
    $scope.HtmlText = null;
    
    $scope.Init = function (model) {
        $scope.Model = model;
    }

    $scope.Apply = function () {
        //$scope.$parent.ApplyRichEditor($scope.Model.HtmlText);
        $('#richtext').val($scope.Model.HtmlText);
    }

    $scope.Close = function () {
        $scope.$parent.CloseCKEditor();
    }
}]);