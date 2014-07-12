
namespace iBizProduct.DataContracts
{
    /// <summary>
    /// Event Status
    /// </summary>
    public enum EventStatus
    {
        /// <summary>
        /// The Event has completed processing.
        /// </summary>
        COMPLETE,

        /// <summary>
        /// There was an issue processing the event. This could be a recoverable or unrecoverable error.
        /// </summary>
        ERROR
    }
}
