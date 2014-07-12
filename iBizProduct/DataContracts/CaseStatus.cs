
namespace iBizProduct.DataContracts
{
    /// <summary>
    /// Case Status 
    /// </summary>
    public enum CaseStatus
    {
        /// <summary>
        /// New Case
        /// </summary>
        NEW,

        /// <summary>
        /// Closed Case
        /// </summary>
        CLOSED,

        /// <summary>
        /// Case has been responded to by the Client
        /// </summary>
        RESPONSE,

        /// <summary>
        /// Upstream Case
        /// </summary>
        UPSTREAM,

        /// <summary>
        /// Case is with the Customer
        /// </summary>
        CUSTOMER,
        
        /// <summary>
        /// Case from Partner
        /// </summary>
        PARTNER,

        /// <summary>
        /// Case Pending
        /// </summary>
        PENDING
    }
}
