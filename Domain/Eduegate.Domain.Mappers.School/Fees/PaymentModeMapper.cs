using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Fees;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class PaymentModeMapper : DTOEntityDynamicMapper
    {
        public static PaymentModeMapper Mapper(CallContext context)
        {
            var mapper = new PaymentModeMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<PaymentModeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private PaymentModeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.PaymentModes.Where(x => x.PaymentModeID == IID)
                    .Include(i => i.Account)
                    .Include(i => i.TenderType)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new PaymentModeDTO()
                {
                    PaymentModeID = entity.PaymentModeID,
                    PaymentModeName = entity.PaymentModeName,
                    Account = entity.AccountId.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.AccountId.ToString(),
                        Value = entity.Account.AccountName
                    } : null,
                    TenderType = entity.TenderTypeID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.TenderTypeID.ToString(),
                        Value = entity.TenderType.TenderTypeName
                    } : null,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PaymentModeDTO;
            if (toDto.AccountId == null || toDto.AccountId == 0)
            {
                throw new Exception("Please Select Account");
            }
            if (toDto.TenderTypeID == null || toDto.TenderTypeID == 0)
            {
                throw new Exception("Please Select Tender Type");
            }
            //convert the dto to entity and pass to the repository.
            var entity = new PaymentMode()
            {
                PaymentModeID = toDto.PaymentModeID,
                PaymentModeName = toDto.PaymentModeName,
                AccountId = toDto.AccountId,
                TenderTypeID = toDto.TenderTypeID,
            };
            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.PaymentModeID == 0)
                {                 
                    var maxGroupID = dbContext.PaymentModes.Max(a => (int?)a.PaymentModeID);
                    entity.PaymentModeID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.PaymentModes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.PaymentModes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.PaymentModeID));
        }
    }
}