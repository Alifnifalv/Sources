using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("SupportCategories", Schema = "cs")]
    public partial class SupportCategory
    {
        public SupportCategory()
        {
            TicketSupportCategories = new HashSet<Ticket>();
            TicketSupportSubCategories = new HashSet<Ticket>();
        }

        [Key]
        public int SupportCategoryID { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        public int? ParentCategoryID { get; set; }

        public int? SortOrder { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Ticket> TicketSupportCategories { get; set; }

        public virtual ICollection<Ticket> TicketSupportSubCategories { get; set; }
    }
}