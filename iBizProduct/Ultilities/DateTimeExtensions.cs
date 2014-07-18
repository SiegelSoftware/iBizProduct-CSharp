using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBizProduct.Ultilities
{
    /// <summary>
    /// Extenstions for the DateTime class to better work with Unix Times
    /// </summary>
    public static class DateTimeExtensions
    {
        public static DateTime ConvertFromUnixTime( this DateTime Obj, long UnixTimeStamp )
        {
            DateTime EPOC = new DateTime( 1970, 1, 1, 0, 0, 0 );

            // Unix timestamp is seconds past epoch
            return EPOC.AddSeconds( UnixTimeStamp ).ToLocalTime();
        }

        public static long ConvertToUnixTime( this DateTime Obj )
        {
            DateTime EPOC = new DateTime( 1970, 1, 1, 0, 0, 0 );
            return ( long )( Obj - EPOC ).TotalSeconds;
        }

        public static long CurrentTimestamp( this DateTime Obj )
        {
            return ConvertToUnixTime( DateTime.Now );
        }
    }
}
