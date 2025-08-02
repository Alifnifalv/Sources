using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TransactionShipments", "CRUDModel.Model.MasterViewModel.ShipmentDetails")]
    [DisplayName("Shipment Details")]
    public class ShipmentDetailViewModel : BaseMasterViewModel
    {
        public long TransactionShipmentIID { get; set; }
        public Nullable<long> TransactionHeadID { get; set; }
        public Nullable<long> SupplierIDFrom { get; set; }
        public Nullable<long> SupplierIDTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Shipment Reference")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string ShipmentReference { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Freight Career")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string FreightCareer { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Clearance type")]
        public Nullable<short> ClearanceTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("AWB")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string AirWayBillNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [DisplayName("Frieght charges")]
        public decimal? FrieghtCharges { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [DisplayName("Broker charges")]
        public decimal? BrokerCharges { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("Weight")]
        public Nullable<double> Weight { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("No of boxes")]
        public int? NoOfBoxes { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Broker account")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string BrokerAccount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [DisplayName("Additional charges")]
        public decimal? AdditionalCharges { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }
    }
}
