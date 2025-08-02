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
    public class RVInvoiceAllocationDTO : BaseMasterDTO
    {
        public RVInvoiceAllocationDTO()
        {
            Receipts = new List<ReceivableDTO>();
        }

        [DataMember]
        public long CustomerID { get; set; }

        [DataMember]
        public List<ReceivableDTO> Receipts { get; set; }
    }
}
