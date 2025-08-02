using Eduegate.Domain.Repository.School;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Security
{
    public class SchoolSecurityBL
    {
        private CallContext _context { get; set; }

        public SchoolSecurityBL(CallContext context)
        {
            _context = context;
        }
        public bool CheckParentAccess(long loginID,List<long> studentIDs)
        {
            var studentDBIds= new SchoolRepository().GetStudentByParentLoginID(loginID);
            return  studentIDs.All(id => studentDBIds.Contains(id));
        }

    }
}
