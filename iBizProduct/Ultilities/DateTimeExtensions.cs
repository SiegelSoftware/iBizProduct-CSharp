// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;

namespace iBizProduct.Ultilities
{
    /// <summary>
    /// Extenstions for the DateTime class to better work with Unix Times
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts from a Unix Timestamp to a DateTime
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="UnixTimeStamp">Unix Timestamp</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertFromUnixTime( this DateTime Obj, long UnixTimeStamp )
        {
            DateTime EPOC = new DateTime( 1970, 1, 1, 0, 0, 0 );

            // Unix timestamp is seconds past epoch
            return EPOC.AddSeconds( UnixTimeStamp ).ToLocalTime();
        }

        /// <summary>
        /// Converts the Time of the Current DateTime object to a UnixTimestamp
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns>Unix Timestamp</returns>
        public static long ConvertToUnixTime( this DateTime Obj )
        {
            DateTime EPOC = new DateTime( 1970, 1, 1, 0, 0, 0 );
            return ( long )( Obj - EPOC ).TotalSeconds;
        }

        /// <summary>
        /// Converts the current DateTime to a Unix Timestamp
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns>Unix Timestamp</returns>
        public static long CurrentTimestamp( this DateTime Obj )
        {
            return ConvertToUnixTime( DateTime.Now );
        }
    }
}
