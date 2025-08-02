using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class tempStudent
    {
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long StudentIID { get; set; }
        public long? LoginID { get; set; }
    }
}
