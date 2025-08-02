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
            using (var sContext = new dbEduegateSchoolContext())
            {
                List<int> _sStudentSubjcets = new List<int>();
                List<int> _sSubjectLangAndOptional = new List<int>() { 2, 3, 7 };

                var student = sContext.Students.Where(x => x.StudentIID == studentID).AsNoTracking().FirstOrDefault();

                if ((student.SecondLangID ?? 0) > 0)
                    _sStudentSubjcets.Add(student.SecondLangID ?? 0);

                if ((student.ThirdLangID ?? 0) > 0)
                    _sStudentSubjcets.Add(student.ThirdLangID ?? 0);

                if ((student.SubjectMapID ?? 0) > 0)
                    _sStudentSubjcets.Add(student.SubjectMapID ?? 0);


                var subject = sContext.ClassSubjectMaps.Where(x => x.ClassID == student.ClassID && x.SectionID == student.SectionID && x.AcademicYearID == student.AcademicYearID)
                    .Include(i => i.Subject)
                    .AsNoTracking()
                    .ToList();

                if (subject.Any())
                    _sStudentSubjcets.AddRange(subject.Where(w => !_sSubjectLangAndOptional.Contains(w.Subject.SubjectTypeID ?? 0)).Select(w => w.SubjectID ?? 0));

                var subjects = new List<KeyValueDTO>();

                subjects = subject.Where(w => _sStudentSubjcets.Contains(w.SubjectID ?? 0)).Select(x => new KeyValueDTO()
                {
                    Key = x.Subject.SubjectID.ToString(),
                    Value = x.Subject.SubjectName,
                }).ToList();

                return subjects;
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