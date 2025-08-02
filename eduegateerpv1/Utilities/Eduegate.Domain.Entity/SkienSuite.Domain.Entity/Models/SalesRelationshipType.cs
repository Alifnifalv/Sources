using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SalesRelationshipType
    {
        public byte CultureID { get; set; }
        public byte SalesRelationTypeID { get; set; }
        public string RelationName { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
