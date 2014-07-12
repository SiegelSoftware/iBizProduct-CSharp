// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
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

        /// <summary>
        /// Instantiate an iBizException with the default message
        /// </summary>
        public iBizException() : base( defaultMessage ) { }

        /// <summary>
        /// Instantiate an iBizException with a custom message
        /// </summary>
        /// <param name="message">Message for the Exception</param>
        public iBizException( string message ) : base( message ) { LogError( message ); }

        /// <summary>
        /// Instantiate an iBizException with a custom message and the Inner Exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="inner">Inner Exception</param>
        public iBizException( string message, Exception inner ) : base( message, inner ) { LogError( message ); }

        private void LogError( string message = defaultMessage )
        {
            Console.WriteLine( message );

            StreamWriter w = null;
            try
            {
                w = File.AppendText( logFile );
                w.Write( "\r\nLog Entry : " );
                w.WriteLine( "{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString() );
                w.WriteLine( "  :" );
                w.WriteLine( "  :{0}", "Exception Thrown: " + this.GetType() );
                w.WriteLine( "  :{0}", "Message: " + this.Message );
                w.WriteLine( "  :{0}", "Stack Trace:\r\n" + this.StackTrace );
                LogInnerException( ref w, this.InnerException );
                w.WriteLine( "-------------------------------" );
            }
            finally
            {
                w.Close();
            }
        }

        private static void LogInnerException( ref StreamWriter w, Exception ex )
        {
            if( ex != null )
            {
                w.WriteLine( "\r\n  :{0}", "Inner Exception" );
                w.WriteLine( "  :{0}", "Exception Thrown: " + ex.GetType() );
                w.WriteLine( "  :{0}", "Message: " + ex.Message );
                w.WriteLine( "  :{0}", "Stack Trace:\r\n" + ex.StackTrace );

                LogInnerException( ref w, ex.InnerException );
            }
        }
    }
}
