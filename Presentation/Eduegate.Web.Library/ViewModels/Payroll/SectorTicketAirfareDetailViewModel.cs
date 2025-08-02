using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SectorTicketAirfareDetail", "CRUDModel.ViewModel.SectorTicketAirfareDetail")]
    [DisplayName("Sector Ticket Airfare Detail")]
    public class SectorTicketAirfareDetailViewModel : BaseMasterViewModel
    {
        public SectorTicketAirfareDetailViewModel()
        {
            FlightClass = new KeyValueViewModel();
            Airport = new KeyValueViewModel();
            ReturnAirport = new KeyValueViewModel();

        }
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", "ng-change=GenerateTravelSector($event,$element,gridModel)")]
        [CustomDisplay("Is Two way")]
        public bool? IsTwoWay { get; set; } = true;

        public long SectorTicketAirfareIID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FlightClass", "Numeric", false)]
        [LookUp("LookUps.FlightClass")]
        [CustomDisplay("Flight Class")]
        public KeyValueViewModel FlightClass { get; set; }
        public short? FlightClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AirportID", "Numeric", false, "GenerateTravelSector($event,$element,gridModel)")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Airports", "LookUps.Airports")]
        [CustomDisplay("Airport")]
        public KeyValueViewModel Airport { get; set; }
        public long? AirportID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AirportID", "Numeric", false, "GenerateTravelSector($event,$element,gridModel)")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Airports", "LookUps.Airports")]
        [CustomDisplay("Return Airport")]
        public KeyValueViewModel ReturnAirport { get; set; }
        public long? ReturnAirportID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Travel Sector")]
        public string GenerateTravelSector { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [CustomDisplay("Rate")]
        public decimal? Rate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.SectorTicketAirfareDetail[0], CRUDModel.ViewModel.SectorTicketAirfareDetail)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.SectorTicketAirfareDetail[0],CRUDModel.ViewModel.SectorTicketAirfareDetail)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
