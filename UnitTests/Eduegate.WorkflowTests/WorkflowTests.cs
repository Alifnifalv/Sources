using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Eduegate.WorkflowTests
{
    [TestClass]
    public class WorkflowTests
    {
        [TestMethod]
        public void Can_WorkflowTests()
        {
            Domain.Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(1, 437);
        }
    }
}
