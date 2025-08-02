using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DocumentFile
    {
        public long DocumentFileIID { get; set; }
        public string FileName { get; set; }
        public int EntityTypeID { get; set; }
        public long? ReferenceID { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public string ActualFileName { get; set; }
        public int? DocFileTypeID { get; set; }
        public long? OwnerEmployeeID { get; set; }
        public string Version { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual DocumentFileStatus DocumentFileStatus { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
