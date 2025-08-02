using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using Eduegate.Framework.Enums;
using System;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AgendaTaskListViewModel", "CRUDModel.ViewModel.AgendaTaskListViewModel")]
    [DisplayName("Task")]
    public class AgendaTaskListViewModel : BaseMasterViewModel
    {
     
        public long AgendaTaskMapIID { get; set; }
        public long? AgendaTopicMapID { get; set; }

      
        public long? AgendaTaskMapID { get; set; }


        //[MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Task Name")]
        public string Task { get; set; }
        

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Start Date")]
        public string StartDateString { get; set; }
        public DateTime? StartDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("End Date")]
        public string EndDateString { get; set; }
        public DateTime? EndDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [DisplayName("Attachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "UploadFile", "")]
        public string UploadFile { get; set; }
        public long? AttachmentReferenceID { get; set; }

        public string AttachmentNotes { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentDescription { get; set; }

    }
}
