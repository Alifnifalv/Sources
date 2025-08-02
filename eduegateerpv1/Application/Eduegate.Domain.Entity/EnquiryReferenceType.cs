namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.EnquiryReferenceTypes")]
    public partial class EnquiryReferenceType
    {
        public byte EnquiryReferenceTypeID { get; set; }

        [StringLength(50)]
        public string ReferenceName { get; set; }
    }
}
