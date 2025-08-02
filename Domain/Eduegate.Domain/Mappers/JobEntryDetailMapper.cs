using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Entity.CustomEntity;

namespace Eduegate.Domain.Mappers
{
    public class JobEntryDetailMapper: IDTOEntityMapper<JobEntryDetailDTO, JobEntryDetail>
    {
        private CallContext _context;

        public static JobEntryDetailMapper Mapper(CallContext context)
        {
            var mapper = new JobEntryDetailMapper();
            mapper._context = context;
            return mapper;
        }

        public JobEntryDetail ToEntity(JobEntryDetailDTO jobEntryDetailDTO) //Translating job entry detail data from DTO to entity
        {
            if (jobEntryDetailDTO.IsNotNull())
            {
                Location location = new ProductCatalogRepository().GetLocationDetailByBarCode(jobEntryDetailDTO.ValidatedLocationBarcode);

                var jobEntryDetail = new JobEntryDetail()
                {
                    JobEntryDetailIID = jobEntryDetailDTO.JobEntryDetailIID,
                    JobEntryHeadID = jobEntryDetailDTO.JobEntryHeadID,
                    ProductSKUID = jobEntryDetailDTO.ProductSKUID,
                    UnitPrice = jobEntryDetailDTO.UnitPrice,
                    Quantity = jobEntryDetailDTO.Quantity,
                    LocationID = jobEntryDetailDTO.LocationID,
                    IsQuantiyVerified = jobEntryDetailDTO.IsQuantiyVerified,
                    IsBarCodeVerified = jobEntryDetailDTO.IsBarCodeVerified,
                    IsLocationVerified = jobEntryDetailDTO.IsLocationVerified,
                    JobStatusID = jobEntryDetailDTO.JobStatusID,
                    ValidatedQuantity = jobEntryDetailDTO.ValidatedQuantity,
                    ValidatedLocationID = location.IsNotNull() ? Convert.ToInt32(location.LocationIID) : (int?)null,
                    ValidatedPartNo = jobEntryDetailDTO.ValidatedPartNo,
                    ValidationBarCode = jobEntryDetailDTO.ValidationBarCode,
                    Remarks = jobEntryDetailDTO.Remarks,
                    ParentJobEntryHeadID = jobEntryDetailDTO.ParentJobEntryHeadID,
                    UpdatedBy = _context != null ? _context.LoginID.HasValue ? int.Parse(_context.LoginID.ToString()) : jobEntryDetailDTO.UpdatedBy : jobEntryDetailDTO.UpdatedBy,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = jobEntryDetailDTO.JobEntryDetailIID == 0 ? DateTime.Now : jobEntryDetailDTO.CreatedDate,
                    CreatedBy = _context != null ? _context.LoginID.HasValue ? jobEntryDetailDTO.JobEntryDetailIID == 0 ? (int)_context.LoginID : jobEntryDetailDTO.CreatedBy
                      : jobEntryDetailDTO.UpdatedBy : jobEntryDetailDTO.UpdatedBy,
                    //TimeStamps = jobEntryDetailDTO.TimeStamps == null ? null : Convert.FromBase64String(jobEntryDetailDTO.TimeStamps),
                    AWBNo = jobEntryDetailDTO.AWBNo,
                };

                if (jobEntryDetail.JobEntryDetailIID <= 0)
                {
                    //jobEntryDetail.TimeStamps = null;
                }

                return jobEntryDetail;
            }
            else
            {
                return new JobEntryDetail();
            }
        }

        public JobEntryDetailDTO ToDTO(JobEntryDetail jobEntryDetail) //Translating job entry detail data from entity to DTO
        {
            return ToDTO(jobEntryDetail, _context.CompanyID.Value);
        }

