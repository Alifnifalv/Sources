app.controller("ResetController", ["$scope", "$timeout", "$window", "$http", "$compile", function ($scope, $timeout, $window, $http, $compile) {
    this.windowContainer = null;
   
    $scope.init = function (passwordResetModel, windowName) {
        windowContainer = '#' + windowName;
        $scope.PasswordResetModel = passwordResetModel;
        $scope.Message = "";
        $scope.MessageType = null;
        $scope.ShowMessage = false;
    };

    $scope.ResetPasswordSubmit = function () {
        if ($("#ResetPasswordForm").valid() == true) {
            $('.preload-overlay', $(windowContainer)).attr('style', 'display:block');

            $scope.ShowMessage = false;
            $scope.Message = "";
            $scope.MessageType = "";
            $scope.IsPasswordNotMatch = $scope.PasswordResetModel.Password != $scope.PasswordResetModel.ConfirmPassword ? false : true;
            if ($scope.IsPasswordNotMatch == true) {
                $.ajax({
                    url: "Account/ResetPasswordSubmit",
                    type: 'POST',
                    data: $scope.PasswordResetModel,
                    success: function (result) {
                        if (result.IsError === false) { 
                            $scope.Message = result.Message;

                            if (result.MessageType != "") {
                                $scope.MessageType = result.MessageType;
                                $scope.ShowMessage = true;
                            }
                           
                            $scope.$apply();


                            $('.preload-overlay', $(windowContainer)).hide();
                        };

                    },
                    error: function (err) {
                        $('.preload-overlay', $(windowContainer)).hide();
                        $scope.MessageType = "Error";
                        $scope.ShowMessage = true;
                    }
                });
            }
            else
            {
                $scope.MessageType = 'Error';
                $scope.Message = 'The password and confirmation password do not match.';
                $scope.ShowMessage = true;
                $('.preload-overlay', $(windowContainer)).hide();
            }
        }
    }

    function callAtTimeout() {
        //console.log("Timeout occurred");
    }
}]);