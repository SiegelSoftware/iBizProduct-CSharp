using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace iBizProduct.DataContracts
{
    public class OrderPricing
    {
        public OrderPricing( JObject obj )
        {
            this.BillingCycle = obj[ "billing_cycle" ].ToString();
            this.CurrentCycle = obj[ "current_cycle" ].ToString();
            this.NextChargeDate = obj[ "next_charge_date" ].ToString();
            this.OfferChain = new List<OfferChainLink>();
            this.ProductCycle = obj[ "product_cycle" ].ToString();
            this.ProductId = Convert.ToInt32( obj[ "product_id" ] );

            foreach( JObject details in obj["offer_chain"])
            {
                var link = new OfferChainLink( details );
                OfferChain.Add( link );
            }
        }

        public string BillingCycle { get; set; }
        public string CurrentCycle { get; set; }
        public string NextChargeDate { get; set; }
        public List<OfferChainLink> OfferChain { get; set; }
        public string ProductCycle { get; set; }
        public int ProductId { get; set; }

        public decimal Markup( decimal BaseCost, PaymentFrequency PaymentType )
        {
            var finalCost = BaseCost;

            foreach( var link in this.OfferChain )
            {
                switch( PaymentType )
                {
                    case PaymentFrequency.OnDemand:
                        if( link.AvailableMonthly == "Yes" )
                        {

                        }
                        break;
                    case PaymentFrequency.Monthly:
                        break;
                    case PaymentFrequency.Quarterly:
                        break;
                    case PaymentFrequency.SemiAnnually:
                        break;
                    case PaymentFrequency.Annually:
                        break;
                }
            }

            return finalCost;
        }
    }
}
