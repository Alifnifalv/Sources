using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using Eduegate.Services.Contracts.School.Fines;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Fines
{
    public class FineMasterMapper : DTOEntityDynamicMapper
    {
        public static FineMasterMapper Mapper(CallContext context)
        {
            var mapper = new FineMasterMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FineMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private FineMasterDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FineMasters.Where(x => x.FineMasterID == IID)
                    .Include(i => i.FeeFineType)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new FineMasterDTO()
                {
                    FineMasterID = entity.FineMasterID,
                    FineCode = entity.FineCode,
                    FineName = entity.FineName,
                    Amount=entity.Amount ,
                    FeeFineType = entity.FeeFineTypeID.ToString(),
                    LedgerAccount = entity.LedgerAccountID.ToString(),
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FineMasterDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new FineMaster()
            {
                FineMasterID = toDto.FineMasterID,
                FineCode = toDto.FineCode,
                FineName = toDto.FineName,
                Amount = toDto.Amount,
                FeeFineTypeID = toDto.FeeFineTypeID,
                LedgerAccountID = toDto.LedgerAccountID,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.FineMasterID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.FineMasterID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.FineMasterID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.FineMasterID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
              
                if (entity.FineMasterID == 0)
                {                    
                    var maxGroupID = dbContext.FineMasters.Max(a => (int?)a.FineMasterID);
                    entity.FineMasterID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.FineMasters.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FineMasters.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.FineMasterID));
        }
    }
}