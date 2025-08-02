using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Salon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Salon
{
    public class ServiceAvailableViewModel : BaseMasterViewModel
    {
        public ServiceAvailableViewModel()
        {

        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Service available ID")]
        public long ServiceAvailableID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string Description { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ServiceAvailableDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ServiceAvailableViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ServiceAvailableDTO, ServiceAvailableViewModel>.CreateMap();
            return Mapper<ServiceAvailableDTO, ServiceAvailableViewModel>.Map(dto as ServiceAvailableDTO);
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ServiceAvailableViewModel, ServiceAvailableDTO>.CreateMap();
            return Mapper<ServiceAvailableViewModel, ServiceAvailableDTO>.Map(this);
        }
    }
}
