using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Accounts;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Security;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Accounting = Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common.Enums;

namespace Eduegate.Domain
{
    public class SupplierBL
    {
        private static Eduegate.Framework.CallContext _callContext { get; set; }
        private SupplierRepository supplierRepository = new SupplierRepository();

        public SupplierBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public SupplierDTO GetSupplier(long supplierID)
        {
            Supplier supplier = supplierRepository.GetSupplier(supplierID);
            var dto =  Mappers.Inventory.SupplierMapper.Mapper(_callContext).ToDTO(supplier);
            dto = GetSupplierAccountMaps(dto);
            dto.Login = LoginMapper.Mapper(_callContext).ToDTO(supplier.Login);
            return dto;
        }

        public SupplierDTO GetSupplierByLoginID(long supplierID)
        {
            var entity = supplierRepository.GetSupplierByLoginID(supplierID);
            if (entity.IsNull())
            {
                return null;
            }
            var dto = new SupplierDTO()
            {
                SupplierIID = entity.SupplierIID,
                LoginID = entity.LoginID,
                TitleID = entity.TitleID.HasValue ? (short?)short.Parse(entity.TitleID.ToString()) : null,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,
                StatusID = entity.StatusID,
                VendorCR = entity.VendorCR,
                CRExpiry = entity.CRExpiry,
                VendorNickName = entity.VendorNickName,
                CompanyLocation = entity.CompanyLocation,
                SupplierCode = entity.SupplierCode,
                SupplierEmail = entity.SupplierEmail,
                SupplierAddress = entity.SupplierAddress,

                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdateDate,
                TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                IsMarketPlace = entity.IsMarketPlace,
                BranchID = entity.BranchID,
                BranchName = entity.BranchID > 0 ? new ReferenceDataRepository().GetBranch(entity.BranchID.Value, false).BranchName : null,
                ReturnMethodID = entity.ReturnMethodID.HasValue ? entity.ReturnMethodID : null,
                ReceivingMethodID = entity.ReceivingMethodID.HasValue ? entity.ReceivingMethodID : null,
                CompanyID = entity.CompanyID.IsNotNull() ? entity.CompanyID : _callContext.CompanyID,
                ReturnMethodName = entity.ReturnMethodID.HasValue ? entity.ReturnMethod.ReturnMethodName : null,
                ReceivingMethodName = entity.ReceivingMethodID.HasValue ? entity.ReceivingMethod.ReceivingMethodName : null,
                Profit = entity.Profit,
            };
            return dto;
        }

        public SKUDTO UpdateSupplierSKUInventory(SKUDTO skuDTO)
        {

            var SKUProfitPercentage = (skuDTO.CostPrice / skuDTO.SellingPrice) * 100;
            if (SKUProfitPercentage > 90)
            {
                skuDTO.ErrorCode = ErrorCodes.SKU.S001;
                return skuDTO;
            }
            // get supplier id from user id
            var supplier = GetSupplier(GetSupplierByLoginID(Convert.ToInt64(new AccountBL(_callContext).GetUserDetails(_callContext.EmailID).LoginID)).SupplierIID);
            var supplierPriceList = supplier.PriceLists;
            var updatedResult = new PriceSettingsRepository().UpdateSupplierSKUInventory(skuDTO, supplierPriceList.PriceListID, (long)supplier.BranchID, (int)_callContext.CompanyID);
            if (updatedResult)
                return new ProductDetailBL(_callContext).GetProductSKUDetails(skuDTO.ProductSKUMapID);

            return null;
        }

