using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.WorkflowEngine.Interfaces
{
    public interface IWorflowManager
    {
        bool GenerateWorkFlow(long workflowID, long referenceID);
        bool UpdateWorkFlow();
    }
}
