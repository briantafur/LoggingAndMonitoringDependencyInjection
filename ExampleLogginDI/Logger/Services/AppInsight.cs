using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Logger.Services
{
    public class AppInsight : LoggerInterface
    {
        TelemetryClient client;

        public AppInsight(String key)
        {
            TelemetryConfiguration.Active.InstrumentationKey = key;
            client = new TelemetryClient();
        }

        #region methods

        public void Info(string message, Type component, [CallerMemberName] string methodName = "")
        {
            client.TrackTrace("[Information] " + component.ToString() + " " + methodName + ": " + message);
        }

        public void Debug(string message, Type component, [CallerMemberName] string methodName = "")
        {
            client.TrackTrace("[Debug] "+component.ToString() + " " + methodName + ": " + message);
        }

        public void Error(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            client.TrackException(exception);
        }

        public void Warning(string message, Type component, [CallerMemberName] string methodName = "")
        {
            client.TrackTrace("[Warning] " + component.ToString() + " " + methodName + ": " + message);
        }

        public void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            client.TrackException(exception);
        }
        
        #endregion

        #region Async methods

        public async Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                client.TrackTrace("[Information] " + component.ToString() + " " + methodName + ": " + message);
            });            
        }

        public async Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                client.TrackTrace("[Debug] " + component.ToString() + " " + methodName + ": " + message);
            });
        }

        public async Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                client.TrackException(exception);
            });
        }

        public async Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                client.TrackTrace("[Warning] " + component.ToString() + " " + methodName + ": " + message);
            });
        }

        public async Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() => {
                client.TrackException(exception);
            });
        }

        #endregion

    }
}
