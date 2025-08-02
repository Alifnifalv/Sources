namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.MarkStatuses")]
    public partial class MarkStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MarkStatusID { get; set; }

        [StringLength(255)]
        public string StatusName { get; set; }

        [StringLength(255)]
        public string StatusCode { get; set; }

        public bool? IsActive { get; set; }
    }
}
