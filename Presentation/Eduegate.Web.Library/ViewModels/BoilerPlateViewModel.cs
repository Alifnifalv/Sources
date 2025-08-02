using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class BoilerPlateViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("BoilerPlateID")]
        public long BoilerPlateID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("BoilerPlateName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Name { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Template")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Template { get; set; }

        public static BoilerPlateViewModel FromDTO(BoilerPlateDTO dto)
        {
            return new BoilerPlateViewModel() { 
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                BoilerPlateID = dto.BoilerPlateID,
                Name = dto.Name,
                Description = dto.Description,
                Template = dto.Template,
                TimeStamps = dto.TimeStamps
            };
        }

        public static BoilerPlateDTO ToDTO (BoilerPlateViewModel vm)
        {
            return new BoilerPlateDTO() { 
                BoilerPlateID = vm.BoilerPlateID,
                Name = vm.Name,
                Description = vm.Description,
                Template = vm.Template,
                CreatedBy = vm.CreatedBy,
                CreatedDate = vm.CreatedDate,
                UpdatedBy = vm.UpdatedBy,
                UpdatedDate = vm.UpdatedDate,
                TimeStamps = vm.TimeStamps
            };
        }

    }
}
