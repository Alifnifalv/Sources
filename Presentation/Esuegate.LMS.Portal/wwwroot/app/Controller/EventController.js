app.controller("EventController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("EventController  Loaded");




    $scope.Init = function (screenType, AssignmentID) {
        const defaultStudentID = localStorage.getItem('defaultStudentID');
   
        $scope.getAssignmentList(defaultStudentID);
        
    };
    $scope.getAssignmentList = function (studentId) {
        //showOverlay();
        $scope.AssignmentEvent = [];

        $.ajax({
            type: "GET",
            data: { studentID: studentId },
            url: utility.myHost + "Home/GetAssignmentStudentwise",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.AssignmentListBySubject = result.Response;
                        $scope.AssignmentListBySubject.forEach(function (assignment) {
                            const startDate = convertDateToISO(assignment.StartDateString);
                            const endDate =  null;

                            $scope.AssignmentEvent.push({
                                title: assignment.Title,
                                start: startDate, // Ensure the API provides these fields
                                end: endDate || null, // Optional end date
                                backgroundColor: "rgb(236, 236, 249)", // Custom styling
                                borderColor: "rgb(236, 236, 249)",
                                textColor: "#3B3B77",
                                className: "fw-bold fs-6 rounded-2 px-2"
                            });
                        });
                        $scope.getLessonPlans(studentId);

                    }
                });
            },
            error: function () {

            },
            complete: function (result) {
            }
        });
    }


    $scope.getLessonPlans = function (studentId) {
        $scope.LessonPlanEvent = []; // Clear existing lesson plan events

        $.ajax({
            type: "GET",
            data: { studentID: studentId },
            url: utility.myHost + "Home/GetLessonPlanList",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result.Response) {
                        result.Response.forEach(function (lessonPlan) {
                            const startDate = convertDateToISO(lessonPlan.StartDate);
                            const endDate =  null;

                            $scope.LessonPlanEvent.push({
                                title: lessonPlan.Title,
                                start: startDate,
                                end: endDate,
                                backgroundColor: "rgb(251 244 212)", // Green background
                                borderColor: "rgb(251 244 212)",    // Dark green border
                                textColor: "#60510D",  
                                className: "fw-bold fs-6 rounded-2 px-2"
                            });
                        });

                        // Combine both events and render the calendar
                        $scope.getCalender([...$scope.AssignmentEvent, ...$scope.LessonPlanEvent]);
                    }
                });
            },
            error: function () {
                console.error("Failed to fetch lesson plans.");
            }
        });
    };

    function convertDateToISO(dateStr) {
        const [day, month, year] = dateStr.split('/');
        return `${year}-${month}-${day}`; // Returns date in ISO format
    }

    $scope.getCalender = function (assignmentEvents) {
        const element = document.getElementById("calendar");

        var calendarEl = document.getElementById("calendar");
        var calendar = new FullCalendar.Calendar(calendarEl, {
            headerToolbar: {
                left: "prev,next",
                center: "title",
                right: ""
            },


            height: 700,
            // contentHeight: 780,
            aspectRatio: 3,

            nowIndicator: true,

            views: {
                dayGridMonth: { buttonText: "month" },
                timeGridWeek: { buttonText: "week" },
                timeGridDay: { buttonText: "day" }
            },

            initialView: "dayGridMonth",
            // initialDate:"2014-02-01",
            // initialDate:$scope.GetDateByMonth(),

            editable: false,
            dayMaxEvents: true, // allow "more" link when too many events
            navLinks: true,

            businessHours: [ // specify an array instead
                {
                    daysOfWeek: [0, 1, 2, 3, 4], //sunday , Monday, Tuesday, Wednesday , thursday
                    startTime: '07:00', // 7am
                    endTime: '18:00' // 6pm
                },
                {
                    daysOfWeek: [6], // saturday
                    startTime: '09:00', // 9am
                    endTime: '12:00' // 12pm
                }
            ],

            //events: [
            //    {
            //        title: "All Day Event All Day Event",
            //        start: "2025-01-21",
            //        backgroundColor: "rgb(236 236 249)",
            //        borderColor: "rgb(236 236 249)  ",    // Dark red border
            //        textColor: "#3B3B77",       // White text
            //        className: "fw-bold fs-6 rounded-2 px-2"
            //    },
            //    {
            //        title: "Long Event",
            //        start: "2025-01-19",
            //        end: "2025-01-20",
            //        backgroundColor: "rgb(251 244 212)", // Green background
            //        borderColor: "rgb(251 244 212)",    // Dark green border
            //        textColor: "#60510D",   // Black text
            //        className: "fw-bold fs-6 rounded-2 px-2"

            //    },
            //    {
            //        title: "Conference",
            //        start: "2025-01-10",
            //        end: "2025-01-11",
            //        backgroundColor: "#eddad3", // Blue background
            //        borderColor: "#eddad3",    // Dark blue border
            //        textColor: "#75221A",       // White text
            //        className: "fw-bold fs-6 rounded-2 px-2"

            //    },
            //    {
            //        title: "Lunch",
            //        start: "2025-01-11",
            //        backgroundColor: "rgb(217 119 87 / 20%)", // Orange background
            //        borderColor: "rgb(217 119 87 / 20%)",    // Dark orange border
            //        textColor: "#ffffff"       // White text
            //    },
            //    {
            //        title: "Happy Hour",
            //        start: "2023-03-10T17:30:00",
            //        end: "2023-03-10T19:30:00",
            //        backgroundColor: "#9900ff", // Purple background
            //        borderColor: "#660099",    // Dark purple border
            //        textColor: "#ffffff"       // White text
            //    },
            //    {
            //        title: "Dinner",
            //        start: "2023-03-10T20:00:00",
            //        backgroundColor: "#00ffff", // Cyan background
            //        borderColor: "#009999",    // Dark cyan border
            //        textColor: "#000000"       // Black text
            //    },
            //    {
            //        title: "Birthday Party",
            //        start: "2023-03-10T07:00:00",
            //        end: "2023-03-10T09:30:00",
            //        backgroundColor: "#ff66cc", // Pink background
            //        borderColor: "#cc3399",    // Dark pink border
            //        textColor: "#000000"       // Black text
            //    },
            //    {
            //        title: "Click for Google",
            //        url: "http://google.com/",
            //        start: "2023-03-10",
            //        backgroundColor: "#ffff00", // Yellow background
            //        borderColor: "#cccc00",    // Dark yellow border
            //        textColor: "#000000"       // Black text
            //    }
            //],

            events: assignmentEvents,

            resources: [{
                start: '2023-03-10',
                type: 'resourceTimeline',
                duration: { days: 4 }
            }],
            eventContent: function (info) {
                const element = $(info.el);

                if (info.event.extendedProps && info.event.extendedProps.description) {
                    if (element.hasClass("fc-day-grid-event")) {
                        element.data("content", info.event.extendedProps.description);
                        element.data("placement", "top");
                        KTApp.initPopover(element);
                    }
                }
            }
        });

        calendar.render();
    }
    $scope.Init();

}]);