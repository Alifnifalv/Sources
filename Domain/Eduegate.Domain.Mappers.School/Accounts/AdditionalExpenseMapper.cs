using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Accounts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Accounts
{
    public class AdditionalExpenseMapper : DTOEntityDynamicMapper
    {
        public static AdditionalExpenseMapper Mapper(CallContext context)
        {
            var mapper = new AdditionalExpenseMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AdditionalExpensDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AdditionalExpensDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.AdditionalExpenses.Where(x => x.AdditionalExpenseID == IID).AsNoTracking().FirstOrDefault();

                var AdditionalExpensesDTO = new AdditionalExpensDTO()
                {
                    AdditionalExpenseID = entity.AdditionalExpenseID,
                    AdditionalExpenseCode = entity.AdditionalExpenseCode,
                    AdditionalExpenseName = entity.AdditionalExpenseName,
                    AccountID = entity.AccountID,
                };

                return AdditionalExpensesDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AdditionalExpensDTO;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new AdditionalExpens()
                {
                    AdditionalExpenseID = toDto.AdditionalExpenseID,
                    AdditionalExpenseCode = toDto.AdditionalExpenseCode,
                    AdditionalExpenseName = toDto.AdditionalExpenseName,
                    AccountID = toDto.AccountID,
                };

                if (entity.AdditionalExpenseID == 0)
                {
                    var maxGroupID = dbContext.AdditionalExpenses.OrderByDescending(a => a.AdditionalExpenseID).AsNoTracking().FirstOrDefault()?.AdditionalExpenseID;
                    entity.AdditionalExpenseID = (byte)(maxGroupID == null || maxGroupID == 0 ? 1 : byte.Parse(maxGroupID.ToString()) + 1);

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.AdditionalExpenseID));
            }
        }

    }
}
