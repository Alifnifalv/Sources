app.controller("RemarksEntryController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("RemarksEntryController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.StudentName = [];

    $scope.ClassSectionChange = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
        var sectionId = $scope.CRUDModel.ViewModel.Section?.Key;
        var examGroupName = $scope.CRUDModel.ViewModel.ExamGroupName;
        if (examGroupName == undefined || examGroupName == null || examGroupName == "") {
            $().showMessage($scope, $timeout, true, "Please select Assesment Term!");
            $scope.CRUDModel.ViewModel.Section = null;
            $scope.CRUDModel.ViewModel.Class = null;
            hideOverlay();
            return false;
        }
        if (classId == null) {
            $().showMessage($scope, $timeout, true, "Please select Class!");
            hideOverlay();
            $scope.CRUDModel.ViewModel.Section = null;
            return false;
        }
        if (sectionId == null) {
            sectionId = 0;
            hideOverlay();
            return false;
        }
        showOverlay();
        var model = crudModel;
        var url = "Schools/School/GetClasswiseRemarksEntryStudentData?classID=" + classId + "&sectionID=" + sectionId + "&examGroupID=" + examGroupName;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.CRUDModel.ViewModel.StudentList= result.data;

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