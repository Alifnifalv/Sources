using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Domain.Entity.Accounts;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Repository.Accounts.Assets;
using Eduegate.Domain.Entity.Accounts.Models.Inventory;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers.Accounts.Assets
{
    public class AssetTransactionHeadMapper : IDTOEntityMapper<AssetTransactionHeadDTO, AssetTransactionHead>
    {
        private CallContext _context;

        public static AssetTransactionHeadMapper Mapper(CallContext context)
        {
            var mapper = new AssetTransactionHeadMapper();
            mapper._context = context;
            return mapper;
        }

        public AssetTransactionHead ToEntity(AssetTransactionHeadDTO dto)
        {
            if (dto != null)
            {
                var transactionHead = new AssetTransactionHead()
                {
                    HeadIID = dto.HeadIID,
                    TransactionNo = dto.TransactionNo,
                    DocumentTypeID = dto.DocumentTypeID,
                    EntryDate = dto.EntryDate,
                    Remarks = dto.Remarks,
                    DocumentStatusID = dto.DocumentStatusID,
                    ProcessingStatusID = dto.ProcessingStatusID,
                    BranchID = dto.BranchID,
                    ToBranchID = dto.ToBranchID,
                    CompanyID = dto.CompanyID,
                    ReferenceHeadID = dto.ReferenceHeadID,
                    SchoolID = dto.SchoolID.HasValue ? dto.SchoolID : Convert.ToByte(_context.SchoolID),
                    AcademicYearID = dto.AcademicYearID.HasValue ? dto.AcademicYearID : _context.AcademicYearID,
                    SupplierID = dto.SupplierID,
                    Reference = dto.Reference,
                    DepartmentID = dto.DepartmentID,
                    AssetLocation = dto.AssetLocation,
                    SubLocation = dto.SubLocation,
                    AssetFloor = dto.AssetFloor,
                    RoomNumber = dto.RoomNumber,
                    UserName = dto.UserName,
                    DepreciationStartDate = dto.DepreciationStartDate,
                    DepreciationEndDate = dto.DepreciationEndDate,
                };

                if (dto.HeadIID <= 0)
                {
                    transactionHead.CreatedBy = _context.IsNotNull() ? (_context.LoginID.HasValue ? (int)_context.LoginID : (int?)null) : null;
                    transactionHead.CreatedDate = DateTime.Now;
                }
                else
                {
                    transactionHead.CreatedBy = dto.CreatedBy;
                    transactionHead.CreatedDate = dto.CreatedDate;
                    transactionHead.UpdatedBy = _context.IsNotNull() ? (_context.LoginID.HasValue ? (int)_context.LoginID : (int?)null) : null;
                    transactionHead.UpdatedDate = DateTime.Now;
                }

                return transactionHead;
            }

            else return new AssetTransactionHead();
        }

        public AssetTransactionHeadDTO ToDTO(AssetTransactionHead entity)
        {
            if (entity != null)
            {
                var transactionStatus = new TransactionStatus();

                if (entity.ProcessingStatusID > 0)
                {
                    transactionStatus = new AssetTransactionRepository().GetTransactionStatus((byte)entity.ProcessingStatusID);
                }
                else
                {
                    transactionStatus = null;
                }

                // Get Reference Transaction Number
                var refTransaction = entity.ReferenceHeadID > 0 ? new AssetTransactionRepository().GetAssetTransaction(entity.ReferenceHeadID.Value) : null;

                var transactionDTO = new AssetTransactionHeadDTO();

                transactionDTO.HeadIID = entity.HeadIID;
                transactionDTO.Reference = entity.Reference;
                transactionDTO.CompanyID = entity.CompanyID;
                transactionDTO.Remarks = entity.Remarks;
                transactionDTO.EntryDate = entity.EntryDate.IsNull() ? (DateTime?)null : Convert.ToDateTime(entity.EntryDate);
                transactionDTO.TransactionNo = entity.TransactionNo;
                transactionDTO.ProcessingStatusID = entity.ProcessingStatusID;
                transactionDTO.DocumentStatusID = entity.DocumentStatusID.HasValue ? short.Parse(entity.DocumentStatusID.ToString()) : (short?)null;
                transactionDTO.DocumentTypeID = entity.DocumentTypeID;
                transactionDTO.DocumentTypeName = entity.DocumentTypeID.HasValue ? new AssetTransactionRepository().GetDocumentType(entity.DocumentTypeID.Value).TransactionTypeName : string.Empty;
                transactionDTO.BranchID = entity.BranchID != null ? (long)entity.BranchID : default(long);
                transactionDTO.ToBranchID = entity.ToBranchID != null ? (long)entity.ToBranchID : default(long);
                transactionDTO.ReferenceHeadID = entity.ReferenceHeadID;
                transactionDTO.SupplierID = entity.SupplierID;
                transactionDTO.CompanyID = entity.CompanyID;
                transactionDTO.DepreciationStartDate = entity.DepreciationStartDate;
                transactionDTO.DepreciationEndDate = entity.DepreciationEndDate;
                
                transactionDTO.CreatedBy = entity.CreatedBy;
                transactionDTO.UpdatedBy = entity.UpdatedBy;
                transactionDTO.CreatedDate = entity.CreatedDate;
                transactionDTO.UpdatedDate = entity.UpdatedDate;

                GetAndFillDetails(transactionDTO);

                var documentReference = new AssetTransactionRepository().GetDocumentReferenceTypesByDocumentType((int)entity.DocumentTypeID);
                transactionDTO.DocumentReferenceType = ((Eduegate.Services.Contracts.Enums.DocumentReferenceTypes)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes),
                    documentReference.ReferenceTypeID.ToString())).ToString();
                transactionDTO.DocumentReferenceTypeID = (Eduegate.Services.Contracts.Enums.DocumentReferenceTypes)documentReference.ReferenceTypeID;

                if (refTransaction.IsNotNull())
                {
                    transactionDTO.ReferenceTransactionNo = refTransaction.TransactionNo;
                    //transactionDTO.PaidAmount = refTransaction.PaidAmount;
                }

                if (transactionStatus.IsNotNull())
                {
                    transactionDTO.ProcessingStatusID = transactionStatus.TransactionStatusID;
                    transactionDTO.ProcessingStatusName = transactionStatus.Description;

                    //if null take set it from the transaction status
                    if (!transactionDTO.DocumentStatusID.HasValue)
                    {
                        if (transactionDTO.ProcessingStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete ||
                            transactionDTO.ProcessingStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Confirmed)
                        {
                            transactionDTO.DocumentStatusID = (short)Eduegate.Services.Contracts.Enums.DocumentStatuses.Completed;
                        }
                        else
                        {
                            transactionDTO.DocumentStatusID = (short)Eduegate.Services.Contracts.Enums.DocumentStatuses.Draft;
                        }
                    }
                    else // transaction engine is missed to update the document status, should handle it here.
                    {
                        if (transactionDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.IntitiateReprecess
                            || transactionDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.InProcess)
                        {
                            transactionDTO.DocumentStatusID = (short)Eduegate.Services.Contracts.Enums.DocumentStatuses.Completed;
                        }
                        else
                        {
                            transactionDTO.DocumentStatusID = entity.DocumentStatusID;
                        }
                    }
                }

                transactionDTO.DocumentStatusName = transactionDTO.DocumentStatusID.HasValue ? new AssetTransactionRepository().GetDocumentStatus(transactionDTO.DocumentStatusID.Value).StatusName : string.Empty;

                transactionDTO.AgainstReferenceHeadID = GetReferenceHeadID(entity.HeadIID);

                if (transactionDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.IntitiateReprecess ||
                    transactionDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete ||
                    transactionDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.InProcess ||
                    transactionDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Cancelled ||
                    transactionDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Confirmed)
                {
                    transactionDTO.IsTransactionCompleted = true;
                }

                return transactionDTO;
            }

            else return new AssetTransactionHeadDTO();
        }

        public List<AssetTransactionHeadDTO> TodTO(List<AssetTransactionHead> entities)
        {
            var dtos = new List<AssetTransactionHeadDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public AssetTransactionHeadDTO ToDTOReference(AssetTransactionHead entity)
        {
            return new AssetTransactionHeadDTO()
            {
                HeadIID = entity.HeadIID,
                EntryDate = entity.EntryDate,
                TransactionNo = entity.TransactionNo,
            };
        }

        public List<AssetTransactionHeadDTO> ToDTOReferenceList(List<AssetTransactionHead> entityList)
        {
            var transactionList = new List<AssetTransactionHeadDTO>();
            foreach (var entity in entityList)
            {
                transactionList.Add(ToDTOReference(entity));
            }
            return transactionList;
        }

        public long GetReferenceHeadID(long? IID)
        {
            long referenceHeadID = new long();

            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var alreadyDoneTransactions = dbContext.AssetTransactionHeads.Where(a => a.ReferenceHeadID == IID).AsNoTracking().FirstOrDefault();
                if (alreadyDoneTransactions != null)
                    referenceHeadID = alreadyDoneTransactions.HeadIID;
            }

            return referenceHeadID;
        }

        public AssetTransactionHeadDTO GetTransactionHeadDetailsByID(long headID)
        {
            var tranHeadDTO = new AssetTransactionHeadDTO();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var transHead = dbContext.AssetTransactionHeads.Where(t => t.HeadIID == headID).AsNoTracking().FirstOrDefault();

                tranHeadDTO = new AssetTransactionHeadDTO()
                {
                    HeadIID = transHead.HeadIID,
                };

            }

            return tranHeadDTO;
        }

        public void GetAndFillDetails(AssetTransactionHeadDTO dto)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var branch = dbContext.Branches.Where(y => y.BranchIID == dto.BranchID).AsNoTracking().FirstOrDefault();
                dto.BranchName = branch?.BranchName;
            }
        }

    }
}