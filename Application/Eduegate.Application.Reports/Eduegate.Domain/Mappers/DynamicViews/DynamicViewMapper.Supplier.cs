using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
       
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetSupplier(Supplier supplier)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (supplier != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SupplierIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FirstName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LastName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "VendorCR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CRExpiry" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CompanyLocation" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "StatusName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EmployeeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdateDate" });


                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {

                          supplier.SupplierIID,supplier.FirstName,supplier.LastName,supplier.VendorCR,supplier.CRExpiry,
                          supplier.CompanyLocation,supplier.SupplierStatus.IsNotNull() ? supplier.SupplierStatus.StatusName : null,supplier.EmployeeID!=null?supplier.Employee.EmployeeName:"-",supplier.CreatedDate,supplier.UpdateDate
                       
                        }
                });
            }

            return searchDTO;
        }
    }
}
