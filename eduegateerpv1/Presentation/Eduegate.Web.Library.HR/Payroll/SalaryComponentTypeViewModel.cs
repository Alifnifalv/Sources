using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    public class SalaryComponentTypeViewModel : BaseMasterViewModel
    {
        ///[Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
       /// [DisplayName("Salary Component Type ID")]
        public byte SalaryComponentTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50,ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("TypeName")]
        public string TypeName { get; set; }



        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SalaryComponentTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryComponentTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SalaryComponentTypeDTO, SalaryComponentTypeViewModel>.CreateMap();
            var vm = Mapper<SalaryComponentTypeDTO, SalaryComponentTypeViewModel>.Map(dto as SalaryComponentTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SalaryComponentTypeViewModel, SalaryComponentTypeDTO>.CreateMap();
            var dto = Mapper<SalaryComponentTypeViewModel, SalaryComponentTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryComponentTypeDTO>(jsonString);
        }
    }

}
