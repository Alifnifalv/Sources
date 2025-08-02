namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ComplainTypes")]
    public partial class ComplainType
    {
        public byte ComplainTypeID { get; set; }

        [StringLength(50)]
        public string ComplainDescription { get; set; }
    }
}
