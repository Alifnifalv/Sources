using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class wrong_logins
    {
        public long ParentIID { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        public long? FirstLoginIID { get; set; }
    }
}
