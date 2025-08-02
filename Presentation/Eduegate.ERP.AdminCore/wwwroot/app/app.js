var app = angular.module('Eduegate.ERP.Admin',
    ['ngRoute', 'ngSanitize', 'ui.select', 'ngMap', 'weeklyScheduler', 'weeklySchedulerI18N', 'daypilot',
        'mwl.calendar', 'ui.bootstrap', 'colorpicker.module', 'angularMoment', 'toaster', 'bc.Flickity']);


(function () {
    angular
        .module('Eduegate.ERP.Admin')
        .value('$subscription', $.connection);   
})();

//setting config for this module
app.config(['$routeProvider', '$locationProvider', 'weeklySchedulerLocaleServiceProvider',
function ($routeProvider, $locationProvider, localeServiceProvider) {
    $locationProvider.html5Mode(false);

    localeServiceProvider.configure({
        doys: { 'en': 1 },
        lang: { 'en': { month: 'Month', weekNb: 'Week', addNew: 'New Appointment' } },
        localeLocationPattern: '/Scripts/angular-i18n/angular-locale_{{locale}}.js'
    });
}]);

app.config(['$httpProvider', function ($httpProvider) {
    // Line break to avoid horizontal scroll. Put it all on one
    //  line in your real code.
    $httpProvider.defaults.headers
                 .common['X-Requested-With'] = 'XMLHttpRequest';
}]);

$(document).ready(function () {
    $(".overlaydiv").on("mouseup", function (e) {
        var l = $(e.target);
        if (l.closest('.popover').length === 0) {
            $(".popover").each(function () {
                $(this).popover("hide");
            });
        }
    });
});
