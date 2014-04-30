using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBizProduct
{
    // TODO: Enable more control for limits and other optional parameters
    public class iBizAPIClient
    {
        #region CommerceManager/ProductManager/ProductOrder

        public static Dictionary<string, object> ProductOrderView( string ProductOrderId )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "productorder_id", ProductOrderId }
            };

            return iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalView", Params ).Result;
        }

        public static Dictionary<string, object> ProductOrderAdd( string ProductOrderId )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "productorder_id", ProductOrderId }
            };

            return iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalAdd", Params ).Result;
        }

        public static Dictionary<string, object> ProductOrderEdit( string ProductOrderId )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "productorder_id", ProductOrderId }
            };

            return iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalEdit", Params ).Result;
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

        // TODO: Create correct Paramater List
        public static Dictionary<string, object> ProductOrderBillOrderAddOneTime( string ProductOrderId ) // On Demand Billing
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "productorder_id", ProductOrderId }
            };

            return iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalBillOrderAddOneTime", Params ).Result;
        }

        #endregion

        #region CommerceManager/ProductOffer

        public static Dictionary<string, object> ProductOfferPrice( string ProductOfferId, string AccountHost, string AccountId ) // Used for getting the offer chain
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ConfigurationManager.AppSettings[ "ExternalKey" ] },
                { "account_id", AccountId },
                { "account_host", AccountHost },
                { "productoffer_id", ProductOfferId }
            };

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
    }
}
