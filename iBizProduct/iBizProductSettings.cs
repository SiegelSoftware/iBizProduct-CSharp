using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using iBizProduct.Ultilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iBizProduct
{
    /// <summary>
    /// Provides a context that allows iBizProduct to work within the context of either a standalone product or
    /// Marketplace Product. 
    /// </summary>
    public sealed class iBizProductSettings
    {

        private static string SettingsFile = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) + "ProductSettings.ibpv3";

        /// <summary>
        /// This contains the settings that iBizProduct has to work with. 
        /// </summary>
        public static NameValueCollection Settings = new NameValueCollection();

        /// <summary>
        /// The Product Id from the settings that iBizProduct has to work with.
        /// </summary>
        public static int ProductId
        {
            get
            {
                if( IsMarketplaceApp )
                {
                    throw new NotImplementedException( "Marketplace Support has not yet been fully implemented" );
                }
                else
                {
                    return GetSetting<int>( "ProductId" );
                }
            }
        }

        /// <summary>
        /// The External Key from the Product settings that iBizProuct has to work with.
        /// </summary>
        public static string ExternalKey
        {
            get
            {
                if( IsMarketplaceApp )
                {
                    throw new NotImplementedException( "Marketplace Support has not yet been fully implemented." );
                }
                else
                {
                    var ExternalKey = GetSetting<string>( "ExternalKey" );

                    return ExternalKey;
                }
            }
        }

        public static string EventLogName
        {
            get
            {
                if( IsMarketplaceApp )
                {
                    throw new NotImplementedException( "Marketplace Support has not yet been fully implemented." );
                }
                else
                {
                    var EventLogName = GetSetting<string>( "EventLogName", "iBizProduct" );

                    return EventLogName;
                }
            }
        }

        public static string EventLogSource
        {
            get
            {
                if( IsMarketplaceApp )
                {
                    throw new NotImplementedException( "Marketplace Support has not yet been fully implemented." );
                }
                else
                {
                    var EventLogSource = GetSetting<string>( "EventLogSource", "iBizProduct" );

                    return EventLogSource;
                }
            }
        }

        public static EventLog EventLog
        {
            get
            {
                var log = new EventLog();
                log.Source = "iBizProduct v3";
                log.Log = EventLogSource;


                if( EventLog.SourceExists( log.Source ) )
                {
                    // We don't want to catch this here if the application does not have the appropriate 
                    // permissions to create the log. 
                    EventLog.CreateEventSource( log.Source, EventLog.Log, "." );
                }

                return log;
            }
        }

        /// <summary>
        /// The Smtp Server from the Product settings that iBizProuct has to work with.
        /// </summary>
        public static SmtpClient SmtpClient
        {
            get
            {
                if( IsMarketplaceApp )
                {
                    throw new NotImplementedException( "Marketplace Support has not yet been fully implemented." );
                }
                else
                {
                    var SmtpServer = GetSetting<string>( "SmtpServer", "localhost" );
                    var SmtpPort = GetSetting<int>( "SmtpPort", 25 );

                    var SmtpClient = new SmtpClient( SmtpServer, SmtpPort );

                    return SmtpClient;
                }
            }
        }

        public static bool IsMarketplaceApp
        {
            get
            {
                return Settings[ "Marketplace" ] == "true" ? true : false;
            }
        }

        static iBizProductSettings()
        {
            ReadSettings();
        }

        internal static string ConfigKey()
        {
            string ConfigKey = Environment.GetEnvironmentVariable( "ConfigKey" );

            if( ConfigKey == null )
            {
                ConfigKey = ConfigurationManager.AppSettings[ "ConfigKey" ];
            }

            if( ConfigKey == null ) throw new iBizException( "An error occured while getting the ConfigKey. Please be sure that the variable exists within the environment." );

            return ConfigKey;
        }

        internal static string ConfigVector()
        {
            string ConfigVector = Environment.GetEnvironmentVariable( "ConfigVector" );

            if( ConfigVector == null )
            {
                ConfigVector = ConfigurationManager.AppSettings[ "ConfigVector" ];
            }

            if( ConfigVector == null ) throw new iBizException( "An error occured while getting the ConfigVector. Please be sure that the variable exists within the environment." );

            return ConfigVector;
        }

        internal static string ConfigSalt()
        {
            string ConfigSalt = Environment.GetEnvironmentVariable( "ConfigSalt" );

            if( ConfigSalt == null )
            {
                ConfigSalt = ConfigurationManager.AppSettings[ "ConfigSalt" ];
            }

            if( ConfigSalt == null ) throw new iBizException( "An error occured while getting the ConfigSalt. Please be sure that the variable exists within the environment." );

            return ConfigSalt;
        }

        public static void AddSetting( string key, string value )
        {
            if( Settings == null || Settings.Count == 0 )
                ReadSettings();

            Settings.Add( key, value );
            WriteSettings();
        }

        public static string GetSetting( string key )
        {
            if( Settings == null || Settings.Count == 0 )
                ReadSettings();

            return Settings[ key ];
        }

        internal static void ReadSettings()
        {
            try
            {
                // We only want to read in the file assuming that it actually exists.
                if( File.Exists( SettingsFile ) )
                {
                    using( StreamReader r = new StreamReader( SettingsFile ) )
                    {
                        string json = r.ReadToEnd();
                        Settings = JsonConvert.DeserializeObject<NameValueCollection>( json );
                    }
                }
            }
            catch( Exception ex )
            {
                // This is most likely due to a parsing error.
                iBizLog.WriteException( ex );
            }
        }

        internal static void WriteSettings()
        {
            try
            {
                var json = new JObject( Settings );

                File.WriteAllText( SettingsFile, json.ToString() );
            }
            catch( Exception ex )
            {
                // This is most likely due to the application running without proper file permissions
                iBizLog.WriteException( ex );
            }
        }

        private static T GetSetting<T>( string SettingName )
        {
            string settingValue = Settings[ SettingName ];

            if( String.IsNullOrEmpty( settingValue ) )
                settingValue = Environment.GetEnvironmentVariable( SettingName );
            
            if( String.IsNullOrEmpty( settingValue ) )
                settingValue = ConfigurationManager.AppSettings[ SettingName ];

            if( String.IsNullOrEmpty( settingValue ) )
            {
                var NullException = new NullReferenceException( string.Format( "There is currently no definition that iBizProduct can use for: {0}.", SettingName ) );
                throw new iBizException( string.Format( "{0} does not exist please check your environmental settings.", SettingName ), NullException );
            }

            return ( T )Convert.ChangeType( settingValue, typeof( T ) );
        }

        private static T GetSetting<T>( string SettingName, T Default )
        {
            try
            {
                return GetSetting<T>( SettingName );
            }
            catch
            {
                return Default;
            }
        }
    }
}
