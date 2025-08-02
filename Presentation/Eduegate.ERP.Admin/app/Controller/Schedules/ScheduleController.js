app.controller("ScheduleController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("ScheduleController Loaded");

    //Initializing the product price
    $scope.init = function (model, windowname) {
        $http({
            method: 'Get', url: 'Payroll/Employee/GetEmployeesByRoleID?roleID=6'
        })
        .then(function (result) {
            $scope.Model = result.data;               
        });

        $timeout(function () {
            const containers = document.querySelectorAll('.timetable');

            if (containers.length === 0) {
                return false;
            }

            $(".timetablesubjects li").draggable();
            $(".schedulerTable .scaleWrap").droppable({
                drop: function (event, ui) {
                    $(this)
                        .addClass("ui-state-highlight")
                        .find("p")
                        .html("Dropped!");
                }
            });

            $(".resizable").resizable({
                resize: function (event, ui) {
                    ui.element.css('height', 'auto');
                }
            });
        });
    };
}]);

