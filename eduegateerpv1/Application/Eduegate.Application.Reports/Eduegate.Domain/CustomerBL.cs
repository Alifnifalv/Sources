using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework;
using Eduegate.Framework.Security;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Globalization;
using Accounting = Eduegate.Services.Contracts.Accounting;
using Eduegate.Domain.Repository.Accounts;
using Eduegate.Services.Contracts.Accounting;
using System.Configuration;
using Eduegate.Framework.Contracts.Common.Enums;

namespace Eduegate.Domain
{
    public class CustomerBL
    {

        private MasterRepository masterRepository = new MasterRepository();
        private CustomerRepository customerRepository = new CustomerRepository();
        private Repository.AccountRepository accountRepo = new Repository.AccountRepository();

        private Eduegate.Framework.CallContext _callContext { get; set; }

        public CustomerBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public void UpdateUserDefaultBranch(long customerID, long branchID)
        {            
            customerRepository.UpdateUserDefaultBranch(customerID, branchID);
        }

        /// <summary>
        /// Get customer details for given customer id
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns>CustomerMaster: Customer Detail</returns>
        public CustomerMaster GetCustomer(long CustomerId)
        {
            return customerRepository.GetCustomer(CustomerId);
        }

        public void SaveCustomerCard(CustomerCardDTO card)
        {
            if (card.CustomerID.HasValue)
            {
                customerRepository.SaveCustomerCard(new CustomerCard()
                {
                    CardNumber = card.CardNumber,
                    CardTypeID = card.CardTypeID,
                    LoginID = card.LoginID,
                });
            }
            else
            {
                if (_callContext != null && _callContext.LoginID.HasValue)
                {
                    var customer = customerRepository.GetCustomerByLoginID(_callContext.LoginID.Value);
                    if (customer != null)
                    {
                        customerRepository.SaveCustomerCard(new CustomerCard()
                        {
                            CardNumber = card.CardNumber,
                            CardTypeID = card.CardTypeID,
                            LoginID = customer.LoginID.Value,
                            CustomerID = customer.CustomerIID
                        });
                    }
                }
            }
        }       

