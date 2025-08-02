using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetBranchTransfer(TransactionDTO transactionHead)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (transactionHead != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "HeadIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FromBranchName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ToBranchName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DocumentTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Description" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerEmailID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SupplierID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SupplierName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DocumentStatusName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionStatusName" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                           transactionHead.TransactionHead.HeadIID,
                           transactionHead.TransactionHead.TransactionNo,
                           transactionHead.TransactionHead.TransactionDate,
                           transactionHead.TransactionHead.BranchName,
                           transactionHead.TransactionHead.ToBranchName,
                           transactionHead.TransactionHead.DocumentTypeName,
                           transactionHead.TransactionHead.Description,
                           transactionHead.TransactionHead.CustomerEmailID,
                           transactionHead.TransactionHead.CustomerName,
                           transactionHead.TransactionHead.SupplierID,
                           transactionHead.TransactionHead.SupplierName,
                           transactionHead.TransactionHead.CreatedDate,
                           transactionHead.TransactionHead.UpdatedDate,
                           transactionHead.TransactionHead.DocumentStatusName,
                           transactionHead.TransactionHead.TransactionStatusName,
                        }
                });
            }

            return searchDTO;
        }
    }
}
