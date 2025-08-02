using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.CustomerService;

namespace Eduegate.Web.Library.ViewModels.CustomerService
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RepairOrderMasterViewModel", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("RO Details")]
    public class RepairOrderMasterViewModel : BaseMasterViewModel
    {
        public RepairOrderMasterViewModel()
        {
            Defect = new DefectViewModel();
            JobDetail = new JobDetailViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "onecol-header-left")]
        [DisplayName("Date")]
        public string OrderDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-blur='GetVehcileDetails($event, $element, CRUDModel.Model.MasterViewModel)'")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Chasis Number")]
        public string ChasisNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "ng-blur='GetVehcileDetails($event, $element, CRUDModel.Model.MasterViewModel)'")]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [DisplayName("Regitration Number")]
        public string RegitrationNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Vehicle Description")]
        public string VehicleDescription { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Stock No/Model")]
        public string StockNoModel { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Regitration Date")]
        public DateTime? RegitrationDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Bill Vehicle Type")]
        public string BillVehicleType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(9, ErrorMessage = "Maximum Length should be within 9!")]
        [DisplayName("Warranty KMs")]
        public int? WarrantyKMs { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [DisplayName("Last Service KMs")]
        public int? LastServiceKMs { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Customer")]
        [Select2("Customer", "Numeric", false, "", false)]
        [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=REPAIRCUSTOMER", "LookUps.Customer")]
        [QuickSmartView("REPAIRCUSTOMER")]
        public KeyValueViewModel Customer { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [DisplayName("Phone No")]
        public int? PhoneNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Civil Id")]
        public long? CivilId { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("RO Shop")]
        [Select2("ROShop", "Numeric", false, "", false)]
        [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=SHOP", "LookUps.Customer")]
        [QuickSmartView("SHOP")]
        public KeyValueViewModel ROShop { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("RO Document")]
        [LookUp("LookUps.REPAIRORDERTYPE")]
        [Select2("OrderType", "Numeric", false, "", false)]
        public KeyValueViewModel OrderType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("RO Number")]
        [QuickSmartView("RepairOrder")]
        public string RONO { get; set; }

        [RegularExpression("1", ErrorMessage = "Enter 1 to give priority or leave blank !")]
        [MaxLength(1, ErrorMessage = "Enter 1 to give priority or leave blank !")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Priority")]
        [LookUp("LookUps.Priority")]
        public string Priority { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "JobDetail", "CRUDModel.Model.MasterViewModel.JobDetail")]
        [DisplayName("Jobs")]
        public JobDetailViewModel JobDetail { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "DefectDetails", "CRUDModel.Model.MasterViewModel.Defect")]
        [DisplayName("Defects")]
        public DefectViewModel Defect { get; set; }

        public static RepairOrderViewModel ToVM(RepairOrderDTO dto)
        {
            return new RepairOrderViewModel()
            {
                MasterViewModel = new RepairOrderMasterViewModel()
                {
                    ChasisNo = dto.CHASSISNO,
                    OrderDate = dto.DOCDATE.HasValue ? dto.DOCDATE.Value.ToString() : string.Empty,
                    RONO = dto.RONO.ToString(),
                    Customer = KeyValueViewModel.ToViewModel(dto.Customer),
                    OrderType = KeyValueViewModel.ToViewModel(dto.OrderType),
                    PhoneNumber = dto.PhoneNumber,
                    CivilId = dto.CivilID,
                    ROShop = KeyValueViewModel.ToViewModel(dto.Shops),
                    RegitrationNo = dto.KTNO,
                    VehicleDescription = dto.VehicleDescription,
                    RegitrationDate = dto.RegitrationDate,
                    BillVehicleType = dto.BillVehicleType,
                    WarrantyKMs = dto.WarrantyKMs,
                    LastServiceKMs = dto.LastServiceKMs,
                    Priority = dto.PRIORITY,
                },

            };
        }
    }
}
