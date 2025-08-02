namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.AgendaTopicMaps")]
    public partial class AgendaTopicMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Agenda Agenda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaTaskMap> AgendaTaskMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaTopicAttachmentMap> AgendaTopicAttachmentMaps { get; set; }
    }
}
