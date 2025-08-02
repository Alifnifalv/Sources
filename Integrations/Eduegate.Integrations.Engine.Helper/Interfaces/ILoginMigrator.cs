using Eduegate.Services.Contracts.School.Common;
using System.Collections.Generic;

namespace Eduegate.Integrations.Engine.Helper
{
    public interface ILoginMigrator
    {
        public List<LoginsDTO> GetLoginData();
    }
}
