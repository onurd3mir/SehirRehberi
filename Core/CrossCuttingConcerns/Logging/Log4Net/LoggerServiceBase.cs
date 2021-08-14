using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LoggerServiceBase
    {
        private ILog _log;

        public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4net.config"));

            ILoggerRepository loggerRepository = 
                LogManager.CreateRepository(Assembly.GetEntryAssembly(),typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(loggerRepository,xmlDocument["log4net"]);

            _log = LogManager.GetLogger(loggerRepository.Name, name);

        }

        public bool IsInfoEnabled => _log.IsInfoEnabled;
        public bool IsDebugEnabled => _log.IsDebugEnabled;
        public bool IsWarnEnabled => _log.IsWarnEnabled;
        public bool IsFatalEnabled => _log.IsFatalEnabled;
        public bool IsErrprEnabled => _log.IsErrorEnabled;

        public void Info(object logmessage)
        {
            if(IsInfoEnabled) _log.Info(logmessage);
        }

        public void Debug(object logmessage)
        {
            if (IsDebugEnabled) _log.Debug(logmessage);
        }

        public void Warn(object logmessage)
        {
            if (IsWarnEnabled) _log.Warn(logmessage);
        }

        public void Fatal(object logmessage)
        {
            if (IsFatalEnabled) _log.Fatal(logmessage);
        }

        public void Error(object logmessage)
        {
            if (IsErrprEnabled) _log.Error(logmessage);
        }


    }
}
