using Eduegate.Framework;
using Eduegate.Services.Contracts.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Saloon
{
    public class SaloonBL
    {
        private CallContext Context { get; set; }

        public SaloonBL(CallContext context)
        {
            Context = context;
        }

        public List<ServiceDTO> GetServices()
        {
            return new List<ServiceDTO>() { new ServiceDTO() { ServiceIID = 1, ServiceName = "test" }, new ServiceDTO() { ServiceIID = 2, ServiceName = "test 2" } };
        }

        public List<SaloonDTO> GetSaloons()
        {
            return new List<SaloonDTO>() { new SaloonDTO() { SaloonIID = 1, SaloonName = "branch 1" }, new SaloonDTO() { SaloonIID = 2, SaloonName = "branch 2" } };
        }
    }
}
