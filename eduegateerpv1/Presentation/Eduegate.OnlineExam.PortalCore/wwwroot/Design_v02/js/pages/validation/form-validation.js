/**
 *  Document   : form-validation.js
 *  Author     : redstar
 *  Description: Script related to validate the form
 *
 **/
var FormValidation = function () {

    // basic validation
    var handleValidation1 = function() {
        // for more info visit the official plugin documentation: 
            // http://docs.jquery.com/Plugins/Validation

        var form1 = $('#form_sample_1'); var StudApplication = $("#StudApplication");

            form1.validate({
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
                        maxlength:10,
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

                submitHandler: function (form) {
                    success1.show();
                    error1.hide();
                }
            });

      
    }

    // validation using icons
    var handleValidation2 = function() {
        // for more info visit the official plugin documentation: 
            // http://docs.jquery.com/Plugins/Validation

        var form2 = $('#form_sample_2');
            var error2 = $('.alert-danger', form2);
            var success2 = $('.alert-success', form2);
            form2.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",  // validate all fields including form hidden input
                rules: {
                    name: {
                        minlength: 2,
                        required: true
                    },
                    email: {
                        required: true,
                        email: true
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
                    creditcard: {
                        required: true,
                        creditcard: true
                    },
                },

                invalidHandler: function (event, validator) { //display error alert on form submit              
                    success2.hide();
                    error2.show();
                    App.scrollTo(error2, -200);
                },

                errorPlacement: function (error, element) { // render error placement for each input type
                    var icon = $(element).parent('.input-icon').children('i');
                    icon.removeClass('fa-check').addClass("fa-warning");  
                    icon.attr("data-original-title", error.text()).tooltip({'container': 'body'});
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').removeClass("has-success").addClass('has-error'); // set error class to the control group   
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    
                },

                success: function (label, element) {
                    var icon = $(element).parent('.input-icon').children('i');
                    $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
                    icon.removeClass("fa-warning").addClass("fa-check");
                },

                submitHandler: function (form) {
                    success2.show();
                    error2.hide();
                    form[0].submit(); // submit the form
                }
            });

        //leaveApplication validation
        //var formleaveApplication = $('#leaveApplication');
        //var errorleaveApplication = $('.alert-danger', form2);
        //var successleaveApplication = $('.alert-success', form2);
        //formleaveApplication.validate({
        //    errorElement: 'span', //default input error message container
        //    errorClass: 'help-block help-block-error', // default input error message class
        //    focusInvalid: false, // do not focus the last invalid input
        //    ignore: "",  // validate all fields including form hidden input
        //    rules: {
        //        name: {
        //            minlength: 2,
        //            required: true
        //        },
        //        email: {
        //            required: true,
        //            email: true
        //        },
        //        email: {
        //            required: true,
        //            email: true
        //        },
        //        url: {
        //            required: true,
        //            url: true
        //        },
        //        number: {
        //            required: true,
        //            number: true
        //        },
        //        creditcard: {
        //            required: true,
        //            creditcard: true
        //        },
        //    },

        //    invalidHandler: function (event, validator) { //display error alert on form submit              
        //        successleaveApplication.hide();
        //        errorleaveApplication.show();
        //        App.scrollTo(errorleaveApplication, -200);
        //    },

        //    errorPlacement: function (error, element) { // render error placement for each input type
        //        var icon = $(element).parent('.input-icon').children('i');
        //        icon.removeClass('fa-check').addClass("fa-warning");
        //        icon.attr("data-original-title", error.text()).tooltip({ 'container': 'body' });
        //    },

        //    highlight: function (element) { // hightlight error inputs
        //        $(element)
        //            .closest('.form-group').removeClass("has-success").addClass('has-error'); // set error class to the control group   
        //    },

        //    unhighlight: function (element) { // revert the change done by hightlight

        //    },

        //    success: function (label, element) {
        //        var icon = $(element).parent('.input-icon').children('i');
        //        $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
        //        icon.removeClass("fa-warning").addClass("fa-check");
        //    },

        //    submitHandler: function (form) {
        //        successleaveApplication.show();
        //        errorleaveApplication.hide();
        //        form[0].submit(); // submit the form
        //    }
        //});
        //end leaveApplication validation


        //transferApplication validation
        var formtransferApplication = $('#transferApplication');
        var errortransferApplication = $('.alert-danger', form2);
        var successtransferApplication = $('.alert-success', form2);
        formtransferApplication.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",  // validate all fields including form hidden input
            rules: {
                name: {
                    minlength: 2,
                    required: true
                },
                email: {
                    required: true,
                    email: true
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
                creditcard: {
                    required: true,
                    creditcard: true
                },
            },

            invalidHandler: function (event, validator) { //display error alert on form submit              
                successtransferApplication.hide();
                errortransferApplication.show();
                App.scrollTo(errortransferApplication, -200);
            },

            errorPlacement: function (error, element) { // render error placement for each input type
                var icon = $(element).parent('.input-icon').children('i');
                icon.removeClass('fa-check').addClass("fa-warning");
                icon.attr("data-original-title", error.text()).tooltip({ 'container': 'body' });
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').removeClass("has-success").addClass('has-error'); // set error class to the control group   
            },

            unhighlight: function (element) { // revert the change done by hightlight

            },

            success: function (label, element) {
                var icon = $(element).parent('.input-icon').children('i');
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
                icon.removeClass("fa-warning").addClass("fa-check");
            },

            submitHandler: function (form) {
                successtransferApplication.show();
                errortransferApplication.hide();
                form[0].submit(); // submit the form
            }
        });
        //end transferApplication validation


        //Compose Mail
        var formComposeMailCommon = $('#ComposeMailCommon');
        var errorComposeMailCommon = $('.alert-danger', form2);
        var successComposeMailCommon = $('.alert-success', form2);
        formComposeMailCommon.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",  // validate all fields including form hidden input
            rules: {
                name: {
                    minlength: 2,
                    required: true
                },
                email: {
                    required: true,
                    email: true
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
                creditcard: {
                    required: true,
                    creditcard: true
                },
            },

            invalidHandler: function (event, validator) { //display error alert on form submit              
                successComposeMailCommon.hide();
                errorComposeMailCommon.show();
                App.scrollTo(errorComposeMailCommon, -200);
            },

            errorPlacement: function (error, element) { // render error placement for each input type
                var icon = $(element).parent('.input-icon').children('i');
                icon.removeClass('fa-check').addClass("fa-warning");
                icon.attr("data-original-title", error.text()).tooltip({ 'container': 'body' });
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').removeClass("has-success").addClass('has-error'); // set error class to the control group   
            },

            unhighlight: function (element) { // revert the change done by hightlight

            },

            success: function (label, element) {
                var icon = $(element).parent('.input-icon').children('i');
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
                icon.removeClass("fa-warning").addClass("fa-check");
            },

            submitHandler: function (form) {
                successComposeMailCommon.show();
                errorComposeMailCommon.hide();
                form[0].submit(); // submit the form
            }
        });
        //End Compose Mail 
    }

    

    return {
        //main function to initiate the module
        init: function () {

            handleValidation1();
            handleValidation2();

        }

    };

}();

jQuery(document).ready(function() {
	'use strict';
    FormValidation.init();
});