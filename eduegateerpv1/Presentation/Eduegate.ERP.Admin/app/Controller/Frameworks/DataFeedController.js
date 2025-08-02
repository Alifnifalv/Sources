app.controller("DataFeedController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Data Feed View Loaded");
    var windowContainer = null;

    //Initializing the Data Feed Controller
    $scope.Init = function (window, model) {
        //$scope.Servicecall();
        windowContainer = '#' + window;
        $scope.DataFeedModel = model;

        console.log(model);

    }

    $scope.DownloadTemplate = function () {
        if ($('#DataFeedType option:selected').index() > 0) {
            var selectedFeedType = $('#DataFeedType option:selected').val();
            $.ajax({
                url: "DataFeed/DataFeed/DownloadTemplate?templateTypeID=" + InitiateFeed,
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
            //window.location = $('#DataFeedType option:selected').attr('attribute_url');
        }
    }

    $scope.UploadDocument = function () {
        if ($('#DataFeedType option:selected').index() > 0) {
            var selectedFeedType = $('#DataFeedType option:selected').val();

            $.ajax({
                url: "DataFeed/DataFeed/UploadDocument?uploadFileType=" + selectedFeedType,
                type: 'GET',
                success: function (result) {
                }
            })
        }

    }

    $scope.InitiateFeed = function () {
        if ($('#DataFeedType option:selected').index() > 0) {

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
    }
    
    $scope.ProcessFeed = function () {
        
        if ($('#DataFeedType option:selected').index() > 0) {
            //var selectedFeedType = $('#DataFeedType option:selected').val();

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

            //$.ajax({
            //    url: "DataFeed/ProcessFeed?uploadFileType=" + selectedFeedType,
            //    type: 'GET',
            //    success: function (result) {
            //        $("#imported-filestatus").append($compile(result)($scope));
            //    }
            //})
        }

    }
}]);
