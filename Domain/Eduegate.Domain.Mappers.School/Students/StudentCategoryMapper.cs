using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Framework;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class StudentCategoryMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "Description" };
        public static StudentCategoryMapper Mapper(CallContext context)
        {
            var mapper = new StudentCategoryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentCategoryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StudentCategoryDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentCategories.Where(a => a.StudentCategoryID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new StudentCategoryDTO()
                {
                    StudentCategoryID = entity.StudentCategoryID,
                    Description = entity.Description,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentCategoryDTO;
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
            var entity = new StudentCategory()
            {
                StudentCategoryID = toDto.StudentCategoryID,
                Description = toDto.Description,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.StudentCategoryID == 0)
                {
                    var maxGroupID = dbContext.StudentCategories.Max(a => (int?)a.StudentCategoryID);
                    entity.StudentCategoryID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.StudentCategories.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.StudentCategories.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.StudentCategoryID));
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as StudentCategoryDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "Description":
                    if (!string.IsNullOrEmpty(toDto.Description))
                    {
                        var hasDuplicated = IsGroupNameDuplicated(toDto.Description, toDto.StudentCategoryID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Description already exists, Please try with different Description.";
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

        public bool IsGroupNameDuplicated(string Description, long StudentCategoryID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<StudentCategory> category;

                if (StudentCategoryID == 0)
                {
                    category = db.StudentCategories.Where(x => x.Description == Description).AsNoTracking().ToList();
                }
                else
                {
                    category = db.StudentCategories.Where(x => x.StudentCategoryID != StudentCategoryID && x.Description == Description).AsNoTracking().ToList();
                }

                if (category.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

    }
}