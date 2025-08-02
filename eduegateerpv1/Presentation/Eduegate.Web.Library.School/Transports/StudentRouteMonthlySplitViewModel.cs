using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Transports
{   
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "RouteMonthlySplit", "CRUDModel.ViewModel.RouteMonthlySplit")]
    [DisplayName("Split")]
    public class StudentRouteMonthlySplitViewModel : BaseMasterViewModel
    {
        public long StudentRouteMonthlySplitIID { get; set; }

        public long? StudentRouteStopMapID { get; set; }

        public int? MonthID { get; set; }
        public int? Year { get; set; }
        public int? FeePeriodID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft")]
        [CustomDisplay("Select")]
        public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Month")]
        public string MonthName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        //[CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft")]
        //[CustomDisplay("Is Collected")]
        public bool? IsCollected { get; set; }

    }
}
