namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("saloon.ServiceEmployeeMaps")]
    public partial class ServiceEmployeeMap
    {
        [Key]
        public long ServiceEmployeeMapIID { get; set; }

        public long? ServiceID { get; set; }

        public long? EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Service Service { get; set; }
    }
}
