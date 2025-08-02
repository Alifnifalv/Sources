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
    public class PVInvoiceAllocationDTO : BaseMasterDTO
    {
        public PVInvoiceAllocationDTO()
        {
            Payments = new List<PayableDTO>();
        }

        [DataMember]
        public long SupplierID { get; set; }

        [DataMember]
        public List<PayableDTO> Payments { get; set; }
    }
}
