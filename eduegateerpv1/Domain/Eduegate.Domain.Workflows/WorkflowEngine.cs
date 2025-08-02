using Eduegate.Domain.WorkflowEngine.Interfaces;
using Eduegate.Framework;
using System;
using System.Reflection;

namespace Eduegate.Domain.Workflows
{
    public class WorkflowEngine
    {
        public static IWorflowManager Mapper(CallContext context)
        {
            string codeBase = Assembly.GetCallingAssembly().Location;
            var assemblyFullPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(codeBase), "Eduegate.Domain.WorkflowEngine");
            var typeRefrence = Activator.CreateInstanceFrom(assemblyFullPath.Replace("file:\\", ""), "Eduegate.Domain.WorkflowEngine.StudentApplication.ApplicationWorkflow");
            return typeRefrence.Unwrap() as IWorflowManager;
        }

        public static void ProcessWorkflow()
        {

        }
    }
}
