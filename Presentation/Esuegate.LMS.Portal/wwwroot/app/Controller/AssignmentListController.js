app.controller("AssignmentListController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("AssignmentListController Loaded");

    $scope.GroupedAssignmentList = {};
    $scope.colors = ['#000000', '#00ff00', '#0000ff'];
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

        $scope.getAssignmentList(defaultStudentID);
    };




    $scope.getAssignmentList = function (studentId) {
        //showOverlay();
        $.ajax({
            type: "GET",
            data: { studentID: studentId },
            url: utility.myHost + "Home/GetAssignmentStudentwise",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.AssignmentListBySubject = result.Response;
                        $scope.groupAssignmentsBySubject();

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

  
    $scope.groupAssignmentsBySubject = function () {
        $scope.GroupedAssignmentList = $scope.AssignmentListBySubject.reduce(
            (acc, item) => {
                const subjectKey = item.Subject.Key;
                acc[subjectKey] = acc[subjectKey] || {
                    Subject: item.Subject,
                    Assignments: []
                };
                acc[subjectKey].Assignments.push(item);
                return acc;
            },
            {}
        );
    };



    $scope.AssignmentViewClick = function (Assignment) {
        window.location.replace(utility.myHost + "Lms/Assignment?AssignmentID=" + Assignment.AssignmentIID);
    }

    $scope.Init();
}]);