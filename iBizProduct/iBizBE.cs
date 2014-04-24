﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;


namespace iBizProduct
{
    public class iBizBE
    {

        public static NameValueCollection APICall( string Endpoint, string Action = "VIEW", Dictionary<string, object> Params = null ) 
        {
            if( Params == null ) Params = new Dictionary<string, object>();

            Uri APIUri = GetAPIUri();
            Params.Add( "session_key", HttpContext.Current.Session[ "SessionID" ].ToString() );
            Params.Add( "account_id", HttpContext.Current.Session[ "AccountID" ].ToString() );
            string jsonContent = JsonConvert.SerializeObject( Params );

            //WebRequest webRequest = WebRequest.Create( APIUri );
            //webRequest.Method = "POST";
            //webRequest.ContentType = "application/json";

            HttpWebRequest request = ( HttpWebRequest )WebRequest.Create( APIUri );
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes( jsonContent );

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using( Stream dataStream = request.GetRequestStream() )
            {
                dataStream.Write( byteArray, 0, byteArray.Length );
            }
            long length = 0;
            try
            {
                using( HttpWebResponse response = ( HttpWebResponse )request.GetResponse() )
                {
                    length = response.ContentLength;
                }
            }
            catch( WebException ex )
            {
                // Get Error Message and throw an iBizException
                WebResponse errorResponse = ex.Response;
                using( Stream responseStream = errorResponse.GetResponseStream() )
                {
                    StreamReader reader = new StreamReader( responseStream, Encoding.GetEncoding( "utf-8" ) );
                    String errorText = reader.ReadToEnd();
                    throw new iBizException( errorText, ex.InnerException );
                }
            }

            //Stream reqStream = webRequest.GetRequestStream();
            
            //WebResponse webResponse =  webRequest.GetResponse();

            return null;
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

        private static Uri GetAPIUri()
        {
            if ( Regex.IsMatch( HttpContext.Current.Request.Url.Host, "/.ibizdevelopers.com$" ) || HttpContext.Current.Request.IsLocal )
            {
                // Check to see if the DevAPIHost is defined in appSettings, otherwise use a default host.
                string DevHost = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["DevAPIHost"])) ? ConfigurationManager.AppSettings["DevAPIHost"] : "backendbeta.ibizapi.com";
                int DevPort = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["DevAPIPort"])) ? int.Parse( ConfigurationManager.AppSettings["DevAPIPort"] ) : 8888;
                return new Uri( "https://" + DevHost + ":" + DevPort );
            }

            return new Uri( "https://backend.ibizapi.com:8888" );
        }
    }
}