using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class SubjectMarkEntryMapper : DTOEntityDynamicMapper
    {
        public static SubjectMarkEntryMapper Mapper(CallContext context)
        {
            var mapper = new SubjectMarkEntryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SubjectMarkEntryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long markRegSubID)
        {
            SubjectMarkEntryDTO markRegister = new SubjectMarkEntryDTO();
            var markSplit = new List<SubjectMarkEntryDetailDTO>();
            //using (var dbContext = new dbEduegateSchoolContext())
            //{
            //    var entity = dbContext.MarkRegisterSubjectMaps.Where(x => x.MarkRegisterSubjectMapIID == markRegSubID)
            //        .AsNoTracking()
            //        .FirstOrDefault();

            //var entity = dbContext.MarkRegisters.Where(x => x.MarkRegisterIID == markRegSubID)
            //    .AsNoTracking()
            //    .FirstOrDefault();

            //    markRegister = new SubjectMarkEntryDTO()
            //    {
            //        ExamID = entity.ExamID,
            //        CreatedBy = entity.CreatedBy,
            //        UpdatedBy = entity.UpdatedBy,
            //        CreatedDate = entity.CreatedDate,
            //        UpdatedDate = entity.UpdatedDate,
            //        MarkRegisterIID = entity.MarkRegisterIID,
            //        ClassID = entity.ClassID,
            //        SectionID = entity.SectionID,
            //        AcademicYearID = entity.AcademicYearID,
            //        SchoolID = entity.SchoolID,
            //        //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            //        Class = entity.Student.Class == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription },
            //        Section = entity.Student.Section == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.SectionID.ToString(), Value = entity.Section.SectionName },
            //        Exam = entity.Exam == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.Exam.ExamIID.ToString(), Value = entity.Exam.ExamDescription },
            //    };



            //    markSplit = (from examsub in dbContext.ExamSubjectMaps.AsEnumerable().Where(w => w.ExamID == entity.ExamID)
            //                 join exam in dbContext.Exams.Where(s => s.ExamIID == entity.ExamID) on examsub.ExamID equals exam.ExamIID
            //                 join examCls in dbContext.ExamClassMaps.Where(w => w.ClassID == entity.ClassID && w.SectionID == entity.SectionID) on exam.ExamIID equals examCls.ExamID
            //                 join mr in dbContext.MarkRegisters.Where(y => y.MarkRegisterIID == entity.MarkRegisterIID) on exam.ExamIID equals mr.ExamID
            //                 join marksubs in dbContext.MarkRegisterSubjectMaps on mr.MarkRegisterIID equals marksubs.MarkRegisterID                             //into tmpMarksub
            //                 //from tmpMarksubi in tmpMarksub.Where(marksubs => (marksubs == null ? 0 : marksubs.SubjectID) == examsub.SubjectID).DefaultIfEmpty()
            //                 where (exam.ExamIID == entity.ExamID && examCls.ClassID == entity.ClassID && examCls.SectionID == entity.SectionID && examsub.SubjectID == marksubs.SubjectID && examsub.SubjectID == entitysub.SubjectID)
            //                 orderby examsub.Subject.SubjectName ascending
            //                 group examsub by new { examsub.SubjectID, examsub.MinimumMarks, examsub.MaximumMarks, examsub.Subject.SubjectName, examsub.MarkGradeID, examsub.MarkGrade, marksubs } into subjectGroup

            //                 select new SubjectMarkEntryDetailDTO()
            //                 {
            //                     MarkRegisterID = entity.MarkRegisterIID,
            //                     StudentID = entity.StudentId,
            //                     StudentName = entity.Student.FirstName + ' ' + entity.Student.MiddleName + ' ' + entity.Student.LastName,
            //                     SubjectID = subjectGroup.Key.SubjectID,
            //                     MinimumMark = subjectGroup.Key.MinimumMarks,
            //                     MaximumMark = subjectGroup.Key.MaximumMarks,
            //                     Subject = subjectGroup.Key.SubjectName,
            //                     MarksGradeID = subjectGroup.Key.MarkGradeID,
            //                     Grade = (subjectGroup.Key.marksubs == null) ? "" : subjectGroup.Key.marksubs.MarkGradeMap.GradeName,
            //                     Mark = subjectGroup.Key.marksubs == null ? 0 : subjectGroup.Key.marksubs.Mark,
            //                     MarkGradeMap = (subjectGroup.Key.marksubs == null) ? "" : subjectGroup.Key.marksubs.MarkGradeMap.Description,
            //                     MarkRegisterSubjectMapIID = (subjectGroup.Key.marksubs == null) ? 0 : subjectGroup.Key.marksubs.MarkRegisterSubjectMapIID,
            //                     MarksGradeMapID = (subjectGroup.Key.marksubs == null) ? 0 : subjectGroup.Key.marksubs.MarksGradeMapID,
            //                     IsAbsent = subjectGroup.Key.marksubs == null ? false : subjectGroup.Key.marksubs.IsAbsent,
            //                     IsPassed = subjectGroup.Key.marksubs == null ? false : subjectGroup.Key.marksubs.IsPassed,
            //                     MarkRegisterSkillGroupDTO = (from classSub in dbContext.ClassSubjectSkillGroupMaps.Where(c => c.ExamID == entity.ExamID.Value && c.SubjectID == subjectGroup.Key.SubjectID)
            //                                                  join marSkillGroups in dbContext.MarkRegisterSkillGroups.Where(y => y.MarkRegisterSubjectMapID == subjectGroup.Key.marksubs.MarkRegisterSubjectMapIID) on (classSub.SkillGroupMasterID) equals
            //                                                   marSkillGroups.SkillGroupMasterID into tmpSkillGrp
            //                                                  from tmpSkillGrpi in tmpSkillGrp.DefaultIfEmpty()
            //                                                  where classSub.ClassID == entity.ClassID
            //                                                  orderby classSub.SkillGroupMaster.SkillGroup ascending
            //                                                  group classSub by new
            //                                                  {
            //                                                      classSub.ClassSubjectSkillGroupMapID,
            //                                                      classSub.SkillGroupMasterID,
            //                                                      classSub.SkillGroupMaster.SkillGroup,
            //                                                      classSub.SubjectID,
            //                                                      classSub.MinimumMarks,
            //                                                      classSub.MaximumMarks,
            //                                                      classSub.Subject.SubjectName,
            //                                                      classSub.MarkGradeID,
            //                                                      tmpSkillGrpi
            //                                                  }
            //                                                  into skillGrpGroup
            //                                                  select new MarkRegisterSkillGroupDTO()
            //                                                  {
            //                                                      SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                                      MinimumMark = skillGrpGroup.Key.MinimumMarks,
            //                                                      MaximumMark = skillGrpGroup.Key.MaximumMarks,
            //                                                      MarksGradeID = skillGrpGroup.Key.MarkGradeID,
            //                                                      SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                                      Grade = (skillGrpGroup.Key.tmpSkillGrpi == null) ? "" : skillGrpGroup.Key.tmpSkillGrpi.MarkGradeMap.GradeName,
            //                                                      MarkObtained = skillGrpGroup.Key.tmpSkillGrpi == null ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkObtained,
            //                                                      MarkGradeMap = (skillGrpGroup.Key.tmpSkillGrpi == null) ? "" : skillGrpGroup.Key.tmpSkillGrpi.MarkGradeMap.Description,
            //                                                      MarkRegisterSubjectMapID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSubjectMapID,
            //                                                      MarksGradeMapID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarksGradeMapID,
            //                                                      MarkRegisterSkillGroupIID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSkillGroupIID,
            //                                                      IsAbsent = skillGrpGroup.Key.tmpSkillGrpi == null ? false : skillGrpGroup.Key.tmpSkillGrpi.IsAbsent,
            //                                                      IsPassed = skillGrpGroup.Key.tmpSkillGrpi == null ? false : skillGrpGroup.Key.tmpSkillGrpi.IsPassed,

            //                                                      MarkRegisterSkillsDTO = (
            //                                                      from classSub in dbContext.ClassSubjectSkillGroupSkillMaps.Where(c => c.ClassSubjectSkillGroupMapID == skillGrpGroup.Key.ClassSubjectSkillGroupMapID)
            //                                                      join marSkills in dbContext.MarkRegisterSkills.Where(w => w.MarkRegisterSkillGroupID == skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSkillGroupIID) on classSub.SkillMasterID equals
            //                                                      marSkills.SkillMasterID
            //                                                      into tmpSkill
            //                                                      from tmpSkilli in tmpSkill.DefaultIfEmpty()
            //                                                      orderby classSub.SkillMaster.SkillName ascending
            //                                                      group classSub by new { classSub.SkillMasterID, classSub.SkillMaster.SkillName, classSub.MinimumMarks, classSub.MaximumMarks, classSub.MarkGradeID, tmpSkilli } into skillGroup
            //                                                      select new MarkRegisterSkillsDTO()
            //                                                      {
            //                                                          SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                                          MinimumMark = skillGroup.Key.MinimumMarks,
            //                                                          MaximumMark = skillGroup.Key.MaximumMarks,
            //                                                          MarksGradeID = skillGroup.Key.MarkGradeID,
            //                                                          SkillMasterID = skillGroup.Key.SkillMasterID,
            //                                                          SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                                          Skill = skillGroup.Key.SkillName,
            //                                                          Grade = (skillGroup.Key.tmpSkilli == null) ? "" : skillGroup.Key.tmpSkilli.MarkGradeMap.GradeName,
            //                                                          MarkGradeMap = (skillGroup.Key.tmpSkilli == null) ? "" : skillGroup.Key.tmpSkilli.MarkGradeMap.Description,
            //                                                          MarksObtained = skillGroup.Key.tmpSkilli == null ? 0 : skillGroup.Key.tmpSkilli.MarksObtained,
            //                                                          MarkRegisterSkillGroupID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarkRegisterSkillGroupID,
            //                                                          MarksGradeMapID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarksGradeMapID,
            //                                                          MarkRegisterSkillIID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarkRegisterSkillIID,
            //                                                          IsAbsent = skillGroup.Key.tmpSkilli == null ? false : skillGroup.Key.tmpSkilli.IsAbsent,
            //                                                          IsPassed = skillGroup.Key.tmpSkilli == null ? false : skillGroup.Key.tmpSkilli.IsPassed,


            //                                                      }).Distinct().ToList()
            //                                                  }).ToList()

            //                 }).ToList();


            //}

            if (markSplit.Count > 0)
            {
                markRegister.SubjectID = markSplit[0].SubjectID;
                markRegister.Subject = markSplit == null ? new KeyValueDTO() : new KeyValueDTO() { Key = markSplit[0].SubjectID.ToString(), Value = markSplit[0].Subject };
                markRegister.SubjectMarkEntryDetails = markSplit;
            }

            if (markRegister != null && markRegister.ExamID.HasValue)
            {
                markRegister.GradeList = GetGradeByExamSubjects(markRegister.ExamID.Value, markRegister.ClassID.Value, markRegister.SubjectID.Value, 1);
                markRegister.SkillGradeList = GetGradeByExamSubjects(markRegister.ExamID.Value, markRegister.ClassID.Value, markRegister.SubjectID.Value, 3); ;
                markRegister.SkillGrpGradeList = GetGradeByExamSubjects(markRegister.ExamID.Value, markRegister.ClassID.Value, markRegister.SubjectID.Value, 2); ;
            }

            return ToDTOString(markRegister);
        }



        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SubjectMarkEntryDTO;
            //MarkRegisterDTO markRegister = new MarkRegisterDTO();
            if (toDto.Class == null || toDto.Section == null || toDto.Exam == null || toDto.Subject == null)
            {
                throw new Exception("Please fill required fields!");
            }
            else if (toDto.SubjectMarkEntryDetails == null || toDto.SubjectMarkEntryDetails.Count == 0 || !toDto.SubjectMarkEntryDetails[0].StudentID.HasValue)
            {
                throw new Exception("Please fill students's mark details!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                foreach (var studentMaps in toDto.SubjectMarkEntryDetails)
                {
                    if (studentMaps.StudentID.HasValue)
                    {

                        var subjectSplit = new List<MarkRegisterSubjectMap>();

                        if ((studentMaps.Mark != 0 || (studentMaps.Mark == 0 && studentMaps.IsAbsent == true)))
                        {
                            var skillGroupSplit = new List<MarkRegisterSkillGroup>();
                            foreach (var skillGroupDto in studentMaps.MarkRegisterSkillGroupDTO)
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

                                }
                            }

                            var entityChild = new MarkRegisterSubjectMap()
                            {
                                IsPassed = studentMaps.IsPassed,
                                IsAbsent = studentMaps.IsAbsent,
                                SubjectID = studentMaps.SubjectID,
                                MarksGradeMapID = studentMaps.MarksGradeMapID,
                                MarkRegisterSubjectMapIID = studentMaps.MarkRegisterSubjectMapIID,
                                Mark = studentMaps.Mark.HasValue ? studentMaps.Mark : (decimal?)null,
                                MarkRegisterID = studentMaps.MarkRegisterID,
                                MarkRegisterSkillGroups = skillGroupSplit
                            };

                            subjectSplit.Add(entityChild);
                            if (entityChild.MarkRegisterSubjectMapIID != 0)
                            {
                                dbContext.Entry(entityChild).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

                        //Updating auto generated ID's into DTO object

                        studentMaps.MarkRegisterID = entity.MarkRegisterIID;
                        var MarkWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("MARK_ENTRY_WORKFLOW_ID", 1, null);
                        if (MarkWorkFlowID == null)
                            throw new Exception("Please set 'MARK_ENTRY_WORKFLOW_ID' in settings");
                        Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(long.Parse(MarkWorkFlowID), entity.MarkRegisterIID);

                        foreach (var markRegEntity in entity.MarkRegisterSubjectMaps)
                        {

                            foreach (var skillGrpEntity in markRegEntity.MarkRegisterSkillGroups)
                            {
                                foreach (var sklGrpjMap in studentMaps.MarkRegisterSkillGroupDTO)
                                {
                                    if (sklGrpjMap.SkillGroupMasterID == skillGrpEntity.SkillGroupMasterID)
                                    {
                                        sklGrpjMap.MarkRegisterSkillGroupIID = skillGrpEntity.MarkRegisterSkillGroupIID;
                                        sklGrpjMap.MarkRegisterSubjectMapID = markRegEntity.MarkRegisterSubjectMapIID;

                                        foreach (var skillEntity in skillGrpEntity.MarkRegisterSkills)
                                        {
                                            foreach (var sklMap in sklGrpjMap.MarkRegisterSkillsDTO)
                                            {
                                                if (sklMap.SkillMasterID == skillEntity.SkillMasterID)
                                                {
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

            return ToDTOString(new SubjectMarkEntryDTO());
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
                             //join examcls in dbContext.ExamClassMaps on exam.ExamIID equals examcls.ExamID
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
                    mgMap = (from markGrade in dbContext.MarkGradeMaps
                             join skl in dbContext.ClassSubjectSkillGroupSkillMaps on markGrade.MarkGradeID equals skl.MarkGradeID
                             join skillGrp in dbContext.ClassSubjectSkillGroupMaps on skl.ClassSubjectSkillGroupMapID equals skillGrp.ClassSubjectSkillGroupMapID
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
            }
            return mgMap;
        }

        public List<SubjectMarkEntryDetailDTO> FillClassStudents(long classID, long sectionID)
        {
            var subjectMarkDTO = new List<SubjectMarkEntryDetailDTO>();
            var subjectList = new List<MarkRegisterDetailsSplitDTO>();
            var subjecMarktList = new List<SubjectMarkEntryDetailDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //Get Student details
                var students = dbContext.Students.Where(w => w.ClassID == classID && w.SectionID == sectionID).AsNoTracking().ToList();

                foreach (Student std in students)
                {
                    subjectMarkDTO.Add(new SubjectMarkEntryDetailDTO()
                    {
                        StudentID = std.StudentIID,
                        StudentName = std.AdmissionNumber + " - " + std.FirstName + " " + std.MiddleName + " " + std.LastName,
                    });
                }

                //students.All(w =>
                //{
                //    markRegisterDetailsSplitDTO
                //}

                return subjectMarkDTO;
            }
        }
        private class TempSubSkillGrp
        {
            public int SkillGroupMasterID { get; set; }
            public long? StudentId { get; set; }
            public decimal? MinimumMark { get; set; }
            public decimal? MaximumMarks { get; set; }
            public int MarksGradeID { get; set; }
            //public int SkillGroupMasterID { get; set; }
            //public int SkillGroupMasterID { get; set; }

        }

        public List<SubjectMarkEntryDetailDTO> GetSubjectsMarkData(long examId, int classId, int sectionID, int subjectID)
        {
            List<SubjectMarkEntryDetailDTO> _sRetData = new List<SubjectMarkEntryDetailDTO>();
            List<Student> _sLstStudents = new List<Student>();
            List<ExamSubjectMap> _sLstExamSubjects = new List<ExamSubjectMap>();
            //try
            //{
            //    using (var dbContext = new dbEduegateSchoolContext())
            //    {
            //        _sLstExamSubjects = dbContext.ExamSubjectMaps.Where(w => w.SubjectID == subjectID && w.ExamID == examId).ToList();
            //        _sLstStudents = repository.Get(a => a.ClassID == classId && a.SectionID == sectionID).ToList();

            //        var dExamMaps = (from n in dbContext.ExamSubjectMaps.AsQueryable().Where(w => w.SubjectID == subjectID && w.ExamID == examId)
            //                         join m in dbContext.Exams.AsQueryable() on n.ExamID equals m.ExamIID
            //                         join k in dbContext.ExamClassMaps.AsQueryable().Where(w => w.ClassID == classId && w.SectionID == sectionID) on n.ExamID equals k.ExamID
            //                         select new { SubjectMP = n }).ToList();

            //        //var ClsSubSkillGrp = from classSub in dbContext.ClassSubjectSkillGroupMaps.Where(w => w.SubjectID == subjectID && w.ExamID == examId && w.ClassID == classId)
            //        //                     select classSub;

            //        //_sLstStudents.All(S =>
            //        //{

            //        //    return true;
            //        //});
            //        var ClsSubSkillGrp = (from classSub in dbContext.ClassSubjectSkillGroupMaps.Where(w => w.SubjectID == subjectID && w.ExamID == examId && w.ClassID == classId)
            //                              join mr in dbContext.MarkRegisterSubjectMaps.AsQueryable() on classSub.ExamID equals (mr.MarkRegister == null ? 0 : mr.MarkRegister.ExamID) into tempMarkreg
            //                              from tempMarkregi in tempMarkreg
            //                                  //.Where(mr => (mr.MarkRegister == null ? 0 : mr.MarkRegister.StudentId) == 0).DefaultIfEmpty()
            //                              join marSkillGroups in dbContext.MarkRegisterSkillGroups.AsQueryable() on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterSubjectMapIID) equals
            //                               marSkillGroups.MarkRegisterSubjectMapID into tmpSkillGrp
            //                              from tmpSkillGrpi in tmpSkillGrp.DefaultIfEmpty()
            //                              orderby classSub.SkillGroupMaster.SkillGroup ascending
            //                              group classSub by new
            //                              {
            //                                  classSub.ClassSubjectSkillGroupMapID,
            //                                  classSub.SkillGroupMasterID,
            //                                  classSub.SkillGroupMaster.SkillGroup,
            //                                  //classSub.SubjectID,
            //                                  classSub.MinimumMarks,
            //                                  classSub.MaximumMarks,
            //                                  classSub.Subject.SubjectName,
            //                                  classSub.MarkGradeID,
            //                                  GradeName = (tmpSkillGrpi == null) ? "" : tmpSkillGrpi.MarkGradeMap.GradeName,
            //                                  MarkObtained = (tmpSkillGrpi == null) ? 0 : tmpSkillGrpi.MarkObtained,
            //                                  MarkRegisterSubjectMapID = (tmpSkillGrpi == null) ? 0 : tmpSkillGrpi.MarkRegisterSubjectMapID,
            //                                  MarksGradeMapID = (tmpSkillGrpi == null) ? 0 : tmpSkillGrpi.MarksGradeMapID,
            //                                  MarkRegisterSkillGroupIID = (tmpSkillGrpi == null) ? 0 : tmpSkillGrpi.MarkRegisterSkillGroupIID,
            //                                  IsAbsent = (tmpSkillGrpi == null) ? false : tmpSkillGrpi.IsAbsent,
            //                                  IsPassed = (tmpSkillGrpi == null) ? false : tmpSkillGrpi.IsPassed,
            //                                  StudentId = (tempMarkregi == null) ? 0 : tempMarkregi.MarkRegister.StudentId,
            //                              }
            //                                                              into skillGrpGroup
            //                              select new
            //                              {
            //                                  SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                  StudentId = skillGrpGroup.Key.StudentId,
            //                                  MinimumMark = skillGrpGroup.Key.MinimumMarks,
            //                                  MaximumMark = skillGrpGroup.Key.MaximumMarks,
            //                                  MarksGradeID = skillGrpGroup.Key.MarkGradeID,
            //                                  SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                  Grade = skillGrpGroup.Key.GradeName,
            //                                  MarkObtained = skillGrpGroup.Key.MarkObtained,
            //                                  MarkRegisterSubjectMapID = skillGrpGroup.Key.MarkRegisterSubjectMapID,
            //                                  MarksGradeMapID = skillGrpGroup.Key.MarksGradeMapID,
            //                                  MarkRegisterSkillGroupIID = skillGrpGroup.Key.MarkRegisterSkillGroupIID,
            //                                  IsAbsent = skillGrpGroup.Key.IsAbsent,
            //                                  IsPassed = skillGrpGroup.Key.IsPassed,
            //                              }).ToList();
            //        //ClsSubSkillGrp.All(w =>
            //        //{

            //        //    return true;
            //        //});
            //        //var _markRegData = (from r in dbContext.MarkRegisters.Where(w => w.ClassID == classId && w.SectionID == sectionID && w.ExamID == examId && _sLstStudents.Select(s=>s.StudentIID).ToList().Contains(w.StudentId.Value))
            //        //                    join s in dbContext.MarkRegisterSubjectMaps.Where(y => y.SubjectID == subjectID) on
            //        //                    r.MarkRegisterIID equals s.MarkRegisterID
            //        //                    join g in dbContext.MarkRegisterSkillGroups on (s.MarkRegisterSubjectMapIID) equals
            //        //                    g.MarkRegisterSubjectMapID
            //        //                    join l in dbContext.MarkRegisterSkills on (g.MarkRegisterSkillGroupIID) equals
            //        //                    l.MarkRegisterSkillGroupID
            //        //                    select new { markReg = r, markRegSubMP = s, sklGrp = g, skl = l }).ToList();


            //        if (dExamMaps.Any())
            //        {
            //            dExamMaps.All(w =>
            //            {
            //                _sRetData = (from n in _sLstStudents
            //                             join m in dbContext.MarkRegisters.AsQueryable() on n.StudentIID equals m.StudentId
            //                             into tempMarkreg
            //                             from tempMarkregi in tempMarkreg.Where(mr => mr.ClassID == n.ClassID && mr.SectionID == n.SectionID).DefaultIfEmpty()
            //                             join k in dbContext.MarkRegisterSubjectMaps.AsQueryable().Where(x => x.SubjectID == subjectID) on
            //                             (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterIID) equals k.MarkRegisterID
            //                             into tmpMarksub
            //                             from tmpMarksubi in tmpMarksub.Where(marksubs => (marksubs == null ? 0 : marksubs.SubjectID) == w.SubjectMP.SubjectID).DefaultIfEmpty()
            //                             orderby n.AdmissionNumber, n.FirstName, n.MiddleName, n.LastName ascending
            //                             group n by new
            //                             {
            //                                 n.StudentIID,
            //                                 n.AdmissionNumber,
            //                                 n.FirstName,
            //                                 n.MiddleName,
            //                                 n.LastName,
            //                                 //tempMarkregi,
            //                                 w.SubjectMP.SubjectID,
            //                                 w.SubjectMP.MinimumMarks,
            //                                 w.SubjectMP.MaximumMarks,
            //                                 //w.SubjectMP.Subject.SubjectName,
            //                                 w.SubjectMP.MarkGradeID,
            //                                 GradeName = (tmpMarksubi == null) ? "" : tmpMarksubi.MarkGradeMap.GradeName,
            //                                 Mark = (tmpMarksubi == null) ? 0 : tmpMarksubi.Mark,
            //                                 MarkRegisterSubjectMapIID = (tmpMarksubi == null) ? 0 : tmpMarksubi.MarkRegisterSubjectMapIID,
            //                                 MarksGradeMapID = (tmpMarksubi == null) ? 0 : tmpMarksubi.MarksGradeMapID,
            //                                 MarkRegisterID = (tmpMarksubi == null) ? 0 : tmpMarksubi.MarkRegisterID,
            //                                 IsAbsent = (tmpMarksubi == null) ? false : tmpMarksubi.IsAbsent,
            //                                 IsPassed = (tmpMarksubi == null) ? false : tmpMarksubi.IsPassed
            //                             } into subjectGroup

            //                             select new SubjectMarkEntryDetailDTO
            //                             {
            //                                 ExamID = w.SubjectMP.ExamID,
            //                                 SubjectID = w.SubjectMP.SubjectID,
            //                                 StudentID = subjectGroup.Key.StudentIID,
            //                                 StudentName = subjectGroup.Key.AdmissionNumber + " - " + subjectGroup.Key.FirstName + " " + subjectGroup.Key.MiddleName + " " + subjectGroup.Key.LastName,
            //                                 MinimumMark = w.SubjectMP.MinimumMarks,
            //                                 MaximumMark = w.SubjectMP.MaximumMarks,
            //                                 Subject = w.SubjectMP.Subject.SubjectName,
            //                                 MarksGradeID = w.SubjectMP.MarkGradeID,
            //                                 Mark = subjectGroup.Key.Mark,
            //                                 MarkRegisterSubjectMapIID = subjectGroup.Key.MarkRegisterSubjectMapIID,
            //                                 Grade = subjectGroup.Key.GradeName,
            //                                 MarksGradeMapID = subjectGroup.Key.MarksGradeMapID,
            //                                 MarkRegisterID = subjectGroup.Key.MarkRegisterID,
            //                                 IsAbsent = subjectGroup.Key.IsAbsent,
            //                                 IsPassed = subjectGroup.Key.IsPassed,
            //                                 MarkRegisterSkillGroupDTO = (from classSub in ClsSubSkillGrp.Where(i=> i.StudentId == subjectGroup.Key.StudentIID)
            //                                                              select new MarkRegisterSkillGroupDTO()
            //                                                              {
            //                                                                  SkillGroupMasterID = classSub.SkillGroupMasterID,
            //                                                                  MinimumMark = classSub.MinimumMark,
            //                                                                  MaximumMark = classSub.MaximumMark,
            //                                                                  MarksGradeID = classSub.MarksGradeID,
            //                                                                  SkillGroup = classSub.SkillGroup,
            //                                                                  Grade = classSub.Grade,
            //                                                                  MarkObtained = classSub.MarkObtained,
            //                                                                  MarkRegisterSubjectMapID = classSub.MarkRegisterSubjectMapID,
            //                                                                  MarksGradeMapID = classSub.MarksGradeMapID,
            //                                                                  MarkRegisterSkillGroupIID = classSub.MarkRegisterSkillGroupIID,
            //                                                                  IsAbsent = classSub.IsAbsent,
            //                                                                  IsPassed = classSub.IsPassed,
            //                                                              }).ToList(),
            //                                 //MarkRegisterSkillGroupDTO = (from classSub in ClsSubSkillGrp
            //                                 //                             join mr in dbContext.MarkRegisterSubjectMaps.AsQueryable() on classSub.ExamID equals (mr.MarkRegister == null ? 0 : mr.MarkRegister.ExamID) into tempMarkreg
            //                                 //                             from tempMarkregi in tempMarkreg.Where(mr => (mr.MarkRegister == null ? 0 : mr.MarkRegister.StudentId) == subjectGroup.Key.StudentIID).DefaultIfEmpty()
            //                                 //                             join marSkillGroups in dbContext.MarkRegisterSkillGroups.AsQueryable() on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterSubjectMapIID) equals
            //                                 //                              marSkillGroups.MarkRegisterSubjectMapID into tmpSkillGrp
            //                                 //                             from tmpSkillGrpi in tmpSkillGrp.DefaultIfEmpty()
            //                                 //                             orderby classSub.SkillGroupMaster.SkillGroup ascending
            //                                 //                             group classSub by new
            //                                 //                             {
            //                                 //                                 classSub.ClassSubjectSkillGroupMapID,
            //                                 //                                 classSub.SkillGroupMasterID,
            //                                 //                                 classSub.SkillGroupMaster.SkillGroup,
            //                                 //                                 classSub.SubjectID,
            //                                 //                                 classSub.MinimumMarks,
            //                                 //                                 classSub.MaximumMarks,
            //                                 //                                 classSub.Subject.SubjectName,
            //                                 //                                 classSub.MarkGradeID,
            //                                 //                                 GradeName = (tmpSkillGrpi == null) ? "" : tmpSkillGrpi.MarkGradeMap.GradeName,
            //                                 //                                 MarkObtained = (tmpSkillGrpi == null) ? 0 : tmpSkillGrpi.MarkObtained,
            //                                 //                                 MarkRegisterSubjectMapID = (tmpSkillGrpi == null) ? 0 : tmpSkillGrpi.MarkRegisterSubjectMapID,
            //                                 //                                 MarksGradeMapID = (tmpSkillGrpi == null) ? 0 : tmpSkillGrpi.MarksGradeMapID,
            //                                 //                                 MarkRegisterSkillGroupIID = (tmpSkillGrpi == null) ? 0 : tmpSkillGrpi.MarkRegisterSkillGroupIID,
            //                                 //                                 IsAbsent = (tmpSkillGrpi == null) ? false : tmpSkillGrpi.IsAbsent,
            //                                 //                                 IsPassed = (tmpSkillGrpi == null) ? false : tmpSkillGrpi.IsPassed
            //                                 //                             }
            //                                 //                             into skillGrpGroup
            //                                 //                             select new MarkRegisterSkillGroupDTO()
            //                                 //                             {
            //                                 //                                 SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                 //                                 MinimumMark = skillGrpGroup.Key.MinimumMarks,
            //                                 //                                 MaximumMark = skillGrpGroup.Key.MaximumMarks,
            //                                 //                                 MarksGradeID = skillGrpGroup.Key.MarkGradeID,
            //                                 //                                 SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                 //                                 Grade = skillGrpGroup.Key.GradeName,
            //                                 //                                 MarkObtained = skillGrpGroup.Key.MarkObtained,
            //                                 //                                 MarkRegisterSubjectMapID = skillGrpGroup.Key.MarkRegisterSubjectMapID,
            //                                 //                                 MarksGradeMapID = skillGrpGroup.Key.MarksGradeMapID,
            //                                 //                                 MarkRegisterSkillGroupIID = skillGrpGroup.Key.MarkRegisterSkillGroupIID,
            //                                 //                                 IsAbsent = skillGrpGroup.Key.IsAbsent,
            //                                 //                                 IsPassed = skillGrpGroup.Key.IsPassed,
            //                                 //                                 MarkRegisterSkillsDTO = (
            //                                 //                        from classSub in dbContext.ClassSubjectSkillGroupSkillMaps.AsQueryable()
            //                                 //                        join mr in dbContext.MarkRegisterSkillGroups.AsQueryable() on classSub.ClassSubjectSkillGroupMap.ExamID equals (mr.MarkRegisterSubjectMap == null ? 0 : mr.MarkRegisterSubjectMap.MarkRegister.ExamID) into tempMarkreg
            //                                 //                        from tempMarkregi in tempMarkreg.Where(mr => (mr.MarkRegisterSubjectMap == null ? 0 : mr.MarkRegisterSubjectMap.MarkRegister.StudentId) == subjectGroup.Key.StudentIID).DefaultIfEmpty()
            //                                 //                        join marSkills in dbContext.MarkRegisterSkills.AsQueryable() on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterSkillGroupIID) equals
            //                                 //                         marSkills.MarkRegisterSkillGroupID into tmpSkills
            //                                 //                        from tmpSkilli in tmpSkills.Where(marSkills => (marSkills == null ? 0 : marSkills.SkillMasterID) == classSub.SkillMasterID).DefaultIfEmpty()
            //                                 //                        where classSub.ClassSubjectSkillGroupMap.ClassID == classId && classSub.ClassSubjectSkillGroupMap.ExamID == examId && classSub.ClassSubjectSkillGroupMap.SubjectID == skillGrpGroup.Key.SubjectID &&
            //                                 //                        classSub.ClassSubjectSkillGroupMap.ClassSubjectSkillGroupMapID == skillGrpGroup.Key.ClassSubjectSkillGroupMapID
            //                                 //                        orderby classSub.SkillMaster.SkillName ascending
            //                                 //                        group classSub by new
            //                                 //                        {
            //                                 //                            classSub.SkillMasterID,
            //                                 //                            classSub.SkillMaster.SkillName,
            //                                 //                            classSub.MinimumMarks,
            //                                 //                            classSub.MaximumMarks,
            //                                 //                            classSub.MarkGradeID,
            //                                 //                            GradeName = (tmpSkilli == null) ? "" : tmpSkilli.MarkGradeMap.GradeName,
            //                                 //                            Description = (tmpSkilli == null) ? "" : tmpSkilli.MarkGradeMap.Description,
            //                                 //                            MarksObtained = (tmpSkilli == null) ? 0 : tmpSkilli.MarksObtained,
            //                                 //                            MarkRegisterSkillGroupID = (tmpSkilli == null) ? 0 : tmpSkilli.MarkRegisterSkillGroupID,
            //                                 //                            MarksGradeMapID = (tmpSkilli == null) ? 0 : tmpSkilli.MarksGradeMapID,
            //                                 //                            MarkRegisterSkillIID = (tmpSkilli == null) ? 0 : tmpSkilli.MarkRegisterSkillIID,
            //                                 //                            IsAbsent = (tmpSkilli == null) ? false : tmpSkilli.IsAbsent,
            //                                 //                            IsPassed = (tmpSkilli == null) ? false : tmpSkilli.IsPassed
            //                                 //                        } into skillGroup
            //                                 //                        select new MarkRegisterSkillsDTO()
            //                                 //                        {
            //                                 //                            SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                 //                            MinimumMark = skillGroup.Key.MinimumMarks,
            //                                 //                            MaximumMark = skillGroup.Key.MaximumMarks,
            //                                 //                            MarksGradeID = skillGroup.Key.MarkGradeID,
            //                                 //                            SkillMasterID = skillGroup.Key.SkillMasterID,
            //                                 //                            SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                 //                            Skill = skillGroup.Key.SkillName,
            //                                 //                            Grade = skillGroup.Key.GradeName,
            //                                 //                            MarkGradeMap = skillGroup.Key.Description,
            //                                 //                            MarksObtained = skillGroup.Key.MarksObtained,
            //                                 //                            MarkRegisterSkillGroupID = skillGroup.Key.MarkRegisterSkillGroupID,
            //                                 //                            MarksGradeMapID = skillGroup.Key.MarksGradeMapID,
            //                                 //                            MarkRegisterSkillIID = skillGroup.Key.MarkRegisterSkillIID,
            //                                 //                            IsAbsent = skillGroup.Key.IsAbsent,
            //                                 //                            IsPassed = skillGroup.Key.IsPassed,


            //                                 //                        }).ToList()
            //                                 //                             }).ToList()
            //                             }).ToList();

            //                return true;
            //            });
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            return _sRetData;
        }

        public SubjectMarkEntryDetailDTO GetSubjectsMarkDataTemp(long examId, int classId, int sectionID, int subjectID, Student student)
        {
            SubjectMarkEntryDetailDTO _sRetData = new SubjectMarkEntryDetailDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dFnd = from n in dbContext.ExamSubjectMaps.Where(w => w.ExamID == examId && w.SubjectID == subjectID).AsNoTracking()
                           select new SubjectMarkEntryDetailDTO
                           {
                               SubjectID = n.SubjectID,
                               ExamID = n.ExamID,
                           };
                if (dFnd.Any())
                    _sRetData = dFnd.FirstOrDefault();

            }
            return _sRetData;
        }

        public List<SubjectMarkEntryDetailDTO> GetSubjectsMarkData(long examId, int classId, int sectionID, int subjectID, Student student)
        {
            var subjectList = new List<SubjectMarkEntryDetailDTO>();
            var subjectDto = new SubjectMarkEntryDetailDTO();
            var subjecMarktList = new List<SubjectMarkEntryDetailDTO>();

            //using (var dbContext = new dbEduegateSchoolContext())
            //{
            //var students = dbContext.Students.Where(a => a.ClassID == classId && a.SectionID == sectionID)
            //        .AsNoTracking()
            //        .ToList();
            //    //foreach(var student in students)
            //    //{
            //    var dFnd = (from examsub in dbContext.ExamSubjectMaps.AsQueryable().Where(w => w.SubjectID == subjectID && w.ExamID == examId)
            //                join exam in dbContext.Exams on examsub.ExamID equals exam.ExamIID
            //                join examCls in dbContext.ExamClassMaps.Where(w => w.ClassID == classId && w.SectionID == sectionID) on exam.ExamIID equals examCls.ExamID
            //                join mr in dbContext.MarkRegisters.Where(w => w.StudentId == student.StudentIID && w.ExamID == examId) on exam.ExamIID equals mr.ExamID
            //                into tempMarkreg
            //                from tempMarkregi in tempMarkreg.Where(mr => mr.ClassID == examCls.ClassID && mr.SectionID == examCls.SectionID).DefaultIfEmpty()
            //                join marksubs in dbContext.MarkRegisterSubjectMaps on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterIID) equals marksubs.MarkRegisterID into tmpMarksub
            //                from tmpMarksubi in tmpMarksub.Where(marksubs => (marksubs == null ? 0 : marksubs.SubjectID) == examsub.SubjectID).DefaultIfEmpty()
            //                where (exam.ExamIID == examId && examsub.SubjectID == subjectID && examCls.ClassID == classId && examCls.SectionID == sectionID)
            //                //&& tempMarkregi.StudentId== student.StudentIID
            //                //orderby examsub.Subject.SubjectName ascending
            //                group examsub by new { tempMarkregi, examsub.SubjectID, examsub.MinimumMarks, examsub.MaximumMarks, examsub.Subject.SubjectName, examsub.MarkGradeID, tmpMarksubi } into subjectGroup
            //                select new SubjectMarkEntryDetailDTO()
            //                {
            //                    StudentID = (subjectGroup.Key.tmpMarksubi == null) ? student.StudentIID : subjectGroup.Key.tmpMarksubi.MarkRegisterSubjectMapIID,
            //                    StudentName = student.AdmissionNumber + " - " + student.FirstName + " " + student.MiddleName + " " + student.LastName,
            //                    SubjectID = subjectGroup.Key.SubjectID,
            //                    MinimumMark = subjectGroup.Key.MinimumMarks,
            //                    MaximumMark = subjectGroup.Key.MaximumMarks,
            //                    Subject = subjectGroup.Key.SubjectName,
            //                    MarksGradeID = subjectGroup.Key.MarkGradeID,
            //                    Mark = subjectGroup.Key.tmpMarksubi == null ? 0 : subjectGroup.Key.tmpMarksubi.Mark,
            //                    MarkRegisterSubjectMapIID = (subjectGroup.Key.tmpMarksubi == null) ? 0 : subjectGroup.Key.tmpMarksubi.MarkRegisterSubjectMapIID,
            //                    MarksGradeMapID = (subjectGroup.Key.tmpMarksubi == null) ? 0 : subjectGroup.Key.tmpMarksubi.MarksGradeMapID,
            //                    MarkRegisterID = (subjectGroup.Key.tmpMarksubi == null) ? 0 : subjectGroup.Key.tmpMarksubi.MarkRegisterID,
            //                    IsAbsent = subjectGroup.Key.tmpMarksubi == null ? false : subjectGroup.Key.tmpMarksubi.IsAbsent,
            //                    IsPassed = subjectGroup.Key.tmpMarksubi == null ? false : subjectGroup.Key.tmpMarksubi.IsPassed,

            //                    MarkRegisterSkillGroupDTO = (from classSub in dbContext.ClassSubjectSkillGroupMaps
            //                                                 join mr in dbContext.MarkRegisterSubjectMaps on classSub.ExamID equals (mr.MarkRegister == null ? 0 : mr.MarkRegister.ExamID) into tempMarkreg
            //                                                 from tempMarkregi in tempMarkreg.Where(mr => (mr.MarkRegister == null ? 0 : mr.MarkRegister.StudentId) == student.StudentIID).DefaultIfEmpty()
            //                                                 join marSkillGroups in dbContext.MarkRegisterSkillGroups on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterSubjectMapIID) equals
            //                                                  marSkillGroups.MarkRegisterSubjectMapID into tmpSkillGrp
            //                                                 from tmpSkillGrpi in tmpSkillGrp.DefaultIfEmpty()
            //                                                 where classSub.ClassID == classId && classSub.ExamID == examId && classSub.SubjectID == subjectGroup.Key.SubjectID
            //                                                 orderby classSub.SkillGroupMaster.SkillGroup ascending
            //                                                 group classSub by new
            //                                                 {
            //                                                     classSub.ClassSubjectSkillGroupMapID,
            //                                                     classSub.SkillGroupMasterID,
            //                                                     classSub.SkillGroupMaster.SkillGroup,
            //                                                     classSub.SubjectID,
            //                                                     classSub.MinimumMarks,
            //                                                     classSub.MaximumMarks,
            //                                                     classSub.Subject.SubjectName,
            //                                                     classSub.MarkGradeID,
            //                                                     tmpSkillGrpi
            //                                                 }
            //                                                 into skillGrpGroup
            //                                                 select new MarkRegisterSkillGroupDTO()
            //                                                 {
            //                                                     SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                                     MinimumMark = skillGrpGroup.Key.MinimumMarks,
            //                                                     MaximumMark = skillGrpGroup.Key.MaximumMarks,
            //                                                     MarksGradeID = skillGrpGroup.Key.MarkGradeID,
            //                                                     SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                                     MarkGradeMap = (skillGrpGroup.Key.tmpSkillGrpi == null) ? "" : skillGrpGroup.Key.tmpSkillGrpi.MarkGradeMap.Description,
            //                                                     MarkObtained = skillGrpGroup.Key.tmpSkillGrpi == null ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkObtained,
            //                                                     MarkRegisterSubjectMapID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSubjectMapID,
            //                                                     MarksGradeMapID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarksGradeMapID,
            //                                                     MarkRegisterSkillGroupIID = (skillGrpGroup.Key.tmpSkillGrpi == null) ? 0 : skillGrpGroup.Key.tmpSkillGrpi.MarkRegisterSkillGroupIID,
            //                                                     IsAbsent = skillGrpGroup.Key.tmpSkillGrpi == null ? false : skillGrpGroup.Key.tmpSkillGrpi.IsAbsent,
            //                                                     IsPassed = skillGrpGroup.Key.tmpSkillGrpi == null ? false : skillGrpGroup.Key.tmpSkillGrpi.IsPassed,
            //                                                     MarkRegisterSkillsDTO = (
            //                                                     from classSub in dbContext.ClassSubjectSkillGroupSkillMaps
            //                                                     join mr in dbContext.MarkRegisterSkillGroups on classSub.ClassSubjectSkillGroupMap.ExamID equals (mr.MarkRegisterSubjectMap == null ? 0 : mr.MarkRegisterSubjectMap.MarkRegister.ExamID) into tempMarkreg
            //                                                     from tempMarkregi in tempMarkreg.Where(mr => (mr.MarkRegisterSubjectMap == null ? 0 : mr.MarkRegisterSubjectMap.MarkRegister.StudentId) == student.StudentIID).DefaultIfEmpty()
            //                                                     join marSkills in dbContext.MarkRegisterSkills on (tempMarkregi == null ? 0 : tempMarkregi.MarkRegisterSkillGroupIID) equals
            //                                                      marSkills.MarkRegisterSkillGroupID into tmpSkills
            //                                                     from tmpSkilli in tmpSkills.Where(marSkills => (marSkills == null ? 0 : marSkills.SkillMasterID) == classSub.SkillMasterID).DefaultIfEmpty()
            //                                                     where classSub.ClassSubjectSkillGroupMap.ClassID == classId && classSub.ClassSubjectSkillGroupMap.ExamID == examId && classSub.ClassSubjectSkillGroupMap.SubjectID == skillGrpGroup.Key.SubjectID &&
            //                                                     classSub.ClassSubjectSkillGroupMap.ClassSubjectSkillGroupMapID == skillGrpGroup.Key.ClassSubjectSkillGroupMapID
            //                                                     orderby classSub.SkillMaster.SkillName ascending
            //                                                     group classSub by new { classSub.SkillMasterID, classSub.SkillMaster.SkillName, classSub.MinimumMarks, classSub.MaximumMarks, classSub.MarkGradeID, tmpSkilli } into skillGroup
            //                                                     select new MarkRegisterSkillsDTO()
            //                                                     {
            //                                                         SkillGroupMasterID = skillGrpGroup.Key.SkillGroupMasterID,
            //                                                         MinimumMark = skillGroup.Key.MinimumMarks,
            //                                                         MaximumMark = skillGroup.Key.MaximumMarks,
            //                                                         MarksGradeID = skillGroup.Key.MarkGradeID,
            //                                                         SkillMasterID = skillGroup.Key.SkillMasterID,
            //                                                         SkillGroup = skillGrpGroup.Key.SkillGroup,
            //                                                         Skill = skillGroup.Key.SkillName,
            //                                                         MarkGradeMap = (skillGroup.Key.tmpSkilli == null) ? "" : skillGroup.Key.tmpSkilli.MarkGradeMap.Description,
            //                                                         MarksObtained = skillGroup.Key.tmpSkilli == null ? 0 : skillGroup.Key.tmpSkilli.MarksObtained,
            //                                                         MarkRegisterSkillGroupID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarkRegisterSkillGroupID,
            //                                                         MarksGradeMapID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarksGradeMapID,
            //                                                         MarkRegisterSkillIID = (skillGroup.Key.tmpSkilli == null) ? 0 : skillGroup.Key.tmpSkilli.MarkRegisterSkillIID,
            //                                                         IsAbsent = skillGroup.Key.tmpSkilli == null ? false : skillGroup.Key.tmpSkilli.IsAbsent,
            //                                                         IsPassed = skillGroup.Key.tmpSkilli == null ? false : skillGroup.Key.tmpSkilli.IsPassed,


            //                                                     }).ToList()
            //                                                 }).ToList()

            //                });//.LastOrDefault();
            //    if (dFnd.Any())

            //        subjectList.Add(dFnd.FirstOrDefault());
            //    //}
            //}
            return subjectList;
        }

    }
}