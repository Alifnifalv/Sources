using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class ClassSubjectMapMapper : DTOEntityDynamicMapper
    {
        public static ClassSubjectMapMapper Mapper(CallContext context)
        {
            var mapper = new ClassSubjectMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassSubjectMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }


        private ClassSubjectMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassSubjectMaps.Where(x => x.ClassSubjectMapIID == IID)
                    .Include(i => i.Class)
                    .AsNoTracking().FirstOrDefault();

                var mapDatas = dbContext.ClassSubjectWorkflowEntityMaps.Where(z => z.ClassSubjectMapID == IID).AsNoTracking().ToList();

                var getDatas = dbContext.ClassSubjectMaps.Where(y => y.ClassID == entity.ClassID && y.AcademicYearID == entity.AcademicYearID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .AsNoTracking().ToList();

                var sectionIDs = getDatas.Select(x => x.SectionID).Distinct().ToList();
                var subjectIDs = getDatas.Select(x => x.SubjectID).Distinct().ToList();

                var subjects = new List<KeyValueDTO>();
                var sections = new List<KeyValueDTO>();

                foreach (var subID in subjectIDs)
                {
                    var clsSubMap = getDatas.FirstOrDefault(x => x.SubjectID == subID);
                    var subject = clsSubMap != null && clsSubMap.Subject != null ? clsSubMap.Subject : dbContext.Subjects.AsNoTracking().FirstOrDefault(sub => sub.SubjectID == subID);
                    if (subject != null)
                    {
                        subjects.Add(new KeyValueDTO()
                        {
                            Key = subject.SubjectID.ToString(),
                            Value = subject?.SubjectName,
                        });
                    }
                }

                foreach (var secID in sectionIDs)
                {
                    var clsSubMap = getDatas.FirstOrDefault(x => x.SectionID == secID);
                    var section = clsSubMap != null && clsSubMap.Section != null ? clsSubMap.Section : dbContext.Sections.AsNoTracking().FirstOrDefault(sub => sub.SectionID == secID);
                    if (section != null)
                    {
                        sections.Add(new KeyValueDTO()
                        {
                            Key = section?.SectionID.ToString(),
                            Value = section?.SectionName,
                        });
                    }
                }

                var mapList = new List<ClassSubjectWorkFlowMapDTO>();
                foreach (var map in mapDatas)
                {
                    mapList.Add(new ClassSubjectWorkFlowMapDTO()
                    {
                        ClassSubjectWorkflowEntityMapIID = map.ClassSubjectWorkflowEntityMapIID,
                        ClassSubjectMapID = map.ClassSubjectMapID,
                        SubjectID = map.SubjectID,
                        WorkflowEntityID = map.WorkflowEntityID,
                        workflowID = map.workflowID,
                    });
                }

                return new ClassSubjectMapDTO()
                {
                    ClassSubjectMapIID = entity.ClassSubjectMapIID,
                    Class = entity.ClassID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.ClassID.ToString(),
                        Value = entity.Class.ClassDescription
                    } : new KeyValueDTO(),
                    //Section = new KeyValueDTO() { Key = classMap.SectionID.ToString(), Value = classMap.Section.SectionName },
                    ClassID = entity.ClassID,
                    //SectionID = classMap.SectionID,
                    Subject = subjects,
                    Section = sections,
                    ClassSubjectWorkFlow = mapList,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = data.TimeStamps == null ? null : Convert.ToBase64String(data.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassSubjectMapDTO;
            if (toDto.ClassID == null || toDto.ClassID == 0)
            {
                throw new Exception("Please Select class");
            }

            //TODO: Refactor saving codes in later
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateTime = DateTime.Now.Date;

                var getSubjectMapData = dbContext.ClassSubjectMaps.Where(x => x.ClassID == toDto.ClassID && x.AcademicYearID == _context.AcademicYearID).AsNoTracking().ToList();

                if (toDto.ClassSubjectMapIID == 0 && getSubjectMapData.Any(x => x.ClassID == toDto.ClassID))
                {
                    throw new Exception("Class Subject map for this Class Already exist. Please use edit..");
                }

                if (getSubjectMapData.Count > 0 && getSubjectMapData != null)
                {
                    foreach (var data in getSubjectMapData)
                    {
                        var delmapData = dbContext.ClassSubjectWorkflowEntityMaps.Where(h => h.ClassSubjectMapID == data.ClassSubjectMapIID).AsNoTracking().ToList();

                        if (delmapData.Count > 0 && delmapData != null)
                        {
                            dbContext.ClassSubjectWorkflowEntityMaps.RemoveRange(delmapData);
                        }

                        dbContext.ClassSubjectMaps.Remove(data);
                        dbContext.SaveChanges();
                    }
                }

                ClassSubjectMap subjectMap = null;
                if (toDto.ListData.Count > 0)
                {
                    foreach (var data in toDto.ListData)
                    {
                        var entity = new ClassSubjectMap()
                        {
                            //ClassSubjectMapIID = toDto.ClassSubjectMapIID,
                            ClassID = toDto.ClassID,
                            SectionID = data.SectionID,
                            SubjectID = data.SubjectID,
                            SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                            AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                            CreatedBy = toDto.ClassSubjectMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = toDto.ClassSubjectMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = toDto.ClassSubjectMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                            UpdatedDate = toDto.ClassSubjectMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                            //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        };
                        
                        //dbContext.ClassSubjectMaps.Add(entity);
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        dbContext.SaveChanges();

                        subjectMap = entity;
                    }

                    if (toDto.ClassSubjectWorkFlow.Count > 0)
                    {
                        foreach (var mapData in toDto.ClassSubjectWorkFlow)
                        {
                            if (mapData.SubjectID.HasValue)
                            {
                                var workfolowMap = new ClassSubjectWorkflowEntityMap()
                                {
                                    //ClassSubjectWorkflowEntityMapIID = mapData.ClassSubjectWorkflowEntityMapIID,
                                    ClassSubjectMapID = subjectMap.ClassSubjectMapIID,
                                    WorkflowEntityID = mapData.WorkflowEntityID,
                                    workflowID = mapData.workflowID,
                                    SubjectID = mapData.SubjectID,
                                    CreatedBy = toDto.ClassSubjectMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                    UpdatedBy = toDto.ClassSubjectMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                    CreatedDate = toDto.ClassSubjectMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                                    UpdatedDate = toDto.ClassSubjectMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),

                                };

                                dbContext.Entry(workfolowMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }
                        dbContext.SaveChanges();
                    }
                }

                return GetEntity(subjectMap.ClassSubjectMapIID);
            }
        }

        //public List<ClassSubjectMapDTO> GetSubjectByClassID(int classID)
        //{
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var classSubjectList = new List<ClassSubjectMapDTO>();

        //        classSubjectList = (from subj in dbContext.ClassSubjectMaps
        //                      where (subj.ClassID == classID && subj.SchoolID == _context.SchoolID)
        //                      orderby subj.ClassSubjectMapIID descending
        //                      group subj by new
        //                      {
        //                          //subj.ClassSubjectMapIID,
        //                          subj.ClassID,
        //                          //subj.SectionID,
        //                          //subj.Class,
        //                          //subj.Section,
        //                          subj.SubjectID,
        //                          subj.Subject

        //                      } into classSubjectGroup
        //                      select new ClassSubjectMapDTO()
        //                      {
        //                          //ClassSubjectMapIID = classSubjectGroup.Key.ClassSubjectMapIID,
        //                          ClassID = classSubjectGroup.Key.ClassID,
        //                          //SectionID = classSubjectGroup.Key.SectionID,
        //                          SubjectID = classSubjectGroup.Key.SubjectID,
        //                          //Subject = new KeyValueDTO() { Key = classSubjectGroup.Key.SubjectID.ToString(), Value = classSubjectGroup.Key.Subject.SubjectName },
        //                          //SectionName = classSubjectGroup.Key.SectionID.HasValue ? classSubjectGroup.Key.Section.SectionName : null,
        //                          //Class = new KeyValueDTO() { Key = classSubjectGroup.Key.ClassID.ToString(), Value = classSubjectGroup.Key.Class.ClassDescription },
        //                      }).Distinct().ToList();

        //        return classSubjectList;
        //    }
        //}

        private ClassSubjectMapDTO DTOTolist(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassSubjectMaps.Where(x => x.ClassSubjectMapIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new ClassSubjectMapDTO()
                {
                    Class = new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription },
                    ClassID = entity.ClassID,
                    SectionID = entity.Section.SectionID,
                    //Section = new KeyValueDTO() { Key = entity.Section.SectionID.ToString(), Value = entity.Section.SectionName },
                    SubjectID = entity.Subject.SubjectID,
                    //Subject = new KeyValueDTO() { Key = entity.Subject.SubjectID.ToString(), Value = entity.Subject.SubjectName }  
                };
            }
        }

        public List<KeyValueDTO> GetSubjectByClass(int classID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classSubjectList = dbContext.ClassSubjectMaps
                    .Where(x => x.ClassID == classID && x.SchoolID == _context.SchoolID && x.AcademicYearID == _context.AcademicYearID)
                    .AsNoTracking().ToList();

                var subjectIDs = classSubjectList.Select(x => x.SubjectID).Distinct().ToList();

                var subjects = dbContext.Subjects.Where(s => s.SubjectID != 0).AsNoTracking().ToList();

                var subjectList = new List<KeyValueDTO>();

                foreach (var subID in subjectIDs)
                {
                    var data = subjects.FirstOrDefault(sub => sub.SubjectID == subID);
                    subjectList.Add(new KeyValueDTO()
                    {
                        Key = data.SubjectID.ToString(),
                        Value = data.SubjectName,
                    });
                }

                return subjectList;
            }
        }

        public List<ClassSectionSubjectListMapDTO> FillClassandSectionWiseSubjects(int classID, int sectionID, int IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var listDTO = new List<ClassSectionSubjectListMapDTO>();

                var classSubjectList = dbContext.ClassSubjectMaps
                    .Where(x => x.ClassID == classID && x.SectionID == sectionID && x.SchoolID == _context.SchoolID && x.AcademicYearID == _context.AcademicYearID)
                    .Include(i => i.Subject)
                    .AsNoTracking().ToList();

                var getDatasByIDs = dbContext.ClassTeacherMaps.Where(x => x.ClassClassTeacherMapID == IID)
                    .Include(i => i.Subject)
                    .Include(i => i.Employee2)
                    .AsNoTracking().ToList();

                foreach (var data in getDatasByIDs)
                {
                    listDTO.Add(new ClassSectionSubjectListMapDTO
                    {
                        ClassSubjectMapIID = (long)data.ClassClassTeacherMapID,
                        Subject = new KeyValueDTO()
                        {
                            Key = data.SubjectID.ToString(),
                            Value = data.Subject?.SubjectName
                        },
                        SubjectID = data.SubjectID,
                        OtherTeacher = new KeyValueDTO()
                        {
                            Key = data.TeacherID.ToString(),
                            Value = data.Employee2?.EmployeeCode + " - " + data.Employee2?.FirstName + ' ' + data.Employee2?.MiddleName + ' ' + data.Employee2?.LastName
                        },
                    });
                }

                foreach (var data in classSubjectList)
                {
                    if (!listDTO.Any(x => x.SubjectID == data.SubjectID))
                    {
                        listDTO.Add(new ClassSectionSubjectListMapDTO
                        {
                            ClassSubjectMapIID = data.ClassSubjectMapIID,
                            ClassID = data.ClassID,
                            SectionID = data.SectionID,
                            Subject = new KeyValueDTO()
                            {
                                Key = data.SubjectID.ToString(),
                                Value = data.Subject?.SubjectName
                            },
                        });
                    }

                }

                return listDTO;
            }
        }

        public List<ClassSubjectMapDTO> GetSubjectsByStudentID(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classSubjectList = new List<ClassSubjectMapDTO>();

                var studDet = dbContext.Students.Where(x => x.StudentIID == studentID).AsNoTracking().FirstOrDefault();

                if (studDet != null)
                {
                    classSubjectList = (from subj in dbContext.ClassSubjectMaps
                                        where (subj.ClassID == studDet.ClassID)
                                        orderby subj.ClassSubjectMapIID descending
                                        group subj by new
                                        {
                                            //subj.ClassSubjectMapIID,
                                            subj.ClassID,
                                            //subj.SectionID,
                                            //subj.Class,
                                            //subj.Section,
                                            subj.SubjectID,
                                            subj.Subject

                                        } into classSubjectGroup
                                        select new ClassSubjectMapDTO()
                                        {
                                            //ClassSubjectMapIID = classSubjectGroup.Key.ClassSubjectMapIID,
                                            ClassID = classSubjectGroup.Key.ClassID,
                                            //SectionID = classSubjectGroup.Key.SectionID,
                                            SubjectID = classSubjectGroup.Key.SubjectID,
                                            //Subject = new KeyValueDTO() { Key = classSubjectGroup.Key.SubjectID.ToString(), Value = classSubjectGroup.Key.Subject.SubjectName },
                                            //SectionName = classSubjectGroup.Key.SectionID.HasValue ? classSubjectGroup.Key.Section.SectionName : null,
                                            //Class = new KeyValueDTO() { Key = classSubjectGroup.Key.ClassID.ToString(), Value = classSubjectGroup.Key.Class.ClassDescription },
                                        }).Distinct().AsNoTracking().ToList();
                }

                return classSubjectList;
            }
        }

        public List<SubjectDTO> GetSubjectDetailsByClassID(int classID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classSubjectList = dbContext.ClassSubjectMaps
                    .Where(x => x.ClassID == classID && x.SchoolID == _context.SchoolID && x.AcademicYearID == _context.AcademicYearID)
                    .AsNoTracking().ToList();

                var subjectIDs = classSubjectList.Select(x => x.SubjectID).Distinct().ToList();

                var subjects = dbContext.Subjects.Where(s => s.SubjectID != 0).AsNoTracking().ToList();

                var subjectList = new List<SubjectDTO>();

                foreach (var subID in subjectIDs)
                {
                    var data = subjects.FirstOrDefault(sub => sub.SubjectID == subID);
                    subjectList.Add(new SubjectDTO()
                    {
                        SubjectID = data.SubjectID,
                        SubjectName = data.SubjectName,
                        HexColorCode = data.HexCodeUpper,
                        IconFileName = data.IconFileName,
                    });
                }

                return subjectList;
            }
        }

    }
}