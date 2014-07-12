using System;

namespace iBizProduct
{
    /// <summary>
    /// Use Unix Timestamps
    /// </summary>
    public class UnixTime
    {
        static DateTime EPOC = new DateTime( 1970, 1, 1, 0, 0, 0 );
        private DateTime ThisTime;

        /// <summary>
        /// Instantiate a new UnixTime object based on the current time.
        /// </summary>
        public UnixTime()
        {
            ThisTime = DateTime.Now;
        }

        /// <summary>
        /// Instantiate a new UnixTime object
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        public UnixTime( int year, int month, int day )
        {
            ThisTime = new DateTime( year, month, day );
        }

        /// <summary>
        /// Instantiate a new UnixTime object
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        /// <param name="hour">Hour</param>
        /// <param name="minute">Minute</param>
        /// <param name="second">Second</param>
        public UnixTime( int year, int month, int day, int hour, int minute, int second )
        {
            ThisTime = new DateTime( year, month, day, hour, minute, second );
        }

        /// <summary>
        /// Instantiate a new UnixTime object
        /// </summary>
        /// <param name="datetime">DateTime object to base UnixTimestamp off of.</param>
        public UnixTime( DateTime datetime )
        {
            ThisTime = datetime;
        }

        /// <summary>
        /// Returns Unix Timestamp from current object.
        /// </summary>
        /// <returns>Unix Timestamp</returns>
        public long GetUnixTime()
        {
            var timeSpan = ( ThisTime - EPOC );
            return ( long )timeSpan.TotalSeconds;
        }

        /// <summary>
        /// Convert from a DateTime object to a Unit Timestamp
        /// </summary>
        /// <param name="datetime">DateTime object</param>
        /// <returns>Unix Timestamp</returns>
        public static long ConvertToUnixTime( DateTime datetime )
        {
            return ( long )( datetime - EPOC ).TotalSeconds;
        }

        /// <summary>
        /// Convert a Unix Timestamp to a DateTime object
        /// </summary>
        /// <param name="UnixTimeStamp">Unix Timestamp</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertFromUnixTime( long UnixTimeStamp )
        {
            // Unix timestamp is seconds past epoch
            return EPOC.AddSeconds( UnixTimeStamp ).ToLocalTime();
        }
    }
}
