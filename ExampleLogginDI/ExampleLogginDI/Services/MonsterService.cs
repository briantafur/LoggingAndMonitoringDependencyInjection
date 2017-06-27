using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleLogginDI.Models;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Core;
using System.Xml;
using System.IO;
using log4net.Repository;
using log4net.Config;

namespace ExampleLogginDI.Services
{
    public class MonsterService : IMonsterService
    {

        private ILog log;

        public MonsterService()
        {
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log = LogManager.GetLogger(typeof(MonsterService));

        }

        public void Error(object msg, Exception e)
        {
            log.Error("haciendo una prueba de error...", e);
        }

        public String Log(object msg)
        {
            log.Error("haciendo prueba....");
            return "haciendo prueba de log";
        }
        
    }
}
