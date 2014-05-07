// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using iBizProduct.Http;

namespace iBizProduct
{

    internal class iBizBE
    {
        private const string ProductionAPI = @"https://backend.ibizapi.com:8888";
        private const string StagingAPI = @"https://backendbeta.ibizapi.com:8888";

        public async static Task<Dictionary<string, object>> APICall( string Endpoint, string Action = "VIEW", Dictionary<string, object> Params = null ) 
        {
            if( Params == null ) Params = new Dictionary<string, object>();
            var RequestEndpoint = Endpoint + "?action=" + Action;

            Dictionary<string, object> return_obj = new Dictionary<string, object>();

            string JsonSerializedParams = JsonConvert.SerializeObject( Params );
            Console.WriteLine( JsonSerializedParams );
            Console.WriteLine( "" );

            try
            {
                using( var handler = new WebRequestHandler() )
                {
                    handler.ServerCertificateValidationCallback = ( sender, cert, chain, sslPolicyErrors ) => true;
                    using( var client = new APIClient( handler ) )
                    {
                        client.BaseAddress = GetAPIUri();
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

                        using( HttpResponseMessage response = await client.PostAsync( RequestEndpoint, new StringContent( JsonSerializedParams, Encoding.UTF8, "application/json" ) ) )
                        using( HttpContent content = response.Content )
                        {
                            // ... Read the string.
                            string result = await content.ReadAsStringAsync();
                            return_obj = JsonConvert.DeserializeObject<Dictionary<string, object>>( result );
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine( "An error occured while making the API Call" );
                Console.WriteLine( ex.Message );
                Console.WriteLine( ex.InnerException );
                return_obj.Add( "error", ex.Message );
            }
            
            
            return return_obj;

        }


        // TODO: Implement this to provide actual, you know SECURITY!!!!!!!!!!!
        public static bool AuthenticateUser( string SessionID, int AccountID, int ProductOrderID )
        {
            if ( HttpContext.Current.Session.SessionID == null )
            {
                HttpContext.Current.Session.Add( "SessionID", SessionID );
                HttpContext.Current.Session.Add( "AccountID", AccountID );
                HttpContext.Current.Session.Add( "ProductOrderID", ProductOrderID );
                HttpContext.Current.Session.Add( "Token", Convert.ToBase64String( Guid.NewGuid().ToByteArray() ) );
            }

            return true;
        }

        

        /// <summary>
        /// This will give you the Uri to use for the backend. It is based on both the web.config and Environmental defaults.
        /// </summary>
        /// <remarks>
        /// Requests coming from developer or staging platform will be directed to the API of choice. It will use general Environment
        /// variables first, application settings second, and default to the stage server last. Non iBizDevelopers must use the 
        /// production API. 
        /// </remarks>
        /// <returns>iBizAPI server Uri</returns>
        private static Uri GetAPIUri()
        {
            if( bool.Parse( ConfigurationManager.AppSettings[ "IsDev" ] ) == true || HttpContext.Current.Request.IsLocal || Regex.IsMatch( HttpContext.Current.Request.Url.Host, "/?:^dev|\\.ibizdevelopers\\.com$" ) ) 
            {
                // Check to see if the DevAPIHost is defined in appSettings, otherwise use a default host.
                string DevHost = ( !String.IsNullOrEmpty( Environment.GetEnvironmentVariable( "DevAPIHost" ) ) ) ? Environment.GetEnvironmentVariable( "DevAPIHost" ) : ( !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DevAPIHost" ] ) ) ? ConfigurationManager.AppSettings[ "DevAPIHost" ] : "backendbeta.ibizapi.com";

                string DevPort = ( !String.IsNullOrEmpty( Environment.GetEnvironmentVariable( "DevAPIPort" ) ) ) ? Environment.GetEnvironmentVariable( "DevAPIPort" ) : ( !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DevAPIPort" ] ) ) ? ConfigurationManager.AppSettings[ "DevAPIPort" ] : "8888";

                string DevProtocol = ( String.Equals( DevPort, "80" ) ) ? "http://" : "https://";
                
                return new Uri( ( String.Equals( DevPort, "80" ) ) ? DevProtocol + DevHost : DevProtocol + DevHost + ":" + DevPort );
            }

            return new Uri( ProductionAPI );
        }

    }
}
