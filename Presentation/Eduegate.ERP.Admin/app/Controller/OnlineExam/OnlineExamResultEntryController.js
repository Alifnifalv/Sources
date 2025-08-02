app.controller("OnlineExamResultEntryController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("OnlineExamResultEntryController Loaded");

    $scope.SearchArgs = [];
    $scope.isMarkEntry = true;
    $scope.isCoScholasticEntry = false;
    $scope.MarkEntry = [];
    $scope.ResultSubjects = [];
    $scope.Candidates = [];
   
    $scope.Mark = {};
 
    $scope.Mark.Subjects = [];
    $scope.Mark.Exams = [];
   
    $scope.SearchArgs.Mark = {};
   
    $scope.SearchArgs.Mark.SubjectID = {};
    $scope.SearchArgs.Mark.ExamID = {};
    $scope.SearchArgs.Mark.LanguageTypeID = {};
    //$scope.SearchArgs.Mark.SubjectMapTypeID = {};
    $scope.MaxMark = 0;
   
    $scope.SearchArgs.IsFetched = false;
    $scope.SearchArgs.submitted = false;

   
    $scope.Init = function (model, windowname, type) {
        $scope.type = type;



        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Candidate&defaultBlank=false",
        }).then(function (result) {

            $scope.Candidates = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=OnlineExams&defaultBlank=false",
        }).then(function (result) {

            $scope.Mark.Exams  = result.data;
        });


        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=AcademicYear&defaultBlank=false",
        }).then(function (result) {
            $scope.AcademicYears = result.data;
            $scope.loadCurrentAcademicYear();

        });

       

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Subject&defaultBlank=false",
        }).then(function (result) {
            $scope.Mark.Subjects = result.data;
        });
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=OnlineExams&defaultBlank=false",
        }).then(function (result) {
            $scope.Mark.Exams = result.data;
        });

       
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=LanguageType&defaultBlank=true",
        }).then(function (result) {

            $scope.LanguageTypes = result.data;
        });

    };

   

    $scope.loadCurrentAcademicYear = function () {
        showOverlay();

        $.ajax({
            type: "GET",

            url: "Schools/School/GetCurrentAcademicYear",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result != null) {

                    $scope.$apply(function () {
                        $scope.SearchArgs.AcademicYear = $scope.AcademicYears.find(x => x.Key == result);
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                hideOverlay();
            }

        });
    }

    $scope.loadMarkEntrySearchCriteria = function (selected) {
        showOverlay();

        $scope.MarkEntry = [];
        $scope.MaxMark = {};
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        if (academicYearId == undefined) {
            hideOverlay();
            $().showGlobalMessage($root, $timeout, true, "Please select Academic Year");
            return false;
        } else {
           
            if (selected.ngModel.$name == "MarkExam") {

                $scope.SearchArgs.Mark.ExamID = selected.selected;
               
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var examId = $scope.SearchArgs.Mark.ExamID?.Key;
                var languageTypeId = $scope.SearchArgs.Mark.LanguageTypeID?.Key ?? null;
                $scope.SearchArgs.Mark.SubjectID = {};
                $scope.SearchArgs.Mark.Subjects = [];
                
                $.ajax({
                    type: "GET",
                    data: { examID: examId, academicYearID: academicYearId, languageTypeID: languageTypeId },
                    url: "OnlineExams/OnlineExam/GetSubjectsByOnlineExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.Mark.Subjects = result;
                            });
                        }
                    },
                    error: function () {

                    },
                    complete: function (result) {
                        hideOverlay();
                    }
                });
            }
            if (selected.ngModel.$name == "MarkLanguageTypes") {

                $scope.SearchArgs.Mark.LanguageTypeID = selected.selected;
               
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var examId = $scope.SearchArgs.Mark.ExamID?.Key;
                var languageTypeId = $scope.SearchArgs.Mark.LanguageTypeID?.Key ?? null;
                $scope.SearchArgs.Mark.SubjectID = {};
                $scope.SearchArgs.Mark.Subjects = [];
              
                $.ajax({
                    type: "GET",
                    data: {  examID: examId, academicYearID: academicYearId, languageTypeID: languageTypeId },
                    url: "OnlineExams/OnlineExam/GetSubjectsByOnlineExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.Mark.Subjects = result;
                            });
                        }
                    },
                    error: function () {

                    },
                    complete: function (result) {
                        hideOverlay();
                    }
                });
            }
            //if (selected.ngModel.$name == "MarkSubjectMapTypes") {
            //    $scope.SearchArgs.Mark.SubjectMapTypeID = selected.selected;                
            //    hideOverlay();
            //}
            if (selected.ngModel.$name == "MarkSubject") {
                $scope.SearchArgs.Mark.SubjectID = selected.selected;
               
                hideOverlay();
            }
        }
    }

   
    $scope.isEmptyObject = function (obj) {
        return angular.equals({}, obj);
    };
    $scope.GetMarkEntry = function () {
        showOverlay();
        $scope.SearchArgs.IsFetched = true;
       
        var subjectId = $scope.SearchArgs.Mark.SubjectID?.Key;
        var examID = $scope.SearchArgs.Mark.ExamID?.Key;
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        var languageTypeId = $scope.SearchArgs.Mark.LanguageTypeID?.Key ?? null;
        if ( examID == undefined || academicYearId == undefined) {
            hideOverlay();
            $().showGlobalMessage($root, $timeout, true, "Please fill out required fields!");
            return false;
        } else {
            $scope.SearchArgs.IsFetched = false;
        }
        $scope.MarkEntry = [];
        $scope.MaxMark = 0;
        $scope.ResultSubjects = [];
        $.ajax({
            url: "OnlineExams/OnlineExam/GetOnlineExamEntryResults",

            type: "GET",
            data: { subjectId: subjectId, examId: examID, academicYearID: academicYearId, languageTypeID: languageTypeId, subjectId: subjectId },
            success: function (result) {
                $scope.$apply(function () {
                    $scope.MarkEntry = result;
                    //if ($scope.MarkEntry.length > 0) {
                    //    $scope.MaxMark = $scope.MarkEntry[0].MaxMark;
                       
                    //} else {

                    //    $().showGlobalMessage($root, $timeout, true, "No record(s) found!");
                    //}
                    if ($scope.MarkEntry.length > 0) {
                        $scope.ResultSubjects = $scope.MarkEntry[0].ResultSubjects;
                        $scope.MaxMark = $scope.MarkEntry[0].MaxMark;

                    } else {

                        $().showGlobalMessage($root, $timeout, true, "No record(s) found!");
                    }
                });
            },
            error: function (result) {
                $().showGlobalMessage($root, $timeout, true, "Failed to fetch!");
            },
            complete: function (result) {

                hideOverlay();
            }
        });
    }
    $scope.ResetFields = function () {
     
        $scope.SearchArgs.Mark.SubjectID = null;
        $scope.SearchArgs.Mark.ExamID = null;
        $scope.SearchArgs.Mark.LanguageTypeID = null;
        $scope.MarkEntry = [];
    }

   

    $scope.SaveMarkEntry = function () {
        //showOverlay();
      
        var subjectId = $scope.SearchArgs.Mark.SubjectID?.Key;
        var examID = $scope.SearchArgs.Mark.ExamID?.Key;
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        $.ajax({
            type: "POST",
            data: {
                "OnlineExamID": examID, "SubjectId": subjectId, "AcademicYearID": academicYearId, "CandidateResultEntry": $scope.MarkEntry
            },
            url: utility.myHost + "OnlineExams/OnlineExam/SaveOnlineExamResultEntries",
            success: function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.Message);
                    return;
                } else {
                    $scope.$apply(function () {
                        $().showGlobalMessage($root, $timeout, false, "Saved successfully!");
                        $scope.MarkEntry = result.MarkEntryStudents;
                    });
                }
            },
            error: function (result) {
                $().showGlobalMessage($root, $timeout, true, "Failed to save!");
            },
            complete: function (result) {
                // hideOverlay();
            }
        });
    }

    $scope.SaveCoScholasticEntry = function () {
        showOverlay();
        var classId = $scope.SearchArgs.ClassID?.Key;
        var sectionId = $scope.SearchArgs.SectionID?.Key;
        var examGroupID = $scope.SearchArgs.TermID?.Key;
        var skillSetID = $scope.SearchArgs.SkillSetID?.Key;
        var skillGroupID = $scope.SearchArgs.SkillGroupID?.Key;
        var skillID = $scope.SearchArgs.SkillID?.Key;
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        var examID = $scope.SearchArgs.ExamID?.Key;
        $.ajax({
            type: "POST",
            data: {
                "ClassID": classId, "ExamGroupID": examGroupID, "SectionId": sectionId, "SkillGroupID": skillGroupID, "SkillSetID": skillSetID, "ExamID": examID, "AcademicYearID": academicYearId, "SkillID": skillID, "Students": $scope.CoScholasticEntry
            },
            url: utility.myHost + "Schools/School/SaveCoScholasticEntry",
            //contentType: "application/json;charset=utf-8",

            success: function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.Message);
                    return;
                } else {
                    $scope.$apply(function () {
                        $().showGlobalMessage($root, $timeout, false, "Saved successfully!");
                        $scope.CoScholasticEntry = [];//result.Students;
                        $scope.CoScholasticSkills = [];//$scope.CoScholasticEntry[0].SkillGroups.Skills;

                    });
                }
            },
            error: function (result) {
                $().showGlobalMessage($root, $timeout, true, "Failed to save!");
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.ValidateMark = function (student, mark) {

        if (student.MaxMark == null || mark == null) {
            return null;
        }
        if (mark == '' || isNaN(parseFloat(mark)) || !isFinite(mark)) {
            $().showGlobalMessage($root, $timeout, true, "Please enter a numeric value!");
            return null;
        } else if (parseFloat(mark) > parseFloat(student.MaxMark)) {
            $().showGlobalMessage($root, $timeout, true, "Mark must be less than Maximum Mark!");
            mark = null;
            return false;
        }


    };

    function showOverlay() {
        $('.preload-overlay', $('#OnlineExamResult')).attr('style', 'display:block');
    };

    function hideOverlay() {
        $('.preload-overlay', $('#OnlineExamResult')).hide();
    };

    $scope.IsSelected = function (field1, field2) {
        if (field1 != null && field1 != null) {
            if (String(field1) == String(field2)) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    };

    $scope.onChangeTab = function (tab) {
        if (tab == 'MarksEntryPanel') {
            $scope.isMarkEntry = true;
            $scope.isCoScholasticEntry = false;

        }
        else if (tab == 'CoScholasticEntryPanel') {
            $scope.isMarkEntry = false;
            $scope.isCoScholasticEntry = true;
        }
    };

    $scope.updateMark = function (mark) {
        mark.isEdit = true;
    };

    function FillGrade() {
        showOverlay();

        var classId = $scope.SearchArgs.Mark.ClassID?.Key;
        var examGroupID = $scope.SearchArgs.Mark.TermID?.Key;
        var subjectID = $scope.SearchArgs.Mark.SubjectID?.Key;
        var examID = $scope.SearchArgs.Mark.ExamID?.Key;
        var url = "Schools/School/GetGradeByExamSubjects?examId=" + examID + "&classId=" + classId + "&subjectID=" + subjectID + "&typeId=1";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.SubjectGrades = result.data;

                $scope.FilterGrades = jQuery.map($scope.SubjectGrades, function (n, i) {
                    return {
                        Key: n.MarksGradeMapIID,
                        Value: n.GradeName
                    }

                });

                $scope.Mark.Grades = $scope.FilterGrades;

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

}]);

