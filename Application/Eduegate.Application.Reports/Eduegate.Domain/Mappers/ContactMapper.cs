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
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers
{
    public class ContactMapper : IDTOEntityMapper<ContactDTO, Contact>
    {
        private CallContext _context;

        public static ContactMapper Mapper(CallContext context)
        {
            var mapper = new ContactMapper();
            mapper._context = context;
            return mapper;
        }

        public ContactDTO ToDTO(Contact entity)
        {
            // Get Country
            var country = new Country();
            if (entity.CountryID > 0)
            {
                country = new CountryMasterRepository().GetCountry((int)entity.CountryID);
            }

            // Get City
            var city = new CityDTO();
            if (entity.CityID > 0)
            {
                city = new MutualBL(_context).GetCity(entity.CityID.Value);
            }

            // Get Area
            var area = new AreaDTO();
            if (entity.AreaID > 0)
            {
                area = new MutualBL(_context).GetArea(entity.AreaID.Value);
            }

            if (entity != null)
            {
                return new ContactDTO()
                {
                    ContactID = entity.ContactIID,
                    LoginID = entity.LoginID,
                    TitleID = entity.TitleID.HasValue ? (short?)short.Parse(entity.TitleID.ToString()) : null,
                    SupplierID = entity.SupplierID,
                    CustomerID = entity.CustomerID,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    Description = entity.Description,
                    BuildingNo = entity.BuildingNo,
                    Floor = entity.Floor,
                    Flat = entity.Flat,
                    Block = entity.Block,
                    AddressName = entity.AddressName,
                    AddressLine1 = entity.AddressLine1,
                    AddressLine2 = entity.AddressLine2,
                    State = entity.State,
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
                    Avenue = entity.Avenue,

                    District = entity.District,
                    LandMark = entity.LandMark,
                    IntlArea = entity.IntlArea,
                    IntlCity = entity.IntlCity,

                    AreaID = Convert.ToInt32(area.AreaID),
                    AreaName = area.AreaName,
                    Areas = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = area.AreaID.ToString(), Value = area.AreaName },

                    CityID = city.CityID,
                    City = city.CityName,
                    Cities = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = city.CityID.ToString(), Value = city.CityName },

                    CountryID = country.CountryID,
                    CountryName = country.CountryName,
                    Country = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = country.CountryID.ToString(), Value = country.CountryName },
                };
            }
            else return new ContactDTO();
        }

        public Contact ToEntity(ContactDTO dto)
        {
            if (dto != null)
            {
                return new Contact()
                {
                    ContactIID = dto.ContactID,
                    LoginID = dto.LoginID,
                    TitleID = dto.TitleID,
                    SupplierID = dto.SupplierID,
                    CustomerID = dto.CustomerID,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    Description = dto.Description,
                    BuildingNo = dto.BuildingNo,
                    Floor = dto.Floor,
                    Flat = dto.Flat,
                    Block = dto.Block,
                    AddressName = dto.AddressName,
                    AddressLine1 = dto.AddressLine1,
                    AddressLine2 = dto.AddressLine2,
                    State = dto.State,
                    City = dto.City,
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
                    AreaID = dto.AreaID,
                    Avenue = dto.Avenue,
                    IntlArea = dto.IntlArea,
                    IntlCity = dto.IntlCity
                };
            }
            else return new Contact();
        }
    }
}
