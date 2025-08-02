using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.TransactionEngineCore.ViewModels
{
    public class TransactionProductSerialKeyMap 
    {
        public long ProductSerialID { get; set; }
        public string SerialNo { get; set; }
        public Nullable<long> DetailID { get; set; }
        public long ProductSKUMapID { get; set; }
        public int ProductLength { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
