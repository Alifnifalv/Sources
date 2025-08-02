app.controller("MarkPublishController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("MarkPublishController Loaded");

    $scope.SearchArgs = [];
    $scope.isMarkPublish = true;
    $scope.isCoScholasticEntry = false;
    $scope.MarkPublish = [];
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
    $scope.SearchArgs.ClassID = {};
    $scope.SearchArgs.SectionID = {};
    $scope.SearchArgs.TermID = {};
    $scope.SearchArgs.SkillSetID = {};
    $scope.SearchArgs.SkillGroupID = {};
    $scope.SearchArgs.SkillID = {};

    $scope.Mark = {};
    $scope.Mark.Class = [];
    $scope.Mark.Section = [];
    $scope.Mark.Terms = [];
    $scope.Mark.Subjects = [];
    $scope.Mark.Skills = [];
    $scope.Mark.Exams = [];
    $scope.Mark.Grades = [];
    $scope.SearchArgs.Mark = {};
    $scope.SearchArgs.Mark.ClassID = {};
    $scope.SearchArgs.Mark.SectionID = {};
    $scope.SearchArgs.Mark.TermID = {};
    $scope.SearchArgs.Mark.SubjectID = {};
    $scope.SearchArgs.Mark.ExamID = {};
    $scope.SearchArgs.Mark.AcademicYearID = {};
    $scope.MaxMark = 0;
    $scope.SearchArgs.IsFetched = false;
    $scope.SearchArgs.submitted = false;
    $scope.PublishStatus = false;

    $scope.SelectedMasterStatus = null;
    $scope.SelectedPromotionStatus = null;

    $scope.selectedFolderPath = null;


    //Initializing the product price
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
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=ActiveAcademicYear&defaultBlank=false",
        }).then(function (result) {
            $scope.AcademicYears = result.data;
            $scope.Mark.AcademicYears = result.data;
            //$scope.loadCurrentAcademicYear();

        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=MarkEntryStatus&defaultBlank=false",
        }).then(function (result) {
            $scope.Status = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Exams&defaultBlank=false",
        }).then(function (result) {
            $scope.Mark.Exams = result.data;
        });

        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "MARK_ENTRY_STATUS_DRAFT",
        }).then(function (result) {
            $scope.MarkEntryDraftStatusID = result.data;
        });

        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "MARK_ENTRY_STATUS_APPROVED",
        }).then(function (result) {
            $scope.MarkEntryApprovedStatusID = result.data;
        });

        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "MARK_ENTRY_STATUS_PUBLISHED",
        }).then(function (result) {
            $scope.MarkEntryPublishedStatusID = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=StudentPromotionStatuses&defaultBlank=false",
        }).then(function (result) {
            $scope.PromotionStatus = result.data;
        });

    };

    $scope.ReloadSearchCriteria = function () {
        showOverlay();

        $scope.SearchArgs.Mark.TermID = {};
        $scope.Mark.Terms = [];
        $scope.SearchArgs.Mark.SubjectID = {};
        $scope.Mark.Subjects = [];
        $scope.SearchArgs.Mark.ExamID = {};
        $scope.Mark.Exams = [];
        $scope.SearchArgs.SkillSetID = {};
        $scope.SkillSets = [];
        $scope.SearchArgs.SkillGroupID = {};
        $scope.SkillGroups = [];
        $scope.SearchArgs.SkillID = {};
        $scope.Skills = [];
        hideOverlay();

    }

    //$scope.loadCurrentAcademicYear = function () {
    //    showOverlay();

    //    $.ajax({
    //        type: "GET",

    //        url: "Schools/School/GetCurrentAcademicYear",
    //        contentType: "application/json;charset=utf-8",
    //        success: function (result) {
    //            if (!result.IsError && result != null) {

    //                $scope.$apply(function () {
    //                    $scope.SearchArgs.AcademicYear = result;
    //                });
    //            }
    //        },
    //        error: function () {

    //        },
    //        complete: function (result) {
    //            hideOverlay();
    //        }

    //    });
    //}

    $scope.loadMarkPublishSearchCriteria = function (selected) {

        var academicYearID = $scope.SearchArgs.Mark.AcademicYearID?.Key;
        var classID = $scope.SearchArgs.Mark.ClassID?.Key;
        var sectionID = $scope.SearchArgs.Mark.SectionID?.Key;
        var examGroupID = $scope.SearchArgs.Mark.TermID?.Key;

        if (!academicYearID) {
            return false;
        }
        else if (!classID) {
            return false;
        }
        else if (!sectionID) {
            return false;
        }
        else if (!examGroupID) {
            return false;
        }
        else {
            //if ($scope.SearchArgs.Mark.AcademicYearID.Key == null || $scope.SearchArgs.Mark.ClassID.Key == null || $scope.SearchArgs.Mark.TermID.Key == null) {
            //    hideOverlay();
            //    $().showGlobalMessage($root, $timeout, true, "Please select to academic year, class and term for filter Exam!");
            //    return false;
            //}  

            showOverlay();

            $.ajax({
                type: "GET",
                data: { classID: classID, sectionID: sectionID, examGroupID: examGroupID, academicYearID: academicYearID },
                url: "Schools/School/GetExamsByClassAndGroup",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $scope.$apply(function () {
                            $scope.Mark.Exams = result;
                        });
                    }
                },
                error: function () {
                    hideOverlay();
                    $().showGlobalMessage($root, $timeout, true, "Something went wrong try again later!");
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
    }

    $scope.isEmptyObject = function (obj) {
        return angular.equals({}, obj);
    };

    $scope.GetMarkEntryDetailsForPublish = function () {
        $scope.SearchArgs.IsFetched = true;

        var academicYearID = $scope.SearchArgs.Mark.AcademicYearID?.Key;
        var classID = $scope.SearchArgs.Mark.ClassID?.Key;
        var sectionID = $scope.SearchArgs.Mark.SectionID?.Key;
        var termID = $scope.SearchArgs.Mark.TermID?.Key;
        var examID = $scope.SearchArgs.Mark.ExamID?.Key;

        if (!classID || !sectionID || !termID || !examID || !academicYearID) {
            $().showGlobalMessage($root, $timeout, true, "Please fill out required fields!");
            return false;
        } else {
            $scope.SearchArgs.IsFetched = false;
        }

        $scope.MarkPublish = [];
        $scope.MaxMark = 0;

        showOverlay();

        $.ajax({
            type: "GET",
            data: { classID: classID, sectionID: sectionID, examID: examID, academicYearID: academicYearID },
            url: "Schools/School/GetSubjectsByClassID",
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
                //hideOverlay();
            }
        });

        showOverlay();
        $.ajax({
            type: "GET",
            data: { classID: classID, sectionID: sectionID, examID: examID, academicYearID: academicYearID, termID: termID },
            url: "Schools/School/GetSkillByExamAndClass",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result != null) {

                    $scope.$apply(function () {
                        $scope.Mark.Skills = result;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });

        //showOverlay();
        $.ajax({
            url: "Schools/School/GetSubjectsAndMarksToPublish",
            type: "GET",
            data: { classID: classID, sectionID: sectionID, termID: termID, examID: examID, academicYearID: academicYearID },
            success: function (result) {
                $scope.$apply(function () {
                    $scope.MarkPublish = result;
                    if ($scope.MarkPublish.length > 0) {
                        $scope.MaxMark = $scope.MarkPublish[0].MaxMark;
                        //FillGrade();
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

    $scope.getMarks = function (subject, markDetail) {

        var filterDetails = $scope.MarkPublish.find(x => x.StudentID == markDetail.Student.Key);
        if (filterDetails.StudentMarkList != null) {
            if (filterDetails.StudentMarkList.length > 0) {
                var subjectData = filterDetails.StudentMarkList.find(x => x.SubjectID == subject.Key);
                if (subjectData != null) {
                    return subjectData.TotalMark;
                }
                else {
                    return null;
                }
            }
            else {
                return null;
            }
        }
        else {
            return null;
        }
    }

    $scope.getSkillGrade = function (skill, markDetail) {

        var filterDetails = $scope.MarkPublish.find(x => x.StudentID == markDetail.Student.Key);
        if (filterDetails.StudentSkillList != null) {
            if (filterDetails.StudentSkillList.length > 0) {
                var skillDetails = filterDetails.StudentSkillList.find(x => x.SkillList.length > 0);
                var skillData = skillDetails.SkillList.find(x => x.SkillMasterID == skill.Key);

                if (skillData != null) {
                    return skillData.MarkGradeMap;
                }
                else {
                    return null;
                }
            }
            else {
                return null;
            }
        }
        else {
            return null;
        }
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
            data: { classID: classId, sectionId: sectionId, skillGroupID: skillGroupID, skillSetID: skillSetID, academicYearID: academicYearId, skillID: skillID },
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

    $scope.SaveMarksToPublish = function () {

        showOverlay();

        var classID = $scope.SearchArgs.Mark.ClassID?.Key;
        var sectionID = $scope.SearchArgs.Mark.SectionID?.Key;
        var examGroupID = $scope.SearchArgs.Mark.TermID?.Key;
        var examID = $scope.SearchArgs.Mark.ExamID?.Key;
        var academicYearID = $scope.SearchArgs.Mark.AcademicYearID?.Key;

        var classData = $scope.SearchArgs.Mark.ClassID;

        var markList = [];
        var studentSkill = [];

        var isErrorOccured = false;

        $scope.MarkPublish.forEach(x => {
            if (x.IsSelected == true) {

                if (x.MarkEntryStatusID == $scope.MarkEntryPublishedStatusID) {
                    if (x.PreviousMarkEntryStatusID != $scope.MarkEntryApprovedStatusID) {
                        $().showGlobalMessage($root, $timeout, true, "Can only publish previously approved entries.", 3000);
                        hideOverlay();
                        isErrorOccured = true;
                    }
                }

                x.StudentMarkList.forEach(y => {
                    markList.push({
                        "IsSelected": x.IsSelected,
                        "MarkRegisterID": y.MarkRegisterID,
                        "MarkRegisterSubjectMapID": y.MarkRegisterSubjectMapID,
                        "StudentID": x.StudentID,
                        "StudentName": x.Student.Value,
                        "MarkEntryStatusID": x.MarkEntryStatusID,
                    });
                });

                x.StudentSkillList.forEach(y => {
                    studentSkill.push({
                        "IsSelected": x.IsSelected,
                        "StudentID": x.StudentID,
                        "StudentName": x.Student.Value,
                        "MarkRegisterSkillGroupIID": y.MarkRegisterSkillGroupIID,
                        "SkillGroupMasterID": y.SkillGroupMasterID,
                        "SkillList": y.SkillList,
                        "MarkEntryStatusID": x.MarkEntryStatusID,
                    });
                });
            };
        });

        if (!isErrorOccured) {
            $.ajax({
                type: "POST",
                data: {
                    "AcademicYearID": academicYearID,
                    "ClassID": classID,
                    "Class": classData,
                    "SectionID": sectionID,
                    "ExamGroupID": examGroupID,
                    "ExamID": examID,
                    "StudentMarkList": markList,
                    "StudentSkillList": studentSkill,
                    "StudentClass": classData
                },
                url: utility.myHost + "Schools/School/UpdateMarkEntryStatus",
                success: function (result) {
                    if (result.IsError) {
                        $().showGlobalMessage($root, $timeout, true, result.Response, 3000);
                        hideOverlay();
                    }
                    else {

                        $().showGlobalMessage($root, $timeout, false, result.Response);
                        hideOverlay();

                        $scope.GetMarkEntryDetailsForPublish();
                    }
                },
                error: function (result) {
                    $().showGlobalMessage($root, $timeout, true, "Failed to save!");
                    hideOverlay();
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
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

        $.ajax({
            type: "POST",
            data: {
                "ClassID": classId, "ExamGroupID": examGroupID, "SectionId": sectionId, "SkillGroupID": skillGroupID, "SkillSetID": skillSetID, "AcademicYearID": academicYearId, "SkillID": skillID, "Students": $scope.CoScholasticEntry
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

            if (item.GradeFrom <= selectedMark && item.GradeTo >= selectedMark) {
                student.GradeID = String(item.MarksGradeMapIID);
                return null;
            }
        });
        //}

    }

    function showOverlay() {
        $('.preload-overlay', $('#MarkPublish')).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $('#MarkPublish')).hide();
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
        if (tab == 'MarkPublishPanel') {
            $scope.isMarkPublish = true;
            $scope.isCoScholasticEntry = false;

        }
        else if (tab == 'CoScholasticEntryPanel') {
            $scope.isMarkPublish = false;
            $scope.isCoScholasticEntry = true;
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
    };

    $scope.UpdateMasterStatus = function () {
        showOverlay();
        var checkBox = document.getElementById("IsSelectedToPublish");

        $scope.MarkPublish.forEach(x => {
            if ($scope.SelectedMasterStatus.Key == $scope.MarkEntryPublishedStatusID) {
                if (!x.StudentFeeDefaulterStatus) {
                    x.MarkEntryStatusID = $scope.SelectedMasterStatus.Key;
                    x.MarkEntryStatus = $scope.SelectedMasterStatus;
                }
                else {
                    var previousStatus = $scope.Status.find(s => s.Key == x.PreviousMarkEntryStatusID);

                    x.IsDisabled = true;

                    x.MarkEntryStatusID = x.PreviousMarkEntryStatusID;
                    x.MarkEntryStatus = previousStatus;
                    x.IsSelected = false;
                }
            }
            else {
                x.MarkEntryStatusID = $scope.SelectedMasterStatus.Key
                x.MarkEntryStatus = $scope.SelectedMasterStatus;

                if (x.PreviousMarkEntryStatusID != x.MarkEntryStatusID) {
                    if (checkBox.checked == true) {
                        x.IsSelected = true;
                    }
                    else {
                        x.IsSelected = false;
                    }
                }
                else {
                    x.IsSelected = false;
                }

                if (x.StudentFeeDefaulterStatus) {
                    x.IsDisabled = false;
                }
            }
        });

        if (($scope.MarkPublish.find(m => m.IsSelected == true) == null) || ($scope.MarkPublish.find(m => m.IsSelected == true) == undefined)) {
            checkBox.checked = false;
        }
        else {
            checkBox.checked = true;
        }

        hideOverlay();
    }

    $scope.ChangeHeaderCheckBox = function (checkedStatus) {
        $scope.MarkPublish.forEach(x => {

            if (x.MarkEntryStatusID == $scope.MarkEntryPublishedStatusID) {
                if (!x.StudentFeeDefaulterStatus) {
                    x.IsSelected = checkedStatus
                }
                else {
                    x.IsSelected = false;
                }
            }
            else {
                if (x.PreviousMarkEntryStatusID != x.MarkEntryStatusID) {
                    x.IsSelected = checkedStatus
                }
                else {
                    x.IsSelected = false;
                }
            }

        });
    };

    $scope.UpdateRowStatus = function (rowData) {

        if (rowData.MarkEntryStatus.Key == $scope.MarkEntryPublishedStatusID) {
            if (!rowData.StudentFeeDefaulterStatus) {
                rowData.MarkEntryStatusID = rowData.MarkEntryStatus.Key;
            }
            else {
                var approvedStatus = $scope.Status.find(s => s.Key == $scope.MarkEntryApprovedStatusID);

                rowData.MarkEntryStatusID = approvedStatus.Key;
                rowData.MarkEntryStatus = approvedStatus;
                $().showGlobalMessage($root, $timeout, true, "Unable to publish this entry because the fee is due for this student!", 3000);
            }
        }
        else {
            rowData.MarkEntryStatusID = rowData.MarkEntryStatus.Key;
        }
    };

    $scope.ResetFields = function () {
        $scope.SearchArgs.Mark.ClassID = null;
        $scope.SearchArgs.Mark.SectionID = null;
        $scope.SearchArgs.Mark.TermID = null;
        $scope.SearchArgs.Mark.SubjectID = null;
        $scope.SearchArgs.Mark.ExamID = null;
        $scope.SearchArgs.Mark.LanguageTypeID = null;
        $scope.SearchArgs.Mark.AcademicYearID = null;
        $scope.MarkPublish = [];
        // $scope.onChangeTab(tab);
    }

    $scope.ViewProgressReport = function (detail) {

        if (!detail.ReportContentID) {
            $().showGlobalMessage($root, $timeout, true, "No data available!");
            return false;
        }

        $('#globalRightDrawer').modal('show');
        $('#globalRightDrawer').find('.modal-body').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');
        $('#globalRightDrawer').find('.modal-title').html("Progress Report");
        $('#globalRightDrawer').find('.modal-body').html('<iframe id="myFrame" src="Content/ReadContentsByIDWithoutAttachment?contentID=' + detail.ReportContentID + '" style="height: 638px; width: 100%;" frameborder="0"></iframe>');
    };

    //$scope.DownloadReports = function () {
    //    showOverlay();
    //    var classId = $scope.SearchArgs.Mark.ClassID?.Key;
    //    var sectionId = $scope.SearchArgs.Mark.SectionID?.Key;
    //    var examGroupID = $scope.SearchArgs.Mark.TermID?.Key;
    //    var examID = $scope.SearchArgs.Mark.ExamID?.Key;
    //    var academicYearId = $scope.SearchArgs.Mark.AcademicYearID?.Key;
    //    var statusID = $scope.SelectedPromotionStatus.Key;

    //    $.ajax({
    //        type: "POST",
    //        data: {
    //            "classID": classId, "sectionID": sectionId, "examGroupID": examGroupID, "examID": examID, "academicYearID": academicYearId, "statusID": statusID, "customFolder": $scope.selectedFolder
    //        },
    //        url: utility.myHost + "Schools/School/DownloadReports",
    //        success: function (response) {
    //            if (response) {
    //                // Provide instructions to the user
    //                $http.get('/download', { params: { fileName: response }, responseType: 'blob' })
    //                        .then(function (response) {
    //                            var blob = new Blob([response.data], { type: 'application/octet-stream' });
    //                            var downloadLink = document.createElement('a');
    //                            downloadLink.href = window.URL.createObjectURL(blob);
    //                            downloadLink.download = fileName;
    //                            downloadLink.click();
    //                        })
    //                        .catch(function (error) {
    //                            console.error('Error downloading file:', error);
    //                        });

    //                $().showGlobalMessage($root, $timeout, false, "Download instructions provided.", 3000);
    //            } else {
    //                $().showGlobalMessage($root, $timeout, true, "No data received.", 3000);
    //            }
    //        },
    //        error: function (xhr, status, error) {
    //            // Handle errors
    //            console.error("Error:", error);
    //            $().showGlobalMessage($root, $timeout, true, "An error occurred while downloading reports.", 3000);
    //        },
    //        complete: function () {
    //            hideOverlay();
    //        }
    //    });
    //}

    $scope.DownloadReports = function () {
        showOverlay();
        var classId = $scope.SearchArgs.Mark.ClassID?.Key;
        var sectionId = $scope.SearchArgs.Mark.SectionID?.Key;
        var examGroupID = $scope.SearchArgs.Mark.TermID?.Key;
        var examID = $scope.SearchArgs.Mark.ExamID?.Key;
        var academicYearId = $scope.SearchArgs.Mark.AcademicYearID?.Key;
        var statusID = $scope.SelectedPromotionStatus.Key;

        var reportName = $scope.SearchArgs.Mark.ClassID?.Value + '-' + $scope.SearchArgs.Mark.SectionID?.Value + '-' + $scope.SearchArgs.Mark.AcademicYearID?.Value + '_' + $scope.SelectedPromotionStatus.Value.split(' ')[0];

        $.ajax({
            type: "GET",
            data: {
                "classID": classId, "sectionID": sectionId, "examGroupID": examGroupID, "examID": examID, "academicYearID": academicYearId, "statusID": statusID
            },
            url: utility.myHost + "Schools/School/DownloadReports",
            xhrFields: {
                responseType: 'blob' // Set the response type to blob
            },
            success: function (data, textStatus, xhr) {
                if (xhr.status === 200) {
                    var blob = new Blob([data], { type: 'application/zip' });
                    var url = window.URL.createObjectURL(blob);
                    var a = document.createElement('a');
                    document.body.appendChild(a);
                    a.style = 'display: none';
                    a.href = url;
                    a.download = reportName + '.zip'; // Set the appropriate file name here
                    a.click();
                    window.URL.revokeObjectURL(url);

                    $().showGlobalMessage($root, $timeout, false, "Download complete.");
                } else {
                    $().showGlobalMessage($root, $timeout, true, "No data received.");
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


    $scope.openFolder = function () {
        // Create a hidden file input element
        var input = document.createElement('input');
        input.type = 'file';
        input.webkitdirectory = true;

        // Trigger the click event on the hidden input
        input.click();

        // Listen for changes in the selected files
        input.addEventListener('change', function (event) {
            const files = event.target.files;
            if (files.length > 0) {
                const folderPath = files[0].webkitRelativePath;
                console.log('Selected folder:', folderPath);
                $scope.$apply(function () {
                    $scope.selectedFolderPath = folderPath.split("/")[0];
                });
            }
        });
    };

}]);