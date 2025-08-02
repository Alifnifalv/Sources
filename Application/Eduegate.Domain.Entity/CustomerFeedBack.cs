namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.CustomerFeedBacks")]
    public partial class CustomerFeedBack
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
