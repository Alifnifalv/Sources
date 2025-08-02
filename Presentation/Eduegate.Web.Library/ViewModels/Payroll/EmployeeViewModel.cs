using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.Enums;
using System.Globalization;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.HR.Leaves;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Eduegate.Services.Contracts.Jobs;
using System.Linq;
using Eduegate.Infrastructure.Enums;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmployeeDetails", "CRUDModel.ViewModel")]
    [DisplayName("Official Details")]
    public class EmployeeViewModel : BaseMasterViewModel
    {
        public EmployeeViewModel()
        {
            IndianNationalityID = int.Parse(new Domain.Setting.SettingBL(null).GetSettingValue<string>("NATIONALITY_ID_INDIAN"));
            //Contacts = new List<ContactsViewModel>();
            Login = new LoginViewModel();
            EmployeeRoles = new List<KeyValueViewModel>();
            AdditionalInfo = new AdditionalDetailViewModel();
            AcademicDetails = new List<AcademicDetailsViewModel>() { new AcademicDetailsViewModel() };
            ExperienceDetails=new List<ExperienceDetailsViewModel> { new ExperienceDetailsViewModel() };
            PassportDetails = new EmployeePassportDetailViewModel();
            BankDetail = new EmployeeBankDetailViewModel();
            Payroll = new EmployeePayrollViewModel();
            ResignDetail = new ResignDetailViewModel();
            AirfareTicketDetail = new EmployeeAirfareTicketDetailsViewModel();
            LeaveTypes = new List<EmployeeLeaveTypeViewModel>() { new EmployeeLeaveTypeViewModel() };
            IsActive = true;
            IsListDisable = true;
            RelationDetails = new List<EmployeeRelationsDetailsViewModel>() { new EmployeeRelationsDetailsViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("Select from the JD Review List")]
        [DataPicker("JDAdvanceSearch")]
        public string ApplicantName { get; set; }
        public long? JobInterviewMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; } 

        public long EmployeeIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("EmployeeCode")]
        public string EmployeeCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.JobType")]
        [CustomDisplay("JobType")]
        public string JobType { get; set; }

        public Nullable<int> JobTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.EmployeeStatuses")]
        [CustomDisplay("Status")]
        public string Status { get; set; }

        public Nullable<int> StatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        public string EmployeeName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FirstName")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string FirstName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MiddleName")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string MiddleName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LastName")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string LastName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Alias")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string EmployeeAlias { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Gender")]
        [CustomDisplay("Gender")]
        public string Gender { get; set; }

        public Nullable<int> GenderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-change='getAgeByDOB($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("DateOfBirth")]
        public string BirthDateString { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Age")]
        public int? Age { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("EmployeePicture")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.EmployeePicture, "ProfileUrl", "")]
        public string ProfileUrl { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("EmployeeSignature")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Social, "SignatureUrl", "")]
        public string SignatureUrl { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("PresentAddress")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string PresentAddress { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("PermenantAddress")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string PermenentAddress { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("BloodGroup")]
        [LookUp("LookUps.BloodGroup")]
        public string BloodGroup { get; set; }
        public int? BloodGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("EmergencyContactNo")]
        public string EmergencyContactNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Nationality", "String", false)]
        [LookUp("LookUps.Nationality")]
        [CustomDisplay("Nationality")]
        public KeyValueViewModel Nationality { get; set; }
        //public int? NationalityID { get; set; }

        public int? IndianNationalityID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = CRUDModel.ViewModel.Nationality.Key!=CRUDModel.ViewModel.IndianNationalityID")]
        [CustomDisplay("Adhaar No")]
        [MaxLength(12, ErrorMessage = "Max 12 characters"), MinLength(12, ErrorMessage = "Min 12 characters")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string AdhaarCardNo { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='RelegionChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("Religion")]
        [LookUp("LookUps.Relegion")]
        public string Relegion { get; set; }
        public int? RelegionID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Caste")]
        [LookUp("LookUps.Cast")]
        public string Cast { get; set; }
        public int? CastID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Community")]
        [LookUp("LookUps.Community")]
        public string Community { get; set; }
        public byte? CommunityID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.MaritalStatus")]
        [CustomDisplay("MaritalStatus")]
        public string MaritalStatus { get; set; }

        public Nullable<int> MaritalStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("WorkPhoneNo")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        public string WorkPhone { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MobileNo.")]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        public string WorkMobileNo { get; set; }

        [EmailAddress]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("WorkEmail")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string WorkEmail { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Roles")]
        [Select2("EmployeeRole", "Numeric", true)]
        [LookUp("LookUps.EmployeeRole")]
        public List<KeyValueViewModel> EmployeeRoles { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Branch")]
        [CustomDisplay("Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public string Branch { get; set; }

        public Nullable<long> BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Designation")]
        [CustomDisplay("Designation")]
        public string Designation { get; set; }

        public Nullable<int> DesignationID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.EmployeeGrades")]
        [CustomDisplay("Grades")]
        public string Grade { get; set; }

        public Nullable<int> GradeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled = CRUDModel.ViewModel.Designation!=10")]
        [LookUp("LookUps.LicenseType")]
        [CustomDisplay("LicenseType")]
        public string LicenseType { get; set; }

        public byte? LicenseTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = CRUDModel.ViewModel.Designation!=10")]
        [CustomDisplay("LicenseNumber")]
        public string LicenseNumber { get; set; }

        public string EmployeePhoto { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateOfJoining")]
        public string JoiningDateString { get; set; }
        public Nullable<System.DateTime> DateOfJoining { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateofConfirmation")]
        public string ConfirmDateString { get; set; }
        public DateTime? ConfirmDate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Department")]
        [CustomDisplay("Department")]
        public string Department { get; set; }
        public Nullable<long> DepartmentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Employee", "String", false)]
        [CustomDisplay("ReportingTo")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel ReportingEmployee { get; set; }
        public Nullable<long> ReportingEmployeeID { get; set; }


        public bool IsListDisable { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='LeaveGroupChanges($event, $element,CRUDModel.ViewModel)'")]
        [LookUp("LookUps.LeaveGroup")]
        [CustomDisplay("LeaveGroup")]
        public string LeaveGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", "ng-change=LeaveGroupChanges($event, $element,CRUDModel.ViewModel)")]
        [CustomDisplay("Override Leave Group")]
        public bool? IsOverrideLeaveGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "")]
        [CustomDisplay("CBSEID")]
        [MaxLength(15, ErrorMessage = "Max 15 characters")]
        //[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        public string CBSEID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled = CRUDModel.ViewModel.IsActive")]
        [CustomDisplay("InActiveDate")]
        public string InActiveDateString { get; set; }
        public Nullable<System.DateTime> InActiveDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]//, attribs:" ng-disabled=gridModel.IsListDisable"
        [CustomDisplay("Leave Types")]
        public List<EmployeeLeaveTypeViewModel> LeaveTypes { get; set; }


        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "EmployeeContacts", "Contacts")]
        //[DisplayName("Contacts")]
        //public List<ContactsViewModel> Contacts { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "PassportDetails", "PassportDetails")]
        [CustomDisplay("EmployeeOtherDetails")]
        public EmployeePassportDetailViewModel PassportDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("SpouseAndChildrenDetails")]
        public List<EmployeeRelationsDetailsViewModel> RelationDetails { get; set; }


        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "LeaveDetails", "LeaveDetails")]
        //[CustomDisplay("Employee Leave Details")]
        //public EmployeeLeaveDetailsViewModel LeaveDetails { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "Payroll", "Payroll")]
        [CustomDisplay("Payroll")]
        public EmployeePayrollViewModel Payroll { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "BankDetail", "BankDetail")]
        [CustomDisplay("BankDetails")]
        public EmployeeBankDetailViewModel BankDetail { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmployeeLogin", "Login")]
        [CustomDisplay("LoginInfo")]
        public LoginViewModel Login { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AdditionalInfo", "AdditionalInfo")]
        [CustomDisplay("AdditionalInfo")]
        public AdditionalDetailViewModel AdditionalInfo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("AcademicDetails")]
        public List<AcademicDetailsViewModel> AcademicDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("ExperienceDetails")]
        public List<ExperienceDetailsViewModel> ExperienceDetails { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ResignDetail", "ResignDetail")]
        [CustomDisplay("ResignDetail")]
        public ResignDetailViewModel ResignDetail { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AirfareTicketDetail", "AirfareTicketDetail")]
        [CustomDisplay("Airfare Ticket Details")]
        public EmployeeAirfareTicketDetailsViewModel AirfareTicketDetail { get; set; }

        public static List<EmployeeViewModel> FromDTO(List<EmployeeDTO> dtos)
        {
            var vms = new List<EmployeeViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static EmployeeViewModel FromDTO(EmployeeDTO dto)
        {
            Mapper<EmployeeDTO, EmployeeViewModel>.CreateMap();
            //Mapper<ContactDTO, ContactsViewModel>.CreateMap();
            Mapper<LoginDTO, LoginViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<EmployeeAdditionalInfoDTO, AdditionalDetailViewModel>.CreateMap();
            Mapper<EmployeeAirFareDTO, EmployeeAirfareTicketDetailsViewModel>.CreateMap();
            //Mapper<EmployeesAdditionalInfoDTO, AcademicDetailsViewModel> CreateMap();
            Mapper<PassportVisaDetailDTO, EmployeePassportDetailViewModel>.CreateMap();
            Mapper<EmployeePayrollDTO, EmployeePayrollViewModel>.CreateMap();
            Mapper<EmployeeBankDetailDTO, EmployeeBankDetailViewModel>.CreateMap();
            Mapper<EmployeeLeaveAllocationDTO, EmployeeLeaveTypeViewModel>.CreateMap();
            Mapper<EmployeeRelationsDetailDTO, EmployeeRelationsDetailsViewModel>.CreateMap();
            Mapper<EmployeeAcademicQualificationDTO, AcademicDetailsViewModel>.CreateMap();
            Mapper<EmployeeExperienceDTO, ExperienceDetailsViewModel>.CreateMap();

            var mapper = Mapper<EmployeeDTO, EmployeeViewModel>.Map(dto);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            mapper.JoiningDateString = dto.DateOfJoining.HasValue ? dto.DateOfJoining.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            mapper.ConfirmDateString = dto.ConfirmationDate.HasValue ? dto.ConfirmationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            mapper.BirthDateString = dto.DateOfBirth.HasValue ? dto.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            mapper.Designation = dto.DesignationID.HasValue ? dto.DesignationID.ToString() : null;
            mapper.Branch = dto.BranchID.HasValue ? dto.BranchID.ToString() : null;
            mapper.Department = dto.DepartmentID.HasValue ? dto.DepartmentID.Value.ToString() : string.Empty;
            //mapper.Designation = dto.DesignationID.HasValue ? dto.DesignationID.ToString() : null;
            mapper.Gender = dto.GenderID.HasValue ? dto.GenderID.ToString() : null;
            mapper.BloodGroup = dto.BloodGroupID.HasValue ? dto.BloodGroupID.ToString() : null;
            mapper.JobType = dto.JobTypeID.HasValue ? dto.JobTypeID.ToString() : null;
            mapper.Cast = dto.CastID.HasValue ? dto.CastID.ToString() : null;
            mapper.Relegion = dto.RelegionID.HasValue ? dto.RelegionID.ToString() : null;
            mapper.Community = dto.CommunityID.HasValue ? dto.CommunityID.ToString() : null;
            mapper.EmergencyContactNo = dto.EmergencyContactNo;
            //mapper.Nationality = dto.NationalityID.ToString();
            mapper.Nationality = dto.NationalityID.HasValue ? new KeyValueViewModel()
            {
                Key = dto.NationalityID.ToString(),
                Value = dto.NationalityName
            } : new KeyValueViewModel();
            mapper.MaritalStatus = dto.MaritalStatusID.HasValue ? dto.MaritalStatusID.ToString() : null;
            mapper.LicenseType = dto.LicenseTypeID.HasValue ? dto.LicenseTypeID.ToString() : null;
            //mapper.Category = dto.CategoryID.ToString();
            mapper.IsActive = dto.IsActive;
            mapper.ReportingEmployee = dto.ReportingEmployeeID.HasValue ? new KeyValueViewModel()
            {
                Key = dto.ReportingEmployeeID.ToString(),
                Value = dto.ReportingEmployeeName
            } : new KeyValueViewModel();
            //mapper.PictureUrl = string.Format("{0}//{1}//{2}",
            //     new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), EduegateImageTypes.EmployeePicture.ToString(), mapper.EmployeePhoto);
            mapper.SignatureUrl = dto.SignatureContentID.HasValue ? dto.SignatureContentID.ToString() : null;
            mapper.ProfileUrl = dto.EmployeePhoto;
            mapper.CBSEID = dto.CBSEID;
            mapper.InActiveDateString = dto.InActiveDate.HasValue ? dto.InActiveDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            mapper.Grade = dto.Grade.HasValue ? dto.Grade.ToString() : null;
            mapper.Status = dto.StatusID.HasValue ? dto.StatusID.ToString() : null;


            //Resignation Details
            mapper.ResignDetail.LeavingType = dto.LeavingTypeID.HasValue ? dto.LeavingTypeID.ToString() : null;
            mapper.ResignDetail.LastDateString = dto.LastWorkingDate.HasValue ? Convert.ToDateTime(dto.LastWorkingDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            mapper.ResignDetail.ResignationDateString = dto.ResignationDate.HasValue ? Convert.ToDateTime(dto.ResignationDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            mapper.AirfareTicketDetail = new EmployeeAirfareTicketDetailsViewModel();
            mapper.AirfareTicketDetail.TicketEligibleFromDateString = dto.AirFareInfo.TicketEligibleFromDate.HasValue ? Convert.ToDateTime(dto.AirFareInfo.TicketEligibleFromDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            mapper.AirfareTicketDetail.EmployeeCountryAirportID = dto.AirFareInfo.EmployeeCountryAirportID;
            mapper.AirfareTicketDetail.EmployeeCountryAirport = dto.AirFareInfo.EmployeeCountryAirportID.HasValue ? KeyValueViewModel.ToViewModel(dto.AirFareInfo.EmployeeCountryAirport) : new KeyValueViewModel();
            mapper.AirfareTicketDetail.ISTicketEligible = dto.AirFareInfo.ISTicketEligible;
            mapper.AirfareTicketDetail.EmployeeNearestAirportID = dto.AirFareInfo.EmployeeNearestAirportID;
            mapper.AirfareTicketDetail.EmployeeNearestAirport = dto.AirFareInfo.EmployeeNearestAirportID.HasValue ? KeyValueViewModel.ToViewModel(dto.AirFareInfo.EmployeeNearestAirport) : new KeyValueViewModel();

            mapper.AirfareTicketDetail.TicketEntitilementID = dto.AirFareInfo.TicketEntitilementID;
            mapper.AirfareTicketDetail.TicketEntitilement= dto.AirFareInfo.TicketEntitilementID.HasValue ? KeyValueViewModel.ToViewModel(dto.AirFareInfo.TicketEntitilement) : new KeyValueViewModel();
            mapper.AirfareTicketDetail.GenerateTravelSector = dto.AirFareInfo.GenerateTravelSector;
            mapper.AirfareTicketDetail.LastTicketGivenDate= dto.AirFareInfo.LastTicketGivenDate;
            mapper.AirfareTicketDetail.LastTicketgivenString= dto.AirFareInfo.LastTicketGivenDate.HasValue ? Convert.ToDateTime(dto.AirFareInfo.LastTicketGivenDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            mapper.AirfareTicketDetail.IsTwoWay = dto.AirFareInfo.IsTwoWay;
            mapper.AirfareTicketDetail.FlightClass = dto.AirFareInfo.FlightClassID.HasValue ? KeyValueViewModel.ToViewModel(dto.AirFareInfo.FlightClass) : new KeyValueViewModel();
            mapper.AirfareTicketDetail.FlightClassID= dto.AirFareInfo.FlightClassID;

            mapper.Login.Status = dto.Login.StatusID.HasValue ? new KeyValueViewModel()
            {
                Key = ((int)dto.Login.StatusID.Value).ToString(),
                Value = dto.Login.StatusID.Value.ToString()
            } : new KeyValueViewModel();

            mapper.PassportDetails = new EmployeePassportDetailViewModel()
            {
                CountryofIssue = dto.PassportVisaInfo.CountryofIssueID.HasValue ? new KeyValueViewModel()
                {
                    Key = dto.PassportVisaInfo.CountryofIssueID.ToString(),
                    Value = dto.PassportVisaInfo.CountryofIssueName
                } : new KeyValueViewModel(),
                //CountryofIssue = dto.PassportVisaInfo.CountryofIssueID.HasValue ? dto.PassportVisaInfo.CountryofIssueID.ToString() : null,
                PassportNo = dto.PassportVisaInfo.PassportNo != null ? dto.PassportVisaInfo.PassportNo : null,
                PassportExpiryDateString = dto.PassportVisaInfo.PassportNoExpiry.HasValue ? Convert.ToDateTime(dto.PassportVisaInfo.PassportNoExpiry).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                PassportVisaIID = dto.PassportVisaInfo.PassportVisaIID,
                PlaceOfIssue = dto.PassportVisaInfo.PlaceOfIssue != null ? dto.PassportVisaInfo.PlaceOfIssue : null,
                NationalIDNo = dto.PassportVisaInfo.NationalIDNo != null ? dto.PassportVisaInfo.NationalIDNo : null,
                NationalIDNoExpiryString = dto.PassportVisaInfo.NationalIDNoExpiry.HasValue ? Convert.ToDateTime(dto.PassportVisaInfo.NationalIDNoExpiry).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                UserType = dto.PassportVisaInfo.UserType != null ? dto.PassportVisaInfo.UserType : null,
                VisaNo = dto.PassportVisaInfo.VisaNo != null ? dto.PassportVisaInfo.VisaNo : null,
                Sponsor = dto.PassportVisaInfo.SponsorID.HasValue ? dto.PassportVisaInfo.SponsorID.ToString() : null,
                AccomodationType = dto.AccomodationTypeID.HasValue ? dto.AccomodationTypeID.ToString() : null,
                PassageType = dto.PassageTypeID.HasValue ? dto.PassageTypeID.ToString() : null,
                MOIID = dto.PassportVisaInfo.MOIID != null ? dto.PassportVisaInfo.MOIID : null,
                HealthCardNo = dto.PassportVisaInfo.HealthCardNo != null ? dto.PassportVisaInfo.HealthCardNo : null,
                LabourCardNo = dto.PassportVisaInfo.LabourCardNo != null ? dto.PassportVisaInfo.LabourCardNo : null,
                VisaExpiryString = dto.PassportVisaInfo.VisaExpiry.HasValue ? Convert.ToDateTime(dto.PassportVisaInfo.VisaExpiry).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
            };

            mapper.RelationDetails = new List<EmployeeRelationsDetailsViewModel>();

            if (dto.RelationDetails != null)
            {
                foreach (var details in dto.RelationDetails)
                {
                    if (details.EmployeeRelationType != null && !string.IsNullOrEmpty(details.EmployeeRelationType.Key))
                    {
                        mapper.RelationDetails.Add(new EmployeeRelationsDetailsViewModel()
                        {
                            EmployeeRelationsDetailIID = details.EmployeeRelationsDetailIID,
                            FirstName = details.FirstName,
                            MiddleName = details.MiddleName,
                            LastName = details.LastName,
                            NationalID = details.NationalIDNo,
                            PassportNumber = details.PassportNo,
                            ContactNumber = details.ContactNo,
                            EmployeeRelationTypesID = (int?)details.EmployeeRelationTypeID,
                            EmployeeRelationTypes = details.EmployeeRelationTypeID.HasValue ? KeyValueViewModel.ToViewModel(details.EmployeeRelationType) : new KeyValueViewModel(),
                        });
                    }
                }
            };

            mapper.LeaveGroup = dto.LeaveGroupID.HasValue ? dto.LeaveGroupID.ToString() : null;
            mapper.LeaveTypes = new List<EmployeeLeaveTypeViewModel>();

            if (dto.EmployeeLeaveAllocationInfo != null)
            {
                foreach (var leaveInfo in dto.EmployeeLeaveAllocationInfo)
                {
                    if (leaveInfo.LeaveType != null && !string.IsNullOrEmpty(leaveInfo.LeaveType.Key))
                    {
                        mapper.LeaveTypes.Add(new EmployeeLeaveTypeViewModel()
                        {
                            LeaveAllocationIID = leaveInfo.LeaveAllocationIID,
                            LeaveTypeID = leaveInfo.LeaveTypeID,
                            AllocatedLeaves = (decimal?)leaveInfo.AllocatedLeaves,
                            LeaveType = leaveInfo.LeaveTypeID.HasValue ? KeyValueViewModel.ToViewModel(leaveInfo.LeaveType) : new KeyValueViewModel(),
                        });
                    }
                }
            }

            mapper.AcademicDetails = new List<AcademicDetailsViewModel>();
            if (dto.AcademicDetails != null)
            {
                foreach (var academics in dto.AcademicDetails)
                {
                    mapper.AcademicDetails.Add(new AcademicDetailsViewModel()
                    {
                        Qualification = academics.QualificationID.HasValue ? academics.QualificationID.ToString() : null,
                        EmployeeQualificationMapIID = academics.EmployeeQualificationMapIID,
                        EmployeeID = dto.EmployeeIID,
                        ModeOfProgramme = academics.ModeOfProgramme,
                        TitleOfProgramme = academics.TitleOfProgramme,
                        University = academics.University,
                        MarksInPercentage = academics.MarksInPercentage,
                        Subject = academics.Subject,
                        Months = academics.GraduationMonth.HasValue ? academics.GraduationMonth.ToString() : null,
                        GraduationYear = academics.GraduationYear
                    });

                }
            }

            mapper.ExperienceDetails = new List<ExperienceDetailsViewModel>();
            if (dto.ExperienceDetails != null)
            {
                foreach (var experience in dto.ExperienceDetails)
                {
                    mapper.ExperienceDetails.Add(new ExperienceDetailsViewModel()
                    {
                        EmployeeExperienceDetailIID = experience.EmployeeExperienceDetailIID,
                        EmployeeID = dto.EmployeeIID,
                        FromDateString = experience.FromDate.HasValue ? Convert.ToDateTime(experience.FromDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        ToDateString = experience.ToDate.HasValue ? Convert.ToDateTime(experience.ToDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        NameOfOraganizationtName = experience.NameOfOraganizationtName,
                        CurriculamOrIndustry = experience.CurriculamOrIndustry,
                        Designation = experience.Designation,
                        SubjectTaught = experience.SubjectTaught,
                        ClassTaught = experience.ClassTaught
                    });


                }
            }

            mapper.BankDetail = new EmployeeBankDetailViewModel()
            {
                EmpBankName = dto.BankDetailInfo.BankID.ToString(),
                IBAN = dto.BankDetailInfo.IBAN != null ? dto.BankDetailInfo.IBAN : null,
                SwiftCode = dto.BankDetailInfo.SwiftCode != null ? dto.BankDetailInfo.SwiftCode : null,
                AccountNo = dto.BankDetailInfo.AccountNo != null ? dto.BankDetailInfo.AccountNo : null,
                EmployeeBankIID = dto.BankDetailInfo.EmployeeBankIID,
            };


            mapper.Payroll = new EmployeePayrollViewModel()
            {
                CalendarType = dto.PayrollInfo.CalendarTypeID.HasValue ? dto.PayrollInfo.CalendarTypeID.ToString() : null,
                IsOTEligible = dto.PayrollInfo.IsOTEligible,
                IsEoSBEligible = dto.PayrollInfo.IsEoSBEligible,
                IsLeaveSalaryEligible = dto.PayrollInfo.IsLeaveSalaryEligible,
                AcademicCalendar = dto.PayrollInfo.AcademicCalendarID.HasValue ? dto.PayrollInfo.AcademicCalendarID.ToString() : null,
            };
            return mapper;
        }

        public static List<EmployeeDTO> ToDTO(List<EmployeeViewModel> vms, CallContext context)
        {
            var dtos = new List<EmployeeDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(ToDTO(vm, context));
            }

            return dtos;
        }

        public static EmployeeDTO ToDTO(EmployeeViewModel vm, CallContext context)
        {

            Mapper<EmployeeViewModel, EmployeeDTO>.CreateMap();
            //Mapper<ContactsViewModel, ContactDTO>.CreateMap();
            Mapper<LoginViewModel, LoginDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<AdditionalDetailViewModel, EmployeeAdditionalInfoDTO>.CreateMap();
            Mapper<EmployeeAirfareTicketDetailsViewModel, EmployeeAirFareDTO>.CreateMap();
            Mapper<EmployeePassportDetailViewModel, PassportVisaDetailDTO>.CreateMap();
            Mapper<EmployeeBankDetailViewModel, EmployeeBankDetailDTO>.CreateMap();
            Mapper<EmployeePayrollViewModel, EmployeePayrollDTO>.CreateMap();
            Mapper<EmployeeLeaveTypeViewModel, EmployeeLeaveAllocationDTO>.CreateMap();
            Mapper<EmployeeRelationsDetailsViewModel, EmployeeRelationsDetailDTO>.CreateMap();
            Mapper<AcademicDetailsViewModel, EmployeeAcademicQualificationDTO>.CreateMap();
            Mapper<ExperienceDetailsViewModel, EmployeeExperienceDTO>.CreateMap();

            var mapper = Mapper<EmployeeViewModel, EmployeeDTO>.Map(vm);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //mapper.DateOfJoining = Convert.ToDateTime(vm.JoiningDateString);
            mapper.DateOfJoining = DateTime.ParseExact(vm.JoiningDateString, dateFormat, CultureInfo.InvariantCulture);
            mapper.ConfirmationDate = string.IsNullOrEmpty(vm.ConfirmDateString) ? (DateTime?)null : DateTime.ParseExact(vm.ConfirmDateString, dateFormat, CultureInfo.InvariantCulture);
            //mapper.DateOfBirth = Convert.ToDateTime(vm.BirthDateString);
            mapper.DateOfBirth = DateTime.ParseExact(vm.BirthDateString, dateFormat, CultureInfo.InvariantCulture);
            mapper.DesignationID = string.IsNullOrEmpty(vm.Designation) ? (int?)null : int.Parse(vm.Designation);
            mapper.BranchID = string.IsNullOrEmpty(vm.Branch) ? (int?)null : int.Parse(vm.Branch);
            mapper.DepartmentID = string.IsNullOrEmpty(vm.Department) ? (int?)null : int.Parse(vm.Department);
            mapper.GenderID = string.IsNullOrEmpty(vm.Gender) ? (byte?)null : byte.Parse(vm.Gender);
            mapper.BloodGroupID = string.IsNullOrEmpty(vm.BloodGroup) ? (int?)null : int.Parse(vm.BloodGroup);
            mapper.JobTypeID = string.IsNullOrEmpty(vm.JobType) ? (int?)null : int.Parse(vm.JobType);
            mapper.LicenseTypeID = string.IsNullOrEmpty(vm.LicenseType) ? (byte?)null : byte.Parse(vm.LicenseType);
            mapper.MaritalStatusID = string.IsNullOrEmpty(vm.MaritalStatus) ? (byte?)null : byte.Parse(vm.MaritalStatus);
            mapper.CastID = string.IsNullOrEmpty(vm.Cast) ? (byte?)null : byte.Parse(vm.Cast);
            mapper.RelegionID = string.IsNullOrEmpty(vm.Relegion) ? (byte?)null : byte.Parse(vm.Relegion);
            mapper.CommunityID = string.IsNullOrEmpty(vm.Community) ? (byte?)null : byte.Parse(vm.Community);
            //mapper.DepartmentID = string.IsNullOrEmpty(vm.Department) ? (int?)null : int.Parse(vm.Department);
            mapper.ReportingEmployeeID = vm.ReportingEmployee == null || string.IsNullOrEmpty(vm.ReportingEmployee.Key) ? (long?)null : long.Parse(vm.ReportingEmployee.Key);
            //mapper.NationalityID = string.IsNullOrEmpty(vm.Nationality) ? (int?)null : int.Parse(vm.Nationality);
            mapper.NationalityID = vm.Nationality == null || string.IsNullOrEmpty(vm.Nationality.Key) ? (int?)null : int.Parse(vm.Nationality.Key);
            mapper.IsActive = vm.IsActive;
            mapper.EmergencyContactNo = vm.EmergencyContactNo;
            mapper.SignatureContentID = string.IsNullOrEmpty(vm.SignatureUrl) ? (long?)null : long.Parse(vm.SignatureUrl);
            //mapper.CategoryID = string.IsNullOrEmpty(vm.Category) ? (byte?)null : byte.Parse(vm.Category);
            mapper.Login.StatusID = !string.IsNullOrEmpty(vm.Login.Status.Key) ?
                (Infrastructure.Enums.LoginUserStatus)long.Parse(vm.Login.Status.Key) : (Infrastructure.Enums.LoginUserStatus?)null;
            mapper.Age = vm.Age;
            mapper.AccomodationTypeID = string.IsNullOrEmpty(vm.PassportDetails.AccomodationType) ? (byte?)null : byte.Parse(vm.PassportDetails.AccomodationType);
            mapper.PassageTypeID = string.IsNullOrEmpty(vm.PassportDetails.PassageType) ? (byte?)null : byte.Parse(vm.PassportDetails.PassageType);
            mapper.CBSEID = vm.CBSEID;
            mapper.InActiveDate = string.IsNullOrEmpty(vm.InActiveDateString) ? (DateTime?)null : DateTime.ParseExact(vm.InActiveDateString, dateFormat, CultureInfo.InvariantCulture);
            mapper.Grade = string.IsNullOrEmpty(vm.Grade) ? (int?)null : int.Parse(vm.Grade);
            mapper.StatusID = string.IsNullOrEmpty(vm.Status) ? (byte?)null : byte.Parse(vm.Status);


            //Resignation Details
            mapper.LastWorkingDate = string.IsNullOrEmpty(vm.ResignDetail.LastDateString) ? (DateTime?)null : DateTime.ParseExact(vm.ResignDetail.LastDateString, dateFormat, CultureInfo.InvariantCulture);
            mapper.ResignationDate = string.IsNullOrEmpty(vm.ResignDetail.ResignationDateString) ? (DateTime?)null : DateTime.ParseExact(vm.ResignDetail.ResignationDateString, dateFormat, CultureInfo.InvariantCulture);
            mapper.LeavingTypeID = string.IsNullOrEmpty(vm.ResignDetail.LeavingType) ? (byte?)null : byte.Parse(vm.ResignDetail.LeavingType);

            mapper.AirFareInfo = new EmployeeAirFareDTO();
            mapper.AirFareInfo.TicketEligibleFromDate = string.IsNullOrEmpty(vm.AirfareTicketDetail.TicketEligibleFromDateString) ? 
                                             (DateTime?)null : DateTime.ParseExact(vm.AirfareTicketDetail.TicketEligibleFromDateString, dateFormat, CultureInfo.InvariantCulture);
            mapper.AirFareInfo.EmployeeCountryAirportID =vm.AirfareTicketDetail.EmployeeCountryAirport == null || string.IsNullOrEmpty(vm.AirfareTicketDetail.EmployeeCountryAirport.Key) ? (long?)null : long.Parse(vm.AirfareTicketDetail.EmployeeCountryAirport.Key);
            mapper.AirFareInfo.ISTicketEligible = vm.AirfareTicketDetail.ISTicketEligible;
            mapper.AirFareInfo.EmployeeNearestAirportID = vm.AirfareTicketDetail.EmployeeNearestAirport == null || string.IsNullOrEmpty(vm.AirfareTicketDetail.EmployeeNearestAirport.Key) ? (long?)null : long.Parse(vm.AirfareTicketDetail.EmployeeNearestAirport.Key);
            mapper.AirFareInfo.TicketEntitilementID = vm.AirfareTicketDetail.TicketEntitilement == null || string.IsNullOrEmpty(vm.AirfareTicketDetail.TicketEntitilement.Key) ? (int?)null : int.Parse(vm.AirfareTicketDetail.TicketEntitilement.Key);
            mapper.AirFareInfo.GenerateTravelSector = vm.AirfareTicketDetail.GenerateTravelSector;
            mapper.AirFareInfo.LastTicketGivenDate = vm.AirfareTicketDetail.LastTicketGivenDate;
            mapper.AirFareInfo.IsTwoWay = vm.AirfareTicketDetail.IsTwoWay;
            mapper.AirFareInfo.FlightClassID = vm.AirfareTicketDetail.FlightClass == null || string.IsNullOrEmpty(vm.AirfareTicketDetail.FlightClass.Key) ? (byte?)null : byte.Parse(vm.AirfareTicketDetail.FlightClass.Key);
            //mapper.LeaveGroupID = string.IsNullOrEmpty(vm.LeaveDetails.LeaveGroup) ? (int?)null : int.Parse(vm.LeaveDetails.LeaveGroup);
            //if (vm.LeaveDetails?.LeaveTypes != null)
            //{
            //    foreach (var leaveInfo in vm.LeaveDetails.LeaveTypes)
            //    {
            //        if (leaveInfo.LeaveType != null && !string.IsNullOrEmpty(leaveInfo.LeaveType.Key))
            //        {
            //            mapper.EmployeeLeaveAllocationInfo.Add(new EmployeeLeaveAllocationDTO()
            //            {
            //                AllocatedLeaves = leaveInfo.AllocatedLeaves,
            //                LeaveTypeID = int.Parse(leaveInfo.LeaveType.Key),
            //            });
            //        }
            //    }
            //}
            mapper.EmployeeLeaveAllocationInfo = new List<EmployeeLeaveAllocationDTO>();
            mapper.LeaveGroupID = string.IsNullOrEmpty(vm.LeaveGroup) ? (int?)null : int.Parse(vm.LeaveGroup);

            foreach (var leaveInfo in vm.LeaveTypes)
            {
                if (leaveInfo.LeaveType != null && !string.IsNullOrEmpty(leaveInfo.LeaveType.Key))
                {
                    mapper.EmployeeLeaveAllocationInfo.Add(new EmployeeLeaveAllocationDTO()
                    {
                        AllocatedLeaves = (double?)leaveInfo.AllocatedLeaves,
                        LeaveTypeID = int.Parse(leaveInfo.LeaveType.Key),
                        LeaveAllocationIID = leaveInfo.LeaveAllocationIID,
                    });
                }
            }


            mapper.PassportVisaInfo = new PassportVisaDetailDTO()
            {
                CountryofIssueID = vm.PassportDetails.CountryofIssue == null || string.IsNullOrEmpty(vm.PassportDetails.CountryofIssue.Key) ? (int?)null : int.Parse(vm.PassportDetails.CountryofIssue.Key),
                //CountryofIssueID = string.IsNullOrEmpty(vm.PassportDetails.CountryofIssue) ? (int?)null : int.Parse(vm.PassportDetails.CountryofIssue),
                PassportNo = vm.PassportDetails.PassportNo != null ? vm.PassportDetails.PassportNo : null,
                PassportNoExpiry = string.IsNullOrEmpty(vm.PassportDetails.PassportExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(vm.PassportDetails.PassportExpiryDateString, dateFormat, CultureInfo.InvariantCulture),
                PassportVisaIID = vm.PassportDetails.PassportVisaIID,
                PlaceOfIssue = vm.PassportDetails.PlaceOfIssue != null ? vm.PassportDetails.PlaceOfIssue : null,
                NationalIDNo = vm.PassportDetails.NationalIDNo != null ? vm.PassportDetails.NationalIDNo : null,
                NationalIDNoExpiry = string.IsNullOrEmpty(vm.PassportDetails.NationalIDNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(vm.PassportDetails.NationalIDNoExpiryString, dateFormat, CultureInfo.InvariantCulture),
                ReferenceID = vm.PassportDetails.ReferenceID != null ? vm.PassportDetails.ReferenceID : null,
                UserType = vm.PassportDetails.UserType != null ? vm.PassportDetails.UserType : null,
                VisaNo = vm.PassportDetails.VisaNo != null ? vm.PassportDetails.VisaNo : null,
                SponsorID = string.IsNullOrEmpty(vm.PassportDetails.Sponsor) ? (byte?)null : byte.Parse(vm.PassportDetails.Sponsor),
                VisaExpiry = string.IsNullOrEmpty(vm.PassportDetails.VisaExpiryString) ? (DateTime?)null : DateTime.ParseExact(vm.PassportDetails.VisaExpiryString, dateFormat, CultureInfo.InvariantCulture),
                MOIID = vm.PassportDetails.MOIID != null ? vm.PassportDetails.MOIID : null,
                HealthCardNo = vm.PassportDetails.HealthCardNo != null ? vm.PassportDetails.HealthCardNo : null,
                LabourCardNo = vm.PassportDetails.LabourCardNo != null ? vm.PassportDetails.LabourCardNo : null,
            };

            mapper.RelationDetails = new List<EmployeeRelationsDetailDTO>();

            foreach (var details in vm.RelationDetails)
            {
                if (details.EmployeeRelationTypes != null && !string.IsNullOrEmpty(details.EmployeeRelationTypes.Key))
                {
                    mapper.RelationDetails.Add(new EmployeeRelationsDetailDTO()
                    {
                        EmployeeRelationsDetailIID = details.EmployeeRelationsDetailIID,
                        EmployeeID = vm.EmployeeIID,
                        FirstName = details.FirstName,
                        MiddleName = details.MiddleName,
                        LastName = details.LastName,
                        EmployeeRelationTypeID = byte.Parse(details.EmployeeRelationTypes.Key),
                        NationalIDNo = details.NationalID,
                        PassportNo = details.PassportNumber,
                        ContactNo = details.ContactNumber,
                    });
                }
            }

            mapper.AcademicDetails = new List<EmployeeAcademicQualificationDTO>();
            foreach (var academics in vm.AcademicDetails)
            {
                if (academics.Qualification != null && !string.IsNullOrEmpty(academics.Qualification))
                {
                    mapper.AcademicDetails.Add(new EmployeeAcademicQualificationDTO()
                    {

                        EmployeeQualificationMapIID = academics.EmployeeQualificationMapIID,
                        EmployeeID = vm.EmployeeIID,
                        TitleOfProgramme = academics.TitleOfProgramme,
                        ModeOfProgramme = academics.ModeOfProgramme,
                        University = academics.University,
                        GraduationMonth = string.IsNullOrEmpty(academics.Months) ? (int?)null : int.Parse(academics.Months),
                        GraduationYear = academics.GraduationYear,
                        QualificationID = string.IsNullOrEmpty(academics.Qualification) ? (byte?)null : byte.Parse(academics.Qualification),
                        MarksInPercentage = academics.MarksInPercentage,
                        Subject = academics.Subject

                    });
                }
            }

            mapper.ExperienceDetails = new List<EmployeeExperienceDTO>();
            foreach (var experience in vm.ExperienceDetails)
            {
                if (experience.NameOfOraganizationtName != null && !string.IsNullOrEmpty(experience.NameOfOraganizationtName))
                {
                    mapper.ExperienceDetails.Add(new EmployeeExperienceDTO()
                    {

                        EmployeeExperienceDetailIID = experience.EmployeeExperienceDetailIID,
                        EmployeeID = vm.EmployeeIID,
                        FromDate = string.IsNullOrEmpty(experience.FromDateString) ? (DateTime?)null : DateTime.ParseExact(experience.FromDateString, dateFormat, CultureInfo.InvariantCulture),
                        ToDate = string.IsNullOrEmpty(experience.ToDateString) ? (DateTime?)null : DateTime.ParseExact(experience.ToDateString, dateFormat, CultureInfo.InvariantCulture),
                        NameOfOraganizationtName = experience.NameOfOraganizationtName,
                        CurriculamOrIndustry = experience.CurriculamOrIndustry,
                        Designation = experience.Designation,
                        SubjectTaught = experience.SubjectTaught,
                        ClassTaught = experience.ClassTaught

                    });
                }
            }

            mapper.PayrollInfo = new EmployeePayrollDTO()
            {
                CalendarTypeID = string.IsNullOrEmpty(vm.Payroll.CalendarType) ? (byte?)null : byte.Parse(vm.Payroll.CalendarType),
                IsOTEligible = vm.Payroll.IsOTEligible,
                IsEoSBEligible = vm.Payroll.IsEoSBEligible,
                IsLeaveSalaryEligible = vm.Payroll.IsLeaveSalaryEligible,
                AcademicCalendarID = string.IsNullOrEmpty(vm.Payroll.AcademicCalendar) ? (long?)null : long.Parse(vm.Payroll.AcademicCalendar),
                //AcademicCalendarID = vm.Payroll.AcademicCalendar == null || string.IsNullOrEmpty(vm.Payroll.AcademicCalendar.Key) ? (long?)null : long.Parse(vm.Payroll.AcademicCalendar.Key),
            };

            mapper.BankDetailInfo = new EmployeeBankDetailDTO()
            {
                BankID = string.IsNullOrEmpty(vm.BankDetail.EmpBankName) ? (int?)null : int.Parse(vm.BankDetail.EmpBankName),
                AccountNo = vm.BankDetail.AccountNo,
                IBAN = vm.BankDetail.IBAN,
                SwiftCode = vm.BankDetail.SwiftCode,
                EmployeeID = vm.BankDetail.EmployeeID,
                EmployeeBankIID = vm.BankDetail.EmployeeBankIID,
            };
            mapper.EmployeePhoto = vm.ProfileUrl;
            return mapper;
        }

        public static List<KeyValueViewModel> ToKeyValueViewModel(List<EmployeeDTO> dtos)
        {
            var vMs = new List<KeyValueViewModel>();

            foreach (var dto in dtos)
            {
                vMs.Add(new KeyValueViewModel() { Key = dto.EmployeeIID.ToString(), Value = dto.FirstName + " " + dto.MiddleName + " " + dto.LastName });
            }

            return vMs;
        }

        public static EmployeeViewModel PackageNegotiationFromDTOtoVM(EmployeeDTO dto)
        {
            var vm = new EmployeeViewModel();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                if (dto != null)
                {
                vm.BirthDateString = dto.DateOfBirth.HasValue ? dto.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                vm.Designation = dto.DesignationID.HasValue ? dto.DesignationID.ToString() : null;
                vm.Department = dto.DepartmentID.HasValue ? dto.DepartmentID.Value.ToString() : string.Empty;
                vm.Gender = dto.GenderID.HasValue ? dto.GenderID.ToString() : null;
                vm.BloodGroup = dto.BloodGroupID.HasValue ? dto.BloodGroupID.ToString() : null;
                vm.JobType = dto.JobTypeID.HasValue ? dto.JobTypeID.ToString() : null;
                vm.EmergencyContactNo = dto.EmergencyContactNo;
                vm.Nationality = dto.NationalityID.HasValue ? new KeyValueViewModel()
                {
                    Key = dto.NationalityID.ToString(),
                    Value = dto.NationalityName
                } : new KeyValueViewModel();

                vm.JobTypeID = dto.JobTypeID;
                vm.IsActive = true;
                vm.FirstName = dto.FirstName;
                vm.MiddleName = dto.MiddleName;
                vm.LastName = dto.LastName;
                vm.Age = dto.Age;
                vm.BloodGroupID = dto.BloodGroupID;
                vm.WorkEmail = dto.WorkEmail;
                vm.WorkPhone = dto.WorkPhone;
                vm.WorkMobileNo = dto.WorkMobileNo;
                vm.EmergencyContactNo = dto.EmergencyContactNo;
                vm.ApplicantName = dto.FirstName + " " + dto.MiddleName + " " + dto.LastName;
                vm.JobInterviewMapID = dto.JobInterviewMapID;
            }

            return vm;
        }
    }
}