using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.HR.Employee;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.HR.Employee
{
    public class EmployeeRoleViewModel : BaseMasterViewModel
    {
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Employee Role ID")]
        public int EmployeeRoleID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("EmployeeRoleName")]
        public string EmployeeRoleName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeeRoleDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeRoleViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeeRoleDTO, EmployeeRoleViewModel>.CreateMap();
            var vm = Mapper<EmployeeRoleDTO, EmployeeRoleViewModel>.Map(dto as EmployeeRoleDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmployeeRoleViewModel, EmployeeRoleDTO>.CreateMap();
            var dto = Mapper<EmployeeRoleViewModel, EmployeeRoleDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeRoleDTO>(jsonString);
        }
    }
}
