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
using System.Globalization;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "GradeTypes", "CRUDModel.ViewModel.GradeTypes")]
    [DisplayName("Grade Types")]
    public class MarkGradeMapViewModel: BaseMasterViewModel
    {
        public MarkGradeMapViewModel()
        {
           
            //IsRowSelected = true;
          
        }

        public int? MarkGradeID { get; set; }

        public long MarksGradeMapIID { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "Maximum Length 3!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        [CustomDisplay("GradeName")]
        public string GradeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsPercentage")]
        public bool? IsPercentage { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "Maximum Length 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        [CustomDisplay("GradeFrom")]
        public decimal? GradeFrom { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "Maximum Length 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        [CustomDisplay("GradeTo")]
        public decimal? GradeTo { get; set; }

        //[Required]
        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Notes")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.GradeTypes[0], CRUDModel.ViewModel.GradeTypes)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.GradeTypes[0],CRUDModel.ViewModel.GradeTypes)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
