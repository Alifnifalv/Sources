using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;

namespace Eduegate.Web.Library.Support.CustomerSupport
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "TicketFeeDueMaps", "CRUDModel.ViewModel.TicketFeeDueMaps")]
    [DisplayName(" ")]
    public class TicketingFeeDueMapViewModel : BaseMasterViewModel
    {
        public TicketingFeeDueMapViewModel()
        {

        }

        public long TicketFeeDueMapIID { get; set; }

        public long? TicketID { get; set; }

        public long? StudentFeeDueID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("InvoiceNo")]
        public string InvoiceNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("InvoiceDate")]
        public string InvoiceDateString { get; set; }
        public DateTime? InvoiceDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("FeeMaster")]
        public string FeeMaster { get; set; }
        public int? FeeMasterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Amount")]
        public decimal? DueAmount { get; set; }

    }
}