using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Admin;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Web.Library.Common;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.Web.Library.ViewModels
{
    //TODO: Need to check "Exclude" error
    //[Bind(Exclude = "Status,Role")]
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "UsersDtails", "CRUDModel.ViewModel")]
    [DisplayName("User")]
    public class UserMasterViewModel : BaseMasterViewModel
    {
        public UserMasterViewModel()
        {
            Customer = new CustomerViewModel();
            //Contacts = new List<ContactsViewModel>();
            Supplier = new SupplierDetailViewModel();
            SecuritySettings = new Security.SecuritySettingsViewModel();
            Access = new List<KeyValueViewModel>();
            //Settings = new List<Common.GridSettingsViewModel>() { new Common.GridSettingsViewModel() };
            Employee = new EmployeeDetailViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("LoginID")]
        public string LoginID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("UserID")]
        public long UserID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("LoginUserID")]
        [MaxLength(50)]
        public string LoginUserID { get; set; }

        [Required]
        [EmailAddress]
        [ControlType(Framework.Enums.ControlTypes.TextBox,"", attribs: "ng-crud-unique controllercall=" + "'Login/CheckCustomerEmailIDAvailability?loginID={{CRUDModel.ViewModel.UserID}}&loginEmailID={{CRUDModel.ViewModel.LoginEmailID}}'" + " message=' already exist.'")]
        [CustomDisplay("LoginEmailID")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LoginEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("UserName")]
        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        public string UserName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Roles", "Numeric", true)]
        [LookUp("LookUps.Roles")]
        [CustomDisplay("Access")]
        public List<KeyValueViewModel> Access { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("SetPassword")]
        public bool IsRequired { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Password, attribs: "ng-disabled='!CRUDModel.ViewModel.IsRequired'")]
        [CustomDisplay("Password")]
        public string Password { get; set; }

        public Nullable<int> RegisteredCountryID { get; set; }
        public string RegisteredIP { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string RegisteredIPCountry { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Password Salt")]
        //public string PasswordSalt { get; set; }

        public byte StatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.LoginUserStatus")]
        [CustomDisplay("Status")]
        public string UserStatus { get; set; }

        public string LastLoginDate { get; set; }

        [Required]
        public string ProfileFile { get; set; }

        public string ProfileUrl { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("UserFile")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.UserProfile, "ProfileUrl", "")]
        public string ProfileUploadFile { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("School")]
        [LookUp("LookUps.School")]
        public string School { get; set; }
        public byte? SchoolID { get; set; }

        //public Nullable<int> Role {
        //    get { return string.IsNullOrEmpty(RoleID) ? null : (int?)(int.Parse(RoleID)); }
        //    set { RoleID = value.ToString(); } 
        //}

        //[ViewModelRole(UserRole.ERP)]
        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerDetail", "Customer")]
        //[DisplayName("Customer Info")]
        public CustomerViewModel Customer { get; set; }

        //[ViewModelRole(UserRole.ERP)]
        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierDetail", "Supplier")]
        //[DisplayName("Supplier Info")]
        public SupplierDetailViewModel Supplier { get; set; }

        [ViewModelRole(UserRole.ERP)]
        [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmployeeDetail", "Employee")]
        [CustomDisplay("EmployeeInfo")]
        public EmployeeDetailViewModel Employee { get; set; }

        //[ViewModelRole(UserRole.ERP)]
        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "LoginContacts", "Contacts")]
        //[DisplayName("Contacts")]
        //public List<ContactsViewModel> Contacts { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SecuritySettings", "SecuritySettings")]
        [CustomDisplay("Security")]
        public Security.SecuritySettingsViewModel SecuritySettings { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Settings")]
        //public List<Common.GridSettingsViewModel> Settings { get; set; }

        public static UserMasterViewModel FromDTO(UserDTO dto)
        {
            Mapper<UserDTO, UserMasterViewModel>.CreateMap();
            Mapper<CustomerDTO, CustomerViewModel>.CreateMap();
            //Mapper<ContactDTO, ContactsViewModel>.CreateMap();
            Mapper<SupplierDTO, SupplierDetailViewModel>.CreateMap();
            Mapper<EmployeeDTO, EmployeeDetailViewModel>.CreateMap();
            var vm = Mapper<UserDTO, UserMasterViewModel>.Map(dto);
            vm.Access = new List<KeyValueViewModel>();
            vm.UserStatus = dto.StatusID.ToString();
            vm.School = dto.SchoolID.HasValue ? dto.SchoolID.ToString() : null;
            vm.ProfileFile = vm.ProfileFile;
            //vm.ProfileUrl = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.UserProfile, vm.ProfileFile);
            vm.ProfileUrl = vm.ProfileFile;

            if (dto.Roles != null)
            {
                foreach (var role in dto.Roles)
                {
                    vm.Access.Add(new KeyValueViewModel() { Key = role.RoleID.ToString(), Value = role.RoleName });
                }
            }

            if (dto.ClaimSets != null)
            {
                vm.SecuritySettings.ClaimSets = KeyValueViewModel.FromDTO(dto.ClaimSets);
            }

            //if (dto.UserSettings != null)
            //{
            //    vm.Settings = GridSettingsViewModel.FromDTO(dto.UserSettings, string.IsNullOrEmpty(dto.LoginID) ? (long?)null : long.Parse(dto.LoginID));
            //}
            //else
            //{
            //    vm.Settings.Add(new GridSettingsViewModel());
            //}

            vm.Employee = new EmployeeDetailViewModel()
            {
                EmployeeIID = dto.EmployeeID,
                EmployeeName = dto.EmployeeName,
                EmployeeCode = dto.EmployeeCode,
            };

            return vm;
        }

        public static UserMasterViewModel FromDTO(UserDTO dto, List<KeyValueDTO> claimTypes)
        {
            var vm = FromDTO(dto);
            vm.SecuritySettings.Claims = Eduegate.Web.Library.ViewModels.Security.SecurityClaimViewModel.GetSecurityVM(claimTypes, dto.Claims);
            //vm.Settings = GridSettingsViewModel.GetMergedSettingVM(dto.UserSettings, long.Parse(dto.LoginID));
            return vm;
        }

        public static UserDTO ToDTO(UserMasterViewModel vm)
        {
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<UserMasterViewModel, UserDTO>.CreateMap();
            Mapper<CustomerViewModel, CustomerDTO>.CreateMap();
            Mapper<SupplierDetailViewModel, SupplierDTO>.CreateMap();
            Mapper<EmployeeDetailViewModel, EmployeeDTO>.CreateMap();
            //Mapper<ContactsViewModel, ContactDTO>.CreateMap();

            //foreach(var contacts in vm.Contacts)
            //{
            //    contacts.CreatedDate = null;
            //    contacts.UpdatedDate = null;
            //}

            var dto = Mapper<UserMasterViewModel, UserDTO>.Map(vm);
            dto.ProfileFile = System.IO.Path.GetFileName(vm.ProfileUrl);

            if (vm.SecuritySettings.ClaimSets != null)
            {
                dto.ClaimSets = KeyValueViewModel.ToDTO(vm.SecuritySettings.ClaimSets);
            }

            dto.Claims = new List<ClaimDetailDTO>();

            if (vm.SecuritySettings.Claims != null)
            {
                foreach(var securityClaim in vm.SecuritySettings.Claims)
                {
                    foreach (var claim in securityClaim.Claims)
                    {
                        dto.Claims.Add(new ClaimDetailDTO() { ClaimIID = long.Parse(claim.Key), ClaimName = claim.Value });
                    }
                }
            }

            if (vm.Access != null)
            {
                dto.Roles = new List<Services.Contracts.Admin.UserRoleDTO>();

                foreach (var role in vm.Access)
                {
                    dto.Roles.Add(new Services.Contracts.Admin.UserRoleDTO() { RoleID = int.Parse(role.Key), RoleName = role.Value });
                }
            }

            //if (vm.Settings != null)
            //{
            //    dto.UserSettings = new List<SettingDTO>();

            //    foreach (var setting in vm.Settings)
            //    {
            //        if (setting.SettingCode.Key.IsNotNullOrEmpty() && setting.SettingValue.Key.IsNotNullOrEmpty())
            //        {
            //            dto.UserSettings.Add(new SettingDTO()
            //            {
            //                Description = setting.Description,
            //                SettingCode = setting.SettingCode.Key,
            //                SettingValue = setting.SettingValue.Key,
            //            });
            //        }
            //    }
            //}

            if (vm.Employee != null)
            {
                dto.EmployeeID = vm.Employee.EmployeeIID;
            }

            dto.StatusID = byte.Parse(vm.UserStatus);  
            if (vm.School != null)
            {
                dto.SchoolID = byte.Parse(vm.School);
            }
            
            return dto;
        }
    }
}