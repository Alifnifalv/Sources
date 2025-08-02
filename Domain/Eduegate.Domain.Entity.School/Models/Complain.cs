namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Complains", Schema = "schools")]
    public partial class Complain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ComplainIID { get; set; }

        public byte? ComplainTypeID { get; set; }

        public byte? SourceID { get; set; }

        [StringLength(50)]
        public string ComplainType { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(200)]
        public string ActionTaken { get; set; }

        [StringLength(50)]
        public string Assigned { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }
    }
}
