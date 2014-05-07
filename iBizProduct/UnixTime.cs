using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBizProduct
{
    public class UnixTime
    {
        private DateTime EPOC = new DateTime( 1970, 1, 1, 0, 0, 0 );
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

        public static long GetUnixTime(DateTime datetime)
        {
            DateTime EPOC = new DateTime( 1970, 1, 1, 0, 0, 0 );
            var timeSpan = ( datetime - EPOC );
            return ( long )timeSpan.TotalSeconds;
        }
    }
}
