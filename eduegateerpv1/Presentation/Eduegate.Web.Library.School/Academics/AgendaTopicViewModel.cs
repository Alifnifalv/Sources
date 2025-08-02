using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using Eduegate.Framework.Enums;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AgendaTopics", "CRUDModel.ViewModel.AgendaTopics")]
    [DisplayName("Agenda Topics")]
    public class AgendaTopicViewModel :BaseMasterViewModel
    {
        public AgendaTopicViewModel()
        {
        }

        public long AgendaTopicMapIID { get; set; }
        public long? AgendaID { get; set; }

        //[Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ChapterName")]
        public string LectureCode { get; set; }

        //[Required]
        //[MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Topic { get; set; }
    }
}
