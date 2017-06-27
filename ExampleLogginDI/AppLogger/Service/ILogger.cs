using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.Service
{
    public interface ILogger
    {
        String Log(Object msg);
        void Error(Object msg, Exception e);
    }
}
