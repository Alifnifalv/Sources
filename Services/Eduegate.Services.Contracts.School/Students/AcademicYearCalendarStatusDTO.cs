using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public  class AcademicYearCalendarStatusDTO
    {
       

        [DataMember]
        public byte AcademicYearCalendarStatusID { get; set; }

        [DataMember]
        public string Description { get; set; }

     
    }
}
