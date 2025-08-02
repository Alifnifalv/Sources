using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeGroups", Schema = "schools")]
    public partial class FeeGroup
    {
        public FeeGroup()
        {
            FeeTypes = new HashSet<FeeType>();
        }

        [Key]
        public int FeeGroupID { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public bool? IsTransport { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        [InverseProperty("FeeGroup")]
        public virtual ICollection<FeeType> FeeTypes { get; set; }
    }
}
