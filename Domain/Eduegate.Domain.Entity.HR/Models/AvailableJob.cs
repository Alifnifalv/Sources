using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Entity.HR.Models


{
    [Table("AvailableJobs", Schema = "hr")]
    public partial class AvailableJob : ISecuredEntity
    {
        public AvailableJob()
        {
            AvailableJobCultureDatas = new List<AvailableJobCultureData>();
            AvailableJobTags = new List<AvailableJobTag>();
        }
        [Key]
        public long JobIID { get; set; }
        public string JobTitle { get; set; }
        public string TypeOfJob { get; set; }
        public string JobDescription { get; set; }
        public string JobDetails { get; set; }
        public string Status { get; set; }
        public System.Guid Id { get; set; }
        public System.Guid PageId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public ICollection<AvailableJobCultureData> AvailableJobCultureDatas { get; set; }
        public ICollection<AvailableJobTag> AvailableJobTags { get; set; }

        public long GetIID()
        {
            return JobIID;
        }
    }
}
