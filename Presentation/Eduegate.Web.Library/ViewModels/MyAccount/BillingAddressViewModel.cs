using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Mutual;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eduegate.Web.Library.ViewModels.MyAccountViewModel
{
    public class BillingAddressViewModel
    {
        public long ContactIID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> TitleID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<long> CustomerID { get; set; }

        [Required]
        [Display(Name = "Building No")]
        public string BuildingNo { get; set; }

        [Display(Name = "Floor")]
        public string Floor { get; set; }

        [Display(Name = "Flat No")]
        public string Flat { get; set; }

        [Required]
        [Display(Name = "Block/PO Box No")]
        public string Block { get; set; }
        public string AddressName { get; set; }

        [Required]
        [Display(Name = "Country")]

        public ICollection<SelectListItem> Countries { get; set; }
        [Required(ErrorMessage = "Select Country")]
        public string SelectedCountry { get; set; }

        [Required(ErrorMessage = "Select Country")]
        public long CountryID { get; set; }
        public int StatusID { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Avenue")]
        public string Avenue
        {
            get;
            set;
        }
        public string MobileNo2 { get; set; }


        [Display(Name = "Area")]
        public ICollection<SelectListItem> Areas { get; set; }

        [Required(ErrorMessage = "Please Select Area")]
        public Nullable<int> AreaID { get; set; }

        [Required(ErrorMessage = "Select Area")]
        public string SelectedArea { get; set; }



        public BillingAddressViewModel()
        {
        }
        public BillingAddressViewModel(List<CountryDTO> countryDTOList)
        {
            Countries = BindCountries(countryDTOList);
            CountryID = 1;
            AreaID = 1;
            SelectedCountry = "1";
            SelectedArea = "";
            Areas = new List<SelectListItem>();
        }


        public static BillingAddressViewModel ToViewModel(ContactDTO dto, List<CountryDTO> countryDTOList, List<AreaDTO> areaDTOList)
        {
            return new BillingAddressViewModel()
            {
                ContactIID = dto.ContactID,
                LoginID = dto.LoginID,
                TitleID = dto.TitleID,
                SupplierID = dto.SupplierID,

                BuildingNo = dto.BuildingNo,
                Floor = dto.Floor,
                Flat = dto.Flat,
                Block = dto.Block,
                AddressName = dto.AddressName,
                StatusID = (int)Eduegate.Services.Contracts.Enums.ContactStatus.Active,
                CountryID = long.Parse(dto.Country.Key),
                CountryName = dto.Country.Value,
                PostalCode = dto.PostalCode,
                Street = dto.Street,
                MobileNo2 = dto.MobileNo2,
                Countries = BindCountries(countryDTOList),
                Areas = BindAreas(areaDTOList),
                AreaID = dto.Areas != null ? string.IsNullOrEmpty(dto.Areas.Key) ? 0 : int.Parse(dto.Areas.Key) : 0,
                Avenue = dto.Avenue,
                SelectedCountry = dto.Country.Key,
                SelectedArea = dto.Areas != null ? string.IsNullOrEmpty(dto.Areas.Key) ? "" : dto.Areas.Key : "",

            };
        }

        public static List<SelectListItem> BindCountries(List<CountryDTO> countryDTOList)
        {
            var Countries = new List<SelectListItem>();
            foreach (var countryDTO in countryDTOList)
            {
                var selectListItemCountry = new SelectListItem();
                selectListItemCountry.Text = countryDTO.CountryName;
                selectListItemCountry.Value = countryDTO.CountryID.ToString();
                Countries.Add(selectListItemCountry);
            }

            return Countries;
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

        public static ContactDTO ToDTO(BillingAddressViewModel viewModel)
        {
            return new ContactDTO()
            {
                ContactID = viewModel.ContactIID,
                LoginID = viewModel.LoginID,
                BuildingNo = viewModel.BuildingNo,
                Floor = viewModel.Floor,
                Flat = viewModel.Flat,
                Block = viewModel.Block,
                AddressName = viewModel.AddressName,
                MobileNo2 = viewModel.MobileNo2,
                StatusID = (int)Eduegate.Services.Contracts.Enums.ContactStatus.Active,
                CountryID = viewModel.CountryID,
                PostalCode = viewModel.PostalCode,
                Street = viewModel.Street,
                IsBillingAddress = true,
                AreaID = viewModel.AreaID,
                Avenue = viewModel.Avenue,
            };
        }

        //public static BillingAddressViewModel ToViewModelArea(BillingAddressViewModel viewModel, List<AreaDTO> areaDTOList)
        //{
        //    return new BillingAddressViewModel()
        //    {
        //        ContactIID = viewModel.ContactIID,
        //        LoginID = viewModel.LoginID,
        //        TitleID = viewModel.TitleID,
        //        SupplierID = viewModel.SupplierID,

        //        BuildingNo = viewModel.BuildingNo,
        //        Floor = viewModel.Floor,
        //        Flat = viewModel.Flat,
        //        Block = viewModel.Block,
        //        AddressName = viewModel.AddressName,

        //        CountryID = viewModel.ContactIID,
        //        CountryName = viewModel.CountryName,
        //        PostalCode = viewModel.PostalCode,
        //        Street = viewModel.Street,
        //        MobileNo2 = viewModel.MobileNo2,
        //        Countries = viewModel.Countries,
        //        Areas = BindAreas(areaDTOList),
        //        AreaID = dto.AreaID,
        //        Avenue = dto.Avenue,
        //    };
        //}

    }
}
