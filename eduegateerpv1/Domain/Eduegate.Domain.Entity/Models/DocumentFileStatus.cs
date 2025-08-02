using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DocumentFileStatuses", Schema = "doc")]
    public partial class DocumentFileStatus
    {
        public DocumentFileStatus()
        {
            this.DocumentFiles = new List<DocumentFile>();
        }

        [Key]
        public long DocumentStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
    }
}
