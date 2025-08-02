//namespace Eduegate.Domain.Entity.Models
//{
//    using Eduegate.Domain.Entity.School.Models;
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;
//    using System.Data.Entity.Spatial;

//    [Table("payroll.LeaveGroupTypeMaps")]
//    public partial class LeaveGroupTypeMap
//    {
//        [Key]
//        public long LeaveGroupTypeMapIID { get; set; }

//        public int? LeaveTypeID { get; set; }

//        public int? NoofLeaves { get; set; }

//        [StringLength(150)]
//        public string Remarks { get; set; }

//        public int? CreatedBy { get; set; }

//        public int? UpdatedBy { get; set; }

//        public DateTime? CreatedDate { get; set; }

//        public DateTime? UpdatedDate { get; set; }

//        [Column(TypeName = "timestamp")]
//        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
//        [MaxLength(8)]
//        public byte[] TimeStamps { get; set; }

//        public int? LeaveGroupID { get; set; }

//        public virtual LeaveGroup LeaveGroup { get; set; }

//        public virtual LeaveType LeaveType { get; set; }
//    }
//}