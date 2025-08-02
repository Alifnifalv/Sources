namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.Lookups")]
    public partial class Lookup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LookupID { get; set; }

        [StringLength(10)]
        public string LookupType { get; set; }

        [StringLength(50)]
        public string LookupName { get; set; }

        [StringLength(500)]
        public string Query { get; set; }

        [StringLength(500)]
        public string Value1 { get; set; }

        [StringLength(500)]
        public string Value2 { get; set; }
    }
}
