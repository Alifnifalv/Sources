using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class ChartOfAccountDTO : BaseMasterDTO
    {
        public ChartOfAccountDTO()
        {
            Details = new List<ChartOfAccountDetailDTO>();
        }

        [DataMember]
        public long ChartOfAccountIID { get; set; }
        [DataMember]
        public string ChartName { get; set; }
        [DataMember]
        public List<ChartOfAccountDetailDTO> Details { get; set; }
    }
}
