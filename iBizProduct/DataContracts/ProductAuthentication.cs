using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace iBizProduct.DataContracts
{
    public class ProductAuthentication// : IDisposable
    {
        //[ DefaultValue( HttpContext.Current.Request.Params["Action"] ) ]
        public string Action { get; set; }

        //[DefaultValue( HttpContext.Current.Request.Params[ "SessionID" ] )]
        public string SessionID { get; set; }

        //[DefaultValue( HttpContext.Current.Request.Params[ "Language" ] )]
        public string Language { get; set; }
        public int MyAccountID { get; set; }
        public int AccountID { get; set; }
        public int OfferID { get; set; }
        public int ProductOrderID { get; set; }

    }
}
