app.controller("HealthEntryController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.StudentName = [];

    $scope.ClassSectionChange = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
        var sectionId = $scope.CRUDModel.ViewModel.Section?.Key;
        var academicYearID = $scope.CRUDModel.ViewModel.Academic?.Key;
        var examGroupID = $scope.CRUDModel.ViewModel.ExamGroupName;
        if (academicYearID == undefined || academicYearID == null || academicYearID == "") {
            $().showMessage($scope, $timeout, true, "Please select Academic Year!");
            hideOverlay();
            $scope.CRUDModel.ViewModel.Section = null;
            $scope.CRUDModel.ViewModel.Class = null;
            return false
        }
        if (examGroupID == undefined || examGroupID == null || examGroupID == "") {
            $().showMessage($scope, $timeout, true, "Please select Assesment Term!");
            hideOverlay();
            $scope.CRUDModel.ViewModel.Section = null;
            $scope.CRUDModel.ViewModel.Class = null;
            return false
        }
        if (classId == undefined || classId == null || classId == "") {
            $().showMessage($scope, $timeout, true, "Please select Class!");
            hideOverlay();
            $scope.CRUDModel.ViewModel.Section = null;
            return false
        }
        if (sectionId == null) {
            sectionId = 0;
            hideOverlay();
            return false;
        }
        showOverlay();
        var url = "Schools/School/GetHealthEntryStudentData?classID=" + classId + "&sectionID=" + sectionId + "&academicYearID=" + academicYearID + "&examGroupID=" + examGroupID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.CRUDModel.ViewModel.StudentList = result.data;

                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };

    $scope.StudentBMI = function (model) {

        if (model.Weight == null || model.Height == null) {
            
            model.BMS = null;
            return null;
        }
        model.BMS = Math.round(((model.Weight) / (model.Height * model.Height)) * 10000);

    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);