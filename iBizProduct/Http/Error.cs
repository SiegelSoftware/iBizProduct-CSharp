// iBizVision - System.Net.Http.Formatting.dll repack

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using iBizProduct.Http.Properties;

namespace iBizProduct.Http
{
    internal static class Error
    {
        private const string HttpScheme = "http";
        private const string HttpsScheme = "https";

        internal static ArgumentException Argument( string messageFormat, params object[] messageArgs )
        {
            return new ArgumentException( Format( messageFormat, messageArgs ) );
        }

        internal static ArgumentException Argument( string parameterName, string messageFormat, params object[] messageArgs )
        {
            return new ArgumentException( Format( messageFormat, messageArgs ), parameterName );
        }

        internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo( string parameterName, object actualValue, object minValue )
        {
            return new ArgumentOutOfRangeException( parameterName, actualValue, Format( CommonWebApiResources.ArgumentMustBeGreaterThanOrEqualTo, new object[] { minValue } ) );
        }

        internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo( string parameterName, object actualValue, object maxValue )
        {
            return new ArgumentOutOfRangeException( parameterName, actualValue, Format( CommonWebApiResources.ArgumentMustBeLessThanOrEqualTo, new object[] { maxValue } ) );
        }

        internal static ArgumentNullException ArgumentNull( string parameterName )
        {
            return new ArgumentNullException( parameterName );
        }

        internal static ArgumentNullException ArgumentNull( string parameterName, string messageFormat, params object[] messageArgs )
        {
            return new ArgumentNullException( parameterName, Format( messageFormat, messageArgs ) );
        }

        internal static ArgumentException ArgumentNullOrEmpty( string parameterName )
        {
            return Argument( parameterName, CommonWebApiResources.ArgumentNullOrEmpty, new object[] { parameterName } );
        }

        internal static ArgumentOutOfRangeException ArgumentOutOfRange( string parameterName, object actualValue, string messageFormat, params object[] messageArgs )
        {
            return new ArgumentOutOfRangeException( parameterName, actualValue, Format( messageFormat, messageArgs ) );
        }

        internal static ArgumentException ArgumentUriHasQueryOrFragment( string parameterName, Uri actualValue )
        {
            return new ArgumentException( Format( CommonWebApiResources.ArgumentUriHasQueryOrFragment, new object[] { actualValue } ), parameterName );
        }

        internal static ArgumentException ArgumentUriNotAbsolute( string parameterName, Uri actualValue )
        {
            return new ArgumentException( Format( CommonWebApiResources.ArgumentInvalidAbsoluteUri, new object[] { actualValue } ), parameterName );
        }

        internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme( string parameterName, Uri actualValue )
        {
            return new ArgumentException( Format( CommonWebApiResources.ArgumentInvalidHttpUriScheme, new object[] { actualValue, "http", "https" } ), parameterName );
        }

        internal static string Format( string format, params object[] args )
        {
            return string.Format( CultureInfo.CurrentCulture, format, args );
        }

        internal static ArgumentException InvalidEnumArgument( string parameterName, int invalidValue, Type enumClass )
        {
            return new InvalidEnumArgumentException( parameterName, invalidValue, enumClass );
        }

        internal static InvalidOperationException InvalidOperation( string messageFormat, params object[] messageArgs )
        {
            return new InvalidOperationException( Format( messageFormat, messageArgs ) );
        }

        internal static InvalidOperationException InvalidOperation( Exception innerException, string messageFormat, params object[] messageArgs )
        {
            return new InvalidOperationException( Format( messageFormat, messageArgs ), innerException );
        }

        internal static KeyNotFoundException KeyNotFound()
        {
            return new KeyNotFoundException();
        }

        internal static KeyNotFoundException KeyNotFound( string messageFormat, params object[] messageArgs )
        {
            return new KeyNotFoundException( Format( messageFormat, messageArgs ) );
        }

        internal static NotSupportedException NotSupported( string messageFormat, params object[] messageArgs )
        {
            return new NotSupportedException( Format( messageFormat, messageArgs ) );
        }

        internal static ObjectDisposedException ObjectDisposed( string messageFormat, params object[] messageArgs )
        {
            return new ObjectDisposedException( null, Format( messageFormat, messageArgs ) );
        }

        internal static OperationCanceledException OperationCanceled()
        {
            return new OperationCanceledException();
        }

        internal static OperationCanceledException OperationCanceled( string messageFormat, params object[] messageArgs )
        {
            return new OperationCanceledException( Format( messageFormat, messageArgs ) );
        }

        internal static ArgumentNullException PropertyNull()
        {
            return new ArgumentNullException( "value" );
        }
    }
}
