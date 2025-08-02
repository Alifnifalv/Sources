using Eduegate.Framework;
using Eduegate.Domain.Mappers.School.School;
using System.Collections.Generic;

namespace Eduegate.Domain.School
{
    public class ParentBL
    {
        private CallContext _context;

        public ParentBL(CallContext context = null)
        {
            _context = context;
        }

        public string GetParentEmailIDByStudentID(long? studentID)
        {
            var emailID = ParentMapper.Mapper(_context).GetParentEmailIDByStudentID(studentID);

            return emailID;
        }

        public List<string> GetParentsEmailIDByClassSection(int? classID, int? sectionID = null)
        {
            var emailIDs = ParentMapper.Mapper(_context).GetParentsEmailIDByClassSection(classID, sectionID);

            return emailIDs;
        }

        public long? GetParentLoginIDByStudentID(long? studentID)
        {
            var parentLoginID = ParentMapper.Mapper(_context).GetParentLoginIDByStudentID(studentID);

            return parentLoginID;
        }

        public List<long?> GetParentsLoginIDByClassSection(int? classID, int? sectionID = null)
        {
            var parentLoginIDs = ParentMapper.Mapper(_context).GetParentsLoginIDByClassSection(classID, sectionID);

            return parentLoginIDs;
        }

    }
}