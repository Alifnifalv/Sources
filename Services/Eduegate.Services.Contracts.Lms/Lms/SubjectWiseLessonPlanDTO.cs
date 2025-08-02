using Eduegate.Services.Contracts.School.Academics;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.Lms
{
    public class SubjectWiseLessonPlanDTO
    {
        public object Subject { get; set; }
        public List<LessonPlanDTO> LessonPlans { get; set; }
    }
}