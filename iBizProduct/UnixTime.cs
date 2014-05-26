using System;

namespace iBizProduct
{
    public class UnixTime
    {
        static DateTime EPOC = new DateTime( 1970, 1, 1, 0, 0, 0 );
        private DateTime ThisTime;

        public UnixTime()
        {
            ThisTime = DateTime.Now;
        }

        public UnixTime( int year, int month, int day )
        {
            ThisTime = new DateTime( year, month, day );
        }

        public UnixTime( int year, int month, int day, int hour, int minute, int second )
        {
            ThisTime = new DateTime( year, month, day, hour, minute, second );
        }

        public UnixTime( DateTime datetime )
        {
            ThisTime = datetime;
        }

        public long GetUnixTime()
        {
            var timeSpan = ( ThisTime - EPOC );
            return ( long )timeSpan.TotalSeconds;
        }

        public static long ConvertToUnixTime( DateTime datetime )
        {
            return ( long )( datetime - EPOC ).TotalSeconds;
        }

        public static DateTime ConvertFromUnixTime( long UnixTimeStamp )
        {
            // Unix timestamp is seconds past epoch
            return EPOC.AddSeconds( UnixTimeStamp ).ToLocalTime();
        }
    }
}
