using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace Logger.Services
{
    public class Serilog : LoggerInterface
    {

        public Serilog()
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.LiterateConsole()
               .WriteTo.RollingFile("logs\\Log-{Date}.txt")
               .CreateLogger();
        }

        public void Info(object msg)
        {
            Log.Logger.Information(msg.ToString());
        }

        public void Debug(object msg)
        {
            Log.Debug(msg.ToString());
        }

        public void Error(Exception ex, Object msg)
        {
            Log.Error(ex, msg.ToString());
        }

        public void Warning(object msg)
        {
            Log.Warning(msg.ToString());
        }

        public void Fatal(Exception ex, object msg)
        {
            Log.Fatal(ex, msg.ToString());
        }
    }
}
