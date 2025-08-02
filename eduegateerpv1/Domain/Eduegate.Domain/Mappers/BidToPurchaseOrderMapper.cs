using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers
{
    public class BidToPurchaseOrderMapper : DTOEntityDynamicMapper
    {
        public static BidToPurchaseOrderMapper Mapper(CallContext context)
        {
            var mapper = new BidToPurchaseOrderMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TransactionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public TransactionDTO ToDTO(long IID)
        {
            var dto = new TransactionDTO();
            using (var dbContext = new dbEduegateERPContext())
            {
                var transHead = dbContext.TransactionHeads.Where(h => h.HeadIID == IID)
                    .Include(i => i.Employee)
                    .Include(i => i.Branch)
                    .Include(i => i.TransactionDetails).ThenInclude(i => i.ProductSKUMap)
                    .AsNoTracking().FirstOrDefault();

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TransactionDTO;

            if (toDto.TransactionHead.HeadIID != 0)
            {
                throw new Exception("Transaction already saved.");
            }

            using (var dbContext = new dbEduegateERPContext())
            {
                if (toDto.TransactionHead.TransactionDate.HasValue)
                {
                    if (toDto.TransactionHead.HeadIID == 0 && toDto.TransactionHead.TransactionDate.Value.Date < DateTime.Now.Date)
                    {
                        throw new Exception("Date must be greater than or equal to today's date");
                    }

                    if (toDto.TransactionHead.DueDate.HasValue)
                    {
                        if (toDto.TransactionHead.TransactionDate.Value.Date > toDto.TransactionHead.DueDate.Value.Date)
                        {
                            throw new Exception("Please select the Due/Valid date properly");
                        }
                    }
                }

                var toSaveList = toDto.TransactionDetails?.ToList();

                if (toSaveList == null)
                {
                    throw new Exception("There is no items to save !");
                }


                var groupedQuotations = toSaveList.GroupBy(x => x.HeadID).ToList();

                List<KeyValueDTO> quotationIDs = groupedQuotations
                    .SelectMany(group => group.Select(item => new KeyValueDTO
                    {
                        Key = group.Key.ToString(), // Use group.Key to access the HeadID  
                        Value = item.QuotationNo
                    }))
                    .ToList();

                //Duplicate validation with Quotation
                foreach (var qt in quotationIDs)
                {
                    var transactions = dbContext.TransactionHeads.FirstOrDefault(x => x.ReferenceHeadID == long.Parse(qt.Key));

                    if (transactions != null)
                    {
                        throw new Exception("Already have purchase order "+transactions.TransactionNo + " against the quotation "+ qt.Value);
                    }
                }

                var entity = new TransactionHead();

                try
                {
                    var documentType = dbContext.DocumentTypes.Where(d => d.TransactionNoPrefix == "PORD-")
                        .AsNoTracking().FirstOrDefault();

                    var groupedBySupplier = toSaveList.GroupBy(item => item.SupplierID);

                    foreach (var group in groupedBySupplier)
                    {
                        var incrLastNo = documentType.LastTransactionNo + 1;

                        entity = new TransactionHead()
                        {
                            CompanyID = 1,
                            BranchID = _context.SchoolID,
                            SupplierID = group.Key,
                            Description = toDto.TransactionHead.Description,
                            DueDate = toDto.TransactionHead.DueDate,
                            DocumentTypeID = documentType.DocumentTypeID,
                            TransactionNo = documentType.TransactionNoPrefix + incrLastNo,
                            TransactionDate = DateTime.Now,
                            TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.InProcess,
                            CreatedBy = (int?)_context.LoginID,
                            UpdatedBy = (int?)_context.LoginID,
                            SchoolID = (byte?)_context.SchoolID,
                            AcademicYearID = _context.AcademicYearID,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            BidID = toDto.TransactionHead.BidID,
                        };

                        foreach (var listDta in group)
                        {
                            entity.TransactionDetails.Add(new TransactionDetail()
                            {
                                HeadID = entity.HeadIID,
                                ProductID = listDta.ProductID,
                                Remark = listDta.Remark,
                                ProductSKUMapID = listDta.ProductSKUMapID,
                                Quantity = listDta.Quantity,
                                UnitPrice = listDta.UnitPrice,
                                Fraction = listDta.Fraction,
                                ForeignRate = listDta.ForeignRate,
                                UnitID = listDta.UnitID,
                                Amount = listDta.Amount,
                                CreatedBy = (int?)_context.LoginID,
                                UpdatedBy = (int?)_context.LoginID,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                            });
                        }

                        entity.ReferenceHeadID = toSaveList.FirstOrDefault(x => x.SupplierID == group.Key).HeadID;

                        dbContext.TransactionHeads.Add(entity);
                        documentType.LastTransactionNo = incrLastNo;

                        dbContext.SaveChanges();

                        //Generate Workflow for Purchase Quotation to Purchase Order - VendorPortal
                        var settingValue2 = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QUOTATION_TO_PO_WORKFLOW_ID");
                        var workflowID = long.Parse(settingValue2);

                        Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(workflowID, entity.HeadIID);
                    }

                    dbContext.Entry(documentType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                    Eduegate.Logger.LogHelper<string>.Fatal($"Bid to PO Saving failed. Error message: {errorMessage}", ex);
                }

                toDto.TransactionHead.HeadIID = entity.HeadIID;
                return ToDTOString(toDto);
            }
        }
    }
}