using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeCollType_Output_Wrong_Restore
    {
        public long? SFeeCollTypeID { get; set; }
        public long? DFeeCollTypeID { get; set; }
        public long? SFeeCollectionIID { get; set; }
        public long? DFeeCollectionIID { get; set; }
    }
}
