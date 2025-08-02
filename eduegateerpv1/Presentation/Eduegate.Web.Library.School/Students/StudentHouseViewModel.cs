using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Studenthouse", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class StudentHouseViewModel : BaseMasterViewModel
    {
        ///[Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
        ///[DisplayName("Student House ID")]
        public int  StudentHouseID { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Description")]
        public string  Description { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentHouseDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentHouseViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentHouseDTO, StudentHouseViewModel>.CreateMap();
            return Mapper<StudentHouseDTO, StudentHouseViewModel>.Map(dto as StudentHouseDTO);
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentHouseViewModel, StudentHouseDTO>.CreateMap();
            return Mapper<StudentHouseViewModel, StudentHouseDTO>.Map(this);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentHouseDTO>(jsonString);
        }
    }
}

