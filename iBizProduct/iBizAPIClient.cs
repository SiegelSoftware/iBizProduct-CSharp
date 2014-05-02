// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBizProduct.DataContracts;

namespace iBizProduct
{
    // TODO: Enable more control for limits and other optional parameters

    /// <summary>
    /// iBizProducts should use the iBizAPIClient to communicate with the iBizAPI. This will greatly reduce the time
    /// needed to implement a connection as the functions iBizProduct is meant to connect to are abstracted into easy to use methods. 
    /// This requires that you have configured your product with the External Key in the App Settings for your Product.
    /// </summary>
    public class iBizAPIClient
    {
        #region CommerceManager/ProductManager/ProductOrder

        /// <summary>
        /// Add a new product order to the panel.
        /// </summary>
        /// <param name="ProductId">The ID of the product you are trying to add a purchase order for</param>
        /// <param name="ProductOrderSpec">An associative array of the specifications that Panel will be tracking</param>
        /// <returns>The ProductOrder ID of the added Product Order.</returns>
        public static int ProductOrderAdd( string ProductId, ProductOrderSpec ProductOrderSpec )
        {
            VerifyExternalKey();

            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "product_id", ProductId },
                { "productorder_spec", ProductOrderSpec }
            };

            var result = iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalAdd", Params ).Result;

            return int.Parse( result[ "productorder_id" ].ToString() );
        }

        /// <summary>
        /// Edit your customer's order. This could be the pricing or the name of a productorder.
        /// </summary>
        /// <param name="ProductOrderId">The ProductOrder Id of the Product Order you wish to edit.</param>
        /// <param name="productOrderSpec">The Specifications that need to change.</param>
        /// <returns>A boolean indicating whether or not the edit was successful.</returns>
        public static bool ProductOrderEdit( string ProductOrderId, ProductOrderSpec productOrderSpec )
        {
            VerifyExternalKey();

            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "productorder_id", ProductOrderId }
            };

            var result = iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalEdit", Params ).Result;

            return ( bool )result[ "success" ];
        }

        /// <summary>
        /// Returns a ProductOrder Sub Spec as a Dictionary<string, object> for the specified Product Order.
        /// </summary>
        /// <param name="ProductOrderId"></param>
        /// <param name="InfoToReturn"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ProductOrderView( string ProductOrderId, ProductOrderInfoToReturn InfoToReturn = null )
        {
            VerifyExternalKey();

            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "productorder_id", ProductOrderId },
                { "info_to_return", InfoToReturn }
            };

            return iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalView", Params ).Result;
        }

        /* We won't be using this function for the moment. We will be implementing this at a later date.
        // TODO: Create correct Paramater List
        public static Dictionary<string, object> ProductOrderUpdateInventory( string ProductOrderId ) 
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "productorder_id", ProductOrderId }
            };

            return iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalUpdateInventory", Params ).Result;
        }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CycleBeginData"></param>
        /// <param name="CycleEndDate"></param>
        /// <param name="OneTimeCost"></param>
        /// <param name="ProductOrderId"></param>
        /// <param name="DetailAddon"></param>
        /// <param name="DescriptionAddOn"></param>
        /// <param name="DueNow"></param>
        /// <returns>BillResponse Enum Value</returns>
        public static BillResponse ProductOrderBillOrderAddOneTime( int CycleBeginData, int CycleEndDate, decimal OneTimeCost, int ProductOrderId, string DetailAddon = null, string DescriptionAddOn = null, int DueNow = -999 ) // On Demand Billing
        {
            VerifyExternalKey();

            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "productorder_id", ProductOrderId },
                { "cycle_begin_date", CycleBeginData },
                { "cycle_end_date", CycleEndDate },
                { "one_time_cost", OneTimeCost }
            };

            if( DetailAddon != null ) Params.Add( "detail_addon", DetailAddon );
            if( DescriptionAddOn != null ) Params.Add( "description_addon", DescriptionAddOn );
            if( DueNow != -999 ) Params.Add( "due_now", DueNow );

            var result = iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalBillOrderAddOneTime", Params ).Result;

            return ( BillResponse )Enum.Parse( typeof( BillResponse ), result[ "response_code" ].ToString() );
        }

        #endregion

        #region CommerceManager/ProductOffer

        /// <summary>
        /// The Product Offer Price provides a way to get a ProductOffer price, including the offer chain.
        /// </summary>
        /// <param name="ProductOfferId">The ID of the Product Offer [REQUIRED]</param>
        /// <param name="AccountHost">Your account host</param>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ProductOfferPrice( string ProductOfferId, string AccountHost, string AccountId = null ) // Used for getting the offer chain
        {
            VerifyExternalKey();

            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "account_host", AccountHost },
                { "productoffer_id", ProductOfferId }
            };

            if( AccountId != null ) Params.Add( "account_id", AccountId );

            return iBizBE.APICall( "JSON/CommerceManager/ProductOffer", "ExternalProductPrice", Params ).Result;
        }

        #endregion

        /*
 * We will not be using this for the moment. We may revisit this later.
 * 
        #region BillingManager/LedgerEntryManager

        // TODO: Create correct Paramater List
        public static Dictionary<string, object> LedgerEntryManagerProrate( string ProductOfferId, string AccountHost, string AccountId )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "account_id", AccountId },
                { "account_host", AccountHost },
                { "productoffer_id", ProductOfferId }
            };

            return iBizBE.APICall( "JSON/BillingManager/LedgerEntryManager", "ExternalProrate", Params ).Result;
        }

        #endregion

        #region CommerceManager/ProductOffer/PurchaseOrder

"JSON/CommerceManager/ProductOffer/PurchaseOrder", "ExternalAdd"
"JSON/CommerceManager/ProductOffer/PurchaseOrder", "ExternalEdit"
"JSON/CommerceManager/ProductOffer/PurchaseOrder", "ExternalListOnAccount"
"JSON/CommerceManager/ProductOffer/PurchaseOrder", "ExternalPriceFromPurchase"
"JSON/CommerceManager/ProductOffer/PurchaseOrder", "ExternalGetCycleDelimiters"

        #endregion
*/
        #region CommerceManager/ProductManager/ProductOrder/Event

        // TODO: Create correct Paramater List
        public static Dictionary<string, object> ProductOrderUpdateEvent( string ProductOfferId, string AccountHost, string AccountId ) // Update Event Que
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "account_id", AccountId },
                { "account_host", AccountHost },
                { "productoffer_id", ProductOfferId }
            };

            return iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder/Event", "ExternalUpdateEvent", Params ).Result;
        }

        #endregion

        /// <summary>
        /// This will verify that you have an External Key set in your AppSettings. If it does not exist 
        /// the Client will not be able to authenticate against the iBizAPI.
        /// </summary>
        /// <returns>True if a value exists for the External Key</returns>
        public static bool ExternalKeyExists()
        {
            return ConfigurationManager.AppSettings[ "ExternalKey" ].Length > 0;
        }

        /// <summary>
        /// This method should be called before attempting to connect to the Backend Services. If you do not have 
        /// an External Key none of the functions will work.
        /// </summary>
        private static void VerifyExternalKey()
        {
            if( !ExternalKeyExists() )
                throw new iBizException( "Your Products External Key was not found or is not accessible. Please verify that the key is set in the AppSettings " +
                                            "section of your config file. You can find the Product External Key in the Panel under the External Attributes section " +
                                            "of the ProductEdit page." );
        }
    }
}
