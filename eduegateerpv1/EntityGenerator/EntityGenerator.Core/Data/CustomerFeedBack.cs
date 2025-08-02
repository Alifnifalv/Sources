using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerFeedBacks", Schema = "cms")]
    public partial class CustomerFeedBack
    {
        [Key]
        public long CustomerFeedBackIID { get; set; }
        public long? LoginID { get; set; }
        public string Message { get; set; }
        public byte? CustomerFeedbackTypeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CustomerFeedbackTypeID")]
        [InverseProperty("CustomerFeedBacks")]
        public virtual CustomerFeedbackType CustomerFeedbackType { get; set; }
    }
}
