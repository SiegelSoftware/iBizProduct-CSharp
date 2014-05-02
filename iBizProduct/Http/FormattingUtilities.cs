// iBizVision - System.Net.Http.Formatting.dll repack

using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Collections.Generic;
using iBizProduct.Http.Formatting;

namespace iBizProduct.Http
{
    internal static class FormattingUtilities
    {
        private static readonly string[] dateFormats = new string[] { "ddd, d MMM yyyy H:m:s 'GMT'", "ddd, d MMM yyyy H:m:s", "d MMM yyyy H:m:s 'GMT'", "d MMM yyyy H:m:s", "ddd, d MMM yy H:m:s 'GMT'", "ddd, d MMM yy H:m:s", "d MMM yy H:m:s 'GMT'", "d MMM yy H:m:s", "dddd, d'-'MMM'-'yy H:m:s 'GMT'", "dddd, d'-'MMM'-'yy H:m:s", "ddd MMM d H:m:s yyyy", "ddd, d MMM yyyy H:m:s zzz", "ddd, d MMM yyyy H:m:s", "d MMM yyyy H:m:s zzz", "d MMM yyyy H:m:s" };
        public const int DefaultMaxDepth = 0x100;
        public const int DefaultMinDepth = 1;
        public static readonly Type DelegatingEnumerableGenericType = typeof( DelegatingEnumerable<> );
        public static readonly Type EnumerableInterfaceGenericType = typeof( IEnumerable<> );
        public static readonly Type HttpContentType = typeof( HttpContent );
        public const string HttpHostHeader = "Host";
        public const string HttpRequestedWithHeader = "x-requested-with";
        public const string HttpRequestedWithHeaderValue = "XMLHttpRequest";
        public static readonly Type HttpRequestMessageType = typeof( HttpRequestMessage );
        public static readonly Type HttpResponseMessageType = typeof( HttpResponseMessage );
        public const string HttpVersionToken = "HTTP";
        public const double Match = 1.0;
        public const double NoMatch = 0.0;
        private const string NonTokenChars = "()<>@,;:\\\"/[]?={}";
        public static readonly Type QueryableInterfaceGenericType = typeof( IQueryable<> );
        public static readonly System.Runtime.Serialization.XsdDataContractExporter XsdDataContractExporter = new System.Runtime.Serialization.XsdDataContractExporter();

        public static XmlDictionaryReaderQuotas CreateDefaultReaderQuotas()
        {
            return new XmlDictionaryReaderQuotas { MaxArrayLength = 0x7fffffff, MaxBytesPerRead = 0x7fffffff, MaxDepth = 0x100, MaxNameTableCharCount = 0x7fffffff, MaxStringContentLength = 0x7fffffff };
        }

        public static HttpContentHeaders CreateEmptyContentHeaders()
        {
            HttpContent content = null;
            HttpContentHeaders headers = null;
            using( content = new StringContent( string.Empty ) )
            {
                headers = content.Headers;
                headers.Clear();
            }
            return headers;
        }

        public static string DateToString( DateTimeOffset dateTime )
        {
            return dateTime.ToUniversalTime().ToString( "r", CultureInfo.InvariantCulture );
        }

        public static bool IsJTokenType( Type type )
        {
            return typeof( JToken ).IsAssignableFrom( type );
        }

        public static bool TryParseDate( string input, out DateTimeOffset result )
        {
            return DateTimeOffset.TryParseExact( input, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AssumeUniversal | DateTimeStyles.AllowWhiteSpaces, out result );
        }

        public static bool TryParseInt32( string value, out int result )
        {
            return int.TryParse( value, NumberStyles.None, NumberFormatInfo.InvariantInfo, out result );
        }

        public static string UnquoteToken( string token )
        {
            if( !string.IsNullOrWhiteSpace( token ) && ( ( token.StartsWith( "\"", StringComparison.Ordinal ) && token.EndsWith( "\"", StringComparison.Ordinal ) ) && ( token.Length > 1 ) ) )
            {
                return token.Substring( 1, token.Length - 2 );
            }
            return token;
        }

        public static bool ValidateHeaderToken( string token )
        {
            if( token == null )
            {
                return false;
            }
            foreach( char ch in token )
            {
                if( ( ( ch < '!' ) || ( ch > '~' ) ) || ( "()<>@,;:\\\"/[]?={}".IndexOf( ch ) != -1 ) )
                {
                    return false;
                }
            }
            return true;
        }
    }
}
