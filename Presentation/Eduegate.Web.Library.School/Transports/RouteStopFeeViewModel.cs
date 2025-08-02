using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Stops", "CRUDModel.ViewModel.Stop")]
    [DisplayName("Stop Fees")]
    public class RouteStopFeeViewModel : BaseMasterViewModel
    {
        public RouteStopFeeViewModel()
        {
            IsActive = true;
        }

        [ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind='gridModel.Key'")]
        [CustomDisplay("Stop")]
        public string Key { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind='gridModel.Value'")]        
        public string Value { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [CustomDisplay("MonthlyFare")]
        public decimal? RouteFareOneWay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=AddBelow($index)")]
        [CustomDisplay("Applybelow")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HiddenWithLabel)]
        public long RouteStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HiddenWithLabel)]
        public long RouteID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GeoLocation, Attributes = "ng-click='ShowGoogleMap(gridModel, \"Latitude\", \"Longitude\")'")]
        [CustomDisplay("pick location from map")]
        public string SelectLocation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox,"textleft")]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(9, ErrorMessage = "Maximum Length should be within 7!")]
        [CustomDisplay("Latitude")]
        public string Latitude { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(9, ErrorMessage = "Maximum Length should be within 7!")]
        [CustomDisplay("Longitude")]
        public string Longitude { get; set; }

    }
}