using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Exams;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class ExamTypeMapper : DTOEntityDynamicMapper
    {
        public static ExamTypeMapper Mapper(CallContext context)
        {
            var mapper = new ExamTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public ExamTypeDTO ToDTO(ExamType entity)
        {
            return new ExamTypeDTO()
            {
                ExamTypeID = entity.ExamTypeID,
                ExamTypeDescription = entity.ExamTypeDescription,
                AcademicYearID = entity.AcademicYearID,
                SchoolID = entity.SchoolID,
                CreatedBy =entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ExamTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as ExamTypeDTO);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ExamTypes.Where(a => a.ExamTypeID == IID).AsNoTracking().FirstOrDefault();

                return ToDTOString(new ExamTypeDTO()
                {
                    ExamTypeID = entity.ExamTypeID,
                    ExamTypeDescription = entity.ExamTypeDescription,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate
                });
            }
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ExamTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new ExamType()
            {
                ExamTypeID = toDto.ExamTypeID,
                ExamTypeDescription = toDto.ExamTypeDescription,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.ExamTypeID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.ExamTypeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.ExamTypeID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.ExamTypeID > 0 ? DateTime.Now : dto.UpdatedDate
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.ExamTypes.Add(entity);
                if (entity.ExamTypeID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(new ExamTypeDTO()
            {
                ExamTypeID = entity.ExamTypeID,
                ExamTypeDescription = entity.ExamTypeDescription,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            });
        }
    }
}