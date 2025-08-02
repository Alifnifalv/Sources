using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("DocumentTypes", Schema = "mutual")]
    public partial class DocumentType
    {
        public DocumentType()
        {
            Tickets = new HashSet<Ticket>();
            DocumentDepartmentMaps = new HashSet<DocumentDepartmentMap>();
        }

        [Key]
        public int DocumentTypeID { get; set; }

        public int? ReferenceTypeID { get; set; }

        [StringLength(50)]
        public string TransactionTypeName { get; set; }

        [StringLength(20)]
        public string System { get; set; }

        [StringLength(50)]
        public string TransactionNoPrefix { get; set; }

        public long? LastTransactionNo { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        //public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public int? TransactionSequenceType { get; set; }

        public int? TaxTemplateID { get; set; }

        public bool? IgnoreInventoryCheck { get; set; }

        public long? WorkflowID { get; set; }

        public bool? IsExternal { get; set; }

        public virtual DocumentReferenceType ReferenceType { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<DocumentDepartmentMap> DocumentDepartmentMaps { get; set; }
    }
}