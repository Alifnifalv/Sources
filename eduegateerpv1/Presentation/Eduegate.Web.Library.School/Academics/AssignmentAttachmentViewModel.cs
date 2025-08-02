using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework.Enums;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Attachments", "CRUDModel.ViewModel.Attachments")]
    [DisplayName("Attachments")]
    public class AssignmentAttachmentViewModel : BaseMasterViewModel
    {
        public long AssignmentAttachmentIID { get; set; }

        public long? AssignmentID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent)]
        [CustomDisplay("Attachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ContentFileIID", "")]
        public long? ContentFileIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("FileName")]
        public string ContentFileName { get; set; }


        //[Required]
        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        //[Required]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Notes")]
        public string Notes { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Attachments[0], CRUDModel.ViewModel.Attachments)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.Attachments[0],CRUDModel.ViewModel.Attachments)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AssignmentAttachmentMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssignmentAttachmentViewModel>(jsonString);
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssignmentAttachmentMapDTO>(jsonString);
        }
    }
}