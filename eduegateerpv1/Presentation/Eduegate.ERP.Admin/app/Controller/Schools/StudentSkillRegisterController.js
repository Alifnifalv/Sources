app.controller("StudentSkillRegisterController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.OnSkillSelect = function (gridModel, element, $index) {
        showOverlay();
        var skillId = gridModel.Skill ?.Key;
        if (skillId === null || skillId === 0) {
            $().showMessage($scope, $timeout, true, "Please Select a Skill");
            hideOverlay();
            return false;
        }

        gridModel.SubSkills = [];
        var url = "Schools/School/GetStudentSkills?skillId=" + skillId;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                gridModel.SubSkills = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.GetGrade = function (ind,gridModel) {
        var grade = '';
       
        if (typeof (gridModel) === 'undefined') {
            return grade;
        }
        var gradeID = gridModel.MarksGradeMapID;
        
        if (typeof ($scope.gradeList) == 'undefined') {
            if ($scope.CRUDModel.ViewModel.Exam.Key == null) { return false; }
            var url = "Schools/School/GetGradeByExamSkill?examId=" + $scope.CRUDModel.ViewModel.Exam.Key + "&skillId=" + gridModel.SkillMasterID + "&subjectId=" + $scope.CRUDModel.ViewModel.Subject.Key + "&markGradeID=" + gridModel.MarkGradeId + "";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.gradeList = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        var selectedMarksGrade = gridModel.MarkGradeID;
        var selectedMark = gridModel.ObtainedMark;

        $.each($scope.gradeList, function (index, item) {

            if (item.MarkGradeID = selectedMarksGrade && item.GradeFrom <= selectedMark && item.GradeTo >= selectedMark) {
                grade = item.GradeName;
                gradeID = item.MarksGradeMapIID;
                return false;
            }
        });

        gridModel.MarksGradeMapID = gradeID;
        if (gridModel.ObtainedMark > gridModel.MaximumMarks) {
            $().showMessage($scope, $timeout, true, "Obtained Mark must be less than Maximum Mark");
            gridModel.ObtainedMark = null;
            hideOverlay();
            return false;
        }
        if (gridModel.MinimumMarks === undefined || gridModel.MinimumMarks === 0 || gridModel.ObtainedMark === undefined || gridModel.ObtainedMark === 0) {
            $scope.CRUDModel.ViewModel.IsPassed = false;
        } else {
            $scope.CRUDModel.ViewModel.IsPassed = (gridModel.MinimumMarks <= gridModel.ObtainedMark);
        }
        return grade;
    };

    $scope.GetTotalMark = function (data) {
        if (typeof (data) === 'undefined') {
            return 0;
        }
        var sum = 0;

        for (var i = data.length - 1; i >= 0; i--) {
            if ((data[i].ObtainedMark)) {
                sum += parseFloat(data[i].ObtainedMark);
            }
        }
        return sum;
    };

    $scope.GetTotalGrade = function (gridModel) {
        var grade = '';
      
        if (typeof (gridModel) === 'undefined') {
            return grade;
        }
        var gradeID = gridModel.MarksGradeMapID;
        if ($scope.CRUDModel.ViewModel.IsAbsent === true) {
            gradeID = null;
            $scope.CRUDModel.ViewModel.MarksGradeMapID = null;
           
        }
       
        var selectedMarksGrade = gridModel.MarkGradeID;
        var selectedMark = gridModel.ObtainedMark;

        $.each($scope.gradeList, function (index, item) {

            if (item.MarkGradeID = selectedMarksGrade && item.GradeFrom <= selectedMark && item.GradeTo >= selectedMark) {
                grade = item.GradeName;
                gradeID = item.MarksGradeMapIID;
                return false;
            }
        });

        $scope.CRUDModel.ViewModel.MarksGradeMapID = gradeID;

       return grade;
    };

    $scope.ClassChanges = function ($event, $element, dueModel) {
        showOverlay();
        dueModel.Student = {};
        var model = dueModel;
        SectionId = 0;
        var url = "Schools/School/GetClassStudents?classId=" + model.Class.Key + "&SectionId=" + SectionId;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Student = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

}]);