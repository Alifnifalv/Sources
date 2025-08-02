using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Domain.Mappers
{
    public class OrderContactMapMapper : IDTOEntityMapper<OrderContactMapDTO, OrderContactMap>
    {
        private CallContext _context;

        public static OrderContactMapMapper Mapper(CallContext context)
        {
            var mapper = new OrderContactMapMapper();
            mapper._context = context;
            return mapper;
        }

        public OrderContactMapDTO ToDTO(OrderContactMap entity)
        {
            if (entity != null)
            {
                // Get Country
                var country = new Country();
                if (entity.CountryID > 0)
                {
                    country = new CountryMasterRepository().GetCountry((int)entity.CountryID);
                }
                //else country = null;

                // Get City
                var city = new CityDTO();
                if (entity.CityID > 0)
                {
                    city = new MutualBL(_context).GetCity(entity.CityID.Value);
                }
                //else city = null;

                // Get Area
                var area = new AreaDTO();
                if (entity.AreaID > 0)
                {
                    area = new MutualBL(_context).GetArea(entity.AreaID.Value);
                }
                //else area = null;


                return new OrderContactMapDTO()
                {
                    OrderContactMapID = entity.OrderContactMapIID,
                    OrderID = entity.OrderID,
                    ContactID = entity.ContactID,

                    CountryID = country.CountryID,
                    CountryName = country.CountryName,

                    CityId = city.CityID,
                    CityName = city.CityName,

                    AreaID = area.AreaID,
                    AreaName = area.AreaName,

                    TitleID = entity.TitleID,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    Description = entity.Description,
                    BuildingNo = entity.BuildingNo,
                    Floor = entity.Floor,
                    Flat = entity.Flat,
                    Avenue = entity.Avenue,
                    Block = entity.Block,
                    AddressName = entity.AddressName,
                    AddressLine1 = entity.AddressLine1,
                    AddressLine2 = entity.AddressLine2,
                    State = entity.State,
                    District = entity.District,
                    LandMark = entity.LandMark,
                    PostalCode = entity.PostalCode,
                    Street = entity.Street,
                    TelephoneCode = entity.TelephoneCode,
                    MobileNo1 = entity.MobileNo1,
                    MobileNo2 = entity.MobileNo2,
                    PhoneNo1 = entity.PhoneNo1,
                    PhoneNo2 = entity.PhoneNo2,
                    PassportNumber = entity.PassportNumber,
                    CivilIDNumber = entity.CivilIDNumber,
                    PassportIssueCountryID = entity.PassportIssueCountryID,
                    AlternateEmailID1 = entity.AlternateEmailID1,
                    AlternateEmailID2 = entity.AlternateEmailID2,
                    WebsiteURL1 = entity.WebsiteURL1,
                    WebsiteURL2 = entity.WebsiteURL2,
                    IsBillingAddress = entity.IsBillingAddress,
                    IsShippingAddress = entity.IsShippingAddress,
                    SpecialInstruction = entity.SpecialInstruction,

                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    CreatedBy = entity.CreatedBy,
                    ////TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
            else return new OrderContactMapDTO();
        }

        public OrderContactMap ToEntity(OrderContactMapDTO dto)
        {
            if (dto != null)
            {
                return new OrderContactMap()
                {
                    OrderContactMapIID = dto.OrderContactMapID,
                    OrderID = dto.OrderID,
                    TitleID = dto.TitleID,
                    ContactID = dto.ContactID,
                    AreaID = dto.AreaID,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    Description = dto.Description,
                    BuildingNo = dto.BuildingNo,
                    Floor = dto.Floor,
                    Flat = dto.Flat,
                    Avenue = dto.Avenue,
                    Block = dto.Block,
                    AddressName = dto.AddressName,
                    AddressLine1 = dto.AddressLine1,
                    AddressLine2 = dto.AddressLine2,
                    State = dto.State,
                    City = dto.City,
                    District = dto.District,
                    LandMark = dto.LandMark,
                    CountryID = dto.CountryID,
                    PostalCode = dto.PostalCode,
                    Street = dto.Street,
                    TelephoneCode = dto.TelephoneCode,
                    MobileNo1 = dto.MobileNo1,
                    MobileNo2 = dto.MobileNo2,
                    PhoneNo1 = dto.PhoneNo1,
                    PhoneNo2 = dto.PhoneNo2,
                    PassportNumber = dto.PassportNumber,
                    CivilIDNumber = dto.CivilIDNumber,
                    PassportIssueCountryID = dto.PassportIssueCountryID,
                    AlternateEmailID1 = dto.AlternateEmailID1,
                    AlternateEmailID2 = dto.AlternateEmailID2,
                    WebsiteURL1 = dto.WebsiteURL1,
                    WebsiteURL2 = dto.WebsiteURL2,
                    IsBillingAddress = dto.IsBillingAddress,
                    IsShippingAddress = dto.IsShippingAddress,
                    SpecialInstruction = dto.SpecialInstruction,
                    CityID = dto.CityId,

                    UpdatedDate = DateTime.Now,
                    UpdatedBy = (int)_context.LoginID,
                    CreatedDate = dto.OrderContactMapID == 0 ? DateTime.Now : dto.CreatedDate,
                    CreatedBy = dto.OrderContactMapID == 0 ? (int)_context.LoginID : dto.CreatedBy//,
                    ////TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps)
                };
            }        
            else return new OrderContactMap();
        }
    }
}
