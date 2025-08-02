namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("mutual.AreaTreeSearch")]
    public partial class AreaTreeSearch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AreaID { get; set; }

        public string AreaName { get; set; }

        public int? ParentAreaID { get; set; }

        //public int Level { get; set; }

        public string TreePath { get; set; }
    }
}
