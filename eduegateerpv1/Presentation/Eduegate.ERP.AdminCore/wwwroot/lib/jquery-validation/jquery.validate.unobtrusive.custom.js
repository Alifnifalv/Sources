/* #region jQuery.validate.unobtrusive.custom */

jQuery.validator.addMethod("isrequired", function (value, element, params) {
    
    if ($(element).val() !== '') return true

    var $other = $('#' + params.other);

    var otherVal = ($other.attr('type').toUpperCase() === "CHECKBOX") ?
             ($other.attr("checked") ? "true" : "false") : $other.val();

    return otherVal
});

jQuery.validator.unobtrusive.adapters.add("isrequired", ["other"], function (options) {

    var $other = $('#' + options.params.other);

    if ($other && $other.length > 0) {
        options.rules['required'] = "#" + options.params.other + (($other.attr('type').toUpperCase() === "CHECKBOX") ?
            ":checked" : ":filled");
        options.messages['required'] = options.message;
    }
});



/* #endregion */