using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AgeCriteriaMap", "CRUDModel.ViewModel.AgeCriteriaMap")]
    [DisplayName("Age Citeria")]
    public class AgeCriteriaMapViewModel: BaseMasterViewModel
    {
        public AgeCriteriaMapViewModel()
        {
            IsActive = true;
        }

        public long AgeCriteriaIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("BirthFrom")]
        public string BirthFromString { get; set; }

        public System.DateTime? BirthFrom { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("BirthTo")]
        public string BirthToString { get; set; }

        public System.DateTime? BirthTo { get; set; }


        //[Required]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MinAge")]
        public decimal? MinAge { get; set; }

        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MaxAge")]
        public decimal? MaxAge { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox,"small-col-width","textleft")]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.AgeCriteriaMap[0], CRUDModel.ViewModel.AgeCriteriaMap)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.AgeCriteriaMap[0],CRUDModel.ViewModel.AgeCriteriaMap)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
