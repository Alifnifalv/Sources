namespace Eduegate.Domain.Entity.School.Models

{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CustomerFeedBacks", Schema = "cms")]
    public partial class CustomerFeedBacks
    {
        [Key]
        public long CustomerFeedBackIID { get; set; }

        public long? LoginID { get; set; }

        public string Message { get; set; }

        public byte? CustomerFeedbackTypeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual CustomerFeedbackType CustomerFeedbackType { get; set; }
    }
}
