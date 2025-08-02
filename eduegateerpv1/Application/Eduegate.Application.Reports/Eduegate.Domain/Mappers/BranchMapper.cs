using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.Mutual;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Domain.Mappers
{
    public class BranchMapper : IDTOEntityMapper<BranchDTO, Branch>
    {
        private CallContext _context;
        public static BranchMapper Mapper(CallContext context)
        {
            var mapper = new BranchMapper();
            mapper._context = context;
            return mapper;
        }

        public BranchDTO ToDTO(Branch entity)
        {
            return ToDTO(entity, true);
        }

        public BranchDTO ToDTO(Branch entity, bool bindRelations)
        {
            var branch = new BranchDTO()
            {
                BranchIID = entity.BranchIID,
                BranchName = entity.BranchName,
                UpdatedBy = entity.UpdatedBy,
                CreatedBy = entity.CreatedBy,
                BranchGroupID = entity.BranchGroupID,
                StatusID = Convert.ToByte(entity.StatusID),
                IsMarketPlace = entity.IsMarketPlace,
                WarehouseID = entity.WarehouseID,
                TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                CompanyID = entity.CompanyID,
                Longitude = entity.Longitude,
                Latitude = entity.Latitude
            };

            if (bindRelations)
            {
                branch.PriceLists = new List<PriceListDetailDTO>();

                foreach (var price in entity.ProductPriceListBranchMaps)
                {
                    branch.PriceLists.Add(new PriceListDetailDTO() { PriceListID = price.ProductPriceListID.Value, PriceDescription = price.ProductPriceList.PriceDescription });
                }
            }

            if (bindRelations)
            {
                branch.DocumentTypeMaps = new List<DocumentTypeDetailDTO>();

                foreach (var doc in entity.BranchDocumentTypeMaps)
                {
                    branch.DocumentTypeMaps.Add(new DocumentTypeDetailDTO() { DocumentTypeID = doc.DocumentTypeID.Value, DocumentName = doc.DocumentType.TransactionTypeName });
                }
            }

            if (entity.BranchCultureDatas != null)
            {
                branch.BranchCultureDatas = BranchCultureDataMapper.Mapper(_context).ToDTO(entity.BranchCultureDatas.ToList());
            }

            return branch;
        }

        public Branch ToEntity(BranchDTO dto)
        {
            var entity = new Branch()
            {
                UpdatedDate = DateTime.Now,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                BranchIID = dto.BranchIID,
                BranchName = dto.BranchName,
                WarehouseID = dto.WarehouseID,
                IsMarketPlace = dto.IsMarketPlace,
                BranchGroupID = dto.BranchGroupID,
                StatusID = dto.StatusID,
                CompanyID = dto.CompanyID,
                Longitude = dto.Longitude,
                Latitude = dto.Latitude
            };

            if (entity.BranchIID == 0)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            if (entity.CompanyID == 0)
            {
                entity.CompanyID = _context.CompanyID;
            }

            entity.ProductPriceListBranchMaps = new List<ProductPriceListBranchMap>();

            if (dto.PriceLists != null)
            {
                foreach (var price in dto.PriceLists)
                {
                    entity.ProductPriceListBranchMaps.Add(new ProductPriceListBranchMap() { ProductPriceListID = price.PriceListID, BranchID = dto.BranchIID });
                }
            }


            entity.BranchDocumentTypeMaps = new List<BranchDocumentTypeMap>();

            if (dto.DocumentTypeMaps != null)
            {
                foreach (var doc in dto.DocumentTypeMaps)
                {
                    entity.BranchDocumentTypeMaps.Add(new BranchDocumentTypeMap() { DocumentTypeID = int.Parse(doc.DocumentTypeID.ToString()), BranchID = dto.BranchIID });
                }
            }

            if (dto.BranchCultureDatas != null)
            {
                entity.BranchCultureDatas = BranchCultureDataMapper.Mapper(_context).ToEntity(dto.BranchCultureDatas);
            }

            return entity;
        }      
    }
}
