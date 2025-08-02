using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetWarehouseDocumentTypeDetails(DocumentType documentType,DocumentReferenceType reference)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (documentType.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DocumentTypeID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InventoryTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          documentType.DocumentTypeID,documentType.TransactionTypeName,reference.InventoryTypeName,
                          documentType.CreatedDate,documentType.UpdatedDate                     
                        }
                });
            }

            return searchDTO;
        }
    }
}
