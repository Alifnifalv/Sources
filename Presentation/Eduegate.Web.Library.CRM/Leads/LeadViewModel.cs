using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Services.Contracts.Leads;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.CRM.Leads
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Lead", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class LeadViewModel : BaseMasterViewModel
    {
        public LeadViewModel()
        {
            CommunicationDetails = new LeadCommunicationViewModel();
            ContactDetails = new LeadContactViewModel();
            EmailCommunication = new LeadEmailCommunicationViewModel();
            IsOrganization = false;
        }

        public long LeadIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("ReferenceCode")]
        public string LeadCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SchoolChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("School")]
        [LookUp("LookUps.School")]
        public string School { get; set; }
        public byte? SchoolIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Lead Name")]
        [CustomDisplay("LeadName")]
        public string LeadName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Lead Type")]
        [LookUp("LookUps.LeadType")]
        [CustomDisplay("LeadType")]
        public string LeadType { get; set; }
        public byte? LeadTypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("How did you hear about our school")]
        [CustomDisplay("Howdidyouhearaboutourschool")]
        [LookUp("LookUps.LeadSource")]
        public string LeadSource { get; set; }
        public int? LeadSourceID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Request Type")]
        [CustomDisplay("RequestType")]
        [LookUp("LookUps.CRMRequestType")]
        public string RequestType { get; set; }
        public byte? RequestTypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Industry Type")]
        [CustomDisplay("IndustryType")]
        [LookUp("LookUps.IndustryType")]
        public string IndustryType { get; set; }
        public int? IndustryTypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Market Segment")]
        [CustomDisplay("MarketSegment")]
        [LookUp("LookUps.MarketSegment")]
        public string MarketSegment { get; set; }
        public byte? MarketSegmentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("CRM Company")]
        [CustomDisplay("CRMCompany")]
        [LookUp("LookUps.CRMCompany")]
        public string CRMCompany { get; set; }
        public int? CompanyID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Lead Status")]
        [CustomDisplay("LeadStatus")]
        [LookUp("LookUps.LeadStatus")]
        public string LeadStatus { get; set; }
        public byte? LeadStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Is Organization")]
        [CustomDisplay("IsOrganization")]
        public bool? IsOrganization { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=!CRUDModel.ViewModel.IsOrganization")]
        //[DisplayName("Organization Name")]
        [CustomDisplay("OrganizationName")]
        public string OrgnanizationName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[DisplayName("Remarks")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes2 = "ng-disabled==(CRUDModel.ViewModel.LeadIID==0)", Attributes = "ng-click=MoveToApplication($event);")]
        //[DisplayName("Move To Application")]
        [CustomDisplay("MoveToApplication")]
        public string btnMoveToApplication { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        ////[DisplayName("Age Criteria Warning Msg")]
        //[CustomDisplay("AgeCriteriaWarningMsg")]
        //public string AgeCriteriaWarningMsg { get; set; }


        public byte? SchoolID { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "CommunicationDetails", "CommunicationDetails")]
        [CustomDisplay("Communication Details")]
        public LeadCommunicationViewModel CommunicationDetails { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmailCommunication", "EmailCommunication")]
        [CustomDisplay("Email Communication")]
        public LeadEmailCommunicationViewModel EmailCommunication { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ContactDetails", "ContactDetails")]
        [CustomDisplay("Contact Details")]
        public LeadContactViewModel ContactDetails { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LeadDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeadViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LeadDTO, LeadViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var ldDto = dto as LeadDTO;
            var vm = Mapper<LeadDTO, LeadViewModel>.Map(dto as LeadDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.LeadIID = ldDto.LeadIID;
            vm.LeadName = ldDto.LeadName;
            vm.LeadCode = ldDto.LeadCode;
            vm.OrgnanizationName = ldDto.OrgnanizationName;
            vm.IsOrganization = ldDto.IsOrganization;
            vm.Remarks = ldDto.Remarks;
            vm.School = ldDto.SchoolID.ToString();
            //vm.SchoolID = ldDto.SchoolID.HasValue ? ldDto.SchoolID : null;
            vm.LeadSource = ldDto.LeadSourceID.HasValue ? ldDto.LeadSourceID.ToString() : null;
            vm.LeadType = ldDto.LeadTypeID.HasValue ? ldDto.LeadTypeID.ToString() : null;
            vm.MarketSegment = ldDto.MarketSegmentID.HasValue ? ldDto.MarketSegmentID.ToString() : null;
            vm.IndustryType = ldDto.IndustryTypeID.HasValue ? ldDto.IndustryTypeID.ToString() : null;
            vm.RequestType = ldDto.RequestTypeID.HasValue ? ldDto.RequestTypeID.ToString() : null;
            vm.LeadStatus = ldDto.LeadStatusID.HasValue ? ldDto.LeadStatusID.ToString() : null;
            vm.CRMCompany = ldDto.CompanyID.HasValue ? ldDto.CompanyID.ToString() : null;

            vm.ContactDetails = new LeadContactViewModel()
            {
                ContactID = ldDto.ContactID,
                StudentName = ldDto.StudentName,
                ParentName = ldDto.ParentName,
                AcademicYear = ldDto.AcademicYearID.HasValue ? ldDto.AcademicYearID.ToString() : null,
                AcademicYearID = ldDto.AcademicYearID,
                EmailAddress = ldDto.EmailAddress,
                Gender = ldDto.GenderID.HasValue ? ldDto.GenderID.ToString() : null,
                ClassName = ldDto.ClassID.HasValue ? ldDto.ClassID.ToString() : null,
                Class = ldDto.ClassID.HasValue ? ldDto.ClassName : null,
                DateOfBirthString = ldDto.DateOfBirth.HasValue ? ldDto.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MobileNumber = ldDto.MobileNumber,
                CivilIDNumber = ldDto.LeadContact.CivilIDNumber,
                AddressName = ldDto.LeadContact.AddressName,
                Flat = ldDto.LeadContact.Flat,
                Block = ldDto.LeadContact.Block,
                TelephoneCode = ldDto.LeadContact.TelephoneCode,
                PhoneNo1 = ldDto.LeadContact.PhoneNo1,
                PhoneNo2 = ldDto.LeadContact.PhoneNo2,
                MobileNo1 = ldDto.LeadContact.MobileNo1,
                MobileNo2 = ldDto.LeadContact.MobileNo2,
                AlternateEmailID1 = ldDto.LeadContact.AlternateEmailID1,
                AlternateEmailID2 = ldDto.LeadContact.AlternateEmailID2,
                ClassID = ldDto.ClassID,
                GenderID = ldDto.GenderID,
                AcademicYearCode = ldDto.AcademicYear,
                DateOfBirth = ldDto.DateOfBirth,
                CurriculamID = ldDto.CurriculamID,
                CurriculamString = ldDto.CurriculamID.HasValue ? ldDto.CurriculamID.ToString() : null,
                Nationality = ldDto.NationalityID.HasValue ? new KeyValueViewModel() { Key = ldDto.Nationality.Key, Value = ldDto.Nationality.Value } : new KeyValueViewModel(),
            };

            vm.CommunicationDetails.CommunicationGrid = new List<LeadCommunicationGridViewModel>();
            foreach (var commGrid in ldDto.LeadCommunication)
            {
                vm.CommunicationDetails.CommunicationGrid.Add(new LeadCommunicationGridViewModel()
                {
                    CommunicationIID = commGrid.CommunicationIID,
                    CommunicationTypeID = commGrid.CommunicationTypeID,
                    CommunicationType = commGrid.CommunicationTypeID.HasValue ? commGrid.CommunicationType : null,
                    EmailTemplateID = commGrid.EmailTemplateID,
                    EmailTemplate = commGrid.EmailTemplateID.HasValue ? commGrid.EmailTemplate : null,
                    //EmailCC = commGrid.EmailCC,
                    Email = commGrid.EmailTemplateID.HasValue ? commGrid.Email : null,
                    EmailContent = commGrid.EmailContent,
                    MobileNumber = commGrid.EmailTemplateID == null ? commGrid.MobileNumber : null,
                    Notes = commGrid.Notes,
                    CommunicationDateString = commGrid.CommunicationDate.HasValue ? commGrid.CommunicationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    FollowUpDateString = commGrid.FollowUpDate.HasValue ? commGrid.FollowUpDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LeadViewModel, LeadDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<LeadCommunicationViewModel, LeadCommunicationDTO>.CreateMap();
            Mapper<LeadContactViewModel, LeadContactDTO>.CreateMap();
            var dto = Mapper<LeadViewModel, LeadDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.LeadIID = this.LeadIID;
            dto.LeadName = this.LeadName;
            dto.OrgnanizationName = this.OrgnanizationName;
            dto.IsOrganization = this.IsOrganization;
            dto.Remarks = this.Remarks;
            dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);
            dto.LeadSourceID = string.IsNullOrEmpty(this.LeadSource) ? (int?)null : int.Parse(this.LeadSource);
            dto.LeadTypeID = string.IsNullOrEmpty(this.LeadType) ? (byte?)null : byte.Parse(this.LeadType);
            dto.MarketSegmentID = string.IsNullOrEmpty(this.MarketSegment) ? (byte?)null : byte.Parse(this.MarketSegment);
            dto.IndustryTypeID = string.IsNullOrEmpty(this.IndustryType) ? (int?)null : int.Parse(this.IndustryType);
            dto.RequestTypeID = string.IsNullOrEmpty(this.RequestType) ? (byte?)null : byte.Parse(this.RequestType);
            dto.LeadStatusID = string.IsNullOrEmpty(this.LeadStatus) ? (byte?)null : byte.Parse(this.LeadStatus);
            dto.CompanyID = string.IsNullOrEmpty(this.CRMCompany) ? (byte?)null : byte.Parse(this.CRMCompany);

            dto.StudentName = this.ContactDetails.StudentName;
            dto.ParentName = this.ContactDetails.ParentName;
            dto.EmailAddress = this.ContactDetails.EmailAddress;
            dto.GenderID = string.IsNullOrEmpty(this.ContactDetails.Gender) ? (byte?)null : byte.Parse(this.ContactDetails.Gender);
            dto.AcademicYearID = string.IsNullOrEmpty(this.ContactDetails.AcademicYear) ? (int?)null : int.Parse(this.ContactDetails.AcademicYear);
            dto.ClassID = this.ContactDetails.ClassName == null || string.IsNullOrEmpty(this.ContactDetails.ClassName) ? (int?)null : int.Parse(this.ContactDetails.ClassName);
            dto.ContactID = this.ContactDetails.ContactID;
            dto.DateOfBirth = string.IsNullOrEmpty(this.ContactDetails.DateOfBirthString) ? (DateTime?)null : DateTime.ParseExact(this.ContactDetails.DateOfBirthString, dateFormat, CultureInfo.InvariantCulture);
            dto.MobileNumber = this.ContactDetails.MobileNumber;
            dto.CurriculamID = this.ContactDetails.CurriculamID.HasValue ? this.ContactDetails.CurriculamID : string.IsNullOrEmpty(this.ContactDetails.CurriculamString) || this.ContactDetails.CurriculamString == "?" ? (byte?)null : byte.Parse(this.ContactDetails.CurriculamString);
            dto.NationalityID = this.ContactDetails.Nationality == null || string.IsNullOrEmpty(this.ContactDetails.Nationality.Key) ? (int?)null : int.Parse(this.ContactDetails.Nationality.Key);

            dto.LeadEmailCommunication = new LeadCommunicationDTO()
            {
                //CommunicationIID = this.EmailCommunication.CommunicationIID,
                EmailTemplateID = string.IsNullOrEmpty(this.EmailCommunication.EmailTemplate) ? (int?)null : int.Parse(this.EmailCommunication.EmailTemplate),
                //EmailCC = this.CommunicationDetails.EmailCC,
                Email = this.ContactDetails.EmailAddress,
                EmailContent = this.EmailCommunication.EmailContent,
                IsSendEmail = this.EmailCommunication.IsSendEmail,
                Notes = this.EmailCommunication.Subject,
                //CommunicationDate = string.IsNullOrEmpty(this.CommunicationDetails.CommunicationDateString) ? (DateTime?)null : DateTime.ParseExact(this.CommunicationDetails.CommunicationDateString, dateFormat, CultureInfo.InvariantCulture),
                //FollowUpDate = string.IsNullOrEmpty(this.CommunicationDetails.FollowUpDateString) ? (DateTime?)null : DateTime.ParseExact(this.CommunicationDetails.FollowUpDateString, dateFormat, CultureInfo.InvariantCulture),
            };

            dto.LeadContact = new LeadContactDTO()
            {
                ContactIID = Convert.ToInt64(this.ContactDetails.ContactID),
                CivilIDNumber = this.ContactDetails.CivilIDNumber,
                AddressName = this.ContactDetails.AddressName,
                Flat = this.ContactDetails.Flat,
                Block = this.ContactDetails.Block,
                TelephoneCode = this.ContactDetails.TelephoneCode,
                PhoneNo1 = this.ContactDetails.PhoneNo1,
                PhoneNo2 = this.ContactDetails.PhoneNo2,
                MobileNo1 = this.ContactDetails.MobileNo1,
                MobileNo2 = this.ContactDetails.MobileNo2,
                AlternateEmailID1 = this.ContactDetails.AlternateEmailID1,
                AlternateEmailID2 = this.ContactDetails.AlternateEmailID2,
            };

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeadDTO>(jsonString);
        }
    }
}

