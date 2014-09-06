// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Globalization;
using iBizProduct.DataContracts;

namespace iBizProduct.Ultilities
{
    /// <summary>
    /// String Extension Methods
    /// </summary>
    public static class StringExtenstions
    {
        /// <summary>
        /// Formats the string with current culture.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The list of arguments.</param>
        /// <returns>Formatted string using the current culture</returns>
        public static string FormatCurrentCulture( this string format, params object[] args )
        {
            return string.Format( CultureInfo.CurrentCulture, format, args );
        }

        /// <summary>
        /// Formats the string with invariant culture.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The list of arguments.</param>
        /// <returns>Formatted string using the invariant culture</returns>
        public static string FormatInvariantCulture( this string format, params object[] args )
        {
            return string.Format( CultureInfo.InvariantCulture, format, args );
        }

        /// <summary>
        /// Formats the string with an Uppercase First Letter.
        /// </summary>
        /// <param name="String"></param>
        /// <returns>Formatted string</returns>
        public static string UppercaseFirst( this string String )
        {
            return char.ToUpper( String[ 0 ] ) + String.Substring( 1 ).ToLower();
        }

        /// <summary>
        /// Converts the Event Action recieved by the Backend API to an EventAction Enum
        /// </summary>
        /// <param name="OrderAction">Event Action string</param>
        /// <returns>EventAction Enum</returns>
        public static EventActions ConvertToEventAction( this string OrderAction )
        {
            OrderAction = OrderAction.Split( 'O' )[ 0 ];
            return ( EventActions )Enum.Parse( typeof( EventActions ), OrderAction, true );
        }
    }
}
