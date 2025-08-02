using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Assets;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("DocumentReferenceStatusMap", Schema = "mutual")]
    public partial class DocumentReferenceStatusMap
    {
        public DocumentReferenceStatusMap()
        {
            AssetTransactionHeads = new HashSet<AssetTransactionHead>();
        }

        [Key]
        public long DocumentReferenceStatusMapID { get; set; }

        public int? ReferenceTypeID { get; set; }

        public long DocumentStatusID { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual DocumentReferenceType ReferenceType { get; set; }

        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
    }
}