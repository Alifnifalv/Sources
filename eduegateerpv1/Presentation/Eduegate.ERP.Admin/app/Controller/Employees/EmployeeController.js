app.controller("EmployeeController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.RelegionChanges = function ($event, $element, employeeModel) {
        if (employeeModel.Relegion == null || employeeModel.Relegion == "") return false;
        showOverlay();
        var model = employeeModel;
        model.Cast = null;
        var url = "Schools/School/GetCastByRelegion?relegionID=" + model.Relegion;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Cast = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.LeaveGroupChanges = function ($event, $element, employeeModel)
    {
        if (employeeModel.LeaveGroup == null || employeeModel.LeaveGroup == "") return false;
        showOverlay();
        var model = employeeModel;

        if (model.IsOverrideLeaveGroup == false) {

            employeeModel.IsListDisable = false;
        }
        else {
            employeeModel.IsListDisable = true;
            var url = "Payroll/Employee/LeaveGroupChanges?leaveGroupID=" + model.LeaveGroup;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    employeeModel.LeaveTypes = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
           
        }
        
    };

    $scope.CalendarTypeChanges = function ($event, $element, payrollModel) {
        if (payrollModel.CalendarType == null || payrollModel.CalendarType == "") return false;
        showOverlay();
        var model = payrollModel;
        model.AcademicCalendar = null;
        var url = "Schools/School/GetCalendarByTypeID?calendarTypeID=" + model.CalendarType;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.CalendarMasters = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    //$scope.ResetProfile = function ($event, $element, employeeModel) {
    //    var model = employeeModel;
    //    dbContext.employeeModel.Remove(ResetProfile);

    //    dbContext.SaveChanges();
    //    return ResetProfile;
    //};

    $scope.getAgeByDOB = function ($event, $element, employeeModel) {
        var userinput = employeeModel.BirthDateString;
        date = Date.parse(userinput).toString("MM/dd/yyyy");
        var dob = new Date(date);
        if (userinput == null || userinput == '') {
            document.getElementById("message").innerHTML = "**Choose a date please!";
            return false;
        } else {

            //calculate month difference from current date in time  
            var month_diff = Date.now() - dob.getTime();

            //convert the calculated difference in date format  
            var age_dt = new Date(month_diff);

            //extract year from date      
            var year = age_dt.getUTCFullYear();

            //now calculate the age of the user  
            var age = Math.abs(year - 1970);

            employeeModel.Age = age;

        }
    }


    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);