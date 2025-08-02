app.controller('LoginController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
    console.log('LoginController loaded.');
    $scope.LoadingWithText = true;
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
    var animation = anime({
        targets: '.tick-demo .el',
        translateY:
        {
            value: 50, // 100px * 2.5 = '250px'
            duration: 2000
        },
        direction: 'alternate',
        loop: true,
        easing: 'easeInOutQuad',
        autoplay: false,
        // Initial delay of 2 seconds removed from here
    });

    var animation2 = anime({
        targets: '.tick-demo2 .el2',
        translateY: 
        {
            value: -50, // 100px * 2.5 = '250px'
            duration: 2000
        },
        direction: 'alternate',
        loop: true,
        easing: 'easeInOutQuad',
        autoplay: false,
        // Initial delay of 3 seconds removed from here
    });

    var animation3 = anime({
        targets: '.tick-demo3 .el3',
        translateY:
        {
            value: 50, // 100px * 2.5 = '250px'
            duration: 2000
        },
        direction: 'alternate',
        loop: true,
        easing: 'easeInOutQuad',
        autoplay: false,
        // Initial delay of 3 seconds removed from here
    });

    function animateWithDelay(animation, delay) {
        setTimeout(function () {
            requestAnimationFrame(function () {
                animation.play();
            });
        }, delay);
    }

    animateWithDelay(animation, 2000); // Start animation after 2 seconds
    animateWithDelay(animation2, 2000); // Start animation2 after 3 seconds
    animateWithDelay(animation3, 2000); // Start animation2 after 3 seconds



    $scope.Signin = function (event) {
        event.preventDefault();

        if ($("#frmLogin").valid() === true) {
           
            LoginViewLoaderWithOverlay();
            $scope.LoadingWithText = false;

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
                            $scope.LoadingWithText = true;

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

