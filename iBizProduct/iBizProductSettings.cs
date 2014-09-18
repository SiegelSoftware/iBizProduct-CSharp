using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using iBizProduct.Models;

namespace iBizProduct
{
    /// <summary>
    /// Provides a context that allows iBizProduct to work within the context of either a standalone product or
    /// Marketplace Product. 
    /// </summary>
    public sealed class iBizProductSettings : SettingsBase
    {
        private static Product Product { get; set; }
        private static ProductContext productContext { get; set; }

        static iBizProductSettings()
        {
            if( IsMarketplaceApp && productContext == null )
                productContext = new ProductContext();

        }

        /// <summary>
        /// The Product Id from the settings that iBizProduct has to work with. For standalone
        /// products this setting must be located within the standard Environmental Configurations.
        /// For Marketplace Configurations you may set this value so that the correct settings are applied.
        /// </summary>
        public static int ProductId
        {
            get
            {
                if( IsMarketplaceApp )
                {
                    return ProductId;
                }
                else
                {
                    return GetSetting<int>( "ProductId" );
                }
            }

            set
            {
                if( IsMarketplaceApp )
                {
                    ProductId = value;
                    Product = productContext.Products.FirstOrDefault( p => p.ProductId == value );
                }
                else
                    throw new iBizException( "The ProductId may only be set for Marketplace Applications." );
            }
        }

        /// <summary>
        /// The External Key from the Product settings that iBizProuct has to work with. If 
        /// the application is configured as a Marketplace Application it will attempt to 
        /// locate the Product Object from the Database Context and determine what the External
        /// Key to use is based on the currently set ProductId.
        /// </summary>
        public static string ExternalKey
        {
            get
            {
                if( IsMarketplaceApp )
                {
                    if( Product != null )
                    {
                        return Product.ExternalKey;
                    }
                    else
                    {
                        throw new MarketplaceException( "ExternalKey", ProductId );
                    }
                }
                else
                {
                    return GetSetting<string>( "ExternalKey" );
                }
            }
        }

        /// <summary>
        /// Product Event Log Name
        /// </summary>
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
                    return GetSetting<string>( "EventLogName", "iBizProduct" );
                }
            }
        }

        /// <summary>
        /// Event Log Source
        /// </summary>
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
                    return GetSetting<string>( "EventLogSource", "iBizProduct" );
                }
            }
        }

        /// <summary>
        /// Event Log
        /// </summary>
        public static EventLog EventLog
        {
            get
            {
                var log = new EventLog( EventLogName, ".", EventLogSource );

                if( EventLog.SourceExists( EventLogSource ) )
                {
                    // We don't want to catch this here if the application does not have the appropriate 
                    // permissions to create the log. 
                    EventLog.CreateEventSource( new EventSourceCreationData( EventLogSource, EventLogName ) );
                }

                return log;
            }
        }

        /// <summary>
        /// The SMTP Server from the Product settings that iBizProuct has to work with.
        /// </summary>
        public static SmtpClient SmtpClient
        {
            get
            {
                var SmtpServer = GetSetting<string>( "SmtpServer", "localhost" );
                var SmtpPort = GetSetting<int>( "SmtpPort", 25 );

                return new SmtpClient( SmtpServer, SmtpPort );
            }
        }

        /// <summary>
        /// Product's Connection String
        /// </summary>
        public static string ProductConnectionString
        {
            get
            {
                var connString = new SqlConnectionStringBuilder();
                connString.DataSource = GetSetting<string>( "ProductDataSource", "localhost" );
                connString.InitialCatalog = GetSetting<string>( "ProductDatabase", "iBizProduct" );

                if( GetSetting<bool>( "ProductUseWindowsProfile", true ) )
                {
                    connString.IntegratedSecurity = true;
                }
                else
                {
                    connString.UserID = GetSetting<string>( "ProductDbUserId", "iBizProduct" );
                    connString.Password = GetSetting<string>( "ProductDbPassword" );
                }


                return connString.ConnectionString;
            }
        }

        /// <summary>
        /// Is this a Marketplace Application. NOTE: There is currently no support for Marketplace Applications.
        /// </summary>
        public static bool IsMarketplaceApp { get { return Settings[ "Marketplace" ].Value == "true" ? true : false; } }

        internal static string ConfigKey { get { return GetSetting<string>( "ConfigKey" ); } }

        internal static string ConfigVector { get { return GetSetting<string>( "ConfigVector" ); } }

        internal static string ConfigSalt { get { return GetSetting<string>( "ConfigSalt" ); } }

        private static T GetMarketplaceSetting<T>( string SettingName )
        {
            var Setting = productContext.ProductSettings.FirstOrDefault( ps => ps.Key == SettingName );
            if( Setting != null )
                return ( T )Convert.ChangeType( Setting, typeof( T ) );
            else
                throw new NullSettingValueException( SettingName, ProductId );
        }

        private static T GetMarketplaceSetting<T>( string SettingName, T DefaultValue )
        {
            try
            {
                return GetMarketplaceSetting<T>( SettingName );
            }
            catch
            {
                return DefaultValue;
            }
        }
    }
}
