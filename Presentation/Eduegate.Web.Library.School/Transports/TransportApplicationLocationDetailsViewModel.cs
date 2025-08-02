using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TransportLocation", "CRUDModel.ViewModel.TransportLocation")]
    [DisplayName("Student Details")]
    public class TransportApplicationLocationDetailsViewModel : BaseMasterViewModel
    {
        public TransportApplicationLocationDetailsViewModel()
        {
            //Class = new KeyValueViewModel();
        }





        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth alignleft")]
        [CustomDisplay("DropStopLocationDetails")]
        public bool? Message { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.IsRouteDifferent!=true")]
        [CustomDisplay("Building/FlatNo")]
        [StringLength(20)]
        public string BuildingNo_Drop { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.IsRouteDifferent!=true")]
        [CustomDisplay("StreetNo")]
        [StringLength(50)]
        public string StreetNo_Drop { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.IsRouteDifferent!=true")]
        [CustomDisplay("ZoneNo")]
        [StringLength(50)]
        public string ZoneNo_Drop { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.IsRouteDifferent!=true")]
        [CustomDisplay("LocationNo")]
        [StringLength(50)]
        public string LocationNo_Drop { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.IsRouteDifferent!=true")]
        [CustomDisplay("StreetName")]
        [StringLength(50)]
        public string StreetName_Drop { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.IsRouteDifferent!=true")]
        [CustomDisplay("LocationName")]
        [StringLength(50)]
        public string LocationName_Drop { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.IsRouteDifferent!=true")]
        [CustomDisplay("LandMark")]
        [StringLength(200)]
        public string LandMark_Drop { get; set; }
    }
}