namespace  Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentStaffMaps", Schema = "schools")]
    public partial class StudentStaffMap
    {
        [Key]
        public long StudentStaffMapIID { get; set; }

        public long StudentID { get; set; }

        public long? StaffID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        ////public byte[] TimeStamps { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Student Student { get; set; }
    }
}
