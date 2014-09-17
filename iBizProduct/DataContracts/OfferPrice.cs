using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace iBizProduct.DataContracts
{
    public class OfferPrice
    {
        public OfferPrice() { }
        public OfferPrice( JObject Response )
        {
            this.OnDemandPrice = Convert.ToDecimal( Response[ "price_m" ] );
            this.MonthlyPrice = Convert.ToDecimal( Response[ "price_m" ] );
            this.QuarterlyPrice = Convert.ToDecimal( Response[ "price_q" ] );
            this.SemiAnnualPrice = Convert.ToDecimal( Response[ "price_s" ] );
            this.AnnualPrice = Convert.ToDecimal( Response[ "price_a" ] );
        }

        public decimal OnDemandPrice { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal QuarterlyPrice { get; set; }
        public decimal SemiAnnualPrice { get; set; }
        public decimal AnnualPrice { get; set; }
    }
}
