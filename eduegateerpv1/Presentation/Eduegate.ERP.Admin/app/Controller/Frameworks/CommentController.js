app.controller("CommentController", ["$scope", "$http", "$compile", "$window", "$timeout", function ($scope, $http, $compile, $window, $timeout) {
    console.log("CommentController is loaded");
    $scope.Comments = [];
    $scope.EntityType = null;
    $scope.CommentText = null;
    $scope.Model = null;
    
    var windowContainer = null;
    

    $scope.Init = function (window, model) {
        windowContainer = '#' + window;
        $scope.Model = model;
        if ($scope.Model.DepartmentID == null) 
            $scope.Model.DepartmentID = 0;
        $scope.GetComments();
    }

    $scope.AddComment = function () {
        $('.preload-overlay', $(windowContainer)).show();

        var commentData = angular.copy($scope.Model);
        commentData.CommentText = $scope.CommentText;

        // Save comment
        $.ajax({
            url: 'Mutual/AddComment',
            type: 'POST',
            data: commentData,
            success: function (content) {
                $scope.GetComments();
                $scope.CommentText = '';
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.SaveComment = function (comment) {
        if (!comment.IsEdit) return;

        $('.preload-overlay', $(windowContainer)).show();
        // Save comment
        $.ajax({
            url: 'Mutual/AddComment',
            type: 'POST',
            data: comment,
            success: function (content) {
                comment.IsEdit = false;
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.ReplyComment = function (row) {
        var commentData = angular.copy($scope.Model);
        commentData.IsEdit = true;

        if (row.Comments == null)
            row.Comments = [];

        row.Comments.push(commentData);
    }

    $scope.GetComments = function () {
        $('.preload-overlay', $(windowContainer)).show();

        $.ajax({
            url: 'Mutual/GetComments?entityType=' + $scope.Model.EntityType + '&referenceID=' + $scope.Model.ReferenceID + '&departmentID=' + $scope.Model.DepartmentID,
            type: 'GET',
            success: function (data) {
                var commentsvalue = [];
                var loopcount = 1;
                $scope.$apply(function () {
                    $scope.Comments = data;
                    commentsvalue = data;
                    $scope.Comments.forEach(function (index) {
                        index.CommentText = index.CommentText.replace('< br />', '<br/>');
                        console.log(index.CommentText);
                    });
  
                    console.log($scope.Comments);
                    $('.preload-overlay', $(windowContainer)).hide();
                });
            }
        });
    }
}]);