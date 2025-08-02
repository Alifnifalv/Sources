using System.ComponentModel;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.PageRenderer
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "parameter", "gridModel.Paramters")]
    [DisplayName("")]
    public class BoilerplateParameterViewModel : BaseMasterViewModel
    {
        public int BoilerPlateMapParameterIID { get; set; }

        public long BoilerPlateMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Name")]
        public string ParameterName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [MaxLength(150)]
        [CustomDisplay("Value")]
        public string ParameterValue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        public static BoilerplateParameterViewModel FromDTO(BoilerPlateParameterDTO dto)
        {
            return new BoilerplateParameterViewModel() { 
                ParameterValue = "",
                ParameterName = dto.ParameterName,
                Description = dto.Description,
            };
        }
    }
}
