app.controller("DataFeedControllerV2", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Data Feed View Loaded V2");
    var windowContainer = null;

    //Initializing the Data Feed Controller
    $scope.Init = function (window, model, dataFeedTypeID) {
        windowContainer = '#' + window;
        $scope.DataFeedModel = model;
        console.log(model);
    }

    $scope.DownloadTemplate = function (templateID) {
        if (templateID != undefined) {
            var selectedFeedType = $('#DataFeedType option:selected').val();
            $.ajax({
                url: "DataFeed/DataFeed/DownloadTemplate?templateTypeID=" + templateID,
                type: 'GET',
                success: function (result) {
                    if (result.IsSuccess == true)
                        window.location = result.DownloadPath;
                    else {

                    }
                },
                error: function () {

                }
            })
        }
    }

    $scope.UploadDocument = function () {
        var selectedFeedType = $('#DataFeedType option:selected').val();

        $.ajax({
            url: "DataFeed/DataFeed/UploadDocument?uploadFileType=" + selectedFeedType,
            type: 'GET',
            success: function (result) {
            }
        })
    }

    $scope.InitiateFeed = function (templateID) {
        $scope.DataFeedModel.FeedFileName = $('.filename').val();
        var model = angular.copy($scope.DataFeedModel);

        $.ajax({
            url: 'DataFeed/DataFeed/InitiateFeed',
            type: 'POST',
            data: model,
            success: function (result) {
                if ($("#divUploadProgress" + result.ID).length == 0) {
                    $("#divUploadedFiles").append(result.RawHTML);
                }
                else {
                    $("#divUploadProgress" + result.ID).replaceWith(result.RawHTML);
                }
                $scope.DataFeedModel.DataFeedID = result.ID;
                $scope.ProcessFeed();
            }
        })
    }

    $scope.ProcessFeed = function () {
        $scope.DataFeedModel.FeedFileName = $('.filename').val();

        var file = $scope.feedFile;
        var uploadUrl = "DataFeed/DataFeed/ProcessFeed?ID=" + $scope.DataFeedModel.DataFeedID;
        var fd = new FormData();
        fd.append('file', file);

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })

        .then(function (result) {
            var divID = "divUploadProgress" + result.ID;
            if ($("#" + divID).length == 0) {
                $("#divUploadedFiles").append(result.RawHTML);
            }
            else {
                $("#divUploadProgress" + result.ID).replaceWith(result.RawHTML);
            }

            $scope.DataFeedModel.DataFeedID = 0;
        })

        .error(function () {
        });
    }
}]);
