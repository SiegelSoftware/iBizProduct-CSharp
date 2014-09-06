// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;

namespace iBizProduct.Ultilities
{
    /// <summary>
    /// Provides a wrapper class for the EventLog. This allows for quick and easy 
    /// </summary>
    public class EventLogger
    {
        static EventLogger Logger;
        public EventLog Log;
        private string[] Email;
        private SmtpClient SmtpClient = iBizProductSettings.SmtpClient;

        /// <summary>
        /// This class will abstract the use of the EventLog class. It will ensure the proper placement of 
        /// Logs so that they are grouped into the folder iBizProduct v3, with a Subfolder for each Log Source.
        /// </summary>
        /// <param name="LogSource">Product Name</param>
        /// <param name="LogName">Type of Log. Default is Operational</param>
        public EventLogger( string LogSource = "MyProduct", string LogName = "Operational" ) : this( "", LogSource, LogName ) { }

        /// <summary>
        /// This class will abstract the use of the EventLog class. It will ensure the proper placement of 
        /// Logs so that they are grouped into the folder iBizProduct v3, with a Subfolder for each Log Source.
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="LogSource"></param>
        /// <param name="LogName"></param>
        public EventLogger( string Email, string LogSource, string LogName = "Operational" )
            : this( new string[] { Email }, LogSource, LogName ) { }

        public EventLogger( string[] EmailList, string LogSource, string LogName = "Operational" )
        {
            this.Email = Email;

            // Check to see if the iBizProduct v3 folder exists... create it if it does not exist

            // Check to see if the LogSource folder exists... create it if it does not exist

        }

        /// <summary>
        /// Writes an entry to the Event Log 
        /// </summary>
        /// <param name="Message">Message to write to the Event Log</param>
        /// <param name="EntryType">EventLog Entry Type: Default Information</param>
        /// <param name="EventId">If unspecified it will update to the most recent Event Id</param>
        public void WriteEntry( string Message, EventLogEntryType EntryType = EventLogEntryType.Information, int EventId = -999 )
        {
            // TODO: Create default Category
            if( EventId == -999 )
                EventId = Log.Entries.Count + 1;

            Log.WriteEntry( Message, EntryType, EventId );
        }

        public void WriteEntry( string Message, EventLogEntryType EntryType, int EventId, short Category )
        {
            Log.WriteEntry( Message, EntryType, EventId, Category );
        }

        //public static void WriteEntry( string Message, string LogSource = "MyProduct", string LogName = "Operational" )
        //{
        //    Logger = new EventLogger( LogSource, LogName );
        //    Logger.WriteEntry( Message );
        //}

        //public static void WriteEntry( string Message, EventLogEntryType EntryType )
        //{
        //    Logger.Log.WriteEntry( Message, EntryType );
        //}

        //public static void WriteEntry( string Message, EventLogEntryType EntryType, int EventId )
        //{
        //    // TODO: Create default Category
        //    Logger.Log.WriteEntry( Message, EntryType, EventId );
        //}

        //public static void WriteEntry( string Message, EventLogEntryType EntryType, int EventId, short Category )
        //{
        //    Logger.Log.WriteEntry( Message, EntryType, EventId, Category );
        //}

        public static bool SetupLogs( string ProductName, string LogName )
        {
            return false;
        }

        /// <summary>
        /// Configures Logs in their appropriate configuration.
        /// It will consume an EventLogCollection or any Dictionary<string,string>, 
        /// where the Key == ApplicationName and Value == LogName
        /// </summary>
        /// <param name="Logs">EventLogCollection</param>
        /// <returns></returns>
        public static bool SetupLogs( IDictionary<string,string> Logs )
        {
            foreach( var Log in Logs )
            {
                if( !SetupLogs( Log.Key, Log.Value ) )
                    return false;
            }
            return true;
        }

        /// <summary>
        /// This will email any contacts you have specified with the event in question.
        /// </summary>
        /// <param name="Message">Message to Send</param>
        /// <param name="EntryType">Entry/Alert Type</param>
        /// <param name="EventId">Event Id</param>
        public void SendEmail( string Message, EventLogEntryType EntryType, int EventId )
        {
            if( this.Email != null )
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress( WindowsIdentity.GetCurrent().Name + "@" + Dns.GetHostName() + "." + System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName );
                foreach( var email in this.Email )
                {
                    if( !String.IsNullOrEmpty( email ) )
                        mail.To.Add( email );
                }

                mail.Subject = this.Log.Source + ": " + EntryType + " - EventId: " + EventId ;
                mail.Body = Message;
                mail.IsBodyHtml = true;

                SmtpClient.Send( mail );
            }
        }
    }
}
