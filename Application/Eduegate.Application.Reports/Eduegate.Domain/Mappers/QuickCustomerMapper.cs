using System;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Newtonsoft.Json;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Accounts;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Domain.Mappers
{
    public class QuickCustomerMapper : DTOEntityDynamicMapper
    {
        public static QuickCustomerMapper Mapper(CallContext context)
        {
            var mapper = new QuickCustomerMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CustomerDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            dto = dto as CustomerDTO;
            return JsonConvert.SerializeObject(dto);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CustomerDTO;
            var entity = ToEntity(toDto);
            var customerID = new CustomerRepository().SaveCustomer(entity);
            //save customer account map
            var mainAccountID = new SettingRepository().GetSettingDetail("RECEIVABLEGLMAINACC", _context.CompanyID.Value);
            if (mainAccountID != null && !string.IsNullOrEmpty(mainAccountID.SettingValue))
            {
                new AccountingRepository().AddOrUpdateSubAccount(AccountEntity(toDto, long.Parse(mainAccountID.SettingValue)), customerID.CustomerIID);
            }

            return GetEntity(entity.CustomerIID);
        }

        public Account AccountEntity(CustomerDTO dto, long mainAccountID)
        {
            return new Account()
            {
                AccountName = dto.FirstName,
                Alias = "",
                ParentAccountID = mainAccountID,
                AccountID = dto==null || dto.GLAccountID == null || long.Parse(dto.GLAccountID.Key) == 0 ? 0 : long.Parse(dto.GLAccountID.Key),
            };
        }

        public override string GetEntity(long IID)
        {
            var repository = new CustomerRepository();
            var entity = repository.GetCustomerV2(IID);
            var dto = ToDTO(entity);
            return ToDTOString(dto);
        }

        public CustomerDTO ToDTO(Customer entity)
        {
            if (entity != null)
            {
                var map = entity.CustomerAccountMaps.FirstOrDefault();
                return new CustomerDTO()
                {
                    CustomerIID = entity.CustomerIID,
                    FirstName = entity.FirstName,
                    StatusID = 1,
                    CustomerCR = entity.CustomerCR,
                    CompanyID = entity.CompanyID,
                    //TelephoneCode = entity.Telephone,
                    TelephoneNumber = entity.Telephone,
                    CustomerEmail = entity.CustomerEmail,
                    //ParentCustomerID = entity.ParentCustomerID,
                    GLAccountID = map == null ? null : new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                    {
                        Key = map.AccountID.ToString(),
                        Value = map.Account.AccountName
                    }
                };
            }
            else return new CustomerDTO();
        }

        public Customer ToEntity(CustomerDTO customer)
        {
            var entity = new Customer();
            var repository = new CustomerRepository();

            if (customer.CustomerIID != 0)
            {
                entity = repository.GetCustomerV2(customer.CustomerIID);
            }

            entity.CustomerIID = customer.CustomerIID;
            entity.FirstName = customer.FirstName;
            entity.CustomerCR = customer.CustomerCR;
            entity.Telephone = customer.TelephoneNumber;
            entity.CustomerEmail = customer.CustomerEmail;

            if (entity.CustomerIID == 0)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }
    }
}