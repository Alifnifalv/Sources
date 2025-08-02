namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.SegmentCustomerMaps")]
    public partial class SegmentCustomerMap
    {
        [Key]
        public long SegmentCustomerMapIID { get; set; }

        public long? SegmentID { get; set; }

        public long? CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Segment Segment { get; set; }
    }
}
