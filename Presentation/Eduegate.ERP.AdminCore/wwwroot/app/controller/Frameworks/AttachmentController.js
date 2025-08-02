app.controller("AttachmentController", ["$scope", "$http", "$compile", "$window", "$timeout", function ($scope, $http, $compile, $window, $timeout) {
    console.log("AttachmentController is loaded");
    $scope.Attachments = [];
    $scope.EntityType = null;
    $scope.AttachmentText = null;
    $scope.Model = null;
    
    var windowContainer = null;
    

    $scope.Init = function (window, model) {
        windowContainer = '#' + window;
        $scope.Model = model;
        if ($scope.Model.DepartmentID == null) 
            $scope.Model.DepartmentID = 0;
        $scope.GetAttachments();
    }

    $scope.AddAttachment = function () {
        $('.preload-overlay', $(windowContainer)).show();

        var commentData = angular.copy($scope.Model);
        commentData.AttachmentText = $scope.AttachmentText;

        // Save comment
        $.ajax({
            url: 'Mutual/AddAttachment',
            type: 'POST',
            data: commentData,
            success: function (content) {
                $scope.GetAttachments();
                $scope.AttachmentText = '';
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.SaveAttachment = function (comment) {
        if (!comment.IsEdit) return;

        $('.preload-overlay', $(windowContainer)).show();
        // Save comment
        $.ajax({
            url: 'Mutual/AddAttachment',
            type: 'POST',
            data: comment,
            success: function (content) {
                comment.IsEdit = false;
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.ReplyAttachment = function (row) {
        var commentData = angular.copy($scope.Model);
        commentData.IsEdit = true;

        if (row.Attachments == null)
            row.Attachments = [];

        row.Attachments.push(commentData);
    }

    $scope.GetAttachments = function () {
        $('.preload-overlay', $(windowContainer)).show();

        $.ajax({
            url: 'Mutual/GetAttachments?entityType=' + $scope.Model.EntityType + '&referenceID=' + $scope.Model.ReferenceID + '&departmentID=' + $scope.Model.DepartmentID,
            type: 'GET',
            success: function (content) {
                console.log(content);
                $scope.Attachments = content;
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }
}]);