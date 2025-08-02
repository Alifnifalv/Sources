namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentPickLogs")]
    public partial class StudentPickLog
    {
        [Key]
        public long StudentPickLogIID { get; set; }

        public DateTime? PickDate { get; set; }

        public long StudentPickerID { get; set; }

        public long? StudentID { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public long? PhotoContentID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public bool? LogStatus { get; set; }

        public virtual StudentPicker StudentPicker { get; set; }

        public virtual Student Student { get; set; }
    }
}
