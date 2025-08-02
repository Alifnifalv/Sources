using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerFeedbackTypes", Schema = "cms")]
    public partial class CustomerFeedbackType
    {
        public CustomerFeedbackType()
        {
            CustomerFeedBacks = new HashSet<CustomerFeedBack>();
        }

        [Key]
        public byte CustomerFeedbackTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("CustomerFeedbackType")]
        public virtual ICollection<CustomerFeedBack> CustomerFeedBacks { get; set; }
    }
}
