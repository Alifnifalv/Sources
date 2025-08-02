using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class ClassSubjectSkillGroupMapMapper : DTOEntityDynamicMapper
    {
        public static ClassSubjectSkillGroupMapMapper Mapper(CallContext context)
        {
            var mapper = new ClassSubjectSkillGroupMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassSubjectSkillGroupMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassSubjectSkillGroupMaps.Where(a => a.ClassSubjectSkillGroupMapID == IID)
                    .Include(i => i.MarkGrade)
                    .Include(i => i.SkillGroupSubjectMaps).ThenInclude(i => i.Subject)
                    .Include(i => i.ClassSubjectSkillGroupSkillMaps).ThenInclude(i => i.SkillMaster)
                    .Include(i => i.ClassSubjectSkillGroupSkillMaps).ThenInclude(i => i.MarkGrade)
                    .Include(i => i.ClassSubjectSkillGroupSkillMaps).ThenInclude(i => i.SkillGroupMaster)
                    .AsNoTracking()
                    .FirstOrDefault();

                var skilldetail = new ClassSubjectSkillGroupMapDTO()
                {
                    ClassSubjectSkillGroupMapID = entity.ClassSubjectSkillGroupMapID,
                    SubjectID = entity.SubjectID,
                    MarkGradeID = entity.MarkGradeID.HasValue ? entity.MarkGradeID : null,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    TotalMarks = entity.TotalMarks,
                    ISScholastic = entity.ISScholastic,
                    Description = entity.Description,
                    ProgressCardHeader = entity.ProgressCardHeader,
                    ConversionFactor = entity.ConversionFactor,

                    MarkGrade = entity.MarkGradeID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.MarkGradeID.ToString(),
                        Value = entity.MarkGrade.Description,
                    } : new KeyValueDTO(),
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate
                };

                skilldetail.SubSkills = new List<ClassSubjectSkillGroupSkillMapDTO>();
                foreach (var skill in entity.ClassSubjectSkillGroupSkillMaps)
                {
                    skilldetail.SubSkills.Add(new ClassSubjectSkillGroupSkillMapDTO()
                    {
                        ClassSubjectSkillGroupSkillMapID = skill.ClassSubjectSkillGroupSkillMapID,
                        ClassSubjectSkillGroupMapID = skill.ClassSubjectSkillGroupMapID,
                        SkillMasterID = skill.SkillMasterID,
                        MarkGradeID = skill.MarkGradeID,
                        MinimumMarks = skill.MinimumMarks,
                        MaximumMarks = skill.MaximumMarks,
                        IsEnableInput = skill.IsEnableInput,
                        SkillMaster = skill.SkillMasterID.HasValue ? new KeyValueDTO()
                        {
                            Key = skill.SkillMasterID.ToString(),
                            Value = skill.SkillMaster.SkillName,
                        }: null,
                        MarkGrade = new KeyValueDTO()
                        {
                            Key = skill.MarkGradeID.ToString(),
                            Value = skill.MarkGrade.Description,
                        },
                        SkillGroupMasterID = skill.SkillGroupMasterID,
                        SkillGroup = skill.SkillGroupMasterID.HasValue ? new KeyValueDTO()
                        {
                            Key = skill.SkillGroupMasterID.ToString(),
                            Value = skill.SkillGroupMaster.SkillGroup,
                        } : new KeyValueDTO(),
                    });
                }

                SubjectsList(entity, skilldetail);

                return ToDTOString(skilldetail);
            }
        }

        private void SubjectsList(ClassSubjectSkillGroupMap entity, ClassSubjectSkillGroupMapDTO mapDTO)
        {
            var subjectList = entity.SkillGroupSubjectMaps.Count > 0 ? entity.SkillGroupSubjectMaps : null;

            if (subjectList != null)
            {
                foreach (var sub in subjectList)
                {
                    mapDTO.Subjects.Add(new KeyValueDTO()
                    {
                        Key = sub.SubjectID.ToString(),
                        Value = sub == null ? null : sub.Subject.SubjectName,
                    });

                }
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassSubjectSkillGroupMapDTO;

            SkillGroupChecking(toDto);
            GradeMapChecking(toDto);

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                //grid Remove
                var IIDs = toDto.SubSkills
                    .Select(a => a.ClassSubjectSkillGroupSkillMapID).ToList();
                //delete maps
                var entities = dbContext.ClassSubjectSkillGroupSkillMaps.Where(x =>
                    x.ClassSubjectSkillGroupMapID == toDto.ClassSubjectSkillGroupMapID &&
                    !IIDs.Contains(x.ClassSubjectSkillGroupSkillMapID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.ClassSubjectSkillGroupSkillMaps.RemoveRange(entities);


                //delete subject maps
                var subjectEntities = dbContext.SkillGroupSubjectMaps.Where(x =>
                    x.ClassSubjectSkillGroupMapID == toDto.ClassSubjectSkillGroupMapID).AsNoTracking().ToList();

                if (subjectEntities.IsNotNull())
                    dbContext.SkillGroupSubjectMaps.RemoveRange(subjectEntities);

                dbContext.SaveChanges();

                var entity = new ClassSubjectSkillGroupMap()
                {
                    ClassSubjectSkillGroupMapID = toDto.ClassSubjectSkillGroupMapID,
                    SubjectID = toDto.SubjectID.HasValue ? toDto.SubjectID : null,
                    MarkGradeID = toDto.MarkGradeID,
                    TotalMarks = toDto.TotalMarks,
                    //MinimumMarks = toDto.MinimumMarks,
                    //MaximumMarks = toDto.MaximumMarks,
                    ISScholastic = toDto.ISScholastic,
                    Description = toDto.Description,
                    ProgressCardHeader = toDto.ProgressCardHeader,
                    ConversionFactor = toDto.ConversionFactor,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                    CreatedBy = toDto.ClassSubjectSkillGroupMapID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.ClassSubjectSkillGroupMapID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.ClassSubjectSkillGroupMapID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                    UpdatedDate = toDto.ClassSubjectSkillGroupMapID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                dbContext.ClassSubjectSkillGroupMaps.Add(entity);

                entity.SkillGroupSubjectMaps = new List<SkillGroupSubjectMap>();
                if (toDto.Subjects != null && toDto.Subjects.Count > 0)
                {
                    foreach (var sub in toDto.Subjects)
                    {
                        entity.SkillGroupSubjectMaps.Add(new SkillGroupSubjectMap()
                        {
                            ClassSubjectSkillGroupMapID = toDto.ClassSubjectSkillGroupMapID,
                            SubjectID = int.Parse(sub.Key),
                            MarkGradeID = toDto.MarkGradeID,
                            MinimumMarks = toDto.MinimumMarks,
                            MaximumMarks = toDto.MaximumMarks,
                            CreatedBy = entity.CreatedBy,
                            UpdatedBy = entity.UpdatedBy,
                            CreatedDate = entity.CreatedDate,
                            UpdatedDate = entity.UpdatedDate,
                        });
                    }
                }

                entity.ClassSubjectSkillGroupSkillMaps = new List<ClassSubjectSkillGroupSkillMap>();
                foreach (var subSkill in toDto.SubSkills)
                {
                    if (subSkill.SkillGroupMasterID != 0)
                    {
                        if (subSkill.MarkGradeID == 0)
                        {
                            throw new Exception("Please Select Sub Skill Grade Map!");
                        }

                        SubSkillGradeMapChecking(subSkill.MarkGradeID, subSkill);

                        entity.ClassSubjectSkillGroupSkillMaps.Add(new ClassSubjectSkillGroupSkillMap()
                        {
                            ClassSubjectSkillGroupSkillMapID = subSkill.ClassSubjectSkillGroupSkillMapID,
                            ClassSubjectSkillGroupMapID = entity.ClassSubjectSkillGroupMapID,
                            SkillMasterID = subSkill.SkillMasterID,
                            MarkGradeID = subSkill.MarkGradeID,
                            MinimumMarks = subSkill.MinimumMarks,
                            MaximumMarks = subSkill.MaximumMarks,
                            CreatedBy = entity.CreatedBy,
                            UpdatedBy = entity.UpdatedBy,
                            CreatedDate = entity.CreatedDate,
                            UpdatedDate = entity.UpdatedDate,
                            //TimeStamps = subSkill.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                            IsEnableInput = subSkill.IsEnableInput,
                            SkillGroupMasterID = subSkill.SkillGroupMasterID,
                        });
                    }
                    else
                    {
                        throw new Exception("Please Select Skill Group!");
                    }
                }

                if (entity.ClassSubjectSkillGroupMapID == 0)
                {
                    var maxGroupID = dbContext.ClassSubjectSkillGroupMaps.Max(a => (long?)a.ClassSubjectSkillGroupMapID);
                    entity.ClassSubjectSkillGroupMapID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;

                    dbContext.ClassSubjectSkillGroupMaps.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var subSkillMap in entity.ClassSubjectSkillGroupSkillMaps)
                    {
                        if (subSkillMap.ClassSubjectSkillGroupSkillMapID != 0)
                        {
                            dbContext.Entry(subSkillMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(subSkillMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    if (entity.SkillGroupSubjectMaps.Count > 0)
                    {
                        var oldSubjectMaps = dbContext.SkillGroupSubjectMaps.Where(s => s.ClassSubjectSkillGroupMapID == entity.ClassSubjectSkillGroupMapID).AsNoTracking().ToList();

                        if (oldSubjectMaps != null && oldSubjectMaps.Count > 0)
                        {
                            dbContext.SkillGroupSubjectMaps.RemoveRange(oldSubjectMaps);
                        }

                        foreach (var subMap in entity.SkillGroupSubjectMaps)
                        {
                            if (subMap.SkillGroupSubjectMapIID == 0)
                            {
                                dbContext.Entry(subMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(subMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return GetEntity(entity.ClassSubjectSkillGroupMapID);

            }
        }

        public List<ExamDTO> GetExamsByClassID(int classID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var examList = new List<ExamDTO>();

                var entities = dbContext.ClassSubjectSkillGroupMaps.Where(a => a.ClassID == classID)
                    .Include(i => i.Exam)
                    .OrderBy(a => a.ExamID)
                    .AsNoTracking().ToList();

                foreach (var classSub in entities)
                {
                    examList.Add(new ExamDTO()
                    {
                        ExamIID = (long)classSub.ExamID,
                        ExamDescription = classSub.Exam.ExamDescription
                    });
                }

                return examList;
            }
        }

        public List<SubjectDTO> GetExamSubjectsByClassID(int classID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subjectList = new List<SubjectDTO>();

                var entities = dbContext.ClassSubjectSkillGroupMaps.Where(a => a.ClassID == classID)
                    .Include(i => i.Subject)
                    .OrderBy(a => a.SubjectID)
                    .AsNoTracking().ToList();

                foreach (var classSub in entities)
                {
                    subjectList.Add(new SubjectDTO()
                    {
                        SubjectID = (int)classSub.SubjectID,
                        SubjectName = classSub.Subject.SubjectName
                    });
                }

                return subjectList;
            }
        }

        public List<ClassSubjectSkillGroupMapDTO> GetExamSubjectSkillGroupByClassID(int classID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subjectList = new List<ClassSubjectSkillGroupMapDTO>();
                var entities = dbContext.ClassSubjectSkillGroupMaps.Where(a => a.ClassID == classID)
                    .Include(i => i.Exam)
                    .Include(i => i.Class)
                    .Include(i => i.Subject)
                    .OrderBy(a => a.SubjectID)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    subjectList.Add(new ClassSubjectSkillGroupMapDTO()
                    {
                        ExamID = entity.ExamID,
                        ClassID = (int)entity.ClassID,
                        SubjectID = entity.SubjectID,

                        TotalMarks = entity.TotalMarks,
                        //MinimumMarks = entity.MinimumMarks,
                        //MaximumMarks = entity.MaximumMarks,
                        //SkillGroup = new KeyValueDTO()
                        //{
                        //    Key = entity.SkillGroupMasterID.ToString(),
                        //    Value = entity.SkillGroupMaster.SkillGroup,
                        //},

                        Exam = new KeyValueDTO()
                        {
                            Key = entity.ExamID.ToString(),
                            Value = entity.Exam.ExamDescription,
                        },
                        Class = new KeyValueDTO()
                        {
                            Key = entity.ClassID.ToString(),
                            Value = entity.Class.ClassDescription,
                        },
                        //Subject = new KeyValueDTO()
                        //{
                        //    Key = entity.SubjectID.ToString(),
                        //    Value = entity.Subject.SubjectName,
                        //},

                        SubSkills = new List<ClassSubjectSkillGroupSkillMapDTO>()
                    });
                }

                return subjectList;
            }
        }

        public List<ClassSubjectSkillGroupMapDTO> GetExamSubjectSkillsByClassID(int classID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subjectList = new List<ClassSubjectSkillGroupMapDTO>();
                var entities = dbContext.ClassSubjectSkillGroupMaps.Where(a => a.ClassID == classID)
                    .Include(i => i.Exam)
                    .Include(i => i.Class)
                    .Include(i => i.Subject)
                    .OrderBy(a => a.SubjectID)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    subjectList.Add(new ClassSubjectSkillGroupMapDTO()
                    {
                        ExamID = entity.ExamID,
                        ClassID = (int)entity.ClassID,
                        SubjectID = entity.SubjectID,

                        TotalMarks = entity.TotalMarks,
                        //MinimumMarks = entity.MinimumMarks,
                        //MaximumMarks = entity.MaximumMarks,
                        //SkillGroup = new KeyValueDTO()
                        //{
                        //    Key = entity.SkillGroupMasterID.ToString(),
                        //    Value = entity.SkillGroupMaster.SkillGroup,
                        //},

                        Exam = new KeyValueDTO()
                        {
                            Key = entity.ExamID.ToString(),
                            Value = entity.Exam.ExamDescription,
                        },
                        Class = new KeyValueDTO()
                        {
                            Key = entity.ClassID.ToString(),
                            Value = entity.Class.ClassDescription,
                        },
                        //Subject = new KeyValueDTO()
                        //{
                        //    Key = entity.SubjectID.ToString(),
                        //    Value = entity.Subject.SubjectName,
                        //},
                    });
                }

                return subjectList;
            }
        }

        public ClassSubjectSkillGroupMapDTO GetExamMarkDetails(long subjectID, long examID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new ClassSubjectSkillGroupMapDTO();

                //Get Employee details

                var examsubMap = dbContext.ExamSubjectMaps.Where(a => a.ExamID == examID && a.SubjectID == subjectID)
                    .OrderByDescending(o => o.ExamSubjectMapIID)
                    .AsNoTracking().FirstOrDefault();

                var subjectSkillDTO = new ClassSubjectSkillGroupMapDTO()
                {
                    //ExamMinimumMark = examsubMap != null && examsubMap.MinimumMarks != null ? examsubMap.MinimumMarks : null,
                    //ExamMaximumMark = examsubMap != null && examsubMap.MaximumMarks != null ? examsubMap.MaximumMarks : null,
                };

                return subjectSkillDTO;
            }
        }

        private void SkillGroupChecking(ClassSubjectSkillGroupMapDTO dto)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var skillGroupMap = dbContext.ClassSubjectSkillGroupMaps.Where(a => a.ClassID == dto.ClassID && a.SubjectID == dto.SubjectID && a.ExamID == dto.ExamID)
                    .OrderByDescending(o => o.ClassSubjectSkillGroupMapID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (skillGroupMap != null)
                {
                    decimal? skillGroupTotalMark = null;

                    if (skillGroupMap.ClassSubjectSkillGroupMapID == dto.ClassSubjectSkillGroupMapID)
                    {
                        skillGroupTotalMark = dto.MaximumMarks;
                    }

                    else
                    {
                        skillGroupTotalMark = skillGroupMap.MaximumMarks + dto.MaximumMarks;
                    }

                    if (skillGroupTotalMark > dto.ExamMaximumMark)
                    {
                        throw new Exception("Skill group already exist under this subject and max mark must be less than or equal to exam max mark!");
                    }
                }
            }
        }

        private void GradeMapChecking(ClassSubjectSkillGroupMapDTO dto)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var gradeTo = dbContext.MarkGradeMaps.Where(a => a.MarkGradeID == dto.MarkGradeID).AsNoTracking().ToList().Max(a => a.GradeTo);
                var gradeFrom = dbContext.MarkGradeMaps.Where(a => a.MarkGradeID == dto.MarkGradeID).AsNoTracking().ToList().Max(a => a.GradeFrom);

                if (gradeTo != null && gradeFrom != null)
                {
                    if (gradeTo.Value > gradeFrom.Value)
                    {
                        if (dto.TotalMarks > gradeTo.Value)
                        {
                            throw new Exception("Skill total mark must be less than or equal to mark grade max mark!");
                        }
                    }
                    else
                    {
                        if (dto.TotalMarks > gradeFrom.Value)
                        {
                            throw new Exception("Skill total mark must be less than or equal to mark grade max mark!");
                        }
                    }
                }
            }
        }

        private void SubSkillGradeMapChecking(int markGradeID, ClassSubjectSkillGroupSkillMapDTO dto)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var gradeTo = dbContext.MarkGradeMaps.Where(a => a.MarkGradeID == markGradeID).AsNoTracking().ToList().Max(a => a.GradeTo);
                var gradeFrom = dbContext.MarkGradeMaps.Where(a => a.MarkGradeID == markGradeID).AsNoTracking().ToList().Max(a => a.GradeFrom);

                if (gradeTo != null && gradeFrom != null)
                {
                    if (gradeTo.Value > gradeFrom.Value)
                    {
                        if (dto.MaximumMarks > gradeTo.Value)
                        {
                            throw new Exception("Sub skill max mark must be less than or equal to sub skill mark grade max mark!");
                        }
                    }
                    else
                    {
                        if (dto.MaximumMarks > gradeFrom.Value)
                        {
                            throw new Exception("Sub skill max mark must be less than or equal to sub skill mark grade max mark!");
                        }
                    }
                }
            }
        }

        public KeyValueDTO GetSkillGroup(int? masterID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var accountList = new KeyValueDTO();
                var skillMasterEntity = dbContext.SkillMasters.Where(X => X.SkillMasterID == masterID).AsNoTracking().FirstOrDefault();
                if (skillMasterEntity != null)
                {
                    var groupEntity = skillMasterEntity.SkillGroupMasterID.HasValue ?
                        dbContext.SkillGroupMasters.Where(X => X.SkillGroupMasterID == skillMasterEntity.SkillGroupMasterID)
                        .AsNoTracking().FirstOrDefault() : null;

                    if (groupEntity != null)
                    {
                        accountList = new KeyValueDTO
                        {
                            Key = groupEntity.SkillGroupMasterID.ToString(),
                            Value = groupEntity.SkillGroup
                        };
                    }
                }
                return accountList;
            }
        }

        public List<KeyValueDTO> GetSubSkillByGroup(int skillGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subSkillList = new List<KeyValueDTO>();

                var entities = dbContext.SkillMasters.Where(X => X.SkillGroupMasterID == skillGroupID).AsNoTracking().ToList();

                foreach (var skillGroup in entities)
                {
                    subSkillList.Add(new KeyValueDTO
                    {
                        Key = skillGroup.SkillMasterID.ToString(),
                        Value = skillGroup.SkillName
                    });
                }

                return subSkillList;
            }
        }

    }
}