        public JobEntryDetailDTO ToDTO(JobEntryDetail jobEntryDetail, int companyID) //Translating job entry detail data from entity to DTO
        {
            if (jobEntryDetail.IsNotNull())
            {
                ProductSKUMap productSkuDetail = new ProductSKUMap();
                ProductImageMap productImage = new ProductImageMap();
                Location location = new Location();

                if (jobEntryDetail.ProductSKUID.HasValue)
                {
                    productSkuDetail = new ProductDetailRepository().GetProductSkuDetails((long)jobEntryDetail.ProductSKUID);
                    productImage = new ProductDetailRepository().GetProductImage(productSkuDetail.ProductSKUMapIID);
                    location = new ProductCatalogRepository().GetProductSKULocation(Convert.ToInt32(jobEntryDetail.ProductSKUID.Value));
                } 

                var locationDetails = jobEntryDetail.ValidatedLocationID.HasValue ? new ProductCatalogRepository().GetLocationDetail(Convert.ToInt32(jobEntryDetail.ValidatedLocationID)) : null;

                var jobEntryDetailDTO = new JobEntryDetailDTO()
                {
                    JobEntryDetailIID = jobEntryDetail.JobEntryDetailIID,
                    JobEntryHeadID = jobEntryDetail.JobEntryHeadID,
                    ProductSKUID = jobEntryDetail.ProductSKUID,
                    ProductSkuName = jobEntryDetail.ProductSKUID.HasValue ?  new ProductDetailRepository().GetProductAndSKUNameByID(Convert.ToInt32(jobEntryDetail.ProductSKUID)) : null,  
                    UnitPrice = jobEntryDetail.UnitPrice,
                    Quantity = jobEntryDetail.Quantity,
                    BarCode = productSkuDetail.BarCode,
                    ProductImage = productImage == null ? null : productImage.ImageFile,
                    LocationID =  Convert.ToInt32(location.LocationIID),
                    LocationName = location.Description, 
                    ProductPrice = productSkuDetail.ProductPrice,
                    LocationBarcode = location.Barcode,
                    PartNo = productSkuDetail.PartNo,
                    IsQuantiyVerified = jobEntryDetail.IsQuantiyVerified,
                    IsBarCodeVerified = jobEntryDetail.IsBarCodeVerified,
                    IsLocationVerified = jobEntryDetail.IsLocationVerified,
                    JobStatusID = jobEntryDetail.JobStatusID,
                    ValidatedQuantity = jobEntryDetail.ValidatedQuantity,
                    ValidatedLocationBarcode = locationDetails == null ? null : locationDetails.Barcode,
                    ValidatedPartNo = jobEntryDetail.ValidatedPartNo,
                    ValidationBarCode = jobEntryDetail.ValidationBarCode,
                    Remarks = jobEntryDetail.Remarks,
                    ParentJobEntryHeadID = jobEntryDetail.ParentJobEntryHeadID.IsNotNull() ? Convert.ToInt32(jobEntryDetail.ParentJobEntryHeadID) : jobEntryDetail.ParentJobEntryHeadID,
                    CreatedBy = jobEntryDetail.CreatedBy,
                    CreatedDate = jobEntryDetail.CreatedDate,
                    UpdatedBy = jobEntryDetail.UpdatedBy,
                    UpdatedDate = jobEntryDetail.UpdatedDate,
                    //TimeStamps = jobEntryDetail.TimeStamps == null ? null : Convert.ToBase64String(jobEntryDetail.TimeStamps),
                    ProductIID = Convert.ToInt64(productSkuDetail.ProductID),
                    AWBNo = jobEntryDetail.AWBNo,
                };

                return jobEntryDetailDTO;
            }
            else
            {
                return new JobEntryDetailDTO();
            }
        }

        public JobEntryDetail Clone(JobEntryDetail jobEntryDetail)
        {
            if (jobEntryDetail != null)
            {
                return new JobEntryDetail()
                {
                    JobEntryDetailIID = jobEntryDetail.JobEntryDetailIID,
                    JobEntryHeadID = jobEntryDetail.JobEntryHeadID,
                    ProductSKUID = jobEntryDetail.ProductSKUID,
                    ParentJobEntryHeadID = jobEntryDetail.ParentJobEntryHeadID,
                    UnitPrice = jobEntryDetail.UnitPrice,
                    Quantity = jobEntryDetail.Quantity,
                    LocationID = jobEntryDetail.LocationID,
                    IsQuantiyVerified = jobEntryDetail.IsQuantiyVerified,
                    IsBarCodeVerified = jobEntryDetail.IsBarCodeVerified,
                    IsLocationVerified = jobEntryDetail.IsLocationVerified,
                    JobStatusID = jobEntryDetail.JobStatusID,
                    ValidatedQuantity = jobEntryDetail.ValidatedQuantity,
                    ValidatedLocationID = jobEntryDetail.ValidatedLocationID,
                    ValidatedPartNo = jobEntryDetail.ValidatedPartNo,
                    ValidationBarCode = jobEntryDetail.ValidationBarCode,
                    Remarks = jobEntryDetail.Remarks,
                    CreatedBy = jobEntryDetail.CreatedBy,
                    UpdatedBy = jobEntryDetail.UpdatedBy,
                    CreatedDate = jobEntryDetail.CreatedDate,
                    UpdatedDate = jobEntryDetail.UpdatedDate,
                    //TimeStamps = jobEntryDetail.TimeStamps,
                    AWBNo = jobEntryDetail.AWBNo,
                };
            }
            else return new JobEntryDetail();
        }
    }
}
