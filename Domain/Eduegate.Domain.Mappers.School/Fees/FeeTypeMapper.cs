using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Framework;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeTypeMapper : DTOEntityDynamicMapper
    {
        public static FeeTypeMapper Mapper(CallContext context)
        {
            var mapper = new FeeTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }


        private FeeTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FeeTypes.Where(x => x.FeeTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new FeeTypeDTO()
                {
                    FeeTypeID = entity.FeeTypeID,
                    FeeCode = entity.FeeCode,
                    TypeName = entity.TypeName,
                    FeeGroupId = entity.FeeGroupId,
                    FeeGroupName = entity.FeeGroupId.HasValue ? entity.FeeGroupId.ToString() : null,
                    FeeCycleId=entity.FeeCycleId,
                    IsRefundable = entity.IsRefundable,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new FeeType()
            {
                FeeTypeID = toDto.FeeTypeID,
                FeeCode = toDto.FeeCode,
                TypeName = toDto.TypeName,
                FeeGroupId = toDto.FeeGroupId,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                //FeeCycleId = toDto.FeeCycleId,
                IsRefundable = toDto.IsRefundable,
                CreatedBy = toDto.FeeTypeID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.FeeTypeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.FeeTypeID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.FeeTypeID > 0 ? DateTime.Now : dto.UpdatedDate,

            };

            using (var dbContext = new dbEduegateSchoolContext())
            {               
                if (entity.FeeTypeID == 0)
                {
                    
                    var maxGroupID = dbContext.FeeTypes.Max(a => (int?)a.FeeTypeID);
                    entity.FeeTypeID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.FeeTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FeeTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.FeeTypeID));
        }
    }
}




