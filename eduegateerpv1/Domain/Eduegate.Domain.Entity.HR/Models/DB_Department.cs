using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Entity.HR.Models
{
    public partial class DB_Department : ISecuredEntity
    {
        public DB_Department()
        {
            DepartmentTags = new List<DepartmentTag>();
        }

        [Key]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Logo { get; set; }
        public virtual ICollection<DepartmentTag> DepartmentTags { get; set; }

        public long GetIID()
        {
            return this.DepartmentID;
        }
    }
}
