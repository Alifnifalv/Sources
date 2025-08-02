using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetDeliveryTypes(List<DeliveryTypes1> types)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (types != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsDeliveryAvailable" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalFrom" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalTo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryCharge" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryChargePercentage" });

                foreach (var type in types)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          type.DeliveryTypeID,type.DeliveryTypeName,false,null,null,null,null                    
                        }
                    });
                }
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductTypeDeliveryTypes(List<ProductTypeDeliveryTypeMap> pdTypes, bool IsProduct, long IID)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (pdTypes.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductDeliveryTypeMapIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUMapID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryCharge" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryChargePercentage" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsDeliveryAvailable" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalFrom" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalTo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TimeStamps" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BranchID" });

                foreach (var type in pdTypes)
                {
                    ProductDeliveryTypeMap pdtMap = new ProductDeliveryTypeMap();

                    if (IsProduct)
                        pdtMap = new DistributionRepository().GetProductDeliveryTypeByProductID(IID, Convert.ToInt32(type.DeliveryTypeID));
                    else
                        pdtMap = new DistributionRepository().GetProductDeliveryTypeBySkuID(IID, Convert.ToInt32(type.DeliveryTypeID));

                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            pdtMap.IsNotNull() ? pdtMap.ProductDeliveryTypeMapIID : 0,
                            pdtMap.IsNotNull() ? pdtMap.ProductID : IsProduct ? IID : (long?)null,
                            pdtMap.IsNotNull() ? pdtMap.ProductSKUMapID : !IsProduct ? IID : (long?)null,
                            pdtMap.IsNotNull() ? pdtMap.DeliveryTypeID : type.DeliveryTypeID,
                            type.DeliveryTypes1.DeliveryTypeName,
                            pdtMap.IsNotNull() ? pdtMap.DeliveryCharge : null,
                            pdtMap.IsNotNull() ? pdtMap.DeliveryChargePercentage : null,
                            pdtMap.IsNotNull() ? pdtMap.IsSelected : false,
                            null,
                            null,
                            pdtMap.IsNotNull() ? pdtMap.CreatedBy : null,
                            pdtMap.IsNotNull() ? pdtMap.CreatedDate : null,
                            pdtMap.IsNotNull() ? pdtMap.UpdatedBy : null,
                            pdtMap.IsNotNull() ? pdtMap.UpdatedDate : null,
                            pdtMap.IsNotNull() ? Convert.ToBase64String(pdtMap.TimeStamps) : null,
                             pdtMap.IsNotNull() ? pdtMap.BranchID : null
                        }
                    });
                }
            }

            return searchDTO;
        }

    }
}
