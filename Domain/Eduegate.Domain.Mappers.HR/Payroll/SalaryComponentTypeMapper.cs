using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
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
    public class SalaryComponentTypeMapper : DTOEntityDynamicMapper
    {
        public static SalaryComponentTypeMapper Mapper(CallContext context)
        {
            var mapper = new SalaryComponentTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalaryComponentTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as SalaryComponentTypeDTO);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SalaryComponentTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.SalaryComponentTypes.Where(X => X.SalaryComponentTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private SalaryComponentTypeDTO ToDTO(SalaryComponentType entity)
        {
            var componetDTO = new SalaryComponentTypeDTO()
            {
                SalaryComponentTypeID = entity.SalaryComponentTypeID,
                TypeName = entity.TypeName,
            };

            return componetDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SalaryComponentTypeDTO;

            //convert the dto to entity and pass to the repository.
            var entity = new SalaryComponentType()
            {
                SalaryComponentTypeID = toDto.SalaryComponentTypeID,
                TypeName = toDto.TypeName,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (entity.SalaryComponentTypeID == 0)
                {
                    var maxGroupID = dbContext.SalaryComponentTypes.Max(a => (byte?)a.SalaryComponentTypeID);
                    entity.SalaryComponentTypeID = Convert.ToByte(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);

                    dbContext.SalaryComponentTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.SalaryComponentTypeID));
        }

    }
}