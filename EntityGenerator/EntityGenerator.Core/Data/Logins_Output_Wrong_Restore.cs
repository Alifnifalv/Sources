using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Logins_Output_Wrong_Restore
    {
        public long? SStudentIID { get; set; }
        public long? DStudentIID { get; set; }
    }
}
