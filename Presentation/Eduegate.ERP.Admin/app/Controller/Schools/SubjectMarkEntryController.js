app.controller("SubjectMarkEntryController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }


    $scope.LookUps['Student'] = [];
    $scope.LookUps['ExamStudentList'] = null;
    $scope.LookUps['ExamSubjectList'] = null;

    $scope.LoadStudent = function (rowIndex) {
        $scope.LookUps['Student'] = [];
        var fitlers = new JSLINQ($scope.LookUps['ExamStudentList']).Where(function (Student) {
            var exists = new JSLINQ($scope.CRUDModel.ViewModel.SubjectMarkEntryDetails)
                .Where(function (item) {
                    if (item == undefined || item.Student == null) return false;
                    return item.Student.Key == Student.Key;
                });
            if (exists.items.length == 0) {
                $scope.LookUps['Student'].push(Student);
            }
        });
    }
    $scope.ClassSectionChange = function ($event, $element, gridModel) {
        FillGrade(1);
        FillGrade(2);
        FillGrade(3);
        FillList();

    };

    function FillList() {
        showOverlay();
        var examId = $scope.CRUDModel.ViewModel.Exam?.Key;
        if (examId == null || examId == 0) {

            hideOverlay();
            return false;
        }
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
        if (classId == null) {

            hideOverlay();
            return false;
        }
        var sectionId = $scope.CRUDModel.ViewModel.Section?.Key;
        if (sectionId == null) {

            hideOverlay();
            return false;
        }
        var subjectId = $scope.CRUDModel.ViewModel.Subject?.Key;
        if (subjectId == null) {
            hideOverlay();
            return false;
        }

        var url = "Schools/School/GetSubjectsMarkData?examId=" + examId + "&classID=" + classId + "&sectionID=" + sectionId + "&subjectId=" + subjectId;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.SubjectMarkEntryDetails = result.data;

                hideOverlay();
            }, function () {

            });
    }

    function FillGrade(gradeTypeId) {
        showOverlay();
        var examId = $scope.CRUDModel.ViewModel.Exam?.Key;
        if (examId == null || examId == 0) {

            hideOverlay();
            return false;
        }
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
        if (classId == null) {

            hideOverlay();
            return false;
        }
        var sectionId = $scope.CRUDModel.ViewModel.Section?.Key;
        if (sectionId == null) {

            hideOverlay();
            return false;
        }
        var subjectId = $scope.CRUDModel.ViewModel.Subject?.Key;
        if (subjectId == null) {
            hideOverlay();
            return false;
        }

        if (gradeTypeId == 1) {
            var url = "Schools/School/GetGradeByExamSubjects?examId=" + $scope.CRUDModel.ViewModel.Exam.Key + "&classId=" + classId + "&subjectID=" + subjectId + "&typeId=1";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.CRUDModel.ViewModel.GradeList = result.data;
                    return;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        if (gradeTypeId == 2) {
            var url = "Schools/School/GetGradeByExamSubjects?examId=" + $scope.CRUDModel.ViewModel.Exam.Key + "&classId=" + classId + "&subjectID=" + subjectId + "&typeId=2";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.CRUDModel.ViewModel.SkillGrpGradeList = result.data;
                    return;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        if (gradeTypeId == 3) {
            var url = "Schools/School/GetGradeByExamSubjects?examId=" + $scope.CRUDModel.ViewModel.Exam.Key + "&classId=" + classId + "&subjectID=" + subjectId + "&typeId=3";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.CRUDModel.ViewModel.SkillGradeList = result.data;
                    return;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }

    }
    $scope.ExamChanges = function ($event, $element, dueModel) {

        var examId = $scope.CRUDModel.ViewModel.Exam?.Key;
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
        var sectionId = $scope.CRUDModel.ViewModel.Section?.Key;
        if (examId == null || examId == 0) {
            $scope.LookUps.Classes = null;
            $scope.LookUps.Section = null;
            $scope.CRUDModel.ViewModel.Section = null;
            $scope.LookUps['ExamStudentList'] = null;
            $scope.LookUps['ExamSubjectList'] = null;

            return false;
        }

        showOverlay();
        var model = dueModel;
        var url = "Schools/School/GetClassByExam?examId=" + model.Exam.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                //$scope.CRUDModel.ViewModel.Class
                $scope.LookUps.Classes = result.data.ExamClassList;
                $scope.LookUps.Section = result.data.ExamSectionList;
                $scope.LookUps.Subject = result.data.ExamSubjectList;
                $scope.LookUps['ExamStudentList'] = result.data.ExamStudentList;
                FillGrade(1);
                FillGrade(2);
                FillGrade(3);
                FillList();
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };



    $scope.OnSubjectChange = function ($event, $element, gridModel) {

        FillGrade(1);
        FillGrade(2);
        FillGrade(3);
        FillList();

    };


    $scope.GetSkillGrade = function (gridModel, gridType) {
        var grade = '';
        var gradeID = gridModel.MarksGradeMapID;


        if (typeof (gridModel) == 'undefined') {
            return grade;
        }
        if (gridModel.IsAbsent == true) {
            gradeID = null;
            gridModel.MarksGradeMapID = null;
            gridModel.IsPassed = false;
            gridModel.Mark = 0;
            hideOverlay();
            return grade;
        }
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
        if (classId == null) {
            hideOverlay();
            classId = 0;
            return null;
        }

        var subjectId = $scope.CRUDModel.ViewModel.Subject?.Key;
        if (subjectId == null) {
            hideOverlay();
            subjectId = 0;
            return null;
        }
        var selectedMarksGrade = gridModel.MarksGradeID;
        var selectedMark = gridModel.Mark;

        //if (typeof ($scope.CRUDModel.ViewModel.SkillGradeList) == 'undefined' ) {
        //    if ($scope.CRUDModel.ViewModel.Exam.Key == null) { hideOverlay(); return false; }
        //    FillGrade(3);
        //}



        $.each($scope.CRUDModel.ViewModel.SkillGradeList, function (index, item) {

            if (item.MarksGradeID == selectedMarksGrade && item.GradeFrom <= selectedMark && item.GradeTo >= selectedMark) {
                grade = item.GradeName;
                gradeID = item.MarksGradeMapIID;
                return null;
            }
        });
        hideOverlay();
        gridModel.MarksGradeMapID = gradeID;
        gridModel.Grade = grade;
        if (gridModel.Mark != undefined && gridModel.Mark > gridModel.MaximumMark) {
            $().showMessage($scope, $timeout, true, "Mark must be less than Maximum Mark");
            gridModel.Mark = null;
            hideOverlay();
            return null;
        }
        if (gridModel.MinimumMark == undefined || gridModel.MinimumMark == 0 || gridModel.Mark == undefined || gridModel.Mark == 0) {
            gridModel.IsPassed = false;
        } else {
            gridModel.IsPassed = (gridModel.MinimumMark <= gridModel.Mark);
        }
        return;
    };

    $scope.GetSkillGroupGrade = function (gridModel, gridType) {
        var grade = '';
        var gradeID = gridModel.MarksGradeMapID;


        if (typeof (gridModel) == 'undefined') {
            return grade;
        }
        if (gridModel.IsAbsent == true) {
            gradeID = null;
            gridModel.MarksGradeMapID = null;
            gridModel.IsPassed = false;
            gridModel.Mark = 0;
            return grade;
        }
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
        if (classId == null) {
            classId = 0;
            return null;
        }
        var subjectId = $scope.CRUDModel.ViewModel.Subject?.Key;
        if (subjectId == null) {
            hideOverlay();
            subjectId = 0;
            return null;
        }

        var selectedMarksGrade = gridModel.MarksGradeID;
        var selectedMark = gridModel.Mark;


        //if (typeof ($scope.CRUDModel.ViewModel.SkillGrpGradeList) == 'undefined' ) {
        //    if ($scope.CRUDModel.ViewModel.Exam.Key == null) { return false; }
        //    FillGrade(2);
        //}



        $.each($scope.CRUDModel.ViewModel.SkillGrpGradeList, function (index, item) {

            if (item.MarksGradeID == selectedMarksGrade && item.GradeFrom <= selectedMark && item.GradeTo >= selectedMark) {
                grade = item.GradeName;
                gradeID = item.MarksGradeMapIID;
                return null;
            }
        });


        gridModel.MarksGradeMapID = gradeID;
        gridModel.Grade = grade;
        if (gridModel.Mark != undefined && gridModel.Mark > gridModel.MaximumMark) {
            $().showMessage($scope, $timeout, true, "Mark must be less than Maximum Mark");
            gridModel.Mark = null;
            hideOverlay();
            return null;
        }
        if (gridModel.MinimumMark == undefined || gridModel.MinimumMark == 0 || gridModel.Mark == undefined || gridModel.Mark == 0) {
            gridModel.IsPassed = false;
        } else {
            gridModel.IsPassed = (gridModel.MinimumMark <= gridModel.Mark);
        }
        return;
    };

    $scope.GetGrade = function (gridModel, gridType) {
        var grade = '';
        var gradeID = gridModel.MarksGradeMapID;


        if (typeof (gridModel) == 'undefined') {
            return grade;
        }
        if (gridModel.IsAbsent == true) {
            gradeID = null;
            gridModel.MarksGradeMapID = null;
            gridModel.IsPassed = false;
            gridModel.Mark = 0;
            return grade;
        }
        var classId = $scope.CRUDModel.ViewModel.Class?.Key;
        if (classId == null) {
            classId = 0;
            return null;
        }

        var subjectId = $scope.CRUDModel.ViewModel.Subject?.Key;
        if (subjectId == null) {
            hideOverlay();
            subjectId = 0;
            return null;
        }

        if (gridModel.Mark != undefined && gridModel.Mark > gridModel.MaximumMark) {
            $().showMessage($scope, $timeout, true, "Mark must be less than Maximum Mark");
            gridModel.Mark = null;
            hideOverlay();
            return false;
        }
        if (gridModel.MinimumMark == undefined || gridModel.MinimumMark == 0 || gridModel.Mark == undefined || gridModel.Mark == 0) {
            gridModel.IsPassed = false;
        } else {
            gridModel.IsPassed = (gridModel.MinimumMark <= gridModel.Mark);
        }
        var selectedMarksGrade = gridModel.MarksGradeID;
        var selectedMark = gridModel.Mark;

       
        $.each($scope.CRUDModel.ViewModel.GradeList, function (index, item) {

            if (item.MarksGradeID == selectedMarksGrade && item.GradeFrom <= selectedMark && item.GradeTo >= selectedMark) {
                grade = item.GradeName;
                gradeID = item.MarksGradeMapIID;
                return null;
            }
        });



        gridModel.MarksGradeMapID = gradeID;
        gridModel.Grade = grade;

       
        return;
    };

    $scope.SubjectCanEdit = function (gridModel) {

        return gridModel.SubjectMarkSkillGroup.length > 0 || (gridModel.IsAbsent == true);
    };

    $scope.SkillGroupCanEdit = function (gridModel) {

        return gridModel.SubjectMarkSkill.length > 0 || (gridModel.IsAbsent == true);
    };
    $scope.SkillCanEdit = function (gridModel) {

        return (gridModel.IsAbsent == true);
    };

    $scope.AbsentCanEdit = function (parentrow) {

        return (parentrow.gridModel.IsAbsent == true);
    };

    $scope.UpdateAbsent = function (gridModel, parentrow, superparentrow) {
        if (gridModel.IsAbsent == true) {
            gridModel.MarksGradeMapID = null;
            gridModel.IsPassed = false;
            gridModel.Mark = 0;
            gridModel.Grade = '';

            //Subject Level
            if (parentrow == null && superparentrow == null) {
                gridModel.SubjectMarkSkillGroup.forEach(gridModel => {
                    gridModel.MarksGradeMapID = null;
                    gridModel.IsPassed = false;
                    gridModel.Mark = 0;
                    gridModel.Grade = '';
                    gridModel.IsAbsent = true;
                    gridModel.SubjectMarkSkill.forEach(gridModel => {
                        gridModel.MarksGradeMapID = null;
                        gridModel.IsPassed = false;
                        gridModel.Mark = 0;
                        gridModel.Grade = '';
                        gridModel.IsAbsent = true;
                    });
                });
            }
            //Skill Group Level
            else if (parentrow != null && superparentrow == null) {
                $scope.UpdateSubject(gridModel, parentrow);
                gridModel.SubjectMarkSkill.forEach(gridModel => {
                    gridModel.MarksGradeMapID = null;
                    gridModel.IsPassed = false;
                    gridModel.Mark = 0;
                    gridModel.Grade = '';
                    gridModel.IsAbsent = true;
                });
            }
            //Skill Level
            else if (parentrow != null && superparentrow != null) {
                $scope.UpdateSkillGroup(gridModel, parentrow, superparentrow);
            }
        }
        else {
            
            //Subject Level
            if (parentrow == null && superparentrow == null) {
                gridModel.SubjectMarkSkillGroup.forEach(gridModel => {
                    gridModel.IsAbsent = false;
                    gridModel.SubjectMarkSkill.forEach(gridModel => {
                        gridModel.IsAbsent = false;
                    });
                });
            }
            //Skill Group Level
            else if (parentrow != null && superparentrow == null) {
                gridModel.SubjectMarkSkill.forEach(gridModel => {
                    gridModel.IsAbsent = false;
                });
            }

        }

    }

    $scope.UpdateSkillGroup = function (gridModel, parentrow, superparentrow) {
        var mark = 0;
        parentrow.gridModel.SubjectMarkSkill.forEach(element => {
            mark += parseInt(element.Mark);
        });
        parentrow.gridModel.Mark = mark;
        if (superparentrow != undefined && superparentrow != null) {
            var pmark = 0;
            superparentrow.gridModel.SubjectMarkSkillGroup.forEach(element => {
                pmark += parseInt(element.Mark);
            });
            superparentrow.gridModel.Mark = pmark;
        }
        $scope.GetSkillGrade(gridModel, 3);
        $scope.GetSkillGroupGrade(parentrow.gridModel, 2);
        $scope.GetGrade(superparentrow.gridModel, 1);
    };
    $scope.UpdateSubject = function (gridModel, parentrow) {
        var mark = 0;
        parentrow.gridModel.SubjectMarkSkillGroup.forEach(element => {
            mark += parseInt(element.Mark);
        });
        parentrow.gridModel.Mark = mark;
        $scope.GetSkillGroupGrade(gridModel, 2);
        $scope.GetGrade(gridModel, 1);

    };
  
}]);