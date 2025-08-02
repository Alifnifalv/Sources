app.controller("StudentController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.RelegionChanges = function ($event, $element, studentModel) {
        if (studentModel.Relegion == null || studentModel.Relegion == "") return false;
        showOverlay();
        var model = studentModel;
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
        model.Academicyear = null;
        model.Class = null;
        model.Section = null;
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
        var url = "Schools/School/GetSectionsBySchool?schoolID=" + model.School;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Section = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.ClassChangesforStream = function () {
        var plusOneClassID = 7;
        var plusTwoClassID = 38;
        $scope.CRUDModel.ViewModel.Stream = null;
        $scope.CRUDModel.ViewModel.OptionalSubjects = null;
        var id = $scope.CRUDModel.ViewModel.Class.Key;
        if (id == plusOneClassID || id == plusTwoClassID) {
            $("#Stream").val("");
            $scope.CRUDModel.ViewModel.onStreams = false;
        }
        else {
            $scope.CRUDModel.ViewModel.onStreams = true;
            $scope.CRUDModel.ViewModel.Stream = null;
            $scope.CRUDModel.ViewModel.OptionalSubjects = null;
        }
    };

    $scope.StreamChanges = function ($event, $element, applicationModel) {
        $scope.CRUDModel.ViewModel.OptionalSubjects = null;
        if (applicationModel.Stream == null || applicationModel.Stream == "")
        return false;
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

    $scope.FillFatherDetailstoGuardian = function ($event, $element, parentdetails) {
        var fatherToGuardian = parentdetails.Guardians;
        if (fatherToGuardian.IsFatherDetailsAsGuardianDetail == true) {
            fatherToGuardian.GuardianFirstName = fatherToGuardian.FatherFirstName;
            fatherToGuardian.GuardianMiddleName = fatherToGuardian.FatherMiddleName;
            fatherToGuardian.GuardianLastName = fatherToGuardian.FatherLastName;
            fatherToGuardian.GuardianOccupation = fatherToGuardian.FatherOccupation;
            fatherToGuardian.GuardianCompanyName = fatherToGuardian.FatherCompanyName;
            fatherToGuardian.GuardianPhone = fatherToGuardian.FatherPhoneNumber;
            fatherToGuardian.GaurdianEmail = fatherToGuardian.FatherEmailID;
            fatherToGuardian.GuardianNationality = fatherToGuardian.FatherCountry;
            fatherToGuardian.GuardianNationalID = fatherToGuardian.FatherNationalID;
            fatherToGuardian.GuardianNationalIDNoIssueDateString = fatherToGuardian.FatherNationalDNoIssueDateString;
            fatherToGuardian.GuardianNationalIDNoExpiryDateString = fatherToGuardian.FatherNationalDNoExpiryDateString;
            fatherToGuardian.GuardianPassportNumber = fatherToGuardian.FatherPassportNumber;
            fatherToGuardian.GuardianCountryofIssue = fatherToGuardian.FatherCountryofIssue;
            fatherToGuardian.GuardianPassportNoIssueString = fatherToGuardian.FatherPassportNoIssueString;
            fatherToGuardian.GuardianPassportNoExpiryString = fatherToGuardian.FatherPassportNoExpiryString;
        }
        else {
            fatherToGuardian.GuardianFirstName = null;
            fatherToGuardian.GuardianMiddleName = null;
            fatherToGuardian.GuardianLastName = null;
            fatherToGuardian.GuardianOccupation = null;
            fatherToGuardian.GuardianCompanyName = null;
            fatherToGuardian.GuardianPhone = null;
            fatherToGuardian.GaurdianEmail = null;
            fatherToGuardian.GuardianNationality = null;
            fatherToGuardian.GuardianNationalID = null;
            fatherToGuardian.GuardianNationalIDNoIssueDateString = null;
            fatherToGuardian.GuardianNationalIDNoExpiryDateString = null;
            fatherToGuardian.GuardianPassportNumber = null;
            fatherToGuardian.GuardianCountryofIssue = null;
            fatherToGuardian.GuardianPassportNoIssueString = null;
            fatherToGuardian.GuardianPassportNoExpiryString = null;
        }
    };

    $scope.FillMotherDetailstoGuardian = function ($event, $element, parentdetails) {
        var motherToGuardian = parentdetails.Guardians;
        if (motherToGuardian.IsMotherDetailsAsGuardianDetail == true) {
            motherToGuardian.GuardianFirstName = motherToGuardian.MotherFirstName;
            motherToGuardian.GuardianMiddleName = motherToGuardian.MotherMiddleName;
            motherToGuardian.GuardianLastName = motherToGuardian.MotherLastName;
            motherToGuardian.GuardianOccupation = motherToGuardian.MotherOccupation;
            motherToGuardian.GuardianCompanyName = motherToGuardian.MotherCompanyName;
            motherToGuardian.GuardianPhone = motherToGuardian.MotherPhone;
            motherToGuardian.GaurdianEmail = motherToGuardian.MotherEmailID;
            motherToGuardian.GuardianNationality = motherToGuardian.MotherCountry;
            motherToGuardian.GuardianNationalID = motherToGuardian.MotherNationalID;
            motherToGuardian.GuardianNationalIDNoIssueDateString = motherToGuardian.MotherNationalDNoIssueDateString;
            motherToGuardian.GuardianNationalIDNoExpiryDateString = motherToGuardian.MotherNationaIDNoExpiryDateString;
            motherToGuardian.GuardianPassportNumber = motherToGuardian.MotherPassportNumber;
            motherToGuardian.GuardianCountryofIssue = motherToGuardian.MotherCountryofIssue;
            motherToGuardian.GuardianPassportNoIssueString = motherToGuardian.MotherPassportNoIssueString;
            motherToGuardian.GuardianPassportNoExpiryString = motherToGuardian.MotherPassportNoExpiryString;
        }
        else {
            motherToGuardian.GuardianFirstName = null;
            motherToGuardian.GuardianMiddleName = null;
            motherToGuardian.GuardianLastName = null;
            motherToGuardian.GuardianOccupation = null;
            motherToGuardian.GuardianCompanyName = null;
            motherToGuardian.GuardianPhone = null;
            motherToGuardian.GaurdianEmail = null;
            motherToGuardian.GuardianNationality = null;
            motherToGuardian.GuardianNationalID = null;
            motherToGuardian.GuardianNationalIDNoIssueDateString = null;
            motherToGuardian.GuardianNationalIDNoExpiryDateString = null;
            motherToGuardian.GuardianPassportNumber = null;
            motherToGuardian.GuardianCountryofIssue = null;
            motherToGuardian.GuardianPassportNoIssueString = null;
            motherToGuardian.GuardianPassportNoExpiryString = null;
        }
    };

    $scope.GuardianAddressAsCurrentAddress = function ($event, $element, address) {
        var grdnAddrsToCurenntAddrs = address.Guardians;
        var currentAddress = address.Addresses;
        if (currentAddress.IsCurrentAddresIsGuardian == true) {
            currentAddress.PermenentBuildingNo = grdnAddrsToCurenntAddrs.BuildingNo;
            currentAddress.PermenentFlatNo = grdnAddrsToCurenntAddrs.FlatNo;
            currentAddress.PermenentStreetNo = grdnAddrsToCurenntAddrs.StreetNo;
            currentAddress.PermenentStreetName = grdnAddrsToCurenntAddrs.StreetName;
            currentAddress.PermenentLocationNo = grdnAddrsToCurenntAddrs.LocationNo;
            currentAddress.PermenentLocationName = grdnAddrsToCurenntAddrs.LocationName;
            currentAddress.PermenentZipNo = grdnAddrsToCurenntAddrs.ZipNo;
            currentAddress.PermenentPostBoxNo = grdnAddrsToCurenntAddrs.PostBoxNo;
            currentAddress.PermenentCity = grdnAddrsToCurenntAddrs.City;
            currentAddress.PermenentCountry = grdnAddrsToCurenntAddrs.Country;
        }
        else {
            currentAddress.PermenentBuildingNo = null;
            currentAddress.PermenentFlatNo = null;
            currentAddress.PermenentStreetNo = null;
            currentAddress.PermenentStreetName = null;
            currentAddress.PermenentLocationNo = null;
            currentAddress.PermenentLocationName = null;
            currentAddress.PermenentZipNo = null;
            currentAddress.PermenentPostBoxNo = null;
            currentAddress.PermenentCity = null;
            currentAddress.PermenentCountry = null;
        }
    };

    $scope.FillFatherWhatsappNoFromMobileNo = function (viewModel) {

        if (viewModel.IsFatherWhatsAppNoSameAsMobile == true) {
            viewModel.FatherWhatsappMobileNo = viewModel.FatherPhoneNumber;
        }
        else {
            viewModel.FatherWhatsappMobileNo = null;
        }
    };

    $scope.FillMotherWhatsappNoFromMobileNo = function (viewModel) {

        if (viewModel.IsMotherWhatsAppNoSameAsMobile == true) {
            viewModel.MotherWhatsappMobileNo = viewModel.MotherPhone;
        }
        else {
            viewModel.MotherWhatsappMobileNo = null;
        }
    };

    $scope.FillMotherWhatsappNoFromMobileNo = function (viewModel) {

        if (viewModel.IsMotherWhatsAppNoSameAsMobile == true) {
            viewModel.MotherWhatsappMobileNo = viewModel.MotherPhone;
        }
        else {
            viewModel.MotherWhatsappMobileNo = null;
        }
    };

    $scope.FillGuardianWhatsappNoFromMobileNo = function (viewModel) {

        if (viewModel.IsGuardianWhatsAppNoSameAsMobile == true) {
            viewModel.GuardianWhatsappMobileNo = viewModel.GuardianPhone;
        }
        else {
            viewModel.GuardianWhatsappMobileNo = null;
        }
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);