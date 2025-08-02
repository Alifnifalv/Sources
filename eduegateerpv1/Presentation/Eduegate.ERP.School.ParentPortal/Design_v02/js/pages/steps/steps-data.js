
var Classes = null;
var Nationalities = null;
var AgeCriteriaData = null;

var wizard = $("#wizard").steps();
 
// Add step
//wizard.steps("add", {
//    title: "HTML code", 
//    content: "<strong>HTML code</strong>"
//});

var siblingForm = $("#StudSibApplication").show();
var guestForm = $("#StudApplication").show();

siblingForm.steps({
    headerTag: "h3",
    bodyTag: "fieldset",
    transitionEffect: "slideLeft",
    onStepChanging: function (event, currentIndex, newIndex)
    {
        // Allways allow previous action even if the current form is not valid!
        if (currentIndex > newIndex)
        {
            return true;
        }
        // Forbid next action on "Warning" step if the user is to young
        if (newIndex === 3 && Number($("#age-2").val()) < 18)
        {
            return false;
        }
        // Needed in some cases if the user went back (clean up)
        if (currentIndex < newIndex)
        {
            // To remove error styles
            siblingForm.find(".body:eq(" + newIndex + ") label.error").remove();
            siblingForm.find(".body:eq(" + newIndex + ") .error").removeClass("error");
        }
        siblingForm.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",  // validate all fields including form hidden input
            messages: {
                select_multi: {
                    maxlength: jQuery.validator.format("Max {0} items allowed for selection"),
                    minlength: jQuery.validator.format("At least {0} items must be selected")
                }
            },
            rules: {
                FirstName: {
                    minlength: 3,
                    maxlength: 10,
                    required: false
                },
                name: {
                    minlength: 2,
                    maxlength: 10,
                    required: true
                },

                email: {
                    required: true,
                    email: true
                },
                url: {
                    required: true,
                    url: true
                },
                number: {
                    required: true,
                    number: true
                },
                digits: {
                    required: true,
                    digits: true
                },
                creditcard: {
                    required: true,
                    creditcard: true
                },
                occupation: {
                    minlength: 5,
                },
                select: {
                    required: true
                },

            },

            invalidHandler: function (event, validator) { //display error alert on form submit              
                success1.hide();
                error1.show();
                App.scrollTo(error1, -200);
            },

            errorPlacement: function (error, element) { // render error placement for each input type
                var cont = $(element).parent('.input-group');
                if (cont) {
                    cont.after(error);
                } else {
                    element.after(error);
                }
            },

            highlight: function (element) { // hightlight error inputs
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error'); // set error class to the control group
            },

            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },

            submitHandler: function (siblingForm) {
                success1.show();
                error1.hide();
            }
        })
            .settings.ignore = ":disabled,:hidden";
        if (siblingForm.valid() == true) {
            var isValideDefultCheck = ValidationByTab(currentIndex);
            return isValideDefultCheck;
        }
        else {
            return siblingForm.valid();
        }

        return siblingForm.valid();
    },
    onStepChanged: function (event, currentIndex, priorIndex)
    {
        // Used to skip the "Warning" step if the user is old enough.
        if (currentIndex === 2 && Number($("#age-2").val()) >= 18)
        {
            siblingForm.steps("next");
        }
        // Used to skip the "Warning" step if the user is old enough and wants to the previous step.
        if (currentIndex === 2 && priorIndex === 3)
        {
            siblingForm.steps("previous");
        }
    },
    onFinishing: function (event, currentIndex) {
        if (document.getElementById("TermsAndConditions").checked == false) {
            $("#TermsAndConditions").focus();
            $(this).prop("disabled", false);
            callToasterPlugin('error', "Please Read SCHOOL FEE POLICY");
            return false;
        }
        siblingForm.validate().settings.ignore = ":disabled";
        return siblingForm.valid();
    },
    onFinished: function (event, currentIndex)
    {
        if ($('#IsStudentStudiedBefore').prop("checked") == true) {

            if ($("#PreviousSchoolName").val() == null || $("#PreviousSchoolName").val() == "") {
                $("#PreviousSchoolName").focus();
                $(this).prop("disabled", false);
                return false;
            }

            else if ($("#PreviousSchoolAcademicYear").val() == null || $("#PreviousSchoolAcademicYear").val() == "") {
                $("#PreviousSchoolAcademicYear").focus();
                $(this).prop("disabled", false);
                return false;
            }
            else if ($("#PreviousSchoolAcademicYear").val().length > 10) {
                $("#PreviousSchoolAcademicYear").focus();
                $(this).prop("disabled", false);
                return false;
            }
            else if ($("#PreviousSchoolSyllabus").val() == null || $("#PreviousSchoolSyllabus").val() == "" || $("#PreviousSchoolSyllabus").val() == '?') {
                $("#PreviousSchoolSyllabus").focus();
                $(this).prop("disabled", false);
                return false;
            }

            else if ($("#PreviousSchoolClassClassKey").val() == null || $("#PreviousSchoolClassClassKey").val() == "" || $("#PreviousSchoolClassClassKey").val() == '?') {
                $("#PreviousSchoolClassClassKey").focus();
                $(this).prop("disabled", false);
                return false;
            }

            else if ($("#PreviousSchoolAddress").val() == null || $("#PreviousSchoolAddress").val() == "") {
                $("#PreviousSchoolAddress").focus();
                $(this).prop("disabled", false);
                return false;
            }
            else {
                $(".PageLoaderBG_v02").show();
                $('#StudSibApplication').submit();
            }

        }
        else {
            $(".PageLoaderBG_v02").show();
            $("#StudSibApplication").submit();
        }
    }
}).validate({
    errorPlacement: function errorPlacement(error, element) {
        element.before(error);
        
    },
    rules: {
        confirm: {
            equalTo: "#password-2"
        }
    }
});

