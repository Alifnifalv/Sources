using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library
{
    public class BranchGroupViewModel : BaseMasterViewModel
    {
       // [Required]
       // [ControlType(Framework.Enums.ControlTypes.Label)]
      //  [DisplayName("Branch Group ID")]
        public long BranchGroupIID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox)]

        [Required]
        [CustomDisplay("GroupName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string GroupName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.BranchGroupStatus")]
        [CustomDisplay("Status")]
        public string BranchGroupStatus { get; set; } 
        public byte StatusID { get; set; }

        public static List<BranchGroupViewModel> FromDTO(List<BranchGroupDTO> dtos)
        {
            var vms = new List<BranchGroupViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static BranchGroupViewModel FromDTO(BranchGroupDTO dto)
        {
            Mapper<BranchGroupDTO, BranchGroupViewModel>.CreateMap();
            var mapper = Mapper<BranchGroupDTO, BranchGroupViewModel>.Map(dto);
            mapper.BranchGroupStatus = dto.StatusID.ToString();
            return mapper;
        }

        public static List<BranchGroupDTO> ToDTO(List<BranchGroupViewModel> vms)
        {
            var dtos = new List<BranchGroupDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(ToDTO(vm));
            }

            return dtos;
        }

        public static BranchGroupDTO ToDTO(BranchGroupViewModel vm)
        {
            Mapper<BranchGroupViewModel, BranchGroupDTO>.CreateMap();
            var mapper = Mapper<BranchGroupViewModel, BranchGroupDTO>.Map(vm);
            mapper.StatusID = Convert.ToByte(vm.BranchGroupStatus);
            return mapper;
        }
    }
}
