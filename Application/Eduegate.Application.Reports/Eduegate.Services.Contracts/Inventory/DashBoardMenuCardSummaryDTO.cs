using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Inventory
{
    [DataContract]
    public class DashBoardMenuCardSummaryDTO
    {
        [DataMember]
        public string Field_Name { get; set; } 

        [DataMember]
        public decimal? Field_Value { get; set; }

        [DataMember]
        public int? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string bg_colour { get; set; }

        [DataMember]
        public string icon { get; set; }
    }
}
