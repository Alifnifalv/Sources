namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ClassRoomTypes", Schema = "schools")]
    public partial class ClassRoomType
    {
        [Key]
        public byte ClassRoomTypeID { get; set; }

        [StringLength(50)]
        public string TypeDescription { get; set; }

        public bool? IsShared { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }
    }
}
