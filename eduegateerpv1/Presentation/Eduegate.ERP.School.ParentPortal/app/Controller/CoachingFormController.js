app.controller('CoachingFormController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', "$q", function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout, $q) {
    console.log('Coaching form controller loaded.');

    $scope.StudentsDetails = [];
    $scope.Students = [];

    $scope.ShowPreLoader = true;

    $scope.init = function () {

        $scope.StudentCoachingApplication = {};

        $scope.StudentCoachingApplication.Siblings = [];

        $scope.StudentCoachingApplication.Student = {};

        $scope.StudentCoachingApplication.Curriculams = {};

        $scope.StudentCoachingApplication.IsSelectTermsAndConditions = false;

        showOverlay();

        $q.all([
            GetStudentsList(),
            GetSchoolSyllabus(),
            GetParentDetails(),
        ]).then(function () {
            hideOverlay();
            $scope.ShowPreLoader = false;
        });

    };

    function GetStudentsList() {
        return $q(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "/Home/GetStudentsSiblings?parentId=" + 0,
                success: function (result) {
                    $timeout(function () {
                        if (!result.IsError) {

                            $scope.StudentsDetails = result.Response;

                            result.Response.forEach(x => {
                                $scope.Students.push({
                                    "Key": x.StudentIID,
                                    "Value": x.AdmissionNumber + " - " + x.FirstName + " " + (x.MiddleName != null ? (x.MiddleName + " ") : "") + x.LastName,
                                });
                            });

                            if ($scope.Students.length == 1) {
                                $scope.StudentCoachingApplication.Student.Key = $scope.Students[0].Key;
                                $scope.StudentCoachingApplication.Student.Value = $scope.Students[0].Value;

                                $scope.StudentChanges($scope.StudentCoachingApplication);
                            }

                        }
                        resolve();
                    }, 1000);
                }
            });
        });
    };

    function GetParentDetails() {
        return $q(function (resolve, reject) {
            $.ajax({
                type: "GET",
                data: { studentId: 0 },
                url: utility.myHost + "/Home/GetGuardianDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {

                    $timeout(function () {
                        if (!result.IsError && result !== null) {
                            $scope.$apply(function () {
                                $scope.ParentDetails = result.Response;

                                $scope.FillParentDetails(result.Response);
                            });
                        }
                        resolve();
                    }, 1000);
                },
                error: function () {

                },
                complete: function (result) {

                }
            });
        });
    };

    function GetSchoolSyllabus() {
        return $q(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=SchoolSyllabus&defaultBlank=false",
                success: function (result) {

                    $timeout(function () {
                        $scope.SchoolSyllabus = result;

                        if ($scope.SchoolSyllabus.length == 1) {
                            $scope.StudentCoachingApplication.Curriculams.Key = $scope.SchoolSyllabus[0].Key;
                            $scope.StudentCoachingApplication.Curriculams.Value = $scope.SchoolSyllabus[0].Value;

                            $scope.CurriculamChanges($scope.StudentCoachingApplication);
                        }
                        resolve();
                    }, 1000);

                },
                complete: function (result) {

                }
            });
        });
    };

    $scope.FillParentDetails = function (data) {

        $scope.StudentCoachingApplication.ParentID = data.ParentIID;
        //Bind father details
        $scope.StudentCoachingApplication.FatherName = data.FatherFirstName + " " + (data.FatherMiddleName != null ? data.FatherMiddleName + " " : "") + data.FatherLastName;
        $scope.StudentCoachingApplication.FatherMobileNumber = data.FatherPhoneNumber;
        $scope.StudentCoachingApplication.FatherDesignation = data.FatherOccupation;
        $scope.StudentCoachingApplication.FatherCompanyName = data.FatherCompanyName;
        $scope.StudentCoachingApplication.FatherNationalID = data.FatherNationalID;
        $scope.StudentCoachingApplication.FatherEmailID = data.FatherEmailID;

        //Bind mother details
        $scope.StudentCoachingApplication.MotherName = data.MotherFirstName + " " + (data.MotherMiddleName != null ? data.MotherMiddleName + " " : "") + data.MotherLastName;
        $scope.StudentCoachingApplication.MotherMobileNumber = data.MotherPhone;
        $scope.StudentCoachingApplication.MotherDesignation = data.MotherOccupation;
        $scope.StudentCoachingApplication.MotherCompanyName = data.MotherCompanyName;
        $scope.StudentCoachingApplication.MotherNationalID = data.MotherNationalID;
        $scope.StudentCoachingApplication.MotherEmailID = data.MotherEmailID;
    };

    $scope.StudentChanges = function (model) {

        //Bind student details
        var studentDetails = $scope.StudentsDetails.find(s => s.StudentIID == model.Student.Key);

        if (studentDetails != null) {
            $scope.FillStudentDetails(studentDetails);
        }

        $scope.StudentCoachingApplication.StudentName = model.Student.Value;

        //Bind siblings details
        $scope.StudentsDetails.forEach(x => {
            if (x.StudentIID != model.Student.Key) {
                $scope.StudentCoachingApplication.Siblings.push({
                    "SiblingStudentID": x.StudentIID,
                    "SiblingAdmissionNumber": x.AdmissionNumber,
                    "SiblingName": x.FirstName + " " + (x.MiddleName != null ? (x.MiddleName + " ") : "") + x.LastName,
                    "SiblingClassID": x.ClassID,
                    "SiblingSectionID": x.SectionID,
                    "SiblingGrade": x.ClassName + " - " + x.SectionName,
                });
            }
        });

    };

    $scope.CurriculamChanges = function (model) {

        $scope.StudentCoachingApplication.Curriculam = model.Curriculams.Value;
    };

    $scope.FillStudentDetails = function (data) {

        //Bind admission details
        $scope.StudentCoachingApplication.StudentID = data.StudentIID;
        $scope.StudentCoachingApplication.AdmissionNumber = data.AdmissionNumber;
        $scope.StudentCoachingApplication.SchoolID = data.SchoolID;
        $scope.StudentCoachingApplication.SchoolName = data.SchoolName;
        $scope.StudentCoachingApplication.AcademicYearID = data.AcademicYearID;
        $scope.StudentCoachingApplication.AcademicYearName = data.AcademicYear;
        $scope.StudentCoachingApplication.ClassID = data.ClassID;
        $scope.StudentCoachingApplication.SectionID = data.SectionID;
        $scope.StudentCoachingApplication.Grade = data.ClassName + " (" + data.SectionName + ")";
        $scope.StudentCoachingApplication.RegistrationDate = data.AdmissionDate != null ? new Date(moment(data.AdmissionDate).format("YYYY-MM-DDTHH:MM:SS.SSSZ")) : null;
        $scope.StudentCoachingApplication.RegistrationDateString = data.AdmissionDateString;
        $scope.StudentCoachingApplication.BloodGroup = data.BloodGroupName;

        //Bind personal details
        $scope.StudentCoachingApplication.GenderID = data.GenderID;
        $scope.StudentCoachingApplication.Gender = data.GenderName;
        $scope.StudentCoachingApplication.ReligionID = data.RelegionID;
        $scope.StudentCoachingApplication.Religion = data.RelegionName;
        $scope.StudentCoachingApplication.DateOfBirth = data.DateOfBirth != null ? new Date(moment(data.DateOfBirth).format("YYYY-MM-DDTHH:MM:SS.SSSZ")) : null;
        $scope.StudentCoachingApplication.DateOfBirthString = data.DateOfBirthString;
        $scope.StudentCoachingApplication.BirthCountryID = data.StudentPassportDetails.CountryofBirthID;
        $scope.StudentCoachingApplication.BirthCountry = data.StudentPassportDetails.CountryofBirth.Value;
        $scope.StudentCoachingApplication.NationalityID = data.StudentPassportDetails.NationalityID;
        $scope.StudentCoachingApplication.Nationality = data.StudentPassportDetails.National.Value;
        $scope.StudentCoachingApplication.SecondLanguageID = data.SecoundLanguageID;
        $scope.StudentCoachingApplication.SecondLanguage = data.SecoundLanguage;
        $scope.StudentCoachingApplication.ThirdLanguageID = data.ThridLanguageID;
        $scope.StudentCoachingApplication.ThirdLanguage = data.ThridLanguage;

        //Bind passport and national details
        $scope.StudentCoachingApplication.PassportNumber = data.StudentPassportDetails.PassportNo;
        $scope.StudentCoachingApplication.PassportExpiryDate = data.StudentPassportDetails.PassportNoExpiry != null ? new Date(moment(data.StudentPassportDetails.PassportNoExpiry).format("YYYY-MM-DDTHH:MM:SS.SSSZ")) : null;
        $scope.StudentCoachingApplication.PassportExpiryDateString = data.StudentPassportDetails.PassportNoExpiry != null ? new Date(moment(data.StudentPassportDetails.PassportNoExpiry).format("YYYY-MM-DD")) : null;
        $scope.StudentCoachingApplication.NationalID = data.StudentPassportDetails.NationalIDNo;
        $scope.StudentCoachingApplication.NationalIDExpiryDate = data.StudentPassportDetails.NationalIDNoExpiry != null ? new Date(moment(data.StudentPassportDetails.NationalIDNoExpiry).format("YYYY-MM-DDTHH:MM:SS.SSSZ")) : null;
        $scope.StudentCoachingApplication.NationalIDExpiryDateString = data.StudentPassportDetails.NationalIDNoExpiry != null ? new Date(moment(data.StudentPassportDetails.NationalIDNoExpiry).format("YYYY-MM-DD")) : null;

        //Bind contact details
        $scope.StudentCoachingApplication.PrimaryContactID = data.PrimaryContactID;
        $scope.StudentCoachingApplication.PrimaryContact = data.PrimaryContact;
        $scope.StudentCoachingApplication.POBoxNumber = data.AdditionalInfo.PermenentPostBoxNo;
        $scope.StudentCoachingApplication.PrimaryAddress = data.AdditionalInfo.PermenentAddress;

        //Bind previous school details
        $scope.StudentCoachingApplication.PreviousSchoolName = data.PreviousSchoolName;
        $scope.StudentCoachingApplication.PreviousSchoolCurriculam = data.PreviousSchoolCurriculam;
        $scope.StudentCoachingApplication.PreviousSchoolGrade = data.PreviousSchoolGrade;
    };

    $scope.TermsAndConditionsClick = function (model) {

        if ($('#IsSelectTermsAndConditions').prop("checked") == true) {

            $scope.StudentCoachingApplication.IsSelectTermsAndConditions = true;
        }
        else {
            $scope.StudentCoachingApplication.IsSelectTermsAndConditions = false;
        }
    };

    function showOverlay() {
        $("#CoachingFormOverlay").fadeIn();
        $("#CoachingFormOverlayButtonLoader").fadeIn();
    };

    function hideOverlay() {
        $("#CoachingFormOverlay").fadeOut();
        $("#CoachingFormOverlayButtonLoader").fadeOut();
    };

}]);