app.controller("CampusTransferController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("CampusTransferController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.StudentName = [];

    $scope.StudentChanges = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;

        var studentId = model.Student?.Key;
        if (studentId == null || studentId == 0) {
            $().showMessage($scope, $timeout, true, "Please Select a Student");
            $scope.FeeTypes = null;

            hideOverlay();
            return false;
        }

        var url = "Schools/School/GetStudentDetailFromStudentID?StudentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result == null || result.data.length == 0) {
                    $().showMessage($scope, $timeout, true, "There is no details available for this student");
                    $scope.FeeTypes = null;
                    hideOverlay();
                    return false;
                }
                $scope.CRUDModel.ViewModel.AdmissionNumber = result.data.AdmissionNumber;
                $scope.CRUDModel.ViewModel.Class = result.data.ClassName;
                $scope.CRUDModel.ViewModel.ClassID = result.data.ClassID;
                $scope.CRUDModel.ViewModel.Section = result.data.SectionName;
                $scope.CRUDModel.ViewModel.SectionID = result.data.SectionID;
                $scope.CRUDModel.ViewModel.AcademicYearID = result.data.AcademicYearID;
                $scope.CRUDModel.ViewModel.AcademicYear = result.data.AcademicYear;
                $scope.CRUDModel.ViewModel.SchoolID = result.data.SchoolID;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillFeeDues = function ($event, $element, crudModel) {
        showOverlay();
        $scope.FeeTypes = null;

        var model = crudModel;

        var studentId = model.Student?.Key;
        var toSchoolID = model.ToSchool;
        var toAcademicYearID = model.ToAcademicYear?.Key;
        var toClassID = model.ToClass?.Key;

        if (studentId == null || studentId == undefined || studentId == '') {
            $().showMessage($scope, $timeout, true, "Please Select a Student");
            hideOverlay();
            return false;
        }
        else if (toSchoolID == null || toSchoolID == undefined || toSchoolID == '') {
            $().showMessage($scope, $timeout, true, "Please Select to transfer campus");
            hideOverlay();
            return false;
        }
        else if (toClassID == null || toClassID == undefined || toClassID == '') {
            $().showMessage($scope, $timeout, true, "Please Select to class");
            hideOverlay();
            return false;
        }
        else if (toAcademicYearID == null || toAcademicYearID == undefined || toAcademicYearID == '') {
            $().showMessage($scope, $timeout, true, "Please Select to academic year");
            hideOverlay();
            return false;
        }

        $scope.FillFeeDueDatas(studentId, toSchoolID, toAcademicYearID, toClassID);
    };


    $scope.FillFeeDueDatas = function (studentId, toSchoolID, toAcademicYearID, toClassID) {
        var url = "Schools/School/GetFeeDuesForCampusTransfer?studentId=" + studentId + "&toSchoolID=" + toSchoolID + "&toAcademicYearID=" + toAcademicYearID + "&toClassID=" + toClassID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result == null || result.data.length == 0) {
                    $().showMessage($scope, $timeout, true, "There is no fee details available for this student");
                    $scope.CRUDModel.ViewModel.FeeTypes = null;
                    hideOverlay();
                    return false;
                }
                $scope.CRUDModel.ViewModel.FeeTypes = result.data;

                const filter_Tution_frm_Campus = result.data.filter(item => item.IsTutionFee);
                $scope.CRUDModel.ViewModel.TutionFeeTotalFromCampus = filter_Tution_frm_Campus.reduce((total, item) => total + item.FromCampusDue, 0);

                const filter_Tution_to_Campus = result.data.filter(item => item.IsTutionFee);
                $scope.CRUDModel.ViewModel.TutionFeeTotalToCampus = filter_Tution_to_Campus.reduce((total, item) => total + item.ToCampusDue, 0);

                const filter_Transport_frm_Campus = result.data.filter(item => item.IsTransportFee);
                $scope.CRUDModel.ViewModel.TransportFeeTotalFromCampus = filter_Transport_frm_Campus.reduce((total, item) => total + item.FromCampusDue, 0);

                const filter_Transport_to_Campus = result.data.filter(item => item.IsTransportFee);
                $scope.CRUDModel.ViewModel.TransportFeeTotalToCampus = filter_Transport_to_Campus.reduce((total, item) => total + item.ToCampusDue, 0);

                const filter_otherFee_frm_Campus = result.data.filter(item => !item.IsTransportFee && !item.IsTutionFee);
                $scope.CRUDModel.ViewModel.OtherFeeTotalFromCampus = filter_otherFee_frm_Campus.reduce((total, item) => total + item.FromCampusDue, 0);

                const filter_otherFee_to_Campus = result.data.filter(item => !item.IsTransportFee && !item.IsTutionFee);
                $scope.CRUDModel.ViewModel.OtherFeeTotalToCampus = filter_otherFee_to_Campus.reduce((total, item) => total + item.ToCampusDue, 0);

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };
 
    $scope.SchoolChanges = function ($event, $element, crudModel) {
        if (crudModel.ToSchool == null || crudModel.ToSchool == "") return false;
        showOverlay();
        var model = crudModel;
        model.ToClasses = null;
        model.ToSections = null;
        model.ToAcademicYear = null;

        var url = "Schools/School/GetClassesBySchool?SchoolID=" + model.ToSchool;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.ToClasses = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

        var url = "Schools/School/GetSectionsBySchool?SchoolID=" + model.ToSchool;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.ToSections = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });


        var url = "Schools/School/GetAllAcademicYearBySchool?SchoolID=" + model.ToSchool;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.AcademicYear = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.ViewCampusTransfer = function (RowValues) {
        var windowName = 'CampusTransfer';
        var viewName = 'Campus Transfer';

        if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + RowValues.CampusTransferIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
            });

        $scope.FillFeeDueDatas(RowValues.StudentID, RowValues.ToSchoolID, RowValues.ToAcademicYearID, RowValues.ToClassID);

    };



    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);