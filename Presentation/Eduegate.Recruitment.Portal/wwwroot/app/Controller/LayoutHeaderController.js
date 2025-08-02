app.controller('LayoutHeaderController', ['$scope', '$http', "$rootScope", "$window", "$timeout", function ($scope, $http, $rootScope, $window, $timeout) {
    console.log('LayoutHeader controller loaded.');
    $scope.UserDetails = {};
    $scope.UserDetails.ProfileFile = utility.myHost + "Images/profilepic.png"

    $scope.Init = function () {
        $.ajax({
            url: utility.myHost + "Home/GetUserDetails",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $scope.$apply(function () {
                        $scope.UserDetails = result.Response;
                    });
                }
            }
        });
    };

}]);