// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Diagnostics;
using System.IO;

namespace iBizProduct
{
    /// <summary>
    /// Exceptions thrown by iBizProduct
    /// </summary>
    [Serializable]
    public class iBizException : Exception
    {
        private const string defaultMessage = "An iBizException has been thrown";
        private const string logFile = @"C:\iBizProduct\ErrorLog";
        private const string logFileDirectory = @"C:\iBizProduct";
        private EventLog iBizEventLog = iBizProductSettings.EventLog;
        private EventLogEntryType EntryType = EventLogEntryType.Error;

        /// <summary>
        /// Instantiate an iBizException with the default message
        /// </summary>
        public iBizException() : base( defaultMessage ) { LogError(); }

        /// <summary>
        /// Instantiate an iBizException with a custom message
        /// </summary>
        /// <param name="message">Message for the Exception</param>
        public iBizException( string message ) : base( message ) { LogError( message ); }

        /// <summary>
        /// Instantiate an iBizException with a custom message. Allows you to include an Inner Exception. Also
        /// optionally allows you to override the default EventLogEntryType or Error. You may also specify the
        /// name of a custom EventLog to write to by specifying the EventLogName and/or EventLogSource.
        /// </summary>
        /// <param name="message">Message for the Exception</param>
        /// <param name="inner">Inner Exception</param>
        /// <param name="LogType">EventLogEntryType</param>
        /// <param name="LogName">EventLogName</param>
        /// <param name="EventLogSource">EventLogSource</param>
        public iBizException( string message, Exception inner, EventLogEntryType LogType = EventLogEntryType.Error, string LogName = "", string EventLogSource = "" ) : base( message, inner ) 
        {
            this.EntryType = LogType;

            if( !String.IsNullOrEmpty( LogName ) && !String.IsNullOrEmpty( EventLogSource ) )
            {
                if( EventLog.SourceExists( EventLogSource ) )
                {
                    try
                    {
                        // We want to catch this and rethrow as an iBizException if the application does not have the appropriate 
                        // permissions to create the log. 
                        EventLog.CreateEventSource( new EventSourceCreationData( EventLogSource, LogName ) );
                        this.iBizEventLog.Log = LogName;
                    }
                    catch( Exception )
                    { }
                }
            }

            LogError( message ); 
        }

        private void LogError( string message = defaultMessage )
        {
            var eventId = iBizEventLog.Entries.Count + 1;

            string ErrorMessage = "Exception thrown: " + this.GetType() + "\r\n\r\n" +
                "Message: " + this.Message + "\r\n" +
                "Stack Trace:\r\n" + this.StackTrace + "\r\n" + LogException( this.InnerException );

            if( iBizEventLog == null )
                iBizEventLog.WriteEntry( ErrorMessage, EventLogEntryType.Error, eventId );

            // The following is considered depricated and will be removed in the near future. 
            Console.WriteLine( message );

            StreamWriter w = null;
            try
            {
                //Checks if directory exists, if not attempts to create it
                Directory.CreateDirectory(logFileDirectory);

                w = File.AppendText( logFile );
                w.Write( "\r\nLog Entry : " );
                w.WriteLine( "{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString() );
                w.WriteLine( "  :" );
                w.WriteLine( LogException( this ) );
                w.WriteLine( "-------------------------------" );
            }
            finally
            {
                w.Close();
            }
        }

        private static string LogException( Exception ex, bool InnerException = false )
        {
            string message = string.Format( "  :Exception Thrown: {0}\r\n  :Message: {1}\r\n  :Stack Trace: {2}\r\n", ex.GetType(), ex.Message, ex.StackTrace );

            if( InnerException )
                message = "Inner Exception:\r\n" + message;
            
            if( ex.InnerException != null )
                message += LogException( ex, true );

            return message;
        }
    }
}
