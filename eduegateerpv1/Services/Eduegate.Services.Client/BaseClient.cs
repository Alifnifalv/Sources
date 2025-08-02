using System;
using Eduegate.Framework;

namespace Eduegate.Service.Client
{
    public class BaseClient
    {
        protected CallContext _callContext = null;
        protected Action<string> _logger = null;
        private CallContext context;
        private Action<string> logger;

        public BaseClient(CallContext callContext = null, Action<string> logger = null)
        {
            //TODO Generate call context from the cookie
            _callContext = callContext;
            _logger = logger;         
        }

        public void WriteLog(string message)
        {
            if (_logger != null)
            {
                _logger(message);
            }
        }
    }
}
