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
    public class LeaveTypeViewModel : BaseMasterViewModel
    {
        public int LeaveTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [Required]
        [CustomDisplay("Description")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(3, ErrorMessage = "Maximum Length should be within 3!")]
        [CustomDisplay("MaxDaysAllowed")]
        public int? MaxDaysAllowed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsCarryForward")]
        public bool? IsCarryForward { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsLeaveWithoutPay")]
        public bool? IsLeaveWithoutPay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Allownegativebalance")]
        public bool? AllowNegativeBalance { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Includeholidaywithinleaves")]
        public bool? IncludeHolidayWithinLeaves { get; set; }

        //public byte StatusID { get; set; }
        //public Nullable<int> CompanyID { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LeaveTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LeaveTypeDTO, LeaveTypeViewModel>.CreateMap();
            var lvDtO = dto as LeaveTypeDTO;
            var vm = Mapper<LeaveTypeDTO, LeaveTypeViewModel>.Map(dto as LeaveTypeDTO);
            //vm.LeaveType = lvDtO.LeaveTypeID.ToString();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LeaveTypeViewModel, LeaveTypeDTO>.CreateMap();
            var dto = Mapper<LeaveTypeViewModel, LeaveTypeDTO>.Map(this);
            //dto.LeaveTypeID = string.IsNullOrEmpty(this.LeaveType) ? (int?)null : int.Parse(this.LeaveType);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveTypeDTO>(jsonString);
        }
    }
}
