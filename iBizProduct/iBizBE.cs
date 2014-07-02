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
        private static string[] APIMethods = { "SOAP", "JSON" };
        private static string APIMethod = "JSON";
        private static bool IsDev = false;

        /// <summary>
        /// API Call is a generic function which can be updated to use different API Methods. 
        /// </summary>
        /// <param name="Endpoint">The Endpoint Object to work with</param>
        /// <param name="Action">The Action to perform</param>
        /// <param name="Params">The Parameter list to send to the API</param>
        /// <returns>API Response as a JObject</returns>
        public async static Task<JObject> APICall( string Endpoint, string Action = "VIEW", Dictionary<string, object> Params = null )
        {
            if( Params == null ) Params = new Dictionary<string, object>();
            var RequestEndpoint = EndpointFormatter( Endpoint, Action );

            switch( APIMethod )
            {
                case "JSON":
                    return await JSONCall( RequestEndpoint, Params );
                default:
                    throw new iBizException( "Unknown API Method Type" );
            }
        }

        private static string EndpointFormatter( string Endpoint, string Action )
        {
            APIMethod = Environment.GetEnvironmentVariable( "APIMethod" ) != null ? Environment.GetEnvironmentVariable( "APIMethod" ) : APIMethod;

            switch( APIMethod )
            {
                case "JSON":
                    foreach( var Method in APIMethods )
                    {
                        Endpoint = Regex.Replace( Endpoint, "^" + Method, "" );
                    }
                    return APIMethod + Endpoint + "?action=" + Action;
                case "SOAP":
                    foreach( var Method in APIMethods )
                    {
                        Endpoint = Regex.Replace( Endpoint, "^" + Method, "" );
                    }
                    return APIMethod + Endpoint;
                default:
                    throw new iBizException( "Unknown API Method Type" );
            }

        }

        /// <summary>
        /// This implements the logic required specifically for working with the JSON endpoint
        /// in the iBizAPI. 
        /// </summary>
        /// <param name="RequestEndpoint">The Formatted endpoint</param>
        /// <param name="Params">Paramaters to be sent to the API</param>
        /// <returns>Result as JObject</returns>
        private async static Task<JObject> JSONCall( string RequestEndpoint, Dictionary<string, object> Params )
        {
            string JsonSerializedParams = JsonConvert.SerializeObject( Params );

            try
            {

                using( var handler = new WebRequestHandler() )
                {
                    if( IsDev )
                        handler.ServerCertificateValidationCallback = ( sender, cert, chain, sslPolicyErrors ) => true;

                    using( var client = new APIClient( handler ) )
                    {
                        client.BaseAddress = GetAPIUri();
                        client.DefaultRequestHeaders.Accept.Clear();
                        //client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

                        using( HttpResponseMessage response = await client.PostAsync( RequestEndpoint, new StringContent( JsonSerializedParams, Encoding.UTF8, "application/json" ) ).ConfigureAwait( false ) )
                        using( HttpContent content = response.Content )
                        {
                            // ... Read the string.
                            string result = await content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<JObject>( result );
                        }
                    }
                }
            }
            catch( Exception ex )
            {
                return new JObject() { { "error", ex.Message } };
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
            IsDev = !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "IsDev" ] ) && bool.Parse( ConfigurationManager.AppSettings[ "IsDev" ] ) == true ? true : false;
            string DevUri = "";

            if( IsDev || HttpContext.Current.Request.IsLocal || Regex.IsMatch( HttpContext.Current.Request.Url.Host, "/?:^dev|\\.ibizdevelopers\\.com$" ) )
            {
                IsDev = true;

                // Check to see if the DevAPIHost is defined in Environment/AppSettings, otherwise use a default host.
                string DevAPIHost = "backendbeta.ibizapi.com";
                if( !String.IsNullOrEmpty( Environment.GetEnvironmentVariable( "DevAPIHost" ) ) ) DevAPIHost = Environment.GetEnvironmentVariable( "DevAPIHost" );
                if( !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DevAPIHost" ] ) ) DevAPIHost = ConfigurationManager.AppSettings[ "DevAPIHost" ];

                // Check to see if the DevAPIPort is defined in Environment/AppSettings, otherwise use a default port.
                string DevAPIPort = "8888";
                if( !String.IsNullOrEmpty( Environment.GetEnvironmentVariable( "DevAPIPort" ) ) ) DevAPIHost = Environment.GetEnvironmentVariable( "DevAPIPort" );
                if( !String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DevAPIPort" ] ) ) DevAPIHost = ConfigurationManager.AppSettings[ "DevAPIPort" ];

                string DevProtocol = String.Equals( DevAPIPort, "80" ) ? "http://" : "https://";

                // Return a Dev/Stage URI
                DevUri = String.Equals( DevAPIPort, "80" ) ? ( DevProtocol + DevAPIHost ) : ( DevProtocol + DevAPIHost + ":" + DevAPIPort );
            }

            return new Uri( IsDev ? DevUri : ProductionAPI );
        }

    }
}
