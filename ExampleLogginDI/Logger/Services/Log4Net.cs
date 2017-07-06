using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net.Repository;

namespace Logger.Services
{
    public class Log4Net : LoggerInterface
    {

        public readonly ILog log = LogManager.GetLogger(typeof(Log4Net));

        public Log4Net()
        {
            Setup();
        }

        #region Configuración

        public static void Setup()
        {
            ILoggerRepository[] nada = LogManager.GetAllRepositories();
            for (int i = 0; i < nada.Length; i++)
            {
                Hierarchy hierarchy = (Hierarchy)nada[i];

                PatternLayout patternLayout = new PatternLayout();
                patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
                patternLayout.ActivateOptions();
                String date = DateTime.Now.ToString("yyyyMMdd");
                RollingFileAppender roller = new RollingFileAppender();
                roller.AppendToFile = true; //revisar
                roller.File = @"logs\\Log-" + date + ".txt";
                roller.Layout = patternLayout;
                roller.MaxSizeRollBackups = 5;
                roller.MaximumFileSize = "1GB";
                roller.RollingStyle = RollingFileAppender.RollingMode.Size;
                roller.StaticLogFileName = true;
                roller.ActivateOptions();
                hierarchy.Root.AddAppender(roller);

                MemoryAppender memory = new MemoryAppender();
                memory.ActivateOptions();
                hierarchy.Root.AddAppender(memory);

                hierarchy.Root.Level = Level.Info;
                hierarchy.Configured = true;
            }
        }

        #endregion

        #region Methods

        public void Info(string message, Type component, [CallerMemberName] string methodName = "")
        {
            log.Info(component.ToString() + " " + methodName + ": " + message);
        }

        public void Debug(string message, Type component, [CallerMemberName] string methodName = "")
        {
            log.Debug(component.ToString() + " " + methodName + ": " + message);
        }

        public void Error(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            log.Error(component.ToString() + " " + methodName, exception);
        }

        public void Warning(string message, Type component, [CallerMemberName] string methodName = "")
        {
            log.Warn(component.ToString() + " " + methodName + ": " + message);
        }

        public void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            log.Fatal(component.ToString() + " " + methodName, exception);
        }

        #endregion

        #region Async Methods

        public async Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() =>
            {
                log.Info(component.ToString() + " " + methodName + ": " + message);
            });
        }

        public async Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() =>
            {
                log.Debug(component.ToString() + " " + methodName + ": " + message);
            });
        }

        public async Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() =>
            {
                log.Error(component.ToString() + " " + methodName, exception);
            });
        }

        public async Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() =>
            {
                log.Warn(component.ToString() + " " + methodName + ": " + message);
            });
        }

        public async Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            await Task.Factory.StartNew(() =>
            {
                log.Fatal(component.ToString() + " " + methodName, exception);
            });
        }

        #endregion

    }
}
