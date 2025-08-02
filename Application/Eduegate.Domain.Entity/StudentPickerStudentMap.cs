namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentPickerStudentMaps")]
    public partial class StudentPickerStudentMap
    {
        [Key]
        public long StudentPickerStudentMapIID { get; set; }

        public long? StudentPickerID { get; set; }

        public long? StudentID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public string QRCODE { get; set; }

        public bool? IsActive { get; set; }

        public long? PickUpLoginID { get; set; }

        public virtual StudentPicker StudentPicker { get; set; }

        public virtual Student Student { get; set; }
    }
}
