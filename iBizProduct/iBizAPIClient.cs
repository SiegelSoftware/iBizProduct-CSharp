// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using iBizProduct.DataContracts;
using iBizProduct.Ultilities;
using Newtonsoft.Json.Linq;

namespace iBizProduct
{
    /// <summary>
    /// iBizProducts should use the iBizAPIClient to communicate with the iBizAPI. This will greatly reduce the time
    /// needed to implement a connection as the functions iBizProduct is meant to connect to are abstracted into easy to use methods. 
    /// This requires that you have configured your product with the External Key in the App Settings for your Product.
    /// </summary>
    public class iBizAPIClient
    {
        private static string ApiKey = iBizProductSettings.ExternalKey;

        static iBizAPIClient()
        {
            VerifyExternalKey();
        }

        #region CommerceManager/ProductManager/ProductOrder

        /// <summary>
        /// Add a new product order to the panel. You should use this when creating a new order such as when a customer
        /// clicks "Add to Cart" or "Checkout" on the PurchaseAdd page of the Panel.
        /// </summary>
        /// <param name="ProductOrderSpec">An associative array of the specifications that Panel will be tracking</param>
        /// <param name="ProductId">[Optional] ProductId. If not provided it must be available in the Environment.</param>
        /// <returns>The ProductOrder ID of the added Product Order.</returns>
        public static int ProductOrderAdd( ProductOrderSpec ProductOrderSpec, int? ProductId = null )
        {
            if( ProductId == null )
            {
                ProductId = iBizProductSettings.ProductId;
            }

            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ApiKey },
                { "product_id", ProductId },
                { "productorder_spec", ProductOrderSpec.OrderSpec() }
            };

