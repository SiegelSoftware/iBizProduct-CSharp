using System;

namespace iBizProduct
{
    public class MarketplaceException : Exception
    {
        public MarketplaceException( string Message )
            : base( Message )
        {

        }

        public MarketplaceException( string SettingName, string Message )
            : this( Message )
        {

        }

        public MarketplaceException( string SettingName, int ProductId )
        {

        }
    }
}
