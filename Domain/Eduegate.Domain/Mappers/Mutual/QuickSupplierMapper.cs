using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Accounts;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Setting;

namespace Eduegate.Domain.Mappers.Mutual
{
    public class QuickSupplierMapper : DTOEntityDynamicMapper
    {
        public static QuickSupplierMapper Mapper(CallContext context)
        {
            var mapper = new QuickSupplierMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SupplierDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            dto = dto as SupplierDTO;
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SupplierDTO;
            var entity = ToEntity(toDto);
            var supplierID = new SupplierRepository().SaveSupplier(entity, _context.CompanyID);
            var mainAccountID = new SettingBL().GetSettingValue<long>("PAYABLEGLMAINACC", _context.CompanyID.Value, 0);
            new AccountingRepository().AddOrUpdateSubAccount(AccountEntity(toDto, mainAccountID), null, supplierID.SupplierIID);
            return GetEntity(entity.SupplierIID);
        }

        public Account AccountEntity(SupplierDTO dto, long mainAccountID)
        {
            return new Account()
            {
                AccountName = dto.FirstName,
                Alias = "",
                ParentAccountID = mainAccountID,
                AccountID = dto.GLAccountID == null || string.IsNullOrEmpty(dto.GLAccountID.Key) ? 0 : long.Parse(dto.GLAccountID.Key),
            };
        }

        public override string GetEntity(long IID)
        {
            var repository = new SupplierRepository();
            var entity = repository.GetSupplier(IID);
            var dto = ToDTO(entity);
            return ToDTOString(dto);
        }

        public SupplierDTO ToDTO(Supplier entity)
        {
            if (entity != null)
            {
                var map = entity.SupplierAccountMaps.FirstOrDefault();

                return new SupplierDTO()
                {
                    SupplierIID = entity.SupplierIID,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    StatusID = 1,
                    CompanyID = entity.CompanyID,
                    //TelephoneCode = entity.Telephone,
                    TelephoneNumber = entity.Telephone,
                    SupplierEmail = entity.SupplierEmail,
                    SupplierCode = entity.SupplierCode,
                    SupplierAddress = entity.SupplierAddress,
                    //ParentSupplierID = entity.ParentSupplierID,

                    GLAccountID = map == null ? null : new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                    {
                        Key = map.AccountID.ToString(),
                        Value = map.Account.AccountName
                    }
                };
            }
            else return new SupplierDTO();
        }

        public Supplier ToEntity(SupplierDTO Supplier)
        {
            var entity = new Supplier();
            var repository = new SupplierRepository();

            if (Supplier.SupplierIID != 0)
            {
                entity = repository.GetSupplier(Supplier.SupplierIID);
            }

            entity.SupplierIID = Supplier.SupplierIID;
            entity.FirstName = Supplier.FirstName;
            entity.MiddleName = Supplier.MiddleName;
            entity.LastName = Supplier.LastName;
            entity.Telephone = Supplier.TelephoneNumber;
            entity.SupplierEmail = Supplier.SupplierEmail;
            entity.SupplierCode = Supplier.SupplierCode;
            entity.SupplierAddress = Supplier.SupplierAddress;
            entity.CompanyID = _context.CompanyID;

            if (entity.SupplierIID == 0)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            else
            {
                entity.UpdatedBy = int.Parse(_context.LoginID.ToString());
                entity.UpdateDate = DateTime.Now;
            }

            return entity;
        }
    }
}
