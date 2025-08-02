using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Mappers.Accounts.Assets;
using Eduegate.Services.Contracts.Leads;

namespace Eduegate.Domain.Accounts.Assets
{
    public class FixedAssetBL
    {
        //private TransactionRepository transactionRepo = new TransactionRepository();
        private CallContext _callContext;
        FixedAssetsRepository fixedAssetsRepository = new FixedAssetsRepository();
        public FixedAssetBL(CallContext context)
        {
            _callContext = context;
        }

        public List<AssetCategoryDTO> GetAssetCategories()
        {
            List<AssetCategory> entities = fixedAssetsRepository.GetAssetCategories();
            return entities.Select(x => new AssetCategoryDTO()
            {
                AssetCategoryID = x.AssetCategoryID,
                CategoryName = x.CategoryName,
            }).ToList();
        }

        public List<KeyValueDTO> GetAssetCodes()
        {
            var lists = new FixedAssetsRepository().GetAssetCodes();
            List<KeyValueDTO> dtos = new List<KeyValueDTO>();
            lists.ForEach(x =>
            {
                dtos.Add(new KeyValueDTO
                {
                    Key = Convert.ToString(x.AssetIID),
                    Value = x.AssetCode
                });
            });
            return dtos;
        }

        public List<AssetDTO> SaveAssets(List<AssetDTO> dtos)
        {
            List<Asset> entityList = fixedAssetsRepository.SaveAssets(ToAssetEntity(dtos));

            List<AssetDTO> dtoList = new List<AssetDTO>();
            foreach (var assetEntity in entityList)
            {
                dtoList.Add(ToAssetDTO(new FixedAssetsRepository().GetAssetById(assetEntity.AssetIID)));
            }
            return dtoList;
        }

        public List<Asset> ToAssetEntity(List<AssetDTO> dtos)
        {
            Mapper<AssetDTO, Asset>.CreateMap();
            return Mapper<List<AssetDTO>, List<Asset>>.Map(dtos);
        }

        public AssetDTO ToAssetDTO(Asset entity)
        {
            AssetDTO dto = new AssetDTO();

            if (entity.AccumulatedDepGLAcc != null)
            {
                dto.AccumulatedDepGLAccount = new KeyValueDTO() { Key = entity.AccumulatedDepGLAccID.ToString(), Value = entity.AccumulatedDepGLAcc.AccountName };
            }
            if (entity.AssetGlAcc != null)
            {
                dto.AssetGlAccount = new KeyValueDTO() { Key = entity.AssetGlAccID.ToString(), Value = entity.AssetGlAcc.AccountName };
            }
            if (entity.DepreciationExpGLAcc != null)
            {
                dto.DepreciationExpGLAccount = new KeyValueDTO() { Key = entity.DepreciationExpGLAccId.ToString(), Value = entity.DepreciationExpGLAcc.AccountName };
            }


            dto.AccumulatedDepGLAccID = entity.AccumulatedDepGLAccID;
            dto.AssetCategoryID = entity.AssetCategoryID;
            dto.AssetCategoryName = entity.AssetCategory?.CategoryName;
            dto.AssetGlAccID = entity.AssetGlAccID;
            dto.AssetIID = entity.AssetIID;
            dto.DepreciationExpGLAccID = entity.DepreciationExpGLAccId;
            dto.DepreciationYears = entity.DepreciationYears;
            dto.Description = entity.Description;
            dto.AssetCode = entity.AssetCode;

            if (entity.AssetTransactionDetails != null)
            {
                AssetTransactionDetail detailItem = entity.AssetTransactionDetails.Where(x => x.Head.DocumentTypeID == (int)DocumentTypes.AssetEntry).FirstOrDefault();
                if (detailItem != null)
                {
                    dto.AssetValue = detailItem.Amount;
                    dto.Quantity = detailItem.Quantity;
                }
            }

            //Accumulated Depreciation
            dto.AccumulatedDepreciation = fixedAssetsRepository.GetAccumulatedDepreciation(dto.AssetIID, (int)DocumentTypes.AssetDepreciation);

            return dto;
        }

