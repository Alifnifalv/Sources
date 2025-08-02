using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class StudentSkillRegisterMapper : DTOEntityDynamicMapper
    {
        public static StudentSkillRegisterMapper Mapper(CallContext context)
        {
            var mapper = new StudentSkillRegisterMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentSkillRegisterDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            StudentSkillRegisterDTO skillRegister = new StudentSkillRegisterDTO();
            //using (var dbContext = new dbEduegateSchoolContext())
            //{
            //    var entity = dbContext.StudentSkillRegisters.Where(x => x.StudentSkillRegisterIID == IID)
            //        .Include(i => i.Class)
            //        .Include(i => i.Exam)
            //        .AsNoTracking()
            //        .FirstOrDefault();

            //    skillRegister = new StudentSkillRegisterDTO()
            //    {
            //        StudentSkillRegisterIID = entity.StudentSkillRegisterIID,
            //        ExamID = entity.ExamID,
            //        ClassID = entity.ClassID,
            //        SubjectID = entity.SubjectID,
            //        StudentId = entity.StudentId,
            //        SectionID = entity.SectionID,
            //        IsAbsent = entity.IsAbsent,
            //        MarksGradeMapID = entity.MarksGradeMapID,
            //        IsPassed = entity.IsPassed,
            //        Mark = entity.Mark,
            //        CreatedBy = entity.CreatedBy,
            //        UpdatedBy = entity.UpdatedBy,
            //        CreatedDate = entity.CreatedDate,
            //        UpdatedDate = entity.UpdatedDate,
            //        //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            //        Student = entity.Student == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.StudentId.ToString(), Value = entity.Student.FirstName + " " + entity.Student.MiddleName + " " + entity.Student.LastName },
            //        Class = entity.Class == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription },
            //        Section = entity.Section == null ? null : entity.Section.SectionName,
            //        Exam = entity.Exam == null ? null : entity.Exam.ExamDescription,
            //        Subject = entity.Subject == null ? null : entity.Subject.SubjectName,
            //    };
            //    var skillSplit = new List<StudentSkillRegisterSplitDTO>();
            //    foreach (var skillregsplit in entity.StudentSkillRegisterMaps)
            //    {
            //        if (skillregsplit.SkillMasterID != 0 && skillregsplit.Mark.HasValue)
            //        {
            //            var grademap = (from markGrade in dbContext.MarkGradeMaps.AsQueryable()
            //                            join skillmap in dbContext.StudentSkillRegisterMaps on markGrade.MarksGradeMapIID equals skillmap.MarksGradeMapID
            //                            join clsskill in dbContext.ClassSubjectSkillGroupSkillMaps on skillmap.SkillMasterID equals clsskill.SkillMasterID
            //                            where (skillmap.MarksGradeMapID == skillregsplit.MarksGradeMapID)
            //                            select new StudentSkillRegisterSplitDTO()
            //                            {
            //                                MaximumMarks = clsskill.MaximumMarks,
            //                                MinimumMarks = clsskill.MinimumMarks,
            //                                MarksGradeMapID = markGrade.MarksGradeMapIID,

            //                            }).FirstOrDefault();
            //            var det = new StudentSkillRegisterSplitDTO()
            //            {
            //                StudentSkillRegisterMapIID = skillregsplit.StudentSkillRegisterMapIID,
            //                Mark = skillregsplit.Mark,
            //                MaximumMarks = grademap.MaximumMarks,
            //                MinimumMarks = grademap.MinimumMarks,
            //                MarkGradeID = Convert.ToInt32(grademap.MarkGradeID),
            //                MarksGradeMapID = skillregsplit.MarksGradeMapID,
            //            };
            //            skillSplit.Add(det);
            //        }
            //    }
            //    skillRegister.StudentSkillRegisterMap = new List<StudentSkillRegisterMapDTO>();
            //    if (entity.StudentId != 0)
            //    {
            //        skillRegister.StudentSkillRegisterMap.Add(new StudentSkillRegisterMapDTO()
            //        {
            //            StudentSkillRegisterSkillMapDTO = skillSplit,
            //            StudentSkillRegisterID = entity.StudentSkillRegisterIID,
            //        });
            //    }
            //}
            return ToDTOString(skillRegister);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentSkillRegisterDTO;

            //convert the dto to entity and pass to the repository.
            //using (var dbContext = new dbEduegateSchoolContext())
            //{
            //    foreach (var skillMaps in toDto.StudentSkillRegisterMap)
            //    {
            //        if (skillMaps.SkillGroupMasterID != 0)
            //        {
            //            var skillSplit = new List<StudentSkillRegisterMap>();
            //            foreach (var skillSplitDto in skillMaps.StudentSkillRegisterSkillMapDTO)
            //            {
            //                if (skillSplitDto.SkillMasterID != 0 && skillSplitDto.Mark.HasValue)
            //                {
            //                    var entityChild = new StudentSkillRegisterMap()
            //                    {
            //                        StudentSkillRegisterID = skillSplitDto.StudentSkillRegisterID,
            //                        StudentSkillRegisterMapIID = skillSplitDto.StudentSkillRegisterMapIID,
            //                        SkillMasterID = skillSplitDto.SkillMasterID,
            //                        MarksGradeMapID = skillSplitDto.MarksGradeMapID,
            //                        Mark = skillSplitDto.Mark.HasValue ? skillSplitDto.Mark : (decimal?)null,
            //                        CreatedBy = skillSplitDto.StudentSkillRegisterMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
            //                        UpdatedBy = skillSplitDto.StudentSkillRegisterMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
            //                        CreatedDate = skillSplitDto.StudentSkillRegisterMapIID == 0 ? DateTime.Now : dto.CreatedDate,
            //                        UpdatedDate = skillSplitDto.StudentSkillRegisterMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
            //                        TimeStamps = skillSplitDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            //                    };

            //                    skillSplit.Add(entityChild);
            //                    if (entityChild.StudentSkillRegisterMapIID != 0)
            //                    {
            //                        dbContext.Entry(entityChild).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //                    }
            //                    else
            //                    {
            //                        dbContext.Entry(entityChild).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            //                    }
            //                }

            //            }
            //            var isSkillExist = dbContext.SkillGroupMasters.AsQueryable().Where(x =>
            //                 x.SkillGroupMasterID == skillMaps.SkillGroupMasterID).FirstOrDefault();
            //            var entity = new StudentSkillRegister()
            //            {
            //                StudentSkillRegisterIID = toDto.StudentSkillRegisterIID,
            //                ExamID = toDto.ExamID,
            //                StudentSkillRegisterMaps = skillSplit,
            //                ClassID = toDto.ClassID,
            //                SubjectID = toDto.SubjectID,
            //                StudentId = toDto.StudentId,
            //                SectionID = toDto.SectionID,
            //                IsAbsent = toDto.IsAbsent,
            //                MarksGradeMapID = toDto.MarksGradeMapID,
            //                IsPassed = toDto.IsPassed,
            //                Mark = toDto.Mark,
            //                CreatedBy = toDto.StudentSkillRegisterIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
            //                UpdatedBy = toDto.StudentSkillRegisterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
            //                CreatedDate = toDto.StudentSkillRegisterIID == 0 ? DateTime.Now : dto.CreatedDate,
            //                UpdatedDate = toDto.StudentSkillRegisterIID > 0 ? DateTime.Now : dto.UpdatedDate,
            //                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            //            };
            //            dbContext.StudentSkillRegisters.Add(entity);
            //            if (entity.StudentSkillRegisterIID == 0)
            //            {
            //                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            //            }
            //            else
            //            {
            //                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //            }
            //            dbContext.SaveChanges();
            //        }

            //    }

            //}

            return ToDTOString(toDto);
        }

        public List<StudentSkillRegisterSplitDTO> GetStudentSkills(long skillId)
        {
            var skillList = new List<StudentSkillRegisterSplitDTO>();
            var subjecMarktList = new List<StudentSkillRegisterSplitDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                skillList = (from subjskill in dbContext.ClassSubjectSkillGroupSkillMaps
                             join skillmstr in dbContext.SkillMasters on subjskill.SkillMasterID equals skillmstr.SkillMasterID
                             join grade in dbContext.MarkGrades on subjskill.MarkGradeID equals grade.MarkGradeIID
                             where (subjskill.SkillMasterID == skillId)
                             orderby subjskill.ClassSubjectSkillGroupSkillMapID descending
                             select new StudentSkillRegisterSplitDTO()
                             {
                                 SkillMasterID = subjskill.SkillMasterID,
                                 MinimumMarks = subjskill.MinimumMarks,
                                 MaximumMarks = subjskill.MaximumMarks,
                                 MarkGradeID = subjskill.MarkGradeID,
                                 SubSkill = subjskill.SkillMaster.SkillName,
                                 MarkGrade = subjskill.MarkGrade.Description,
                             }).AsNoTracking().ToList();

                foreach (var dto in skillList)
                {
                    var det = (from grademap in dbContext.MarkGradeMaps
                               join grade in dbContext.MarkGrades on grademap.MarkGradeID equals grade.MarkGradeIID
                               where (grademap.MarkGradeID == dto.MarkGradeID)
                               select grademap).AsNoTracking().FirstOrDefault();

                    if (det != null)
                    {
                        dto.MarksGradeMapID = det.MarksGradeMapIID;
                        dto.MarkGrade = det.GradeName;
                    }
                    subjecMarktList.Add(dto);
                }
            }

            return skillList;
        }


        //public ProgressReportDTO GetStudentSkillRegister(long studentId, int ClassID)
        //{

        //    var progressReportData = new ProgressReportDTO();
        //    try
        //    {
        //        var subjectList = new List<ProgressReportSubjectListDTO>();
        //        var examList = new List<ProgressReportExamListDTO>();
        //        using (var dbContext = new dbEduegateSchoolContext())
        //        {

        //            examList = (from classSub in dbContext.ClassSubjectSkillGroupMaps.AsEnumerable()
        //                        where classSub.ClassID == ClassID
        //                        group classSub by new
        //                        {
        //                            classSub.ExamID,
        //                            classSub.Exam.ExamDescription
        //                        } into classExamgrp
        //                        select new ProgressReportExamListDTO()
        //                        {
        //                            ExamID = classExamgrp.Key.ExamID,
        //                            ExamName = classExamgrp.Key.ExamDescription

        //                        }).ToList();

        //            subjectList = (from classSub in dbContext.ClassSubjectSkillGroupMaps
        //                           where (classSub.ClassID == ClassID)
        //                           group classSub by new
        //                           {
        //                               classSub.SubjectID,
        //                               classSub.Subject.SubjectName
        //                           } into classsubjgrp
        //                           select new ProgressReportSubjectListDTO()
        //                           {
        //                               SubjectID = classsubjgrp.Key.SubjectID,
        //                               SubjectName = classsubjgrp.Key.SubjectName,

        //                               Exams = (from studSkillReg in dbContext.StudentSkillRegisters
        //                                        join examsub2 in dbContext.ExamSubjectMaps on
        //                                        new { exam_id = studSkillReg.ExamID, Subject_ID = studSkillReg.SubjectID } equals
        //                                        new { exam_id = examsub2.ExamID, Subject_ID = examsub2.SubjectID }

        //                                        where studSkillReg.ClassID == ClassID && examsub2.SubjectID == classsubjgrp.Key.SubjectID && studSkillReg.StudentId == studentId

        //                                        group examsub2 by new { studSkillReg.StudentSkillRegisterIID, examsub2.Exam, examsub2.MinimumMarks, examsub2.MaximumMarks, studSkillReg.MarkObtained, studSkillReg.MarkGradeMap } into subExamGroup
        //                                        select new ProgressReportExamListDTO()
        //                                        {
        //                                            ExamID = subExamGroup.Key.Exam.ExamIID,
        //                                            MaximumMarks = subExamGroup.Key.MaximumMarks,
        //                                            MinimumMarks = subExamGroup.Key.MinimumMarks,
        //                                            Mark = subExamGroup.Key.MarkObtained,
        //                                            Grade = subExamGroup.Key.MarkGradeMap.GradeName

        //                                        }).ToList(),
        //                               SkillGroups = (from skillGroupMap in dbContext.ClassSubjectSkillGroupMaps

        //                                              where skillGroupMap.SubjectID == classsubjgrp.Key.SubjectID &&
        //                                              skillGroupMap.ClassID == ClassID
        //                                              group skillGroupMap by new
        //                                              {
        //                                                  skillGroupMap.SkillGroupMaster.SkillGroup,
        //                                                  skillGroupMap.SkillGroupMasterID
        //                                              } into skillgrpGroup

        //                                              select new ProgressReportSkillGroupListDTO()
        //                                              {
        //                                                  SkillGroupID = skillgrpGroup.Key.SkillGroupMasterID,
        //                                                  SkillGroupName = skillgrpGroup.Key.SkillGroup,
        //                                                  Exams = (from skillGroupMap in dbContext.StudentSkillGroupMaps
        //                                                           join studSkillReg in dbContext.StudentSkillRegisters on
        //                                                           skillGroupMap.StudentSkillRegisterID equals studSkillReg.StudentSkillRegisterIID
        //                                                           where skillGroupMap.SkillGroupMasterID == skillgrpGroup.Key.SkillGroupMasterID &&
        //                                                           studSkillReg.ClassID == ClassID && studSkillReg.SubjectID == classsubjgrp.Key.SubjectID && studSkillReg.StudentId == studentId
        //                                                           group skillGroupMap by new
        //                                                           {

        //                                                               studSkillReg.ExamID,
        //                                                               skillGroupMap.MarkGradeMap,
        //                                                               skillGroupMap.MinimumMark,
        //                                                               skillGroupMap.MaximumMark,
        //                                                               skillGroupMap.MarkObtained
        //                                                           } into skillGroupExamGroup
        //                                                           select new ProgressReportExamListDTO()
        //                                                           {
        //                                                               ExamID = skillGroupExamGroup.Key.ExamID,
        //                                                               MaximumMarks = skillGroupExamGroup.Key.MaximumMark,
        //                                                               MinimumMarks = skillGroupExamGroup.Key.MinimumMark,
        //                                                               Mark = skillGroupExamGroup.Key.MarkObtained,
        //                                                               Grade = skillGroupExamGroup.Key.MarkGradeMap.GradeName

        //                                                           }).ToList(),
        //                                                  Skills =
        //                                                  (from skill in dbContext.ClassSubjectSkillGroupSkillMaps
        //                                                   join skillGroupMap in dbContext.ClassSubjectSkillGroupMaps
        //                                                    on
        //                                                           skill.ClassSubjectSkillGroupMapID equals skillGroupMap.ClassSubjectSkillGroupMapID
        //                                                   where skillGroupMap.SubjectID == classsubjgrp.Key.SubjectID &&
        //                                                   skillGroupMap.ClassID == ClassID

        //                                                   group skill by new
        //                                                   {
        //                                                       skill.SkillMaster.SkillMasterID,
        //                                                       skill.SkillMaster.SkillName,
        //                                                   } into skillGroup
        //                                                   select new ProgressReportSkillsListDTO()
        //                                                   {
        //                                                       SkillID = skillGroup.Key.SkillMasterID,
        //                                                       SkillName = skillGroup.Key.SkillName,
        //                                                       Exams = (from skillMap in dbContext.StudentSkillMasterMaps
        //                                                                join skillGroupMap in dbContext.StudentSkillGroupMaps on
        //                                                                       skillMap.StudentSkillGroupMapsID equals skillGroupMap.StudentSkillGroupMapsIID
        //                                                                join studSkillReg in dbContext.StudentSkillRegisters on
        //                                                                       skillGroupMap.StudentSkillRegisterID equals studSkillReg.StudentSkillRegisterIID
        //                                                                where skillGroupMap.SkillGroupMasterID == skillgrpGroup.Key.SkillGroupMasterID && skillMap.SkillMasterID== skillGroup.Key.SkillMasterID &&
        //                                                                studSkillReg.ClassID == ClassID && studSkillReg.SubjectID == classsubjgrp.Key.SubjectID && studSkillReg.StudentId == studentId

        //                                                                group skillMap by new
        //                                                                {
        //                                                                    studSkillReg.ExamID,
        //                                                                    skillMap.MarkGradeMap,
        //                                                                    skillMap.MinimumMark,
        //                                                                    skillMap.MaximumMark,
        //                                                                    skillMap.MarksObtained
        //                                                                } into skillExamGrp
        //                                                                select new ProgressReportExamListDTO()
        //                                                                {
        //                                                                    ExamID = skillExamGrp.Key.ExamID,
        //                                                                    MaximumMarks = skillExamGrp.Key.MaximumMark,
        //                                                                    MinimumMarks = skillExamGrp.Key.MinimumMark,
        //                                                                    Mark = skillExamGrp.Key.MarksObtained,
        //                                                                    Grade = skillExamGrp.Key.MarkGradeMap.GradeName

        //                                                                }).ToList(),
        //                                                   }).ToList(),
        //                                              }).ToList(),

        //                           }).ToList();


        //        }
        //        progressReportData.Exams = examList;
        //        progressReportData.Subjects = subjectList;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return progressReportData;
        //}

        public List<MarkGradeMapDTO> GetGradeByExamSkill(long examId, int skillId, int subjectId, int classId, int markGradeID)
        {
            List<MarkGradeMapDTO> mgMap = new List<MarkGradeMapDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                mgMap = (from markGrade in dbContext.MarkGradeMaps
                         join examsub in dbContext.ClassSubjectSkillGroupSkillMaps on markGrade.MarkGradeID equals examsub.MarkGradeID
                         join exam in dbContext.ClassSubjectSkillGroupMaps on examsub.ClassSubjectSkillGroupMapID equals exam.ClassSubjectSkillGroupMapID
                         where (exam.ExamID == examId && exam.SubjectID == subjectId && exam.ClassID == classId && examsub.SkillMasterID == skillId && examsub.MarkGradeID == markGradeID)
                         //)&& examsub.SubjectID == subjectId &&
                         //(mark >= markGrade.GradeFrom && mark <= markGrade.GradeTo))
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
    }
}