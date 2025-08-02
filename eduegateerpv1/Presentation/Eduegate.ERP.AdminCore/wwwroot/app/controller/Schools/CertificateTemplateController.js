app.controller("CertificateTemplateController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root)
{
    console.log("CertificateTemplateController Loaded");


    $scope.Students = [];
    $scope.Staffs = [];
    $scope.CurrentAttendence = {};
    $scope.Model = {};
    $scope.SelectedMonth = null;
    $scope.SelectedYear = null;
    $scope.CurrentDate = new Date();
    $scope.SelectedDay = new Date().getDate();
    $scope.SelectedYear = new Date().getFullYear();
    $scope.SelectedMonth = new Date().getMonth();
    $scope.AttendenceReasons = [];
    $scope.PresentStatuses = [];
    $scope.AllStudentAttendance = [];
    $scope.Reason = null;
    $scope.type = null;
    $scope.PresentStatus = null;
    $scope.PStatus = 0;
    $scope.updateStatus = 0;
    $scope.HoliDayData = [];
    $scope.PresentStatuses = [];
    $scope.AttendenceReasonKey = null;
    $scope.SelectedMonthDate = 0;
    $scope.ButtonText = "Mark All";

    //Initializing the product price
    $scope.Init = function (model, windowname, type) {
        $scope.ResetDay();
        $scope.type = type;
        //Sections
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=MainTeacherSections&defaultBlank=false",
        }).then(function (result) {
            $scope.Section = result.data;
        });
        $.ajax({
            type: 'GET',
            url: utility.myHost + "Mutual/GetDynamicLookUpData?lookType=AttendenceReason&defaultBlank=false;",
            success: function (result) {
                $scope.AttendenceReasons = result;
            }
        });
        $.ajax({
            type: 'GET',
            url: utility.myHost + "Mutual/GetDynamicLookUpData?lookType=PresentStatus&defaultBlank=false;",
            success: function (result) {
                $scope.PresentStatusData = result;
            }
        });
        $.ajax({
            type: 'GET',
            url: "Attendence/GetPresentStatuses",
            success: function (result) {
                $scope.PresentStatuses = result;
                console.log($scope.PresentStatuses);
            }
        });
    };

    $scope.LoadData = function ()
    {
        if ($scope.selectedClass == undefined || $scope.selectedSection == undefined)
        {
            $().showGlobalMessage($root, $timeout, true, "Select Class and Section!");
            return false;
        }
        else
        {
            $scope.LoadAttendenceData();
        }
    }


    $scope.LoadCertificateTemplateData = function ()
    {
        if ($scope.selectedClass == undefined || $scope.selectedSection == undefined)
        {
            $().showGlobalMessage($root, $timeout, true, "Select Class and Section!");
            return false;
        }
        showOverlay();
        var ReportName = $scope.selectedClass?.Key;
        var sectionId = $scope.selectedSection?.Key;
            $.ajax({
                type: 'GET',
                url: utility.myHost + "Schools/CertificateTemplate/GetCertificateTemplateByReportName?reportname=" + $scope.ReportName,
                success: function (result) {
                    $scope.AttendenceData = result;
                }
            });
        hideOverlay();
    };



    function saveAttendence()
    {
        showOverlay();
        if ($scope.PresentStatus == null)
        {
            alert("Please Select Any Status!");
            $scope.updateStatus = 0;
            hideOverlay();
            return false;
        }
       else
        {
            var attendenceDate = moment($scope.CurrentAttendence.actualDate).format(_dateFormat.toUpperCase());
            $scope.CurrentAttendence.date = attendenceDate;
            if ($scope.PresentStatus.Key == undefined) {
                $scope.PresentStatus = $scope.PresentStatus;
            }
            else {
                $scope.PresentStatus = $scope.PresentStatus.Key;
            }

            if ($scope.AttendenceReason == undefined || $scope.AttendenceReason.Key == null) {
                $scope.AttendenceReasonKey = $scope.AttendenceReasonKey;
            }
            else {
                $scope.AttendenceReasonKey = $scope.AttendenceReason.Key;
            }

            $.ajax({
                url: utility.myHost + "Schools/Attendence/SaveAttendence",
                type: "POST",
                data: {
                    "StaffID": $scope.Model.EmployeeIID,
                    "StudentID": $scope.Model.StudentIID,
                    "AttendenceDateString": $scope.CurrentAttendence.date,
                    "PresentStatusID": $scope.PresentStatus,
                    "AttendenceReasonID": $scope.PStatus == 0 ? $scope.AttendenceReasonKey : null,
                    "Reason": $scope.Reason,
                    "ClassID": $scope.selectedClass.Key,
                    "SectionID": $scope.selectedSection.Key
                },
                success: function (result) {
                    if (!result.IsError && result != null)
                    {
                    }
                    if (result.IsFailed)
                        $scope.updateStatus = 0;
                    else
                        $scope.updateStatus = 1;

                    $scope.$apply(function () {
                        if ($scope.PStatus == 1 || ($scope.PresentStatus == null || $scope.PresentStatus.Key == null || ($scope.PresentStatus.Key != null && $scope.PresentStatus.Key == 1))) {
                            if ($scope.updateStatus == 1) {
                                $scope.CurrentAttendence.status = true;
                                $scope.PStatus = 0;
                            }
                        }
                        else {
                            if ($scope.updateStatus == 1)
                                $scope.CurrentAttendence.status = false;
                        }
                        $scope.LoadAttendenceData();
                    });
                },
            });
            hideOverlay();
        }
    }

    function showOverlay()
    {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay()
    {
        $('.window-tab .preload-overlay').hide();
    }

    
    //$scope.UpdateAllStudentAttendence = function () {

    //    showOverlay();
    //    if ($scope.PresentStatus == null) {
    //        alert("Please Select Any Status!");
    //        $scope.updateStatus = 0;
    //        hideOverlay();
    //        return false;
    //    }
    //    var attendenceDate = moment($scope.CurrentAttendence.actualDate).format(_dateFormat.toUpperCase());
    //    var classId = $scope.selectedClass?.Key;
    //    var sectionId = $scope.selectedSection?.Key;
    //    var PStatus = $scope.PresentStatus;
    //    if ($scope.type === 'student')
    //    {
    //        $.ajax({
    //            url: utility.myHost + "Schools/Attendence/UpdateAllStudentAttendence?classId=" + classId + "&sectionId=" + sectionId + "&AttendenceDateString=" + attendenceDate + "&PStatus=" + PStatus,
    //            type: "POST",
    //            success: function (result) {
    //                if (result.IsFailed)
    //                    $scope.updateStatus = 0;
    //                else
    //                    $scope.updateStatus = 1;

    //                $scope.$apply(function () {
    //                    $scope.LoadAttendenceData();
    //                    $scope.ResetDay();
    //                });
    //            }

    //        });
            
    //    }
        
    //    hideOverlay();
    //}

    $scope.MarkDay = function ($index)
    {
        var date = new Date($index.toString() + '/' + $scope.Months[$scope.SelectedMonth] + '/' + $scope.SelectedYear.toString())
        $scope.CurrentAttendence.actualDate = date;
        var datestring = moment($scope.CurrentAttendence.actualDate).format(_dateFormat.toUpperCase());

        $scope.ButtonText = "Mark All For ( " + datestring + " )";

    };

    $scope.ResetDay = function ()
    {
        $scope.CurrentAttendence.actualDate = new Date();
        var datestring = moment($scope.CurrentAttendence.actualDate).format(_dateFormat.toUpperCase());
        $scope.ButtonText = "Mark All For ( " + datestring + " )";
    };

}]);

