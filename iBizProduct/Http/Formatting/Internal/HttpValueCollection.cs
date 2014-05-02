// iBizVision - System.Net.Http.Formatting.dll repack

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Text;
using iBizProduct.Http.Properties;

namespace iBizProduct.Http.Formatting.Internal
{
    [Serializable]
    internal class HttpValueCollection : NameValueCollection
    {
        private HttpValueCollection()
            : base( StringComparer.OrdinalIgnoreCase )
        {
        }

        protected HttpValueCollection( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }

        public override void Add( string name, string value )
        {
            ThrowIfMaxHttpCollectionKeysExceeded( this.Count );
            name = name ?? string.Empty;
            value = value ?? string.Empty;
            base.Add( name, value );
        }

        private static bool AppendNameValuePair( StringBuilder builder, bool first, bool urlEncode, string name, string value )
        {
            string str = name ?? string.Empty;
            string str2 = urlEncode ? UriQueryUtility.UrlEncode( str ) : str;
            string str3 = value ?? string.Empty;
            string str4 = urlEncode ? UriQueryUtility.UrlEncode( str3 ) : str3;
            if( first )
            {
                first = false;
            }
            else
            {
                builder.Append( "&" );
            }
            builder.Append( str2 );
            if( !string.IsNullOrEmpty( str4 ) )
            {
                builder.Append( "=" );
                builder.Append( str4 );
            }
            return first;
        }

        internal static HttpValueCollection Create()
        {
            return new HttpValueCollection();
        }

        internal static HttpValueCollection Create( IEnumerable<KeyValuePair<string, string>> pairs )
        {
            HttpValueCollection values = new HttpValueCollection();
            foreach( KeyValuePair<string, string> pair in pairs )
            {
                values.Add( pair.Key, pair.Value );
            }
            values.IsReadOnly = false;
            return values;
        }

        private static void ThrowIfMaxHttpCollectionKeysExceeded( int count )
        {
            if( count >= MediaTypeFormatter.MaxHttpCollectionKeys )
            {
                throw Error.InvalidOperation( Resources.MaxHttpCollectionKeyLimitReached, new object[] { MediaTypeFormatter.MaxHttpCollectionKeys, typeof( MediaTypeFormatter ) } );
            }
        }

        public override string ToString()
        {
            return this.ToString( true );
        }

        private string ToString( bool urlEncode )
        {
            if( this.Count == 0 )
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            bool first = true;
            foreach( string str in this )
            {
                string[] values = this.GetValues( str );
                if( ( values == null ) || ( values.Length == 0 ) )
                {
                    first = AppendNameValuePair( builder, first, urlEncode, str, string.Empty );
                }
                else
                {
                    foreach( string str2 in values )
                    {
                        first = AppendNameValuePair( builder, first, urlEncode, str, str2 );
                    }
                }
            }
            return builder.ToString();
        }
    }
}
