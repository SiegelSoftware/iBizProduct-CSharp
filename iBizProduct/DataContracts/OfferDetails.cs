using System;
using Newtonsoft.Json.Linq;

namespace iBizProduct.DataContracts
{
    public class OfferChainLink
    {
        public OfferChainLink( JObject Obj )
        {
            this.OfferCurrency = Obj[ "OFFER_CURRENCY" ].ToString();
            this.BaseCycle = Obj[ "base_cycle" ].ToString();
            this.AnnualBase = Convert.ToDecimal( Obj[ "base_cycle" ] );
            this.SemiAnnualBase = Convert.ToDecimal( Obj[ "base_price_s" ] );
            this.QuarterlyBase = Convert.ToDecimal( Obj[ "base_price_q" ] );
            this.MonthlyBase = Convert.ToDecimal( Obj[ "base_price_m" ] );
            this.SetupBase = Convert.ToDecimal( Obj[ "base_setup" ] );
            this.AvailableAnnually = Obj[ "price_a" ].ToString();
            this.AvailableSemiAnnually = Obj[ "price_s" ].ToString();
            this.AvailableQuarterly = Obj[ "price_q" ].ToString();
            this.AvailableMonthly = Obj[ "price_m" ].ToString();
            this.OfferDetails = Obj[ "offer_details" ].ToString();
            this.AnnualMarkup = Convert.ToDecimal( Obj[ "price_a" ] );
            this.SemiAnnualMarkup = Convert.ToDecimal( Obj[ "price_s" ] );
            this.QuarterlyMarkup = Convert.ToDecimal( Obj[ "price_q" ] );
            this.MonthlyMarkup = Convert.ToDecimal( Obj[ "price_m" ] );
            this.PricingModel = ( PricingModel )Enum.Parse( typeof( PricingModel ), Obj[ "pricing_model" ].ToString() );
            this.ProductOfferId = Convert.ToInt32( Obj[ "productoffer_id" ] );
            this.ProductOfferName = Obj[ "productoffer_name" ].ToString();
            this.Setup = Convert.ToDecimal( Obj[ "setup" ] );
        }

        /// <summary>
        /// The string that indicates the offer currency
        /// </summary>
        public string OfferCurrency { get; set; }

        /// <summary>
        /// The base cycle for this offer
        /// </summary>
        public string BaseCycle { get; set; }

        /// <summary>
        /// The base price if the chosen billing cycle is anually
        /// </summary>
        public decimal AnnualBase { get; set; }

        /// <summary>
        /// The base price if the chosen billing cycle is semi anually
        /// </summary>
        public decimal SemiAnnualBase { get; set; }

        /// <summary>
        /// The base price if the chosen billing cycle is quarterly
        /// </summary>
        public decimal QuarterlyBase { get; set; }

        /// <summary>
        /// The base price if the chosen billing cicle is monthly
        /// </summary>
        public decimal MonthlyBase { get; set; }

        /// <summary>
        /// The base setup for this offer
        /// </summary>
        public decimal SetupBase { get; set; }

        /// <summary>
        /// Indicates if this offer is available in anually cycle.
        /// </summary>
        public string AvailableAnnually { get; set; }

        /// <summary>
        /// Indicates if this offer is available in semi anually cycle.
        /// </summary>
        public string AvailableSemiAnnually { get; set; }

        /// <summary>
        /// Indicates if this offer is available in quarterly cycle.
        /// </summary>
        public string AvailableQuarterly { get; set; }

        /// <summary>
        /// Indicates if this offer is available in monthly cycle.
        /// </summary>
        public string AvailableMonthly { get; set; }

        /// <summary>
        /// Aditional info for this offer
        /// </summary>
        public string OfferDetails { get; set; }

        /// <summary>
        /// The markup that will be applied if the chosen billing cycle is anually
        /// </summary>
        public decimal AnnualMarkup { get; set; }

        /// <summary>
        /// The markup that will be applied if the chosen billing cycle is semi anually
        /// </summary>
        public decimal SemiAnnualMarkup { get; set; }

        /// <summary>
        /// The markup that will be applied if the chosen billing cycle is quarterly
        /// </summary>
        public decimal QuarterlyMarkup { get; set; }

        /// <summary>
        /// The markup that will be applied if the chosen billing cycle is monthly
        /// </summary>
        public decimal MonthlyMarkup { get; set; }

        /// <summary>
        /// The pricing model applied to this offer ('FLAT', 'PERCENT', 'FIXED')
        /// </summary>
        public PricingModel PricingModel { get; set; }

        /// <summary>
        /// The id of this offer
        /// </summary>
        public int ProductOfferId { get; set; }

        /// <summary>
        /// The offer's name
        /// </summary>
        public string ProductOfferName { get; set; }

        /// <summary>
        /// The setup markup for this offer
        /// </summary>
        public decimal Setup { get; set; }
    }
}