guestForm.steps({
    headerTag: "h3",
    bodyTag: "fieldset",
    transitionEffect: "slideLeft",
    onStepChanging: function (event, currentIndex, newIndex) {
        
        // Allways allow previous action even if the current form is not valid!
        if (currentIndex > newIndex) {
            return true;
        }
        // Forbid next action on "Warning" step if the user is to young
        if (newIndex === 3 && Number($("#age-2").val()) < 18) {
            return false;
        }
        // Needed in some cases if the user went back (clean up)
        if (currentIndex < newIndex) {
            // To remove error styles
            guestForm.find(".body:eq(" + newIndex + ") label.error").remove();
            guestForm.find(".body:eq(" + newIndex + ") .error").removeClass("error");
        }
        guestForm.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",  // validate all fields including form hidden input
            messages: {
                select_multi: {
                    maxlength: jQuery.validator.format("Max {0} items allowed for selection"),
                    minlength: jQuery.validator.format("At least {0} items must be selected")
                }
            },
            rules: {
                FirstName: {
                    minlength: 3,
                    maxlength: 10,
                    required: true
                },
                name: {
                    minlength: 2,
                    maxlength: 10,
                    required: true
                },

                email: {
                    required: true,
                    email: true
                },
                url: {
                    required: true,
                    url: true
                },
                number: {
                    required: true,
                    number: true
                },
                digits: {
                    required: true,
                    digits: true
                },
                creditcard: {
                    required: true,
                    creditcard: true
                },
                occupation: {
                    minlength: 5,
                },
                select: {
                    required: true
                },

            },

            invalidHandler: function (event, validator) { //display error alert on form submit              
                success1.hide();
                error1.show();
                App.scrollTo(error1, -200);
            },

            errorPlacement: function (error, element) { // render error placement for each input type
                
                var cont = $(element).parent('.input-group');
                if (cont) {
                    cont.after(error);
                } else {
                    element.after(error);
                }
            },

            highlight: function (element) { // hightlight error inputs
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error'); // set error class to the control group
            },

            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set error class to the control group
            },

            success: function (label) {
                
                label.closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },

            submitHandler: function (guestForm) {
                success1.show();
                error1.hide();
            }
        })
            .settings.ignore = ":disabled,:hidden";
        if (guestForm.valid() == true) {
            var isValideDefultCheck = ValidationByTab(currentIndex);
            return isValideDefultCheck;
        }
        else {
            return guestForm.valid();
        }
        
    },
    onStepChanged: function (event, currentIndex, priorIndex) {
        
        // Used to skip the "Warning" step if the user is old enough.
        if (currentIndex === 2 && Number($("#age-2").val()) >= 18) {
            guestForm.steps("next");
        }
        // Used to skip the "Warning" step if the user is old enough and wants to the previous step.
        if (currentIndex === 2 && priorIndex === 3) {
            guestForm.steps("previous");
        }
    },
    onFinishing: function (event, currentIndex) {
        if (document.getElementById("TermsAndConditions").checked == false) {
            $("#TermsAndConditions").focus();
            $(this).prop("disabled", false);
            callToasterPlugin('error', "Please Read SCHOOL FEE POLICY");
            return false;
        }
        guestForm.validate().settings.ignore = ":disabled";
        return guestForm.valid();
    },
    onFinished: function (event, currentIndex) {
        if ($('#IsStudentStudiedBefore').prop("checked") == true) {

            if ($("#PreviousSchoolName").val() == null || $("#PreviousSchoolName").val() == "") {
                $("#PreviousSchoolName").focus();
                $(this).prop("disabled", false);
                return false;
            }

            else if ($("#PreviousSchoolAcademicYear").val() == null || $("#PreviousSchoolAcademicYear").val() == "") {
                $("#PreviousSchoolAcademicYear").focus();
                $(this).prop("disabled", false);
                return false;
            }
            else if ($("#PreviousSchoolAcademicYear").val().length > 10) {
                $("#PreviousSchoolAcademicYear").focus();
                $(this).prop("disabled", false);
                return false;
            }
            else if ($("#PreviousSchoolSyllabus").val() == null || $("#PreviousSchoolSyllabus").val() == "" || $("#PreviousSchoolSyllabus").val() == '?') {
                $("#PreviousSchoolSyllabus").focus();
                $(this).prop("disabled", false);
                return false;
            }

            else if ($("#PreviousSchoolClassClassKey").val() == null || $("#PreviousSchoolClassClassKey").val() == "" || $("#PreviousSchoolClassClassKey").val() == '?') {
                $("#PreviousSchoolClassClassKey").focus();
                $(this).prop("disabled", false);
                return false;
            }

            else if ($("#PreviousSchoolAddress").val() == null || $("#PreviousSchoolAddress").val() == "") {
                $("#PreviousSchoolAddress").focus();
                $(this).prop("disabled", false);
                return false;
            }
            else {
                $(".PageLoaderBG_v02").show();
                $('#StudApplication').submit();
            }

        }
        else {
            $(".PageLoaderBG_v02").show();
            $("#StudApplication").submit();
        }
    }
}).validate({
    errorPlacement: function errorPlacement(error, element) { element.before(error); },
    rules: {
        confirm: {
            equalTo: "#password-2"
        }
    }
});

