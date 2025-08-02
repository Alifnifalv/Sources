using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using Eduegate.Framework.Enums;
using System;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AgendaTaskAttachMap", "CRUDModel.ViewModel.AgendaTaskAttachMap")]
    [DisplayName("Types")]
    public class AgendaTastAttachmentViewModel :BaseMasterViewModel
    {
        public AgendaTastAttachmentViewModel()
        {
            TaskType = new KeyValueViewModel();
        }

        public long AgendaTaskMapIID { get; set; }
        public long? AgendaTopicMapID { get; set; }

        //Task attachment
        public long AgendacTaskAttachmentMapIID { get; set; }
        public long? AgendaTaskMapID { get; set; }
        public long? AgendaID { get; set; }

        ////[MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=true")]
        //[CustomDisplay("TaskName")]
        //public string Task { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" TaskType", "Numeric", false, "")]
        [LookUp("LookUps.LessonPlanTaskTypes")]
        [CustomDisplay("TaskType")]
        public KeyValueViewModel TaskType { get; set; }
        public byte? TaskTypeID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("StartDate")]
        public string Date1String { get; set; }
        public DateTime? StartDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("SubmissionDate")]
        public string Date2String { get; set; }
        public DateTime? EndDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent)]
        [CustomDisplay("Attachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ContentFileIID", "")]
        public long? ContentFileIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("")]
        public string ContentFileName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "TaskAttachment")]
        //[FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ProfileUrl")]
        //[DisplayName("")]
        //public string AgendaAttachment { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.AgendaTaskAttachMap[0], CRUDModel.ViewModel.AgendaTaskAttachMap)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.AgendaTaskAttachMap[0], CRUDModel.ViewModel.AgendaTaskAttachMap)'")]
        [DisplayName("-")]
        public string Remove { get; set; }


    }
}
