using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeePaymentModesMapper: DTOEntityDynamicMapper
    {
        public static FeePaymentModesMapper Mapper(CallContext context)
        {
            var mapper = new FeePaymentModesMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeePaymentModesDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FeePaymentModes.Where(x => x.PaymentModeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new FeePaymentModesDTO()
                {
                    PaymentModeID=(byte)entity.PaymentModeID,
                    PaymentModeName=entity.PaymentModeName
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeePaymentModesDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new FeePaymentMode()

            {
                PaymentModeID = toDto.PaymentModeID,
                PaymentModeName = toDto.PaymentModeName
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {               
                if (entity.PaymentModeID == 0)
                {
                   
                    var maxGroupID = dbContext.FeePaymentModes.Max(a => (int?)a.PaymentModeID);
                    entity.PaymentModeID = Convert.ToByte(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);
                    dbContext.FeePaymentModes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FeePaymentModes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new FeePaymentModesDTO()
            {
                PaymentModeID = (byte)(entity.PaymentModeID),
                PaymentModeName = entity.PaymentModeName
            });
        }
    }
}