function onDateOfBirthChange() {
    AgeCriteriaData = null;
    var academicYearID = $("#SchoolAcademicyear").val();
    var schoolID = $("#School").val();
    var classID = $("#ClassKey").val();
    var dateOfBirth = $("#DateOfBirthString").val();
    var dobString = moment(dateOfBirth).format('DD/MM/YYYY');

    $.ajax({
        type: "GET",
        data: { classID: classID, academicID: academicYearID, schoolID: schoolID, dobString: dobString },
        url: utility.myHost + "Home/GetAgeCriteriaDetails",
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            AgeCriteriaData = result;

            if (AgeCriteriaData) {
                callToasterPlugin('error', AgeCriteriaData);
                //return true;
            }
            else {
                //return false;
            }
            return AgeCriteriaData;

        },
        error: function () {

        },
        complete: function (result) {
        }
    });
}

function ValidationByTab(index) {

    if (Classes == null) {
        var schoolID = $("#School").val();
        $.ajax({
            type: "GET",
            data: { schoolID: schoolID },
            url: utility.myHost + "/Home/GetClassesBySchool?schoolID",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                Classes = result.Response;
            }
        });
    }

    if (Nationalities == null) {
        $.ajax({
            type: 'GET',
            url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=Nationality&defaultBlank=false",
            success: function (result) {
                Nationalities = result;
            }
        });
    }

    if (index == 1) {

        var filterClass = Classes.find(x => x.Key == $("#ClassKey").val());
        var filterNationality = Nationalities.find(x => x.Key == $("#Nationality").val());

        //trim the Classname with school to the Classname only
        var trimdClassName = filterClass.Value.substring(0, filterClass.Value.indexOf(' -'));
            //filterClass.Value
            //.replace(" - Meshaf", "")
            //.replace(" - Westbay", "")
            //.replace(" - Thumama.", "");


        //if (filterNationality.Value == 'Indian' && ($("#AdhaarCardNo").val() == null || $("#AdhaarCardNo").val() == "")) {
        //    $("#AdhaarCardNo").focus();
        //    $("#AdhaarCardNo").addClass("error");
        //    callToasterPlugin('error', 'Please Enter Adhar No');
        //    return false;
        //}
        //else if (filterNationality.Value == 'Indian' && $("#AdhaarCardNo").val().length != 12) {
        //    $("#AdhaarCardNo").focus();
        //    $("#AdhaarCardNo").addClass("error");
        //    callToasterPlugin('error', 'Please Enter Valide Adhar No');
        //    return false;
        

        if (AgeCriteriaData) {
            $("#DateOfBirthString").focus();
            $("#DateOfBirthString").addClass("error");
            callToasterPlugin('error', AgeCriteriaData);
            return false;
        }
        else if ($("#StudentNationalID").val() == null || $("#StudentNationalID").val() == "") {
            $("#StudentNationalID").focus();
            $("#StudentNationalID").addClass("error");
            callToasterPlugin('error', 'Enter Student National ID');
            return false;
        }
        else if ($("#StudentNationalID").val().length!=11) {
            $("#StudentNationalID").focus();
            $("#StudentNationalID").addClass("error");
            callToasterPlugin('error', 'Please Enter Valide Student National ID');
            return false;
        }
        //else if ($("#StudentNationalIDNoIssueDateString").val() == null || $("#StudentNationalIDNoIssueDateString").val()=="") {
        //    $("#StudentNationalIDNoIssueDateString").focus();
        //    $("#StudentNationalIDNoIssueDateString").addClass("error");
        //    callToasterPlugin('error', 'Please Enter National ID Issue Date');
        //    return false;
        //}
        //else if ($("#StudentNationalIDNoExpiryDateString").val().length == null || $("#StudentNationalIDNoExpiryDateString").val()=="") {
        //    $("#StudentNationalIDNoExpiryDateString").focus();
        //    $("#StudentNationalIDNoExpiryDateString").addClass("error");
        //    callToasterPlugin('error', 'Please Enter National ID Expiry Date');
        //    return false;
        //}
        else if ($("#SecoundLanguageString").val() == null || $("#SecoundLanguageString").val() == "") {
            if (trimdClassName.match(/Class KG*/)) {
                return true;
            }
            else {
                $("#SecoundLanguageString").focus();
                $("#SecoundLanguageString").addClass("error");
                callToasterPlugin('error', 'Please Select Second Language');
                return false;
            }
        }
        else if ($("#ThridLanguageString").val() == null || $("#ThridLanguageString").val() == "")
        {
            if (trimdClassName == 'Class 1' || trimdClassName == 'Class 2' || trimdClassName == 'Class 3' || trimdClassName == 'Class 4' || trimdClassName == 'Class 9' || trimdClassName == 'Class 10' || trimdClassName.match(/Class KG*/)) {
                return true;
            }
            else {
                $("#ThridLanguageString").focus();
                $("#ThridLanguageString").addClass("error");
                callToasterPlugin('error', 'Please Select Third Language');
                return false;
            }
        }
        else {
            return true;
        }
    }
    else if (index == 3) {
        if ($("#FatherNationalID").val() == null || $("#FatherNationalID").val() == "") {
            $("#FatherNationalID").focus();
            $("#FatherNationalID").addClass("error");
            callToasterPlugin('error', 'Enter Father National ID');
            return false;
        }
        else if ($("#FatherNationalID").val().length != 11) {
            $("#FatherNationalID").focus();
            $("#FatherNationalID").addClass("error");
            callToasterPlugin('error', 'Please Enter Valide Father National ID');
            return false;
        }
        //else if ($("#FatherNationalDNoIssueDateString").val() == null || $("#FatherNationalDNoIssueDateString").val()=="") {
        //    $("#FatherNationalDNoIssueDateString").focus();
        //    $("#FatherNationalDNoIssueDateString").addClass("error");
        //    callToasterPlugin('error', 'Please Enter Father National ID Issue Date');
        //    return false;
        //}
        //else if ($("#FatherNationalDNoExpiryDateString").val().length == null || $("#FatherNationalDNoExpiryDateString").val()=="") {
        //    $("#FatherNationalDNoExpiryDateString").focus();
        //    $("#FatherNationalDNoExpiryDateString").addClass("error");
        //    callToasterPlugin('error', 'Please Enter Father National ID Expiry Date');
        //    return false;
        //}
        else {
            return true;
        }
    }
    else if (index == 3) {
        if ($("#MotherNationalID").val() == null || $("#MotherNationalID").val() == "") {
            $("#MotherNationalID").focus();
            $("#MotherNationalID").addClass("error");
            callToasterPlugin('error', 'Enter Mother National ID');
            return false;
        }
        else if ($("#MotherNationalID").val().length != 11) {
            $("#MotherNationalID").focus();
            $("#MotherNationalID").addClass("error");
            callToasterPlugin('error', 'Please Enter Valide Mother National ID');
            return false;
        }
        //else if ($("#MotherNationalDNoIssueDateString").val() == null || $("#MotherNationalDNoIssueDateString").val()=="") {
        //    $("#MotherNationalDNoIssueDateString").focus();
        //    $("#MotherNationalDNoIssueDateString").addClass("error");
        //    callToasterPlugin('error', 'Please Enter Mother National ID Issue Date');
        //    return false;
        //}
        //else if ($("#MotherNationalDNoIssueDateString").val().length == null || $("#MotherNationalDNoIssueDateString").val()=="") {
        //    $("#MotherNationalDNoIssueDateString").focus();
        //    $("#MotherNationalDNoIssueDateString").addClass("error");
        //    callToasterPlugin('error', 'Please Enter Mother National ID Expiry Date');
        //    return false;
        //}
        else {
            return true;
        }
    }
    else {
        return true;
    }
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
$("#example-vertical").steps({
    headerTag: "h3",
    bodyTag: "section",
    transitionEffect: "slideLeft",
    stepsOrientation: "vertical"
});