            var result = iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalAdd", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            return int.Parse( result[ "productorder_id" ].ToString() );
        }

        /// <summary>
        /// Edit your customer's order. This could be the pricing or the name of a productorder.
        /// </summary>
        /// <param name="ProductOrderId">The ProductOrder Id of the Product Order you wish to edit.</param>
        /// <param name="productOrderSpec">The Specifications that need to change.</param>
        /// <returns>A boolean indicating whether or not the edit was successful.</returns>
        public static bool ProductOrderEdit( int ProductOrderId, ProductOrderSpec productOrderSpec )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ApiKey },
                { "productorder_id", ProductOrderId },
                { "productorder_spec", productOrderSpec.OrderSpec() }
            };

            var result = iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder", "ExternalEdit", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            return true;
        }

        /// <summary>
        /// Used for Viewing information about a particular Purchase Order.
        /// </summary>
        /// <param name="ProductOrderId">Product Order ID</param>
        /// <param name="InfoToReturn">Optional ProductOfferInfoToReturn</param>
        /// <returns>Dictionary&lt;string, object&gt; with the requested View Object</returns>
        public static JObject ProductOrderView( int ProductOrderId, ProductOrderInfoToReturn InfoToReturn = null )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ApiKey },
                { "productorder_id", ProductOrderId },
            };

            if( InfoToReturn != null ) Params.Add( "info_to_return", InfoToReturn );

            var view = iBizBE.APICall( "CommerceManager/ProductManager/ProductOrder", "ExternalView", Params ).Result;

            if( view[ "error" ] != null ) throw new iBizException( view[ "error" ].ToString() );

            return view;
        }

        /// <summary>
        /// This will instruct the Panel to make a one time charge to a client. This could be due to a customer modifying the terms of 
        /// their services with your product or an On Demand payment.
        /// </summary>
        /// <param name="CycleBeginData"></param>
        /// <param name="CycleEndDate"></param>
        /// <param name="OneTimeCost"></param>
        /// <param name="ProductOrderId"></param>
        /// <param name="DetailAddon"></param>
        /// <param name="DescriptionAddOn"></param>
        /// <param name="DueNow"></param>
        /// <returns>BillResponse Enum Value</returns>
        public static BillResponse ProductOrderBillOrderAddOneTime( DateTime CycleBeginData, DateTime CycleEndDate, decimal OneTimeCost, int ProductOrderId, string DetailAddon = null, string DescriptionAddOn = null, bool DueNow = true )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", ApiKey },
                { "productorder_id", ProductOrderId },
                { "cycle_begin_date", UnixTime.ConvertToUnixTime( CycleBeginData ) },
                { "cycle_end_date", UnixTime.ConvertToUnixTime( CycleEndDate ) },
                { "one_time_cost", OneTimeCost }
            };

            if( DetailAddon != null ) Params.Add( "detail_addon", DetailAddon );
            if( DescriptionAddOn != null ) Params.Add( "description_addon", DescriptionAddOn );
            Params.Add( "due_now", ( ( DueNow == true ) ? 1 : 0 ).ToString() );

            var result = iBizBE.APICall( "CommerceManager/ProductManager/ProductOrder", "ExternalBillOrderAddOneTime", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            var ResponseCode = int.Parse( result[ "response_code" ].ToString() );

            return ( BillResponse )ResponseCode;            
        }

        /// <summary>
        /// Get the next date the ProductOrder will be charged for by the Panel
        /// </summary>
        /// <param name="ProductOrderId">ProductOrder Id to lookup</param>
        /// <returns>DateTime of the next charge</returns>
        public static DateTime GetNextChargeDate( int ProductOrderId )
        {
            var Params = new Dictionary<string, object>() 
            {
                { "external_key", ApiKey },
                { "productorder_id", ProductOrderId }
            };

            var result = iBizBE.APICall( "CommerceManager/ProductManager/ProductOrder", "ExternalGetNextChargeDate", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            var ChargeDate = new DateTime();

            int NextChargeDate;
            if( !int.TryParse( result[ "next_charge_date" ].ToString(), out NextChargeDate ) )
                throw new iBizException( "Unable to Parse Next Charge Date" );

            return ChargeDate.ConvertFromUnixTime( NextChargeDate );
        }

        /// <summary>
        /// Get the language of the account that ownes the provided order.
        /// </summary>
        /// <param name="ProductOrderId">The id of the productorder to get the language for.</param>
        /// <returns>The language of the owner.</returns>
        public static Language GetOwnerLanguage( int ProductOrderId )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() 
            {
                { "external_key", ApiKey },
                { "productorder_id", ProductOrderId }
            };

            var result = iBizBE.APICall( "CommerceManager/ProductManager/ProductOrder", "ExternalGetOwnerLanguage", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            return (Language)Enum.Parse( typeof( Language ), result[ "language " ].ToString() );
        }

        /// <summary>
        /// Return the pricing for the provided product order ID (basically looks up the purchaser and calls ExternalPriceFromPurchase).
        /// </summary>
        /// <param name="ProductOrderId">This is the id of the ProductOrder to generate the list of dependent orders for.</param>
        /// <returns>Product Details and Offer Chain</returns>
        public static OrderPricing ProductOrderPricing( int ProductOrderId )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() 
            {
                { "external_key", ApiKey },
                { "productorder_id", ProductOrderId }
            };

            var result = iBizBE.APICall( "CommerceManager/ProductManager/ProductOrder", "ExternalProductOrderPricing", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            return new OrderPricing( result );
        }

        /// <summary>
        /// Open a case with the owner (end user) of a particular order. (Note: Use $$OFFER_NAME$$ in case fields to substitute the name of the offer at the level of owner.)
        /// </summary>
        /// <param name="ProductOrderId">The id of the productorder to open a case for.</param>
        /// <param name="CaseSpec">The spec for the case added</param>
        /// <returns>The id of the case added</returns>
        public static int ProductOpenCaseWithOwner( int ProductOrderId, CaseSpec CaseSpec )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() 
            {
                { "external_key", ApiKey },
                { "productorder_id", ProductOrderId },
                { "case_spec", CaseSpec.GetSpec() }
            };

            var result = iBizBE.APICall( "CommerceManager/ProductManager/ProductOrder", "ExternalOpenCaseWithOwner", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            return int.Parse( result[ "new_id" ].ToString() );
        }

        /// <summary>
        /// Update a case with the owner (end user) of a particular order (usually a case previously created with ExternalOpenCaseWithOwner). 
        /// (Note: Use $$OFFER_NAME$$ in case fields to substitute the name of the offer at the level of owner.)
        /// </summary>
        /// <param name="ProductOrderId">The id of the productorder to update a case for.</param>
        /// <param name="CaseId">The id of the case to update.</param>
        /// <param name="CaseSpec">The spec for the case edited</param>
        /// <returns>The affected rows</returns>
        public static int ProductUpdateCaseWithOwner( int ProductOrderId, int CaseId, CaseSpec CaseSpec )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() 
            {
                { "external_key", ApiKey },
                { "productorder_id", ProductOrderId },
                { "case_id", CaseId },
                { "case_spec", CaseSpec.GetSpec() }
            };

            var result = iBizBE.APICall( "CommerceManager/ProductManager/ProductOrder", "ExternalUpdateCaseWithOwner", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            return int.Parse( result[ "effected_rows " ].ToString() );
        }

        #endregion

        #region CommerceManager/ProductManager/ProductOrder/Event

        /// <summary>
        /// This function allows you to notify iBizAPI of Event Updates on Event requests it has
        /// sent. 
        /// </summary>
        /// <remarks>
        /// This does not affect backoff requests. Backoff requests must be handled by the Product's 
        /// API endpoint Order/{Object}/{Action}
        /// </remarks>
        /// <param name="EventId">The id of the productorder that we're going to update.</param>
        /// <param name="Status">The status for the event, COMPLETE or ERROR.</param>
        /// <param name="Message">A message to include with errors (this is customer visable).</param>
        /// <returns>Indicates whether or not the update was successful</returns>
        public static bool UpdateEvent( int EventId, EventStatus Status, string Message = null )
        {
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", iBizProductSettings.ExternalKey },
                { "productorderevent_id", EventId },
                { "status", Status.ToString() }
            };

            if( !String.IsNullOrEmpty( Message ) ) Params.Add( "message", Message );

            var result = iBizBE.APICall( "JSON/CommerceManager/ProductManager/ProductOrder/Event", "ExternalUpdateEvent", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            return int.Parse( result[ "success" ].ToString() ) == 1 ? true : false;
        }

        #endregion

        #region CommerceManager/ProductOffer

        public static OfferPrice GetPriceByOffer( int OfferId, bool Markup = false, int AccountId = 0 )
        {
            // Need Backend to accept Markup Flag
            Dictionary<string, object> Params = new Dictionary<string, object>() {
                { "external_key", iBizProductSettings.ExternalKey },
                { "productoffer_id", OfferId }
            };

            if( AccountId > 0 ) Params.Add( "account_id", AccountId );

            var result = iBizBE.APICall( "JSON/CommerceManager/ProductOffer", "ExternalProductOfferPrice", Params ).Result;

            if( result[ "error" ] != null ) throw new iBizException( result[ "error" ].ToString() );

            return new OfferPrice( result );
        }

        #endregion

        #region Helper Functions
        /// <summary>
        /// This method sets the ProductAuthentication variables to the Session for the Product to be able to track
        /// and handle. It also verifies that the session the user has is valid.
        /// </summary>
        /// <param name="authentication">Authentication Paramaters</param>
        /// <returns>Boolean to indicate a valid session</returns>
        public static bool AuthenticateUser( ProductAuthentication authentication )
        {
            //if( HttpContext.Current.Session["Token"] != null )
            {
                // Check the SessionID Passed and validate that you have a valid Panel session
                Dictionary<string, object> Params = new Dictionary<string, object>()
                {
                    { "session_id", authentication.SessionID }
                };

                //var VerifyAccount = iBizBE.APICall( "JSON/AccountManager/AAA", "ViewAccountID", Params );

                // We only load MyAccountID and the Token here. NOTE: We do not want to track the Panel Session ID.
                // This would encourage developers to use non-external calls beyond what we are making here.
                HttpContext.Current.Session[ "MyAccountID" ] = authentication.MyAccountID;
                HttpContext.Current.Session[ "Token" ] = Convert.ToBase64String( Guid.NewGuid().ToByteArray() );
            }

            // We will always load the follow here as they may have changed.
            HttpContext.Current.Session.Add( "AccountID", authentication.AccountID );
            HttpContext.Current.Session.Add( "ProductOrderID", authentication.ProductOrderID );
            HttpContext.Current.Session.Add( "OfferID", authentication.OfferID );
            HttpContext.Current.Session.Add( "Language", authentication.Language );

            return true;
        }

        /// <summary>
        /// Verify that the External Key Sent by the Backend is Valid for iBizProduct
        /// </summary>
        /// <param name="ExternalKey"></param>
        /// <returns></returns>
        public static bool IsValidBackendRequest( string ExternalKey )
        {
            return ExternalKey == iBizProductSettings.ExternalKey;
        }

        /// <summary>
        /// This will verify that you have an External Key set in your AppSettings. If it does not exist 
        /// the Client will not be able to authenticate against the iBizAPI.
        /// </summary>
        /// <returns>True if a value exists for the External Key</returns>
        public static bool ExternalKeyExists()
        {
            return !String.IsNullOrEmpty( iBizProductSettings.ExternalKey );
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
                                            "of the ProductEdit page.", new SettingsPropertyNotFoundException( "The setting you are looking for is not available or does not exist within the current scope of the application. Please refer to the setup documentation for required systems configurations." ) );
        }
        #endregion
    }
}
