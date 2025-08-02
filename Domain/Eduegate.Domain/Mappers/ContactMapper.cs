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
using Eduegate.Domain.Entity;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Security;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Newtonsoft.Json;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using Eduegate.Services.Contracts.Vendor;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Domain.Entity.School.Models.School;

namespace Eduegate.Domain.Mappers
{
    public class ContactMapper : DTOEntityDynamicMapper
    {
        public static ContactMapper Mapper(CallContext context)
        {
            var mapper = new ContactMapper();
            mapper._context = context;
            return mapper;
        }


        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ContactDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            var entity = new Contact();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                entity = dbContext.Contacts.FirstOrDefault(x => x.ContactIID == IID);
            }
            return ToDTOString(ToDTO(entity));
        }   


        public ContactDTO ToDTO(Contact entity)
        {
            // Get Country
            //var country = new Country();
            //if (entity.CountryID > 0)
            //{
            //    country = new CountryMasterRepository().GetCountry((int)entity.CountryID);
            //}

            //// Get City
            //var city = new CityDTO();
            //if (entity.CityID > 0)
            //{
            //    city = new MutualBL(_context).GetCity(entity.CityID.Value);
            //}

            //// Get Area
            //var area = new AreaDTO();
            //if (entity.AreaID > 0)
            //{
            //    area = new MutualBL(_context).GetArea(entity.AreaID.Value);
            //}

            if (entity != null)
            {
                return new ContactDTO()
                {
                    ContactID = entity.ContactIID,
                    SupplierID = entity.SupplierID, 
                    LoginID = entity.LoginID,
                    //TitleID = entity.TitleID.HasValue ? (short?)short.Parse(entity.TitleID.ToString()) : null,
                    //SupplierID = entity.SupplierID,
                    //CustomerID = entity.CustomerID,
                    FirstName = entity.FirstName,
                    //MiddleName = entity.MiddleName,
                    //LastName = entity.LastName,
                    //Description = entity.Description,
                    //BuildingNo = entity.BuildingNo,
                    //Floor = entity.Floor,
                    //Flat = entity.Flat,
                    //Block = entity.Block,
                    //AddressName = entity.AddressName,
                    //AddressLine1 = entity.AddressLine1,
                    //AddressLine2 = entity.AddressLine2,
                    //State = entity.State,
                    //PostalCode = entity.PostalCode,
                    //Street = entity.Street,
                    //TelephoneCode = entity.TelephoneCode,
                    //MobileNo1 = entity.MobileNo1,
                    //MobileNo2 = entity.MobileNo2,
                    PhoneNo1 = entity.PhoneNo1,
                    //PhoneNo2 = entity.PhoneNo2,
                    //PassportNumber = entity.PassportNumber,
                    //CivilIDNumber = entity.CivilIDNumber,
                    //PassportIssueCountryID = entity.PassportIssueCountryID,
                    AlternateEmailID1 = entity.AlternateEmailID1,
                    IsPrimaryContactPerson = entity.IsPrimaryContactPerson,
                    Title = entity.Title,
                    //AlternateEmailID2 = entity.AlternateEmailID2,
                    //WebsiteURL1 = entity.WebsiteURL1,
                    //WebsiteURL2 = entity.WebsiteURL2,
                    //IsBillingAddress = entity.IsBillingAddress,
                    //IsShippingAddress = entity.IsShippingAddress,
                    //Avenue = entity.Avenue,

                    //District = entity.District,
                    //LandMark = entity.LandMark,
                    //IntlArea = entity.IntlArea,
                    //IntlCity = entity.IntlCity,

                    //AreaID = Convert.ToInt32(area.AreaID),
                    //AreaName = area.AreaName,
                    //Areas = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = area.AreaID.ToString(), Value = area.AreaName },

                    //CityID = city.CityID,
                    //City = city.CityName,
                    //Cities = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = city.CityID.ToString(), Value = city.CityName },

                    //CountryID = country.CountryID,
                    //CountryName = country.CountryName,
                    //Country = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = country.CountryID.ToString(), Value = country.CountryName },
                };
            }
            else return new ContactDTO();
        }

        //From Vendor Portal Submit ContactPerson
        public string SubmitContactPerson(ContactDTO contactDTO)
        {
            var result = "Something went wrong";
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                //duplicate Validation 
                var contacts = db.Contacts
                                .AsNoTracking()
                                .FirstOrDefault(x => x.AlternateEmailID1 == contactDTO.AlternateEmailID1 && x.PhoneNo1 == contactDTO.PhoneNo1);

                if (contacts != null)
                {
                    result = "Contact person already exists";
                }
                else
                {
                    var returnIID = SaveEntity(contactDTO);

                    if (returnIID != null)
                    {
                        result = "Success";
                    }
                }
            }
            return result;
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ContactDTO;

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
               var entity = new Contact();

                entity = new Contact()
                {
                    ContactIID = toDto.ContactID,
                    LoginID = toDto.LoginID,
                    TitleID = toDto.TitleID,
                    SupplierID = toDto.SupplierID.HasValue ? toDto.SupplierID : _context.SupplierID,
                    CustomerID = toDto.CustomerID,
                    FirstName = toDto.FirstName,
                    MiddleName = toDto.MiddleName,
                    LastName = toDto.LastName,
                    Description = toDto.Description,
                    BuildingNo = toDto.BuildingNo,
                    Floor = toDto.Floor,
                    Flat = toDto.Flat,
                    Block = toDto.Block,
                    AddressName = toDto.AddressName,
                    AddressLine1 = toDto.AddressLine1,
                    AddressLine2 = toDto.AddressLine2,
                    State = toDto.State,
                    City = toDto.City,
                    CountryID = toDto.CountryID,
                    PostalCode = toDto.PostalCode,
                    Street = toDto.Street,
                    TelephoneCode = toDto.TelephoneCode,
                    MobileNo1 = toDto.MobileNo1,
                    MobileNo2 = toDto.MobileNo2,
                    PhoneNo1 = toDto.PhoneNo1,
                    PhoneNo2 = toDto.PhoneNo2,
                    PassportNumber = toDto.PassportNumber,
                    CivilIDNumber = toDto.CivilIDNumber,
                    PassportIssueCountryID = toDto.PassportIssueCountryID,
                    AlternateEmailID1 = toDto.AlternateEmailID1,
                    AlternateEmailID2 = toDto.AlternateEmailID2,
                    WebsiteURL1 = toDto.WebsiteURL1,
                    WebsiteURL2 = toDto.WebsiteURL2,
                    IsBillingAddress = toDto.IsBillingAddress,
                    IsShippingAddress = toDto.IsShippingAddress,
                    AreaID = toDto.AreaID,
                    Avenue = toDto.Avenue,
                    IntlArea = toDto.IntlArea,
                    IntlCity = toDto.IntlCity,
                    IsPrimaryContactPerson = toDto.IsPrimaryContactPerson,
                    Title = toDto.Title,
                };

                if (entity != null)
                {
                    if(entity.ContactIID != 0)
                    {
                        db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    db.SaveChanges();
                }

                return GetEntity(entity.ContactIID);
            }
        }


        public List<ContactDTO> GetVendorContactByLoginID(long loginID)
        {
            var listDto = new List<ContactDTO>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var contacts = db.Contacts.Where(x => x.LoginID == loginID).AsNoTracking().OrderByDescending(x => x.ContactIID).ToList();

                foreach (var cnct in contacts)
                {
                    if (contacts.Count > 0)
                    {
                        listDto.Add(new ContactDTO
                        {
                            ContactID = cnct.ContactIID,
                            SupplierID = cnct.SupplierID,
                            FirstName = cnct.FirstName,
                            Title = cnct.Title,
                            IsPrimaryContactPerson = cnct.IsPrimaryContactPerson,
                            PhoneNo1 = cnct.PhoneNo1,
                            AlternateEmailID1 = cnct.AlternateEmailID1,
                            LoginID = cnct.LoginID,
                        });
                    }
                }

            }

            return listDto;
        }
    }
}
