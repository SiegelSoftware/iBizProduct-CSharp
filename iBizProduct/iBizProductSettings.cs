using System;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace iBizProduct
{
    internal class iBizProductSettings
    {
        private const string SettingsFile = "ProductSettings.ibpv3";
        private static NameValueCollection Settings = new NameValueCollection();

        public static string ConfigKey()
        {
            string ConfigKey = Environment.GetEnvironmentVariable( "ConfigKey" );

            if( ConfigKey == null )
            {
                ConfigKey = ConfigurationManager.AppSettings[ "ConfigKey" ];
            }

            if( ConfigKey == null ) throw new iBizException( "An error occured while getting the ConfigKey. Please be sure that the variable exists within the environment." );

            return ConfigKey;
        }

        public static string ConfigVector()
        {
            string ConfigVector = Environment.GetEnvironmentVariable( "ConfigVector" );

            if( ConfigVector == null )
            {
                ConfigVector = ConfigurationManager.AppSettings[ "ConfigVector" ];
            }

            if( ConfigVector == null ) throw new iBizException( "An error occured while getting the ConfigVector. Please be sure that the variable exists within the environment." );

            return ConfigVector;
        }

        public static string ConfigSalt()
        {
            string ConfigSalt = Environment.GetEnvironmentVariable( "ConfigSalt" );

            if( ConfigSalt == null )
            {
                ConfigSalt = ConfigurationManager.AppSettings[ "ConfigSalt" ];
            }

            if( ConfigSalt == null ) throw new iBizException( "An error occured while getting the ConfigSalt. Please be sure that the variable exists within the environment." );

            return ConfigSalt;
        }

        public static int ProductId()
        {
            int ProductId;

            if( !int.TryParse( Environment.GetEnvironmentVariable( "ProductId" ), out ProductId ) )
            {
                if ( !int.TryParse( ConfigurationManager.AppSettings[ "ProductId" ], out ProductId ) )
                {
                    throw new iBizException( "We seem to be having some trouble finding your Product Id. Please make sure that this is available in your Environment or AppSettings." );
                }
            }

            return ProductId;
        }

        public static string ExternalKey()
        {
            string ExternalKey = Environment.GetEnvironmentVariable( "ExternalKey" );

            if( String.IsNullOrEmpty( ExternalKey ) )
                ExternalKey = ConfigurationManager.AppSettings[ "ExternalKey" ];

            return ExternalKey;
        }

        public static void AddSetting( string name, string value )
        {
            Settings.Add( name, value );
        }
    }
}
