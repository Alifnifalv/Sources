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
    public class ServiceGroupViewModel : BaseMasterViewModel
    {
        public ServiceGroupViewModel()
        {

        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Service Group ID")]
        public long ServiceAvailableID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string GroupName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ServiceGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ServiceGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ServiceGroupDTO, ServiceGroupViewModel>.CreateMap();
            return Mapper<ServiceGroupDTO, ServiceGroupViewModel>.Map(dto as ServiceGroupDTO);
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ServiceGroupViewModel, ServiceGroupDTO>.CreateMap();
            return Mapper<ServiceGroupViewModel, ServiceGroupDTO>.Map(this);
        }
    }
}
