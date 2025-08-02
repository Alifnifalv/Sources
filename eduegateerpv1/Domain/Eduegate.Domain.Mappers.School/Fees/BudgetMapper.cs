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
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class BudgetMapper : DTOEntityDynamicMapper
    {
        public static BudgetMapper Mapper(CallContext context)
        {
            var mapper = new BudgetMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<BudgetDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private BudgetDTO ToDTO(long IID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Budgets.AsNoTracking().FirstOrDefault(a => a.BudgetID == IID);

                var dto = new BudgetDTO()
                {
                    BudgetID = entity.BudgetID,
                    BudgetCode = entity.BudgetCode,
                    BudgetName = entity.BudgetName,
                    IsActive = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as BudgetDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Budget()
            {
                BudgetID = toDto.BudgetID,
                BudgetCode = toDto.BudgetCode,
                BudgetName = toDto.BudgetName,
                IsActive = toDto.IsActive,
                CreatedBy = toDto.BudgetID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.BudgetID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.BudgetID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.BudgetID > 0 ? DateTime.Now : dto.UpdatedDate,
            };


            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.BudgetID == 0)
                {
                    var maxGroupID = dbContext.Budgets.Max(a => (int?)a.BudgetID);                  
                    entity.BudgetID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Budgets.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Budgets.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

            }

            return GetEntity(entity.BudgetID);
        }

    }
}