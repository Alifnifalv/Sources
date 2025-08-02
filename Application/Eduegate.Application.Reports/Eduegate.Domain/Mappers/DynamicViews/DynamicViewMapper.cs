using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public static DynamicViewMapper Mapper { get { return new DynamicViewMapper(); } }


        public Eduegate.Services.Contracts.Search.SearchResultDTO GetVoucher(Voucher voucher)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (voucher != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "VoucherIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "VoucherNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Description" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "VoucherStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FirstName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ExpiryDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CurrentBalance" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Amount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "VoucherType" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          voucher.VoucherIID,voucher.VoucherNo,voucher.Description,voucher.VoucherStatus.StatusName,
                            voucher.CustomerID, voucher.Customer != null ? voucher.Customer.FirstName : string.Empty,
                           voucher.ExpiryDate,voucher.CurrentBalance,
                          voucher.Amount,voucher.VoucherType.VoucherTypeName
                        }
                });
            }
            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetPurchaseTransactionDetail(TransactionHead transactionhead)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (transactionhead != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "HeadIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SupplierID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Supplier" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Customer" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BranchName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EntitlementName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryMethod" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DueDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CurrencyName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EmployeeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryOption" });

                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionStatusID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DocumentStatusID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DocumentStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobOperationStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobEntryHeadId" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobStatusName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "From" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Created" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedByforSalesOrder" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ReceivingMethod" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ReturnMethod" });

                var documentStatus = transactionhead.DocumentStatusID.HasValue ? new MutualRepository().GetDocumentStatus(transactionhead.DocumentStatusID.Value) : null;
                var created = transactionhead.CreatedBy.HasValue ? new AccountRepository().GetLoginDetailByLoginID(long.Parse(transactionhead.CreatedBy.ToString())) : null;
                var createdName = transactionhead.CreatedBy.HasValue ? new CustomerRepository().GetCustomerByLoginID(created.LoginIID) : null;
                var tshoppingcartmaps = new UserServiceRepository().GetTransactionHeadShoppingCartMap(transactionhead.HeadIID);
                var createdby = tshoppingcartmaps.IsNotNull() ? new ShoppingCartRepository().GetShoppingCartLogin(tshoppingcartmaps.ShoppingCartID.Value) : null;
                var createdbyName = createdby.IsNotNull() && createdby.LoginIID.IsNotNull() ? new CustomerRepository().GetCustomerByLoginID(createdby.LoginIID) : null;

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                        transactionhead.HeadIID,transactionhead.SupplierID,transactionhead.Supplier != null ? transactionhead.Supplier.FirstName : string.Empty,
                        transactionhead.CustomerID,
                        transactionhead.Customer != null ? transactionhead.Customer.FirstName : string.Empty,
                        transactionhead.TransactionNo,transactionhead.Branch != null ? transactionhead.Branch.BranchName : string.Empty,
                        transactionhead.DocumentType != null ? transactionhead.DocumentType.TransactionTypeName : string.Empty,
                        transactionhead.EntityTypeEntitlement != null ? transactionhead.EntityTypeEntitlement.EntitlementName  :string.Empty,
                        transactionhead.DeliveryType != null ? transactionhead.DeliveryType.DeliveryMethod : string.Empty,
                        transactionhead.TransactionDate,transactionhead.DueDate,transactionhead.DeliveryDate,transactionhead.CreatedDate,transactionhead.UpdatedDate,
                        transactionhead.Customer.IsNotNull()? transactionhead.Customer.FirstName.ToString() : string.Empty,
                        transactionhead.Customer.IsNotNull()? transactionhead.Customer.FirstName.ToString() : string.Empty,
                        transactionhead.Currency != null ?transactionhead.Currency.Name:string.Empty,transactionhead.Employee!= null?transactionhead.Employee.EmployeeName:string.Empty,
                        transactionhead.DeliveryTypeID != null ? transactionhead.DeliveryTypes1.DeliveryTypeName : string.Empty, transactionhead.TransactionStatusID,
                        transactionhead.TransactionStatus == null ? string.Empty : transactionhead.TransactionStatus.Description,
                        transactionhead.DocumentStatusID, documentStatus == null ? string.Empty : documentStatus.StatusName,

                        transactionhead.JobEntryHeads !=null ? transactionhead.JobEntryHeads.Select(x=>x.JobStatusID).FirstOrDefault() :default(int),
                        transactionhead.JobEntryHeads !=null ? transactionhead.JobEntryHeads.Select(x=>x.JobOperationStatusID).FirstOrDefault() :default(byte),
                        transactionhead.JobEntryHeads !=null ? transactionhead.JobEntryHeads.Select(x=>x.JobEntryHeadIID).FirstOrDefault() :default(long),
                        transactionhead.JobEntryHeads !=null ? transactionhead.JobEntryHeads.Select(x=>x.JobStatus.StatusName).FirstOrDefault() : null,
                        transactionhead.TransactionRole.ToString().Contains("4") ? "Mobile App En" : transactionhead.TransactionRole.ToString().Contains("8") ? "Mobile App Ar" : transactionhead.TransactionRole.IsNull() && transactionhead.TransactionNo.ToString().Contains("SO2") ? "ERP" : "Web Site",
                        createdName.IsNotNull() && createdName.FirstName.IsNotNull() ? createdName.FirstName : null,
                        createdbyName.IsNotNull() && createdbyName.FirstName.IsNotNull() ? createdbyName.FirstName : createdName.IsNotNull() && createdName.FirstName.IsNotNull() ? createdName.FirstName : null,
                        transactionhead.Supplier.IsNotNull() ? transactionhead.Supplier.ReceivingMethod != null ? transactionhead.Supplier.ReceivingMethod.ReceivingMethodName : string.Empty : string.Empty,
                        transactionhead.Supplier.IsNotNull() ? transactionhead.Supplier.ReturnMethod != null ? transactionhead.Supplier.ReturnMethod.ReturnMethodName : string.Empty : string.Empty,
                    }
                });
            }

            return searchDTO;
        }




        public Eduegate.Services.Contracts.Search.SearchResultDTO GetJobTransactionDetail(TransactionHead transactionhead)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (transactionhead != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "HeadIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SupplierID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Supplier" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Customer" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BranchName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EntitlementName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryMethod" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DueDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CurrencyName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EmployeeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryOption" });

                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionStatusID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DocumentStatusID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DocumentStatus" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobStatus" });

                var documentStatus = transactionhead.DocumentStatusID.HasValue ? new MutualRepository().GetDocumentStatus(transactionhead.DocumentStatusID.Value) : null;

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                        transactionhead.HeadIID,transactionhead.SupplierID,transactionhead.Supplier != null ? transactionhead.Supplier.FirstName : string.Empty,
                        transactionhead.CustomerID,
                        transactionhead.Customer != null ? transactionhead.Customer.FirstName : string.Empty,
                        transactionhead.TransactionNo,transactionhead.Branch != null ? transactionhead.Branch.BranchName : string.Empty,
                        transactionhead.DocumentType != null ? transactionhead.DocumentType.TransactionTypeName : string.Empty,
                        transactionhead.EntityTypeEntitlement != null ? transactionhead.EntityTypeEntitlement.EntitlementName  :string.Empty,
                        transactionhead.DeliveryType != null ? transactionhead.DeliveryType.DeliveryMethod : string.Empty,
                        transactionhead.TransactionDate,transactionhead.DueDate,transactionhead.DeliveryDate,transactionhead.CreatedDate,transactionhead.UpdatedDate,
                        transactionhead.Currency != null ?transactionhead.Currency.Name:string.Empty,transactionhead.Employee!= null?transactionhead.Employee.EmployeeName:string.Empty,
                        transactionhead.DeliveryTypeID != null ? transactionhead.DeliveryTypes1.DeliveryTypeName : string.Empty, transactionhead.TransactionStatusID,
                        transactionhead.TransactionStatus == null ? string.Empty : transactionhead.TransactionStatus.Description,
                        transactionhead.DocumentStatusID, documentStatus == null ? string.Empty : documentStatus.StatusName,

                    }
                });
            }

            return searchDTO;
        }


        public Eduegate.Services.Contracts.Search.SearchResultDTO GetDeliveryTransactionDetail(TransactionHead transactionhead)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (transactionhead.OrderContactMaps != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FirstName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MobileNo1" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsBillingAddress" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsShippingAddress" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AWBNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProviderName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Latitude" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Longitude" });

                OrderContactMap billing = transactionhead.OrderContactMaps.Where(x => x.IsBillingAddress == true).FirstOrDefault();
                OrderContactMap shipping = transactionhead.OrderContactMaps.Where(x => x.IsShippingAddress == true).FirstOrDefault();

                var Job = new MutualRepository().GetJobByHeadID(transactionhead.HeadIID);
                var Mission = new MutualRepository().GetMissionByJobID(Job.IsNotNull() && Job.JobEntryHeadIID.IsNotNull() ? Job.JobEntryHeadIID : 0);
                var awbno = new WarehouseRepository().GetJobEntryDetails(Mission.IsNotNull() ? Mission.JobEntryHeadIID : 0);
                var getawbno = awbno.IsNotNull() ? awbno.JobEntryDetails.FirstOrDefault() : null;
                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                        shipping !=null ? shipping.FirstName:"-",shipping !=null ? shipping.MobileNo1:"-",
                        billing != null? string.Join(",", billing !=null ? billing.FirstName :"-",billing != null ? billing.MobileNo1:"-",
                        billing !=null ? billing.Block:"",billing !=null ? billing.Street:"",billing !=null ? billing.Floor:"",billing !=null ? billing.BuildingNo:"",
                        billing !=null ? billing.Flat:"",billing !=null ? billing.City:"",billing !=null ? billing.District:"",billing !=null ? billing.AddressName:""):"-",
                        shipping != null? string.Join(",", shipping !=null ? shipping.FirstName :"-",shipping != null ? shipping.MobileNo1:"-",
                        shipping !=null ? shipping.Block:"",shipping !=null ? shipping.Floor:"",shipping !=null ? shipping.BuildingNo :"",shipping !=null ? shipping.Flat:"",
                        shipping !=null ? shipping.City:"",shipping !=null ? shipping.District:"",shipping !=null ? shipping.AddressName:""):"-",
                        string.Concat( Job.IsNotNull() && Job.JobStatusID != null ? Job.JobStatus.StatusName:"","",Job.IsNotNull() && Job.JobOperationStatusID != null ? Job.JobOperationStatus.Description:""),
                        getawbno.IsNotNull() ? getawbno.AWBNo : null,//This has to be Corrected //
                        Mission.IsNotNull() && Mission.ServiceProvider != null && Mission.Employee.IsNotNull() ? Mission.Employee.EmployeeName : null,
                        //string.Concat(billing !=null?billing.FirstName: string.Empty, billing != null?billing.LastName: string.Empty),
                        //string.Concat(shipping !=null?billing.FirstName: string.Empty, billing != null?billing.LastName: string.Empty),
                        shipping!=null && shipping.Latitude.IsNotNull() ? Convert.ToString(shipping.Latitude):"-",shipping!=null && shipping.Longitude.IsNotNull() ? Convert.ToString(shipping.Longitude):"-"
                    }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetDeliveryTransactionHead(List<TransactionHeadShoppingCartMap> thscm, TransactionHead transactionhead)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (transactionhead.OrderContactMaps != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FirstName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AreaName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Block" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Avenue" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LandMark" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Street" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BuildingNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Flat" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Floor" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Telephone" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AswakNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ShoppingCartID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionHeadID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobStatus" });

                OrderContactMap shipping = transactionhead.OrderContactMaps.Where(x => x.IsShippingAddress == true).FirstOrDefault();
                var areaName = shipping != null && shipping.AreaID.HasValue ? new Repository.MutualRepository().GetAreaById(shipping.AreaID.Value) : null;
                var aswakreference = new Repository.TransactionRepository().GetinvoiceByOrderId(transactionhead.HeadIID).FirstOrDefault();
                
                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                        shipping !=null ? shipping.FirstName:"-",
                          areaName !=null ? areaName.AreaName : null,
                          shipping !=null ? shipping.Block:"-",shipping !=null ? shipping.Avenue:"-",shipping !=null ? shipping.LandMark:"-",
                          shipping !=null ? shipping.Street:"-",shipping !=null ? shipping.Flat:"-",shipping !=null ? shipping.BuildingNo:"-",shipping !=null ? shipping.Floor:"-",
                          shipping !=null ?  shipping.MobileNo1 + "," + shipping.MobileNo2:"-",
                          aswakreference != null && aswakreference.Reference != null ? aswakreference.Reference : null,
                          thscm.Select(x=>x.ShoppingCartID).FirstOrDefault(),transactionhead.HeadIID,transactionhead.TransactionNo,
                          transactionhead.Customer.FirstName.IsNotNull() ? transactionhead.Customer.FirstName : null,
                          transactionhead.JobStatusID != null ?transactionhead.JobStatus.StatusName:"-"
                    }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetTransaction(TransactionDTO transactiondto)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            var TransactionDto = new List<TransactionDetailDTO>();
            TransactionDto = transactiondto.TransactionDetails;

            if (transactiondto != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Quantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SKU" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UnitPrice" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Barcode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PartNo" });


                foreach (var sku in TransactionDto)
                {
                    var pDetail = new ProductDetailRepository().GetProductBySKUID((long)sku.ProductSKUMapID);
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          pDetail.ProductIID,pDetail.ProductName,sku.Quantity,sku.SKU,sku.UnitPrice,sku.Barcode,sku.PartNo
                        }
                    });
                }
            }

            return searchDTO;
        }
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetTransaction(List<TransactionDetail> transactiondto)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            //var TransactionDto = new List<TransactionDetailDTO>();
            //TransactionDto = transactiondto.TransactionDetails;

            if (transactiondto != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SKU" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Quantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UnitPrice" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Barcode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PartNo" });

                foreach (var sku in transactiondto)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          sku.ProductSKUMap.SKUName,sku.Quantity,sku.UnitPrice,sku.ProductSKUMap.BarCode,sku.ProductSKUMap.PartNo
                        }
                    });
                }
            }

            return searchDTO;
        }


        public Eduegate.Services.Contracts.Search.SearchResultDTO GetInvoiceDeliveryDetail(TransactionHead transactionhead, TransactionHead head)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (head.OrderContactMaps != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FirstName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MobileNo1" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsBillingAddress" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsShippingAddress" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AWBNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProviderName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Latitude" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Longitude" });

                OrderContactMap billing = head.OrderContactMaps.Where(x => x.IsBillingAddress == true).FirstOrDefault();
                OrderContactMap shipping = head.OrderContactMaps.Where(x => x.IsShippingAddress == true).FirstOrDefault();

                var Job = new MutualRepository().GetJobByHeadID((long)head.HeadIID);
                var Mission = new MutualRepository().GetMissionByJobID(Job.JobEntryHeadIID);

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                        shipping !=null ? shipping.FirstName:"-",shipping !=null ? shipping.MobileNo1:"-",
                        billing != null? string.Join(",", billing !=null ? billing.FirstName :"-",billing != null ? billing.MobileNo1:"-",
                        billing !=null ? billing.Block:"",billing !=null ? billing.Street:"",billing !=null ? billing.Floor:"",billing !=null ? billing.BuildingNo:"",
                        billing !=null ? billing.Flat:"",billing !=null ? billing.City:"",billing !=null ? billing.District:"",billing !=null ? billing.AddressName:""):"-",
                        shipping != null? string.Join(",", shipping !=null ? shipping.FirstName :"-",shipping != null ? shipping.MobileNo1:"-",
                        shipping !=null ? shipping.Block:"",shipping !=null ? shipping.Floor:"",shipping !=null ? shipping.BuildingNo :"",shipping !=null ? shipping.Flat:"",
                        shipping !=null ? shipping.City:"",shipping !=null ? shipping.District:"",shipping !=null ? shipping.AddressName:""):"-",
                        string.Concat(Job.JobStatusID != null ? Job.JobStatus.StatusName:"","",Job.JobOperationStatusID != null ? Job.JobOperationStatus.Description:""),
                        Job.JobEntryDetails1.Select(x=>x.AWBNo).FirstOrDefault(),//This has to be Corrected //
                        Mission.ServiceProvider != null ? Mission.ServiceProvider.ProviderName :"",
                        shipping!=null?Convert.ToString(shipping.Latitude):"-",shipping != null? Convert.ToString(shipping.Longitude):"-"
                    }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetInvoiceDetails(List<TransactionHead> head)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            if (head != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InvoiceID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DocumentStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProcessingStatus" });


                foreach (var Head in head)
                {
                    var documentStatus = Head.DocumentStatusID.HasValue ? new MutualRepository().GetDocumentStatus(Head.DocumentStatusID.Value) : null;
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          Head.HeadIID,Head.DocumentStatusID == null ?string.Empty : documentStatus.StatusName,
                          Head.TransactionStatus == null ? string.Empty : Head.TransactionStatus.Description,

                        }
                    });
                }
            }
            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetEmploymentRequest(Eduegate.Services.Contracts.HR.EmploymentRequestDTO Head)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            if (Head != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EMP_REQ_NO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EmpNumber" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Name" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DOB" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MaritalStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Recruittype" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CIVILID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Agent" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "NAT_CODE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PASSPORT_NO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PassportIssueDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PassportExpiryDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EmploymentType" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ReplacementFor" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "isBudgeted" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Department" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Location" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PayComp" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Group" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MainDesignation" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Designation" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductiveType" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BasicSalary" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProposedIncrease" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IncreaseReason" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SalaryAfterChange" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "RecruitingRemark" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "OfficialEmail" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AlternateEmail" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PersonalRemark" });


                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                        Head.EMP_REQ_NO,
                        Head.EMP_NO,
                        !string.IsNullOrEmpty(Head.F_Name + " " + Head.M_Name + " " + Head.L_Name) ? Head.F_Name + " " + Head.M_Name + " " + Head.L_Name : Head.NAME,
                       Head.DOB.Value.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                       Head.Marital_Status.Value,
                       Head.RecruitmentType.Value,
                       Head.CIVILID,
                       Head.Agent.Value,
                       Head.Nationality.Value,
                       Head.PASSPORT_NO,
                       Head.PassportIssueDate.Value.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                       Head.PassportExpiryDate.Value.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                       Head.EmploymentType.Value,
                       Head.replacedEmployee.IsNull() ?   Head.replacedEmployee.Value : "",
                      Head.isbudgeted.HasValue ?  Head.isbudgeted.Value.ToString() : "",
                      Head.Shop.Value,
                      Head.Location.Value,
                      Head.PayComp.Value,
                      Head.Group.Value,
                      Head.MainDesignation.Value,
                      Head.Designation.Value,
                      Head.ProductiveType.Value,
                      Head.BasicSalary,
                      //Head.ProposedIncrease.HasValue ? Head.ProposedIncrease.Value.ToString() : "",
                      Head.Reason_Basic,
                      //Head.SalaryChangeAfterPeriod.Value,
                      Head.RecuritRemark,
                      Head.EmailID,
                      Head.AlternativeEmailID,
                      Head.EmpPersonalRemarks

                    }
                });

                //var documentStatus = Head.DocumentStatusID.HasValue ? new MutualRepository().GetDocumentStatus(Head.DocumentStatusID.Value) : null;
                //searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                //{
                //    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                //        {
                //          Head.HeadIID,Head.DocumentStatusID == null ?string.Empty : documentStatus.StatusName,
                //          Head.TransactionStatus == null ? string.Empty : Head.TransactionStatus.Description,

                //        }
                //});

            }
            return searchDTO;
        }


        public Eduegate.Services.Contracts.Search.SearchResultDTO GetNotificationDetails(NotificationDTO dto)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            try
            {
                if (dto.IsNotNull())
                {
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "NotificationQueueID" });
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FromEmailID" });
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ToEmailID" });
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Subject" });
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EmailData" });
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "NotificationStatusName" });
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsReprocess" });
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "NotificationProcessingID" });
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Reason" });
                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                    //var alerts =  new NotificationRepository().GetNotificationDetail
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                        dto.NotificationQueueIID,
                        dto.FromEmailID,
                        dto.ToEmailID,
                        dto.Subject,
                        dto.Emaildata.IsNotNull() ? dto.Emaildata : string.Empty,
                        dto.NotificationStatusName.IsNotNull()? dto.NotificationStatusName : string.Empty,
                        dto.IsReprocess.IsNotNull()? dto.IsReprocess : null,
                        dto.NotificationProcessingID.IsNotNull()? dto.NotificationProcessingID: null,
                        dto.Reason.IsNotNullOrEmpty()? dto.Reason : null,
                        dto.CreatedDate.IsNotNull()? dto.CreatedDate: null
                        }
                    });
                }
            }
            catch (Exception)
            {

            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetEmaildataDetails(EmailNotificationData dto)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            try
            {
                if (dto.IsNotNull())
                {

                    searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EmailData" });

                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {

                        dto.EmailData.IsNotNull() ? dto.EmailData : string.Empty,

                        }
                    });
                }
            }
            catch (Exception)
            {

            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetEmploymentAllowance(List<Eduegate.Services.Contracts.HR.EmployementAllowanceDTO> Head)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            if (Head != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Allowance" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Amount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AmountAfterProbation" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Remark" });

                foreach (var head in Head)
                {

                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            head.Allowance.Value,
                            head.Amount.HasValue   ? head.Amount.Value.ToString() : "",
                            head.AmountAfterProbation.HasValue   ? head.AmountAfterProbation.Value.ToString() : "",
                            head.Remark
                        }
                    });
                }
                //var documentStatus = Head.DocumentStatusID.HasValue ? new MutualRepository().GetDocumentStatus(Head.DocumentStatusID.Value) : null;
                //searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                //{
                //    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                //        {
                //          Head.HeadIID,Head.DocumentStatusID == null ?string.Empty : documentStatus.StatusName,
                //          Head.TransactionStatus == null ? string.Empty : Head.TransactionStatus.Description,

                //        }
                //});

            }
            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetVoucherDetails(ShoppingCartVoucherMap voucher)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (voucher != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "VoucherID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Amount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          voucher.ShoppingCartID,voucher.VoucherID,voucher.Amount,voucher.CreatedDate
                        }
                });
            }
            return searchDTO;
        }

        public Services.Contracts.Search.SearchResultDTO GetDayBookDetails(Account daybook1, AccountTransaction daybook2, TransactionHead daybook3)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (daybook1 != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AccountName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DebitOrCredit" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Amount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Description" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionNo" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          daybook2.TransactionIID,daybook1.AccountName, daybook2.DebitOrCredit==true? "Debit" : "Credit", daybook2.Amount, daybook2.Description,daybook3.TransactionNo

                        }
                });
            }
            return searchDTO;
        }


        public Eduegate.Services.Contracts.Search.SearchResultDTO GetPaymentKnetDetails(PaymentDetailsKnet Details, Currency cur, Status stat)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (Details.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TrackID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TrackKey" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitON" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Status" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Email" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentAmount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentCurrency" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransResult" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransRef" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransErrorMsg" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartID" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          Details.TrackID,Details.TrackKey,Details.CustomerID,Details.PaymentID,Details.InitOn,stat.StatusName
                          ,Details.PaymentAmount,Details.PaymentCurrency.IsNotNull()? cur.DisplayCode : null,Details.TransID,Details.TransResult,Details.TransRef,
                          Details.TransErrorMsg,Details.CartID

                        }
                });
            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetPayPalDetails(PaymentDetailsPayPal Details, Status stat)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (Details.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TrackID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Email" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitON" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Status" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Email" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitIP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitAmount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransAmount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransDateTime" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransMessage" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransOn" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "OrderID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransAmountFee" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartID" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          Details.TrackID, Details.RefCustomerID,Details.BusinessEmail, Details.PaymentID, Details.InitOn, stat.StatusName, Details.InitIP
                          ,Details.InitAmount, Details.TransID, Details.TransAmount, Details.TransDateTime, Details.TransMessage
                          , Details.TransOn, Details.OrderID, Details.TransAmountFee, Details.CartID

                        }
                });
            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetMasterVisaDetails(PaymentMasterVisa Details, Status stat)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (Details.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TrackID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Email" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitON" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Status" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Email" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitIP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitLocation" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentAmount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ResponseOn" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ResponseCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Message" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BankAuthorizationID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CardType" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ResponseAmount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ReceiptNumber" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          Details.TrackIID, Details.CustomerID, Details.PaymentID, Details.InitOn, stat.StatusName, Details.InitIP,
                          Details.InitLocation, Details.PaymentAmount, Details.ResponseOn, Details.ResponseCode, Details.Message,
                          Details.TransID, Details.BankAuthorizationID, Details.CardType, Details.ResponseAmount,
                          Details.CartID, Details.ReceiptNumber.IsNotNull() ? Details.ReceiptNumber : null

                        }
                });
            }


            return searchDTO;
        }

    }
}