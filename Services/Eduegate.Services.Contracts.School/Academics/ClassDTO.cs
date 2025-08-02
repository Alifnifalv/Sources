using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassDTO : BaseMasterDTO
    {
        public ClassDTO()
        {
            CostCenter = new KeyValueDTO();
            GroupDescription = new List<KeyValueDTO>();
            WorkFlowListDTO = new List<ClassWorkFlowListDTO>();
        }

        [DataMember]
        public int  ClassID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Code { get; set; }

        [DataMember]
        public string  ClassDescription { get; set; }
        
        [DataMember]
        public int? CostCenterID { get; set; }

        [DataMember]
        public KeyValueDTO CostCenter { get; set; }

        [DataMember]
        public int? ORDERNO { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public long? ClassGroupID { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public byte? ShiftID { get; set; }

        [DataMember]
        public bool? IsVisible { get; set; }

        [DataMember]
        public List<KeyValueDTO> GroupDescription { get; set; }

        [DataMember]
        public List<ClassWorkFlowListDTO> WorkFlowListDTO { get; set; }

    }
}