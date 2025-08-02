using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("TempStudentTransport")]
    public partial class TempStudentTransport
    {
        public long? StudentIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        public long? PickupRouteID { get; set; }
        public long? DropRouteID { get; set; }
    }
}
