using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeDuePaymentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public FeeDuePaymentDTO()
        {           
            FeeDueFeeTypeMap = new List<FeeDueFeeTypeMapDTO>();            
        }

        [DataMember]
        public long StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public List<FeeDueFeeTypeMapDTO> FeeDueFeeTypeMap { get; set; }
    }
}
