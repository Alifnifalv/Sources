using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Logging.Models
{
    [Table("ExceptionTypes", Schema = "errors")]
    public partial class ExceptionType
    {
        public ExceptionType()
        {
            Exceptions = new HashSet<Exceptions>();
        }

        [Key]
        public byte ExceptionTypeID { get; set; }

        [StringLength(50)]
        public string TypeName { get; set; }

        public virtual ICollection<Exceptions> Exceptions { get; set; }
    }
}
