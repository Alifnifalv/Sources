app.controller("TimeTableController", ["$scope", "$http", "$compile", "$window",
    "$timeout", "$location", "$route", "$rootScope", function ($scope, $http,
        $compile, $window, $timeout, $location, $route, $root) {

        console.log("TimeTableController Loaded");

        $scope.Model = [];
        $scope.Subjects = [];
        $scope.Teachers = [];
        $scope.ClassMapping = [];
        $scope.ClassSections = [];
        $scope.Classes = [];
        $scope.TimeTableMapping = [];
        $scope.TimeTableDaily = [];
        $scope.Model = {};
        $scope.WeekDay = {};
        $scope.ClassTime = {};
        $scope.SelectedTeacher = {};
        $scope.SelectedSubject = {};
        $scope.selectedTableMasterData = {};
        $scope.selectedClasses = {};
        $scope.PopupSubject = 0;
        $scope.PopupTeacher = 0;
        $scope.TabName = "Tab_01";
        $scope.AllocatedDate == null;
        $scope.isMasterTabVisible = false;
        $scope.ClassSectionTimeTable = [];
        $scope.showPopup = false;
        $scope.ScreenLoaded = false;
        $scope.IsSetupScreenVisible = false;


        //Initializing the time table controller.
        $scope.init = function (model, windowname) {

            //---Start: Fetching masters data on load.  

            ////Teachers
            //$http({
            //    method: 'Get', url: 'Payroll/Employee/GetEmployeesByRoleID?roleID=182'
            //}).then(function (result) {
            //    $scope.Teachers = result.data;
            //});

            //TimeTableMasterData
            $http({
                method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=TimeTable&defaultBlank=false'
            }).then(function (result) {
                $scope.TableMasterData = result.data;
            });

            //Classes
            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Classes&defaultBlank=false",
            }).then(function (result) {
                $scope.Classes = result.data;
            });

            //Sections
            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Section&defaultBlank=false",
            }).then(function (result) {
                $scope.Sections = result.data;
            });

            //Week Days
            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=WeekDay&defaultBlank=false",
            }).then(function (result) {
                $scope.WeekDay = result.data;
            });

            //Class times
            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=ClassTime&defaultBlank=false",
            }).then(function (result) {
                $scope.ClassTime = result.data;
            });

            //---End: Fetching masters data on load.

        };


        function showOverlay() {
            $('.preload-overlay', $('#TimeTableBuilder')).attr('style', 'display:block');
        }

        function hideOverlay() {
            $('.preload-overlay', $('#TimeTableBuilder')).hide();
        }
        function showPopupOverlay() {
            var tabId = $scope.TabName;
            if (tabId == "Tab_01") {

                $("#datedaily").hide();
            }
            else if (tabId == "Tab_02") {

                $("#datedaily").show();
            }
            $('.preload-overlay', $('#ItemPopup')).attr('style', 'display:block');
        }

        function hidePopupOverlay() {
            $('.preload-overlay', $('#ItemPopup')).hide();
        }


        //To tab open and close
        $scope.onTimeTableAllacateTab = function (tabId) {

            $(".nav-link").removeClass('active'); 
            $("#TimeTableAllacate_timetablesubjects_Tab_01, #TimeTableAllacate_Tab_01, #TimeTableAllacate_timetablesubjects_Tab_02, #TimeTableAllacate_Tab_02, #TimeTableAllacate_timetablesubjects_Tab_03, #TimeTableAllacate_Tab_03").hide();  // Hide all tab content  

            if (tabId === "Tab_01") {
                $scope.TabName = "Tab_01";
                $("#Tab_01_nav").addClass('active'); 
                $("#TimeTableAllacate_timetablesubjects_Tab_01").show();
                $("#TimeTableAllacate_Tab_01").show();

                //$timeout(function () {
                //    $scope.$apply(function () {
                //        $scope.TimeTableDailyMapping = null; 

                //        $scope.selectedTableMasterData.Key = null;
                //        $scope.selectedTableMasterData.Value = null;
                //        $scope.selectedTableMasterData.object = null;

                //        $scope.selectedClasses.Key = null;
                //        $scope.selectedClasses.Value = null;
                //        $scope.selectedClasses.object = null;

                //    });
                //});

                $("#datedaily").hide();
                $("#AllocatedDateController").hide();
                $scope.loadGlobalTimeTable($scope.selectedTableMasterData, $scope.selectedClasses);
                $scope.GetAllWeekDays();

                $scope.IsSetupScreenVisible = false;

            } else if (tabId === "Tab_02") {
                $scope.TabName = "Tab_02";
                $("#Tab_02_nav").addClass('active');
                $("#TimeTableAllacate_timetablesubjects_Tab_02").show();
                $("#TimeTableAllacate_Tab_02").show();
                $("#datedaily").show();
                $("#AllocatedDateController").show();

                //$scope.selectedTableMasterData = null;
                //$scope.selectedClasses = null;

                $scope.AllocatedDate = moment(new Date()).format(_dateFormat.toUpperCase());
                $scope.TimeTableMappingByDate($scope.AllocatedDate);

                $scope.GetWeekDayByDate();
                $scope.IsSetupScreenVisible = false;

            } else if (tabId === "Tab_03") {
                $scope.TabName = "Tab_03";
                $("#Master_Tab_02_nav").addClass('active');
                $("#TimeTableAllacate_timetablesubjects_Tab_03").show();
                $("#TimeTableAllacate_Tab_03").show();

                //$scope.selectedTableMasterData = {};
                $scope.GetAllWeekDays();
                $scope.IsSetupScreenVisible = false;

                $("#datedaily").hide();
                $("#AllocatedDateController").hide();
                $scope.GetSmartTimeTableDetails();

            } else if (tabId === "Setups_Tab") {
                $scope.TabName = "Setups_Tab";
                $("#Setups_Tab_nav").addClass('active');
                $("#TimeTableAllacate_timetablesubjects_Tab_03").hide();
                $("#TimeTableAllacate_Tab_03").hide();

                $scope.IsSetupScreenVisible = true;

                $("#datedaily").hide();
                $("#AllocatedDateController").hide();

            }

            //$scope.ClearDatas();
        };

        $scope.onTimeTableAllacateMasterTab = function (tabId) {
            // Reset all tab content and nav links  
            $(".nav-link").removeClass('active');  // Remove active class from all  
            $("#TimeTableAllacate_timetablesubjects_Tab_01, #TimeTableAllacate_Tab_01, #TimeTableAllacate_timetablesubjects_Tab_02, #TimeTableAllacate_Tab_02, #TimeTableAllacate_timetablesubjects_Tab_03, #TimeTableAllacate_Tab_03").hide();  // Hide all tab content  

            // Show/Activate the selected tab  
            if (tabId === "Master_Tab_01") {
                $scope.TabName = "Master_Tab_01";
                $("#Master_Tab_01_nav").addClass('active');
                $("#TimeTableAllacate_Tab_01").show();
                $("#TimeTableAllacate_Tab_02").show();
                $("#TimeTableAllacate_Tab_03").hide();

                $("#MasterTab_01").show();
                $("#MasterTab_02").hide();
                $("#Setups_Details").hide();

                $scope.isMasterTabVisible = false;

                $scope.onTimeTableAllacateTab('Tab_01');
                $scope.ClearDatas();
            }
            else if (tabId === "Master_Tab_02") {
                $scope.TabName = "Master_Tab_02";
                $("#Master_Tab_02_nav").addClass('active');
                $("#TimeTableAllacate_Tab_01").hide();
                $("#TimeTableAllacate_Tab_02").hide();
                $("#TimeTableAllacate_Tab_03").show();
                $("#MasterTab_01").hide();
                $("#Setups_Details").hide();
                //$("#MasterTab_02").show();
                //$scope.isMasterTabVisible = true;

                //$scope.GetTeacherSummary();
                //$scope.GetClassSummary();
                //$scope.GetClassSectionTimeTableSummary();

                $scope.onTimeTableAllacateTab('Tab_03');
                $scope.ClearDatas();
            }
            else if (tabId === "Setups_Tab") {
                $scope.TabName = "Setups_Tab";
                $("#Setups_Tab_nav").addClass('active');
                $("#TimeTableAllacate_Tab_01").hide();
                $("#TimeTableAllacate_Tab_02").hide();
                $("#Setups_Details").show();

                $("#MasterTab_01").hide();
                $("#MasterTab_02").hide();
                //$("#Setups_Tab_Detail").show();

                $scope.onTimeTableAllacateTab('Setups_Tab');
                $scope.ClearDatas();
            }

        };

        //To convert value to integer.
        $scope.parseInt = function (val) {
            return parseInt(val);
        }

        //To enable subject items to drag and drop
        $scope.setDraggable = function () {

            $timeout(function () {

                const containers = document.querySelectorAll('.timetable');
                if (containers.length === 0) {
                    return false;
                }

                $(".resizable").resizable({
                    handles: 'e',
                    stop: function (event, ui) {
                        var newWidth = $(event.target).width();
                        $(this).css("width", newWidth);
                    }
                });

                $('.timetablesubjects li span.seletedsubject').draggable({
                    revert: true, // bounce back when dropped
                    helper: "clone", // create "copy" with original properties, but not a true clone
                    cursor: "move",
                    revertDuration: 0 // immediate snap
                });
                if ($scope.TabName == "Tab_01") {
                    $(".schedulerTable table tr.content td div.scaleWrap span.colspan").droppable({
                        accept: ".timetablesubjects li span.seletedsubject",
                        drop: function (event, ui) {
                            // clone item to retain in original "list"
                            if ($(this)[0].innerText != "Break") {
                                var $item = ui.draggable.clone();
                                var selectedSpanId = $(this).parents(".section").attr('data-section-id') + $(this).parents(".WeekDay").attr('data-week-id') + $(this).parents(".classtime").attr('data-time-id');
                                $item.attr('id', selectedSpanId);
                                $(this).html($item);
                                $item.append('<span ng-click="DeleteTimeTableEntry(0)" class="closeItem" data-Alloc-id="0">x</span>');
                                $scope.LoadPopup('.gridItemPopup', event);
                                $scope.FillPopup($(this));
                                $(".gridItemPopup").show();
                            }
                        }
                    });
                }
                else if ($scope.TabName == "Tab_03") {
                    $(".schedulerTable row card d-flex overflow-scroll table tr.content td div.scaleWrap span.colspan").droppable({
                        accept: ".timetablesubjects li span.seletedsubject",
                        drop: function (event, ui) {
                            // clone item to retain in original "list"
                            if ($(this)[0].innerText != "Break") {
                                var $item = ui.draggable.clone();
                                var selectedSpanId = $(this).parents(".section").attr('data-section-id') + $(this).parents(".WeekDay").attr('data-week-id') + $(this).parents(".classtime").attr('data-time-id');
                                $item.attr('id', selectedSpanId);
                                $(this).html($item);
                                $item.append('<span ng-click="DeleteTimeTableEntry(0)" class="closeItem" data-Alloc-id="0">x</span>');
                                $scope.LoadPopup('.gridItemPopup', event);
                                $scope.FillPopup($(this));
                                $(".gridItemPopup").show();
                            }
                        }
                    });

                    var selectedClass = $scope.selectedClass;
                    //$scope.loadClasswiseSubject();
                }
                else {
                    $(".schedulerTable table tr.content td div.scaleWrap span.colspan").droppable({
                        accept: ".timetablesubjects li span.seletedsubject",
                        drop: function (event, ui) {
                            // clone item to retain in original "list"
                            if ($(this)[0].innerText != "Break") {
                                var $item = ui.draggable.clone();
                                var selectedSpanId = $(this).parents(".section").attr('data-section-id') + $(this).parents(".WeekDay").attr('data-week-id') + $(this).parents(".classtime").attr('data-time-id');
                                $item.attr('id', selectedSpanId);
                                $(this).html($item);
                                $item.append('<span ng-click="DeleteTimeTableEntry(0)" class="closeItem" data-Alloc-id="0">x</span>');
                                $scope.LoadPopup('.gridItemPopupDaily', event);
                                $scope.FillPopup($(this));
                                $(".gridItemPopupDaily").show();
                            }
                        }
                    });
                }

                //-----NOTE: Needed for TAB chnages.
                //$('.timetable').on('click', '.menuinnertab li', function (e) {
                //    if ($(this).hasClass('active')) return;
                //    var menuattr = $(this).attr('data-target');
                //    $('.timetable .menuinnertab li').removeClass('active');
                //    $(this).addClass('active');
                //    $('.timetable .innertab').hide();
                //    $('.timetable .innertab.' + menuattr).fadeIn();
                //});

            });

        };

        //To open popup to view, add and edit details.
        $scope.LoadPopup = function (popupcontainer, event) {
            if ($scope.TabName == "Tab_03") {
                if ($scope.selectedClasses == null || $scope.selectedClasses == 'undefined') {
                    $().showGlobalMessage($root, $timeout, true, "Please select a class to drag!!");
                    return;
                }
            }


            //$scope.Model = model;
            //$scope.CurrentData = CurrentData;
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

        $scope.FillPopup = function (currentelement) {
            $scope.$apply(function () {
                $scope.submitted = false;
                $scope.PopupClass = $scope.selectedClasses;
                $scope.PopupTimeTableMaster = $scope.selectedTableMasterData;
                $scope.PopupSection = $(currentelement).parents(".section").attr("data-section-id");
                $scope.PopupDay = $(currentelement).parents(".WeekDay").attr("data-week-id");
                $scope.PopupTime = $(currentelement).parents(".classtime").attr("data-time-id");
                $scope.PopupSubject = $(currentelement).find(".seletedsubject").attr("data-subject-id");
                $scope.PopupAllocId = $(currentelement).find(".seletedsubject").attr("data-Alloc-id");
                $scope.SelectedTeacher = {};
                $scope.PopupTeacher = 0;

                $scope.PopupDate = $scope.AllocatedDate;
                $scope.loadSubjectwiseTeacher();
            });
        }

        //$scope.FillPopupNew = function (currentelement) {
        //    $scope.$apply(function () {
        //        $scope.PopupClass = $scope.selectedClasses.Key;
        //        $scope.PopupTimeTableMaster = $scope.selectedTableMasterData.Key;
        //        $scope.PopupSection = $(currentelement).parents(".section").attr("data-section-id");
        //        $scope.PopupDay = $(currentelement).parents(".WeekDay").attr("data-week-id");
        //        $scope.PopupTime = $(currentelement).parents(".classtime").attr("data-time-id");

        //        $scope.PopupSubject = $(currentelement).find(".seletedsubject").attr("data-subject-id");
        //        if (angular.isUndefined($scope.PopupSubject) || $scope.PopupSubject == null) {
        //            $(".PopupSubject").prop('disabled', false);
        //            $scope.PopupSubject = $scope.Subjects;
        //        }
        //        else {
        //            $(".PopupSubject").prop('disabled', true);

        //        }

        //        $scope.PopupAllocId = $(currentelement).find(".seletedsubject").attr("data-Alloc-id");
        //        $scope.loadSubjectwiseTeacher($(currentelement).parents(".section").attr("data-section-id"), $(currentelement).find(".seletedsubject").attr("data-subject-id"));
        //    });
        //}

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

                var subjItem = $(this).find(".seletedsubject");

                if (subjItem.length > 0) {//Edit 
                    $scope.PopupSubject = $(subjItem).attr("data-Subject-id");
                    if ($scope.PopupSubject == null || $scope.PopupSubject == 0) {
                        return;
                    }

                }
                $scope.LoadPopup('.gridItemPopup', event);
                $scope.FillPopupFromSaved($(this));
                $(".gridItemOverlay").show();

            });


            //To delete existing time table
            //$(document).on('click', '.closeItem', function () {
            $scope.DeleteTimeTableEntry = function (timeTableAlocID) {
                var currentElement = $(this);

                if (timeTableAlocID != null && timeTableAlocID > 0) {
                    if (window.confirm("Do you want to delete subject?") == false) {
                        return;
                    }
                    var tabid = $scope.TabName;
                    showOverlay();
                    if (tabid == "Tab_02") {
                        $.ajax({
                            url: utility.myHost + "Schools/TimeTable/DeleteDailyTimeTableEntry",
                            type: "POST",
                            data: { "timeTableLogID": timeTableAlocID },
                            success: function (result) {
                                //$(currentElement).closest(".colspan").find("span").remove();
                                $scope.TimeTableMappingByDate($scope.AllocatedDate);
                            },
                            complete: function (result) {
                                hideOverlay();
                            }
                        });
                    }
                    else {
                        $.ajax({
                            url: utility.myHost + "Schools/TimeTable/DeleteTimeTableEntry",
                            type: "POST",
                            data: { "timeTableAllocationID": timeTableAlocID },
                            success: function (result) {
                                //$(currentElement).closest(".colspan").find("span").remove();
                                $scope.loadGlobalTimeTable($scope.selectedTableMasterData, $scope.selectedClasses);
                            },
                            complete: function (result) {
                                hideOverlay();
                            }
                        });

                    }
                }
            };

            $(document).mouseup(function (e) {
                var container = $("#ItemPopup");
                // if the target of the click isn't the container nor a descendant of the container
                if (container.is(":visible") && !container.is(e.target) && container.has(e.target).length === 0) {
                    //container.hide();
                    $scope.ClosePopup();
                }
            });

            //$(document).scroll(function () {
            //    //if ($("#ItemPopup").is(":visible")) {
            //    $scope.ClosePopup();
            //    //}
            //});

        });



        //To fill existing details for viewing and editing.
        $scope.FillPopupFromSaved = function (currentelement) {
            $scope.$apply(function () {
                $scope.submitted = false;
                $scope.PopupClass = $scope.selectedClasses;
                $scope.PopupTimeTableMaster = $scope.selectedTableMasterData;
                $scope.PopupSection = $(currentelement).parents(".section").attr("data-section-id");
                $scope.PopupDay = $(currentelement).parents(".WeekDay").attr("data-week-id");
                $scope.PopupTime = $(currentelement).parents(".classtime").attr("data-time-id");
                $scope.PopupDate = $scope.AllocatedDate;
                var subjItem = $(currentelement).find(".seletedsubject");
                if (subjItem.length > 0) {//Edit 
                    $scope.PopupSubject = $(subjItem).attr("data-Subject-id");
                    $scope.loadSubjectwiseTeacher();
                    $scope.PopupTeacher = $(subjItem).attr("data-Staff-id");
                    $scope.PopupAllocId = $(subjItem).attr("data-Alloc-id");
                } else {//New
                    $timeout(function () {
                        $scope.PopupSubject = 0;
                        $scope.PopupTeacher = 0;
                        $scope.PopupAllocId = 0;
                        $scope.Teachers = [];
                        $scope.SelectedTeacher = {};
                        $scope.SelectedSubject = {};
                    }, 0);
                }
            });
        }

        //------Start: Core functionalities or events.



        $scope.LoadClasswiseTimeTable = function (type, selected, userChanged) {

            if (type == "TimeTable") {
                $scope.selectedTableMasterData = selected;
            }
            if (type == "Class") {
                $scope.selectedClasses = selected;
            }

            var tableMasterId = $scope.selectedTableMasterData;
            var classId = $scope.selectedClasses;
            var tabid = $scope.TabName;

            showOverlay();
            if (tabid == "Tab_01") {
                $scope.loadGlobalTimeTable(tableMasterId, classId);
            }
            else if (tabid == "Tab_03") {
                $scope.GetSmartTimeTableDetails(tableMasterId);
            }
            else {
                if ($scope.AllocatedDate == 'undefined' || $scope.AllocatedDate == null)
                    $scope.AllocatedDate = FormatDate(new Date());
                $scope.TimeTableMappingByDate($scope.AllocatedDate);
            }
        }

        function FormatDate(inputDate) {
            var today = inputDate;
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = dd + '/' + mm + '/' + yyyy;

            return today;
        }

        $scope.LoadDailyTimeTable = function (date) {

            var dateParts = date.split('/');
            var selectedDate = new Date(
                parseInt(dateParts[2], 10),
                parseInt(dateParts[1], 10) - 1,
                parseInt(dateParts[0], 10)
            );

            var todayDate = new Date();
            todayDate.setHours(0, 0, 0, 0);

            if (selectedDate < todayDate) {
                $().showGlobalMessage($root, $timeout, true, "Selecting a prevoius date is not allowed.!");
                $scope.AllocatedDate = FormatDate(new Date());

                return;
            }

            $scope.TimeTableMappingByDate(date);
            $scope.GetWeekDayByDate();
        }

        $scope.TimeTableMappingByDate = function (selectedDate) {

            showOverlay();

            var tableMasterId = $scope.selectedTableMasterData;
            var classId = $scope.selectedClasses;

            if (tableMasterId == null || tableMasterId == 0) {
                $().showGlobalMessage($root, $timeout, true, "Please select a time table type!!");
                $scope.TimeTableMapping = null;
                $scope.TimeTableDaily = null;
                hideOverlay();

                return false;
            }

            if (classId == null || classId == 0) {
                $scope.TimeTableMapping = null;
                $scope.TimeTableDaily = null;
                $().showGlobalMessage($root, $timeout, true, "Please select the class and section");
                hideOverlay();

                return false;
            }

            $.ajax({
                type: "GET",
                data: { classId: classId, tableMasterId: tableMasterId, timeTableDate: $scope.AllocatedDate },
                url: "Schools/TimeTable/GetTimeTableByDate",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.TimeTableDailyMapping = result;
                            $scope.setDraggable();

                        });
                    }
                    $(".gridItemPopup").fadeOut("fast");
                    $(".gridItemOverlay").hide();
                },
                error: function () {

                },
                complete: function (result) {
                    $scope.loadClasswiseSubject();
                    $scope.GetWeekDays();

                }
            });
        }

        //To load class-wise section and saved time tables
        $scope.loadGlobalTimeTable = function (tableMasterId, classId) {
            showOverlay();
            $.ajax({
                type: "GET",
                data: { classId: classId, tableMasterId: tableMasterId },
                url: "Schools/TimeTable/GetTimeTableByClassID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.TimeTableMapping = result;
                            $scope.setDraggable();

                        });
                    }
                    $(".gridItemPopup").fadeOut("fast");
                    $(".gridItemOverlay").hide();
                },
                error: function () {

                },
                complete: function (result) {
                    $scope.loadClasswiseSubject();
                }
            });
        }

        //To load class & date wise section and saved time tables
        $scope.loadDailyTimeTable = function (tableMasterId, classId, timetableDate) {

            if (selected.ngModel.$name == "TimeTable") {
                $scope.selectedTableMasterData = selected.selected;
            }
            if (selected.ngModel.$name == "Class") {
                $scope.selectedClasses = selected.selected;
            }

            var tableMasterId = $scope.selectedTableMasterData;
            var classId = $scope.selectedClasses;

            if (classId == null || classId == 0) {
                $scope.TimeTableDailyMapping = null;
                return false;
            }
            if (tableMasterId == null || tableMasterId == 0) {
                alert("Please Select Time Table Type");
                $scope.TimeTableDailyMapping = null;
                return false;
            }
            showOverlay();
            $.ajax({
                type: "GET",
                data: { classId: classId, tableMasterId: tableMasterId, TimeTableDate: timetableDate },
                url: "Schools/TimeTable/GetTimeTableByDate",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.TimeTableDailyMapping = result;
                            $scope.setDraggable();

                        });
                    }
                    $(".gridItemPopup").fadeOut("fast");
                    $(".gridItemOverlay").hide();
                },
                error: function () {

                },
                complete: function (result) {
                    $scope.loadClasswiseSubject();
                }
            });
        }

        $scope.loadClasswiseSubject = function () {
            $scope.Subjects = [];
            var cls = $scope.selectedClasses;
            showOverlay();
            $.ajax({
                type: "GET",
                data: { classId: cls },
                url: "Schools/TimeTable/GetSubjectDetailsByClassID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        //result.forEach(x => {
                        //    $scope.Subjects.push({
                        //        "Key": x.Key,
                        //        "Value": x.Value,
                        //    });
                        //    $scope.setDraggable();
                        //});

                        result.forEach(x => {
                            $scope.Subjects.push({
                                "SubjectID": x.SubjectID,
                                "SubjectName": x.SubjectName,
                                "HexColorCode": x.HexColorCode,
                                "IconFileName": x.IconFileName
                            });
                            $scope.setDraggable();
                        });
                        
                    }
                    hideOverlay();
                },
                error: function () {

                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }

        $scope.loadSubjectwiseTeacher = function (selectedItem) {//selectedItem
            var cls = $scope.selectedClasses;

            if (selectedItem != null) {
                $scope.PopupSubject = selectedItem.selected.SubjectID;
            }

            if ($scope.PopupSubject != null || $scope.PopupSubject != undefined) {
                $scope.SelectedSubject = $scope.Subjects.find(x => x.SubjectID == $scope.PopupSubject);
            }

            showPopupOverlay();
            $.ajax({
                type: "GET",
                data: { classId: cls, sectionId: $scope.PopupSection, subjectId: $scope.PopupSubject },
                url: "Schools/SubjectTeacherMap/GetTeacherBySubject",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.Teachers = result;
                        });
                    }
                    //$(".gridItemPopup").fadeOut("fast");
                    //$(".gridItemOverlay").hide();
                },
                error: function () {

                },
                complete: function (result) {
                    hidePopupOverlay();
                }
            });
        }

        $scope.loadTeacheKey = function (selectedItem) {
            $scope.SelectedTeacher = [];
            if (selectedItem != null) {
                $scope.PopupTeacher = selectedItem.selected.Key;
            }

            if ($scope.PopupTeacher != null || $scope.PopupTeacher != undefined || $scope.PopupTeacher != 0) {
                $scope.SelectedTeacher = $scope.Teachers.find(x => x.Key == $scope.PopupTeacher);
            }
        }

        //To close Popup
        $scope.ClosePopup = function () {
            if ($scope.PopupAllocId == null || $scope.PopupAllocId == "0") {
                $("#" + $scope.PopupSection + $scope.PopupDay + $scope.PopupTime).remove();
            }
            $(".gridItemPopup").fadeOut("fast");
            $(".gridItemOverlay").hide();
        }

        //To add or edit time table
        $scope.SaveTimeTable = function (popupcontainer) {
            var tabid = $scope.TabName;

            var subjItem = $(popupcontainer).find(".seletedsubject");
            if (subjItem.length > 0) {
                $scope.PopupAllocId = $(subjItem).attr("data-Alloc-id");
            }

            $scope.submitted = true;
            if ($scope.PopupSubject == null || $scope.PopupSubject == 0 || $scope.PopupTeacher == null || $scope.PopupTeacher == 0) {
                $().showGlobalMessage($root, $timeout, true, "Please fill out required fields!");
                return false;
            } else {
                showPopupOverlay();

                if (tabid == "Tab_01" || tabid == "Tab_03") {

                    $.ajax({

                        url: utility.myHost + "Schools/TimeTable/SaveTimeTable",
                        type: "POST",
                        data: {
                            "TimeTableId": $scope.PopupTimeTableMaster,
                            "ClassId": $scope.PopupClass,
                            "SectionID": $scope.PopupSection,
                            "WeekDayID": $scope.PopupDay,
                            "ClassTimingID": $scope.PopupTime,
                            "SubjectID": $scope.PopupSubject,
                            "StaffID": $scope.PopupTeacher, 
                            "TimeTableAllocationIID": $scope.PopupAllocId,
                            "IsEnteredManually": 1,
                        },
                        success: function (result) {
                            if (!result.IsError && result != null) {
                                if (result == "#101") {
                                    alert("This teacher is already assigned for the same time slot")
                                    return;
                                }
                                var currentColspan = $("#colspan" + $scope.PopupSection + $scope.PopupDay + $scope.PopupTime);
                                var subjItemId = $scope.PopupSection + $scope.PopupDay + $scope.PopupTime;
                                if ($(currentColspan).find('#' + subjItemId).length > 0) {
                                    $("#" + subjItemId).attr("data-subject-id", $scope.PopupSubject);
                                    $("#" + subjItemId).html($(popupcontainer + ' #PopupSubject option:selected').text() + '<span ng-click="DeleteTimeTableEntry('+ result.AllocationIIDs +')" class="closeItem" data-Alloc-id="' + result.AllocationIIDs + '">x</span></span>');
                                    $("#" + subjItemId).attr("data-Staff-id", result.StaffIDs);
                                    $("#" + subjItemId).attr("data-Alloc-id", result.AllocationIIDs);
                                    $("#" + subjItemId + " [data-Alloc-id]").attr("data-Alloc-id", result.AllocationIIDs);
                                    $("#" + subjItemId).attr("title", "Subject:" + $(popupcontainer + " #PopupSubject option:selected").text() + "\n" + "Teacher:" + result.StaffIDs);
                                } else {
                                    $(currentColspan).html('<span class="seletedsubject" id="' + subjItemId + '" title="Subject:' + $(popupcontainer + " #PopupSubject option:selected").text() + "\n" + "Teacher:" + result.StaffIDs + '" data-staff-id="' + result.AllocationIIDs + '" data-subject-id="' + $scope.PopupSubject + '" data-Alloc-id="' + result.AllocationIIDs + '" style="background: linear-gradient(to top, #0099ff 0%, #66ffff 46%);">' + $(popupcontainer + " #PopupSubject option:selected").text() + '<span class="closeItem" data-Alloc-id="' + result.AllocationIIDs + '">x</span></span>');
                                }
                                $(popupcontainer).fadeOut("fast");
                                $(".gridItemOverlay").hide();
                                $scope.loadGlobalTimeTable($scope.PopupTimeTableMaster, $scope.PopupClass);
                            }
                        },
                        complete: function (result) {
                            hidePopupOverlay();
                        }
                    });
                }
                else {
                    $.ajax({

                        url: utility.myHost + "Schools/TimeTable/SaveTimeTableLog",
                        type: "POST",
                        data: {
                            "TimeTableId": $scope.PopupTimeTableMaster,
                            "ClassId": $scope.PopupClass,
                            "SectionID": $scope.PopupSection,
                            "WeekDayID": $scope.PopupDay,
                            "ClassTimingID": $scope.PopupTime,
                            "SubjectID": $scope.PopupSubject,
                            "StaffID": $scope.PopupTeacher,
                            "AllocatedDateString": $scope.AllocatedDate,
                            "IsEnteredManually": 1,
                            //"TimeTableAllocationIID":
                        },
                        success: function (result) {
                            if (!result.IsError && result != null) {
                                if (result == "#101") {
                                    alert("This teacher is already assigned for the same time slot")
                                    return;
                                }
                                var currentColspan = $("#colspan" + $scope.PopupSection + $scope.PopupDay + $scope.PopupTime);
                                var subjItemId = $scope.PopupSection + $scope.PopupDay + $scope.PopupTime;
                                if ($(currentColspan).find('#' + subjItemId).length > 0) {
                                    $("#" + subjItemId).attr("data-subject-id", $scope.PopupSubject);
                                    $("#" + subjItemId).html($(popupcontainer + ' #PopupSubject option:selected').text() + '<span ng-click="DeleteTimeTableEntry(' + result +')" class="closeItem" data-Alloc-id="' + result + '">x</span></span>');
                                    $("#" + subjItemId).attr("data-Staff-id", $scope.SelectedTeacher.Key);
                                    $("#" + subjItemId).attr("data-Alloc-id", result);
                                    $("#" + subjItemId + " [data-Alloc-id]").attr("data-Alloc-id", result);
                                    $("#" + subjItemId).attr("title", "Subject:" + $scope.SelectedSubject.SubjectName + "\n" + "Teacher:" + $scope.SelectedTeacher.Value);

                                    var imgTag = $scope.SelectedSubject && $scope.SelectedSubject.IconFileName
                                       ? `<img 
                                       src="../gallery/icons/${$scope.SelectedSubject.IconFileName}" 
                                       alt="icon" 
                                       class="me-2" 
                                       style="width: 20px; height: 20px;">`
                                                                    : '';

                                                                $(currentColspan).html(`
                                <span class="seletedsubject d-flex align-items-center justify-content-between glow-border_edit" 
                                      id="${subjItemId}" 
                                      title="Subject: ${$scope.SelectedSubject.SubjectName}\nTeacher: ${$scope.SelectedTeacher.Value}" 
                                      data-staff-id="${$scope.PopupTeacher}" 
                                      data-subject-id="${$scope.PopupSubject}" 
                                      data-Alloc-id="${result}" 
                                      style="background: #${$scope.SelectedSubject.HexColorCode || '346C9F'}; color: white; padding: 10px; border-radius: 5px;">

                                    ${imgTag}

                                    <!-- Subject and Teacher Details -->
                                    <div class="d-flex flex-column">
                                        <span class="d-flex fw-normal fs-8 text-uppercase p-1" style="color: white;">
                                            ${$scope.SelectedSubject.SubjectName}
                                        </span>
                                        <span class="d-flex fw-light fs-8 text-uppercase p-1" style="color: white;">
                                            ${$scope.SelectedTeacher.Value}
                                        </span>
                                    </div>

                                    <!-- Close Button -->
                                    <span ng-click="DeleteTimeTableEntry(${result})" class="closeItem ms-2 text-white" data-Alloc-id="${result}">&times;</span>
                                </span>
                            `);


                                } else {
                                    $(currentColspan).html(`
                                        <span class="d-flex align-items-center justify-content-between glow-border" 
                                              id="${subjItemId}" 
                                              title="Subject: ${$scope.SelectedSubject.SubjectName}\nTeacher: ${$scope.SelectedTeacher.Value}" 
                                              data-staff-id="${$scope.PopupTeacher}" 
                                              data-subject-id="${$scope.PopupSubject}" 
                                              data-Alloc-id="${result}" 
                                              style="background: #${$scope.SelectedSubject.HexColorCode || '346C9F'}; color: white; padding: 10px; border-radius: 5px;">

                                            <img ng-if="${$scope.SelectedSubject.IconFileName}" 
                                                 src="@imageHostURL/gallery/icons/${$scope.SelectedSubject.IconFileName}" 
                                                 alt="icon" 
                                                 class="me-2" 
                                                 style="width: 20px; height: 20px;">

                                            <!-- Subject and Teacher Details -->
                                            <div class="d-flex flex-column">
                                                <span class="d-flex fw-normal fs-8 text-uppercase p-1" style="color: white;">
                                                    ${$scope.SelectedSubject.SubjectName}
                                                </span>
                                                <span class="d-flex fw-light fs-8 text-uppercase p-1" style="color: white;">
                                                    ${$scope.SelectedTeacher.Value}
                                                </span>
                                            </div>

                                            <!-- Close Button -->
                                            <span ng-click="DeleteTimeTableEntry(${result})" class="closeItem ms-2 text-white" data-Alloc-id="${result}">&times;</span>`);
                                }

                                $(popupcontainer).fadeOut("fast");
                                $(".gridItemOverlay").hide();
                            }
                        },
                        complete: function (result) {
                            hidePopupOverlay();
                        }
                    });
                }

            }
        }


        //------End: Core functionalities or events.

        //To tab open and close
        $scope.TimeTableGenerate = function (buttonName) {
            showOverlay();
            var timeTableMasterID = $scope.selectedTableMasterData;
            var classID = $scope.selectedClasses;
            var date = $scope.AllocatedDate;
            var weekDay = $scope.WeekDay[0].Key;

            if (buttonName == "Generate") {
                $.ajax({
                    url: utility.myHost + "Schools/TimeTable/GenerateTimeTable",
                    type: "POST",
                    data: {
                        "TimeTableMasterID": timeTableMasterID,
                        "ClassID": classID,
                        "AllocatedDateString": date,
                        "IsGenerate": 1,
                        "WeekDayID": weekDay
                    },
                    success: function (result) {
                        //$(currentElement).closest(".colspan").find("span").remove();

                        if (result.operationResult == 2) {
                            $().showGlobalMessage($root, $timeout, true, result.Message);
                            hideOverlay();
                        }
                        else {
                            $scope.TimeTableMappingByDate($scope.AllocatedDate);
                            hideOverlay();
                        }
                    },
                    complete: function (result) {
                        
                    }
                });

            }
            else if (buttonName == "Re_generate") {
                $.ajax({
                    url: utility.myHost + "Schools/TimeTable/GenerateTimeTable",
                    type: "POST",
                    data: {
                        "TimeTableMasterID": timeTableMasterID,
                        "ClassID": classID,
                        "AllocatedDateString": date,
                        "IsGenerate": 0,
                        "WeekDayID": weekDay
                    },
                    success: function (result) {
                        //$(currentElement).closest(".colspan").find("span").remove();
                    },
                    complete: function (result) {
                        //$scope.onTimeTableAllacateTab('Tab_02');
                        $scope.TimeTableMappingByDate($scope.AllocatedDate);

                        hideOverlay();
                    }
                });
            }
            else if (buttonName == "Smart_Scheduler") {
                var timeTableMaster = $scope.selectedTableMasterData;

                if (timeTableMaster == null || timeTableMaster == 'undefined') {
                    alert("Please Select Time Table Type");
                    return;
                }

                $.ajax({
                    url: utility.myHost + "Schools/TimeTable/GenerateSmartTimeTable",
                    type: "POST",
                    data: {
                        "tableMasterId": timeTableMaster,
                        "ClassID": classID,
                        "AllocatedDateString": date,
                        "IsGenerate": 0,
                    },
                    success: function (result) {
                        //$(currentElement).closest(".colspan").find("span").remove();
                        //$scope.onTimeTableAllacateMasterTab("Master_Tab_02");
                        $scope.GetSmartTimeTableDetails(timeTableMaster)
                    },
                    complete: function (result) {
                        hideOverlay();
                    }
                });
            }
        }
        //End tab open and close


        $scope.isVisible = false; 

        $scope.toggleSidebar = function () {
            $scope.isVisible = !$scope.isVisible;

            const element = document.getElementById("smartscheduler");  
            if ($scope.isVisible) {
                element.classList.remove("col-12");
                element.classList.add("col-8");
            }
            else {
                element.classList.remove("col-8");
                element.classList.add("col-12");
            }
        };

        $scope.GetWeekDays = function () {
            $scope.WeekDays = [];
            showOverlay();
            $.ajax({
                type: "GET",
                url: "Schools/TimeTable/GetWeekDays",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.WeekDays = result;
                        });
                    }
                    hideOverlay();
                },
                error: function () {

                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }

        $scope.GetSmartTimeTableDetails = function (tableMasterId, ClassID) {

            showOverlay();

            //if (tableMasterId == 'undefined' || tableMasterId == null) {
            //    if (!$scope.selectedTableMasterData)
            //        return;

            //    tableMasterId = $scope.selectedTableMasterData;
            //}

            if (tableMasterId == 'undefined' || tableMasterId == null) {
                hideOverlay();
                return;
            }

            $scope.selectedClasses = ClassID;

            $scope.GetSmartTimeTableData(tableMasterId, ClassID);
            $scope.GetTeacherSummary(tableMasterId);
            $scope.GetClassSummary(tableMasterId);
            $scope.GetClassSectionTimeTableSummary(tableMasterId);
            $scope.ProgressBar();
            $scope.loadClasswiseSubject();

            hideOverlay();
        }

        $scope.GetSmartTimeTableData = function (tableMasterId,classID) {
            showOverlay();

            if ($scope.AllocatedDate == 'undefined' || $scope.AllocatedDate == null) {
                $scope.AllocatedDate = FormatDate(new Date());
            }

            if (tableMasterId == 0) {
                return;
            }

            //if (TableMasterId == 'undefined' || TableMasterId == null) {
            //    TableMasterId = $scope.TableMasterData[0].Key;
            //}

            $.ajax({
                type: "GET",
                data: { tableMasterId: tableMasterId, timeTableDate: $scope.AllocatedDate, classID: classID },
                url: "Schools/TimeTable/GetSmartTimeTableByDate",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.selectedTableMasterData = tableMasterId;
                            $scope.TimeTableDailyMapping = result;
                            $scope.setDraggable();

                        });
                    }
                    $(".gridItemPopup").fadeOut("fast");
                    $(".gridItemOverlay").hide();
                },
                error: function () {

                },
                complete: function (result) {
                    //$scope.loadClasswiseSubject();
                    $scope.GetWeekDays();

                }
            });
        }

        $scope.GetTeacherSummary = function (tableMasterID) {
            showOverlay();
            $.ajax({
                type: "GET",
                url: "Schools/TimeTable/GetTeacherSummary?tableMasterID=" + tableMasterID,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.TeacherSummary = result;
                            hideOverlay();
                        });
                    }
                },
                error: function () {

                },
            });
        }

        $scope.GetClassSummary = function (tableMasterID) {
            showOverlay();
            $.ajax({
                type: "GET",
                url: "Schools/TimeTable/GetClassSummary?tableMasterID=" + tableMasterID,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.ClassSummary = result;
                            hideOverlay();
                        });
                    }
                },
                error: function () {

                },
            });
        }

        $scope.GetClassSectionTimeTableSummary = function (tableMasterID) {
            showOverlay();
            $.ajax({
                type: "GET",
                url: "Schools/TimeTable/GetClassSectionTimeTableSummary?tableMasterID=" + tableMasterID,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.ClassSectionTimeTable = result;
                            hideOverlay();
                        });
                    }
                },
                error: function () {
                    hideOverlay();
                },
            });
        };

        $scope.ViewDetails = function (firstID,secondID,gridName) {

            event.preventDefault();
            $("[data-original-title]").popover('dispose');

            $(event.currentTarget).popover({
                container: 'body',
                placement: 'left',
                html: true,
                content: function () {
                    return '<label></label>';
                }

            });

            const tableMasterID = $scope.selectedTableMasterData;

            $(event.currentTarget).popover('show');

            if (gridName == 'TeacherSummary') {
                $scope.GetTeacherSummaryByTeacherID(firstID, tableMasterID);
                var htmlContent = $('#timetableTeacherPopover').html();
            }
            else if (gridName == 'ClassSummary')
            {
                $scope.GetClassSummaryDetails(firstID, secondID, tableMasterID);
                var htmlContent = $('#timetableClassPopover').html();
            }
            var content = $compile(htmlContent)($scope);
            $('#' + $(event.currentTarget).attr('aria-describedby')).find('.popover-body').html(content);
        }

        $scope.HideDataHistory = function () {
            event.preventDefault();
            $(".popover").popover('dispose');

        }

        $scope.GetTeacherSummaryByTeacherID = function (employeeID, tableMasterID) {
            showOverlay();
            $.ajax({
                type: "GET",
                data: { employeeID: employeeID },
                url: "Schools/TimeTable/GetTeacherSummaryByTeacherID?tableMasterID=" + tableMasterID,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.TeacherTimeTableList = result;
                            hideOverlay();
                        });
                    }
                },
                error: function () {
                    hideOverlay();
                },
            });
        };

        $scope.GetClassSummaryDetails = function (classID, sectionID, tableMasterID) {
            showOverlay();
            $.ajax({
                type: "GET",
                data: { classID: classID, sectionID : sectionID },
                url: "Schools/TimeTable/GetClassSummaryDetails?tableMasterID=" + tableMasterID,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.ClassTimeTableList = result;
                            hideOverlay();
                        });
                    }
                },
                error: function () {
                    hideOverlay();
                },
            });
        };

        $scope.customSearchFilter = function (classSummary) {
            if (!$scope.searchQuery) {
                return true; // If no search query, show all results
            }
            var query = $scope.searchQuery.toLowerCase();

            return (classSummary.Class.Value.toLowerCase().indexOf(query) !== -1 ||
                classSummary.Section.Value.toLowerCase().indexOf(query) !== -1);
        };

        $scope.customSearchFilterTT = function (ClassSectionTimeTable) {
            if (!$scope.searchTTQuery) {
                return true;
            }
            var query = $scope.searchTTQuery.toLowerCase();

            var hasMatchInMainDetails = ClassSectionTimeTable.MainDetails.some(mainDetail =>
                mainDetail.MapDetails.some(mapDetail =>
                    mapDetail.Class?.Value?.toLowerCase().includes(query) ||
                    mapDetail.Section?.Value?.toLowerCase().includes(query)
                )
            );

            return hasMatchInMainDetails;
        };


        //$scope.DownloadReports = function (className, sectionName) {

        //    var reportName = 'StudentTimetableReport';
        //    var reportHeader = 'Student Timetable Report';

        //    var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        //    var parameter = 'class=' + className + '&' + 'section=' + '&' + 'SchoolID=' + '30';

        //    let commaSeparated = parameter.includes('&') ? parameter.replace(/&/g, ',') : parameter;

        //    // Convert to JSON-like object
        //    let parameterObject = {};
        //    commaSeparated.split(',').forEach(function (param) {
        //        let [key, value] = param.split('=');

        //        if (key.toLowerCase().includes("date")) {
        //            var dateFormat = $root.DateFormat != null ? $root.DateFormat.toUpperCase() : "DD/MM/YYYY";
        //            // Using moment to format the date
        //            value = moment(value).format(dateFormat);
        //        }

        //        parameterObject[key] = value || "";  // Assign empty string if no value exists
        //    });

        //    // Convert to JSON format
        //    let parameterString = JSON.stringify(parameterObject);

        //    var reportUrl = utility.myHost + 'Reports/ReportView/ViewReports?reportName=' + reportName + "&parameter=" + parameterString;
        //    $.ajax({
        //        url: reportUrl,
        //        type: 'GET',
        //        success: function (result) {
        //            $('#' + windowName, '#LayoutContentSection').replaceWith($compile(result)($scope))
        //        }
        //    });
        //};

        $scope.DownloadReports = function (classID, sectionID, schoolID) {

            var format = "pdf";
            //var type = "print"
            var isPrint = true;

            var reportName = 'StudentTimetableReport';

            var parameterObject = {
                "SchoolID": schoolID,
                "Class": classID,
                "Section": sectionID
            };

            // Convert to JSON format
            let parameterString = JSON.stringify(parameterObject);

            var reportUrl = utility.myHost + "Reports/ReportView/Show?reportName=" + reportName + "&format=" + format + "&parameters=" + parameterString + "&isPrint=" + isPrint;
            const pdfWindow = window.open(reportUrl);
            pdfWindow.print();
        };

        $scope.ProgressBar = function () {
            $timeout(function () {
                $('[id^="progress-"]').each(function () {
                    const allocatedPeriods = parseInt($(this).attr('data-value'), 10);
                    const weekPeriods = parseInt($(this).attr('data-total'), 10);
                    let percent = (allocatedPeriods / weekPeriods) * 100;

                    let exceeded = allocatedPeriods > weekPeriods;

                    if (!exceeded) {
                        $(this).progress({
                            percent: percent,
                            text: {
                                active: allocatedPeriods + '/' + weekPeriods,
                                success: allocatedPeriods + '/' + weekPeriods
                            }
                        });
                    } else {
                        $(this).find('.bar').css('width', '100%');
                        $(this).find('.label').text(allocatedPeriods + '/' + weekPeriods);
                    }

                    if (exceeded) {
                        $(this).removeClass('red green success').addClass('orange progress');
                    } else if (allocatedPeriods < weekPeriods) {
                        $(this).removeClass('green orange').addClass('red');
                    } else if (allocatedPeriods === weekPeriods) {
                        $(this).removeClass('red orange').addClass('green');
                    }
                });
            }, 3000);
        };


        $scope.ReAssignTimeTable = function () {

            showOverlay();

            var timeTableMasterID = $scope.selectedTableMasterData;
            var classID = $scope.selectedClasses;
            var date = $scope.AllocatedDate;

            $.ajax({
                url: utility.myHost + "Schools/TimeTable/ReAssignTimeTable",
                type: "POST",
                data: {
                    "tableMasterId": timeTableMasterID,
                    "classID": classID,
                    "timeTableDate": date,
                },
                success: function (result) {
                    if (result.operationResult == 2) {
                        $().showGlobalMessage($root, $timeout, true, result.Message);
                        hideOverlay();
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, false, result.Message);

                        $timeout(function () {
                            $scope.$apply(function () {
                                $scope.LoadDailyTimeTable(date);
                            });
                        }, 0);
                        hideOverlay();
                    }
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }

        $scope.ClearDatas = function () {

            $scope.TimeTableMapping = null;
            $scope.TimeTableDaily = null;
            $scope.selectedTableMasterData = null;
            $scope.selectedClasses = null;
            $scope.TimeTableDailyMapping = null;
            $scope.Subjects = null;
            $scope.Screens = [];
        }

        $scope.GetWeekDayByDate = function () {

            showOverlay();

            var date = $scope.AllocatedDate;

            $.ajax({
                url: utility.myHost + "Schools/TimeTable/GetWeekDayByDate",
                type: "POST",
                data: {
                    "date": date,
                },
                success: function (result) {
                    //$(currentElement).closest(".colspan").find("span").remove();

                    var weekDay = result;
                    $scope.$apply(function () {
                        $scope.WeekDay = result;
                    });
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }

        $scope.GetAllWeekDays = function () {
            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=WeekDay&defaultBlank=false",
            }).then(function (result) {
                $scope.WeekDay = result.data;
            });
        }

        $scope.GetSetupScreens = function () {
            showOverlay();

            $.ajax({
                url: utility.myHost + "Schools/TimeTable/GetSetupScreens",
                type: "GET",
                success: function (result) {
                    $scope.$apply(function () {
                        result.forEach(x => {
                            var params = x.Value.split(',');
                            $scope.Screens.push({
                                "MenuName": params[3],
                                "Link": x.Value,
                            });
                        });
                    });
                },
                complete: function (result) {
                    hideOverlay();
                }

            });

            hideOverlay();
        }

        $scope.GetScreen = function ($event, parameters) {
            $root.FireEvent($event, parameters, null)
        }

        $scope.$watch('searchTeacher', function (newValue, oldValue) {
            if (newValue !== oldValue) {
                $timeout(function () {
                    $scope.ProgressBar();
                }, 300);
            }
        });

        $scope.$watch('searchQuery', function (newValue, oldValue) {
            if (newValue !== oldValue) {
                $timeout(function () {
                    $scope.ProgressBar();
                }, 300);
            }
        });


    }]);

