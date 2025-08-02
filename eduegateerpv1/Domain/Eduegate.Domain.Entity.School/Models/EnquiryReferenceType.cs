namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EnquiryReferenceTypes", Schema = "schools")]
    public partial class EnquiryReferenceType
    {
        [Key]
        public byte EnquiryReferenceTypeID { get; set; }

        [StringLength(50)]
        public string ReferenceName { get; set; }
    }
}
