using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TreatmentGroups", Schema = "saloon")]
    public partial class TreatmentGroup
    {
        public TreatmentGroup()
        {
            TreatmentTypes = new HashSet<TreatmentType>();
        }

        [Key]
        public int TreatmentGroupID { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("TreatmentGroup")]
        public virtual ICollection<TreatmentType> TreatmentTypes { get; set; }
    }
}
