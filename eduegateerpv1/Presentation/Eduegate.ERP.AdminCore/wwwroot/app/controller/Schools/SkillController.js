app.controller("SkillController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("SkillController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.ExamChanges = function ($event, $element, crudModel) {
        if (crudModel.Subject == null || crudModel.Subject == "") return false;
        showOverlay();
        var model = crudModel;
        var url = "Schools/School/GetExamMarkDetails?subjectID=" + model.Subject.Key + "&examID=" + model.Exam.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.ExamMinimumMark = result.data.ExamMinimumMark;
                model.ExamMaximumMark = result.data.ExamMaximumMark;
                
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.SkillGroupChanges = function ($event, $element, skillModel) {
        if (skillModel.SkillGroup.Key == null || skillModel.SkillGroup.Key == "") return false;
        showOverlay();
        var gridModel = skillModel;
        gridModel.SubSkill = null;
        var url = "Schools/School/GetSubSkillByGroup?skillGroupID=" + gridModel.SkillGroup.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.SubSkills = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

}]);