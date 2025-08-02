using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain
{
    public class UserManagementBL
    {
        public UserManagementDTO GetUserManagement(decimal userIID)
        {
            Login entity = new UserManagementRepository().GetUserManagement(userIID);
            return FromEntity(entity);
        }

        public bool SaveUserManagement(UserManagementDTO userManagementDTO)
        {
            bool result = new UserManagementRepository().SaveUserManagement(ToEntity(userManagementDTO));
            return result;
        }

        public static Login ToEntity(UserManagementDTO userManagementDTO)
        {
            var entity = new Login();
            entity.LoginRoleMaps = new List<LoginRoleMap>();
            entity.Customers = new List<Customer>();
            entity.Contacts = new List<Contact>();

            if (userManagementDTO.IsNotNull())
            {
                entity = new Login()
                {
                    LoginIID = Convert.ToInt64(userManagementDTO.UserIID),
                    LoginEmailID = userManagementDTO.LoginEmailID,
                    Password = userManagementDTO.Password,
                    PasswordSalt = userManagementDTO.PasswordSalt,
                    StatusID = Convert.ToByte(userManagementDTO.StatusID),
                };

                entity.LoginRoleMaps.Add(new LoginRoleMap()
                {
                    LoginID = Convert.ToInt64(userManagementDTO.UserIID),
                    RoleID = userManagementDTO.RoleID,
                });

                entity.Customers.Add(new Customer()
                {
                    LoginID = Convert.ToInt64(userManagementDTO.UserIID),
                    TitleID = Convert.ToInt16(userManagementDTO.TitleIID),
                    FirstName = userManagementDTO.FirstName,
                    LastName = userManagementDTO.LastName,
                });

                entity.Contacts.Add(new Contact()
                {
                    LoginID = Convert.ToInt64(userManagementDTO.UserIID),
                    TitleID = Convert.ToInt16(userManagementDTO.TitleIID),
                    FirstName = userManagementDTO.FirstName,
                    LastName = userManagementDTO.LastName,
                });
            }

            return entity;
        }

        public static UserManagementDTO FromEntity(Login entity)
        {
            UserManagementDTO dto = new UserManagementDTO();

            if(entity.IsNotNull())
            {
                dto.UserIID = entity.LoginIID;
                dto.LoginEmailID = entity.LoginEmailID;
                dto.Password = entity.Password;
                dto.StatusID = Convert.ToDecimal(entity.StatusID);
                dto.PasswordSalt = entity.PasswordSalt;

                var roleMap = entity.LoginRoleMaps.Where(x => x.LoginID == entity.LoginIID).FirstOrDefault();

                if (roleMap.IsNotNull())
                    dto.RoleID = roleMap.RoleID;

                var customer = entity.Customers.Where(y => y.LoginID == entity.LoginIID).FirstOrDefault();

                if(customer.IsNotNull())
                {
                    dto.TitleIID = customer.TitleID.HasValue ? (short?)short.Parse(customer.TitleID.ToString()) : null;
                    dto.FirstName = customer.FirstName;
                    dto.LastName = customer.LastName;
                }
            }

            return dto;
        }

    }
}
