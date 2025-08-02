using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AgendaTaskMaps", Schema = "schools")]
    public partial class AgendaTaskMap
    {
        public AgendaTaskMap()
        {
            AgendaTaskAttachmentMaps = new HashSet<AgendaTaskAttachmentMap>();
        }

        [Key]
        public long AgendaTaskMapIID { get; set; }
        public long? AgendaTopicMapID { get; set; }
        public string Task { get; set; }
        public byte? TaskTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? AgendaID { get; set; }

        [ForeignKey("AgendaID")]
        [InverseProperty("AgendaTaskMaps")]
        public virtual Agenda Agenda { get; set; }
        [ForeignKey("AgendaTopicMapID")]
        [InverseProperty("AgendaTaskMaps")]
        public virtual AgendaTopicMap AgendaTopicMap { get; set; }
        [ForeignKey("TaskTypeID")]
        [InverseProperty("AgendaTaskMaps")]
        public virtual TaskType TaskType { get; set; }
        [InverseProperty("AgendaTaskMap")]
        public virtual ICollection<AgendaTaskAttachmentMap> AgendaTaskAttachmentMaps { get; set; }
    }
}
