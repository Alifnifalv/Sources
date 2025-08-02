using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeDiscountMapper : DTOEntityDynamicMapper
    {
        public static FeeDiscountMapper Mapper(CallContext context)
        {
            var mapper = new FeeDiscountMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeDiscountDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FeeDiscounts.Where(x => x.FeeDiscountID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new FeeDiscountDTO()
                {
                    FeeDiscountID = entity.FeeDiscountID,
                    DiscountCode = entity.DiscountCode,
                    DiscountPercentage = entity.DiscountPercentage,
                    Amount = entity.Amount,
                    Description = entity.Description,
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeDiscountDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new FeeDiscount()
            {
                FeeDiscountID = toDto.FeeDiscountID,
                DiscountCode = toDto.DiscountCode,
                DiscountPercentage = toDto.DiscountPercentage,
                Amount = toDto.Amount,
                Description = toDto.Description,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                
                if (entity.FeeDiscountID == 0)
                {
                   
                    var maxGroupID = dbContext.FeeDiscounts.Max(a => (int?)a.FeeDiscountID);
                    entity.FeeDiscountID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.FeeDiscounts.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FeeDiscounts.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new FeeDiscountDTO()
            {
                FeeDiscountID = entity.FeeDiscountID,
                DiscountCode = entity.DiscountCode,
                DiscountPercentage = entity.DiscountPercentage,
                Amount = entity.Amount,
                Description = entity.Description,
            });
        }
    }
}