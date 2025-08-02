///<reference path="utils.js" />

// here, the index maps to the error code returned from getValidationError - see readme
var errorMap = ["Invalid number", "Invalid country code", "Too short", "Too long", "Invalid number"];

/*--------------------------------------------------------------------------------------------------------------------------------*/

/* Father Mobile Number Start */
var inputMobileNumber = document.querySelector("#MobileNumber"),
    errorMsgMobileNumber = document.querySelector(".error-msg-MobileNumber"),
    validMsgMobileNumber = document.querySelector(".valid-msg-MobileNumber");

// initialise plugin
var itiFatherMobileNumber = window.intlTelInput(inputMobileNumber, {
    utilsScript: "../Design_v02/PhoneValid/js/utils.js"
});

var reset = function () {
    inputMobileNumber.classList.remove("error");
    errorMsgMobileNumber.innerHTML = "";
    errorMsgMobileNumber.classList.add("hide");
    validMsgMobileNumber.classList.add("hide");
};

// on blur: validate
inputMobileNumber.addEventListener('blur', function () {
    reset();
    if (inputMobileNumber.value.trim()) {
        if (itiFatherMobileNumber.isValidNumber()) {
            validMsgMobileNumber.classList.remove("hide");
        } else {
            inputMobileNumber.classList.add("error");
            var errorCode = itiFatherMobileNumber.getValidationError();
            if (errorCode == undefined) { errorCode = 0; }
            errorMsgMobileNumber.innerHTML = errorMap[errorCode];
            errorMsgMobileNumber.classList.remove("hide");
        }
    }
});

// on keyup / change flag: reset
inputMobileNumber.addEventListener('change', reset);
inputMobileNumber.addEventListener('keyup', reset);

/* Father Mobile Number End */

/*--------------------------------------------------------------------------------------------------------------------------------*/



/* Father Mobile Number2 Start */
var inputMobileNumber2 = document.querySelector("#FatherMobileNumberTwo"),
    errorMsgMobileNumber2 = document.querySelector(".error-msg-FatherMobileNumberTwo"),
    validMsgMobileNumber2 = document.querySelector(".valid-msg-FatherMobileNumberTwo");

// initialise plugin
var itiFatherMobileNumber2 = window.intlTelInput(inputMobileNumber2, {
    utilsScript: "../Design_v02/PhoneValid/js/utils.js"
});

var reset = function () {
    inputMobileNumber2.classList.remove("error");
    errorMsgMobileNumber2.innerHTML = "";
    errorMsgMobileNumber2.classList.add("hide");
    validMsgMobileNumber2.classList.add("hide");
};

// on blur: validate
inputMobileNumber2.addEventListener('blur', function () {
    reset();
    if (inputMobileNumber2.value.trim()) {
        if (itiFatherMobileNumber2.isValidNumber()) {
            validMsgMobileNumber2.classList.remove("hide");
        } else {
            inputMobileNumber.classList.add("error");
            var errorCode = itiFatherMobileNumber2.getValidationError();
            if (errorCode == undefined) { errorCode = 0; }
            errorMsgMobileNumber2.innerHTML = errorMap[errorCode];
            errorMsgMobileNumber2.classList.remove("hide");
        }
    }
});

// on keyup / change flag: reset
inputMobileNumber2.addEventListener('change', reset);
inputMobileNumber2.addEventListener('keyup', reset);

/* Father Mobile Number End */

/*--------------------------------------------------------------------------------------------------------------------------------*/

/* Mother Mobile Number Start */
var inputMother = document.querySelector("#MotherMobileNumber"),
    errorMsgMother = document.querySelector(".error-msg-MotherMobileNumber"),
    validMsgMother = document.querySelector(".valid-msg-MotherMobileNumber");

// initialise plugin
var iti = window.intlTelInput(inputMother, {
    utilsScript: "../Design_v02/PhoneValid/js/utils.js"
});

var reset = function () {
    inputMother.classList.remove("error");
    errorMsgMother.innerHTML = "";
    errorMsgMother.classList.add("hide");
    validMsgMother.classList.add("hide");
};

// on blur: validate
inputMother.addEventListener('blur', function () {
    reset();
    if (inputMother.value.trim()) {
        if (iti.isValidNumber()) {
            validMsgMother.classList.remove("hide");
        } else {
            inputMother.classList.add("error");
            var errorCode = iti.getValidationError();
            errorMsgMother.innerHTML = errorMap[errorCode];
            errorMsgMother.classList.remove("hide");
        }
    }
});

// on keyup / change flag: reset
inputMother.addEventListener('change', reset);
inputMother.addEventListener('keyup', reset);

/* Mother Mobile Number End */

/*--------------------------------------------------------------------------------------------------------------------------------*/


///* Test Mobile Number Start */
//var inputTest = document.querySelector("#testNumber"),
//    errorMsgTest = document.querySelector(".error-msg-testNumber"),
//    validMsgTest = document.querySelector(".valid-msg-testNumber");

//// initialise plugin
//var iti = window.intlTelInput(inputTest, {
//    utilsScript: "../Design_v02/PhoneValid/js/utils.js"
//});

//var reset = function () {
//    inputTest.classList.remove("error");
//    errorMsgTest.innerHTML = "";
//    errorMsgTest.classList.add("hide");
//    validMsgTest.classList.add("hide");
//};

//// on blur: validate
//inputTest.addEventListener('blur', function () {
//    reset();
//    if (inputTest.value.trim()) {
//        if (iti.isValidNumber()) {
//            validMsgTest.classList.remove("hide");
//        } else {
//            inputTest.classList.add("error");
//            var errorCode = iti.getValidationError();
//            errorMsgTest.innerHTML = errorMap[errorCode];
//            errorMsgTest.classList.remove("hide");
//        }
//    }
//});

//// on keyup / change flag: reset
//inputTest.addEventListener('change', reset);
//inputTest.addEventListener('keyup', reset);

///* Test Mobile Number End */

///*--------------------------------------------------------------------------------------------------------------------------------*/

