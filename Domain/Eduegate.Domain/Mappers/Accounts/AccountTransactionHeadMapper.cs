using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Accounts.Taxes;
using System;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class AccountTransactionHeadMapper : DTOEntityDynamicMapper
    {
        public static AccountTransactionHeadMapper Mapper(CallContext context)
        {
            var mapper = new AccountTransactionHeadMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AccountTransactionHeadDTO>(entity);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AccountTransactionHeadDTO;
            var entity = ToEntity(toDto);
            var repository = new AccountTransactionRepository();
            var returnEntity = repository.SaveAccountTransactionHead(entity);
            return GetEntity(returnEntity.AccountTransactionHeadIID);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            dto = dto as AccountTransactionHeadDTO;
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public AccountTransactionHeadDTO ToDTO(AccountTransactionHead headEntity)
        {
            var headDTO = new AccountTransactionHeadDTO();

            if (headEntity == null)
            {
                return headDTO;
            }
            headDTO.AccountTransactionHeadIID = headEntity.AccountTransactionHeadIID;
            headDTO.CreatedBy = headEntity.CreatedBy;
            headDTO.CreatedDate = headEntity.CreatedDate;
            headDTO.DocumentStatusID = headEntity.DocumentStatusID;
            headDTO.DocumentTypeID = headEntity.DocumentTypeID;
            headDTO.DocumentReferenceTypeID = (Eduegate.Services.Contracts.Enums.DocumentReferenceTypes?)(DocumentReferenceTypes?)headEntity.DocumentType.ReferenceTypeID;
            headDTO.Remarks = headEntity.Remarks;
            headDTO.UpdatedBy = headEntity.UpdatedBy;
            headDTO.UpdatedDate = headEntity.UpdatedDate;
            headDTO.TransactionDate = headEntity.TransactionDate;
            headDTO.TransactionNumber = headEntity.TransactionNumber;
            headDTO.PaymentModeID = headEntity.PaymentModeID;
            headDTO.ExchangeRate = headEntity.ExchangeRate;
            headDTO.CurrencyID = headEntity.CurrencyID;
            headDTO.CostCenterID = headEntity.CostCenterID;
            headDTO.UpdatedDate = headEntity.UpdatedDate;
            headDTO.AmountPaid = headEntity.AmountPaid;
            headDTO.AccountID = headEntity.AccountID.HasValue ? headEntity.AccountID : null;
            headDTO.DiscountAmount = headEntity.DiscountAmount;
            headDTO.DiscountPercentage = headEntity.DiscountPercentage;
            headDTO.CompanyID = headEntity.CompanyID;
            headDTO.BranchID = headEntity.BranchID;
            headDTO.TransactionStatusID = headEntity.TransactionStatusID;
            headDTO.ChequeNumber = headEntity.ChequeNumber;
            headDTO.ChequeDate = headEntity.ChequeDate;
            headDTO.Reference = headEntity.Reference;
            if (headEntity.BranchID != null)
            {
                headDTO.BranchID = headEntity.BranchID;
                headDTO.BranchName = headEntity.BranchID.ToString();
            }

            if (headEntity.Account != null)
            {
                headDTO.AccountCode = headEntity.Account.AccountCode;
                headDTO.AccountAlias = headEntity.Account.Alias;
            }

            if (headEntity.DocumentStatus != null)
            {
                headDTO.DocumentStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.DocumentStatusID.ToString(), Value = headEntity.DocumentStatus.StatusName };
            }

            if (headEntity.DocumentStatusID != null)
            {
                headDTO.DocumentType = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.DocumentTypeID.ToString(), Value = headEntity.DocumentType.TransactionTypeName };
            }

            if (headEntity.DocumentType != null)
            {
                headDTO.DocumentType = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.DocumentTypeID.ToString(), Value = headEntity.DocumentType.TransactionTypeName };
            }
            if (headEntity.TransactionStatus != null)
            {
                headDTO.TransactionStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.TransactionStatusID.ToString(), Value = headEntity.TransactionStatus.Description };
            }
            if (headEntity.PaymentModeID.HasValue && headEntity.PaymentModeID != 0)
            {
                if (headEntity.PaymentMode != null)
                {
                    headDTO.PaymentModes = new KeyValueDTO() { Key = headEntity.PaymentModeID.ToString(), Value = headEntity.PaymentMode.PaymentModeName };
                }
            }
            if (headEntity.Currency != null)
            {
                headDTO.Currency = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.CurrencyID.ToString(), Value = headEntity.Currency.Name };
            }
            if (headEntity.CostCenterID.HasValue && headEntity.CostCenterID != 0)
            {
                if (headEntity.CostCenter != null)
                {
                    headDTO.CostCenter = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.CostCenterID.ToString(), Value = headEntity.CostCenter.CostCenterName };
                }
            }

            if (headEntity.Account != null)
            {
                headDTO.Account = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.AccountID.ToString(), Value = headEntity.Account.AccountName };
            }

            if (headEntity.Account != null)
            {
                headDTO.DetailAccount = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.AccountID.ToString(), Value = headEntity.Account.AccountName };
            }

            headDTO.AccountID = headEntity.AccountID;
            headDTO.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            headDTO.TaxDetails = new List<TaxDetailsDTO>();

            if (headEntity.AccountTaxTransactions != null)
            {
                foreach (var tax in headEntity.AccountTaxTransactions)
                {
                    headDTO.TaxDetails.Add(new TaxDetailsDTO()
                    {
                        AccountID = tax.AccoundID,
                        Amount = tax.Amount,
                        Percentage = tax.Percentage,
                        TaxID = tax.TaxTransactionIID,
                        TaxName = tax.TaxTemplate != null ? tax.TaxTemplate.TemplateName : string.Empty,
                        TaxTemplateID = tax.TaxTemplateID,
                        TaxTemplateItemID = tax.TaxTemplateItemID,
                        TaxTypeID = tax.TaxTypeID
                    });
                }
            }

            decimal? amount = 0;
            decimal? taxAmount = 0;

            foreach (var detailEntityItem in headEntity.AccountTransactionDetails)
            {
                var detailDTO = new AccountTransactionDetailsDTO();
                detailDTO.AccountTransactionDetailIID = detailEntityItem.AccountTransactionDetailIID;
                detailDTO.Amount = detailEntityItem.Amount;
                detailDTO.TaxAmount = detailEntityItem.TaxAmount;
                detailDTO.DiscountAmount = detailEntityItem.DiscountAmount;
                taxAmount += detailEntityItem.TaxAmount;
                amount += detailEntityItem.Amount;
                detailDTO.UpdatedBy = detailEntityItem.UpdatedBy;
                detailDTO.UpdatedDate = detailEntityItem.UpdatedDate;
                detailDTO.AccountID = detailEntityItem.AccountID;
                detailDTO.CreatedBy = detailEntityItem.CreatedBy;
                detailDTO.CreatedDate = detailEntityItem.CreatedDate;
                detailDTO.CostCenterID = detailEntityItem.CostCenterID;
                detailDTO.ReferenceNumber = detailEntityItem.ReferenceNumber;
                detailDTO.ReturnAmount = detailEntityItem.ReturnAmount;
                detailDTO.InvoiceAmount = detailEntityItem.InvoiceAmount;
                detailDTO.PaidAmount = detailEntityItem.PaidAmount;
                detailDTO.Remarks = detailEntityItem.Remarks;
                detailDTO.PaymentDueDate = detailEntityItem.PaymentDueDate;
                detailDTO.ExchangeRate = detailEntityItem.ExchangeRate;
                detailDTO.CurrencyID = detailEntityItem.CurrencyID;
                detailDTO.AccountID = detailEntityItem.AccountID;
                detailDTO.Remarks = detailEntityItem.Remarks;
                detailDTO.ExternalReference1 = detailEntityItem.ExternalReference1;
                detailDTO.ExternalReference2 = detailEntityItem.ExternalReference2;
                detailDTO.ExternalReference3 = detailEntityItem.ExternalReference3;
                detailDTO.ReferenceQuantity = detailEntityItem.ReferenceQuantity;
                detailDTO.ReferenceRate = detailEntityItem.ReferenceRate;

                if (detailEntityItem.ProductSKUMap != null)
                {
                    detailDTO.ProductSKUCode = detailEntityItem.ProductSKUMap.ProductSKUCode;
                }

                detailDTO.AvailableQuantity = detailEntityItem.AvailableQuantity;
                detailDTO.CurrentAvgCost = detailEntityItem.CurrentAvgCost;
                detailDTO.NewAvgCost = detailEntityItem.NewAvgCost;

                detailDTO.AccountTypeID = detailEntityItem.AccountType;

                detailDTO.InvoiceNumber = detailEntityItem.InvoiceNumber;
                detailDTO.UnPaidAmount = detailEntityItem.UnPaidAmount;
                detailDTO.JobMissionNumber = detailEntityItem.JobMissionNumber;
                detailDTO.TaxTemplateID = detailEntityItem.TaxTemplateID;
                detailDTO.TaxPercentage = detailEntityItem.TaxPercentage;
                detailDTO.ReferencePaymentID = detailEntityItem.ReferencePaymentID;
                detailDTO.ReferenceReceiptID = detailEntityItem.ReferenceReceiptID;
                detailDTO.ReceivableID = detailEntityItem.ReferenceReceiptID;
                detailDTO.SubLedgerID = detailEntityItem.SubLedgerID;
                if (detailEntityItem.Accounts_SubLedger != null)
                {
                    detailDTO.SubLedger = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = detailEntityItem.SubLedgerID.ToString(), Value = detailEntityItem.Accounts_SubLedger.SL_AccountName };
                }

                if (detailEntityItem.CostCenterID.HasValue && detailEntityItem.CostCenterID != 0)
                {
                    if (detailEntityItem.CostCenter != null)
                    {
                        detailDTO.CostCenter = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = detailEntityItem.CostCenterID.ToString(), Value = detailEntityItem.CostCenter.CostCenterName };
                    }
                }
                if (detailEntityItem.Account != null)
                {
                    detailDTO.Account = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = detailEntityItem.AccountID.ToString(), Value = detailEntityItem.Account.AccountName };
                }
                if (detailEntityItem.ProductSKUMap != null)
                {
                    detailDTO.SKUID = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = detailEntityItem.ProductSKUId.ToString(), Value = detailEntityItem.ProductSKUMap.PartNo + '-' + detailEntityItem.ProductSKUMap.SKUName };
                }
                if (detailEntityItem.Currency != null)
                {
                    detailDTO.Currency = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = detailEntityItem.Currency.CurrencyID.ToString(), Value = detailEntityItem.Currency.Name };
                }
                if (detailEntityItem.BudgetID != null)
                {
                    detailDTO.Budget = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = detailEntityItem.BudgetID.ToString(), Value = detailEntityItem.Budget.BudgetName };
                }
                //Set Account Object
                if (detailEntityItem.AccountID.HasValue)
                {
                    detailDTO.Account = new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                    {
                        Key = detailEntityItem.Account.AccountID.ToString(),
                        Value = detailEntityItem.Account.AccountName,
                    };
                }

                if (detailEntityItem.AccountID.HasValue)
                {
                    detailDTO.AccountGroupID = AccountGroupDetail(detailEntityItem.AccountID).AccountGroupID;
                    detailDTO.AccountGroup = AccountGroupDetail(detailEntityItem.AccountID).AccountGroup;
                }

                headDTO.AccountTransactionDetails.Add(detailDTO);
            }

            headDTO.Amount = amount;
            headDTO.DiscountAmount = headEntity.DiscountAmount;
            headDTO.TaxAmount = taxAmount;

            if (taxAmount != 0 && headDTO.TaxDetails.Count == 0) // take the tax from detail and put it
            {
                headDTO.TaxDetails.Add(
                    new TaxDetailsDTO()
                    {
                        ExclusiveTaxAmount = taxAmount
                    }
                    );
            }

            return headDTO;
        }

        public AccountTransactionHead ToEntity(AccountTransactionHeadDTO headDto)
        {
            var entity = new AccountTransactionHead();

            entity.CreatedBy = headDto.CreatedBy;
            entity.CreatedDate = headDto.CreatedDate;
            entity.DocumentTypeID = headDto.DocumentTypeID;
            entity.Remarks = headDto.Remarks;
            entity.UpdatedBy = headDto.UpdatedBy;
            entity.UpdatedDate = headDto.UpdatedDate;
            entity.AccountID = headDto.AccountID;
            entity.AdvanceAmount = headDto.AdvanceAmount;
            entity.AmountPaid = headDto.AmountPaid;
            entity.CostCenterID = headDto.CostCenterID;
            entity.CurrencyID = headDto.CurrencyID;
            entity.DocumentStatusID = (int)headDto.DocumentStatusID;
            entity.ExchangeRate = headDto.ExchangeRate;
            entity.PaymentModeID = headDto.PaymentModeID;
            entity.TransactionStatusID = headDto.TransactionStatusID;
            entity.AccountTransactionHeadIID = headDto.AccountTransactionHeadIID;
            entity.TransactionDate = headDto.TransactionDate;
            entity.TransactionNumber = headDto.TransactionNumber;
            entity.AccountTransactionDetails = new List<AccountTransactionDetail>();
            entity.AccountTaxTransactions = new List<AccountTaxTransaction>();
            entity.TaxAmount = headDto.TaxAmount;
            entity.DiscountAmount = headDto.DiscountAmount;
            entity.DiscountPercentage = headDto.DiscountPercentage;
            entity.CompanyID = _context.CompanyID;
            entity.BranchID = headDto.BranchID;
            entity.ChequeNumber = headDto.ChequeNumber;
            entity.ChequeDate = headDto.ChequeDate;
            entity.Reference = headDto.Reference;
            if (headDto.TaxDetails != null)
            {
                foreach (var tax in headDto.TaxDetails)
                {
                    entity.AccountTaxTransactions.Add(new AccountTaxTransaction()
                    {
                        AccoundID = tax.AccountID,
                        Amount = tax.Amount,
                        Percentage = tax.Percentage,
                        TaxTransactionIID = tax.TaxID,
                        TaxTemplateID = tax.TaxTemplateID,
                        TaxTemplateItemID = tax.TaxTemplateItemID,
                        TaxTypeID = tax.TaxTypeID
                    });
                }
            }

            foreach (AccountTransactionDetailsDTO detailDTOItem in headDto.AccountTransactionDetails)
            {
                var detailEntity = new AccountTransactionDetail();

                detailEntity.Amount = detailDTOItem.Amount;
                detailEntity.UpdatedBy = detailDTOItem.UpdatedBy;
                detailEntity.UpdatedDate = detailDTOItem.UpdatedDate;
                detailEntity.AccountID = detailDTOItem.AccountID;
                detailEntity.CreatedBy = detailDTOItem.CreatedBy;
                detailEntity.CreatedDate = detailDTOItem.CreatedDate;
                detailEntity.Remarks = detailDTOItem.Remarks;
                detailEntity.ReferenceNumber = detailDTOItem.ReferenceNumber;
                detailEntity.CostCenterID = detailDTOItem.CostCenterID;
                detailEntity.Amount = detailDTOItem.Amount;
                detailEntity.AccountTransactionHeadID = detailDTOItem.AccountTransactionHeadID;
                detailEntity.AccountTransactionDetailIID = detailDTOItem.AccountTransactionDetailIID;
                detailEntity.AccountID = detailDTOItem.AccountID;
                detailEntity.InvoiceAmount = detailDTOItem.InvoiceAmount;
                detailEntity.ReturnAmount = detailDTOItem.ReturnAmount;
                detailEntity.PaidAmount = detailDTOItem.PaidAmount;
                detailEntity.PaymentDueDate = detailDTOItem.PaymentDueDate;
                detailEntity.CurrencyID = detailDTOItem.CurrencyID;
                detailEntity.ExchangeRate = detailDTOItem.ExchangeRate;
                detailEntity.CurrentAvgCost = detailDTOItem.CurrentAvgCost;
                detailEntity.AvailableQuantity = detailDTOItem.AvailableQuantity;
                detailEntity.NewAvgCost = detailDTOItem.NewAvgCost;
                detailEntity.ProductSKUId = detailDTOItem.ProductSKUId;
                detailEntity.AccountType = detailDTOItem.AccountTypeID;
                detailEntity.InvoiceNumber = detailDTOItem.InvoiceNumber;
                detailEntity.UnPaidAmount = detailDTOItem.UnPaidAmount;
                detailEntity.JobMissionNumber = detailDTOItem.JobMissionNumber;
                detailEntity.TaxPercentage = detailDTOItem.TaxPercentage;
                detailEntity.TaxTemplateID = detailDTOItem.TaxTemplateID;
                detailEntity.ReferenceReceiptID = detailDTOItem.ReferenceReceiptID;
                detailEntity.ReferencePaymentID = detailDTOItem.ReferencePaymentID;
                detailEntity.Remarks = detailDTOItem.Remarks;
                detailEntity.ExternalReference1 = detailDTOItem.ExternalReference1;
                detailEntity.ExternalReference2 = detailDTOItem.ExternalReference2;
                detailEntity.ExternalReference3 = detailDTOItem.ExternalReference3;
                detailEntity.ReferenceRate = detailDTOItem.ReferenceRate;
                detailEntity.ReferenceQuantity = detailDTOItem.ReferenceQuantity;
                detailEntity.TaxAmount = detailDTOItem.TaxAmount;
                detailEntity.TaxPercentage = detailDTOItem.TaxPercentage;
                detailEntity.TaxTemplateID = detailDTOItem.TaxTemplateID;
                detailEntity.SubLedgerID = detailDTOItem.SubLedgerID;
                detailEntity.BudgetID = detailDTOItem.BudgetID;
                if (detailEntity.ProductSKUMap != null)
                {
                    detailEntity.ProductSKUMap.ProductSKUCode = detailDTOItem.ProductSKUCode;
                }

                entity.AccountTransactionDetails.Add(detailEntity);
            }

            return entity;
        }

        public override string GetEntity(long IID)
        {
            var repository = new AccountTransactionRepository();
            var returnEntity = repository.GetAccountTransactionHeadById(IID);

            return ToDTOString(ToDTO(returnEntity));
        }
        //public List<AccountTransactionHeadDTO> GetAccountTransactionHeadByCostCenterID(int CostCenterID)
        //{
        //    var repository = new AccountTransactionRepository();
        //    var returnEntity = repository.GetAccountTransactionHeadByCostCenterID(CostCenterID);



        //    return ToDTOString(ToDTO(returnEntity));
        //}

        private AccountTransactionDetailsDTO AccountGroupDetail(long? accountID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var dtos = new AccountTransactionDetailsDTO();

                var accountDetail = dbContext.Accounts.Where(X => X.AccountID == accountID)
                    .Include(i => i.Group)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (accountDetail != null)
                {
                    var groupDetail = accountDetail.Group;

                    if (groupDetail != null)
                    {
                        dtos.AccountGroupID = groupDetail.GroupID;
                        dtos.AccountGroup = new KeyValueDTO() { Key = groupDetail.GroupID.ToString(), Value = groupDetail.GroupName };
                    }
                }

                return dtos;
            }
        }

        public List<KeyValueDTO> GetAccountByPayementModeID(long paymentModeID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var accountList = new List<KeyValueDTO>();

                var paymentMode = dbContext.PaymentModes.Where(X => X.PaymentModeID == paymentModeID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var entities = paymentMode != null ? dbContext.Accounts.Where(X => X.AccountID == paymentMode.AccountId)
                    .AsNoTracking()
                    .ToList() : null;

                foreach (var account in entities)
                {
                    accountList.Add(new KeyValueDTO
                    {
                        Key = account.AccountID.ToString(),
                        Value = account.AccountName
                    });
                }

                return accountList;
            }
        }

        public List<KeyValueDTO> GetCustomerPendingInvoices(long accountID, long? branchID = null)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var settingValue = dbContext.Settings
                    .AsNoTracking()
                    .Where(s => s.SettingCode == "INVOICE_DOCUMENTTYPE_CREDITNOTE")
                    .Select(s => s.SettingValue)
                    .FirstOrDefault();

                var documentTypeIds = new List<long>();
                if (!string.IsNullOrWhiteSpace(settingValue))
                {
                    documentTypeIds = settingValue
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(val =>
                        {
                            if (long.TryParse(val.Trim(), out var id))
                                return id;
                            return (long?)null;
                        })
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();
                }

                if (!documentTypeIds.Any())
                    return new List<KeyValueDTO>();
               
                var rawResult = (from r in dbContext.Receivables.AsNoTracking()
                                 join ah in dbContext.AccountTransactionHeads.AsNoTracking()
                                     on r.TransactionNumber equals ah.TransactionNumber
                                 where r.AccountID == accountID
                                       && (!branchID.HasValue || ah.BranchID == branchID.Value)
                                       && documentTypeIds.Contains(ah.DocumentTypeID ?? 0)
                                       && (r.Amount ?? 0) > (r.PaidAmount ?? 0)
                                 select new
                                 {
                                     ah.AccountTransactionHeadIID,
                                     r.ReceivableIID,
                                     ah.TransactionNumber,
                                     Balance = (r.Amount ?? 0) - (r.PaidAmount ?? 0)
                                 }).ToList();

               
                var result = rawResult.Select(x => new KeyValueDTO
                {
                    Key = $"{x.AccountTransactionHeadIID}#{x.ReceivableIID}",
                    Value = $"{x.TransactionNumber} ({Math.Round(x.Balance, 2)})"
                }).ToList();

                return result;
            }
        }


        public List<KeyValueDTO> GetAccountGroupByAccountID(long accountID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var groupList = new List<KeyValueDTO>();

                //var account = dbContext.Accounts.Where(X => X.AccountID == accountID)
                //    .Include(i => i.Group)
                //    .AsNoTracking()
                //    .FirstOrDefault();

                //var entities = account != null ? dbContext.Groups.Where(g => g.GroupID == account.GroupID).ToList() : null;

                var entities = dbContext.Groups.Where(g => g.Accounts.Any(a => a.AccountID == accountID))
                    .Include(i => i.Accounts)
                    .AsNoTracking()
                    .ToList();

                foreach (var group in entities)
                {
                    groupList.Add(new KeyValueDTO
                    {
                        Key = group.GroupID.ToString(),
                        Value = group.GroupName
                    });
                }

                return groupList;
            }
        }

    }
}
