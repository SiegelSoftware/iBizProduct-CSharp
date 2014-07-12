
namespace iBizProduct.DataContracts
{
    /// <summary>
    /// Response from the Backend for Billing Requests.
    /// </summary>
    public enum BillResponse
    {
        /// <summary>
        /// An error occured. You will need to try again.
        /// </summary>
        FAIL = 0,

        /// <summary>
        /// The Billing Charge was Successful
        /// </summary>
        SUCCESS = 1,

        /// <summary>
        /// The request was marked as due now, but will be charged later.
        /// </summary>
        REQUESTED = 2
    }
}
