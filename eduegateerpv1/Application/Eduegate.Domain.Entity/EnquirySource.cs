namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.EnquirySources")]
    public partial class EnquirySource
    {
        public byte EnquirySourceID { get; set; }

        [StringLength(50)]
        public string SourceName { get; set; }
    }
}
