using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Students;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class MarkRegisterMapper : DTOEntityDynamicMapper
    {
        public static MarkRegisterMapper Mapper(CallContext context)
        {
            var mapper = new MarkRegisterMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<MarkRegisterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        # region Mark Register
        public override string GetEntity(long markRegID)
        {
            MarkRegisterDTO markRegister = new MarkRegisterDTO();
            //using (var dbContext = new dbEduegateSchoolContext())
            //{

            //    var entity = dbContext.MarkRegisters.Where(X => X.MarkRegisterIID == markRegID)
            //        .AsNoTracking()
            //        .FirstOrDefault();

            //    foreach (var markRegDet in grouped)
            //    {
            //        markRegister = new MarkRegisterDTO()
            //        {
            //            ExamID = entity.ExamID,
            //            CreatedBy = entity.CreatedBy,
            //            UpdatedBy = entity.UpdatedBy,
            //            CreatedDate = entity.CreatedDate,
            //            UpdatedDate = entity.UpdatedDate,
            //            MarkRegisterIID = entity.MarkRegisterIID,
            //            ClassID = entity.ClassID,
            //            SectionID = entity.SectionID,
            //            AcademicYearID = entity.AcademicYearID,
            //            SchoolID = entity.SchoolID,
            //            //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            //            Class = entity.Student.Class == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription },
            //            Section = entity.Student.Section == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.SectionID.ToString(), Value = entity.Section.SectionName },
            //            Exam = entity.Exam == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.Exam.ExamIID.ToString(), Value = entity.Exam.ExamDescription },
            //        };

            //        var markSplit = new List<MarkRegisterDetailsSplitDTO>();

            //        markSplit = (from examsub in dbContext.ExamSubjectMaps.AsEnumerable()

            //                     join exam in dbContext.Exams on examsub.ExamID equals exam.ExamIID
            //                     join mr in dbContext.MarkRegisters on exam.ExamIID equals mr.ExamID into tempMarkreg
            //                     from tempMarkregi in tempMarkreg.Where(mr => mr.StudentId == entity.StudentId).DefaultIfEmpty()
            //                     join marksubs in dbContext.MarkRegisterSubjectMaps on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterIID) equals marksubs.MarkRegisterID into tmpMarksub
            //                     from tmpMarksubi in tmpMarksub.Where(marksubs => (marksubs == null ? 0 : marksubs.SubjectID) == examsub.SubjectID).DefaultIfEmpty()
            //                     where (exam.ExamIID == entity.ExamID)
            //                     orderby examsub.Subject.SubjectName ascending
            //                     group examsub by new { examsub.SubjectID, examsub.MinimumMarks, examsub.MaximumMarks, examsub.Subject.SubjectName, examsub.MarkGradeID, examsub.MarkGrade, tmpMarksubi } into subjectGroup

            //                     select new MarkRegisterDetailsSplitDTO()
            //                     {
            //                         SubjectID = subjectGroup.Key.SubjectID,
            //                         MinimumMark = subjectGroup.Key.MinimumMarks,
            //                         MaximumMark = subjectGroup.Key.MaximumMarks,
            //                         Subject = subjectGroup.Key.SubjectName,
            //                         MarksGradeID = subjectGroup.Key.MarkGradeID,
            //                         Mark = subjectGroup.Key.tmpMarksubi == null ? 0 : subjectGroup.Key.tmpMarksubi.Mark,
            //                         Grade = (subjectGroup.Key.tmpMarksubi == null) ? "" : subjectGroup.Key.tmpMarksubi.MarkGradeMap.GradeName,
            //                         MarkGradeMap = (subjectGroup.Key.tmpMarksubi == null) ? "" : subjectGroup.Key.tmpMarksubi.MarkGradeMap.Description,
            //                         MarkRegisterSubjectMapIID = (subjectGroup.Key.tmpMarksubi == null) ? 0 : subjectGroup.Key.tmpMarksubi.MarkRegisterSubjectMapIID,
            //                         MarksGradeMapID = (subjectGroup.Key.tmpMarksubi == null) ? 0 : subjectGroup.Key.tmpMarksubi.MarksGradeMapID,
            //                         MarkRegisterID = (subjectGroup.Key.tmpMarksubi == null) ? 0 : subjectGroup.Key.tmpMarksubi.MarkRegisterID,
            //                         IsAbsent = subjectGroup.Key.tmpMarksubi == null ? false : subjectGroup.Key.tmpMarksubi.IsAbsent,
            //                         IsPassed = subjectGroup.Key.tmpMarksubi == null ? false : subjectGroup.Key.tmpMarksubi.IsPassed,
            //                         MarkRegisterSkillGroupDTO = (from classSub in dbContext.ClassSubjectSkillGroupMaps.Where(c => c.ExamID == entity.ExamID.Value && c.SubjectID == subjectGroup.Key.SubjectID)
            //                                                      join marSkillGroups in dbContext.MarkRegisterSkillGroups.Where(y => y.MarkRegisterSubjectMapID == subjectGroup.Key.tmpMarksubi.MarkRegisterSubjectMapIID) on (classSub.SkillGroupMasterID) equals
            //                                                       marSkillGroups.SkillGroupMasterID into tmpSkillGrp
            //                                                      from tmpSkillGrpi in tmpSkillGrp.DefaultIfEmpty()
            //                                                      where classSub.ClassID == entity.ClassID
            //                                                      orderby classSub.SkillGroupMaster.SkillGroup ascending

            //                                                      group classSub by new
            //                                                      {
            //                                                          classSub.ClassSubjectSkillGroupMapID,
            //                                                          classSub.SkillGroupMasterID,
            //                                                          classSub.SkillGroupMaster.SkillGroup,
            //                                                          classSub.SubjectID,
            //                                                          classSub.MinimumMarks,
            //                                                          classSub.MaximumMarks,
            //                                                          classSub.Subject.SubjectName,
            //                                                          classSub.MarkGradeID,
            //                                                          tmpSkillGrpi
            //                                                      }
            //                                                      into skillGrpGroup
            //                                                      select new MarkRegisterSkillGroupDTO()
            //                                                      {
            //                                                          SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                                          MinimumMark = skillGrpGroup.Key.MinimumMarks,
            //                                                          MaximumMark = skillGrpGroup.Key.MaximumMarks,
            //                                                          MarksGradeID = skillGrpGroup.Key.MarkGradeID,
            //                                                          SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                                          Grade = (skillGrpGroup.Key.tmpSkillGrpi == null) ? "" : skillGrpGroup.Key.tmpSkillGrpi.MarkGradeMap.GradeName,
            //                                                          MarkObtained = skillGrpGroup.Key.tmpSkillGrpi == null ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkObtained,
            //                                                          MarkGradeMap = (skillGrpGroup.Key.tmpSkillGrpi == null) ? "" : skillGrpGroup.Key.tmpSkillGrpi.MarkGradeMap.Description,
            //                                                          MarkRegisterSubjectMapID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSubjectMapID,
            //                                                          MarksGradeMapID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarksGradeMapID,
            //                                                          MarkRegisterSkillGroupIID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSkillGroupIID,
            //                                                          IsAbsent = skillGrpGroup.Key.tmpSkillGrpi == null ? false : skillGrpGroup.Key.tmpSkillGrpi.IsAbsent,
            //                                                          IsPassed = skillGrpGroup.Key.tmpSkillGrpi == null ? false : skillGrpGroup.Key.tmpSkillGrpi.IsPassed,

            //                                                          MarkRegisterSkillsDTO = (
            //                                                          from classSub in dbContext.ClassSubjectSkillGroupSkillMaps.Where(c => c.ClassSubjectSkillGroupMapID == skillGrpGroup.Key.ClassSubjectSkillGroupMapID)
            //                                                          join marSkills in dbContext.MarkRegisterSkills.Where(w => w.MarkRegisterSkillGroupID == skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSkillGroupIID) on classSub.SkillMasterID equals
            //                                                          marSkills.SkillMasterID
            //                                                          into tmpSkill
            //                                                          from tmpSkilli in tmpSkill.DefaultIfEmpty()
            //                                                          orderby classSub.SkillMaster.SkillName ascending
            //                                                          group classSub by new { classSub.SkillMasterID, classSub.SkillMaster.SkillName, classSub.MinimumMarks, classSub.MaximumMarks, classSub.MarkGradeID, tmpSkilli } into skillGroup
            //                                                          select new MarkRegisterSkillsDTO()
            //                                                          {
            //                                                              SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                                              MinimumMark = skillGroup.Key.MinimumMarks,
            //                                                              MaximumMark = skillGroup.Key.MaximumMarks,
            //                                                              MarksGradeID = skillGroup.Key.MarkGradeID,
            //                                                              SkillMasterID = skillGroup.Key.SkillMasterID,
            //                                                              SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                                              Skill = skillGroup.Key.SkillName,
            //                                                              Grade = (skillGroup.Key.tmpSkilli == null) ? "" : skillGroup.Key.tmpSkilli.MarkGradeMap.GradeName,
            //                                                              MarkGradeMap = (skillGroup.Key.tmpSkilli == null) ? "" : skillGroup.Key.tmpSkilli.MarkGradeMap.Description,
            //                                                              MarksObtained = skillGroup.Key.tmpSkilli == null ? 0 : skillGroup.Key.tmpSkilli.MarksObtained,
            //                                                              MarkRegisterSkillGroupID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarkRegisterSkillGroupID,
            //                                                              MarksGradeMapID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarksGradeMapID,
            //                                                              MarkRegisterSkillIID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarkRegisterSkillIID,
            //                                                              IsAbsent = skillGroup.Key.tmpSkilli == null ? false : skillGroup.Key.tmpSkilli.IsAbsent,
            //                                                              IsPassed = skillGroup.Key.tmpSkilli == null ? false : skillGroup.Key.tmpSkilli.IsPassed,


            //                                                          }).Distinct().ToList()
            //                                                      }).ToList()

            //                     }).ToList();
            //        markRegister.MarkRegistersDetails.Add(new MarkRegisterDetailsDTO()
            //        {
            //            MarkRegisterSplitDTO = markSplit,
            //            MarkRegisterID = entity.MarkRegisterIID,
            //            StudentID = entity.StudentId,
            //            Student = new KeyValueDTO() { Key = entity.Student.StudentIID.ToString(), Value = (string.IsNullOrEmpty(entity.Student.AdmissionNumber) ? " " : entity.Student.AdmissionNumber) + '-' + entity.Student.FirstName + ' ' + entity.Student.MiddleName + ' ' + entity.Student.LastName },

            //        });
            //        markRegister.GradeList = GetGradeByExamSubjects(markRegister.ExamID.Value, markRegister.ClassID.Value, 0, 1);
            //        markRegister.SkillGradeList = GetGradeByExamSubjects(markRegister.ExamID.Value, markRegister.ClassID.Value, 0, 3); ;
            //        markRegister.SkillGrpGradeList = GetGradeByExamSubjects(markRegister.ExamID.Value, markRegister.ClassID.Value, 0, 2); ;
            //    }
            //}

            return ToDTOString(markRegister);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as MarkRegisterDTO;
            //MarkRegisterDTO markRegister = new MarkRegisterDTO();
            if (toDto.Class == null || toDto.Section == null || toDto.Exam == null)
            {
                throw new Exception("Please fill required fields!");
            }
            else if (toDto.MarkRegistersDetails == null || toDto.MarkRegistersDetails.Count == 0 || !toDto.MarkRegistersDetails[0].StudentID.HasValue)
            {
                throw new Exception("Please fill student details!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                foreach (var studentMaps in toDto.MarkRegistersDetails)
                {
                    if (studentMaps.StudentID.HasValue)
                    {
                        //get existing child iids
                        var childRowIIDs = studentMaps.MarkRegisterSplitDTO
                        .Select(a => a.MarkRegisterSubjectMapIID).ToList();

                        if (childRowIIDs == null || childRowIIDs.Count == 0)
                        {
                            throw new Exception("Please fill subjects and marks!");
                        }


                        var subjectSplit = new List<MarkRegisterSubjectMap>();


                        foreach (var subjectSplitDto in studentMaps.MarkRegisterSplitDTO)
                        {
                            if (subjectSplitDto.SubjectID.HasValue && (subjectSplitDto.Mark != 0 || (subjectSplitDto.Mark == 0 && subjectSplitDto.IsAbsent == true)))
                            {
                                var skillGroupSplit = new List<MarkRegisterSkillGroup>();
                                foreach (var skillGroupDto in subjectSplitDto.MarkRegisterSkillGroupDTO)
                                {
                                    if (skillGroupDto.SkillGroupMasterID.HasValue && (skillGroupDto.MarkObtained != 0 || (skillGroupDto.MarkObtained == 0 && skillGroupDto.IsAbsent == true)))
                                    {
                                        var skills = new List<MarkRegisterSkill>();
                                        foreach (var skillDto in skillGroupDto.MarkRegisterSkillsDTO)
                                        {
                                            if (skillDto.SkillGroupMasterID == skillGroupDto.SkillGroupMasterID && skillDto.SkillMasterID != 0 && (skillDto.MarksObtained != 0 || (skillDto.MarksObtained == 0 && skillDto.IsAbsent == true)))
                                            {
                                                var entitySkill = new MarkRegisterSkill()
                                                {
                                                    IsAbsent = skillGroupDto.IsAbsent,
                                                    IsPassed = skillGroupDto.IsPassed,
                                                    MarksObtained = skillDto.MarksObtained,
                                                    MaximumMark = skillDto.MaximumMark,
                                                    MinimumMark = skillDto.MinimumMark,
                                                    SkillMasterID = skillDto.SkillMasterID,
                                                    SkillGroupMasterID = skillDto.SkillGroupMasterID,
                                                    MarksGradeMapID = skillDto.MarksGradeMapID,
                                                    MarkRegisterSkillGroupID = skillDto.MarkRegisterSkillGroupID.Value,
                                                    MarkRegisterSkillIID = skillDto.MarkRegisterSkillIID
                                                };
                                                skills.Add(entitySkill);
                                                if (entitySkill.MarkRegisterSkillIID != 0)
                                                {
                                                    dbContext.Entry(entitySkill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                                }
                                                //else
                                                //{
                                                //    dbContext.Entry(entitySkill).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                                //}
                                            }
                                        }

                                        var entitySkillGroup = new MarkRegisterSkillGroup()
                                        {
                                            IsAbsent = skillGroupDto.IsAbsent,
                                            IsPassed = skillGroupDto.IsPassed,
                                            MarkObtained = skillGroupDto.MarkObtained,
                                            MaximumMark = skillGroupDto.MaximumMark,
                                            MinimumMark = skillGroupDto.MinimumMark,
                                            SkillGroupMasterID = skillGroupDto.SkillGroupMasterID,
                                            MarksGradeMapID = skillGroupDto.MarksGradeMapID,
                                            MarkRegisterSkillGroupIID = skillGroupDto.MarkRegisterSkillGroupIID,
                                            MarkRegisterSubjectMapID = skillGroupDto.MarkRegisterSubjectMapID,
                                            MarkRegisterSkills = skills
                                        };
                                        skillGroupSplit.Add(entitySkillGroup);

                                        if (entitySkillGroup.MarkRegisterSkillGroupIID != 0)
                                        {
                                            dbContext.Entry(entitySkillGroup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                        }
                                        //else
                                        //{
                                        //    dbContext.Entry(entitySkillGroup).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        //}
                                    }
                                }

                                var entityChild = new MarkRegisterSubjectMap()
                                {
                                    IsPassed = subjectSplitDto.IsPassed,
                                    IsAbsent = subjectSplitDto.IsAbsent,
                                    SubjectID = subjectSplitDto.SubjectID,
                                    MarksGradeMapID = subjectSplitDto.MarksGradeMapID,
                                    MarkRegisterSubjectMapIID = subjectSplitDto.MarkRegisterSubjectMapIID,
                                    Mark = subjectSplitDto.Mark.HasValue ? subjectSplitDto.Mark : (decimal?)null,
                                    MarkRegisterID = subjectSplitDto.MarkRegisterID,
                                    MarkRegisterSkillGroups = skillGroupSplit
                                };

                                subjectSplit.Add(entityChild);
                                if (entityChild.MarkRegisterSubjectMapIID != 0)
                                {
                                    dbContext.Entry(entityChild).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                //else
                                //{
                                //    dbContext.Entry(entityChild).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                //}
                            }

                        }

                        var isStudentExist = dbContext.Students.AsQueryable().Where(x =>
                             x.StudentIID == studentMaps.StudentID.Value).AsNoTracking().FirstOrDefault();

                        var entity = new MarkRegister()
                        {
                            ExamID = toDto.ExamID,
                            MarkRegisterSubjectMaps = subjectSplit,
                            ClassID = isStudentExist.ClassID.Value,
                            StudentId = studentMaps.StudentID.Value,
                            SectionID = isStudentExist.SectionID != null ? isStudentExist.SectionID : null,
                            SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                            AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                            ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                            MarkRegisterIID = subjectSplit[0].MarkRegisterID == null ? 0 : (long)subjectSplit[0].MarkRegisterID,
                            CreatedBy = toDto.MarkRegisterIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = toDto.MarkRegisterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = toDto.MarkRegisterIID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = toDto.MarkRegisterIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        };

                        dbContext.MarkRegisters.Add(entity);
                        if (entity.MarkRegisterIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        dbContext.SaveChanges();

                        //Updating auto generated ID's into DTO object
                        studentMaps.MarkRegisterID = entity.MarkRegisterIID;

                        var MarkWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("MARK_ENTRY_WORKFLOW_ID", 1, null);
                        if (MarkWorkFlowID == null)
                            throw new Exception("Please set 'MARK_ENTRY_WORKFLOW_ID' in settings");
                        Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(long.Parse(MarkWorkFlowID), entity.MarkRegisterIID);

                        foreach (var markRegEntity in entity.MarkRegisterSubjectMaps)
                        {
                            foreach (var subjMap in studentMaps.MarkRegisterSplitDTO)
                            {
                                if (subjMap.SubjectID == markRegEntity.SubjectID)
                                {
                                    subjMap.MarkRegisterID = markRegEntity.MarkRegisterID;
                                    subjMap.MarkRegisterSubjectMapIID = markRegEntity.MarkRegisterSubjectMapIID;

                                    foreach (var skillGrpEntity in markRegEntity.MarkRegisterSkillGroups)
                                    {
                                        foreach (var sklGrpjMap in subjMap.MarkRegisterSkillGroupDTO)
                                        {
                                            if (sklGrpjMap.SkillGroupMasterID == skillGrpEntity.SkillGroupMasterID)
                                            {
                                                //                     
                                                sklGrpjMap.MarkRegisterSkillGroupIID = skillGrpEntity.MarkRegisterSkillGroupIID;
                                                sklGrpjMap.MarkRegisterSubjectMapID = markRegEntity.MarkRegisterSubjectMapIID;

                                                foreach (var skillEntity in skillGrpEntity.MarkRegisterSkills)
                                                {
                                                    foreach (var sklMap in sklGrpjMap.MarkRegisterSkillsDTO)
                                                    {
                                                        if (sklMap.SkillMasterID == skillEntity.SkillMasterID)
                                                        {
                                                            //                                
                                                            sklMap.MarkRegisterSkillIID = skillEntity.MarkRegisterSkillIID;
                                                            sklMap.MarkRegisterSkillGroupID = skillGrpEntity.MarkRegisterSkillGroupIID;
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }

                }

            }

            return ToDTOString(toDto);
        }
        public List<MarkGradeMapDTO> GetGradeByExamSubjects(long examId, int classId, long subjectID, int typeId)
        {
            List<MarkGradeMapDTO> mgMap = new List<MarkGradeMapDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (typeId == 1)
                {
                    mgMap = (from markGrade in dbContext.MarkGradeMaps
                             join examsub in dbContext.ExamSubjectMaps on markGrade.MarkGradeID equals examsub.MarkGradeID
                             join exam in dbContext.Exams on examsub.ExamID equals exam.ExamIID
                             where (exam.ExamIID == examId && (subjectID == 0 || examsub.SubjectID == subjectID))
                             select new MarkGradeMapDTO()
                             {
                                 GradeTo = markGrade.GradeTo,
                                 GradeFrom = markGrade.GradeFrom,
                                 GradeName = markGrade.GradeName,
                                 Description = markGrade.Description,
                                 MarksGradeID = markGrade.MarkGradeID,
                                 IsPercentage = markGrade.IsPercentage,
                                 MarksGradeMapIID = markGrade.MarksGradeMapIID
                             }).AsNoTracking().ToList();
                }
                else if (typeId == 2)
                {
                    mgMap = (from markGrade in dbContext.MarkGradeMaps
                             join skillGrp in dbContext.ClassSubjectSkillGroupMaps on markGrade.MarkGradeID equals skillGrp.MarkGradeID

                             where (skillGrp.ExamID == examId && (classId == 0 || skillGrp.ClassID == classId) && (subjectID == 0 || skillGrp.SubjectID == subjectID))

                             select new MarkGradeMapDTO()
                             {
                                 GradeTo = markGrade.GradeTo,
                                 GradeFrom = markGrade.GradeFrom,
                                 GradeName = markGrade.GradeName,
                                 Description = markGrade.Description,
                                 MarksGradeID = markGrade.MarkGradeID,
                                 IsPercentage = markGrade.IsPercentage,
                                 MarksGradeMapIID = markGrade.MarksGradeMapIID
                             }).AsNoTracking().ToList();
                }
                else if (typeId == 3)
                {

                    // examId = " + skillSetID + " & classId = " + skillGroupID + " & subjectID = " + skillID + 
                    mgMap = (from markGrade in dbContext.MarkGradeMaps
                             join skl in dbContext.ClassSubjectSkillGroupSkillMaps on markGrade.MarkGradeID equals skl.MarkGradeID
                             join skillGrp in dbContext.ClassSubjectSkillGroupMaps on skl.ClassSubjectSkillGroupMapID equals skillGrp.ClassSubjectSkillGroupMapID
                             where (skillGrp.ClassSubjectSkillGroupMapID == examId && skl.SkillGroupMasterID == classId && (subjectID == 0 || skl.SkillMasterID == subjectID))

                             select new MarkGradeMapDTO()
                             {
                                 GradeTo = markGrade.GradeTo,
                                 GradeFrom = markGrade.GradeFrom,
                                 GradeName = markGrade.GradeName,
                                 Description = markGrade.Description,
                                 MarksGradeID = markGrade.MarkGradeID,
                                 IsPercentage = markGrade.IsPercentage,
                                 MarksGradeMapIID = markGrade.MarksGradeMapIID
                             }).AsNoTracking().ToList();
                }
            }
            return mgMap;
        }

        public List<MarkListViewDTO> GetMarkListTeacherwise(long employeeID)
        {
            DateTime currentDate = System.DateTime.Now;
            DateTime preYearDate = DateTime.Now.AddYears(-1);
            //List<MarkListViewDTO> markRegisterDTO = new List<MarkListViewDTO>();
            List<MarkListViewDTO> markRegisterDTOList = new List<MarkListViewDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var markRegisterDTO = (from markReg in dbContext.MarkRegisters
                                       join classTeacher in dbContext.ClassTeacherMaps on markReg.ClassID equals classTeacher.ClassID
                                       where classTeacher.EmployeeID == employeeID
                                       orderby markReg.MarkRegisterIID descending
                                       select markReg
                                       ).AsNoTracking().ToList();

                markRegisterDTOList = markRegisterDTO.Select(markReg => new MarkListViewDTO()
                {
                    StudentID = markReg.StudentId,
                    MarkRegisterIID = markReg.MarkRegisterIID,
                    Exam = new KeyValueDTO() { Key = markReg.Exam.ExamIID.ToString(), Value = markReg.Exam.ExamDescription },
                    //Class = new KeyValueDTO() { Key = markReg.Class==null ?"": markReg.Class.ClassID.ToString(), Value = markReg.Class == null ? "" : markReg.Class.ClassDescription },
                    // Section = new KeyValueDTO() { Key = markReg.Section == null ? "" : markReg.Section.SectionID.ToString(), Value = markReg.Section == null ? "" : markReg.Section.SectionName },
                    Student = new KeyValueDTO() { Key = markReg.Student.StudentIID.ToString(), Value = (string.IsNullOrEmpty(markReg.Student.AdmissionNumber) ? " " : markReg.Student.AdmissionNumber) + '-' + markReg.Student.FirstName + ' ' + markReg.Student.MiddleName + ' ' + markReg.Student.LastName },
                    MarkSubjectDetails = (from split in markReg.MarkRegisterSubjectMaps
                                          join examsub in dbContext.ExamSubjectMaps on split.MarkRegister.ExamID equals examsub.ExamID
                                          where split.SubjectID == examsub.SubjectID && split.MarkRegisterID == markReg.MarkRegisterIID
                                          select new MarkListSubjectMapDTO()
                                          {
                                              IsPassed = split.IsPassed,
                                              IsAbsent = split.IsAbsent,
                                              SubjectID = split.SubjectID,
                                              MaximumMark = examsub.MaximumMarks,
                                              MinimumMark = examsub.MinimumMarks,
                                              Subject = split.Subject.SubjectName,
                                              MarkRegisterID = split.MarkRegisterID,
                                              MarksGradeMapID = split.MarksGradeMapID,
                                              Mark = split.Mark.HasValue ? split.Mark : (decimal?)null,
                                              MarkRegisterSubjectMapIID = split.MarkRegisterSubjectMapIID,
                                              Grade = split.MarksGradeMapID == null ? "" : split.MarkGradeMap.GradeName,

                                          }).ToList(),
                }).ToList();
            }

            return markRegisterDTOList;
        }

        public List<MarkListViewDTO> GetMarkListStudentwise(long studentId)
        {
            DateTime currentDate = System.DateTime.Now;
            DateTime preYearDate = DateTime.Now.AddYears(-1);
            //List<MarkListViewDTO> markRegisterDTO = new List<MarkListViewDTO>();
            List<MarkListViewDTO> markRegisterDTOList = new List<MarkListViewDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var markRegisterDTO = (from markReg in dbContext.MarkRegisters
                                       where markReg.StudentId == studentId
                                       orderby markReg.MarkRegisterIID descending
                                       select markReg
                                       ).AsNoTracking().ToList();

                markRegisterDTOList = markRegisterDTO.Select(markReg => new MarkListViewDTO()
                {
                    StudentID = markReg.StudentId,
                    MarkRegisterIID = markReg.MarkRegisterIID,
                    Exam = new KeyValueDTO() { Key = markReg.Exam.ExamIID.ToString(), Value = markReg.Exam.ExamDescription },
                    //Class = new KeyValueDTO() { Key = markReg.Class==null ?"": markReg.Class.ClassID.ToString(), Value = markReg.Class == null ? "" : markReg.Class.ClassDescription },
                    // Section = new KeyValueDTO() { Key = markReg.Section == null ? "" : markReg.Section.SectionID.ToString(), Value = markReg.Section == null ? "" : markReg.Section.SectionName },
                    Student = new KeyValueDTO() { Key = markReg.Student.StudentIID.ToString(), Value = (string.IsNullOrEmpty(markReg.Student.AdmissionNumber) ? " " : markReg.Student.AdmissionNumber) + '-' + markReg.Student.FirstName + ' ' + markReg.Student.MiddleName + ' ' + markReg.Student.LastName },
                    MarkSubjectDetails = (from split in markReg.MarkRegisterSubjectMaps
                                          join examsub in dbContext.ExamSubjectMaps on split.MarkRegister.ExamID equals examsub.ExamID
                                          where split.SubjectID == examsub.SubjectID && split.MarkRegisterID == markReg.MarkRegisterIID
                                          select new MarkListSubjectMapDTO()
                                          {
                                              IsPassed = split.IsPassed,
                                              IsAbsent = split.IsAbsent,
                                              SubjectID = split.SubjectID,
                                              MaximumMark = examsub.MaximumMarks,
                                              MinimumMark = examsub.MinimumMarks,
                                              Subject = split.Subject.SubjectName,
                                              MarkRegisterID = split.MarkRegisterID,
                                              MarksGradeMapID = split.MarksGradeMapID,
                                              Mark = split.Mark.HasValue ? split.Mark : (decimal?)null,
                                              MarkRegisterSubjectMapIID = split.MarkRegisterSubjectMapIID,
                                              Grade = split.MarksGradeMapID == null ? "" : split.MarkGradeMap.GradeName,

                                          }).ToList(),
                }).ToList();
                //var markRegisterDTO = (from markReg in dbContext.MarkRegisters.AsEnumerable()
                //                   join exam in dbContext.Exams on markReg.ExamID equals exam.ExamIID
                //                   where (markReg.StudentId == studentId)
                //                   orderby markReg.MarkRegisterIID descending
                //                   select new MarkListViewDTO()
                //                   {
                //                       StudentID = markReg.StudentId,
                //                       MarkRegisterIID = markReg.MarkRegisterIID,
                //                       Exam = new KeyValueDTO() { Key = markReg.Exam.ExamIID.ToString(), Value = markReg.Exam.ExamDescription },
                //                       //Class = new KeyValueDTO() { Key = markReg.Class==null ?"": markReg.Class.ClassID.ToString(), Value = markReg.Class == null ? "" : markReg.Class.ClassDescription },
                //                       // Section = new KeyValueDTO() { Key = markReg.Section == null ? "" : markReg.Section.SectionID.ToString(), Value = markReg.Section == null ? "" : markReg.Section.SectionName },
                //                       Student = new KeyValueDTO() { Key = markReg.Student.StudentIID.ToString(), Value = (string.IsNullOrEmpty(markReg.Student.AdmissionNumber) ? " " : markReg.Student.AdmissionNumber) + '-' + markReg.Student.FirstName + ' ' + markReg.Student.MiddleName + ' ' + markReg.Student.LastName },
                //                       MarkSubjectDetails = (from split in markReg.MarkRegisterSubjectMaps
                //                                             join examsub in dbContext.ExamSubjectMaps on split.MarkRegister.ExamID equals examsub.ExamID
                //                                             where split.SubjectID == examsub.SubjectID && split.MarkRegisterID == markReg.MarkRegisterIID
                //                                             select new MarkListSubjectMapDTO()
                //                                             {
                //                                                 IsPassed = split.IsPassed,
                //                                                 IsAbsent = split.IsAbsent,
                //                                                 SubjectID = split.SubjectID,
                //                                                 MaximumMark = examsub.MaximumMarks,
                //                                                 MinimumMark = examsub.MinimumMarks,
                //                                                 Subject = split.Subject.SubjectName,
                //                                                 MarkRegisterID = split.MarkRegisterID,
                //                                                 MarksGradeMapID = split.MarksGradeMapID,
                //                                                 Mark = split.Mark.HasValue ? split.Mark : (decimal?)null,
                //                                                 MarkRegisterSubjectMapIID = split.MarkRegisterSubjectMapIID,
                //                                                 Grade = split.MarksGradeMapID == null ? "" : split.MarkGradeMap.GradeName,

                //                                             }).ToList(),

                //                   }).ToList();//var markRegisterDTO = (from markReg in dbContext.MarkRegisters.AsEnumerable()
                //                   join exam in dbContext.Exams on markReg.ExamID equals exam.ExamIID
                //                   where (markReg.StudentId == studentId)
                //                   orderby markReg.MarkRegisterIID descending
                //                   select new MarkListViewDTO()
                //                   {
                //                       StudentID = markReg.StudentId,
                //                       MarkRegisterIID = markReg.MarkRegisterIID,
                //                       Exam = new KeyValueDTO() { Key = markReg.Exam.ExamIID.ToString(), Value = markReg.Exam.ExamDescription },
                //                       //Class = new KeyValueDTO() { Key = markReg.Class==null ?"": markReg.Class.ClassID.ToString(), Value = markReg.Class == null ? "" : markReg.Class.ClassDescription },
                //                       // Section = new KeyValueDTO() { Key = markReg.Section == null ? "" : markReg.Section.SectionID.ToString(), Value = markReg.Section == null ? "" : markReg.Section.SectionName },
                //                       Student = new KeyValueDTO() { Key = markReg.Student.StudentIID.ToString(), Value = (string.IsNullOrEmpty(markReg.Student.AdmissionNumber) ? " " : markReg.Student.AdmissionNumber) + '-' + markReg.Student.FirstName + ' ' + markReg.Student.MiddleName + ' ' + markReg.Student.LastName },
                //                       MarkSubjectDetails = (from split in markReg.MarkRegisterSubjectMaps
                //                                             join examsub in dbContext.ExamSubjectMaps on split.MarkRegister.ExamID equals examsub.ExamID
                //                                             where split.SubjectID == examsub.SubjectID && split.MarkRegisterID == markReg.MarkRegisterIID
                //                                             select new MarkListSubjectMapDTO()
                //                                             {
                //                                                 IsPassed = split.IsPassed,
                //                                                 IsAbsent = split.IsAbsent,
                //                                                 SubjectID = split.SubjectID,
                //                                                 MaximumMark = examsub.MaximumMarks,
                //                                                 MinimumMark = examsub.MinimumMarks,
                //                                                 Subject = split.Subject.SubjectName,
                //                                                 MarkRegisterID = split.MarkRegisterID,
                //                                                 MarksGradeMapID = split.MarksGradeMapID,
                //                                                 Mark = split.Mark.HasValue ? split.Mark : (decimal?)null,
                //                                                 MarkRegisterSubjectMapIID = split.MarkRegisterSubjectMapIID,
                //                                                 Grade = split.MarksGradeMapID == null ? "" : split.MarkGradeMap.GradeName,

                //                                             }).ToList(),

                //                   }).ToList();
            }
            return markRegisterDTOList;
        }


        public List<MarkRegisterDetailsSplitDTO> GetExamSubjectsMarks(long studentId, long examId, int ClassId)
        {

            var subjectList = new List<MarkRegisterDetailsSplitDTO>();
            var subjecMarktList = new List<MarkRegisterDetailsSplitDTO>();

            //using (var dbContext = new dbEduegateSchoolContext())
            //{
            //    subjectList = (from examsub in dbContext.ExamSubjectMaps.AsEnumerable()

            //                   join exam in dbContext.Exams on examsub.ExamID equals exam.ExamIID
            //                   join mr in dbContext.MarkRegisters on exam.ExamIID equals mr.ExamID into tempMarkreg
            //                   from tempMarkregi in tempMarkreg.Where(mr => mr.StudentId == studentId).DefaultIfEmpty()
            //                   join marksubs in dbContext.MarkRegisterSubjectMaps on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterIID) equals marksubs.MarkRegisterID into tmpMarksub
            //                   from tmpMarksubi in tmpMarksub.Where(marksubs => (marksubs == null ? 0 : marksubs.SubjectID) == examsub.SubjectID).DefaultIfEmpty()
            //                   where (exam.ExamIID == examId)
            //                   orderby examsub.Subject.SubjectName ascending
            //                   group examsub by new { examsub.SubjectID, examsub.MinimumMarks, examsub.MaximumMarks, examsub.Subject.SubjectName, examsub.MarkGradeID, tmpMarksubi } into subjectGroup

            //                   select new MarkRegisterDetailsSplitDTO()
            //                   {
            //                       SubjectID = subjectGroup.Key.SubjectID,
            //                       MinimumMark = subjectGroup.Key.MinimumMarks,
            //                       MaximumMark = subjectGroup.Key.MaximumMarks,
            //                       Subject = subjectGroup.Key.SubjectName,
            //                       MarksGradeID = subjectGroup.Key.MarkGradeID,
            //                       Mark = subjectGroup.Key.tmpMarksubi == null ? 0 : subjectGroup.Key.tmpMarksubi.Mark,
            //                       MarkRegisterSubjectMapIID = (subjectGroup.Key.tmpMarksubi == null) ? 0 : subjectGroup.Key.tmpMarksubi.MarkRegisterSubjectMapIID,
            //                       MarksGradeMapID = (subjectGroup.Key.tmpMarksubi == null) ? 0 : subjectGroup.Key.tmpMarksubi.MarksGradeMapID,
            //                       MarkRegisterID = (subjectGroup.Key.tmpMarksubi == null) ? 0 : subjectGroup.Key.tmpMarksubi.MarkRegisterID,
            //                       IsAbsent = subjectGroup.Key.tmpMarksubi == null ? false : subjectGroup.Key.tmpMarksubi.IsAbsent,
            //                       IsPassed = subjectGroup.Key.tmpMarksubi == null ? false : subjectGroup.Key.tmpMarksubi.IsPassed,
            //                       MarkRegisterSkillGroupDTO = (from classSub in dbContext.ClassSubjectSkillGroupMaps
            //                                                    join mr in dbContext.MarkRegisterSubjectMaps on classSub.ExamID equals (mr.MarkRegister == null ? 0 : mr.MarkRegister.ExamID) into tempMarkreg
            //                                                    from tempMarkregi in tempMarkreg.Where(mr => (mr.MarkRegister == null ? 0 : mr.MarkRegister.StudentId) == studentId).DefaultIfEmpty()
            //                                                    join marSkillGroups in dbContext.MarkRegisterSkillGroups on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterSubjectMapIID) equals
            //                                                     marSkillGroups.MarkRegisterSubjectMapID into tmpSkillGrp
            //                                                    from tmpSkillGrpi in tmpSkillGrp.DefaultIfEmpty()
            //                                                    where classSub.ClassID == ClassId && classSub.ExamID == examId && classSub.SubjectID == subjectGroup.Key.SubjectID
            //                                                    orderby classSub.SkillGroupMaster.SkillGroup ascending
            //                                                    group classSub by new
            //                                                    {
            //                                                        classSub.ClassSubjectSkillGroupMapID,
            //                                                        classSub.SkillGroupMasterID,
            //                                                        classSub.SkillGroupMaster.SkillGroup,
            //                                                        classSub.SubjectID,
            //                                                        classSub.MinimumMarks,
            //                                                        classSub.MaximumMarks,
            //                                                        classSub.Subject.SubjectName,
            //                                                        classSub.MarkGradeID,
            //                                                        tmpSkillGrpi
            //                                                    }
            //                                                    into skillGrpGroup
            //                                                    select new MarkRegisterSkillGroupDTO()
            //                                                    {
            //                                                        SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                                        MinimumMark = skillGrpGroup.Key.MinimumMarks,
            //                                                        MaximumMark = skillGrpGroup.Key.MaximumMarks,
            //                                                        MarksGradeID = skillGrpGroup.Key.MarkGradeID,
            //                                                        SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                                        MarkGradeMap = (skillGrpGroup.Key.tmpSkillGrpi == null) ? "" : skillGrpGroup.Key.tmpSkillGrpi.MarkGradeMap.Description,
            //                                                        MarkObtained = skillGrpGroup.Key.tmpSkillGrpi == null ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkObtained,
            //                                                        MarkRegisterSubjectMapID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSubjectMapID,
            //                                                        MarksGradeMapID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarksGradeMapID,
            //                                                        MarkRegisterSkillGroupIID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSkillGroupIID,
            //                                                        IsAbsent = skillGrpGroup.Key.tmpSkillGrpi == null ? false : skillGrpGroup.Key.tmpSkillGrpi.IsAbsent,
            //                                                        IsPassed = skillGrpGroup.Key.tmpSkillGrpi == null ? false : skillGrpGroup.Key.tmpSkillGrpi.IsPassed,
            //                                                        MarkRegisterSkillsDTO = (
            //                                                        from classSub in dbContext.ClassSubjectSkillGroupSkillMaps
            //                                                        join mr in dbContext.MarkRegisterSkillGroups on classSub.ClassSubjectSkillGroupMap.ExamID equals (mr.MarkRegisterSubjectMap == null ? 0 : mr.MarkRegisterSubjectMap.MarkRegister.ExamID) into tempMarkreg
            //                                                        from tempMarkregi in tempMarkreg.Where(mr => (mr.MarkRegisterSubjectMap == null ? 0 : mr.MarkRegisterSubjectMap.MarkRegister.StudentId) == studentId).DefaultIfEmpty()
            //                                                        join marSkills in dbContext.MarkRegisterSkills on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterSkillGroupIID) equals
            //                                                         marSkills.MarkRegisterSkillGroupID into tmpSkills
            //                                                        from tmpSkilli in tmpSkills.Where(marSkills => (marSkills == null ? 0 : marSkills.SkillMasterID) == classSub.SkillMasterID).DefaultIfEmpty()
            //                                                        where classSub.ClassSubjectSkillGroupMap.ClassID == ClassId && classSub.ClassSubjectSkillGroupMap.ExamID == examId && classSub.ClassSubjectSkillGroupMap.SubjectID == skillGrpGroup.Key.SubjectID &&
            //                                                        classSub.ClassSubjectSkillGroupMap.ClassSubjectSkillGroupMapID == skillGrpGroup.Key.ClassSubjectSkillGroupMapID
            //                                                        orderby classSub.SkillMaster.SkillName ascending
            //                                                        group classSub by new { classSub.SkillMasterID, classSub.SkillMaster.SkillName, classSub.MinimumMarks, classSub.MaximumMarks, classSub.MarkGradeID, tmpSkilli } into skillGroup
            //                                                        select new MarkRegisterSkillsDTO()
            //                                                        {
            //                                                            SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                                            MinimumMark = skillGroup.Key.MinimumMarks,
            //                                                            MaximumMark = skillGroup.Key.MaximumMarks,
            //                                                            MarksGradeID = skillGroup.Key.MarkGradeID,
            //                                                            SkillMasterID = skillGroup.Key.SkillMasterID,
            //                                                            SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                                            Skill = skillGroup.Key.SkillName,
            //                                                            MarkGradeMap = (skillGroup.Key.tmpSkilli == null) ? "" : skillGroup.Key.tmpSkilli.MarkGradeMap.Description,
            //                                                            MarksObtained = skillGroup.Key.tmpSkilli == null ? 0 : skillGroup.Key.tmpSkilli.MarksObtained,
            //                                                            MarkRegisterSkillGroupID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarkRegisterSkillGroupID,
            //                                                            MarksGradeMapID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarksGradeMapID,
            //                                                            MarkRegisterSkillIID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarkRegisterSkillIID,
            //                                                            IsAbsent = skillGroup.Key.tmpSkilli == null ? false : skillGroup.Key.tmpSkilli.IsAbsent,
            //                                                            IsPassed = skillGroup.Key.tmpSkilli == null ? false : skillGroup.Key.tmpSkilli.IsPassed,


            //                                                        }).ToList()
            //                                                    }).ToList()

            //                   }).ToList();


            //}

            return subjectList;
        }


        public ProgressReportDTO GetStudentSkillRegister(long studentId, int ClassID)
        {
            var progressReportData = new ProgressReportDTO();

            var subjectList = new List<ProgressReportSubjectListDTO>();
            var examList = new List<ProgressReportExamListDTO>();

            //using (var dbContext = new dbEduegateSchoolContext())
            //{
            //    examList = (from examCls in dbContext.ExamClassMaps//.AsEnumerable()
            //                join exams in dbContext.Exams on examCls.ExamID equals exams.ExamIID
            //                where examCls.ClassID == ClassID
            //                group exams by new
            //                {
            //                    exams.ExamIID,
            //                    exams.ExamDescription
            //                } into classExamgrp
            //                select new ProgressReportExamListDTO()
            //                {
            //                    ExamID = classExamgrp.Key.ExamIID,
            //                    ExamName = classExamgrp.Key.ExamDescription

            //                }).ToList();

            //    subjectList = (from classSub in dbContext.MarkRegisterSubjectMaps
            //                   where (classSub.MarkRegister.ClassID == ClassID)
            //                   group classSub by new
            //                   {
            //                       classSub.SubjectID,
            //                       classSub.Subject.SubjectName
            //                   } into classsubjgrp
            //                   select new ProgressReportSubjectListDTO()
            //                   {
            //                       SubjectID = classsubjgrp.Key.SubjectID,
            //                       SubjectName = classsubjgrp.Key.SubjectName,

            //                       Exams = (from studSkillReg in dbContext.MarkRegisters
            //                                join examsub2 in dbContext.MarkRegisterSubjectMaps
            //                                //new { exam_id = studSkillReg.ExamID, Subject_ID = studSkillReg.SubjectID } equals
            //                                //new { exam_id = examsub2.ExamID, Subject_ID = examsub2.SubjectID }
            //                                on studSkillReg.MarkRegisterIID equals examsub2.MarkRegisterID
            //                                join examsub in dbContext.ExamSubjectMaps on
            //                                new { exam_id = studSkillReg.ExamID, Subject_ID = examsub2.SubjectID } equals
            //                                new { exam_id = examsub.ExamID, Subject_ID = examsub.SubjectID }
            //                                where studSkillReg.ClassID == ClassID && examsub2.SubjectID == classsubjgrp.Key.SubjectID && studSkillReg.StudentId == studentId

            //                                group examsub2 by new { studSkillReg.MarkRegisterIID, studSkillReg.Exam, examsub.MinimumMarks, examsub.MaximumMarks, examsub2.Mark, examsub2.IsPassed, examsub2.IsAbsent, examsub2.MarkGradeMap } into subExamGroup
            //                                select new ProgressReportExamListDTO()
            //                                {
            //                                    ExamID = subExamGroup.Key.Exam.ExamIID,
            //                                    MaximumMarks = subExamGroup.Key.MaximumMarks,
            //                                    MinimumMarks = subExamGroup.Key.MinimumMarks,
            //                                    Mark = subExamGroup.Key.Mark,
            //                                    Grade = subExamGroup.Key.MarkGradeMap.GradeName,
            //                                    IsAbsent = subExamGroup.Key.IsAbsent,
            //                                    IsPassed = subExamGroup.Key.IsPassed

            //                                }).ToList(),
            //                       SkillGroups = (from skillGroupMap in dbContext.ClassSubjectSkillGroupMaps

            //                                      where skillGroupMap.SubjectID == classsubjgrp.Key.SubjectID &&
            //                                      skillGroupMap.ClassID == ClassID
            //                                      group skillGroupMap by new
            //                                      {
            //                                          skillGroupMap.SkillGroupMaster.SkillGroup,
            //                                          skillGroupMap.SkillGroupMasterID
            //                                      } into skillgrpGroup

            //                                      select new ProgressReportSkillGroupListDTO()
            //                                      {
            //                                          SkillGroupID = skillgrpGroup.Key.SkillGroupMasterID,
            //                                          SkillGroupName = skillgrpGroup.Key.SkillGroup,
            //                                          Exams = (from skillGroupMap in dbContext.MarkRegisterSkillGroups
            //                                                   join studSkillReg in dbContext.MarkRegisters on
            //                                                   skillGroupMap.MarkRegisterSubjectMap.MarkRegisterID equals studSkillReg.MarkRegisterIID
            //                                                   where skillGroupMap.SkillGroupMasterID == skillgrpGroup.Key.SkillGroupMasterID &&
            //                                                   studSkillReg.ClassID == ClassID && skillGroupMap.MarkRegisterSubjectMap.SubjectID == classsubjgrp.Key.SubjectID && studSkillReg.StudentId == studentId
            //                                                   group skillGroupMap by new
            //                                                   {

            //                                                       studSkillReg.ExamID,
            //                                                       skillGroupMap.MarkGradeMap,
            //                                                       skillGroupMap.MinimumMark,
            //                                                       skillGroupMap.MaximumMark,
            //                                                       skillGroupMap.MarkObtained,
            //                                                       skillGroupMap.IsPassed,
            //                                                       skillGroupMap.IsAbsent
            //                                                   } into skillGroupExamGroup
            //                                                   select new ProgressReportExamListDTO()
            //                                                   {
            //                                                       ExamID = skillGroupExamGroup.Key.ExamID,
            //                                                       MaximumMarks = skillGroupExamGroup.Key.MaximumMark,
            //                                                       MinimumMarks = skillGroupExamGroup.Key.MinimumMark,
            //                                                       Mark = skillGroupExamGroup.Key.MarkObtained,
            //                                                       Grade = skillGroupExamGroup.Key.MarkGradeMap.GradeName,
            //                                                       IsAbsent = skillGroupExamGroup.Key.IsAbsent,
            //                                                       IsPassed = skillGroupExamGroup.Key.IsPassed

            //                                                   }).ToList(),
            //                                          Skills =
            //                                          (from skill in dbContext.ClassSubjectSkillGroupSkillMaps
            //                                           join skillGroupMap in dbContext.ClassSubjectSkillGroupMaps
            //                                            on
            //                                                   skill.ClassSubjectSkillGroupMapID equals skillGroupMap.ClassSubjectSkillGroupMapID
            //                                           where skillGroupMap.SubjectID == classsubjgrp.Key.SubjectID &&
            //                                           skillGroupMap.ClassID == ClassID

            //                                           group skill by new
            //                                           {
            //                                               skill.SkillMaster.SkillMasterID,
            //                                               skill.SkillMaster.SkillName,
            //                                           } into skillGroup
            //                                           select new ProgressReportSkillsListDTO()
            //                                           {
            //                                               SkillID = skillGroup.Key.SkillMasterID,
            //                                               SkillName = skillGroup.Key.SkillName,
            //                                               Exams = (from skillMap in dbContext.MarkRegisterSkills
            //                                                        join skillGroupMap in dbContext.MarkRegisterSkillGroups on
            //                                                               skillMap.MarkRegisterSkillGroupID equals skillGroupMap.MarkRegisterSkillGroupIID
            //                                                        join studSkillReg in dbContext.MarkRegisters on
            //                                                               skillGroupMap.MarkRegisterSubjectMap.MarkRegisterID equals studSkillReg.MarkRegisterIID
            //                                                        where skillGroupMap.SkillGroupMasterID == skillgrpGroup.Key.SkillGroupMasterID && skillMap.SkillMasterID == skillGroup.Key.SkillMasterID &&
            //                                                        studSkillReg.ClassID == ClassID && skillGroupMap.MarkRegisterSubjectMap.SubjectID == classsubjgrp.Key.SubjectID && studSkillReg.StudentId == studentId

            //                                                        group skillMap by new
            //                                                        {
            //                                                            studSkillReg.ExamID,
            //                                                            skillMap.MarkGradeMap,
            //                                                            skillMap.MinimumMark,
            //                                                            skillMap.MaximumMark,
            //                                                            skillMap.MarksObtained,
            //                                                            skillMap.IsAbsent,
            //                                                            skillMap.IsPassed
            //                                                        } into skillExamGrp
            //                                                        select new ProgressReportExamListDTO()
            //                                                        {
            //                                                            ExamID = skillExamGrp.Key.ExamID,
            //                                                            MaximumMarks = skillExamGrp.Key.MaximumMark,
            //                                                            MinimumMarks = skillExamGrp.Key.MinimumMark,
            //                                                            Mark = skillExamGrp.Key.MarksObtained,
            //                                                            Grade = skillExamGrp.Key.MarkGradeMap.GradeName,
            //                                                            IsAbsent = skillExamGrp.Key.IsAbsent,
            //                                                            IsPassed = skillExamGrp.Key.IsPassed


            //                                                        }).ToList(),
            //                                           }).ToList(),
            //                                      }).ToList(),

            //                   }).ToList();


            //}

            progressReportData.Exams = examList;
            progressReportData.Subjects = subjectList;

            return progressReportData;
        }

        #endregion

        #region CoScholasticEntry

        public string SaveCoScholasticEntry(BaseMasterDTO dto)
        {
            #region Validate DTOs

            var toDto = dto as MarkRegisterDTO;
            //MarkRegisterDTO markRegister = new MarkRegisterDTO();
            if (toDto.Class == null || toDto.Section == null || toDto.Exam == null)
            {
                throw new Exception("Please fill required fields!");
            }
            else if (toDto.MarkRegistersDetails == null || toDto.MarkRegistersDetails.Count == 0 || !toDto.MarkRegistersDetails[0].StudentID.HasValue)
            {
                throw new Exception("Please fill student details!");
            }
            #endregion Validate DTOs


            using (var dbContext = new dbEduegateSchoolContext())
            {
                //Map DTO to Entities
                #region Map Entities
                var entities = new List<MarkRegister>();
                var _sLstMarkRegister_IDs = new List<long>();
                var _sLstMarkRegisterSkilGrp_IDs = new List<long>();
                var _sLstMarkRegisterSkill_IDs = new List<long>();

                entities = toDto.MarkRegistersDetails.Select(s => new MarkRegister
                {
                    ExamID = toDto.ExamID,
                    MarkRegisterSkillGroups = GetMarkRegisterSkillGroupMaps(toDto.SkillSetID.Value, toDto.SkillGroupID.Value, s.MarkRegisterSkillGroupDTO, null),
                    ClassID = toDto.ClassID.Value,
                    StudentId = s.StudentID,
                    SectionID = toDto.SectionID ?? null,
                    SchoolID = toDto.SchoolID ?? (byte)_context.SchoolID,
                    AcademicYearID = toDto.AcademicYearID ?? (int)_context.AcademicYearID,
                    ExamGroupID = toDto.ExamGroupID ?? (int?)null,
                    PresentStatusID = s.MarkStatusID ?? (byte?)null,
                    CreatedBy = toDto.MarkRegisterIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.MarkRegisterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.MarkRegisterIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.MarkRegisterIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    // TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                    // MarkRegisterIID = s.MarkRegisterID ?? 0
                }).ToList();

                // Delete Entities
                var studentIds = entities.Select(x => x.StudentId).ToList();
                var skillGroupIds = entities.SelectMany(x => x.MarkRegisterSkillGroups.Select(y => y.SkillGroupMasterID)).ToList();
                var skillIds = entities.SelectMany(x => x.MarkRegisterSkillGroups.SelectMany(y => y.MarkRegisterSkills.Select(z => z.SkillMasterID))).ToList();

                var existMarkReg = dbContext.MarkRegisterSkills
                    .Join(dbContext.MarkRegisters, markregsub => markregsub.MarkRegisterSkillGroup.MarkRegisterID, markreg => markreg.MarkRegisterIID, (markregsub, markreg) => new { markregsub, markreg })
                    .Where(x => x.markreg.ClassID == toDto.ClassID
                                && x.markreg.AcademicYearID == toDto.AcademicYearID
                                && x.markreg.ExamGroupID == toDto.ExamGroupID
                                && x.markreg.ExamID == toDto.ExamID
                                && x.markregsub.MarkRegisterSkillGroup.ClassSubjectSkillGroupMapID == toDto.SkillSetID
                                && x.markregsub.MarkRegisterSkillGroup.SubjectID == toDto.SubjectID
                                && studentIds.Contains(x.markreg.StudentId)
                                && skillGroupIds.Contains(x.markregsub.SkillGroupMasterID)
                                && skillIds.Contains(x.markregsub.SkillMasterID))
                    .Select(x => new StudentSkillsDTO
                    {
                        MarkRegisterSkillGroupID = x.markregsub.MarkRegisterSkillGroupID,
                        MarkRegisterSkillID = x.markregsub.MarkRegisterSkillIID,
                        MarkRegisterID = x.markregsub.MarkRegisterSkillGroup.MarkRegisterID,
                        StudentID = x.markreg.StudentId.Value,
                        SkillMasterID = x.markregsub.SkillMasterID,
                        SkillGroupMasterID = x.markregsub.SkillGroupMasterID,
                        ExamID = x.markreg.ExamID,
                        AcademicYearID = x.markreg.AcademicYearID,
                        ExamGroupID = x.markreg.ExamGroupID
                    });

                _sLstMarkRegister_IDs = existMarkReg.Select(x => x.MarkRegisterID.Value).ToList();
                _sLstMarkRegisterSkilGrp_IDs = existMarkReg.Select(x => x.MarkRegisterSkillGroupID.Value).ToList();
                _sLstMarkRegisterSkill_IDs = existMarkReg.Select(x => x.MarkRegisterSkillID.Value).ToList();


                //Delete:- Skills
                if (_sLstMarkRegisterSkill_IDs.Any())
                {
                    //_sLstMarkRegisterSkill_IDs
                    //    .All(w =>
                    //    {
                    var removableSkEntities = dbContext.MarkRegisterSkills
                                         .Where(x => _sLstMarkRegisterSkill_IDs.Contains(x.MarkRegisterSkillIID)).AsNoTracking();

                    if (removableSkEntities.Any())
                        dbContext.MarkRegisterSkills.RemoveRange(removableSkEntities);

                    dbContext.SaveChanges();
                    //    return true;
                    //});
                }
                //Delete:- Skills Groups
                if (_sLstMarkRegisterSkilGrp_IDs.Any())
                {
                    //_sLstMarkRegisterSkilGrp_IDs
                    //    .All(w =>
                    //    {
                    var removableSkGEntities = dbContext.MarkRegisterSkillGroups
                                         .Where(x => _sLstMarkRegisterSkilGrp_IDs.Contains(x.MarkRegisterSkillGroupIID) && x.MarkRegisterSkills.Count() == 0).AsNoTracking();

                    if (removableSkGEntities.Any())
                    {
                        dbContext.MarkRegisterSkillGroups.RemoveRange(removableSkGEntities);
                        //    return true;
                        //});
                        dbContext.SaveChanges();
                    }
                }
                //Delete:- Mark Register
                if (_sLstMarkRegister_IDs.Any())
                {
                    //_sLstMarkRegister_IDs
                    //    .All(w =>
                    //    {
                    var removableMarkRegEntities = dbContext.MarkRegisters
                                         .Where(x => _sLstMarkRegister_IDs.Contains(x.MarkRegisterIID) && x.MarkRegisterSkillGroups.Count() == 0).AsNoTracking();

                    if (removableMarkRegEntities.Any())
                    {
                        dbContext.MarkRegisters.RemoveRange(removableMarkRegEntities);
                        dbContext.SaveChanges();
                    }
                    //    return true;
                    //});

                }


                //Delete


                #endregion Delete Entities

                //Add or Modify entities
                #region Save Entities

                dbContext.MarkRegisters.AddRange(entities);
                dbContext.SaveChanges();


                #endregion Save Entities
                //dbContext.SaveChanges();
            }

            //#endregion DB Updates

            return ToDTOString(toDto);
        }

        private List<MarkRegisterSkillGroup> GetMarkRegisterSkillGroupMaps(long classSubjectSkillGroupMapID, int skillGrpID, List<MarkRegisterSkillGroupDTO> markRegisterSkillGroupDTO, int? subjectID)
        {
            var sRetData = new List<MarkRegisterSkillGroup>();

            var markRegisterSkills = markRegisterSkillGroupDTO.Select(x => x.MarkRegisterSkillsDTO
                    .Select(sk => new MarkRegisterSkill
                    {
                        SkillGroupMasterID = skillGrpID,
                        SkillMasterID = sk.SkillMasterID,
                        MarksObtained = sk.MarksObtained,
                        MarkRegisterSkillGroupID = sk.MarkRegisterSkillGroupID ?? 0,
                        // MarkRegisterSkillIID = sk.MarkRegisterSkillIID,
                        MarksGradeMapID = sk.MarksGradeMapID,
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now
                    })
                ).FirstOrDefault().ToList();

            var data = new MarkRegisterSkillGroup
            {
                ClassSubjectSkillGroupMapID = classSubjectSkillGroupMapID,
                SkillGroupMasterID = skillGrpID,
                CreatedBy = (int)_context.LoginID,
                CreatedDate = DateTime.Now,
                SubjectID = subjectID,
                MarkRegisterSkills = markRegisterSkills
            };

            sRetData.Add(data);

            return sRetData;
        }



        public List<KeyValueDTO> GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var groupList = new List<KeyValueDTO>();

                var classMaps = dbContext.ExamClassMaps
                    .Include(i => i.Exam)
                    .ThenInclude(i => i.ExamGroup)
                    .ThenInclude(i => i.Exams)
                    .Where(X => X.ClassID == classID && (sectionID == null || X.SectionID == sectionID) && X.Exam.AcademicYearID == academicYearID).AsNoTracking();

                groupList = (from n in classMaps
                             select new KeyValueDTO
                             {
                                 Key = n.Exam.ExamGroupID.ToString(),
                                 Value = n.Exam.ExamGroup.ExamGroupName
                             }).ToList();

                return groupList;
            }
        }

        public List<KeyValueDTO> GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {

                var lstSkills = new List<KeyValueDTO>();
                //var SkillSetList = (from es in dbContext.ExamSkillMaps
                //                    join ec in dbContext.ExamClassMaps on es.ExamID equals ec.ExamID
                //                    where ec.ClassID == classID && (sectionID == null
                //                    || ec.SectionID == sectionID) && ec.ExamID == examID.Value
                //                    && es.Exam.AcademicYearID == academicYearID
                //                    select new KeyValueDTO
                //                    {
                //                        Key = es.ClassSubjectSkillGroupMapID.ToString(),
                //                        Value = es.ClassSubjectSkillGroupMap.Description
                //                    });
                List<ClassSubjectSkillGroupMap> SkillSetList = (from es in dbContext.ExamSkillMaps
                                                                join ec in dbContext.ExamClassMaps on es.ExamID equals ec.ExamID
                                                                where ec.ClassID == classID && (sectionID == null
                                                                || ec.SectionID == sectionID) && ec.ExamID == examID.Value
                                                                && es.Exam.AcademicYearID == academicYearID
                                                                select es.ClassSubjectSkillGroupMap
                                  ).AsNoTracking().ToList();

                if (languageTypeID.HasValue && SkillSetList.Count() > 0)

                    SkillSetList.RemoveAll(w => !SkillSetList.Any(x => x.Subject != null && x.Subject.SubjectTypeID == languageTypeID));
                if (SkillSetList.Any())
                {
                    SkillSetList.All(w =>
                    {
                        var lst = new KeyValueDTO { Key = w.ClassSubjectSkillGroupMapID.ToString(), Value = w.Description };
                        lstSkills.Add(lst);
                        return true;
                    });
                }
                return lstSkills;
            }

        }

        public List<KeyValueDTO> GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID)
        {
            var SkillGrpList = new List<KeyValueDTO>(); using (var dbContext = new dbEduegateSchoolContext())
            {
                var skillentities = dbContext.ClassSubjectSkillGroupSkillMaps.Where(x => x.ClassSubjectSkillGroupMapID == skillSetID).Select(x => new { SkillGroupMasterID = x.SkillGroupMasterID, SkillGroup = x.SkillGroupMaster.SkillGroup }).Distinct().AsNoTracking().ToList();

                SkillGrpList = skillentities.Select(s => new KeyValueDTO
                {
                    Key = s.SkillGroupMasterID.ToString(),
                    Value = s.SkillGroup
                }).ToList();
            }

            return SkillGrpList;
        }

        public List<KeyValueDTO> GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID)
        {
            var SkillGrpList = new List<KeyValueDTO>(); using (var dbContext = new dbEduegateSchoolContext())
            {
                var skillentities = dbContext.ClassSubjectSkillGroupSkillMaps.Where(x => x.ClassSubjectSkillGroupMapID == skillSetID && x.SkillGroupMasterID == skillGroupID).Select(x => new { SkillMasterID = x.SkillMasterID, Skill = x.SkillMaster.SkillName }).Distinct().AsNoTracking().ToList();

                SkillGrpList = skillentities.Select(s => new KeyValueDTO
                {
                    Key = s.SkillMasterID.ToString(),
                    Value = s.Skill
                }).ToList();
            }

            return SkillGrpList;
        }

        #region Listing Student's CoScholastic Data 

        public List<MarkRegisterDetailsDTO> GetCoScholasticEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            List<MarkRegisterDetailsDTO> sRetData = new List<MarkRegisterDetailsDTO>();
            int? subjectID = null; byte? subjectTypeID = null;

            var islamicSubjID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("ISLAMIC_STUDIES_SUBJID");
            var moralSubjID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("MORAL_SCIENCE_SUBJID");

            using (var sContext = new dbEduegateSchoolContext())
            {
                #region Validations for skills
                ////Temporary Fix
                //var className = sContext.Classes.Where(c => c.ClassID == markEntrySearchArgsDTO.ClassID).Select(x=> x.ClassDescription).AsNoTracking().FirstOrDefault();

                //if (!className.Contains("KG") && markEntrySearchArgsDTO.SkillID == null)
                //{
                //    throw new Exception("Please select skills!");
                //}
                #endregion

                var dFndSkills = (from n in sContext.ClassSubjectSkillGroupSkillMaps
                                  where n.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID
                                  && n.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                                  && (markEntrySearchArgsDTO.SkillID == null || (n.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                                  && n.SkillMasterID == markEntrySearchArgsDTO.SkillID))
                                  select new SkillSDataDTO
                                  {
                                      SkillGroupMasterID = n.SkillGroupMasterID,
                                      SkillGroup = n.SkillGroupMaster.SkillGroup,
                                      SkillMasterID = n.SkillMasterID,
                                      Skill = n.SkillMaster.SkillName,
                                      MarkGradeID = n.MarkGradeID
                                  }).Distinct().AsNoTracking();


                var subjectData = sContext.SkillGroupSubjectMaps.Where(x => x.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID).AsNoTracking().Select(x => x.Subject).FirstOrDefault();

                if (subjectData != null)
                {
                    subjectID = subjectData.SubjectID;
                    subjectTypeID = subjectData.SubjectTypeID;
                }
                if (dFndSkills.Any())
                {
                    var mgMap = (from markGrade in sContext.MarkGradeMaps

                                 select new MarkGradeMapDTO()
                                 {
                                     GradeTo = markGrade.GradeTo,
                                     GradeFrom = markGrade.GradeFrom,
                                     GradeName = markGrade.GradeName + " - " + markGrade.Description,
                                     Description = markGrade.Description,
                                     MarksGradeID = markGrade.MarkGradeID,
                                     IsPercentage = markGrade.IsPercentage,
                                     MarksGradeMapIID = markGrade.MarksGradeMapIID
                                 }).AsNoTracking().ToList();

                    List<StudentDTO> studentList = (from n in sContext.Students
                                                    join a in sContext.AcademicYears on n.SchoolAcademicyearID equals a.AcademicYearID
                                                    where n.ClassID == markEntrySearchArgsDTO.ClassID && n.IsActive == true
                                                    && n.SectionID == markEntrySearchArgsDTO.SectionID && n.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID //&& a.AcademicYearStatusID != 3
                                                    && n.Status == 1
                                                    //&& n.StudentIID == 2480
                                                    orderby n.AdmissionNumber
                                                    select new StudentDTO
                                                    {
                                                        StudentIID = n.StudentIID,
                                                        StudentFullName = n.FirstName + " " + n.MiddleName + " " + n.LastName,
                                                        RollNumber = n.AdmissionNumber,
                                                        SecoundLanguageID = n.SecondLangID,
                                                        ThridLanguageID = n.ThirdLangID,
                                                        SubjectMapID = n.SubjectMapID
                                                    }).AsNoTracking().ToList();

                    List<long> studentIDS = studentList.Select(x => (x.StudentIID)).ToList();

                    var studentOptionSubList = (from n in sContext.Students
                                                join o in sContext.StudentStreamOptionalSubjectMaps on n.StudentIID equals o.StudentID
                                                where studentIDS.Contains(n.StudentIID) && (subjectID != null || o.SubjectID == subjectID)
                                                && n.Status == 1 && n.IsActive == true
                                                select n.StudentIID
                                               ).ToList();

                    if (studentOptionSubList.Count() > 0)
                    {
                        var studentOptionSub = studentList.Where(x => studentOptionSubList.Contains(x.StudentIID)).ToList();
                        studentList.RemoveAll(w => !studentOptionSub.Any(x => x == w));
                    }

                    //Check Ism/Msc SubjectIDs
                    if (markEntrySearchArgsDTO.SubjectMapID == islamicSubjID || markEntrySearchArgsDTO.SubjectMapID == moralSubjID)
                    {
                        var subjectMapID = studentList.Where(x => x.SubjectMapID == markEntrySearchArgsDTO.SubjectMapID).ToList();
                        studentList.RemoveAll(w => !subjectMapID.Any(x => x == w));
                    }

                    if (markEntrySearchArgsDTO.LanguageTypeID.HasValue && markEntrySearchArgsDTO.LanguageTypeID == 2)
                    {
                        var secondLangStud = studentList.Where(x => x.SecoundLanguageID == subjectID).ToList();
                        studentList.RemoveAll(w => !secondLangStud.Any(x => x == w));
                    }
                    if (markEntrySearchArgsDTO.LanguageTypeID.HasValue && markEntrySearchArgsDTO.LanguageTypeID == 3)
                    {
                        var thirdLangStud = studentList.Where(x => x.ThridLanguageID == subjectID).ToList();
                        studentList.RemoveAll(w => !thirdLangStud.Any(x => x == w));
                    }

                    studentIDS = studentList.Select(x => (x.StudentIID)).ToList();
                    var dfndMarkReg = (from markregsub in sContext.MarkRegisterSkills
                                       join markreg in sContext.MarkRegisters on markregsub.MarkRegisterSkillGroup.MarkRegisterID
                                       equals markreg.MarkRegisterIID
                                       where markreg.ClassID == markEntrySearchArgsDTO.ClassID
                                       //&& markreg.SectionID == markEntrySearchArgsDTO.SectionID
                                       && markreg.ExamGroupID == markEntrySearchArgsDTO.TermID && markreg.ExamID == markEntrySearchArgsDTO.ExamID
                                  && markregsub.MarkRegisterSkillGroup.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID
                                  && ((markregsub.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                                  && markEntrySearchArgsDTO.SkillID == null) || (markregsub.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                                  && markregsub.SkillMasterID == markEntrySearchArgsDTO.SkillID))
                                  && studentIDS.Contains(markreg.StudentId.Value)

                                       select new StudentSkillsDTO()
                                       {
                                           StudentID = markreg.StudentId.Value,
                                           SkillGroupMasterID = markregsub.SkillGroupMasterID,
                                           SkillMasterID = markregsub.SkillMasterID,
                                           MarksObtained = markregsub.MarksObtained,
                                           GradeID = markregsub.MarksGradeMapID,
                                           PresentStatusID = markreg.PresentStatusID,
                                           MarkRegisterSkillGroupID = markregsub.MarkRegisterSkillGroupID,
                                           MarkRegisterSkillID = markregsub.MarkRegisterSkillIID,
                                           MarkRegisterID = markregsub.MarkRegisterSkillGroup.MarkRegisterID
                                       }).AsNoTracking().ToList();
                    studentList.All(w =>
                    {
                        var studMarkReg = new List<StudentSkillsDTO>();
                        if (dfndMarkReg.Any())
                            studMarkReg = dfndMarkReg.Where(x => x.StudentID == w.StudentIID).ToList();
                        sRetData.Add(GetStudentData(w.RollNumber, w.StudentIID, w.StudentFullName, markEntrySearchArgsDTO,
                                              dFndSkills.ToList(), mgMap, studMarkReg.ToList())); return true;
                    });
                }
            }


            return sRetData;
        }
        private MarkRegisterDetailsDTO GetStudentData(string EnrolNo, long StudentID, string StudentName, MarkEntrySearchArgsDTO markEntrySearchArgsDTO, List<SkillSDataDTO> skill, List<MarkGradeMapDTO> MarkGradeMap, List<StudentSkillsDTO> markRegData)
        {

            var markRegisterSplitDTO = GetSkillGroupData(StudentID, markEntrySearchArgsDTO, skill, MarkGradeMap, markRegData);
            var sRetData = new MarkRegisterDetailsDTO()
            {
                StudentID = StudentID,
                Student = new KeyValueDTO() { Key = StudentID.ToString(), Value = StudentName },
                AdmissionNumber = EnrolNo,
                MarkRegisterSkillGroupDTO = markRegisterSplitDTO,
                MarkStatusID = markRegData.Select(x => x.PresentStatusID).FirstOrDefault()
            };

            return sRetData;
        }

        private List<MarkRegisterSkillGroupDTO> GetSkillGroupData(long studentID, MarkEntrySearchArgsDTO markEntrySearchArgsDTO, List<SkillSDataDTO> skill, List<MarkGradeMapDTO> MarkGradeMap, List<StudentSkillsDTO> markRegData)
        {
            var sRetData = (from s in skill
                            select new MarkRegisterSkillGroupDTO
                            {
                                SkillGroupMasterID = s.SkillGroupMasterID,
                                SkillGroup = s.SkillGroup,
                                MarkRegisterSkillsDTO = (from sk in skill
                                                         select new MarkRegisterSkillsDTO
                                                         {
                                                             SkillGroupMasterID = s.SkillGroupMasterID,
                                                             SkillGroup = s.SkillGroup,
                                                             SkillMasterID = sk.SkillMasterID,
                                                             Skill = sk.Skill,
                                                             MinimumMark = sk.MaximumMarks,
                                                             MaximumMark = sk.MaximumMarks,
                                                             ConvertionFactor = sk.ConvertionFactor,
                                                             MarkGradeMapList = MarkGradeMap.Count > 0 ?
                                                            (from mg in MarkGradeMap
                                                             where mg.MarksGradeID == sk.MarkGradeID
                                                             select new KeyValueDTO
                                                             {
                                                                 Key = mg.MarksGradeMapIID.ToString(),
                                                                 Value = mg.GradeName
                                                             }).ToList() : null,
                                                             MarksObtained = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                 && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarksObtained).FirstOrDefault() : (decimal?)null,
                                                             MarksGradeMapID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                  && x.SkillMasterID == sk.SkillMasterID && x.GradeID != null).Select(y => y.GradeID).FirstOrDefault() : (long?)null,

                                                             MarkRegisterSkillGroupID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                      && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarkRegisterSkillGroupID).FirstOrDefault() : (long?)null,
                                                             MarkRegisterSkillIID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                     && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarkRegisterSkillID ?? 0).FirstOrDefault() : 0,

                                                             MarkRegisterID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                     && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarkRegisterID).FirstOrDefault() : (long?)null,
                                                         }).ToList()
                            }

            ).ToList();

            return sRetData;

        }

        private MarkRegisterSkillGroup GetMarkRegSkillGroupData(dbEduegateSchoolContext sContext, long studentID, MarkEntrySearchArgsDTO markEntrySearchArgsDTO, List<ClassSubjectSkillGroupMap> skillgrp, List<ClassSubjectSkillGroupSkillMap> skill)
        {

            MarkRegisterSkillGroup sRetData = new MarkRegisterSkillGroup();
            var dFnd = (from n in sContext.MarkRegisters
                        join m in sContext.MarkRegisterSkillGroups on n.MarkRegisterIID equals m.MarkRegisterID
                        where n.StudentId == studentID && n.ClassID == markEntrySearchArgsDTO.ClassID
                        && n.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID && m.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                        select m).Include(m => m.MarkGradeMap).FirstOrDefault();
            if (dFnd.IsNotNull())
                sRetData = dFnd;
            return sRetData;

        }

        #endregion

        #endregion

        #region Mark Entry

        public string SaveMarkEntry(BaseMasterDTO dto)
        {
            var _sLstMarkRegister_IDs = new List<long>();
            var _sLstMarkRegisterSubject_IDs = new List<long>();
            #region Validate DTOs
            var toDto = dto as MarkRegisterDTO;


            if (toDto.Class == null || toDto.Section == null || toDto.Exam == null)
            {
                throw new Exception("Please fill required fields!");
            }
            else if (toDto.StudentMarkEntryList == null || toDto.StudentMarkEntryList.Count == 0)
            {
                throw new Exception("Please fill student details!");
            }
            #endregion Validate DTOs

            #region Map Entities

            List<MarkRegister> entities = new List<MarkRegister>();
            if (toDto.StudentMarkEntryList.Any())
            {
                toDto.StudentMarkEntryList.All(w =>
                {
                    entities = (from s in toDto.StudentMarkEntryList
                                select new MarkRegister()
                                {
                                    ExamID = toDto.ExamID,
                                    MarkRegisterSubjectMaps = GetMarkRegisterSubjectMaps(toDto.StudentMarkEntryList.Where(x => x.StudentID == s.StudentID).FirstOrDefault()),
                                    ClassID = toDto.ClassID.Value,
                                    StudentId = s.StudentID,
                                    SectionID = toDto.SectionID != null ? toDto.SectionID : null,
                                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                                    AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                                    ExamGroupID = toDto.ExamGroupID.HasValue ? toDto.ExamGroupID.Value : (int?)null,
                                    PresentStatusID = s.PresentStatusID.HasValue ? s.PresentStatusID.Value : (byte?)null,
                                    CreatedBy = toDto.MarkRegisterIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                    UpdatedBy = toDto.MarkRegisterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                    CreatedDate = toDto.MarkRegisterIID == 0 ? DateTime.Now : dto.CreatedDate,
                                    UpdatedDate = toDto.MarkRegisterIID > 0 ? DateTime.Now : dto.UpdatedDate,
                                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                                    //MarkRegisterIID = s.MarkRegisterID.HasValue ? s.MarkRegisterID.Value : 0,

                                }).ToList();


                    return true;
                });
            }
            #endregion Map Entities

            #region DB Updates
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entities.Any())
                {
                    // Delete entities, if not exists in the user input.
                    #region Delete Entities

                    List<long?> studentIDS = entities.Select(x => (x.StudentId)).ToList();

                    var existMarkReg = (from markregsub in dbContext.MarkRegisterSubjectMaps
                                        join markreg in dbContext.MarkRegisters on
                                        markregsub.MarkRegisterID equals markreg.MarkRegisterIID
                                        where markreg.ClassID == toDto.ClassID
                                        //&& markreg.SectionID == toDto.SectionID
                                        && markreg.ExamID == toDto.ExamID && markreg.ExamGroupID == toDto.ExamGroupID
                                        && markreg.AcademicYearID == toDto.AcademicYearID
                                        && markregsub.SubjectID == toDto.SubjectID
                                        && studentIDS.Contains(markreg.StudentId)
                                        select new StudentMarkEntryDTO()
                                        {
                                            MarkRegisterID = markreg.MarkRegisterIID,
                                            MarkRegisterSubjectMapID = markregsub.MarkRegisterSubjectMapIID
                                        }).AsNoTracking();

                    _sLstMarkRegister_IDs = existMarkReg.Select(x => x.MarkRegisterID.Value).ToList();
                    _sLstMarkRegisterSubject_IDs = existMarkReg.Select(x => x.MarkRegisterSubjectMapID.Value).ToList();

                    // Delete:-SubjectMap
                    {
                        var removableSmEntities = dbContext.MarkRegisterSubjectMaps
                                              .Where(x => _sLstMarkRegisterSubject_IDs.Contains(x.MarkRegisterSubjectMapIID))
                                              .AsNoTracking().ToList();

                        if (removableSmEntities != null && removableSmEntities.Count > 0)
                        {
                            dbContext.MarkRegisterSubjectMaps.RemoveRange(removableSmEntities);

                            dbContext.SaveChanges();
                        }
                    }

                    //Delete:- Mark Register
                    {
                        var removableEntities = dbContext.MarkRegisters
                                               .Where(x => _sLstMarkRegister_IDs.Contains(x.MarkRegisterIID) && x.MarkRegisterSubjectMaps.Count() == 0)
                                               .AsNoTracking().ToList();

                        if (removableEntities != null && removableEntities.Count > 0)
                        {
                            dbContext.MarkRegisters.RemoveRange(removableEntities);
                        }
                    }
                    #endregion Delete Entities

                    // Add or Modify entities
                    #region Save Entities

                    //non-marked students need to be delete from list
                    var getNullStudentslist = toDto.StudentMarkEntryList.Where(x => x.MarksObtained == null && x.GradeID == null && x.PresentStatusID == null).Select(x => x.StudentID).ToList();
                    entities = entities.Where(s => !getNullStudentslist.Contains(s.StudentId)).ToList();

                    dbContext.MarkRegisters.AddRange(entities);
                    dbContext.SaveChanges();

                    #endregion Save Entities
                }
            }
            #endregion DB Updates

            return ToDTOString(toDto);
        }

        private List<MarkRegisterSubjectMap> GetMarkRegisterSubjectMaps(StudentMarkEntryDTO studentMarkEntry)
        {
            var sRetData = new List<MarkRegisterSubjectMap>();
            if (studentMarkEntry != null)
            {
                sRetData.Add(new MarkRegisterSubjectMap
                {
                    //MarkRegisterSubjectMapIID = studentMarkEntry.MarkRegisterSubjectMapID.HasValue ? studentMarkEntry.MarkRegisterSubjectMapID.Value : 0,
                    MarkRegisterSubjectMapIID = 0,
                    //MarkRegisterID = studentMarkEntry.MarkRegisterID,
                    MarkRegisterID = 0,
                    MarksGradeMapID = studentMarkEntry.GradeID,
                    Mark = studentMarkEntry.MarksObtained,
                    SubjectID = studentMarkEntry.SubjectID,
                    TotalMark = studentMarkEntry.TotalMark,
                    ConversionFactor = studentMarkEntry.MarkConvertionFactor,
                });
            }

            return sRetData;
        }
        #region Listing Student's Mark Entry Data 

        public List<StudentMarkEntryDTO> GetMarkEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            List<StudentMarkEntryDTO> sRetData = new List<StudentMarkEntryDTO>();
            using (var sContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var islamicSubjID = new Domain.Setting.SettingBL(null).GetSettingValue<long?>("ISLAMIC_STUDIES_SUBJID");
                var moralSubjID = new Domain.Setting.SettingBL(null).GetSettingValue<long?>("MORAL_SCIENCE_SUBJID");

                var classes = sContext.Classes.Where(a => a.ClassID == markEntrySearchArgsDTO.ClassID).AsNoTracking().FirstOrDefault();
                string className = classes.ClassDescription;

                var dFndexam = (from n in sContext.Exams
                                join ec in sContext.ExamClassMaps on n.ExamIID equals ec.ExamID
                                join eb in sContext.ExamSubjectMaps on n.ExamIID equals eb.ExamID
                                where (n.ExamIID == markEntrySearchArgsDTO.ExamID
                                  && n.ExamGroupID == markEntrySearchArgsDTO.TermID
                                  && n.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID
                                  && ec.ClassID == markEntrySearchArgsDTO.ClassID &&
                                  ec.SectionID == markEntrySearchArgsDTO.SectionID &&
                                  eb.SubjectID == markEntrySearchArgsDTO.SubjectId
                                     )
                                select eb)
                                .Include(x => x.Subject).AsNoTracking();

                if (dFndexam.Any())
                {
                    var maxMark = dFndexam.Select(x => x.MaximumMarks).FirstOrDefault();
                    var markConvertionFactor = dFndexam.Select(x => x.ConversionFactor).FirstOrDefault();
                    List<StudentDTO> studentList = (from n in sContext.Students
                                                    where n.ClassID == markEntrySearchArgsDTO.ClassID && n.IsActive == true && n.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID
                                                    && n.SectionID == markEntrySearchArgsDTO.SectionID
                                                    && n.Status == 1
                                                    orderby n.AdmissionNumber
                                                    select new StudentDTO
                                                    {
                                                        StudentIID = n.StudentIID,
                                                        StudentFullName = n.FirstName + " " + n.MiddleName + " " + n.LastName,
                                                        RollNumber = n.AdmissionNumber,
                                                        SecoundLanguageID = n.SecondLangID,
                                                        ThridLanguageID = n.ThirdLangID,
                                                        SubjectMapID = n.SubjectMapID
                                                    }).AsNoTracking().ToList();

                    List<long> studentIDS = studentList.Select(x => (x.StudentIID)).ToList();
                    List<StudentDTO> studentOptionSubList = (from n in sContext.Students
                                                             join o in sContext.StudentStreamOptionalSubjectMaps on n.StudentIID equals o.StudentID
                                                             where studentIDS.Contains(n.StudentIID) && (o.SubjectID == markEntrySearchArgsDTO.SubjectId)
                                                             && n.Status == 1
                                                             orderby n.AdmissionNumber
                                                             select new StudentDTO
                                                             {
                                                                 StudentIID = n.StudentIID,
                                                                 StudentFullName = n.FirstName + " " + n.MiddleName + " " + n.LastName,
                                                                 RollNumber = n.AdmissionNumber,
                                                                 SecoundLanguageID = n.SecondLangID,
                                                                 ThridLanguageID = n.ThirdLangID,
                                                                 SubjectMapID = n.SubjectMapID
                                                             }).AsNoTracking().ToList();

                    List<StudentDTO> studentCumpolSubList = (from n in sContext.Students
                                                             join o in sContext.Streams on n.StreamID equals o.StreamID
                                                             join s in sContext.StreamSubjectMaps on n.StreamID equals s.StreamID
                                                             where studentIDS.Contains(n.StudentIID) && (s.SubjectID == markEntrySearchArgsDTO.SubjectId)
                                                             && n.Status == 1
                                                             orderby n.AdmissionNumber
                                                             select new StudentDTO
                                                             {
                                                                 StudentIID = n.StudentIID,
                                                                 StudentFullName = n.FirstName + " " + n.MiddleName + " " + n.LastName,
                                                                 RollNumber = n.AdmissionNumber,
                                                                 SecoundLanguageID = n.SecondLangID,
                                                                 ThridLanguageID = n.ThirdLangID,
                                                                 SubjectMapID = n.SubjectMapID
                                                             }).AsNoTracking().ToList();

                    List<StudentDTO> studentSubjectList = new List<StudentDTO>();

                    studentSubjectList.AddRange(studentOptionSubList);
                    studentSubjectList.AddRange(studentCumpolSubList);

                    if (markEntrySearchArgsDTO.SubjectId == moralSubjID || markEntrySearchArgsDTO.SubjectId == islamicSubjID)
                    {
                        var subjectMapID = studentList.Where(x => x.SubjectMapID == markEntrySearchArgsDTO.SubjectId).ToList();
                        studentList.RemoveAll(w => !subjectMapID.Any(x => x == w));
                    }

                    if (markEntrySearchArgsDTO.LanguageTypeID.HasValue && markEntrySearchArgsDTO.LanguageTypeID == 2)
                    {
                        var secondLangStud = studentList.Where(x => x.SecoundLanguageID == markEntrySearchArgsDTO.SubjectId).ToList();
                        studentList.RemoveAll(w => !secondLangStud.Any(x => x == w));
                    }
                    if (markEntrySearchArgsDTO.LanguageTypeID.HasValue && markEntrySearchArgsDTO.LanguageTypeID == 3)
                    {
                        var thirdLangStud = studentList.Where(x => x.ThridLanguageID == markEntrySearchArgsDTO.SubjectId).ToList();
                        studentList.RemoveAll(w => !thirdLangStud.Any(x => x == w));
                    }

                    if (className.Contains("Class 11") || className.Contains("Class 12"))
                    {
                        if (studentSubjectList.Count() > 0)
                        {
                            if (studentSubjectList.Any())
                            {
                                var dfndMarkReg = (from markregsub in sContext.MarkRegisterSubjectMaps
                                                   join markreg in sContext.MarkRegisters on
                                                   markregsub.MarkRegisterID equals markreg.MarkRegisterIID
                                                   where markreg.ClassID == markEntrySearchArgsDTO.ClassID
                                                   && markregsub.SubjectID == markEntrySearchArgsDTO.SubjectId
                                                   && markreg.ExamID == markEntrySearchArgsDTO.ExamID
                                                   && markreg.ExamGroupID == markEntrySearchArgsDTO.TermID
                                                   // && markreg.SectionID == markEntrySearchArgsDTO.SectionID
                                                   && markreg.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID

                                                   select new StudentMarkEntryDTO()
                                                   {
                                                       StudentID = markreg.StudentId.Value,
                                                       SubjectID = markregsub.SubjectID,
                                                       MarksObtained = markregsub.Mark,
                                                       GradeID = markregsub.MarksGradeMapID,
                                                       PresentStatusID = markreg.PresentStatusID,
                                                       MarkRegisterID = markreg.MarkRegisterIID,
                                                       MarkRegisterSubjectMapID = markregsub.MarkRegisterSubjectMapIID,
                                                       TotalMark = markregsub.TotalMark,
                                                       MarkConvertionFactor = markregsub.ConversionFactor,
                                                   }).AsNoTracking().ToList();

                                studentSubjectList.All(o =>
                                {
                                    var studentMarkEntryDTO = new StudentMarkEntryDTO();

                                    if (dfndMarkReg.Any())
                                    {
                                        studentMarkEntryDTO = dfndMarkReg.Where(s => s.StudentID == o.StudentIID).FirstOrDefault();
                                    }
                                    sRetData.Add(GetStudentForMarkEntry(o.RollNumber, o.StudentIID, o.StudentFullName,
                                        markEntrySearchArgsDTO, maxMark, markConvertionFactor, studentMarkEntryDTO));
                                    return true;

                                });
                            }
                        }
                        else
                        {
                            if (studentList.Any())
                            {
                                var dfndMarkReg = (from markregsub in sContext.MarkRegisterSubjectMaps
                                                   join markreg in sContext.MarkRegisters on
                                                   markregsub.MarkRegisterID equals markreg.MarkRegisterIID
                                                   where markreg.ClassID == markEntrySearchArgsDTO.ClassID
                                                   && markregsub.SubjectID == markEntrySearchArgsDTO.SubjectId
                                                   && markreg.ExamID == markEntrySearchArgsDTO.ExamID
                                                   && markreg.ExamGroupID == markEntrySearchArgsDTO.TermID
                                                   // && markreg.SectionID == markEntrySearchArgsDTO.SectionID
                                                   && markreg.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID

                                                   select new StudentMarkEntryDTO()
                                                   {
                                                       StudentID = markreg.StudentId.Value,
                                                       SubjectID = markregsub.SubjectID,
                                                       MarksObtained = markregsub.Mark,
                                                       GradeID = markregsub.MarksGradeMapID,
                                                       PresentStatusID = markreg.PresentStatusID,
                                                       MarkRegisterID = markreg.MarkRegisterIID,
                                                       MarkRegisterSubjectMapID = markregsub.MarkRegisterSubjectMapIID,
                                                       TotalMark = markregsub.TotalMark,
                                                       MarkConvertionFactor = markregsub.ConversionFactor,
                                                   }).AsNoTracking().ToList();

                                studentList.All(w =>
                                {
                                    var studentMarkEntryDTO = new StudentMarkEntryDTO();

                                    if (dfndMarkReg.Any())
                                    {
                                        studentMarkEntryDTO = dfndMarkReg.Where(s => s.StudentID == w.StudentIID).FirstOrDefault();
                                    }
                                    sRetData.Add(GetStudentForMarkEntry(w.RollNumber, w.StudentIID, w.StudentFullName,
                                        markEntrySearchArgsDTO, maxMark, markConvertionFactor, studentMarkEntryDTO));
                                    return true;

                                });
                            }
                        }
                    }

                    else
                    {

                        if (studentList.Any())
                        {
                            var dfndMarkReg = (from markregsub in sContext.MarkRegisterSubjectMaps
                                               join markreg in sContext.MarkRegisters on
                                               markregsub.MarkRegisterID equals markreg.MarkRegisterIID
                                               where markreg.ClassID == markEntrySearchArgsDTO.ClassID
                                               && markregsub.SubjectID == markEntrySearchArgsDTO.SubjectId
                                               && markreg.ExamID == markEntrySearchArgsDTO.ExamID
                                               && markreg.ExamGroupID == markEntrySearchArgsDTO.TermID
                                               // && markreg.SectionID == markEntrySearchArgsDTO.SectionID
                                               && markreg.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID

                                               select new StudentMarkEntryDTO()
                                               {
                                                   StudentID = markreg.StudentId.Value,
                                                   SubjectID = markregsub.SubjectID,
                                                   MarksObtained = markregsub.Mark,
                                                   GradeID = markregsub.MarksGradeMapID,
                                                   PresentStatusID = markreg.PresentStatusID,
                                                   MarkRegisterID = markreg.MarkRegisterIID,
                                                   MarkRegisterSubjectMapID = markregsub.MarkRegisterSubjectMapIID,
                                                   TotalMark = markregsub.TotalMark,
                                                   MarkConvertionFactor = markregsub.ConversionFactor,
                                               }).AsNoTracking().ToList();

                            studentList.All(w =>
                            {
                                var studentMarkEntryDTO = new StudentMarkEntryDTO();

                                if (dfndMarkReg.Any())
                                {
                                    studentMarkEntryDTO = dfndMarkReg.Where(s => s.StudentID == w.StudentIID).FirstOrDefault();
                                }
                                sRetData.Add(GetStudentForMarkEntry(w.RollNumber, w.StudentIID, w.StudentFullName,
                                    markEntrySearchArgsDTO, maxMark, markConvertionFactor, studentMarkEntryDTO));
                                return true;

                            });
                        }
                    }
                }
            }

            return sRetData;
        }

        private StudentMarkEntryDTO GetStudentForMarkEntry(string EnrolNo, long StudentID, string StudentName, MarkEntrySearchArgsDTO markEntrySearchArgsDTO, decimal? maxMark, decimal? markConvertionFactor, StudentMarkEntryDTO studentMarkEntryDTO)
        {

            StudentMarkEntryDTO sRetData = new StudentMarkEntryDTO()
            {
                StudentID = StudentID,
                StudentName = StudentName,
                EnrolNo = EnrolNo,
                MaxMark = maxMark ?? 0,
                TotalMark = studentMarkEntryDTO != null ? studentMarkEntryDTO.TotalMark : (decimal?)null,
                MarkConvertionFactor = markConvertionFactor ?? 0,
                MarksObtained = studentMarkEntryDTO != null ? studentMarkEntryDTO.MarksObtained : (decimal?)null,
                GradeID = studentMarkEntryDTO != null ? studentMarkEntryDTO.GradeID : (long?)null,
                PresentStatusID = studentMarkEntryDTO != null ? studentMarkEntryDTO.PresentStatusID : (byte?)null,
                MarkRegisterID = studentMarkEntryDTO != null ? studentMarkEntryDTO.MarkRegisterID : (long?)null,
                MarkRegisterSubjectMapID = studentMarkEntryDTO != null ? studentMarkEntryDTO.MarkRegisterSubjectMapID : (long?)null,

            };

            return sRetData;
        }

        #endregion

        #endregion

        #region ScholasticInternaL Entry

        public string SaveScholasticInternalEntry(BaseMasterDTO dto)
        {
            #region Validate DTOs

            var toDto = dto as MarkRegisterDTO;
            //MarkRegisterDTO markRegister = new MarkRegisterDTO();
            if (toDto.Class == null || toDto.Section == null || toDto.Exam == null)
            {
                throw new Exception("Please fill required fields!");
            }
            else if (toDto.MarkRegistersDetails == null || toDto.MarkRegistersDetails.Count == 0 || !toDto.MarkRegistersDetails[0].StudentID.HasValue)
            {
                throw new Exception("Please fill student details!");
            }
            #endregion Validate DTOs


            using (var dbContext = new dbEduegateSchoolContext())
            {
                //Map DTO to Entities
                #region Map Entities
                var entities = new List<MarkRegister>();
                var _sLstMarkRegister_IDs = new List<long>();
                var _sLstMarkRegisterSkilGrp_IDs = new List<long>();
                var _sLstMarkRegisterSkill_IDs = new List<long>();

                entities = (from s in toDto.MarkRegistersDetails
                            select new MarkRegister()
                            {
                                ExamID = toDto.ExamID,
                                MarkRegisterSkillGroups = GetMarkRegisterSkillGroupMaps(toDto.SkillSetID.Value, toDto.SkillGroupID.Value, s.MarkRegisterSkillGroupDTO, toDto.SubjectID),
                                ClassID = toDto.ClassID.Value,
                                StudentId = s.StudentID,
                                SectionID = toDto.SectionID != null ? toDto.SectionID : null,
                                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                                ExamGroupID = toDto.ExamGroupID.HasValue ? toDto.ExamGroupID.Value : (int?)null,
                                PresentStatusID = s.MarkStatusID.HasValue ? s.MarkStatusID.Value : (byte?)null,
                                CreatedBy = toDto.MarkRegisterIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                UpdatedBy = toDto.MarkRegisterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                CreatedDate = toDto.MarkRegisterIID == 0 ? DateTime.Now : dto.CreatedDate,
                                UpdatedDate = toDto.MarkRegisterIID > 0 ? DateTime.Now : dto.UpdatedDate,
                                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                                // MarkRegisterIID = s.MarkRegisterID.HasValue ? s.MarkRegisterID.Value : 0,

                            }).ToList();

                #endregion Map Entities

                //#region DB Updates


                //Deletes entitie
                #region Delete Entities
                List<long?> studentIDS = entities.Select(x => (x.StudentId)).ToList();
                List<int?> skllsGrpIDS = entities.SelectMany(x => (x.MarkRegisterSkillGroups.Select(y => y.SkillGroupMasterID))).ToList();
                List<int?> skllsIDS = entities.SelectMany(x => (x.MarkRegisterSkillGroups.SelectMany(y => y.MarkRegisterSkills.Select(z => z.SkillMasterID)))).ToList();


                var existMarkReg = dbContext.MarkRegisterSkills
                    .Join(dbContext.MarkRegisters, markregsub => markregsub.MarkRegisterSkillGroup.MarkRegisterID, markreg => markreg.MarkRegisterIID,
                    (markregsub, markreg) => new { markregsub, markreg })
                    //.Include(x => x.markreg.MarkRegisterSkillGroups)
                    .Where(x => x.markreg.ClassID == toDto.ClassID
                                && x.markreg.AcademicYearID == toDto.AcademicYearID
                                && x.markreg.ExamGroupID == toDto.ExamGroupID
                                && x.markreg.ExamID == toDto.ExamID
                                && x.markregsub.MarkRegisterSkillGroup.ClassSubjectSkillGroupMapID == toDto.SkillSetID
                                && x.markregsub.MarkRegisterSkillGroup.SubjectID == toDto.SubjectID
                                && studentIDS.Contains(x.markreg.StudentId)
                                && skllsGrpIDS.Contains(x.markregsub.SkillGroupMasterID)
                                && skllsIDS.Contains(x.markregsub.SkillMasterID))
                    .Select(x => new StudentSkillsDTO
                    {
                        MarkRegisterSkillGroupID = x.markregsub.MarkRegisterSkillGroupID,
                        MarkRegisterSkillID = x.markregsub.MarkRegisterSkillIID,
                        MarkRegisterID = x.markregsub.MarkRegisterSkillGroup.MarkRegisterID,
                        StudentID = x.markreg.StudentId.Value,
                        SkillMasterID = x.markregsub.SkillMasterID,
                        SkillGroupMasterID = x.markregsub.SkillGroupMasterID,
                        ExamID = x.markreg.ExamID,
                        AcademicYearID = x.markreg.AcademicYearID,
                        ExamGroupID = x.markreg.ExamGroupID
                    });




                _sLstMarkRegister_IDs = existMarkReg.Select(x => x.MarkRegisterID.Value).ToList();
                _sLstMarkRegisterSkilGrp_IDs = existMarkReg.Select(x => x.MarkRegisterSkillGroupID.Value).ToList();
                _sLstMarkRegisterSkill_IDs = existMarkReg.Select(x => x.MarkRegisterSkillID.Value).ToList();


                //Delete:- Skills
                if (_sLstMarkRegisterSkill_IDs.Any())
                {
                    //_sLstMarkRegisterSkill_IDs
                    //    .All(w =>
                    //    {
                    var removableSkEntities = dbContext.MarkRegisterSkills
                                         .Where(x => _sLstMarkRegisterSkill_IDs.Contains(x.MarkRegisterSkillIID));

                    if (removableSkEntities.Any())
                        dbContext.MarkRegisterSkills
                                 .RemoveRange(removableSkEntities);
                    dbContext.SaveChanges();
                    //    return true;
                    //});
                }
                //Delete:- Skills Groups
                if (_sLstMarkRegisterSkilGrp_IDs.Any())
                {
                    //_sLstMarkRegisterSkilGrp_IDs
                    //    .All(w =>
                    //    {
                    var removableSkGEntities = dbContext.MarkRegisterSkillGroups
                                         .Where(x => _sLstMarkRegisterSkilGrp_IDs.Contains(x.MarkRegisterSkillGroupIID) && x.MarkRegisterSkills.Count() == 0);

                    if (removableSkGEntities.Any())
                    {
                        dbContext.MarkRegisterSkillGroups
                                 .RemoveRange(removableSkGEntities);
                        //    return true;
                        //});
                        dbContext.SaveChanges();
                    }
                }
                //Delete:- Mark Register
                if (_sLstMarkRegister_IDs.Any())
                {
                    //_sLstMarkRegister_IDs
                    //    .All(w =>
                    //    {
                    var removableMarkRegEntities = dbContext.MarkRegisters
                                         .Where(x => _sLstMarkRegister_IDs.Contains(x.MarkRegisterIID) && x.MarkRegisterSkillGroups.Count() == 0);

                    if (removableMarkRegEntities.Any())
                    {
                        dbContext.MarkRegisters
                                 .RemoveRange(removableMarkRegEntities);
                        dbContext.SaveChanges();
                    }
                    //    return true;
                    //});

                }


                //Delete


                #endregion Delete Entities

                //Add or Modify entities
                #region Save Entities

                dbContext.MarkRegisters.AddRange(entities);
                dbContext.SaveChanges();


                #endregion Save Entities
                //dbContext.SaveChanges();
            }

            //#endregion DB Updates

            return ToDTOString(toDto);
        }


        #region Listing Student's Scholastic Internal Data 

        public List<MarkRegisterDetailsDTO> GetScholasticInternalEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            List<MarkRegisterDetailsDTO> sRetData = new List<MarkRegisterDetailsDTO>();
            int? subjectID = null; byte? subjectTypeID = null;

            var islamicSubjID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("ISLAMIC_STUDIES_SUBJID");
            var moralSubjID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("MORAL_SCIENCE_SUBJID");

            using (var sContext = new dbEduegateSchoolContext())
            {
                var dFndSkills = (from n in sContext.ClassSubjectSkillGroupSkillMaps
                                  join s in sContext.ClassSubjectSkillGroupMaps on n.ClassSubjectSkillGroupMapID equals s.ClassSubjectSkillGroupMapID
                                  where n.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID
                                  && n.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                                  && (markEntrySearchArgsDTO.SkillID == null || (n.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                                  && n.SkillMasterID == markEntrySearchArgsDTO.SkillID))
                                  select new SkillSDataDTO
                                  {
                                      SkillGroupMasterID = n.SkillGroupMasterID,
                                      SkillGroup = n.SkillGroupMaster.SkillGroup,
                                      SkillMasterID = n.SkillMasterID,
                                      Skill = n.SkillMaster.SkillName,
                                      MarkGradeID = n.MarkGradeID,
                                      ConvertionFactor = s.ConversionFactor,
                                      MinimumMarks = n.MinimumMarks,
                                      MaximumMarks = n.MaximumMarks
                                  }).Distinct().AsNoTracking();

                var subjectData = sContext.SkillGroupSubjectMaps.Where(x => x.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID).Select(x => x.Subject).AsNoTracking().FirstOrDefault();

                if (subjectData != null)
                {
                    subjectID = subjectData.SubjectID;
                    subjectTypeID = subjectData.SubjectTypeID;
                }
                if (dFndSkills.Any())
                {
                    var markGradeList = dFndSkills.Select(x => x.MarkGradeID).ToList();

                    var mgMap = (from markGrade in sContext.MarkGradeMaps
                                 where markGradeList.Contains(markGrade.MarkGradeID.Value)
                                 select new MarkGradeMapDTO()
                                 {
                                     GradeTo = markGrade.GradeTo,
                                     GradeFrom = markGrade.GradeFrom,
                                     GradeName = markGrade.GradeName + " - " + markGrade.Description,
                                     Description = markGrade.Description,
                                     MarksGradeID = markGrade.MarkGradeID,
                                     IsPercentage = markGrade.IsPercentage,
                                     MarksGradeMapIID = markGrade.MarksGradeMapIID
                                 }).AsNoTracking().ToList();

                    List<StudentDTO> studentList = (from n in sContext.Students
                                                    join a in sContext.AcademicYears on n.SchoolAcademicyearID equals a.AcademicYearID
                                                    where n.ClassID == markEntrySearchArgsDTO.ClassID && n.IsActive == true
                                                    && n.SectionID == markEntrySearchArgsDTO.SectionID && n.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID //&& a.AcademicYearStatusID != 3
                                                    && n.Status == 1
                                                    //&& n.StudentIID == 2480
                                                    orderby n.AdmissionNumber
                                                    select new StudentDTO
                                                    {
                                                        StudentIID = n.StudentIID,
                                                        StudentFullName = n.FirstName + " " + n.MiddleName + " " + n.LastName,
                                                        RollNumber = n.AdmissionNumber,
                                                        SecoundLanguageID = n.SecondLangID,
                                                        ThridLanguageID = n.ThirdLangID,
                                                        SubjectMapID = n.SubjectMapID
                                                    }).AsNoTracking().ToList();

                    List<long> studentIDS = studentList.Select(x => (x.StudentIID)).ToList();

                    var selectedSubjID = markEntrySearchArgsDTO.SubjectId;

                    var studentOptionSubList = sContext.Students
                        .AsNoTracking()
                        .Where(x => x.ClassID == markEntrySearchArgsDTO.ClassID && x.SectionID == markEntrySearchArgsDTO.SectionID && x.IsActive == true && x.Status == 1 && (x.SecondLangID == selectedSubjID || x.ThirdLangID == selectedSubjID))
                        .Select(x => x.StudentIID)
                        .ToList();

                    if (studentOptionSubList.Count() > 0)
                    {
                        var studentOptionSub = studentList.Where(x => studentOptionSubList.Contains(x.StudentIID)).ToList();
                        studentList.RemoveAll(w => !studentOptionSub.Any(x => x == w));
                    }

                    if (markEntrySearchArgsDTO.SubjectMapID == islamicSubjID || markEntrySearchArgsDTO.SubjectMapID == moralSubjID)
                    {
                        var subjectMapID = studentList.Where(x => x.SubjectMapID == markEntrySearchArgsDTO.SubjectMapID).ToList();
                        studentList.RemoveAll(w => !subjectMapID.Any(x => x == w));
                    }

                    if (markEntrySearchArgsDTO.LanguageTypeID.HasValue && markEntrySearchArgsDTO.LanguageTypeID == 2)
                    {
                        var secondLangStud = studentList.Where(x => x.SecoundLanguageID == subjectID).ToList();
                        studentList.RemoveAll(w => !secondLangStud.Any(x => x == w));
                    }
                    if (markEntrySearchArgsDTO.LanguageTypeID.HasValue && markEntrySearchArgsDTO.LanguageTypeID == 3)
                    {
                        var thirdLangStud = studentList.Where(x => x.ThridLanguageID == subjectID).ToList();
                        studentList.RemoveAll(w => !thirdLangStud.Any(x => x == w));
                    }

                    studentIDS = studentList.Select(x => (x.StudentIID)).ToList();

                    //var dfndMarkReg = (from markregsub in sContext.MarkRegisterSkills
                    //                   join markreg in sContext.MarkRegisters on markregsub.MarkRegisterSkillGroup.MarkRegisterID
                    //                   equals markreg.MarkRegisterIID
                    //                   where markreg.ClassID == markEntrySearchArgsDTO.ClassID
                    //                   //&& markreg.SectionID == markEntrySearchArgsDTO.SectionID
                    //              && markregsub.MarkRegisterSkillGroup.SubjectID == markEntrySearchArgsDTO.SubjectId
                    //              && markreg.ExamGroupID == markEntrySearchArgsDTO.TermID && markreg.ExamID == markEntrySearchArgsDTO.ExamID
                    //              && markregsub.MarkRegisterSkillGroup.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID
                    //              && ((markregsub.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                    //              && markEntrySearchArgsDTO.SkillID == null) || (markregsub.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                    //              && markregsub.SkillMasterID == markEntrySearchArgsDTO.SkillID))
                    //              && studentIDS.Contains(markreg.StudentId.Value)

                    //                   select new StudentSkillsDTO()
                    //                   {
                    //                       StudentID = markreg.StudentId.Value,
                    //                       SkillGroupMasterID = markregsub.SkillGroupMasterID,
                    //                       SkillMasterID = markregsub.SkillMasterID,
                    //                       MarksObtained = markregsub.MarksObtained,
                    //                       GradeID = markregsub.MarksGradeMapID,
                    //                       PresentStatusID = markreg.PresentStatusID,
                    //                       MarkRegisterSkillGroupID = markregsub.MarkRegisterSkillGroupID,
                    //                       MarkRegisterSkillID = markregsub.MarkRegisterSkillIID,
                    //                       MarkRegisterID = markregsub.MarkRegisterSkillGroup.MarkRegisterID
                    //                   }).AsNoTracking().ToList();




                    var dfndMarkReg = sContext.MarkRegisterSkills
                    .Join(sContext.MarkRegisters, markregsub => markregsub.MarkRegisterSkillGroup.MarkRegisterID, markreg => markreg.MarkRegisterIID,
                    (markregsub, markreg) => new { markregsub, markreg })
                    //.Include(x => x.markreg.MarkRegisterSkillGroups)
                    .Where(x => x.markreg.ClassID == markEntrySearchArgsDTO.ClassID
                                && x.markreg.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID
                                && x.markreg.ExamGroupID == markEntrySearchArgsDTO.TermID
                                && x.markreg.ExamID == markEntrySearchArgsDTO.ExamID
                                && x.markregsub.MarkRegisterSkillGroup.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID
                                && x.markregsub.MarkRegisterSkillGroup.SubjectID == markEntrySearchArgsDTO.SubjectId
                                && studentIDS.Contains(x.markreg.StudentId.Value)
                                && x.markregsub.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                                && x.markregsub.SkillMasterID == markEntrySearchArgsDTO.SkillID)
                    .Select(x => new StudentSkillsDTO
                    {
                        MarkRegisterSkillGroupID = x.markregsub.MarkRegisterSkillGroupID,
                        MarkRegisterSkillID = x.markregsub.MarkRegisterSkillIID,
                        MarkRegisterID = x.markregsub.MarkRegisterSkillGroup.MarkRegisterID,
                        StudentID = x.markreg.StudentId.Value,
                        SkillMasterID = x.markregsub.SkillMasterID,
                        SkillGroupMasterID = x.markregsub.SkillGroupMasterID,
                        ExamID = x.markreg.ExamID,
                        AcademicYearID = x.markreg.AcademicYearID,
                        ExamGroupID = x.markreg.ExamGroupID,
                        MarksObtained = x.markregsub.MarksObtained,
                        GradeID = x.markregsub.MarksGradeMapID,
                        PresentStatusID = x.markreg.PresentStatusID,
                        
                    });


                    studentList.All(w =>
                    {
                        var studMarkReg = new List<StudentSkillsDTO>();
                        if (dfndMarkReg.Any())
                            studMarkReg = dfndMarkReg.Where(x => x.StudentID == w.StudentIID).ToList();
                        sRetData.Add(GetStudentScholasticData(w.RollNumber, w.StudentIID, w.StudentFullName, markEntrySearchArgsDTO,
                                              dFndSkills.ToList(), mgMap, studMarkReg.ToList())); return true;
                    });
                }
            }


            return sRetData;
        }
        private MarkRegisterDetailsDTO GetStudentScholasticData(string EnrolNo, long StudentID, string StudentName, MarkEntrySearchArgsDTO markEntrySearchArgsDTO, List<SkillSDataDTO> skill, List<MarkGradeMapDTO> MarkGradeMap, List<StudentSkillsDTO> markRegData)
        {

            var markRegisterSplitDTO = GetScholasticIntrnalSkillGroupData(StudentID, markEntrySearchArgsDTO, skill, MarkGradeMap, markRegData);
            var sRetData = new MarkRegisterDetailsDTO()
            {
                StudentID = StudentID,
                Student = new KeyValueDTO() { Key = StudentID.ToString(), Value = StudentName },
                AdmissionNumber = EnrolNo,
                MarkRegisterSkillGroupDTO = markRegisterSplitDTO,
                MarkStatusID = markRegData.Select(x => x.PresentStatusID).FirstOrDefault()
            };

            return sRetData;
        }


        private List<MarkRegisterSkillGroupDTO> GetScholasticIntrnalSkillGroupData(long studentID, MarkEntrySearchArgsDTO markEntrySearchArgsDTO, List<SkillSDataDTO> skill, List<MarkGradeMapDTO> MarkGradeMap, List<StudentSkillsDTO> markRegData)
        {
            var sRetData = (from s in skill
                            select new MarkRegisterSkillGroupDTO
                            {
                                SkillGroupMasterID = s.SkillGroupMasterID,
                                SkillGroup = s.SkillGroup,
                                MarkRegisterSkillsDTO = (from sk in skill
                                                         select new MarkRegisterSkillsDTO
                                                         {
                                                             SkillGroupMasterID = s.SkillGroupMasterID,
                                                             SkillGroup = s.SkillGroup,
                                                             SkillMasterID = sk.SkillMasterID,
                                                             Skill = sk.Skill,
                                                             MinimumMark = sk.MaximumMarks,
                                                             MaximumMark = sk.MaximumMarks,
                                                             ConvertionFactor = sk.ConvertionFactor,
                                                             GradeMarkRangeList = GetGradeBySkillset(markEntrySearchArgsDTO.SkillSetID.Value, s.SkillGroupMasterID.Value, sk.SkillMasterID),
                                                             MarkGradeMapList = MarkGradeMap.Count > 0 ?
                                                            (from mg in MarkGradeMap
                                                             where mg.MarksGradeID == sk.MarkGradeID
                                                             select new KeyValueDTO
                                                             {
                                                                 Key = mg.MarksGradeMapIID.ToString(),
                                                                 Value = mg.GradeName
                                                             }).ToList() : null,
                                                             MarksObtained = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                 && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarksObtained).FirstOrDefault() : (decimal?)null,
                                                             MarksGradeMapID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                  && x.SkillMasterID == sk.SkillMasterID && x.GradeID != null).Select(y => y.GradeID).FirstOrDefault() : (long?)null,

                                                             MarkRegisterSkillGroupID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                      && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarkRegisterSkillGroupID).FirstOrDefault() : (long?)null,
                                                             MarkRegisterSkillIID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                     && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarkRegisterSkillID ?? 0).FirstOrDefault() : 0,

                                                             MarkRegisterID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                     && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarkRegisterID).FirstOrDefault() : (long?)null,
                                                         }).ToList()
                            }

            ).ToList();

            return sRetData;

        }

        private List<MarkGradeMapDTO> GetGradeBySkillset(long SkillSetID, int skillGroupMasterID, int? SkillMasterID)
        {
            List<MarkGradeMapDTO> mgMap = new List<MarkGradeMapDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                mgMap = (from markGrade in dbContext.MarkGradeMaps
                         join skl in dbContext.ClassSubjectSkillGroupSkillMaps on markGrade.MarkGradeID equals skl.MarkGradeID
                         join skillGrp in dbContext.ClassSubjectSkillGroupMaps on skl.ClassSubjectSkillGroupMapID equals skillGrp.ClassSubjectSkillGroupMapID
                         where (skillGrp.ClassSubjectSkillGroupMapID == SkillSetID && skl.SkillGroupMasterID == skillGroupMasterID && (SkillMasterID == 0 || skl.SkillMasterID == SkillMasterID))

                         select new MarkGradeMapDTO()
                         {
                             GradeTo = markGrade.GradeTo,
                             GradeFrom = markGrade.GradeFrom,
                             GradeName = markGrade.GradeName,
                             Description = markGrade.Description,
                             MarksGradeID = markGrade.MarkGradeID,
                             IsPercentage = markGrade.IsPercentage,
                             MarksGradeMapIID = markGrade.MarksGradeMapIID


                         }).AsNoTracking().ToList();

            }

            return mgMap;
        }

        //public List<KeyValueDTO> GetSubjectsBySkillset(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        //{
        //    var subjectList = new List<KeyValueDTO>();
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {

        //        var skillentities = dbContext.SkillGroupSubjectMaps
        //            .Select(x => new
        //            {
        //                SubjectID = x.SubjectID,
        //                Subject = x.Subject.SubjectName,
        //                ClassSubjectSkillGroupMapID = x.ClassSubjectSkillGroupMapID
        //            })
        //            .Include(i => i.Subject)
        //            .Where(X => X.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID).Distinct().AsNoTracking();


        //        subjectList = (from s in skillentities
        //                       select new KeyValueDTO
        //                       {
        //                           Key = s.SubjectID.ToString(),
        //                           Value = s.Subject
        //                       }).ToList();

        //    }

        //    return subjectList;
        //}

        public List<KeyValueDTO> GetSubjectsBySkillset(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            var subjectList = new List<KeyValueDTO>(); using (var dbContext = new dbEduegateSchoolContext())
            {
                var skillentities = dbContext.SkillGroupSubjectMaps.Where(x => x.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID).Select(x => new { SubjectID = x.SubjectID, Subject = x.Subject.SubjectName }).Distinct().AsNoTracking().ToList();

                subjectList = skillentities.Select(s => new KeyValueDTO
                {
                    Key = s.SubjectID.ToString(),
                    Value = s.Subject
                }).ToList();
            }

            return subjectList;
        }

        #endregion

        #endregion ScholasticInterna Entry

    }
}