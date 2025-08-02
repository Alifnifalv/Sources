var tempOTP = ""; var isOTPverify = false;
function OTPGenerate() {
    
    var emailid = $("#Email").val();
    if (emailid != null && emailid != "") {
        if (ValidateEmail(emailid) == true) {
            $("#LoginOverlay").show();
            $("#Overlay").fadeIn();
            $("#ButtonLoader").fadeIn();
            $.getJSON("OTPforVerifyEmail", { email: emailid }, function (result) {
                if (result != "0") {
                    tempOTP = result;
                    $("#Register_02").show();
                    $("#Register_01").hide();
                    var emaillength = emailid.length;
                    var emailshort = emailid.substring(0, 2) + "*****" + emailid.substring(emaillength / 2, (emaillength / 2) + 3) + "***" + emailid.substring(emaillength - 3, emaillength);
                    $("#Register_02").prepend('<span style="color:green;font-weight: bold;">OTP has been sent to ' + emailshort + '</span>');
                }
                else {
                    alert("Account is already created.");
                }
                $("#Overlay").fadeOut();
                $("#ButtonLoader").fadeOut();
                $("#LoginOverlay").hide();
            })
        }
        else {
            callToasterPlugin('error', 'You have entered an invalid email address!');
        }
    }
    else {
        callToasterPlugin('error', 'Please enter mail id');
        $("#Email").focus();
        return;
    }
};

function VerifyOTP() {
    var emailid = $("#Email").val();
    var otptext = $("#otp").val();
    if (otptext != null && otptext != "") {
        $("#LoginOverlay").show();
        $("#Overlay").fadeIn();
        $("#ButtonLoader").fadeIn();
        //$.getJSON("VerifyOTP", { OTP: otptext, email: emailid }, function (result) {
        if (tempOTP == otptext) {
            $("#Register_03").show();
            $("#Register_02").hide();

            $("#Overlay").fadeOut();
            $("#ButtonLoader").fadeOut();
            isOTPverify = true;
        }
        else {
            callToasterPlugin('error', 'Invalid OTP');
        }
    }
    else {
        callToasterPlugin('error', 'Please enter OTP');
    }
    //})
    $("#LoginOverlay").hide();
}

//call ToasterPlugin
function callToasterPlugin(status, title) {
    new Notify({
        status: status,
        title: title,
        effect: 'fade',
        speed: 300,
        customClass: null,
        customIcon: null,
        showIcon: true,
        showCloseButton: true,
        autoclose: true,
        autotimeout: 3000,
        gap: 20,
        distance: 20,
        type: 1,
        position: 'right top'
    })
}
//end call ToasterPlugin

$("#frmRegister").submit(function (event) {
    var Password = $("#Password").val();
    var ConfirmPassword = $("#ConfirmPassword").val();
    if ($("#Email").val() == null || $("#Email").val() == "") {
        callToasterPlugin('error', 'Please enter mail id');
        $("#Email").focus();
        return;
    }
    else if (ValidateEmail($("#Email").val()) != true) {
        callToasterPlugin('error', 'You have entered an invalid email address!');
        $("#Email").focus();
        return;
    }
    else if ($("#Password").val() == null || $("#Password").val() == "") {
        callToasterPlugin('error', 'Please enter password');
        $("#Password").focus();
        return;
    }
    else if ($("#Password").val().length < 6 || $("#Password").val().length > 18) {
        callToasterPlugin('error', 'You must enter a password between 6 and 18.');
        $("#Password").focus();
        return;
    }
    else if ($("#ConfirmPassword").val() == null || $("#ConfirmPassword").val() == "") {
        callToasterPlugin('error', 'Please enter confirm password');
        $("#ConfirmPassword").focus();
        return;
    }
    else if ($("#ConfirmPassword").val() != $("#Password").val()) {
        callToasterPlugin('error', 'The password and confirmation password do not match.');
        $("#Password").focus();
        return;
    }
    else if (isOTPverify == true) {
        $("#LoginOverlay").show();
        $("#submit").trigger('click');
        if (Password != ConfirmPassword) {
            $("#LoginOverlay").hide();
        }
        if (Password == null || Password == "" || ConfirmPassword == null || ConfirmPassword == "") {
            $("#LoginOverlay").hide();
        }
    }
    else {
        event.preventDefault();
        $("#LoginOverlay").hide();
    }

});
//verify email id

//validation
function validationchecking() {
    var Password = $("#Password").val();
    var ConfirmPassword = $("#ConfirmPassword").val();
    if ($("#Email").val() == null || $("#Email").val() == "") {
        callToasterPlugin('error', 'Please enter mail id');
        $("#Email").focus();
        return;
    }
    else if (ValidateEmail($("#Email").val()) != true) {
        callToasterPlugin('error', 'You have entered an invalid email address!');
        $("#Email").focus();
        return;
    }
    else {
        if ($("#otp").val() == null || $("#otp").val() == "") {
            OTPGenerate();
        }
        else if (isOTPverify == false) {
            VerifyOTP();
        }
        else {
            if ($("#Password").val() == null || $("#Password").val() == "") {
                callToasterPlugin('error', 'Please enter password');
                $("#Password").focus();
                return;
            }
            else if ($("#Password").val().length < 6 || $("#Password").val() > 18) {
                callToasterPlugin('error', 'You must enter a password between 6 and 18.');
                $("#Password").focus();
                return;
            }
            else if ($("#ConfirmPassword").val() == null || $("#ConfirmPassword").val() == "") {
                callToasterPlugin('error', 'Please enter confirm password');
                $("#ConfirmPassword").focus();
                return;
            }
            else if ($("#ConfirmPassword").val() != $("#Password").val()) {
                callToasterPlugin('error', 'The password and confirmation password do not match.');
                $("#Password").focus();
                return;
            }
        }
        return;
    }

    
    
}


$(document).ready(function () {
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            validationchecking()
            return false;
        }
    });
});

function ValidateEmail(inputText) {
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (inputText.match(mailformat)) {
        return true;
    }
    else {
        
        return false;
    }
}
        //end verify email id
