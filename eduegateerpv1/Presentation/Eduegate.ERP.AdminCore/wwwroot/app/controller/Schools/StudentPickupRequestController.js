app.controller("StudentPickupRequestController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.ParentDetails = null;

    $scope.StudentChanges = function (model) {

        if (!model.Student.Key) {
            return false;
        };

        showOverlay();

        var url = "Schools/School/GetGuardianDetails?studentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (!result.data.IsError) {
                    $scope.ParentDetails = result.data.Response;
                }
                hideOverlay();
                if (model.PickedBy) {
                    $scope.PickedByChanges(model);
                }
            }, function () {
                hideOverlay();
            });

    };

    $scope.PickedByChanges = function (model) {

        if (!model.PickedBy) {
            return false;
        }

        var pickedByData = $scope.LookUps.StudentPickedBy.find(x => x.Key == model.PickedBy);

        if (pickedByData && $scope.ParentDetails) {
            if (pickedByData.Value == "Father") {
                model.FirstName = $scope.ParentDetails.FatherFirstName;
                model.MiddleName = $scope.ParentDetails.FatherMiddleName;
                model.LastName = $scope.ParentDetails.FatherLastName;
            }
            else if (pickedByData.Value == "Mother") {
                model.FirstName = $scope.ParentDetails.MotherFirstName;
                model.MiddleName = $scope.ParentDetails.MotherMiddleName;
                model.LastName = $scope.ParentDetails.MotherLastName;
            }
            else if (pickedByData.Value == "Guardian") {
                model.FirstName = $scope.ParentDetails.GuardianFirstName;
                model.MiddleName = $scope.ParentDetails.GuardianMiddleName;
                model.LastName = $scope.ParentDetails.GuardianLastName;
            }
            else {
                model.FirstName = null;
                model.MiddleName = null;
                model.LastName = null;
            }
        }
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);