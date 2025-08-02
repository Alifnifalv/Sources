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
            url: utility.myHost + "Home/GetStudentsSiblings?parentId=" + 0,
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
            var studentID = studentDetail.StudentIID;
            $.ajax({
                type: "GET",
                data: { studentID: studentID },
                url: utility.myHost + "Home/GetAcademicYearByProgressReport",
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
            url: utility.myHost + "Home/GetReportCardList?studentID=" +
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

    $scope.DirectDownload = function (contentID) {
        showOverlay();

        $.ajax({
            type: "GET",
            url: utility.myHost + "Content/DirectDownloadByContentID?contentID=" + contentID,
            xhrFields: {
                responseType: 'blob' // Set the response type to blob
            },
            success: function (data, textStatus, xhr) {

                if (xhr.status === 200) {

                    var fileName = '';
                    var fileType = data.type;

                    const headers = xhr.getAllResponseHeaders();

                    const disposition = headers.trim().split(/[\r\n]+/)[0];

                    if (disposition && disposition.indexOf('attachment') !== -1) {
                        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                        var matches = filenameRegex.exec(disposition);
                        if (matches != null && matches[1]) {
                            fileName = matches[1].replace(/['"]/g, '');
                        }
                    }

                    var blob = new Blob([data], { type: fileType });
                    var url = window.URL.createObjectURL(blob);
                    var a = document.createElement('a');
                    document.body.appendChild(a);
                    a.style = 'display: none';
                    a.href = url;
                    a.download = fileName; // Set the appropriate file name here
                    a.click();
                    window.URL.revokeObjectURL(url);

                    callToasterPlugin('success', "File downloaded successfully!");
                } else {
                    callToasterPlugin('error', "No data received.");
                }
            },
            error: function (result) {
                callToasterPlugin('error', "Failed to download file! Error: " + error);
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

}]);