app.controller('ReportCardController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', "$q", function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout, $q) {
    console.log('Report Card Controller loaded.');

    $scope.AcademicYear = [];
    $scope.Students = [];

    $scope.SelectedStudent = null;
    $scope.SelectedAcademicYear = null;


    $scope.ShowPreLoader = true;

    $scope.init = function (model) {

        $scope.StudentProgressCard = model;

        $scope.GetStudentsList();
    };

    $scope.GetStudentsList = function () {
        showOverlay()
        $.ajax({
            type: 'GET',
            url: utility.myHost + "/Home/GetStudentsSiblings?parentId=" + 0,
            success: function (result) {
                if (!result.IsError) {
                    $scope.StudentFullDetails = result.Response;

                    result.Response.forEach(x => {
                        $scope.Students.push({
                            "Key": x.StudentIID,
                            "Value": x.AdmissionNumber + " - " + x.FirstName + " " + (x.MiddleName != null ? (x.MiddleName + " ") : "") + x.LastName,
                        });
                    });
                }
                hideOverlay();
                $scope.ShowPreLoader = false;
            }
        });
    };


    //$scope.GetAcademicBySchool = function () {

    //    $.ajax({
    //        type: 'GET',
    //        url: utility.myHost + "Home/GetDynamicLookUpData?lookType=AcademicYear&defaultBlank=false",
    //        success: function (result) {
    //            if (!result.IsError) {
    //                $scope.AcademicYear = result;
    //            }
    //        }
    //    });
    //};

    $scope.StudentChanges = function () {
        var studentDetail = $scope.StudentFullDetails.find(s => s.StudentIID == $scope.SelectedStudent.Key);

        if (studentDetail != null || studentDetail != undefined) {
            var schoolID = studentDetail.SchoolID;
            $.ajax({
                type: "GET",
                data: { schoolID: schoolID },
                url: utility.myHost + "/Home/GetAcademicYearBySchool?schoolID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.AcademicYear = result.Response;

                        });
                    }
                },
            });
        }
    }

    $scope.DownloadURL = function (url) {
        var link = document.createElement("a");
        link.href = url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };

    $scope.GetReportCardDetails = function () {

        var studentDetail = $scope.StudentFullDetails.find(s => s.StudentIID == $scope.SelectedStudent.Key);
        var sectionID = studentDetail.SectionID;
        var classID = studentDetail.ClassID;

        $.ajax({
            type: "GET",
            url: utility.myHost + "/Home/GetReportCardList?studentID=" +
                $scope.SelectedStudent.Key + '&classID=' + classID + '&sectionID=' + sectionID + '&academicYearID=' + $scope.SelectedAcademicYear.Key,
            success: function (result) {
                $timeout(function () {
                    $scope.$apply(function () {
                        if (!result.IsError && result !== null) {

                            $scope.ReportCards = result.Response;

                            hideOverlay();
                        }
                    });
                });
            },
            error: function () {
                hideOverlay();
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }

    function showOverlay() {
        $("#ReportCardOverlay").fadeIn();
        $("#ReportCardOverlayButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#ReportCardOverlay").fadeOut();
        $("#ReportCardOverlayButtonLoader").fadeOut();
    }

}]);