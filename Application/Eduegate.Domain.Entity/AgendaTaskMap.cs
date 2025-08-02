namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.AgendaTaskMaps")]
    public partial class AgendaTaskMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AgendaTaskMap()
        {
            AgendaTaskAttachmentMaps = new HashSet<AgendaTaskAttachmentMap>();
        }

        [Key]
        public long AgendaTaskMapIID { get; set; }

        public long? AgendaTopicMapID { get; set; }

        public string Task { get; set; }

        public byte? TaskTypeID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? AgendaID { get; set; }

        public virtual Agenda Agenda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaTaskAttachmentMap> AgendaTaskAttachmentMaps { get; set; }

        public virtual TaskType TaskType { get; set; }

        public virtual AgendaTopicMap AgendaTopicMap { get; set; }
    }
}
