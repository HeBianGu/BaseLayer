using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Define
{
    /// <summary> 日志记录器 </summary>
    public abstract class Logger
    {
        private static Logger
            logger = new Loggert_() { _log4net = log4net.LogManager.GetLogger("mdc") };

        /// <summary> 获取 Logger </summary>
        public static Logger GetLogger(object o)
        {
            if (o == null)
                return logger;
            ILog log = null;
            if (o is Type)
            {
                log = LogManager.GetLogger(o as Type);
            }
            else if (o is String)
            {
                log = LogManager.GetLogger(o.ToString());
            }
            else
            {
                log = LogManager.GetLogger(o.GetType());
            }
            return new Loggert_() { _log4net = log };
        }

        private ILog _log4net = null;//LogManager.GetLogger

        /// <summary>
        /// 摘要: 
        ///     Checks if this logger is enabled for the log4net.Core.Level.Debug level.
        ///
        /// 备注: 
        ///      This function is intended to lessen the computational cost of disabled log
        ///     debug statements.
        ///     For some ILog interface log, when you write:
        ///     log.Debug("This is entry number: " + i );
        ///     You incur the cost constructing the message, string construction and concatenation
        ///     in this case, regardless of whether the message is logged or not.
        ///     If you are worried about speed (who isn't), then you should write:
        ///     if (log.IsDebugEnabled) { log.Debug("This is entry number: " + i ); }
        ///     This way you will not incur the cost of parameter construction if debugging
        ///     is disabled for log. On the other hand, if the log is debug enabled, you
        ///     will incur the cost of evaluating whether the logger is debug enabled twice.
        ///     Once in log4net.ILog.IsDebugEnabled and once in the Debug(object). This is
        ///     an insignificant overhead since evaluating a logger takes about 1% of the
        ///     time it takes to actually log. This is the preferred style of logging.
        ///     Alternatively if your logger is available statically then the is debug enabled
        ///     state can be stored in a static variable like this:
        ///     private static readonly bool isDebugEnabled = log.IsDebugEnabled;
        ///     Then when you come to log you can write:
        ///     if (isDebugEnabled) { log.Debug("This is entry number: " + i ); }
        ///     This way the debug enabled state is only queried once when the class is loaded.
        ///     Using a private static readonly variable is the most efficient because it
        ///     is a run time constant and can be heavily optimized by the JIT compiler.
        ///     Of course if you use a static readonly variable to hold the enabled state
        ///     of the logger then you cannot change the enabled state at runtime to vary
        ///     the logging that is produced. You have to decide if you need absolute speed
        ///     or runtime flexibility.
        /// </summary>
        public bool IsDebugEnabled { get { return _log4net.IsDebugEnabled; } }

        ///<summary>
        /// 摘要: 
        ///     Checks if this logger is enabled for the log4net.Core.Level.Error level.
        ///
        /// 备注: 
        ///     For more information see log4net.ILog.IsDebugEnabled.
        /// </summary>
        public bool IsErrorEnabled { get { return _log4net.IsErrorEnabled; } }

        /// <summary>
        ///摘要: 
        ///    Checks if this logger is enabled for the log4net.Core.Level.Fatal level.
        ///
        ///备注: 
        ///    For more information see log4net.ILog.IsDebugEnabled.
        /// </summary>
        public bool IsFatalEnabled { get { return _log4net.IsFatalEnabled; } }

        /// <summary>
        /// 摘要: 
        ///     Checks if this logger is enabled for the log4net.Core.Level.Info level.
        ///
        /// 备注: 
        ///     For more information see log4net.ILog.IsDebugEnabled.
        /// </summary>
        public bool IsInfoEnabled { get { return _log4net.IsInfoEnabled; } }

        /// <summary>
        /// 摘要: 
        ///     Checks if this logger is enabled for the log4net.Core.Level.Warn level.
        /// 
        /// 备注: 
        ///     For more information see log4net.ILog.IsDebugEnabled.
        /// </summary>
        public bool IsWarnEnabled { get { return _log4net.IsWarnEnabled; } }

        /// <summary>
        /// 摘要: 
        ///     Log a message object with the log4net.Core.Level.Debug level.
        /// 备注: 
        ///      This method first checks if this logger is DEBUG enabled by comparing the
        ///     level of this logger with the log4net.Core.Level.Debug level. If this logger
        ///     is DEBUG enabled, then it converts the message object (passed as parameter)
        ///     to a string by invoking the appropriate log4net.ObjectRenderer.IObjectRenderer.
        ///     It then proceeds to call all the registered appenders in this logger and
        ///     also higher in the hierarchy depending on the value of the additivity flag.
        ///     WARNING Note that passing an System.Exception to this method will print the
        ///     name of the System.Exception but no stack trace. To print a stack trace use
        ///     the Debug(object,Exception) form instead.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Debug(object message)
        {
            _log4net.Debug(message);
        }

        /// <summary>
        /// 摘要: 
        ///     Log a message object with the log4net.Core.Level.Debug level including the
        ///     stack trace of the System.Exception passed as a parameter.
        ///
        /// 参数: 
        ///   message:
        ///     The message object to log.
        ///
        ///   exception:
        ///     The exception to log, including its stack trace.
        ///
        /// 备注: 
        ///      See the Debug(object) form for more detailed information. 
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public void Debug(object message, Exception exception)
        {
            _log4net.Debug(message, exception);
        }

        /// <summary> Logs a formatted message string with the log4net.Core.Level.Debug level.
        ///
        /// 参数: 
        ///   format:
        ///     A String containing zero or more format items
        ///
        ///   args:
        ///     An Object array containing zero or more objects to format
        ///
        /// 备注: 
        ///      The message is formatted using the String.Format method. See String.Format(string,
        ///     object[]) for details of the syntax of the format string and the behavior
        ///     of the formatting.
        ///     This method does not take an System.Exception object to include in the log
        ///     event. To pass an System.Exception use one of the Debug(object,Exception)
        ///     methods instead.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void DebugFormat(string format, params object[] args)
        {
            _log4net.DebugFormat(format, args);
        }

        /// <summary> Logs a formatted message string with the log4net.Core.Level.Debug level.
        ///
        /// 参数: 
        ///   provider:
        ///     An System.IFormatProvider that supplies culture-specific formatting information
        ///
        ///   format:
        ///     A String containing zero or more format items
        ///
        ///   args:
        ///     An Object array containing zero or more objects to format
        ///
        /// 备注: 
        ///      The message is formatted using the String.Format method. See String.Format(string,
        ///     object[]) for details of the syntax of the format string and the behavior
        ///     of the formatting.
        ///     This method does not take an System.Exception object to include in the log
        ///     event. To pass an System.Exception use one of the Debug(object,Exception)
        ///     methods instead.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log4net.DebugFormat(provider, format, args);
        }

        /// <summary> Logs a message object with the log4net.Core.Level.Error level.
        ///
        /// 备注: 
        ///      This method first checks if this logger is ERROR enabled by comparing the
        ///     level of this logger with the log4net.Core.Level.Error level. If this logger
        ///     is ERROR enabled, then it converts the message object (passed as parameter)
        ///     to a string by invoking the appropriate log4net.ObjectRenderer.IObjectRenderer.
        ///     It then proceeds to call all the registered appenders in this logger and
        ///     also higher in the hierarchy depending on the value of the additivity flag.
        ///     WARNING Note that passing an System.Exception to this method will print the
        ///     name of the System.Exception but no stack trace. To print a stack trace use
        ///     the Error(object,Exception) form instead.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Error(object message)
        {
            _log4net.Error(message);
        }

        /// <summary>
        /// 摘要: 
        ///     Log a message object with the log4net.Core.Level.Error level including the
        ///     stack trace of the System.Exception passed as a parameter.
        ///
        /// 备注: 
        ///      See the Error(object) form for more detailed information.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public void Error(object message, Exception exception)
        {
            _log4net.Error(message, exception);
        }

        /// <summary> Logs a formatted message string with the log4net.Core.Level.Error level.
        /// 参数: 
        ///   format:
        ///     A String containing zero or more format items
        ///
        ///   args:
        ///     An Object array containing zero or more objects to format
        ///
        /// 备注: 
        ///      The message is formatted using the String.Format method. See String.Format(string,
        ///     object[]) for details of the syntax of the format string and the behavior
        ///     of the formatting.
        ///     This method does not take an System.Exception object to include in the log
        ///     event. To pass an System.Exception use one of the Error(object) methods instead.
        /// 
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void ErrorFormat(string format, params object[] args)
        {
            _log4net.ErrorFormat(format, args);
        }

        /// <summary> Logs a formatted message string with the log4net.Core.Level.Error level.
        /// 参数: 
        ///   provider:
        ///     An System.IFormatProvider that supplies culture-specific formatting information
        ///
        ///   format:
        ///     A String containing zero or more format items
        ///
        ///   args:
        ///     An Object array containing zero or more objects to format
        ///
        /// 备注: 
        ///      The message is formatted using the String.Format method. See String.Format(string,
        ///     object[]) for details of the syntax of the format string and the behavior
        ///     of the formatting.
        ///     This method does not take an System.Exception object to include in the log
        ///     event. To pass an System.Exception use one of the Error(object) methods instead.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log4net.ErrorFormat(provider, format, args);
        }

        /// <summary> Log a message object with the log4net.Core.Level.Fatal level.
        ///     
        /// 备注: 
        ///      This method first checks if this logger is FATAL enabled by comparing the
        ///     level of this logger with the log4net.Core.Level.Fatal level. If this logger
        ///     is FATAL enabled, then it converts the message object (passed as parameter)
        ///     to a string by invoking the appropriate log4net.ObjectRenderer.IObjectRenderer.
        ///     It then proceeds to call all the registered appenders in this logger and
        ///     also higher in the hierarchy depending on the value of the additivity flag.
        ///     WARNING Note that passing an System.Exception to this method will print the
        ///     name of the System.Exception but no stack trace. To print a stack trace use
        ///     the Fatal(object,Exception) form instead. 
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Fatal(object message)
        {
            _log4net.Fatal(message);
        }

        /// <summary>
        /// 摘要: 
        ///     Log a message object with the log4net.Core.Level.Fatal level including the
        ///     stack trace of the System.Exception passed as a parameter.
        ///
        /// 参数: 
        ///   message:
        ///     The message object to log.
        ///
        ///   exception:
        ///     The exception to log, including its stack trace.
        ///
        /// 备注: 
        ///      See the Fatal(object) form for more detailed information.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public void Fatal(object message, Exception exception)
        {
            _log4net.Fatal(message, exception);
        }

        /// <summary> Logs a formatted message string with the log4net.Core.Level.Fatal level.
        ///
        /// 备注: 
        ///      The message is formatted using the String.Format method. See String.Format(string,
        ///     object[]) for details of the syntax of the format string and the behavior
        ///     of the formatting.
        ///     This method does not take an System.Exception object to include in the log
        ///     event. To pass an System.Exception use one of the Fatal(object) methods instead.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void FatalFormat(string format, params object[] args)
        {
            _log4net.FatalFormat(format, args);
        }

        /// <summary> Logs a formatted message string with the log4net.Core.Level.Fatal level.
        /// 参数: 
        ///   provider:
        ///     An System.IFormatProvider that supplies culture-specific formatting information
        ///
        ///   format:
        ///     A String containing zero or more format items
        ///
        ///   args:
        ///     An Object array containing zero or more objects to format
        ///
        /// 备注: 
        ///      The message is formatted using the String.Format method. See String.Format(string,
        ///     object[]) for details of the syntax of the format string and the behavior
        ///     of the formatting.
        ///     This method does not take an System.Exception object to include in the log
        ///     event. To pass an System.Exception use one of the Fatal(object) methods instead.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format"> A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log4net.FatalFormat(provider, format, args);
        }

        /// <summary> Logs a message object with the log4net.Core.Level.Info level.
        /// 备注: 
        ///      This method first checks if this logger is INFO enabled by comparing the
        ///     level of this logger with the log4net.Core.Level.Info level. If this logger
        ///     is INFO enabled, then it converts the message object (passed as parameter)
        ///     to a string by invoking the appropriate log4net.ObjectRenderer.IObjectRenderer.
        ///     It then proceeds to call all the registered appenders in this logger and
        ///     also higher in the hierarchy depending on the value of the additivity flag.
        ///     WARNING Note that passing an System.Exception to this method will print the
        ///     name of the System.Exception but no stack trace. To print a stack trace use
        ///     the Info(object,Exception) form instead.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Info(object message)
        {
            _log4net.Info(message);
        }

        /// <summary>
        /// 摘要: 
        ///     Logs a message object with the INFO level including the stack trace of the
        ///     System.Exception passed as a parameter.
        ///
        /// 备注: 
        ///      See the Info(object) form for more detailed information.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public void Info(object message, Exception exception)
        {
            _log4net.Info(message, exception);
        }

        /// <summary>
        /// 摘要: 
        ///     Logs a formatted message string with the log4net.Core.Level.Info level.
        ///
        /// 参数: 
        ///   format:
        ///     A String containing zero or more format items
        ///
        ///   args:
        ///     An Object array containing zero or more objects to format
        ///
        /// 备注: 
        ///      The message is formatted using the String.Format method. See String.Format(string,
        ///     object[]) for details of the syntax of the format string and the behavior
        ///     of the formatting.
        ///     This method does not take an System.Exception object to include in the log
        ///     event. To pass an System.Exception use one of the Info(object) methods instead.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void InfoFormat(string format, params object[] args)
        {
            _log4net.InfoFormat(format, args);
        }

        /// <summary> Logs a formatted message string with the log4net.Core.Level.Info level.
        /// 备注: 
        ///      The message is formatted using the String.Format method. See String.Format(string,
        ///     object[]) for details of the syntax of the format string and the behavior
        ///     of the formatting.
        ///     This method does not take an System.Exception object to include in the log
        ///     event. To pass an System.Exception use one of the Info(object) methods instead.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log4net.InfoFormat(provider, format, args);
        }

        /// <summary> Log a message object with the log4net.Core.Level.Warn level.
        ///
        /// 备注: 
        ///      This method first checks if this logger is WARN enabled by comparing the
        ///     level of this logger with the log4net.Core.Level.Warn level. If this logger
        ///     is WARN enabled, then it converts the message object (passed as parameter)
        ///     to a string by invoking the appropriate log4net.ObjectRenderer.IObjectRenderer.
        ///     It then proceeds to call all the registered appenders in this logger and
        ///     also higher in the hierarchy depending on the value of the additivity flag.
        ///     WARNING Note that passing an System.Exception to this method will print the
        ///     name of the System.Exception but no stack trace. To print a stack trace use
        ///     the Warn(object,Exception) form instead.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Warn(object message)
        {
            _log4net.Warn(message);
        }

        /// <summary>
        /// 摘要: 
        ///     Log a message object with the log4net.Core.Level.Warn level including the
        ///     stack trace of the System.Exception passed as a parameter.
        ///
        /// 备注: 
        ///      See the Warn(object) form for more detailed information.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public void Warn(object message, Exception exception)
        {
            _log4net.Warn(message, exception);
        }

        /// <summary> Logs a formatted message string with the log4net.Core.Level.Warn level.
        ///
        ///备注: 
        ///     The message is formatted using the String.Format method. See String.Format(string,
        ///    object[]) for details of the syntax of the format string and the behavior
        ///    of the formatting.
        ///    This method does not take an System.Exception object to include in the log
        ///    event. To pass an System.Exception use one of the Warn(object) methods instead.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void WarnFormat(string format, params object[] args)
        {
            _log4net.WarnFormat(format, args);
        }

        /// <summary>
        /// 摘要: 
        ///     Logs a formatted message string with the log4net.Core.Level.Warn level.
        ///
        /// 备注: 
        ///      The message is formatted using the String.Format method. See String.Format(string,
        ///     object[]) for details of the syntax of the format string and the behavior
        ///     of the formatting.
        ///     This method does not take an System.Exception object to include in the log
        ///     event. To pass an System.Exception use one of the Warn(object) methods instead.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log4net.WarnFormat(provider, format, args);
        }

        internal class Loggert_ : Logger { }
    }
}
