namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.PhoneCallTypes")]
    public partial class PhoneCallType
    {
        public byte PhoneCallTypeID { get; set; }

        [StringLength(50)]
        public string TypeDescription { get; set; }
    }
}
