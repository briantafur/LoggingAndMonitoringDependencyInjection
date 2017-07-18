using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Logger.Services
{
    public class AppInsight : ILoggerInterface
    {
        readonly TelemetryClient Client;

        public AppInsight(String key)
        {
            TelemetryConfiguration.Active.InstrumentationKey = key;
            Client = new TelemetryClient();
        }

        #region methods

        public void Info(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Client.TrackTrace("[Information] " + component.ToString() + " " + methodName + ": " + message);
        }

        public void Debug(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Client.TrackTrace("[Debug] " + component.ToString() + " " + methodName + ": " + message);
        }

        public void Error(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            Client.TrackException(exception);
        }

        public void Warning(string message, Type component, [CallerMemberName] string methodName = "")
        {
            Client.TrackTrace("[Warning] " + component.ToString() + " " + methodName + ": " + message);
        }

        public void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            Client.TrackException(exception);
        }

        #endregion

        #region Async methods

        public async Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Client.TrackTrace("[Information] " + component.ToString() + " " + methodName + ": " + message);
            });
        }

        public async Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Client.TrackTrace("[Debug] " + component.ToString() + " " + methodName + ": " + message);
            });
        }

        public async Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Client.TrackException(exception);
            });
        }

        public async Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Client.TrackTrace("[Warning] " + component.ToString() + " " + methodName + ": " + message);
            });
        }

        public async Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                Client.TrackException(exception);
            });
        }

        #endregion

    }
}
