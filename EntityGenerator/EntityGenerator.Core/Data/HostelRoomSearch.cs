using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class HostelRoomSearch
    {
        public long HostelRoomIID { get; set; }
        [StringLength(50)]
        public string RoomNumber { get; set; }
        public int? HostelID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string HostelName { get; set; }
        public int? RoomTypeID { get; set; }
        [StringLength(100)]
        public string RoomType { get; set; }
        public int? NumberOfBed { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostPerBed { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
