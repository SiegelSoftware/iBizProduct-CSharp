
namespace iBizProduct.Models.Templates
{
    public class MarketplaceProductOrderBase : ProductOrderBase
    {
        /// <summary>
        /// ProductId of the ProductOrder. This allows for Marketplace Implementation.
        /// </summary>
        public int ProductId { get; set; }
    }
}
