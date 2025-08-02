app.controller('LayoutController', ['$scope', '$http', '$compile', "$rootScope",  "$timeout", function ($scope, $http, $compile, $rootScope, $root, $timeout) {
    console.log('LayoutController controller loaded.');

    $scope.WindowTabs = [];
    $scope.SelectedWindowIndex = 0;
    $scope.AlertCount = 0;
    $scope.MenuItems = [];
    var _menuItemsCache = [];
    $scope.layout = null;
    $scope.WindowCount = 0;
    $scope.Students = [];
    $scope.SelectedStudent = [];
    $scope.StudentDetails = null;
    $scope.ParentDetails = null;
    $scope.Students = [];
    $scope.StudentsAttendence = [];
    $scope.SelectedStudent = [];
    $scope.FeeMonthly = [];
    $scope.FeeMonthlyHis = [];
    $scope.FeeTypes = [];
    $scope.FeeTypeHis = [];
    $scope.StudentDetails = null;
    $scope.ParentDetails = null;
    $scope.StudentLeaveApplication = null;
    $scope.FineDues = [];
    $scope.FineHis = [];
    $scope.ReasonData = [];
    $scope.CircularList = [];
    $scope.AssignmentList = [];
    $scope.WeekDay = {};
    $scope.ClassTime = {};
    $scope.StudentName = {};
    $scope.Class = {};
    $scope.Section = {};
    $scope.ClassId = {};
    $scope.StudentDetailsFull = null;
    $scope.activeStudentID = 0;
    $scope.activeStudentClassID = 0;
    $scope.activeStudentSectionID = 0;
    $scope.StudentTransportDetails = {};
    $scope.isStudentTransport = false;
    var table;

    $scope.totalPages = 1; // Initialize totalPages
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.pageNumbers = [];

    $scope.StudentMarkList = [];
    $scope.ExamsList = [];
    $scope.ProgressReportGraphLabel = [];
    $scope.ProgressReportDataSet = [];
    $scope.isOpenProgressReportGraph = false;
    $scope.ShowPreLoader = false;
    $scope.ProgressReportURL = "";

    function SetAlertCount(count) {
        $scope.$apply(function () {
            $scope.AlertCount = count;
        });
    }

    $scope.ShowClose = function (index) {
        if (index == 0) return false;
        return true; 
    }
    $scope.isClicked = false;
    $scope.value = 'month';

    $scope.showDatePicker = function () {
        if ($scope.value == 'custom') {
            $scope.isClicked = true;
        } else {
            $scope.isClicked = false;
        }
    }
    $scope.Init = function (layout) {
        $scope.layout = layout;
        $scope.WindowTabs.push({ Index: 0, Name: 'Dashbaord', Title: 'Dashbaord', Container: 'DashbaordWindow' });
        $.ajax({
            type: "GET",
            data: { parentId: 60 },
            url: utility.myHost + "Home/GetStudentsSiblings",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.Students = result.Response;
                        $scope.initializeStudents(); // Initialize after fetching students
                    });
                } else if (result == null) {
                    window.location.href = "Account/LogIn";
                }
            },
            error: function () {
                console.error("Failed to fetch students.");
            }
        });


        $scope.changeurl = function () {
            var currentLocation = location.protocol + '//' + location.host + location.pathname;
            //alert(currentLocation);
            const nextURL = 'https://my-website.com/page_b';
            const nextTitle = 'My new page title';
            const nextState = { additionalInformation: 'Updated the URL with JS' };

            // This will create a new entry in the browser's history, without reloading
            window.history.pushState("", "", event.target.currentLocation);
            event.preventDefault();
        }
        $scope.ShowPreLoader = true;


    }
    angular.element(document).ready(function () {
        $(document).on('click', '.otherCandidates', function () {
            $scope.$apply(function () {
                var element = angular.element('#Activities');
                element.addClass('Active');
            });
            var currentElement = $(this);
            var studentID = $(this).attr('id');
            $scope.activeStudentID = studentID;

            $("#CandidateIDfromOutSide").val(studentID);
            $scope.onOtherCandidatesclick(studentID);
            //$scope.ProgressReport(studentID);
            //$scope.getstudentTransportDetails();
            //$("#StudentProfile").show();
        });

    });

    $scope.initializeStudents = function () {
        const defaultStudentID = localStorage.getItem('defaultStudentID');
        if (defaultStudentID) {
            $scope.activeStudentID = Number(defaultStudentID);

            // Loop through students and set the default student as active
            $.each($scope.Students, function (index, objModel) {
                if (objModel.StudentIID === $scope.activeStudentID) {
                    objModel.IsSelected = true;
                    $scope.SelectedStudent = {
                        "ClassID": "" + objModel.ClassID + "",
                        "ClassName": "" + objModel.ClassName + "",
                        "SectionID": "" + objModel.SectionID + "",
                        "SectionName": "" + objModel.SectionName + "",
                        "StudentID": "" + objModel.StudentIID + "",
                        "StudentName": "" + objModel.FirstName + ' ' + objModel.MiddleName + ' ' + objModel.LastName + ""
                    };
                    $scope.activeStudentClassID = objModel.ClassID;
                    $scope.activeStudentSectionID = objModel.SectionID;

                    // Load data for the default student
                    $scope.studentProfile($scope.activeStudentID);
                    //$scope.getLessonPlanList($scope.activeStudentID);
                    //$scope.getAssignments($scope.activeStudentID);
                    //$scope.studentsubjectlist($scope.activeStudentID);
                } else {
                    objModel.IsSelected = false;
                }
            });
        } else {
            // If no defaultStudentID, fallback to the first student in the list
            if ($scope.Students && $scope.Students.length > 0) {
                $scope.onOtherCandidatesclick($scope.Students[0].StudentIID);
            }
        }
    };


    //on other candidates click function
    $scope.onOtherCandidatesclick = function (studentID) {
        if (studentID !== null && studentID > 0) {
            $scope.activeStudentID = studentID;

            $scope.$apply(function () {
                $.each($scope.Students, function (index, objModel) {
                    if (objModel.StudentIID === Number(studentID)) {
                        objModel.IsSelected = true;
                        $scope.SelectedStudent = {
                            "ClassID": "" + objModel.ClassID + "", "ClassName": "" + objModel.ClassName + "",
                            "SectionID": "" + objModel.SectionID + "", "SectionName": "" + objModel.SectionName + "",
                            "StudentID": "" + objModel.StudentIID + "", "StudentName": "" + objModel.FirstName + ' ' + objModel.MiddleName + ' ' + objModel.LastName + ""
                        };
                        $scope.activeStudentClassID = objModel.ClassID;
                        $scope.activeStudentSectionID = objModel.SectionID;

                        // Update the defaultStudentID in localStorage
                        localStorage.setItem('defaultStudentID', studentID);
                    } else {
                        objModel.IsSelected = false;
                    }
                });

                // Load data for the newly selected student
                $scope.studentProfile(studentID);
                window.location.href = utility.myHost + "";

                //$scope.getLessonPlanList(studentID);
                //$scope.getAssignments(studentID);
                //$scope.studentsubjectlist(studentID);
            });
        }
    };
  $scope.studentProfile = function (studentId) {

        //showOverlay();
        $scope.StudentDetails = [];
        $scope.ParentDetails = [];
        $scope.StudentDetailsFull = [];

        if (studentId) {
            $.ajax({
                type: "GET",
                data: { studentId: studentId },
                url: utility.myHost + "Home/GetStudentDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.StudentDetails = result.Response[0];
                        });
                    }
                },
                error: function () {

                },
                complete: function (result) {
                    //hideOverlay();
                }
            });

            $.ajax({
                type: "GET",
                data: { studentId: studentId },
                url: utility.myHost + "Home/GetGuardianDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.ParentDetails = result.Response;
                        });
                    }
                },
                error: function () {

                },
                complete: function (result) {
                    //hideOverlay();
                }
            });
        }

        }



 

    $scope.FilterMenu = function (event) {
        var filteredMenu = new JSLINQ(_menuItemsCache)
            .Where(function (item) {
                return item.Hierarchy.toUpperCase().indexOf($root.MenuSearchText.toUpperCase()) >= 0;
            }).items;

        $scope.MenuItems = [];
        $.each(filteredMenu, function (index, value) {
            $scope.MenuItems.push(value);
        });
    };

    $scope.ShowWindow = function (name, title, container) {
        var item = $.grep($scope.WindowTabs, function (e) { return e.Name == name; });
        if (item.length == 0)
            return false;
        else {
            $scope.SelectedWindowIndex = item[0].Index;
            var itemContainer = $("#" + item[0].Container);
            itemContainer.attr('windowindex', $scope.SelectedWindowIndex);
            $(".windowcontainer").slideUp(100);
            $("#" + item[0].Container).slideDown(100);
            return true;
        }
    };

    $scope.AddWindow = function (name, title, container) {
        $scope.WindowCount = $scope.WindowCount + 1;
        var item = $.grep($scope.WindowTabs, function(e){ return e.Name == name; });

        if (item.length == 0) {
            $scope.WindowTabs.push({ Index: $scope.WindowTabs.length, Name: name, Title: title, Container: container });
            $scope.SelectedWindowIndex = $scope.WindowTabs.length - 1;
        }
        else
        {
            $scope.SelectedWindowIndex = item[0].Index;
        }

        var item = $.grep($scope.WindowTabs, function (e) { return e.Name == name; });
        $(".windowcontainer").hide();
        var container = $("#" + item[0].Container);
        container.attr('windowindex', $scope.SelectedWindowIndex);

        if (container.length == 0) {
            $("#LayoutContentSection").append("<div id='" + item[0].Container + "' class='windowcontainer' style='display:block;color:white;'><center><div class='bounce-wrapper' id='Load'><div class='three-bounce-inner'><div class='bounce-item bi-item1'></div><div class='bounce-item bi-item2'></div><div class='bounce-item bi-item3'></div></div></div></center></div>");
        }

        container.slideDown('slow');
        return item[0].Container;
    }

    $scope.SelectWindowTab = function (event, index) {
        if ($scope.SelectedWindowIndex == index) return;
        $scope.SelectedWindowIndex = index;
        $(".windowcontainer").hide();
        $("#" + $scope.WindowTabs[index].Container).show();
    }

    $scope.CloseWindowTab = function (index) {

        if (index >= $scope.WindowTabs.length) {
            index = $scope.WindowTabs.length - 1;
        }

        var window = $('#' + $scope.WindowTabs[index].Container);
        window.hide();
        window.html('');
        window.remove();
        $scope.WindowTabs.splice(index, 1);

        if ($scope.SelectedWindowIndex >= index)
            $scope.SelectWindowTab(null, $scope.SelectedWindowIndex - 1);

        var topmenucontainer = $("body").find(".topmenuwrap-inner").outerWidth();
        var itemwidth = 0;
        $('ul.bodyrightmain-tab').children().each(function () {
            itemwidth += $(this).outerWidth();
            // change to .outerWidth(true) if you want to calculate margin too.
        });
        if (itemwidth < topmenucontainer) {
            $('.btncontrols-wrap').hide();
        }

        $scope.WindowCount = $scope.WindowCount - 1;
    };

    $scope.CloseSmartWindow = function (event) {
        $scope.CloseWindowTab($scope.WindowCount);
    };

    $scope.CloseWindow = function (event) {
        var window = $(event.currentTarget).closest('.windowcontainer');
        $scope.CloseWindowTab(window.attr('windowindex'));        
    };

    $scope.ChangePassword = function (event) {
        event.stopPropagation();
        var offs = $('.header').offset();
        var yposition = event.pageY - offs.top;
        $('.transparent-overlay').show();
        $('.popup.gridpopupfields').slideDown("fast");
        $('.popup.gridpopupfields').addClass('fixedpos');
        $('.popup.gridpopupfields').css({ 'top': yposition });
        $('.myprofile-dropdown').slideUp('fast');
        

        $('#globalPopupContainer').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

        $.ajax({
            url: "Account/ResetPassword",
            type: 'GET',
            success: function (content) {
                $('#globalPopupContainer').html($compile(content)($scope));
                $('#globalPopupContainer').removeClass('loading');
                $('#globalPopupContainer').addClass('loaded');
            }
        });
    }


    $scope.ClosePopup = function (event) {
        $(event.currentTarget).hide();
        $('.popup.gridpopupfields').fadeOut("fast");
        setTimeout(function (e) {
            $('.popup.gridpopupfields').css('top', '');
            $('.popup.gridpopupfields').removeClass('fixedpos');
        }, 200);
        $('.popup.gridpopupfields', $(windowContainer)).removeAttr('data-list');
        $('#popupContainer', $(windowContainer)).html('');
        //$('.statusview').removeClass('active');
    }

    $rootScope.ShowPopup = function (currentTarget, windowContainer) {
        var popdetect = $(currentTarget).attr('data-popup-type')
        $('.preload-overlay', $(windowContainer)).show()
        var xpos = $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).outerWidth() / 2
        var ypos = $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).outerHeight() / 2
        $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).css({ 'margin-top': '-' + ypos + 'px', 'margin-left': '-' + xpos + 'px' })
        $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).fadeIn(500)
        $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).addClass('show')

        $('.popup', $(windowContainer)).on('click', 'span.close', function () {
            $(this).closest('.popup').fadeOut(500);
            $(this).closest('.popup').removeClass('show cropPopup');
            $('.preload-overlay', $(windowContainer)).fadeOut(500);
            $(".dynamicPopoverOverlay, .snapshotOverlay").hide();
            $("input.UploadFile").val('');
        })

        $('.popupbtn a', $(windowContainer)).on('click', function (e) {
            e.preventDefault()
        })

        $('.preload-overlay', $(windowContainer)).on('click', function () {
            $('.popup', $(windowContainer)).removeClass('show')
            $('.popup', $(windowContainer)).fadeOut(500)
            $('.preload-overlay', $(windowContainer)).fadeOut(500);
        })
    }


    $scope.LessonTabClick = function () {
        window.location.href = utility.myHost + "Home/MyLessonPlans";
    };

    $scope.AssignmentClick = function () {
        window.location.href = utility.myHost + "Home/MyAssignments";
    };
    $scope.Signout = function () {
        $.ajax({
            url: utility.myHost + "Account/Signout",
            type: "GET",
            success: function (result) {
                setTimeout(preventBack, 0);
                Logout();
                utility.redirect(utility.myHost + "Account/LogIn");
            }
        });
    };

    //Clear History
    function preventBack() {
        window.history.forward(-1);
    }

    function Logout() {
        try {
            Android.Logout();
        } catch (e) {
        }
    }



    $scope.updatePageNumbers = function () {
        $scope.pageNumbers = [];
        const numPagesToShow = 7;
        const midpoint = Math.floor(numPagesToShow / 2);
        let startPage = Math.max(1, $scope.currentPage - midpoint);
        let endPage = Math.min($scope.totalPages, startPage + numPagesToShow - 1);

        // Adjust start and end if boundaries are hit
        if (startPage === 1) {
            endPage = Math.min(numPagesToShow, $scope.totalPages);
        } else if (endPage === $scope.totalPages) {
            startPage = Math.max(1, $scope.totalPages - numPagesToShow + 1);
        }

        for (let i = startPage; i <= endPage; i++) {
            $scope.pageNumbers.push(i);
        }
    };

    $scope.gotoPage = function (page) {
        if (page < 1 || page > $scope.totalPages || page === $scope.currentPage) {
            return; // Prevent invalid navigation
        }
        $scope.getAssignments($scope.SelectedStudent.StudentID, $scope.SelectedSubject, page);
    };




   

    $scope.LessonPlanTabClick = function () {
        window.location.href = utility.myHost + "Lms/LessonplanList";
    };

    $scope.AssignmentTabClick = function () {
        window.location.href = utility.myHost + "Lms/AssignmentList";
    };

    $scope.CalenderTabClick = function () {
        window.location.href = utility.myHost + "Lms/Event";
    };
    
}]);