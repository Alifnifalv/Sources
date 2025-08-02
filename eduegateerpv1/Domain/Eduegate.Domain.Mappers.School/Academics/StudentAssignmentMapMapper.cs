using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Academics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class StudentAssignmentMapMapper : DTOEntityDynamicMapper
    {
        public static StudentAssignmentMapMapper Mapper(CallContext context)
        {
            var mapper = new StudentAssignmentMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentAssignmentDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StudentAssignmentDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentAssignmentMaps.Where(X => X.StudentAssignmentMapIID == IID)
                    .Include(i => i.Assignment).ThenInclude(i => i.Class)
                    .Include(i => i.Assignment).ThenInclude(i => i.Section)
                    .Include(i => i.Assignment).ThenInclude(i => i.Subject)
                    .AsNoTracking()
                    .FirstOrDefault();

                //var assignment = dbContext.Assignments.Where(X => X.AssignmentIID == entity.AssignmentID)
                //   .Include(i => i.Section)
                //   .Include(i => i.Class)
                //   .Include(i => i.Subject)                   
                //   .AsNoTracking()
                //   .FirstOrDefault();

                var assignment = entity.Assignment;

                var assignmentMapDetails = dbContext.StudentAssignmentMaps.Where(X => X.AssignmentID == entity.AssignmentID)
                   .Include(i => i.Student)
                   .Include(i => i.Assignment)
                   .AsNoTracking()
                   .ToList();

                var assignmentDTO = new StudentAssignmentDTO()
                {
                    StudentAssignmentMapIID = entity.StudentAssignmentMapIID,
                    ClassID = assignment.ClassID,
                    ClassName = assignment.ClassID.HasValue ? assignment.Class.ClassDescription : null,
                    SectionID = assignment.SectionID,
                    SectionName = assignment.SectionID.HasValue ? assignment.Section.SectionName : null,
                    AssignmentID = assignment.AssignmentIID,
                    AssignmentName = assignment.Title,
                    SubjectID = assignment.SubjectID,
                    SubjectName = assignment.SubjectID.HasValue ? assignment.Subject.SubjectName : null
                };

                assignmentDTO.StudentAssignmentMaps = new List<StudentAssignmentMapDTO>();
                foreach (var assignmentMap in assignmentMapDetails)
                {
                    assignmentDTO.StudentAssignmentMaps.Add(new StudentAssignmentMapDTO()
                    {
                        StudentAssignmentMapIID = assignmentMap.StudentAssignmentMapIID,
                        AssignmentID = assignmentMap.AssignmentID,
                        Assignment = new KeyValueDTO()
                        {
                            Key = assignmentMap.AssignmentID.ToString(),
                            Value = assignmentMap.Assignment.Title
                        },
                        DateOfSubmission = assignmentMap.DateOfSubmission,
                        AssignmentStatusID = assignmentMap.AssignmentStatusID,
                        AttachmentReferenceId = assignmentMap.AttachmentReferenceId,
                        Notes = assignmentMap.Notes,
                        Description = assignmentMap.Description,
                        AttachmentName = assignmentMap.AttachmentName,
                        StudentId = assignmentMap.StudentId,
                        CreatedBy = assignmentMap.CreatedBy,
                        UpdatedBy = assignmentMap.UpdatedBy,
                        CreatedDate = assignmentMap.CreatedDate,
                        UpdatedDate = assignmentMap.UpdatedDate,
                        //TimeStamps = Convert.ToBase64String(assignmentMap.TimeStamaps),
                        StudentName = assignmentMap.StudentId.HasValue ? assignmentMap.Student.AdmissionNumber + " - " + assignmentMap.Student.FirstName + " " + assignmentMap.Student.MiddleName + " "+ assignmentMap.Student.LastName : null,
                        Remarks = assignmentMap.Remarks,
                    });
                }

                return assignmentDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentAssignmentDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new StudentAssignmentMap()
            {
                StudentAssignmentMapIID = toDto.StudentAssignmentMapIID,
                AssignmentID = toDto.AssignmentID,
            };
            using (var dbContext = new dbEduegateSchoolContext())
            {
                foreach (var assignmentmap in toDto.StudentAssignmentMaps)
                {
                    var mapDetail = dbContext.StudentAssignmentMaps.Where(X => X.StudentAssignmentMapIID == assignmentmap.StudentAssignmentMapIID).AsNoTracking().FirstOrDefault();
                    if (mapDetail != null)
                    {
                        mapDetail.Remarks = assignmentmap.Remarks;
                        mapDetail.UpdatedBy = (int)_context.LoginID;
                        mapDetail.UpdatedDate = DateTime.Now;

                        dbContext.Entry(mapDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
            }
            return ToDTOString(ToDTO(entity.StudentAssignmentMapIID));
        }

    }
}