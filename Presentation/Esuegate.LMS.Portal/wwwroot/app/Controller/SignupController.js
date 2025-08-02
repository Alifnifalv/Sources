app.controller("SignupController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("SignupController Loaded");

    $scope.SignUpDetails = [];
    $scope.FreeSlotCount = 0;
    $scope.Students = [];
    $scope.StudentsAttendence = [];
    $scope.SelectedStudent = [];
    $scope.StudentDetails = null;
    $scope.ParentDetails = null;

    $scope.SelectedStudent = {
        "Key": null,
        "Value": null,
    };

    $scope.IsError = false;
    $scope.ErrorMessage = "";

    $scope.Init = function (screenType, groupID) {

        $scope.ShowPreLoader = true;
        $scope.GroupID = groupID;

        $scope.GetUserDetails();

        $.ajax({
            type: "GET",
            data: { parentId: 60 },
            url: utility.myHost + "Home/GetStudentsSiblings",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                var activeStudentID = $("#CandidateIDfromOutSide").val();
                $scope.activeStudentID = activeStudentID;
                var activeStudentClassID = 0;
                var activeStudentSectionID = 0;
                var activeStudentName = "";
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.Students = result.Response;
                        if ($scope.Students !== null && $scope.Students.length > 0) {
                            if (activeStudentID == 0) {
                                activeStudentID = $scope.Students[0].StudentIID;
                                activeStudentClassID = $scope.Students[0].ClassID;
                                activeStudentSectionID = $scope.Students[0].SectionID;
                                activeStudentName = $scope.Students[0].FirstName + ' ' + $scope.Students[0].MiddleName + ' ' + $scope.Students[0].LastName + ""
                                $scope.activeStudentID = $scope.Students[0].StudentIID;
                                $scope.activeStudentClassID = $scope.Students[0].ClassID;
                                $scope.activeStudentSectionID = $scope.Students[0].SectionID;
                            }
                            else {
                                $scope.Students.forEach(function (index) {
                                    if (index.StudentIID == activeStudentID) {
                                        activeStudentClassID = index.ClassID;
                                        activeStudentSectionID = index.SectionID;
                                        activeStudentName = index.FirstName + ' ' + index.MiddleName + ' ' + index.LastName + "";
                                        $scope.activeStudentID = index.StudentIID;
                                        $scope.activeStudentClassID = index.ClassID;
                                        $scope.activeStudentSectionID = index.SectionID;
                                    }
                                });
                            }
                            $("#StudentID").val($scope.activeStudentID);
                            $("#ClassID").val($scope.activeStudentClassID);
                            $scope.SelectedStudent = {
                                "ClassID": "" + $scope.Students[0].ClassID + "", "ClassName": "" + $scope.Students[0].ClassName + "",
                                "SectionID": "" + $scope.Students[0].SectionID + "", "SectionName": "" + $scope.Students[0].SectionName + "",
                                "StudentID": "" + activeStudentID + "", "StudentName": "" + activeStudentName
                            };

                            $("#activedStudentID").val(activeStudentID);
                            $("#activedClassID").val(activeStudentClassID);
                            $("#activedSectionID").val(activeStudentSectionID);

                            $scope.studentProfile(activeStudentID);
                        }
                    });
                }
                if (result == null) {
                    window.location.href = "Account/LogIn";
                }
                $("#angularContent").show(); $scope.changeurl();
            },
            error: function () {

            },
            complete: function (result) {
                ///hideOverlay();
            }
        });

        if (screenType == "eventDetails") {

            $scope.FillSignUpDetailsByGroupID(groupID);
        }

        $scope.getLessonPlanList($scope.activeStudentID);
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
        showOverlay();
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
                hideOverlay();
            }
        });
    }

  

}]);