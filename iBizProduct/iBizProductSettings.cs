using System;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using iBizProduct.Ultilities;

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
                    int ProductId;

                    if( !int.TryParse( Settings[ "ProductId" ], out ProductId ) )
                    {
                        if( !int.TryParse( Environment.GetEnvironmentVariable( "ProductId" ), out ProductId ) )
                        {
                            if( !int.TryParse( ConfigurationManager.AppSettings[ "ProductId" ], out ProductId ) )
                            {
                                throw new iBizException( "We seem to be having some trouble finding your Product Id. Please make sure that this is available in your Environment or AppSettings." );
                            }
                        }
                    }

                    return ProductId;
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
                    string ExternalKey = Settings[ "ExternalKey" ];

                    if( String.IsNullOrEmpty( ExternalKey ) )
                        ExternalKey = Environment.GetEnvironmentVariable( "ExternalKey" );

                    if( String.IsNullOrEmpty( ExternalKey ) )
                        ExternalKey = ConfigurationManager.AppSettings[ "ExternalKey" ];

                    return ExternalKey;
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
    }
}
