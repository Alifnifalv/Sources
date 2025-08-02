using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_DueTransactions_2023_20231120
    {
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public bool CollectionStatus { get; set; }
        public int? FeeMasterID { get; set; }
    }
}
