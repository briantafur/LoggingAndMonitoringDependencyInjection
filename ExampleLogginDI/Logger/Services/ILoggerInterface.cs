using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Services
{
    /// <summary>
    /// Interface con las firmas de los métodos necesarios para la creación de loggers.
    /// </summary>
    public interface ILoggerInterface
    {
        /// <summary>
        /// Function to track events of interest through log files.
        /// </summary>
        /// <param name="message">The message that want to be included in the log file</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        void Info(String message, Type component, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Function to track events of interest through log files in an async way.
        /// </summary>
        /// <param name="message">The message that want to be included in the log file</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        /// <returns></returns>
        Task InfoAsync(String message, Type component, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Function to track internal flow and helps to facilitate the recognition of problems.
        /// </summary>
        /// <param name="message">The message that want to be included in the log file</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        void Debug(String message, Type component, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Function to track internal flow and helps to facilitate the recognition of problems in an async way.
        /// </summary>
        /// <param name="message">The message that want to be included in the log file</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        /// <returns></returns>
        Task DebugAsync(String message, Type component, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Function to track a failure in the application.
        /// </summary>
        /// <param name="exception">The exception that want to be tracked</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        void Error(Exception exception, Type component, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Function to track a failure in the application in an async way.
        /// </summary>
        /// <param name="exception">The exception that want to be tracked</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        /// <returns></returns>
        Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Function to track possible issues in the functionality of the application.
        /// </summary>
        /// <param name="message">The message that want to be included in the log file</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        void Warning(String message, Type component, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Function to track possible issues in the functionality of the application in an async way.
        /// </summary>
        /// <param name="message">The message that want to be included in the log file</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        /// <returns></returns>
        Task WarningAsync(String message, Type component, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Function to track critical errors that cause a complete failure in the functionality of the application.
        /// </summary>
        /// <param name="exception">The exception that want to be tracked</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Function to track critical errors that cause a complete failure in the functionality of the application in an async way.
        /// </summary>
        /// <param name="exception">The exception that want to be tracked</param>
        /// <param name="component">The type of the actual instance of the object that invoke this function</param>
        /// <param name="methodName">Name of the function that invoke this function</param>
        /// <returns></returns>
        Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "");
    }
}
