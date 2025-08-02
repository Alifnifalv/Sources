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
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetCustomerGroup(CustomerGroup customergroup)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (customergroup != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerGroupIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GroupName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PointLimit" });


                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          customergroup.CustomerGroupIID, customergroup.GroupName,customergroup.PointLimit
                        }
                });
            }

            return searchDTO;
        }
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetCustomer(Customer customer, Customer parentCustomer)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (customer != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FirstName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EmailID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Telephone" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerCR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CRExpiryDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GroupName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "StatusName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CivilIDNumber" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ParentFirstName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsBlocked" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsConfirmed" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsVerified" });


                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                      {
                        customer.CustomerIID,customer.FirstName,customer.Login.IsNotNull()?customer.Login.LoginEmailID:"-",
                        customer.Contacts.IsNotNull() && customer.Contacts.Count>0?customer.Contacts.First().MobileNo1 + "," + customer.Contacts.First().MobileNo2 + "," + customer.Contacts.First().PhoneNo1:string.Empty,
                        customer.CustomerCR,customer.CRExpiryDate,customer.GroupID != null ?customer.CustomerGroup.GroupName : "-",customer.StatusID != null ? customer.CustomerStatus.StatusName :"-" ,
                        customer.CivilIDNumber,parentCustomer.IsNotNull() ? parentCustomer.FirstName :"-",customer.CustomerSettings.Select(x=>x.IsBlocked).FirstOrDefault(),
                        customer.CustomerSettings.Select(x=>x.IsConfirmed).FirstOrDefault(),customer.CustomerSettings.Select(x=>x.IsVerified).FirstOrDefault()
                      }
                });
            }

            return searchDTO;
        }
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetCustomerGroupDeliveryTypes(List<DeliveryTypes1> CustomerDeliveryType, long IID)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (CustomerDeliveryType.IsNotNull()) 
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerGroupDeliveryTypeMapIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerGroupID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalFrom" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalTo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryCharge" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryChargePercentage" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsDeliveryAvailable" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TimeStamps" });

                foreach (var group in CustomerDeliveryType)
                {
                    CustomerGroupDeliveryTypeMap cgdtMap = new CustomerGroupDeliveryTypeMap();
                    cgdtMap = new DistributionRepository().GetProductDeliveryTypeByCustomerGroupID(IID, Convert.ToInt32(group.DeliveryTypeID));
                    //  else
                    //var cgdtMap = new DistributionRepository().GetProductDeliveryTypeByCustomerGroupID(IID1);

                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            cgdtMap.IsNotNull() ? cgdtMap.CustomerGroupDeliveryTypeMapIID : 0,
                            cgdtMap.IsNotNull() ? cgdtMap.DeliveryTypeID : group.DeliveryTypeID,
                            group.DeliveryTypeName,
                            cgdtMap.IsNotNull() ? cgdtMap.CustomerGroupID :IID,
                            cgdtMap.IsNotNull() ? cgdtMap.CartTotalFrom : null,
                            cgdtMap.IsNotNull() ? cgdtMap.CartTotalTo : null,
                            cgdtMap.IsNotNull() ? cgdtMap.DeliveryCharge : null,
                            cgdtMap.IsNotNull() ? cgdtMap.DeliveryChargePercentage  : null,
                            cgdtMap.IsNotNull() ? cgdtMap.IsSelected : false,
                            cgdtMap.IsNotNull() ? cgdtMap.CreatedBy : null,
                            cgdtMap.IsNotNull() ? cgdtMap.CreatedDate : null,
                            cgdtMap.IsNotNull() ? cgdtMap.UpdatedBy : null,
                            cgdtMap.IsNotNull() ? cgdtMap.UpdatedDate : null,
                            cgdtMap.IsNotNull() ? Convert.ToBase64String(cgdtMap.TimeStamps) : null,
                        }
                    });
                }
            }
            return searchDTO;
        }
    }
}
