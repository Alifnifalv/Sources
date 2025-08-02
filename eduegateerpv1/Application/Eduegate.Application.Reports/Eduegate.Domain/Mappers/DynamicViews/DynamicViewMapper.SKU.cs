using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Search;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductSKUDetails(ProductSKUMap sku, Product product)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (sku != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUMapIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PartNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BarCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BrandID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SKUName" });

                searchDTO.Rows.Add(new DataRowDTO()
                {
                    DataCells = new DataCellListDTO()
                        {
                            sku.ProductSKUMapIID,sku.ProductID, sku.ProductSKUCode, sku.PartNo,
                            sku.BarCode,
                            sku.CreatedDate, sku.UpdatedDate,
                            product == null ? string.Empty : product.ProductName,
                            product == null ? (long?) null : product.BrandID ,
                            sku.SKUName
                        }
                });
            }

            return searchDTO;
        }
    }
}
