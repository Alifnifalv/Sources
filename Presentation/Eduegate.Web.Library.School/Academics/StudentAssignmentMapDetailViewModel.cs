using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AssignmentDetails", "CRUDModel.ViewModel.AssignmentDetails")]
    [DisplayName("Assignment Details")]
    public class StudentAssignmentMapDetailViewModel : BaseMasterViewModel
    {
        public StudentAssignmentMapDetailViewModel()
        {
        }
        public long StudentAssignmentMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        //[Select2("QuestionGroups", "Numeric", false, "")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=QuestionGroups", "LookUps.QuestionGroups")]
        //[LookUp("LookUps.QuestionGroups")]
        [CustomDisplay("Student")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("Attachment")]
        [FileUploadInfo("Content/UploadContents", Framework.Enums.EduegateImageTypes.Documents, "ProfileUrl", "")]
        public string ImageName { get; set; }
        public string ProfileUrl { get; set; }
        public long? ContentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }
    }   
}
