using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class WRONG_SPLIT_NOT_FOUND_FEE_DUE_20220621
    {
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
    }
}
