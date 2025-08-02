using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Distributions;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "TimeSlots", "CRUDModel.ViewModel.TimeSlots")]
    [DisplayName("Delivery Time Slot")]
    public class DeliveryTimeSlotViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [DisplayName("Time From")]
        public string TimeFrom { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [DisplayName("Time To")]
        public string TimeTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='InsertGridRow($index, ModelStructure.TimeSlots[0], CRUDModel.ViewModel.TimeSlots)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='RemoveGridRow($index, ModelStructure.TimeSlots[0], CRUDModel.ViewModel.TimeSlots)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public bool? IsCutOff { get; set; }

        public short? CutOffDays { get; set; }

        public string CutOffTime { get; set; }

        public string CutOffHour { get; set; }

        public string CutOffDisplayText { get; set; }

        public long DeliveryTypeTimeSlotMapIID { get; set; }

        public static List<DeliveryTimeSlotDTO> ToDTO(List<DeliveryTimeSlotViewModel> vm)
        {
            Mapper<DeliveryTimeSlotViewModel, DeliveryTimeSlotDTO>.CreateMap();
            var mapper = Mapper<List<DeliveryTimeSlotViewModel>, List<DeliveryTimeSlotDTO>>.Map(vm);
            return mapper;
        }

        public static List<DeliveryTimeSlotViewModel> FromDTO(List<DeliveryTimeSlotDTO> dto)
        {
            Mapper<DeliveryTimeSlotDTO, DeliveryTimeSlotViewModel>.CreateMap();
            var mapper = Mapper<List<DeliveryTimeSlotDTO>, List<DeliveryTimeSlotViewModel>>.Map(dto);
            return mapper;
        } 
    }
}
