namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ComplaintSourceTypes")]
    public partial class ComplaintSourceType
    {
        public byte ComplaintSourceTypeID { get; set; }

        [StringLength(50)]
        public string SourceDescription { get; set; }
    }
}
