using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Security;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.Admin;
using Eduegate.Globalization;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Domain
{
    public class AccountBL
    {
        private IAccountRepository accountRepo = new AccountRepository();
        private SupplierRepository supplierRepository = new SupplierRepository();
        EmployeeRepository employeeRepository = new EmployeeRepository();

        private CallContext Context { get; set; }

        public AccountBL(CallContext context)
        {
            Context = context;
        }

        public UserDTO GetUser(long loginID)
        {
            var login = accountRepo.GetUser(loginID);
            return FromEntity(login, false);
        }

        public List<UserDTO> GetUsers()
        {
            var dtos = new List<UserDTO>();
            var logins = accountRepo.GetUsers();
            logins.ForEach(login => dtos.Add(FromEntity(login, false)));
            return dtos;
        }

        public UserDTO SaveLogin(UserDTO userDTO)
        {
            var entity = ToEntity(userDTO);

            if (userDTO.IsRequired)
            {
                entity.PasswordSalt = PasswordHash.CreateHash(userDTO.Password);
                //encryt the value to save in the DB as Password
                entity.Password = StringCipher.Encrypt(userDTO.Password, entity.PasswordSalt);
            }

            return FromEntity(accountRepo.SaveLogin(entity));
        }

        public UserDTO GetUserData(UserDTO userDTO)
        {
            if (!string.IsNullOrEmpty(userDTO.LoginUserID))
            {
                var isUserExist = accountRepo.GetEmail((userDTO.LoginUserID));
                if (isUserExist != null)
                {
                    userDTO.LoginEmailID = isUserExist.LoginEmailID;
                   
                }
                else
                {
                    userDTO.LoginEmailID =string.Empty ;
                }
               

            }
            return userDTO;
        }

        public UserDTO RegisterUser(UserDTO userDTO)
        {
            if (!string.IsNullOrEmpty(userDTO.LoginEmailID))
            {
                var isEmailExist = accountRepo.GetEmailDetails((userDTO.LoginEmailID));
                if (isEmailExist != null)
                {
                    userDTO.LoginEmailID = isEmailExist.LoginEmailID;
                    throw new Exception("Email ID already exist!");
                }

            }

            if (!string.IsNullOrEmpty(userDTO.LoginID))
            {
                var existing = accountRepo.GetCustomerDetails(long.Parse(userDTO.LoginID));
                if (existing != null)
                {
                    userDTO.Customer.CustomerIID = existing.CustomerIID;
                    userDTO.Customer.TelephoneNumber = existing.Telephone;
                }
            }

            var entity = ToEntity(userDTO);

            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                //new user always set the password (from website)
                entity.PasswordSalt = PasswordHash.CreateHash(userDTO.Password);
                entity.Password = StringCipher.Encrypt(userDTO.Password, entity.PasswordSalt);
            }

            var login = accountRepo.RegisterUser(entity);

            //var login = accountRepo.RegisterUser(entity);

            userDTO.LoginUserID = login.LoginUserID;
            userDTO.UserID = login.LoginIID;
            userDTO.StatusID = Convert.ToByte(login.StatusID);

            if (userDTO.Customer != null)
            {
                userDTO.Customer.Property = new PropertyDTO()
                {
                    PropertyName = GetPropertyName(Convert.ToInt64(userDTO.Customer.TitleID))
                };
            }

            return userDTO;
        }

        public Login ToEntity(UserDTO userDTO)
        {
            Login login = new Login()
            {
                LoginIID = string.IsNullOrEmpty(userDTO.LoginID) ? userDTO.UserID : long.Parse(userDTO.LoginID),
                LoginUserID = string.IsNullOrEmpty(userDTO.LoginUserID) ? Guid.NewGuid().ToString() : userDTO.LoginUserID,
                LoginEmailID = userDTO.LoginEmailID,
                UserName = userDTO.UserName,
                ProfileFile = userDTO.ProfileFile,
                PasswordHint = userDTO.PasswordHint,
                StatusID = userDTO.StatusID,
                SchoolID = userDTO.SchoolID.HasValue ? userDTO.SchoolID : null,
                LoginRoleMaps = new List<LoginRoleMap>(),
                //TimeStamps = string.IsNullOrEmpty(userDTO.TimeStamps) ? null : Convert.FromBase64String(userDTO.TimeStamps),
                RegisteredCountryID = userDTO.RegisteredCountryID.HasValue ? userDTO.RegisteredCountryID.Value :
                    userDTO.Customer.IsNotDefault() && userDTO.Customer.CountryID.HasValue ? (int)userDTO.Customer.CountryID : (int?)null,
                RegisteredIP = userDTO.Geolocation.IsNotNull() ? userDTO.Geolocation.ip : userDTO.RegisteredIP,
                RegisteredIPCountry = userDTO.Geolocation.IsNotNull() ? userDTO.Geolocation.country_name : userDTO.RegisteredIPCountry,
                SiteID = userDTO.SiteID.IsNotNull() ? userDTO.SiteID : null,
                EmployeeID = userDTO.EmployeeID,
            };

            if (userDTO.Roles != null)
            {
                foreach (var role in userDTO.Roles)
                {
                    login.LoginRoleMaps.Add(new LoginRoleMap() { RoleID = role.RoleID, LoginID = login.LoginIID });
                }
            }

            if (login.LoginIID != 0)
            {
                foreach (var role in login.LoginRoleMaps)
                {
                    role.LoginID = login.LoginIID;
                }
            }

            if (login.LoginIID != 0)
            {
                login.UpdatedBy = int.Parse(Context.LoginID.Value.ToString());
                login.UpdatedDate = DateTime.Now;
            }
            else
            {
                login.CreatedBy = Context.LoginID.IsNotNull() ? int.Parse(Context.LoginID.Value.ToString()) : login.CreatedBy;
                login.CreatedDate = DateTime.Now;
            }

            //make feature based not hard coding
            var isJustCMS = ConfigurationExtensions.GetAppConfigValue<bool>("IsJustCMS");

            if (!isJustCMS)
            {
                var blockedDTO = Context.CompanyID.HasValue ? new SettingBL().GetSettingDetail(Eduegate.Framework.Helper.Constants.TransactionSettings.ISNEWUSERBLOCKED, long.Parse(Context.CompanyID.ToString())) : new SettingDTO() { SettingValue = "0" };

                if (userDTO.Customer.IsNotNull())
                {
                    if (userDTO.Customer.CustomerIID == 0)
                    {
                        login.Customers = new List<Customer>()
                        {
                            new Customer()
                            {
                                LoginID = string.IsNullOrEmpty(userDTO.LoginID) ? (long?)null : long.Parse(userDTO.LoginID),
                                FirstName = userDTO.Customer.FirstName,
                                MiddleName = userDTO.Customer.MiddleName,
                                LastName = userDTO.Customer.LastName,
                                DefaultBranchID = userDTO.Branch != null && userDTO.Branch.BranchIID != 0 ? userDTO.Branch.BranchIID : (long?) null,
                                GenderID = userDTO.Customer.GenderID,
                                CustomerAddress = userDTO.CustomerAddress,
                                AddressLatitude = userDTO.AddressLatitude,
                                AddressLongitude = userDTO.AddressLongitude,
                                TitleID = userDTO.Customer.TitleID,
                                //CountryID = userDTO.Customer.CountryID,
                                PassportNumber = userDTO.Customer.PassportNumber,
                                PassportIssueCountryID = userDTO.Customer.PassportIssueCountryID,
                                CivilIDNumber = userDTO.Customer.CivilIDNumber,
                                //TelephoneCode = userDTO.Customer.TelephoneCode,
                                //TelephoneNumber = userDTO.Customer.TelephoneNumber,
                                IsDifferentBillingAddress = userDTO.Customer.IsDifferentBillingAddress,
                                IsSubscribeForNewsLetter = userDTO.Customer.IsSubscribeOurNewsLetter,
                                IsTermsAndConditions = userDTO.Customer.IsTermsAndConditions,
                                CustomerIID = userDTO.Customer.CustomerIID,
                                HowKnowOptionID = userDTO.Customer.HowKnowOptionID,
                                HowKnowText = userDTO.Customer.HowKnowText,
                                CreatedDate = DateTime.Now,
                                CustomerSettings = new List<CustomerSetting>()
                                {
                                    new CustomerSetting()
                                    {
                                        CustomerID = userDTO.Customer.CustomerIID,
                                        IsVerified = false,
                                        IsConfirmed = false,
                                        CurrentLoyaltyPoints = 0,
                                        TotalLoyaltyPoints = 0,
                                        IsBlocked = blockedDTO.SettingValue == "0" ? false : true, // every new customer must be blocked by default
                                    }
                                }
                            }
                        };
                    }
                    else
                    {
                        login.Customers = new List<Customer>()
                        {
                            new Customer()
                            {
                                 CustomerIID = userDTO.Customer.CustomerIID,
                                 FirstName = userDTO.Customer.FirstName,
                                 MiddleName = userDTO.Customer.MiddleName,
                                 LastName = userDTO.Customer.LastName,
                                 DefaultBranchID = userDTO.Branch != null && userDTO.Branch.BranchIID != 0 ? userDTO.Branch.BranchIID : (long?) null,
                                 GenderID = userDTO.Customer.GenderID,
                                 CustomerAddress = userDTO.CustomerAddress,
                                 AddressLatitude = userDTO.AddressLatitude,
                                 AddressLongitude = userDTO.AddressLongitude,
                                 PassportNumber = userDTO.Customer.PassportNumber,
                                PassportIssueCountryID = userDTO.Customer.PassportIssueCountryID,
                                CivilIDNumber = userDTO.Customer.CivilIDNumber,
                                //TelephoneCode = userDTO.Customer.TelephoneCode,
                                Telephone = userDTO.Customer.TelephoneNumber,
                                IsDifferentBillingAddress = userDTO.Customer.IsDifferentBillingAddress,
                                IsSubscribeForNewsLetter = userDTO.Customer.IsSubscribeOurNewsLetter,
                                IsTermsAndConditions = userDTO.Customer.IsTermsAndConditions,
                                HowKnowOptionID = userDTO.Customer.HowKnowOptionID,
                            }};
                    }
                }

                if (userDTO.Supplier.IsNotNull())
                {
                    login.Suppliers = new List<Supplier>()
                {
                    new Supplier()
                    {
                        SupplierIID = userDTO.Supplier.SupplierIID,
                    }
                };
                }


                login.Contacts = MapContactsToDBEntity(userDTO.Contacts, Context);
            }

            login.ClaimSetLoginMaps = Mappers.Security.ClaimSetLoginMapper.Mapper(Context).FromKeyValueDTO(userDTO.ClaimSets, login.LoginIID);
            login.ClaimLoginMaps = Mappers.Security.ClaimLoginMapper.Mapper(Context).FromClaimDetailDTO(userDTO.Claims, login.LoginIID);
            var mapper = Mappers.Common.SettingMapper.Mapper();
            login.UserSettings = new List<UserSetting>();

            if (userDTO.UserSettings != null)
            {
                foreach (var setting in userDTO.UserSettings)
                {
                    login.UserSettings.Add(mapper.ToUserSettingEntity(setting, Context.CompanyID.Value, Context.LoginID.Value));
                }
            }
            return login;
        }

        public List<ContactDTO> GetContactSummaryList(CallContext context)
        {
            long loginID = UtilityRepository.GetLoginID(context.EmailID);
            List<ContactDTO> contacs = new List<ContactDTO>();

            foreach (Contact contact in accountRepo.GetContactSummaryList(loginID))
            {
                contacs.Add(new ContactDTO()
                {
                    ContactID = contact.ContactIID,
                    AddressName = contact.AddressName,
                });
            }

            return contacs;
        }

        public ContactDTO GetContactDetail(long contactID)
        {
            return MapContactsToDTO(accountRepo.GetContactDetail(contactID));
        }

        public bool AddContact(ContactDTO contactDTO, CallContext context)
        {
            List<Contact> contacts = MapContactsToDBEntity(new List<ContactDTO>() { contactDTO }, Context);
            contacts.First().LoginID = UtilityRepository.GetLoginID(context.EmailID);
            return accountRepo.AddContact(contacts.First());
        }

        public long AddContactContactID(ContactDTO contactDTO, CallContext context)
        {
            List<Contact> contacts = MapContactsToDBEntity(new List<ContactDTO>() { contactDTO }, Context);
            contacts.First().LoginID = context.LoginID.HasValue && context.LoginID.Value != 0 ? context.LoginID.Value : UtilityRepository.GetLoginID(context.EmailID);
            return accountRepo.AddContactContactID(contacts.First());
        }

        public bool UpdateContact(ContactDTO contactDTO)
        {
            //return accountRepo.UpdateContact(contactDTO);
            return true;
        }

        public bool RemoveContact(long contactID)
        {
            return accountRepo.RemoveContact(contactID);
        }

        private static List<Contact> MapContactsToDBEntity(List<ContactDTO> contacts, CallContext context)
        {
            List<Contact> contactsEntityList = null;
            if (contacts.IsNotNull() && contacts.Count() > 0)
            {
                contactsEntityList = new List<Contact>();
                foreach (ContactDTO contact in contacts)
                {
                    contactsEntityList.Add(new Contact()
                    {
                        ContactIID = contact.ContactID,
                        FirstName = contact.FirstName,
                        MiddleName = contact.MiddleName,
                        LastName = contact.LastName,
                        TitleID = contact.TitleID,
                        AddressLine1 = contact.AddressLine1,
                        AddressLine2 = contact.AddressLine2,
                        AddressName = contact.AddressName,
                        Block = contact.Block,
                        BuildingNo = contact.BuildingNo,
                        City = contact.City,
                        CivilIDNumber = contact.CivilIDNumber,
                        CountryID = contact.CountryID,
                        Description = contact.Description,
                        Flat = contact.Flat,
                        Floor = contact.Floor,
                        PassportIssueCountryID = contact.PassportIssueCountryID,
                        PassportNumber = contact.PassportNumber,
                        PostalCode = contact.PostalCode,
                        State = contact.State,
                        Street = contact.Street,
                        AlternateEmailID1 = contact.AlternateEmailID1,
                        AlternateEmailID2 = contact.AlternateEmailID2,
                        IsBillingAddress = contact.IsBillingAddress,
                        IsShippingAddress = contact.IsShippingAddress,
                        TelephoneCode = contact.TelephoneCode,
                        PhoneNo1 = contact.TelephoneNumber,
                        //TimeStamps = string.IsNullOrEmpty(contact.TimeStamps) ? null : Convert.FromBase64String(contact.TimeStamps),
                        CreatedBy = context.LoginID.IsNotNull() && contact.ContactID == 0 ? Convert.ToInt32(context.LoginID) : contact.CreatedBy,
                        CreatedDate = contact.ContactID == 0 ? DateTime.Now : contact.CreatedDate,
                        UpdatedBy = context.LoginID.IsNotNull() ? Convert.ToInt32(context.LoginID) : contact.UpdatedBy,
                        UpdatedDate = DateTime.Now,
                        MobileNo1 = contact.MobileNo1,
                        MobileNo2 = contact.MobileNo2,
                        Avenue = contact.Avenue,
                        AreaID = contact.AreaID,
                        CityID = contact.CityID,
                        District = contact.District,
                        LandMark = contact.LandMark,
                        IntlCity = contact.IntlCity,
                        IntlArea = contact.IntlArea,
                        StatusID = (int)Eduegate.Services.Contracts.Enums.ContactStatus.Active
                    });
                }
            }
            return contactsEntityList;
        }

        private static ContactDTO MapContactsToDTO(Contact contact)
        {
            if (contact.IsNotNull())
            {
                var Cities = new MutualRepository().GetCity(contact.CityID > 0 ? (long)contact.CityID : 0);
                var Area = new MutualRepository().GetArea(contact.AreaID > 0 ? (long)contact.AreaID : 0);
                var Customer = new AccountRepository().GetCustomerIDbyLoginID(contact.LoginID.HasValue ? contact.LoginID.Value : default(long));

                return new ContactDTO()
                {
                    AddressLine1 = contact.AddressLine1,
                    AddressLine2 = contact.AddressLine2,
                    AddressName = contact.AddressName,
                    AlternateEmailID1 = contact.AlternateEmailID1,
                    AlternateEmailID2 = contact.AlternateEmailID2,
                    Block = contact.Block,
                    BuildingNo = contact.BuildingNo,
                    // City = contact.City,
                    CivilIDNumber = contact.CivilIDNumber,
                    ContactID = contact.ContactIID,
                    CountryID = contact.CountryID,
                    Description = contact.Description,
                    FirstName = contact.FirstName,
                    Flat = contact.Flat,
                    Floor = contact.Floor,
                    IsBillingAddress = contact.IsBillingAddress,
                    IsShippingAddress = contact.IsShippingAddress,
                    LastName = contact.LastName,
                    MiddleName = contact.MiddleName,
                    MobileNo1 = contact.MobileNo1,
                    MobileNo2 = contact.MobileNo2,
                    PassportIssueCountryID = contact.PassportIssueCountryID,
                    PassportNumber = contact.PassportNumber,
                    PhoneNo1 = contact.PhoneNo1,
                    PhoneNo2 = contact.PhoneNo2,
                    PostalCode = contact.PostalCode,
                    State = contact.State,
                    Street = contact.Street,
                    TelephoneCode = contact.TelephoneCode,
                    TitleID = contact.TitleID.HasValue ? (short?)short.Parse(contact.TitleID.ToString()) : null,
                    CreatedDate = DateTime.Now,
                    CreatedBy = contact.CreatedBy,
                    LoginID = contact.LoginID,
                    //TimeStamps = Convert.ToBase64String(contact.TimeStamps),
                    CountryName = new ReferenceDataRepository().GetCountryName(contact.CountryID.HasValue ? contact.CountryID.Value : default(long)),
                    City = Cities.IsNotNull() ? Cities.CityName : string.Empty,
                    LandMark = contact.LandMark,
                    District = contact.District,
                    Avenue = contact.Avenue,
                    AreaName = Area.IsNotNull() ? Area.AreaName : string.Empty,
                    CustomerID = Customer.IsNotNull() ? Customer : contact.CustomerID,
                    AreaID = contact.AreaID,
                    StatusID = contact.StatusID
                };
            }
            return null;
        }

        public bool IsUserAvailable(string userName)
        {
            return accountRepo.IsUserAvailable(userName);
        }

        public bool Login(string userName, string password, byte AppId)
        {
            bool isMigratedCustomer = false;

            if (ConfigurationExtensions.GetAppConfigValue<bool>("IsMigratedUserCheck"))
            {
                isMigratedCustomer = accountRepo.IsMigratedCustomer(userName);
            }

            string encryptPassword = "";

            if (!isMigratedCustomer)
            {
                // get the hash code based on userName or Email
                string saltHash = GetSaltHashByUserName(userName);
                //checking userName is null or empty
                if (string.IsNullOrEmpty(saltHash))
                    return false;
                // is it valid or not
                bool isbool = PasswordHash.ValidatePassword(password, saltHash);
                CreateActivityStream(isbool, userName);
                if (!isbool)
                    return isbool;
                //encrypt the value to save in the DB as Password
                encryptPassword = StringCipher.Encrypt(password, saltHash);
            }
            else
            {

            }

            var isSuccess = accountRepo.Login(userName, encryptPassword, AppId);
            CreateActivityStream(isSuccess, userName);
            return isSuccess;
        }

        public void CreateActivityStream(bool isSuccess, string loginEmailID)
        {
            var loginDetails = new AccountRepository().GetUserByLoginEmail(loginEmailID);
            if (isSuccess)
            {
                new Logging.LoggingBL(null).SaveActivities(new List<Services.Contracts.Logging.ActivityDTO>() {
                new Services.Contracts.Logging.ActivityDTO() {
                        Description = "User logged-in",
                        CreatedUserName = loginDetails.LoginEmailID,
                        CreatedBy = int.Parse(loginDetails.LoginIID.ToString()),
                        CreatedDate = DateTime.Now,
                        ActivityTypeID =  (int)Eduegate.Services.Contracts.Enums.Logging.ActivityTypes.Login,
                        ActionStatusID = (int) Eduegate.Services.Contracts.Enums.Logging.ActionStatuses.Success
                    }
                });
            }
            else
            {
                new Logging.LoggingBL(null).SaveActivities(new List<Services.Contracts.Logging.ActivityDTO>() {
                new Services.Contracts.Logging.ActivityDTO() {
                        Description = "User logged-in",
                        CreatedUserName = loginDetails.LoginEmailID,
                        CreatedDate = DateTime.Now,
                        CreatedBy = int.Parse(loginDetails.LoginIID.ToString()),
                        ActivityTypeID =  (int)Eduegate.Services.Contracts.Enums.Logging.ActivityTypes.Login,
                        ActionStatusID = (int) Eduegate.Services.Contracts.Enums.Logging.ActionStatuses.Failed
                    }
                });
            }
        }

        public string GetSaltHashByUserName(string userName)
        {
            return accountRepo.GetSaltHashByUserName(userName);
        }

        public Common ConfirmEmail(string userId)
        {
            return accountRepo.ConfirmEmail(userId);
        }

        public KeyValuePair<Common, string> ForgotPassword(string loginEmailId)
        {
            return accountRepo.ForgotPassword(loginEmailId);
        }

        public Common ResetPassword(UserDTO user)
        {
            Login login = new Entity.Models.Login()
            {
                LoginIID = string.IsNullOrEmpty(user.LoginID) ? 0 : long.Parse(user.LoginID),
                LoginUserID = user.LoginUserID,
                Password = user.Password,
                PasswordSalt = user.PasswordSalt,
                UserName = user.UserName,
                LoginEmailID = user.LoginEmailID
            };
            return accountRepo.UpdatePassword(login);
        }

        public UserDTO GetUserDetailsByCustomerID(long customerID, bool isSecurityInfoRequired = true)
        {
            Login login = accountRepo.GetUserByCustomerID(customerID);
            return FromEntity(login, isSecurityInfoRequired);
        }

        public UserDTO GetUserDetails(string loginEmailID, string mobileNumber = null)        
        {
            string loginUserID = null;

            if (Context.UserId != null)
            {
                loginUserID = Context.UserId;
            }

            Login login = !string.IsNullOrEmpty(loginEmailID) ? accountRepo.GetUserByLoginEmail(loginEmailID) : !string.IsNullOrEmpty(loginUserID) ? accountRepo.GetUserByLoginUserID(loginUserID) : accountRepo.GetUserByMobileNumber(mobileNumber);

            if (login.IsNull())
                return null;

            var detail = GetDetails(login.LoginIID);

            if (Context.IsNotNull() && Context.CompanyID.HasValue)
            {
                var company = new ReferenceDataRepository().GetCompany(Context.CompanyID.Value);
                detail.CompanyName = company != null ? company.CompanyName : string.Empty;
            }

            if (detail.Branch != null && detail.Branch.BranchIID != 0)
            {
                detail.IsProfileCompleted = true;
            }
            else
            {
                detail.IsProfileCompleted = false;
            }

            return detail;
        }

        public UserDTO GetUserDetailsByID(long loginIID)
        {
            return GetDetails(loginIID); ;
        }

        private UserDTO GetDetails(long loginIID, Login login = null)
        {
            if (login == null)
            {
                login = accountRepo.GetUserDetailsByID(loginIID);
            }

            return FromEntity(login);
        }

        public UserDTO GetEmployeeDetails(string loginEmailID)
        {
            Login login = !string.IsNullOrEmpty(loginEmailID) ? accountRepo.GetEmployeeDetails(loginEmailID) : null;
            var detail = login != null ? GetDetails(login.LoginIID) : null;
            return detail;
        }

        public UserDTO FromEntity(Login login, bool isSecurityInfoRequired = true)
        {
            var isJustCMS = ConfigurationExtensions.GetAppConfigValue<bool>("IsJustCMS");
            if (login == null) return null;

            var contactsDTO = new List<ContactDTO>();

            var roleMaps = accountRepo.GetRoleDetails(login.LoginIID);
            var customer = isJustCMS ? null : accountRepo.GetCustomerDetails(login.LoginIID);
            var branch = isJustCMS || !(customer != null && customer.DefaultBranchID.HasValue) ? null : new ReferenceDataRepository().GetBranch(customer.DefaultBranchID.Value, false);
            var userClaims = new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaims(login.LoginIID);
            var supplier = isJustCMS ? null : supplierRepository.GetSupplierByLoginID(login.LoginIID);
            var employee = isJustCMS ? null : employeeRepository.GetEmployeeByLoginID(login.LoginIID);
            var contacts = accountRepo.GetContacts(login.LoginIID);

            var emp = login.EmployeeID.HasValue ? employeeRepository.GetEmployeeByEmployeeID(login.EmployeeID) : null;

            foreach (Contact contact in contacts)
            {
                contactsDTO.Add(MapContactsToDTO(contact));
            }

            var user = new UserDTO()
            {
                AddressLatitude = customer != null ? customer.AddressLatitude : null,
                AddressLongitude = customer != null ? customer.AddressLongitude : null,
                CustomerAddress = customer != null ? customer.CustomerAddress : null,
                Branch = customer != null && customer.DefaultBranchID.HasValue ? new Services.Contracts.Mutual.BranchDTO()
                {
                    BranchIID = customer.DefaultBranchID.Value,
                    BranchName = branch != null ? branch.BranchName : null
                } : null,
                LoginID = login.LoginIID.ToString(),
                UserID = login.LoginIID,
                LoginEmailID = login.LoginEmailID,
                LoginUserID = login.LoginUserID,
                Password = login.Password,
                PasswordSalt = login.PasswordSalt,
                PasswordHint = login.PasswordHint,
                EmployeeID = login.EmployeeID.HasValue ? login.EmployeeID : null,
                EmployeeName = emp != null ? emp.FirstName + " " + emp.MiddleName + " " + emp.LastName : null,
                EmployeeCode = emp != null ? emp.EmployeeCode : null,
                RoleName = roleMaps != null && roleMaps.FirstOrDefault() != null ? roleMaps.FirstOrDefault().Role.RoleName : null,
                UserName = login.UserName.IsNotNullOrEmpty() ? login.UserName : customer == null ? login.LoginEmailID : (string.IsNullOrEmpty(customer.FirstName) ? string.Empty : customer.FirstName) + " " +
                (string.IsNullOrEmpty(customer.MiddleName) ? string.Empty : customer.MiddleName) + " " + (string.IsNullOrEmpty(customer.LastName) ? string.Empty : customer.LastName),
                ProfileFile = login.ProfileFile,
                StatusID = Convert.ToByte(login.StatusID),
                SchoolID = login.SchoolID.HasValue ? login.SchoolID : null,
                Roles = isSecurityInfoRequired ? roleMaps.Select(a => new UserRoleDTO() { RoleID = a.RoleID.Value, RoleName = a.Role.RoleName }).ToList() : null,
                UserClaims = isSecurityInfoRequired ? userClaims.Select(a => ((Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Framework.Security.Enums.ClaimType), a.ClaimTypeID.ToString())).ToString() + "." + a.ResourceName).ToList() : null,
                ClaimSets = isSecurityInfoRequired ? Mappers.Security.ClaimSetLoginMapper.Mapper(Context).ToKeyValueDTO(login.ClaimSetLoginMaps.ToList()) : null,
                Claims = isSecurityInfoRequired ? Mappers.Security.ClaimLoginMapper.Mapper(Context).ToClaimDetailDTO(login.ClaimLoginMaps.ToList()) : null,
                //TimeStamps = Convert.ToBase64String(login.TimeStamps),
                SiteID = login.SiteID,
                RegisteredCountryID = login.RegisteredCountryID,
                RegisteredIP = login.RegisteredIP,
                RegisteredIPCountry = login.RegisteredIPCountry,
                IsRequired = login.RequirePasswordReset.HasValue ? login.RequirePasswordReset.Value : false,
                Customer = customer == null ? null : new CustomerDTO()
                {
                    GenderID = customer.GenderID,
                    CustomerIID = customer.CustomerIID,
                    LoginID = login.LoginIID,
                    FirstName = customer.FirstName,
                    MiddleName = customer.MiddleName,
                    LastName = customer.LastName != null ? customer.LastName : string.Empty,
                    TitleID = customer.TitleID.HasValue ? (short?)short.Parse(customer.TitleID.ToString()) : null,
                    //CountryID = customer.CountryID,
                    PassportNumber = customer.PassportNumber,
                    PassportIssueCountryID = customer.PassportIssueCountryID,
                    CivilIDNumber = customer.CivilIDNumber,
                    //TelephoneCode = customer.TelephoneCode,
                    TelephoneNumber = customer.Telephone,
                    IsDifferentBillingAddress = customer.IsDifferentBillingAddress,
                    IsTermsAndConditions = customer.IsTermsAndConditions,
                    IsSubscribeOurNewsLetter = customer.IsSubscribeForNewsLetter,
                    CustomerCardNumber = customer.CustomerCards != null && customer.CustomerCards.Count > 0 ? customer.CustomerCards.First().CardNumber : string.Empty,
                },
                Supplier = supplier == null ? null : new SupplierDTO()
                {
                    FirstName = supplier.FirstName,
                    LastName = supplier.LastName,
                    MiddleName = supplier.MiddleName,
                    TitleID = supplier.TitleID.HasValue ? (short?)short.Parse(supplier.TitleID.ToString()) : null,
                    SupplierIID = supplier.SupplierIID,
                },
                Employee = employee == null ? null : new Services.Contracts.Payroll.EmployeeDTO()
                {
                    EmployeeAlias = employee.EmployeeAlias,
                    EmployeeName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName,
                    EmployeeIID = employee.EmployeeIID,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    EmployeeCode = employee.EmployeeCode,
                    EmployeeRoles = new List<KeyValueDTO>()
                },
                Contacts = new List<ContactDTO>(),
            };

            if (user.Employee != null)
            {
                foreach (var role in employee.EmployeeRoleMaps)
                {
                    user.Employee.EmployeeRoles.Add(new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                    {
                        Key = role.EmployeeRole.EmployeeRoleID.ToString(),
                        Value = role.EmployeeRole.EmployeeRoleName,
                    });
                }
            }

            user.Contacts = contactsDTO;

            if (user.Customer.IsNotNull() && user.Customer.CustomerIID > 0)
            {
                CustomerSetting customerSettings = accountRepo.GetCustomerSettings(user.Customer.CustomerIID);
                if (customerSettings.IsNotNull())
                {
                    user.Customer.Settings = new CustomerSettingDTO()
                    {
                        // Fill other properties if needed
                        IsVerified = customerSettings.IsVerified.IsNotNull() ? customerSettings.IsVerified : false,
                        IsBlocked = customerSettings.IsBlocked.IsNotNull() ? customerSettings.IsBlocked : false,
                    };
                }
            }

            if (login.UserSettings != null)
            {
                var mapper = Mappers.Common.SettingMapper.Mapper();
                user.UserSettings = mapper.ToDTO(login.UserSettings.ToList());
            }

            return user;
        }

        public bool IsPasswordResetRequired(string loginEmailID)
        {
            return accountRepo.IsPasswordResetRequired(loginEmailID);
        }

        public string GetPropertyName(long proprtyID)
        {
            IPropertyRepository propertyRepo = new PropertyRepository();
            var property = propertyRepo.GetPropertyDetail(proprtyID);
            if (property.IsNotNull())
            {
                return property.PropertyName;
            }
            else
            {
                return string.Empty;
            }
        }


        public List<ContactDTO> GetBillingShippingContact(long customerID, AddressType addressType)
        {
            // declaraion
            var contact = new Contact();
            var dtoContacts = new List<ContactDTO>();

            // get the contact by customerID
            var contacts = new AccountRepository().GetContactsByCustomerID(customerID);
            var mapper = ContactMapper.Mapper(Context);

            // check based on addressType
            switch (addressType)
            {
                case AddressType.All:
                    // Convert db entity to dto contract
                    dtoContacts = contacts.Select(x => mapper.ToDTO(x)).ToList();
                    break;
                case AddressType.Billing:
                    dtoContacts = contacts.Where(x => x.IsBillingAddress == true).Select(x => mapper.ToDTO(x)).ToList();
                    break;
                case AddressType.Shipping:
                    dtoContacts = contacts.Where(x => x.IsShippingAddress == true).Select(x => mapper.ToDTO(x)).ToList();
                    break;
                default:
                    dtoContacts = contacts.Where(x => x.IsShippingAddress == true).Select(x => mapper.ToDTO(x)).ToList();
                    break;
            }
            return dtoContacts;
        }

        public List<KnowHowOptionDTO> GetKnowHowOptions()
        {
            var knowHowOptionList = new AccountRepository().GetKnowHowOptions();
            var knowHowOptionDTOList = new List<KnowHowOptionDTO>();
            foreach (var knowHowOption in knowHowOptionList)
            {
                knowHowOptionDTOList.Add(KnowHowOptionMapper.Mapper(Context).ToDTO(knowHowOption));
            }
            return knowHowOptionDTOList;
        }

        public bool isCustomerConfirmed(long CustomerID)
        {
            return accountRepo.isCustomerConfirmed(CustomerID);
        }


        public List<ContactDTO> GetShippingAddressContacts(long loginID, int siteID = 0)
        {
            var contactList = accountRepo.GetContacts(loginID);
            contactList = contactList.Where(a => a.IsShippingAddress == true && a.StatusID == (int)AddressStatus.Active).ToList();

            var contactDTOList = new List<ContactDTO>();
            foreach (var contact in contactList)
            {
                var countryDetail = new CountryMasterBL().GetCountryDetail((long)contact.ContactIID);
                var contactDTO = ContactMapper.Mapper(Context).ToDTO(contact);
                contactDTO.Country = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = countryDetail.CountryID.IsNotNull() ? countryDetail.CountryID.ToString() : null, Value = countryDetail.CountryNameEn.IsNotNull() ? countryDetail.CountryNameEn : null };
                if (contactDTO.AreaID.HasValue)
                {
                    var areaDTO = new MutualBL(Context).GetArea((int)contactDTO.AreaID);
                    contactDTO.Areas = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = areaDTO.AreaID.ToString(), Value = areaDTO.AreaName };
                    contactDTO.AreaName = areaDTO.AreaName;
                    var cityDTO = new MutualBL(Context).GetCity((int)contactDTO.CityID);
                    contactDTO.City = cityDTO.CityName;
                }
                if (contactDTO.CityID > 0)
                {
                    var cityDTO = new MutualBL(Context).GetCity((int)contactDTO.CityID);
                    contactDTO.City = cityDTO.CityName;
                }
                contactDTOList.Add(contactDTO);
            }
            return contactDTOList;
        }

        public ContactDTO GetBillingAddressContacts(long loginID)
        {
            var contactList = accountRepo.GetContacts(loginID);
            var Contact = new Contact();
            Contact = contactList.Where(a => a.IsBillingAddress == true).FirstOrDefault();
            var contactDTOList = new ContactDTO();

            if (Contact.IsNotNull() && Contact.IsNotDefault())
            {
                var countryDetail = new CountryMasterBL().GetCountryDetail((long)Contact.ContactIID);
                var contactDTO = ContactMapper.Mapper(Context).ToDTO(Contact);
                contactDTO.Country = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = countryDetail.CountryID.IsNotNull() ? countryDetail.CountryID.ToString() : null, Value = countryDetail.CountryNameEn.IsNotNull() ? countryDetail.CountryNameEn : null };



                if (contactDTO.AreaID.HasValue)
                {
                    //var areaDTO = new MutualBL(Context).GetArea(contactDTO.AreaID.Value);
                    //contactDTO.Areas = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = areaDTO.AreaID.ToString(), Value = areaDTO.AreaName };
                    var areaDTO = new MutualBL(Context).GetArea((int)contactDTO.AreaID);
                    contactDTO.Areas = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = areaDTO.AreaID.ToString(), Value = areaDTO.AreaName };
                    contactDTO.AreaName = areaDTO.AreaName;
                    var cityDTO = new MutualBL(Context).GetCity((int)contactDTO.CityID);
                    contactDTO.City = cityDTO.CityName;
                }

                if (contactDTO.CityID > 0)
                {
                    var cityDTO = new MutualBL(Context).GetCity((int)contactDTO.CityID);
                    contactDTO.City = cityDTO.CityName;
                }

                contactDTOList = contactDTO;
            }
            return contactDTOList;
        }

        public long GetCustomerIDbyLoginID(long loginID)
        {
            return accountRepo.GetCustomerIDbyLoginID(loginID);
        }

        public long GetLoginIDbyCustomerID(long customerID)
        {
            return accountRepo.GetLoginIDbyCustomerID(customerID);
        }

        public bool isUserAvailableEmailPhone(string emailID, string mobileNo)
        {
            return accountRepo.isUserAvailableEmailPhone(emailID, mobileNo);
        }

        public bool isDuplicateUserAvailableEmailPhone(string emailID, string mobileNo, long loginID)
        {
            return accountRepo.isDuplicateUserAvailableEmailPhone(emailID, mobileNo, loginID);
        }

        public ContactDTO GetLastShippingAddressContacts(long loginID, long addressID, long siteID)
        {
            var contactList = accountRepo.GetContacts(loginID);
            contactList = contactList.Where(a => a.IsShippingAddress == true && a.StatusID == (int)AddressStatus.Active).ToList();
            var contact = contactList.Where(a => a.IsShippingAddress == true && a.ContactIID == addressID && a.StatusID == (int)AddressStatus.Active).FirstOrDefault();

            if (contact.IsNull())
            {
                var lastContact = accountRepo.GetLastOrderContact(loginID);
                if (lastContact.IsNotNull() && lastContact.IsNotDefault())
                {
                    contact = contactList.Where(a => a.ContactIID == lastContact.ContactIID && a.StatusID == (int)AddressStatus.Active).FirstOrDefault();
                    if (contact.IsNull())
                    {
                        contact = contactList.Where(a => a.IsShippingAddress == true && a.StatusID == (int)AddressStatus.Active).FirstOrDefault();
                    }
                }
                else
                {
                    contact = contactList.Where(a => a.IsShippingAddress == true && a.StatusID == (int)AddressStatus.Active).FirstOrDefault();
                }
            }

            if (contact.IsNotNull())
            {
                var contactDTO = ContactMapper.Mapper(Context).ToDTO(contact);
                var countryDetail = new CountryMasterBL().GetCountryDetail((long)contact.ContactIID);
                contactDTO.Country = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = countryDetail.CountryID.IsNotNull() ? countryDetail.CountryID.ToString() : null, Value = countryDetail.CountryNameEn.IsNotNull() ? countryDetail.CountryNameEn : null };

                if (contactDTO.AreaID.HasValue)
                {
                    var areaDTO = new MutualBL(Context).GetArea(contactDTO.AreaID.Value);
                    contactDTO.Areas = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = areaDTO.AreaID.ToString(), Value = areaDTO.AreaName };
                    var cityDTO = new MutualBL(Context).GetCity((int)contactDTO.CityID);
                    contactDTO.City = cityDTO.CityName;
                }

                if (contactDTO.CityID > 0)
                {
                    var cityDTO = new MutualBL(Context).GetCity((int)contactDTO.CityID);
                    contactDTO.City = cityDTO.CityName;
                }


                return contactDTO;
            }
            else
            {
                return null;
            }
        }

        public ContactDTO GetAddressByContactID(long contactID, long loginID)
        {
            var contactDTO = new ContactDTO();
            var contactList = accountRepo.GetContacts(loginID);
            var contact = contactList.Where(a => a.ContactIID == contactID).FirstOrDefault();
            if (contact.IsNotNull())
            {
                contactDTO = ContactMapper.Mapper(Context).ToDTO(contact);
                var countryDetail = new CountryMasterBL().GetCountryDetail((long)contact.ContactIID);
                contactDTO.Country = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = countryDetail.CountryID.IsNotNull() ? countryDetail.CountryID.ToString() : null, Value = countryDetail.CountryNameEn.IsNotNull() ? countryDetail.CountryNameEn : null };
                if (contactDTO.AreaID.HasValue)
                {
                    var areaDTO = new MutualBL(Context).GetArea((int)contactDTO.AreaID);
                    contactDTO.Areas = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = areaDTO.AreaID.ToString(), Value = areaDTO.AreaName };
                    var cityDTO = new MutualBL(Context).GetCity((int)contactDTO.CityID);
                    contactDTO.City = cityDTO.CityName;
                }

                if (contactDTO.CityID > 0)
                {
                    var cityDTO = new MutualBL(Context).GetCity((int)contactDTO.CityID);
                    contactDTO.City = cityDTO.CityName;
                }

                return contactDTO;
            }
            else
            {

                var userDetails = new AccountBL(this.Context).GetUserDetails(this.Context.EmailID, this.Context.MobileNumber);
                if (userDetails != null)
                {
                    contactDTO.FirstName = userDetails.Customer.FirstName;
                    contactDTO.LastName = userDetails.Customer.LastName;
                    contactDTO.MobileNo1 = userDetails.Customer.TelephoneNumber;
                    contactDTO.AddressLine1 = userDetails.CustomerAddress;
                }

                return contactDTO;
            }
        }

        public List<OrderContactMapDTO> GetShippingAddressDetail(long customerID)
        {
            List<OrderContactMapDTO> ocmDtos = new List<OrderContactMapDTO>();

            var orderContact = new AccountRepository().GetOrderContactMapsByCustomerID(customerID);

            if (orderContact.IsNotNull())
            {
                OrderContactMapDTO ocmDTO = new OrderContactMapDTO();
                ocmDTO = OrderContactMapMapper.Mapper(Context).ToDTO(orderContact);
            }
            else
            {
                var contacts = new AccountRepository().GetContactByCustomerID(customerID);

                foreach (var contact in contacts)
                {
                    OrderContactMapDTO ocmDTO = new OrderContactMapDTO();
                    if (contact.IsNotNull())
                    {
                        ocmDTO.ContactID = contact.ContactIID;
                        ocmDTO.FirstName = contact.FirstName;
                        ocmDTO.AddressName = contact.AddressName;
                        ocmDTO.MobileNo1 = contact.MobileNo1;
                        ocmDTO.PhoneNo1 = contact.PhoneNo1;
                        ocmDTO.AddressName = contact.AddressName;
                        ocmDTO.Block = contact.Block;
                        ocmDTO.MobileNo2 = contact.MobileNo2;
                        ocmDTO.PhoneNo1 = contact.PhoneNo1;
                        ocmDTO.Floor = contact.Floor;
                        ocmDTO.LandMark = contact.LandMark;
                        ocmDTO.BuildingNo = contact.BuildingNo;
                        ocmDTO.Street = contact.Street;
                        ocmDTO.Flat = contact.Flat;
                        ocmDTO.Avenue = contact.Avenue;
                        ocmDTO.District = contact.District;
                        ocmDTO.IsBillingAddress = contact.IsBillingAddress;
                        ocmDTO.IsShippingAddress = contact.IsShippingAddress;

                        var countryDetail = new CountryMasterBL().GetCountryDetail((long)contact.ContactIID);
                        ocmDTO.CountryID = contact.CountryID;
                        ocmDTO.CountryName = countryDetail.CountryNameEn.IsNotNull() ? countryDetail.CountryNameEn : null;

                        if (contact.AreaID.HasValue)
                        {
                            var areaDTO = new MutualBL(Context).GetArea((int)contact.AreaID);
                            ocmDTO.AreaID = contact.AreaID;
                            ocmDTO.AreaName = areaDTO.AreaName;

                            if (contact.CityID.IsNotNull())
                            {
                                var cityDTO = new MutualBL(Context).GetCity((int)contact.CityID);
                                ocmDTO.CityId = (int)contact.CityID;
                                ocmDTO.CityName = cityDTO.CityName;
                            }
                        }

                        if (contact.CityID > 0)
                        {
                            var cityDTO = new MutualBL(Context).GetCity((int)contact.CityID);
                            ocmDTO.CityId = (int)contact.CityID;
                            ocmDTO.CityName = cityDTO.CityName;
                        }

                    }

                    ocmDtos.Add(ocmDTO);
                }
            }

            return ocmDtos;
        }

        public bool CheckCustomerEmailIDAvailability(long loginID, string loginEmailID)
        {
            return new AccountRepository().CheckCustomerEmailIDAvailability(loginID, loginEmailID);
        }
        public bool CheckStudentEmailIDAvailability(long loginID, string EmailID)
        {
            return new AccountRepository().CheckStudentEmailIDAvailability(loginID, EmailID);
        }
        public Login GetLoginDetailByLoginID(long loginId)
        {
            return accountRepo.GetLoginDetailByLoginID(loginId);
        }

        public NotifyMeDTO NotifyMe(NotifyMeDTO dto)
        {
            dto.StatusID = accountRepo.NotifyMe(NotifyMeMapper.ToEntity(dto));
            switch (dto.StatusID)
            {
                case 1:
                    dto.StatusMessage = Resources.NotifyMeSucces;
                    break;
                case 2:
                    dto.StatusMessage = Resources.NotifyMeExists;
                    break;
                default:
                    dto.StatusMessage = Resources.NotifyMeFailure;
                    break;
            }
            return dto;
        }

        public long OnlineStoreAddContactContactID(ContactDTO contactDTO)
        {
            var contacts = MapContactsToDBEntity(new List<ContactDTO>() { contactDTO }, Context);
            //get cart login ID
            var loginID = Context.LoginID;

            if (!loginID.HasValue)
            {
                loginID = Context.LoginID.HasValue && Context.LoginID.Value != 0 ?
                    Context.LoginID.Value : new AccountRepository().GetLoginIDforOnlineStore(Context.EmailID);
            }

            contacts.First().LoginID = loginID;

            //var isGeolocationEnabled = new SettingBL(Context).GetSettingValue<bool>("DELIVERYRESTRICTEDTOGEOLOCATION",
            //               (long)this.Context.CompanyID);

            //if (isGeolocationEnabled && (string.IsNullOrEmpty(contactDTO.Longitude) || string.IsNullOrEmpty(contactDTO.Latitude)))
            //{
            //    throw new Exception("Please pick your address from the map.");
            //}
            //if (new SettingBL(null).GetSettingValue<bool>("AUTOSETBRANCHBYAREA") && contactDTO.AreaID.HasValue)
            //{
            //    var branchID = new CustomerBL(this.Context, _dbContext).UpdateUserDefaultBranchByLoginID(context.LoginID.Value, contactDTO.AreaID.Value);
            //    contacts.First().BranchID = branchID;
            //}

            return accountRepo.AddContactContactID(contacts.First());
        }

        public bool IsRoleExists(long loginId, int roleID)
        {
            var hasAccess = Framework.CacheManager.MemCacheManager<bool?>
                .Get("ISROLEEXISTS_" + loginId.ToString() + "_" + roleID.ToString());

            if (!hasAccess.HasValue)
            {
                hasAccess = accountRepo.IsRoleExists(loginId, roleID);
                Framework.CacheManager.MemCacheManager<bool?>
                    .Add(hasAccess, "ISROLEEXISTS_" + loginId.ToString() + "_" + roleID.ToString());
            }

            return hasAccess.Value;
        }
    }
}