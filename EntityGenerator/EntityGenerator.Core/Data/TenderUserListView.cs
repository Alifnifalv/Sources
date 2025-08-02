using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TenderUserListView
    {
        public long? TenderIID { get; set; }
        public long AuthenticationID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Tender { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OpeningDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SubmissionDate { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsOpened { get; set; }
        public long LoginID { get; set; }
    }
}
