app.controller("LoginController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {
        console.log("LoginController Loaded");

        $scope.loginDTO = null;

        $scope.registrationDTO = {
            FirstName: '',
            VendorCr: '',
            Email: '',
            TelephoneNo: '',
            Password: '',
            ConfirmPassword: ''
        };

        $scope.OTPbutton = "Get OTP"; 

        $scope.Init = function (model) {
            $scope.LoginModel = model;
        };

        $scope.submitLoginForm = function () {
            const submitBtn = document.getElementById('submitBtn');
            submitBtn.disabled = true;
            showSpinner();

            document.body.classList.add('disabled');

            $.ajax({
                url: utility.myHost + "Account/Register",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.registrationDTO),
                success: function (result) {
                    if (result.IsError === false) {
                        toastr.success(result.ReturnMessage);
                        setTimeout(function () {
                            window.location = '/Account/Login';
                            hideSpinner();
                        }, 1000);
                    } else {
                        toastr.error(result.ReturnMessage);
                        submitBtn.disabled = false;
                        document.body.classList.remove('disabled');
                        hideSpinner();
                    }
                }
            });
        };

        $scope.showPassword = function () {
            var x = document.getElementById("ConfirmPassword");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }


        $scope.GetOTP = function () {

            document.body.classList.add('disabled');

            $.ajax({
                url: utility.myHost + "Account/OTPGenerate",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.loginDTO),
                success: function (result) {
                    if (result.IsError === false) {
                        toastr.success(result.ReturnMessage);
                        setTimeout(function () {
                            document.body.classList.remove('disabled');
                        }, 300);
                        $scope.$apply(function () {
                            $scope.OTPbutton = 'Resend OTP';
                        });

                    } else {
                        toastr.error(result.ReturnMessage);
                        document.body.classList.remove('disabled');
                    }
                }
            });
        };

        $scope.ResetPassword = function () {

            document.body.classList.add('disabled');

            $.ajax({
                url: utility.myHost + "Account/ResetPasswordSubmit",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.loginDTO),
                success: function (result) {
                    if (result.IsError === false) {
                        toastr.success(result.Message);
                        setTimeout(function () {
                            window.location = '/Account/Login';
                        }, 1000);
                    } else {
                        toastr.error(result.Message);
                        document.body.classList.remove('disabled');
                    }
                }
            });
        };


        function showSpinner() {
            document.getElementById('spinner').style.display = 'block';
        }
 
        function hideSpinner() {
            document.getElementById('spinner').style.display = 'none';
        }  
    }
]);
