namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.PhoneCallLogs")]
    public partial class PhoneCallLog
    {
        [Key]
        public long PhoneCallLogIID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime? NextFollowUpDate { get; set; }

        public int? CallDuration { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        public byte? CallType { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }
    }
}
