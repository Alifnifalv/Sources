namespace Eduegate.Domain.Entity.School.Models

{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CustomerFeedbackTypes", Schema = "cms")]
    public partial class CustomerFeedbackType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerFeedbackType()
        {
            CustomerFeedBacks = new HashSet<CustomerFeedBacks>();
        }

        public byte CustomerFeedbackTypeID { get; set; }

        [StringLength(50)]
        public string TypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerFeedBacks> CustomerFeedBacks { get; set; }
    }
}
