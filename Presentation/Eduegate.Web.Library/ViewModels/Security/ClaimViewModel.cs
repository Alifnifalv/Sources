using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Security;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Security
{
    public class ClaimViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CLAIM_ID")]
        public long ClaimIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CLAIM_NAME")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string ClaimName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ResourceName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string ResourceName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.ClaimType")]
        [CustomDisplay("CLAIM_TYPE")]
        public string ClaimType { get; set; }

        public ClaimType ClaimTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Rights")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Rights { get; set; }

        public static ClaimDTO ToDTO(ClaimViewModel vm)
        {
            if (vm != null)
            {
                return new ClaimDTO()
                {
                    ClaimIID = vm.ClaimIID,
                    ClaimName = vm.ClaimName,
                    ClaimTypeID = vm.ClaimType.IsNotNull() ? (ClaimType)Convert.ToInt32(vm.ClaimType) : 0,
                    ResourceName = vm.ResourceName,
                    Rights = vm.Rights,
                    CreatedBy = vm.CreatedBy,
                    CreatedDate = vm.CreatedDate,
                    UpdatedBy = vm.UpdatedBy,
                    UpdatedDate = vm.UpdatedDate,
                    TimeStamps = vm.TimeStamps
                };
            }
            else
                return new ClaimDTO();
        }

        public static List<ClaimViewModel> FromDTO(List<ClaimDTO> dtos)
        {
            var vms = new List<ClaimViewModel>();

            foreach(var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static ClaimViewModel FromDTO(ClaimDTO dto)
        {
            if (dto != null)
            {
                 return new ClaimViewModel()
                {
                    ClaimIID = dto.ClaimIID,
                    ClaimName = dto.ClaimName,
                    ClaimType = ((int)dto.ClaimTypeID).ToString(),
                    ResourceName = dto.ResourceName,
                    Rights = dto.Rights,
                    CreatedBy = dto.CreatedBy,
                    CreatedDate = dto.CreatedDate,
                    UpdatedBy = dto.UpdatedBy,
                    UpdatedDate = dto.UpdatedDate,
                    TimeStamps = dto.TimeStamps
                };
            }
            else
                return new ClaimViewModel();
        }

        public static List<KeyValueViewModel> ToKeyValueVM(List<ClaimDTO> dtos)
        {
            var vMs = new List<KeyValueViewModel>();
            foreach (var dto in dtos)
            {
                vMs.Add(new KeyValueViewModel() { Key = dto.ClaimIID.ToString(), Value = dto.ClaimName });
            }

            return vMs;
        }
    }
}
