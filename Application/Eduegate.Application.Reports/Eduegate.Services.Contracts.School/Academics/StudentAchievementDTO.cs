using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class StudentAchievementDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentAchievementDTO()
        {

        }

        [DataMember]
        public long StudentAchievementIID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string Class { get; set; }

        [DataMember]
        public string Section { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? CategoryID { get; set; }

        [DataMember]
        public int? TypeID { get; set; }

        [DataMember]
        public int? RankingID { get; set; }

        [DataMember]
        public string AchievementDescription { get; set; }

    }
}