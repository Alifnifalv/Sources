namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.EmailTemplateParameterMaps")]
    public partial class EmailTemplateParameterMap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmailTemplateParameterMapID { get; set; }

        public int? EmailTemplateID { get; set; }

        [MaxLength(50)]
        public byte[] TemplateVariable { get; set; }

        [StringLength(20)]
        public string VariableType { get; set; }

        public virtual EmailTemplates2 EmailTemplates2 { get; set; }
    }
}
