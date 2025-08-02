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

namespace Eduegate.Web.Library.School.CounselorHub
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StudentList", "CRUDModel.ViewModel.StudentList")]
    [DisplayName("Student List")]
    public class CounselorHubStudentListViewModel : BaseMasterViewModel
    {
        public CounselorHubStudentListViewModel()
        {

        }

        public long CounselorHubMapIID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Label, "alignleft")]
        [CustomDisplay("Student")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }
    }
}
