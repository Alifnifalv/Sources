using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AgendaTopicMaps", Schema = "schools")]
    [Index("AgendaID", Name = "IDX_AgendaTopicMaps_AgendaID_LectureCode__Topic__CreatedBy__UpdatedBy__CreatedDate__UpdatedDate")]
    public partial class AgendaTopicMap
    {
        public AgendaTopicMap()
        {
            AgendaTaskMaps = new HashSet<AgendaTaskMap>();
            AgendaTopicAttachmentMaps = new HashSet<AgendaTopicAttachmentMap>();
        }

        [Key]
        public long AgendaTopicMapIID { get; set; }
        public long? AgendaID { get; set; }
        [StringLength(50)]
        public string LectureCode { get; set; }
        public string Topic { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AgendaID")]
        [InverseProperty("AgendaTopicMaps")]
        public virtual Agenda Agenda { get; set; }
        [InverseProperty("AgendaTopicMap")]
        public virtual ICollection<AgendaTaskMap> AgendaTaskMaps { get; set; }
        [InverseProperty("AgendaTopicMap")]
        public virtual ICollection<AgendaTopicAttachmentMap> AgendaTopicAttachmentMaps { get; set; }
    }
}
