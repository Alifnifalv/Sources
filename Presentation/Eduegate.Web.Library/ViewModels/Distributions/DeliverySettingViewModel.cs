using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Distributions;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliverySettings", "CRUDModel.ViewModel")]
    [DisplayName("Settings")]
    public class DeliverySettingViewModel : BaseMasterViewModel
    {
        public DeliverySettingViewModel()
        {
            ProductMap = new DeliveryTypeProductViewModel();
            AreaMap = new DeliveryTypeAreaViewModel();
            CustomerGroupCharge = new DeliveryTypeCustomerGroupViewModel();
            TimeSlots = new List<DeliveryTimeSlotViewModel>() { new DeliveryTimeSlotViewModel() };
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Delivery Type ID")]
        public int DeliveryTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Type Name")]
        public string DeliveryTypeName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Priority")]
        public string Priority { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.DeliveryTypeStatus")]
        [DisplayName("Status")]
        public string StatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "NumberOfDays", "ng-bind=\"ShowNumberOfDaysTextBox(CRUDModel.ViewModel.DeliveryTypeID, 5)\"")]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("Number Of Days")]
        public Nullable<int> NumberOfDays { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AreaMap", "AreaMap", "", "", "largegrid")]
        [DisplayName("Area")]
        public DeliveryTypeAreaViewModel AreaMap { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ProductMap", "ProductMap", "", "", "largegrid")]
        [DisplayName("Product")]
        public DeliveryTypeProductViewModel ProductMap { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerGroupCharge", "CustomerGroupCharge", "", "", "largegrid")]
        [DisplayName("Customer Group")]
        public DeliveryTypeCustomerGroupViewModel CustomerGroupCharge { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left", "", "", "ng-click='InsertGridRow($index, ModelStructure.TimeSlots[0], CRUDModel.ViewModel.TimeSlots)'")]
        [DisplayName("Delivery Time Slot")]
        public List<DeliveryTimeSlotViewModel> TimeSlots { get; set; }

        public static DeliverySettingDTO ToDTO(DeliverySettingViewModel vm)
        {
            Mapper<DeliverySettingViewModel, DeliverySettingDTO>.CreateMap();
            Mapper<DeliveryTimeSlotViewModel, DeliveryTimeSlotDTO>.CreateMap();
            Mapper<DeliveryTypeProductViewModel, ProductSKUDeliveryTypeChargeDTO>.CreateMap();
            Mapper<DeliveryTypeAreaViewModel, AreaDeliveryChargeDTO>.CreateMap();
            Mapper<CustomerGroupDeliveryChargeViewModel, CustomerGroupDeliveryChargeDTO>.CreateMap();

            var mapper = Mapper<DeliverySettingViewModel, DeliverySettingDTO>.Map(vm);
            mapper.StatusID = byte.Parse(vm.StatusID);
            mapper.Priority = int.Parse(vm.Priority);
            mapper.Days = vm.NumberOfDays;

            return mapper;
        }

        public static DeliverySettingViewModel FromDTO(DeliverySettingDTO dto)
        {
            Mapper<DeliverySettingDTO, DeliverySettingViewModel>.CreateMap();
            Mapper<DeliveryTimeSlotDTO, DeliveryTimeSlotViewModel>.CreateMap();
            Mapper<DeliveryTypeProductViewModel, DeliveryTypeProductViewModel>.CreateMap();
            Mapper<DeliveryTypeAreaViewModel, DeliveryTypeAreaViewModel>.CreateMap();
            Mapper<CustomerGroupDeliveryChargeViewModel, CustomerGroupDeliveryChargeViewModel>.CreateMap();

            var mapper = Mapper<DeliverySettingDTO, DeliverySettingViewModel>.Map(dto);
            mapper.Priority = dto.Priority.ToString();
            mapper.StatusID = dto.StatusID.ToString();
            mapper.NumberOfDays = dto.Days;

            return mapper;
        }
    }
}
