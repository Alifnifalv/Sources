using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class ClassSectionMapMapper : DTOEntityDynamicMapper
    {
        public static ClassSectionMapMapper Mapper(CallContext context)
        {
            var mapper = new ClassSectionMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassSectionMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ClassSectionMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entities = dbContext.ClassSectionMaps.Where(x => x.ClassID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .AsNoTracking()
                    .ToList();

                var grouped = entities.GroupBy(x => x.ClassID).FirstOrDefault();

                var sections = new List<KeyValueDTO>();
                ClassSectionMap classMap = null;
                foreach (var grp in grouped)
                {
                    sections.Add(new KeyValueDTO()
                    {
                        Key = grp.SectionID.ToString(),
                        Value = grp.Section.SectionName
                    });

                    classMap = grp;
                }

                var classSecMapDTO = new ClassSectionMapDTO();
                if (classMap != null)
                {
                    classSecMapDTO = new ClassSectionMapDTO()
                    {
                        ClassSectionMapIID = classMap.ClassSectionMapIID,
                        ClassID = classMap.ClassID,
                        //Class = entity.Class != null ? entity.Class.ClassDescription : null,
                        Class = new KeyValueDTO()
                        {
                            Key = classMap.ClassID.ToString(),
                            Value = classMap.Class.ClassDescription
                        },
                        SectionID = classMap.SectionID,
                        Section = sections,
                        //Description = classMap.Description,
                        AcademicYearID = classMap.AcademicYearID,
                        SchoolID = classMap.SchoolID,
                        //Section = entity.Section != null ? entity.Section.SectionName : null,
                        //Section = new KeyValueDTO()
                        //{
                        //    Key = entity.SectionID.ToString(),
                        //    Value = entity.Section.SectionName
                        //},
                        MinimumCapacity = classMap.MinimumCapacity,
                        MaximumCapacity = classMap.MaximumCapacity,
                        CreatedBy = classMap.CreatedBy,
                        UpdatedBy = classMap.UpdatedBy,
                        CreatedDate = classMap.CreatedDate,
                        UpdatedDate = classMap.UpdatedDate,
                        //TimeStamps = classMap.TimeStamps == null ? null : Convert.ToBase64String(classMap.TimeStamps),
                    };
                }

                return classSecMapDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassSectionMapDTO;
            if (toDto.ClassID == null || toDto.ClassID == 0)
            {
                throw new Exception("Please Select class");
            }
            //if (toDto.SectionID == null || toDto.SectionID == 0)
            //{
            //    throw new Exception("Please Select Section");
            //}

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                //delete all mapping and recreate
                var oldMapping = dbContext.ClassSectionMaps.Where(x => x.ClassID == toDto.ClassID).AsNoTracking().ToList();
                if (oldMapping.Count > 0)
                {
                    dbContext.ClassSectionMaps.RemoveRange(oldMapping);
                    dbContext.SaveChanges();
                }

                ClassSectionMap subjectMap = null;
                if (toDto.Section.Count > 0)
                {
                    foreach (KeyValueDTO keyval in toDto.Section)
                    {
                        var entity = new ClassSectionMap()
                        {
                            //ClassSectionMapIID = toDto.ClassSectionMapIID,
                            ClassID = toDto.ClassID,
                            //SectionID = toDto.SectionID,
                            SectionID = int.Parse(keyval.Key),
                            //Description = toDto.Description,
                            SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                            AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                            CreatedBy = toDto.ClassSectionMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = toDto.ClassSectionMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = toDto.ClassSectionMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                            UpdatedDate = toDto.ClassSectionMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                            //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                            MinimumCapacity = toDto.MinimumCapacity,
                            MaximumCapacity = toDto.MaximumCapacity,
                        };
                        subjectMap = entity;

                        dbContext.ClassSectionMaps.Add(entity);
                        dbContext.SaveChanges();
                    }
                }

                dbContext.SaveChanges();
                return GetEntity(subjectMap.ClassID.Value);
            }
        }
        //        //convert the dto to entity and pass to the repository.
        //        var entity = new ClassSectionMap()
        //        {
        //            ClassSectionMapIID = toDto.ClassSectionMapIID,
        //            ClassID = toDto.ClassID,
        //            SectionID = toDto.SectionID,
        //            CreatedBy = toDto.ClassSectionMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
        //            UpdatedBy = toDto.ClassSectionMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
        //            CreatedDate = toDto.ClassSectionMapIID == 0 ? DateTime.Now : dto.CreatedDate,
        //            UpdatedDate = toDto.ClassSectionMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
        //            //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
        //        };

        //        using (var dbContext = new dbEduegateSchoolContext())
        //        {
        //            var repository = new EntityRepository<ClassSectionMap, dbEduegateSchoolContext>(dbContext);

        //            if (entity.ClassSectionMapIID == 0)
        //            {
        //                var maxGroupID = repository.GetMaxID(a => a.ClassSectionMapIID);
        //                //entity.ClassSectionMapIID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;
        //                entity.ClassSectionMapIID = Convert.ToByte(maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1);
        //                entity = repository.Insert(entity);
        //            }
        //            else
        //            {
        //                entity = repository.Update(entity);
        //            }
        //        }

        //        return ToDTOString(ToDTO(entity.ClassSectionMapIID));
        //    }
        //}

        public List<ClassSectionMapDTO> GetSubjectByClassID(int classID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classSubjectList = new List<ClassSectionMapDTO>();

                var entities = dbContext.ClassSectionMaps.Where(a => a.ClassID == classID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .OrderBy(a => a.Section.SectionName)
                    .AsNoTracking()
                    .ToList();

                foreach (var classSub in entities)
                {
                    classSubjectList.Add(DTOTolist(classSub.ClassSectionMapIID));
                }

                return classSubjectList;
            }
        }

        public List<KeyValueDTO> Getstudentsubjectlist(long studentID)
        {
            //int? subjectID = null; byte? subjectTypeID = null;
            using (var sContext = new dbEduegateSchoolContext())
            {
                var classid = sContext.Students.Where(x => x.StudentIID == studentID).AsNoTracking().Select(x => x.ClassID).FirstOrDefault();
                var subject = sContext.ClassSubjectMaps.Where(x => x.Equals(classid)).AsNoTracking().ToList();

                var subjects = new List<KeyValueDTO>();


                subjects = subject.Select(x => new KeyValueDTO()
                {
                    Key = x.SubjectID.ToString(),
                    Value = x.SubjectID.ToString(),
                }).ToList();

                return subjects;
                //if (subjectData != null)
                //{
                //    subjectID = subjectData.SubjectID;
                //    subjectTypeID = subjectData.SubjectTypeID;
                //}


                //    List<StudentDTO> studentList = (from n in sContext.Students
                //                                    join a in sContext.AcademicYears on n.SchoolAcademicyearID equals a.AcademicYearID
                //                                    where n.ClassID == markEntrySearchArgsDTO.ClassID && n.IsActive == true
                //                                    && n.SectionID == markEntrySearchArgsDTO.SectionID && a.AcademicYearStatusID != 3
                //                                    //&& n.StudentIID == 2480
                //                                    orderby n.AdmissionNumber
                //                                    select new StudentDTO
                //                                    {
                //                                        StudentIID = n.StudentIID,
                //                                        StudentFullName = n.FirstName + " " + n.MiddleName + " " + n.LastName,
                //                                        RollNumber = n.AdmissionNumber,
                //                                        SecoundLanguageID = n.SecondLangID,
                //                                        ThridLanguageID = n.ThirdLangID,
                //                                        SubjectMapID = n.SubjectMapID
                //                                    }).ToList();
                //    List<long> studentIDS = studentList.Select(x => (x.StudentIID)).ToList();
                //    var studentOptionSubList = (from n in sContext.Students
                //                                join o in sContext.StudentStreamOptionalSubjectMaps on n.StudentIID equals o.StudentID
                //                                where studentIDS.Contains(n.StudentIID) && (subjectID != null || o.SubjectID == subjectID)
                //                                select n.StudentIID
                //                               ).ToList();


                //    if (studentOptionSubList.Count() > 0)
                //    {
                //        var studentOptionSub = studentList.Where(x => studentOptionSubList.Contains(x.StudentIID)).ToList();
                //        studentList.RemoveAll(w => !studentOptionSub.Any(x => x == w));
                //    }

                //    if (markEntrySearchArgsDTO.SubjectMapID.HasValue)
                //    {
                //        var subjectMapID = studentList.Where(x => x.SubjectMapID == subjectID).ToList();
                //        studentList.RemoveAll(w => !subjectMapID.Any(x => x == w));
                //    }

                //    if (markEntrySearchArgsDTO.LanguageTypeID.HasValue && markEntrySearchArgsDTO.LanguageTypeID == 2)
                //    {
                //        var secondLangStud = studentList.Where(x => x.SecoundLanguageID == subjectID).ToList();
                //        studentList.RemoveAll(w => !secondLangStud.Any(x => x == w));
                //    }
                //    if (markEntrySearchArgsDTO.LanguageTypeID.HasValue && markEntrySearchArgsDTO.LanguageTypeID == 3)
                //    {
                //        var thirdLangStud = studentList.Where(x => x.ThridLanguageID == subjectID).ToList();
                //        studentList.RemoveAll(w => !thirdLangStud.Any(x => x == w));
                //    }

                //    studentIDS = studentList.Select(x => (x.StudentIID)).ToList();
                //    var dfndMarkReg = (from markregsub in sContext.MarkRegisterSkills
                //                       join markreg in sContext.MarkRegisters on markregsub.MarkRegisterSkillGroup.MarkRegisterID
                //                       equals markreg.MarkRegisterIID
                //                       where markreg.ClassID == markEntrySearchArgsDTO.ClassID
                //                       //&& markreg.SectionID == markEntrySearchArgsDTO.SectionID
                //                  && markregsub.MarkRegisterSkillGroup.SubjectID == markEntrySearchArgsDTO.SubjectId
                //                  && markreg.ExamGroupID == markEntrySearchArgsDTO.TermID && markreg.ExamID == markEntrySearchArgsDTO.ExamID
                //                  && markregsub.MarkRegisterSkillGroup.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID
                //                  && ((markregsub.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                //                  && markEntrySearchArgsDTO.SkillID == null) || (markregsub.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                //                  && markregsub.SkillMasterID == markEntrySearchArgsDTO.SkillID))
                //                  && studentIDS.Contains(markreg.StudentId.Value)

                //                       select new StudentSkillsDTO()
                //                       {
                //                           StudentID = markreg.StudentId.Value,
                //                           SkillGroupMasterID = markregsub.SkillGroupMasterID,
                //                           SkillMasterID = markregsub.SkillMasterID,
                //                           MarksObtained = markregsub.MarksObtained,
                //                           GradeID = markregsub.MarksGradeMapID,
                //                           PresentStatusID = markreg.PresentStatusID,
                //                           MarkRegisterSkillGroupID = markregsub.MarkRegisterSkillGroupID,
                //                           MarkRegisterSkillID = markregsub.MarkRegisterSkillIID,
                //                           MarkRegisterID = markregsub.MarkRegisterSkillGroup.MarkRegisterID
                //                       })
                //                   .ToList();
                //    studentList.All(w =>
                //    {
                //        var studMarkReg = new List<StudentSkillsDTO>();
                //        if (dfndMarkReg.Any())
                //            studMarkReg = dfndMarkReg.Where(x => x.StudentID == w.StudentIID).ToList();
                //        sRetData.Add(GetStudentScholasticData(w.RollNumber, w.StudentIID, w.StudentFullName, markEntrySearchArgsDTO,
                //                              dFndSkills.ToList(), mgMap, studMarkReg.ToList())); return true;
                //    });
                //}

            }
        }

        private ClassSectionMapDTO DTOTolist(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassSectionMaps.Where(x => x.ClassSectionMapIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new ClassSectionMapDTO()
                {
                    Class = new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription },
                    ClassID = entity.ClassID,
                    SectionID = entity.Section.SectionID,
                    Sections = new KeyValueDTO() { Key = entity.Section.SectionID.ToString(), Value = entity.Section.SectionName }
                };
            }
        }

    }
}