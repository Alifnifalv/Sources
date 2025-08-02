using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Domain.Entity.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Eduegate.Domain.Repository.Accounts.Assets;
using Eduegate.Framework.Extensions;
using System.Reflection.Metadata;

namespace Eduegate.Domain.Mappers.Accounts.Assets
{
    public class AssetManualEntryMapper : DTOEntityDynamicMapper
    {
        public static AssetManualEntryMapper Mapper(CallContext context)
        {
            var mapper = new AssetManualEntryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AssetTransactionHeadDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AssetTransactionHeadDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetTransactionHeads.Where(x => x.HeadIID == IID)
                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.AssetTransactionSerialMaps).ThenInclude(i => i.Supplier)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private AssetTransactionHeadDTO ToDTO(AssetTransactionHead entity)
        {
            var transactionHeadDTO = new AssetTransactionHeadDTO()
            {
                HeadIID = entity.HeadIID,
                TransactionNo = entity.TransactionNo,
                DocumentTypeID = entity.DocumentTypeID,
                EntryDate = entity.EntryDate,
                Remarks = entity.Remarks,
                DocumentStatusID = entity.DocumentStatusID,
                ProcessingStatusID = entity.ProcessingStatusID,
                BranchID = entity.BranchID,
                ToBranchID = entity.ToBranchID,
                CompanyID = entity.CompanyID,
                ReferenceHeadID = entity.ReferenceHeadID,
                SchoolID = entity.SchoolID,
                AcademicYearID = entity.AcademicYearID,
                SupplierID = entity.SupplierID,
                Reference = entity.Reference,
                DepartmentID = entity.DepartmentID,
                AssetLocation = entity.AssetLocation,
                SubLocation = entity.SubLocation,
                AssetFloor = entity.AssetFloor,
                RoomNumber = entity.RoomNumber,
                UserName = entity.UserName,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };
            transactionHeadDTO.AssetID = entity.AssetTransactionDetails?.FirstOrDefault()?.AssetID;
            GetAndFillDetails(transactionHeadDTO);

            if (transactionHeadDTO.ProcessingStatusID == (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete)
            {
                transactionHeadDTO.IsTransactionCompleted = true;
            }
            else
            {
                transactionHeadDTO.IsTransactionCompleted = false;
            }

            transactionHeadDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();
            transactionHeadDTO.AssetTransactionDetails = entity.AssetTransactionDetails.Select(x => AssetTransactionDetailsMapper.Mapper(_context).ToDTO(x)).ToList();

            switch (transactionHeadDTO.DocumentStatusID)
            {
                case (long)Services.Contracts.Enums.DocumentStatuses.Submitted:
                    transactionHeadDTO.DocumentStatusName = Services.Contracts.Enums.DocumentStatuses.Submitted.ToString();
                    break;
                case (long)Services.Contracts.Enums.DocumentStatuses.Completed:
                    transactionHeadDTO.DocumentStatusName = Services.Contracts.Enums.DocumentStatuses.Completed.ToString();
                    break;
                case (long)Services.Contracts.Enums.DocumentStatuses.Draft:
                default:
                    // New
                    transactionHeadDTO.DocumentStatusName = Services.Contracts.Enums.DocumentStatuses.Draft.ToString();
                    break;
            }

            return transactionHeadDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AssetTransactionHeadDTO;

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var documentTypeID = toDto.DocumentTypeID.HasValue ? toDto.DocumentTypeID : GetAssetOpeningDocumentType();
                if (toDto.HeadIID == 0)
                {
                    toDto.TransactionNo = GetNextTransactionNumber(documentTypeID.Value);
                }

                // once TransactionStatus completed we should not allow any operation on transaction.
                if (toDto.IsTransactionCompleted)
                {
                    throw new Exception("Cannot update the trasaction, it's already processed.");
                }

                var entity = new AssetTransactionHead()
                {
                    HeadIID = toDto.HeadIID,
                    TransactionNo = toDto.TransactionNo,
                    DocumentTypeID = documentTypeID,
                    EntryDate = toDto.EntryDate,
                    Remarks = toDto.Remarks,
                    DocumentStatusID = toDto.DocumentStatusID,
                    ProcessingStatusID = toDto.ProcessingStatusID,
                    BranchID = toDto.BranchID,
                    ToBranchID = toDto.ToBranchID,
                    CompanyID = toDto.CompanyID,
                    ReferenceHeadID = toDto.ReferenceHeadID,
                    SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                    SupplierID = toDto.SupplierID,
                    Reference = toDto.Reference,
                    DepartmentID = toDto.DepartmentID,
                    AssetLocation = toDto.AssetLocation,
                    SubLocation = toDto.SubLocation,
                    AssetFloor = toDto.AssetFloor,
                    RoomNumber = toDto.RoomNumber,
                    UserName = toDto.UserName,
                    CreatedBy = toDto.HeadIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.HeadIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.HeadIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.HeadIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                var assetTransFullSerialMaps = new List<AssetTransactionSerialMap>();
                entity.AssetTransactionDetails = new List<AssetTransactionDetail>();
                foreach (var tranDetail in toDto.AssetTransactionDetails)
                {
                    var serialMaps = new List<AssetTransactionSerialMap>();
                    foreach (var serialMap in tranDetail.AssetTransactionSerialMaps)
                    {
                        if (toDto.IsRequiredSerialNumber == true && string.IsNullOrEmpty(serialMap.SerialNumber))
                        {
                            throw new Exception("Serial number is required for this asset!");
                        }

                        if (serialMap.AssetTransactionSerialMapIID == 0)
                        {
                            serialMap.AssetSequenceCode = AssetMapper.Mapper(_context).GetNextAssetTransactionNumberByID(toDto.AssetID.Value);
                        }

                        serialMaps.Add(new AssetTransactionSerialMap()
                        {
                            AssetTransactionSerialMapIID = serialMap.AssetTransactionSerialMapIID,
                            TransactionDetailID = serialMap.TransactionDetailID,
                            AssetID = serialMap.AssetID,
                            AssetSequenceCode = serialMap.AssetSequenceCode,
                            SerialNumber = serialMap.SerialNumber,
                            AssetTag = serialMap.AssetTag,
                            CostPrice = serialMap.CostPrice,
                            ExpectedLife = serialMap.ExpectedLife,
                            DepreciationRate = serialMap.DepreciationRate,
                            ExpectedScrapValue = serialMap.ExpectedScrapValue,
                            AccumulatedDepreciationAmount = serialMap.AccumulatedDepreciationAmount,
                            DateOfFirstUse = serialMap.DateOfFirstUse,
                            SupplierID = serialMap.SupplierID,
                            BillNumber = serialMap.BillNumber,
                            BillDate = serialMap.BillDate,
                            CreatedBy = serialMap.AssetTransactionSerialMapIID == 0 ? (int)_context.LoginID : serialMap.CreatedBy,
                            UpdatedBy = serialMap.AssetTransactionSerialMapIID > 0 ? (int)_context.LoginID : serialMap.UpdatedBy,
                            CreatedDate = serialMap.AssetTransactionSerialMapIID == 0 ? DateTime.Now : serialMap.CreatedDate,
                            UpdatedDate = serialMap.AssetTransactionSerialMapIID > 0 ? DateTime.Now : serialMap.UpdatedDate,
                        });
                    }

                    assetTransFullSerialMaps.AddRange(serialMaps);

                    entity.AssetTransactionDetails.Add(new AssetTransactionDetail()
                    {
                        DetailIID = tranDetail.DetailIID,
                        HeadID = tranDetail.HeadID,
                        AssetID = tranDetail.AssetID,
                        Quantity = tranDetail.Quantity,
                        Amount = tranDetail.Amount,
                        CostAmount = tranDetail.CostAmount,
                        NetValue = tranDetail.NetValue,
                        AccountID = tranDetail.AccountID,
                        CreatedBy = tranDetail.DetailIID == 0 ? (int)_context.LoginID : tranDetail.CreatedBy,
                        UpdatedBy = tranDetail.DetailIID > 0 ? (int)_context.LoginID : tranDetail.UpdatedBy,
                        CreatedDate = tranDetail.DetailIID == 0 ? DateTime.Now : tranDetail.CreatedDate,
                        UpdatedDate = tranDetail.DetailIID > 0 ? DateTime.Now : tranDetail.UpdatedDate,
                        AssetTransactionSerialMaps = serialMaps,
                    });

                    toDto.TotalNetAmount = toDto.TotalNetAmount.HasValue ? toDto.TotalNetAmount + tranDetail.Amount : tranDetail.Amount;
                    toDto.TotalAssetQuantity = toDto.TotalAssetQuantity.HasValue ? toDto.TotalAssetQuantity + tranDetail.Quantity : tranDetail.Quantity;
                }

                dbContext.AssetTransactionHeads.Add(entity);
                if (entity.HeadIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    foreach (var detail in entity.AssetTransactionDetails)
                    {
                        if (detail.DetailIID == 0)
                        {
                            dbContext.Entry(detail).State = EntityState.Added;
                        }
                        else
                        {
                            foreach (var serialMap in detail.AssetTransactionSerialMaps)
                            {
                                if (serialMap.AssetTransactionSerialMapIID == 0)
                                {
                                    dbContext.Entry(serialMap).State = EntityState.Added;
                                }
                                else
                                {
                                    dbContext.Entry(serialMap).State = EntityState.Modified;
                                }
                            }

                            dbContext.Entry(detail).State = EntityState.Modified;
                        }
                    }

                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                var submittedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<int>("DOCUMENT_STATUS_SUBMITTED_ID", 2);
                if (toDto.DocumentStatusID == submittedStatusID)
                {
                    toDto.HeadIID = entity.HeadIID;

                    bool updatedResult = false;

                    updatedResult = CheckAndAddedAssetInventories(toDto);
                    updatedResult = CheckAndAddedAssetSerialMaps(assetTransFullSerialMaps, toDto);

                    SetAndUpdateAssetTransactionStatus(toDto, entity, updatedResult);
                }

                return ToDTOString(ToDTO(entity.HeadIID));
            }
        }

        public string GetNextTransactionNumber(int documentTypeID)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var documentType = dbContext.DocumentTypes.Where(x => x.DocumentTypeID == documentTypeID).AsNoTracking().FirstOrDefault();
                if (documentType != null)
                {
                    documentType.LastTransactionNo = documentType.LastTransactionNo.HasValue ? documentType.LastTransactionNo + 1 : 1;

                    dbContext.Entry(documentType).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    return documentType.TransactionNoPrefix + "-" + documentType.LastTransactionNo.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public void GetAndFillDetails(AssetTransactionHeadDTO dto)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var supplier = dbContext.Suppliers.Where(y => y.SupplierIID == dto.SupplierID).AsNoTracking().FirstOrDefault();

                if (supplier != null)
                {
                    dto.SupplierName = supplier.SupplierCode + " - " + supplier.FirstName + " " +
                        (string.IsNullOrEmpty(supplier.MiddleName) ? "" : supplier.MiddleName + " ") + supplier.LastName;
                }

                var branch = dbContext.Branches.Where(x => x.BranchIID == dto.BranchID).AsNoTracking().FirstOrDefault();
                dto.BranchName = branch?.BranchName;

                var assetData = dbContext.Assets.Where(y => y.AssetIID == dto.AssetID)
                    .Include(i => i.AssetCategory)
                    .AsNoTracking().FirstOrDefault();

                dto.AssetName = assetData?.Description;
                dto.AssetCode = assetData?.AssetCode;
                dto.AssetPrefix = assetData?.AssetPrefix;
                dto.AssetLastSequenceNumber = assetData?.LastSequenceNumber;
                dto.IsRequiredSerialNumber = assetData?.IsRequiredSerialNumber;
                dto.AssetCategoryID = assetData?.AssetCategoryID;
                dto.AssetCategoryName = assetData?.AssetCategory?.CategoryName;
                dto.AssetCategoryDepreciationRate = assetData?.AssetCategory?.DepreciationRate;
            }
        }

        public int GetAssetOpeningDocumentType()
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                int referenceTypeID = (int)Framework.Enums.DocumentReferenceTypes.AssetEntry;
                var documentType = dbContext.DocumentTypes.Where(a => a.ReferenceTypeID == referenceTypeID).AsNoTracking().FirstOrDefault();

                int documentTypeID = documentType != null ? documentType.DocumentTypeID : 200;

                return documentTypeID;
            }
        }

