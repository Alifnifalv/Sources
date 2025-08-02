app.controller('AppointmentController', ['$scope', '$timeout', '$log', 'weeklySchedulerLocaleService', '$http',
    function ($scope, $timeout, $log, scheduler, $http) {
        $scope.events = [];
        var ctrl = this;

        this.onEventSelected = function (args) {
            ctrl.selectedEvents = ctrl.scheduler.multiselect.events();
        };

        this.onRowSelected = function (args) {
            ctrl.selectedEvents = ctrl.scheduler.multiselect.events();
        };

        this.onEventMoved = function (args) {
            ctrl.scheduler.message("Event moved: " + args.e.text());
        };

        $scope.config = {
            scale: "CellDuration",
            cellDuration: 15,
            startDate: new Date() - 30,
            days: 10,
            timeHeaders: [
              { groupBy: "Day" },
              { groupBy: "Hour", format: "hh tt" },
              { groupBy: "Cell", format: "mm" },
            ],
            eventClickHandling: "Select",
            rowClickHandling: "Select",
            onTimeRangeSelected: function (args) {

                var name = prompt("New event name:", "Event");
                if (!name) return;
                $scope.$apply(function () {
                    $scope.config.events.push(
                      {
                          start: args.start,
                          end: args.end,
                          id: DayPilot.guid(),
                          resource: args.resource,
                          text: name,
                          bubbleHtml: "Details"
                      }
                    );
                });
                modal.showHtml("<center><br/><br/><input type='textbox'/><br><input type=button value=Apply></center>");


            },
            contextMenu: new DayPilot.Menu({
                items: [
                    { text: "Show event ID", onclick: function () { alert("Event value: " + this.source.value()); } },
                    { text: "Show event text", onclick: function () { alert("Event text: " + this.source.text()); } },
                    { text: "Show event start", onclick: function () { alert("Event start: " + this.source.start().toStringSortable()); } },
                    { text: "Delete", onclick: function () { $scope.dp.events.remove(this.source); } },
                    { text: "Disabled menu item", onclick: function () { alert("disabled") }, disabled: true }
                ]
            }),
            resources: [],
            events: [],
        };

        $http({ method: 'GET', url: 'Payroll/Employee/GetEmployees' })
                .then(function (result) {
                    $.each(result.data, function (index, data) {
                        $scope.config.resources.push({ name: data.Value, id: data.Key });
                    });
                });

        this.add = function () {
            $scope.config.events.push(
                    {
                        start: new DayPilot.Date("2014-09-05T00:00:00"),
                        end: new DayPilot.Date("2014-09-06T00:00:00"),
                        id: DayPilot.guid(),
                        resource: "B",
                        text: "One-Day Event",
                        bubbleHtml: "Details"
                    }
            );
        };

        this.move = function () {
            var event = $scope.config.events[0];
            if (event) {
                event.start = event.start.addDays(1);
                event.end = event.end.addDays(1);
            }
        };

        this.rename = function () {
            $scope.config.events[0].text = "New name";
        };

        this.scrollTo = function (date) {
            $scope.scheduler.scrollTo(date);
        };

        this.scale = function (val) {
            $scope.config.scale = val;
        };
    }]);