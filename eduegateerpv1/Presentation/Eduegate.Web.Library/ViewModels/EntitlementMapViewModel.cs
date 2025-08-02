using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "EntityTypeEntitlement", "CRUDModel.ViewModel.Entitlements.EntitlementMaps")]
    [DisplayName("")]
    public partial class EntitlementMapViewModel : BaseMasterViewModel
    {
        public byte EntitlementID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Entitlement")]
        public string EntitlementName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Enable")]
        public bool IsLocked { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox,  "medium-col-width textright")]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [DisplayName("Amount")]
        public string EntitlementAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("Days")]
        public string EntitlementDays { get; set; }

        public static EntitlementMapDTO ToDTO(EntitlementMapViewModel vm)
        {
            Mapper<EntitlementMapViewModel, EntitlementMapDTO>.CreateMap();
            return Mapper<EntitlementMapViewModel, EntitlementMapDTO>.Map(vm);
        }

        public static EntitlementMapViewModel ToVM(EntitlementMapDTO dto)
        {
            Mapper<EntitlementMapDTO, EntitlementMapViewModel>.CreateMap();
            return Mapper<EntitlementMapDTO, EntitlementMapViewModel>.Map(dto);
        }

        public static List<EntitlementMapViewModel> DefaultData(List<KeyValueDTO> values)
        {
            var data = new List<EntitlementMapViewModel>();

            foreach (var value in values)
            {
                data.Add(new EntitlementMapViewModel()
                {
                    EntitlementName = value.Value,
                    EntitlementID = byte.Parse(value.Key)
                });
            }

            return data;
        }
    }
}
