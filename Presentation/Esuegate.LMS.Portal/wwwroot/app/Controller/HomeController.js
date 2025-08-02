app.controller('HomeController', ['$scope', '$http', '$compile', "$rootScope",  "$timeout", function ($scope, $http, $compile, $rootScope, $root, $timeout) {
    console.log('HomeController controller loaded.');


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

  
    $scope.Init = function (layout) {
  
                        $scope.initializeStudents(); // Initialize after fetching students
      


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
  

        $scope.getLessonPlanList(defaultStudentID);
        $scope.getAssignments(defaultStudentID);
        $scope.studentsubjectlist(defaultStudentID);

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
                $scope.getLessonPlanList(studentID);
                $scope.getAssignments(studentID);
                $scope.studentsubjectlist(studentID);
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
                    $("#getStudentAttendancebtn").click();
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
                    $("#getStudentAttendancebtn").click();
                },
                error: function () {

                },
                complete: function (result) {
                    //hideOverlay();
                }
            });
        }

        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "Home/UserApplications",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.StudentDetailsFull = result.Response[0];
                    });
                }
                $("#getStudentAttendancebtn").click();
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.GetActiveCourses = function () {
        $.ajax({
            url: utility.myHost + "SignUp/GetActiveCourses",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $scope.$apply(function () {
                        $scope.SignUpGroupDetails = result.Response;

                        $scope.ShowPreLoader = false;
                    });
                }
            }
        });
    };


    $scope.getLessonPlanList = function (studentId) {
        //showOverlay();
        $.ajax({
            type: "GET",
            data: { studentID: studentId },
            url: utility.myHost + "Home/GetLessonPlanList",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.LessonPlanList = result.Response;
                    }
                });
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }


    $scope.getAssignments = function (studentId, Subject, page) {
        if ($scope.IsAssignmentOpened && table) {
            table.destroy();
        }
        $scope.IsAssignmentOpened = true;

        var subjectId = Subject != null ? Subject.Key : null;

        $scope.currentPage = page || 1; // Set current page from parameter or default to 1
        $.ajax({
            type: "GET",
            data: { studentId: studentId, SubjectID: subjectId, page: $scope.currentPage, pageSize: $scope.pageSize },
            url: utility.myHost + "Home/GetAssignmentStudentwise",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result.Response) {
                    $scope.$apply(function () {
                        $scope.AssignmentList = result.Response;
                        $scope.totalPages = result.TotalPages; // Get total pages from the server response
                        $scope.updatePageNumbers();
                    });
                }
            },
            error: function () {
                console.error("Error while fetching assignments.");
            },
            complete: function () {
                // Any cleanup actions
            }
        });
    };

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


    $scope.studentsubjectlist = function (studentId) {


        $scope.TeacherDetails = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "Home/Getstudentsubjectlist",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.studentsubjectlist = result.Response;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
       
            }
        });
    };

    $scope.Init();
}]);