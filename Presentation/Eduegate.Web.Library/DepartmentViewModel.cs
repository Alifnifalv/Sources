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
    public class DepartmentViewModel : BaseMasterViewModel
    {
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Department ID")]
        public long DepartmentID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox)]

        [Required]
        [CustomDisplay("DepartmentName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string DepartmentName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.DepartmentStatus")]
        [CustomDisplay("Status")]
        public string DepartmentStatus { get; set; }
        public byte StatusID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public static List<DepartmentViewModel> FromDTO(List<DepartmentDTO> dtos)
        {
            var vms = new List<DepartmentViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static DepartmentViewModel FromDTO(DepartmentDTO dto)
        {
            Mapper<DepartmentDTO, DepartmentViewModel>.CreateMap();
            var mapper = Mapper<DepartmentDTO, DepartmentViewModel>.Map(dto);
            mapper.DepartmentStatus = dto.StatusID.ToString();
            mapper.CompanyID = dto.CompanyID;
            return mapper;
        }

        public static List<DepartmentDTO> ToDTO(List<DepartmentViewModel> vms)
        {
            var dtos = new List<DepartmentDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(ToDTO(vm));
            }

            return dtos;
        }

        public static DepartmentDTO ToDTO(DepartmentViewModel vm)
        {
            Mapper<DepartmentViewModel, DepartmentDTO>.CreateMap();
            var mapper = Mapper<DepartmentViewModel, DepartmentDTO>.Map(vm);
            mapper.StatusID = Convert.ToByte(vm.DepartmentStatus);
                       { 

            }
            mapper.CompanyID = vm.CompanyID;
            return mapper;
        }
    }
}
