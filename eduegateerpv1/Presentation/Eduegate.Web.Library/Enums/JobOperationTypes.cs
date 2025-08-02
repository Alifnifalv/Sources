using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.Enums
{
    public enum JobOperationTypes
    {
        Receiving = 1,
        PutAway = 2,
        Picking = 3,
        StockOut = 4,
        Packing = 5,
        Packed = 6,
        FailedReceiving = 11,
        FailedReceived = 12,
        ServiceJob = 19
    }
}
