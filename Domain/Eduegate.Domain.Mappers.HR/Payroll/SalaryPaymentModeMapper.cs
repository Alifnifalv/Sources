using Eduegate.Domain.Entity;
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
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class SalaryPaymentModeMapper : DTOEntityDynamicMapper
    {
        public static SalaryPaymentModeMapper Mapper(CallContext context)
        {
            var mapper = new SalaryPaymentModeMapper();
            mapper._context = context;
            return mapper;
        }
        public List<SalaryPaymentModeDTO> ToDTO(List<SalaryPaymentMode> entities)
        {
            var dtos = new List<SalaryPaymentModeDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public SalaryPaymentModeDTO ToDTO(SalaryPaymentMode entity)
        {
            return new SalaryPaymentModeDTO()
            {
                SalaryPaymentModeID = entity.SalaryPaymentModeID,
                PaymentName = entity.PaymentName,
                PyamentModeTypeID = entity.PyamentModeTypeID,

            };
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalaryPaymentModeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as SalaryPaymentModeDTO);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.SalaryPaymentModes.Where(X => X.SalaryPaymentModeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new SalaryPaymentModeDTO()
                {
                    SalaryPaymentModeID = entity.SalaryPaymentModeID,
                    PaymentName = entity.PaymentName,
                    PyamentModeTypeID = entity.PyamentModeTypeID,
                });
            }
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {


            var toDto = dto as SalaryPaymentModeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new SalaryPaymentMode()
            {
                SalaryPaymentModeID = toDto.SalaryPaymentModeID,
                PaymentName = toDto.PaymentName,
                PyamentModeTypeID = toDto.PyamentModeTypeID,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {               
                if (entity.SalaryPaymentModeID == 0)
                {                   
                    var maxGroupID = dbContext.SalaryPaymentModes.Max(a => (int?)a.SalaryPaymentModeID);                   
                    entity.SalaryPaymentModeID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.SalaryPaymentModes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.SalaryPaymentModes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new SalaryPaymentModeDTO()
            {
                SalaryPaymentModeID = entity.SalaryPaymentModeID,
                PaymentName = entity.PaymentName,
                PyamentModeTypeID = entity.PyamentModeTypeID,
            });
        }
    }
}