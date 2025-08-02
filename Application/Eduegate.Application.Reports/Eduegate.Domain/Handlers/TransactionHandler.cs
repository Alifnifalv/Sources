using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Handlers
{
    public class TransactionHandler
    {
        CallContext _callContext;
        Eduegate.Domain.Mappers.OrderContactMapMapper orderContactMapper;
        private TransactionHandler()
        {

        }

        public static TransactionHandler Handler(CallContext _callContext)
        {
            var handler = new TransactionHandler();
            handler._callContext = _callContext;
            handler.orderContactMapper = Eduegate.Domain.Mappers.OrderContactMapMapper.Mapper(_callContext);
            return handler;
        }

        public string GetNextTransactionNumber(int documentTypeID, List<KeyValueParameterDTO> parameters = null)
        {
            string nextTransactionNumber = string.Empty;
            var entity = new Eduegate.Domain.Repository.ReferenceDataRepository().GetDocumentType(documentTypeID);
            if (entity.IsNotNull())
            {
                var transactionNo = default(string);
                nextTransactionNumber = entity.TransactionNoPrefix;
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        transactionNo += entity.TransactionNoPrefix.Replace("{" + item.ParameterName.Trim().ToUpper() + "}", item.ParameterValue);
                    }
                    nextTransactionNumber = transactionNo.Trim() == string.Empty ? nextTransactionNumber : transactionNo.Trim();
                }
               
                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }

        public string GetAndSaveNextTransactionNumber(int documentTypeID, List<KeyValueParameterDTO> parameters = null)
        {
            string nextTransactionNumber = string.Empty;
            var entity = new Eduegate.Domain.Repository.ReferenceDataRepository().GetDocumentType(documentTypeID);
            if (entity.IsNotNull())
            {
                var transactionNo = default(string);
                nextTransactionNumber = entity.TransactionNoPrefix;
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        transactionNo += entity.TransactionNoPrefix.Replace("{" + item.ParameterName.Trim().ToUpper() + "}", item.ParameterValue);
                    }
                    nextTransactionNumber = transactionNo.Trim() == string.Empty ? nextTransactionNumber : transactionNo.Trim();
                }
                entity.LastTransactionNo = entity.LastTransactionNo.HasValue ? entity.LastTransactionNo.Value + 1 : 1;
                entity = new Eduegate.Domain.Repository.ReferenceDataRepository().SaveDocumentType(entity);
                nextTransactionNumber += entity.LastTransactionNo.IsNull() ? "1" : Convert.ToString(entity.LastTransactionNo);
                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }

        public string GetEntitlementType(Eduegate.Framework.Payment.PaymentGatewayType paymentGatewayMethod)
        {
            //Eduegate.Framework.Payment.EntitlementType entitlementID;
            var entitlementID = string.Empty;
            switch (paymentGatewayMethod)
            {
                case Framework.Payment.PaymentGatewayType.MIGS:
                    entitlementID = new SettingBL(_callContext).GetSettingValue<string>("VISAMASTERCARD_ENTITLEMENTID");
                    break;

                case Framework.Payment.PaymentGatewayType.COD:
                default:
                    entitlementID = new SettingBL(_callContext).GetSettingValue<string>("COD_ENTITLEMENTID");
                    break;
            }
            return entitlementID;
        }

        public TransactionDTO SaveTransactions(TransactionDTO transaction)
        {
            var transactionHead = new TransactionHead();

            if (transaction.IsNotNull())
            {
                if (transaction.TransactionHead.IsNotNull())
                {
                    transactionHead = new TransactionHead()
                    {
                        HeadIID = transaction.TransactionHead.HeadIID,
                        DocumentTypeID = (int)transaction.TransactionHead.DocumentTypeID,
                        TransactionDate = transaction.TransactionHead.TransactionDate.IsNotNull() ? Convert.ToDateTime(transaction.TransactionHead.TransactionDate) : DateTime.Now,
                        CustomerID = transaction.TransactionHead.CustomerID,
                        Description = transaction.TransactionHead.Description,
                        Reference = transaction.TransactionHead.Reference,
                        TransactionNo = transaction.TransactionHead.TransactionNo,
                        SupplierID = transaction.TransactionHead.SupplierID,
                        TransactionStatusID = transaction.TransactionHead.TransactionStatusID,
                        DiscountAmount = transaction.TransactionHead.DiscountAmount,
                        DiscountPercentage = transaction.TransactionHead.DiscountPercentage,
                        BranchID = transaction.TransactionHead.BranchID,
                        ToBranchID = transaction.TransactionHead.ToBranchID,
                        DueDate = transaction.TransactionHead.DueDate,
                        DeliveryDate = transaction.TransactionHead.DeliveryDate,
                        CurrencyID = transaction.TransactionHead.CurrencyID,
                        DeliveryMethodID = transaction.TransactionHead.DeliveryMethodID,
                        IsShipment = transaction.TransactionHead.IsShipment,
                        EntitlementID = transaction.TransactionHead.EntitlementID,
                        CreatedBy = transaction.TransactionHead.CreatedBy,
                        UpdatedBy = transaction.TransactionHead.UpdatedBy,
                        ReferenceHeadID = transaction.TransactionHead.ReferenceHeadID,
                        JobEntryHeadID = transaction.TransactionHead.JobEntryHeadID,
                        CompanyID = transaction.TransactionHead.CompanyID,
                        DeliveryTypeID = transaction.TransactionHead.DeliveryTypeID,
                        DeliveryCharge = transaction.TransactionHead.DeliveryCharge,
                        DeliveryDays = transaction.TransactionHead.DeliveryDays,
                        TransactionRole = transaction.TransactionHead.TransactionRole,
                        DeliveryTimeslotID = transaction.TransactionHead.DeliveryTimeslotID,
                        StudentID = transaction.TransactionHead.StudentID,
                        SchoolID = transaction.TransactionHead.SchoolID,
                        AcademicYearID = transaction.TransactionHead.AcademicYearID,
                        PaidAmount = transaction.TransactionHead.PaidAmount,
                        CreatedDate = DateTime.Now,
                        //UpdatedDate = Convert.ToDateTime(transaction.TransactionHead.UpdatedDate),
                    };
                }

                if (transaction.TransactionDetails.IsNotNull() && transaction.TransactionDetails.Count > 0)
                {
                    transactionHead.TransactionDetails = new List<TransactionDetail>();

                    foreach (TransactionDetailDTO transactionDetailDTO in transaction.TransactionDetails)
                    {
                        var transactionDetail = new TransactionDetail();
                        transactionDetail.ProductSerialMaps = new List<ProductSerialMap>();

                        transactionDetail.DetailIID = transactionDetailDTO.DetailIID;
                        transactionDetail.HeadID = transactionDetailDTO.HeadID;
                        transactionDetail.ProductID = new ProductDetailRepository().GetProductBySKUID(Convert.ToInt64(transactionDetailDTO.ProductSKUMapID)).ProductIID;
                        transactionDetail.ProductSKUMapID = transactionDetailDTO.ProductSKUMapID;
                        transactionDetail.Quantity = transactionDetailDTO.Quantity;
                        transactionDetail.UnitID = transactionDetailDTO.UnitID;
                        transactionDetail.DiscountPercentage = transactionDetailDTO.DiscountPercentage;
                        transactionDetail.DiscountAmount = transactionDetailDTO.DiscountAmount;
                        transactionDetail.CartItemID = transactionDetailDTO.CartItemID;
                        transactionDetail.UnitPrice = transactionDetailDTO.UnitPrice;
                        transactionDetail.ActualUnitPrice = transactionDetailDTO.ActualUnitPrice;
                        transactionDetail.Amount = transactionDetailDTO.Amount;
                        transactionDetail.ExchangeRate = transactionDetailDTO.ExchangeRate;
                        transactionDetail.CreatedBy = transactionDetailDTO.CreatedBy;
                        transactionDetail.UpdatedBy = transactionDetailDTO.UpdatedBy;
                        transactionDetail.CreatedDate = DateTime.Now;
                        //UpdatedDate = Convert.ToDateTime(transactionDetail.UpdatedDate);
                        if (transactionDetailDTO.SKUDetails != null && transactionDetailDTO.SKUDetails.Count > 0)
                        {
                            foreach (var skuDetail in transactionDetailDTO.SKUDetails)
                            {
                                var serialMap = new ProductSerialMap();
                                serialMap.SerialNo = skuDetail.SerialNo;
                                serialMap.DetailID = transactionDetail.DetailIID;
                                transactionDetail.ProductSerialMaps.Add(serialMap);
                            }
                        }
                        transactionHead.TransactionDetails.Add(transactionDetail);
                    }
                }

                if (transaction.ShipmentDetails.IsNotNull())
                {
                    transactionHead.TransactionShipments = new List<TransactionShipment>();

                    transactionHead.TransactionShipments.Add(new TransactionShipment()
                    {
                        TransactionShipmentIID = transaction.ShipmentDetails.TransactionShipmentIID,
                        TransactionHeadID = transaction.ShipmentDetails.TransactionHeadID,
                        SupplierIDFrom = transaction.ShipmentDetails.SupplierIDFrom,
                        SupplierIDTo = transaction.ShipmentDetails.SupplierIDTo,
                        ShipmentReference = transaction.ShipmentDetails.ShipmentReference,
                        FreightCarrier = transaction.ShipmentDetails.FreightCareer,
                        ClearanceTypeID = transaction.ShipmentDetails.ClearanceTypeID,
                        AirWayBillNo = transaction.ShipmentDetails.AirWayBillNo,
                        FreightCharges = transaction.ShipmentDetails.FrieghtCharges,
                        BrokerCharges = transaction.ShipmentDetails.BrokerCharges,
                        AdditionalCharges = transaction.ShipmentDetails.AdditionalCharges,
                        Weight = transaction.ShipmentDetails.Weight,
                        NoOfBoxes = transaction.ShipmentDetails.NoOfBoxes,
                        BrokerAccount = transaction.ShipmentDetails.BrokerAccount,
                        Description = transaction.ShipmentDetails.Remarks,
                        CreatedBy = transaction.ShipmentDetails.CreatedBy,
                        UpdatedBy = transaction.ShipmentDetails.UpdatedBy,
                        CreatedDate = transaction.ShipmentDetails.CreatedDate,
                        UpdatedDate = transaction.ShipmentDetails.UpdatedDate,
                        //TimeStamps = transaction.ShipmentDetails.TimeStamps,
                    });
                }
                if (transaction.TransactionHeadEntitlementMap.IsNotNull())
                {
                    transactionHead.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMap>();

                    transactionHead.TransactionHeadEntitlementMaps.Add(new TransactionHeadEntitlementMap()
                    {
                        //TransactionHeadEntitlementMapIID = transaction.TransactionHeadEntitlementMap.,
                        TransactionHeadID = transaction.TransactionHead.HeadIID,
                        EntitlementID = transaction.TransactionHeadEntitlementMap.EntitlementID,
                        Amount = transaction.TransactionHeadEntitlementMap.Amount,
                    });
                }

            }//passing entity model data to repository

            transactionHead = new Eduegate.Domain.Repository.TransactionRepository().SaveTransactions(transactionHead);

            // create dto variable for OrderCOntactMap
            var dtoOrderContactMap = new OrderContactMapDTO();
            if (transactionHead.IsNotNull())
            {
                // update LastTransactionNo in [mutual].[DocumentTypes] table
                //DocumentType entity = new MutualBL(_callContext).UpdateLastTransactionNo(Convert.ToInt32(transactionHead.DocumentTypeID), transactionHead.TransactionNo);

                // Save OrderContact Map
                if (transaction.OrderContactMap.IsNotNull())
                {
                    transaction.OrderContactMap.OrderID = transactionHead.HeadIID;
                    dtoOrderContactMap = orderContactMapper.ToDTO(
                        DeliveryHandler.Handler(_callContext).SaveOrderContactMap(orderContactMapper.ToEntity(transaction.OrderContactMap)));
                }
            }

            // Updated entity data and moved to the dto / ?? Eswar> Is this required to populate again to DTO again Prabha? I think, DTO is already available. Please check and delete below code.
            var transactionDTO = new TransactionDTO();
            if (transactionHead.IsNotNull())
            {
                transactionDTO =  new Eduegate.Domain.Repository.TransactionRepository().GetTransactionDTO(transactionHead.HeadIID);
            }

            // assign updated OrderContactMap into dto
            if (dtoOrderContactMap.IsNotNull())
            {
                transactionDTO.OrderContactMap = new OrderContactMapDTO();
                transactionDTO.OrderContactMap = dtoOrderContactMap;
            }

            return transactionDTO;
        }

        public OrderContactMap ToEntity(OrderContactMapDTO dto)
        {
            if (dto != null)
            {
                return new OrderContactMap()
                {
                    OrderContactMapIID = dto.OrderContactMapID,
                    OrderID = dto.OrderID,
                    TitleID = dto.TitleID,
                    ContactID = dto.ContactID,
                    AreaID = dto.AreaID,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    Description = dto.Description,
                    BuildingNo = dto.BuildingNo,
                    Floor = dto.Floor,
                    Flat = dto.Flat,
                    Avenue = dto.Avenue,
                    Block = dto.Block,
                    AddressName = dto.AddressName,
                    AddressLine1 = dto.AddressLine1,
                    AddressLine2 = dto.AddressLine2,
                    State = dto.State,
                    City = dto.City,
                    District = dto.District,
                    LandMark = dto.LandMark,
                    CountryID = dto.CountryID,
                    PostalCode = dto.PostalCode,
                    Street = dto.Street,
                    TelephoneCode = dto.TelephoneCode,
                    MobileNo1 = dto.MobileNo1,
                    MobileNo2 = dto.MobileNo2,
                    PhoneNo1 = dto.PhoneNo1,
                    PhoneNo2 = dto.PhoneNo2,
                    PassportNumber = dto.PassportNumber,
                    CivilIDNumber = dto.CivilIDNumber,
                    PassportIssueCountryID = dto.PassportIssueCountryID,
                    AlternateEmailID1 = dto.AlternateEmailID1,
                    AlternateEmailID2 = dto.AlternateEmailID2,
                    WebsiteURL1 = dto.WebsiteURL1,
                    WebsiteURL2 = dto.WebsiteURL2,
                    IsBillingAddress = dto.IsBillingAddress,
                    IsShippingAddress = dto.IsShippingAddress,
                    SpecialInstruction = dto.SpecialInstruction,
                    CityID = dto.CityId,

                    UpdatedDate = DateTime.Now,
                    UpdatedBy = (int)_callContext.LoginID,
                    CreatedDate = dto.OrderContactMapID == 0 ? DateTime.Now : dto.CreatedDate,
                    CreatedBy = dto.OrderContactMapID == 0 ? (int)_callContext.LoginID : dto.CreatedBy//,
                    //TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps)
                };
            }
            else return new OrderContactMap();
        }

        public long? GetSellingUnit(long skumapID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {

                var skumap = dbContext.ProductSKUMaps.FirstOrDefault(a => a.ProductSKUMapIID == skumapID);

                var productID = skumap.ProductID;

                var product = dbContext.Products.FirstOrDefault(a => a.ProductIID == productID);

                var unitID = product.SellingUnitID;

                return unitID;
            }
        }
    }
}
