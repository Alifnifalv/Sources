app.directive('skienDatepicker', function () {
  return {
    restrict: 'A',
    require: 'ngModel',
    link: function (scope, elem, attrs, ngModel) {
      // HH:mm ->  24 hours format, hh:mm A -> 12 hrs
      //var splittedDateTime = _dateTimeFormat.split(' ')
      //var dateTimeFormat = splittedDateTime[0]
      setTimeout(function () {
          const isDarkTheme = $('html').attr('data-bs-theme') === 'dark';
          $(elem).datetimepicker({
              "allowInputToggle": true,
              "showClose": true,
              "showClear": true,
              theme: isDarkTheme ? 'dark' : 'default', 
              "showTodayButton": true,
              format: window._bootstrapDateFormat || "DD/MM/YYYY",
              timepicker: false,
              scrollInput: false,
              beforeShow: function () {
                  setTimeout(function () {
                      $('.ui-datepicker').css('z-index', 99);
                  }, 0);
              }
          })
       });

      $(elem).on('dp.change', function (e) {
        ngModel.$setViewValue($(e.target).data().date)
      })
    }
  }
})
