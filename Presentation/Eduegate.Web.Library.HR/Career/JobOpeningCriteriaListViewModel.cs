using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;

namespace Eduegate.Web.Library.HR.Career
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "JobCriteria", "CRUDModel.ViewModel.JobCriteria")]
    [DisplayName("Job Criteria")]
    public class JobOpeningCriteriaListViewModel : BaseMasterViewModel
    {
        public JobOpeningCriteriaListViewModel()
        {

        }
        public long CriteriaID { get; set; }
        public long? JobID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.JobCriteriaTypes")]
        [DisplayName("Job Criteria")]
        public string JobCriteria { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Qualifications")]
        [DisplayName("Education Qualification")]
        public string EducationQualification { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Field Of Study")]
        public string FieldOfStudy { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.JobCriteria[0],CRUDModel.ViewModel.JobCriteria)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.JobCriteria[0],CRUDModel.ViewModel.JobCriteria)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