        public bool CheckAndAddedAssetInventories(AssetTransactionHeadDTO headDTO = null)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                try
                {
                    var inventoryData = dbContext.AssetInventories
                        .Where(x => x.BranchID == headDTO.BranchID && x.AssetID == headDTO.AssetID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    var entity = new AssetInventory()
                    {
                        AssetID = headDTO.AssetID,
                        BranchID = headDTO.BranchID,
                        HeadID = headDTO.HeadIID,
                        CompanyID = headDTO.CompanyID ?? _context?.CompanyID,
                        CostAmount = headDTO.TotalNetAmount,
                        IsActive = true,
                    };

                    if (inventoryData == null)
                    {
                        entity.Batch = 1;
                        entity.Quantity = headDTO.TotalAssetQuantity;
                        entity.OriginalQty = headDTO.TotalAssetQuantity;
                        entity.CreatedBy = (int)_context.LoginID;
                        entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        if (headDTO.OldTransactionHeadID == headDTO.HeadIID)
                        {
                            entity.AssetInventoryIID = inventoryData.AssetInventoryIID;
                            entity.Batch = inventoryData.Batch;
                        }
                        else
                        {
                            entity.AssetInventoryIID = 0;
                            entity.Batch = inventoryData.Batch + 1;
                        }

                        entity.Quantity = inventoryData.Quantity;
                        entity.OriginalQty = inventoryData.OriginalQty;
                        entity.CreatedBy = inventoryData.CreatedBy;
                        entity.CreatedDate = inventoryData.CreatedDate;
                        entity.UpdatedBy = (int)_context.LoginID;
                        entity.UpdatedDate = DateTime.Now;
                    }

                    dbContext.AssetInventories.Add(entity);

                    if (entity.AssetInventoryIID == 0)
                    {
                        dbContext.Entry(entity).State = EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    headDTO.AssetInventoryID = entity.AssetInventoryIID;

                    return true;
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                    Eduegate.Logger.LogHelper<string>.Fatal($"Error on CheckAndAddedAssetInventories method. Error message: {errorMessage}", ex);

                    return false;
                }
            }
        }

