using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace iBizProduct.DataContracts
{
    /// <summary>
    /// This represents the Order Pricing object returned by the backend
    /// </summary>
    public class OrderPricing
    {
        /// <summary>
        /// Default constructor handles constructing the object from the backend response.
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// Current Billing Cycle
        /// </summary>
        public string BillingCycle { get; set; }

        /// <summary>
        /// Current Cycle
        /// </summary>
        public string CurrentCycle { get; set; }
        public string NextChargeDate { get; set; }
        public List<OfferChainLink> OfferChain { get; set; }
        public string ProductCycle { get; set; }

        /// <summary>
        /// ProductId
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Handles converting a base Product Price to the final price a customer should see.
        /// </summary>
        /// <param name="BaseCost">Base Product Price</param>
        /// <param name="PaymentType">Payment Frequency</param>
        /// <param name="IsSetup">If the cost you are looking for is for the Setup cost this must be specified true. False by default.</param>
        /// <returns>Final Marked up Price or Setup Cost</returns>
        public decimal Markup( decimal BaseCost, PaymentFrequency PaymentType, bool IsSetup = false )
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
