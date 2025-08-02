using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Leaves;
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
namespace Eduegate.Web.Library.HR.Leaves
{
    public class LeaveBlockViewModel : BaseMasterViewModel
    {
        public long LeaveBlockListIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Department")]
        [LookUp("LookUps.Department")]
        public string Department { get; set; }
        public long? DepartmentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        [StringLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Description { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LeaveBlockDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveBlockViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LeaveBlockDTO, LeaveBlockViewModel>.CreateMap();
            var lvDtO = dto as LeaveBlockDTO; 
            var vm = Mapper<LeaveBlockDTO, LeaveBlockViewModel>.Map(dto as LeaveBlockDTO);
            vm.Department = lvDtO.DepartmentID.ToString();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LeaveBlockViewModel, LeaveBlockDTO>.CreateMap();
            var dto = Mapper<LeaveBlockViewModel, LeaveBlockDTO>.Map(this);
            dto.DepartmentID = string.IsNullOrEmpty(this.Department) ? (long?)null : long.Parse(this.Department);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveBlockDTO>(jsonString);
        }
    }
}
