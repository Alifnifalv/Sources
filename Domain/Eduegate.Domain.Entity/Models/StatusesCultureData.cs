using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("StatusesCultureDatas", Schema = "mutual")]
    public partial class StatusesCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        public byte StatusID { get; set; }
        public string StatusName { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual Status Status { get; set; }
    }
}
