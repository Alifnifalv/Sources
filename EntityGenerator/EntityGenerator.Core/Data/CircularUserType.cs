using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CircularUserTypes", Schema = "schools")]
    public partial class CircularUserType
    {
        public CircularUserType()
        {
            CircularUserTypeMaps = new HashSet<CircularUserTypeMap>();
        }

        [Key]
        public byte CircularUserTypeID { get; set; }
        [StringLength(50)]
        public string UserType { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("CircularUserType")]
        public virtual ICollection<CircularUserTypeMap> CircularUserTypeMaps { get; set; }
    }
}
