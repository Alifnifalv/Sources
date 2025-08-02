app.controller("ResetController", ["$scope", "$timeout", "$window", "$http", "$compile", function ($scope, $timeout, $window, $http, $compile) {
    this.windowContainer = null;
   
    $scope.init = function (passwordResetModel, windowName) {
        windowContainer = '#' + windowName;
        $scope.PasswordResetModel = passwordResetModel;
        $scope.Message = "";
        $scope.MessageType = null;
        $scope.ShowMessage = false;
    };

    $scope.OTPGenerate = function () {
        var emailid = $("#Email").val();
        $("#Overlay").fadeIn();
        $("#ButtonLoader").fadeIn();
        $("#LoginOverlay").show();
        $.getJSON("OTPGenerate", { email: emailid }, function (result) {
            if (result == "1") {
                $("#ResetPasswordStep_02").show();
                $("#ResetPasswordStep_01").hide();
                var emaillength = emailid.length;
                var emailshort = emailid.substring(0, 2) + "*****" + emailid.substring(emaillength / 2, (emaillength / 2) + 3) + "***" + emailid.substring(emaillength - 3, emaillength);
                $("#ResetPasswordStep_02").prepend('<span style="color:green;font-weight: bold;">OTP has been sent to ' + emailshort + '</span>');
            }
            else {
                alert(result);
            }
            $("#Overlay").fadeOut();
            $("#ButtonLoader").fadeOut();
            $("#LoginOverlay").hide();
        })
    };
    var isOTPverify = false;
    $scope.VerifyOTP = function () {
        var emailid = $("#Email").val();
        if (emailid != "" && emailid != null) {
            var otptext = $("#otp").val();
            $("#Overlay").fadeIn();
            $("#ButtonLoader").fadeIn();
            $.getJSON("VerifyOTP", { OTP: otptext, email: emailid }, function (result) {
                if (result == "1") {
                    $("#ResetPasswordStep_03").show();
                    $("#ResetPasswordStep_02").hide();
                    isOTPverify = true;
                }
                else {
                    alert("Invalid OTP");
                }
                $("#Overlay").fadeOut();
                $("#ButtonLoader").fadeOut();
            })
        }
       
    }
    $("#frmRegister").submit(function (event) {
        if (isOTPverify == true) {
            //$("#submit").trigger('click');
        }
        else {
            event.preventDefault();
        }

    });
    $scope.ResetPasswordSubmit = function () {
        if (isOTPverify == false) { event.preventDefault();}
        else if ($("#ResetPasswordForm").valid() == true) {
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
                        $timeout(function () {
                            $scope.Message = result.Message;

                            if (result.MessageType != "") {
                                $scope.MessageType = result.MessageType;
                                $scope.ShowMessage = true;
                            }

                            $('.preload-overlay', $(windowContainer)).hide();
                        });

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