// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;

namespace iBizProduct.Ultilities
{
    /// <summary>
    /// Provides a wrapper class for the EventLog. This allows for quick and easy event logging.
    /// Notes on EventLog handling, windows Event Viewer does not actually read a folder structure
    /// where Custom EventLogs have been created but rather it renders a list of EventLogs, where
    /// their display name can become a folder like structure.
    /// a EventLog whose display name is: iBizProduct-MyProduct-MyCustomLog
    /// Would be rendered as:
    /// -iBizProduct
    ///     -MyProduct
    ///         MyCustomLog
    /// </summary>
    public class EventLogger
    {
        private const string RootDisplayLogName = "iBizProduct";
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
            this.Email = EmailList;

            // Check to see if the iBizProduct v3 folder exists... create it if it does not exist
            if ( !Directory.Exists( @"c:\iBizProductv3" ) )
            {
                Directory.CreateDirectory( @"c:\iBizProductv3" );
            }
            // Check to see if the LogSource folder exists... create it if it does not exist
            // Update:↑ LogSource folder doesn't exist is just the way EventViewer renders the display name of the EventLog.
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
            if (!ProductName.Equals(RootDisplayLogName))// If it is not the library's default log
            {
                if ( EventLog.SourceExists( RootDisplayLogName + "-" + ProductName + "-" + LogName ) )
                    return false;// Nope, can't set up it exists already.
                else
                {
                    EventLog.CreateEventSource( ProductName, RootDisplayLogName + "-" + ProductName + "-" + LogName );
                    ModifyRegistryStructure( RootDisplayLogName + "-" + ProductName + "-" + LogName );
                }                
            }
            else // Set up the default log for the library
            {
                if ( !EventLog.SourceExists( ProductName ) )
                    EventLog.CreateEventSource( ProductName, RootDisplayLogName + "-" + LogName );
                ModifyRegistryStructure( RootDisplayLogName + "-" + LogName );
            }
            return true;
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
                else
                {
                    //Log was created successfuly
                }
            }
            return true;
        }

        private static void ModifyRegistryStructure( string LogName )
        {
            var PublisherGuid = CreateNewPublisher( LogName );
            LinkChannels( LogName, PublisherGuid );
        }

        private static void LinkChannels( string LogName, string PublisherGuid )
        {
            RegistryKey LocalMachineRegistry = Registry.LocalMachine;
            var Channels = LocalMachineRegistry.OpenSubKey( "Software/Microsoft/Windows/CurrentVersion/WINEVT/Channels", true );
            Channels.CreateSubKey( LogName + "/Operational" );
            var Channel = Channels.OpenSubKey( LogName + "/Operational", true );

            // Modify isolation level
            Channel.SetValue( "Isolation", 0, RegistryValueKind.DWord );
            // Enabled 1
            Channel.SetValue( "Enabled", 1, RegistryValueKind.DWord );
            // OwningPublisher
            Channel.SetValue( "OwningPublisher", PublisherGuid, RegistryValueKind.ExpandString );
            // Type 
            // 0 Admin
            // 1 Operational
            // 2 Analytic
            // 3 Debug
            Channel.SetValue( "Type", 0, RegistryValueKind.DWord );
        }

        private static RegistryKey OpenRegistry( String path, RegistryKey? root )
        {
            string NextSubKey = path.Substring( 0, path.IndexOf( "/" ) );
            RegistryKey NextKey = null;

            /**
             * Needs fixing... the idea is to consume all of the path string or open up all of the available keys down to the one we need.
             * **/
            if (path.Length > 1)
            {
                if ( root.HasValue )
                {
                    NextKey = root.Value.OpenSubKey( NextSubKey, true );
                    NextKey = OpenRegistry( path.Substring( path.IndexOf( "/" ), path.Length - path.IndexOf( "/" ) ), NextKey );//trim right side of /
                }
                else
                {
                    RegistryKey RootKey = Registry.LocalMachine;
                    NextKey = RegistryKey.OpenBaseKey( RegistryHive.LocalMachine, RegistryView.Registry64 ).OpenSubKey( NextSubKey );
                    NextKey = OpenRegistry( path.Substring( path.IndexOf( "/" ), path.Length - path.IndexOf( "/" ) ), NextKey );
                }
            }
            

            return NextKey;
        }

        private static string CreateNewPublisher( string LogName )
        {
            RegistryKey LocalMachineRegistry = Registry.LocalMachine;
            //var PublishersKey = RegistryKey.OpenBaseKey( RegistryHive.LocalMachine, RegistryView.Registry64 ).OpenSubKey( "SOFTWARE" ); // as funny as it is, it won't allow to open the key with the whole path
            //PublishersKey = PublishersKey.OpenSubKey( "Microsoft" );
            //PublishersKey = PublishersKey.OpenSubKey( "Windows" );
            //PublishersKey = PublishersKey.OpenSubKey( "CurrentVersion" );
            //PublishersKey = PublishersKey.OpenSubKey( "WINEVT" );
            //PublishersKey = PublishersKey.OpenSubKey( "Publishers", true );
            var PublishersKey = OpenRegistry("SOFTWARE/Microsoft/CurrentVersion/WINEVT/Publishers");
            /*
             * RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE")
             */

            // Add new publisher
            string PublisherGuid = "{" + Guid.NewGuid().ToString() + "}";
            PublishersKey.CreateSubKey( PublisherGuid );
            // Add ChannelReferences subkey and MessageFileName
            var NewPublisher = PublishersKey.OpenSubKey( PublisherGuid, true );//Open the new publisher
            NewPublisher.SetValue( "MessageFileName", GetFrameworkPath() + @"\EventLogMessages.dll", RegistryValueKind.ExpandString );
            NewPublisher.SetValue( "ResourceFileName", GetFrameworkPath() + @"\EventLogMessages.dll", RegistryValueKind.ExpandString );
            NewPublisher.CreateSubKey( "ChannelReferences" );
            // Add Count
            var ChannelReferences = NewPublisher.OpenSubKey( "ChannelReferences", true );
            ChannelReferences.SetValue( "Count", 1, RegistryValueKind.DWord );
            ChannelReferences.CreateSubKey( "0" );
            // Add single Channel reference
            var SingleChannelReference = ChannelReferences.OpenSubKey( "0", true );
            SingleChannelReference.SetValue( "", LogName + "/Operational" );//modify default value

            return PublisherGuid;
        }

        private static string GetFrameworkPath()
        {
            // This is the location of the .Net Framework Registry Key
            string framworkRegPath = @"Software\Microsoft\.NetFramework";

            // Get a non-writable key from the registry
            RegistryKey netFramework = Registry.LocalMachine.OpenSubKey( framworkRegPath, false );

            // Retrieve the install root path for the framework
            string installRoot = netFramework.GetValue( "InstallRoot" ).ToString();

            // Retrieve the version of the framework executing this program
            string version = string.Format( @"v{0}.{1}.{2}\",
              Environment.Version.Major,
              Environment.Version.Minor,
              Environment.Version.Build );

            // Return the path of the framework
            return System.IO.Path.Combine( installRoot, version );
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

        public void CreatePublisherKey(string[] Channels)
        {
            
        }
    }
}
