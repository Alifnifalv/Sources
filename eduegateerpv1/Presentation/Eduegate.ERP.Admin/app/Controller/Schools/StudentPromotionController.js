app.controller("StudentPromotionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.PromotionStatus = [];
    $scope.ExcludePromotionStatus = [];
    $scope.ChangePromotionStatus = function (viewModel) {
        $scope.LookUps.PromotionStatus = {};
        $scope.CRUDModel.ViewModel.Remarks = "";
        if ($scope.CRUDModel.ViewModel.IsPromoted == true) {
            if ($scope.PromotionStatus != undefined && $scope.PromotionStatus.find(x => x.Key == 1) != null) {

                $scope.LookUps.PromotionStatus = $scope.PromotionStatus;
                // $scope.CRUDModel.ViewModel.PromotionStatus = $scope.PromotionStatus;
                $scope.CRUDModel.ViewModel.Student = {};

            }
            else {
                $http({
                    method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=PromotionStatus&defaultBlank=false'
                }).then(function (result) {
                    $scope.PromotionStatus = result.data;
                    $scope.LookUps.PromotionStatus = $scope.PromotionStatus;


                });
            }
            $scope.CRUDModel.ViewModel.PromotionStatus = "1";//$scope.PromotionStatus.find(x => x.Key == 1);
            $scope.CRUDModel.ViewModel.Remarks = "Successfully Promoted";
        }
        else {

            if ($scope.ExcludePromotionStatus != undefined > 0) {
                $scope.LookUps.PromotionStatus = $scope.ExcludePromotionStatus;
            }
            else {
                $http({
                    method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=ExcludePromotionStatus&defaultBlank=false'
                }).then(function (result) {
                    $scope.ExcludePromotionStatus = result.data;
                    $scope.LookUps.PromotionStatus = $scope.ExcludePromotionStatus;

                });
            }
            if ($scope.ExcludePromotionStatus != undefined && $scope.CRUDModel.ViewModel.PromotionStatus != undefined)
                $scope.CRUDModel.ViewModel.Remarks = $scope.ExcludePromotionStatus.find(x => x.Key==$scope.CRUDModel.ViewModel.PromotionStatus).Value;

        }

    };
    $scope.SchoolChanges = function ($event, $element, screenModel, type) {
        if (type == 1) {
            if (screenModel.ShiftFromSchool == null || screenModel.ShiftFromSchool == "") return false;
            showOverlay();
            var model = screenModel;

            model.ShiftFromAcademicYear = null;
            model.ShiftFromClass = null;
            model.ShiftFromSection = null;
            var url = "Schools/School/GetAcademicYearBySchool?schoolID=" + model.ShiftFromSchool;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.ShiftFromAcademicYear = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
            var url = "Schools/School/GetClassesBySchool?schoolID=" + model.ShiftFromSchool;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.ShiftFromClass = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
            var url = "Schools/School/GetSectionsBySchool?schoolID=" + model.ShiftFromSchool;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.ShiftFromSection = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        else {
            if (screenModel.ToASchool == null || screenModel.ToASchool == "") return false;
            showOverlay();
            var model = screenModel;

            model.Academicyear = null;
            model.Class = null;
            model.Section = null;
            var url = "Schools/School/GetAcademicYearBySchool?schoolID=" + model.ToASchool;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.SchoolAcademicyear = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
            var url = "Schools/School/GetClassesBySchool?schoolID=" + model.ToASchool;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.ToClass = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
            var url = "Schools/School/GetSectionsBySchool?schoolID=" + model.ToASchool;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.ToSection = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    }
    $scope.ClassChanges = function ($event, $element, promoModel) {
        showOverlay();
        dueModel.Student = {};
        var model = promoModel;
        var url = "Schools/School/GetClassStudents?classId=" + model.ShiftFromClass + "&sectionId=0";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Students = result.data;
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
