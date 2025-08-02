using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class MAP_ACCOUNT_NEW_LEDGERCODE_TB_2021
    {
        public int? ROWINDEX { get; set; }
        public long? ACCOUNTID { get; set; }
        [StringLength(100)]
        public string NEWACCOUNTCODE { get; set; }
        [StringLength(100)]
        public string ACCOUNTCODE { get; set; }
        [StringLength(100)]
        public string ACCOUNTNAME { get; set; }
        [StringLength(100)]
        public string GL_ACCOUNT { get; set; }
    }
}
