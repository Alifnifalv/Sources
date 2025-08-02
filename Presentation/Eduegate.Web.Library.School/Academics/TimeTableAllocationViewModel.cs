using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Academics
{
    public class TimeTableAllocationViewModel
    {
        //public TimeTableAllocationViewModel()
        //{
        //    Class = new KeyValueViewModel();
        //    Section = new KeyValueViewModel();
        //}

        public long TimeTableAllocationIID { get; set; }

        public int? TimeTableID { get; set; }

        public int? WeekDayID { get; set; }

        public int? ClassTimingID { get; set; }

        public int? SubjectID { get; set; }
        public KeyValueViewModel Subject { get; set; }

        public List<long> StaffID { get; set; }
        public List<KeyValueViewModel> StaffList { get; set; }
        public KeyValueViewModel Employee { get; set; }
        public int? SectionID { get; set; }
        public KeyValueViewModel Section { get; set; }

        public int? ClassId { get; set; }
        public KeyValueViewModel StudentClass { get; set; }
        public KeyValueViewModel ClassTiming { get; set; }
        public KeyValueViewModel WeekDay { get; set; }

        public int? TimeTableMasterID { get; set; }

        public string AllocatedDateString { get; set; }

        public byte? IsGenerate { get; set; }

        public bool? IsEnteredManually { get; set; }
    }
}
