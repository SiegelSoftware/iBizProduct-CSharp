
namespace iBizProduct.DataContracts
{
    /// <summary>
    /// Denotes an Offers Markup Type or Pricing Model
    /// </summary>
    public enum PricingModel
    {
        /// <summary>
        /// Provides a FLAT markup price. i.e. The base cost is $5.00
        /// and the FLAT markup is $2.00. The total cost would be
        /// $7.00 to the end user.
        /// </summary>
        FLAT,

        /// <summary>
        /// Provides a Percentage markup. i.e. The base cost is $5.00
        /// and the PERCENT markup is 10%. The total cost would be
        /// $5.50 to the end user.
        /// </summary>
        PERCENT,

        /// <summary>
        /// Provides a FIXED price. i.e. The base cost is $5.00
        /// and the FIXED price is $6.00. The total cost would be
        /// $6.00 to the end user.
        /// </summary>
        FIXED
    }
}
