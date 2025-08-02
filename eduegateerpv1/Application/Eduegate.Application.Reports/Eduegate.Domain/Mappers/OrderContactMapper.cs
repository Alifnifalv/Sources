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
    public class OrderContactMapper : IDTOEntityMapper<OrderContactMapDTO, OrderContactMap>
    {
        private CallContext _context;

        public static OrderContactMapper Mapper(CallContext context)
        {
            var mapper = new OrderContactMapper();
            mapper._context = context;
            return mapper;
        }

        public OrderContactMap ToEntity(OrderContactMapDTO dto)
        {
            if (dto.IsNotNull())
            {
                OrderContactMap vm = new OrderContactMap();

                vm.OrderContactMapIID = dto.OrderContactMapID;
                vm.OrderID = dto.OrderID;
                vm.TitleID = dto.TitleID;
                vm.FirstName = dto.FirstName;
                vm.MiddleName = dto.MiddleName;
                vm.LastName = dto.LastName;
                vm.Description = dto.Description;
                vm.BuildingNo = dto.BuildingNo;
                vm.Floor = dto.Floor;
                vm.Flat = dto.Flat;
                vm.Block = dto.Block;
                vm.AddressName = dto.AddressName;
                vm.AddressLine1 = dto.AddressLine1;
                vm.AddressLine2 = dto.AddressLine2;
                vm.State = dto.State;
                vm.City = dto.City;
                vm.CountryID = dto.CountryID;
                vm.PostalCode = dto.PostalCode;
                vm.Street = dto.Street;
                vm.TelephoneCode = dto.TelephoneCode;
                vm.MobileNo1 = dto.MobileNo1;
                vm.MobileNo2 = dto.MobileNo2;
                vm.PhoneNo1 = dto.PhoneNo1;
                vm.PhoneNo2 = dto.PhoneNo2;
                vm.PassportNumber = dto.PassportNumber;
                vm.CivilIDNumber = dto.CivilIDNumber;
                vm.PassportIssueCountryID = dto.PassportIssueCountryID;
                vm.AlternateEmailID1 = dto.AlternateEmailID1;
                vm.AlternateEmailID2 = dto.AlternateEmailID2;
                vm.WebsiteURL1 = dto.WebsiteURL1;
                vm.WebsiteURL2 = dto.WebsiteURL2;
                vm.IsBillingAddress = dto.IsBillingAddress;
                vm.IsShippingAddress = dto.IsShippingAddress;
                vm.SpecialInstruction = dto.SpecialInstruction;
                vm.CreatedBy = dto.OrderContactMapID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                vm.CreatedDate = dto.OrderContactMapID > 0 ? dto.CreatedDate : DateTime.Now;
                vm.UpdatedBy = dto.OrderContactMapID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                vm.UpdatedDate = dto.OrderContactMapID > 0 ? DateTime.Now : dto.UpdatedDate;
                //vm.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                return vm;
            }
            else
            {
                return new OrderContactMap();
            }
        }

        public OrderContactMapDTO ToDTO(OrderContactMap entity)
        {
            if (entity.IsNotNull())
            {
                OrderContactMapDTO dto = new OrderContactMapDTO();

                dto.OrderContactMapID = entity.OrderContactMapIID;
                dto.OrderID = entity.OrderID;
                dto.TitleID = entity.TitleID;
                dto.FirstName = entity.FirstName;
                dto.MiddleName = entity.MiddleName;
                dto.LastName = entity.LastName;
                dto.Description = entity.Description;
                dto.BuildingNo = entity.BuildingNo;
                dto.Floor = entity.Floor;
                dto.Flat = entity.Flat;
                dto.Block = entity.Block;
                dto.AddressName = entity.AddressName;
                dto.AddressLine1 = entity.AddressLine1;
                dto.AddressLine2 = entity.AddressLine2;
                dto.State = entity.State;
                dto.City = entity.City;
                dto.CountryID = entity.CountryID;
                dto.PostalCode = entity.PostalCode;
                dto.Street = entity.Street;
                dto.TelephoneCode = entity.TelephoneCode;
                dto.MobileNo1 = entity.MobileNo1;
                dto.MobileNo2 = entity.MobileNo2;
                dto.PhoneNo1 = entity.PhoneNo1;
                dto.PhoneNo2 = entity.PhoneNo2;
                dto.PassportNumber = entity.PassportNumber;
                dto.CivilIDNumber = entity.CivilIDNumber;
                dto.PassportIssueCountryID = entity.PassportIssueCountryID;
                dto.AlternateEmailID1 = entity.AlternateEmailID1;
                dto.AlternateEmailID2 = entity.AlternateEmailID2;
                dto.WebsiteURL1 = entity.WebsiteURL1;
                dto.WebsiteURL2 = entity.WebsiteURL2;
                dto.IsBillingAddress = entity.IsBillingAddress;
                dto.IsShippingAddress = entity.IsShippingAddress;
                dto.SpecialInstruction = entity.SpecialInstruction;
                dto.CreatedBy = entity.CreatedBy;
                dto.CreatedDate = entity.CreatedDate;
                dto.UpdatedBy = entity.UpdatedBy;
                dto.UpdatedDate = entity.UpdatedDate;
                //dto.TimeStamps = Convert.ToBase64String(entity.TimeStamps);

                return dto;
            }
            else
            {
                return new OrderContactMapDTO();
            }
        }

    }
}
