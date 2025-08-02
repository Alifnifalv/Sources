using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "RouteList", "CRUDModel.ViewModel.RouteList")]
    [DisplayName("Route List")]
    public class RouteGroupRouteListViewModel : BaseMasterViewModel
    {
        public RouteGroupRouteListViewModel()
        {
            //ExamSubjectGrid = new List<RemarksEntryExamMapViewModel>() { new RemarksEntryExamMapViewModel() };
        }

        public int RouteID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("RouteCode")]
        public string RouteCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("RouteDescription")]
        public string RouteDescription { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.GridGroup, "onecol-header-left", Attributes4 = "colspan=4")]
        //[DisplayName("")]
        //public List<RemarksEntryExamMapViewModel> ExamSubjectGrid { get; set; }

    }
}