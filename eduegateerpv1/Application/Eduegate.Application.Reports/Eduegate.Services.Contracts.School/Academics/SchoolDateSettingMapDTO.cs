
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class SchoolDateSettingMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SchoolDateSettingMapDTO()
        {
        }

        [DataMember]
        public long SchoolDateSettingMapsIID { get; set; }

        [DataMember]
        public long? SchoolDateSettingID { get; set; }

        [DataMember]
        public int? TotalWorkingDays { get; set; }

        [DataMember]
        [StringLength(500)]
        public string Description { get; set; }

        [DataMember]
        public byte? MonthID { get; set; }

        [DataMember]
        public int? YearID { get; set; }

        [DataMember]
        public DateTime? PayrollCutoffDate { get; set; }

        [DataMember]
        public int? TotalHoursInMonth { get; set; }

        [DataMember]
        public virtual SchoolDateSetting SchoolDateSetting { get; set; }

        [DataMember]
        public string MonthName { get; set; }
    }
}