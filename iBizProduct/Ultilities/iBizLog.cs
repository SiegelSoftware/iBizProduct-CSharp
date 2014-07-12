using System;
using System.Diagnostics;

namespace iBizProduct.Ultilities
{
    /// <summary>
    /// Provides a base class for logging issues to the Event Log
    /// </summary>
    public class iBizLog
    {
        /// <summary>
        /// Event Log Name set by the Environment
        /// </summary>
        public static string EventLogName
        {
            get
            {
                string logName = Environment.GetEnvironmentVariable( "EventLogName" );

                if( String.IsNullOrEmpty( logName ) ) return "iBizProduct";

                return logName;
            }
        }
        /// <summary>
        /// Event Log Source set by the Environment
        /// </summary>
        public static string EventLogSource
        {
            get
            {
                string logSource = Environment.GetEnvironmentVariable( "EventLogSource" );

                if( String.IsNullOrEmpty( logSource ) ) return "iBizProduct v3";

                return logSource;
            }
        }

        private static readonly EventLog log;

        static iBizLog()
        {
            log = new EventLog( EventLogName )
            {
                Source = EventLogSource,
            };
        }

        /// <summary>
        /// Write an informational message to the Event Log with specified Cultural arguments
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="args">Cultural Arguments</param>
        public static void WriteInformation( string message, params object[] args )
        {
            WriteInformation( message.FormatCurrentCulture( args ) );
        }

        /// <summary>
        /// Write an informational message to the Event Log
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void WriteInformation( string message )
        {
            log.WriteEntry( message, EventLogEntryType.Information );
        }

        /// <summary>
        /// Write an error message to the Event Log
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void WriteError( string message )
        {
            log.WriteEntry( message, EventLogEntryType.Error );
        }

        /// <summary>
        /// Write an error message to the Event Log with specified Cultural arguments
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="args">Cultural Arguments</param>
        public static void WriteError( string message, params object[] args )
        {
            WriteError( message.FormatCurrentCulture( args ) );
        }

        /// <summary>
        /// Write a warning message to the Event Log
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void WriteWarning( string message )
        {
            log.WriteEntry( message, EventLogEntryType.Warning );
        }

        /// <summary>
        /// Write a warning message to the Event Log with specified Cultural arguments
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="args">Cultural Arguments</param>
        public static void WriteWarning( string message, params object[] args )
        {
            WriteWarning( message.FormatCurrentCulture( args ) );
        }

        /// <summary>
        /// Write an Exception to the Event Log
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="Verbose">Boolean to indicate whether to recurse the Inner Exception. [Default = true]</param>
        public static void WriteException( Exception ex, bool Verbose = true )
        {
            var message = "\r\nLog Entry : ";
            message += DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
            message += "  :";
            message += "  :Exception Thrown: " + ex.GetType();
            message += "  :Message: " + ex.Message;
            message += "  :Stack Trace:\r\n" + ex.StackTrace;

            if( Verbose )
                message += LogInnerException( ex.InnerException );

            message += "-------------------------------";

            log.WriteEntry( message, EventLogEntryType.FailureAudit );
        }

        private static string LogInnerException( Exception ex, string message = "" )
        {
            if( ex != null )
            {
                message += "\r\n  :Inner Exception";
                message += "  :Exception Thrown: " + ex.GetType();
                message += "  :Message: " + ex.Message;
                message += "  :Stack Trace:\r\n" + ex.StackTrace;

                message += LogInnerException( ex.InnerException, message );
            }

            return message;
        }

        internal static void CreateEventSource()
        {
            EventLog.CreateEventSource( EventLogSource, EventLogName );
        }
    }
}
