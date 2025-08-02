using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Repository.Security;
using Eduegate.Framework.Security;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Translator;

namespace Eduegate.Domain
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
            return entities.Select(x => AssetCategoryMapper.ToDto(x)).ToList();
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
            List < Asset > entityList= fixedAssetsRepository.SaveAssets(ToAssetEntity(dtos));

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
            return  Mapper<List<AssetDTO>, List<Asset>>.Map(dtos);
        }

        public AssetDTO ToAssetDTO(Asset entity)
        {
            AssetDTO dto = new AssetDTO();

            if (entity.AssetCategory != null)
            {
                dto.AssetCategory = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.AssetCategoryID.ToString(), Value = entity.AssetCategory.CategoryName };
            }
            if (entity.Account1 != null)
            {
                dto.AccumulatedDepGLAccount = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.AccumulatedDepGLAccID.ToString(), Value = entity.Account1.AccountName };
            }
            if (entity.Account != null)
            {
                dto.AssetGlAccount = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.AssetGlAccID.ToString(), Value = entity.Account.AccountName };
            }
            if (entity.Account2 != null)
            {
                dto.DepreciationExpGLAccount = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.DepreciationExpGLAccId.ToString(), Value = entity.Account2.AccountName };
            }


            dto.AccumulatedDepGLAccID = entity.AccumulatedDepGLAccID;
            dto.AssetCategoryID = entity.AssetCategoryID;
            dto.AssetGlAccID = entity.AssetGlAccID;
            dto.AssetIID = entity.AssetIID;
            dto.DepreciationExpGLAccId = entity.DepreciationExpGLAccId;
            dto.DepreciationYears = entity.DepreciationYears;
            dto.Description = entity.Description;
            dto.AssetCode = entity.AssetCode;

            if(entity.AssetTransactionDetails !=null)
            {
                AssetTransactionDetail detailItem= entity.AssetTransactionDetails.Where(x => x.AssetTransactionHead.DocumentTypeID == (int)Eduegate.Framework.Enums.DocumentTypes.AssetEntry).FirstOrDefault();
                if(detailItem!=null)
                {
                    dto.StartDate = detailItem.StartDate;
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
            Asset asset= fixedAssetsRepository.GetAssetById(AssetId);
            AssetDTO assetDTO = ToAssetDTO(asset);
            return assetDTO;           
        }

        public bool DeleteAsset(long AssetId)
        {
            return fixedAssetsRepository.DeleteAsset(AssetId);
        }

        public AssetTransactionHeadDTO SaveAssetTransaction(AssetTransactionHeadDTO headDto)
        {
            AssetTransactionHead entity  = fixedAssetsRepository.SaveAssetTransaction(ToAssetTransactionHeadEntity(headDto));
            if(entity !=null)
            {
                return GetAssetTransactionHeadById(entity.HeadIID);
            }
            return headDto;
        }

        public AssetTransactionHead ToAssetTransactionHeadEntity(AssetTransactionHeadDTO headDto)
        {
            AssetTransactionHead entity = new AssetTransactionHead();

            entity.CreatedBy = headDto.CreatedBy;
            entity.CreatedDate = headDto.CreatedDate;
            entity.DocumentStatusID = headDto.DocumentStatusID;
            entity.DocumentTypeID = headDto.DocumentTypeID;
            entity.EntryDate = headDto.EntryDate;
            entity.ProcessingStatusID = headDto.ProcessingStatusID;
            entity.Remarks = headDto.Remarks;
            entity.UpdatedBy = headDto.UpdatedBy;
            entity.UpdatedDate = headDto.UpdatedDate;
            entity.HeadIID = headDto.HeadIID;

            entity.AssetTransactionDetails = new List<AssetTransactionDetail>();

            foreach (AssetTransactionDetailsDTO detailDTOItem in headDto.AssetTransactionDetails)
            {
                AssetTransactionDetail detailEntity = new AssetTransactionDetail();

                detailEntity.Amount = detailDTOItem.Amount;
                detailEntity.AssetID = detailDTOItem.AssetID;
                detailEntity.Quantity = detailDTOItem.Quantity;
                detailEntity.StartDate = detailDTOItem.StartDate;
                detailEntity.UpdatedBy = detailDTOItem.UpdatedBy;
                detailEntity.UpdatedDate = detailDTOItem.UpdatedDate;
                detailEntity.AccountID = detailDTOItem.AccountID;
                detailEntity.CreatedBy = detailDTOItem.CreatedBy;
                detailEntity.CreatedDate = detailDTOItem.CreatedDate;
                detailEntity.DetailIID = detailDTOItem.DetailIID;
                detailEntity.HeadID = detailDTOItem.HeadID;

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
            if (headEntity ==null)
            {
                return HeadDTO;
            }
            HeadDTO.CreatedBy = headEntity.CreatedBy;
            HeadDTO.CreatedDate = headEntity.CreatedDate;
            HeadDTO.DocumentStatusID = headEntity.DocumentStatusID;
            HeadDTO.DocumentTypeID = headEntity.DocumentTypeID;
            HeadDTO.EntryDate = headEntity.EntryDate;
            HeadDTO.ProcessingStatusID = headEntity.ProcessingStatusID;
            HeadDTO.Remarks = headEntity.Remarks;
            HeadDTO.UpdatedBy = headEntity.UpdatedBy;
            HeadDTO.UpdatedDate = headEntity.UpdatedDate;
            HeadDTO.HeadIID = headEntity.HeadIID;
            if (headEntity.DocumentReferenceStatusMap != null && headEntity.DocumentReferenceStatusMap.DocumentStatus != null)
            {
                HeadDTO.DocumentStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.DocumentStatusID.ToString(), Value = headEntity.DocumentReferenceStatusMap.DocumentStatus.StatusName };
            }
            if (headEntity.DocumentType != null)
            {
                HeadDTO.DocumentType = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.DocumentTypeID.ToString(), Value = headEntity.DocumentType.TransactionTypeName };
            }
            if (headEntity.TransactionStatus != null)
            {
                HeadDTO.TransactionStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = headEntity.ProcessingStatusID.ToString(), Value = headEntity.TransactionStatus.Description };
            }



            HeadDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();

            foreach (AssetTransactionDetail detailEntityItem in headEntity.AssetTransactionDetails)
            {
                AssetTransactionDetailsDTO detailDTO = new AssetTransactionDetailsDTO();

              

                detailDTO.Amount = detailEntityItem.Amount;
                detailDTO.AssetID = detailEntityItem.AssetID;
                detailDTO.Quantity = detailEntityItem.Quantity;
                detailDTO.StartDate = detailEntityItem.StartDate;
                detailDTO.UpdatedBy = detailEntityItem.UpdatedBy;
                detailDTO.UpdatedDate = detailEntityItem.UpdatedDate;
                detailDTO.AccountID = detailEntityItem.AccountID;
                detailDTO.Createdby = detailEntityItem.CreatedBy;
                detailDTO.CreatedDate = detailEntityItem.CreatedDate;
                detailDTO.DetailIID = detailEntityItem.DetailIID;
                detailDTO.HeadID = detailEntityItem.HeadID;

                //Set Asset Object
                if(detailEntityItem.Asset !=null)
                {
                    AssetDTO assetDTO = new AssetDTO();                    
                    assetDTO.AssetCategoryID = detailEntityItem.Asset.AssetCategoryID;
                    if(detailEntityItem.Asset.AssetCategory !=null)
                    {
                        assetDTO.AssetCategory = new KeyValueDTO() { Key = detailEntityItem.Asset.AssetCategory.AssetCategoryID.ToString(), Value= detailEntityItem.Asset.AssetCategory.CategoryName};
                    }

                    assetDTO.AccumulatedDepGLAccID = detailEntityItem.Asset.AccumulatedDepGLAccID;
                    assetDTO.DepreciationExpGLAccId = detailEntityItem.Asset.DepreciationExpGLAccId;
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

                    detailDTO.AssetCode = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = detailEntityItem.AssetID.ToString(), Value = detailEntityItem.Asset.AssetCode };
                    detailDTO.AssetGlAccID = detailEntityItem.AccountID;
                    detailDTO.Asset = assetDTO;
                    
                }
                
                //Set Account Object
                if (detailEntityItem.Account !=null)
                {
                    AccountDTO accountDTO = new AccountDTO();
                    accountDTO.AccountID = detailEntityItem.Account.AccountID;
                    accountDTO.AccountName = detailEntityItem.Account.AccountName;
                    accountDTO.Alias = detailEntityItem.Account.Alias;

                    detailDTO.AssetGlAccount = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = detailEntityItem.AccountID.ToString(), Value = detailEntityItem.Account.AccountName };

                    detailDTO.Account = accountDTO;
                }
                

                    HeadDTO.AssetTransactionDetails.Add(detailDTO);
            }
            return HeadDTO;
        }

        public List<AssetTransactionHeadDTO> GetAssetTransactionHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            List<AssetTransactionHead> assetTransactionHeadEntity = new List<AssetTransactionHead>();
            assetTransactionHeadEntity = fixedAssetsRepository.GetAssetTransactionHeads(referenceTypes, transactionStatus);
            List<AssetTransactionHeadDTO> DTOList = new List<AssetTransactionHeadDTO>();
            foreach (AssetTransactionHead head in assetTransactionHeadEntity )
            {
                DTOList.Add( ToAssetTransactionHeadDTO(head));
            }
                return DTOList;
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto)
        {
            var assetTransactionHead = new AssetTransactionHead();
            assetTransactionHead.HeadIID = dto.HeadIID;
            assetTransactionHead.ProcessingStatusID = dto.ProcessingStatusID;
            assetTransactionHead.UpdatedBy = _callContext.IsNotNull() ? _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : default(int) : default(int);
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

        public decimal GetAccumulatedDepreciation(long assetId, int documentTypeID)
        {
            return new FixedAssetsRepository().GetAccumulatedDepreciation(assetId, documentTypeID);
        }
    }
}
