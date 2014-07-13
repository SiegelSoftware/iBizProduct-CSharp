
namespace iBizProduct.DataContracts
{
    /// <summary>
    /// Product Authentication Object
    /// </summary>
    public class ProductAuthentication// : IDisposable
    {
        /// <summary>
        /// Action to perform. ( i.e. PurchaseAdd => Add, PurchaseEdit => Edit
        /// </summary>
        public AuthenticationAction Action { get; set; }

        /// <summary>
        /// Session Id to verify the request has a valid backend session
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// The language the Panel is currently being viewed in
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The AccountId of the user who is logged in.
        /// </summary>
        public int MyAccountID { get; set; }

        /// <summary>
        /// The AccountId the user is drilled down to ( viewing )
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        /// The Offer Id
        /// </summary>
        public int OfferID { get; set; }

        /// <summary>
        /// The ProductOrderId, the Product should use when necessary. 
        /// </summary>
        public int ProductOrderID { get; set; }

    }
}
