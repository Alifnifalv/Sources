using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamResultStatuses", Schema = "exam")]
    public partial class OnlineExamResultStatus
    {
        public OnlineExamResultStatus()
        {
            OnlineExamResults = new HashSet<OnlineExamResult>();
        }

        [Key]
        public byte OnlineExamResultStatusID { get; set; }
        [StringLength(50)]
        public string StatusNameEn { get; set; }
        [StringLength(50)]
        public string StatusNameAr { get; set; }

        [InverseProperty("ResultStatus")]
        public virtual ICollection<OnlineExamResult> OnlineExamResults { get; set; }
    }
}
