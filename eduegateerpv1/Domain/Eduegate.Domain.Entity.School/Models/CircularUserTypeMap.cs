using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.School.Models
{
    [Table("CircularUserTypeMaps", Schema = "schools")]
    public partial class CircularUserTypeMap
    {
        [Key]
        public long CircularUserTypeMapIID { get; set; }

        public long? CircularID { get; set; }

        public byte? CircularUserTypeID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Circular Circular { get; set; }

        public virtual CircularUserType CircularUserType { get; set; }
    }
}