using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        #region methods

        public void Info(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Log.Logger.Information(component.ToString()+ " " + methodName + ": " + message, component);
        }

        public void Debug(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Log.Debug(component.ToString() + " " + methodName + ": " + message, component);
        }

        public void Error(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            Log.Error(exception, component.ToString() + " " + methodName, component);
        }

        public void Warning(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Log.Warning(component.ToString() + " " + methodName + ": " + message, component);
        }

        public void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            Log.Fatal(exception, component.ToString() + " " + methodName, component);
        }

        #endregion

        #region Async methods

        public async Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Log.Logger.Information(component.ToString() + " " + methodName + ": " + message, component);
            });
        }

        public async Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Log.Debug(component.ToString() + " " + methodName + ": " + message, component);
            });
        }

        public async Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Log.Error(exception, component.ToString() + " " + methodName, component);
            });
        }

        public async Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Log.Warning(component.ToString() + " " + methodName + ": " + message, component);
            });
        }

        public async Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Log.Fatal(exception, component.ToString() + " " + methodName, component);
            });
        }

        #endregion
    }
}
