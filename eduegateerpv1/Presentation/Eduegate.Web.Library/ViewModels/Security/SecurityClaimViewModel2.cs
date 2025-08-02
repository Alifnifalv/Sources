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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Claims", "CRUDModel.ViewModel.Claims")]
    [LocalizedDisplayName("CLAIM_TITLE")]
    public class SecurityClaimViewModel2 : SecurityClaimViewModel
    {
        public static new List<Security.SecurityClaimViewModel2> GetSecurityVM(List<KeyValueDTO> claimTypes, List<ClaimSetClaimMapDTO> claims)
        {
            var VMs = new List<Security.SecurityClaimViewModel2>();

            foreach (var type in claimTypes)
            {
                var securityVM = new Security.SecurityClaimViewModel2()
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

        public static new List<Security.SecurityClaimViewModel2> GetSecurityVM(List<KeyValueDTO> claimTypes, List<ClaimDetailDTO> claims)
        {
            var VMs = new List<Security.SecurityClaimViewModel2>();

            foreach (var type in claimTypes)
            {
                var securityVM = new Security.SecurityClaimViewModel2()
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
    }
}
