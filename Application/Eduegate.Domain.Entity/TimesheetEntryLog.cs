namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.TimesheetEntryLogs")]
    public partial class TimesheetEntryLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TimesheetEntryLogID { get; set; }

        public long? EmployeeID { get; set; }

        public short? EntryTypeID { get; set; }

        public short? LogInTypeID { get; set; }

        public DateTime? EntryDateTime { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual TimesheetEntryType TimesheetEntryType { get; set; }

        public virtual TimesheetLogInType TimesheetLogInType { get; set; }
    }
}
