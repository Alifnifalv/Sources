using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TableDataChanx
    {
        [StringLength(255)]
        [Unicode(false)]
        public string SCHEMANAME { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string TABLENAME { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string IDENTITYCOLNAME { get; set; }
        public long? REF_ID { get; set; }
        public long? CRN_TIMESTAMP { get; set; }
        public long? OLD_TIMESTAMP { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string UPDATEDBY { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DML_ACTION { get; set; }
        public bool? DML_STATUS { get; set; }
    }
}
