namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.Relegions")]
    public partial class Relegions1
    {
        [Key]
        public byte RelegionID { get; set; }

        [StringLength(100)]
        public string RelegionName { get; set; }

        [StringLength(20)]
        public string RelegionCode { get; set; }

        public bool? IsActive { get; set; }
    }
}
