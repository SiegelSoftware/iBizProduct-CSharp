
namespace iBizProduct.Models.Templates
{
    /// <summary>
    /// This provides a base model for a ProductOrder. This may be applied to any model
    /// within the DbContext of your product which will be tracking the specific ProductOrder.
    /// </summary>
    public abstract class ProductOrderBase
    {
        /// <summary>
        /// ProductOrderId of the specific order. 
        /// </summary>
        public int ProductOrderId { get; set; }

        /// <summary>
        /// ProductId of the ProductOrder. This allows for Marketplace Implementation.
        /// </summary>
        public int ProductId { get; set; }
    }
}
