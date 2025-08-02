using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class DepartmentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long DepartmentID { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

        [DataMember]
        public string Logo { get; set; }

        [DataMember]
        public byte? StatusID { get; set; }

        [DataMember]
        public string DepartmentNumber { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string DepartmentCode { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }
    }
}
