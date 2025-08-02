using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Lms;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Web.Library.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AirfareTicketEntry", "CRUDModel.ViewModel")]
    [DisplayName("Airfare Ticket Entry")]
    public class TicketEntitilementEntryViewModel : BaseMasterViewModel
    {
        public TicketEntitilementEntryViewModel()
        {

        }
        public long TicketEntitilementEntryIID { get; set; }      

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Employee", "String", false, "EmployeeChanges($event, $element, CRUDModel.ViewModel)")]
        [CustomDisplay("Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=NonSettledEmployees", "LookUps.NonSettledEmployees")]
        public KeyValueViewModel Employee { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-change=GenerateTravelSector($event,$element,CRUDModel.ViewModel)")]
        [CustomDisplay("Airfare Ticket Entry Date")]
        public string TicketIssueDateString { get; set; }
        public System.DateTime? TicketIssueDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled=true")]
        [CustomDisplay("Date of Joining")]
        public string DateOfJoiningString { get; set; }
        public DateTime? DateOfJoining { get; set; }


        public long? EmployeeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", "ng-change=TicketEligibleChanges($event,$element,CRUDModel.ViewModel)")]
        [CustomDisplay("Is Ticket Eligible")]
        public bool? ISTicketEligible { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay(" Date from when ticket is eligible")]
        public string TicketEligibleFromDateString { get; set; }
        public DateTime? TicketEligibleFromDate { get; set; }

        //, optionalAttribs: "ng-disabled=! CRUDModel.ViewModel.ISTicketEligible"
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", "ng-change=GenerateTravelSector($event,$element,CRUDModel.ViewModel)")]
        [CustomDisplay("Is Two way")]
        public bool? IsTwoWay { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("NearestAirport", "Numeric", false, "GenerateTravelSector($event, $element, CRUDModel.ViewModel)")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Airports", "LookUps.Airports")]
        [CustomDisplay("Employee Nearest Airport")]
        public KeyValueViewModel EmployeeNearestAirport { get; set; }
        public long? EmployeeNearestAirportID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FlightClass", "Numeric", false, "GenerateTravelSector($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.FlightClass")]
        [CustomDisplay("Flight Class")]
        public KeyValueViewModel FlightClass { get; set; }
        public short? FlightClassID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("TicketEntitilements", "Numeric", false, "GenerateTravelSector($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.TicketEntitilements")]
        [CustomDisplay("Ticket Entitilement")]
        public KeyValueViewModel TicketEntitilement { get; set; }
        public int? TicketEntitilementID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = true")]
        [CustomDisplay("Travel Sector")]
        public string GenerateTravelSector { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled = true")]
        [CustomDisplay("Last Ticket Given Date")]
        public string LastTicketgivenString { get; set; }

        public DateTime? LastTicketGivenDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Ticket Fare Issued")]
        public bool? IsTicketFareIssued { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=!CRUDModel.ViewModel.IsTicketFareIssued")]
        [CustomDisplay("Ticket Fare Issue %")]
        public decimal? TicketFareIssuedPercentage { get; set; } = 0;
        
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Ticket Fare Reimbursed")]
        public bool? IsTicketFareReimbursed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=!CRUDModel.ViewModel.IsTicketFareReimbursed")]
        [CustomDisplay("Ticket Fare Reimbursement %")]
        public decimal? TicketFareReimbursementPercentage { get; set; } = 0;

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Vacation Days EveryYear")]
        public int? VacationDaysEveryYear { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Vacation Starting Date")]
        public string VacationStartingDateString { get; set; }
        public DateTime? VacationStartingDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Travel Return Airfare")]
        public decimal? TravelReturnAirfare { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Balance Brought Forward")]
        public decimal? BalanceBroughtForward { get; set; } = 0;

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.TicketEntitlementEntryStatus")]
        [CustomDisplay("Ticket Entitlement Entry Status")]
        public string TicketEntitlementEntryStatus { get; set; } = "1";
        public short? TicketEntitlementEntryStatusID { get; set; }

      
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Consider LOP")]
        public bool? IsConsidereLOP { get; set; }
      
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=!CRUDModel.ViewModel.IsConsidereLOP")]
        [CustomDisplay("Days To Be Considered For LOP")]
        public decimal? DaysToBeConsideredForLOP { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=!CRUDModel.ViewModel.IsConsidereLOP")]
        [CustomDisplay("LOP to be considered for calculation")]
        public decimal? LOPToBeConsideredCalculation { get; set; } = 0;

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=GenerateAirfare(CRUDModel.ViewModel)")]
        [CustomDisplay("Generate")]
        public string GenerateButton { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = true")]
        [CustomDisplay("Ticket Entitilement Days")]
        public int? TicketEntitilementDays { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = true")]
        [CustomDisplay("LOP for Ticket Entitilement")]
        public decimal? LOPforTicketEntitilement { get; set; } = 0;
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = true")]
        [CustomDisplay("Ticket Entitilement%")]
        public decimal? TicketEntitilementPer { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = true")]
        [CustomDisplay("Ticket Fare Payable")]
        public decimal? TicketfarePayable { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = true")]
        [CustomDisplay("Ticket Issued Or Fare Reimbursed")]
        public decimal? TicketIssuedOrFareReimbursed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = true")]
        [CustomDisplay("Balance Carried Forward %")]
        public decimal? BalanceCarriedForwardPer { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = true")]
        [CustomDisplay("Balance Ticket Amount Payable")]
        public decimal? BalanceTicketAmountPayable { get; set; }

        public long? DepartmentID { get; set; }
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TicketEntitilementEntryDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TicketEntitilementEntryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TicketEntitilementEntryDTO, TicketEntitilementEntryViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var ssDto = dto as TicketEntitilementEntryDTO;
            var vm = Mapper<TicketEntitilementEntryDTO, TicketEntitilementEntryViewModel>.Map(ssDto);

            vm.BalanceCarriedForwardPer = ssDto.BalanceCarriedForwardPer;
            vm.BalanceTicketAmountPayable = ssDto.BalanceTicketAmountPayable;
            vm.EmployeeID = ssDto.EmployeeID;
            vm.Employee = ssDto.EmployeeID.HasValue ? new KeyValueViewModel() { Key = ssDto.Employee.Key, Value = ssDto.Employee.Value } : new KeyValueViewModel();
            vm.EmployeeNearestAirport = ssDto.EmployeeCountryAirportID.HasValue ? new KeyValueViewModel() { Key = ssDto.EmployeeCountryAirport.Key, Value = ssDto.EmployeeCountryAirport.Value } : new KeyValueViewModel();
            vm.EmployeeNearestAirportID = ssDto.EmployeeCountryAirportID;
            vm.FlightClass = ssDto.FlightClassID.HasValue ? new KeyValueViewModel() { Key = ssDto.FlightClass.Key, Value = ssDto.FlightClass.Value } : new KeyValueViewModel();
            vm.FlightClassID = ssDto.FlightClassID;
            vm.ISTicketEligible = ssDto.ISTicketEligible;
            vm.IsTicketFareIssued = ssDto.IsTicketFareIssued;
            vm.IsTicketFareReimbursed = ssDto.IsTicketFareReimbursed;
            vm.IsTwoWay = ssDto.IsTwoWay;
            vm.TicketEligibleFromDate = ssDto.TicketEligibleFromDate; ;
            vm.TicketEligibleFromDateString = (ssDto.TicketEligibleFromDate.HasValue ? ssDto.TicketEligibleFromDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.TicketEntitilement = ssDto.TicketEntitilementID.HasValue ? new KeyValueViewModel() { Key = ssDto.TicketEntitilement.Key, Value = ssDto.TicketEntitilement.Value } : new KeyValueViewModel();
            vm.TicketEntitilementID = ssDto.TicketEntitilementID;
            vm.TicketEntitilementDays = ssDto.TicketEntitilementDays;
            vm.TicketEntitilementEntryIID = ssDto.TicketEntitilementEntryIID;
            vm.TicketEntitilementPer = ssDto.TicketEntitilementPer;
            vm.TicketFareIssuedPercentage = ssDto.TicketFareIssuedPercentage;
            vm.TicketfarePayable = ssDto.TicketfarePayable;
            vm.TicketFareReimbursementPercentage = ssDto.TicketFareReimbursementPercentage;
            vm.TicketIssueDate = ssDto.TicketIssueDate;
            vm.TicketIssueDateString = ssDto.TicketIssueDate.HasValue ? ssDto.TicketIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.TicketIssuedOrFareReimbursed = ssDto.TicketIssuedOrFareReimbursed;
            vm.TravelReturnAirfare = ssDto.TravelReturnAirfare;
            vm.LastTicketGivenDate = ssDto.LastTicketGivenDate;
            vm.LastTicketgivenString = ssDto.LastTicketGivenDate.HasValue ? ssDto.LastTicketGivenDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.DateOfJoining = ssDto.DateOfJoining;
            vm.DateOfJoiningString = ssDto.DateOfJoining.HasValue ? ssDto.DateOfJoining.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.VacationDaysEveryYear = ssDto.VacationDaysEveryYear;
            vm.VacationStartingDate = ssDto.VacationStartingDate;
            vm.VacationStartingDateString = ssDto.VacationStartingDate.HasValue ? ssDto.VacationStartingDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.GenerateTravelSector = ssDto.GenerateTravelSector;
            vm.TicketEntitlementEntryStatusID = ssDto.TicketEntitlementEntryStatusID;
            vm.TicketEntitlementEntryStatus = ssDto.TicketEntitlementEntryStatusID.HasValue ? vm.TicketEntitlementEntryStatusID.ToString() : null;
            vm.LOPforTicketEntitilement = ssDto.LOPforTicketEntitilement;
            vm.IsConsidereLOP=ssDto.IsConsidereLOP;
            vm.DaysToBeConsideredForLOP = ssDto.DaysToBeConsideredForLOP;
            vm.BalanceBroughtForward = ssDto.BalanceBroughtForward;
            vm.LOPToBeConsideredCalculation = ssDto.LOPforTicketEntitilement;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TicketEntitilementEntryViewModel, TicketEntitilementEntryDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<TicketEntitilementEntryViewModel, TicketEntitilementEntryDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.BalanceCarriedForwardPer = this.BalanceCarriedForwardPer;
            dto.BalanceTicketAmountPayable = this.BalanceTicketAmountPayable;
            dto.EmployeeID = string.IsNullOrEmpty(this.Employee.Key) ? (long?)null : long.Parse(this.Employee.Key);
            dto.EmployeeCountryAirportID = string.IsNullOrEmpty(this.EmployeeNearestAirport.Key) ? (long?)null : long.Parse(this.EmployeeNearestAirport.Key);
            dto.FlightClassID = string.IsNullOrEmpty(this.FlightClass.Key) ? (short?)null : short.Parse(this.FlightClass.Key);
            dto.ISTicketEligible = this.ISTicketEligible;
            dto.IsTicketFareIssued = this.IsTicketFareIssued;
            dto.IsTicketFareReimbursed = this.IsTicketFareReimbursed;
            dto.IsTwoWay = this.IsTwoWay;
            dto.TicketEligibleFromDate = dto.TicketIssueDate = string.IsNullOrEmpty(this.TicketEligibleFromDateString) ? (DateTime?)null : DateTime.ParseExact(this.TicketEligibleFromDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.TicketEntitilementID = string.IsNullOrEmpty(this.TicketEntitilement.Key) ? (short?)null : short.Parse(this.TicketEntitilement.Key);
            dto.TicketEntitilementDays = this.TicketEntitilementDays;
            dto.TicketEntitilementEntryIID = this.TicketEntitilementEntryIID;
            dto.TicketEntitilementPer = this.TicketEntitilementPer;
            dto.TicketFareIssuedPercentage = this.TicketFareIssuedPercentage;
            dto.TicketfarePayable = this.TicketfarePayable;
            dto.TicketFareReimbursementPercentage = this.TicketFareReimbursementPercentage;
            dto.TicketIssueDate = string.IsNullOrEmpty(this.TicketIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.TicketIssueDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.TicketIssuedOrFareReimbursed = this.TicketIssuedOrFareReimbursed;
            dto.TravelReturnAirfare = this.TravelReturnAirfare;
            dto.LastTicketGivenDate = string.IsNullOrEmpty(this.LastTicketgivenString) ? (DateTime?)null : DateTime.ParseExact(this.LastTicketgivenString, dateFormat, CultureInfo.InvariantCulture);
            dto.DateOfJoining = string.IsNullOrEmpty(this.DateOfJoiningString) ? (DateTime?)null : DateTime.ParseExact(this.DateOfJoiningString, dateFormat, CultureInfo.InvariantCulture);
            dto.VacationDaysEveryYear = this.VacationDaysEveryYear;
            dto.VacationStartingDate = string.IsNullOrEmpty(this.VacationStartingDateString) ? (DateTime?)null : DateTime.ParseExact(this.VacationStartingDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.GenerateTravelSector = this.GenerateTravelSector;
            dto.TicketEntitlementEntryStatusID = string.IsNullOrEmpty(this.TicketEntitlementEntryStatus) ? (short?)null : byte.Parse(this.TicketEntitlementEntryStatus);
            dto.LOPforTicketEntitilement = this.LOPforTicketEntitilement;
            dto.IsConsidereLOP = this.IsConsidereLOP;
            dto.DaysToBeConsideredForLOP = this.DaysToBeConsideredForLOP;
            dto.BalanceBroughtForward = this.BalanceBroughtForward;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TicketEntitilementEntryDTO>(jsonString);
        }
    }
}
