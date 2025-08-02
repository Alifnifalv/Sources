using Eduegate.Domain.Repository.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Charts
{
    public class ChartBL
    {
        private ChartRepository repository;
        private Eduegate.Framework.CallContext _callContext { get; set; }

        public ChartBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
            repository = new ChartRepository();
        }

        public static void GetAllChartMetadatas()
        {

        }

        public static void GetChartMetadata(int chartID)
        {

        }

        public static void GetChartData(int chartID)
        {

        }
    }
}
