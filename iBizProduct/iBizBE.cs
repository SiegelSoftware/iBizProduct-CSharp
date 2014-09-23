// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using iBizProduct.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iBizProduct
{

    internal class iBizBE
    {
        private const string ProductionAPI = @"https://backend.ibizapi.com:8888";
        private const string StagingAPI = @"https://backendbeta.ibizapi.com:8888";
        private static string[] APIMethods = { "SOAP", "JSON", @"iBiz\/API" };
        private static string APIMethod = "";
        private static string DefaultAPIMethod = "JSON";
        private static bool IsDev = false;

        /// <summary>
        /// API Call is a generic function which can be updated to use different API Methods. 
        /// </summary>
        /// <param name="Endpoint">The Endpoint Object to work with</param>
        /// <param name="Action">The Action to perform</param>
        /// <param name="Params">The Parameter list to send to the API</param>
        /// <returns>API Response as a JObject</returns>
        public async static Task<JObject> APICall( string Endpoint, string Action = "VIEW", Dictionary<string, object> Params = null, string APIMethodToUse = "" )
        {
            APIMethod = String.IsNullOrEmpty( APIMethodToUse ) ? DefaultAPIMethod : APIMethodToUse;

            if( Params == null )
                Params = new Dictionary<string, object>();
            var RequestEndpoint = EndpointFormatter( Endpoint, Action );

            switch( APIMethod )
            {
                case "JSON":
                    return await JSONCall( RequestEndpoint, Params ).ConfigureAwait( false );
                case "REST":
                    throw new iBizException( "This is yet to be implemented.", new NotImplementedException( "The REST API is still in development and is not yet in use." ) );
                default:
                    throw new iBizException( "Unknown API Method Type" );
            }
        }

        private static string EndpointFormatter( string Endpoint, string Action )
        {
            if( String.IsNullOrEmpty( APIMethod ) )
                APIMethod = Environment.GetEnvironmentVariable( "APIMethod" ) != null ? Environment.GetEnvironmentVariable( "APIMethod" ) : DefaultAPIMethod;

            switch( APIMethod )
            {
                case "JSON":
                    foreach( var Method in APIMethods )
                    {
                        Endpoint = Regex.Replace( Endpoint, "^" + Method, "" );
                    }

                    if( !Regex.IsMatch( Endpoint, @"^\/" ) )
                        Endpoint = "/" + Endpoint;

                    return APIMethod + Endpoint + "?action=" + Action;
                case "SOAP":
                    foreach( var Method in APIMethods )
                    {
                        Endpoint = Regex.Replace( Endpoint, "^" + Method, "" );
                    }

                    if( !Regex.IsMatch( Endpoint, @"^\/" ) )
                        Endpoint = "/" + Endpoint;

                    return APIMethod + Endpoint;
                case "REST":
                    throw new iBizException( "This is yet to be implemented.", new NotImplementedException( "The REST API is still in development and is not yet in use." ) );
                default:
                    throw new iBizException( "Unknown API Method Type" );
            }

        }

        /// <summary>
        /// This implements the logic required specifically for working with the JSON endpoint
        /// in the iBizAPI. 
        /// </summary>
        /// <param name="RequestEndpoint">The Formatted endpoint</param>
        /// <param name="Params">Parameters to be sent to the API</param>
        /// <returns>Result as JObject</returns>
        private async static Task<JObject> JSONCall( string RequestEndpoint, Dictionary<string, object> Params )
        {
            string JsonSerializedParams = JsonConvert.SerializeObject( Params, Formatting.Indented );

            using( var client = new APIClient() )
            {
                client.BaseAddress = GetAPIUri();
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

                using( HttpResponseMessage response = await client.PostAsync( RequestEndpoint, new StringContent( JsonSerializedParams, Encoding.UTF8, "application/json" ) ).ConfigureAwait( false ) )
                using( HttpContent content = response.Content )
                {
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync().ConfigureAwait( false );
                    // Convert to JObject
                    var jsonResult = JsonConvert.DeserializeObject<JObject>( result );

                    // Return an iBizBackendException if we received an error from the backend.
                    if( jsonResult[ "error" ] != null )
                        throw new iBizBackendException( jsonResult[ "error" ].ToString(), RequestEndpoint, Params );
                    else
                        return jsonResult;
                }
            }
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
            // Verify if Dev is explicitly defined in Environment/AppSettings
            IsDev = !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "IsDev" ] ) && bool.Parse( ConfigurationManager.AppSettings[ "IsDev" ] ) == true ?
                true : ( !String.IsNullOrEmpty( Environment.GetEnvironmentVariable( "IsDev" ) ) ) ? bool.Parse( Environment.GetEnvironmentVariable( "IsDev" ) ) : false;
            string DevUri = "";
            bool IsLocal = false;
            string RequestHost = "";

            // We really need to be able to have a way to grab this in child threads and handle this where HttpContext does not exist.
            try
            {
                IsLocal = HttpContext.Current.Request.IsLocal;
                RequestHost = HttpContext.Current.Request.Url.Host;
            }
            catch( Exception ) { }

            try
            {
                if( IsDev || IsLocal || ( RequestHost != "" && Regex.IsMatch( RequestHost, @"/?:^dev|\.ibizdevelopers\.com$" ) ) )
                {
                    // Check to see if the DevAPIHost is defined in Environment/AppSettings, otherwise use a default host.
                    string DevAPIHost = GetDevApiHost();

                    // Check to see if the DevAPIPort is defined in Environment/AppSettings, otherwise use a default port.
                    string DevAPIPort = GetDevApiPort();

                    string DevProtocol = GetDevApiProtocol();

                    // Return a Dev/Stage URI
                    if( String.IsNullOrEmpty( DevAPIHost ) || String.IsNullOrEmpty( DevProtocol ) )
                        return new Uri( StagingAPI );

                    DevUri = Regex.IsMatch( DevProtocol, "^http" ) ? ( DevProtocol + DevAPIHost ) : ( DevProtocol + DevAPIHost + ":" + DevAPIPort );

                    return new Uri( DevUri );
                }
            }
            catch( Exception ex )
            {
                throw new iBizException( "An exception occurred while determining the correct backend API to use. Please refer to the environmental documentation and confirm your environment is correctly configured.", ex );
            }

            // Return the default Production URI
            return new Uri( ProductionAPI );

        }

        public static string GetDevApiHost()
        {
            string DevAPIHost = "";
            if( !String.IsNullOrEmpty( Environment.GetEnvironmentVariable( "DevAPIHost" ) ) )
                DevAPIHost = Environment.GetEnvironmentVariable( "DevAPIHost" );
            if( !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DevAPIHost" ] ) )
                DevAPIHost = ConfigurationManager.AppSettings[ "DevAPIHost" ];

            return DevAPIHost;
        }

        public static string GetDevApiPort()
        {
            string DevAPIPort = "";
            if( !String.IsNullOrEmpty( Environment.GetEnvironmentVariable( "DevAPIPort" ) ) )
                DevAPIPort = Environment.GetEnvironmentVariable( "DevAPIPort" );
            if( !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DevAPIPort" ] ) )
                DevAPIPort = ConfigurationManager.AppSettings[ "DevAPIPort" ];

            return DevAPIPort;
        }

        public static string GetDevApiProtocol()
        {
            string DevApiProtocol = "";
            if( !String.IsNullOrEmpty( Environment.GetEnvironmentVariable( "DevApiProtocol" ) ) )
                DevApiProtocol = Environment.GetEnvironmentVariable( "DevApiProtocol" );
            if( !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DevApiProtocol" ] ) )
                DevApiProtocol = ConfigurationManager.AppSettings[ "DevApiProtocol" ];

            if( !String.IsNullOrEmpty( DevApiProtocol ) && Regex.IsMatch( DevApiProtocol, "^http?" ) )
                throw new iBizException( "Invalid Protocol format. ApiProtocol must be either http:// or https://" );

            return DevApiProtocol;
        }
    }
}
