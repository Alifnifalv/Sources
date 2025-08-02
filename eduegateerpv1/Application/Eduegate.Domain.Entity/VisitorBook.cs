namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.VisitorBooks")]
    public partial class VisitorBook
    {
        [Key]
        public long VisitorBookIID { get; set; }

        public byte? VisitingPurposeID { get; set; }

        [StringLength(50)]
        public string VisitorName { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string IDCard { get; set; }

        [StringLength(500)]
        public string NumberOfPerson { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? InTime { get; set; }

        public TimeSpan? OutTime { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual VisitingPurpos VisitingPurpos { get; set; }
    }
}
