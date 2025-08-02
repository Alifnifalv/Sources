using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeColl_Output_Wrong_Restore
    {
        public long? SFeeCollectionIID { get; set; }
        public long? DFeeCollectionIID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string SFeeReceiptNo { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string DFeeReceiptNo { get; set; }
    }
}
