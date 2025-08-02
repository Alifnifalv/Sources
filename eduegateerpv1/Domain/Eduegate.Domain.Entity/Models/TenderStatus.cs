using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TenderStatuses", Schema = "inventory")]
    public partial class TenderStatus
    {
        public TenderStatus()
        {
            Tenders = new HashSet<Tender>();
        }

        [Key]
        public long TenderStatusID { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Tender> Tenders { get; set; }
    }
}
