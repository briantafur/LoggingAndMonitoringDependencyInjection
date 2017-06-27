using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.Service
{
    public class Log4Net : ILogger
    {
        public void Error(object msg, Exception e)
        {
            throw new NotImplementedException();
        }

        public string Log(object msg)
        {
            return "Creando prueba de logger...";
        }
    }
}
