using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Mutual;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eduegate.Web.Library.ViewModels.Checkout
{
    public class ContactViewModel
    {
        public long ContactIID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> TitleID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        //[Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Description { get; set; }
        //[Required]
        [Display(Name = "Building No")]
        public string BuildingNo { get; set; }


        [Display(Name = "Floor")]
        public string Floor { get; set; }


        [Display(Name = "Flat No")]
        public string Flat { get; set; }

        //[Required]
        [Display(Name = "Block/PO Box No")]
        public string Block { get; set; }
        public string AddressName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        //[Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Country")]


        public ICollection<SelectListItem> Countries { get; set; }
        [Required(ErrorMessage = "Select Country")]
        public string SelectedCountry { get; set; }
        public Nullable<long> CountryID { get; set; }


        //[Display(Name = "Area")]
        public ICollection<SelectListItem> Areas { get; set; }

        public string SelectedArea { get; set; }

        public Nullable<int> AreaID { get; set; }


        [Display(Name = "City")]
        public ICollection<SelectListItem> Cities { get; set; }

        [Required(ErrorMessage = "Select City")]
        public string SelectedCity { get; set; }

        [Required(ErrorMessage = "Please Select City")]
        public int CityID { get; set; }


        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        //[Required]
        [Display(Name = "Street")]
        public string Street { get; set; }
        public string TelephoneCode { get; set; }

        [Required]
        [Display(Name = "MobileNo")]
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string PassportNumber { get; set; }
        public string CivilIDNumber { get; set; }
        public Nullable<long> PassportIssueCountryID { get; set; }
        public string AlternateEmailID1 { get; set; }
        public string AlternateEmailID2 { get; set; }
        public string WebsiteURL1 { get; set; }
        public string WebsiteURL2 { get; set; }
        public Nullable<bool> IsBillingAddress { get; set; }
        public Nullable<bool> IsShippingAddress { get; set; }

        public string District { get; set; }
        public string LandMark { get; set; }

        //[MinLength(3, ErrorMessage = "The {0} must be at least {2} characters long")]
        [Display(Name = "City")]
        public string IntlCity { get; set; }

        //[MinLength(3, ErrorMessage = "The {0} must be at least {2} characters long")]
        [Display(Name = "Area")]
        public string IntlArea { get; set; }

        [Display(Name = "Avenue/Jadda")]
        public string Avenue
        {
            get;
            set;
        }

        public static ContactViewModel ToViewModel(ContactDTO dto)
        {
            return new ContactViewModel()
            {
                ContactIID = dto.ContactID,
                LoginID = dto.LoginID,
                TitleID = dto.TitleID,
                SupplierID = dto.SupplierID,
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
                CountryID = long.Parse(dto.Country.Key),
                CountryName = dto.Country.Value,
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
                CityID = dto.CityID,
                District = dto.District,
                LandMark = dto.LandMark,
                IntlCity = dto.IntlCity,
                IntlArea = dto.IntlArea
                
            };
        }

        public static ContactDTO ToDTO(ContactViewModel viewModel)
        {
            return new ContactDTO()
            {
                ContactID = viewModel.ContactIID,
                LoginID = viewModel.LoginID,
                FirstName = viewModel.FirstName,
                MiddleName = viewModel.MiddleName,
                LastName = viewModel.LastName,
                Description = viewModel.Description,
                BuildingNo = viewModel.BuildingNo,
                Floor = viewModel.Floor,
                Flat = viewModel.Flat,
                Block = viewModel.Block,
                AddressName = viewModel.AddressName,
                AddressLine1 = viewModel.AddressLine1,
                AddressLine2 = viewModel.AddressLine2,
                State = viewModel.State,
                City = viewModel.City,
                CountryID = long.Parse(viewModel.SelectedCountry),
                PostalCode = viewModel.PostalCode,
                Street = viewModel.Street,
                TelephoneCode = viewModel.TelephoneCode,
                MobileNo1 = viewModel.MobileNo1,
                MobileNo2 = viewModel.MobileNo2,
                PhoneNo1 = viewModel.PhoneNo1,
                PhoneNo2 = viewModel.PhoneNo2,
                PassportNumber = viewModel.PassportNumber,
                CivilIDNumber = viewModel.CivilIDNumber,
                PassportIssueCountryID = viewModel.PassportIssueCountryID,
                AlternateEmailID1 = viewModel.AlternateEmailID1,
                AlternateEmailID2 = viewModel.AlternateEmailID2,
                WebsiteURL1 = viewModel.WebsiteURL1,
                WebsiteURL2 = viewModel.WebsiteURL2,
                IsBillingAddress = viewModel.IsBillingAddress,
                IsShippingAddress = viewModel.IsShippingAddress,
                AreaID = viewModel.AreaID,
                Avenue = viewModel.Avenue,
                CityID = viewModel.CityID,
                District = viewModel.District,
                LandMark  = viewModel.LandMark,
                IntlCity = viewModel.IntlCity,
                IntlArea = viewModel.IntlArea
            };
        }

        public void BindCountries(List<CountryDTO> countryDTOList)
        {
            Countries = new List<SelectListItem>();
            foreach (var countryDTO in countryDTOList)
            {
                var selectListItemCountry = new SelectListItem();
                selectListItemCountry.Text = countryDTO.CountryName;
                selectListItemCountry.Value = countryDTO.CountryID.ToString();
                Countries.Add(selectListItemCountry);
            }
        }

        public void BindCities(List<CityDTO> cityDTOList)
        {
            Cities = new List<SelectListItem>();
            foreach (var cityDTO in cityDTOList)
            {
                var selectListItemCity = new SelectListItem();
                selectListItemCity.Text = cityDTO.CityName;
                selectListItemCity.Value = cityDTO.CityID.ToString();
                Cities.Add(selectListItemCity);
            }
        }

        public static List<SelectListItem> BindAreas(List<AreaDTO> areaDTOList)
        {
            var Areas = new List<SelectListItem>();
            foreach (var areaDTO in areaDTOList)
            {
                var selectListItemArea = new SelectListItem();
                selectListItemArea.Text = areaDTO.AreaName;
                selectListItemArea.Value = areaDTO.AreaID.ToString();
                Areas.Add(selectListItemArea);
            }
            return Areas;
        }
    }
}
