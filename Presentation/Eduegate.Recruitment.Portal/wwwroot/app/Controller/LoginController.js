app.controller("LoginController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {
        console.log("LoginController Loaded");

        $scope.loginDTO = null;
        $scope.IsOTPGenerated = false;
        $scope.OTPbutton = "Get OTP";

        const button = document.getElementById('submitBtn');
        if (button) {
            button.value = $scope.IsOTPGenerated ? 'Continue' : 'Submit';
        }

        $scope.registrationDTO = {
            FirstName: '',
            VendorCr: '',
            Email: '',
            TelephoneNo: '',
            Password: '',
            ConfirmPassword: '',
            OTP: null
        };

        $scope.RegisterButton = "Continue"; 

        $scope.Init = function (model) {
            $scope.LoginModel = model;
        };

        $scope.submitRegisterForm = function () {

            if ($scope.registrationDTO.Password != $scope.registrationDTO.ConfirmPassword) {
                toastr.error("The confirmed password does not match the original password. Please ensure they are the same");
                return false;
            }

            if ($scope.IsOTPGenerated) {
                validateOTP().then(function (isValid) {
                    if (isValid) {
                        updateIsActiveStatusAndSendWelcomeMail().then(function () {
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 1000);
                        }).catch(function (errorMessage) {
                            return false;
                        });
                    } else {
                        toastr.error("Invalid OTP");
                        return false;
                    }
                }).catch(function (errorMessage) {
                    return false;
                });
            }
            else {
                registerUser();
            }
        };

        $scope.submitResetPassword = function () {
            if ($scope.registrationDTO.Password != $scope.registrationDTO.ConfirmPassword) {
                toastr.error("The confirmed password does not match the original password. Please ensure they are the same");
                return false;
            }

            if ($scope.registrationDTO.OTP == null || $scope.registrationDTO.OTP == undefined) {
                toastr.error("Please generate and enter the OTP (One-Time Password) to proceed.");
            }
            else {
                validateOTP().then(function (isValid) {
                    if (isValid) {
                        resetLoginPassword().then(function () {
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 1000);
                        }).catch(function (errorMessage) {
                            return false;
                        });
                    } else {
                        toastr.error("Invalid OTP");
                        return false;
                    }
                }).catch(function (errorMessage) {
                    return false;
                });
            }
        };

        function updateIsActiveStatusAndSendWelcomeMail() {
            showSpinner();
            return $.ajax({
                url: utility.myHost + "Account/UpdateIsActiveStatusAndSendMail",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.registrationDTO)
            }).then(function (result) {
                hideSpinner();
                if (!result.IsError) {
                    return true; 
                } else {
                    return false;
                }
            }).catch(function (error) {
                hideSpinner();
                throw error;
            });
        } 

        function validateOTP() {
            showSpinner();
            return $.ajax({
                url: utility.myHost + "Account/ValidateOTP",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.registrationDTO)
            }).then(function (result) {
                hideSpinner();
                if (!result.IsError) {
                    return true;
                } else {
                    return false;
                }
            }).catch(function (error) {
                hideSpinner();
                throw error;  
            });
        } 

        $scope.validateLogin = function () {
            showSpinner(); 
            $.ajax({
                url: utility.myHost + "Account/LoginValidate",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.loginDTO),
                success: function (result) {
                    if (result.IsError === false) {
                        setTimeout(function () {
                            window.location = '/Home/Home';
                        }, 1000);
                        hideSpinner();
                    } else {
                        toastr.error(result.ReturnMessage);
                        hideSpinner();
                        return false;
                    }
                    hideSpinner();
                }
            });
        };

        $scope.showPassword = function () {
            var x = document.getElementById("password");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }


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

        function registerUser () {
            const submitBtn = document.getElementById('submitBtn');
            submitBtn.disabled = true;
            showSpinner();

            document.body.classList.add('disabled');

            $.ajax({
                url: utility.myHost + "Account/RegisterUser",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.registrationDTO),
                success: function (result) {
                    if (result.IsError === false) {
                        //toastr.success(result.ReturnMessage);
                        $scope.generateOTP();
                        setTimeout(function () {
                            hideSpinner();
                            document.body.classList.remove('disabled');
                            submitBtn.disabled = false;
                        }, 1000);
                    } else {
                        toastr.error(result.ReturnMessage);
                        submitBtn.disabled = false;
                        document.body.classList.remove('disabled');
                        hideSpinner();
                    }
                }
            });
        }

        $scope.generateOTP = function () {
            showSpinner();
            document.body.classList.add('disabled');

            $.ajax({
                url: utility.myHost + "Account/OTPGenerate",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.registrationDTO),
                success: function (result) {
                    if (result.IsError === false) {
                        toastr.success(result.ReturnMessage);
                        setTimeout(function () {
                            document.body.classList.remove('disabled');
                            hideSpinner();
                        }, 300);
                        $scope.$apply(function () {
                            $scope.IsOTPGenerated = true;
                            button.value = $scope.IsOTPGenerated ? 'Continue' : 'Submit';
                            $scope.OTPbutton = "Resend OTP";
                            hideSpinner();
                        });

                    } else {
                        toastr.error(result.ReturnMessage);
                        document.body.classList.remove('disabled');
                        hideSpinner();
                    }
                }
            });
        }

        function resetLoginPassword() {
            showSpinner();
            return $.ajax({
                url: utility.myHost + "Account/ResetLoginPassword",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.registrationDTO)
            }).then(function (result) {
                hideSpinner();
                if (!result.IsError) {
                    toastr.success(result.UserMessage);
                    return true;
                } else {
                    toastr.error(result.UserMessage);
                    return false;
                }
            }).catch(function (error) {
                hideSpinner();
                throw error;
            });
        } 

        function showSpinner() {
            document.getElementById('spinner').style.display = 'block';
        }

        function hideSpinner() {
            document.getElementById('spinner').style.display = 'none';
        } 
    }
]);
