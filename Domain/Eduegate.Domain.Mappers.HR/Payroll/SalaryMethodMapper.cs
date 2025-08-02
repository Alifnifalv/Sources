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
    public class SalaryMethodMapper : DTOEntityDynamicMapper
    {
        public static SalaryMethodMapper Mapper(CallContext context)
        {
            var mapper = new SalaryMethodMapper();
            mapper._context = context;
            return mapper;
        }

        //public List<SalaryMethodDTO> ToDTO(List<SalaryMethod> entities)
        //{
        //    var dtos = new List<SalaryMethodDTO>();

        //    foreach (var entity in entities)
        //    {
        //        dtos.Add(ToDTO(entity));
        //    }

        //    return dtos;
        //}

        public SalaryMethodDTO ToDTO(SalaryMethod entity)
        {
            return new SalaryMethodDTO()
            {
                SalaryMethodID = entity.SalaryMethodID,
                SalaryMethodName = entity.SalaryMethodName
            };
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalaryMethodDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as SalaryMethodDTO);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.SalaryMethods.Where(X => X.SalaryMethodID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new SalaryMethodDTO()
                {
                    SalaryMethodID = entity.SalaryMethodID,
                    SalaryMethodName = entity.SalaryMethodName,
                });
            }
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SalaryMethodDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new SalaryMethod()
            {
                SalaryMethodID = toDto.SalaryMethodID,
                SalaryMethodName = toDto.SalaryMethodName,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (entity.SalaryMethodID == 0)
                {                   
                    var maxGroupID = dbContext.SalaryMethods.Max(a => (int?)a.SalaryMethodID);
                    entity.SalaryMethodID = Convert.ToInt32(maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1);
                    dbContext.SalaryMethods.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.SalaryMethods.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(new SalaryMethodDTO()
            {
                SalaryMethodID = entity.SalaryMethodID,
                SalaryMethodName = entity.SalaryMethodName,
            });
        }
    }
}