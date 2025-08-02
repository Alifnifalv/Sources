using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Security;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Security
{
    public class ClaimSetViewModel : BaseMasterViewModel
    {
        public ClaimSetViewModel()
        {
            //Claims = new List<KeyValueViewModel>();
            ClaimSets = new List<KeyValueViewModel>();
            Claims = new List<SecurityClaimViewModel2>() { new SecurityClaimViewModel2() };
        }

       // [Required]
       // [ControlType(Framework.Enums.ControlTypes.Label)]
       // [LocalizedDisplayName("CLAIMSET_ID")]
        public long ClaimSetIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ClaimSetName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string ClaimSetName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<SecurityClaimViewModel2> Claims { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.ClaimSets")]
        [Select2("ClaimSets", "Numeric", true)]
        [CustomDisplay("ClaimSets")]
        public List<KeyValueViewModel> ClaimSets { get; set; }

        public static ClaimSetDTO ToDTO(ClaimSetViewModel vm)
        {
            if (vm != null)
            {
                var claimSetDTO = new ClaimSetDTO()
                {
                    ClaimSetIID = vm.ClaimSetIID,
                    ClaimSetName = vm.ClaimSetName,
                    CreatedBy = vm.CreatedBy,
                    CreatedDate = vm.CreatedDate,
                    UpdatedBy = vm.UpdatedBy,
                    UpdatedDate = vm.UpdatedDate,
                    TimeStamps = vm.TimeStamps
                };

                if (vm.Claims != null)
                {
                    claimSetDTO.Claims = new List<ClaimSetClaimMapDTO>();

                    foreach (var securityClaim in vm.Claims)
                    {
                        foreach (var claim in securityClaim.Claims)
                        {
                            claimSetDTO.Claims.Add(new ClaimSetClaimMapDTO() { ClaimIID = long.Parse(claim.Key), ClaimName = claim.Value });
                        }
                    }
                }

                claimSetDTO.ClaimSets = new List<ClaimSetClaimSetMapDTO>();
                if (vm.ClaimSets != null && vm.ClaimSets.Count > 0)
                {
                    foreach (var claimSets in vm.ClaimSets)
                    {
                        var claimSetMapDTO = new ClaimSetClaimSetMapDTO();
                        claimSetMapDTO.ClaimSetIID = Convert.ToInt32(claimSets.Key);
                        claimSetDTO.ClaimSets.Add(claimSetMapDTO);
                    }
                }
                return claimSetDTO;
            }
            else
                return new ClaimSetDTO();
        }

        public static ClaimSetViewModel FromDTO(ClaimSetDTO dto, List<KeyValueDTO> claimTypes)
        {
            var vm = FromDTO(dto);
            vm.Claims = SecurityClaimViewModel2.GetSecurityVM(claimTypes, dto.Claims);
            return vm;
        }

        public static ClaimSetViewModel FromDTO(ClaimSetDTO dto)
        {
            if (dto != null)
            {
                var claimSet = new ClaimSetViewModel()
                {
                    ClaimSetIID = dto.ClaimSetIID,
                    ClaimSetName = dto.ClaimSetName,
                    CreatedBy = dto.CreatedBy,
                    CreatedDate = dto.CreatedDate,
                    UpdatedBy = dto.UpdatedBy,
                    UpdatedDate = dto.UpdatedDate,
                    TimeStamps = dto.TimeStamps
                };

                claimSet.ClaimSets = new List<KeyValueViewModel>();

                if (dto.ClaimSets != null && dto.ClaimSets.Count > 0)
                {
                    foreach (var claimSetMap in dto.ClaimSets)
                    {
                        var claimSetKeyValue = new KeyValueViewModel();
                        claimSetKeyValue.Key = claimSetMap.ClaimSetIID.ToString();
                        claimSetKeyValue.Value = claimSetMap.ClaimSetName;
                        claimSet.ClaimSets.Add(claimSetKeyValue);
                    }
                }

                return claimSet;
            }
            else
                return new ClaimSetViewModel();
        }
    }
}
