using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Academics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class AssignmentMapper : DTOEntityDynamicMapper
    {
        public static AssignmentMapper Mapper(CallContext context)
        {
            var mapper = new AssignmentMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AssignmentDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AssignmentDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Assignments.Where(X => X.AssignmentIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Subject)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AssignmentType)
                    .Include(i => i.AssignmentStatus)
                    .Include(i => i.AssignmentAttachmentMaps)
                    .Include(i => i.AssignmentSectionMaps).ThenInclude(i => i.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private AssignmentDTO ToDTO(Assignment entity)
        {
            var assignDto = new AssignmentDTO()
            {
                AssignmentIID = entity.AssignmentIID,
                Title = entity.Title,
                ClassID = entity.ClassID,
                IsActive = entity.IsActive,
                SubjectID = entity.SubjectID,
                //SectionID = entity.SectionID,
                Description = entity.Description,
                SchoolID = entity.SchoolID,
                AcademicYearID = entity.AcademicYearID,
                AssignmentTypeID = entity.AssignmentTypeID,
                AssignmentStatusID = entity.AssignmentStatusID,
                OldAssignmentStatusID = entity.AssignmentStatusID,
                StartDate = entity.StartDate,
                FreezeDate = entity.FreezeDate,
                //Section = entity.SectionID.HasValue ? new KeyValueDTO() { Key = entity.SectionID.ToString(), Value = entity.Section.SectionName } : new KeyValueDTO(),
                Class = new KeyValueDTO() { Key = entity.Class.ClassID.ToString(), Value = entity.Class?.ClassDescription },
                Subject = new KeyValueDTO() { Key = entity.Subject.SubjectID.ToString(), Value = entity.Subject?.SubjectName },
                DateOfSubmission = entity.DateOfSubmission.IsNotNull() ? Convert.ToDateTime(entity.DateOfSubmission) : DateTime.Now,
                AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID?.ToString(), Value = entity.AcademicYear?.Description + " " + '(' + entity.AcademicYear?.AcademicYearCode?.ToString() + ')' } : new KeyValueDTO(),
                AssignmentType = entity.AssignmentTypeID.HasValue ? new KeyValueDTO() { Key = entity.AssignmentTypeID?.ToString(), Value = entity.AssignmentType?.TypeName } : new KeyValueDTO(),
                AssignmentStatus = entity.AssignmentStatusID.HasValue ? new KeyValueDTO() { Key = entity.AssignmentStatusID?.ToString(), Value = entity.AssignmentStatus?.StatusName } : new KeyValueDTO(),
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
            };

            assignDto.SectionList = new List<KeyValueDTO>();
            foreach (var map in entity.AssignmentSectionMaps)
            {
                assignDto.SectionList.Add(new KeyValueDTO()
                {
                    Key = map.SectionID.ToString(),
                    Value = map.Section.SectionName
                });
            }

            assignDto.AssignmentAttachmentMaps = new List<AssignmentAttachmentMapDTO>();
            foreach (var attach in entity.AssignmentAttachmentMaps)
            {
                assignDto.AssignmentAttachmentMaps.Add(new AssignmentAttachmentMapDTO()
                {
                    Notes = attach.Notes != null ? attach.Notes : null,
                    AssignmentID = attach.AssignmentID.HasValue ? attach.AssignmentID : null,
                    AttachmentName = attach.AttachmentName != null ? attach.AttachmentName : null,
                    AttachmentDescription = attach.AttachmentDescription != null ? attach.AttachmentDescription : null,
                    AttachmentReferenceID = attach.AttachmentReferenceID.HasValue ? attach.AttachmentReferenceID : null,
                    AssignmentAttachmentMapIID = attach.AssignmentAttachmentMapIID,
                    CreatedBy = attach.CreatedBy,
                    UpdatedBy = attach.UpdatedBy,
                    CreatedDate = attach.CreatedDate,
                    UpdatedDate = attach.UpdatedDate,
                });
            }

            return assignDto;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AssignmentDTO;

            if (toDto.AcademicYearID == null || toDto.AcademicYearID == 0)
            {
                throw new Exception("Select academic year!");
            }

            if (toDto.AssignmentTypeID == null || toDto.AssignmentTypeID == 0)
            {
                throw new Exception("Select assignment type!");
            }

            if (toDto.ClassID == null || toDto.ClassID == 0)
            {
                throw new Exception("Select class!");
            }

            if (toDto.SectionList == null || toDto.SectionList.Count == 0)
            {
                throw new Exception("Select atleast one section!");
            }

            if (toDto.SubjectID == null || toDto.SubjectID == 0)
            {
                throw new Exception("Select subject!");
            }

            if (toDto.AssignmentStatusID == null || toDto.AssignmentStatusID == 0)
            {
                throw new Exception("Select assignment status!");
            }

            if (toDto.AssignmentAttachmentMaps == null || toDto.AssignmentAttachmentMaps.Count == 0)
            {
                throw new Exception("Please add attachments!");
            }

            if (toDto.OldAssignmentStatusID != null && toDto.OldAssignmentStatusID == 3)
            {
                throw new Exception("Assignment can't edit !! it's already published..");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new Assignment()
                {
                    Title = toDto.Title,
                    ClassID = toDto.ClassID,
                    IsActive = toDto.IsActive,
                    SubjectID = toDto.SubjectID,
                    //SectionID = toDto.SectionID,
                    Description = toDto.Description,
                    AssignmentIID = toDto.AssignmentIID,
                    AssignmentTypeID = toDto.AssignmentTypeID,
                    AssignmentStatusID = toDto.AssignmentStatusID,
                    AcademicYearID = toDto.AcademicYearID,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    StartDate = toDto.StartDate.IsNotNull() ? Convert.ToDateTime(toDto.StartDate) : DateTime.Now,
                    FreezeDate = toDto.FreezeDate.IsNotNull() ? Convert.ToDateTime(toDto.FreezeDate) : DateTime.Now,
                    DateOfSubmission = toDto.DateOfSubmission.IsNotNull() ? Convert.ToDateTime(toDto.DateOfSubmission) : DateTime.Now,
                    CreatedBy = toDto.AssignmentIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.AssignmentIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.AssignmentIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.AssignmentIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                entity.AssignmentSectionMaps = new List<AssignmentSectionMap>();

                foreach(var sec in toDto.SectionList)
                {
                    entity.AssignmentSectionMaps.Add(new AssignmentSectionMap()
                    {
                        AssignmentID = entity.AssignmentIID,
                        SectionID = int.Parse(sec.Key),
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedDate = entity.UpdatedDate,
                    });
                }

                //get Assignment Attachment Maps
                var IIDs = toDto.AssignmentAttachmentMaps
                    .Select(a => a.AssignmentAttachmentMapIID).ToList();

                //delete maps
                var entities = dbContext.AssignmentAttachmentMaps.Where(x =>
                    x.AssignmentID == entity.AssignmentIID &&
                    !IIDs.Contains(x.AssignmentAttachmentMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull() && entities.Count() > 0)
                    dbContext.AssignmentAttachmentMaps.RemoveRange(entities);

                entity.AssignmentAttachmentMaps = new List<AssignmentAttachmentMap>();

                foreach (var map in toDto.AssignmentAttachmentMaps)
                {
                    if (!string.IsNullOrEmpty(map.AttachmentName))
                    {
                        entity.AssignmentAttachmentMaps.Add(new AssignmentAttachmentMap()
                        {
                            AssignmentAttachmentMapIID = map.AssignmentAttachmentMapIID,
                            AssignmentID = map.AssignmentID,
                            AttachmentName = map.AttachmentName,
                            AttachmentDescription = map.AttachmentDescription,
                            AttachmentReferenceID = map.AttachmentReferenceID,
                            Notes = map.Notes,
                            CreatedBy = map.AssignmentAttachmentMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = map.AssignmentAttachmentMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = map.AssignmentAttachmentMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = map.AssignmentAttachmentMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            //TimeStamps = map.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        });
                    }
                }

                dbContext.Assignments.Add(entity);

                if (entity.AssignmentIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    if (entity.AssignmentAttachmentMaps.Count > 0)
                    {
                        foreach (var atch in entity.AssignmentAttachmentMaps)
                        {
                            if (atch.AssignmentAttachmentMapIID == 0)
                            {
                                dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    if (entity.AssignmentSectionMaps.Count > 0)
                    {
                        using (var dbContext1 = new dbEduegateSchoolContext())
                        {
                            var oldSectionMaps = dbContext1.AssignmentSectionMaps.Where(s => s.AssignmentID == entity.AssignmentIID).AsNoTracking().ToList();

                            if (oldSectionMaps != null && oldSectionMaps.Count > 0)
                            {
                                dbContext1.AssignmentSectionMaps.RemoveRange(oldSectionMaps);

                                dbContext1.SaveChanges();
                            }
                        }

                        foreach (var secMap in entity.AssignmentSectionMaps)
                        {
                            if (secMap.AssignmentSectionMapIID == 0)
                            {
                                dbContext.Entry(secMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(secMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                #region Assignment workflow code
                long? AssWorkFlowID = null;
                int? AssWorkFlowEntity = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("AssignmentWorkFlowEntity", 1, 2);
                if (AssWorkFlowEntity != 0)
                {
                    AssWorkFlowID = (from sub in dbContext.ClassSubjectWorkflowEntityMaps
                                            join wentity in dbContext.ClassSubjectMaps on sub.ClassSubjectMapID equals wentity.ClassSubjectMapIID
                                            where sub.SubjectID == entity.SubjectID && sub.WorkflowEntityID == AssWorkFlowEntity && wentity.ClassID == entity.ClassID
                                            && wentity.AcademicYearID == entity.AcademicYearID
                                            select sub.workflowID).FirstOrDefault();
                }
                if (AssWorkFlowID == null)
                {
                    AssWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<long?>("ASSIGNMENT_WORKFLOW_ID", 1, null);
                    if (AssWorkFlowID == null)
                        throw new Exception("Please set 'ASSIGNMENT_WORKFLOW_ID' in settings");
                }

                Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(AssWorkFlowID.Value, entity.AssignmentIID);
                #endregion

                return ToDTOString(ToDTO(entity.AssignmentIID));
            }
        }

        public int GetAssignmentEmployeeCount(long employeeID)
        {
            var assignmentCount = 0;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var assignmentDet = dbContext.Assignments
                //    .Include(i => i.Class")
                //    .Include(i => i.Section")
                //    .Include(i => i.Subject")
                //    .OrderByDescending(d => d.StartDate)
                //    .Where(x => x.Subject.SubjectTeacherMaps.Any(y => y.EmployeeID == employeeID) &&
                //    (x.Class.ClassClassTeacherMaps.Any(a => a.TeacherID == employeeID) || x.Class.ClassTeacherMaps.Any(b => b.TeacherID == employeeID)) && x.IsActive == true).ToList();

                int? currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var assignmentDet = dbContext.Assignments
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.Class).ThenInclude(i => i.ClassTeacherMaps)
                    .OrderByDescending(d => d.StartDate)
                    .Where(x => x.Class.ClassTeacherMaps.Any(b => b.TeacherID == employeeID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID && x.ClassID == b.ClassID && x.SectionID == b.SectionID && x.SubjectID == b.SubjectID)
                    && x.IsActive == true && x.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().ToList();

                assignmentCount = assignmentDet.Count;

                return assignmentCount;

            }
        }

        public int GetAssignmentStudentCount(long parentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var data =  (from asi in dbContext.Assignments
                        join stud in dbContext.Students on asi.ClassID equals stud.ClassID
                        where stud.ParentID == parentID && asi.AssignmentStatusID == 3 &&
                        asi.FreezeDate <= DateTime.Now
                        select asi).AsNoTracking().ToList();

                return data != null ? data.Count > 0 ? data.Count() : 0 : 0;
            }
        }

        public List<AssignmentDTO> GetAssignmentStudentwise(long studentId ,int? SubjectID)
        {
            DateTime currentDate = System.DateTime.Now;
            DateTime preYearDate = DateTime.Now.AddYears(-1);
            List<AssignmentDTO> assignmentDTOList = new List<AssignmentDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var assignments = (from asi in dbContext.Assignments
                                     join stud in dbContext.Students on asi.ClassID equals stud.ClassID
                                     where (stud.StudentIID == studentId && asi.AssignmentSectionMaps.Any(m => m.SectionID == stud.SectionID) && asi.IsActive == true && asi.AcademicYearID == stud.AcademicYearID && asi.AssignmentStatusID == 3 && (SubjectID.HasValue ? asi.SubjectID == SubjectID : asi.SubjectID != null))
                                     orderby asi.AssignmentIID descending
                                     select asi)
                                     .Include(i => i.Section)
                                     .Include(i => i.Class)
                                     .Include(i => i.Subject)
                                     .Include(i => i.AcademicYear)
                                     .Include(i => i.AssignmentType)
                                     .Include(i => i.AssignmentStatus)
                                     .Include(i => i.AssignmentAttachmentMaps)
                                     .Include(i => i.AssignmentSectionMaps)
                                     .AsNoTracking().ToList();

                assignmentDTOList = assignments.Select(assignmentGroup => new AssignmentDTO()
                {
                    Title = assignmentGroup.Title != null ? assignmentGroup.Title : null,
                    ClassID = assignmentGroup.ClassID.HasValue ? assignmentGroup.ClassID : null,
                    SubjectID = assignmentGroup.SubjectID.HasValue ? assignmentGroup.SubjectID : null,
                    SectionID = assignmentGroup.SectionID.HasValue ? assignmentGroup.SectionID : null,
                    Description = assignmentGroup.Description != null ? assignmentGroup.Description : null,
                    AssignmentIID = assignmentGroup.AssignmentIID,
                    AcademicYearID = assignmentGroup.AcademicYearID.HasValue ? assignmentGroup.AcademicYearID : null,
                    AssignmentTypeID = assignmentGroup.AssignmentTypeID.HasValue ? assignmentGroup.AssignmentTypeID : null,
                    AssignmentStatusID = assignmentGroup.AssignmentStatusID.HasValue ? assignmentGroup.AssignmentStatusID : null,
                    StartDate = assignmentGroup.StartDate.IsNotNull() ? Convert.ToDateTime(assignmentGroup.StartDate) : (DateTime?)null,
                    FreezeDate = assignmentGroup.FreezeDate.IsNotNull() ? Convert.ToDateTime(assignmentGroup.FreezeDate) : (DateTime?)null,
                    Section = assignmentGroup.SectionID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.SectionID.ToString(), Value = assignmentGroup.Section.SectionName } : new KeyValueDTO(),
                    Class = assignmentGroup.ClassID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.Class.ClassID.ToString(), Value = assignmentGroup.Class.ClassDescription } : new KeyValueDTO(),
                    Subject = assignmentGroup.SubjectID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.Subject.SubjectID.ToString(), Value = assignmentGroup.Subject.SubjectName } : new KeyValueDTO(),
                    DateOfSubmission = assignmentGroup.DateOfSubmission.IsNotNull() ? Convert.ToDateTime(assignmentGroup.DateOfSubmission) : (DateTime?)null,
                    AcademicYear = assignmentGroup.AcademicYearID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.AcademicYear.AcademicYearID.ToString(), Value = assignmentGroup.AcademicYear.Description } : new KeyValueDTO(),
                    AssignmentType = assignmentGroup.AssignmentTypeID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.AssignmentType.AssignmentTypeID.ToString(), Value = assignmentGroup.AssignmentType.TypeName } : new KeyValueDTO(),
                    AssignmentStatus = assignmentGroup.AssignmentStatusID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.AssignmentStatus.AssignmentStatusID.ToString(), Value = assignmentGroup.AssignmentStatus.StatusName } : new KeyValueDTO(),
                    CreatedDate = assignmentGroup.CreatedDate.IsNotNull() ? Convert.ToDateTime(assignmentGroup.CreatedDate) : (DateTime?)null,
                    AssignmentAttachmentMaps = (from aat in assignmentGroup.AssignmentAttachmentMaps//.AsEnumerable().AsQueryable()

                                                select new AssignmentAttachmentMapDTO()
                                                {
                                                    Notes = aat.Notes,
                                                    AssignmentID = aat.AssignmentID,
                                                    AttachmentName = aat.AttachmentName,
                                                    AttachmentDescription = aat.AttachmentDescription,
                                                    AttachmentReferenceID = aat.AttachmentReferenceID,
                                                    AssignmentAttachmentMapIID = aat.AssignmentAttachmentMapIID,
                                                }).ToList(),

                }).ToList();

            }

            return assignmentDTOList;
        }

        public List<AssignmentDTO> GetAssignmentStaffwise(long employeeID)
        {
            List<AssignmentDTO> assignmentDTOList = new List<AssignmentDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var assignmentDet = dbContext.Assignments
                //    .Include(i => i.Class")
                //    .Include(i => i.Section")
                //    .Include(i => i.Subject")
                //    .OrderByDescending(d => d.StartDate)
                //    .Where(x => x.Subject.SubjectTeacherMaps.Any(y => y.EmployeeID == employeeID) &&
                //    (x.Class.ClassClassTeacherMaps.Any(a => a.TeacherID == employeeID) || x.Class.ClassTeacherMaps.Any(b => b.TeacherID == employeeID)) && x.IsActive == true).ToList();

                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var assignmentDet = dbContext.Assignments
                    .Include(i => i.Class).ThenInclude(i => i.ClassTeacherMaps)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AssignmentType)
                    .Include(i => i.AssignmentStatus)
                    .Include (i => i.AssignmentAttachmentMaps)
                    .Where(x => x.Class.ClassTeacherMaps.Any(b => b.TeacherID == employeeID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID && x.ClassID == b.ClassID && x.AssignmentSectionMaps.Any(s => s.SectionID == b.SectionID) && x.SubjectID == b.SubjectID)
                    && x.IsActive == true && x.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .OrderByDescending(d => d.StartDate)
                    .AsNoTracking().ToList();

                if (assignmentDet.Count > 0)
                {
                    assignmentDTOList = assignmentDet.Select(assignmentGroup => new AssignmentDTO()
                    {
                        Title = assignmentGroup.Title ?? null,
                        ClassID = assignmentGroup.ClassID.HasValue ? assignmentGroup.ClassID : null,
                        SubjectID = assignmentGroup.SubjectID.HasValue ? assignmentGroup.SubjectID : null,
                        SectionID = assignmentGroup.SectionID.HasValue ? assignmentGroup.SectionID : null,
                        Description = assignmentGroup.Description ?? null,
                        AssignmentIID = assignmentGroup.AssignmentIID,
                        AcademicYearID = assignmentGroup.AcademicYearID.HasValue ? assignmentGroup.AcademicYearID : null,
                        AssignmentTypeID = assignmentGroup.AssignmentTypeID.HasValue ? assignmentGroup.AssignmentTypeID : null,
                        AssignmentStatusID = assignmentGroup.AssignmentStatusID.HasValue ? assignmentGroup.AssignmentStatusID : null,
                        StartDate = assignmentGroup.StartDate.IsNotNull() ? Convert.ToDateTime(assignmentGroup.StartDate) : (DateTime?)null,
                        FreezeDate = assignmentGroup.FreezeDate.IsNotNull() ? Convert.ToDateTime(assignmentGroup.FreezeDate) : (DateTime?)null,
                        Section = assignmentGroup.SectionID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.SectionID.ToString(), Value = assignmentGroup.Section.SectionName } : new KeyValueDTO(),
                        Class = assignmentGroup.ClassID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.Class.ClassID.ToString(), Value = assignmentGroup.Class.ClassDescription } : new KeyValueDTO(),
                        Subject = assignmentGroup.SubjectID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.Subject.SubjectID.ToString(), Value = assignmentGroup.Subject.SubjectName } : new KeyValueDTO(),
                        DateOfSubmission = assignmentGroup.DateOfSubmission.IsNotNull() ? Convert.ToDateTime(assignmentGroup.DateOfSubmission) : (DateTime?)null,
                        AcademicYear = assignmentGroup.AcademicYearID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.AcademicYear.AcademicYearID.ToString(), Value = assignmentGroup.AcademicYear.Description } : new KeyValueDTO(),
                        AssignmentType = assignmentGroup.AssignmentTypeID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.AssignmentType.AssignmentTypeID.ToString(), Value = assignmentGroup.AssignmentType.TypeName } : new KeyValueDTO(),
                        AssignmentStatus = assignmentGroup.AssignmentStatusID.HasValue ? new KeyValueDTO() { Key = assignmentGroup.AssignmentStatus.AssignmentStatusID.ToString(), Value = assignmentGroup.AssignmentStatus.StatusName } : new KeyValueDTO(),
                        AssignmentAttachmentMaps = (from aat in assignmentGroup.AssignmentAttachmentMaps

                                                    select new AssignmentAttachmentMapDTO()
                                                    {
                                                        Notes = aat.Notes,
                                                        AssignmentID = aat.AssignmentID,
                                                        AttachmentName = aat.AttachmentName,
                                                        AttachmentDescription = aat.AttachmentDescription,
                                                        AttachmentReferenceID = aat.AttachmentReferenceID,
                                                        AssignmentAttachmentMapIID = aat.AssignmentAttachmentMapIID,
                                                    }).ToList(),

                    }).ToList();
                }
            }

            return assignmentDTOList;
        }

        public List<KeyValueDTO> Getstudentsubjectlist(long studentID)
        {
            using (var sContext = new dbEduegateSchoolContext())
            {
                var student = sContext.Students.Where(x => x.StudentIID == studentID).AsNoTracking().FirstOrDefault();

                var subject = sContext.ClassSubjectMaps.Where(x => x.ClassID == student.ClassID && x.SectionID == student.SectionID && x.AcademicYearID == student.AcademicYearID)
                    .Include(i => i.Subject)
                    .AsNoTracking()
                    .ToList();

                var subjects = new List<KeyValueDTO>();

                subjects = subject.Select(x => new KeyValueDTO()
                {
                    Key = x.Subject.SubjectID.ToString(),
                    Value = x.Subject.SubjectName,
                }).ToList();

                return subjects;
            }
        }

    }
}