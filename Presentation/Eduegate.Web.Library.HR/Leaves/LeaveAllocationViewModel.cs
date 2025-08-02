using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Leaves
{
    public class LeaveAllocationViewModel : BaseMasterViewModel
    {
        public long LeaveAllocationIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Leave Group")]
        [LookUp("LookUps.LeaveGroupList")]
        public string LeaveGroup { get; set; }
        public int? LeaveGroupID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("LeaveType")]
        [LookUp("LookUps.LeaveType")]
        public string LeaveType { get; set; }
        public int? LeaveTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateFrom")]
        public string DateFromString { get; set; }
        public System.DateTime? DateFrom { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateTo")]
        public string DateToString { get; set; }
        public System.DateTime? DateTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [CustomDisplay("AllocatedLeaves")]
        public double? AllocatedLeaves { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditor)]
        [CustomDisplay("Description")]
        [StringLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        public string Description { get; set; }

        //public virtual LeaveTypeViewModel LeaveType { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LeaveAllocationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveAllocationViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LeaveAllocationDTO, LeaveAllocationViewModel>.CreateMap();
            var lvDtO = dto as LeaveAllocationDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<LeaveAllocationDTO, LeaveAllocationViewModel>.Map(dto as LeaveAllocationDTO);
            vm.LeaveGroup = lvDtO.LeaveGroupID.ToString();
            vm.LeaveType = lvDtO.LeaveTypeID.ToString();
            vm.DateFromString = lvDtO.DateFrom.HasValue ? lvDtO.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.DateToString = lvDtO.DateTo.HasValue ? lvDtO.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LeaveAllocationViewModel, LeaveAllocationDTO>.CreateMap();
            var dto = Mapper<LeaveAllocationViewModel, LeaveAllocationDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.LeaveGroupID = string.IsNullOrEmpty(this.LeaveGroup) ? (int?)null : int.Parse(this.LeaveGroup);
            dto.LeaveTypeID = string.IsNullOrEmpty(this.LeaveType) ? (int?)null : int.Parse(this.LeaveType);
            dto.DateFrom = string.IsNullOrEmpty(this.DateFromString) ? (DateTime?)null : DateTime.ParseExact(this.DateFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.DateTo = string.IsNullOrEmpty(this.DateToString) ? (DateTime?)null : DateTime.ParseExact(this.DateToString, dateFormat, CultureInfo.InvariantCulture);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveAllocationDTO>(jsonString);
        }
    }
}