namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ComplaintSourceTypes", Schema = "schools")]
    public partial class ComplaintSourceType
    {
        [Key]
        public byte ComplaintSourceTypeID { get; set; }

        [StringLength(50)]
        public string SourceDescription { get; set; }
    }
}
