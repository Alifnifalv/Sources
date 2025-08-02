using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class LSProvisionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LSProvisionDTO()
        {
            Department = new List<KeyValueDTO>();
            Employees = new List<KeyValueDTO>();
           
        }

        [DataMember]
        public DateTime? EntryDate { get; set; }       

        [DataMember]
        public List<KeyValueDTO> Department { get; set; }

        [DataMember]
        public List<KeyValueDTO> Employees { get; set; }
        
        [DataMember]
        public long? ReportContentID { get; set; }
        
        [DataMember]
        public string EntryString { get; set; }

    }
}
