namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.VariableSettings")]
    public partial class VariableSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VariableSettingID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public string RelatedName { get; set; }

        public string Formula { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }
    }
}
