using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ChartOfAccountSearView
    {
        public long ChartOfAccountIID { get; set; }
        [StringLength(100)]
        public string ChartName { get; set; }
        public int? NoOfGLAccount { get; set; }
    }
}
