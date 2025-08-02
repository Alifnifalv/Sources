using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    public partial class JobStatusOperationMap
    {
        public Nullable<int> JobStatusOperationMapId { get; set; }
        public Nullable<int> JobStatusId { get; set; }
        public Nullable<short> JobOperationStatusId { get; set; }
        public Nullable<int> JobTypeId { get; set; }
    }
}