        public bool UpdateMarketPlaceOrderStatus(MarketPlaceTransactionActionDTO orderDetail)
        {
            try
            {
                var result = new TransactionBL(_callContext).UpdateTransactionHead(
                new TransactionHeadDTO()
                {
                    HeadIID = orderDetail.HeadIID,
                    TransactionStatusID = (byte)orderDetail.TransactionStatusID,
                    DocumentStatusID = (short)orderDetail.DocumentStatusID
                });

                if (orderDetail.Reason.IsNotNullOrEmpty())
                {
                    // Sales order comment
                    var salesComment = new CommentDTO();
                    salesComment.CommentText = orderDetail.Reason;
                    salesComment.EntityType = EntityTypes.Transaction;
                    salesComment.ReferenceID = (long)orderDetail.ReferenceHeadID;
                    salesComment = new MutualBL(_callContext).SaveComment(salesComment);

                    // purchase order comment
                    var purchaseComment = new CommentDTO();
                    purchaseComment.CommentText = orderDetail.Reason;
                    purchaseComment.EntityType = EntityTypes.Transaction;
                    purchaseComment.ReferenceID = (long)orderDetail.HeadIID;
                    purchaseComment = new MutualBL(_callContext).SaveComment(purchaseComment);


                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SKUDTO GetSupplierProductPriceListSKUMapDetails(long SKUID)
        {
            var skuMap = new ProductDetailRepository().GetProductSkuDetails(SKUID);

            var userdetail = new AccountBL(_callContext).GetUserDetails(_callContext.EmailID);
            var supplier = GetSupplierByLoginID(Convert.ToInt64(userdetail.LoginID));
            var supplierDetail = GetSupplier(supplier.SupplierIID);

            var productPricelistSKUMap = new PriceSettingsRepository().GetProductPriceListSKUMap(supplierDetail.PriceLists.PriceListID, SKUID);
            var inventoryDetails = new ProductBL(_callContext).GetProductSKUInventoryDetail(SKUID);
            if (skuMap.IsNotNull())
            {
                var skuDTO = new SKUDTO();
                skuDTO = Mappers.Catalog.SKUMapper.Mapper(_callContext).ToDTO(skuMap);
                skuDTO.CostPrice = productPricelistSKUMap.Cost;
                skuDTO.SellingPrice = productPricelistSKUMap.Price;
                skuDTO.Quantity = Convert.ToInt32(inventoryDetails.Quantity);

                return skuDTO;
            }
            else
            {
                return null;
            }
        }

        public List<SupplierDTO> GetSuppliers(string searchText, int dataSize)
        {
            List<SupplierDTO> suppliersDTO = new List<SupplierDTO>();
            SupplierDTO supplierDTO = null;

            List<Supplier> suppliers = supplierRepository.GetSuppliers(searchText, dataSize, _callContext.CompanyID.IsNotNull() ? (int?)_callContext.CompanyID : default(int));

            if (suppliers.IsNotNull() && suppliers.Count > 0)
            {
                foreach (Supplier supplier in suppliers)
                {
                    supplierDTO = new SupplierDTO();

                    supplierDTO.SupplierIID = supplier.SupplierIID;
                    supplierDTO.FirstName = supplier.FirstName;
                    supplierDTO.MiddleName = supplier.MiddleName;
                    supplierDTO.LastName = supplier.LastName;

                    suppliersDTO.Add(supplierDTO);
                }
            }

            return suppliersDTO;
        }

        public SupplierDTO SaveSupplier(SupplierDTO dtoSupplier)
        {
            var mapper = Mappers.Inventory.SupplierMapper.Mapper(_callContext);

            var supplier = mapper.ToEntity(dtoSupplier);
            supplier.Login = LoginMapper.Mapper(_callContext).ToEntity(dtoSupplier.Login); //ToLoginEntity(customer.Login, _callContext);

            supplier = supplierRepository.SaveSupplier(supplier, _callContext.CompanyID);

            if (supplier.SupplierIID.IsNotNull() && supplier.SupplierIID > 0)
            {
                // Save EntitlementMaps
                List<EntitlementMap> entities = dtoSupplier.Entitlements.EntitlementMaps.Select(x => EntitlementMapMapper.Mapper.ToEntity(x)).ToList();
                foreach (var entity in entities)
                {
                    if (entity.EntitlementID == 0) continue;
                    entity.ReferenceID = supplier.SupplierIID;
                    new MutualRepository().SaveEntitlementMap(entity);
                }

                // Save branch/pricelist for marketplace while creating new supplier if no branch selected
                if ((dtoSupplier.IsMarketPlace == true) && (dtoSupplier.SupplierIID.IsNull() || dtoSupplier.SupplierIID == 0) && (dtoSupplier.BranchID.IsNull() || dtoSupplier.BranchID == 0))
                {
                    var marketPlaceBranchGroupSetting = new SettingBL().GetSettingDetail("MARKETPLACEBRANCHGROUPID");
                    var marketPlaceWareHouseSetting = new SettingBL().GetSettingDetail("MARKETPLACEWAREHOUSEID");

                    // new Branch
                    var branch = new BranchDTO();
                    branch.BranchName = supplier.FirstName + " " + supplier.LastName + "Branch";
                    branch.IsMarketPlace = true;
                    if (marketPlaceBranchGroupSetting.IsNotNull())
                        branch.BranchGroupID = Convert.ToInt64(marketPlaceBranchGroupSetting.SettingValue);
                    if (marketPlaceWareHouseSetting.IsNotNull())
                        branch.WarehouseID = Convert.ToInt64(marketPlaceWareHouseSetting.SettingValue);
                    branch.StatusID = Convert.ToByte(BranchStatuses.Active);
                    branch.CompanyID = _callContext.CompanyID;

                    var updatedBranch = new ReferenceDataBL(_callContext).SaveBranch(branch);

                    // New pricelist for this branch
                    var productPriceDTO = new ProductPriceDTO();
                    productPriceDTO.ProductPriceListTypeID = Convert.ToInt16(new SettingBL().GetSettingDetail("MARKETPLACEPRODUCTPRICELISTTYPEID").SettingValue);
                    productPriceDTO.ProductPriceListLevelID = Convert.ToInt16(new SettingBL().GetSettingDetail("MARKETPLACEPRODUCTPRICELISTLEVELID").SettingValue);
                    productPriceDTO.PriceDescription = supplier.FirstName + " " + supplier.LastName + "Price List";

                    // Create branch map
                    productPriceDTO.BranchMaps = new List<BranchMapDTO>();
                    var pricelistBranchMap = new BranchMapDTO();
                    pricelistBranchMap.BranchName = updatedBranch.BranchName;
                    pricelistBranchMap.BranchID = updatedBranch.BranchIID;
                    productPriceDTO.BranchMaps.Add(pricelistBranchMap);


                    //Updating BranchId after taken from Updated branch
                    supplier.BranchID = pricelistBranchMap.BranchID;
                    supplierRepository.UpdateSupplierBranch(supplier);

                    var updatedproductPriceDTO = new ProductDetailBL(_callContext).CreatePriceInformationDetail(productPriceDTO);
                }


                // Save EntityTypePaymentMethodMap
                dtoSupplier.SupplierIID = supplier.SupplierIID;
                SaveEntityTypePaymentMethodMap(dtoSupplier);

                // Save customer docs
                if (dtoSupplier.Document.IsNotNull() && dtoSupplier.Document.Documents.Count > 0 && dtoSupplier.Document.Documents[0].FileName.IsNotNullOrEmpty())
                {
                    new DocumentBL(_callContext).SaveDocuments(dtoSupplier.Document.Documents, EntityTypes.Supplier, supplier.SupplierIID);
                }




                //// Save CustomerProductReferences 
                //List<CustomerProductReferenceDTO> dtos = new List<CustomerProductReferenceDTO>();
                //foreach (var item in dtoSupplier.ExternalSettings.ExternalProductSettings)
                //{
                //    if (item.ProductSKUMapID.IsNotNull() && item.ProductSKUMapID > 0)
                //    {
                //        CustomerProductReferenceDTO dtoCustomerProductReference = new CustomerProductReferenceDTO();
                //        dtoCustomerProductReference.CustomerID = supplier.SupplierIID;
                //        dtoCustomerProductReference.ProductSKUMapID = item.ProductSKUMapID;
                //        dtoCustomerProductReference.BarCode = item.ExternalBarcode;
                //        dtos.Add(dtoCustomerProductReference);
                //    }
                //}
                //new CustomerBL(_callContext).SaveCustomerProductReferences(dtos);
            }


            var dto = mapper.ToDTO(supplier);
            dto = GetSupplierAccountMaps(dto);
            return dto;
        }


        public List<SupplierDTO> GetSupplierBySupplierIdAndCR(string searchText)
        {
            List<Supplier> suppliers = new SupplierRepository().GetSupplierBySupplierIdAndCR(searchText);
            return suppliers.Select(x => new SupplierDTO() { SupplierIID = x.SupplierIID, FirstName = x.FirstName, MiddleName = x.MiddleName, LastName = x.LastName }).ToList();
        }

        private void SaveEntityTypePaymentMethodMap(SupplierDTO dtoSupplier)
        {
            EntityTypePaymentMethodMap entity = new EntityTypePaymentMethodMap() { ReferenceID = dtoSupplier.SupplierIID };
            // Delete Call
            new MutualRepository().DeleteEntityTypePaymentMethodMapByReferenceID(entity);

            // Save EntityTypePaymentMethodMap
            if (dtoSupplier.IsCash)
            {
                entity = new EntityTypePaymentMethodMap();

                entity.EntityTypeID = (short)EntityTypes.Supplier;
                entity.PaymentMethodID = (short)PaymentMethods.Cash;
                entity.ReferenceID = dtoSupplier.SupplierIID;
                entity.UpdatedBy = (int)_callContext.LoginID;
                entity.UpdatedDate = DateTime.Now;

                // Save Call
                new MutualRepository().SaveEntityTypePaymentMethodMap(entity);
            }

            if (dtoSupplier.IsCheque)
            {
                entity = new EntityTypePaymentMethodMap();

                entity.EntityTypeID = (short)EntityTypes.Supplier;
                entity.PaymentMethodID = (short)PaymentMethods.Cheque;
                entity.ReferenceID = dtoSupplier.SupplierIID;
                entity.EntityPropertyID = dtoSupplier.ChequeTypeID;
                entity.EntityPropertyTypeID = (int)EntityPropertyTypes.ChequeType;
                entity.NameOnCheque = dtoSupplier.ChequeName;
                entity.UpdatedBy = (int)_callContext.LoginID;
                entity.UpdatedDate = DateTime.Now;

                // Save Call
                new MutualRepository().SaveEntityTypePaymentMethodMap(entity);
            }

            if (dtoSupplier.IsBankAccount)
            {
                foreach (var item in dtoSupplier.BankAccounts)
                {
                    entity = new EntityTypePaymentMethodMap();

                    entity.EntityTypeID = (short)EntityTypes.Supplier;
                    entity.PaymentMethodID = (short)PaymentMethods.BankTransfer;
                    entity.ReferenceID = dtoSupplier.SupplierIID;
                    entity.AccountID = item.AccountNo;
                    entity.AccountName = item.AccountHolderName;
                    entity.IBANCode = item.IBAN;
                    entity.SWIFTCode = item.SwiftCode;
                    entity.UpdatedBy = (int)_callContext.LoginID;
                    entity.UpdatedDate = DateTime.Now;

                    // Save Call
                    new MutualRepository().SaveEntityTypePaymentMethodMap(entity);
                }
            }
        }

        public List<SupplierStatusDTO> GetSupplierStatuses()
        {
            List<SupplierStatus> supplierStatuses = new SupplierRepository().GetSupplierStatuses();
            return supplierStatuses.Select(x => SupplierStatusMapper.Mapper.ToDTO(x)).ToList();
        }

        public ProductPriceListSKUMapDTO GetSKUPriceDetailByBranch(long branchID, long skuMapID)
        {
            var priceDetail = new SupplierRepository().GetSKUPriceDetailByBranch(branchID, skuMapID);
            return ProductPriceListSKUMapMapper.Mapper().ToDTO(priceDetail);
        }

        #region Supplier Account Maps
        public List<Accounting.SupplierAccountEntitlmentMapsDTO> SaveSupplierAccountMaps(List<Accounting.SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs)
        {
            List<SupplierAccountMap> entityList = FromSupplierAccountMapsDTOtoEntity(SupplierAccountMapsDTOs);
            entityList = supplierRepository.SaveSupplierAccountMaps(entityList);
            return FromEntityToSupplierAccountMapsDTO(entityList);

            //Eduegate.Services.Contracts.Enums.EntityTypes SupplierCustomer = 1;
            //Accounting.SupplierAccountEntitlmentMapsDTO firstDTOObject= SupplierAccountMapsDTOs.FirstOrDefault();
            //if (firstDTOObject != null)
            //{
            //    SupplierCustomer = firstDTOObject.SupplierCustomer;
            //}
            //if (SupplierCustomer == Eduegate.Services.Contracts.Enums.EntityTypes.Supplier)
            //{
            //    List<SupplierAccountMap> entityList = FromSupplierAccountMapsDTOtoEntity(SupplierAccountMapsDTOs);
            //    entityList = supplierRepository.SaveSupplierAccountMaps(entityList);
            //    return FromEntityToSupplierAccountMapsDTO(entityList);
            //}
            //else
            //{
            //    CustomerBL customerBL = new CustomerBL(_callContext);
            //    List<CustomerAccountMap> entityList = customerBL.FromAccountMapsDTOtoEntity(SupplierAccountMapsDTOs);
            //    entityList = new CustomerRepository().SaveCustomerAccountMaps(entityList);
            //    return customerBL.FromEntityToAccountMapsDTO(entityList);
            //}
        }

        private static SupplierDTO GetSupplierAccountMaps(SupplierDTO dto)
        {
            //int supplierCustomer = 1;// dto.SupplierAccountMaps.SupplierAccountEntitlements;// Default to Supplier
            //if (supplierCustomer == 1)//Supplier Account Maps
            //{
            var SupplierAccountMapEntityList = new SupplierRepository().GetSupplierAccountMaps((long)dto.SupplierIID);
            dto.SupplierAccountMaps = new SupplierAccountMapDTO();
            dto.SupplierAccountMaps.SupplierAccountEntitlements = FromEntityToSupplierAccountMapsDTO(SupplierAccountMapEntityList);
            return dto;
            //}
            //else//Customer Acc Maps
            //{
            //    var CustomerAccountMapEntityList = new CustomerBL(_callContext).GetCustomerAccountMaps((long)dto.SupplierIID);//Customer ID
            //    dto.SupplierAccountMaps = new SupplierAccountMapDTO();
            //    dto.SupplierAccountMaps.SupplierAccountEntitlements = CustomerAccountMapEntityList;
            //    return dto;
            //}
        }
        public static List<SupplierAccountMap> FromSupplierAccountMapsDTOtoEntity(List<Accounting.SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs)
        {
            List<SupplierAccountMap> entityList = new List<SupplierAccountMap>();

            Accounting.SupplierAccountEntitlmentMapsDTO baseEntitlementDTO = SupplierAccountMapsDTOs.Where(e => e.EntitlementID == null).FirstOrDefault();
            Account baseAccount = new Account();
            if (baseEntitlementDTO != null && baseEntitlementDTO.Account.AccountID.HasValue)
            {
                baseAccount = new AccountingRepository().GetAccount(baseEntitlementDTO.Account.AccountID.Value);
            }

            foreach (Accounting.SupplierAccountEntitlmentMapsDTO dto in SupplierAccountMapsDTOs)
            {
                SupplierAccountMap entity = new SupplierAccountMap();
                entity.EntitlementID = dto.EntitlementID != null ? dto.EntitlementID : dto.EntitlementID;

                entity.SupplierID = dto.SupplierID;
                entity.SupplierAccountMapIID = dto.SupplierAccountMapIID;

                if (entity.EntitlementID != null)
                {
                    if (dto.Account.AccountID == 0)//new account
                    {
                        Account AccountEntity = new Account();
                        AccountEntity.AccountName = dto.SupplierID.ToString() + " " + dto.Account.AccountName;
                        AccountEntity.Alias = dto.Account.Alias;

                        //Base Account values
                        AccountEntity.AccountBehavoirID = (byte)dto.Account.AccountBehavior;
                        //AccountEntity.GroupID = dto.Account.AccountGroup.AccountGroupID;
                        //AccountEntity.ParentAccountID = dto.Account.ParentAccount.AccountID;
                        //AccountEntity.AccountBehavoirID = (byte)baseAccount.AccountBehavoir.AccountBehavoirID;
                        AccountEntity.GroupID = baseAccount.Group.GroupID;
                        AccountEntity.ParentAccountID = baseAccount.AccountID;
                        AccountEntity.ChildAliasPrefix = baseAccount.ChildAliasPrefix;
                        AccountEntity.ChildLastID = 0;


                        AccountEntity.AccountID = dto.Account.AccountID.Value;

                        entity.Account = AccountEntity; // New Account
                    }
                }
                if (dto.Account.AccountID != 0)
                {
                    entity.AccountID = dto.Account.AccountID; //Existing Account OR Root Account
                }


                entityList.Add(entity);
            }
            return entityList;
        }

        public static List<Accounting.SupplierAccountEntitlmentMapsDTO> FromEntityToSupplierAccountMapsDTO(List<SupplierAccountMap> entityList)
        {
            List<Accounting.SupplierAccountEntitlmentMapsDTO> dtoList = new List<Accounting.SupplierAccountEntitlmentMapsDTO>();
            foreach (SupplierAccountMap entity in entityList)
            {
                if (!entity.AccountID.HasValue) continue;
                var dto = new Accounting.SupplierAccountEntitlmentMapsDTO();
                dto.AccountID = (long)entity.AccountID;
                dto.SupplierID = (long)entity.SupplierID;
                dto.SupplierAccountMapIID = entity.SupplierAccountMapIID;
                dto.EntitlementID = entity.EntitlementID;
                dto.EntitlementName = entity.EntityTypeEntitlement != null ? entity.EntityTypeEntitlement.EntitlementName : "";

                if (entity.Account != null)
                {
                    AccountDTO entitelmentAccount = new AccountDTO();
                    entitelmentAccount.AccountID = entity.Account.AccountID;
                    entitelmentAccount.AccountName = entity.Account.AccountName;
                    entitelmentAccount.Alias = entity.Account.Alias;
                    entitelmentAccount.AccountBehavior = entity.Account.AccountBehavoir != null ? (Services.Contracts.Enums.Accounting.AccountBehavior)entity.Account.AccountBehavoir.AccountBehavoirID : 0;
                    entitelmentAccount.AccountGroup = new AccountGroupDTO() { AccountGroupID = entity.Account.Group.GroupID };
                    entitelmentAccount.ParentAccount = entity.Account != null && entity.Account.Account1 != null ? new AccountDTO() { AccountID = entity.Account.Account1.AccountID } : new AccountDTO();
                    dto.Account = entitelmentAccount;
                }
                dtoList.Add(dto);
            }
            return dtoList;
        }
        #endregion Supplier Account Maps

        public KeyValueDTO GetSupplierDeliveryMethod(long supplierID)
        {
            KeyValueDTO dto = new KeyValueDTO();
            var supplierDelivery= new SupplierRepository().GetSupplierDeliveryMethod(supplierID);

            if (supplierDelivery.IsNotNull()) 
            {
                dto.Key = supplierDelivery.ReceivingMethodID.ToString();
                dto.Value = supplierDelivery.ReceivingMethodName;
            }

            return dto;
        }

        public KeyValueDTO GetSupplierReturnMethod(long supplierID)
        {  
            KeyValueDTO dto = new KeyValueDTO();
            var supplierReturn = new SupplierRepository().GetSupplierReturnMethod(supplierID);

            if (supplierReturn.IsNotNull()) 
            {
                dto.Key = supplierReturn.ReturnMethodID.ToString();
                dto.Value = supplierReturn.ReturnMethodName;
            }

            return dto;
        }
    }
}