        public bool IsValidateOTP(string emirateID, string mobileNumber, string otpText)
        {
            var customer = customerRepository.GetCustomerByMobileNumber(mobileNumber);
            var smsTest = ConfigurationManager.AppSettings["SMSTest"];

            if (smsTest == "true")
            {
                return true;
            }

            if (customer.Login.LastOTP == otpText)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Customer GetCustomerByLoginID(long LonginID)
        {
            return customerRepository.GetCustomerByLoginID(LonginID);
        }

        /// <summary>
        /// Get all customer categorizations(memberships)
        /// </summary>
        /// <returns>List of CustomerCategorization</returns>
        public List<CustomerCategorization> GetCustomerCategorizations()
        {
            return masterRepository.GetCustomerCategorizations();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public CustomerMembershipDTO GetCustomerMembership(string CustomerId)
        {
            CustomerMembershipDTO customerMembershipDTO = new CustomerMembershipDTO();

            //get customer detail from customer repo
            CustomerMaster customerDetails = GetCustomer(Convert.ToInt32(CustomerId));

            //get membership slabs from Master repo
            List<CustomerCategorization> customerMemberships = GetCustomerCategorizations();

            //Check what slab customer lies in (using CategorizationPoints from customer)
            CustomerCategorization lowerRange = customerMemberships.Where(x => x.SlabPoint < customerDetails.CategorizationPoints || x.SlabPoint == customerDetails.CategorizationPoints).OrderByDescending(x => x.SlabPoint).FirstOrDefault();
            CustomerCategorization upperRange = customerMemberships.Where(x => x.SlabPoint > customerDetails.CategorizationPoints).OrderBy(x => x.SlabPoint).FirstOrDefault();

            //filling details in customer dto
            customerMembershipDTO.CustomerEmailId = customerDetails.EmailID;
            customerMembershipDTO.CustomerFirstName = customerDetails.FirstName;
            customerMembershipDTO.CustomerLastName = customerDetails.LastName;
            customerMembershipDTO.CustomerId = customerDetails.CustomerID;
            customerMembershipDTO.CategorizationPoints = Convert.ToInt32(customerDetails.CategorizationPoints);
            customerMembershipDTO.LoyaltyPoints = Convert.ToInt32(customerDetails.TotalLoyaltyPoints);
            customerMembershipDTO.ApplicableCategory = lowerRange.IsNotNull() ? lowerRange.CategoryName : null;
            customerMembershipDTO.NextApplicableCategory = upperRange.IsNotNull() ? upperRange.CategoryName : null;
            customerMembershipDTO.PointsNeededForNextCategory = upperRange.IsNotNull() ? Convert.ToInt32(upperRange.SlabPoint) - Convert.ToInt32(customerDetails.CategorizationPoints) : 0;

            //finally return CustomerMembershipDTO
            return customerMembershipDTO;
        }

        public List<CustomerDTO> GetCustomers(string searchText, int dataSize)
        {
            List<CustomerDTO> customerDTOList = new List<CustomerDTO>();

            List<Customer> customerList = customerRepository.GetCustomers(searchText, dataSize);

            if (customerList != null && customerList.Count > 0)
            {
                foreach (Customer customer in customerList)
                {
                    customerDTOList.Add(new CustomerDTO()
                    {
                        CustomerIID = customer.CustomerIID,
                        FirstName = customer.FirstName,
                        MiddleName = customer.MiddleName,
                        LastName = customer.LastName,
                    });
                }
            }

            return customerDTOList;
        }

        public string AddSubscription(string Email)
        {
            if (customerRepository.IsSubscribe(Email))
            {
                return null;
            }

            Subscription subscription = new Subscription();

            subscription.SubscribeEmail = Email;
            subscription.StatusID = (byte)UserStatus.NeedEmailVerification;
            subscription.VarificationCode = Convert.ToString(Guid.NewGuid());
            subscription.CreatedDate = DateTime.Now;

            return customerRepository.AddSubscription(subscription);
        }

        public string SubscribeConfirmation(string VarificationCode)
        {
            return customerRepository.SubscribeConfirmation(VarificationCode);
        }

        public CustomerDTO GetCustomerV2(long customerID)
        {
            var customer = customerRepository.GetCustomerV2(customerID);
            var dto = FromEntity(customer, _callContext);
                      

            return dto;
        }

        public CustomerDTO GetCustomerByTransactionHeadId(long headId)
        {
            var customer = customerRepository.GetCustomerByTransactionHeadId(headId);
            var dto = FromEntity(customer, _callContext);

            return dto;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">maybe supplier or customer </param>
        /// <param name="entityTypes"></param>
        /// <param name="entityPropertyTypes"></param>
        /// <returns></returns>
        public static List<EntityPropertyMapDTO> EntityPropertyMapDTOs(long id, EntityTypes entityTypes, EntityPropertyTypes entityPropertyTypes)
        {
            var repo = new CustomerRepository();
            List<EntityPropertyMap> phonePropertyMaps = repo.GetEntityPropertyMapsByType(id, (short)entityTypes, (short)entityPropertyTypes);

            List<EntityPropertyMapDTO> dtos = phonePropertyMaps.Select(x => EntityPropertyMapMapper.ToDto(x, repo.GetEntityPropertyName(x.EntityPropertyID))).ToList();
            return dtos;
        }

        public void SaveOTP(string mobileNumber, string otpText)
        {
            var repo = new CustomerRepository();
            var customer = repo.GetCustomerByMobileNumber(mobileNumber);

            if (customer == null) //create a new user
            {
                customer = repo.SaveCustomer(new Customer()
                {
                    CompanyID = _callContext.CompanyID.Value,
                    FirstName = mobileNumber,                    
                    Login = new Login() { LastOTP = otpText, LoginEmailID = mobileNumber + "@unknown.com", LoginUserID = mobileNumber },
                    Telephone = mobileNumber,
                    IsOfflineCustomer = false,
                });

                customer = repo.GetCustomerByMobileNumber(mobileNumber);
            }

            if (customer != null && customer.LoginID.HasValue)
            {
                repo.SaveCustomerOTP(customer.LoginID.Value, otpText);
            }
        }

        public CustomerDTO SaveCustomer(CustomerDTO customer)
        {
            var customerEntity = ToEntity(customer, _callContext);
            // here we don't need Customer Contacts
            customerEntity.Contacts = null;

            customerEntity.Login = Mappers.LoginMapper.Mapper(_callContext).ToEntity(customer.Login);

            foreach (var contact in customer.Contacts)
            {
                var contactEntity = ToContactEntity(contact, _callContext);
                contactEntity.LoginID = contact.LoginID.IsNotNull() ? contact.LoginID : customerEntity.Login.LoginIID;
                customerEntity.Login.Contacts.Add(contactEntity);
            }

            var updatedEntity = customerRepository.SaveCustomer(customerEntity);

            // Save CustomerSupplierMap
            if (customer.SupplierID.IsNotNull() && !string.IsNullOrEmpty(customer.SupplierID.Key) && updatedEntity.CustomerIID.IsNotNull())
            {
                if (Convert.ToInt64(customer.SupplierID.Key) > 0)
                {
                    CustomerSupplierMap customerSupplierMap = new CustomerRepository().SaveCustomerSupplierMap(ToCustomerSupplierMapEntity(updatedEntity.CustomerIID, Convert.ToInt64(customer.SupplierID.Key)));
                    updatedEntity.CustomerSupplierMaps.Add(customerSupplierMap);
                }
            }

            // Save EntitlementMaps
            if (updatedEntity.CustomerIID.IsNotNull() && updatedEntity.CustomerIID > 0)
            {
                List<EntitlementMap> entities = customer.Entitlements.EntitlementMaps.Select(x => EntitlementMapMapper.Mapper.ToEntity(x)).ToList();
                foreach (var entity in entities)
                {
                    if (entity.EntitlementID.HasValue && entity.EntitlementID.Value != 0)
                    {
                        entity.ReferenceID = updatedEntity.CustomerIID;
                        new MutualRepository().SaveEntitlementMap(entity);
                    }
                }

                // Save customer docs
                if (customer.Document.IsNotNull() && customer.Document.Documents.Count > 0 && customer.Document.Documents[0].FileName.IsNotNullOrEmpty())
                {
                    new DocumentBL(_callContext).SaveDocuments(customer.Document.Documents, EntityTypes.Customer, updatedEntity.CustomerIID);
                }

                // Save customer-entitlement-pricelist maps
                if (customer.PriceListEntitlement.IsNotNull() && customer.PriceListEntitlement.EntitlementPriceListMaps.Count > 0)
                {
                    // filter entitlements to get ones with ProductPriceList attached
                    var filteredEntitlementPriceListMaps = customer.PriceListEntitlement.EntitlementPriceListMaps
                        .Select(x => EntitlementPriceListMapMapper.Mapper(_callContext).ToEntity(x))
                        .Where(x => x.ProductPriceListID > 0).ToList();

                    filteredEntitlementPriceListMaps.ForEach(m => m.CustomerID = updatedEntity.CustomerIID);
                    // Save 
                    new CustomerRepository().SavePriceListEntitlementMaps(filteredEntitlementPriceListMaps);
                }

                // Save CustomerProductReferences 
                List<CustomerProductReferenceDTO> dtos = new List<CustomerProductReferenceDTO>();
                foreach (var item in customer.ExternalSettings.ExternalProductSettings)
                {
                    if (item.ProductSKUMapID.IsNotNull() && item.ProductSKUMapID > 0)
                    {
                        CustomerProductReferenceDTO dtoCustomerProductReference = new CustomerProductReferenceDTO();
                        dtoCustomerProductReference.CustomerID = updatedEntity.CustomerIID;
                        dtoCustomerProductReference.ProductSKUMapID = item.ProductSKUMapID;
                        dtoCustomerProductReference.BarCode = item.ExternalBarcode;
                        dtos.Add(dtoCustomerProductReference);
                    }
                }
                SaveCustomerProductReferences(dtos);
            }

            var dto = FromEntity(updatedEntity, _callContext);
            return dto;
        }

        public static CustomerDTO FromEntity(Customer entity, CallContext context)
        {
            Customer customer = new Customer();
            if (entity.ParentCustomerID.IsNotNull() && entity.ParentCustomerID > 0)
            {
                customer = new CustomerRepository().GetCustomerV2((long)entity.ParentCustomerID);
            }

            Supplier supplier = new Supplier();
            if (entity.CustomerSupplierMaps.IsNotNull() && entity.CustomerSupplierMaps.Count > 0)
            {
                if (entity.CustomerSupplierMaps.FirstOrDefault().SupplierID != null && entity.CustomerSupplierMaps.FirstOrDefault().SupplierID > 0)
                {
                    supplier = new SupplierRepository().GetSupplier((long)entity.CustomerSupplierMaps.FirstOrDefault().SupplierID);
                }
            }

            List<EntitlementMap> EntitlementMaps = new List<EntitlementMap>();
            List<EntitlementMapDTO> dtoEntitlementMaps = new List<EntitlementMapDTO>();
            EntitlementMap entitlementMap = new EntitlementMap() { ReferenceID = entity.CustomerIID };

            EntitlementMaps = new MutualRepository().GetEntitlementMaps(entitlementMap, (short)EntityTypes.Customer);
            dtoEntitlementMaps = EntitlementMaps.Select(x => EntitlementMapMapper.Mapper.ToDTO(x)).ToList();

            // Get Customer-Entitlement-PriceList Maps
            var customerEntitlementPriceListMaps = new MutualRepository().GetCustomerEntitlementPriceListMaps(entity.CustomerIID);

            var customerEntitlementPriceListMapsDTO = new PriceListEntitlementDTO();
            customerEntitlementPriceListMapsDTO.EntitlementPriceListMaps = new List<EntitlementPriceListMapDTO>();

            foreach (var entitlement in dtoEntitlementMaps)
            {
                // Create new map
                var mapDTO = new EntitlementPriceListMapDTO();

                // Assign other properties from entitlements
                mapDTO.EntitlementID = entitlement.EntitlementID;
                mapDTO.EntitlementName = entitlement.EntitlementName;
                mapDTO.CustomerID = entity.CustomerIID;
                mapDTO.TimeStamps = entitlement.TimeStamps;

                foreach (var map in customerEntitlementPriceListMaps)
                {
                    // Check if it has a pricelist map
                    if (map.EntitlementID == entitlement.EntitlementID)
                    {
                        mapDTO.ProductPriceListCustomerMapIID = map.ProductPriceListCustomerMapIID;
                        mapDTO.ProductPriceListIID = map.ProductPriceList.ProductPriceListIID;
                        mapDTO.PriceListName = map.ProductPriceList.PriceDescription;
                        mapDTO.CreatedBy = map.CreatedBy;
                        mapDTO.CreatedDate = map.CreatedDate;
                        mapDTO.UpdatedBy = map.UpdatedBy;
                        mapDTO.UpdatedDate = map.UpdatedDate;
                        mapDTO.TimeStamps = Convert.ToBase64String(map.TimeStamps);
                    }
                }

                // Add map to list
                customerEntitlementPriceListMapsDTO.EntitlementPriceListMaps.Add(mapDTO);
            }


            // Get CustomerProductReferences
            CustomerProductReference entityCustomerProductReference = new CustomerProductReference() { CustomerID = entity.CustomerIID };
            List<CustomerProductReference> listCustomerProductReference = new CustomerRepository().GetCustomerProductReferencesByCustomerID(entityCustomerProductReference);
            List<ExternalProductSettingsDTO> dtosExternalProductSettings = new List<ExternalProductSettingsDTO>();
            foreach (var item in listCustomerProductReference)
            {
                ExternalProductSettingsDTO dtoExternalProductSetting = new ExternalProductSettingsDTO();
                var posProduct = new ProductCatalogRepository().GetProductAndSKUByID((long)item.ProductSKUMapID);
                dtoExternalProductSetting.CustomerProductReferenceID = item.CustomerProductReferenceIID;
                dtoExternalProductSetting.CustomerID = item.CustomerID;
                dtoExternalProductSetting.ProductSKUMapID = item.ProductSKUMapID;
                dtoExternalProductSetting.ProductName = posProduct.ProductName;
                dtoExternalProductSetting.PartNo = posProduct.PartNo;
                dtoExternalProductSetting.ExternalBarcode = item.BarCode;
                dtosExternalProductSettings.Add(dtoExternalProductSetting);
            }


            var dto = new CustomerDTO()
            {
                TitleID = entity.TitleID.HasValue ? (short?)short.Parse(entity.TitleID.ToString()) : null,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                CustomerIID = entity.CustomerIID,
                FirstName = entity.FirstName,
                IsDifferentBillingAddress = entity.IsDifferentBillingAddress,
                IsSubscribeOurNewsLetter = entity.IsSubscribeForNewsLetter,
                IsTermsAndConditions = entity.IsTermsAndConditions,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,
                UpdatedDate = entity.UpdatedDate,
                LoginID = entity.LoginID,
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                StatusID = entity.StatusID.HasValue ? entity.StatusID.Value : (int?)null,
                GroupID = entity.GroupID.HasValue.IsNotNull() ? Convert.ToInt32(entity.GroupID) : (int?)null,
                IsOfflineCustomer = entity.IsOfflineCustomer,
                CivilIDNumber = entity.CivilIDNumber,
                CustomerCR = entity.CustomerCR,
                CRExpiryDate = entity.CRExpiryDate,
                TelephoneNumber = entity.Telephone,

                ParentCustomerID = new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                {
                    Key = customer.CustomerIID.IsNotNull() ? customer.CustomerIID.ToString() : null,
                    Value = string.Concat(customer.FirstName + " ", customer.MiddleName, customer.LastName)
                },

                ProductManagerID = new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                {
                    Key = entity.ProductManagerID.IsNotNull() ? entity.ProductManagerID.ToString() : null,
                    Value = entity.ProductManagerID.IsNotNull() ? new Payroll.EmployeeBL(null).GetEmployee(Convert.ToInt64(entity.ProductManagerID)).EmployeeName : null,
                },

                SupplierID = new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                {
                    Key = supplier.SupplierIID.IsNotNull() ? supplier.SupplierIID.ToString() : null,
                    Value = string.Concat(supplier.FirstName + " ", supplier.MiddleName, supplier.LastName)
                },

                Entitlements = new EntitlementDTO()
                {
                    EntitlementMaps = dtoEntitlementMaps
                },
                ExternalSettings = new ExternalSettingsDTO()
                {
                    ExternalProductSettings = dtosExternalProductSettings
                },
                //TelephoneNumber = entity.TelephoneNumber

                PriceListEntitlement = new PriceListEntitlementDTO()
                {
                    EntitlementPriceListMaps = customerEntitlementPriceListMapsDTO.EntitlementPriceListMaps
                },
            };

            if (entity.Login != null)
            {
                dto.Login = LoginMapper.Mapper(context).ToDTO(entity.Login);
                dto.Contacts = new List<ContactDTO>();

                if (entity.Login.Contacts != null)
                {
                    foreach (var contact in entity.Login.Contacts)
                    {
                        dto.Contacts.Add(FromContactEntity(contact));
                    }
                }
            }


            if (dto.Contacts.IsNotNull() && dto.Contacts.Count > 0)
            {
                foreach (var contact in dto.Contacts)
                {
                    // Phone
                    contact.Phones = EntityPropertyMapDTOs(dto.CustomerIID, EntityTypes.Customer, EntityPropertyTypes.Telephone);
                    // Email
                    contact.Emails = EntityPropertyMapDTOs(dto.CustomerIID, EntityTypes.Customer, EntityPropertyTypes.Email);
                    // Fax
                    contact.Faxs = EntityPropertyMapDTOs(dto.CustomerIID, EntityTypes.Customer, EntityPropertyTypes.Fax);
                }
            }
            //dto.ExternalSettings.ExternalProductSettings = new List<ExternalProductSettingsDTO>();
            //dto.ExternalSettings.ExternalProductSettings = dtosExternalProductSettings;
            dto.Document = new DocumentViewDTO { Documents = null };

            if (entity.CustomerSettings != null)
            {
                foreach (var setting in entity.CustomerSettings)
                {
                    dto.Settings = new Services.Contracts.Admin.CustomerSettingDTO()
                    {
                        CustomerSettingIID = setting.CustomerSettingIID,
                        CurrentLoyaltyPoints = setting.CurrentLoyaltyPoints,
                        TotalLoyaltyPoints = setting.TotalLoyaltyPoints,
                        IsConfirmed = setting.IsConfirmed,
                        IsVerified = setting.IsVerified,
                        IsBlocked = setting.IsBlocked,
                        //TimeStamps = Convert.ToBase64String(setting.TimeStamps),
                    };
                }
            }

            return dto;
        }

        public static Customer ToEntity(CustomerDTO customer, CallContext _callContext)
        {
            var entity = new Customer()
            {
                TitleID = customer.TitleID,
                CustomerIID = customer.CustomerIID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MiddleName = customer.MiddleName,
                IsDifferentBillingAddress = customer.IsDifferentBillingAddress,
                IsSubscribeForNewsLetter = customer.IsSubscribeOurNewsLetter,
                IsTermsAndConditions = customer.IsTermsAndConditions,
                LoginID = customer.LoginID,
                //TimeStamps = string.IsNullOrEmpty(customer.TimeStamps) ? null : Convert.FromBase64String(customer.TimeStamps),
                UpdatedBy = int.Parse(_callContext.LoginID.ToString()),
                UpdatedDate = DateTime.Now,
                StatusID = customer.StatusID > 0 ? (byte)customer.StatusID : (byte?)null,
                GroupID = customer.GroupID.IsNotNull() ? customer.GroupID : null,
                IsOfflineCustomer = customer.IsOfflineCustomer,
                CivilIDNumber = customer.CivilIDNumber,
                CustomerCR = customer.CustomerCR,
                CRExpiryDate = customer.CRExpiryDate,
                Telephone = customer.TelephoneNumber,
                ParentCustomerID = (customer.ParentCustomerID.IsNull() || string.IsNullOrWhiteSpace(customer.ParentCustomerID.Key)) ? (long?)null : (customer.ParentCustomerID.Key == "0" ? (long?)null : Convert.ToInt64(customer.ParentCustomerID.Key)),

                ProductManagerID = (customer.ProductManagerID.IsNull() || string.IsNullOrWhiteSpace(customer.ProductManagerID.Key)) ? (long?)null : Convert.ToInt64(customer.ProductManagerID.Key),

                //TelephoneNumber = customer.TelephoneNumber
                CustomerSettings = new List<CustomerSetting>()
                        {
                            new CustomerSetting()
                            {
                                CustomerSettingIID = customer.Settings.CustomerSettingIID,
                                CustomerID = customer.CustomerIID,
                                IsVerified = customer.Settings.IsVerified,
                                IsConfirmed = customer.Settings.IsConfirmed,
                                IsBlocked = customer.Settings.IsBlocked,
                                CurrentLoyaltyPoints = customer.Settings.CurrentLoyaltyPoints,
                                TotalLoyaltyPoints = customer.Settings.TotalLoyaltyPoints,
                                //TimeStamps = string.IsNullOrEmpty(customer.Settings.TimeStamps) ? null : Convert.FromBase64String(customer.Settings.TimeStamps),
                            }
                        }
            };

            if (entity.CustomerIID == 0)
            {
                entity.CreatedBy = int.Parse(_callContext.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }

        public static ContactDTO FromContactEntity(Contact entity)
        {
            var countryDetail = new CountryMasterBL().GetCountryDetail((long)entity.ContactIID);

            var area = entity.AreaID.HasValue ? new MutualRepository().GetAreaById(entity.AreaID.Value) : null;
            var city = entity.CityID.HasValue ? new MutualRepository().GetCityById(entity.CityID.Value) : null;

            return new ContactDTO()
            {
                AddressLine2 = entity.AddressLine2,
                AddressLine1 = entity.AddressLine1,
                AddressName = entity.AddressName,
                AlternateEmailID1 = entity.AlternateEmailID1,
                AlternateEmailID2 = entity.AlternateEmailID2,
                Block = entity.Block,
                BuildingNo = entity.BuildingNo,
                City = entity.City,
                CivilIDNumber = entity.CivilIDNumber,
                ContactID = entity.ContactIID,
                CustomerID = entity.CustomerID,
                CountryID = entity.CountryID,
                // handle country
                Country = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = countryDetail.CountryID.IsNotNull() ? countryDetail.CountryID.ToString() : null, Value = countryDetail.CountryNameEn.IsNotNull() ? countryDetail.CountryNameEn : null },

                Description = entity.Description,
                FirstName = entity.FirstName,
                Flat = entity.Flat,
                Floor = entity.Floor,
                IsBillingAddress = entity.IsBillingAddress,
                IsShippingAddress = entity.IsShippingAddress,
                LastName = entity.LastName,
                LoginID = entity.LoginID,
                MiddleName = entity.MiddleName,
                MobileNo1 = entity.MobileNo1,
                MobileNo2 = entity.MobileNo2,
                //OfficePhoneNo = entity.OfficePhoneNo,
                PassportIssueCountryID = entity.PassportIssueCountryID,
                PassportNumber = entity.PassportNumber,
                PhoneNo1 = entity.PhoneNo1,
                PhoneNo2 = entity.PhoneNo2,
                PostalCode = entity.PostalCode,
                State = entity.State,
                Street = entity.Street,
                TelephoneCode = entity.TelephoneCode,
                //TelephoneNumber = entity.TelephoneNumber,
                //OfficePhoneNo = entity.OfficePhoneNo,
                TitleID = entity.TitleID.HasValue ? (short?)short.Parse(entity.TitleID.ToString()) : null,
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                WebsiteURL1 = entity.WebsiteURL1,
                WebsiteURL2 = entity.WebsiteURL2,
                Status = entity.StatusID.IsNotNull() ? new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.StatusID.ToString(), Value = entity.StatusID.Value == 1 ? "Active" : "In Active" } : null,
                AreaID = entity.AreaID,
                Avenue = entity.Avenue,
                District = entity.District,
                LandMark = entity.LandMark,
                CityID = entity.CityID.HasValue ? entity.CityID.Value : 0,
                StatusID = entity.StatusID,
                Areas = area.IsNotNull() ? new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = area.AreaID.ToString(), Value = area.AreaName} : null,
                Cities = city.IsNotNull() ?  new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = city.CityID.ToString(), Value = city.CityName} : null,
            };
        }

        public static Contact ToContactEntity(ContactDTO dto, CallContext _callContext)
        {
            var entity = new Contact()
            {
                UpdatedBy = int.Parse(_callContext.LoginID.ToString()),
                UpdatedDate = DateTime.Now,
                //TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps),
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                AddressName = dto.AddressName,
                AlternateEmailID1 = dto.AlternateEmailID1,
                AlternateEmailID2 = dto.AlternateEmailID2,
                Block = dto.Block,
                BuildingNo = dto.BuildingNo,
                CivilIDNumber = dto.CivilIDNumber,
                City = dto.City,
                ContactIID = dto.ContactID,
                CustomerID = dto.CustomerID,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                FirstName = dto.FirstName,
                Flat = dto.Flat,
                Floor = dto.Floor,
                IsBillingAddress = dto.IsBillingAddress,
                IsShippingAddress = dto.IsShippingAddress,
                LastName = dto.LastName,
                LoginID = dto.LoginID,
                MiddleName = dto.MiddleName,
                MobileNo1 = dto.MobileNo1,
                MobileNo2 = dto.MobileNo2,
                PassportIssueCountryID = dto.PassportIssueCountryID,
                PassportNumber = dto.PassportNumber,
                PhoneNo1 = dto.PhoneNo1,
                PhoneNo2 = dto.PhoneNo2,
                PostalCode = dto.PostalCode,
                State = dto.State,
                Street = dto.Street,
                TelephoneCode = dto.TelephoneCode,
                TitleID = dto.TitleID,
                WebsiteURL1 = dto.WebsiteURL1,
                WebsiteURL2 = dto.WebsiteURL2,
                Avenue = dto.Avenue,
                District = dto.District,
                LandMark = dto.LandMark,
                AreaID = dto.Areas.IsNotNull() ? int.Parse(dto.Areas.Key) : dto.AreaID,
                CountryID = dto.Country.IsNotNull() ? long.Parse(dto.Country.Key) : dto.CountryID, //.IsNotNull() ? Convert.ToInt64(dto.Country.Key) : 1, // 1 for kuwait now
                CityID = dto.Cities.IsNotNull() ? int.Parse(dto.Cities.Key) : (dto.CityID > 0 ? dto.CityID : (int?)null),
                StatusID = dto.Status.IsNotNull() ? Convert.ToInt32(dto.Status.Key.ToString()) : (int)Eduegate.Services.Contracts.Enums.ContactStatus.Active
            };


            if (entity.ContactIID == 0)
            {
                entity.CreatedBy = int.Parse(_callContext.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }

        public CustomerGroupDTO GetCustomerGroup(long customerGroupID)
        {
            var customerGroup = customerRepository.GetCustomerGroup(customerGroupID);
            return FromGroupEntity(customerGroup);
        }

        public CustomerGroupDTO SaveCustomerGroup(CustomerGroupDTO customer)
        {
            return FromGroupEntity(customerRepository.SaveCustomerGroup(ToGroupEntity(customer, _callContext)));
        }

        public static CustomerGroupDTO FromGroupEntity(CustomerGroup entity)
        {
            return new CustomerGroupDTO()
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                CustomerGroupIID = entity.CustomerGroupIID,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                GroupName = entity.GroupName,
                UpdatedBy = entity.UpdatedBy,
                PointLimit = entity.PointLimit,
            };
        }

        public static CustomerGroup ToGroupEntity(CustomerGroupDTO group, CallContext _callContext)
        {
            var entity = new CustomerGroup()
            {
                //TimeStamps = string.IsNullOrEmpty(group.TimeStamps) ? null : Convert.FromBase64String(group.TimeStamps),
                UpdatedBy = int.Parse(_callContext.LoginID.ToString()),
                UpdatedDate = DateTime.Now,
                CustomerGroupIID = group.CustomerGroupIID,
                GroupName = group.GroupName,
                PointLimit = group.PointLimit,
            };

            if (entity.CustomerGroupIID == 0)
            {
                entity.CreatedBy = int.Parse(_callContext.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }


        /// <summary>
        /// get two input param and return Customer object
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="phone">string</param>
        /// <returns>return a Customer objectweather email or phone exist in DB</returns>
        public CustomerDTO IsCustomerExist(string email, string phone)
        {
            Customer customer = new Customer();
            CustomerDTO dto = new CustomerDTO();
            customer = customerRepository.IsCustomerExist(email, phone);
            if (customer.IsNull())
            {
                return null;
            }
            dto = FromEntity(customer, _callContext);
            return dto;
        }

        public List<CustomerDTO> GetCustomerByCustomerIdAndCR(string searchText)
        {
            List<Customer> customers = new CustomerRepository().GetCustomerByCustomerIdAndCR(searchText);
            return customers.Select(x => new CustomerDTO() { CustomerIID = x.CustomerIID, FirstName = x.FirstName, MiddleName = x.MiddleName, LastName = x.LastName }).ToList();
        }

        public CustomerSupplierMap ToCustomerSupplierMapEntity(long customerID, long? supplierID)
        {
            return new CustomerSupplierMap()
            {
                CustomerID = customerID,
                SupplierID = supplierID,
                UpdatedBy = (int)_callContext.LoginID,
                UpdatedDate = DateTime.Now
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
            };
        }


        public List<CustomerProductReferenceDTO> SaveCustomerProductReferences(List<CustomerProductReferenceDTO> dtos)
        {
            if (dtos.IsNull() || dtos.Count == 0)
            {
                return null;
            }
            bool isSuccess = false;
            // Remove CustomerProductReferences
            isSuccess = new CustomerRepository().RemoveCustomerProductReferences(CustomerProductReferenceMapper.Mapper(_callContext).ToEntity(dtos[0]));

            List<CustomerProductReferenceDTO> dtosNew = new List<CustomerProductReferenceDTO>();

            foreach (var item in dtos)
            {
                var entitySaved =
                new CustomerRepository().SaveCustomerProductReferences(CustomerProductReferenceMapper.Mapper(_callContext).ToEntity(item));
                dtosNew.Add(CustomerProductReferenceMapper.Mapper(_callContext).ToDTO(entitySaved));
            }

            return dtosNew;
        }

        public CustomerDTO GetCustomerByContactID(long contactID)
        {
            CustomerDTO dto = new CustomerDTO();

            var customer = new CustomerRepository().GetCustomerByContactID(contactID);            // get the data from repo

            if (customer.IsNotNull())
            {
                dto = CustomerMapper.Mapper(_callContext).ToDTO(customer);

                if (customer.Contacts.IsNotNull() && customer.Contacts.Count > 0)
                {
                    dto.Contacts = new List<ContactDTO>();

                    foreach (var contact in customer.Contacts)
                    {
                        var dtoContact = ContactMapper.Mapper(_callContext).ToDTO(contact);
                        dto.Contacts.Add(dtoContact);
                    }
                }
            }

            return dto;
        }

        public NewsletterDTO AddNewsletterSubsciption(string emailID, int cultureID, string ipAddress)
        {
            var newsletterDTO = new NewsletterDTO();
            //cultureID = new UtilityBL().GetLanguageCultureId(_callContext.LanguageCode).CultureID;
            var result = new CustomerRepository().AddNewsletterSubsciption(emailID, cultureID, ipAddress);
            newsletterDTO.result = result;
            switch (newsletterDTO.result)
            {
                case 1:
                    newsletterDTO.message = ResourceHelper.GetValue("NewsletterSubscribed",this._callContext.LanguageCode);
                    break;
                case 2:
                    newsletterDTO.message = ResourceHelper.GetValue("NewsletterAlreadySubscribed", this._callContext.LanguageCode);
                    break;
                case 3:
                    newsletterDTO.message = ResourceHelper.GetValue("NewsletterAlreadySubscribedLogIn", this._callContext.LanguageCode);
                    break;
                default:
                    newsletterDTO.message = ResourceHelper.GetValue("PltryLater", this._callContext.LanguageCode);
                    break;
            }
            return newsletterDTO;
        }

        #region Customer Account Maps

        public List<Accounting.SupplierAccountEntitlmentMapsDTO> SaveCustomerAccountMaps(List<Accounting.SupplierAccountEntitlmentMapsDTO> customerAccountMapsDTOs)
        {
            List<CustomerAccountMap> entityList = FromAccountMapsDTOtoEntity(customerAccountMapsDTOs);
            entityList = customerRepository.SaveCustomerAccountMaps(entityList);
            return FromEntityToAccountMapsDTO(entityList);
        }

        public List<Accounting.SupplierAccountEntitlmentMapsDTO> GetCustomerAccountMaps(long customerID)
        {
           var SupplierAccountMapEntityList = new CustomerRepository().GetCustomerAccountMaps(customerID);
           return FromEntityToAccountMapsDTO(SupplierAccountMapEntityList);            
        }
       
        public  List<CustomerAccountMap> FromAccountMapsDTOtoEntity(List<Accounting.SupplierAccountEntitlmentMapsDTO> customerAccountMapsDTOs)
        {

            //DTO for Customer Account map and Supplier Account map are same
            List<CustomerAccountMap> entityList = new List<CustomerAccountMap>();

            Accounting.SupplierAccountEntitlmentMapsDTO baseEntitlementDTO = customerAccountMapsDTOs.Where(e => e.EntitlementID == null).FirstOrDefault();
            Account baseAccount = new Account();
            if (baseEntitlementDTO != null)
            {
                baseAccount = baseEntitlementDTO.Account.AccountID.HasValue ? new AccountingRepository().GetAccount(baseEntitlementDTO.Account.AccountID.Value) : null;
            }

            foreach (Accounting.SupplierAccountEntitlmentMapsDTO dto in customerAccountMapsDTOs)
            {
                CustomerAccountMap entity = new CustomerAccountMap();
                entity.EntitlementID = dto.EntitlementID != null ? (byte)dto.EntitlementID : dto.EntitlementID;

                entity.CustomerID = dto.SupplierID;
                entity.CustomerAccountMapIID = dto.SupplierAccountMapIID;

                if (entity.EntitlementID != null)
                {
                    if (dto.Account.AccountID == 0)//new account
                    {
                        Account AccountEntity = new Account();
                        AccountEntity.AccountName = dto.SupplierID.ToString() + " " + dto.Account.AccountName;
                        AccountEntity.Alias = dto.Account.Alias;

                        //Base Account values
                        AccountEntity.AccountBehavoirID = dto.Account.AccountBehavior.HasValue ? (byte)dto.Account.AccountBehavior : (byte?)null;
                        //AccountEntity.GroupID = dto.Account.AccountGroup.AccountGroupID;
                        //AccountEntity.ParentAccountID = dto.Account.ParentAccount.AccountID;
                        //AccountEntity.AccountBehavoirID = (byte)baseAccount.AccountBehavoir.AccountBehavoirID;
                        if (baseAccount != null)
                        {
                            AccountEntity.GroupID = baseAccount.Group.GroupID;
                            AccountEntity.ParentAccountID = baseAccount.AccountID;
                            AccountEntity.ChildAliasPrefix = baseAccount.ChildAliasPrefix;
                        }

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

        public  List<Accounting.SupplierAccountEntitlmentMapsDTO> FromEntityToAccountMapsDTO(List<CustomerAccountMap> entityList)
        {
            //DTO for Customer Account map and Supplier Account map are same
            List<Accounting.SupplierAccountEntitlmentMapsDTO> dtoList = new List<Accounting.SupplierAccountEntitlmentMapsDTO>();
            foreach (CustomerAccountMap entity in entityList)
            {
                var dto = new Accounting.SupplierAccountEntitlmentMapsDTO();
                dto.AccountID = Convert.ToInt32(entity.AccountID);
                dto.SupplierID = entity.CustomerID;
                dto.SupplierAccountMapIID = entity.CustomerAccountMapIID;
                dto.EntitlementID = entity.EntitlementID;
                dto.EntitlementName = entity.EntityTypeEntitlement != null ? entity.EntityTypeEntitlement.EntitlementName : "";

                if (entity.Account != null)
                {
                    AccountDTO entitelmentAccount = new AccountDTO();
                    entitelmentAccount.AccountID = entity.Account.AccountID;
                    entitelmentAccount.AccountName = entity.Account.AccountName;
                    entitelmentAccount.Alias = entity.Account.Alias;
                    entitelmentAccount.AccountBehavior = entity.Account.AccountBehavoir != null ? (Services.Contracts.Enums.Accounting.AccountBehavior)entity.Account.AccountBehavoir.AccountBehavoirID : (Services.Contracts.Enums.Accounting.AccountBehavior?)null;
                    entitelmentAccount.AccountGroup = new AccountGroupDTO() { AccountGroupID = entity.Account.Group != null ? entity.Account.Group.GroupID : (int?)null };
                    entitelmentAccount.ParentAccount = entity.Account.Account1 != null ? new AccountDTO() { AccountID = entity.Account.Account1.AccountID } : null;
                    dto.Account = entitelmentAccount;
                }
                dtoList.Add(dto);
            }
            return dtoList;
        }
        #endregion Customer Account Maps

        public bool CheckCustomerEmailIDAvailability(long contactId, string mobileNumber)
        {
            return new CustomerRepository().CheckCustomerEmailIDAvailability(contactId, mobileNumber);
        }

        public CustomerDTO GetCustomerDetailsLoyaltyPoints(long customerID)
        {
            var customer = customerRepository.GetCustomerDetailsLoyaltyPoints(customerID);
            return CustomerMapper.Mapper(_callContext).ToDTOReference(customer);
        }

        public List<TransactionHeadDTO> GetTransactionHeadLoyaltyPoints(long customerID)
        {
            var transactionList = customerRepository.GetTransactionHeadLoyaltyPoints(customerID, this._callContext.CompanyID.Value);
            var dtoList = new List<TransactionHeadDTO>();
            var mapper = new TransactionHeadMapper();
            dtoList = mapper.ToDTOReferenceList(transactionList);

            return dtoList;
        }

        public CustomerGroupDTO GetCustomerGroup(decimal categorizationPoints)
        {
            var customerGroup = customerRepository.GetCustomerGroup(categorizationPoints);
            var dto = new CustomerGroupMapper().ToDTO(customerGroup);
            var enumCustomerGroup = new CustomerGroups();
            Enum.TryParse(customerGroup.CustomerGroupIID.ToString(), out enumCustomerGroup);
            switch (enumCustomerGroup)
            {
                case CustomerGroups.BlueMember:
                    dto.GroupName = ResourceHelper.GetValue("BlueMember", _callContext.LanguageCode);
                    break;
                case CustomerGroups.SilverMember:
                    dto.GroupName = ResourceHelper.GetValue("SilverMember", _callContext.LanguageCode);
                    break;
                case CustomerGroups.GoldMember:
                    dto.GroupName = ResourceHelper.GetValue("GoldMember", _callContext.LanguageCode);
                    break;
                case CustomerGroups.PlatinumMember:
                    dto.GroupName = ResourceHelper.GetValue("PlatinumMember", _callContext.LanguageCode);
                    break;
                case CustomerGroups.DiamondMember:
                    dto.GroupName = ResourceHelper.GetValue("DiamondMember", _callContext.LanguageCode);
                    break;
                case CustomerGroups.DiamondsMember:
                    dto.GroupName = ResourceHelper.GetValue("DiamondPlusMember", _callContext.LanguageCode);
                    break;
                default:
                    dto.GroupName = "";
                    break;
            }
            return dto;
        }

        public bool CustomerVerificatonCheck(long customerID)
        {
            var customerDetail = new AccountBL(_callContext).GetUserDetailsByCustomerID(customerID, false);
            return customerDetail.IsNotNull() && customerDetail.Customer.Settings.IsVerified.HasValue && customerDetail.Customer.Settings.IsBlocked.HasValue && customerDetail.Customer.Settings.IsVerified.Value == true && customerDetail.Customer.Settings.IsBlocked.Value == false ? true : false;
        }

        public long GetCustomerIDByContext()
        {
            return GetCustomerIDByLogin(this._callContext.EmailID,
                this._callContext.MobileNumber,
                this._callContext.LoginID, this._callContext.LanguageCode);
        }

        public long GetCustomerIDByLogin(string loginEmailID, string mobileNumber = null,
        long? loginID = null, string languageCode = null)
        {
            if (!loginID.HasValue)
            {
                var login = 
                   !string.IsNullOrEmpty(loginEmailID) ?
                   accountRepo.GetUserByLoginEmail(loginEmailID) :
                   accountRepo.GetUserByMobileNumber(mobileNumber);
                loginID = login.LoginIID;
            }

            return accountRepo.GetCustomerIDbyLoginID(loginID.Value);
        }
    }
}
