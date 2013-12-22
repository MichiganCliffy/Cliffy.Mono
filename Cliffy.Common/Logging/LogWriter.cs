//==============================================================================
//
//  File:        LogWriter.cs
//
//------------------------------------------------------------------------------
//
//  SUMMARY:     Allows Developers to Log messages to various log4net appenders
//
//==============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Cliffy.Common;
using log4net;
using log4net.Config;

namespace Cliffy.Common.Logging
{
    /// <summary>
    /// The LogWriter class is utilized for central logging of application events in GenX Framework Based applications.  
    /// it wraps log4net logging facilities.  
    ///
    ///  
    /// Centralized logging supports the following LogTypes defined by the LogTypes Enum:
    /// <para>
    ///	TraceLog: Used by developers for writing detailed events for stepping through code and seeing whats going on, in order to assist debuging. </para>
    /// <para>AuditLog: Used for system events of importance like counters. For example, Requests since last reset.</para>
    /// <para>AdminLog: Used for administration events of importance.  For Example, imagine administrators being notified of an individual failing to acess a resource multiple times, due to invalid security credentials. </para>
    /// <para>ErrorLog: Used for major application errors.  Requires a ErrorSeverity level defined by the Enum ErrorSeverity.  Requires and Error Code which should be developed during application development and relate to Unit Tests and Functional Tests.</para>
    /// </summary>
    [Serializable()]
    [ComVisible(false)]
    public class LogWriter : IDisposable
    {
        [NonSerialized()]
        private string eventSource = string.Empty;

        [NonSerialized()]
        private ILog myLog;

        /// <summary>
        /// Creates an Instance of LogWriter class. Does not initialize the Event source, so errors will come from the containing executable.
        /// </summary>
        public LogWriter()
        {
            try
            {
                CommonConstruct(AppDomain.CurrentDomain.FriendlyName);
                if (this.myLog == null)
                {
                    CommonConstruct("default");
                    this.Log("AppDomain Not available to security", LogType.ErrorLog);
                }
            }
            catch
            {
                CommonConstruct("default");
                this.Log("AppDomain Not available to security", LogType.ErrorLog);
            }
        }

        /// <summary>
        /// Creates an instance fo LogWriter class, and sets its source to string passed in.  Sources are defined and configured in the EnterpriseInstrumentation.config.
        /// </summary>
        /// <param name="source">Source parameter is the event source for the instrumented event.  Examples: ShoppingCart, Search, BatchProcessing</param>
        public LogWriter(string source)
        {
            CommonConstruct(source);
        }

        private void CommonConstruct(string source)
        {
            try
            {
                this.eventSource = source;
                this.myLog = LogManager.GetLogger(this.eventSource);
            }
            catch (Exception ex)
            {
                LastChanceExceptionHandler(ex);
            }
        }

        /// <summary>
        /// Logs messages to the TraceLog
        /// </summary>
        /// <param name="message">Message to send to Event sinks</param>
        public void Log(string message)
        {
            this.Log(message, LogType.TraceLog);

        }

        /// <summary>
        /// Logs messages to the specified LogType
        /// </summary>
        /// <param name="message">Mesage to send to event sinks</param>
        /// <param name="outputLog">Type of event to send, as defined by LogType enum.  Types: Trace, Admin, Audit, and Error</param>
        public void Log(string message, LogType outputLog)
        {
            switch (outputLog)
            {
                case LogType.AuditLog:
                    this.AuditLog(message);
                    break;

                case LogType.ErrorLog:
                    this.ErrorLog(message);
                    break;

                default:
                case LogType.TraceLog:
                    this.TraceLog(message);
                    break;
            }
        }

        /// <summary>
        /// Wraps Microsft EIF ErrorMessageEvent.Raise method.  Utilized for sending application errors
        /// </summary>
        /// <param name="message">Message to send to event sinks</param>
        /// <param name="severity">Severity as defined by ErrorSeverity Enum</param>
        /// <param name="errorCode">Error code as defined by runbook</param>
        private void ErrorLog(string message)
        {
            try
            {
                if (myLog == null)
                {
                    //ErrorMessageEvent.Raise(message, (int)severity, errorCode);
                    myLog = LogManager.GetLogger(this.eventSource);
                }

                myLog.Error(message);
            }
            catch (Exception ex)
            {
                LastChanceExceptionHandler(ex);
            }
        }

        /// <summary>
        /// Wraps Microsoft EIF AuditMessageEvent.Raise() method.  Responsible for sending audit messages from application.
        /// </summary>
        /// <param name="message">Message to send to Audit Listeners</param>
        private void AuditLog(string message)
        {
            try
            {
                if (null == myLog)
                {
                    myLog = LogManager.GetLogger(this.eventSource);
                }

                myLog.Info(message);
            }
            catch (Exception ex)
            {
                LastChanceExceptionHandler(ex);
            }
        }

        /// <summary>
        /// Wraps Microsoft EIF TraceMessageEvent.Raise() method.
        /// </summary>
        /// <param name="message"></param>
        private void TraceLog(string message)
        {
            try
            {
                if (null == myLog)
                {
					Debugger.Log(0, eventSource, message);
                }
                else
                {
                    myLog.Debug(message);
                }
            }
            catch (Exception ex)
            {
                LastChanceExceptionHandler(ex);
            }
        }

        private static void LastChanceExceptionHandler(Exception ex)
        {
            //FUTURE : Add Basic Trace File Logging / Event Logging upon failure.
        }

        #region IDisposable Members

        public void Dispose()
        {

            if (this.myLog != null)
            {
                //this.myLog.Dispose();
                this.myLog = null;
            }
        }

        #endregion
    }
}