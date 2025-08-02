using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Hostels", "CRUDModel.ViewModel.Hostels")]
    [DisplayName("Hostels")]
    public class HostelViewModel : BaseMasterViewModel
    {
        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Hostel")]
        [LookUp("LookUps.Hostel")]
        public string Hostel { get; set; }

        public int HostelID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Hostel Room")]
        [LookUp("LookUps.HostelRoom")]
        public string HostelRoom { get; set; }

        public int RoomID { get; set; }

    }
}
