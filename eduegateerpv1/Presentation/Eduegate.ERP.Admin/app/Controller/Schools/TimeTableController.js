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
        $scope.PopupTeacher = [];
        $scope.TabName = "Tab_01";
        $scope.AllocatedDate == null;

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
            if (tabId == "Tab_01") {
                $scope.TabName = "Tab_01";
                $("#TimeTableAllacate_timetablesubjects_Tab_01").show();
                $("#TimeTableAllacate_Tab_01").show();

                $("#TimeTableAllacate_timetablesubjects_Tab_02").hide();
                $("#TimeTableAllacate_Tab_02").hide();

                $("#datedaily").hide();
                $("#AllocatedDateController").hide();

                $("#Tab_01_nav").toggleClass('active');
                $("#Tab_02_nav").toggleClass('active');
            }
            else if (tabId == "Tab_02") {
                $scope.TabName = "Tab_02";
                $("#TimeTableAllacate_timetablesubjects_Tab_02").show();
                $("#TimeTableAllacate_Tab_02").show();

                $("#TimeTableAllacate_timetablesubjects_Tab_01").hide();
                $("#TimeTableAllacate_Tab_01").hide();

                $("#datedaily").show();
                $("#AllocatedDateController").show();

                $("#Tab_02_nav").toggleClass('active');
                $("#Tab_01_nav").toggleClass('active');

                $scope.AllocatedDate = moment(new Date()).format(_dateFormat.toUpperCase());
                $scope.TimeTableMappingByDate($scope.AllocatedDate);
            }
        }
        //End tab open and close

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
                                $item.append('<span class="closeItem" data-Alloc-id="0">x</span>');
                                $scope.LoadPopup('.gridItemPopup', event);
                                $scope.FillPopup($(this));
                                $(".gridItemPopup").show();
                            }
                        }
                    });
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
                                $item.append('<span class="closeItem" data-Alloc-id="0">x</span>');
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
                $scope.PopupClass = $scope.selectedClasses.Key;
                $scope.PopupTimeTableMaster = $scope.selectedTableMasterData.Key;
                $scope.PopupSection = $(currentelement).parents(".section").attr("data-section-id");
                $scope.PopupDay = $(currentelement).parents(".WeekDay").attr("data-week-id");
                $scope.PopupTime = $(currentelement).parents(".classtime").attr("data-time-id");
                $scope.PopupSubject = $(currentelement).find(".seletedsubject").attr("data-subject-id");
                $scope.PopupAllocId = $(currentelement).find(".seletedsubject").attr("data-Alloc-id");

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
            $(document).on('click', '.closeItem', function () {
                var currentElement = $(this);
                var timeTableAlocID = $(this).attr('data-Alloc-id');

                if (timeTableAlocID != null && timeTableAlocID > 0) {
                    if (window.confirm("Do you want to delete subject?") == false) {
                        return;
                    }
                    var tabid = $scope.TabName;
                    showOverlay();
                    if (tabid == "Tab_01") {
                        $.ajax({
                            url: utility.myHost + "Schools/TimeTable/DeleteTimeTableEntry",
                            type: "POST",
                            data: { "timeTableAllocationID": timeTableAlocID },
                            success: function (result) {
                                $(currentElement).closest(".colspan").find("span").remove();
                            },
                            complete: function (result) {
                                hideOverlay();
                            }
                        });
                    }
                    else {
                        $.ajax({
                            url: utility.myHost + "Schools/TimeTable/DeleteDailyTimeTableEntry",
                            type: "POST",
                            data: { "timeTableLogID": timeTableAlocID },
                            success: function (result) {
                                $(currentElement).closest(".colspan").find("span").remove();
                            },
                            complete: function (result) {
                                hideOverlay();
                            }
                        });

                    }
                }
            });

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
                $scope.PopupClass = $scope.selectedClasses.Key;
                $scope.PopupTimeTableMaster = $scope.selectedTableMasterData.Key;
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
                    $scope.PopupSubject = 0;
                    $scope.PopupTeacher = [];
                    $scope.PopupAllocId = 0;
                    $scope.Teachers = null;
                }
            });
        }

        //------Start: Core functionalities or events.



        $scope.loadClasswiseTimeTable = function (selected) {
            if (selected.ngModel.$name == "TimeTable") {
                $scope.selectedTableMasterData = selected.selected;
            }
            if (selected.ngModel.$name == "Class") {
                $scope.selectedClasses = selected.selected;
            }
            var tableMasterId = $scope.selectedTableMasterData?.Key;
            var classId = $scope.selectedClasses?.Key;
            if (classId == null || classId == 0) {
                $scope.TimeTableMapping = null;
                $scope.TimeTableDaily = null;
                return false;
            }
            if (tableMasterId == null || tableMasterId == 0) {
                alert("Please Select Time Table Type");
                $scope.TimeTableMapping = null;
                $scope.TimeTableDaily = null;
                return false;
            }
            var tabid = $scope.TabName;

            showOverlay();
            if (tabid == "Tab_01") {
                $scope.loadGlobalTimeTable(tableMasterId, classId);
            }
            else {
                if ($scope.AllocatedDate == 'undefined' || $scope.AllocatedDate == null)
                    $scope.AllocatedDate = new Date();
                $scope.TimeTableMappingByDate($scope.AllocatedDate);
            }
        }

        $scope.LoadDailyTimeTable = function (date) {
            $scope.TimeTableMappingByDate(date);
        }

        $scope.TimeTableMappingByDate = function (selectedDate) {
            var tableMasterId = $scope.selectedTableMasterData?.Key;
            var classId = $scope.selectedClasses?.Key;

            if (classId == null || classId == 0) {
                $scope.TimeTableMapping = null;
                $scope.TimeTableDaily = null;
                $().showGlobalMessage($root, $timeout, true, "Please select the class and section");
                return false;
            }

            if (tableMasterId == null || tableMasterId == 0) {
                $().showGlobalMessage($root, $timeout, true, "Please select a time table type!!");
                $scope.TimeTableMapping = null;
                $scope.TimeTableDaily = null;
                return false;
            }

            showOverlay();

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
            var tableMasterId = $scope.selectedTableMasterData?.Key;
            var classId = $scope.selectedClasses?.Key;
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
            var cls = $scope.selectedClasses.Key;
            showOverlay();
            $.ajax({
                type: "GET",
                data: { classId: cls },
                url: "Schools/TimeTable/GetSubjectByClassID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        result.forEach(x => {
                            $scope.Subjects.push({
                                "Key": x.Key,
                                "Value": x.Value,
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
            var cls = $scope.selectedClasses.Key;
            
            if (selectedItem != null) {
                $scope.PopupSubject = selectedItem.selected.Key;
            }

            if ($scope.PopupSubject != null || $scope.PopupSubject != undefined) {
                $scope.SelectedSubject = $scope.Subjects.find(x => x.Key == $scope.PopupSubject);
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
            selectedItem.selected.forEach(x => {
                $scope.SelectedTeacher.push({
                        "Key": x.Key,
                        "Value": x.Value,
                    });
            });
            $scope.PopupTeacher = selectedItem.selected;
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
            $scope.submitted = true;
            if ($scope.PopupSubject == null || $scope.PopupSubject == 0 || $scope.PopupTeacher == null || $scope.PopupTeacher == []) {
                alert("Please fill out required fields!");
                return false;
            } else {
                showPopupOverlay();

                if (tabid == "Tab_01") {
                    
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
                            "StaffList": $scope.PopupTeacher
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
                                    $("#" + subjItemId).html($(popupcontainer + ' #PopupSubject option:selected').text() + '<span class="closeItem" data-alloc-id="' + result.AllocationIIDs + '">x</span></span>');
                                    $("#" + subjItemId).attr("data-Staff-id", result.StaffIDs);
                                    $("#" + subjItemId).attr("data-Alloc-id", result.AllocationIIDs);
                                    $("#" + subjItemId + " [data-Alloc-id]").attr("data-Alloc-id", result.AllocationIIDs);
                                    $("#" + subjItemId).attr("title", "Subject:" + $(popupcontainer + " #PopupSubject option:selected").text() + "\n" + "Teacher:" + result.StaffIDs);
                                } else {
                                    $(currentColspan).html('<span class="seletedsubject" id="' + subjItemId + '" title="Subject:' + $(popupcontainer + " #PopupSubject option:selected").text() + "\n" + "Teacher:" + result.StaffIDs + '" data-staff-id="' + result.AllocationIIDs + '" data-subject-id="' + $scope.PopupSubject + '" data-alloc-id="' + result.AllocationIIDs + '" style="background: linear-gradient(to top, #0099ff 0%, #66ffff 46%);">' + $(popupcontainer + " #PopupSubject option:selected").text() + '<span class="closeItem" data-alloc-id="' + result.AllocationIIDs + '">x</span></span>');
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
                                    $("#" + subjItemId).html($(popupcontainer + ' #PopupSubject option:selected').text() + '<span class="closeItem" data-alloc-id="' + result + '">x</span></span>');
                                    $("#" + subjItemId).attr("data-Staff-id", $scope.PopupTeacher);
                                    $("#" + subjItemId).attr("data-Alloc-id", result);
                                    $("#" + subjItemId + " [data-Alloc-id]").attr("data-Alloc-id", result);
                                    $("#" + subjItemId).attr("title", "Subject:" + $(popupcontainer + " #PopupSubject option:selected").text() + "\n" + "Teacher:" + $(popupcontainer + " #PopupTeacher option:selected").text());
                                } else {
                                    $(currentColspan).html('<span class="seletedsubject" id="' + subjItemId + '" title="Subject:' + $(popupcontainer + " #PopupSubject option:selected").text() + "\n" + "Teacher:" + $(popupcontainer + " #PopupTeacher option:selected").text() + '" data-staff-id="' + $scope.PopupTeacher + '" data-subject-id="' + $scope.PopupSubject + '" data-alloc-id="' + result + '" style="background: linear-gradient(to top, #0099ff 0%, #66ffff 46%);">' + $(popupcontainer + " #PopupSubject option:selected").text() + '<span class="closeItem" data-alloc-id="' + result + '">x</span></span>');
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
            var timeTableMasterID = $scope.selectedTableMasterData?.Key;
            var classID = $scope.selectedClasses?.Key;
            var date = $scope.AllocatedDate;
            if (buttonName == "Generate") {
                $.ajax({
                    url: utility.myHost + "Schools/TimeTable/GenerateTimeTable",
                    type: "POST",
                    data: {
                        "TimeTableMasterID": timeTableMasterID,
                        "ClassID": classID,
                        "AllocatedDateString": date,
                        "IsGenerate": 1,
                    },
                    success: function (result) {
                        //$(currentElement).closest(".colspan").find("span").remove();
                    },
                    complete: function (result) {
                        hideOverlay();
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
                    },
                    success: function (result) {
                        //$(currentElement).closest(".colspan").find("span").remove();
                    },
                    complete: function (result) {
                        hideOverlay();
                    }
                });
            }
        }
        //End tab open and close

    }]);

