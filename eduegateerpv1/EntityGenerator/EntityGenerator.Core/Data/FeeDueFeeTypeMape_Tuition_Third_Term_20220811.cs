using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeDueFeeTypeMape_Tuition_Third_Term_20220811
    {
        public long? StudentId { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
    }
}
