using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace Logger.Services
{
    class AmazonS3 : ILoggerInterface
    {
        public void Debug(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task DebugAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task ErrorAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task FatalAsync(Exception exception, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task InfoAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public void Warning(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }

        public Task WarningAsync(string message, Type component, [CallerMemberName] string methodName = "")
        {
            throw new NotImplementedException();
        }
    }
}
