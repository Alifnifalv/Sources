namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ClassCoordinatorClassMaps")]
    public partial class ClassCoordinatorClassMap
    {
        [Key]
        public long ClassCoordinatorClassMapIID { get; set; }

        public long? ClassCoordinatorID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? AllClass { get; set; }

        public bool? AllSection { get; set; }

        public virtual Class Class { get; set; }

        public virtual ClassCoordinator ClassCoordinator { get; set; }

        public virtual Section Section { get; set; }
    }
}