        public bool CheckAndAddedAssetSerialMaps(List<AssetTransactionSerialMap> transactionSerialMaps, AssetTransactionHeadDTO headDTO = null)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                try
                {
                    foreach (var map in transactionSerialMaps)
                    {
                        var serialData = dbContext.AssetSerialMaps
                            .Where(x => x.AssetID == map.AssetID && x.AssetSequenceCode == map.AssetSequenceCode)
                            .AsNoTracking()
                            .FirstOrDefault();

                        var entity = new AssetSerialMap();

                        if (serialData != null)
                        {
                            entity = serialData;

                            entity.SerialNumber = map.SerialNumber;
                            entity.AssetTag = map.AssetTag;
                            entity.CostPrice = map.CostPrice;
                            entity.ExpectedLife = map.ExpectedLife;
                            entity.DepreciationRate = map.DepreciationRate;
                            entity.ExpectedScrapValue = map.ExpectedScrapValue;
                            entity.AccumulatedDepreciationAmount = map.AccumulatedDepreciationAmount;
                            entity.DateOfFirstUse = map.DateOfFirstUse;
                            entity.SupplierID = map.SupplierID;
                            entity.BillNumber = map.BillNumber;
                            entity.BillDate = map.BillDate;

                            entity.UpdatedBy = (int)_context.LoginID;
                            entity.UpdatedDate = DateTime.Now;
                        }
                        else
                        {
                            entity = new AssetSerialMap()
                            {
                                AssetSerialMapIID = 0,
                                AssetID = map.AssetID,
                                AssetSequenceCode = map.AssetSequenceCode,
                                SerialNumber = map.SerialNumber,
                                AssetTag = map.AssetTag,
                                CostPrice = map.CostPrice,
                                ExpectedLife = map.ExpectedLife,
                                DepreciationRate = map.DepreciationRate,
                                ExpectedScrapValue = map.ExpectedScrapValue,
                                AccumulatedDepreciationAmount = map.AccumulatedDepreciationAmount,
                                IsActive = true,
                                AssetInventoryID = headDTO.AssetInventoryID,
                                DateOfFirstUse = map.DateOfFirstUse,
                                SupplierID = map.SupplierID,
                                BillNumber = map.BillNumber,
                                BillDate = map.BillDate,
                                CreatedBy = (int)_context.LoginID,
                                CreatedDate = DateTime.Now,
                            };
                        }

                        dbContext.AssetSerialMaps.Add(entity);

                        if (entity.AssetSerialMapIID == 0)
                        {
                            dbContext.Entry(entity).State = EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = EntityState.Modified;
                        }
                    }
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                    Eduegate.Logger.LogHelper<string>.Fatal($"Error on CheckAndAddedAssetSerialMaps method. Error message: {errorMessage}", ex);

                    return false;
                }
            }
        }

        public long? SaveAssetOpeningEntryDetails(long feedLogID)
        {
            long? headID = 0;
            try
            {
                using (var dbContext = new dbEduegateAccountsContext())
                {
                    string message = string.Empty;
                    SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                    _sBuilder.ConnectTimeout = 30; // Set Timedout
                    using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                        }
                        catch (Exception ex)
                        {
                            var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                            Eduegate.Logger.LogHelper<string>.Fatal($"Error in SaveAssetOpeningEntryDetails() SqlConnection conn.Open() method. Error message: {errorMessage}", ex);

                            throw;
                        }

                        using (SqlCommand sqlCommand = new SqlCommand("asset.SPS_AssetOpeningDataFeed", conn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@FeedLogID", SqlDbType.BigInt));
                            adapter.SelectCommand.Parameters["@FeedLogID"].Value = feedLogID;

                            DataSet dt = new DataSet();
                            adapter.Fill(dt);
                            DataTable dataTable = null;

                            if (dt.Tables.Count > 0)
                            {
                                if (dt.Tables[0].Rows.Count > 0)
                                {
                                    dataTable = dt.Tables[0];
                                }
                            }

                            if (dataTable != null)
                            {
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    //headID = (long?)row["BankReconciliationHeadIID"];
                                }
                            }

                            return headID;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                var fields = "FeedLogID = " + feedLogID;
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + "," + fields;
                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in asset manual entry SaveAssetOpeningDetails(). Error message: {errorMessage}", ex);

                throw;
            }
        }

        private void SetAndUpdateAssetTransactionStatus(AssetTransactionHeadDTO transaction, AssetTransactionHead transactionHead, bool updatedResult)
        {
            if (!updatedResult)
            {
                transaction.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Draft;
            }

            // Set Transaction Status as per Document Status 
            if (transaction.DocumentStatusID.IsNull())
            {
                // If Null then Draft
                transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                transactionHead.ProcessingStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Hold;
            }
            else
            {
                // Get Document Statues by document reference type
                var documentStatus = new AssetTransactionRepository().GetDocumentReferenceStatusMap(Convert.ToInt32(transaction.DocumentStatusID));

                switch (documentStatus.DocumentStatusID)
                {
                    case (long)Services.Contracts.Enums.DocumentStatuses.Submitted:
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Completed;
                        transactionHead.ProcessingStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete;
                        break;
                    case (long)Services.Contracts.Enums.DocumentStatuses.Draft:
                    default:
                        // New
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                        transactionHead.ProcessingStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Hold;
                        break;
                }
            }

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetTransactionHeads.Where(x => x.HeadIID == transaction.HeadIID).AsNoTracking().FirstOrDefault();
                if (entity != null)
                {
                    entity.DocumentStatusID = transactionHead.DocumentStatusID;
                    entity.ProcessingStatusID = transactionHead.ProcessingStatusID;

                    dbContext.Entry(entity).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
        }

    }
}