app.controller('StaticContentDataController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService",
    "toaster", '$timeout', '$location', "$q", function ($scope, $http, $compile, $root, $subscription, $toaster,
        $timeout, $location, $q) {
        console.log('StaticContentDataController controller loaded.');

        $scope.AboutUs = null;
        $scope.ContactUs = null;
        $scope.ContactDetails = null;

        $scope.init = function (model) {
            GetAboutUs();
            GetContactUs();
            GetContactDetails();
        };

        function GetAboutUs() {
            var contentID = 1;
            $.ajax({
                type: "GET",
                data: { contentID: contentID },
                url: utility.myHost + "Home/GetAboutandContactDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.AboutUs = result.Response.Description;
                        });
                    }
                },
                error: function () {
                },
                complete: function (result) {
                }
            });
        }

        function GetContactUs() {
            var contentID = 2;
            $.ajax({
                type: "GET",
                data: { contentID: contentID },
                url: utility.myHost + "Home/GetAboutandContactDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.ContactUs = result.Response.Description;
                        });
                    }
                },
                error: function () {
                },
                complete: function (result) {
                }
            });
        }

        function GetContactDetails() {
            var contentID = 4;
            $.ajax({
                type: "GET",
                data: { contentID: contentID },
                url: utility.myHost + "Home/GetAboutandContactDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.ContactDetails = result.Response.Description;
                        });
                    }
                },
                error: function () {
                },
                complete: function (result) {
                }
            });
        }

    }]);