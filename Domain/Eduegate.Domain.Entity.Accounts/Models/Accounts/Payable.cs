using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Inventory;
using Eduegate.Domain.Entity.Accounts.Models.Jobs;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("Payables", Schema = "account")]
    public partial class Payable
    {
        public Payable()
        {
            InverseReferencePayables = new HashSet<Payable>();
            JobsEntryHeadPayableMaps = new HashSet<JobsEntryHeadPayableMap>();
        }

        [Key]
        public long PayableIID { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }

        public DateTime? TransactionDate { get; set; }

        public DateTime? DueDate { get; set; }

        public long? SerialNumber { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? ReferencePayablesID { get; set; }

        public long? DocumentStatusID { get; set; }

        public long? AccountID { get; set; }

        public decimal? Amount { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? TaxAmount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public DateTime? AccountPostingDate { get; set; }

        public decimal? ExchangeRate { get; set; }

        public int? CurrencyID { get; set; }

        public byte? TransactionStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public bool? DebitOrCredit { get; set; }

        public virtual Account Account { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Payable ReferencePayables { get; set; }

        public virtual TransactionStatus TransactionStatus { get; set; }

        public virtual ICollection<Payable> InverseReferencePayables { get; set; }

        public virtual ICollection<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }
    }
}