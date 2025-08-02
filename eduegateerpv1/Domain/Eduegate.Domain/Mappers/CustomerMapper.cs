using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers
{
    public class CustomerMapper : IDTOEntityMapper<CustomerDTO, Customer>
    {
        private CallContext _context;

        public static CustomerMapper Mapper(CallContext context)
        {
            var mapper = new CustomerMapper();
            mapper._context = context;
            return mapper;
        }

        public CustomerDTO ToDTO(Customer entity)
        {
            if (entity != null)
            {
                return new CustomerDTO()
                {
                    CustomerIID = entity.CustomerIID,
                    LoginID = entity.LoginID,
                    TitleID = entity.TitleID.IsNotNull() ? short.Parse(entity.TitleID.ToString()) : (short?)null,
                    GroupID = Convert.ToInt32(entity.GroupID),
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    IsOfflineCustomer = entity.IsOfflineCustomer,
                    IsDifferentBillingAddress = entity.IsDifferentBillingAddress,
                    IsTermsAndConditions = entity.IsTermsAndConditions,
                    IsSubscribeOurNewsLetter = entity.IsSubscribeForNewsLetter,
                    StatusID = entity.StatusID,
                    CivilIDNumber = entity.CivilIDNumber,
                    PassportIssueCountryID = entity.PassportIssueCountryID,
                    CustomerCR = entity.CustomerCR,
                    CRExpiryDate = entity.CRExpiryDate,
                    PassportNumber = entity.PassportNumber,
                    CompanyID = entity.CompanyID
                    //ParentCustomerID = entity.ParentCustomerID,
                };
            }
            else return new CustomerDTO();
        }

        public Customer ToEntity(CustomerDTO dto)
        {
            throw new NotImplementedException();
        }

        public CustomerDTO FromContactToCustomerDTO(Contact entity)
        {
            if (entity.IsNotNull())
            {
                return new CustomerDTO()
                {
                    CustomerIID = entity.Customer.IsNotNull() ? entity.Customer.CustomerIID : 0,
                    LoginID = entity.LoginID,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    CustomerEmail = entity.Login.IsNotNull() ? entity.Login.LoginEmailID : string.Empty,
                    CustomerNumber = entity.MobileNo1,
                };
            }
            else
            {
                return new CustomerDTO();
            }
        }

        public CustomerDTO ToDTOReference(Customer entity)
        {
            if (entity.IsNotNull())
            {
                var customerDTO = ToDTO(entity);
                customerDTO.Settings = new Services.Contracts.Admin.CustomerSettingDTO();
                if(entity.CustomerSettings.IsNotNull())
                {
                    customerDTO.Settings.CurrentLoyaltyPoints = entity.CustomerSettings.FirstOrDefault().CurrentLoyaltyPoints;
                    customerDTO.Settings.TotalLoyaltyPoints = entity.CustomerSettings.FirstOrDefault().TotalLoyaltyPoints;
                    if(customerDTO.Settings.TotalLoyaltyPoints.HasValue && customerDTO.Settings.TotalLoyaltyPoints.Value > 0)
                    {
                        var customerGroup = new CustomerBL(this._context).GetCustomerGroup(customerDTO.Settings.TotalLoyaltyPoints.Value);
                        customerDTO.Settings.CustomerGroup = customerGroup.GroupName;
                    }
                }
                return customerDTO;
            }
            else
            {
                return new CustomerDTO();
            }
        }
    }


}
