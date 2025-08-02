using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SegmentCustomerMaps", Schema = "marketing")]
    public partial class SegmentCustomerMap
    {
        [Key]
        public long SegmentCustomerMapIID { get; set; }
        public long? SegmentID { get; set; }
        public long? CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("SegmentCustomerMaps")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("SegmentID")]
        [InverseProperty("SegmentCustomerMaps")]
        public virtual Segment Segment { get; set; }
    }
}
