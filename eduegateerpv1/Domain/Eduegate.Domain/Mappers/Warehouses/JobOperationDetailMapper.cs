using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.Warehouses
{
    public class JobOperationDetailMapper : IDTOEntityMapper<JobOperationDetailDTO, JobEntryDetail>
    {
        private CallContext _context;

        public static JobOperationDetailMapper Mapper(CallContext context)
        {
            var mapper = new JobOperationDetailMapper();
            mapper._context = context;
            return mapper;
        }

        public JobOperationDetailDTO ToDTO(JobEntryDetail jobEntryDetail)
        {
            return ToDTO(jobEntryDetail, _context.CompanyID.Value);
        }

        public JobOperationDetailDTO ToDTO(JobEntryDetail jobEntryDetail, int companyID)
        {
            var productRepository = new ProductDetailRepository();
            var productSkuDetail = new ProductSKUDetail();
            var location = new Location();
            var categoryRepository = new ProductCatalogRepository();

            if (jobEntryDetail.ProductSKUID.HasValue)
            {
                productSkuDetail = categoryRepository.GetProductSKUDetail(jobEntryDetail.ProductSKUID.Value);
                location = categoryRepository.GetProductSKULocation(jobEntryDetail.ProductSKUID.Value);
            }
         
            return new JobOperationDetailDTO()
            {
                TransactionDetailID = jobEntryDetail.JobEntryDetailIID,
                JobEntryHeadID = jobEntryDetail.JobEntryHeadID.HasValue ? jobEntryDetail.JobEntryHeadID.Value : 0,
                ProductSKUID =  jobEntryDetail.ProductSKUID,
                ProductDescription = jobEntryDetail.ProductSKUID.HasValue ? productRepository.GetProductAndSKUNameByID(jobEntryDetail.ProductSKUID.Value) : string.Empty,
                UnitPrice = jobEntryDetail.UnitPrice,
                Quantity = jobEntryDetail.Quantity,
                BarCode = productSkuDetail.Barcode,
                ProductImage = productSkuDetail.ImageFile,
                Price = jobEntryDetail.UnitPrice,//productSkuDetail.ProductPrice,
                LocationID = Convert.ToInt32(location.LocationIID),
                LocationBarcode = location.Barcode,
                PartNo = productSkuDetail.PartNo,
                IsQuantiyVerified = jobEntryDetail.IsQuantiyVerified,
                IsBarCodeVerified = jobEntryDetail.IsBarCodeVerified,
                IsLocationVerified = jobEntryDetail.IsLocationVerified,
                JobStatusID = jobEntryDetail.JobStatusID,
                ValidatedQuantity = jobEntryDetail.ValidatedQuantity,
                ValidatedLocationBarcode = jobEntryDetail.ValidatedLocationID.HasValue ? categoryRepository.GetLocationDetail(jobEntryDetail.ValidatedLocationID.Value).Barcode : null,
                ValidatedPartNo = jobEntryDetail.ValidatedPartNo,
                ValidationBarCode = jobEntryDetail.ValidationBarCode,
                IsSerialNumber = productSkuDetail.IsNotNull() && productSkuDetail.IsSerialNumber != null ? Convert.ToBoolean(productSkuDetail.IsSerialNumber) : false,
                IsSerialNumberOnPurchase = productSkuDetail.IsNotNull() && productSkuDetail.IsSerialNumberOnPurchase != null ? Convert.ToBoolean(productSkuDetail.IsSerialNumberOnPurchase) : false,          
                Remarks = jobEntryDetail.Remarks,
                CreatedBy = jobEntryDetail.CreatedBy,
                CreatedDate = jobEntryDetail.CreatedDate,
                UpdatedBy = jobEntryDetail.UpdatedBy,
                UpdatedDate = jobEntryDetail.UpdatedDate,
                //TimeStamps = jobEntryDetail.TimeStamps == null ? null : Convert.ToBase64String(jobEntryDetail.TimeStamps),
                ProductIID = productSkuDetail.ProductIID,
                AWBNo = jobEntryDetail.AWBNo,
                Amount = jobEntryDetail.UnitPrice * jobEntryDetail.Quantity,
            };
        }

        public JobEntryDetail ToEntity(JobOperationDetailDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
