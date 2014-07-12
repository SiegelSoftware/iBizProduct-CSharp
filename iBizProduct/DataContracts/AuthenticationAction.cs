
namespace iBizProduct.DataContracts
{
    /// <summary>
    /// Authentication Action
    /// </summary>
    public enum AuthenticationAction
    {
        /// <summary>
        /// Create a Product Order and add the Purchase Order to the Cart
        /// </summary>
        ADD,

        /// <summary>
        /// Edit an exisitng Product Order
        /// </summary>
        EDIT,

        /// <summary>
        /// Edit a specific Offer
        /// </summary>
        OFFER,

        /// <summary>
        /// Edit/Manage the Product
        /// </summary>
        PRODUCT
    }
}
