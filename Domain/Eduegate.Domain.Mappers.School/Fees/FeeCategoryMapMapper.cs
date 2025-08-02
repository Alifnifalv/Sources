using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeCategoryMapMapper : DTOEntityDynamicMapper
    {
        public static FeeCategoryMapMapper Mapper(CallContext context)
        {
            var mapper = new FeeCategoryMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeCategoryMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public FeeCategoryMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.CategoryFeeMaps.Where(x => x.CategoryFeeMapIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var FeeCategoryMapDTO = new FeeCategoryMapDTO()
                {
                    CategoryFeeMapIID = entity.CategoryFeeMapIID,
                    IsPrimary = entity.IsPrimary,
                    FeeMasterID = entity.FeeMasterID,
                    //FeeMaster = entity.FeeMasterID == null ? new KeyValueDTO() { Key = null, Value = null } : new KeyValueDTO()
                    //{
                    //    Key = entity.FeeMasterID.ToString(),
                    //    //Value = entity.FeeMaster.;
                    //},s
                    CategoryID = entity.CategoryID,
                    //Categories = entity.CategoryID == null ? new KeyValueDTO() { Key = null, Value = null } : new KeyValueDTO()
                    //{
                    //    Key = entity.CategoryID.ToString(),
                    //    //Value = entity.;
                    //},

                };

                return FeeCategoryMapDTO;
            };
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeCategoryMapDTO;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new CategoryFeeMap()
                {
                    CategoryFeeMapIID = toDto.CategoryFeeMapIID,
                    CategoryID = toDto.CategoryID.HasValue ? toDto.CategoryID : null,
                    FeeMasterID = toDto.FeeMasterID.HasValue ? toDto.FeeMasterID : null,
                    //SchoolID = _context.SchoolID.HasValue ? Convert.ToByte(_context.SchoolID) : (byte?)null,
                    //AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                    CreatedBy = toDto.CategoryFeeMapIID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                    CreatedDate = toDto.CategoryFeeMapIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedBy = toDto.CategoryFeeMapIID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
                    UpdatedDate = toDto.CategoryFeeMapIID != 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                if (toDto.CategoryFeeMapIID == 0)
                {
                    dbContext.CategoryFeeMaps.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.CategoryFeeMaps.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.CategoryFeeMapIID));
            }
        }

    }
}