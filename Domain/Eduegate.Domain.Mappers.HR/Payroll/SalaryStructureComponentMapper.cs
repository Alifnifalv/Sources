using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class SalaryStructureComponentMapper : DTOEntityDynamicMapper
    {
        public static SalaryStructureComponentMapper Mapper(CallContext context)
        {
            var mapper = new SalaryStructureComponentMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalaryStructureComponentDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.SalaryStructureComponentMaps.Where(X => X.SalaryStructureComponentMapIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new SalaryStructureComponentDTO()
                {
                    SalaryStructureComponentMapIID = entity.SalaryStructureComponentMapIID,
                    SalaryComponentID = entity.SalaryComponentID,
                    SalaryStructureID = entity.SalaryStructureID,
                    MinAmount = entity.MinAmount,
                    MaxAmount = entity.MaxAmount,
                    Formula = entity.Formula
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SalaryStructureComponentDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new SalaryStructureComponentMap()
            {
                SalaryStructureComponentMapIID = toDto.SalaryStructureComponentMapIID,
                SalaryComponentID = toDto.SalaryComponentID,
                SalaryStructureID = toDto.SalaryStructureID,
                MinAmount = toDto.MinAmount,
                MaxAmount = toDto.MaxAmount,
                Formula = toDto.Formula
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (string.IsNullOrEmpty(Convert.ToString(entity.SalaryStructureComponentMapIID)))
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(new SalaryStructureComponentDTO()
            {
                SalaryStructureComponentMapIID = entity.SalaryStructureComponentMapIID,
                SalaryComponentID = entity.SalaryComponentID,
                SalaryStructureID = entity.SalaryStructureID,
                MinAmount = entity.MinAmount,
                MaxAmount = entity.MaxAmount,
                Formula = entity.Formula
            });
        }
    }
}