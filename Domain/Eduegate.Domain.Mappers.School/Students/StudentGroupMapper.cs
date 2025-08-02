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
    public class StudentGroupMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "GroupName" };
        public static StudentGroupMapper Mapper(CallContext context)
        {
            var mapper = new StudentGroupMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentGroupDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }
        public StudentGroupDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentGroups.Where(a => a.StudentGroupID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new StudentGroupDTO()
                {
                    StudentGroupID = entity.StudentGroupID,
                    GroupName = entity.GroupName,
                    GroupTypeID = entity.GroupTypeID,
                    IsActive = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                };
            }
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentGroupDTO;
            var errorMessage = string.Empty;

            //validate first
            foreach (var field in validationFields)
            {
                var isValid = ValidateField(toDto, field);

                if (isValid.Key.Equals("true"))
                {
                    errorMessage = string.Concat(errorMessage, "-", isValid.Value, "<br>");
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }
            //convert the dto to entity and pass to the repository.
            var entity = new StudentGroup()
            {
                StudentGroupID = toDto.StudentGroupID,
                GroupName = toDto.GroupName,
                GroupTypeID = toDto.GroupTypeID,
                IsActive = toDto.IsActive,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.StudentGroupID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.StudentGroupID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.StudentGroupID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.StudentGroupID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.StudentGroupID == 0)
                {
                    var maxGroupID = dbContext.StudentGroups.Max(a => (int?)a.StudentGroupID);
                    entity.StudentGroupID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.StudentGroups.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.StudentGroups.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new StudentGroupDTO()
            {
                StudentGroupID = entity.StudentGroupID,
                GroupName = entity.GroupName,
                GroupTypeID = entity.GroupTypeID,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            });
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as StudentGroupDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "GroupName":
                    if (!string.IsNullOrEmpty(toDto.GroupName))
                    {
                        var hasDuplicated = IsGroupNameDuplicated(toDto.GroupName, toDto.StudentGroupID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "GroupName already exists, Please try with different GroupName.";
                        }
                        else
                        {
                            valueDTO.Key = "false";
                        }
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }
                    break;
            }
            return valueDTO;
        }

        public bool IsGroupNameDuplicated(string GroupName, long StudentGroupID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<StudentGroup> group;

                if (StudentGroupID == 0)
                {
                    group = db.StudentGroups.Where(x => x.GroupName == GroupName).AsNoTracking().ToList();
                }
                else
                {
                    group = db.StudentGroups.Where(x => x.StudentGroupID != StudentGroupID && x.GroupName == GroupName).AsNoTracking().ToList();
                }

                if (group.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

    }
}