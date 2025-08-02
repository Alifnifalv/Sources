using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LeaveAllocationSearch
    {
        public long LeaveAllocationIID { get; set; }
        [StringLength(50)]
        public string LeaveType { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public double? AllocatedLeaves { get; set; }
        [StringLength(50)]
        public string LeaveGroup { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateFrom { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateTo { get; set; }
    }
}
