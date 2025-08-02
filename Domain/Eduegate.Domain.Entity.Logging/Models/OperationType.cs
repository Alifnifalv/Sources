using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Logging.Models
{
    [Table("OperationTypes", Schema = "sync")]
    public partial class OperationType
    {
        [Key]
        public int OperationTypeID { get; set; }
        public string OperationTypeName { get; set; }
    }
}