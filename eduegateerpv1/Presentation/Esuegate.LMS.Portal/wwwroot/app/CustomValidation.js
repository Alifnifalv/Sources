//custom validation rule for checking max hours is greater than sum of RT,OT and DT Hours
//checking min hours is greater than the sum of RT,OT and DT hours
//Param value is L.H.S and value(componen value) is R.H.S based on this we need to pass comparator
$.validator.addMethod("expressionvalidator",
    function (value, element, params) {

        var expressionValue = eval(params.evalexpression).toString();

        if (eval(params.evalconditionvalue) != undefined)
            value = eval(params.evalconditionvalue);

        var existingMessage = htmlUnescape($(element).attr('data-val-expressionvalidator').toString());

        if (existingMessage.indexOf("{")) // if really required for custom message
        {
            $.data(element.form, 'validator').settings.messages[element.id]["expressionvalidator"] = eval(existingMessage).toString();
        }

        // if the value is not a number, probably we can ignore
        if (expressionValue == "NaN" || value == "0")
            return true;

        if (eval(expressionValue + params.evaloperator + value))
            return true;
        else
            return false;

    }, '');

$.validator.unobtrusive.adapters.add('expressionvalidator', ['evalexpression', 'evaloperator', 'evalconditionvalue'], function (options) {
    options.rules["expressionvalidator"] = options.params;
    options.messages["expressionvalidator"] = options.message;
});

$.validator.unobtrusive.adapters.add("requiredif", ["other", "otherval"], function (options) {
    var value = {
        depends: function () {
            var element = $(options.form).find(":input[name='" + options.params.other + "']")[0];
            return element && $(element).val() == options.params.otherval;
        }
    }
    options.rules["required"] = value;
    options.messages["required"] = options.message;
});