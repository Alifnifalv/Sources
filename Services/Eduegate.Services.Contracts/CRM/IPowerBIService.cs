using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Leads;
using Microsoft.PowerBI.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.CRM
{
    public interface IPowerBIService
    {

        Task<EmbedToken> GetEmbedToken();

    }
}