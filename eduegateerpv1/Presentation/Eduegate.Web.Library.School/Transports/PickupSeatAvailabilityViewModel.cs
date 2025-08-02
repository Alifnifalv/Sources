using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "PickupSeatAvailability", "CRUDModel.ViewModel.PickupSeatAvailability")]
    public class PickupSeatAvailabilityViewModel : BaseMasterViewModel
    {

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("MaximumSeatCapacity")]
        public int? MaximumSeatCapacity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]       
        [CustomDisplay("AllowSeatCapacity")]
        public int? AllowSeatCapacity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("SeatOccupied")]
        public int? SeatOccupied { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("SeatAvailability")]
        public int? SeatAvailability { get; set; }

    }

    [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "DropSeatAvailability", "CRUDModel.ViewModel.DropSeatAvailability")]
    public class DropSeatAvailabilityViewModel : BaseMasterViewModel
    {

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("MaximumSeatCapacity")]
        public int? MaximumSeatCapacity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AllowSeatCapacity")]
        public int? AllowSeatCapacity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("SeatOccupied")]
        public int? SeatOccupied { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("SeatAvailability")]
        public int? SeatAvailability { get; set; }


    }
}
