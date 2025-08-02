using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.School.Models
{
    [Table("ClassAgeLimits", Schema = "schools")]
    public partial class ClassAgeLimit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AgeLimitIID { get; set; }

        public int? ClassID { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public virtual Class Class { get; set; }
    }
}
