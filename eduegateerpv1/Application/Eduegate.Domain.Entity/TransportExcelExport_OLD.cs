namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.TransportExcelExport_OLD")]
    public partial class TransportExcelExport_OLD
    {
        [Key]
        public long TransportDataIID { get; set; }

        [StringLength(100)]
        public string BusNumber { get; set; }

        [StringLength(100)]
        public string PickNumber { get; set; }

        [StringLength(100)]
        public string PNumber { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Class { get; set; }

        [StringLength(100)]
        public string Place { get; set; }

        [StringLength(100)]
        public string MobileNo1 { get; set; }

        [StringLength(100)]
        public string MobileNo2 { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public DateTime? FromDate { get; set; }

        [StringLength(100)]
        public string Campus { get; set; }
    }
}
