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
using System;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AssignmentView", "CRUDModel.ViewModel.AssignmentView")]
    [DisplayName("Assignment")]
    public class AssignmentViewViewModel : BaseMasterViewModel
    {
        public AssignmentViewViewModel()
        {
            StudentClass = new KeyValueViewModel();
            Section = new KeyValueViewModel();
            Subject = new KeyValueViewModel();
            Academic = new KeyValueViewModel();
            AssignmentType = new KeyValueViewModel();
            AssignmentStatus = new KeyValueViewModel();
            Attachments = new List<AssignmentAttachmentViewModel>() { new AssignmentAttachmentViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        public int AssignmentIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [DisplayName("Academic Year")]
        public KeyValueViewModel Academic { get; set; }

        public int? AcademicYearID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AssignmentType", "Numeric", false, "")]
        [LookUp("LookUps.AssignmentType")]
        [DisplayName("Assignment Type")]
        public KeyValueViewModel AssignmentType { get; set; }

        public int? AssignmentTypeId { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "")]
        [LookUp("LookUps.Classes")]
        [DisplayName("Class")]
        public KeyValueViewModel StudentClass { get; set; }

        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "")]
        [DisplayName("Section")]
        [LookUp("LookUps.Section")]
        public KeyValueViewModel Section { get; set; }

        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [DisplayName("Subject")]
        [LookUp("LookUps.Subject")]
        public KeyValueViewModel Subject { get; set; }

        public int? SubjectID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date Of Submission")]
        public string SubmissionDateString { get; set; }
        public System.DateTime? SubmissionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Start Date")]
        public string StartDateString { get; set; }
        public System.DateTime? StartDate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Freeze Date")]
        public string FreezeDateString { get; set; }
        public System.DateTime? FreezeDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine28 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Title")]
        public string Title { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AssignmentStatus", "Numeric", false, "")]
        [DisplayName("Assignment Status")]
        [LookUp("LookUps.AssignmentStatus")]
        public KeyValueViewModel AssignmentStatus { get; set; }

        public int? AssignmentStatusId { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine21 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditor)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Active")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine20 { get; set; }

        public string CreatedDateString { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Attachments")]
        public List<AssignmentAttachmentViewModel> Attachments { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AssignmentDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssignmentViewModel>(jsonString);
        }


    }
}
