using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Logging.Models
{
    [Table("Exceptions", Schema = "errors")]
    public partial class Exceptions
    {
        [Key]
        public long ExceptionIID { get; set; }

        public string Message { get; set; }

        public string SerializedException { get; set; }

        public byte? ExceptionTypeID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [StringLength(50)]
        public string AppId { get; set; }

        public virtual ExceptionType ExceptionType { get; set; }
    }
}
