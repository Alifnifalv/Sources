using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.School.Exams;
using Newtonsoft.Json;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerContacts", "CRUDModel.ViewModel")]
    [DisplayName("Contacts")]
    public class ContactsViewModel : BaseMasterViewModel
    {
        public ContactsViewModel()
        {
            //Phones = new List<PhoneViewModel>();
            //Emails = new List<EmailViewModel>();
            //Faxs = new List<FaxViewModel>();
            IsShippingAddress = true;
            IsBillingAddress = true;
        }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Contact ID")]
        public long ContactID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string FirstName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Title")]
        public string Title { get; set; }
        public string TitleID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Phone Number")]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        public string PhoneNo1 { get; set; }

        public string TelephoneCode { get; set; }

        [EmailAddress]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Email Address")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string AlternateEmailID1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is primary contact person")]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        public bool? IsPrimaryContactPerson { get; set; }

        public Nullable<long> LoginID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Supplier ID")]
        public Nullable<long> SupplierID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Customer ID")]
        public Nullable<long> CustomerID { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Middle Name")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string MiddleName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-init='CustomerLastNameChange(CRUDModel.ViewModel.LastName,CRUDModel.ViewModel.Contacts)'")]
        //[DisplayName("Last Name")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LastName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Notes")]
        //[MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string Description { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Building No")]
        //[MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string BuildingNo { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Floor")]
        //[MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string Floor { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Flat")]
        //[MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string Flat { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Block")]
        //[MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string Block { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Address 1")]
        //[MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        public string AddressName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Address 2")]
        //[MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        public string AddressLine1 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Address 3")]
        //[MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        public string AddressLine2 { get; set; }

        public string CountryID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, attribs: "ng-change='GetCityByCountryId($event, $element)'")]
        //[LookUp("LookUps.Countries")]
        //[DisplayName("Countries")]
        //[Select2("Country", "Numeric", false)]
        public KeyValueViewModel Country { get; set; }

        public string CityId { get; set; }

        //TODO: Need to check why this is not working
        //[RequiredIf("contactGridRow[{{$index}}].IsCityMandatory", true)]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[LookUp("LookUps.Cities")]
        //[DisplayName("City")]
        //[Select2("City", "Numeric", false)]
        public KeyValueViewModel Cities { get; set; }

        public string AreaID { get; set; }

        //TODO: Need to check why this is not working
        //[RequiredIf("contactGridRow[{{$index}}].IsAreaMandatory", true)]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[LookUp("LookUps.Areas")]
        //[DisplayName("Areas")]
        //[Select2("Areas", "Numeric", false)]
        public KeyValueViewModel Areas { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("District")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string District { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Landmark")]
        //[MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        public string Landmark { get; set; }

        //[MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Jadda/Avenue")]
        public string Avenue { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("State")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string State { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Post code")]
        //[MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        public string PostalCode { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Street")]
        //[MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string Street { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-crud-unique controllercall=" + "'Customer/CheckContactMobileAvailability?contactId={{contactGridRow.ContactID}}&mobileNumber={{contactGridRow.MobileNo1}}'" + " message=' already exist.'")]
        //[DisplayName("Mobile No 1")]
        //[MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        public string MobileNo1 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Mobile no 2")]
        //[MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        public string MobileNo2 { get; set; }


        public string PhoneNo2 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Passport Number")]
        //[MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        public string PassportNumber { get; set; }



        public long PassportIssueCountryID { get; set; }


        //[EmailAddress]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Alternative email 2")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string AlternateEmailID2 { get; set; }

        //[Url]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Web site 1")]
        //[MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string WebsiteURL1 { get; set; }

        //[Url]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Web site 2")]
        //[MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string WebsiteURL2 { get; set; }

        //[Required(ErrorMessage = "Status is required")]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("ContactStatus", "Numeric", false)]
        //[LookUp("LookUps.ContactStatus")]
        //[DisplayName("Status")]
        public KeyValueViewModel Status { get; set; }
        public string StatusID { get; set; }

        public string CountryName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Fax")]
        //public List<FaxViewModel> Faxs { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Phone")]
        //public List<PhoneViewModel> Phones { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Email")]
        //public List<EmailViewModel> Emails { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Is billing address")]
        public bool IsBillingAddress { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Is shipping address")]
        public bool IsShippingAddress { get; set; }

        //public string CountryName { get; set; }
        public string AreaName { get; set; }

        // public string AreaID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Hidden)]
        //[DisplayName("")]
        public bool IsAreaMandatory { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Hidden)]
        //[DisplayName("")]
        public bool IsCityMandatory { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ContactDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ContactsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ContactDTO, ContactsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var rmrksdto = dto as ContactDTO;
            var vm = Mapper<ContactDTO, ContactsViewModel>.Map(rmrksdto);

            vm.ContactID = rmrksdto.ContactID;
            vm.SupplierID = rmrksdto?.SupplierID;
            vm.FirstName = rmrksdto.FirstName;
            vm.Title = rmrksdto.Title;
            vm.PhoneNo1 = rmrksdto.PhoneNo1;
            vm.AlternateEmailID1 = rmrksdto.AlternateEmailID1;
            vm.IsPrimaryContactPerson = rmrksdto.IsPrimaryContactPerson;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ContactsViewModel, ContactDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<ContactsViewModel, ContactDTO>.Map(this);

            dto.ContactID = this.ContactID;
            dto.SupplierID = this.SupplierID;
            dto.FirstName = this.FirstName;
            dto.Title = this.Title;
            dto.PhoneNo1 = this.PhoneNo1;
            dto.AlternateEmailID1 = this.AlternateEmailID1;
            dto.IsPrimaryContactPerson = this.IsPrimaryContactPerson;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ContactDTO>(jsonString);
        }
    }
}