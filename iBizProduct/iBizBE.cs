using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace iBizProduct
{
    public class iBizBE
    {

        public async static Task<Dictionary<string, object>> APICall( string Endpoint, string Action = "VIEW", Dictionary<string, object> Params = null ) 
        {
            if( Params == null ) Params = new Dictionary<string, object>();
            var RequestEndpoint = Endpoint + "?action=" + Action;

            Dictionary<string, object> return_obj = new Dictionary<string, object>();

            try
            {
                using( var client = new APIClient() )
                {
                    client.BaseAddress = GetAPIUri();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

                    // HTTP POST
                    HttpResponseMessage response = await client.PostAsJsonAsync( RequestEndpoint, Params );
                    //if( response.IsSuccessStatusCode )
                    {
                        var message = await response.Content.ReadAsStringAsync();
                        return_obj = JsonConvert.DeserializeObject<Dictionary<string, object>>( message );
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine( ex.Message );
                Console.WriteLine( ex.InnerException );
                return_obj.Add( "Error", ex.Message );
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
        /// This will give you the Uri to use for the backend. It is based on both the web.config and general defaults.
        /// </summary>
        /// <returns></returns>
        private static Uri GetAPIUri()
        {
            if( Regex.IsMatch( HttpContext.Current.Request.Url.Host, "/?:^dev|\\.ibizdevelopers\\.com$" ) || HttpContext.Current.Request.IsLocal )
            {
                // Check to see if the DevAPIHost is defined in appSettings, otherwise use a default host.
                string DevHost = ( String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DevAPIHost" ] ) ) ? ConfigurationManager.AppSettings[ "DevAPIHost" ] : "backendbeta.ibizapi.com";
                string DevPort = "8888";// ( String.IsNullOrEmpty( ConfigurationManager.AppSettings[ "DevAPIPort" ] ) ) ? ConfigurationManager.AppSettings[ "DevAPIPort" ] : "8888";
                string DevProtocol = ( String.Equals( DevPort, "80" ) ) ? "http://" : "https://";
                
                return new Uri( ( String.Equals( DevPort, "80" ) ) ? DevProtocol + DevHost : DevProtocol + DevHost + ":" + DevPort );
            }

            return new Uri( "https://backend.ibizapi.com:8888" );
        }

    }
}
