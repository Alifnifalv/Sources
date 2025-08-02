using Eduegate.Domain;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SubjectKnowledge", "CRUDModel.ViewModel.SubjectKnowledge")]
    [DisplayName("Subject Knowledge")]
    public class SubjectKnowledgeViewModel : BaseMasterViewModel
    {
        public long SubjectKnowledgeIID { get; set; }

        public long? StudentID { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Countries", "Numeric", false, "")]
        [LookUp("LookUps.LessonSubjectKnowledges")]
        [CustomDisplay("Options")]
        public KeyValueViewModel Nationality { get; set; }
        public int? NationalityID { get; set; }

        public int? IndianNationalityID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "")]
        [LookUp("LookUps.RatingScales")]
        [CustomDisplay("RatingScales")]
        public int? SubjectID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.SubjectKnowledge[0], CRUDModel.ViewModel.SubjectKnowledge)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.SubjectKnowledge[0],CRUDModel.ViewModel.SubjectKnowledge)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Additional Comments")]
        public string FormNo { get; set; }

    }
}
