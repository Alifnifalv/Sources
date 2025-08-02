using Eduegate.Domain.Frameworks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AirfareTicketDetail", "CRUDModel.ViewModel.AirfareTicketDetail")]
    [DisplayName("Airfare Ticket Details")]
    public class EmployeeAirfareTicketDetailsViewModel : BaseMasterViewModel
    {
        public EmployeeAirfareTicketDetailsViewModel()
        {
            ISTicketEligible = false;
            IsTwoWay = true;
            TicketEntitilement = new KeyValueViewModel();
            EmployeeCountryAirport = new KeyValueViewModel();
            EmployeeNearestAirport = new KeyValueViewModel();           
        }

       
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", "ng-change=TicketEligibleChanges($event, $element,CRUDModel.ViewModel.AirfareTicketDetail)")]
        [CustomDisplay("Is Ticket Eligible")]
        public bool? ISTicketEligible { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled=!CRUDModel.ViewModel.AirfareTicketDetail.ISTicketEligible")]
        [CustomDisplay(" Date from when ticket is eligible")]
        public string TicketEligibleFromDateString { get; set; }
        public DateTime? TicketEligibleFromDate { get; set; }

      
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CountryAirport", "Numeric", false, "", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.AirfareTicketDetail.ISTicketEligible")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Airports", "LookUps.Airports")]        
        [CustomDisplay("Employee Country Airport")]
        public KeyValueViewModel EmployeeCountryAirport { get; set; }
        public long? EmployeeCountryAirportID { get; set; }    

       
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("NearestAirport", "Numeric", false, "GenerateTravelSector($event,$element,CRUDModel.ViewModel.AirfareTicketDetail)", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.AirfareTicketDetail.ISTicketEligible")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Airports", "LookUps.Airports")]
        [CustomDisplay("Employee Nearest Airport")]
        public KeyValueViewModel EmployeeNearestAirport { get; set; }      
        public long? EmployeeNearestAirportID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", "ng-change=GenerateTravelSector($event,$element,CRUDModel.ViewModel.AirfareTicketDetail)", optionalAttribs: "ng-disabled=!CRUDModel.ViewModel.AirfareTicketDetail.ISTicketEligible")]
        [CustomDisplay("Is Two way")]
        public bool? IsTwoWay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FlightClass", "Numeric", false, "GenerateTravelSector($event, $element,CRUDModel.ViewModel.AirfareTicketDetail)", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.AirfareTicketDetail.ISTicketEligible")]
        [LookUp("LookUps.FlightClass")]
        [CustomDisplay("Flight Class")]
        public KeyValueViewModel FlightClass { get; set; }
        public short? FlightClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("TicketEntitilements", "Numeric", false, "GenerateTravelSector($event, $element,CRUDModel.ViewModel.AirfareTicketDetail)", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.AirfareTicketDetail.ISTicketEligible")]
        [LookUp("LookUps.TicketEntitilements")]
        [CustomDisplay("Ticket Entitilement")]
        public KeyValueViewModel TicketEntitilement { get; set; }
        public int? TicketEntitilementID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = true")]
        [CustomDisplay("Travel Sector")]       
        public string GenerateTravelSector { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled = true")]
        [CustomDisplay("Last Ticket Given Date")]
        public string LastTicketgivenString { get; set; }

        public DateTime? LastTicketGivenDate { get; set; }


    }
}