        public AssetDTO GetAssetById(long AssetId)
        {
            Asset asset = fixedAssetsRepository.GetAssetById(AssetId);
            AssetDTO assetDTO = ToAssetDTO(asset);
            return assetDTO;
        }

        public bool DeleteAsset(long AssetId)
        {
            return fixedAssetsRepository.DeleteAsset(AssetId);
        }

        public AssetTransactionHeadDTO SaveAssetTransaction(AssetTransactionHeadDTO headDto)
        {
            AssetTransactionHead entity = fixedAssetsRepository.SaveAssetTransaction(ToAssetTransactionHeadEntity(headDto));
            
            if (entity != null)
            {
                return GetAssetTransactionHeadById(entity.HeadIID);
            }

            return headDto;
        }

        public AssetTransactionHead ToAssetTransactionHeadEntity(AssetTransactionHeadDTO headDto)
        {
            AssetTransactionHead entity = new AssetTransactionHead();

            if (headDto.HeadIID == 0)
            {
                headDto.TransactionNo = fixedAssetsRepository.GetNextTransactionNumberByDocument(headDto.DocumentTypeID.Value);
            }

            entity.HeadIID = headDto.HeadIID;
            entity.DocumentStatusID = headDto.DocumentStatusID;
            entity.EntryDate = headDto.EntryDate;
            entity.ProcessingStatusID = headDto.ProcessingStatusID;
            entity.Remarks = headDto.Remarks;
            entity.TransactionNo = headDto.TransactionNo;
            entity.DocumentTypeID = headDto.DocumentTypeID;
            entity.BranchID = headDto.BranchID;
            entity.ToBranchID = headDto.ToBranchID;
            entity.CompanyID = headDto.CompanyID;
            entity.ReferenceHeadID = headDto.ReferenceHeadID;
            entity.SchoolID = headDto.SchoolID.HasValue ? headDto.SchoolID : Convert.ToByte(_callContext.SchoolID);
            entity.AcademicYearID = headDto.AcademicYearID.HasValue ? headDto.AcademicYearID : _callContext.AcademicYearID;
            entity.SupplierID = headDto.SupplierID;
            entity.Reference = headDto.Reference;
            entity.DepartmentID = headDto.DepartmentID;
            entity.AssetLocation = headDto.AssetLocation;
            entity.SubLocation = headDto.SubLocation;
            entity.AssetFloor = headDto.AssetFloor;
            entity.RoomNumber = headDto.RoomNumber;
            entity.UserName = headDto.UserName;
            entity.CreatedBy = headDto.HeadIID == 0 ? (int)_callContext.LoginID : headDto.CreatedBy;
            entity.UpdatedBy = headDto.HeadIID > 0 ? (int)_callContext.LoginID : headDto.UpdatedBy;
            entity.CreatedDate = headDto.HeadIID == 0 ? DateTime.Now : headDto.CreatedDate;
            entity.UpdatedDate = headDto.HeadIID > 0 ? DateTime.Now : headDto.UpdatedDate;

            entity.AssetTransactionDetails = new List<AssetTransactionDetail>();

            foreach (AssetTransactionDetailsDTO detailDTOItem in headDto.AssetTransactionDetails)
            {
                AssetTransactionDetail detailEntity = new AssetTransactionDetail();

                var serialMaps = new List<AssetTransactionSerialMap>();
                foreach (var serialMap in detailDTOItem.AssetTransactionSerialMaps)
                {
                    if (serialMap.AssetTransactionSerialMapIID == 0)
                    {
                        //serialMap.AssetSequenceCode = AssetMapper.Mapper(_callContext).GetNextAssetTransactionNumberByID(headDto.AssetID.Value);
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
                        CreatedBy = serialMap.AssetTransactionSerialMapIID == 0 ? (int)_callContext.LoginID : serialMap.CreatedBy,
                        UpdatedBy = serialMap.AssetTransactionSerialMapIID > 0 ? (int)_callContext.LoginID : serialMap.UpdatedBy,
                        CreatedDate = serialMap.AssetTransactionSerialMapIID == 0 ? DateTime.Now : serialMap.CreatedDate,
                        UpdatedDate = serialMap.AssetTransactionSerialMapIID > 0 ? DateTime.Now : serialMap.UpdatedDate,
                    });
                }

                detailEntity.DetailIID = detailDTOItem.DetailIID;
                detailEntity.HeadID = detailDTOItem.HeadID;
                detailEntity.AssetID = detailDTOItem.AssetID;
                detailEntity.Quantity = detailDTOItem.Quantity;
                detailEntity.Amount = detailDTOItem.Amount;
                detailEntity.AccountID = detailDTOItem.AccountID;
                detailEntity.AssetGlAccID = detailDTOItem.AssetGlAccID;
                detailEntity.AccumulatedDepGLAccID = detailDTOItem.AccumulatedDepGLAccID;
                detailEntity.DepreciationExpGLAccID = detailDTOItem.DepreciationExpGLAccID;
                detailEntity.CostAmount = detailDTOItem.CostAmount;
                detailEntity.NetValue = detailDTOItem.NetValue;
                detailEntity.AccountingPeriodDays = detailDTOItem.AccountingPeriodDays;
                detailEntity.DepAccumulatedTillDate = detailDTOItem.DepAccumulatedTillDate;
                detailEntity.DepFromDate = detailDTOItem.DepFromDate;
                detailEntity.DepToDate = detailDTOItem.DepToDate;
                detailEntity.DepAbovePeriod = detailDTOItem.DepAbovePeriod;
                detailEntity.BookedDepreciation = detailDTOItem.BookedDepreciation;
                detailEntity.AccumulatedDepreciationAmount = detailDTOItem.AccumulatedDepreciationAmount;
                detailEntity.PreviousAcculatedDepreciationAmount = detailDTOItem.PreviousAcculatedDepreciationAmount;

                detailEntity.CreatedBy = detailDTOItem.DetailIID == 0 ? (int)_callContext.LoginID : detailDTOItem.CreatedBy;
                detailEntity.UpdatedBy = detailDTOItem.DetailIID > 0 ? (int)_callContext.LoginID : detailDTOItem.UpdatedBy;
                detailEntity.CreatedDate = detailDTOItem.DetailIID == 0 ? DateTime.Now : detailDTOItem.CreatedDate;
                detailEntity.UpdatedDate = detailDTOItem.DetailIID > 0 ? DateTime.Now : detailDTOItem.UpdatedDate;
                detailEntity.AssetTransactionSerialMaps = serialMaps;

                entity.AssetTransactionDetails.Add(detailEntity);
            }

