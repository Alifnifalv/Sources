using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AccountSearchView
    {
        public long AccountID { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Alias { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Accountcode { get; set; }
        [StringLength(50)]
        public string AccountName { get; set; }
        public long? ParentAccountID { get; set; }
        public long? Main_GroupID { get; set; }
        [StringLength(50)]
        public string Main_GroupCode { get; set; }
        [StringLength(100)]
        public string Main_GroupName { get; set; }
        public long? Sub_GroupID { get; set; }
        [StringLength(50)]
        public string Sub_GroupCode { get; set; }
        [StringLength(100)]
        public string Sub_GroupName { get; set; }
        [StringLength(50)]
        public string GroupCode { get; set; }
        public long? GroupID { get; set; }
        [StringLength(100)]
        public string GroupName { get; set; }
        public byte? AccountBehavoirID { get; set; }
        [StringLength(50)]
        public string Behaviour { get; set; }
        [Column(TypeName = "money")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Credit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Balance { get; set; }
    }
}
