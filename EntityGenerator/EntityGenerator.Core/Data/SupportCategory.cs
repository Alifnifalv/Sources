using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("SupportCategory")]
        public virtual ICollection<Ticket> TicketSupportCategories { get; set; }
        [InverseProperty("SupportSubCategory")]
        public virtual ICollection<Ticket> TicketSupportSubCategories { get; set; }
    }
}
