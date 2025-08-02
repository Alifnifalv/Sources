using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.ValueObjects;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetTransactions(List<Transaction> trans)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (trans != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Amount" });

                foreach (var tran in trans)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                           tran.TransactionTypeName, tran.TransactionNo, tran.CreatedDate, tran.Amount
                        }
                    });
                }
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetBranchwiseInventory(List<BranchInventory> inventories)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (inventories != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BranchID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BranchName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Batch" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Quantity" });

                foreach (var tran in inventories)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                           tran.BranchID, tran.BranchName, tran.Batch, tran.Quantity
                        }
                    });
                }
            }

            return searchDTO;
        }
    }
}
