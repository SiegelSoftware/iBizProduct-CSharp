
namespace iBizProduct.DataContracts
{
    /// <summary>
    /// How often the Billing takes place.
    /// </summary>
    public enum PaymentFrequency
    {
        /// <summary>
        /// Billing occurs on demand
        /// </summary>
        OnDemand,

        /// <summary>
        /// Billing occurs Monthly
        /// </summary>
        Monthly,

        /// <summary>
        /// Billing occurs Quarterly
        /// </summary>
        Quarterly,

        /// <summary>
        /// Billing occurs Semi-Annually
        /// </summary>
        SemiAnnually,

        /// <summary>
        /// Billing occurs Annually
        /// </summary>
        Annually
    }
}
