using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PhoneCallLogs", Schema = "schools")]
    public partial class PhoneCallLog
    {
        [Key]
        public long PhoneCallLogIID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NextFollowUpDate { get; set; }
        public int? CallDuration { get; set; }
        [StringLength(1000)]
        public string Note { get; set; }
        public byte? CallType { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
