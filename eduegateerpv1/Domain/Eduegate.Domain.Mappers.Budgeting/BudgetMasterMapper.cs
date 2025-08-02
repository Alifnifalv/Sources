using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Budgeting;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Eduegate.Domain.Entity.Budgeting;
using Eduegate.Domain.Entity.Budgeting.Models;
using System.Globalization;

namespace Eduegate.Domain.Mappers.Budgeting
{
    public class BudgetMasterMapper : DTOEntityDynamicMapper
    {
        public static BudgetMasterMapper Mapper(CallContext context)
        {
            var mapper = new BudgetMasterMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<BudgetMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public BudgetMasterDTO ToDTO(long IID)
        {
            using (dbEduegateBudgetingContext dbContext = new dbEduegateBudgetingContext())
            {
                var entity = dbContext.Budgets1
                    .Where(a => a.BudgetID == IID)
                    .Include(i => i.Currency)
                    .Include(i => i.Department)
                    .Include(i => i.BudgetStatus)
                    .Include(i => i.BudgetType)
                    .Include(i => i.BudgetGroup)
                    .Include(i => i.Company)
                    .Include(i => i.FinancialYear)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private BudgetMasterDTO ToDTO(Budget1 entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var dto = new BudgetMasterDTO()
            {
                BudgetID = entity.BudgetID,
                BudgetCode = entity.BudgetCode,
                BudgetName = entity.BudgetName,
                CurrencyID = entity.CurrencyID,
                BudgetStatusID = entity.BudgetStatusID,
                BudgetTypeID = entity.BudgetTypeID,
                BudgetType = entity.BudgetTypeID.HasValue ? entity.BudgetType?.BudgetTypeName : null,
                BudgetGroupID = entity.BudgetGroupID,
                DepartmentID = entity.DepartmentID,
                CompanyID = entity.CompanyID,
                FinancialYearID = entity.FinancialYearID,
                Currency = entity.CurrencyID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.CurrencyID.ToString(),
                    Value = entity.Currency.Name
                } : new KeyValueDTO(),
                Department = entity.DepartmentID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.DepartmentID.ToString(),
                    Value = entity.Department.DepartmentName
                } : new KeyValueDTO() { Key = "", Value = "All Departments" },
                PeriodStart = entity.PeriodStart,
                PeriodEnd = entity.PeriodEnd,
                PeriodStartDateString = entity.PeriodStart.HasValue ? entity.PeriodStart.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                PeriodEndDateString = entity.PeriodEnd.HasValue ? entity.PeriodEnd.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                Remarks = entity.Remarks,
                BudgetTotalValue = entity.BudgetTotalValue,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            return dto;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as BudgetMasterDTO;
            var entity = new Budget1()
            {
                BudgetID = toDto.BudgetID,
                Remarks = toDto.Remarks,
                BudgetCode = toDto.BudgetCode,
                BudgetName = toDto.BudgetName,
                PeriodStart = toDto.PeriodStart,
                PeriodEnd = toDto.PeriodEnd,
                BudgetTypeID = toDto.BudgetTypeID,
                BudgetGroupID = toDto.BudgetGroupID,
                BudgetStatusID = toDto.BudgetStatusID,
                CompanyID = toDto.CompanyID,
                FinancialYearID = toDto.FinancialYearID,
                BudgetTotalValue = toDto.BudgetTotalValue,
                CreatedDate = toDto.BudgetID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.BudgetID > 0 ? DateTime.Now : dto.UpdatedDate,
                CreatedBy = toDto.BudgetID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.BudgetID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CurrencyID = toDto.Currency != null ? int.Parse(toDto.Currency.Key) : null,
                DepartmentID = toDto.Department != null && toDto.Department.Key != null && toDto.Department.Key != string.Empty ? long.Parse(toDto.Department.Key) : null,
            };


            using (var dbContext = new dbEduegateBudgetingContext())
            {

                if (entity.BudgetID == 0)
                {
                    var maxGroupID = dbContext.Budgets1.Max(a => (int?)a.BudgetID);
                    entity.BudgetID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Budgets1.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Budgets1.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

            }

            return GetEntity(entity.BudgetID);
        }

        public BudgetMasterDTO GetBudgetDetailsByID(int budgetID)
        {
            var budgetDTO = new BudgetMasterDTO();

            using (var dbContext = new dbEduegateBudgetingContext())
            {
                var entity = dbContext.Budgets1
                    .Where(a => a.BudgetID == budgetID)
                    .Include(i => i.Currency)
                    .Include(i => i.Department)
                    .Include(i => i.BudgetStatus)
                    .Include(i => i.BudgetType)
                    .Include(i => i.BudgetGroup)
                    .AsNoTracking()
                    .FirstOrDefault();

                budgetDTO = ToDTO(entity);
            }

            return budgetDTO;
        }

    }
}