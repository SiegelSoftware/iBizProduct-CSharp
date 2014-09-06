
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
        /// The recurring cost of the Product Order
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// The setup cost of the Product Order
        /// </summary>
        public decimal Setup { get; set; }

        /// <summary>
        /// This is the name that you should use when you create the Product Order with the Panel.
        /// </summary>
        public string FriendlyName { get; set; }
    }
}
