using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Security;

namespace Eduegate.Web.Library.ViewModels
{
    public class UserManagementViewModel
    {
        public decimal UserIID { get; set; }

        public Nullable<short> TitleIID { get; set; }

        public Nullable<int> RoleID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LoginEmailID { get; set; }

        public string CurrentEmailID { get; set; }

        public string Password { get; set; }

        public string CurrentPassword { get; set; }

        public string PasswordSalt { get; set; }

        public Nullable<decimal> StatusID { get; set; }

        public static UserManagementViewModel FromDTO(UserManagementDTO dto)
        {
            var vm = new UserManagementViewModel();

            if(dto.IsNotNull())
            {
                vm = new UserManagementViewModel()
                {
                    UserIID = dto.UserIID,
                    RoleID = dto.RoleID,
                    TitleIID = dto.TitleIID,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    LoginEmailID = dto.LoginEmailID,
                    CurrentEmailID = dto.LoginEmailID,
                    Password = dto.Password,
                    CurrentPassword = dto.Password,
                    PasswordSalt = dto.PasswordSalt,
                    StatusID = dto.StatusID
                };
            }

            return vm;
        }

        public static UserManagementDTO ToDTO(UserManagementViewModel vm)
        {
            UserManagementDTO dto = new UserManagementDTO();

            if(vm.IsNotNull())
            {
                dto.UserIID = vm.UserIID;
                dto.RoleID = (vm.RoleID.IsNotNull() && vm.RoleID > 0) ? vm.RoleID : (int)UserRole.ERP;
                dto.TitleIID = vm.TitleIID;
                dto.FirstName = vm.FirstName;
                dto.LastName = vm.LastName;
                dto.LoginEmailID = vm.LoginEmailID;
                dto.PasswordSalt = PasswordHash.CreateHash(vm.Password);
                dto.Password = StringCipher.Encrypt(vm.Password, dto.PasswordSalt);
                dto.StatusID = vm.StatusID;
            }

            return dto;
        }

    }
}
