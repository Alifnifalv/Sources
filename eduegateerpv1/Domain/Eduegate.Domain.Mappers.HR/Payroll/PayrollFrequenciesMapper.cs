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
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class PayrollFrequenciesMapper : DTOEntityDynamicMapper
    {
        public static PayrollFrequenciesMapper Mapper(CallContext context)
        {
            var mapper = new PayrollFrequenciesMapper();
            mapper._context = context;
            return mapper;
        }
        public List<PayrollFrequenciesDTO> ToDTO(List<PayrollFrequency> entities)
        {
            var dtos = new List<PayrollFrequenciesDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public PayrollFrequenciesDTO ToDTO(PayrollFrequency entity)
        {
            return new PayrollFrequenciesDTO()
            {
                PayrollFrequencyID = entity.PayrollFrequencyID,
                FrequencyName = entity.FrequencyName,
            };
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<PayrollFrequenciesDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as PayrollFrequenciesDTO);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.PayrollFrequencies.Where(X => X.PayrollFrequencyID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new PayrollFrequenciesDTO()
                {
                    PayrollFrequencyID = entity.PayrollFrequencyID,
                    FrequencyName = entity.FrequencyName,
                });
            }
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PayrollFrequenciesDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new PayrollFrequency()
            {
                PayrollFrequencyID = toDto.PayrollFrequencyID,
                FrequencyName = toDto.FrequencyName,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (entity.PayrollFrequencyID == 0)
                {
                    var maxGroupID = dbContext.PayrollFrequencies.Max(a => (byte?)a.PayrollFrequencyID);
                    entity.PayrollFrequencyID = Convert.ToByte(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);
                    dbContext.PayrollFrequencies.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.PayrollFrequencies.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(new PayrollFrequenciesDTO()
            {
                PayrollFrequencyID = entity.PayrollFrequencyID,
                FrequencyName = entity.FrequencyName,
            });
        }
    }
}