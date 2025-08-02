namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ClassRoomTypes")]
    public partial class ClassRoomType
    {
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