            return entity;
        }

        public AssetTransactionHeadDTO GetAssetTransactionHeadById(long HeadID)
        {
            AssetTransactionHead assetTransactionHeadEntity = fixedAssetsRepository.GetAssetTransactionHeadById(HeadID);
            AssetTransactionHeadDTO assetTransactionHeadDTO = ToAssetTransactionHeadDTO(assetTransactionHeadEntity);

            return assetTransactionHeadDTO;
        }

        public AssetTransactionHeadDTO ToAssetTransactionHeadDTO(AssetTransactionHead headEntity)
        {
            AssetTransactionHeadDTO HeadDTO = new AssetTransactionHeadDTO();
            if (headEntity == null)
            {
                return HeadDTO;
            }

            HeadDTO.HeadIID = headEntity.HeadIID;
            HeadDTO.DocumentStatusID = headEntity.DocumentStatusID;
            HeadDTO.DocumentTypeID = headEntity.DocumentTypeID;
            HeadDTO.EntryDate = headEntity.EntryDate;
            HeadDTO.ProcessingStatusID = headEntity.ProcessingStatusID;
            HeadDTO.Remarks = headEntity.Remarks;
            HeadDTO.TransactionNo = headEntity.TransactionNo;
            HeadDTO.BranchID = headEntity.BranchID;
            HeadDTO.ToBranchID = headEntity.ToBranchID;
            HeadDTO.CompanyID = headEntity.CompanyID;
            HeadDTO.ReferenceHeadID = headEntity.ReferenceHeadID;
            HeadDTO.SchoolID = headEntity.SchoolID;
            HeadDTO.AcademicYearID = headEntity.AcademicYearID;
            HeadDTO.SupplierID = headEntity.SupplierID;
            HeadDTO.Reference = headEntity.Reference;
            HeadDTO.DepartmentID = headEntity.DepartmentID;
            HeadDTO.AssetLocation = headEntity.AssetLocation;
            HeadDTO.SubLocation = headEntity.SubLocation;
            HeadDTO.AssetFloor = headEntity.AssetFloor;
            HeadDTO.RoomNumber = headEntity.RoomNumber;
            HeadDTO.UserName = headEntity.UserName;
            HeadDTO.UpdatedBy = headEntity.UpdatedBy;
            HeadDTO.UpdatedDate = headEntity.UpdatedDate;
            HeadDTO.CreatedBy = headEntity.CreatedBy;
            HeadDTO.CreatedDate = headEntity.CreatedDate;

            //if (headEntity.DocumentStatus != null && headEntity.DocumentStatus.DocumentStatus != null)
            //{
            //    HeadDTO.DocumentStatus = new KeyValueDTO() { Key = headEntity.DocumentStatusID.ToString(), Value = headEntity.DocumentStatus.DocumentStatus.StatusName };
            //}
            //if (headEntity.DocumentType != null)
            //{
            //    HeadDTO.DocumentType = new KeyValueDTO() { Key = headEntity.DocumentTypeID.ToString(), Value = headEntity.DocumentType.TransactionTypeName };
            //}
            //if (headEntity.ProcessingStatus != null)
            //{
            //    HeadDTO.TransactionStatus = new KeyValueDTO() { Key = headEntity.ProcessingStatusID.ToString(), Value = headEntity.ProcessingStatus.Description };
            //}

            HeadDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();

            foreach (AssetTransactionDetail detailEntityItem in headEntity.AssetTransactionDetails)
            {
                AssetTransactionDetailsDTO detailDTO = new AssetTransactionDetailsDTO();

                detailDTO.Amount = detailEntityItem.Amount;
                detailDTO.AssetID = detailEntityItem.AssetID;
                detailDTO.Quantity = detailEntityItem.Quantity;
                detailDTO.UpdatedBy = detailEntityItem.UpdatedBy;
                detailDTO.UpdatedDate = detailEntityItem.UpdatedDate;
                detailDTO.AccountID = detailEntityItem.AccountID;
                detailDTO.Createdby = detailEntityItem.CreatedBy;
                detailDTO.CreatedDate = detailEntityItem.CreatedDate;
                detailDTO.DetailIID = detailEntityItem.DetailIID;
                detailDTO.HeadID = detailEntityItem.HeadID;

                //Set Asset Object
                if (detailEntityItem.Asset != null)
                {
                    AssetDTO assetDTO = new AssetDTO();

                    assetDTO.AssetCategoryID = detailEntityItem.Asset.AssetCategoryID;
                    assetDTO.AssetCategoryName = detailEntityItem.Asset?.AssetCategory?.CategoryName;

                    assetDTO.AccumulatedDepGLAccID = detailEntityItem.Asset.AccumulatedDepGLAccID;
                    assetDTO.DepreciationExpGLAccID = detailEntityItem.Asset.DepreciationExpGLAccId;
                    assetDTO.AssetGlAccID = detailEntityItem.Asset.AssetGlAccID;
                    assetDTO.AssetCode = detailEntityItem.Asset.AssetCode;
                    assetDTO.Description = detailEntityItem.Asset.Description;
                    assetDTO.DepreciationYears = detailEntityItem.Asset.DepreciationYears;

                    assetDTO.AccumulatedDepreciation = fixedAssetsRepository.GetAccumulatedDepreciation(detailEntityItem.Asset.AssetIID, (int)DocumentTypes.AssetDepreciation);

                    AssetDTO AssetEntry_AssetDTO = GetAssetById((long)detailEntityItem.AssetID);
                    if (AssetEntry_AssetDTO != null)
                    {
                        assetDTO.StartDate = AssetEntry_AssetDTO.StartDate;
                        assetDTO.AssetValue = AssetEntry_AssetDTO.AssetValue;
                        assetDTO.Quantity = AssetEntry_AssetDTO.Quantity;
                    }

                    detailDTO.AssetCodeKeyValue = new KeyValueDTO() { Key = detailEntityItem.AssetID.ToString(), Value = detailEntityItem.Asset.AssetCode };
                    detailDTO.AssetGlAccID = detailEntityItem.AccountID;
                    detailDTO.Asset = assetDTO;

                }

                //Set Account Object
                if (detailEntityItem.Account != null)
                {
                    AccountDTO accountDTO = new AccountDTO();
                    accountDTO.AccountID = detailEntityItem.Account.AccountID;
                    accountDTO.AccountName = detailEntityItem.Account.AccountName;
                    accountDTO.Alias = detailEntityItem.Account.Alias;

                    detailDTO.AssetGlAccount = new KeyValueDTO() { Key = detailEntityItem.AccountID.ToString(), Value = detailEntityItem.Account.AccountName };

                    detailDTO.Account = accountDTO;
                }


                HeadDTO.AssetTransactionDetails.Add(detailDTO);
            }
            return HeadDTO;
        }

