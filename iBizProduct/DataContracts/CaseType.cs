
namespace iBizProduct.DataContracts
{
    /// <summary>
    /// Type of case
    /// </summary>
    public enum CaseType
    {
        /// <summary>
        /// Case was opened for a Question
        /// </summary>
        QUESTION,

        /// <summary>
        /// There is a problem
        /// </summary>
        PROBLEM,

        /// <summary>
        /// Case was opened for a comment
        /// </summary>
        COMMENT,

        /// <summary>
        /// Case was opened to Inform Client
        /// </summary>
        INFO,

        /// <summary>
        /// Case was opened to followup with the Client
        /// </summary>
        FOLLOWUP,

        /// <summary>
        /// Private Case
        /// </summary>
        PRIVATE
    }
}
