app.controller("StudentApplicationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.RelegionChanges = function ($event, $element, applicationModel) {
        if (applicationModel.Relegion == null || applicationModel.Relegion == "") return false;
        showOverlay();
        var model = applicationModel;
        model.Cast = null;
        var url = "Schools/School/GetCastByRelegion?relegionID=" + model.Relegion;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Cast = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.SchoolChanges = function ($event, $element, applicationModel) {
        if (applicationModel.School == null || applicationModel.School == "") return false;
        showOverlay();
        var model = applicationModel;
        model.SchoolAcademicyear = null;
        model.Class = null;
        var url = "Schools/School/GetAcademicYearBySchool?schoolID=" + model.School;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.AcademicYear = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
        var url = "Schools/School/GetClassesBySchool?schoolID=" + model.School;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Classes = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    //$scope.AcademicYearChanges = function ($event, $element, applicationModel) {
    //    if (applicationModel.SchoolAcademicyear == null || applicationModel.SchoolAcademicyear == "") return false;
    //    showOverlay();
    //    var model = applicationModel;
    //    model.Class = null;
    //    var url = "Schools/School/GetClasseByAcademicyear?academicyearID=" + model.SchoolAcademicyear;
    //    $http({ method: 'Get', url: url })
    //        .then(function (result) {
    //            $scope.LookUps.Classes = result.data;
    //            hideOverlay();
    //        }, function () {
    //            hideOverlay();
    //        });
    //};

    $scope.StreamGroupChanges = function ($event, $element, applicationModel) {
        $scope.CRUDModel.ViewModel.Stream = null;
        $scope.CRUDModel.ViewModel.OptionalSubjects = null;
        if (applicationModel.StreamGroup == null || applicationModel.StreamGroup == "") return false;
        showOverlay();
        var model = applicationModel;
        model.Stream = null;
        model.OptionalSubjects = null;
        var url = "Schools/School/GetStreamByStreamGroup?streamGroupID=" + model.StreamGroup.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.StreamsForApplication = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.ClassChangesforStream = function () {
        var plusOneClassID = 7;
        var plusTwoClassID = 38;
        $scope.CRUDModel.ViewModel.StreamGroup = null;
        $scope.CRUDModel.ViewModel.Stream = null;
        $scope.CRUDModel.ViewModel.OptionalSubjects = null;
        var id = $scope.CRUDModel.ViewModel.Class.Key;
        if (id == plusOneClassID || id == plusTwoClassID) {
            $("#Stream").val("");
            $scope.CRUDModel.ViewModel.onStreams = true;
        }
        else {
            $scope.CRUDModel.ViewModel.onStreams = false;
            $scope.CRUDModel.ViewModel.Stream = null;
            $scope.CRUDModel.ViewModel.OptionalSubjects = null;
            $scope.CRUDModel.ViewModel.StreamGroup = null;
        }
    };

    $scope.StreamChanges = function ($event, $element, applicationModel) {
        if (applicationModel.Stream == null || applicationModel.Stream == "") return false;
        showOverlay();
        var model = applicationModel;
        var url = "Schools/School/GetStreamOptionalSubjects?streamID=" + model.Stream.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.OptionalSubjects = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillFatherDetailsFromGuardian = function ($event, $element, applicationModel) {
        var fatherMother = applicationModel.FatherMotherDetails;
        var guradianDetail = applicationModel.GuardianDetails
        if (guradianDetail.IsGuardianDetailAsFatherDetails == true) {
            guradianDetail.GuardianFirstName = fatherMother.FatherFirstName;
            guradianDetail.GuardianMiddleName = fatherMother.FatherMiddleName;
            guradianDetail.GuardianLastName = fatherMother.FatherLastName;
            guradianDetail.GuardianOccupation = fatherMother.FatherOccupation;
            guradianDetail.GuardianCompanyName = fatherMother.FatherCompanyName;
            guradianDetail.GuardianMobileNumber = fatherMother.MobileNumber;
            guradianDetail.GuardianEmailID = fatherMother.EmailID;
            guradianDetail.GuardianNationality = fatherMother.FatherCountry;
            guradianDetail.GuardianNationalID = fatherMother.FatherNationalID;
            guradianDetail.GuardianNationalIDNoIssueDateString = fatherMother.FatherNationalDNoIssueDateString;
            guradianDetail.GuardianNationalIDNoExpiryDateString = fatherMother.FatherNationalDNoExpiryDateString;
            guradianDetail.GuardianPassportNumber = fatherMother.FatherPassportNumber;
            guradianDetail.GuardianCountryofIssue = fatherMother.FatherCountryofIssue;
            guradianDetail.GuardianPassportNoIssueString = fatherMother.FatherPassportNoIssueString;
            guradianDetail.GuardianPassportNoExpiryString = fatherMother.FatherPassportNoExpiryString;
        }
        else {
            guradianDetail.GuardianFirstName = null;
            guradianDetail.GuardianMiddleName = null;
            guradianDetail.GuardianLastName = null;
            guradianDetail.GuardianOccupation = null;
            guradianDetail.GuardianCompanyName = null;
            guradianDetail.GuardianMobileNumber = null;
            guradianDetail.GuardianEmailID = null;
            guradianDetail.GuardianNationality = null;
            guradianDetail.GuardianNationalID = null;
            guradianDetail.GuardianNationalIDNoIssueDateString = null;
            guradianDetail.GuardianNationalIDNoExpiryDateString = null;
            guradianDetail.GuardianPassportNumber = null;
            guradianDetail.GuardianCountryofIssue = null;
            guradianDetail.GuardianPassportNoIssueString = null;
            guradianDetail.GuardianPassportNoExpiryString = null;
        }
    };

    $scope.FillMotherDetailsFromGuardian = function ($event, $element, applicationModel) {
        var fatherMother = applicationModel.FatherMotherDetails;
        var guradianDetail = applicationModel.GuardianDetails
        if (guradianDetail.IsGuardianDetailAsMotherDetails == true) {
            guradianDetail.GuardianFirstName = fatherMother.MotherFirstName;
            guradianDetail.GuardianMiddleName = fatherMother.MotherMiddleName;
            guradianDetail.GuardianLastName = fatherMother.MotherLastName;
            guradianDetail.GuardianOccupation = fatherMother.MotherOccupation;
            guradianDetail.GuardianCompanyName = fatherMother.MotherCompanyName;
            guradianDetail.GuardianMobileNumber = fatherMother.MotherMobileNumber;
            guradianDetail.GuardianEmailID = fatherMother.MotherEmailID;
            guradianDetail.GuardianNationality = fatherMother.MotherCountry;
            guradianDetail.GuardianNationalID = fatherMother.MotherNationalID;
            guradianDetail.GuardianNationalIDNoIssueDateString = fatherMother.MotherNationalDNoIssueDateString;
            guradianDetail.GuardianNationalIDNoExpiryDateString = fatherMother.MotherNationaIDNoExpiryDateString;
            guradianDetail.GuardianPassportNumber = fatherMother.MotherPassportNumber;
            guradianDetail.GuardianCountryofIssue = fatherMother.MotherCountryofIssue;
            guradianDetail.GuardianPassportNoIssueString = fatherMother.MotherPassportNoIssueString;
            guradianDetail.GuardianPassportNoExpiryString = fatherMother.MotherPassportNoExpiryString;
        }
        else {
            guradianDetail.GuardianFirstName = null;
            guradianDetail.GuardianMiddleName = null;
            guradianDetail.GuardianLastName = null;
            guradianDetail.GuardianOccupation = null;
            guradianDetail.GuardianCompanyName = null;
            guradianDetail.GuardianMobileNumber = null;
            guradianDetail.GuardianEmailID = null;
            guradianDetail.GuardianNationality = null;
            guradianDetail.GuardianNationalID = null;
            guradianDetail.GuardianNationalIDNoIssueDateString = null;
            guradianDetail.GuardianNationalIDNoExpiryDateString = null;
            guradianDetail.GuardianPassportNumber = null;
            guradianDetail.GuardianCountryofIssue = null;
            guradianDetail.GuardianPassportNoIssueString = null;
            guradianDetail.GuardianPassportNoExpiryString = null;
        }
    };

    $scope.FillGuardianWhatsappNoFromMobileNo = function (viewModel) {

        if (viewModel.IsWhatsAppNoSameAsMobile == true) {
            viewModel.GuardianWhatsappMobileNo = viewModel.GuardianMobileNumber;
        }
        else {
            viewModel.GuardianWhatsappMobileNo = null;
        }
    };


    $scope.FillFatherWhatsappNoFromMobileNo = function (viewModel) {

        if (viewModel.IsFatherWhatsAppNoSameAsMobile == true) {
            viewModel.FatherWhatsappMobileNo = viewModel.MobileNumber;
        }
        else {
            viewModel.FatherWhatsappMobileNo = null;
        }
    };

    $scope.FillMotherWhatsappNoFromMobileNo = function (viewModel) {

        if (viewModel.IsMotherWhatsAppNoSameAsMobile == true) {
            viewModel.MotherWhatsappMobileNo = viewModel.MotherMobileNumber;
        }
        else {
            viewModel.MotherWhatsappMobileNo = null;
        }
    };

    //Function for StudentLeaveApplicationEdit
    $scope.EditLeaveApplication = function (studentLeaveIID) {
        var windowName = 'StudentLeaveApplication';
        var viewName = 'Edit Leave Application';

        if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + studentLeaveIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
            });
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);