        public List<AssetTransactionHeadDTO> GetAssetTransactionHeads(DocumentReferenceTypes referenceTypes, TransactionStatus transactionStatus)
        {
            List<AssetTransactionHead> assetTransactionHeadEntity = new List<AssetTransactionHead>();
            assetTransactionHeadEntity = fixedAssetsRepository.GetAssetTransactionHeads(referenceTypes, transactionStatus);
            List<AssetTransactionHeadDTO> DTOList = new List<AssetTransactionHeadDTO>();
            foreach (AssetTransactionHead head in assetTransactionHeadEntity)
            {
                DTOList.Add(ToAssetTransactionHeadDTO(head));
            }
            return DTOList;
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto)
        {
            var assetTransactionHead = new AssetTransactionHead();
            assetTransactionHead.HeadIID = dto.HeadIID;
            assetTransactionHead.ProcessingStatusID = dto.ProcessingStatusID;
            assetTransactionHead.UpdatedBy = _callContext.IsNotNull() ? _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : default : default;
            assetTransactionHead.DocumentStatusID = dto.DocumentStatusID;
            return new FixedAssetsRepository().UpdateAssetTransactionHead(assetTransactionHead);
        }

        public List<KeyValueDTO> GetAssetCodesSearch(string SearchText)
        {
            var lists = new FixedAssetsRepository().GetAssetCodesSearch(SearchText);
            List<KeyValueDTO> dtos = new List<KeyValueDTO>();
            lists.ForEach(x =>
            {
                dtos.Add(new KeyValueDTO
                {
                    Key = Convert.ToString(x.AssetIID),
                    Value = x.AssetCode
                });
            });
            return dtos;
        }

        public List<KeyValueDTO> GetAccountCodesSearch(string SearchText)
        {
            var lists = new FixedAssetsRepository().GetAccountCodesSearch(SearchText);
            List<KeyValueDTO> dtos = new List<KeyValueDTO>();
            lists.ForEach(x =>
            {
                dtos.Add(new KeyValueDTO
                {
                    Key = Convert.ToString(x.AccountID),
                    Value = x.AccountName
                });
            });
            return dtos;
        }

        public AssetDTO GetAssetByAssetCode(string AssetCode)
        {
            Asset asset = fixedAssetsRepository.GetAssetByAssetCode(AssetCode);
            if (asset == null) return null;
            AssetDTO assetDTO = ToAssetDTO(asset);
            return assetDTO;
        }

        public decimal GetAccumulatedDepreciation(long assetID, int documentTypeID)
        {
            return new FixedAssetsRepository().GetAccumulatedDepreciation(assetID, documentTypeID);
        }
    }
}