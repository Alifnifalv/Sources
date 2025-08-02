namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("FilterColumnConditionMaps", Schema = "setting")]
    public partial class FilterColumnConditionMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FilterColumnConditionMapID { get; set; }

        public byte? DataTypeID { get; set; }

        public long? FilterColumnID { get; set; }

        public byte? ConidtionID { get; set; }

        public virtual Condition Condition { get; set; }

        public virtual DataType DataType { get; set; }

        public virtual FilterColumn FilterColumn { get; set; }
    }
}
