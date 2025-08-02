using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Inventory
{
    public class SupplierMapper : IDTOEntityMapper<SupplierDTO, Supplier>
    {
        private CallContext _context;

        public static SupplierMapper Mapper(CallContext context)
        {
            var mapper = new SupplierMapper();
            mapper._context = context;
            return mapper;
        }

        public SupplierDTO ToDTO(Supplier entity)
        {
            var dto = new SupplierDTO();

            if (entity.IsNotNull())
            {
                List<EntitlementMap> EntitlementMaps = new List<EntitlementMap>();
                List<EntitlementMapDTO> dtoEntitlementMaps = new List<EntitlementMapDTO>();
                EntitlementMap entitlementMap = new EntitlementMap() { ReferenceID = entity.SupplierIID };

                EntitlementMaps = new MutualRepository().GetEntitlementMaps(entitlementMap, (short)EntityTypes.Supplier);
                dtoEntitlementMaps = EntitlementMaps.Select(x => EntitlementMapMapper.Mapper.ToDTO(x)).ToList();

                var PriceListbranch = new ReferenceDataRepository().GetBranchPriceListMaps(entity.BranchID.IsNotNull() ? (long)entity.BranchID : default(long));
                var PriceList = new ProductDetailRepository().GetProductPriceListDetail(PriceListbranch.IsNotNull() ?
                    (long)PriceListbranch.ProductPriceListID : 0, _context.IsNotNull() && _context.CompanyID.HasValue ? (int)_context.CompanyID.Value : default(int));

                var branch = entity.BranchID.IsNull() ? (Branch)null : new ReferenceDataRepository().GetBranch(entity.BranchID.Value, false);

                dto = new SupplierDTO()
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

                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdateDate,
                    TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                    IsMarketPlace = entity.IsMarketPlace,
                    BranchID = entity.BranchID,
                    ReturnMethodID = entity.ReturnMethodID,
                    ReceivingMethodID = entity.ReceivingMethodID,
                    BranchName = entity.BranchID > 0 ? branch.IsNotNull() ? branch.BranchName : null : null,
                    ReceivingMethodName = entity.ReceivingMethod.IsNotNull() ? entity.ReceivingMethod.ReceivingMethodName : null,
                    ReturnMethodName = entity.ReturnMethod.IsNotNull() ? entity.ReturnMethod.ReturnMethodName : null,
                    CompanyID = entity.CompanyID,
                    Entitlements = new EntitlementDTO()
                    {
                        EntitlementMaps = dtoEntitlementMaps
                    },
                    PriceLists = new PriceListDetailDTO()
                    {
                        PriceListID = PriceList.IsNotNull() ? PriceList.ProductPriceListIID : 0,
                        PriceDescription = PriceList.IsNotNull() ? PriceList.PriceDescription : string.Empty
                    },

                    Login = LoginMapper.Mapper(_context).ToDTO(entity.Login),
                    Profit = entity.Profit,
                    SupplierEmail = entity.SupplierEmail,
                    TelephoneNumber = entity.Telephone
                };

                if (entity.Contacts.IsNotNull() && entity.Contacts.Count > 0)
                {
                    dto.Contacts = new List<ContactDTO>();

                    foreach (Contact contact in entity.Contacts)
                    {
                        var countryDetail = new CountryMasterBL().GetCountryDetail(contact.ContactIID);
                        var area = contact.AreaID.HasValue ? new MutualRepository().GetAreaById(contact.AreaID.Value) : null;
                        var city = contact.CityID.HasValue ? new MutualRepository().GetCityById(contact.CityID.Value) : null;

                        dto.Contacts.Add(new ContactDTO()
                        {
                            ContactID = contact.ContactIID,
                            LoginID = contact.LoginID,
                            SupplierID = contact.SupplierID,
                            TitleID = (short?)contact.TitleID, //entity.TitleID.HasValue ? (short?)short.Parse(entity.TitleID.ToString()) : null,
                            FirstName = contact.FirstName,
                            LastName = contact.LastName,
                            MiddleName = contact.MiddleName,
                            BuildingNo = contact.BuildingNo,
                            Floor = contact.Floor,
                            Block = contact.Block,
                            Flat = contact.Flat,
                            AddressName = contact.AddressName,
                            AddressLine1 = contact.AddressLine1,
                            AddressLine2 = contact.AddressLine2,
                            Cities = new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                            {
                                Key = contact.CityID.ToString(),
                                Value = city.IsNotNull() ? city.CityName : string.Empty
                            },
                            Country = new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                            {
                                Key = contact.CountryID.IsNotNull() ? contact.CountryID.ToString() : null,
                                Value = countryDetail.IsNotNull() ? countryDetail.CountryNameEn : string.Empty
                            },
                            Areas = new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                            {
                                Key = contact.AreaID.HasValue ? contact.AreaID.ToString() : null,
                                Value = area.IsNotNull() ? area.AreaName : string.Empty
                            },
                            State = contact.State,
                            CityID = Convert.ToInt32(contact.CityID),
                            District = contact.District,
                            LandMark = contact.LandMark,
                            Avenue = contact.Avenue,
                            Street = contact.Street,
                            PostalCode = contact.PostalCode,
                            Description = contact.Description,
                            PassportNumber = contact.PassportNumber,
                            TelephoneCode = contact.TelephoneCode,
                            PhoneNo1 = contact.PhoneNo1,
                            MobileNo1 = contact.MobileNo1,
                            MobileNo2 = contact.MobileNo2,
                            AlternateEmailID1 = contact.AlternateEmailID1,
                            AlternateEmailID2 = contact.AlternateEmailID2,
                            WebsiteURL1 = contact.WebsiteURL1,
                            WebsiteURL2 = contact.WebsiteURL2,
                            IsBillingAddress = contact.IsBillingAddress,
                            IsShippingAddress = contact.IsShippingAddress,
                            //TimeStamps = contact.TimeStamps.ToString(),
                            CreatedBy = contact.CreatedBy,
                            CreatedDate = contact.CreatedDate,
                            UpdatedBy = contact.UpdatedBy,
                            UpdatedDate = contact.UpdatedDate,
                            StatusID = contact.StatusID,
                            Status = contact.StatusID.IsNotNull() ? new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = contact.StatusID.ToString(), Value = contact.StatusID.Value == 1 ? "Active" : "In Active" } : null,
                            // Phone
                            Phones = CustomerBL.EntityPropertyMapDTOs(dto.SupplierIID, EntityTypes.Supplier, EntityPropertyTypes.Telephone),
                            // Email
                            Emails = CustomerBL.EntityPropertyMapDTOs(dto.SupplierIID, EntityTypes.Supplier, EntityPropertyTypes.Email),
                            // Fax
                            Faxs = CustomerBL.EntityPropertyMapDTOs(dto.SupplierIID, EntityTypes.Supplier, EntityPropertyTypes.Fax),
                        });
                    }
                }

                // Get List<EntityTypePaymentMethodMap> GetEntityTypePaymentMethodMapByReferenceID
                EntityTypePaymentMethodMap entityTypePaymentMethodMap = new EntityTypePaymentMethodMap() { ReferenceID = entity.SupplierIID };
                List<EntityTypePaymentMethodMap> entityTypePaymentMethodMaps = new MutualRepository().GetEntityTypePaymentMethodMapByReferenceID(entityTypePaymentMethodMap);
                dto.BankAccounts = new List<BankAccountDTO>();
                foreach (var item in entityTypePaymentMethodMaps)
                {
                    if (item.PaymentMethodID == (short)PaymentMethods.Cash)
                    {
                        dto.IsCash = true;
                    }

                    if (item.PaymentMethodID == (short)PaymentMethods.Cheque)
                    {
                        dto.IsCheque = true;
                        dto.ChequeTypeID = item.EntityPropertyID;
                        dto.ChequeName = item.NameOnCheque;
                    }

                    if (item.PaymentMethodID == (short)PaymentMethods.BankTransfer)
                    {
                        BankAccountDTO dtoBankAccount = new BankAccountDTO();
                        dto.IsBankAccount = true;
                        dtoBankAccount.AccountNo = item.AccountID;
                        dtoBankAccount.AccountHolderName = item.AccountName;
                        dtoBankAccount.IBAN = item.IBANCode;
                        dtoBankAccount.SwiftCode = item.SWIFTCode;

                        dto.BankAccounts.Add(dtoBankAccount);
                    }

                    if (dto.BankAccounts.IsNotNull() && dto.BankAccounts.Count > 0)
                        dto.IsBankAccount = true;
                    else
                        dto.IsBankAccount = false;
                }
            }
            dto.Document = new DocumentViewDTO { Documents = null };

            //var SupplierAccountMapEntityList=  new SupplierRepository().GetSupplierAccountMaps((int)dto.SupplierIID);
            //dto.SupplierAccountMaps = new SupplierAccountMapDTO();
            //dto.SupplierAccountMaps.SupplierAccountEntitlements = FromEntityToSupplierAccountMapsDTO(SupplierAccountMapEntityList);
            

            return dto;
        }

        public Supplier ToEntity(SupplierDTO supplier)
        {
            var entity = new Supplier();

            if (supplier.IsNotNull())
            {
                // Supplier
                entity = new Supplier()
                {
                    TitleID = supplier.TitleID,
                    LoginID = supplier.LoginID, //(supplier.LoginID > 0 && supplier.LoginID.IsNotNull()) ? supplier.LoginID : _context.LoginID,
                    SupplierIID = supplier.SupplierIID,
                    FirstName = supplier.FirstName,
                    LastName = supplier.LastName,
                    MiddleName = supplier.MiddleName,
                    StatusID = supplier.StatusID,
                    VendorCR = supplier.VendorCR,
                    CRExpiry = supplier.CRExpiry,
                    VendorNickName = supplier.VendorNickName,
                    CompanyLocation = supplier.CompanyLocation,
                    IsMarketPlace = supplier.IsMarketPlace,
                    BranchID = supplier.BranchID,

                    ReceivingMethodID = supplier.ReceivingMethodID == 0 ? (int?)null : (int?)supplier.ReceivingMethodID,
                    ReturnMethodID = supplier.ReturnMethodID == 0 ? (int?)null : (int?)supplier.ReturnMethodID,
                    TimeStamps = string.IsNullOrEmpty(supplier.TimeStamps) ? null : Convert.FromBase64String(supplier.TimeStamps),
                    UpdatedBy = int.Parse(_context.LoginID.ToString()),
                    UpdateDate = DateTime.Now,
                    CompanyID = supplier.CompanyID.IsNotNull() ? supplier.CompanyID : _context.CompanyID,
                    Profit = supplier.Profit,
                    SupplierEmail = supplier.SupplierEmail,
                    Telephone = supplier.TelephoneNumber
                };

                if (entity.SupplierIID == 0)
                {
                    entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                    entity.CreatedDate = DateTime.Now;
                }
                // Contact
                if (supplier.Contacts.IsNotNull() && supplier.Contacts.Count > 0)
                {
                    entity.Contacts = new List<Contact>();

                    foreach (ContactDTO contactDTO in supplier.Contacts)
                    {
                        entity.Contacts.Add(new Contact()
                        {
                            ContactIID = contactDTO.ContactID,
                            LoginID = contactDTO.LoginID, //(contactDTO.LoginID > 0 && contactDTO.LoginID.IsNotNull()) ? contactDTO.LoginID : _context.LoginID,
                            TitleID = contactDTO.TitleID,
                            FirstName = contactDTO.FirstName,
                            MiddleName = contactDTO.MiddleName,
                            LastName = contactDTO.LastName,
                            SupplierID = contactDTO.SupplierID,
                            BuildingNo = contactDTO.BuildingNo,
                            Floor = contactDTO.Floor,
                            Block = contactDTO.Block,
                            AddressName = contactDTO.AddressName,
                            AddressLine1 = contactDTO.AddressLine1,
                            AddressLine2 = contactDTO.AddressLine2,
                            AreaID = contactDTO.Areas.IsNotNull() && !string.IsNullOrEmpty(contactDTO.Areas.Key) ? int.Parse(contactDTO.Areas.Key) : contactDTO.AreaID,
                            CountryID = contactDTO.Country.IsNotNull() && !string.IsNullOrEmpty(contactDTO.Country.Key) ? long.Parse(contactDTO.Country.Key) : contactDTO.CountryID,
                            CityID = contactDTO.Cities.IsNotNull() && !string.IsNullOrEmpty(contactDTO.Cities.Key) ? int.Parse(contactDTO.Cities.Key) : (contactDTO.CityID > 0 ? contactDTO.CityID : (int?)null),
                            State = contactDTO.State,
                            City = contactDTO.City,
                            District = contactDTO.District,
                            LandMark = contactDTO.LandMark,
                            Avenue = contactDTO.Avenue,
                            PostalCode = contactDTO.PostalCode,
                            TelephoneCode = contactDTO.TelephoneCode,
                            PhoneNo1 = contactDTO.PhoneNo1,
                            MobileNo1 = contactDTO.MobileNo1,
                            MobileNo2 = contactDTO.MobileNo2,
                            PassportNumber = contactDTO.PassportNumber,
                            AlternateEmailID1 = contactDTO.AlternateEmailID1,
                            AlternateEmailID2 = contactDTO.AlternateEmailID2,
                            WebsiteURL1 = contactDTO.WebsiteURL1,
                            WebsiteURL2 = contactDTO.WebsiteURL2,
                            IsBillingAddress = contactDTO.IsBillingAddress,
                            IsShippingAddress = contactDTO.IsShippingAddress,
                            Street = contactDTO.Street,
                            Flat = contactDTO.Flat,
                            CivilIDNumber = contactDTO.CivilIDNumber,
                            Description = contactDTO.Description,
                            //TimeStamps = string.IsNullOrEmpty(contactDTO.TimeStamps) ? null : Convert.FromBase64String(contactDTO.TimeStamps),
                            CreatedBy = _context.LoginID.IsNotNull() && contactDTO.ContactID == 0 ? Convert.ToInt32(_context.LoginID) : contactDTO.CreatedBy,
                            CreatedDate = contactDTO.ContactID == 0 ? DateTime.Now : contactDTO.CreatedDate,
                            UpdatedBy = _context.LoginID.IsNotNull() ? Convert.ToInt32(_context.LoginID) : contactDTO.UpdatedBy,
                            UpdatedDate = DateTime.Now,
                            StatusID = contactDTO.Status.IsNotNull() ? Convert.ToInt32(contactDTO.Status.Key) : (int)Eduegate.Services.Contracts.Enums.ContactStatus.Active,
                        });
                    }
                }
            }

            return entity;
        }
    }
}
