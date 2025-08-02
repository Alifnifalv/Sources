app.controller("LessonplanListController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("LessonplanListController Loaded");

    $scope.GroupedLessonPlanList = {};
    $scope.colors = ['#57A886', '#5E94D4', '#65A8B7'];

    function showOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#MeetingRequestListOverlay").fadeIn();
            });
        });
    }

    function hideOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#MeetingRequestListOverlay").fadeOut();
            });
        });
    }

    $scope.Init = function () {
        $scope.ShowPreLoader = true;
        const defaultStudentID = localStorage.getItem('defaultStudentID');

        $scope.getLessonPlanList(defaultStudentID);
    };

    $scope.NewRequestButtonClick = function () {
        window.location.href = utility.myHost + "SignUp/MeetingRequest";
    };

    $scope.GetUserDetails = function () {
        $.ajax({
            url: utility.myHost + "Home/GetUserDetails",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $timeout(function () {
                        $scope.$apply(function () {

                            $scope.UserDetails = result.Response;
                            $scope.ParentID = $scope.UserDetails != null && $scope.UserDetails.Parent != null ? $scope.UserDetails.Parent.ParentIID : null;
                        });
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
            url: utility.myHost + "Lms/GetLessonPlanListBySubject",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.LessonPlanListBySubject = result.Response;
                        $scope.groupLessonPlansBySubject();

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

    $scope.studentsubjectlist = function (studentId) {


        $scope.TeacherDetails = [];
        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: utility.myHost + "Lms/Getstudentsubjectlist",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.LessonPlanListBySubject = result.Response;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {

            }
        });
    };

    $scope.groupLessonPlansBySubject = function () {
        $scope.GroupedLessonPlanList = $scope.LessonPlanListBySubject.reduce(
            (acc, item) => {
                const subjectKey = item.Subject.Key;
                if (!acc[subjectKey]) {
                    acc[subjectKey] = { Subject: item.Subject, LessonPlans: [] };
                }
                acc[subjectKey].LessonPlans = acc[subjectKey].LessonPlans.concat(
                    item.LessonPlans
                );
                return acc;
            },
            {}
        );
    };

    $scope.getBackgroundColor = function (index) {
        var rotatedIndex = (index + 3) % 3; // Rotate colors starting from the fourth one
        return $scope.colors[rotatedIndex];
    };

    $scope.LessonPlanViewClick = function (lessonPlan) {
        window.location.replace(utility.myHost + "Lms/Lessonplan?LessonPlanID=" + lessonPlan.LessonPlanIID);
    }

    $scope.Init();
}]);