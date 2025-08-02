app.controller("MarkRegisterController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

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
            var exists = new JSLINQ($scope.CRUDModel.ViewModel.MarkRegisterDetails)
                .Where(function (item) {
                    if (item == undefined || item.Student == null) return false;
                    return item.Student.Key == Student.Key;
                });
            if (exists.items.length == 0) {
                $scope.LookUps['Student'].push(Student);
            }
        });
    }

    $scope.ClassSectionChange = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;
        var classId = $scope.CRUDModel.ViewModel.StudentClass?.Key;
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

                $scope.LookUps.Students = result.data;
                FillGrade(1);
                FillGrade(2);
                FillGrade(3);
                hideOverlay();

            }, function () {

                hideOverlay();

            });
    };


    function FillGrade(gradeTypeId) {
        showOverlay();
        var examId = $scope.CRUDModel.ViewModel.Exam?.Key;
        if (examId == null || examId == 0) {

            hideOverlay();
            return false;
        }
        var classId = $scope.CRUDModel.ViewModel.StudentClass?.Key;
        if (classId == null) {

            hideOverlay();
            return false;
        }
        var sectionId = $scope.CRUDModel.ViewModel.Section?.Key;
        if (sectionId == null) {

            hideOverlay();
            return false;
        }


        if (gradeTypeId == 1) {
            var url = "Schools/School/lByExamSubjects?examId=" + $scope.CRUDModel.ViewModel.Exam.Key + "&classId=" + classId + "&subjectID=0&typeId=1";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.CRUDModel.ViewModel.GradeList = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        if (gradeTypeId == 2) {
            var url = "Schools/School/GetGradeByExamSubjects?examId=" + $scope.CRUDModel.ViewModel.Exam.Key + "&classId=" + classId + "&subjectID=0&typeId=2";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.CRUDModel.ViewModel.SkillGrpGradeList = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        if (gradeTypeId == 3) {
            var url = "Schools/School/GetGradeByExamSubjects?examId=" + $scope.CRUDModel.ViewModel.Exam.Key + "&classId=" + classId + "&subjectID=0&typeId=3";
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.CRUDModel.ViewModel.SkillGradeList = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }

    }
    $scope.ExamChanges = function ($event, $element, dueModel) {
        var examId = $scope.CRUDModel.ViewModel.Exam?.Key;
        var classId = $scope.CRUDModel.ViewModel.StudentClass?.Key;
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
        //$scope.CRUDModel.ViewModel.MarkRegisterDetails.Student = {};
        var model = dueModel;
        var url = "Schools/School/GetClassByExam?examId=" + model.Exam.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.MarkRegisterDetails.Student = result.data.ExamStudentList;
                //$scope.CRUDModel.ViewModel.StudentClass
                $scope.LookUps.Classes = result.data.ExamClassList;
                $scope.LookUps.Section = result.data.ExamSectionList;
                $scope.LookUps['ExamStudentList'] = result.data.ExamStudentList;
                $scope.LookUps['ExamSubjectList'] = result.data.ExamSubjectList;
                FillGrade(1);
                FillGrade(2);
                FillGrade(3);
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.OnStudentChange = function (gridmodel, element, $index) {
        showOverlay();
        var examId = $scope.CRUDModel.ViewModel.Exam?.Key;
        if (examId == null || examId == 0) {

            $().showMessage($scope, $timeout, true, "Please Select an Exam");
            hideOverlay();
            return false;
        }
        var classId = $scope.CRUDModel.ViewModel.StudentClass?.Key;
        if (classId == null) {

            classId = 0;
            return false;
            //$().showMessage($scope, $timeout, true, "Please Select Class");
            //hideOverlay();            
            //return false;
        }
        var studentId = gridmodel.Student?.Key;
        if (studentId == null || studentId == 0) {
            $().showMessage($scope, $timeout, true, "Please Select a Student");
            hideOverlay();
            return false;
        }

        gridmodel.MarkRegisterDetailsSplit = [];
        var url = "Schools/School/GetExamSubjectsMarks?studentId=" + studentId + "&examId=" + examId + "&classId=" + classId;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                gridmodel.MarkRegisterDetailsSplit = result.data;
                hideOverlay();

            }, function () {
                hideOverlay();
            });

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
        var classId = $scope.CRUDModel.ViewModel.StudentClass?.Key;
        if (classId == null) {
            hideOverlay();
            classId = 0;
            return false;
        }
        if (gridModel.Mark > gridModel.MaximumMark) {
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

        $.each($scope.CRUDModel.ViewModel.SkillGradeList, function (index, item) {

            if (item.MarksGradeID == selectedMarksGrade && item.GradeFrom <= selectedMark && item.GradeTo >= selectedMark) {
                grade = item.GradeName;
                gradeID = item.MarksGradeMapIID;
                return false;
            }
        });
        
        gridModel.Grade = grade;
        gridModel.MarksGradeMapID = gradeID;
        hideOverlay();
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
        var classId = $scope.CRUDModel.ViewModel.StudentClass?.Key;
        if (classId == null) {
            classId = 0;
            return false;
        }
        if (gridModel.Mark > gridModel.MaximumMark) {
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




        $.each($scope.CRUDModel.ViewModel.SkillGrpGradeList, function (index, item) {

            if (item.MarksGradeID == selectedMarksGrade && item.GradeFrom <= selectedMark && item.GradeTo >= selectedMark) {
                grade = item.GradeName;
                gradeID = item.MarksGradeMapIID;
                return false;
            }
        });


        gridModel.MarksGradeMapID = gradeID;
        gridModel.Grade = grade;
      
        return grade;
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
        var classId = $scope.CRUDModel.ViewModel.StudentClass?.Key;
        if (classId == null) {
            classId = 0;
            return false;
        }
        if (gridModel.Mark > gridModel.MaximumMark) {
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
                return false;
            }
        });


        gridModel.Grade = grade;
        gridModel.MarksGradeMapID = gradeID;
        
        return grade;
    };


    $scope.SubjectCanEdit = function (gridModel) {
        return gridModel.MarkRegSkillGroupSplit.length > 0 || (gridModel.IsAbsent == true);
    };

    $scope.SkillGroupCanEdit = function (gridModel) {
        return gridModel.MarkRegSkillSplit.length > 0 || (gridModel.IsAbsent == true);
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
                gridModel.MarkRegSkillGroupSplit.forEach(gridModel => {
                    gridModel.MarksGradeMapID = null;
                    gridModel.IsPassed = false;
                    gridModel.Mark = 0;
                    gridModel.Grade = '';
                    gridModel.IsAbsent = true;
                    gridModel.MarkRegSkillSplit.forEach(gridModel => {
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
                gridModel.MarkRegSkillSplit.forEach(gridModel => {
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
                gridModel.MarkRegSkillGroupSplit.forEach(gridModel => {
                    gridModel.IsAbsent = false;
                    gridModel.MarkRegSkillSplit.forEach(gridModel => {
                        gridModel.IsAbsent = false;
                    });
                });
            }
            //Skill Group Level
            else if (parentrow != null && superparentrow == null) {
                gridModel.MarkRegSkillSplit.forEach(gridModel => {
                    gridModel.IsAbsent = false;
                });
            }

        }

    }


    $scope.UpdateSkillGroup = function (gridModel, parentrow, superparentrow) {

        var mark = 0;
        parentrow.gridModel.MarkRegSkillSplit.forEach(element => {
            mark += parseInt(element.Mark);
        });
        parentrow.gridModel.Mark = mark;
        if (superparentrow != undefined && superparentrow != null) {
            var pmark = 0;
            superparentrow.gridModel.MarkRegSkillGroupSplit.forEach(element => {
                pmark += parseInt(element.Mark);
            });
            superparentrow.gridModel.Mark = pmark;
        }
        $scope.GetSkillGrade(gridModel, 3);
        $scope.GetSkillGroupGrade(parentrow.gridModel, 2);
        $scope.GetGrade(superparentrow.gridModel, 1);
    };

    $scope.UpdateSubject = function (gridModel,parentrow) {
        var mark = 0;
        parentrow.gridModel.MarkRegSkillGroupSplit.forEach(element => {
            mark += parseInt(element.Mark);
        });
        parentrow.gridModel.Mark = mark;
        $scope.GetSkillGroupGrade(gridModel, 2);
        $scope.GetGrade(gridModel, 1);

    };

    $scope.UpdateFromSubject = function (gridModel) {
      
        $scope.GetGrade(gridModel, 1);

    };
}]);