using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassSectionShiftingDTO
    {
        public ClassSectionShiftingDTO()
        {
             ToShiftSectionIDs = new List<KeyValueDTO>();
             ClassSectionStudentDTO = new List<ClassSectionStudentDTO>();
           
        }
        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public string ClassName { get; set; }
        [DataMember]
        public List<KeyValueDTO> ToShiftSectionIDs { get; set; }
        [DataMember]
        public int? FromShiftSectionID { get; set; }
        [DataMember]
        public int? ToShiftSectionID { get; set; }
        [DataMember]
        public string SectionName { get; set; }
        [DataMember]
        public long StudentID { get; set; }
      
        [DataMember]
        public List<ClassSectionStudentDTO> ClassSectionStudentDTO { get; set; }

    }

    [DataContract]
    public class ClassSectionStudentDTO
    {
        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public long StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }
    }
}