using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ScreenFields", Schema = "setting")]
    public partial class ScreenField
    {
        public ScreenField()
        {
            ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
            UserScreenFieldSettings = new HashSet<UserScreenFieldSetting>();
        }

        [Key]
        public long ScreenFieldID { get; set; }
        [StringLength(100)]
        public string FieldName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ModelName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string PhysicalFieldName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LookupName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DateType { get; set; }

        [InverseProperty("ScreenField")]
        public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }
        [InverseProperty("ScreenField")]
        public virtual ICollection<UserScreenFieldSetting> UserScreenFieldSettings { get; set; }
    }
}
