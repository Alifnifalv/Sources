using Newtonsoft.Json;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Salon;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Eduegate.Domain;

namespace Eduegate.Web.Library.ViewModels.Saloon
{
    //[Bind(Exclude = "Status")]
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ServiceDetails", "CRUDModel.Model")]
    [DisplayName("Details")]
    public class ServiceMasterViewModel : BaseMasterViewModel
    {
        private static string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");
        public ServiceMasterViewModel()
        {
            PriceSetting = new ServicePriceSettingViewModel();
            Staffs = new List<KeyValueViewModel>();
            AvailableFor = new KeyValueViewModel();
            PricingType = new KeyValueViewModel();
            TreatmentType = new KeyValueViewModel();
            ExtraTimeType = new KeyValueViewModel();
            Duration = new KeyValueViewModel();
            Staffs = new List<KeyValueViewModel>();
        }

        public Nullable<long> LoginID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Service ID")]
        public long ServiceIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Service Name")]
        public string ServiceName { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("TreatmentType", "Numeric", false)]
        [LookUp("LookUps.TreatmentType")]
        [DisplayName("Treatment type")]
        public KeyValueViewModel TreatmentType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AvailableFor", "Numeric", false)]
        [LookUp("LookUps.AvailableFor")]
        [DisplayName("Available For")]
        public KeyValueViewModel AvailableFor { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PricingType", "Numeric", false)]
        [LookUp("LookUps.PricingType")]
        [DisplayName("Pricing Type")]
        public KeyValueViewModel PricingType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ExtraTimeType", "Numeric", false)]
        [LookUp("LookUps.ExtraTimeType")]
        [DisplayName("Extra Time Type")]
        public KeyValueViewModel ExtraTimeType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Duration", "Numeric", false)]
        [LookUp("LookUps.Duration")]
        [DisplayName("Duration")]
        public KeyValueViewModel Duration { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "PriceSetting", "PriceSetting")]
        [DisplayName("Price Settigns")]
        public ServicePriceSettingViewModel PriceSetting { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employees", "Numeric", true)]
        [LookUp("LookUps.Employees")]
        [DisplayName("Staffs")]
        public List<KeyValueViewModel> Staffs { get; set; }
        

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ServiceDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ServiceMasterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ServiceDTO, ServiceMasterViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            return Mapper<ServiceDTO, ServiceMasterViewModel>.Map(dto as ServiceDTO);
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ServiceMasterViewModel, ServiceDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            return Mapper<ServiceMasterViewModel, ServiceDTO>.Map(this);
        }
    }
}
