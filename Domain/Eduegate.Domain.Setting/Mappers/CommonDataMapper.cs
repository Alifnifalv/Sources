using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Setting.Mappers
{
    public class CommonDataMapper : DTOEntityDynamicMapper
    {
        private CallContext _callContext;

        public static CommonDataMapper Mapper(CallContext _context = null)
        {
            var mapper = new CommonDataMapper();
            mapper._callContext = _context;
            return mapper;
        }

        public object ToDTO(object entity)
        {
            throw new System.NotImplementedException();
        }

        public object ToEntity(object dto)
        {
            throw new System.NotImplementedException();
        }

        public long? GetLoginDetailsByParentID(long? parentID)
        {
            long? loginID = null;
            if (parentID.HasValue)
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var parentData = dbContext.Parents.Where(p => p.ParentIID == parentID).AsNoTracking().FirstOrDefault();

                    loginID = parentData?.LoginID;
                }
            }

            return loginID;
        }

        public long? GetParentLoginDetailsByStudentID(long? studentID)
        {
            long? loginID = null;
            if (studentID.HasValue)
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var studentData = dbContext.Students.Where(p => p.StudentIID == studentID)
                        .Include(i => i.Parent)
                        .AsNoTracking().FirstOrDefault();

                    loginID = studentData?.Parent?.LoginID;
                }
            }

            return loginID;
        }

        public long? GetLoginDetailsByCustomerID(long? customerID)
        {
            long? loginID = null;
            if (customerID.HasValue)
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var customerData = dbContext.Customers.Where(p => p.CustomerIID == customerID).AsNoTracking().FirstOrDefault();

                    loginID = customerData?.LoginID;
                }
            }

            return loginID;
        }

        public long? GetLoginDetailsByEmployeeID(long? employeeID)
        {
            long? loginID = null;
            if (employeeID.HasValue)
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var customerData = dbContext.Employees.Where(p => p.EmployeeIID == employeeID).AsNoTracking().FirstOrDefault();

                    loginID = customerData?.LoginID;
                }
            }

            return loginID;
        }

        public LoginDTO GetLoginDetailByEmployeeID(long? employeeID)
        {
            var loginDTO = new LoginDTO();
            if (employeeID.HasValue)
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var customerData = dbContext.Employees.Where(p => p.EmployeeIID == employeeID)
                        .Include(i => i.Login)
                        .AsNoTracking().FirstOrDefault();

                    if (customerData != null && customerData.Login != null)
                    {
                        loginDTO.LoginIID = customerData.Login.LoginIID;
                        loginDTO.LoginEmailID = customerData.Login.LoginEmailID;
                    }
                }
            }

            return loginDTO;
        }

    }
}