using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ScreenLookupMaps", Schema = "setting")]
    public partial class ScreenLookupMap
    {
        public ScreenLookupMap()
        {
            LookupColumnConditionMaps = new HashSet<LookupColumnConditionMap>();
        }

        [Key]
        public long ScreenLookupMapID { get; set; }
        public long? ScreenID { get; set; }
        public bool? IsOnInit { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LookUpName { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string Url { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CallBack { get; set; }

        [ForeignKey("ScreenID")]
        [InverseProperty("ScreenLookupMaps")]
        public virtual ScreenMetadata Screen { get; set; }
        [InverseProperty("ScreenLookupMap")]
        public virtual ICollection<LookupColumnConditionMap> LookupColumnConditionMaps { get; set; }
    }
}
