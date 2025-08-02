app.controller("UserManagementController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("User Management Loaded");

    //Initializing the pos view model
    $scope.Init = function (model) {
        $scope.UserManagementModel = model;
        $scope.submitted = false;
        $scope.IsUserExists = false;
        $scope.UserExistsMessage = "";
        $scope.GetTitleMasters();
        $scope.GetLoginStatusMasters();

        window.setTimeout(function () {
            $scope.$apply();
        }, 2000);
    }

    $scope.GetTitleMasters = function () {

        $scope.TitleMasters = [];

        $.ajax({
            type: "GET",
            url: "Mutual/GetLookUpData?lookType=Title",
            success: function (result) {

                $.each(result, function (index, item) {

                    if (item.Value != "")
                    {
                        var title = { TitleIID: item.Key, TitleName: item.Value };
                        $scope.TitleMasters.push(title);
                    }

                });
            }
        });
    }

    $scope.GetLoginStatusMasters = function () {

        $scope.StatusMasters = [];

        $.ajax({
            type: "GET",
            url: "UserManagement/GetLoginStatus",
            success: function (result) {

                if (result.IsError == false) {

                    $.each(result.StatusMasters, function (index, item) {
                        $scope.StatusMasters.push(item);
                    });
                }
            }
        });
    }

    $scope.LoadUserManagementList = function () {

        $.ajax({
            type: "GET",
            url: "UserManagement/List",
            success: function (result) {
                console.log("User Management view loaded successfully");
                $("#LayoutContentSection").html($compile(result)($scope));
            }
        });
    }

    $scope.SaveUserManagement = function () {

        $scope.submitted = true;
        var formValid = $scope.UM.$valid;

        if (formValid == true) {

            ViewButtonLoaderWithOverlay();

            $.ajax({
                type: "POST",
                url: "UserManagement/SaveUserManagement",
                data: $scope.UserManagementModel,
                success: function (result) {

                    HideButtonLoaderWithOverlay();

                    if (result.IsError == true)
                    {
                        if (result.IsUserExists == true)
                        {
                            $scope.IsUserExists = result.IsUserExists;
                            $scope.UserExistsMessage = result.Message;
                        }
                    }
                    else
                    {
                        window.setTimeout(function () {
                            $scope.LoadUserManagementList();
                        }, 10);
                    }

                    $scope.$apply();
                }
            });
        }
    }

    $scope.EmailKeyUp = function () {

        $scope.IsUserExists = false;
    }

}]);