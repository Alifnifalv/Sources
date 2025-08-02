using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Admin;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Security;

namespace Eduegate.Web.Library.ViewModels.Security
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Claims", "CRUDModel.ViewModel.SecuritySettings.Claims")]
    [LocalizedDisplayName("CLAIM_TITLE")]
    public class SecurityClaimViewModel : BaseMasterViewModel
    {
        public SecurityClaimViewModel()
        {
            Claims = new List<KeyValueViewModel>();
        }

        public ClaimType ClaimType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("")]
        public string ClaimTypeCaption { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LazyLoad("", "Securities/Login/GetClaimsByType?claimType={{gridModel.ClaimType}}", "LookUps.Claims_{{gridModel.ClaimType}}", true,  dynamicDataSource:"GetClaims(gridModel.ClaimType)")]
        [LocalizedDisplayName("")]
        [Select2("Claims", "Numeric", true, "", false, "", false)]
        public List<KeyValueViewModel> Claims { get; set; }

        public static List<Security.SecurityClaimViewModel> GetSecurityVM(List<KeyValueDTO> claimTypes, List<ClaimDetailDTO> claims)
        {
            var VMs = new List<Security.SecurityClaimViewModel>();

            foreach (var type in claimTypes)
            {
                var securityVM = new Security.SecurityClaimViewModel()
                {
                    ClaimType = (ClaimType)Enum.Parse(typeof(ClaimType), type.Key),
                    ClaimTypeCaption = type.Value.ToString(),
                    Claims = new List<KeyValueViewModel>()
                };

                foreach (var claim in claims.Where(a => a.ClaimType == (ClaimType)Enum.Parse(typeof(ClaimType), type.Key)))
                {
                    securityVM.Claims.Add(new KeyValueViewModel() { Key = claim.ClaimIID.ToString(), Value = claim.ClaimName });
                }

                VMs.Add(securityVM);
            }

            return VMs;
        }

        public static List<Security.SecurityClaimViewModel> GetSecurityVM(List<KeyValueDTO> claimTypes, List<ClaimSetClaimMapDTO> claims)
        {
            var VMs = new List<Security.SecurityClaimViewModel>();

            foreach (var type in claimTypes)
            {
                var securityVM = new Security.SecurityClaimViewModel()
                {
                    ClaimType = (ClaimType)Enum.Parse(typeof(ClaimType), type.Key),
                    ClaimTypeCaption = type.Value.ToString(),
                    Claims = new List<KeyValueViewModel>()
                };

                foreach (var claim in claims.Where(a => a.ClaimTypeID == (ClaimType)Enum.Parse(typeof(ClaimType), type.Key)))
                {
                    securityVM.Claims.Add(new KeyValueViewModel() { Key = claim.ClaimIID.ToString(), Value = claim.ClaimName });
                }

                VMs.Add(securityVM);
            }

            return VMs;
        }
    }
}
