app.controller('LoginController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
    console.log('LoginController loaded.');
 
    $scope.Init = function (model) {
        $scope.LoginModel = model;
        $scope.IsEmailPasswordIncorrect = false;
    };

    $scope.Signup = function () {
        $location.path('/Signup');
    };

    $scope.Register = function (event) {
        event.preventDefault();

        if ($("#frmRegister").valid() === true) {
            return true;
        }
    };

  

    $scope.Signin = function (event) {
        event.preventDefault();

        if ($("#frmLogin").valid() === true) {
           
                LoginViewLoaderWithOverlay();

                $.ajax({
                    url: utility.myHost + 'Account/Login',
                    type: 'POST',
                    data: $scope.LoginModel,
                    success: function (result) {
                        if (result.IsSuccess === true) { //Success
                            var redirectUrl = utility.myHost + "?1";
                            if (result.UserSettings) {
                                if (!utility.UserCache) {
                                    utility.UserCache = {};
                                }

                                utility.UserCache.UserSettings = result.UserSettings;
                                var layout = result.UserSettings.find(setting => setting.SettingCode.toLowerCase() === 'layout');
                                var theme = result.UserSettings.find(setting => setting.SettingCode.toLowerCase() === 'theme');

                                if (layout) {
                                    redirectUrl = redirectUrl + '&layout=' + layout.SettingValue;
                                }

                                if (theme) {
                                    redirectUrl = redirectUrl + '&theme=' + theme.SettingValue;
                                }
                            }

                            utility.redirect(redirectUrl);
                            window.setTimeout(function () {
                                LoginHideLoaderWithOverlay();
                            }, 500);
                        }
                        else { //Failure
                            LoginHideLoaderWithOverlay();
                            $scope.IsEmailPasswordIncorrect = true;
                            $scope.$apply();
                        }
                    }
                });
            
        }
    };

    //Type any text in email input field, setting boolean property (IsEmailPasswordIncorrect) is false
    $scope.EmailKeyup = function () {
        $scope.IsEmailPasswordIncorrect = false;
    };

    //Type any text in password input field, setting boolean property (IsEmailPasswordIncorrect) is false
    $scope.PasswordKeyup = function () {
        $scope.IsEmailPasswordIncorrect = false;
    };

    //form is valid on submit login loading button loader with overlay
    function LoginViewLoaderWithOverlay() {
        $("#LoginOverlay").fadeIn();
        $("#LoginButtonLoader").fadeIn();
    }

    //
    function LoginHideLoaderWithOverlay() {
        $("#LoginOverlay").fadeOut();
        $("#LoginButtonLoader").fadeOut();
    }
}]);

