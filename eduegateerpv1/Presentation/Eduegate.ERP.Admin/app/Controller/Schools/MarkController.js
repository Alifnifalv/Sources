app.controller("MarkController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("MarkController Loaded");

    $scope.SearchArgs = [];
    $scope.isMarkEntry = true;
    $scope.isCoScholasticEntry = false;
    $scope.isScholasticInternal = false;
    $scope.MarkEntry = [];
    $scope.CoScholasticEntry = [];
    $scope.CoScholasticSkills = [];
    $scope.Grades = [];
    $scope.SubjectGrades = [];
    $scope.Class = [];
    $scope.Section = [];
    $scope.Terms = [];
    $scope.SkillSets = [];
    $scope.SkillGroups = [];
    $scope.Skills = [];
    $scope.Exams = [];
    $scope.Subjects = [];
    $scope.LanguageTypes = [];
    $scope.SubjectMapTypes = [];
    $scope.SearchArgs.ClassID = {};
    $scope.SearchArgs.SectionID = {};
    $scope.SearchArgs.TermID = {};
    $scope.SearchArgs.SkillSetID = {};
    $scope.SearchArgs.SkillGroupID = {};
    $scope.SearchArgs.SkillID = {};
    $scope.SearchArgs.LanguageTypeID = {};
    $scope.SearchArgs.SubjectMapTypeID = {};


    $scope.Internal = [];
    $scope.Internal.Grades = [];
    $scope.Internal.SubjectGrades = [];
    $scope.Internal.Class = [];
    $scope.Internal.Section = [];
    $scope.Internal.Terms = [];
    $scope.Internal.SkillSets = [];
    $scope.Internal.SkillGroups = [];
    $scope.Internal.Skills = [];
    $scope.Internal.Exams = [];
    $scope.Internal.Subjects = [];
    $scope.Internal.LanguageTypes = [];              

    $scope.SearchArgs.Internal = [];
    $scope.SearchArgs.Internal.ClassID = {};
    $scope.SearchArgs.Internal.SectionID = {};
    $scope.SearchArgs.Internal.TermID = {};
    $scope.SearchArgs.Internal.SubjectID = {};
    $scope.SearchArgs.Internal.ExamID = {};
    $scope.SearchArgs.Internal.LanguageTypeID = {};
    $scope.SearchArgs.Internal.SkillSetID = {};
    $scope.SearchArgs.Internal.SkillGroupID = {};
    $scope.SearchArgs.Internal.SkillID = {};
    $scope.SearchArgs.Internal.SubjectMapTypeID = {};
  
    $scope.Mark = {};
    $scope.Mark.Class = [];
    $scope.Mark.Section = [];
    $scope.Mark.Terms = [];
    $scope.Mark.Subjects = [];
    $scope.Mark.Exams = [];
    $scope.Mark.Grades = [];
    $scope.SearchArgs.Mark = {};
    $scope.SearchArgs.Mark.ClassID = {};
    $scope.SearchArgs.Mark.SectionID = {};
    $scope.SearchArgs.Mark.TermID = {};
    $scope.SearchArgs.Mark.SubjectID = {};
    $scope.SearchArgs.Mark.ExamID = {};
    $scope.SearchArgs.Mark.LanguageTypeID = {};
    //$scope.SearchArgs.Mark.SubjectMapTypeID = {};
    $scope.MaxMark = 0;
    $scope.MarkConvertionFactor = 0;
    $scope.SearchArgs.IsFetched = false;
    $scope.SearchArgs.submitted = false;

    //Initializing
    $scope.Init = function (model, windowname, type) {
        $scope.type = type;

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Classes&defaultBlank=false",
        }).then(function (result) {
            $scope.Classes = result.data;
            $scope.Mark.Classes = result.data;
        });

        //Sections
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Section&defaultBlank=false",
        }).then(function (result) {
            $scope.Sections = result.data;
            $scope.Mark.Sections = result.data;
        });
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=ExamGroups&defaultBlank=false",
        }).then(function (result) {
            $scope.Terms = result.data;
            $scope.Mark.Terms = result.data;
        });


        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=AcademicYear&defaultBlank=false",
        }).then(function (result) {
            $scope.AcademicYears = result.data;
            $scope.loadCurrentAcademicYear();

        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=MarkStatuses&defaultBlank=true",
        }).then(function (result) {
            $scope.MarkStatuses = result.data;

        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Subject&defaultBlank=false",
        }).then(function (result) {
            $scope.Mark.Subjects = result.data;
        });
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Exams&defaultBlank=false",
        }).then(function (result) {
            $scope.Mark.Exams = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=SubjectMap&defaultBlank=true",
        }).then(function (result) {

            $scope.SubjectMapTypes = result.data;
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
            if (selected.ngModel.$name == "MarkClass") {
                //$scope.SearchArgs.Mark.TermID = {};
                //$scope.Mark.Terms = [];
                $scope.SearchArgs.Mark.SubjectID = {};
                $scope.Mark.Subjects = [];
                $scope.SearchArgs.Mark.ExamID = {};
                $scope.Mark.Exams = [];
                $scope.SearchArgs.Mark.ClassID = selected.selected;
                hideOverlay();
            }
            if (selected.ngModel.$name == "MarkSection") {

                $scope.SearchArgs.Mark.SectionID = selected.selected;
                $scope.SearchArgs.Mark.SubjectID = {};
                $scope.Mark.Subjects = [];
                $scope.SearchArgs.Mark.ExamID = {};
                $scope.Mark.Exams = [];

                hideOverlay();
            }
            if (selected.ngModel.$name == "MarkTerm") {
                $scope.SearchArgs.Mark.SubjectID = {};
                $scope.Mark.Subjects = [];
                $scope.SearchArgs.Mark.ExamID = {};
                $scope.Mark.Exams = [];

                $scope.SearchArgs.Mark.TermID = selected.selected;
                var classId = $scope.SearchArgs.Mark.ClassID?.Key;
                var sectionId = $scope.SearchArgs.Mark.SectionID?.Key ?? null;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var termID = $scope.SearchArgs.Mark.TermID?.Key;

                var tabType = null;
                if ($scope.isMarkEntry == true) {
                    tabType = "Scholastic";
                }
                else if ($scope.isCoScholasticEntry == true) {
                    tabType = "Co-Scholastic";
                }
                else {
                    tabType = "Scholastic Internals";
                }
                $.ajax({
                    type: "GET",
                    data: { classID: classId, sectionID: sectionId, termID: termID, academicYearID: academicYearId, tab: tabType },
                    url: "Schools/School/GetExamsByTermID",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.Mark.Exams = result;
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
            if (selected.ngModel.$name == "MarkExam") {

                $scope.SearchArgs.Mark.ExamID = selected.selected;
                var classId = $scope.SearchArgs.Mark.ClassID?.Key;
                var sectionId = $scope.SearchArgs.Mark.SectionID?.Key ?? null;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var examId = $scope.SearchArgs.Mark.ExamID?.Key;
                var languageTypeId = $scope.SearchArgs.Mark.LanguageTypeID?.Key ?? null;
                $scope.SearchArgs.Mark.SubjectID = {};
                $scope.SearchArgs.Mark.Subjects = [];
                $scope.Mark.Grades = [];
                $.ajax({
                    type: "GET",
                    data: { classID: classId, sectionID: sectionId, examID: examId, academicYearID: academicYearId, languageTypeID: languageTypeId },
                    url: "Schools/School/GetSubjectsBySubjectType",
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
                var classId = $scope.SearchArgs.Mark.ClassID?.Key;
                var sectionId = $scope.SearchArgs.Mark.SectionID?.Key ?? null;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var examId = $scope.SearchArgs.Mark.ExamID?.Key;
                var languageTypeId = $scope.SearchArgs.Mark.LanguageTypeID?.Key ?? null;
                $scope.SearchArgs.Mark.SubjectID = {};
                $scope.SearchArgs.Mark.Subjects = [];
                $scope.Mark.Grades = [];
                $.ajax({
                    type: "GET",
                    data: { classID: classId, sectionID: sectionId, examID: examId, academicYearID: academicYearId, languageTypeID: languageTypeId },
                    url: "Schools/School/GetSubjectsBySubjectType",
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
                $scope.Mark.Grades = [];
                hideOverlay();
            }
        }
    }

    $scope.loadSearchCriteria = function (selected) {
        showOverlay();
        $scope.CoScholasticEntry = [];
        $scope.CoScholasticSkills = [];
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        if (academicYearId == undefined) {
            hideOverlay();
            $().showGlobalMessage($root, $timeout, true, "Please select Academic Year");
            return false;
        } else {
            if (selected.ngModel.$name == "coClass") {

                //$scope.SearchArgs.TermID = {};
                //$scope.Terms = [];
                $scope.SearchArgs.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.SkillID = {};
                $scope.Skills = [];
                $scope.SearchArgs.ExamID = {};
                $scope.Exams = [];

                $scope.SearchArgs.ClassID = selected.selected;


                hideOverlay();

            }
            if (selected.ngModel.$name == "CoSection") {
                $scope.SearchArgs.SectionID = selected.selected;
                $scope.SearchArgs.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.SkillID = {};
                $scope.Skills = [];
                $scope.SearchArgs.ExamID = {};
                $scope.Exams = [];
                hideOverlay();

            }
            if (selected.ngModel.$name == "CoTerms") {

                $scope.SearchArgs.TermID = selected.selected;

                $scope.SearchArgs.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.SkillID = {};
                $scope.Skills = [];
                $scope.SearchArgs.ExamID = {};
                $scope.Exams = [];

                var classId = $scope.SearchArgs.ClassID?.Key;
                var sectionId = $scope.SearchArgs.SectionID?.Key ?? null;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var termID = $scope.SearchArgs.TermID?.Key;

                var tabType = null;
                if ($scope.isMarkEntry == true) {
                    tabType = "Scholastic";
                }
                else if ($scope.isCoScholasticEntry == true) {
                    tabType = "Co-Scholastic";
                }
                else {
                    tabType = "Scholastic Internals";
                }

                $.ajax({
                    type: "GET",
                    data: { classID: classId, sectionID: sectionId, termID: termID, academicYearID: academicYearId, tab: tabType },
                    url: "Schools/School/GetExamsByTermID",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.Exams = result;
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
            if (selected.ngModel.$name == "coExams") {

                $scope.SearchArgs.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.SkillID = {};
                $scope.Skills = [];

                $scope.SearchArgs.ExamID = selected.selected;

                var classId = $scope.SearchArgs.ClassID?.Key;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var examID = $scope.SearchArgs.ExamID?.Key ?? null;
                var sectionId = $scope.SearchArgs.SectionID?.Key ?? null;
                var languageTypeId = $scope.SearchArgs.LanguageTypeID?.Key ?? null;
                $.ajax({
                    type: "GET",
                    data: { classID: classId, sectionId: sectionId, examID: examID, academicYearID: academicYearId, languageTypeID: languageTypeId },
                    url: "Schools/School/GetSkillSetByClassExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.SkillSets = result;
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
            if (selected.ngModel.$name == "coLanguageTypes") {

                $scope.SearchArgs.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.SkillID = {};
                $scope.Skills = [];

                $scope.SearchArgs.LanguageTypeID = selected.selected;

                var classId = $scope.SearchArgs.ClassID?.Key;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var examID = $scope.SearchArgs.ExamID?.Key ?? null;
                var sectionId = $scope.SearchArgs.SectionID?.Key ?? null;
                var languageTypeId = $scope.SearchArgs.LanguageTypeID?.Key ?? null;
                $.ajax({
                    type: "GET",
                    data: { classID: classId, sectionId: sectionId, examID: examID, academicYearID: academicYearId, languageTypeID: languageTypeId },
                    url: "Schools/School/GetSkillSetByClassExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.SkillSets = result;

                            });
                        }
                    },
                    error: function () {

                    },
                    complete: function (result) {
                        hideOverlay();
                        return;
                    }
                });

            }
            if (selected.ngModel.$name == "coSubjectMapTypes") {
                $scope.SearchArgs.SubjectMapTypeID = selected.selected;
                hideOverlay();
            }
            if (selected.ngModel.$name == "coSkillSets") {

                $scope.SearchArgs.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.SkillID = {};
                $scope.Skills = [];

                $scope.SearchArgs.SkillSetID = selected.selected;
                var classId = $scope.SearchArgs.ClassID?.Key;
                var examGroupID = $scope.SearchArgs.TermID?.Key;
                var skillSetID = $scope.SearchArgs.SkillSetID?.Key;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                $.ajax({
                    type: "GET",
                    data: { classID: classId, examGroupID: examGroupID, skillSetID: skillSetID, academicYearID: academicYearId },
                    url: "Schools/School/GetSkillGroupByClassExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.SkillGroups = result;
                            });
                        }
                    },
                    error: function () {

                    },
                    complete: function (result) {
                        hideOverlay();
                        return;
                    }
                });
            }
            if (selected.ngModel.$name == "coSkillGroups") {

                $scope.SearchArgs.SkillID = {};
                $scope.Skills = [];

                $scope.SearchArgs.SkillGroupID = selected.selected;
                var classId = $scope.SearchArgs.ClassID?.Key;
                var examGroupID = $scope.SearchArgs.TermID?.Key;
                var skillSetID = $scope.SearchArgs.SkillSetID?.Key;
                var skillGroupID = $scope.SearchArgs.SkillGroupID?.Key;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                $.ajax({
                    type: "GET",
                    data: { classID: classId, skillGroupID: skillGroupID, skillSetID: skillSetID, academicYearID: academicYearId },
                    url: "Schools/School/GetSkillsByClassExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.Skills = result;
                            });
                        }
                    },
                    error: function () {

                    },
                    complete: function (result) {
                        hideOverlay();
                        return;
                    }
                });

            }
            if (selected.ngModel.$name == "coSkills") {
                $scope.SearchArgs.SkillID = selected.selected;
                hideOverlay();
                return;
            }
        }
    }
    
    $scope.loadSearchScholIntrnCriteria = function (selected) {
        showOverlay();
        $scope.ScholasticInternalEntry = [];
        $scope.ScholasticInternalSkills = [];
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        if (academicYearId == undefined) {
            hideOverlay();
            $().showGlobalMessage($root, $timeout, true, "Please select Academic Year");
            return false;
        } else {
            if (selected.ngModel.$name == "intClass") {

                //$scope.SearchArgs.TermID = {};
                //$scope.Terms = [];
                $scope.SearchArgs.Internal.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.Internal.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.Internal.SkillID = {};
                $scope.Skills = [];
                $scope.SearchArgs.Internal.ExamID = {};
                $scope.Exams = [];

                $scope.SearchArgs.Internal.ClassID = selected.selected;


                hideOverlay();

            }
            if (selected.ngModel.$name == "intSection") {
                $scope.SearchArgs.Internal.SectionID = selected.selected;
                $scope.SearchArgs.Internal.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.Internal.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.Internal.SkillID = {};
                $scope.Skills = [];
                $scope.SearchArgs.Internal.ExamID = {};
                $scope.Exams = [];
                hideOverlay();

            }
            if (selected.ngModel.$name == "intTerms") {

                $scope.SearchArgs.TermID = selected.selected;

                $scope.SearchArgs.Internal.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.Internal.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.Internal.SkillID = {};
                $scope.Skills = [];
                $scope.SearchArgs.Internal.ExamID = {};
                $scope.Exams = [];

                var classId = $scope.SearchArgs.Internal.ClassID?.Key;
                var sectionId = $scope.SearchArgs.Internal.SectionID?.Key ?? null;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var termID = $scope.SearchArgs.Internal.TermID?.Key;

                var tabType = null;
                if ($scope.isMarkEntry == true) {
                    tabType = "Scholastic";
                }
                else if ($scope.isCoScholasticEntry == true) {
                    tabType = "Co-Scholastic";
                }
                else {
                    tabType = "Scholastic Internals";
                }

                $.ajax({
                    type: "GET",
                    data: { classID: classId, sectionID: sectionId, termID: termID, academicYearID: academicYearId, tab: tabType },
                    url: "Schools/School/GetExamsByTermID",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.Exams = result;
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
            if (selected.ngModel.$name == "intExams") {

                $scope.SearchArgs.Internal.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.Internal.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.Internal.SkillID = {};
                $scope.Skills = [];

                $scope.SearchArgs.Internal.ExamID = selected.selected;

                var classId = $scope.SearchArgs.Internal.ClassID?.Key;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var examID = $scope.SearchArgs.Internal.ExamID?.Key ?? null;
                var sectionId = $scope.SearchArgs.Internal.SectionID?.Key ?? null;
                var languageTypeId = $scope.SearchArgs.Internal.LanguageTypeID?.Key ?? null;
                $.ajax({
                    type: "GET",
                    data: { classID: classId, sectionId: sectionId, examID: examID, academicYearID: academicYearId, languageTypeID: languageTypeId },
                    url: "Schools/School/GetSkillSetByClassExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.SkillSets = result;
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
            if (selected.ngModel.$name == "intLanguageTypes") {

                $scope.SearchArgs.Internal.SkillSetID = {};
                $scope.SkillSets = [];
                $scope.SearchArgs.Internal.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.Internal.SkillID = {};
                $scope.Skills = [];

                $scope.SearchArgs.Internal.LanguageTypeID = selected.selected;

                var classId = $scope.SearchArgs.Internal.ClassID?.Key;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                var examID = $scope.SearchArgs.Internal.ExamID?.Key ?? null;
                var sectionId = $scope.SearchArgs.Internal.SectionID?.Key ?? null;
                var languageTypeId = $scope.SearchArgs.Internal.LanguageTypeID?.Key ?? null;
                $.ajax({
                    type: "GET",
                    data: { classID: classId, sectionId: sectionId, examID: examID, academicYearID: academicYearId, languageTypeID: languageTypeId },
                    url: "Schools/School/GetSkillSetByClassExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.SkillSets = result;

                            });
                        }
                    },
                    error: function () {

                    },
                    complete: function (result) {
                        hideOverlay();
                        return;
                    }
                });

            }          
            if (selected.ngModel.$name == "intSubjectMapTypes") {
                $scope.SearchArgs.Internal.SubjectMapTypeID = selected.selected;
                hideOverlay();
            }
            if (selected.ngModel.$name == "intSkillSets") {

                $scope.SearchArgs.Internal.SkillGroupID = {};
                $scope.SkillGroups = [];
                $scope.SearchArgs.Internal.SubjectID = {};
                $scope.Internal.Subjects = [];
                $scope.SearchArgs.Internal.SkillID = {};
                $scope.Skills = [];

                $scope.SearchArgs.Internal.SkillSetID = selected.selected;
                var classId = $scope.SearchArgs.Internal.ClassID?.Key;
                var examGroupID = $scope.SearchArgs.Internal.TermID?.Key;
                var skillSetID = $scope.SearchArgs.Internal.SkillSetID?.Key;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                $.ajax({
                    type: "GET",
                    data: { classID: classId, examGroupID: examGroupID, skillSetID: skillSetID, academicYearID: academicYearId },
                    url: "Schools/School/GetSkillGroupByClassExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.SkillGroups = result;
                            });
                        }
                    },
                    error: function () {

                    },
                    complete: function (result) {
                        $.ajax({
                            type: "GET",
                            data: { skillSetID: skillSetID },
                            url: "Schools/School/GetSubjectsBySkillset",
                            contentType: "application/json;charset=utf-8",
                            success: function (result) {
                                if (!result.IsError && result != null) {

                                    $scope.$apply(function () {
                                        $scope.Internal.Subjects = result;
                                    });
                                }
                            },
                            error: function () {

                            },
                            complete: function (result) {
                                hideOverlay();
                            }
                        });
                        hideOverlay();
                        return;
                    }
                });
            }
            if (selected.ngModel.$name == "intSkillGroups") {

                $scope.SearchArgs.Internal.SkillID = {};
                $scope.Skills = [];

                $scope.SearchArgs.Internal.SkillGroupID = selected.selected;
                var classId = $scope.SearchArgs.Internal.ClassID?.Key;
                var examGroupID = $scope.SearchArgs.Internal.TermID?.Key;
                var skillSetID = $scope.SearchArgs.Internal.SkillSetID?.Key;
                var skillGroupID = $scope.SearchArgs.Internal.SkillGroupID?.Key;
                var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
                $.ajax({
                    type: "GET",
                    data: { classID: classId, skillGroupID: skillGroupID, skillSetID: skillSetID, academicYearID: academicYearId },
                    url: "Schools/School/GetSkillsByClassExam",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (!result.IsError && result != null) {

                            $scope.$apply(function () {
                                $scope.Skills = result;
                            });
                        }
                    },
                    error: function () {

                    },
                    complete: function (result) {
                        hideOverlay();
                        return;
                    }
                });

            }
            if (selected.ngModel.$name == "intSubjects") {
                $scope.SearchArgs.Internal.SubjectID = selected.selected;
                hideOverlay();
                return;
            }
            if (selected.ngModel.$name == "intSkills") {
                $scope.SearchArgs.Internal.SkillID = selected.selected;
                hideOverlay();
                return;
            }
        }
    }

    $scope.isEmptyObject = function (obj) {
        return angular.equals({}, obj);
    };
    $scope.GetMarkEntry = function () {
        showOverlay();
        $scope.SearchArgs.IsFetched = true;
        var classId = $scope.SearchArgs.Mark.ClassID?.Key;
        var sectionId = $scope.SearchArgs.Mark.SectionID?.Key;
        var termID = $scope.SearchArgs.Mark.TermID?.Key;
        var subjectId = $scope.SearchArgs.Mark.SubjectID?.Key;
        var examID = $scope.SearchArgs.Mark.ExamID?.Key;
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        var languageTypeId = $scope.SearchArgs.Mark.LanguageTypeID?.Key ?? null;
        //var subjectMapId = $scope.SearchArgs.Mark.SubjectMapTypeID?.Key ?? null;
        if (classId == undefined || sectionId == undefined || termID == undefined || subjectId == undefined || examID == undefined || academicYearId == undefined) {
            hideOverlay();
            $().showGlobalMessage($root, $timeout, true, "Please fill out required fields!");
            return false;
        } else {
            $scope.SearchArgs.IsFetched = false;
        }
        $scope.MarkEntry = [];
        $scope.MaxMark = 0;
        $scope.MarkConvertionFactor = 0;
        $scope.MarkConvertionFactor = 0;
        $.ajax({
            url: "Schools/School/GetMarkEntry",

            type: "GET",
            data: { classID: classId, sectionId: sectionId, termID: termID, subjectId: subjectId, examId: examID, academicYearID: academicYearId, languageTypeID: languageTypeId, SubjectMapID: null },
            success: function (result) {
                $scope.$apply(function () {
                    $scope.MarkEntry = result.MarkEntryStudents;
                    if ($scope.MarkEntry.length > 0) {
                        $scope.MaxMark = $scope.MarkEntry[0].MaxMark;
                        $scope.MarkConvertionFactor = $scope.MarkEntry[0].MarkConvertionFactor;
                        FillGrade();
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
    $scope.ResetFields = function (tab) {
        $scope.SearchArgs.Mark.ClassID = null;
        $scope.SearchArgs.Mark.SectionID = null;
        $scope.SearchArgs.Mark.TermID = null;
        $scope.SearchArgs.Mark.SubjectID = null;
        $scope.SearchArgs.Mark.ExamID = null;
        $scope.SearchArgs.Mark.LanguageTypeID = null;
        $scope.MarkEntry = [];
       // $scope.onChangeTab(tab);
    }

    $scope.ResetCoScholasticFields = function (tab) {
        $scope.SearchArgs.ClassID = null;
        $scope.SearchArgs.SectionID = null;
        $scope.SearchArgs.TermID = null;
        $scope.SearchArgs.SubjectID = null;
        $scope.SearchArgs.ExamID = null;
        $scope.SearchArgs.LanguageTypeID = null;
        $scope.SearchArgs.SkillSetID = null;
        $scope.SearchArgs.SkillGroupID = null;
        $scope.SearchArgs.SkillID = null;
        $scope.SearchArgs.SubjectMapTypeID = null;
        $scope.CoScholasticEntry = [];
        $scope.CoScholasticSkills = [];
        //$scope.onChangeTab(tab);
    }
    $scope.ResetScholasticInternals = function (tab) {
        $scope.SearchArgs.Internal.ClassID = null;
        $scope.SearchArgs.Internal.SectionID = null;
        $scope.SearchArgs.Internal.TermID = null;
        $scope.SearchArgs.Internal.SubjectID = null;
        $scope.SearchArgs.Internal.ExamID = null;
        $scope.SearchArgs.Internal.LanguageTypeID = null;
        $scope.SearchArgs.Internal.SkillSetID = null;
        $scope.SearchArgs.Internal.SkillGroupID = null;
        $scope.SearchArgs.Internal.SkillID = null;
        $scope.SearchArgs.Internal.SubjectMapTypeID = null;
        $scope.ScholasticInternals = [];
        $scope.ScholasticInternalSkills = [];
       // $scope.onChangeTab(tab);
    }



    $scope.GetCoScholasticEntry = function () {
        showOverlay();
        $scope.SearchArgs.IsFetched = true;
        var classId = $scope.SearchArgs.ClassID?.Key;
        var sectionId = $scope.SearchArgs.SectionID?.Key;
        var examGroupID = $scope.SearchArgs.TermID?.Key;
        var skillSetID = $scope.SearchArgs.SkillSetID?.Key;
        var skillGroupID = $scope.SearchArgs.SkillGroupID?.Key;
        var skillID = $scope.SearchArgs.SkillID?.Key;
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        var examID = $scope.SearchArgs.ExamID?.Key;
        var languageTypeId = $scope.SearchArgs.LanguageTypeID?.Key ?? null;
        var subjectMapId = $scope.SearchArgs.SubjectMapTypeID?.Key ?? null;
        if (classId == undefined || sectionId == undefined || examGroupID == undefined || examID == undefined || skillSetID == undefined || skillGroupID == undefined || academicYearId == undefined) {
            hideOverlay();
            $().showGlobalMessage($root, $timeout, true, "Please fill out required fields!");
            return false;
        } else {
            $scope.SearchArgs.IsFetched = false;
        }
        $scope.CoScholasticEntry = [];
        $scope.CoScholasticSkills = [];
        $.ajax({
            type: "GET",
            data: { classID: classId, sectionId: sectionId, termID: examGroupID, examID: examID, skillGroupID: skillGroupID, skillSetID: skillSetID, academicYearID: academicYearId, skillID: skillID, languageTypeID: languageTypeId, SubjectMapID: subjectMapId },
            url: "Schools/School/GetCoScholasticEntry",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    $scope.CoScholasticEntry = result.Students;
                    if ($scope.CoScholasticEntry.length > 0) {
                        $scope.CoScholasticSkills = $scope.CoScholasticEntry[0].SkillGroups[0].Skills;

                    } else {

                        $().showGlobalMessage($root, $timeout, true, "No record(s) found!");
                    }
                });
            },
            error: function (result) {
                $().showGlobalMessage($root, $timeout, true, "Failed to fecth!");
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }

    $scope.SaveMarkEntry = function () {
        //showOverlay();
        var classId = $scope.SearchArgs.Mark.ClassID?.Key;
        var sectionId = $scope.SearchArgs.Mark.SectionID?.Key;
        var examGroupID = $scope.SearchArgs.Mark.TermID?.Key;
        var subjectId = $scope.SearchArgs.Mark.SubjectID?.Key;
        var examID = $scope.SearchArgs.Mark.ExamID?.Key;
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        $.ajax({
            type: "POST",
            data: {
                "ClassID": classId, "SectionId": sectionId, "ExamGroupID": examGroupID, "ExamID": examID, "SubjectId": subjectId, "AcademicYearID": academicYearId, "MarkEntryStudents": $scope.MarkEntry
            },
            url: utility.myHost + "Schools/School/SaveMarkEntry",
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
    }

    $scope.validateMark = function (student) {

        if (student.MarksObtained == null) {
            return null;
        }
        if (student.MarksObtained == '' || isNaN(parseFloat(student.MarksObtained)) || !isFinite(student.MarksObtained)) {
            //$().showGlobalMessage($root, $timeout, true, "Please enter a numeric value!");
            alert('Please enter a numeric value!');
            student.MarksObtained = null;
            student.GradeID = null;
            return null;
        } else if (parseFloat(student.MarksObtained) > parseFloat($scope.MaxMark)) {
            //$().showGlobalMessage($root, $timeout, true, "Mark must be less than Maximum Mark(" + $scope.MaxMark + ")!");           
            alert('Mark must be less than Maximum Mark');
            student.MarksObtained = null;
            student.GradeID = null;
            return null;
        }

        var selectedMark = student.MarksObtained;
        //if (selectedMark != undefined && selectedMark != null) {
        $.each($scope.SubjectGrades, function (index, item) {

            if (item.GradeFrom < selectedMark && item.GradeTo >= selectedMark) {
                student.GradeID = String(item.MarksGradeMapIID);
                return null;
            }
        });
        //}
        student.TotalMark = (student.MarksObtained / $scope.MaxMark) * $scope.MarkConvertionFactor;
    }

    $scope.validateScholasticInternalMark = function (skill) {

        if (skill.MarksObtained == null) {
            return null;
        }
        if (skill.MarksObtained == '' || isNaN(parseFloat(skill.MarksObtained)) || !isFinite(skill.MarksObtained)) {
           
            alert('Please enter a numeric value!');
            skill.MarksObtained = null;
            skill.GradeID = null;
            return null;
        } else if (parseFloat(skill.MarksObtained) > parseFloat(skill.MaxMark)) {
            //$().showGlobalMessage($root, $timeout, true, "Mark must be less than Maximum Mark(" + $scope.MaxMark + ")!");           
            alert('Mark must be less than Maximum Mark');
            skill.MarksObtained = null;
            skill.GradeID = null;
            return null;
        }

        var selectedMark = skill.MarksObtained;
        //if (selectedMark != undefined && selectedMark != null) {
        $.each(skill.GradeMarkRangeList, function (index, item) {

            if (item.GradeFrom < selectedMark && item.GradeTo >= selectedMark) {
                skill.GradeID = String(item.MarksGradeMapIID);
                return null;
            }
        });
        //}
      //  student.TotalMark = (student.MarksObtained / $scope.MaxMark) * $scope.MarkConvertionFactor;
    }

    function showOverlay() {
        $('.preload-overlay', $('#MarkEntry')).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $('#MarkEntry')).hide();
    }
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
    }

    $scope.onChangeTab = function (tab) {
        if (tab == 'MarksEntryPanel') {
            $scope.isMarkEntry = true;
            $scope.isCoScholasticEntry = false;
            $scope.isScholasticInternal = false;
            $("#MarksEntryPanel").show();    
            $("#ScholasticInternalPanel").hide(); 
            $("#CoScholasticEntryPanel").hide();         

            $scope.ResetFields();

        }
        else if (tab == 'CoScholasticEntryPanel') {
            $scope.isMarkEntry = false;
            $scope.isCoScholasticEntry = true;
            $scope.isScholasticInternal = false;
            $("#CoScholasticEntryPanel").show();         
            $("#ScholasticInternalPanel").hide();   
            $("#MarksEntryPanel").hide();   


            $scope.ResetCoScholasticFields();
        }

        else if (tab == 'ScholasticInternalPanel') {

            $scope.isMarkEntry = false;
            $scope.isCoScholasticEntry = false;
            $scope.isScholasticInternal = true;         
            $("#ScholasticInternalPanel").show();          
            $("#CoScholasticEntryPanel").hide();         
            $("#MarksEntryPanel").hide();   

            $scope.ResetScholasticInternals();
        }

    }

    $scope.updateMark = function (mark) {
        mark.isEdit = true;
    }

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

    }


    $scope.GetScholasticInternalEntry = function () {
        showOverlay();
        $scope.SearchArgs.Internal.IsFetched = true;
        var classId = $scope.SearchArgs.Internal.ClassID?.Key;
        var sectionId = $scope.SearchArgs.Internal.SectionID?.Key;
        var examGroupID = $scope.SearchArgs.Internal.TermID?.Key;
        var skillSetID = $scope.SearchArgs.Internal.SkillSetID?.Key;
        var skillGroupID = $scope.SearchArgs.Internal.SkillGroupID?.Key;
        var skillID = $scope.SearchArgs.Internal.SkillID?.Key;
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        var examID = $scope.SearchArgs.Internal.ExamID?.Key;
        var languageTypeId = $scope.SearchArgs.Internal.LanguageTypeID?.Key ?? null;
        var subjectMapId = $scope.SearchArgs.Internal.SubjectMapTypeID?.Key ?? null;
        var subjectID = $scope.SearchArgs.Internal.SubjectID?.Key;
        if (classId == undefined || sectionId == undefined || examGroupID == undefined || examID == undefined || skillSetID == undefined || skillGroupID == undefined || academicYearId == undefined || subjectID==undefined) {
            hideOverlay();
            $().showGlobalMessage($root, $timeout, true, "Please fill out required fields!");
            return false;
        } else {
            $scope.SearchArgs.Internal.IsFetched = false;
        }
        $scope.ScholasticInternalEntry = [];
        $scope.ScholasticInternalSkills = [];
        $.ajax({
            type: "GET",
            data: { classID: classId, sectionId: sectionId, termID: examGroupID, examID: examID, skillGroupID: skillGroupID, skillSetID: skillSetID, academicYearID: academicYearId, skillID: skillID, languageTypeID: languageTypeId, SubjectMapID: subjectMapId, subjectID: subjectID },
            url: "Schools/School/GetScholasticInternalEntry",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    $scope.ScholasticInternalEntry = result.Students;
                    if ($scope.ScholasticInternalEntry.length > 0) {
                        $scope.ScholasticInternalSkills = $scope.ScholasticInternalEntry[0].SkillGroups[0].Skills;

                    } else {

                        $().showGlobalMessage($root, $timeout, true, "No record(s) found!");
                    }
                });
            },
            error: function (result) {
                $().showGlobalMessage($root, $timeout, true, "Failed to fecth!");
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    }


    $scope.SaveScholasticInternalEntry = function () {
        showOverlay();
        var classId = $scope.SearchArgs.Internal.ClassID?.Key;
        var sectionId = $scope.SearchArgs.Internal.SectionID?.Key;
        var examGroupID = $scope.SearchArgs.Internal.TermID?.Key;
        var skillSetID = $scope.SearchArgs.Internal.SkillSetID?.Key;
        var skillGroupID = $scope.SearchArgs.Internal.SkillGroupID?.Key;
        var skillID = $scope.SearchArgs.Internal.SkillID?.Key;
        var academicYearId = $scope.SearchArgs.AcademicYear?.Key;
        var examID = $scope.SearchArgs.Internal.ExamID?.Key;
        var subjectID = $scope.SearchArgs.Internal.SubjectID?.Key;
        $.ajax({
            type: "POST",
            data: {
                "ClassID": classId, "ExamGroupID": examGroupID, "SectionId": sectionId, "SkillGroupID": skillGroupID, "SkillSetID": skillSetID, "ExamID": examID, "AcademicYearID": academicYearId, "SkillID": skillID, "subjectID" :subjectID, "Students": $scope.ScholasticInternalEntry
            },
            url: utility.myHost + "Schools/School/SaveScholasticInternalEntry",
            //contentType: "application/json;charset=utf-8",

            success: function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.Message);
                    return;
                } else {
                    $scope.$apply(function () {
                        $().showGlobalMessage($root, $timeout, false, "Saved successfully!");
                        $scope.ScholasticInternalEntry = [];
                        $scope.ScholasticInternalSkills = [];

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
    }
}]);

