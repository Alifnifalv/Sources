using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CircularUserTypeMaps", Schema = "schools")]
    public partial class CircularUserTypeMap
    {
        [Key]
        public long CircularUserTypeMapIID { get; set; }
        public long? CircularID { get; set; }
        public byte? CircularUserTypeID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CircularID")]
        [InverseProperty("CircularUserTypeMaps")]
        public virtual Circular Circular { get; set; }
        [ForeignKey("CircularUserTypeID")]
        [InverseProperty("CircularUserTypeMaps")]
        public virtual CircularUserType CircularUserType { get; set; }
    }
}
