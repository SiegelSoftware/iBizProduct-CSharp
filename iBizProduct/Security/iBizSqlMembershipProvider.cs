using System;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Text;
using System.Web.Security;

namespace iBizProduct.Security
{
    /// <summary>
    /// Provides a base class to work with SQL Membership in a way that allows for connection and settings abstraction.
    /// This allows you to set the provider dynamically at runtime using environmental settings. 
    /// </summary>
    public class iBizSqlMembershipProvider : SqlMembershipProvider
    {
        private readonly NameValueCollection defaultSettings = new NameValueCollection()
        {
            { "ApplicationName", "iBizProduct v3" },
            { "PasswordFormat", "Hashed" },
            { "EnablePasswordRetrieval", "false" },
            { "PasswordStrengthRegularExpression", "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,35}$" },
            { "EnablePasswordReset", "true" },
            { "RequiresQuestionAndAnswer", "false" },
            { "RequiresUniqueEmail", "true" },
            { "MaxInvalidPasswordAttempts", "3" },
            { "PasswordAttemptWindow", "30" },
            { "MinRequiredPasswordLength", "8" },
            { "MinRequiredNonAlphanumericCharacters", "0" }
        };

        /// <summary>
        /// This overrides the SQLMembershipProvider Initialization so that it will work based on your Environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        public override void Initialize( string name, NameValueCollection config )
        {
            config[ "applicationName" ] = GetSetting( "ApplicationName" );
            config[ "connectionString" ] = GetConnectionString();
            config[ "passwordFormat" ] = GetSetting( "PasswordFormat" );
            config[ "enablePasswordRetrieval" ] = GetSetting( "EnablePasswordRetrieval" );
            config[ "passwordStrengthRegularExpression" ] = GetSetting( "PasswordStrengthRegularExpression" );
            config[ "enablePasswordReset" ] = GetSetting( "EnablePasswordReset" );
            config[ "requiresQuestionAndAnswer" ] = GetSetting( "RequiresQuestionAndAnswer" );
            config[ "requiresUniqueEmail" ] = GetSetting( "RequiresUniqueEmail" );
            config[ "maxInvalidPasswordAttempts" ] = GetSetting( "MaxInvalidPasswordAttempts" );
            config[ "passwordAttemptWindow" ] = GetSetting( "PasswordAttemptWindow" );
            config[ "minRequiredPasswordLength" ] = GetSetting( "MinRequiredPasswordLength" );
            config[ "minRequiredNonalphanumericCharacters" ] = GetSetting( "MinRequiredNonAlphanumericCharacters" );

            base.Initialize( name, config );
        }

        private string GetSetting( string name, bool allowNull = false )
        {
            try
            {
                string value = iBizProductSettings.GetSetting( name );

                if( String.IsNullOrEmpty( value ) ) value = defaultSettings[ name ];

                if( String.IsNullOrEmpty( value ) && !allowNull ) throw new NullReferenceException( "There is no setting available for " + name );
                
                return value;
            }
            catch( Exception ex )
            {
                throw new iBizException( "Please refer to the iBizProduct documentation on how to properly configure your environment.", ex );
            }
        }

        private string GetConnectionString()
        {
            try
            {
                // Return the Membership ConnectionString if it exists. If it does not, it will throw an exception.
                return GetSetting( "MembershipConnectionString" );
            }
            catch( Exception )
            {
                // Build a ConnectionString to return based on the Environment. If it does not we want the Exception to bubble up.
                var connectionString = new SqlConnectionStringBuilder();
                connectionString.UserID = GetSetting( "MembershipUserId" );
                connectionString.Password = Encoding.UTF8.GetString( Convert.FromBase64String( GetSetting( "MembershipPassword" ) ) );
                connectionString.InitialCatalog = GetSetting( "MembershipInitialCatalog" );
                connectionString.TrustServerCertificate = true;
                connectionString.DataSource = GetSetting( "MembershipDataSource" );

                return connectionString.ConnectionString;
            }
        }
    }
}
