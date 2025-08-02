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
    public class FeeMasterMapper : DTOEntityDynamicMapper
    {
        public static FeeMasterMapper Mapper(CallContext context)
        {
            var mapper = new FeeMasterMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FeeMasters.Where(x => x.FeeMasterID == IID)
                    .Include(i => i.FeeType)
                    .Include(i => i.FeeCycle)
                    .Include(i => i.Account)
                    .Include(i => i.Account1)
                    .Include(i => i.Account2)
                    .Include(i => i.Account3)
                    .Include(i => i.Account4)
                    .Include(i => i.Account5)
                    .Include(i => i.Account6)
                    .Include(i => i.Account7)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new FeeMasterDTO()
                {
                    FeeMasterID = entity.FeeMasterID,
                    FeeTypeID = entity.FeeTypeID.HasValue ? entity.FeeTypeID : null,
                    Description=entity.Description != null ? entity.Description : null,
                    DueDate = entity.DueDate.HasValue ? entity.DueDate : null,
                    Amount = entity.Amount != null ? entity.Amount :null,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                    FeeCycleID=entity.FeeCycleID.HasValue ? entity.FeeCycleID : null,
                    LedgerAccountID=entity.LedgerAccountID.HasValue ? entity.LedgerAccountID : null,
                    TaxLedgerAccountID=entity.TaxLedgerAccountID.HasValue ? entity.TaxLedgerAccountID : null,
                    IsExternal = entity.IsExternal,
                    ReportName = entity.ReportName,
                    TaxPercentage=entity.TaxPercentage != null ? entity.TaxPercentage : null,
                    DueInDays = entity.DueInDays != null ? entity.DueInDays : null,
                    OSTaxPercentage = entity.OSTaxPercentage != null ? entity.OSTaxPercentage : null,
                    AdvanceTaxPercentage = entity.AdvanceTaxPercentage != null ? entity.AdvanceTaxPercentage : null,
                    IsActive = entity.IsActive,
                    TaxLedgerAccount = entity.TaxLedgerAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.TaxLedgerAccountID.ToString(),
                        Value = entity.Account1.AccountName
                    }:new KeyValueDTO(),
                    LedgerAccount = entity.LedgerAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.LedgerAccountID.ToString(),
                        Value = entity.Account.AccountName
                    } : new KeyValueDTO(),
                    AdvanceAccount = entity.AdvanceAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.AdvanceAccountID.ToString(),
                        Value = entity.Account3.AccountName
                    } : new KeyValueDTO(),
                    AdvanceTaxAccount = entity.AdvanceTaxAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.AdvanceTaxAccountID.ToString(),
                        Value = entity.Account5.AccountName
                    } : new KeyValueDTO(),
                    OutstandingAccount = entity.OutstandingAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.OutstandingAccountID.ToString(),
                        Value = entity.Account2.AccountName
                    } : new KeyValueDTO(),
                    OSTaxAccount = entity.OSTaxAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.OSTaxAccountID.ToString(),
                        Value = entity.Account4.AccountName
                    } : new KeyValueDTO(),
                    ProvisionforAdvanceAccount = entity.ProvisionforAdvanceAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.ProvisionforAdvanceAccountID.ToString(),
                        Value = entity.Account6.AccountName
                    } : new KeyValueDTO(),
                    ProvisionforOutstandingAccount = entity.ProvisionforOutstandingAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.ProvisionforOutstandingAccountID.ToString(),
                        Value = entity.Account7.AccountName
                    } : new KeyValueDTO(),
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeMasterDTO;
            if (toDto.LedgerAccountID == null || toDto.LedgerAccountID == 0)
            {
                throw new Exception("Please select Ledger Account ID!");
            }
            if (toDto.TaxPercentage >= 100)
            {
                throw new Exception("Enter Tax Percentage Below 100% !");
            }
            //convert the dto to entity and pass to the repository.
            var entity = new FeeMaster()
            {
                FeeMasterID = toDto.FeeMasterID,
                FeeTypeID = toDto.FeeTypeID,
                Description = toDto.Description,
                DueDate = toDto.DueDate,
                Amount = toDto.Amount,
                CreatedBy = toDto.FeeMasterID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.FeeMasterID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.FeeMasterID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.FeeMasterID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                FeeCycleID = toDto.FeeCycleID,
                LedgerAccountID=toDto.LedgerAccountID,
                TaxLedgerAccountID=toDto.TaxLedgerAccountID,
                TaxPercentage = toDto.TaxPercentage,
                DueInDays = toDto.DueInDays,
                AdvanceTaxAccountID = toDto.AdvanceTaxAccountID,
                AdvanceAccountID = toDto.AdvanceAccountID,
                AdvanceTaxPercentage = toDto.AdvanceTaxPercentage,
                OSTaxAccountID = toDto.OSTaxAccountID,
                OutstandingAccountID = toDto.OutstandingAccountID,
                ProvisionforAdvanceAccountID= toDto.ProvisionforAdvanceAccountID,
                ProvisionforOutstandingAccountID = toDto.ProvisionforOutstandingAccountID,
                OSTaxPercentage = toDto.OSTaxPercentage,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                IsExternal = toDto.IsExternal,
                ReportName = toDto.ReportName,
                IsActive = toDto.IsActive,
            };
          // if(toDto.FeeMasterID > 0)
          //  {
          //      entity.UpdatedDate = DateTime.Now;
          //      entity.UpdatedBy = (int)_context.LoginID;
          //  }
         //  else
         //   {
          //      entity.CreatedDate = DateTime.Now;
          //      entity.CreatedBy = (int)_context.LoginID;
           // }

            using (var dbContext = new dbEduegateSchoolContext())
            {               
                if (entity.FeeMasterID == 0)
                {                   
                    var maxGroupID = dbContext.FeeMasters.Max(a => (int?)a.FeeMasterID);
                    entity.FeeMasterID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.FeeMasters.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FeeMasters.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }
            return GetEntity(entity.FeeMasterID);
        }
    }
}