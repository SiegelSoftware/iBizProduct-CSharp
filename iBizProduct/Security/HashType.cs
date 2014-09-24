
namespace iBizProduct.Security
{
    /// <summary>
    /// This provides a context of Supported Encryption Methods
    /// </summary>
    public enum HashType
    {
        /// <summary>
        /// No Encryption Used.
        /// </summary>
        None = 0,

        /// <summary>
        /// Use SHA Encryption with 256 Bits.
        /// </summary>
        SHA256 = 1,

        /// <summary>
        /// Use SHA Encryption with 512 Bits.
        /// </summary>
        SHA512 = 2,
    }
}
