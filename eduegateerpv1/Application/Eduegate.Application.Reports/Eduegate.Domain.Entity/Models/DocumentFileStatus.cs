using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DocumentFileStatus
    {
        public DocumentFileStatus()
        {
            this.DocumentFiles = new List<DocumentFile>();
        }

        public long DocumentStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
    }
}
