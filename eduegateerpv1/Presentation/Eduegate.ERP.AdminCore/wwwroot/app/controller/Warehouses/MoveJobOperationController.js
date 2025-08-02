app.controller("MoveJobOperationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("MoveJobOperationController");
        $scope.IsError = false;

        $scope.UpdateAllJobs = function () {
            $("#Overlay").fadeIn(100);
            var model = $scope.$parent.SelectedIds;
            $.ajax({
                type: "POST",
                url: 'JobOperation/UpdateAllJobs',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(model),
                success: function (result) {
                    if (result.IsError == false)
                        $().showMessage($scope, $timeout, false, result.UserMessage);
                    else
                        $().showMessage($scope, $timeout, true, "Updation of Job Status Failed");
                    $("#Overlay").fadeOut(100);
                }
            });
        }
    }]);