using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Accounts_20221214
    {
        public long AccountID { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Alias { get; set; }
        [StringLength(500)]
        public string AccountName { get; set; }
        public long? ParentAccountID { get; set; }
        public int? GroupID { get; set; }
        public byte? AccountBehavoirID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(100)]
        public string ChildAliasPrefix { get; set; }
        public long? ChildLastID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReferenceID { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string AccountCode { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string AccountAddress { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TaxRegistrationNum { get; set; }
        public bool? IsEnableSubLedger { get; set; }
    }
}
