using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using System;
using System.Globalization;


namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AgendaList", "CRUDModel.ViewModel")]
    [DisplayName("AgendaList")]
    public class AgendaListViewModel : BaseMasterViewModel
    {
        public AgendaListViewModel()
        {
           
            AgendaTaskAttachMap = new List<AgendaTaskListViewModel>() { new AgendaTaskListViewModel() };
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            Date1String = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
        }

        public long AgendaIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public string Date1String { get; set; }
        public System.DateTime? Date1 { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false, "")]
        [DisplayName("Academic Year")]
        [LookUp("LookUps.AcademicYear")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "ClassChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [DisplayName("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "")]
        [DisplayName("Section")]
        [LookUp("LookUps.Section")]
        public KeyValueViewModel Section { get; set; }
        public int? SectionID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [DisplayName("Subject")]
        [LookUp("LookUps.Subject")]
        public KeyValueViewModel Subject { get; set; }
        public int? SubjectID { get; set; }

  
       

        public string Topic { get; set; }

        public string LectureCode { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<AgendaTaskListViewModel> AgendaTaskAttachMap { get; set; }

      
    }
}