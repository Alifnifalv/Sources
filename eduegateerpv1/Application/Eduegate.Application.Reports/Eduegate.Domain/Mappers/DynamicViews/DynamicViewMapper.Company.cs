using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetCompanyDetails(Company company)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (company.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CompanyID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CompanyName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                      company.CompanyID, company.CompanyName,company.CreatedDate,company.UpdatedDate                                            
                    }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetDepartmentDetails(Department department) 
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (department.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DepartmentID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DepartmentName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                      department.DepartmentID,department.DepartmentName,department.CreatedDate,department.UpdatedDate
                    }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetWarehouseDetails(Warehouse warehouse)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (warehouse.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WarehouseID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WarehouseName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                      warehouse.WarehouseID,warehouse.WarehouseName,warehouse.CreatedDate,warehouse.UpdatedDate
                    }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetBranchGroupDetails(BranchGroup branchGroup)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (branchGroup.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BranchGroupIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GroupName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                      branchGroup.BranchGroupIID,branchGroup.GroupName,branchGroup.CreatedDate,branchGroup.UpdatedDate
                    }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetBranchDetails(Branch branch)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (branch.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BranchIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BranchName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                      branch.BranchIID,branch.BranchName,branch.CreatedDate,branch.UpdatedDate
                    }
                });
            }

            return searchDTO;
        }
    }
}
