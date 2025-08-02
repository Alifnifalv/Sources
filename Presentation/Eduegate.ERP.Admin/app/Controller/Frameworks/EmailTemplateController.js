app.controller("EmailTemplateController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("EmailTemplateController");

        angular.extend(this, $controller('SummaryViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $location:$location ,$route : $route}));

        $scope.GetEmailDetails = function (ID) {
            $('.preload-overlay', $(windowContainer)).show();
            var editUrl = "EmailTemplate/GetEmailDetails?Id=" + ID;
            $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                console.log(result.data);
                if (!result.IsError) {
                    $scope.EmailNotificationTypeViewModel = result.data;
                }
                else {
                    $().showMessage($scope, $timeout, true, result.UserMessage);
                }
                $('.preload-overlay', $(windowContainer)).hide();

            });
        }

        $scope.SaveEmailTemplate = function (model) {
            $('.preload-overlay', $(windowContainer)).show();
            var url = "EmailTemplate/SaveEmailTemplates";
            var email = model;
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(email),
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError)
                        $('.preload-overlay', $(windowContainer)).hide();
                    else
                        $().showMessage($scope, $timeout, true, result.UserMessage);
                }
            });
        }
    }]);