using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers
{
    public class StockUpdationMapper : DTOEntityDynamicMapper
    {
        public static StockUpdationMapper Mapper(CallContext context)
        {
            var mapper = new StockUpdationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StockVerificationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public StockVerificationDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {

                var transHead = dbContext.TransactionHeads.FirstOrDefault(h => h.HeadIID == IID);

                var compDetails = dbContext.StockCompareDetails.Where(h => h.TransactionHeadID == IID).ToList();
                var docTypes = dbContext.DocumentStatuses.FirstOrDefault(b => b.DocumentStatusID == transHead.DocumentStatusID);
                var stkVerifyDetail = compDetails.FirstOrDefault(d => d.TransactionHeadID == IID);
                var stckVeriftHead = dbContext.StockCompareHeads.FirstOrDefault(x => x.HeadIID == stkVerifyDetail.HeadID);

                var dto = new StockVerificationDTO()
                {
                    HeadIID = transHead.HeadIID,
                    CurrentStatusID = (byte?)transHead.DocumentStatusID,
                    PhysicalVerTransNo = stckVeriftHead?.TransactionNo,
                    Employee = transHead.EmployeeID.HasValue ? new KeyValueDTO() { Key = transHead.EmployeeID.ToString(), Value = transHead.Employee != null ? transHead.Employee.EmployeeCode+" - "+transHead.Employee.FirstName+" "+transHead.Employee.MiddleName+" "+transHead.Employee.LastName : null } : new KeyValueDTO(),
                    Branch = transHead.BranchID.HasValue ? new KeyValueDTO() { Key = transHead.BranchID.ToString(), Value = transHead.Branch != null ? transHead.Branch.BranchName : null } : new KeyValueDTO(),
                    DocumentStatus = transHead.DocumentStatusID.HasValue ? new KeyValueDTO() { Key = transHead.DocumentStatusID.ToString(), Value = docTypes != null ? docTypes.StatusName : null } : new KeyValueDTO(),
                    TransactionDate = transHead.TransactionDate,
                    TransactionNo = transHead.TransactionNo,
                    CreatedBy = transHead.CreatedBy,
                    UpdatedBy = transHead.UpdatedBy,
                    CreatedDate = transHead.CreatedDate,
                    UpdatedDate = transHead.UpdatedDate,
                };

                foreach (var detailList in transHead.TransactionDetails)
                {
                    var det = compDetails.FirstOrDefault(c => c.ProductSKUMapID == detailList.ProductSKUMapID);
                    dto.StockVerificationMap.Add(new StockVerificationMapDTO()
                    {
                        DetailIID = detailList.DetailIID,
                        HeadID = transHead.HeadIID,
                        ProductID = detailList.ProductID,
                        ProductSKU = detailList.ProductSKUMapID.HasValue ? new KeyValueDTO() { Key = detailList.ProductSKUMapID.ToString(), Value = detailList.ProductSKUMap != null ? detailList.ProductSKUMap?.SKUName : null } : new KeyValueDTO(),
                        Remark = detailList.Remark,
                        BookStock = detailList.ActualQuantity,
                        AvailableQuantity = det.ActualQuantity,
                        PhysicalQuantity = det.PhysicalQuantity,
                        DifferQuantity = det.PhysicalQuantity - detailList.ActualQuantity,
                        ProductSKUMapID = detailList.ProductSKUMapID,
                        Description = detailList.ProductSKUMap?.SKUName,
                        Quantity = detailList.Quantity,
                        CreatedBy = detailList.CreatedBy,
                        UpdatedBy = detailList.UpdatedBy,
                        CreatedDate = detailList.CreatedDate,
                        UpdatedDate = detailList.UpdatedDate,
                    });
                }

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StockVerificationDTO;

            if (toDto.HeadIID != 0 && toDto.CurrentStatusID == (int)Services.Contracts.Enums.DocumentStatuses.Submitted)
            {
                throw new Exception("Can't edit already submitted transaction");
            }

            using (var dbContext = new dbEduegateERPContext())
            {

                //Get DocumentType for Stock Updation
                var documentType = dbContext.DocumentTypes.FirstOrDefault(d => d.TransactionNoPrefix == "STKADJ-");
                var incrLastNo = documentType.LastTransactionNo + 1;

                var entity = new TransactionHead()
                {
                    HeadIID = toDto.HeadIID,
                    CompanyID = 1,
                    BranchID = toDto.BranchID,
                    EmployeeID = toDto.EmployeeID,
                    DocumentTypeID = documentType.DocumentTypeID,
                    TransactionNo = toDto.HeadIID == 0 ? documentType.TransactionNoPrefix + incrLastNo : toDto.TransactionNo,
                    TransactionDate = toDto.TransactionDate,
                    DocumentStatusID = toDto.DocStatusID,
                    CreatedBy = (int?)(toDto.HeadIID == 0 ? _context.LoginID : toDto.CreatedBy),
                    UpdatedBy = (int?)(toDto.HeadIID != 0 ? _context.LoginID : toDto.UpdatedBy),
                    SchoolID = (byte?)_context.SchoolID,
                    AcademicYearID = _context.AcademicYearID,
                    CreatedDate = toDto.HeadIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedDate = toDto.HeadIID != 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                //List only corrected Quantity items from dto
                var toSaveList = toDto.StockVerificationMap.Where(s => s.CorrectedQuantity != null).ToList();

                if (toSaveList.Count() == 0)
                {
                    throw new Exception("Please enter value in Corrected Quantity..");
                }

                foreach (var listDta in toSaveList)
                {
                        entity.TransactionDetails.Add(new TransactionDetail()
                        {
                            DetailIID = listDta.DetailIID,
                            HeadID = entity.HeadIID,
                            ProductID = listDta.ProductID,
                            Remark = listDta.Remark,
                            ProductSKUMapID = listDta.ProductSKUMapID,
                            Quantity = listDta.CorrectedQuantity,
                            ActualQuantity = listDta.BookStock,
                            CreatedBy = (int?)(toDto.HeadIID == 0 ? _context.LoginID : toDto.CreatedBy),
                            UpdatedBy = (int?)(toDto.HeadIID != 0 ? _context.LoginID : toDto.UpdatedBy),
                            CreatedDate = toDto.HeadIID == 0 ? DateTime.Now : toDto.CreatedDate,
                            UpdatedDate = toDto.HeadIID != 0 ? DateTime.Now : toDto.UpdatedDate,
                        });
                }

                //only data insert in Submitted Status
                if (toDto.DocStatusID == (int)Services.Contracts.Enums.DocumentStatuses.Submitted)
                {
                    foreach (var listDta in toSaveList)
                    {
                            entity.InvetoryTransactions.Add(new InvetoryTransaction()
                            {
                                InventoryTransactionIID = listDta.InventoryTransactionIID,
                                DocumentTypeID = documentType.DocumentTypeID,
                                HeadID = entity.HeadIID,
                                BranchID = entity.BranchID,
                                TransactionNo = entity.TransactionNo,
                                TransactionDate = entity.TransactionDate,
                                ProductSKUMapID = listDta.ProductSKUMapID,
                                Quantity = listDta.CorrectedQuantity,
                                CreatedBy = (int?)(toDto.HeadIID == 0 ? _context.LoginID : toDto.CreatedBy),
                                UpdatedBy = (int?)(toDto.HeadIID != 0 ? _context.LoginID : toDto.UpdatedBy),
                                CreatedDate = toDto.HeadIID == 0 ? DateTime.Now : toDto.CreatedDate,
                                UpdatedDate = toDto.HeadIID != 0 ? DateTime.Now : toDto.UpdatedDate,
                            });
                    }
                }

                if (toDto.HeadIID == 0)
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;

                    //increment Doc Transaction No
                    documentType.LastTransactionNo = incrLastNo;
                    dbContext.Entry(documentType).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();


                    //update transactionHeadIID in stockcompare detail table for reference
                    foreach (var compDetail in toSaveList)
                    {
                        var StkCmprDetails = dbContext.StockCompareDetails.FirstOrDefault(x => x.DetailIID == compDetail.StockVerficationDetailIID);
                        StkCmprDetails.TransactionHeadID = entity.HeadIID;
                        dbContext.Entry(StkCmprDetails).State = System.Data.Entity.EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    if (toDto.DocStatusID == (int)Services.Contracts.Enums.DocumentStatuses.Submitted)
                    {
                        //Generate Workflow for Stock updation
                        var settingValue = dbContext.Settings.FirstOrDefault(s => s.SettingCode == "STOCK_UPDATION_WORKFLOW_ID").SettingValue;
                        var workflowID = long.Parse(settingValue);

                        Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(workflowID, entity.HeadIID);
                    }
                }
                else
                {
                    var updateHeadSts = dbContext.TransactionHeads.FirstOrDefault(s => s.HeadIID == entity.HeadIID);
                    updateHeadSts.DocumentStatusID = toDto.DocStatusID;
                    dbContext.Entry(updateHeadSts).State = System.Data.Entity.EntityState.Modified;

                    //edit case delete all related map details and re-entry
                    var getMapDatas = dbContext.TransactionDetails.Where(s => s.HeadID == entity.HeadIID).ToList();

                    if (getMapDatas.Count > 0)
                    {
                        dbContext.TransactionDetails.RemoveRange(getMapDatas);
                        dbContext.SaveChanges();
                    }

                    foreach(var listDet in entity.TransactionDetails)
                    {
                        dbContext.Entry(listDet).State = System.Data.Entity.EntityState.Added;
                        dbContext.SaveChanges();
                    }


                    if (entity.InvetoryTransactions.Count > 0)
                    {
                        foreach (var toEntityDet in entity.InvetoryTransactions)
                        {
                            dbContext.Entry(toEntityDet).State = System.Data.Entity.EntityState.Added;
                        }
                        dbContext.SaveChanges();
                    }

                    if (toDto.DocStatusID == (int)Services.Contracts.Enums.DocumentStatuses.Submitted)
                    {
                        //Generate Workflow for Stock updation
                        var settingValue = dbContext.Settings.FirstOrDefault(s => s.SettingCode == "STOCK_UPDATION_WORKFLOW_ID").SettingValue;
                        var workflowID = long.Parse(settingValue);

                        Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(workflowID, entity.HeadIID);
                    }
                }

                return ToDTOString(ToDTO(entity.HeadIID));
            }
        }
    }
}