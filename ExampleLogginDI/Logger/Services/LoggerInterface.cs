using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Services
{
    public interface LoggerInterface
    {
        void Info(Object msg);
        void Debug(Object msg);
        void Error(Exception ex, Object msg);
        void Warning(Object msg);
        void Fatal(Exception ex, Object msg);
    }
}
