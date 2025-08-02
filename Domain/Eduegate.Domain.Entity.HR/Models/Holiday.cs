namespace Eduegate.Domain.Entity.HR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Holidays", Schema = "payroll")]
    public partial class Holiday
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long HolidayIID { get; set; }

        public long? HolidayListID { get; set; }

        public DateTime? HolidayDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        //public virtual HolidayList HolidayList { get; set; }
    }
}
