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
    public interface LoggerInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="component"></param>
        /// <param name="methodName"></param>
        void Info(String message, Type component, [CallerMemberName] string methodName = "");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="component"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        Task InfoAsync(String message, Type component, [CallerMemberName] string methodName = "");
        void Debug(String message, Type component, [CallerMemberName] string methodName = "");
        Task DebugAsync(String message, Type component, [CallerMemberName] string methodName = "");
        void Error(Exception exception, Type component, [CallerMemberName] string methodName = "");
        Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "");
        void Warning(String message, Type component, [CallerMemberName] string methodName = "");
        Task WarningAsync(String message, Type component, [CallerMemberName] string methodName = "");
        void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "");
        Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "");
    }
}
