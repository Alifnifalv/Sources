app.controller("StudentAttendenceController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    //$scope.ClassSectionChange = function ($event, $element, crudModel) {
    //    showOverlay();
    //    var model = crudModel;
    //    var url = "Schools/School/GetClassStudents?classID=" + model.Class.Key + "&sectionID=" + model.Section.Key;
    //    $http({ method: 'Get', url: url })
    //        .then(function (result) {

    //            $scope.LookUps.Students = result.data;

    //            hideOverlay();

    //        }, function () {

    //            hideOverlay();

    //        });
    //};

    $scope.ClassSectionChange = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
        if (classId == null) {

            hideOverlay();
            return false;
        }
        var sectionId = $scope.CRUDModel.ViewModel.Section?.Key;
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


    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);