using Eduegate.Services.Contracts.School.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eduegate.Integrations.Engine.Helper
{
    public interface ILoginMigrator
    {
        public List<LoginDTO> GetLoginData();
    }
}
