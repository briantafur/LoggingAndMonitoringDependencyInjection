using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.Services;
using System.Runtime.CompilerServices;

namespace ExampleLogginDI.Utils
{
    public class LoggerBlobStorage
    {

        private readonly ILoggerInterface _log;


        public LoggerBlobStorage(ILoggerInterface log)
        {
            _log = log;
        }

        public void InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {

            lock (_log.InfoAsync(message, component, methodName));

        }



    }
}
