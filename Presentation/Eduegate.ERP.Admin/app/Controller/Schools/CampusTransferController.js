app.controller("CampusTransferController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("CampusTransferController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.StudentName = [];

    $scope.GetStudentDetails = function ($event, $element, crudModel) {

        if (crudModel.FromClass == undefined || crudModel.FromClass == null || crudModel.FromClass == "" || crudModel.FromClass.Key == null || crudModel.FromClass.Key == "") {
            $().showMessage($scope, $timeout, true, "Select a Class!");
            return false;
        }

        if (crudModel.FromSection == undefined || crudModel.FromSection == null || crudModel.FromSection == "" || crudModel.FromSection.Key == null || crudModel.FromSection.Key == "") {
            $().showMessage($scope, $timeout, true, "Select a Section!");
            crudModel.Section = null;
            return false;
        }

        showOverlay();
        var model = crudModel;
        var url = "Schools/School/GetClasswiseStudentDataForCampusTransfer?classID=" + model.FromClass.Key + "&sectionID=" + model.FromSection.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.CRUDModel.ViewModel.StudentList= result.data;

                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };


    $scope.ClassSectionChange = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;
        var classId = model.FromClass?.Key;
        if (classId == null) {

            hideOverlay();
            return false;
        }
        var sectionId = model.FromSection?.Key;
        if (sectionId == null) {
            sectionId = 0;
        }
        var url = "Schools/School/GetClassStudents?classID=" + classId + "&sectionID=" + sectionId;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.LookUps.Student = result.data;

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

        //var url = "Schools/School/GetAdvancedAcademicYearBySchool?SchoolID=" + model.ToSchool;
        //$http({ method: 'Get', url: url })
        //    .then(function (result) {
        //        $scope.LookUps.AdvancedAcademicYear = result.data;
        //        hideOverlay();
        //    }, function () {
        //        hideOverlay();
        //    });

        var url = "Schools/School/GetAllAcademicYearBySchool?SchoolID=" + model.ToSchool;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.AcademicYear = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });


    };

    $scope.StudentChanges = function ($event, $element, gridModel) {
        if (gridModel.Student.Key == null || gridModel.Student.Key == "") return false;
        model = gridModel;
        showOverlay();

        var url = "Schools/School/GetStudentDetailFromStudentID?StudentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.ToClasses = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

    };
   

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);