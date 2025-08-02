namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("DataHistoryEntities", Schema = "setting")]
    public partial class DataHistoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DataHistoryEntityID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(200)]
        public string TableName { get; set; }

        [StringLength(50)]
        public string DBName { get; set; }

        [StringLength(50)]
        public string SchemaName { get; set; }

        [StringLength(200)]
        public string LoggerTableName { get; set; }

        [StringLength(50)]
        public string LoggerDBName { get; set; }

        [StringLength(50)]
        public string LoggerSchemaName { get; set; }
    }
}
