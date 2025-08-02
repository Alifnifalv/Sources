using System.Collections.Generic;
using System.ComponentModel;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.PageRenderer
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "boilerPlate", "CRUDModel.ViewModel.BoilerPlates")]
    [DisplayName("")]
    public class BoilerPlateGridViewModel : BaseMasterViewModel
    {
        public BoilerPlateGridViewModel()
        {
            Paramters = new List<BoilerplateParameterViewModel>();
        }

        public long BoilerPlateID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Template { get; set; }
        public List<KeyValueViewModel> RuntimeParameters { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width")]
        [CustomDisplay("Serial")]
        public int SerialNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("BoilerPlate", "String", false, onSelectChangeEvent: "OnChangeBoilerPlate($select,$index);")]
        [CustomDisplay("Boilerplate")]
        [LookUp("LookUps.Boilerplate")]
        public KeyValueViewModel BoilerPlate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width", "", "", "ng-disabled='CRUDModel.ViewModel.HasReferenceID' ng-init='gridModel.ReferenceID=CRUDModel.ViewModel.ReferenceID'", true)]
        [MaxLength(127)]
        [CustomDisplay("ReferenceID")]
        
        public long? ReferenceID { get; set; }

        public string ReferenceIDName { get; set; }

        //public bool ReferenceIDRequired { get; set; }

        public long BoilerplateMapIID { get; set; }

        public long PageID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        [CustomDisplay("Parameter")]
        public List<BoilerplateParameterViewModel> Paramters { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width addremovebutton", "ng-click='RemoveGridRow($index, ModelStructure.BoilerPlates[0], CRUDModel.ViewModel.BoilerPlates)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width addremovebutton", "ng-click='InsertGridRow($index, ModelStructure.BoilerPlates[0], CRUDModel.ViewModel.BoilerPlates)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        public static BoilerPlateGridViewModel FromDTO(BoilerPlateDTO dto)
        {
            Mapper<BoilerPlateDTO, BoilerPlateGridViewModel>.CreateMap();
            var mapper = Mapper<BoilerPlateDTO, BoilerPlateGridViewModel>.Map(dto);
            mapper.Paramters = new List<BoilerplateParameterViewModel>();
            mapper.BoilerPlate = new KeyValueViewModel() { Key = dto.BoilerPlateID.ToString(), Value = dto.Name };    

            foreach (var param in dto.BoilerPlateParameters)
            {
                mapper.Paramters.Add(new BoilerplateParameterViewModel() { ParameterName = param.ParameterName, ParameterValue = "", Description = param.Description });
            }

            return mapper;
        }
    }
}
