app.controller("AcademicCalenderController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("AcademicCalenderController Loaded");

    $scope.AcademicCalendarData = [];
    $scope.Model = {};
    $scope.SelectedMonth = null;
    $scope.SelectedYear = null;
    $scope.CurrentDate = new Date();
    $scope.SelectedDay = new Date().getDate();
    $scope.SelectedYear = new Date().getFullYear();
    $scope.SelectedMonth = new Date().getMonth();
    $scope.SelectedAcademicCalendar = {};
    $scope.type = null;
    $scope.PopupMindate = "";
    $scope.PopupMaxdate = "";
    $scope.CalendarID = null;
    $scope.SelectedAcademicCalendarID = null;


    $scope.AcYearStartDate = null;
    $scope.AcYearEndDate = null;
    //$scope.AcademicYearCalendarStatus = null;
    $scope.AcademicYearCalendarEventType = [];

    $scope.MonthNames = [
        "January", "February", "March",
        "April", "May", "June",
        "July", "August", "September",
        "October", "November", "December"
    ];


    $scope.Days = [
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
        21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31
    ];

    $scope.Events = [];

    $scope.GetAvatarText = function (value) {
        var words = value.split(' ');
        if (words.length == 1) {
            return value.substr(0, 2).toUpperCase();
        }
        else {
            return words[0].substr(0, 1).toUpperCase() + words[1].substr(0, 1).toUpperCase();
        }
    }

    //Initializing the product price
    $scope.Init = function (model, windowname, type) {
        $scope.type = type;

        //$http({
        //    method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=AcademicYear&defaultBlank=false'
        //}).then(function (result) {
        //    $scope.AcademicYearData = result.data;
        //});


        //$http({
        //    method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=HolidayList&defaultBlank=false",
        //}).then(function (result) {
        //    $scope.AcademicYearCalendarStatus = result.data;
        //});

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=AcademicYearCalendarEventType&defaultBlank=false",
        }).then(function (result) {
            $scope.AcademicYearCalendarEventType = result.data;
        });

        if ($scope.type == 'Academic') {
            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=AcademicCalendarMaster&defaultBlank=false",
            }).then(function (result) {
                $scope.CalendarMasterData = result.data;
            });
        }
        else {
            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=NonAcademicCalendarMaster&defaultBlank=false",
            }).then(function (result) {
                $scope.CalendarMasterData = result.data;
            });
        }
    };


    angular.element(document).ready(function () {
        //To show popup with existing details for viewing and editing.
        //$(document).on('dblclick', '.seletedsubject', function (event) {

        //    //var itemVal = $(this).attr('data-Subject-id');
        //    //if (itemVal != null && itemVal > 0) {
        //    $scope.LoadPopup('.gridItemPopup', event);
        //    $scope.FillPopupFromSaved($(this));
        //    $(".gridItemOverlay").show();
        //    // }
        //});

        $(document).on('dblclick', '.colspan', function (event) {

            var academicCalendarID = $scope.SelectedAcademicCalendar?.Key;

            if (academicCalendarID == null || academicCalendarID == 0) {
                $().showGlobalMessage($root, $timeout, true, "Please Select Calendar!");
                $scope.AcademicCalendarData = null;
                return false;
            }
            var dateValidityMsg = checkValidDay($(this));
            if (dateValidityMsg == '') {
                $scope.FillPopupFromSaved($(this));
                $scope.LoadPopup('.gridItemPopup', event);
                $(".gridItemOverlay").show();
            } else {
                $().showGlobalMessage($root, $timeout, true, dateValidityMsg);
            }

        });

        function checkValidDay(currentelement) {
            var month = $(currentelement).parents(".monthName").attr("data-month-id");
            var day = $(currentelement).parents(".DayId").attr("data-Day-id");
            //var Year = $scope.SelectedYear;
            var selectedDate = new Date((parseInt(month) + 1).toString() + '/' + day.toString() + '/' + $scope.AcYearStartDate.getFullYear().toString());
            var selectedEndDate = new Date((parseInt(month) + 1).toString() + '/' + day.toString() + '/' + $scope.AcYearEndDate.getFullYear().toString());

            var msg = '';
            if ($scope.AcYearStartDate <= selectedDate && $scope.AcYearEndDate >= selectedDate) {
                msg = '';
                $scope.SelectedYear = $scope.AcYearStartDate.getFullYear();
            } else {
                if ($scope.AcYearStartDate <= selectedEndDate && $scope.AcYearEndDate >= selectedEndDate) {
                    msg = '';
                    $scope.SelectedYear = $scope.AcYearEndDate.getFullYear();
                } else {
                    msg = 'The selected date should belong to the selected acadamic period!';
                }
            }


            return msg;
        }


        $scope.DeleteEvent = function ($event) {
            var currentElement = $($event.currentTarget);
            var academicid = $($event.currentTarget).attr('data-academic-id');
            var AcademicEventid = $($event.currentTarget).attr('data-AcademicEvent-id');
            if (academicid != null && academicid > 0 && AcademicEventid != null && AcademicEventid > 0) {
                if (window.confirm("Do you want to delete event?") == false) {
                    return;
                }
                showOverlay();
                $.ajax({
                    url: utility.myHost + "Schools/Calender/DeleteAcademicCalendarEvent",
                    type: "POST",
                    data: { "academicYearCalendarEventIID": AcademicEventid, "academicYearCalendarID": academicid },
                    success: function (result) {
                        //$(currentElement).closest(".colspannew").find("span").remove();
                        $scope.LoadAcademicData();

                    },
                    complete: function (result) {
                        hideOverlay();
                    }
                });
            }
        };

        $(document).mouseup(function (e) {
            var container = $("#ItemPopup");
            // if the target of the click isn't the container nor a descendant of the container
            if (container.is(":visible") && !container.is(e.target) && container.has(e.target).length === 0) {
                //container.hide();
                /* $scope.ClosePopup();*/
            }
        });


    });

    $scope.CheckCalenderData = function (selected) {

        if (selected.ngModel.$name == "AcademicCalendar") {
            $scope.SelectedAcademicCalendar = selected.selected;
        }

        var academicCalendarID = $scope.SelectedAcademicCalendar?.Key;
        $scope.SelectedAcademicCalendarID = academicCalendarID;

        if (academicCalendarID == null || academicCalendarID == 0) {
            $().showGlobalMessage($root, $timeout, true, "Please Select Calendar");
            $scope.AcademicCalendarData = null;
            return false;
        }
        showOverlay();

        $.ajax({
            type: "GET",
            data: { calendarID: academicCalendarID },
            url: "Schools/School/CheckAndInsertCalendarEntries",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.LoadAcademicCalenderData(academicCalendarID)
            },
            error: function () {
                hideOverlay();
            },
            complete: function (result) {
            }
        });

    }

    $scope.LoadAcademicCalenderData = function (academicCalendarID) {
        showOverlay();

        $.ajax({
            type: "GET",
            data: { calendarID: academicCalendarID },
            url: "Schools/School/GetAcademicYearDataByCalendarID",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result != null) {

                    $scope.$apply(function () {
                        result.forEach(x => {
                            var startDate = new Date(x.StartDate);
                            var endDate = new Date(x.EndDate);
                            $scope.AcYearStartDate = startDate,
                                $scope.AcYearEndDate = endDate,
                                $scope.PopupMindate = getFormatDate(startDate),
                                $scope.PopupMaxdate = getFormatDate(endDate),
                                $scope.SelectedYear = startDate.getFullYear()
                        });
                    });
                }

            },
            error: function () {
                hideOverlay();
            },
            complete: function (result) {

                $.ajax({
                    type: "GET",
                    data: { calendarID: academicCalendarID },
                    url: "Schools/Calender/GetAcademicMonthAndYearByCalendarID",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {

                            });
                        }
                        $(".gridItemPopup").fadeOut("fast");
                        $(".gridItemOverlay").hide();
                    },
                    error: function () {
                        hideOverlay();
                    },
                    complete: function (result) {
                        $.ajax({
                            type: "GET",
                            data: { academicYearID: 0, year: $scope.SelectedYear, academicCalendarStatusID: 0, academicCalendarId: academicCalendarID },
                            url: "Schools/Calender/GetAcademicCalenderByAcademicYear",
                            contentType: "application/json;charset=utf-8",
                            success: function (result) {
                                if (!result.IsError && result != null) {

                                    $scope.$apply(function () {
                                        $scope.AcademicCalendarData = result;
                                        $scope.FillEventColorHead($scope.AcademicCalendarData);

                                    });
                                }
                                $(".gridItemPopup").fadeOut("fast");
                                $(".gridItemOverlay").hide();
                            },
                            error: function () {
                                hideOverlay();
                            },
                            complete: function (result) {
                                hideOverlay();
                            }
                        });
                    }
                });
            }
        });
    }

    function getFormatDate(curDate) {
        var dd = curDate.getDate();
        var mm = curDate.getMonth() + 1;
        var yyyy = curDate.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        return yyyy + '-' + mm + '-' + dd;
    }

    $scope.LoadAcademicData = function () {

        showOverlay();
        var academicCalendarID = $scope.SelectedAcademicCalendarID;
        if (academicCalendarID == null || academicCalendarID == 0) {
            $().showGlobalMessage($root, $timeout, true, "Please Select Calendar");
            $scope.AcademicCalendarData = null;
            return false;
        }

        $.ajax({
            type: "GET",
            data: { academicYearID: 0, year: $scope.SelectedYear, academicCalendarStatusID: 0, academicCalendarId: academicCalendarID },
            url: "Schools/Calender/GetAcademicCalenderByAcademicYear",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result != null) {

                    $scope.$apply(function () {
                        $scope.AcademicCalendarData = result;
                        // $scope.setDraggable();
                        $scope.FillEventColorHead($scope.AcademicCalendarData);

                    });
                }
                $(".gridItemPopup").fadeOut("fast");
                $(".gridItemOverlay").hide();
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }
        });
        // $scope.LoadAcademicCalender(result);
    };
    $scope.FillEventColorHead = function (academicData) {
        $scope.Events = [];

        angular.forEach(academicData, function (item) {
            var isExist = false;
            for (var i = 0; i < $scope.Events.length; i++) {
                if (item.EventTitle) {
                    if ($scope.Events[i]) {
                        if (item.EventTitle.toUpperCase() == $scope.Events[i].EventTitle.toUpperCase()) {
                            isExist = true;
                            break;
                        }
                    }
                }
            }
            if (isExist == false) {
                if (item.EventTitle) {
                    $scope.Events.push({
                        'EventTitle': item.EventTitle,
                        'ColorCode': item.ColorCode
                    });
                }
            }
        });
    };
    $scope.LoadAcademicCalender = function (academicData) {
        $scope.SelectedDay = new Date().getDate();
        $scope.AcademicCalendarData = [];
        for (var m = 1; m <= 12; m++) {

            for (var i = 1; i <= 31; i++) {

                $scope.AcademicCalendarData.push({
                    date: i.toString() + '/' + (m + 1).toString() + '/' + $scope.SelectedYear.toString(),
                    month: m, year: $scope.SelectedYear, day: i,
                    reason: '',
                    actualDate: new Date(i.toString() + '/' + $scope.MonthNames[m] + '/' + $scope.SelectedYear.toString())
                });
            }

        }

    };
    $scope.LoadPopup = function (popupcontainer, event) {
        var dataAttr = $(event.currentTarget).attr('data-attr');
        var popupLeftPos = 0;
        var targetLeftPos = $(event.target).offset().left;
        var targetTopPos = $(event.target).offset().top - $(document).scrollTop();
        var pageWidth = $(document).outerWidth();
        var windowHeight = $(window).height();
        var eventWidth = $(event.target).outerWidth();
        var eventHeight = $(event.target).height();
        var popcontainerWidth = $(popupcontainer).outerWidth();
        var popcontainerHeight = $(popupcontainer).outerHeight();
        var popupTopPos = targetTopPos + eventHeight;
        var displayLeftArea = targetLeftPos + popcontainerWidth;
        var visiblePopupArea = popupTopPos + popcontainerHeight;
        $(popupcontainer).fadeIn("fast");
        if (displayLeftArea > pageWidth) {
            popupLeftPos = targetLeftPos - popcontainerWidth + eventWidth;
            $(popupcontainer).addClass('rightAligned');
        }
        else {
            popupLeftPos = targetLeftPos;
            $(popupcontainer).removeClass('rightAligned');
        }
        if (visiblePopupArea > windowHeight) {
            newTopPos = popupTopPos - popcontainerHeight - eventHeight;
            $(popupcontainer).addClass('setTooltipBottom');
        }
        else {
            newTopPos = popupTopPos;
            $(popupcontainer).removeClass('setTooltipBottom');
        }

        $(popupcontainer).css({ "left": popupLeftPos, "top": newTopPos });
    };


    //To fill existing details for viewing and editing.
    $scope.FillPopupFromSaved = function (currentelement) {
        $scope.$apply(function () {
            $scope.submitted = false;

            var subjItem = $(currentelement).find(".seletedEvent");
            if (subjItem.length > 0) {//Edit 

                var startDate = new Date($(subjItem).attr("data-startDate-id"));

                var endDate = new Date($(subjItem).attr("data-endDate-id"));
                $scope.PopupStartDate = startDate;
                $scope.PopupEndDate = endDate;
                $scope.PopupColorCode = $(subjItem).attr("data-ColorCode-id");
                $scope.PopupEventTitle = $(subjItem).attr("data-EventTitle-id");
                $scope.PopupDescription = $(subjItem).attr("data-Description-id");
                $scope.PopupAcademicId = $(subjItem).attr("data-academic-id");
                $scope.PopupAcademicEventId = $(subjItem).attr("data-AcademicEvent-id");
                $scope.PopupType = $(subjItem).attr("data-AcademicYearCalendarEventType-id");

                $scope.PopupIsHoliDay = $(subjItem).attr("data-isHoliDay-id") == "true" ? true : false;
                $scope.PopupHours = $(subjItem).attr("data-NoofHours-id");
            } else {//New
                var month = $(currentelement).parents(".monthName").attr("data-month-id");
                var day = $(currentelement).parents(".DayId").attr("data-Day-id");
                var Year = $scope.SelectedYear;
                $scope.PopupStartDate = new Date((parseInt(month) + 1).toString() + '/' + day.toString() + '/' + Year.toString());
                $scope.PopupEndDate = new Date((parseInt(month) + 1).toString() + '/' + day.toString() + '/' + Year.toString());
                $scope.PopupEventTitle = null;
                $scope.PopupDescription = null;
                $scope.PopupAcademicId = $scope.SelectedAcademicCalendarID;
                $scope.PopupAcademicEventId = 0;
                $scope.PopupColorCode = '#FFFFFF';
                $scope.PopupIsHoliDay = true;
                $scope.PopupHours = null;
            }
        });
    }

    $scope.ChangeCheckBoxData = function () {
        var checkBoxData = $scope.PopupIsHoliDay;
        if (checkBoxData == true) {
            if ($scope.type == 'NonAcademic') {
                $scope.PopupHours = 0;
            }
        }
        else {
            $scope.PopupHours = null;
        }
    }


    $scope.SaveEvents = function (popupcontainer) {

        if ($scope.type == 'Academic') {
            if ($scope.PopupEventTitle == null || $scope.PopupDescription == null || $scope.PopupStatus == 0) {
                alert("Please fill out required fields!");
                return false;
            }

            if ($scope.PopupColorCode == null || $scope.PopupColorCode == '#FFFFFF') {
                alert("Please select color code!");
                return false;
            }
        }

        if ($scope.type == 'NonAcademic') {

            if ($scope.PopupStatus == 0) {
                alert("Please fill out required fields!");
                return false;
            }

            if ($scope.PopupIsHoliDay == false) {
                if ($scope.PopupHours <= 0 || !$scope.PopupHours) {
                    alert("Please fill out working hour greater than 0!");
                    return false;
                }
            }
            else {
                if (!$scope.PopupType) {
                    alert("Select an event type!");
                    return false;
                }
            }
        }

        if ($scope.PopupType) {
            if (!$scope.PopupEventTitle) {
                alert("Please fill out title!");
                return false;
            }
            if (!$scope.PopupDescription) {
                alert("Please fill out description!");
                return false;
            }
        }

        var academicCalendarID = $scope.SelectedAcademicCalendarID;

        if (academicCalendarID == null || academicCalendarID == 0) {
            alert("Please Select Calendar");
            $scope.AcademicCalendarData = null;
            return false;
        }

        else {
            SaveEventData();
        }
    };

    function SaveEventData() {
        $scope.submitted = true;
        var selectedDate = new Date(moment(new Date($scope.PopupStartDate)).format(_dateFormat.toUpperCase()));
        var selectedEndDate = new Date(moment(new Date($scope.PopupEndDate)).format(_dateFormat.toUpperCase()));

        var startday = $scope.PopupStartDate.getDate();
        var startMonth = parseInt($scope.PopupStartDate.getMonth()) + 1;
        var endday = $scope.PopupEndDate.getDate();
        var endMonth = parseInt($scope.PopupEndDate.getMonth()) + 1;

        var startyear = $scope.PopupStartDate.getFullYear();
        var endyear = $scope.PopupEndDate.getFullYear();
        if (startday.toString().length == 1)
            startday = '0' + startday;
        if (startMonth.toString().length == 1)
            startMonth = '0' + startMonth;

        if (endday.toString().length == 1)
            endday = '0' + endday;
        if (endMonth.toString().length == 1)
            endMonth = '0' + endMonth;

        var startDateValue = (startday.toString() + '/' + startMonth + '/' + startyear.toString()).toString();

        var endDateValue = (endday.toString() + '/' + endMonth + '/' + endyear.toString()).toString();

        //var selectedDate = new Date(startDateValue);
        //var selectedEndDate = new Date(endDateValue);

        var msg = '';
        if ($scope.AcYearStartDate <= $scope.PopupStartDate && $scope.AcYearEndDate >= $scope.PopupStartDate) {
            msg = '';
            $scope.SelectedYear = $scope.AcYearStartDate.getFullYear();
        } else {
            if ($scope.AcYearStartDate <= $scope.PopupEndDate && $scope.AcYearEndDate >= $scope.PopupEndDate) {
                msg = '';
                $scope.SelectedYear = $scope.AcYearEndDate.getFullYear();
            } else {
                alert("The selected date should belong to the selected acadamic period!")
                return;
            }
        }

        $.ajax({
            url: utility.myHost + "Schools/Calender/SaveAcademicCalendar",
            type: "POST",
            data: {
                "EventTitle": $scope.PopupEventTitle,
                "Description": $scope.PopupDescription,
                "StartDateString": startDateValue,
                "EndDateString": endDateValue,
                "ColorCode": $scope.PopupColorCode,
                "AcademicYearID": 1,
                "AcademicCalendarStatusID": 1,
                "AcademicCalendarEventTypeID": $scope.PopupType,
                "AcademicCalendarID": $scope.PopupAcademicId,
                "AcademicYearCalendarEventIID": $scope.PopupAcademicEventId,
                "IsThisAHoliday": $scope.PopupIsHoliDay,
                "NoofHours": $scope.PopupHours,
            },
            success: function (result) {
                if (!result.IsError && result != null) {
                    if (result == "#101") {
                        alert("Color Code Already Exists!")
                        return;
                    }
                    if (result == "#102") {
                        alert("Dates conflicted!")
                        return;
                    }
                    if (result == "#103") {
                        alert("End Date should be less than Start Date!")
                        return;
                    }
                    $scope.LoadAcademicData();
                }
            },
        });
    }

    function showOverlay() {
        $('.preload-overlay', $('#AcademicCalender_' + $scope.type)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $('#AcademicCalender_' + $scope.type)).hide();
    }

    function showPopupOverlay() {
        $('.preload-overlay', $('#ItemPopup')).attr('style', 'display:block');
    }

    function hidePopupOverlay() {
        $('.preload-overlay', $('#ItemPopup')).hide();
    }
    //To convert value to integer.
    $scope.parseInt = function (val) {
        return parseInt(val);
    }


    //To close Popup
    $scope.ClosePopup = function () {
        //if ($scope.PopupAcademicEventId == null || $scope.PopupAcademicEventId == "0") {
        //    $("#" + $scope.PopupSection + $scope.PopupDay + $scope.PopupTime).remove();
        //}
        $(".gridItemPopup").fadeOut("fast");
        $(".gridItemOverlay").hide();
    }

    //To tab open and close
    $scope.onCalendarViewTab = function (tabId) {
        if (tabId == "Tab_01") {
            $scope.TabName = "Tab_01";

            $("#Calendar_Yearly_Tab_01").show();

            $("#Calendar_Event_Tab_02").hide();

            $("#Tab_01_nav").toggleClass('active');
            $("#Tab_02_nav").toggleClass('active');
        }
        else if (tabId == "Tab_02") {
            $scope.TabName = "Tab_02";

            $("#Calendar_Event_Tab_02").show();

            $("#Calendar_Yearly_Tab_01").hide();

            $("#Tab_02_nav").toggleClass('active');
            $("#Tab_01_nav").toggleClass('active');
        }
    }
    //End tab open and close

    $scope.AddNewEvent = function () {
        $scope.PopupColorCode = null;
        $scope.PopupEventTitle = null;
        $scope.PopupDescription = null;
        $scope.PopupAcademicEventId = null;
        $scope.PopupType = null;

        $scope.PopupIsHoliDay = true;
        $scope.PopupHours = 0;
    }

}]);